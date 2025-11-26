---
layout: "post"
title: "Customization with Navisworks Freedom"
date: "2012-06-14 21:29:00"
author: "Saikat Bhattacharya"
categories:
  - ".NET"
  - "Navisworks"
  - "Saikat Bhattacharya"
original_url: "https://adndevblog.typepad.com/aec/2012/06/customization-with-navisworks-freedom.html "
typepad_basename: "customization-with-navisworks-freedom"
typepad_status: "Draft"
---

<p>By <a href="http://adndevblog.typepad.com/aec/saikat-bhattacharya.html" target="_self">Saikat Bhattacharya</a></p>
<p>With the three flavors of Navisworks, often third party developers have wondered about the customization possibilities with Navisworks Freedom. This blog post covers this topic - </p>
<p>Is it posible to run any program/plugin inside NW Freedom? <br />Is there any way to show Navisworks Freedom in the browser or any application like sample prog ACTX_01.html ? </p>
<p>Plugins will not work in Freedom because they are not supported (neither COM nor .NET API). Even though you might find the PlugIns folder in the Freedom install location, the directory has been created erroneously by the installer and should be ignored. </p>
<p>Regarding the query on running Freedom in the browser, one alternative could be the use of controls of the Navisworks .NET API and embed them in the web application. But these controls are built-in which means they need to have a product of <br />Navisworks installed. We can specify the product of the control e.g. if we want to specify Freedom as the product. We could set the Autodesk.Navisworks.Api.Controls.ApplicationControl.RequestedRuntime value to be RuntimeNames.NavisworksFreedom as enumeration value. </p>
<p>To sum up - A developer could use Freedom to use the basic Controls API functionality; its only the plug-ins that are invalid and do not work with Freedom. </p>
