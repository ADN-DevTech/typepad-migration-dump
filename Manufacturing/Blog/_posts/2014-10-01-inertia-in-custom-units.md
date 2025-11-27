---
layout: "post"
title: "Inertia in custom units"
date: "2014-10-01 16:42:00"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/10/inertia-in-custom-units.html "
typepad_basename: "inertia-in-custom-units"
typepad_status: "Publish"
---

<p>By&nbsp;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>As mentioned in this blog post internal units are fixed so that <strong>mass</strong> is always <strong>kg</strong>, <strong>length</strong> is always <strong>cm</strong>, etc: <a title="" href="http://adndevblog.typepad.com/manufacturing/2012/07/unitsofmeasure-object-and-example.html" target="_self">http://adndevblog.typepad.com/manufacturing/2012/07/unitsofmeasure-object-and-example.html</a></p>
<p>The same is true for compound units like the one for inertia, which is <strong>mass * length ^ 2</strong>, and so internally it's <strong>kg cm^2</strong></p>
<p>Based on the document settings it might be displayed as <strong>lbmass in^2</strong>. You can easily convert the internal unit to that using the above mentioned <strong>UnitsOfMeasure</strong> object:</p>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb07908960970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb07908960970d image-full img-responsive" title="Units" src="/assets/image_d8e834.jpg" alt="Units" border="0" /></a></p>
<pre>Sub UnitTest()
  Dim uom As UnitsOfMeasure
  Set uom = ThisApplication.UnitsOfMeasure
  
  Dim conversion As Double
  conversion = uom.ConvertUnits(1, "kg cm^2", "lbmass in^2")
  
  Dim doc As PartDocument
  Set doc = ThisApplication.ActiveDocument
   
  Dim mp As MassProperties
  Set mp = doc.ComponentDefinition.MassProperties
   
  Dim m As Double
  m = mp.Mass
  
  ' Internal units are cm, kg
  Dim Ixx As Double, Iyy As Double, Izz As Double
  Dim Ixy As Double, Iyz As Double, Ixz As Double
  Call mp.XYZMomentsOfInertia(Ixx, Iyy, Izz, Ixy, Iyz, Ixz)
  
  Dim Ixx2 As Double, Iyy2 As Double, Izz2 As Double
  Dim Ixy2 As Double, Iyz2 As Double, Ixz2 As Double
  Ixx2 = Ixx * conversion
  Iyy2 = Iyy * conversion
  Izz2 = Izz * conversion
  Ixy2 = Ixy * conversion
  Iyz2 = Iyz * conversion
  Ixz2 = Ixz * conversion
End Sub</pre>
