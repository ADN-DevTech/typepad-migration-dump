---
layout: "post"
title: "About IPointCloudAccess.ReadPoints method"
date: "2016-04-08 11:00:20"
author: "Augusto Goncalves"
categories:
  - ".NET"
  - "Augusto Goncalves"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2016/04/about-ipointcloudaccessreadpoints-method.html "
typepad_basename: "about-ipointcloudaccessreadpoints-method"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/augusto-goncalves.html">Augusto Goncalves</a> (<a href="https://twitter.com/augustomaia">@augustomaia</a>)</p>
<p><strong>Forge DevCon</strong></p>
<p>First of all, early bird registration is due to end on April 15th, so you don&#39;t have much time to buy your ticket at the low cost of $499. If you&#39;re a student, you can come for FREE- just sign up for a student ticket using a .edu email address.We&#39;re still working on the agenda. Visit <a href="http://forge.autodesk.com/tracks-and-speakers/">this link</a> for a list of the classes we currently have planned.</p>
<p><a class="asset-img-link" href="http://forge.autodesk.com" style="display: inline;" target="_blank" title="Forge DevCON"><img alt="Forge_devcon" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1ba90a5970c image-full img-responsive" src="/assets/image_930845.jpg" title="Forge_devcon" /></a></p>
<p><strong>IPointCloudAccess.ReadPoints method</strong></p>
<p>After some discussions for specific questions asked by some developers, this blog post summarizes what have being discussed.</p>
<p>When drawing point cloud, Revit calculates the total number of points that can read in the current view and camera, divide the number into several blocks and ask the IPointSetIterator to fill the block by calling ReadPoints(). Revit keep asking for the points until all the blocks as filed or there no points returned. This happens for every view change or camera change. The ReadPoints() method implementation needs to case about the points returned to each call with consideration of LOD.</p>
<p>Most engines implemented the LOD. The figure below shows the context of calling readPoints, and a typical implementation of this interface. The rule is simple:</p>
<ol>
<li>The filter can be used to identify the volume of point cloud to be read.</li>
<li>For each call of readPoints, it returns the denser points compared to last call.</li>
</ol>
<p>This pseudo code is the abstraction from a REAL engine and it works well in Revit.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1ba9068970c-pi" style="display: inline;"><img alt="Image1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1ba9068970c image-full img-responsive" src="/assets/image_244756.jpg" title="Image1" /></a></p>
