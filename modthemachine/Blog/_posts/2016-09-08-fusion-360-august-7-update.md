---
layout: "post"
title: "Fusion 360 August 7 Update"
date: "2016-09-08 19:01:27"
author: "Adam Nagy"
categories:
  - "Brian"
original_url: "https://modthemachine.typepad.com/my_weblog/2016/09/fusion-360-august-7-update.html "
typepad_basename: "fusion-360-august-7-update"
typepad_status: "Publish"
---

<p>Fusion 360 rolled out a new update yesterday and there are quite a few API enhancements.&#0160; For a complete list you can go to the <a href="http://help.autodesk.com/view/NINVFUS/ENU/?guid=GUID-36B1FFB5-5291-4532-8F11-90E912769B34">Fusion 360 API help</a>, but here are some of the highlights.</p>
<p><strong>Getting the Active Length Unit<br /></strong>There’s been a problem since the beginning of the API that we just recently discovered and have fixed in this release, but it’s possible that you may need to react to this change in your code.&#0160; If you want to find out what length units the user has chosen to use, (as shown below), you can use the defaultLengthUnits property of the UnitsManager object that you get from the Design object.&#0160;</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401bb0934565c970d-pi"><img alt="FusionUnits" border="0" height="157" src="/assets/image_202789.jpg" style="background-image: none; padding-top: 0px; padding-left: 0px; display: inline; padding-right: 0px; border: 0px;" title="FusionUnits" width="207" /></a>&#0160;&#0160;&#0160;</p>
<p>&#0160;</p>
<p>This returns a string indicating what the current length units are.&#0160; The problem was that the string returned wasn’t consistent and depended on the preference settings highlighted below.&#0160; So if the units were inch you could get back “inch”, “in”, or &quot; (a single double quote).&#0160; It’s likely that you didn’t discover this in your testing because the preferences were always consistent on your machine but then when someone else uses your program it could break because they have a different setting.&#0160; The API now always returns the abbreviated form of the unit.&#0160; So you can now always expect one of the following “in”, “ft”, “mm”, “cm”, or “m”.&#0160; If you were looking for other versions like “inch” or “centimeter” in your code, you’ll need to adjust for that.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401b8d21a9cd2970c-pi"><img alt="FusionUnitDisplay" border="0" height="167" src="/assets/image_243252.jpg" style="background-image: none; padding-top: 0px; padding-left: 0px; display: inline; padding-right: 0px; border: 0px;" title="FusionUnitDisplay" width="598" /></a></p>
<p>&#0160;</p>
<p><strong>Additional Feature Creation</strong><br />The API now supports the creation of some additional features.&#0160; These are Loft and two different flavors of Delete Face; deleting a face in the Patch workspace (SurfaceDeleteFaceFeature) and deleting face in the Model workspace (DeleteFaceFeature).&#0160; If you hadn’t noticed previously, these commands behave quite differently in the two workspaces.&#0160; When deleting faces in the Patch workspace faces are removed from the model.&#0160; When deleting faces in the Model workspace, the face is removed and then the model is “healed” to close the solid.</p>
<p>&#0160;</p>
<p><strong>Getting a Body as NURBS</strong><br />A body in Fusion can consist of a mix of surfaces of different geometry types.&#0160; For example, a box with a hole in it is made up of 6 planes and 1 cylinder.&#0160; Freeform surfaces in a body are represented by NURBS surfaces.&#0160; Some applications prefer to deal exclusively with NURBS rather than a mix of NURBS and analytic surfaces.&#0160; The new BRepBody.convert method creates a new transient body that is a copy of the original body but all of the geometry has been converted to NURBS surfaces.&#0160; New functionality is also provided through the new tempId property of the BRepFace, BrepEdge, and BRepVertex objects and the new findByTempId method of the BRepBody to find the vertices, edges, and faces, that match between the original body and the new body.</p>
<p>&#0160;</p>
<p>We’re working hard on some other API enhancements for the update currently scheduled for the end of September.</p>
