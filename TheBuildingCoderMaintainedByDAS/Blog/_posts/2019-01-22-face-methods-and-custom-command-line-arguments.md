---
layout: "post"
title: "Face Methods and Custom Command Line Arguments"
date: "2019-01-22 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Algorithm"
  - "C++"
  - "Cloud"
  - "External"
  - "Family"
  - "Geometry"
  - "Getting Started"
  - "SDK Samples"
original_url: "https://thebuildingcoder.typepad.com/blog/2019/01/face-methods-and-custom-command-line-arguments.html "
typepad_basename: "face-methods-and-custom-command-line-arguments"
typepad_status: "Publish"
---

<p>Inundated with interesting topics, let's skim a few topmost ones off:</p>

<ul>
<li><a href="#2">Passing an add-in custom command line parameters</a> </li>
<li><a href="#3">Useful methods help verify a <code>Face</code> is rectangular</a> </li>
<li><a href="#4">Automate complex family creation</a> </li>
<li><a href="#5">C++ code extracts 3D line segments from point cloud</a> </li>
<li><a href="#6">Big data visualisation and storytelling</a> </li>
</ul>

<h4><a name="2"></a> Passing an Add-In Custom Command Line Parameters</h4>

<p>Morten Bastue Jacobsen, Senior BIM Specialist of <a href="https://ramboll.com">Ramboll</a>,
discovered a way to pass in Custom Command Line Parameters to Revit.exe to be picked up by an add-in.</p>

<p><a href="https://thebuildingcoder.typepad.com/blog/2017/01/distances-switches-kiss-ing-and-a-dino.html#3">Revit implements built-in support for certain command line arguments</a>,
and their meaning obviously cannot be changed or customised.</p>

<p>If you specify a command line argument that is not one of the 'switches' listed, then the first one must be the path to a Revit model to open, and the second one must be the path to a journal file to replay.</p>

<p>No other non-switch command line arguments seem to be supported.</p>

<p>However, when we start <code>revit.exe</code> with custom parameters, the process terminates without any error messages.</p>

<p>Therefore, if we want to add custom parameters, the first parameter to the Revit.exe command must be name of a Revit project file.</p>

<p>This works:</p>

<ul>
<li>"C:\Program Files\Autodesk\Revit 2019\Revit.exe" "C:\temp\Revit_export\958-mobj_test.rvt" ---- C:\temp\Revit_export\2d_export_test.txt</li>
</ul>

<p>This doesn't work:</p>

<ul>
<li>"C:\Program Files\Autodesk\Revit 2019\Revit.exe" ---- C:\temp\Revit_export\2d_export_test.txt</li>
</ul>

<p>The purpose of starting the process with custom parameters is to read out these parameters in an add-in and perform automation tasks on the models.</p>

<p>Exploring this further, I discovered the following behaviour in Revit:</p>

<p>If Revit.exe is started with parameters like <code>revit.exe</code> <code>-param1</code>, the process terminates with a Windows error without any warning.</p>

<p>I think that no Windows process should ever behave like that.</p>

<p>However, these two alternatives work:</p>

<pre>
  revit.exe &lt;path to revit model&gt; -param1
  revit.exe /param1
</pre>

<p>So, my workaround is to format my custom parameters with a forward slash <code>/</code>.</p>

<p>Many thanks to Morten for discovering and sharing this.</p>

<h4><a name="3"></a> Useful Methods Help Verify a <code>Face</code> is Rectangular</h4>

<p>Alexander <a href="https://forums.autodesk.com/t5/user/viewprofilepage/user-id/1257478">@aignatovich</a> Ignatovich, aka Александр Игнатович,
pointed out some useful methods on the <code>CurveLoop</code> class that help solve
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> task
to <a href="https://forums.autodesk.com/t5/revit-api-forum/verify-a-face-is-rectangular/m-p/8520751">verify a face is rectangular</a>:</p>

<p><strong>Question:</strong> From a slab, I get the geometry and then the upper face.</p>

