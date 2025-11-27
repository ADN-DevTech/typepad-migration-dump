---
layout: "post"
title: "A quick Visual Studio tip - automatically implement interfaces in C#"
date: "2006-10-24 23:05:36"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Visual Studio"
original_url: "https://www.keanw.com/2006/10/a_quick_visual_.html "
typepad_basename: "a_quick_visual_"
typepad_status: "Publish"
---

<p>A colleague of mine in one of our Engineering teams just shared this tip that I'm in turn sharing with you...</p><blockquote dir="ltr"><p>I was implementing an override of the abstract class Autodesk.AutoCAD.DatabaseServices.DwgFiler, which has about 40 abstract functions that have to be overridden.&nbsp; Since there are no header files in C#, I couldn’t just cut and paste all the signatures as a template, like I would in C++.&nbsp; As I was manually typing in each signature, I noticed that Intellisense was keeping track of which signatures I had already accounted for.&nbsp; I thought… if it can do that, why can’t it just fill them all in for me?</p>

<p>Took some poking around, but you can right-click on the parent class name in your class declaration and use the option “Implement Abstract Class”.&nbsp; It fills in all the signatures automatically complete with “not implemented yet” stubs.&nbsp; It even knew to skip the ones I had already done manually. </p>

<p>Brilliant!</p></blockquote><p>I tried it out with our old friend the IExtensionApplication interface, and it worked a charm (jn my case I hovered over and left-clicked the little glyph that appeared by the classname - although I forget what those things are called...): </p>

<p><a onclick="window.open(this.href, '_blank', 'width=614,height=146,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/implementing_interfaces.png"><img title="Implementing_interfaces" height="71" alt="Implementing_interfaces" src="/assets/implementing_interfaces.png" width="299" border="0" /></a></p>

<p>It generates the code at the end of your class, complete with function stubs:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; #region</span> IExtensionApplication Members</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> Initialize()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">throw</span> <span style="COLOR: blue">new</span> System.<span style="COLOR: teal">Exception</span>(<span style="COLOR: maroon">&quot;The method or operation is not implemented.&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> Terminate()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">throw</span> <span style="COLOR: blue">new</span> System.<span style="COLOR: teal">Exception</span>(<span style="COLOR: maroon">&quot;The method or operation is not implemented.&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; #endregion</span></p></div><p>Anyway - this is probably old news to many of you (which is why I chose not to reveal the identity of my friend - I'd rather not expose him to ridicule if this ends up being something everyone takes for granted when working with C#).</p>

<p>If you have your own tip to share with the readership of this blog, please post a comment!</p>
