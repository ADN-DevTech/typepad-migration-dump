---
layout: "post"
title: "Advanced Visual Studio debugging: automatic expansion of watched variables"
date: "2006-07-24 17:08:46"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "Debugging"
  - "ObjectARX"
  - "Visual Studio"
original_url: "https://www.keanw.com/2006/07/advanced_visual.html "
typepad_basename: "advanced_visual"
typepad_status: "Publish"
---

<p>I've been using Visual C++ (and afterwards Visual Studio) since it was 16-bit, back in version 1.52. OK, maybe that's not so long ago, relatively (11 short years), but the point is that in spite of having followed the Visual Studio technology over this period, I've so far been completely unaware of the autoexp.dat file.</p>

<p>This feature of the Visual Studio was brought to my attention by Ahsan Ali, a programmer in the Inventor Engineering team who was based over in Bangalore at the same time I was (we had both previously worked in the US - he had come across from Tualatin while I had moved there from San Rafael). During a recent technology discussion, Ahsan shared some information with our Bangalore-based team on some advanced debugging techniques, and I thought it would be a great topic for this blog.</p>

<p>The autoexp.dat file's default location (for Visual Studio 2005) is:</p><blockquote dir="ltr"><p><em>C:\Program Files\Microsoft Visual Studio 8\Common7\Packages\Debugger\autoexp.dat</em></p></blockquote><p>It's basically a text file that informs the Visual Studio IDE which information is especially relevant in a particular class, so that this information can be displayed automatically (and in the appropriate format) in the watch window, without you having to expand the various datatypes. The clicks you use to get down to data all add up, especially when dealing with a particularly repetitive debugging task, so this feature can really be helpful. MSDN refers to it <a href="http://msdn2.microsoft.com/en-us/library/zf0e8s14.aspx">here</a>.</p>

<p>The technique seems to be specific for native (presumably C++?) code, but the above link refers to techniques to implement this type of functionality for VB.NET and C#.</p>

<p>So let's look at an example of the problem - as it relates to ObjectARX - and how this technique helps. Here's the default view of two very common ObjectARX datatypes, AcGePoint3d and AcGeMatrix3d:</p>

<p><a href="http://through-the-interface.typepad.com/photos/uncategorized/watch_window_1.png"><img class="image-full" title="Watch_window_1" alt="Watch_window_1" src="/assets/watch_window_1.png" border="0" /></a> </p>

<p>The default view of the pickpnt variable (an AcGePoint3d) is OK, but all those zeroes do make your eyes hurt. The data displayed for xform (an AcGeMatrix3D) tells you nothing whatsoever of any use - it's the address of the 2D array holding the matrix contents. Here's what you get when you expand these two variables:</p>

<p><a href="http://through-the-interface.typepad.com/photos/uncategorized/watch_window_1b_1.png"><img class="image-full" title="Watch_window_1b_1" alt="Watch_window_1b_1" src="/assets/watch_window_1b_1.png" border="0" /></a> </p>

<p>You need 5 clicks just to determine that the xform variable is actually the unit matrix, which is way too much.</p>

<p>So what can we do? The autoexp.dat rules format is very simple and is documented in the header of the file itself. I won't reproduce the whole description of the format here, aside from this brief statement:</p><blockquote dir="ltr"><p><em>An AutoExpand rule is a line with the name of a type, an equals sign, and text with replaceable parts in angle brackets. The part in angle brackets names a member of the type and an&nbsp; optional Watch format specifier.</em></p></blockquote><p>Thereafter follow some specifics regarding the syntax - for an alternative source of information regarding this, you might also try <a href="http://www.codeguru.com/Cpp/V-S/debug/article.php/c1281/">this CodeGuru article</a>. I'd also recommend looking at the various types listed in the file itself - as usual real-life examples often paint better pictures than abstract descriptions.</p>

<p>So, let's look at what we can do for AcGePoint3d and AcGeMatrix3d. Here are the two entries I added to the autoexp.dat file:</p>

<p><strong>AcGePoint3d =x=&lt;x,g&gt; y=&lt;y,g&gt; z=&lt;z,g&gt;</strong></p>

<p><strong>AcGeMatrix3d =&lt;entry[0][0],g&gt; &lt;entry[0][1],g&gt; &lt;entry[0][2],g&gt; &lt;entry[0][3],g&gt;,&lt;entry[1][0],g&gt; &lt;entry[1][1],g&gt; &lt;entry[1][2],g&gt; &lt;entry[1][3],g&gt;,&lt;entry[2][0],g&gt; &lt;entry[2][1],g&gt; &lt;entry[2][2],g&gt; &lt;entry[2][3],g&gt;,&lt;entry[3][0],g&gt; &lt;entry[3][1],g&gt; &lt;entry[3][2],g&gt; &lt;entry[3][3],g&gt;</strong></p>

<p>The first is fairly simple: it simply tells Visual Studio to display each of the X, Y and Z values of the point, but using the &quot;g&quot; type specifier. Without going into details, this means that they are floating-point values that should be abbreviated to significant digits only. I chose to leave in the labels, x, y &amp; z, to improve readability.</p>

<p>The second is clearly longer - once again it uses the &quot;g&quot; type specifier for each of the 16 entries in the 4x4 matrix. Given the volume of information in the matrix class, I decided to leave out the labels, simply listing the contents with each row separated each by a comma.</p>

<p>Here are the results:</p>

<p><a href="http://through-the-interface.typepad.com/photos/uncategorized/watch_window_2a.png"><img class="image-full" title="Watch_window_2a" alt="Watch_window_2a" src="/assets/watch_window_2a.png" border="0" /></a> </p>

<p>When we have something more meaningful contained in the variables, the descriptions will get longer, of course:</p>

<p><a href="http://through-the-interface.typepad.com/photos/uncategorized/watch_window_2b.png"><img class="image-full" title="Watch_window_2b" alt="Watch_window_2b" src="/assets/watch_window_2b.png" border="0" /></a> </p>

<p><a href="http://through-the-interface.typepad.com/photos/uncategorized/watch_window_2c.png"><img class="image-full" title="Watch_window_2c" alt="Watch_window_2c" src="/assets/watch_window_2c.png" border="0" /></a> </p>

<p>A few tips about the entries in autoexp.dat:</p>

<ul><li>Don't just add them to the end of the file: they should be part of the [AutoExpand] section, not the [Visualizer] section. I placed mine at line 147.</li>

<li>It seems that the string itself is limited to 256 characters to the right of the first equals sign (in this day and age - can you imagine?). I was lucky - the AcGeMatrix3d rule eventually came to exactly 256 characters (which is how I found out about the limitation - I had to squeeze out a few redundant spaces for it to work).</li>

<li>I haven't been able to get multiple lines to display - which would be especially useful for the matrix class, of course. If someone can work it out or find the information on the internet, please post a comment!</li>

<li>It would also be great to do conditional display of data, especially for union types such as the good old resbuf. While this is not supported directly, you can develop an AddIn to make it work, apparently. See <a href="http://www.flipcode.com/cgi-bin/fcarticles.cgi?show=63905">this article</a> for more details.</li></ul>
