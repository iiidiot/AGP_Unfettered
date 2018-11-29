===============================================
 Teleport Fade Shader
===============================================

<< Package >>
TeleportFadeShader/
  Documents/
    License.txt
    Readme.txt
  Samples/
  Shaders/
    TeleportFade.shader
    TeleportFade_NormalMap.shader
  Textures/
    TeleportFadeNoise1.png
    TeleportFadeNoise2.png


<< Usage >>
1. Create a material with 'TeleportFade' or 'TeleportFade (Normal Map)' shader.
2. Set that material to meshes.
3. Change parameters.
4. Enjoy!

<< Parameters >>
* Fade Texture ... Set TeleportFadeNoise1, TeleportFadeNoise2 or your created noise texture.
* Rise power ... Rise up ratio in fading.
* Twist power ... twist ratio in fading.
* Spread power ... Spread XZ ratio in fading.
* Particle color ... Emissive color in fading. If zero alpha, no emissive color change.
* Fade rate ... Fading rate. You control this parameter in scripts.
* Object height ... Set the fade object height.
* Object fade height ... Set the fading height.
* Object base position ... Set the object position. If the object moving, you have to reset this parameter. 

<< Contact >>
If you need more help, contact us.
http://tk-syoutem.com/


© tk-syouten
