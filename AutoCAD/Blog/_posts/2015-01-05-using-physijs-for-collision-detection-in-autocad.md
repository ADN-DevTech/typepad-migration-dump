---
layout: "post"
title: "Using PhysiJS for collision detection in AutoCAD"
date: "2015-01-05 10:52:04"
author: "Balaji"
categories:
  - ".NET"
  - "2015"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "JavaScript"
original_url: "https://adndevblog.typepad.com/autocad/2015/01/using-physijs-for-collision-detection-in-autocad.html "
typepad_basename: "using-physijs-for-collision-detection-in-autocad"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>AutoCAD solids when imported in a Three.js scene can be used for some interesting physics simulations. Collision detection of certain solids with other solids in the scene can be one of them. In this blog post, we will look at using PhysiJS to identify the minimum and maximum swing angle of a rotating arm as it collides with two other solids that act as stoppers.</p>
<p>Here is a video recording :&nbsp;</p>
<p><iframe width="500" height="281" src="http://www.youtube.com/embed/W8-GfDdSKnU?feature=oembed" frameborder="0" allowfullscreen=""></iframe>&nbsp;</p>
<p>In this PhysiJS scene, we create three types of meshes.</p>
<p>1) The mesh that will be rotated. In our case, the rotating swing arm.</p>
<p>2) The solids that will be grounded. In our case, the stoppers with which the swing arm will collide but the stoppers will not be affected by the collision.</p>
<p>3) Any other AutoCAD solids that may not be of interest in the collision detection and can safely be imported as Three.JS meshes. PhysiJS will not compute collision with such meshes as they are not PhysiJS mesh. An example of such mesh in our case is the solid that supports the rotating swing arm.</p>
<p>To classify the solids in the drawings under one of the above categories, an XData has been added to the driven and grounded solids with a reg appname set to "PHYSIMESHTYPE".&nbsp;</p>
<p>A few points to note about the implementation :</p>
<p>- At present, the driven solid is always assumed to be rotating at origin and along the Z axis.</p>
<p>- The force applied by the motor on the driven solid is fixed at 100 units. Depending on the mass of the solid being driven, this may be required to be changed. Too less force would result in no rotation of the swing arm while too much force will swing it too fast.</p>
<p>- The restitution value of the material is intentionally set to zero to ensure that the swing arm does not bounce back on impact with the stopper.</p>
<p>- The accuracy with which the angle of the swing arm after it collides with the stopper seems to be dependent on the mesh type and also due to the computational errors introduced by PhysiJS's collision detection algorithm. In my tests using a test drawing, while the exact value at which the swing arm should be stopping was 13 degrees, the value obtained by using PhysiJS was at 12.53 degrees.&nbsp;</p>
<p>To try it,&nbsp;</p>
<p>1) please download the files from Kean's blog on integrating AutoCAD with Three.js.</p>
<p><a title="Connecting Three.js to an AutoCAD model Part II" href="http://through-the-interface.typepad.com/through_the_interface/2014/10/connecting-threejs-to-an-autocad-model-part-2.html" target="_blank">Connecting Three.js to an AutoCAD model Part II</a></p>
<p>2) Download PhysiJS from here :&nbsp;<a title="PhysiJS" href="https://github.com/chandlerprall/Physijs" target="_blank">PhysiJS</a></p>
<p>3) Replace the following files that includes the changes mentioned in this blog post :</p>
<p><span class="asset  asset-generic at-xid-6a0167607c2431970b01b7c72ec943970b img-responsive"><a href="http://adndevblog.typepad.com/files/utils-1.cs">Download Utils.cs</a></span></p>
<p><span class="asset  asset-generic at-xid-6a0167607c2431970b01b7c72ec943970b img-responsive"> <span class="asset  asset-generic at-xid-6a0167607c2431970b01bb07d2c90b970d img-responsive"><a href="http://adndevblog.typepad.com/files/threesolids2-1.js">Download Threesolids2.js</a></span></span></p>
<p><span class="asset  asset-generic at-xid-6a0167607c2431970b01b7c72ec943970b img-responsive"><span class="asset  asset-generic at-xid-6a0167607c2431970b01bb07d2c90b970d img-responsive"> <span class="asset  asset-generic at-xid-6a0167607c2431970b01b8d0b82de9970c img-responsive"><a href="http://adndevblog.typepad.com/files/threesolids2.html">Download Threesolids2.html</a><br /></span></span></span></p>
<p><span class="asset  asset-generic at-xid-6a0167607c2431970b01b7c72ec943970b img-responsive"><span class="asset  asset-generic at-xid-6a0167607c2431970b01bb07d2c90b970d img-responsive"><span class="asset  asset-generic at-xid-6a0167607c2431970b01b8d0b82de9970c img-responsive"> <span class="asset  asset-generic at-xid-6a0167607c2431970b01b8d0b82e01970c img-responsive"><a href="http://adndevblog.typepad.com/files/acadext2.js">Download Acadext2.js</a></span></span></span></span></p>
<p><span class="asset  asset-generic at-xid-6a0167607c2431970b01b7c72ec943970b img-responsive"><span class="asset  asset-generic at-xid-6a0167607c2431970b01bb07d2c90b970d img-responsive"><span class="asset  asset-generic at-xid-6a0167607c2431970b01b8d0b82de9970c img-responsive"><span class="asset  asset-generic at-xid-6a0167607c2431970b01b8d0b82e01970c img-responsive">Here is the sample drawing with XData attached to the solids to identify driven, grounded and other solids which do not participate in collision detection.</span></span></span></span></p>
<p><span class="asset  asset-generic at-xid-6a0167607c2431970b01b7c72ec943970b img-responsive"><span class="asset  asset-generic at-xid-6a0167607c2431970b01bb07d2c90b970d img-responsive"><span class="asset  asset-generic at-xid-6a0167607c2431970b01b8d0b82de9970c img-responsive"><span class="asset  asset-generic at-xid-6a0167607c2431970b01b8d0b82e01970c img-responsive"> <span class="asset  asset-generic at-xid-6a0167607c2431970b01b8d0b82eac970c img-responsive"><a href="http://adndevblog.typepad.com/files/swing.dwg">Download Swing.dwg</a></span></span></span></span></span></p>
<p>&nbsp;</p>
