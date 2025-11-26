---
layout: "post"
title: "Find an entity under the cursor position using Win32 and ObjectARX"
date: "2013-01-17 15:10:26"
author: "Fenton Webb"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Fenton Webb"
  - "MFC"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/find-an-entity-under-the-cursor-position-using-win32-and-objectarx.html "
typepad_basename: "find-an-entity-under-the-cursor-position-using-win32-and-objectarx"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>  <p><b>Issue</b></p>  <p>How do I find the entity under the cursor position using raw Win32?</p>  <p><a name="section2"></a></p>  <p><b>Solution</b></p>  <p>The best way to find the entity under the cursor/crosshair is to use the <a href="http://adndevblog.typepad.com/autocad/2013/01/how-to-find-out-if-an-acedgetpoint-was-taken-by-an-osnap-override.html">AcEdInputPointMonitor</a> class. However, if you must find the entity under the cursor using raw Win32, here’s how…</p>  <p>First, to catch a mouse event before any &quot;normal&quot; AutoCAD's action (such as, pressing mouse down/up) takes place, you can register your own event-handling function, using <strong>acedRegisterFilterWinMsg</strong>(). ObjectARX 2008 SDK comes with a good sample about this (objectarx\samples\editor\mfcsamps\pretranslate). Please refer it for the detail about capturing AutoCAD Window's messaging.</p>  <p>Secondly, in the actual event-handling function of your own, you can use <strong>acedSSGet</strong>() with the option &quot;_:E&quot; (select everything within the cursor's object selection pickbox option). By specifying only the first point, you can make a selection window as the same as the cursor position.</p>  <p>The following code defines a function that you can register with <strong>acedRegisterFilterWinMsg</strong>(). To test this code, simply replace the function named FilterMouse() in the sample project (\objectarx\samples\editor\mfcsamps\pretranslate) with the following one.</p>  <pre>BOOL filterMouse(MSG *pMsg)<br />{<br /> if( pMsg-&gt;message == WM_MOUSEMOVE&#160;&#160; || <br />&#160; pMsg-&gt;message == WM_LBUTTONDOWN || pMsg-&gt;message == WM_LBUTTONUP ) {
<p>&#160; acedDwgPoint cpt={ 0, 0, 0 };<br />&#160; CPoint cPnt(pMsg-&gt;lParam);<br />&#160; acedCoordFromPixelToWorld(cPnt, cpt) ;<br />&#160; ads_point pt={ cpt[X], cpt[Y], 0 } ;<br />&#160; ads_name ss;<br />&#160; acedSSGet(L&quot;_:E&quot;, pt, NULL, NULL, ss) ;<br />&#160; long len =0 ;<br />&#160; acedSSLength(ss, &amp;len) ;<br />&#160; acutPrintf(L&quot;\nThe ss length is %d&quot;, len) ;<br />&#160; acedSSFree(ss) ;<br />&#160; if ( pMsg-&gt;message == WM_LBUTTONDOWN || pMsg-&gt;message == WM_LBUTTONUP )<br />&#160;&#160; unmouse ();<br /> }<br /> return (FALSE) ; //----- continue<br />}</p></pre>
