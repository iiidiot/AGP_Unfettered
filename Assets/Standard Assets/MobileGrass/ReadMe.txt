Update Version 1.1:
Added the ability to automatically set the correct layer

==================================================================================================================================

MobileGrass1.0:

Very early gpu gems series books have been introduced to the rendering of large-scale grass, 
although the technology inside is old but very practical. I made a certain modification on the basis of their description, 
by increasing the vertices without using any alpha test, and reducing the use of the billboard through the intersection of a super large grid.
In addition, I have added additional interactive features that require OpenGL 3.0 support. 
You can see that when the ball moves, the grass will fall in the right direction.
I drew a love through the ball.
The entire grassland can still have excellent performance on mobile phones.

Quick Start
Click Tools-->CreateGrassTool to open the panel for building the grass.

Num – You create the number of sides of the grass, and the final total is the square of num.
Range – The length of the land size created by the grass
GrassUnitWidth – Width ratio of each grass
Segment – Number of segments required for each grass
Create – create the grass
Save – save the grass


You can also use MobileGrass.prefab in the MobileGrass/Prefab directory directly and adjust it to the size you want by zooming.
Then form a prairie by means of multiple prefab stitching.

Note that if you create more grassy vertices than 65535, you will get an error.

See the document Large-scale grass rendering for more details.