---
layout: "post"
title: "Removing xdata attached to an entity regardless of the appname"
date: "2012-10-25 23:32:57"
author: "Balaji"
categories:
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "LISP"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/10/removing-xdata-attached-to-an-entity-regardless-of-the-appname.html "
typepad_basename: "removing-xdata-attached-to-an-entity-regardless-of-the-appname"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>The following ObjectARX / Lisp code removes XData that is attached to an entity regardless of the APPNAME. Use it with caution and only if you need to do this, since removing XData from an entity without considering the appname may cause plug-ins that rely on them to misbehave.</p>
<p>Here is the ObjectARX code :</p>
<p></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">static</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> AdskTestCommand(</span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ads_name eNam;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ads_point pt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> ret;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ret = acedEntSel(ACRX_T(</span><span style="color: #a31515; line-height: 140%;">&quot;\nPick an entity :&quot;</span><span style="line-height: 140%;">), eNam, pt);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (RTNORM != ret)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; AcDbObjectId ObjId;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; acdbGetObjectId(ObjId, eNam);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; AcDbEntity *pEnt = NULL;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; Acad::ErrorStatus es </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp; = acdbOpenAcDbEntity(pEnt, ObjId, AcDb::kForWrite);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; resbuf *xdata = pEnt-&gt;xData(NULL);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (xdata)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; xdata-&gt;rbnext = NULL;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; pEnt-&gt;setXData(xdata);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; acutRelRb(xdata);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; pEnt-&gt;close();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>Here is the Lisp code:</p>
<p></p>
<p><span style="line-height: 120%; font-family: 'courier new', courier; font-size: 8pt;"><span style="color:#ff0000">(</span><span style="color:#0000ff">defun</span>&nbsp;c:DelXdata<span style="color:#ff0000">(</span><span style="color:#ff0000">)</span><br />
&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">setq</span>&nbsp;l&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">car</span>&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">entsel</span>&nbsp;<span style="color:#ff00ff">"Pick&nbsp;object:"</span><span style="color:#ff0000">)</span><span style="color:#ff0000">)</span><span style="color:#ff0000">)</span><br />
&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">if</span>&nbsp;l&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">progn</span><br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">(</span>redraw&nbsp;l&nbsp;<span style="color:#008000">3</span><span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">setq</span>&nbsp;le&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">entget</span>&nbsp;l&nbsp;'<span style="color:#ff0000">(</span><span style="color:#ff00ff">"*"</span><span style="color:#ff0000">)</span><span style="color:#ff0000">)</span>&nbsp;<span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">setq</span>&nbsp;xdata&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">assoc</span>&nbsp;'-3&nbsp;le<span style="color:#ff0000">)</span><span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">setq</span>&nbsp;le<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">(</span>subst&nbsp;<span style="color:#ff0000">(</span>cons&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">car</span>&nbsp;xdata<span style="color:#ff0000">)</span>&nbsp;<span style="color:#ff0000">(</span>list&nbsp;<span style="color:#ff0000">(</span>list&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">car</span>&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">car</span>&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">cdr</span>&nbsp;xdata<span style="color:#ff0000">)</span><span style="color:#ff0000">)</span><span style="color:#ff0000">)</span><span style="color:#ff0000">)</span><span style="color:#ff0000">)</span><span style="color:#ff0000">)</span><br />
xdata&nbsp;le<span style="color:#ff0000">)</span><span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">(</span>entmod&nbsp;le<span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">(</span>redraw&nbsp;l&nbsp;<span style="color:#008000">4</span><span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;le<br />
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">)</span><br />
&nbsp;<span style="color:#ff0000">)</span><br />
<span style="color:#ff0000">)</span></span></p>
