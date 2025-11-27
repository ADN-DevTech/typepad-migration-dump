---
layout: "post"
title: "Add the sketch using DrawingView Sketches when creating a section view"
date: "2016-07-15 16:23:17"
author: "Wayne Brill"
categories:
  - "Inventor"
  - "Wayne Brill"
original_url: "https://adndevblog.typepad.com/manufacturing/2016/07/add-the-sketch-using-drawingview-sketches-when-creating-a-section-view.html "
typepad_basename: "add-the-sketch-using-drawingview-sketches-when-creating-a-section-view"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/wayne-brill.html" target="_self">Wayne Brill</a></p> <p>In the SDK help--&gt;Inventor API User's Manual----&gt; Drawings---&gt;Working With Drawing Views---&gt; “Creating a section drawing view” it includes this code:</p> <p><font style="background-color: #cccccc">Dim oDrawingSketch As DrawingSketch<br>Set oDrawingSketch = <strong>oSheet</strong>.Sketches.Add</font></p> <p>AddSectionView() fails when the Drawing Sketch is added using the Sheet Sketches. </p> <p>If the DrawingSketch is added using the View Sketches the error goes away and the Section View is created.</p> <p><font style="background-color: #cccccc">Dim oDrawingSketch As DrawingSketch</font><br><font style="background-color: #cccccc" color="#0000ff">’ Error goes away on AddSectionView() using the base view sketches collection</font><br><font style="background-color: #cccccc">Set oDrawingSketch = <strong>oView1</strong>.Sketches.Add</font></p> <p>This issue with the sample in the SDK help has been reported to Inventor Engineering.</p> <p><font color="#000000"></font></p>
