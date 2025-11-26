---
layout: "post"
title: "AutoCAD 2020: Quick Overview"
date: "2019-03-31 22:09:00"
author: "Madhukar Moogala"
categories:
  - "AutoCAD"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2019/03/autocad-2020-quick-overview.html "
typepad_basename: "autocad-2020-quick-overview"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar 
Moogala</a></p><p><br></p><p><strong>Alert</strong>: 32-bit version is no longer available.</p><p>&nbsp;&nbsp; <a href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a49b5373200b-pi"><img width="252" height="273" title="image" style="display: inline; background-image: none;" alt="image" src="/assets/image_187589.jpg" border="0"></a></p><p><br></p><p><br></p><p><strong>Overview</strong></p><p>AutoCAD 2020 provides a set of enhancements based on customer feedback, surveys, and analytic data that prioritize our efforts. Several features are the result of the need to modernize and streamline frequently used features across many customer disciplines.</p><p><strong>New Dark Theme</strong></p><p><a href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a49b537c200b-pi"><img width="460" height="274" title="image" style="display: inline; background-image: none;" alt="image" src="/assets/image_455100.jpg" border="0"></a></p><p><strong>Block Palettes</strong></p><p><a href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a476c142200d-pi"><img width="359" height="410" title="image" style="display: inline; background-image: none;" alt="image" src="/assets/image_428210.jpg" border="0"></a></p><p><strong><br></strong></p><p><strong>Purge Dialog box Redesign</strong></p><p><a href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a49b5385200b-pi"><img width="510" height="456" title="image" style="display: inline; background-image: none;" alt="image" src="/assets/image_765193.jpg" border="0"></a></p><p><strong>DWG Compare Enhancements</strong></p><p><a href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a44d83e7200c-pi"><img width="517" height="487" title="image" style="display: inline; background-image: none;" alt="image" src="/assets/image_319390.jpg" border="0"></a></p><p><strong>Measure Geometry Option–Quick Measure</strong></p><p><a href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a476c147200d-pi"><img width="326" height="322" title="image" style="display: inline; background-image: none;" alt="image" src="/assets/image_267420.jpg" border="0"></a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <a href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a44d83f4200c-pi"><img width="318" height="298" title="image" style="display: inline; background-image: none;" alt="image" src="/assets/image_171951.jpg" border="0"></a></p><p><br></p><p><strong>Save to Web &amp; Mobile Enhancements</strong></p><p><a href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a44d83f8200c-pi"><br></a></p><p><strong><a href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a44d83fe200c-pi"><img width="394" height="196" title="image" style="display: inline; background-image: none;" alt="image" src="/assets/image_307186.jpg" border="0"></a></strong></p><p>To get detailed overview on AutoCAD 2020 product I encourage you to follow <a href="https://blogs.autodesk.com/autocad/introducing-autocad-2020-autocad-lt-2020/" target="_blank">Autodesk AutoCAD Blog</a></p><p><br></p><p><strong>API Updates</strong>:</p><p><strong>ObjectARX</strong> : No New APIs.</p><p><strong>.NET</strong> : No New APIs</p><p><strong>Activex</strong>:</p><p>AcadApplication now supports the progIds "<strong>AutoCAD.Application.23</strong>" and "<strong>AutoCAD.Application.23.1</strong>" and <em>AcadComparedReference</em> has been added. </p><p>The AcadComparedReference object represents the drawing in which you are comparing the current drawing against. AcadComparedReference inherits from the AcadExternalReference object. The following is a basic example how you might go about determining whether an Xref is of the AcadComparedReference type: </p><p><br></p><p><br></p>
<pre>' Checks to see if an object is of the ComparedReference type
Sub CheckForComparedReference()
Dim ent As AcadEntity
Dim comRef As AcadComparedReference
On Error Resume Next
' Step through all the objects in model space
For Each ent In ThisDrawing.ModelSpace
' Check to see if the object is a Block Reference
If ent.ObjectName = "AcDbBlockReference" Then
' Try to cast the entity (Block Reference) to a ComparedReference
Set comRef = ent
' If an occurs, then the entity is not a ComparedReference
If Err &lt;&gt; 0 Then
MsgBox "Not a Compared Reference"
Else
MsgBox "Xref Name: " + comRef.Name + vbLf + "Compared Reference"
End If
' Clear the Error object
Err.Clear
End If
Next ent
End Sub

</pre>
<p><strong>AutoLISP</strong>:</p><p>There are no new functions or additions.<br>
Xrefs related to the Drawing Compare feature are known as ComparedReference objects. The following is a basic statement that demonstrates how to return the IsComparedReference property of an external reference to determine whether it’s a ComparedReference object</p><p><br></p>
<pre>(getpropertyvalue (car (entsel)) "IsComparedReference")</pre>
<p><strong>ObjectARX and Managed .NET</strong> </p>
<div>AutoCAD 2020 is a binary compatible release, so ObjectARX and Managed .NET applications built for AutoCAD 2019 should load and function without any problems in AutoCAD 2020. The following configuration is officially supported to develop </div><div><br></div><div><b>ObjectARX and/or Managed .NET applications for AutoCAD 2020:</b></div><div><ul><li>Microsoft Visual Studio 2017 with Update 2 (version 15.7.5 and earlier)</li><li>Microsoft .NET Framework 4.7</li></ul></div><div><strong>Note:</strong> The product release value used in the Windows Registry and the ObjectARX API version has been updated from R23.0 to R23.1.</div><p><!-- x-textbox/html --></p><div>For additional information, refer to the ObjectARX Reference Guide as well as the Readme for the latest changes.</div><div><br></div><div><strong>Getting ObjectARX 2020 SDK</strong></div><div><strong><br></strong></div><div><a href="https://www.autodesk.com/developer-network/platform-technologies/autocad/objectarx-license-download" target="_blank">Fill up&nbsp; your details and agree to terms and conditions.</a></div><div>It will take you downloads page.</div><div><br></div><div><a href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a476c14c200d-pi"><img width="605" height="234" title="image" style="display: inline; background-image: none;" alt="image" src="/assets/image_142615.jpg" border="0"></a></div>
