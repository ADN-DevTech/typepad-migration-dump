---
layout: "post"
title: "Reflecting on AutoCAD .NET"
date: "2007-01-23 18:20:52"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Visual Studio"
original_url: "https://www.keanw.com/2007/01/reflecting_on_a.html "
typepad_basename: "reflecting_on_a"
typepad_status: "Publish"
---

<p><strong>Reflection</strong></p>

<p>Now we’re finally going to spend some time looking at <a href="http://msdn2.microsoft.com/en-us/library/f7ykdhsy.aspx">Reflection</a>.</p>

<p>As mentioned in <a href="http://through-the-interface.typepad.com/through_the_interface/2007/01/protecting_inte.html">a previous post</a>, .NET assemblies include intermediate language instructions plus metadata about types, members and assembly references. It is possible to access this information through Reflection.</p>

<p>For another definition of Reflection, here’s what <a href="http://msdn2.microsoft.com/en-us/library/system.reflection.aspx">MSDN</a> has to say about System.Reflection:</p><blockquote dir="ltr"><p><em>The System.Reflection namespace contains types that retrieve information about assemblies, modules, members, parameters, and other entities in managed code by examining their metadata. These types also can be used to manipulate instances of loaded types, for example to hook up events or to invoke methods. To dynamically create types, use the System.Reflection.Emit namespace.</em></p></blockquote><p>Just as COM uses Type Libraries to store type information, .NET has type information stored directly in the assemblies themselves (which is also possible with COM, but not essential). This rich type information can be accessed programmatically using Reflection. In fact my first ever AutoCAD .NET sample used reflection to go through and “dump” information about the various types in AutoCAD’s managed API to a tree control: it’s called DrawingBrowser, and can be found under samples/dotNet/DrawingBrowser on the ObjectARX SDK.</p>

<p><a onclick="window.open(this.href, '_blank', 'width=343,height=621,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/browsedwg.png"><img title="Browsedwg" height="543" alt="Browsedwg" src="/assets/browsedwg.png" width="300" border="0" /></a> </p>

<p>Just to use as an example, here's the VB code that makes up the main BROWSEDWG command in this sample (I didn't edit the code to fit the width of this blog, so it looks a little messy):</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; &lt;Autodesk.AutoCAD.Runtime.CommandMethod(<span style="COLOR: maroon">&quot;BrowseDWG&quot;</span>)&gt; _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">Public</span> <span style="COLOR: blue">Sub</span> BrowseDrawing()</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Dim</span> dlg <span style="COLOR: blue">As</span> <span style="COLOR: blue">New</span> BrowseDialog()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;dlg.Show()</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;dlg.TreeView1.BeginUpdate()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;Cursor.Current = Cursors.AppStarting</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;dlg.TreeView1.Nodes.Clear()</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Dim</span> rootNode <span style="COLOR: blue">As</span> TreeNode = dlg.TreeView1.Nodes.Add(<span style="COLOR: maroon">&quot;Database&quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;ParseDWG.fillIcons(dlg.TreeView1)</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Dim</span> myT <span style="COLOR: blue">As</span> Transaction = HostApplicationServices.WorkingDatabase.TransactionManager.StartTransaction()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;rootNode.Text = HostApplicationServices.WorkingDatabase.Filename</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;ParseDWG.ExtractObjectInfo(rootNode, HostApplicationServices.WorkingDatabase)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;myT.Commit()</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;ParseDWG.Cleanup()</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;Cursor.Current = Cursors.Default</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">'dlg.TreeView1.ExpandAll()</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;dlg.TreeView1.EndUpdate()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">End</span> <span style="COLOR: blue">Sub</span></p></div>

<p><strong>Reflector</strong></p>

<p>The Reflection sub-system was also originally used to develop the Reflector tool – an incredibly useful piece of software written by Lutz Roeder that is available from <a href="http://www.aisto.com/roeder/dotnet/">http://www.aisto.com/roeder/dotnet/</a> - that really takes the lid off .NET. Reflector no longer uses reflection, interestingly – apparently it makes use of unmanaged APIs to access the same information, but I’m not sure of the reason for this change.</p>

<p>So what’s so great about this tool? Well, it enables you to:</p>

<ol><li>Assess what other people might be able to glean from your own assemblies, should they choose to take a look.</li>

<li>Learn from people who are happy for you to be looking at how they do things (see below for examples of that).</li>

<li>Optimise your own coding by looking at the IL created by various bits of code – it also helps you understand what programming techniques are functionally equivalent (but perhaps easier to understand - examples are the use of foreach or using, functions that are functionally equivalent to more longwinded approaches).</li></ol>

