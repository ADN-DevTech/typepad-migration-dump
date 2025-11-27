---
layout: "post"
title: "A mathematical F# application integrating with AutoCAD via .NET"
date: "2007-11-05 22:06:03"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "F#"
original_url: "https://www.keanw.com/2007/11/a-mathematical-.html "
typepad_basename: "a-mathematical-"
typepad_status: "Publish"
---

<p>For my follow-up F# post I wanted to find something that showed off more of the capabilities of the F# language, while remaining something worth integrating with AutoCAD. The good news (at least as far as I'm concerned :-) is that one of the samples that ships with F# turned out to be perfect for this.</p>

<p>The sample is known as &quot;<a href="http://cs.hubfs.net/forums/thread/95.aspx">The Famous DirectX Demo</a>&quot;, and is really, really cool. It uses F# to represent - extremely succinctly - a simulated, animated surface. And when I say succinct I mean it's tiny: with F# it's possible to represent complex mathematical formulae (this particular code creates a surface representing a function over X &amp; Y coordinates as well as being a function of time). This was perhaps the real &quot;aha&quot; moment for me: while it's possible to do this in C#, the code would be much larger... F# is very well suited to simulation and scientific computing.</p>

<p>That's not to say the F# code is easy - it takes some time to get your head around functional programming generally, and it can be very hard to pick up code written by someone else. But for certain applications the code created is often much smaller, and therefore easier to maintain (for knowledgeable coders), than the code written in a classic imperative language.</p>

<p>A quick note on the code I added. As I mentioned earlier, I do have some background in functional programming: back in my University days I worked on a project to model a full <a href="http://en.wikipedia.org/wiki/Motorola_6800">Motorola 6800 processor</a> in a <a href="http://en.wikipedia.org/wiki/Purely_functional">purely functional language</a> (which is tricky, as you don't get to store or modify state in your functions). We implemented it from the <a href="http://en.wikipedia.org/wiki/Logic_gates">logic gates</a> upward, even to the point of being able to code against it using <a href="http://en.wikipedia.org/wiki/Assembly_language">assembly language</a>. Ah, what fun we had. :-)</p>

<p>All this to say, that despite this background, it's all coming back <em>very slowly</em>. It seems I no longer have the academic's desire to code something as perfectly as possible: I'm now more moved by seeing something working and making the code as readable as possible (over the years I've learned the value in making code easy for other people to understand). So the code I've inserted doesn't always follow the most elegant approach from a functional programming perspective. But I'm pleased to note that I have been getting the odd flash of inspiration as to how certain code works in this sample. Hopefully I'll understand more as time goes on.</p>

<p>OK - while I'm not going to include the full code directly in this post (you can, of course, <a href="http://through-the-interface.typepad.com/through_the_interface/files/surface-acad-dx.zip">download the project</a>, which includes a compiled assembly that you should just be able to NETLOAD, assuming you have the right version of DirectX installed), here are a few notes that are worth pointing out.</p>

<p>Firstly, in F# it's possible to use the #I directive to specify an &quot;include&quot; folder and #r to specify an assembly reference:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">#I <span style="COLOR: maroon">@&quot;C:\Program Files\Autodesk\AutoCAD 2008&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">#r <span style="COLOR: maroon">&quot;acdbmgd.dll&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">#r <span style="COLOR: maroon">&quot;acmgd.dll&quot;</span></p></div>

<p>Secondly, while F# infers types automatically, there are times you need to perform the equivalent of a cast, allowing you to access properties and call methods of that particular class. This code specifies a runtime type-check and cast to the BlockTable type for bt:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">let</span> bt = (tr.GetObject(db.BlockTableId,OpenMode.ForRead) :?&gt; BlockTable);</p></div>

<p>The project implements two commands, &quot;wow&quot; and &quot;wow2&quot; (I was that impressed). &quot;wow&quot; implements the full version of the code, with a few balls drawn on the surface, moving around over time. &quot;wow2&quot; uses a UserControl including a simplified version of the code, which is a little quicker to execute.</p>

<p>Using either command you can orbit the view using the left mouse button and zoom using the right. The code I added is driven by the middle mouse button: once you click the mouse wheel (if you have one - you'll have to modify the code if you don't) the code takes a snapshot of the surface being displayed in the separate dialog using DirectX, and creates 3D faces inside the modelspace of the current AutoCAD drawing.</p>

<p>Here's a view of the &quot;wow&quot; command. It turns out that each time you run it, more balls get added to the surface. But I'm calling that a feature, for now. :-)</p>

<p><a onclick="window.open(this.href, '_blank', 'width=800,height=643,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/2007/11/05/wow_command.png"><img title="Wow_command" height="241" alt="Wow_command" src="/assets/wow_command.png" width="300" border="0" /></a> </p>

<p>And here's &quot;wow2&quot;, which is somewhat simpler:</p>

<p><a onclick="window.open(this.href, '_blank', 'width=800,height=644,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/2007/11/05/wow2_command.png"><img title="Wow2_command" height="241" alt="Wow2_command" src="/assets/wow2_command.png" width="300" border="0" /></a> </p>

<p>Finally, once we use the middle mouse button (and it sometimes need a little drag for the handler to kick in), we get the snapshot of the surface created inside AutoCAD - shown here with the &quot;realistic&quot; visual style:</p>

<p><a onclick="window.open(this.href, '_blank', 'width=800,height=644,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/2007/11/05/our_autocad_surface.png"><img title="Our_autocad_surface" height="241" alt="Our_autocad_surface" src="/assets/our_autocad_surface.png" width="300" border="0" /></a> </p>

<p>I hope this helps demonstrate some of the strong potential of F# for certain types of application. One of the things I love about working with .NET is the flexibility you have to choose the right language for the job in hand. F# definitely brings interesting new capabilities to the .NET language family, and I'm very much looking forward to working with it some more.</p>
