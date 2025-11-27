---
layout: "post"
title: "Does Dynamo (and Revit) crash for your projects? We'd love to hear from you!"
date: "2019-08-30 09:44:00"
author: "Kean Walmsley"
categories:
  - "Autodesk"
  - "Autodesk Research"
  - "Dynamo"
  - "Python"
original_url: "https://www.keanw.com/2019/08/does-dynamo-and-revit-crash-for-your-projects-wed-love-to-hear-from-you.html "
typepad_basename: "does-dynamo-and-revit-crash-for-your-projects-wed-love-to-hear-from-you"
typepad_status: "Publish"
---

<p><a href="https://forum.dynamobim.com/t/dynamo-geometry-stability-improvements-request-for-feedback" rel="noopener" target="_blank">An interesting post over on the DynamoBIM forum</a> was brought to my attention, and I felt it worth reproducing it here. The Dynamo team have found a way to avoid the need to manually dispose of geometry when creating it from Python or C# – as many package developers do – which they believe will help avoid a whole lot of crashes when working with large geometry sets. If you suspect you’re hitting these issues – due to your own code or a package you use – and can help test this new capability, we’d really appeciate you getting in touch.</p>
<p>As I didn’t have to write much content for this post, I did spend a couple of minutes (yes, as much that!) creating the below accompanying image. I honestly can’t claim to be particularly proud of it, but there you go. At this stage I can&#39;t unsee it, and unfortunately neither can you. :-)</p>
<p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e20240a47c7454200c-pi" rel="noopener" target="_blank"><img alt="Stop Dynamo crashing" border="0" height="500" src="/assets/image_304333.jpg" style="margin: 30px auto; float: none; display: block; background-image: none;" title="Stop Dynamo crashing" width="500" /></a></p>
<p>Here’s the text of the forum post by Michael Kirschner…</p>
<blockquote>
<p>Hello Dynamo users and Dynamo package developers,</p>
<p>We’ve been working for a while on improving the stability of the Dynamo geometry library. A major stumbling block for developing stable Dynamo packages - both internal to Autodesk and for external node and package authors is the <strong>requirement that memory be managed manually when using Dynamo Geometry types from Python and C#</strong>. <a href="https://github.com/DynamoDS/Dynamo/wiki/Zero-Touch-Plugin-Development#dispose--using-statement" rel="noopener" target="_blank">Here’s some more information on this</a>.</p>
<p>The dreaded <strong>system access violation</strong> or mysterious hang and crash of Revit/Dynamo!</p>
<p>A small snippet that reproduces the issue in python is:</p>
<pre><code>for i in range(100000):
    x = Cuboid.ByLengths(0,0,0)

OUT = &quot;some string&quot;
</code></pre>
<p>If you are not familiar with garbage collection – the idea here is that the runtime (in this case .NET) detects that the cubes have gone out of scope, and cannot be referenced from anywhere else in your code, including the Dynamo graph – because they have not been returned – and tries to clean their memory up.</p>
<p>Unfortunately, this causes issues with the Dynamo geometry library, and as advised in the link above, should be avoided by manually calling <code>dispose()</code> on geometry that is not returned or referenced.</p>
<p>This is a pretty big burden when writing exploratory code, and hard to get right.</p>
<p>Well, I have some good news.</p>
<p><a href="/assets/a54bd46b7327cc55cb0c050f30b495ed33df139a.jpeg"><img alt="image" height="500" src="/assets/a54bd46b7327cc55cb0c050f30b495ed33df139a.jpeg" style="margin: 30px auto; float: none; display: block;" width="341" /></a></p>
<p>In the latest daily builds of Dynamo master (<a href="https://dyn-builds-data.s3-us-west-2.amazonaws.com/DynamoCoreRuntime_2.5.0.5845_20190808T1234.zip">DynamoCoreRuntime_2.5.0.5845 8</a>) – we have removed <strong>the requirement for you to manually manage memory</strong> for most use cases.</p>
<p>Yay!</p>
<p>But what’s the catch?</p>
<p>There are some complex use cases that still require manual disposal, and during our investigation into this issue we believe we have identified some good rules to follow:</p>
<ul>
<li>
<p>If you are using the Dynamo Geometry library namespaces (<strong>Autodesk.DesignScript.Geometry – i.e. ProtoGeometry.dll</strong>) – from C# or Python and you are not spinning up your own threads, or using classes which create threads (Task, Thread, Parallel etc) – then you should be safe to no longer manually dispose geometry which you do not return – Dynamo will do it for you safely.</p>
</li>
<li>
<p>If you are using <strong>Autodesk.DesignScript.Geometry</strong> classes, and you are using them in a multi-threaded way - you are still responsible for disposing of that geometry which you do not return into the graph or hold onto in your classes. <strong>You must do this on the same threads which you created the geometry!</strong></p>
</li>
<li>
<p>The multi-threaded safety of LibG has not been satisfactorily tested, and we have never guaranteed its thread safety, and are not doing so now.</p>
</li>
<li>
<p>You are still free to manually dispose geometry if you want to and have determined it is the best option for your use case. This means, you don’t need to update your packages to take advantage of this fix! If you are writing a library for others to use, manually disposing is still a good practice.</p>
</li>
</ul>
<p><strong>What do we need from you?</strong></p>
<p>We need some help testing this change, as it is a fundamental change to the geometry library. If you have a graph using packages, zero touch nodes, or python nodes that create geometry – and you have seen the crashing behavior described above as you run this graph on larger sets of geometry, we would love to hear your feedback after you try out the latest daily build.</p>
<p>You’ll need to run these tests in sandbox, so no Revit interactions of course (unless you want to move some dlls around).</p>
<p>Please check out these changes in the latest daily build and let us know what you think or if you find any improvements or strange behavior. We are still internally profiling and testing this change, but wanted to get it out for feedback now.</p>
</blockquote>
<p>If you’re able to test this new capability and have some feedback regarding it, please respond via <a href="https://forum.dynamobim.com/t/dynamo-geometry-stability-improvements-request-for-feedback" rel="noopener" target="_blank">this DynamoBIM forum thread</a>. In the Research team we’ll be looking into our projects that create stacks of geometry (such as <a href="http://autode.sk/mars-graph" rel="noopener" target="_blank">the MaRS graph</a>) to see if it impacts performance in any way.</p>
