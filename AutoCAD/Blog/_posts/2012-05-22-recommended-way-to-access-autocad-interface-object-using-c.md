---
layout: "post"
title: "Recommended way to access AutoCAD Interface Object using C++"
date: "2012-05-22 15:05:31"
author: "Augusto Goncalves"
categories:
  - "Augusto Goncalves"
  - "AutoCAD"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/recommended-way-to-access-autocad-interface-object-using-c.html "
typepad_basename: "recommended-way-to-access-autocad-interface-object-using-c"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/augusto-goncalves.html">Augusto Goncalves</a></p>  <p>The common way to access AutoCAD interface object is GetActiveObject, but this may return null on certain scenarios, such as On_kInitAppMsg, and is not guaranteed to access the current AutoCAD.</p>  <p>Since the ObjectARX application is loaded into AutoCAD process space, there is no need to go to running object’s table to get the COM pointer. So we can get it directly using the following code snippet (which requires MFC support):</p>  <div style="font-family: ; background: white">   <p style="margin: 0px"><font face="Consolas"><font style="font-size: 10pt" color="#000000">IAcadApplicationPtr pAcadApp;</font></font></p>    <p style="margin: 0px"><font face="Consolas"><font style="font-size: 10pt" color="#000000">IDispatch* pDispatch = acedGetAcadWinApp()-&gt;GetIDispatch(FALSE);</font></font></p>    <p style="margin: 0px"><font face="Consolas"><font color="#000000"><font style="font-size: 10pt">pDispatch-&gt;QueryInterface(__uuidof(IAcadApplication), </font></font></font></p>    <p style="margin: 0px"><font face="Consolas"><font color="#000000"><font style="font-size: 10pt">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; (</font></font><font style="font-size: 10pt"><span><font color="#0000ff">void</font></span><font color="#000000">**)&amp;pAcadApp);</font></font></font></p> </div>
