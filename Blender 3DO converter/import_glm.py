import bpy
import bmesh
import struct

def create_objects(model):
    materials_names = []    # strings
    materials_table = []    # materials
    
    # Process the first LOD only
    for s in range(model['num_surfaces']):
        surface = model['lods'][0]['surfaces'][s]
        
        new_bmesh = bmesh.new()
        uv_layer = new_bmesh.loops.layers.uv.new()
        
        vertices = []
        for v in surface['vertices']:
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
    
        mesh_name = model['surf_hierarchies'][s]['name']  # .rsplit('\\', 1)[0]
        newmesh = bpy.data.meshes.new(mesh_name)
        new_bmesh.to_mesh(newmesh)
        newobj = bpy.data.objects.new(mesh_name, newmesh)
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

        # Make a material for shader; check first to see if it already exists
        mat_name = model['surf_hierarchies'][s]['shader']

        if mat_name != "" and mat_name in materials_names:
            index = materials_names.index(mat_name)
            existing_mat = materials_table[index]
            newobj.data.materials.append(existing_mat)
        else:
            new_mat = bpy.data.materials.new(mat_name)
            new_mat.use_nodes = True
            # new_mat.node_tree.nodes.new('ShaderNodeTexImage')
            materials_names.append(mat_name)
            materials_table.append(new_mat)
            newobj.data.materials.append(new_mat)
                
        bpy.context.collection.objects.link(newobj)



def read_glm(context, filepath):
    scale = 8
    model = dict()

    with open(filepath, mode = 'rb') as file:
        ident = file.read(4)
        if ident != b'2LGM':
            return

        # File header
        model['version'] = read_int32(file)
        model['name'] = bytes_to_string(file.read(64))
        model['anim_name'] = bytes_to_string(file.read(64))
        model['anim_index'] = read_int32(file)
        model['num_bones'] = read_int32(file)
        model['num_lods'] = read_int32(file)
        model['ofs_lods'] = read_int32(file)
        model['num_surfaces'] = read_int32(file)
        model['ofs_surface_hierarchy'] = read_int32(file)
        model['ofs_end'] = read_int32(file)

        model['surf_hierarchy_offsets'] = []
        for i in range(model['num_surfaces']):
            model['surf_hierarchy_offsets'].append(read_int32(file))

        # Surface hierarchies
        model['surf_hierarchies'] = []
        file.seek(model['ofs_surface_hierarchy'])
        for s in range(model['num_surfaces']):
            surf_hierarchy = dict()
            surf_hierarchy['name'] = bytes_to_string(file.read(64))
            surf_hierarchy['flags'] = read_uint32(file)
            surf_hierarchy['shader'] = bytes_to_string(file.read(64))
            surf_hierarchy['shader_index'] = read_int32(file)
            surf_hierarchy['parent_index'] = read_int32(file)
            surf_hierarchy['num_children'] = read_int32(file)

            surf_hierarchy['child_indexes'] = []
            for c in range(surf_hierarchy['num_children']):
                surf_hierarchy['child_indexes'].append(read_int32(file))

            model['surf_hierarchies'].append(surf_hierarchy)

        # LODs
        model['lods'] = []
        file.seek(model['ofs_lods'])
        for lod in range(model['num_lods']):
            lod_start = file.tell()
            lod = dict()
            lod['ofs_end'] = read_int32(file)
            lod['ofs_surfaces'] = []

            for surf in range(model['num_surfaces']):
                lod['ofs_surfaces'].append(read_int32(file))

            # Surfaces
            lod['surfaces'] = []
            for surf in range(model['num_surfaces']):
                surf_start = file.tell()
                surface = dict()
                surface['ident'] = read_int32(file)
                surface['index'] = read_int32(file)
                surface['ofs_header'] = read_int32(file)
                surface['num_verts'] = read_int32(file)
                surface['ofs_verts'] = read_int32(file)
                surface['num_triangles'] = read_int32(file)
                surface['ofs_triangles'] = read_int32(file)
                surface['num_bonerefs'] = read_int32(file)
                surface['ofs_bonerefs'] = read_int32(file)
                surface['ofs_end'] = read_int32(file)

                surface['triangles'] = []
                for tri in range(surface['num_triangles']):
                    surface['triangles'].append((
                        read_int32(file),
                        read_int32(file),
                        read_int32(file)
                    ))

                surface['vertices'] = []
                for vert in range(surface['num_verts']):
                    vertex = dict()
                    vertex['normal'] = (
                        read_float32(file),
                        read_float32(file),
                        read_float32(file)
                    )

                    vertex['coords'] = (
                        read_float32(file) / scale,
                        read_float32(file) / scale,
                        read_float32(file) / scale
                    )

                    vertex['numWeightsAndBoneIndexes'] = read_uint32(file)
                    vertex['bone_weightings'] = (
                        file.read(1),
                        file.read(1),
                        file.read(1),
                        file.read(1)
                    )

                    surface['vertices'].append(vertex)

                surface['sts'] = []
                for st in range(surface['num_verts']):
                    surface['sts'].append((
                        read_float32(file),
                        read_float32(file)
                    ))

                lod['surfaces'].append(surface)
                file.seek(surf_start + surface['ofs_end'])

            model['lods'].append(lod)
            file.seek(lod_start + lod['ofs_end'])
    
    
    create_objects(model)
    return {"FINISHED"}


def read_int32(file):
    return int.from_bytes(file.read(4), byteorder="little", signed=True)


def read_uint32(file):
    return int.from_bytes(file.read(4), byteorder="little", signed=False)


def read_float32(file):
    return struct.unpack('f', file.read(4))[0]


def bytes_to_string(bytes):
    result = ""

    for i in bytes:
        if i == 0:
            break

        result += chr(i)

    return result



# ImportHelper is a helper class, defines filename and
# invoke() function which calls the file selector.
from bpy_extras.io_utils import ImportHelper
from bpy.props import StringProperty, BoolProperty, EnumProperty
from bpy.types import Operator


class ImportSomeData(Operator, ImportHelper):
    """This appears in the tooltip of the operator and in the generated docs"""
    bl_idname = "import_test.some_data"  # important since its how bpy.ops.import_test.some_data is constructed
    bl_label = "Import GLM"

    # ImportHelper mixin class uses this
    filename_ext = ".glm"

    filter_glob: StringProperty(
        default="*.glm",
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
        return read_glm(context, self.filepath)


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
