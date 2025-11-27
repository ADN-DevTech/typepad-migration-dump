---
layout: "post"
title: "Unload .NET AddIn without closing Inventor"
date: "2013-10-15 07:18:19"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/10/unload-net-addin-without-closing-inventor.html "
typepad_basename: "unload-net-addin-without-closing-inventor"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>Unfortunately, it is not possible. This is not specific to <strong>Inventor</strong> or the other Autodesk products, but is the case in many applications loading AddIn&#39;s. The problem is that for various reasons, one of which could be e.g. <a href="http://stackoverflow.com/questions/18456491/why-is-entity-framework-significantly-slower-when-running-in-a-different-appdoma" target="_self">speed</a>, applications tend to load the AddIn into the default&#0160;<strong><a href="http://blogs.msdn.com/b/cclayton/archive/2013/05/21/understanding-application-domains.aspx" target="_self">AppDomain</a></strong>. And inside .NET you cannot unload a single assembly, you can only unload the <strong>AppDomain</strong> the assembly was loaded into, as also mentioned here in context of <strong>AutoCAD</strong>: <a href="http://through-the-interface.typepad.com/through_the_interface/2008/09/tired-of-not-be.html" target="_self">http://through-the-interface.typepad.com/through_the_interface/2008/09/tired-of-not-be.html</a>&#0160;</p>
<p>Note: you can logically unload an Inventor AddIn inside the <strong>Add-In Manager</strong>, i.e. its&#0160;<strong>Deactivate</strong>&#0160;function will be called where the AddIn can remove its user interface, stop listening to events, etc, but the dll itself will not be unloaded from the Inventor process.</p>
<p>As Kean says, probably the best thing to do is use <strong>Edit and Continue</strong>&#0160;instead which works fine on 32 bit. On 64 bit, however, it does not work yet. But at last it seems to be coming in <strong>VS 2013</strong> :)&#0160;<a href="http://blogs.msdn.com/b/dotnet/archive/2013/06/26/announcing-the-net-framework-4-5-1-preview.aspx" target="_self">http://blogs.msdn.com/b/dotnet/archive/2013/06/26/announcing-the-net-framework-4-5-1-preview.aspx</a></p>
<p>I had a look at modifying an existing AddIn so that it runs in a separate <strong>AppDomain</strong> but it&#39;s not straight forward to make it unloadable. Even if you create your AddIn class in a separate <strong>AppDomain</strong>&#0160;you might still not be able to unload the <strong>AddIn&#0160;dll&#0160;</strong>itself.&#0160;</p>
