---
layout: "post"
title: "AU Handouts: Enriching Your DWF™ - Part 1"
date: "2007-10-26 09:10:00"
author: "Kean Walmsley"
categories:
  - "AU"
  - "AutoCAD"
  - "AutoCAD .NET"
  - "DWF"
  - "Training"
original_url: "https://www.keanw.com/2007/10/au-handouts-enr.html "
typepad_basename: "au-handouts-enr"
typepad_status: "Publish"
---

<p><strong>Introduction</strong></p>

<p>This session focuses on technologies that can be used to view and access DWF content downstream from Autodesk’s design products. Yesterday’s session focused on DWG – we’re going to take the model containing 3D solids that we created during that session and publish it to DWF, adding custom metadata. We can then look at how to harness this data in lightweight applications, whether to access it non-graphically or to view it.</p>

<p>Firstly, why are we using 3D solids in this example? The choice was somewhat arbitrary – the point is really to demonstrate the ability to access properties of objects stored in a DWG file without AutoCAD running – but it does suit our overall purpose for a few reasons:</p>

<ol><li>As 3D entities, 3D solids allow us to evaluate the 3D capabilities of the viewing technologies we’re looking at.</li>

<li>While they have precise geometric properties (such as volume), as DWF is not a double-precision format we’re going to focus on accessing data of a less precision-oriented nature: the solids’ materials.</li></ol>

<p>A quick word on the programming technology used in this demonstration. The code samples are in a mixture of languages: the AutoCAD .NET sample that adds metadata is in C#, the downstream DWF Toolkit sample is in C++ (for the DWF Toolkit component) and in VB.NET (for the user interface), while the Design Review and Freewheel samples are in HTML.</p>

<p>These handouts, along with the sample projects demonstrated within them, have been posted to my blog: <a href="http://blogs.autodesk.com/through-the-interface">http://blogs.autodesk.com/through-the-interface</a>. Only significant portions of code will be highlighted in this document, and even those should not need to be typed back in. :-)</p>

<p><strong>Creating DWFx files</strong></p>

<p>In AutoCAD 2008, the PUBLISH command creates DWF files. In the future, however, Autodesk will increasingly publish to the DWFx format, one which more based on industry (particularly Microsoft) standard technology. For more information on DWFx, search for it on the Autodesk website, and search for XPS on Microsoft’s website.</p>

<p>To create DWFx files from AutoCAD 2008 today, you need to download and install the DWFx Printer Driver, which can then be selected from the PLOT dialog:</p>

<p><a href="http://usa.autodesk.com/adsk/servlet/ps/dl/item?siteID=123112&amp;id=9124615&amp;linkID=9240618">http://usa.autodesk.com/adsk/servlet/ps/dl/item?siteID=123112&amp;id=9124615&amp;linkID=9240618</a></p>

<p>AutoCAD’s PLOT mechanism does support multiple sheet printing, but won’t let you plot both 2D and 3D sheets to the same file (unlike the PUBLISH command, which does).</p>

<p>The other technique for creating DWFx files is to save DWFs from Design Review 2008 as DWFx. This approach has the benefit of including any 3D and metadata content in the resultant DWFx file.</p>

<p><strong>Publishing DWF and plotting DWFx from AutoCAD</strong></p>

<p>Now we’re going to take a quick look at the contents of DWF and DWF files. For the purpose of this example, we’ll simply PUBLISH our 2D layout to DWF and PLOT it using the DWFx driver to DWFx.</p>

<p>Let's launch PUBLISH and remove the modelspace sheet, and then hit the “Publish” button, selecting a DWF file as the destination (in our case <strong>solids-Layout1.dwf</strong>):</p>

<p><a onclick="window.open(this.href, '_blank', 'width=762,height=437,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/2007/10/25/plotting_a_single_sheet_dwf_from_au.png"><img title="Plotting_a_single_sheet_dwf_from_au" height="172" alt="Plotting_a_single_sheet_dwf_from_au" src="/assets/plotting_a_single_sheet_dwf_from_au.png" width="300" border="0" /></a> </p>

<p><em>Figure 1 – publishing a single-sheet DWF file from AutoCAD</em></p>

<p>Now we launch the PLOT command and select the DWFx driver, using it to PLOT our 2D layout to a DWFx file (in our case <strong>solids-Layout1.dwfx</strong>):</p>

