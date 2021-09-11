# Written by Ryan Puhalovich 1064808
# Made because I wanted comments, variables and arithmetic.
# USAGE: Run scripts/final_scene.sh or scripts/final_scene.bat

import random

# Marble Parameters
NUM_MARBLES = 0
MARBLE_RADIUS = 0.12 # TODO: make random

# Box Parameters
SIDE_LEN = 8.0 # where it's the length +- this value
FLOOR_HEIGHT = -1
LEFT_RIGHT_DIST = 1

# Light Parameters
LIGHT_INTENSITY = 0.2
NUM_EDGE_LIGHTS = 2

def gen():
    f = open("tests/final_scene.txt", "w")

    # COLORS
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

    f.write('Material "WhiteEmissive" Emissive (1, 1, 1) 1\n')
    f.write('Material "RedEmissive" Emissive (1, 0.5, 0.6) 1\n')

    # BIG MARBLES
    # f.write('Sphere "TestSphere1" (0, 1, 5) 1 "WhiteMat"\n')
    # f.write('Sphere "TestSphere2" (-3, 0, 7) 1 "BlueMat"\n')

    f.write('Sphere "BigBoi1" (-1.5, 0, 10) 1 "WhiteMat"\n')
    f.write('Sphere "BigBoi2" (0, 0, 7.5) 1 "GlassMat"\n')
    f.write('Sphere "BigBoi3" (1.25, 0, 5) 1 "MirrorMat"\n')
    f.write('Sphere "BigBoi4" (3, 0, -1) 1 "WhiteMat"\n')
    f.write('Sphere "BigBoi5" (0, 0, -1) 1 "GlassMat"\n')
    f.write('Sphere "BigBoi6" (-3, 0, -1) 1 "MirrorMat"\n')

    # BOX
    roofA0 = '(-' + str(SIDE_LEN / 2) + ', ' + str(SIDE_LEN) + ', -' + str(SIDE_LEN) + ')'
    roofA1 = '(-' + str(SIDE_LEN / 2) + ', ' + str(SIDE_LEN) + ', ' + str(SIDE_LEN * 2) + ')'
    roofA2 = '(' + str(SIDE_LEN / 2) + ', ' + str(SIDE_LEN) + ', ' + str(SIDE_LEN * 2) + ')'

    roofB0 = '(-' + str(SIDE_LEN / 2) + ', ' + str(SIDE_LEN) + ', -' + str(SIDE_LEN) + ')'
    roofB1 = '(' + str(SIDE_LEN / 2) + ', ' + str(SIDE_LEN) + ', ' + str(SIDE_LEN * 2) + ')'
    roofB2 = '(' + str(SIDE_LEN / 2) + ', ' + str(SIDE_LEN) + ', -' + str(SIDE_LEN) + ')'

    f.write('Triangle "RoofA" ' + roofA0 + roofA1 + roofA2 + ' "WhiteMat"\n')
    f.write('Triangle "RoofB" ' + roofB0 + roofB1 + roofB2 + '  "WhiteMat"\n')
    # f.write('Plane "Roof" (0, ' + str(SIDE_LEN) + ', 0) (0, -1, 0) "WhiteMat"\n')

    f.write('Plane "Floor" (0, -1, 0) (0, 1, 0) "lightGreyMat"\n')
    f.write('Plane "BackWall" (0, 0, ' + str(SIDE_LEN * 2) + ') (0, 0, -1) "WhiteMat"\n')
    f.write('Plane "BehindWall" (0, 0, -' + str(SIDE_LEN) + ') (0, 0, 1) "WhiteMat"\n')
    f.write('Plane "Left" (-' + str(SIDE_LEN / 2) + ', 0, 0) (1, 0, 0) "RedMat"\n')
    f.write('Plane "Right" (' + str(SIDE_LEN / 2) + ', 0, 0) (-1, 0, 0) "GreenMat"\n')

    sideLenAdj = SIDE_LEN - MARBLE_RADIUS
    lightIntensityStr = '(' + str(LIGHT_INTENSITY) + ', ' + str(LIGHT_INTENSITY) + ', ' + str(LIGHT_INTENSITY) + ')'
    f.write('PointLight "Light1" (-' + str((sideLenAdj / 2) - 1) + ', 0.01, ' + str(sideLenAdj - 1) + ') ' + lightIntensityStr + '\n')
    f.write('PointLight "Light2" (' + str((sideLenAdj / 2) - 1) + ', 0.01, ' + str(sideLenAdj - 1) + ') ' + lightIntensityStr + '\n')
    f.write('PointLight "Light3" (-' + str((sideLenAdj / 2) - 1) + ', 0.01, -' + str(sideLenAdj - 1) + ') ' + lightIntensityStr + '\n')
    f.write('PointLight "Light4" (' + str((sideLenAdj / 2) - 1) + ', 0.01, -' + str(sideLenAdj - 1) + ') ' + lightIntensityStr + '\n')

    # MARBLES
    marbleHeight = MARBLE_RADIUS + FLOOR_HEIGHT
    for i in range(1, NUM_MARBLES + 1):
        x = random.uniform(-SIDE_LEN, SIDE_LEN)
        z = random.uniform(-SIDE_LEN, SIDE_LEN)

        mattype = ""
        mat = random.randint(0, 1)
        if mat == 0:
            mattype = "MirrorMat"
        elif mat == 1:
            mattype = "GlassMat"
        elif mat == 2:
            mattype = "WhiteEmissive"

        f.write('Sphere "Marble' + str(i) + '" (' + str(x) + ', ' + str(marbleHeight) + ',' + str(z) + ') ' + str(MARBLE_RADIUS) + ' "' + mattype + '"\n')

    f.close()

gen()
