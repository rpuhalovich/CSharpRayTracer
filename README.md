# COMP30019 Assignment 1 - Ray Tracer

This is your README.md... you should write anything relevant to your implementation here.

Please ensure your student details are specified below (_exactly_ as on UniMelb records):

**Name:** Ryan Puhalovich \
**Student Number:** 1064808 \
**Username:** rpuhalovich \
**Email:** rpuhalovich@student.unimelb.edu.au

## Completed stages

Tick the stages bellow that you have completed so we know what to mark (by editing README.md). At most **six** marks can be chosen in total for stage three. If you complete more than this many marks, pick your best one(s) to be marked!

<!---
Tip: To tick, place an x between the square brackes [ ], like so: [x]
-->

##### Stage 1

- [x] Stage 1.1 - Familiarise yourself with the template
- [x] Stage 1.2 - Implement vector mathematics
- [x] Stage 1.3 - Fire a ray for each pixel
- [x] Stage 1.4 - Calculate ray-entity intersections
- [x] Stage 1.5 - Output primitives as solid colours

##### Stage 2

- [x] Stage 2.1 - Diffuse materials
- [x] Stage 2.2 - Shadow rays
- [x] Stage 2.3 - Reflective materials
- [x] Stage 2.4 - Refractive materials
- [x] Stage 2.5 - The Fresnel effect
- [x] Stage 2.6 - Anti-aliasing

##### Stage 3

- [ ] Option A - Emissive materials (+6)
- [ ] Option B - Ambient lighting/occlusion (+6)
- [ ] Option C - OBJ models (+6)
- [x] Option D - Glossy materials (+3)
- [ ] Option E - Custom camera orientation (+3)
- [ ] Option F - Beer's law (+3)
- [x] Option G - Depth of field (+3)

_Please summarise your approach(es) to stage 3 here._

Depth of field was calculated by utilising a disk that was of radius `options.AperatureRadius`. A random point on this disk would be calculated and a ray would then be fired from it, through a point that corresponds to a pixel direction, at the length of the focalLength. Doing a number of these samples will give us a depth of field effect.

My particular approach to glossy materials was to have it such that 85% of the colour information coming from a regular diffuse calculation. The other 15% would be to have a reflection where the reflected ray is rotated by a small amount to give the surface a very slight blur. In my opinion, this gives a more convincing 'glossy' look to say, a single specular highlight, however I've implemented that too. The specular highlight, being a Phong model specular highlight (see references). In the `/images/final_image.png` submission, the red sphere in the centre, as well as the floor plane are both glossy.

_Below is an explanation to my attempt at emissive materials which could not be debugged in time. A solution that works for spheres can be found on the `emissive` branch._

My approach to emissive materials was to have a very similar approach to point lights. However for every point that is tested in shadows, a set number of rays fired in a random cone (currently set to 10), whos circle radius is the same length as the sphere or longest edge of the triangle, from the shadowPoint to to the emissive entity. Unfortunately, I was not able to debug the issue where triangles would not emit light, thus I omitted it from my marking choice. Spheres are functioning, and can be seen in the `images/sphere_emissive.png`. `images/naive_emissive.png` shows an extremely inefficient yet fully functional implementation.

Note that I still process ray hits with emissive materials such that no shadow is cast, as can be seen in the sphere on the left in `/images/final_image.png`. I just think it looks quite cool, with the glossy ground giving the illusion of emission.

## Final scene render

Be sure to replace `/images/final_scene.png` with your final render so it shows up here:

![My final render](/images/final_scene.png)

This render took **88** minutes and **51** seconds on my PC.

I used the following command to render the image exactly as shown:

```
dotnet run -- -f tests/final_scene.txt -o images/final_scene.png -w 1000 -h 1000 --aperture-radius 0.1 --focal-length 7.5
```

### A small extra

In order to efficiently distribute the 'Marble' spheres around my scene, I had written a simple Python script (`tests/final_scene.py`) that generates the `tests/final_scene.txt` file with the randomly placed marbles. This also had the benefit of allowing for comments, variables and arithmatic.

## Sample outputs

We have provided you with some sample tests located at `/tests/*`. So you have some point of comparison, here are the outputs our ray tracer solution produces for given command line inputs (for the first two stages, left and right respectively):

###### Sample 1

```
dotnet run -- -f tests/sample_scene_1.txt -o images/sample_scene_1.png -x 4
```

<p float="left">
  <img src="/images/sample_scene_1_s1.png" />
  <img src="/images/sample_scene_1_s2.png" /> 
</p>

###### Sample 2

```
dotnet run -- -f tests/sample_scene_2.txt -o images/sample_scene_2.png -x 4
```

<p float="left">
  <img src="/images/sample_scene_2_s1.png" />
  <img src="/images/sample_scene_2_s2.png" /> 
</p>

## References

_You must list any references you used!_

To get you started, here is some good reading material:

Working through a ray tracer, from the head of the xbox games studio: https://www.linkedin.com/pulse/writing-simple-ray-tracer-c-matt-booty/

_Ray Tracing in a Weekend_: https://raytracing.github.io/

Great walkthrough of some of the basic maths: https://blog.scottlogic.com/2020/03/10/raytracer-how-to.html

Scratchapixel: intro to ray tracing: https://www.scratchapixel.com/lessons/3d-basic-rendering/introduction-to-ray-tracing/how-does-it-work

### My References

C# version of the Fast Inverse Square Root: https://stackoverflow.com/questions/268853/is-it-possible-to-write-quakes-fast-invsqrt-function-in-c

Fast Inverse Square Root if other libraries were permitted: https://docs.microsoft.com/en-us/dotnet/api/opentk.functions.inversesqrtfast?view=xamarin-ios-sdk-12

Next Generation Post Processing in Call of Duty: Advanced Warfare: http://www.iryoku.com/next-generation-post-processing-in-call-of-duty-advanced-warfare

Unity Bloom Shader: https://github.com/Unity-Technologies/Graphics/blob/master/com.unity.postprocessing/PostProcessing/Shaders/Builtins/Bloom.shader

Andrew's Ray Visualizer: https://github.com/shangzhel/RayTracer.Debug

Microsoft's Stopwatch: https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.stopwatch?redirectedfrom=MSDN&view=net-5.0

glm (referencing their vector methods against my own): https://github.com/g-truc/glm

tinyraytracer: https://github.com/ssloy/tinyraytracer

Refraction Paper: https://graphics.stanford.edu/courses/cs148-10-summer/docs/2006--degreve--reflection_refraction.pdf

Ray-tracing soft shadows in real-time: https://medium.com/@alexander.wester/ray-tracing-soft-shadows-in-real-time-a53b836d123b

Ray tracing - soft shadow: https://stackoverflow.com/questions/31709332/ray-tracing-soft-shadow

References for depth of field implementation in a raytracer?: https://stackoverflow.com/questions/13532947/references-for-depth-of-field-implementation-in-a-raytracer

Rotating a Vector in 3D Space: https://stackoverflow.com/questions/14607640/rotating-a-vector-in-3d-space

Ray-tracing soft shadows in real-time: https://medium.com/@alexander.wester/ray-tracing-soft-shadows-in-real-time-a53b836d123b

Phong Model Introduction: https://www.scratchapixel.com/lessons/3d-basic-rendering/phong-shader-BRDF
