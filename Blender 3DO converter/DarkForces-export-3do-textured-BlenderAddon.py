bl_info = {
    "name": "Dark Forces 3DO Exporter",
    "description": "Export Dark Forces 3DO models",
    "author": "JerethK",
    "blender": (3, 4, 0),
    "category": "Import-Export",
}


import bpy
import bmesh

# CREATE 3DO DATA FROM BLENDER MESH OBJECTS
def create_3do_data(data):
    objects = []
    textures = []
    first_texture_index = 0

    for obj in data.objects:
        if obj.type != 'MESH':
            continue

        if obj.hide_get() == True:
            continue

        mesh = obj.data
        
        # Materials / textures
        this_obj_textures = []
        for mat in mesh.materials:
            this_obj_textures.append(mat.name)
            
        textures.append(this_obj_textures)
        
        # Vertices
        mesh_vertices = []
        for v in mesh.vertices:
            mesh_vertices.append((
                v.co[0],
                v.co[1],
                v.co[2]
            ))

        # Determine if polygons can be quads (all polygons have 4 vertices)
        mesh_copy = mesh.copy()
        is_quads = True
        poly_type = 'QUADS'
        for p in mesh.polygons:
            if len(p.vertices) != 4:
                is_quads = False
                poly_type = 'TRIANGLES'
                break
            
        # Triangulate mesh if not quads
        if not is_quads:
            bm = bmesh.new()
            bm.from_mesh(mesh_copy)
            bmesh.ops.triangulate(bm, faces=bm.faces)
            bm.to_mesh(mesh_copy)
            bm.free()
            
        # Now have to iterate through materials (textures) and make one object for the polygons which use each one
        for mat_index in range(len(mesh.materials)):
            this_object = dict()
            this_object['name'] = "{}.{}".format(mesh.name, mat_index)
            this_object['vertices'] = mesh_vertices
            this_object['num_vertices'] = len(mesh.vertices)
            this_object['poly_type'] = poly_type
            this_object['polygons'] = []
            this_object['tex_vertices'] = []
            this_object['tex_polygons'] = []
            
            colour_counter = 0        # use this for coloring
            tvert_counter = 0
            for p in mesh_copy.polygons:
                if p.material_index == mat_index:
                    this_poly = dict()
                    this_poly['vertices'] = []
                    for v in p.vertices:
                        this_poly['vertices'].insert(0, v)  # vertex order is reversed in Blender (anti-clockwise)
                        
                    this_poly['colour'] = 40 + colour_counter  # greys
                    this_poly['shading'] = "texture"
                    this_object['polygons'].append(this_poly)

                    colour_counter += 1
                    if colour_counter == 4:
                        colour_counter = 0

                    if not len(mesh_copy.uv_layers):
                        continue

                    # Texture data for polygon
                    tp_verts = []
                    for l in p.loop_indices:
                        existing_tv = -1
                        
                        # Search for an existing texture vertex that matches; if there is none, a new one will be added
                        for tv in range(len(this_object['tex_vertices'])):
                            if mesh_copy.uv_layers[0].data[l].uv[0] == this_object['tex_vertices'][tv][0] and mesh_copy.uv_layers[0].data[l].uv[1] == this_object['tex_vertices'][tv][1]:
                                existing_tv = tv
                                break
                                
                        if existing_tv >= 0:
                            tp_verts.insert(0, existing_tv)     # reverse order                        
                        else:
                            this_object['tex_vertices'].append((
                                mesh_copy.uv_layers[0].data[l].uv[0],
                                mesh_copy.uv_layers[0].data[l].uv[1]
                            ))
                            tp_verts.insert(0, tvert_counter)       # reverse order
                            tvert_counter += 1
                            
                    this_object['tex_polygons'].append(tp_verts)
                
            this_object['texture'] = mat_index + first_texture_index
            this_object['num_polygons'] = len(this_object['polygons'])
            objects.append(this_object)

        data.meshes.remove(mesh_copy)
        first_texture_index += len(this_obj_textures)
        
    return (objects, textures)


