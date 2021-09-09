# Written by Ryan Puhalovich 1064808
# Made because I wanted comments, variables, arithmetic and functions.
# USAGE: Run scripts/final_scene.sh or scripts/final_scene.bat

import random

# Marble Parameters
NUM_MARBLES = 100
MARBLE_RADIUS = 0.12 # TODO: make random

# Box Parameters
SIDE_LEN = 8.0
FLOOR_HEIGHT = -1

# Light Parameters
LIGHT_INTENSITY = 0.5

def gen():
    f = open("tests/final_scene.txt", "w")

    f.write('Material "WhiteMat" Diffuse (.9, .85, .9) 1\n')
    f.write('Material "PurpleMat" Diffuse (.9, 0, .9) 1\n')
    f.write('Material "lightGreyMat" Diffuse (.9, .9, .9) 1\n')
    f.write('Material "GreyMat" Diffuse (.5, .5, .5) 1\n')
    f.write('Material "RedMat" Diffuse (1, .5, .5) 1\n')
    f.write('Material "GreenMat" Diffuse (.5, 1, .5) 1\n')
    f.write('Material "BlueMat" Diffuse (.5, .5, 1) 1\n')
    f.write('Material "LightBlueMat" Diffuse (.8, .8, 1) 1\n')
    f.write('Material "GlassMat" Refractive (1, 1, 1) 1.4\n')
    f.write('Material "MirrorMat" Reflective (1, 1, 1) 1\n')
    f.write('\n')

    f.write('PointLight "Light1" (-1.0, 0.0, 0) (' + str(LIGHT_INTENSITY) + ', ' + str(LIGHT_INTENSITY) + ', ' + str(LIGHT_INTENSITY) + ')\n')
    f.write('PointLight "Light2" (1, 0.0, 0) (' + str(LIGHT_INTENSITY) + ', ' + str(LIGHT_INTENSITY) + ', ' + str(LIGHT_INTENSITY) + ')\n')
    # f.write('PointLight "Light3" (-4.0, 0.0, 10) (0.4, 0.4, 0.4)\n')
    # f.write('PointLight "Light4" (4, 0.0, 10) (0.4, 0.4, 0.4)\n')
    f.write('\n')

    f.write('Sphere "BigBoi1" (-1.5, 0, 10) 1 "GreyMat"\n')
    f.write('Sphere "BigBoi2" (0, 0, 7.5) 1 "GlassMat"\n')
    f.write('Sphere "BigBoi3" (1.25, 0, 5) 1 "MirrorMat"\n')
    f.write('\n')

    sideLenAdj = SIDE_LEN - MARBLE_RADIUS
    f.write('Plane "Floor" (0, -1, 0) (0, 1, 0) "lightGreyMat"\n')
    f.write('Plane "Roof" (0, ' + str(sideLenAdj) + ', 0) (0, -1, 0) "WhiteMat"\n')
    f.write('Plane "BackWall" (0, 0, ' + str(sideLenAdj * 2) + ') (0, 0, -1) "WhiteMat"\n')
    f.write('Plane "BehindWall" (0, 0, -' + str(sideLenAdj) + ') (0, 0, 1) "WhiteMat"\n')
    f.write('Plane "Left" (-' + str(sideLenAdj / 2) + ', 0, 0) (1, 0, 0) "RedMat"\n')
    f.write('Plane "Right" (' + str(sideLenAdj / 2) + ', 0, 0) (-1, 0, 0) "GreenMat"\n')
    f.write('\n')

    marbleHeight = MARBLE_RADIUS + FLOOR_HEIGHT
    for i in range(1, NUM_MARBLES + 1):
        x = random.uniform(-SIDE_LEN, SIDE_LEN)
        z = random.uniform(-SIDE_LEN, SIDE_LEN)

        mattype = ""
        mat = random.randint(0, 2)
        if mat == 0:
            mattype = "GreyMat"
        elif mat == 1:
            mattype = "GlassMat"
        elif mat == 2:
            mattype = "MirrorMat"

        f.write('Sphere "Marble' + str(i) + '" (' + str(x) + ', ' + str(marbleHeight) + ',' + str(z) + ') ' + str(MARBLE_RADIUS) + ' "' + mattype + '"\n')

    f.close()

gen()