<p>Here’s what <a href="http://msdn.microsoft.com/msdnmag/issues/04/07/MustHaveTools/#S8">MSDN Magazine</a> says about Reflector:</p><blockquote dir="ltr"><p><em>Using .NET Reflector, you can browse the classes and methods of an assembly, you can examine the Microsoft intermediate language (MSIL) generated by these classes and methods, and you can decompile the classes and methods and see the equivalent in C# or Visual Basic® .NET.</em></p></blockquote><p>So let's use Reflector to take a look at the code in the DrawingBrowser sample, to see what's visible if we don't obfuscate. I loaded the built assembly into Reflector, and browsed down to the code, using Tools-&gt;Disassembler to bring up a view on the disassembled source (or IL, if you'd rather see that):</p>

<p><a onclick="window.open(this.href, '_blank', 'width=686,height=593,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/reflector_1.png"><img title="Reflector_1" height="259" alt="Reflector_1" src="/assets/reflector_1.png" width="300" border="0" /></a> </p>

<p>The code you can retrieve with the tool is almost identical to that found in the source project, other than the fact that comments and variable names have been removed (the variable names have been replaced with generic ones based on the datatype).</p>

<p>For fun, let's see what we can see once we've obfuscated the assembly using the Dotfuscator Community Edition...</p>

<p>A few tips on Dotfuscating AutoCAD .NET assemblies: I found it easiest to set &quot;Copy Local&quot; to false on the references to the standard AutoCAD assemblies in the project (acmgd.dll and acdbmgd.dll), and then set the output location to be the AutoCAD program folder. This allowed the tool to resolve the various references properly.</p>

<p>Otherwise I just used the standard settings and picked the obfuscated assembly from the \Dotfuscated subdirectory of the config folder in the settings.</p>

<p>Loading it in Reflector, you can see straight away it's harder to make sense of. Just finding the appropriate command method took some time:</p>

<p><a onclick="window.open(this.href, '_blank', 'width=750,height=559,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/reflector_2.png"><img title="Reflector_2" height="223" alt="Reflector_2" src="/assets/reflector_2.png" width="300" border="0" /></a> </p>

<p>The code is suddenly a lot harder to follow - especially as it makes use of a number of classes defined in the project (which have been renamed to have incomprehensible names).</p>

<p>So let's look at the second major use for the Reflector - learning from existing assemblies. Here's some more information from the MSDN Magazine article about Reflector:</p><blockquote dir="ltr"><p><em>The .NET Framework offers many different ways to perform similar operations. For example, if you need to read a set of data from XML, there are a variety of different ways to do this using XmlDocument, XPathNavigator, or XmlReader. By using .NET Reflector, you can see what Microsoft used when writing the ReadXml method of the DataSet, or what they did when reading data from the configuration files. .NET Reflector is also an excellent way to see the best practices for creating objects like HttpHandlers or configuration handlers because you get to see how the team at Microsoft actually built those objects in the Framework.</em></p></blockquote><p>This is a very interesting point, and one that is very relevant to Autodesk’s own managed implementation. The AutoCAD Engineering team does not obfuscate its managed code, for instance, as it sees value in developers being able to take a look at it, to learn by example.</p>

<p>There are clearly potentially cases where there’s a much greater need to obfuscate code – licensing systems are one example (here’s <a href="http://www.structuretoobig.com/home/read.aspx?m=2&amp;y=2006">an interesting article</a> on that topic), but as AutoCAD’s licensing subsystem is in unmanaged code that isn't an issue.</p>

<p>If you use the reflector on acmgd.dll and acdbmgd.dll you’ll come across one or two areas of interest – where we’re doing more than a straight pass-through to the underlying ObjectARX code – but it’s generally not very exciting. Here's an example of one of the relatively few actual functions in the standard assemblies:</p>

<p><a onclick="window.open(this.href, '_blank', 'width=800,height=648,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/reflector_3.png"><img title="Reflector_3" height="243" alt="Reflector_3" src="/assets/reflector_3.png" width="300" border="0" /></a> </p>

<p>As you can see, even that is a little difficult to understand, as it uses some quite obscure-looking datatypes and much dereferencing of pointers.</p>

<p>Other managed components yield more interesting information, however (the ones that actually make use of the managed layer). One example is AcLayerTools.dll. Loading this assembly into Reflector shows some quite juicy chunks of code:</p>

<p><a onclick="window.open(this.href, '_blank', 'width=800,height=612,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/reflector_4_1.png"><img title="Reflector_4_1" height="229" alt="Reflector_4_1" src="/assets/reflector_4_1.png" width="300" border="0" /></a> </p>

<p>So that's about it for this post... Hopefully you're able to see the potential for this tool, and it also reinforces the importance of thinking about which code you should protect and which you need not.</p>

<p>As a final note, for those that are interested Reflector also exposes an API that can be used in applications, according to <a href="http://msdn.microsoft.com/msdnmag/issues/06/03/TestRun/#S3">another MSDN Magazine article</a>.</p>
