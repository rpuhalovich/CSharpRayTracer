import random

def gen():
    f = open("tests/final_scene.txt", "w")

    f.write('Material "WhiteMat" Diffuse (.9, .85, .9) 1\n')
    f.write('Material "PurpleMat" Diffuse (.9, 0, .9) 1\n')
    f.write('Material "GreyMat" Diffuse (.5, .5, .5) 1\n')
    f.write('Material "RedMat" Diffuse (1, .5, .5) 1\n')
    f.write('Material "GreenMat" Diffuse (.5, 1, .5) 1\n')
    f.write('Material "BlueMat" Diffuse (.5, .5, 1) 1\n')
    f.write('Material "LightBlueMat" Diffuse (.8, .8, 1) 1\n')
    f.write('Material "GlassMat" Refractive (1, 1, 1) 1.4\n')
    f.write('Material "MirrorMat" Reflective (1, 1, 1) 1\n')
    f.write('\n')

    f.write('PointLight "Light1" (-1.0, 0.0, 0) (0.4, 0.4, 0.4)\n')
    f.write('PointLight "Light2" (1, 0.0, 0) (0.4, 0.4, 0.4)\n')
    #f.write('PointLight "Light3" (-4.0, 0.0, 10) (0.4, 0.4, 0.4)\n')
    #f.write('PointLight "Light4" (4, 0.0, 10) (0.4, 0.4, 0.4)\n')
    f.write('\n')

    f.write('Sphere "BigBoi1" (-1.5, 0, 10) 1 "GreyMat"\n')
    f.write('Sphere "BigBoi2" (0, 0, 7.5) 1 "GlassMat"\n')
    f.write('Sphere "BigBoi3" (1.25, 0, 5) 1 "MirrorMat"\n')
    f.write('\n')

    sidelen = 10.0
    f.write('Triangle "FloorA" (-' + str(sidelen) + ', -1, -' + str(sidelen) + ') (-' + str(sidelen) + ', -1, ' + str(sidelen) + ') (' + str(sidelen) + ', -1, ' + str(sidelen) + ') "LightBlueMat"\n')
    f.write('Triangle "FloorB" (-' + str(sidelen) + ', -1, -' + str(sidelen) + ') (' + str(sidelen) + ', -1, ' + str(sidelen) + ') (' + str(sidelen) + ', -1, -' + str(sidelen) + ') "LightBlueMat"\n')
    f.write('\n')

    f.write('Plane "BackWall" (0, 0, ' + str(sidelen) + ') (0, 0, -1) "WhiteMat"\n')
    f.write('Plane "BehindWall" (0, 0, -' + str(sidelen) + ') (0, 0, 1) "WhiteMat"\n')
    f.write('Plane "Roof" (0, ' + str(sidelen) + ', 0) (0, -1, 0) "WhiteMat"\n')
    f.write('Plane "Left" (-' + str(sidelen) + ', 0, 0) (1, 0, 0) "RedMat"\n')
    f.write('Plane "Right" (' + str(sidelen) + ', 0, 0) (-1, 0, 0) "GreenMat"\n')
    f.write('\n')

    for i in range(1, 201):
        x = random.uniform(-sidelen, sidelen)
        z = random.uniform(-sidelen, sidelen)

        mattype = ""
        mat = random.randint(0, 2)
        if mat == 0:
            mattype = "GreyMat"
        elif mat == 1:
            mattype = "GlassMat"
        elif mat == 2:
            mattype = "MirrorMat"

        f.write('Sphere "Marble' + str(i) + '" (' + str(x) + ', -0.9,' + str(z) + ') 0.1 "' + mattype + '"\n')

    f.close()

gen()
