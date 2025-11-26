---
layout: "post"
title: "Restore hatch boundaries if they have been lost"
date: "2013-01-24 02:32:19"
author: "Philippe Leefsma"
categories:
  - "AutoCAD"
  - "ObjectARX"
  - "Philippe Leefsma"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/restore-hatch-boundaries-if-they-have-been-lost.html "
typepad_basename: "restore-hatch-boundaries-if-they-have-been-lost"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/philippe-leefsma.html" target="_self">Philippe Leefsma</a></p>  <p><b>Q:</b></p>  <p>How can I restore hatch boundaries if they have been lost due to some reason?</p>  <p><a name="section2"></a></p>  <p><b>A:</b></p>  <p>AutoCAD doesn't have a command to do that. Fortunately, we can create one with ObjectARX API. </p>  <p>The most complex in the task is to handle splines properly as there are various of splines in AutoCAD, fixed point spline, rational spline, not rational spline, etc. The code works quite well for lines, arcs, circles, ellipses and splines.&#160; Note that error checking is minimal for brevity.&#160; Please bear in mind that it is not possible to restore the original boundary entities exactly, because the underlying geometry objects prefixed with AcGe in AcDbHatch do not match AutoCAD database entities prefixed with AcDb one by one. For example, if there is a closed loop composed of some lines in the original hatch boundary entities, it will become a polyline in the final result restored back.</p>  <p>In the example, the boundaries restored are in red and put on the current layer. You can use selection filter to get them for further modification.&#160; There is a Test.dwg drawing included, for test purposes.</p>  <p>How To Use The Example:</p>  <p>1. Build the attached project with VC2010 or copy the code for “BzhRestoreHatchBoundary” to your own project</p>  <p>2. Startup AutoCAD </p>  <p>3. Load the ARX file</p>  <p>4. Open the drawing that you want to restore hatch boundaries</p>  <p>5. Issue the command with short name RHB or long name RESTOREHATCHBOUNDARY</p>  <p>6. Choose hatch entities that you would like to restore boundaries. You could choose multiple hatches one time</p>  <div style="padding-bottom: 0px; margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; float: none; padding-top: 0px" id="scid:fb3a1972-4489-4e52-abe7-25a00bb07fdf:786b717b-fc6b-4ad3-8876-c5b4396992ba" class="wlWriterEditableSmartContent"><p> <a href="http://adndevblog.typepad.com/restorehatchboundary.zip" target="_blank">RestoreHatchBoundary.zip</a></p></div>
