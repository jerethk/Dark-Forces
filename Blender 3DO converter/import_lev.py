import bpy
import bmesh


def load_lev(context, filepath):
    lines = read_lev(filepath)
    lev = parse_lev(lines)
    

    # Create 1 mesh / object per layer
    for layer in lev['layers_used']:
        new_bmesh = bmesh.new()

        for sector in lev['sectors']:
            if sector['layer'] == layer:
                # floor (don't create if pit)
                if sector['flags'][0] & 128 == 0:
                    f_vertices = []
                    for v in sector['vertices']:
                        f_vertices.insert(0, new_bmesh.verts.new((v[0], v[1], sector['floor_alt'])))

                    new_bmesh.faces.new(f_vertices)
                    
                # ceiling (don't create if sky)
                if sector['flags'][0] & 1 == 0:
                    c_vertices = []
                    for v in sector['vertices']:
                        c_vertices.append(new_bmesh.verts.new((v[0], v[1], sector['ceiling_alt'])))

                    new_bmesh.faces.new(c_vertices)
                    
                # walls
                if sector['flags'][0] & 1024 == 0:
                    for w in sector['walls']:
                        # Unadjoined wall
                        if w['adjoin'] == -1:
                            left_floor = new_bmesh.verts.new((sector['vertices'][w['vertices'][0]][0], sector['vertices'][w['vertices'][0]][1], sector['floor_alt']))
                            left_ceil = new_bmesh.verts.new((sector['vertices'][w['vertices'][0]][0], sector['vertices'][w['vertices'][0]][1], sector['ceiling_alt']))
                            right_floor = new_bmesh.verts.new((sector['vertices'][w['vertices'][1]][0], sector['vertices'][w['vertices'][1]][1], sector['floor_alt']))
                            right_ceil = new_bmesh.verts.new((sector['vertices'][w['vertices'][1]][0], sector['vertices'][w['vertices'][1]][1], sector['ceiling_alt']))

                            w_vertices = []
                            w_vertices.append(right_floor)
                            w_vertices.append(right_ceil)
                            w_vertices.append(left_ceil)
                            w_vertices.append(left_floor)
                            new_bmesh.faces.new(w_vertices)
                            
                        # Adjoined wall
                        if w['adjoin'] >= 0:
                            adj_sec = lev['sectors'][w['adjoin']]
                            
                            # top segment
                            if adj_sec['ceiling_alt'] < sector['ceiling_alt']:
                                a = new_bmesh.verts.new((sector['vertices'][w['vertices'][0]][0], sector['vertices'][w['vertices'][0]][1], adj_sec['ceiling_alt']))
                                b = new_bmesh.verts.new((sector['vertices'][w['vertices'][0]][0], sector['vertices'][w['vertices'][0]][1], sector['ceiling_alt']))
                                c = new_bmesh.verts.new((sector['vertices'][w['vertices'][1]][0], sector['vertices'][w['vertices'][1]][1], sector['ceiling_alt']))
                                d = new_bmesh.verts.new((sector['vertices'][w['vertices'][1]][0], sector['vertices'][w['vertices'][1]][1], adj_sec['ceiling_alt']))
                                
                                t_vertices = []
                                t_vertices.append(d)
                                t_vertices.append(c)
                                t_vertices.append(b)
                                t_vertices.append(a)
                                new_bmesh.faces.new(t_vertices)
                                
                            # bottom segment
                            if adj_sec['floor_alt'] > sector['floor_alt']:
                                a = new_bmesh.verts.new((sector['vertices'][w['vertices'][0]][0], sector['vertices'][w['vertices'][0]][1], sector['floor_alt']))
                                b = new_bmesh.verts.new((sector['vertices'][w['vertices'][0]][0], sector['vertices'][w['vertices'][0]][1], adj_sec['floor_alt']))
                                c = new_bmesh.verts.new((sector['vertices'][w['vertices'][1]][0], sector['vertices'][w['vertices'][1]][1], adj_sec['floor_alt']))
                                d = new_bmesh.verts.new((sector['vertices'][w['vertices'][1]][0], sector['vertices'][w['vertices'][1]][1], sector['floor_alt']))
                                
                                b_vertices = []
                                b_vertices.append(d)
                                b_vertices.append(c)
                                b_vertices.append(b)
                                b_vertices.append(a)
                                new_bmesh.faces.new(b_vertices)
                    
        newmesh = bpy.data.meshes.new(lev['name'] + "_layer_" + str(layer))
        new_bmesh.to_mesh(newmesh)
        newobj = bpy.data.objects.new(lev['name'] + "_layer_" + str(layer), newmesh)
        new_bmesh.free()

        bpy.context.collection.objects.link(newobj)

    return {'FINISHED'}