<p><a onclick="window.open(this.href, '_blank', 'width=517,height=567,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/2007/10/25/plotting_dwfx_from_autocad.png"><img title="Plotting_dwfx_from_autocad" height="329" alt="Plotting_dwfx_from_autocad" src="/assets/plotting_dwfx_from_autocad.png" width="300" border="0" /></a> </p>

<p><em>Figure 2 – plotting a single-sheet DWFx file from AutoCAD</em></p>

<p>So now we have two files, solids-Layout1.dwf &amp; solids-Layout1.dwfx. To look at their contents, we rename them to .zip and open them with WinZIP (both DWF and DWFx are package formats which are implemented as ZIP files of different types of data):</p>

<p><a onclick="window.open(this.href, '_blank', 'width=621,height=304,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/2007/10/25/contents_of_our_simple_dwf_file.png"><img title="Contents_of_our_simple_dwf_file" height="146" alt="Contents_of_our_simple_dwf_file" src="/assets/contents_of_our_simple_dwf_file.png" width="300" border="0" /></a> </p>

<p><em>Figure 3 – the contents of our DWF file</em></p>

<p>The interesting piece of our 2D DWF file is the .w2d file. If we extract it and open it in Notepad, we can see it’s contains both ASCII and binary information:</p>

<p><a onclick="window.open(this.href, '_blank', 'width=557,height=489,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/2007/10/25/contents_of_our_w2d_stream.png"><img title="Contents_of_our_w2d_stream" height="263" alt="Contents_of_our_w2d_stream" src="/assets/contents_of_our_w2d_stream.png" width="300" border="0" /></a> </p>

<p><em>Figure 4 – the contents of the W2D stream of our DWF</em></p>

<p>The big change between DWF and DWFx is the move away from the W2D format (which harks back to the days of the <em>Whip! </em>format) and adopting the more modern XML-based XPS format.</p>

<p>Let’s take a look at our DWFx file, now:</p>

<p><a onclick="window.open(this.href, '_blank', 'width=800,height=369,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/2007/10/25/contents_of_our_simple_dwfx_file.png"><img title="Contents_of_our_simple_dwfx_file" height="138" alt="Contents_of_our_simple_dwfx_file" src="/assets/contents_of_our_simple_dwfx_file.png" width="300" border="0" /></a> </p>

<p><em>Figure 5 – the contents of our DWFx file</em></p>

<p>We can see straight away that there’s more to it: to start with there are more files included (and the DWFx file is 16KB while our DWF file is 8KB, but the ratio shifts as the files get larger – the overhead in this example is high relative to the amount of content we’re storing). The interesting file is the selected XML file – let’s load that into Internet Explorer to view it:</p>

<p><a onclick="window.open(this.href, '_blank', 'width=800,height=674,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/2007/10/25/contents_of_our_w2x_stream.png"><img title="Contents_of_our_w2x_stream" height="252" alt="Contents_of_our_w2x_stream" src="/assets/contents_of_our_w2x_stream.png" width="300" border="0" /></a> </p>

<p><em>Figure 6 – the contents of the W2X stream of our DWFx</em></p>

<p>This XML content defines the 2D geometry in our DWFx file, and is read by the XPS Viewer when we load it.</p>

<p>That’s it for our brief look at the DWF and DWFx formats. We could certainly go into greater depth – especially when it comes to the storage of metadata and 3D, but this is left as an exercise for the reader.</p>

<p><strong>Publishing additional metadata to DWF files from AutoCAD</strong></p>

<p>Now we’re going to take a look at the more advanced capabilities of the PUBLISH command in AutoCAD. Our DWG file contains 3D content in the modelspace and 2D content in its paperspace layout, and we ideally want a DWF file containing both. Not only that, but we’d like to attach metadata to both our 2D and 3D geometry.</p>

