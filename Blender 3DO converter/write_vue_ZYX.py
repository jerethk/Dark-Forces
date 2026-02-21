import bpy
import math


def write_vue(context, filepath):
    file = open(filepath, 'w', encoding='utf-8')
    file.write("VUE \n\n")

    for f in range(bpy.context.scene.frame_end):
        bpy.context.scene.frame_set(f)

        obj = bpy.data.objects[0]
        pch = -obj.rotation_euler.y 
        yaw = -obj.rotation_euler.z
        rol = -obj.rotation_euler.x
        a = math.cos(pch) * math.cos(yaw)
        b = math.sin(rol) * math.sin(pch) * math.cos(yaw) - math.cos(rol) * math.sin(yaw)
        c = math.sin(rol) * math.sin(yaw) + math.cos(rol) * math.sin(pch) * math.cos(yaw)
        d = math.cos(pch) * math.sin(yaw)
        e = math.cos(rol) * math.cos(yaw) + math.sin(rol) * math.sin(pch) * math.sin(yaw)
        f = math.cos(rol) * math.sin(pch) * math.sin(yaw) - math.sin(rol) * math.cos(yaw)
        g = -math.sin(pch)
        h = math.sin(rol) * math.cos(pch)
        i = math.cos(rol) * math.cos(pch)
        
        line = "transform \"{}\" {:f} {:f} {:f} {:f} {:f} {:f} {:f} {:f} {:f} {:.2f} {:.2f} {:.2f}"
        file.write(line.format(obj.name, a, b, c, d, e, f, g, h, i, obj.location.x, obj.location.y, obj.location.z) + "\n")
        
    file.close()
    return {'FINISHED'}


# ExportHelper is a helper class, defines filename and
# invoke() function which calls the file selector.
from bpy_extras.io_utils import ExportHelper
from bpy.props import StringProperty, BoolProperty, EnumProperty
from bpy.types import Operator


class ExportSomeData(Operator, ExportHelper):
    """This appears in the tooltip of the operator and in the generated docs"""
    bl_idname = "export_test.some_data"  # important since its how bpy.ops.import_test.some_data is constructed
    bl_label = "Write VUE"

    # ExportHelper mixin class uses this
    filename_ext = ".vue"

    filter_glob: StringProperty(
        default="*.vue",
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
        return write_vue(context, self.filepath)


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