def parse_lev(lines):
    level = dict()
    current_line = 0

    if lines[current_line][0].upper() != "LEV":
        print("Error, not a LEV file")
        return None

    while current_line < len(lines):
        if lines[current_line][0].upper() != "LEVELNAME":
            current_line += 1
        else:
            level['name'] = lines[current_line][1]
            current_line += 1
            break

    while current_line < len(lines):
        if lines[current_line][0].upper() != "NUMSECTORS":
            current_line += 1
        else:
            level['num_sectors'] = int(lines[current_line][1])
            current_line += 1
            break

    # SECTORS
    level['sectors'] = []
    level['layers_used'] = set()
    for sector in range(level['num_sectors']):
        this_sector = dict()

        while current_line < len(lines):
            if lines[current_line][0].upper() != "SECTOR":
                current_line += 1
            else:
                current_line += 1
                break

        this_sector['name'] = ""
        while current_line < len(lines):
            if lines[current_line][0].upper() != "NAME":
                current_line += 1
            else:
                if len(lines[current_line]) > 1:
                    this_sector['name'] = lines[current_line][1]
                break

        current_line += 3
        this_sector['floor_alt'] = -float(lines[current_line][2])

        current_line += 2
        this_sector['ceiling_alt'] = -float(lines[current_line][2])

        current_line += 2
        this_sector['flags'] = (int(lines[current_line][1]), int(lines[current_line][2]), int(lines[current_line][3]))

        current_line += 1
        layer = int(lines[current_line][1])
        this_sector['layer'] = layer
        if layer not in level['layers_used']:
            level['layers_used'].add(layer)

        # VERTICES
        current_line += 1
        if lines[current_line][0].upper() != "VERTICES":
            print("Error")
            return
        else:
            this_sector['num_vertices'] = int(lines[current_line][1])

        this_sector['vertices'] = []
        for v in range(this_sector['num_vertices']):
            current_line += 1
            this_sector['vertices'].append((float(lines[current_line][1]), float(lines[current_line][3])))

        # WALLS
        current_line += 1
        if lines[current_line][0].upper() != "WALLS":
            print("Error")
            return
        else:
            this_sector['num_walls'] = int(lines[current_line][1])

        this_sector['walls'] = []
        for w in range(this_sector['num_walls']):
            current_line += 1
            this_wall = dict()
            this_wall['vertices'] = (int(lines[current_line][2]), int(lines[current_line][4]))
            this_wall['adjoin'] = int(lines[current_line][25])
            this_wall['mirror'] = int(lines[current_line][27])
            this_sector['walls'].append(this_wall)

        level['sectors'].append(this_sector)

    return level

def read_lev(filepath):
    with open(filepath, 'r') as levfile:
        lines = []
        for line in levfile:
            next_line = split_line(line)
            if len(next_line):
                lines.append(next_line)

    return lines

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
    bl_label = "Import DF LEV"

    # ImportHelper mixin class uses this
    filename_ext = ".lev"

    filter_glob: StringProperty(
        default="*.lev",
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
        return load_lev(context, self.filepath)


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