<p>We’re going to use an AutoCAD .NET module to attach this information. Here’s the C# code:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.ApplicationServices;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.DatabaseServices;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.EditorInput;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.Runtime;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.Publishing;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">namespace</span> DWFMetadata</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">class</span> <span style="COLOR: teal">SolidsPublisher</span> : <span style="COLOR: teal">IExtensionApplication</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">private</span> <span style="COLOR: blue">bool</span> mbPublishingTo3D = <span style="COLOR: blue">false</span>;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> SolidsPublisher() {}</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> Initialize()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Register for publishing events</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Publisher</span> pb = <span style="COLOR: teal">Application</span>.Publisher;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;pb.AboutToBeginPublishing +=</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">new</span> <span style="COLOR: teal">AboutToBeginPublishingEventHandler</span>(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; OnAboutToBeginPublishing</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;pb.BeginEntity +=</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">new</span> <span style="COLOR: teal">BeginEntityEventHandler</span>(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; OnBeginEntity</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;pb.BeginSheet +=</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">new</span> <span style="COLOR: teal">BeginSheetEventHandler</span>(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; OnBeginSheet</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> Terminate()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Unregister from publishing events</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Publisher</span> pb = <span style="COLOR: teal">Application</span>.Publisher;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;pb.AboutToBeginPublishing -=</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">new</span> <span style="COLOR: teal">AboutToBeginPublishingEventHandler</span>(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; OnAboutToBeginPublishing</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;pb.BeginEntity -=</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">new</span> <span style="COLOR: teal">BeginEntityEventHandler</span>(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; OnBeginEntity</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;pb.BeginSheet -=</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">new</span> <span style="COLOR: teal">BeginSheetEventHandler</span>(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; OnBeginSheet</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: green">// Although an empty handler, responding to</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: green">// this events allows the subsequent ones</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: green">// to fire</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">void</span> OnAboutToBeginPublishing(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">object</span> sender,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">AboutToBeginPublishingEventArgs</span> e</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; ){}</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: green">// Check whether we're publishing to 2D or 3D</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">private</span> <span style="COLOR: blue">void</span> OnBeginSheet(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">object</span> sender,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">PublishSheetEventArgs</span> e</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; )</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;mbPublishingTo3D = e.PublishingTo3DDwf;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: green">// This event does the real work</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">private</span> <span style="COLOR: blue">void</span> OnBeginEntity(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">object</span> sender,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">PublishEntityEventArgs</span> e</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; )</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">try</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">Solid3d</span> solid = e.Entity <span style="COLOR: blue">as</span> <span style="COLOR: teal">Solid3d</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">if</span> (solid != <span style="COLOR: blue">null</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> (mbPublishingTo3D)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Adding 3D metadata is easy</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;e.Add3DDwfProperty(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;Material&quot;</span>,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;Name&quot;</span>,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; solid.Material</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;e.Add3DDwfProperty(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;Identity&quot;</span>,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;Handle&quot;</span>,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; solid.Handle.ToString()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// 2D is more complicated...</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">ObjectIdCollection</span> objIds =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">new</span> <span style="COLOR: teal">ObjectIdCollection</span>();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;System.<span style="COLOR: teal">Int32</span> nodeId =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; e.GetEntityNode(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; e.Entity.ObjectId,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; objIds</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (nodeId &lt;= 0)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Create node with next sequential number</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// and a unique name</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; nodeId =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; e.GetNextAvailableNode();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">string</span> strUnique =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; e.UniqueEntityId;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">string</span> nodeName =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: maroon">&quot;ASDK&quot;</span> + strUnique;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">DwfNode</span> node =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">new</span> <span style="COLOR: teal">DwfNode</span>(nodeId, nodeName);</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Create a string property in</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// the Material category</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">EPlotProperty</span> matprop =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">new</span> <span style="COLOR: teal">EPlotProperty</span>(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: maroon">&quot;Name&quot;</span>,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;solid.Material</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; matprop.Category = <span style="COLOR: maroon">&quot;Material&quot;</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; matprop.Type = <span style="COLOR: maroon">&quot;string&quot;</span>;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Create a string property in</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// the Identity category</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">EPlotProperty</span> handprop =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">new</span> <span style="COLOR: teal">EPlotProperty</span>(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: maroon">&quot;Handle&quot;</span>,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;solid.Handle.ToString()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; handprop.Category = <span style="COLOR: maroon">&quot;Identity&quot;</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; handprop.Type = <span style="COLOR: maroon">&quot;string&quot;</span>;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Create a property bag, containing</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// the properties and references</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">EPlotPropertyBag</span> bag =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">new</span> <span style="COLOR: teal">EPlotPropertyBag</span>();</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// This should set the ID of the property bag,</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// to be used in the property definition</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; bag.Id = nodeName;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Add it to the object/instance references</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; bag.References.Add(nodeName);</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// The properties themselves needs adding</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; bag.Properties.Add(matprop);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; bag.Properties.Add(handprop);</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Add properties and relationships to the DWF</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; e.AddPropertyBag(bag);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; e.AddNodeToMap(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; e.Entity.ObjectId,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; objIds,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; nodeId</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; e.AddPropertiesIds(bag, node);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">catch</span> (System.<span style="COLOR: teal">Exception</span> ex)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">Document</span> doc =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">Application</span>.DocumentManager.MdiActiveDocument;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">Editor</span> ed = doc.Editor;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.WriteMessage(<span style="COLOR: maroon">&quot;\nException: &quot;</span> + ex.Message);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">}</p></div>

