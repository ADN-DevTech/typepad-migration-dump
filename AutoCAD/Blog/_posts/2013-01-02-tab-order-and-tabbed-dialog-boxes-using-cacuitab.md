---
layout: "post"
title: "Tab order and tabbed dialog boxes using CAcUiTab"
date: "2013-01-02 17:48:49"
author: "Gopinath Taget"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Gopinath Taget"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/tab-order-and-tabbed-dialog-boxes-using-cacuitab.html "
typepad_basename: "tab-order-and-tabbed-dialog-boxes-using-cacuitab"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/gopinath-taget.html">Gopinath Taget</a></p>  <p>When using the CAcUiTab class, you might observe some problems tabbing between the main dialog controls and the child dialog controls of the current tab.</p>  <p>It should work like ordinary AutoCAD tab dialogs. So how can you do that i.e, to the controls in child dialog to be in &quot;Tab Order&quot;.</p>  <p>The tabbing problem can be resolved by setting WS_EX_CONTROLPARENT style for the child dialog. This can also be done through the dialog resource editor by setting the child dialog's style 'control' to true by selecting the same from properties window.</p>
