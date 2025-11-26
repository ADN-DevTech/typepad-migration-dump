---
layout: "post"
title: "How do I know I'm in paperspace for sure in ARX?"
date: "2012-12-28 18:57:18"
author: "Gopinath Taget"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Gopinath Taget"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/12/how-do-i-know-im-in-paperspace-for-sure-in-arx.html "
typepad_basename: "how-do-i-know-im-in-paperspace-for-sure-in-arx"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/gopinath-taget.html">Gopinath Taget</a></p>  <p>The following code snippet would do the job.</p>  <div style="font-family: ; background: white">   <p style="margin: 0px"><span style="line-height: 11pt"><font face="Consolas"><font style="font-size: 8pt" color="#000000">BOOL purePaperSpace () { </font></font></span></p>    <p style="margin: 0px"><font face="Consolas"><span style="line-height: 11pt"><font color="#000000"><font style="font-size: 8pt">&#160;</font></font></span><font style="font-size: 8pt"><span style="line-height: 11pt"><font color="#0000ff">struct</font></span></font><span style="line-height: 11pt"><font style="font-size: 8pt" color="#000000"> resbuf res ;</font></span></font></p>    <p style="margin: 0px"><font face="Consolas"><span style="line-height: 11pt"><font color="#000000"><font style="font-size: 8pt"> acedGetVar (L</font></font></span><font style="font-size: 8pt"><span style="line-height: 11pt"><font color="#a31515">&quot;tilemode&quot;</font></span></font><span style="line-height: 11pt"><font style="font-size: 8pt" color="#000000">, &amp;res) ;</font></span></font></p>    <p style="margin: 0px"><font face="Consolas"><span style="line-height: 11pt"><font color="#000000"><font style="font-size: 8pt">&#160;</font></font></span><font style="font-size: 8pt"><span style="line-height: 11pt"><font color="#0000ff">int</font></span></font><span style="line-height: 11pt"><font style="font-size: 8pt" color="#000000"> tilemode =res.resval.rint ;</font></span></font></p>    <p style="margin: 0px"><font face="Consolas"><span style="line-height: 11pt"><font color="#000000"><font style="font-size: 8pt"> acedGetVar (L</font></font></span><font style="font-size: 8pt"><span style="line-height: 11pt"><font color="#a31515">&quot;cvport&quot;</font></span></font><span style="line-height: 11pt"><font style="font-size: 8pt" color="#000000">, &amp;res) ;</font></span></font></p>    <p style="margin: 0px"><font face="Consolas"><span style="line-height: 11pt"><font color="#000000"><font style="font-size: 8pt">&#160;</font></font></span><font style="font-size: 8pt"><span style="line-height: 11pt"><font color="#0000ff">int</font></span></font><span style="line-height: 11pt"><font style="font-size: 8pt" color="#000000"> cvport =res.resval.rint ;</font></span></font></p>    <p style="margin: 0px"><font face="Consolas"><span style="line-height: 11pt"><font color="#000000"><font style="font-size: 8pt">&#160;</font></font></span><font style="font-size: 8pt"><span style="line-height: 11pt"><font color="#0000ff">return</font></span></font><span style="line-height: 11pt"><font style="font-size: 8pt" color="#000000"> (tilemode == 0 &amp;&amp; cvport == 1) ;</font></span></font></p>    <p style="margin: 0px"><span style="line-height: 11pt"><font face="Consolas"><font style="font-size: 8pt" color="#000000">}</font></font></span></p>    <p style="margin: 0px"><span style="line-height: 11pt"><font color="#000000" face="Consolas"></font></span></p>    <p style="margin: 0px"><span style="line-height: 11pt">&#160;</span></p> </div>
