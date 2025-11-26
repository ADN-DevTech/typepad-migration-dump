---
layout: "post"
title: "Default Selection Highlight method"
date: "2012-05-28 20:22:03"
author: "Xiaodong Liang"
categories:
  - "COM"
  - "Navisworks"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/aec/2012/05/default-selection-highlight-method.html "
typepad_basename: "default-selection-highlight-method"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p><strong>Issue </strong><br />In Navisworks you can change the default option of highlight mode e.g. Shaded, Wireframe, Tinted. ActiveX control uses the mode. But how to change the mode without opening the product ?    <br /><br /><strong>Solution </strong><br />ActiveX control’s depends on the option of product, which can be set in registry: HKEY_CURRENT_USER\Software\Autodesk\Navisworks Manage\8.0\GlobalOptions\interface\selection\highlight  There is no direct API way to set this option, but it is not difficult to manipulate the registry. <br /><br /> 8 27:RoamerGUI_HighlightMethod:0Shaded <br />8 27:RoamerGUI_HighlightMethod:1Wireframe <br />8 27:RoamerGUI_HighlightMethod:2Tinted</p>