# FUNCTION TO WRITE 3DO FILE
def write_3do(context, filepath, objects, textures):
    # Calculate total vertices and polygons in 3DO
    total_vertices = 0
    total_polys = 0
    for o in objects:
        total_vertices += o['num_vertices']
        total_polys += o['num_polygons']

    with open(filepath, 'w') as file:
        num_objects = len(objects)
        
        file.write("3DO 1.2 \n")
        file.write("#\n# Created by Jereth's Blender exporter \n#\n")
        file.write("3DONAME " + filepath.split("\\")[-1] + "\n")
        file.write("OBJECTS {} \n".format(num_objects))
        file.write("VERTICES {} \n".format(total_vertices))
        file.write("POLYGONS {} \n".format(total_polys))
        file.write("PALETTE DEFAULT.PAL\n\n")
        file.write("TEXTURES {}\n".format(num_objects))
        
        # Texture table
        counter = 0
        for obj_textures in textures:
            for t in obj_textures:
                file.write(" TEXTURE: {}.bm \t#{} \n".format(t, counter))
                counter += 1
            
        file.write("\n")
        
        for o in objects:
            file.write("#---------------------------------------------------------------------\n")
            file.write("OBJECT \"{}\" \n".format(o['name']))
            file.write("TEXTURE {} \n\n".format(o['texture']))
            
            file.write("VERTICES {} \n".format(o['num_vertices']))
            file.write("# \tnum: \t  x \t  y \t  z \n")
            for (n, v) in enumerate(o['vertices']):
                file.write("\t{}:\t{:.3f}\t{:.3f}\t{:.3f}\n".format(n, v[0], -v[2], v[1]))
                
            file.write("\n{} {} \n".format(o['poly_type'], o['num_polygons']))
            if o['poly_type'] == 'TRIANGLES':
                file.write("# \tnum:\t<a>\t<b>\t<c>\t<color>\t<shading> \n")
            else:
                file.write("# \tnum:\t<a>\t<b>\t<c>\t<d>\t<color>\t<shading> \n")

            for (n, p) in enumerate(o['polygons']):
                if o['poly_type'] == 'TRIANGLES':
                    file.write("\t{}:\t{}\t{}\t{}\t{}\t{}\n".format(n, p['vertices'][0], p['vertices'][1], p['vertices'][2], p['colour'], p['shading']))
                else:
                    file.write("\t{}:\t{}\t{}\t{}\t{}\t{}\t{}\n".format(n, p['vertices'][0], p['vertices'][1], p['vertices'][2], p['vertices'][3], p['colour'], p['shading']))
            
            file.write("\nTEXTURE VERTICES {} \n".format(len(o['tex_vertices'])))
            for (n, tv) in enumerate(o['tex_vertices']):
                file.write("\t{}:\t{:.3f}\t{:.3f}\n".format(n, tv[0], tv[1]))
            
            file.write("\nTEXTURE {} {} \n".format(o['poly_type'], len(o['tex_polygons'])))
            for (n, tp) in enumerate(o['tex_polygons']):
                if o['poly_type'] == "TRIANGLES":
                    file.write("\t{}:\t{}\t{}\t{}\n".format(n, tp[0], tp[1], tp[2]))
                else:
                    file.write("\t{}:\t{}\t{}\t{}\t{}\n".format(n, tp[0], tp[1], tp[2], tp[3]))
                
            
            file.write("\n")
            
    return {'FINISHED'}


# ExportHelper is a helper class, defines filename and
# invoke() function which calls the file selector.
from bpy_extras.io_utils import ExportHelper
from bpy.props import StringProperty, BoolProperty, EnumProperty
from bpy.types import Operator


class Export3DO(Operator, ExportHelper):
    """This appears in the tooltip of the operator and in the generated docs"""
    bl_idname = "export_.df3do"  # important since its how bpy.ops.import_test.some_data is constructed
    bl_label = "Export DF 3DO"

    # ExportHelper mixin class uses this
    filename_ext = ".3do"

    filter_glob: StringProperty(
        default="*.3DO",
        options={'HIDDEN'},
        maxlen=255,  # Max internal buffer length, longer would be clamped.
    )

    # List of operator properties, the attributes will be assigned
    # to the class instance from the operator settings before calling.
    use_setting: BoolProperty(
        name="Example Boolean",
        description="Example Tooltip",
        default=True,
    )

    type: EnumProperty(
        name="Example Enum",
        description="Choose between two items",
        items=(
            ('OPT_A', "First Option", "Description one"),
            ('OPT_B', "Second Option", "Description two"),
        ),
        default='OPT_A',
    )

    def execute(self, context):
        data = create_3do_data(context.blend_data)
        return write_3do(context, self.filepath, data[0], data[1])


# Only needed if you want to add into a dynamic menu
def menu_func_export(self, context):
    self.layout.operator(Export3DO.bl_idname, text="Dark Forces 3DO (.3do)")


# Register and add to the "file selector" menu (required to use F3 search "Text Export Operator" for quick access).
def register():
    bpy.utils.register_class(Export3DO)
    bpy.types.TOPBAR_MT_file_export.append(menu_func_export)


def unregister():
    bpy.utils.unregister_class(Export3DO)
    bpy.types.TOPBAR_MT_file_export.remove(menu_func_export)


if __name__ == "__main__":
    register()

    # test call
    bpy.ops.export_.df3do('INVOKE_DEFAULT')
