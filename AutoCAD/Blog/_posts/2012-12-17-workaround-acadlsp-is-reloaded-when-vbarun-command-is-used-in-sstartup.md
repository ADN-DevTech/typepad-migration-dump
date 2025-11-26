---
layout: "post"
title: "Workaround: Acad.lsp is reloaded when -VBARUN command is used in S::STARTUP"
date: "2012-12-17 18:10:49"
author: "Gopinath Taget"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "ActiveX"
  - "AutoCAD"
  - "Gopinath Taget"
  - "LISP"
original_url: "https://adndevblog.typepad.com/autocad/2012/12/workaround-acadlsp-is-reloaded-when-vbarun-command-is-used-in-sstartup.html "
typepad_basename: "workaround-acadlsp-is-reloaded-when-vbarun-command-is-used-in-sstartup"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/gopinath-taget.html">Gopinath Taget</a></p>  <p>You might observe that Acad.lsp is reloaded when -VBARUN command is used in the S::STARTUP function to load a drawing. Is this a known behavior and is there a way to stop it from reloading?&#160; This happens even when ACADLSPASDOC variable is 0. </p>  <p>The reason that Acad.lsp is reloaded, with a drawing opened by a procedure in&#160; Acad.dvb (called with -VBARUN in S:STARTUP), is that AutoCAD has not finished initializing at the point in the S::STARTUP function where the command -VBARUN is called.&#160; </p>  <p>As a workaround, the VBASTMT command allows you to call VBA functions with arguments, from either the command line or a LISP expression.&#160; In this case, we can use (vla-sendcommand) in the (S::STARTUP) function, to call the VBA &quot;RunMacro&quot; method.&#160; This approach will not cause a reload of Acad.lsp</p>  <pre>(defun-q mystartup ( )<br />&#160;&#160; (vl-load-com) ;load ActiveX objects<br />&#160;&#160; ;;replace this line: (command &quot;.-vbarun&quot; &quot;MyModule.MySub&quot;) <br />&#160;&#160; ;;with the following:&#160; <br />&#160;&#160; (arxload &quot;acadvba.arx&quot;) ;ensure Acad.dvb is loaded<br />&#160;&#160; (vla-sendcommand <br />&#160;&#160;&#160;&#160;&#160; (vla-get-activedocument (vlax-get-acad-object))<br />&#160;&#160;&#160;&#160;&#160; &quot;vbastmt\n\ThisDrawing.Application.RunMacro \&quot;MyModule.MySub\&quot;\n&quot;<br />&#160;&#160; )<br />)
<p>(setq s::startup (append s::startup mystartup))<br /></p></pre>
