---
layout: "post"
title: "Custom transparent meshes with View & Data API"
date: "2016-02-23 01:40:15"
author: "Philippe Leefsma"
categories:
  - "Philippe Leefsma"
  - "View and Data API"
  - "WebGL"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/02/custom-transparent-meshes-with-view-data-api.html "
typepad_basename: "custom-transparent-meshes-with-view-data-api"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/cloud_and_mobile/philippe-leefsma.html">Philippe Leefsma</a> <a href="https://twitter.com/F3lipek">(@F3lipek)</a></p>
<p>Here is a quick post for the road, the topic was raised by Bill Malcom in <a href="http://forums.autodesk.com/t5/view-and-data-api/no-material-transpareny-on-custom-object3d-s-added-to-the-viewer/td-p/6044267">that</a> forum thread:</p>
<p><em>&quot;I have been adding my own custom THREE.js objects in the existing viewer.impl.scene for a while with no problem. However, I need to set the transparency on a simple THREE.CircleGeometry. For some reason these custom objects don&#39;t display the transparency in the viewer. Maybe the viewer renderer in affecting this? I have done similar in a pure THREE.js viewer (non LMV) and it works. [...]&quot;</em></p>
<p>It comes from the fact&#0160;is that the custom&#0160;geometry is added to a scene&#0160;which is rendered before the actual model. In order for this to work correctly, the mesh&#0160;have to be rendered <u>after</u>&#0160;the model.&#0160;It works when adding it to the sceneAfter:</p>
<pre style="line-height: 100%; font-family: monospace; background-color: #ffffff; padding: 4px; font-size: 10pt; border: 0.01mm solid #000000;"><span style="color: #800000; background-color: #f0f0f0;"> 1 </span><span style="color: #000080; background-color: #ffffff; font-weight: bold;">var</span><span style="background-color: #ffffff;"> transparentMaterial = </span><span style="color: #000080; background-color: #ffffff; font-weight: bold;">new</span><span style="background-color: #ffffff;"> THREE.MeshBasicMaterial({
</span><span style="color: #800000; background-color: #f0f0f0;"> 2 </span><span style="background-color: #ffffff;">  color: </span><span style="color: #0000ff; background-color: #ffffff;">0x7094FF</span><span style="background-color: #ffffff;">,
</span><span style="color: #800000; background-color: #f0f0f0;"> 3 </span><span style="background-color: #ffffff;">  opacity: </span><span style="color: #0000ff; background-color: #ffffff;">0.1</span><span style="background-color: #ffffff;">,
</span><span style="color: #800000; background-color: #f0f0f0;"> 4 </span><span style="background-color: #ffffff;">  transparent: </span><span style="color: #000080; background-color: #ffffff; font-weight: bold;">true</span><span style="background-color: #ffffff;"> });
</span><span style="color: #800000; background-color: #f0f0f0;"> 5 
 6 </span><span style="background-color: #ffffff;">viewer.impl.matman().addMaterial(
</span><span style="color: #800000; background-color: #f0f0f0;"> 7 </span>  <span style="color: #008000; background-color: #ffffff; font-weight: bold;">&#39;transparent&#39;</span><span style="background-color: #ffffff;">,
</span><span style="color: #800000; background-color: #f0f0f0;"> 8 </span><span style="background-color: #ffffff;">  transparentMaterial,
</span><span style="color: #800000; background-color: #f0f0f0;"> 9 </span>  <span style="color: #000080; background-color: #ffffff; font-weight: bold;">true</span><span style="background-color: #ffffff;">);
</span><span style="color: #800000; background-color: #f0f0f0;">10 
11 </span><span style="color: #000080; background-color: #ffffff; font-weight: bold;">var</span><span style="background-color: #ffffff;"> geometry = </span><span style="color: #000080; background-color: #ffffff; font-weight: bold;">new</span><span style="background-color: #ffffff;"> THREE.CircleGeometry(</span><span style="color: #0000ff; background-color: #ffffff;">500</span><span style="background-color: #ffffff;">, </span><span style="color: #0000ff; background-color: #ffffff;">50</span><span style="background-color: #ffffff;">);
</span><span style="color: #800000; background-color: #f0f0f0;">12 </span><span style="color: #000080; background-color: #ffffff; font-weight: bold;">var</span><span style="background-color: #ffffff;"> circlegeom = </span><span style="color: #000080; background-color: #ffffff; font-weight: bold;">new</span><span style="background-color: #ffffff;"> THREE.Mesh(
</span><span style="color: #800000; background-color: #f0f0f0;">13 </span><span style="background-color: #ffffff;">  geometry,
</span><span style="color: #800000; background-color: #f0f0f0;">14 </span><span style="background-color: #ffffff;">  transparentMaterial);
</span><span style="color: #800000; background-color: #f0f0f0;">15 
16 </span><span style="background-color: #ffffff;">viewer.impl.sceneAfter.add(circlegeom);
</span><span style="color: #800000; background-color: #f0f0f0;">17 </span><span style="background-color: #ffffff;">viewer.impl.invalidate(</span><span style="color: #000080; background-color: #ffffff; font-weight: bold;">true</span><span style="background-color: #ffffff;">);</span></pre>
<p>Unfortunately&#0160;this has&#0160;a&#0160;side-effect: the native viewer selection mechanism has to deal with meshes that do not have the properties it expects.</p>
<p>It can be fixed but you&#0160;likely have to edit the code of the viewer and load a custom version, see my fix below (viewer3D.js #L21962). Another option would be to create the custom geometry the same way the viewer does, so it could participate in the selection, but probably more work. If you have a better fix, I&#39;m happy to hear it ...</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8192638970b-pi" style="display: inline;"><img alt="Viewerfix" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c8192638970b image-full img-responsive" src="/assets/image_72a9af.jpg" title="Viewerfix" /></a></p>
<p>Here is a screenshot of the&#0160;transparent custom mesh circle with a loaded model:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1a347b2970c-pi" style="display: inline;"><img alt="Screen Shot 2016-02-19 at 09.55.40" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1a347b2970c img-responsive" src="/assets/image_928aea.jpg" title="Screen Shot 2016-02-19 at 09.55.40" /></a></p>
