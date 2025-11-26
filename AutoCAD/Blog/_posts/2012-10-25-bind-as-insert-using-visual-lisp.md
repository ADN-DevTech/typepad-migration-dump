---
layout: "post"
title: "Bind-as-Insert, using Visual LISP"
date: "2012-10-25 23:53:45"
author: "Balaji"
categories:
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "LISP"
original_url: "https://adndevblog.typepad.com/autocad/2012/10/bind-as-insert-using-visual-lisp.html "
typepad_basename: "bind-as-insert-using-visual-lisp"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<div>To&nbsp;perform a bind-as-insert of an Xref, using Visual LISP, here is a sample code.<!--startindex--></div>
<div>
<div>By setting the BINDTYPE system variable to 1, a bind-as-insert is performed:</div>
<p><span style="line-height: 120%; font-family: 'courier new', courier; font-size: 8pt;"><span style="color:#ff0000">(</span><span style="color:#0000ff">setvar</span>&nbsp;<span style="color:#ff00ff">"BINDTYPE"</span>&nbsp;<span style="color:#008000">1</span><span style="color:#ff0000">)</span><br />
<span style="color:#ff0000">(</span><span style="color:#0000ff">command</span>&nbsp;<span style="color:#ff00ff">"_-xref"</span>&nbsp;<span style="color:#ff00ff">"_bind"</span>&nbsp;<span style="color:#ff00ff">"MYBLOCK"</span><span style="color:#ff0000">)</span><br />
<span style="color:#ff0000">(</span><span style="color:#0000ff">setvar</span>&nbsp;<span style="color:#ff00ff">"BINDTYPE"</span>&nbsp;<span style="color:#008000">0</span><span style="color:#ff0000">)</span></span></p>
<div>Using Visual Lisp, the following code will accomplish the same task.</div>
</div>
<p><span style="line-height: 120%; font-family: 'courier new', courier; font-size: 8pt;"><span style="color:#ff0000">(</span><span style="color:#0000ff">defun</span>&nbsp;c:BindInsert&nbsp;<span style="color:#ff0000">(</span>&nbsp;<span style="color:#0000ff">/</span>&nbsp;appAcad&nbsp;docActive&nbsp;colBlocks&nbsp;objBlk<span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vl-load-com</span><span style="color:#ff0000">)</span>&nbsp;<span style="background-color:#e6e6e6; color:#800080">;load&nbsp;ActiveX<br /></span>
&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">setq</span>&nbsp;appAcad&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vlax-get-acad-object</span><span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;docActive&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vla-get-ActiveDocument</span>&nbsp;appAcad<span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">)</span>&nbsp;<span style="background-color:#e6e6e6; color:#800080">;setq<br /></span>
&nbsp;&nbsp;&nbsp;<span style="background-color:#e6e6e6; color:#800080">;;get&nbsp;the&nbsp;blocks&nbsp;collection<br /></span>
&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">setq</span>&nbsp;colBlocks&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vla-get-blocks</span>&nbsp;docActive<span style="color:#ff0000">)</span><span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vlax-for</span>&nbsp;objBlk&nbsp;colBlocks<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="background-color:#e6e6e6; color:#800080">;;Is&nbsp;the&nbsp;block&nbsp;an&nbsp;xref?<br /></span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">if</span>&nbsp;<span style="color:#ff0000">(</span>=&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vlax-get-property</span>&nbsp;objBlk&nbsp;'IsXref<span style="color:#ff0000">)</span>&nbsp;:vlax-true<span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="background-color:#e6e6e6; color:#800080">;;if&nbsp;True,&nbsp;then&nbsp;bind&nbsp;it&nbsp;as&nbsp;an&nbsp;insert<br /></span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vlax-invoke-method</span>&nbsp;objBlk&nbsp;<span style="color:#ff00ff">"bind"</span>&nbsp;:vlax-true<span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">)</span>&nbsp;<span style="background-color:#e6e6e6; color:#800080">;if<br /></span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vlax-release-object</span>&nbsp;objBlk<span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">)</span>&nbsp;<span style="background-color:#e6e6e6; color:#800080">;vlax-for<br /></span>
&nbsp;&nbsp;&nbsp;<span style="background-color:#e6e6e6; color:#800080">;;release&nbsp;objects&nbsp;from&nbsp;memory<br /></span>
&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vlax-release-object</span>&nbsp;colBlocks<span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vlax-release-object</span>&nbsp;docActive<span style="color:#ff0000">)</span><br />
<span style="color:#ff0000">)</span><br />
</span></p>
