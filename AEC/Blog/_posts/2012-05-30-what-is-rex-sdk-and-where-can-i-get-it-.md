---
layout: "post"
title: "What is REX SDK and where can I get it? "
date: "2012-05-30 16:33:00"
author: "Mikako Harada"
categories:
  - ".NET"
  - "Mikako Harada"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2012/05/what-is-rex-sdk-and-where-can-i-get-it-.html "
typepad_basename: "what-is-rex-sdk-and-where-can-i-get-it-"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/mikako-harada.html" target="_self" title="Mikako Harada">Mikako Harada</a></p>
<p>Yesterday we mentioned about&#0160;REX SDK. Here is a little background information about this SDK. Revit has subscription-only add-ins called Revit Extensions. There is a SDK to write an add-ins in a similar manner and it is called&#0160;REX SDK or SDK for Revit Extensions.</p>
<p>REX SDK is development environment used&#0160;by the Autodesk Revit Extension development team. It is written on top of Revit API and offers access to functionalities used in Revit Extensions, such as UI components used in Revit Extensions-style&#0160;dialog and controls, as well as utility functions like unit conversions.</p>
<p>The REX SDK is currently implemented as a form of Microsoft Visual Studio C# template. Using the template provided, you can quickly build an add-in that has a similar look &amp; feel to Autodesk Revit Extensions. Functionalities of REX SDK themselves do not provide an additional access to internal Revit or other main products themselves.&#0160;</p>
<p>Since Revit 2012 releases, it is a part of Revit API SDK.&#0160; Please take a look under &lt;SDK install&gt;/REX SDK/ folder for samples and documentations.&#0160; Please also note that REX SDK is still subject to changes.</p>
<p>&#0160;</p>
