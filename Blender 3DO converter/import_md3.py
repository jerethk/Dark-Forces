import bpy
import bmesh
import struct

def create_objects(model):
    for surface in model['surfaces']:
        new_bmesh = bmesh.new()
        uv_layer = new_bmesh.loops.layers.uv.new()
        
        vertices = []
        for v in surface['verts']:
            vertices.append(new_bmesh.verts.new(v['coords']))

        failed_polygons = []
        for (counter, t) in enumerate(surface['triangles']):
            polygon_verts = []
            for pv in t:
                polygon_verts.insert(0, vertices[pv])   # Reverse order
            
            try:
                newface = new_bmesh.faces.new(polygon_verts)
            except:
                failed_polygons.append(counter)
                print("unable to create polygon", counter)
    
        newmesh = bpy.data.meshes.new(surface['name'].rsplit('\\', 1)[0])
        new_bmesh.to_mesh(newmesh)
        newobj = bpy.data.objects.new(surface['name'].rsplit('\\', 1)[0], newmesh)
        new_bmesh.free()

        # Create UVs
        failed_poly_count = 0
        for polygon in newobj.data.polygons:
            if polygon.index in failed_polygons:
                failed_poly_count += 1  # increment by 1 to ensure we continue to line up with the correct texture polygon
            
            tx_poly = surface['triangles'][polygon.index + failed_poly_count]
            
            tvert_counter = len(polygon.loop_indices) - 1
            for loop_index in polygon.loop_indices:
                st = surface['sts'][tx_poly[tvert_counter]]
                newobj.data.uv_layers[0].data[loop_index].uv = [st[0], (1 - st[1])]   # V coordinate is inverse of T coordinate
                tvert_counter -= 1

        newobj.data.materials.append(bpy.data.materials.new("texture"))

        bpy.context.collection.objects.link(newobj)



def read_md3(context, filepath):
    model = dict()

    with open(filepath, mode = 'rb') as file:
        magic = file.read(4)
        if magic != b'IDP3':
            return

        model['version'] = file.read(4)
        model['name'] = bin_to_string(file.read(64))
        model['flags'] = file.read(4)

        model['num_frames'] = read_int32(file)
        model['num_tags'] = read_int32(file)
        model['num_surfaces'] = read_int32(file)
        model['num_skins'] = read_int32(file)
        model['ofs_frames'] = read_int32(file)
        model['ofs_tags'] = read_int32(file)
        model['ofs_surfaces'] = read_int32(file)
        model['ofs_eof'] = read_int32(file)

        # Go to surfaces and read them
        file.seek(model['ofs_surfaces'])
        model['surfaces'] = read_surfaces(file, model['num_surfaces'])

    
    create_objects(model)
    return {"FINISHED"}


def read_int16(file):
    return int.from_bytes(file.read(2), byteorder="little", signed=True)


def read_int32(file):
    return int.from_bytes(file.read(4), byteorder="little", signed=True)


def read_float32(file):
    return struct.unpack('f', file.read(4))[0]


def bin_to_string(bin):
    result = ""

    for i in bin:
        if i == 0:
            break

        result += chr(i)

    return result


def read_surfaces(file, num_surfaces):
    scale = 256
    surfaces = []

    for surf in range(num_surfaces):
        surface = dict()

        surface_start = file.tell()
        surface['magic'] = file.read(4)
        surface['name'] = bin_to_string(file.read(64))
        surface['flags'] = read_int32(file)
        surface['num_frames'] = read_int32(file)
        surface['num_shaders'] = read_int32(file)
        surface['num_verts'] = read_int32(file)
        surface['num_triangles'] = read_int32(file)
        surface['ofs_triangles'] = read_int32(file)
        surface['ofs_shaders'] = read_int32(file)
        surface['ofs_st'] = read_int32(file)
        surface['ofs_verts'] = read_int32(file)
        surface['ofs_end'] = read_int32(file)

        surface['shaders'] = []
        surface['verts'] = []
        surface['triangles'] = []
        surface['sts'] = []

        # Read shaders
        file.seek(surface_start + surface['ofs_shaders'])
        for shd in range(surface['num_shaders']):
            shader = dict()
            shader['name'] = bin_to_string(file.read(64))
            shader['index'] = read_int32(file)
            surface['shaders'].append(shader)

        # Read vertexes (only the first animation frame)
        file.seek(surface_start + surface['ofs_verts'])
        for v in range(surface['num_verts']):
            vx = dict()
            vx['coords'] = []
            vx['coords'].append(read_int16(file) / scale)  # X
            vx['coords'].append(read_int16(file) / scale)  # Y
            vx['coords'].append(read_int16(file) / scale)  # Z
            vx['normal'] = read_int16(file)                # normal
            surface['verts'].append(vx)

        # Read triangles
        file.seek(surface_start + surface['ofs_triangles'])
        for t in range(surface['num_triangles']):
            tri = []
            tri.append(read_int32(file))
            tri.append(read_int32(file))
            tri.append(read_int32(file))
            surface['triangles'].append(tri)

        # Read STs
        file.seek(surface_start + surface['ofs_st'])
        for st in range(surface['num_verts']):
            txv = []
            txv.append(read_float32(file))
            txv.append(read_float32(file))
            surface['sts'].append(txv)

        file.seek(surface_start + surface['ofs_end'])
        surfaces.append(surface)

    return surfaces


# ImportHelper is a helper class, defines filename and
# invoke() function which calls the file selector.
from bpy_extras.io_utils import ImportHelper
from bpy.props import StringProperty, BoolProperty, EnumProperty
from bpy.types import Operator


class ImportSomeData(Operator, ImportHelper):
    """This appears in the tooltip of the operator and in the generated docs"""
    bl_idname = "import_test.some_data"  # important since its how bpy.ops.import_test.some_data is constructed
    bl_label = "Import MD3"

    # ImportHelper mixin class uses this
    filename_ext = ".md3"

    filter_glob: StringProperty(
        default="*.md3",
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
        return read_md3(context, self.filepath)


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
