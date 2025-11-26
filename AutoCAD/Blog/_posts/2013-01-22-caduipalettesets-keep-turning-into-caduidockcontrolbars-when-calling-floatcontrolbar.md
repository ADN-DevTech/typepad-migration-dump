---
layout: "post"
title: "CAdUiPaletteSet's keep turning into CAdUiDockControlBars when calling FloatControlBar"
date: "2013-01-22 19:28:27"
author: "Gopinath Taget"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Gopinath Taget"
  - "MFC"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/caduipalettesets-keep-turning-into-caduidockcontrolbars-when-calling-floatcontrolbar.html "
typepad_basename: "caduipalettesets-keep-turning-into-caduidockcontrolbars-when-calling-floatcontrolbar"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/gopinath-taget.html">Gopinath Taget</a></p>  <p>You might find that instances of CAdUiPaletteSet's keep turning into CAdUiDockControlBars when called with FloatControlBar method.</p>  <p>Applications normally manage the state of docking windows (CControlBars) using the standard MFC methods CFrameWnd::DockControlBar(), CFrameWnd::FloatControlBar() and CFrameWnd::ShowControlBar() (Docking windows should not be shown/hidden using calls to CWnd::ShowWindow()).&#160; </p>  <p>Applications can continue to call CFrameWnd::DockControlBar() and CFrameWnd::ShowControlBar() normally. However, when calling CFrameWnd::FloatControlBar(), applications will need to &quot;swap&quot; the floating frame class if the control bar passed to FloatControlBar() is a kind of CAdUiPaletteSet. Applications can perform this &quot;swap&quot; by calling the new method AdUiSetFloatingFrameClass() before and after the call to FloatControlBar(): </p>  <p><font size="1">CRect mRect(100,100, 300, 600);     <br />GetPaletteSet()-&gt;InitFloatingPosition(&amp;mRect);</font></p>  <p><font size="1">CMDIFrameWnd* pAcadFrame = acedGetAcadFrame();     <br />CRuntimeClass* pCurrentFloatingFrameClass = AdUiSetFloatingFrameClass(RUNTIME_CLASS(CAdUiPaletteSetDockFrame));      <br />acedGetAcadFrame()-&gt;FloatControlBar(GetPaletteSet(),CPoint(mRect.left, mRect.top),CBRS_ALIGN_TOP );       <br />AdUiSetFloatingFrameClass(pCurrentFloatingFrameClass);</font></p>  <p>Why is all of this necessary? MFC's CFrameWnd class caches a pointer to the floating frame class used as a private member of the class. This class pointer is used to instantiate a floating frame for dock control bars when they are floating by CFrameWnd::FloatControlBar(). AdUi needs to continue to support for custom dialogs derived directly from CAdUiControlBar. So AdUi host applications need to use two different floating frame classes, one for those derived from CAdUiControlBar and another for those derived from CAdUiPaletteSet.&#160; </p>  <p>Unfortunately FloatControlBar() is not a virtual method so it is not possible to override it in the application frame class to automatically detect the control bar class and use the correct floating frame. Instead applications must detect the control bar class themselves and manually swap the floating frame class before calling CFrameWnd::FloatControlBar().</p>
