Thanks for purchasing UETools 100+ Magic Particle Effects!

Package Contents:
=================
- Effects
- Materials
- Textures

Effects:
========
Currently 136 particle effect prefabs that can be spawned in your game

Materials:
==========
Materials linked with corresponding textures and translucent shader, ready to use with particle systems.

Textures:
=========
Hi-Res pre-rendered RGBA textures for use with transparent shader

Pre-Rendering
=============
Let's say you are going to make a game for mobile with rich effects. One particle effects consists of 50 particle texture instances (exploding stars, auras, ...), EVERY FRAME!
You want to support old devices such as iPhone 4, so the effect texture with 1/4 display size has up to +- 200x200 pixels. With 50 instances every frame, it is exactly 2 000 000 (yes, 2M for worst case scenario) pixels overdraw.
Compared to 40 000 pixels overdraw for pre-rendered textures, we know who is the winner. At every frame, only 1 texture instance is visible.
Are there any cons? Yes, every effect texture is pre-renderes so if you want to modify the effect, you have write your own post-process for rendered sprite. You can experiment with built-in features in Shuriken, modify Hue in a graphics editing soft to change color etc.

Tips:
=====
If you are making effects for mobile devices, especially low performance models, you might want to scale down the texture (select texture, set maximum size e.g. 1024 and effects 2048x1024 will be scaled down to 1024x512. That saves 75% memory.) You know that effect will be small on display? Scale it down and check quality ingame.
Don't forget to set best compression quality for final builds (e.g. PVRTC 4bits "Best" for iOS, ...), otherwise the effects might look ugly.
If you are making a game for mobile, set material shader to Mobile->Particles->AlphaBlended !!

Modifications:
==============
By default, effects are looping seamlessly without any In/Out fades.

Q: I want to spawn effect with fade in/out and without any loop, how to do this?
A: Simply. Select effect prefab or game object, disable "Looping", enable "Color over Lifetime" and here comes the magic. Edit the color as below:
	0%		->	Alpha = 0
	10%		->	Alpha = 100
	75%		->	Alpha = 100
	100%	->	Alpha = 0
This is only example, you can play with alpha as you wish. Also try modify the color, you will be surprised.

Q: I want my effect to play faster, how to do this?
A: Let's say the effect have to be 2x faster and still repeat seamlessly.
	Select effect prefab or game object
	Set "Duration" to 0.5 (Default is 1 second; 2x faster = 2x per second, so 0.5sec per loop)
	Set "Start Lifetime" same as "Duration" to 0.5
	Set "Emission->Rate" to 2
	You are done!

If you stuck with something or have advices that we can share in this file, please contact us at support@uetools.com

History:
========
1.0 Initial Release
1.1 Added Support for Unity 5
1.2 Added Support for other Unity releases
