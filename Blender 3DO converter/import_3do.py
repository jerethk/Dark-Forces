import bpy
import bmesh


def load_3do(context, filepath):
    model = read_3do(filepath)
    
    if not len(model[1]):
        return
    
    materials_table = []
    for texture in model[0]:
        new_mat = bpy.data.materials.new(texture.split('.')[0])
        new_mat.use_nodes = True
        materials_table.append(new_mat)
    
    for object in model[1]:
        new_bmesh = bmesh.new()
        uv_layer = new_bmesh.loops.layers.uv.new()

        vertices = []
        for v in object['vertices']:
            vertices.append(new_bmesh.verts.new(v))

        p_counter = 0
        failed_polygons = []
        for p in object['polygons']:
            polygon_verts = []
            for pv in p['vertices']:
                polygon_verts.insert(0, vertices[pv])    # note - vertex order is reversed in Blender (anti-clockwise)
                
            try:
                newface = new_bmesh.faces.new(polygon_verts)
            except:
                # try with a copy of the vertices - in case it failed due to a "double sided" polygon
                polygon_verts_b = []
                for pv in p['vertices']:
                    pv_x = vertices[pv].co[0]
                    pv_y = vertices[pv].co[1]
                    pv_z = vertices[pv].co[2]
                    
                    vert_copy = new_bmesh.verts.new((pv_x, pv_y, pv_z))
                    polygon_verts_b.insert(0, vert_copy)
                    
                try:
                    newface = new_bmesh.faces.new(polygon_verts_b)    
                except:        
                    print("unable to create polygon", p_counter)
                    failed_polygons.append(p_counter)

            p_counter += 1
                
        newmesh = bpy.data.meshes.new(object['name'])
        new_bmesh.to_mesh(newmesh)
        newobj = bpy.data.objects.new(object['name'], newmesh)
        new_bmesh.free()
        
        # Create UVs if there are texture polygons
        failed_poly_count = 0
        for polygon in newobj.data.polygons:
            if polygon.index in failed_polygons:
                failed_poly_count += 1  # increment by 1 to ensure we continue to line up with the correct texture polygon
            
            if (polygon.index + failed_poly_count) >= len(object['tex_polygons']):
                break   # there are no more texture polygons
                
            tx_poly = object['tex_polygons'][polygon.index + failed_poly_count]
            
            tvert_counter = len(polygon.loop_indices) - 1
            for loop_index in polygon.loop_indices:
                tx_vert = object['tex_vertices'][tx_poly[tvert_counter]]
                newobj.data.uv_layers[0].data[loop_index].uv = tx_vert
                tvert_counter -= 1

        if object['texture'] != -1:
            newobj.data.materials.append(materials_table[object['texture']])
        
        bpy.context.collection.objects.link(newobj)
        
    return {'FINISHED'}


def read_3do(filename):
    lines = []
    objects = []
    num_objects = 0

    # Read the file into lines[], skipping empty/whitespace lines
    with open(filename, 'r') as file:
        for line in file:
            next_line = split_line(line)
            if len(next_line):
                lines.append(next_line)

    if lines[0][0].upper() != "3DO":
        print("Not a 3DO file!")
        return

    current_line = 0

    while current_line < len(lines):
        if lines[current_line][0].upper() != "OBJECTS":
            current_line += 1
        else:
            num_objects = int(lines[current_line][1])
            break

    # Texture table
    num_textures = 0
    textures = []
    while current_line < len(lines):
        if lines[current_line][0].upper() != "TEXTURES":
            current_line += 1
        else:
            num_textures = int(lines[current_line][1])
            break

    for tex in range(num_textures):
        current_line += 1
        if lines[current_line][0].rstrip(":").upper() == "TEXTURE":
            textures.append(lines[current_line][1])

    # Parse the objects
    for obj in range(num_objects):
        if current_line + 2 >= len(lines):  # this deals with _8x32.3DO (more objects declared in header than exist)
            break

        this_object = dict()

        # Object name
        current_line += 1
        while current_line < len(lines):
            if lines[current_line][0].upper() != "OBJECT":
                current_line += 1
            else:
                this_object['name'] = lines[current_line][1].strip("\"")
                break

        # Texture
        current_line += 1
        if lines[current_line][0].upper() == "TEXTURE":
            this_object['texture'] = int(lines[current_line][1])

        # Parse vertices
        current_line += 1
        while current_line < len(lines):
            if lines[current_line][0].upper() != "VERTICES":
                current_line += 1
            else:
                num_vertices = int(lines[current_line][1])
                break

        this_object['vertices'] = []
        for v in range(num_vertices):
            current_line += 1
            vertex = lines[current_line]

