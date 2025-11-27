---
layout: "post"
title: "Bringing 3ds Max point clouds into AutoCAD 2011"
date: "2010-08-13 17:55:55"
author: "Kean Walmsley"
categories:
  - "3ds Max"
  - "AutoCAD"
  - "Point clouds"
  - "Reality capture"
original_url: "https://www.keanw.com/2010/08/bringing-3ds-max-point-clouds-into-autocad-2011.html "
typepad_basename: "bringing-3ds-max-point-clouds-into-autocad-2011"
typepad_status: "Publish"
---

<p>Some of you may have seen the <a href="http://labs.autodesk.com/utilities/3dsmax_pointcloud/">point cloud tool for 3ds Max and 3ds Max Design</a> posted on <a href="http://labs.autodesk.com/">Autodesk Labs</a>. This plugin currently works with points defined in an ASCII format, although I expect in time it will support similar formats to those supported by AutoCAD.</p>
<p>In case you’re interested in bringing point cloud files that intended for – or generated/edited by – this tool into AutoCAD, I thought I’d share a quick tip. You can use <a href="http://www.cs.unc.edu/~isenburg/lastools">the TXT2LAS tool</a> – which I’ve used extensively in my efforts to bring point clouds into AutoCAD <a href="http://through-the-interface.typepad.com/through_the_interface/2010/04/importing-photosynth-point-clouds-into-autocad-2011-part-4.html">from Photosynth</a> <a href="http://through-the-interface.typepad.com/through_the_interface/2010/07/proving-the-concept-one-click-point-cloud-generation-inside-autocad-2011-with-project-photofly.html">and Photofly</a> – to convert the PTS file into a LAS, and then index/attach the file there.</p>
<p>PTS files are simply space-delimited text files, with the first line containing the number of points. The txt2las tool doesn’t need this number, so you can ignore the warning it’ll generate (or delete the first line from the file – your choice).</p>
<p>Each subsequent line is comprised of the XYZ values of the point, followed by an unknown (to me, anyway) decimal (which was the same for each point in the file I tested with, so I’ve just ignored it) and then finally the RGB values of that point:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">755047</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">55.74190000 77.90000000 4.15650000  0.1  246 246 246</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">55.74190000 77.90000000 4.15610000  0.1  246 246 246</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">55.74190000 77.90000000 4.15570000  0.1  246 246 246</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">55.74190000 77.90000000 4.15580000  0.1  245 245 245</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">55.74190000 77.90000000 4.15580000  0.1  246 246 246</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">55.74190000 77.90000000 4.15590000  0.1  246 246 246</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">55.74190000 77.90000000 4.15550000  0.1  245 245 245</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">55.74190000 77.89990000 4.15580000  0.1  245 245 245</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">55.74190000 77.89990000 4.15590000  0.1  245 245 245</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">...</span></p>
</div>
<p>With all this in mind, here’s a working usage example for creating a LAS from a PTS using the txt2las utility:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">txt2las -parse xyzsRGB -i test.pts -o test.las</span></p>
</div>
<p>Trying this with the Lobby_Scan_Ascii.pts sample file linked to from the Labs page…</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e20134862f6215970c-pi"><img alt="Running txt2las on a sample PTS file" border="0" height="243" src="/assets/image_334722.jpg" style="margin: 20px auto; display: block; float: none; border-width: 0px;" title="Running txt2las on a sample PTS file" width="479" /></a> … we end up with a LAS file that can be indexed in AutoCAD using POINTCLOUDINDEX and attached using POINTCLOUDATTACH:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e20133f30bed0a970b-pi"><img alt="Our converted PTS file inside AutoCAD 2011" border="0" height="394" src="/assets/image_53343.jpg" style="margin: 20px auto; display: block; float: none; border-width: 0px;" title="Our converted PTS file inside AutoCAD 2011" width="480" /></a></p>
