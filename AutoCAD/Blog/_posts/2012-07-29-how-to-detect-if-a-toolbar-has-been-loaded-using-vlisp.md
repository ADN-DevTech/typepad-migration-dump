---
layout: "post"
title: "How to detect if a toolbar has been loaded using VLISP?"
date: "2012-07-29 07:58:05"
author: "Balaji"
categories:
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "LISP"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/how-to-detect-if-a-toolbar-has-been-loaded-using-vlisp.html "
typepad_basename: "how-to-detect-if-a-toolbar-has-been-loaded-using-vlisp"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>Using VLisp, to detect if a toolbar has been loaded, we can use ActiveX methods to do the work, just like VBA does. The following code can achieve the task:</p>
<p></p>
<p><span style="line-height: 120%; font-family: 'courier new', courier; font-size: 8pt;"><span style="background-color:#e6e6e6; color:#800080">;<span style="background-color:#e6e6e6; color:#800080">;;&nbsp;Detect&nbsp;if&nbsp;the&nbsp;<span style="color:#ff00ff">"Web"</span>&nbsp;toolbar&nbsp;is&nbsp;loaded<br /></span></span>
<span style="color:#ff0000">(</span><span style="color:#0000ff">defun</span>&nbsp;c:detectToolbar&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">/</span>&nbsp;acadapp&nbsp;menuGroups&nbsp;menuGroup0&nbsp;toolbars<span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;&nbsp;<span style="background-color:#e6e6e6; color:#800080">;;&nbsp;Load&nbsp;COM&nbsp;support<br /></span>
&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">if</span>&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">car</span>&nbsp;<span style="color:#ff0000">(</span>atoms-family&nbsp;<span style="color:#008000">1</span>&nbsp;'<span style="color:#ff0000">(</span><span style="color:#ff00ff">"vl-load-com"</span><span style="color:#ff0000">)</span><span style="color:#ff0000">)</span><span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vl-load-com</span><span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">)</span><br />
<br />
&nbsp;&nbsp;&nbsp;<span style="background-color:#e6e6e6; color:#800080">;;&nbsp;Get&nbsp;AutoCAD&nbsp;application,&nbsp;the&nbsp;ACAD&nbsp;menuGroup&nbsp;and&nbsp;toolbars<br /></span>
&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">setq</span>&nbsp;acadapp&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vlax-get-acad-object</span><span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;menuGroups&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vla-get-menugroups</span>&nbsp;acadapp<span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;menuGroup0&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vla-item</span>&nbsp;menuGroups&nbsp;<span style="color:#ff00ff">"ACAD"</span><span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;toolbars&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vla-get-toolbars</span>&nbsp;menuGroup0<span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">)</span><br />
<br />
&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vlax-for</span>&nbsp;eachTB&nbsp;toolbars<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">setq</span>&nbsp;nameTB&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vla-get-name</span>&nbsp;eachTB<span style="color:#ff0000">)</span><span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">if</span>&nbsp;<span style="color:#ff0000">(</span>=&nbsp;nameTB&nbsp;<span style="color:#ff00ff">"Web"</span><span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">princ</span>&nbsp;<span style="color:#ff00ff">"\n&nbsp;Web&nbsp;toolbar&nbsp;has&nbsp;been&nbsp;loaded."</span><span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">)</span><br />
<br />
&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">princ</span><span style="color:#ff0000">)</span><br />
<span style="color:#ff0000">)</span></span></p>
<p></p>
<p>This example determines if a menugroup named&nbsp;"BatchPublish" has been loaded:</p>
<p><span style="line-height: 120%; font-family: 'courier new', courier; font-size: 8pt;"><span style="color:#ff0000">(</span><span style="color:#0000ff">defun</span>&nbsp;c:chkmenu&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">/</span>&nbsp;menuGroups&nbsp;eachMG&nbsp;nameMG<span style="color:#ff0000">)</span><br />
<span style="color:#ff0000">(</span><span style="color:#0000ff">if</span>&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">car</span>&nbsp;<span style="color:#ff0000">(</span>atoms-family&nbsp;<span style="color:#008000">1</span>&nbsp;'<span style="color:#ff0000">(</span><span style="color:#ff00ff">"vl-load-com"</span><span style="color:#ff0000">)</span><span style="color:#ff0000">)</span><span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vl-load-com</span><span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">)</span><br />
<br />
&nbsp;&nbsp;&nbsp;<span style="background-color:#e6e6e6; color:#800080">;;&nbsp;Get&nbsp;AutoCAD&nbsp;application,&nbsp;and&nbsp;menuGroups<br /></span>
&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">setq</span>&nbsp;acadapp&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vlax-get-acad-object</span><span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;menuGroups&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vla-get-menugroups</span>&nbsp;acadapp<span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">)</span><br />
<br />
&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vlax-for</span>&nbsp;eachMG&nbsp;menuGroups<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">setq</span>&nbsp;nameMG&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vla-get-name</span>&nbsp;eachMG<span style="color:#ff0000">)</span><span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">if</span>&nbsp;<span style="color:#ff0000">(</span>=&nbsp;nameMG&nbsp;<span style="color:#ff00ff">"BATCHPUBLISH"</span><span style="color:#ff0000">)</span>&nbsp;<span style="background-color:#e6e6e6; color:#800080">;Change&nbsp;this&nbsp;to&nbsp;the&nbsp;name&nbsp;of&nbsp;your&nbsp;menu<br /></span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">princ</span>&nbsp;<span style="color:#ff00ff">"\n&nbsp;BATCHPUBLISH&nbsp;menu&nbsp;has&nbsp;been&nbsp;loaded."</span><span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">princ</span><span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;<span style="color:#ff0000">)</span><br />
</span></p>