<p>Once we build this into a managed assembly we can simply load it with NETLOAD – the module initialization takes care of adding the event handlers into the publishing system, so there’s no need to call a command.</p>

<p>Now let’s launch the PUBLISH command and create our DWF file.</p>

<p>AutoCAD’s PUBLISH command allows us to generate DWF files that contain both 2D and 3D data (this is not possible using PLOT – it has to be PUBLISH). All we need to do is set the Model sheet to be a “3D DWF”:</p>

<p><a onclick="window.open(this.href, '_blank', 'width=762,height=437,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/2007/10/25/publish_dwf_from_autocad.png"><img title="Publish_dwf_from_autocad" height="172" alt="Publish_dwf_from_autocad" src="/assets/publish_dwf_from_autocad.png" width="300" border="0" /></a> </p>

<p><em>Figure 7 – setting 3D content in AutoCAD’s PUBLISH dialog</em></p>

<p>Once we hit the “Publish” button and select our DWF file (solids.dwf, as that’s the name our embedded viewing sample will look for later).</p>

<p>We can now load the DWF file in Design Review and take a look at the 3D and 2D content.</p>

<p><a onclick="window.open(this.href, '_blank', 'width=800,height=607,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/2007/10/25/3d_dwf_content_with_metadata.png"><img title="3d_dwf_content_with_metadata" height="227" alt="3d_dwf_content_with_metadata" src="/assets/3d_dwf_content_with_metadata.png" width="300" border="0" /></a> </p>

<p><em>Figure 8 – 3D DWF content with metadata in Design Review</em></p>

<p>We can see that as we select a spherical object in the model, we can see the associated metadata in the “Properties” view on the left. The key thing for this demo is that we have identical “identity” metadata attached to the 2D content, so we can relate the two views.</p>

<p><a onclick="window.open(this.href, '_blank', 'width=800,height=607,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/2007/10/25/2d_dwf_content_with_metadata.png"><img title="2d_dwf_content_with_metadata" height="227" alt="2d_dwf_content_with_metadata" src="/assets/2d_dwf_content_with_metadata.png" width="300" border="0" /></a> </p>

<p><em>Figure 9 – 2D DWF content with metadata in Design Review</em></p>

<p>Sure enough, we see the same “material” and ‘identity” metadata associated with objects in both 2D and 3D content of the DWF.</p>

<p>Before we move on to using the DWF Toolkit to mine the content, let’s quickly Save As DWFx from Design Review 2008 and see the capabilities of the XPS Viewer.</p>

<p><a onclick="window.open(this.href, '_blank', 'width=800,height=607,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/2007/10/25/save_as_dwfx_from_design_review_200.png"><img title="Save_as_dwfx_from_design_review_200" height="227" alt="Save_as_dwfx_from_design_review_200" src="/assets/save_as_dwfx_from_design_review_200.png" width="300" border="0" /></a> </p>

<p><em>Figure 10 – Save As DWFx from Design Review 2008</em></p>

<p>The file created (solids.dwfx) is actually about the same size as the equivalent DWF file (solids.dwf), but that’s also because the 3D and metadata content is implemented in the same way (as XPS doesn’t currently support either).</p>

<p>When we load it into the XPS Viewer (by browsing to it from Internet Explorer, assuming you’ve installed the XPS Viewer from <a href="http://www.microsoft.com/whdc/xps/viewxps.mspx">http://www.microsoft.com/whdc/xps/viewxps.mspx</a> or are using Vista, which has it pre-installed), you see a basic interface for 2D content:</p>

<p><a onclick="window.open(this.href, '_blank', 'width=800,height=545,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/2007/10/25/xps_viewer.png"><img title="Xps_viewer" height="204" alt="Xps_viewer" src="/assets/xps_viewer.png" width="300" border="0" /></a> </p>

<p><em>Figure 11 – using the XPS Viewer to view our DWFx file</em></p>

<p>Note that our initial 3D sheet is replaced by one pointing us at the Design Review download location, as the XPS Viewer doesn’t currently support 3D content.</p>
