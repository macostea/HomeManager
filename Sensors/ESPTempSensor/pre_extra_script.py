Import("env")
from os import path

if path.exists("locals.py"):
    from locals import DEFINES
else:
    from defaults import DEFINES


env.Append(CPPDEFINES=DEFINES)
