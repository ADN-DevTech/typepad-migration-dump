---
layout: "post"
title: "Creating a circular slab"
date: "2012-05-01 17:01:10"
author: "Saikat Bhattacharya"
categories:
  - ".NET"
  - "Revit"
  - "Saikat Bhattacharya"
original_url: "https://adndevblog.typepad.com/aec/2012/05/creating-a-circular-slab.html "
typepad_basename: "creating-a-circular-slab"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/saikat-bhattacharya.html" target="_self">Saikat Bhattacharya</a></p>
<p>For creating a circular slab using the Revit API, simply creating a circular arc with start angle 0 and end angle of Math.Pi * 2 (which means 360 degrees) and passing this arc to the NewFloor() does not work. It throws an error.</p>
<p>The approach is to use two semi-circular arcs to complete the entire circular profile and then using these two semi-circular arcs in the NewFloor() method. The following short (and sweet) code snippet illustrates this approach:<span style="font-family: Courier New;"><span style="line-height: 11pt;"><span style="color: #008000;">&nbsp;</span></span></span></p>
<div style="background: white;">
<div style="background: white;">
<p style="margin: 0px;"><span style="line-height: 13pt; font-size: 8pt;"><span style="font-family: Courier New;"><span style="color: #008000;">// get access to application and document object</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New; font-size: 8pt;"><span style="line-height: 13pt;"><span style="color: #2b91af;">Document</span></span><span style="line-height: 13pt;"><span style="color: #000000;"> doc = commandData.Application.ActiveUIDocument.Document;</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New; font-size: 8pt;"><span style="line-height: 13pt;"><span style="color: #2b91af;">Application</span></span><span style="line-height: 13pt;"><span style="color: #000000;"> app = commandData.Application.Application;</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New; font-size: 8pt;"><span style="color: #000000;">&nbsp;</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New; font-size: 8pt;"><span style="line-height: 13pt;"><span style="color: #008000;">// Define the plane</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New; font-size: 8pt;"><span style="line-height: 13pt;"><span style="color: #000000;">Autodesk.Revit.DB.</span></span><span style="line-height: 13pt;"><span style="color: #2b91af;">Plane</span></span><span style="line-height: 13pt;"><span style="color: #000000;"> pln = </span></span><span style="line-height: 13pt;"><span style="color: #0000ff;">new</span></span><span style="line-height: 13pt;"><span style="color: #000000;"> Autodesk.Revit.DB.</span></span><span style="line-height: 13pt;"><span style="color: #2b91af;">Plane</span></span><span style="line-height: 13pt;"><span style="color: #000000;">(</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New; font-size: 8pt;"><span style="line-height: 13pt;"><span style="color: #2b91af;">&nbsp; XYZ</span></span><span style="line-height: 13pt;"><span style="color: #000000;">.BasisZ, </span></span><span style="line-height: 13pt;"><span style="color: #2b91af;">XYZ</span></span><span style="line-height: 13pt;"><span style="color: #000000;">.Zero);</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New; font-size: 8pt;"><span style="line-height: 13pt;"><span style="color: #2b91af;">CurveArray</span></span><span style="line-height: 13pt;"><span style="color: #000000;"> ca = </span></span><span style="line-height: 13pt;"><span style="color: #0000ff;">new</span></span><span style="line-height: 13pt;"><span style="color: #000000;"> </span></span><span style="line-height: 13pt;"><span style="color: #2b91af;">CurveArray</span></span><span style="line-height: 13pt;"><span style="color: #000000;">();</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New; font-size: 8pt;"><span style="color: #000000;">&nbsp;</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New; font-size: 8pt;"><span style="line-height: 13pt;"><span style="color: #008000;">// Start the transaction</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New; font-size: 8pt;"><span style="line-height: 13pt;"><span style="color: #2b91af;">Transaction</span></span><span style="line-height: 13pt;"><span style="color: #000000;"> trans = </span></span><span style="line-height: 13pt;"><span style="color: #0000ff;">new</span></span><span style="line-height: 13pt;"><span style="color: #000000;"> </span></span><span style="line-height: 13pt;"><span style="color: #2b91af;">Transaction</span></span><span style="line-height: 13pt;"><span style="color: #000000;">(doc, </span></span><span style="line-height: 13pt;"><span style="color: #a31515;">"slab"</span></span><span style="line-height: 13pt;"><span style="color: #000000;">);</span></span></span></p>
<p style="margin: 0px;"><span style="line-height: 13pt; font-size: 8pt;"><span style="font-family: Courier New;"><span style="color: #000000;">trans.Start();</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New; font-size: 8pt;"><span style="color: #000000;">&nbsp;</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New; font-size: 8pt;"><span style="line-height: 13pt;"><span style="color: #0000ff;">double</span></span><span style="line-height: 13pt;"><span style="color: #000000;"> radius = 40;</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New; font-size: 8pt;"><span style="line-height: 13pt;"><span style="color: #0000ff;">double</span></span><span style="line-height: 13pt;"><span style="color: #000000;"> startAngle = 0;&nbsp; </span></span><span style="line-height: 13pt;"><span style="color: #008000;">// unit : radians</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New; font-size: 8pt;"><span style="line-height: 13pt;"><span style="color: #0000ff;">double</span></span><span style="line-height: 13pt;"><span style="color: #000000;"> endAngle = </span></span><span style="line-height: 13pt;"><span style="color: #2b91af;">Math</span></span><span style="line-height: 13pt;"><span style="color: #000000;">.PI;&nbsp; </span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New; font-size: 8pt;"><span style="color: #000000;">&nbsp;</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New; font-size: 8pt;"><span style="line-height: 13pt;"><span style="color: #008000;">// Create the first arc</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New; font-size: 8pt;"><span style="line-height: 13pt;"><span style="color: #000000;">Autodesk.Revit.DB.</span></span><span style="line-height: 13pt;"><span style="color: #2b91af;">Arc</span></span><span style="line-height: 13pt;"><span style="color: #000000;"> a = app.Create.NewArc(</span></span></span></p>
<p style="margin: 0px;"><span style="line-height: 13pt; font-size: 8pt;"><span style="font-family: Courier New;"><span style="color: #000000;">&nbsp; pln, radius, startAngle, endAngle);</span></span></span></p>
<p style="margin: 0px;"><span style="line-height: 13pt; font-size: 8pt;"><span style="font-family: Courier New;"><span style="color: #000000;">ca.Append(a);</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New; font-size: 8pt;"><span style="color: #000000;">&nbsp;</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New; font-size: 8pt;"><span style="line-height: 13pt;"><span style="color: #008000;">// Create the second arc</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New; font-size: 8pt;"><span style="line-height: 13pt;"><span style="color: #000000;">Autodesk.Revit.DB.</span></span><span style="line-height: 13pt;"><span style="color: #2b91af;">Arc</span></span><span style="line-height: 13pt;"><span style="color: #000000;"> a1 = app.Create.NewArc(</span></span></span></p>
<p style="margin: 0px;"><span style="line-height: 13pt; font-size: 8pt;"><span style="font-family: Courier New;"><span style="color: #000000;">&nbsp; pln, radius, -endAngle, startAngle);</span></span></span></p>
<p style="margin: 0px;"><span style="line-height: 13pt; font-size: 8pt;"><span style="font-family: Courier New;"><span style="color: #000000;">ca.Append(a1);</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New; font-size: 8pt;"><span style="color: #000000;">&nbsp;</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New; font-size: 8pt;"><span style="line-height: 13pt;"><span style="color: #000000;">doc.Create.NewFloor(ca, </span></span><span style="line-height: 13pt;"><span style="color: #0000ff;">true</span></span><span style="line-height: 13pt;"><span style="color: #000000;">);</span></span></span></p>
<p style="margin: 0px;"><span style="line-height: 13pt; font-size: 8pt;"><span style="font-family: Courier New;"><span style="color: #000000;">trans.Commit();</span></span></span></p>
</div>
</div>
<p>And this creates a neat circular slab!</p>
