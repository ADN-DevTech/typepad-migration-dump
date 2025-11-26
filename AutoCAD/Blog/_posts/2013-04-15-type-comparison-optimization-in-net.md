---
layout: "post"
title: "Type comparison optimization in .NET"
date: "2013-04-15 10:45:20"
author: "Augusto Goncalves"
categories:
  - ".NET"
  - "Augusto Goncalves"
original_url: "https://adndevblog.typepad.com/autocad/2013/04/type-comparison-optimization-in-net.html "
typepad_basename: "type-comparison-optimization-in-net"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/augusto-goncalves.html">Augusto Goncalves</a></p>  <p>Although this is not AutoCAD or Autodesk related, I always see developers doing .NET code that can be optimized. Sure in some cases this is not a concern, or the processing time that will be saved doesn’t worth the trouble. But doing some small good practices can save you big at the end of your project.</p>  <p>So this post is quite simple and about type comparison. The idea came from this interesting post: <a href="http://blogs.msdn.com/b/vancem/archive/2006/10/01/779503.aspx">Drilling into .NET Runtime microbenchmarks: 'typeof' optimizations</a>. </p>  <p>Basically it says that the following type comparison is the faster:</p>  <div style="font-family: ; background: white">   <p style="margin: 0px"><font face="Courier New"><span><font color="#2b91af"><font style="font-size: 8pt">Entity</font></font></span><font style="font-size: 8pt"><font color="#000000"> ent = </font></font><span><font style="font-size: 8pt" color="#008000">// initialize here</font></span></font></p>    <p style="margin: 0px"><font face="Courier New"><span><font color="#0000ff"><font style="font-size: 8pt">if</font></font></span><font style="font-size: 8pt"><font color="#000000"> (ent.GetType() == </font><span><font color="#0000ff">typeof</font></span><font color="#000000">(</font><span><font color="#2b91af">Line</font></span><font color="#000000">))</font></font></font></p>    <p style="margin: 0px"><font face="Courier New"><font style="font-size: 8pt" color="#000000">{</font></font></p>    <p style="margin: 0px"><font face="Courier New"><font style="font-size: 8pt" color="#000000">}</font></font></p> </div>  <p><strong>[Update] </strong><strong><u>Comments</u></strong>:</p>  <p>As well pointed by the comments (see below), the variations of object type comparison shown here (and the <a href="http://blogs.msdn.com/b/vancem/archive/2006/10/01/779503.aspx">linked post</a>) will produce different results. So you need make sure which is one is the correct for each scenario. Once that is decided, I would suggest review the performance trade off of your choice at the <a href="http://blogs.msdn.com/b/vancem/archive/2006/10/01/779503.aspx">linked post</a>. </p>
