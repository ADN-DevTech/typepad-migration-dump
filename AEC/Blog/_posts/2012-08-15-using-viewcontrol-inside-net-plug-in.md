---
layout: "post"
title: "Using ViewControl inside .NET plug-in"
date: "2012-08-15 01:45:41"
author: "Xiaodong Liang"
categories:
  - ".NET"
  - "Navisworks"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/aec/2012/08/using-viewcontrol-inside-net-plug-in.html "
typepad_basename: "using-viewcontrol-inside-net-plug-in"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>  <p><b>Issue</b></p>  <p>Is it possible to use separate Navisworks ViewControl&#160; (as usercontrol) inside a .NET plug-in?</p>  <p><a name="section2"></a></p>  <p><b>Solution</b></p>  <p>it is not a current supported scenario to use .NET control within .NET plug-in of Navisworks.&#160;&#160; The comments <strong>IApplicationGui</strong> in the topic [Using Control] in the API help document may probably have misguided you. It says:</p>  <p><em>This interface has been created to provide access to the GUI of the application which is hosting the API. The Gui property provides this interface, primarily for plug-ins contained within Navisworks. However, applications using the controls API will find that Gui is null as Navisworks is not loaded. Instead, applications which wish to access this (possibly using plug-ins which support controls) will need to implement this interface and call SetApplicationGui. </em></p>  <p>It should be better described as below:</p>  <p><em>This interface IApplicationGui provides access to the GUI of the application which is hosting the API. The Gui property provides access to the interface. When the API is used within Navisworks, an implementation of the interface is provided by the Navisworks application. Third party applications that use the controls API must provide their own implementation of this interface if required and call SetApplicationGui. This approach allows plug-ins to be created that can interact with the application GUI and which can be loaded into both Navisworks and third party controls based applications.</em></p>
