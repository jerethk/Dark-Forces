import bpy
import bmesh

# CREATE 3DO OBJECTS FROM BLENDER MESH OBJECTS
objects = []

for obj in bpy.data.objects:
    if obj.type != 'MESH':
        continue
    
    this_object = dict()
    mesh = obj.data
    this_object['name'] = mesh.name
    
    # Vertices
    this_object['vertices'] = []
    this_object['num_vertices'] = len(mesh.vertices)
    for v in mesh.vertices:
        this_object['vertices'].append((
            v.co[0],
            v.co[1],
            v.co[2]
        ))
    
    # Determine if polygons can be quads (all polygons have 4 vertices)
    is_quads = True
    this_object['poly_type'] = 'QUADS'
    for p in mesh.polygons:
        if len(p.vertices) != 4:
            is_quads = False
            this_object['poly_type'] = 'TRIANGLES'
            break
        
    # Polygons
    this_object['polygons'] = []
    this_object['tex_vertices'] = []
    this_object['tex_polygons'] = []
    
    mesh_copy = mesh.copy()
    
    # Triangulate if not quads
    if not is_quads:
        bm = bmesh.new()
        bm.from_mesh(mesh_copy)
        bmesh.ops.triangulate(bm, faces=bm.faces)
        bm.to_mesh(mesh_copy)
        bm.free()
        
    this_object['num_polygons'] = len(mesh_copy.polygons)
    colour_counter = 0        # use this for coloring
    tvert_counter = 0
    for p in mesh_copy.polygons:
        this_poly = dict()
        this_poly['vertices'] = []
        for v in p.vertices:
            this_poly['vertices'].insert(0, v)  # vertex order is reversed in Blender (anti-clockwise)
            
        this_poly['colour'] = 40 + 4 * colour_counter  # greys
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
            this_object['tex_vertices'].append((
                mesh_copy.uv_layers[0].data[l].uv[0],
                mesh_copy.uv_layers[0].data[l].uv[1]
            ))
            tp_verts.insert(0, tvert_counter)  # reverse order
            tvert_counter += 1
            
        this_object['tex_polygons'].append(tp_verts)
    
    bpy.data.meshes.remove(mesh_copy)
    objects.append(this_object)



# FUNCTION TO WRITE 3DO FILE
def write_3do(context, filepath):
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
        file.write("3DONAME " + filepath + "\n")
        file.write("OBJECTS {} \n".format(num_objects))
        file.write("VERTICES {} \n".format(total_vertices))
        file.write("POLYGONS {} \n".format(total_polys))
        file.write("PALETTE DEFAULT.PAL\n\n")
        file.write("TEXTURES {}\n".format(num_objects))
        
        # Give 1 texture per object
        for t in range(num_objects):
            file.write(" TEXTURE: default.bm \t#{} \n".format(t))
            
        file.write("\n")
        
        object_counter = 0
        for o in objects:
            file.write("#------------------------------------------------------------------\n")
            file.write("OBJECT \"{}\" \n".format(o['name']))
            file.write("TEXTURE {} \n\n".format(object_counter))
            object_counter += 1
            
            file.write("VERTICES {} \n".format(o['num_vertices']))
            file.write("# \tnum: \t  x \t  y \t  z \n")
            counter = 0
            for v in o['vertices']:
                file.write("\t{}:\t{:.3f}\t{:.3f}\t{:.3f}\n".format(counter, v[0], -v[2], v[1]))
                counter += 1
                
            file.write("\n{} {} \n".format(o['poly_type'], o['num_polygons']))
            if o['poly_type'] == 'TRIANGLES':
                file.write("# \tnum:\t<a>\t<b>\t<c>\t<color>\t<shading> \n")
            else:
                file.write("# \tnum:\t<a>\t<b>\t<c>\t<d>\t<color>\t<shading> \n")

            counter = 0
            for p in o['polygons']:
                if o['poly_type'] == 'TRIANGLES':
                    file.write("\t{}:\t{}\t{}\t{}\t{}\t{}\n".format(counter, p['vertices'][0], p['vertices'][1], p['vertices'][2], p['colour'], p['shading']))
                else:
                    file.write("\t{}:\t{}\t{}\t{}\t{}\t{}\t{}\n".format(counter, p['vertices'][0], p['vertices'][1], p['vertices'][2], p['vertices'][3], p['colour'], p['shading']))
                counter += 1
            
            file.write("\nTEXTURE VERTICES {} \n".format(len(o['tex_vertices'])))
            counter = 0
            for tv in o['tex_vertices']:
                file.write("\t{}:\t{:.3f}\t{:.3f}\n".format(counter, tv[0], tv[1]))
                counter += 1
            
            file.write("\nTEXTURE {} {} \n".format(o['poly_type'], len(o['tex_polygons'])))
            counter = 0
            for tp in o['tex_polygons']:
                if o['poly_type'] == "TRIANGLES":
                    file.write("\t{}:\t{}\t{}\t{}\n".format(counter, tp[0], tp[1], tp[2]))
                else:
                    file.write("\t{}:\t{}\t{}\t{}\t{}\n".format(counter, tp[0], tp[1], tp[2], tp[3]))
                counter += 1
                
            
            file.write("\n")
            
    return {'FINISHED'}


# ExportHelper is a helper class, defines filename and
# invoke() function which calls the file selector.
from bpy_extras.io_utils import ExportHelper
from bpy.props import StringProperty, BoolProperty, EnumProperty
from bpy.types import Operator


class ExportSomeData(Operator, ExportHelper):
    """This appears in the tooltip of the operator and in the generated docs"""
    bl_idname = "export_test.some_data"  # important since its how bpy.ops.import_test.some_data is constructed
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
        return write_3do(context, self.filepath)


# Only needed if you want to add into a dynamic menu
def menu_func_export(self, context):
    self.layout.operator(ExportSomeData.bl_idname, text="Text Export Operator")


# Register and add to the "file selector" menu (required to use F3 search "Text Export Operator" for quick access).
def register():
    bpy.utils.register_class(ExportSomeData)
    bpy.types.TOPBAR_MT_file_export.append(menu_func_export)


def unregister():
    bpy.utils.unregister_class(ExportSomeData)
    bpy.types.TOPBAR_MT_file_export.remove(menu_func_export)


if __name__ == "__main__":
    register()

    # test call
    bpy.ops.export_test.some_data('INVOKE_DEFAULT')