#            if int(vertex[0].rstrip(":")) != v:
#                print("Error at object \"", this_object['name'], "\" vertex", v)
#                return

            this_object['vertices'].append((
                float(vertex[1]),
                float(vertex[3]),
                -float(vertex[2])
            ))

        # Parse polygons
        current_line += 1
        if lines[current_line][0].upper() == "TRIANGLES":
            n_polyverts = 3
        elif lines[current_line][0].upper() == "QUADS":
            n_polyverts = 4
        else:
            print("Error at object \"", this_object['name'], "\" - could not find TRIANGLES or QUADS")
            return

        num_polys = int(lines[current_line][1])
        this_object['polygons'] = []
        for p in range(num_polys):
            current_line += 1
            this_polygon = dict()
            poly = lines[current_line]

#            if int(poly[0].rstrip(":")) != p:
#                print("Error at object \"", this_object['name'], "\" polygon", p)
#                return

            this_polygon['vertices'] = []
            for pv in range(n_polyverts):
                v = int(poly[pv + 1])
                if v not in this_polygon['vertices']:   # prevent adding duplicate vertices
                    this_polygon['vertices'].append(v)

            this_polygon['colour'] = int(poly[n_polyverts + 1])
            this_polygon['shading'] = poly[n_polyverts + 2]
            this_object['polygons'].append(this_polygon)

        # Look for texture data
        this_object['tex_vertices'] = []
        this_object['tex_polygons'] = []
        if current_line + 2 >= len(lines):  # reached end of file
            objects.append(this_object)
            break

        if lines[current_line + 1][0].upper() == "OBJECT":      # there is no texture data; continue to the next object
            objects.append(this_object)
            continue

        if len(lines[current_line + 1]) != 3 or (lines[current_line + 1][0].upper() != 'TEXTURE' and lines[current_line + 1][1].upper() != 'VERTICES'):
            objects.append(this_object)
            continue

        # Parse texture vertices
        current_line += 1         
        num_tx_verts = int(lines[current_line][2])
        for tv in range(num_tx_verts):
            current_line += 1
            tex_vertex = lines[current_line]

#            if int(tex_vertex[0].rstrip(":")) != tv:
#                print("Error at object \"", this_object['name'], "\" texture vertex", tv)
#                return

            this_object['tex_vertices'].append((
                float(tex_vertex[1]),
                float(tex_vertex[2])
            ))

        # Parse texture polygons
        current_line += 1
        if len(lines[current_line]) != 3 or lines[current_line][0].upper() != 'TEXTURE' or lines[current_line][1].upper() not in {'TRIANGLES', 'QUADS'}:
            print("Error at object \"", this_object['name'], "\" - could not find TEXTURE TRIANGLES or QUADS")
            return

        num_tx_polys = int(lines[current_line][2])
        for tp in range(num_tx_polys):
            current_line += 1
            tex_poly = lines[current_line]

#            if int(tex_poly[0].rstrip(":")) != tp:
#                print("Error at object \"", this_object['name'], "\" texture polygon", tp)
#                return

            this_tex_poly = []
            for pv in range(n_polyverts):
                tv = int(tex_poly[pv + 1])
                if tv not in this_tex_poly:     # prevent adding duplicate vertices
                    this_tex_poly.append(tv)

            this_object['tex_polygons'].append(this_tex_poly)

        objects.append(this_object)
    return (textures, objects)


# returns a line as a list of strings, removing comments
def split_line(line):
    line = line.split("#")
    return line[0].split()



# ImportHelper is a helper class, defines filename and
# invoke() function which calls the file selector.
from bpy_extras.io_utils import ImportHelper
from bpy.props import StringProperty, BoolProperty, EnumProperty
from bpy.types import Operator


class ImportSomeData(Operator, ImportHelper):
    """This appears in the tooltip of the operator and in the generated docs"""
    bl_idname = "import_test.some_data"  # important since its how bpy.ops.import_test.some_data is constructed
    bl_label = "Import 3DO"

    # ImportHelper mixin class uses this
    filename_ext = ".3do"

    filter_glob: StringProperty(
        default="*.3do",
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
        return load_3do(context, self.filepath)


# Only needed if you want to add into a dynamic menu.
def menu_func_import(self, context):
    self.layout.operator(ImportSomeData.bl_idname, text="Text Import Operator")


# Register and add to the "file selector" menu (required to use F3 search "Text Import Operator" for quick access).
def register():
    bpy.utils.register_class(ImportSomeData)
    bpy.types.TOPBAR_MT_file_import.append(menu_func_import)


def unregister():
    bpy.utils.unregister_class(ImportSomeData)
    bpy.types.TOPBAR_MT_file_import.remove(menu_func_import)


if __name__ == "__main__":
    register()

    # test call
    bpy.ops.import_test.some_data('INVOKE_DEFAULT')
