---
layout: "post"
title: "Inventor:  Creating work features in VB.NET"
date: "2013-01-06 03:25:41"
author: "Vladimir Ananyev"
categories:
  - "AutoCAD Mechanical"
  - "Inventor"
  - "Vladimir Ananyev"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/01/creating-work-features-using-the-inventor-api-in-vbnet.html "
typepad_basename: "creating-work-features-using-the-inventor-api-in-vbnet"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/manufacturing/vladimir-ananyev.html">Vladimir Ananyev</a></p>
<p><strong>Issue</strong></p>
<p>Is there a VB.NET example available for creating work features? </p>
<p><a name="section2"></a><strong>Solution</strong></p>
<p>Work features are objects used to create and position other features and are used when current geometry is not sufficient for constructing features. There are three types of Work features: </p>
<p><strong>Work points     <br /></strong>Work points are placed on faces, linear edges or on arcs and circles. They can be used to mark pattern centers, define coordinate systems, and define planes and 3d paths.</p>
<p><strong>Work axes     <br /></strong>Work axes can be used to mark symmetry lines, centerlines or distance between revolved features.</p>
<p><strong>Work planes     <br /></strong>Work planes can be used when constructing axes, sketch planes, termination planes and to sketch entities on a part face when it is not available as a sketch plane.</p>
<p>When you use the Inventor user interface to create work features you are able to select objects &quot;visually&quot;. For example in the case of creating a workplane tangent to the cylindrical face and parallel to another planar face, in the UI all you have to do is select the cylindrical face and then select the planar face. When doing this using the API we do not have the visual feedback and it is sometimes necessary to query for geometry information in order to determine the shape and orientation of the faces. </p>
<p>One of the attached zip files provides a VB.NET example that uses different methods to create work features. A part named “test.ipt” (2008 format) is included in the zip file and can be used with this example. Currently the code needs to find this ipt in the c:\temp. Open this file and notice there are no user defined work features. Close the ipt and then run the project. In the Form1_Load procedure test.ipt is opened and the work features are added. </p>
<p>This example creates a workplane tangent to a cylindrical face and parallel to a planar face. It does this using Brep to obtain the Face whose shape is cylindrical, and then query for a point on the face whose normal is parallel to the planar face. </p>
<p>See the help file for other methods for creating work features. (Such as AddByPlaneAndTangent)</p>
<p>&#0160;</p>
<p>
<span class="asset  asset-generic at-xid-6a0167607c2431970b017c355d190f970b"><a href="http://adndevblog.typepad.com/files/test.ipt">Download Test</a>&#0160;(.ipt file)</span></p>
<p><span class="asset  asset-generic at-xid-6a0167607c2431970b017c355d190f970b">
<span class="asset  asset-generic at-xid-6a0167607c2431970b017ee7006eea970d"><a href="http://adndevblog.typepad.com/files/workfeaturesvb_net.zip">Download WorkFeaturesVB_NET</a></span><br /></span></p>
<p>&#0160;</p>