<p>I'd like to verify this face is a rectangle. Does anyone have an idea?</p>

<p><strong>Answer:</strong> Alexander provides a succinct solution demonstrating the use of some interesting methods on the <code>CurveLoop</code> class:</p>

<ul>
<li><a href="https://apidocs.co/apps/revit/2019/ca966f5d-7db8-b28a-928e-12063dd143e6.htm"><code>IsCounterclockwise</code></a>,</li>
<li><a href="https://apidocs.co/apps/revit/2019/5a82c7ad-4b6e-a62c-6b0c-7fe790886995.htm"><code>IsRectangular</code></a></li>
</ul>

<p>He suggests:</p>

<ul>
<li>Get the edge curve loops: <code>face.GetEdgesAsCurveLoops()</code></li>
<li>Find the outermost loop: <code>loop</code> <code>=</code> <code>face.GetEdgesAsCurveLoops().First(</code> <code>x</code> <code>=&gt;</code> <code>x.IsCounterclockwise(</code> <code>face.ComputeNormal(</code> <code>UV.Zero</code> <code>))</code></li>
<li>Check if the curve loop has a plane: <code>curveloop.HasPlane()</code></li>
<li>Check if it is rectangular: <code>curveloop.IsRectangular(</code> <code>curveLoop.GetPlane())</code></li>
</ul>

<p>Many thanks to Alexander for solving this and pointing out these interesting methods.</p>

<h4><a name="4"></a> Automate Complex Family Creation</h4>

<p>A non-forum question for a change, on automating the creation of complex Revit families, for example, an air handler or a chiller:</p>

<p><strong>Question:</strong> I have not worked with the Revit API at all and wanted to know your expert opinion on it.</p>

<p>Do you think it possible to completely automate drawing of complex Revit families (for example an Air Handler or a Chiller) using the Revit API in Autodesk Revit?</p>

<p>If so, could you point me to any Revit API training you know of?</p>

<p><strong>Answer:</strong> Yes, it is.</p>

<p>First, look at the <a href="https://thebuildingcoder.typepad.com/blog/about-the-author.html#2">Revit API getting started material</a>.</p>

<p>Once you have a grasp of that, you can turn straight to
the <a href="https://thebuildingcoder.typepad.com/blog/2010/11/connector-direction-and-createairhandler.html">Revit SDK sample FamilyCreation/CreateAirHandler</a> &ndash;
it creates an air handler and adds air and water piping and ductwork connectors to it.</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833022ad3d499c4200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833022ad3d499c4200b img-responsive" style="width: 299px; display: block; margin-left: auto; margin-right: auto;" alt="Air handler" title="Air handler" src="/assets/image_95e6f9.jpg" /></a><br /></p>

<p></center></p>

<h4><a name="5"></a> C++ Code Extracts 3D Line Segments from Point Cloud</h4>

<p>Moving away just a little bit from the Revit API...</p>

<p>Do you have a need to extract 3D line segments from point clouds?</p>

<p>If so, you might want to check out the new algorithm presented by 
the <a href="https://github.com/xiaohulugo/3DLineDetection">3DLineDetection C++ library</a>,
implementing a simple and efficient 3D line detection algorithm for large scale unorganised point clouds,
and the associated conference paper based on this code,
<a href="https://arxiv.org/abs/1901.02532">Fast 3D Line Segment Detection From Unorganized Point Cloud</a>, by Xiaohu Lu, Yahui Liu, and Kai Li.</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833022ad3b4f0c3200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833022ad3b4f0c3200d image-full img-responsive" alt="3D line detection" title="3D line detection" src="/assets/image_519c88.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<h4><a name="6"></a> Big Data Visualisation and Storytelling</h4>

<p>Finally, an interesting sample of analysing big data, visualising it effectively, and telling a story with it:
the interactive <a href="https://pudding.cool/2018/10/city_3d"><em>Human Terrain</em> visualization of population density over time</a>
by <a href="https://pudding.cool">The Pudding</a> shows an impressive example of all three steps.</p>
