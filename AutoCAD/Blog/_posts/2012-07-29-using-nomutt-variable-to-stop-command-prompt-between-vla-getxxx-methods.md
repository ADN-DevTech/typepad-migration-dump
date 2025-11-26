---
layout: "post"
title: "Using NOMUTT variable to stop \"Command: \" Prompt between vla-getXXX methods"
date: "2012-07-29 08:10:06"
author: "Balaji"
categories:
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "LISP"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/using-nomutt-variable-to-stop-command-prompt-between-vla-getxxx-methods.html "
typepad_basename: "using-nomutt-variable-to-stop-command-prompt-between-vla-getxxx-methods"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>You may want to suppress the echo of the "Command: " prompt after subsequent calls to any of the 'Getxxxxxx' methods using ActiveX. For this the NOMUTT system variable along with extra Prompts as shown in the following sample lisp code can be used to stop the Command prompt from appearing after each vla-getXXX method.</p>
<p></p>
<p><span style="line-height: 120%; font-family: 'courier new', courier; font-size: 8pt;"><span style="color:#ff0000">(</span><span style="color:#0000ff">defun</span>&nbsp;c:test&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">/</span>&nbsp;doc&nbsp;p1&nbsp;p2&nbsp;uo<span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vl-load-com</span><span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">setq</span>&nbsp;doc&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vla-get-ActiveDocument</span>&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vlax-get-acad-object</span><span style="color:#ff0000">)</span><span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;msp&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vla-get-modelspace</span>&nbsp;doc<span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;uo&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vla-get-utility</span>&nbsp;doc<span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;<span style="color:#ff0000">)</span><br />
<br />
&nbsp;&nbsp;<span style="color:#ff0000">(</span>prompt&nbsp;<span style="color:#ff00ff">"\nStart&nbsp;point:"</span><span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">setvar</span>&nbsp;<span style="color:#ff00ff">"nomutt"</span>&nbsp;<span style="color:#008000">1</span><span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vla-InitializeUserInput</span>&nbsp;uo&nbsp;<span style="color:#008000">9</span><span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">setq</span>&nbsp;p1&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vla-getPoint</span>&nbsp;uo&nbsp;nil<span style="color:#ff0000">)</span><span style="color:#ff0000">)</span><br />
<br />
&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">setvar</span>&nbsp;<span style="color:#ff00ff">"nomutt"</span>&nbsp;<span style="color:#008000">0</span><span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;<span style="color:#ff0000">(</span>prompt&nbsp;<span style="color:#ff00ff">"\nEnd&nbsp;point:"</span><span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">setvar</span>&nbsp;<span style="color:#ff00ff">"nomutt"</span>&nbsp;<span style="color:#008000">1</span><span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vla-InitializeUserInput</span>&nbsp;uo&nbsp;<span style="color:#008000">9</span><span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">setq</span>&nbsp;p2&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vla-getPoint</span>&nbsp;uo&nbsp;p1<span style="color:#ff0000">)</span><span style="color:#ff0000">)</span><br />
<br />
&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vla-addLine</span>&nbsp;msp&nbsp;p1&nbsp;p2<span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">setvar</span>&nbsp;<span style="color:#ff00ff">"nomutt"</span>&nbsp;<span style="color:#008000">0</span><span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">princ</span><span style="color:#ff0000">)</span><br />
<span style="color:#ff0000">)</span><br />
</span></p>
