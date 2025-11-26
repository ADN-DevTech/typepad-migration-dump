---
layout: "post"
title: "Getting bounding boxes of each component in the viewer"
date: "2015-08-28 08:22:33"
author: "Philippe Leefsma"
categories:
  - "HTML5"
  - "Philippe Leefsma"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/08/getting-bounding-boxes-of-each-component-in-the-viewer.html "
typepad_basename: "getting-bounding-boxes-of-each-component-in-the-viewer"
typepad_status: "Publish"
---

<p>By&nbsp;<a href="http://adndevblog.typepad.com/cloud_and_mobile/philippe-leefsma.html">Philippe Leefsma</a></p>
<p>Accessing the bounding box is a pretty straightforward thing to do from a standard Three.js model:</p>
<p><em>var bb = mesh.geometry.boundingBox</em></p>
<p>However optimisations in our custom renderer makes it a little bit less intuitive. Below are two snippets that illustrate how to compute bounding box for the selected component. Typically when you select a component visually, the&nbsp;<em><em><em>Autodesk.Viewing.SELECTION_CHANGED_EVENT</em></em></em> returns an object that contains the list of fragmentIds, which you need to pass to those methods (see full sample further down):</p>

<pre style="line-height: 100%;font-family:monospace;background-color:#ffffff; border-width:0.01mm; border-color:#000000; border-style:solid;padding:4px;font-size:10pt;"><span style="color:#800000;background-color:#f0f0f0;"> 1 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//returns bounding box as it appears in the viewer
</span><span style="color:#800000;background-color:#f0f0f0;"> 2 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// (transformations could be applied)
</span><span style="color:#800000;background-color:#f0f0f0;"> 3 </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> getModifiedWorldBoundingBox(fragIds, fragList) {
</span><span style="color:#800000;background-color:#f0f0f0;"> 4 
 5 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> fragbBox = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> THREE.Box3();
</span><span style="color:#800000;background-color:#f0f0f0;"> 6 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> nodebBox = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> THREE.Box3();
</span><span style="color:#800000;background-color:#f0f0f0;"> 7 
 8 </span><span style="background-color:#ffffff;">    fragIds.forEach(</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(fragId) {
</span><span style="color:#800000;background-color:#f0f0f0;"> 9 
10 </span><span style="background-color:#ffffff;">        fragList.getWorldBounds(fragId, fragbBox);
</span><span style="color:#800000;background-color:#f0f0f0;">11 </span><span style="background-color:#ffffff;">        nodebBox.union(fragbBox);
</span><span style="color:#800000;background-color:#f0f0f0;">12 </span><span style="background-color:#ffffff;">    });
</span><span style="color:#800000;background-color:#f0f0f0;">13 
14 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> nodebBox;
</span><span style="color:#800000;background-color:#f0f0f0;">15 </span><span style="background-color:#ffffff;">}
</span><span style="color:#800000;background-color:#f0f0f0;">16 
17 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// Returns bounding box as loaded in the file
</span><span style="color:#800000;background-color:#f0f0f0;">18 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// (no explosion nor transformation)
</span><span style="color:#800000;background-color:#f0f0f0;">19 </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> getOriginalWorldBoundingBox(fragIds, fragList) {
</span><span style="color:#800000;background-color:#f0f0f0;">20 
21 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> fragBoundingBox = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> THREE.Box3();
</span><span style="color:#800000;background-color:#f0f0f0;">22 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> nodeBoundingBox = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> THREE.Box3();
</span><span style="color:#800000;background-color:#f0f0f0;">23 
24 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> fragmentBoxes = fragList.boxes;
</span><span style="color:#800000;background-color:#f0f0f0;">25 
26 </span><span style="background-color:#ffffff;">    fragIds.forEach(</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(fragId) {
</span><span style="color:#800000;background-color:#f0f0f0;">27 
28 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> boffset = fragId * </span><span style="color:#0000ff;background-color:#ffffff;">6</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;">29 
30 </span><span style="background-color:#ffffff;">        fragBoundingBox.min.x = fragmentBoxes[boffset];
</span><span style="color:#800000;background-color:#f0f0f0;">31 </span><span style="background-color:#ffffff;">        fragBoundingBox.min.y = fragmentBoxes[boffset+</span><span style="color:#0000ff;background-color:#ffffff;">1</span><span style="background-color:#ffffff;">];
</span><span style="color:#800000;background-color:#f0f0f0;">32 </span><span style="background-color:#ffffff;">        fragBoundingBox.min.z = fragmentBoxes[boffset+</span><span style="color:#0000ff;background-color:#ffffff;">2</span><span style="background-color:#ffffff;">];
</span><span style="color:#800000;background-color:#f0f0f0;">33 </span><span style="background-color:#ffffff;">        fragBoundingBox.max.x = fragmentBoxes[boffset+</span><span style="color:#0000ff;background-color:#ffffff;">3</span><span style="background-color:#ffffff;">];
</span><span style="color:#800000;background-color:#f0f0f0;">34 </span><span style="background-color:#ffffff;">        fragBoundingBox.max.y = fragmentBoxes[boffset+</span><span style="color:#0000ff;background-color:#ffffff;">4</span><span style="background-color:#ffffff;">];
</span><span style="color:#800000;background-color:#f0f0f0;">35 </span><span style="background-color:#ffffff;">        fragBoundingBox.max.z = fragmentBoxes[boffset+</span><span style="color:#0000ff;background-color:#ffffff;">5</span><span style="background-color:#ffffff;">];
</span><span style="color:#800000;background-color:#f0f0f0;">36 
37 </span><span style="background-color:#ffffff;">        nodeBoundingBox.union(fragBoundingBox);
</span><span style="color:#800000;background-color:#f0f0f0;">38 </span><span style="background-color:#ffffff;">    });
</span><span style="color:#800000;background-color:#f0f0f0;">39 
40 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> nodeBoundingBox;
</span><span style="color:#800000;background-color:#f0f0f0;">41 </span><span style="background-color:#ffffff;">}</span></pre>

Here is the result of the extension code provided below. You can test a live version <a href="http://viewer.autodesk.io/node/gallery/embed?id=560c6c57611ca14810e1b2bf&extIds=Autodesk.ADN.Viewing.Extension.BoundingBox" target="_blank">here</a>:
</br></br>
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0869f10a970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb0869f10a970d image-full img-responsive" alt="Screen Shot 2015-08-28 at 5.09.23 PM" title="Screen Shot 2015-08-28 at 5.09.23 PM" src="/assets/image_d96abe.jpg" border="0" /></a><br />

<script src="https://gist.github.com/leefsmp/08314b7f329b2a88920c.js"></script>
