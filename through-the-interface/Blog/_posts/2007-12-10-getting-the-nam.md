---
layout: "post"
title: "Getting the names of the colors in an AutoCAD color-book using .NET"
date: "2007-12-10 18:32:17"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
original_url: "https://www.keanw.com/2007/12/getting-the-nam.html "
typepad_basename: "getting-the-nam"
typepad_status: "Publish"
---

<p>The <a href="http://through-the-interface.typepad.com/through_the_interface/2007/12/using-a-color-b.html">last post</a> got me thinking about how to get the names of all the colours that are contained in a particular color-book inside AutoCAD (the last post also contains the explanation for my using both &quot;color&quot; and &quot;colour&quot; in the same sentence, in case that bothers anyone :-).</p>

<p>Color-books are stored in .acb files: these files are essentially XML files with the RGB values encoded to prevent people from editing them and polluting the colour definitions on a particular system. So while the RGB information is not directly useful, it is very possible to iterate through these files and extract the names of the colours contained in a particular color-book.</p>

<p>This was also an excuse for me to look at the XmlReader class, to see how I might use that. It turns out that you can't instanciate an XmlReader directly - it's an abstract class. You need to use one of its concrete children, such as XmlTextReader or XmlNodeReader. XmlNodeReader gives you tree-like access to the XML content (such as you get when using the <a href="http://en.wikipedia.org/wiki/Document_Object_Model">DOM</a> to read an XML file), while the XmlTextReader is a forward-only reader which allows you to (for all intents and purposes) stream in XML content such as you might when using <a href="http://en.wikipedia.org/wiki/Simple_API_for_XML">SAX</a>. To understand how XmlTextReader differs from SAX, see <a href="http://msdn2.microsoft.com/en-us/library/sbw89de7(VS.80).aspx">this brief comparison</a>.</p>

<p>I have historically used DOM to read XML files that need to maintained in their entirety (as they have internal references, etc.) and SAX when this was less of a concern (such as when iterating through to harvest certain data). SAX was always quicker, but you had to maintain more state information if the content was inter-related.</p>

<p>In this case I would therefore have chosen SAX over DOM, so in the .NET world this means XmlTextReader is the class to use.</p>

<p>One other quick comment on the implementation: the code uses HostApplicationServices.FindFile() to find the location of the particular .acb file we’re interested in (typically inside “Support/Color” beneath the AutoCAD install folder).</p>

<p>Here's the C# code:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.Runtime;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.ApplicationServices;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.DatabaseServices;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.EditorInput;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> System;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> System.Xml;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">namespace</span> ColorBookApplication</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">class</span> <span style="COLOR: teal">Commands</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; [<span style="COLOR: teal">CommandMethod</span>(<span style="COLOR: maroon">&quot;LC&quot;</span>)]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">static</span> <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> ListRalColors()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Document</span> doc =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">Application</span>.DocumentManager.MdiActiveDocument;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Database</span> db = doc.Database;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Editor</span> ed = doc.Editor;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">string</span> loc =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">HostApplicationServices</span>.Current.FindFile(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: maroon">&quot;ral classic.acb&quot;</span>,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; db,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">FindFileHint</span>.Default</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">XmlTextReader</span> xr =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">new</span> <span style="COLOR: teal">XmlTextReader</span>(loc);</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">while</span> (xr.Read())</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; xr.MoveToContent();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">if</span> (xr.NodeType == <span style="COLOR: teal">XmlNodeType</span>.Element &amp;&amp;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;xr.LocalName == <span style="COLOR: maroon">&quot;colorName&quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> (xr.Read())</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (xr.HasValue)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.WriteMessage(<span style="COLOR: maroon">&quot;\n&quot;</span> + xr.Value);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;xr.Close();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">}</p></div>

<p>And here's what happens when we run the LC command:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">Command: <span style="COLOR: red">lc</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">RAL 1000</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">RAL 1001</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">RAL 1002</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">... entries deleted...</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">RAL 9016</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">RAL 9017</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">RAL 9018</p></div>
