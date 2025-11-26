---
layout: "post"
title: "Using (vlax-ldata-test data)"
date: "2012-10-25 22:09:46"
author: "Balaji"
categories:
  - "ActiveX"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "LISP"
original_url: "https://adndevblog.typepad.com/autocad/2012/10/using-vlax-ldata-test-data.html "
typepad_basename: "using-vlax-ldata-test-data"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<div><strong>Issue</strong></div>
<p>How to use the (vlax-ldata-test data) Visual LISP function? The documentation refers to a "session boundary", what does it mean?</p>
<!--stopindex-->
<div><a name="section2"> </a><!--startindex-->
<div><strong>Solution</strong></div>
<p>The following sample shows how to use the session boundary, which means you can store and retrieve any kind of LISP data into and out of a dictionary or an object. It does not mean that it is possible to transfer among different AutoCAD instances using<br />vlax-ldata-xxxxx family functions.</p>
<p>In the following code, foo is a function and it is expressed as a list in LISP, so 'foo as the parameter will return True, otherwise, foo will return false. For fnm, a file descriptor that can be used by other I/O functions, it will change every time you open the same file and you can use it in a certain context. If you store it somewhere and want to use it in a different running context, it will fail. That is a clear image of session boundary.</p>
</div>
<p></p>
<p><span style="line-height: 120%; font-family: 'courier new', courier; font-size: 8pt;"><span style="color:#ff0000">(</span><span style="color:#0000ff">vl-load-com</span><span style="color:#ff0000">)</span><br />
<br />
<span style="color:#ff0000">(</span><span style="color:#0000ff">setq</span>&nbsp;lin&nbsp;<span style="color:#ff0000">(</span>entmake&nbsp;'<span style="color:#ff0000">(</span><span style="color:#ff0000">(</span><span style="color:#008000">0</span>&nbsp;.&nbsp;<span style="color:#ff00ff">"LINE"</span><span style="color:#ff0000">)</span>&nbsp;<span style="color:#ff0000">(</span><span style="color:#008000">10</span>&nbsp;<span style="color:#008000">1</span>&nbsp;<span style="color:#008000">1</span>&nbsp;<span style="color:#008000">1</span><span style="color:#ff0000">)</span>&nbsp;<span style="color:#ff0000">(</span><span style="color:#008000">11</span>&nbsp;<span style="color:#008000">20</span>&nbsp;<span style="color:#008000">20</span>&nbsp;<span style="color:#008000">20</span><span style="color:#ff0000">)</span><span style="color:#ff0000">)</span><span style="color:#ff0000">)</span><span style="color:#ff0000">)</span><br />
<span style="color:#ff0000">(</span><span style="color:#0000ff">setq</span>&nbsp;fnm&nbsp;<span style="color:#ff0000">(</span>open&nbsp;<span style="color:#ff00ff">"mmm.out"</span>&nbsp;<span style="color:#ff00ff">"W"</span><span style="color:#ff0000">)</span><span style="color:#ff0000">)</span><br />
<br />
<span style="color:#ff0000">(</span><span style="color:#0000ff">defun</span>&nbsp;foo&nbsp;<span style="color:#ff0000">(</span><span style="color:#ff0000">)</span>&nbsp;<span style="color:#ff0000">(</span>list&nbsp;<span style="color:#008000">1</span>&nbsp;<span style="color:#008000">2</span>&nbsp;<span style="color:#008000">3</span><span style="color:#ff0000">)</span><span style="color:#ff0000">)</span><br />
<br />
<span style="background-color:#e6e6e6; color:#800080">;<span style="background-color:#e6e6e6; color:#800080">;&nbsp;True&nbsp;<span style="color:#ff0000">(</span>T<span style="color:#ff0000">)</span>&nbsp;for:<br /></span></span>
<span style="color:#ff0000">(</span><span style="color:#0000ff">vlax-ldata-test</span>&nbsp;<span style="color:#008000">1</span><span style="color:#ff0000">)</span><br />
<span style="color:#ff0000">(</span><span style="color:#0000ff">vlax-ldata-test</span>&nbsp;'foo<span style="color:#ff0000">)</span><br />
<span style="color:#ff0000">(</span><span style="color:#0000ff">vlax-ldata-test</span>&nbsp;<span style="color:#ff00ff">"A"</span><span style="color:#ff0000">)</span><br />
<span style="color:#ff0000">(</span><span style="color:#0000ff">vlax-ldata-test</span>&nbsp;lin<span style="color:#ff0000">)</span><br />
<span style="color:#ff0000">(</span><span style="color:#0000ff">vlax-ldata-test</span>&nbsp;<span style="color:#ff0000">(</span>list&nbsp;<span style="color:#008000">1</span>&nbsp;<span style="color:#ff00ff">"a"</span>&nbsp;'foo&nbsp;lin<span style="color:#ff0000">)</span><span style="color:#ff0000">)</span><br />
<br />
<span style="background-color:#e6e6e6; color:#800080">;<span style="background-color:#e6e6e6; color:#800080">;&nbsp;False&nbsp;<span style="color:#ff0000">(</span>NIL<span style="color:#ff0000">)</span>&nbsp;for:<br /></span></span>
<span style="color:#ff0000">(</span><span style="color:#0000ff">vlax-ldata-test</span>&nbsp;foo<span style="color:#ff0000">)</span><br />
<span style="color:#ff0000">(</span><span style="color:#0000ff">vlax-ldata-test</span>&nbsp;fnm<span style="color:#ff0000">)</span><br />
<span style="color:#ff0000">(</span><span style="color:#0000ff">vlax-ldata-test</span>&nbsp;<span style="color:#ff0000">(</span>list&nbsp;<span style="color:#008000">1</span>&nbsp;<span style="color:#ff00ff">"a"</span>&nbsp;foo&nbsp;lin<span style="color:#ff0000">)</span><span style="color:#ff0000">)</span><br />
<span style="color:#ff0000">(</span><span style="color:#0000ff">vlax-ldata-test</span>&nbsp;<span style="color:#ff0000">(</span>cons&nbsp;<span style="color:#008000">1</span>&nbsp;fnm<span style="color:#ff0000">)</span><span style="color:#ff0000">)</span></span></p>
