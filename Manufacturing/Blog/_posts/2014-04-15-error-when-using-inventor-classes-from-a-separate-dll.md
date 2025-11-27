---
layout: "post"
title: "Error when using Inventor classes from a separate dll"
date: "2014-04-15 02:22:07"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/04/error-when-using-inventor-classes-from-a-separate-dll.html "
typepad_basename: "error-when-using-inventor-classes-from-a-separate-dll"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>When you have a standalone application you may want to place some of the functionality in a separate dll.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fcefc7d9970b-pi" style="display: inline;"><img alt="EmbeddedType" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a3fcefc7d9970b image-full img-responsive" src="/assets/image_b8269a.jpg" title="EmbeddedType" /></a></p>
<p>In this case if you create classes which are derived somehow from an Inventor class and try to use those from your main application then you&#39;ll get an error:</p>
<pre>&quot;Error	1	Type &#39;Classes_Inventor.Collection_Axes&#39; from assembly
&#39;c:\Users\Administrator\Documents\Visual Studio 2010\Projects\
InventorTestConsole\Classes_Inventor\bin\Debug\Classes_Inventor.dll&#39; 
cannot be used across assembly boundaries because a type in its 
inheritance hierarchy has a generic type parameter that is an 
<strong>embedded interop type</strong>.	
c:\users\administrator\documents\visual studio 2010\
Projects\InventorTestConsole\InventorTestConsole\Program.cs	
12	29	InventorTestConsole&quot;</pre>
<p>The highlighted words provide a clue about the solution, which is the same as shown in this article:<br /><a href="http://modthemachine.typepad.com/my_weblog/2012/07/set-embed-interop-types-to-false-to-avoid-problems-with-events.html" target="_self" title="">http://modthemachine.typepad.com/my_weblog/2012/07/set-embed-interop-types-to-false-to-avoid-problems-with-events.html</a></p>
<p>Once we changed <strong>Embed Interop Types</strong> property of the reference to Inventor in the <strong>dll</strong> to <strong>False</strong>, the whole project compiled fine.</p>
<p>&#0160;</p>
