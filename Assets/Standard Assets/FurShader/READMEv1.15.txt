#Fur Shader 
ReadMe v1.15
August 12th, 2016
Ryan Theriot

#Fixes

##1.20
-Fixed Skinned Mesh Renderers UV maps being incorrect.
-Single UV Map now. Fur, Skin, and Heightmap all share the same UV to avoid UV errors. Tilling and Offset for all three are handled by the Fur/Grass texture.
-Added a dirt texture to the grass shader for those requesting this feature. This is similar to the Skin Texture on the FurShader.

##1.15
-Added a Grass Shader which is a simplified version of the Fur shader **I did not document the Grass Shader, read the Fur documentation below which is similar
-Added 3 different detail levels for each shader High, Medium, Low. **High is the original level of detail from past versions

##1.10
-Added different controls for color, brightness, and transparency to Fur and Skin Layer
-Fixed errors concering 'ps_2_0' Shader Model

##1.05
-Add Fog support
-Moved the Gravity Direction setting to the shader properties under Fur Properties. This will allow users who don't want to use the script to still have the option to affect gravity. You do loose other features not attaching the script.
-Fixed a PS4 error


#How to use

1. Create a new material.
2. In the inspector pane select RIOT/FurShader under Shader.
3. Apply your texture and heightmap. (Ideas on how certain textures/heightmaps interact with the shader can be seen in the sample scene)
4. Attach the new material to a GameObject.
5. Attach the FurScript to the same GameObject. (Optional but required for some featrues, e.g. Simulate Movement)
6. In the inspector pane drag the Material on to the "Fur Material" slot on the script in the inspector pane. (Optional if script was attached in step 5)


#Tips

***Inspect sample scene to see how settings are applied to achieve a look you desire.

***Adjust the Main Texture tiling to achieve your desired fur look.

***If you enable shadows you will have to adjust the "Brightness" to achieve your desired color. Shadow Strength and Brightness should be adjusted together.

***To simulate movement your GameObject must have a RigidBody attached


#Settings

##Shader Settings

Main Texture : The Main RGB color texture for the material

Height Map : The grayscale height map for the material

Fur Color : Changes the color of the fur 

Fur Brightness : Increases/Decreases the brightness of the fur 

Height Map Brightness : Increases/Decreases the brightness of the height map texture

Fur Transparency : Increase/Decrese the overall transparency of the fur

Enable Skin Layer : Will enable the skin texture to be applied to the mesh

SKin Color : Changes the color of the skin layer

Skin Brightness: Increases/Decresases the brightness of the skin

Skin Transparency : Increase/Decreases the transparency of the skin 

Fur Length : Increases/Decreases the length of the fur shader

Fur Stiffness : The stiffness of the fur. Fur will resist gravity and such.

Gravity : The direction the fur feels gravity. It does not have to be same direction as the gravity for you scene. You can apply it upwards (Y = 1) to simulate fire or grass.

Depth Shadows : Applys a shadow effect to the fur. Fur at a lower layer will be darker then higher layers. Useful for grass and fur. NONE : Disabled, Normal : Lower layers darker, Invert: Higher layers darker

Depth Shadow Strength: Increases/Decreases the shadow intensity

WindSpeed : The speed of the wind. Wind is randomized

WindStrength : The strength of the wind. Wind is randomized.

Cull Velocity Angle: This will cull the opposite side of the mesh in realtion to the velcotiy vector. Useful if creating trails on sides of mesh.

Cull Angle: The angle on the mesh the culling will begin

##Script Settings

Fur Material : The material attached to the same gameobject that has the fur shader enabled should be attached here

Simulate Movement : With this enabled, if the gameobject has a rigidbody attached, the velocity of the rigidbody with simulate fur movement. Example is shown in the sample scene.

Cull Correction: If you find your fur objects are dissappering near the edge of the screen enable this option. ***WARNING: While in the editor with this option enabled meshes may dissapear in the SCENE VIEW, They will still be visable in game though as can be seen in the GAME view in the editor***

Transparent Depth: With the shader transparency property set to a value lower than 1.0, when a transparent gameobject comes in front of another transparent gameobject, the gameobject behind can become invisable. With this you can set which transparent objects have priority. A lower value will cause this object to be drawn first and others behind it to be invisable. If you set to a higher value, objects behind this one wont dissappear but this object will dissapear behind other transparent objects with a lower value. You must set this option intelligently for your particular scene. 





