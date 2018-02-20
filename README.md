# Obj2Ply
Import mesh from *.obj and export as *.ply (x y z r g b). <br>
3D models were captured using 'Structure Sensor for ios',  
the script is typically suited for this device. <br>
I only checked works for MacOS, maybe works for Linux.

### Required
Unity: 2017.3.0f

### Download Sample Files

2 of scanned 3D model samples. <br>
- https://drive.google.com/file/d/1GMvnY4M1auUPRcF1uBcK7t8jqVTqJHiD/view?usp=sharing
- https://drive.google.com/file/d/192x5YrVu0Nl3RgIszwAvKVLHIZcOmodg/view?usp=sharing

### How To Use
Look at the script 'Assets/MainApp.cs'. <br>
After Build ( [File] -> [BuildSetting] -> [Build] ), You can use the program from command line. 
Or, use through UnityEditor.

```
./main hoge.obj

```

### How To View

I recommend to use MeshLab (https://sourceforge.net/projects/meshlab/). <br>

### Issue
Y axis is not correct.
