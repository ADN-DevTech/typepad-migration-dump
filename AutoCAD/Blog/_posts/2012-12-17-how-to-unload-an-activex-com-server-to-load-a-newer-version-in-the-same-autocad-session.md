---
layout: "post"
title: "How to Unload an ActiveX COM Server to load a newer version in the same AutoCAD session"
date: "2012-12-17 16:10:05"
author: "Fenton Webb"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "ActiveX"
  - "AutoCAD"
  - "Fenton Webb"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/12/how-to-unload-an-activex-com-server-to-load-a-newer-version-in-the-same-autocad-session.html "
typepad_basename: "how-to-unload-an-activex-com-server-to-load-a-newer-version-in-the-same-autocad-session"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>  <p><b>Issue</b></p>  <p>We are using in-process ActiveX servers. To use the component, we call GetInterfaceObject(). When developing it, we are changing the code and need to load newer version, and to save time, preferably within the same AutoCAD session. Sometimes we can load the new version using GetInterfaceObject() with newer version, but other times it fails.</p>  <p>Is there any method to unload the interface object to load newer version of the component?</p>  <p><a name="section2"></a></p>  <p><b>Solution</b></p>  <p>The COM unloading mechanism requires that a COM client from &quot;time to time&quot; calls the function <a href="http://msdn.microsoft.com/en-us/library/windows/desktop/ms679712(v=vs.85).aspx">CoFreeUnusedLibraries</a>() in order to free up the memory and unload the libraries which are loaded but not used by any client in the system. AutoCAD being a COM client also makes calls to this function but you cannot anticipate the time when this call is being made.</p>  <p>The fact that sometimes you can easily GetInterfaceObject with newer version but sometimes it fails means that the old library is still loaded, and AutoCAD didn't call <a href="http://msdn.microsoft.com/en-us/library/windows/desktop/ms679712(v=vs.85).aspx">CoFreeUnusedLibraries</a>() yet.</p>  <p>To solve the problem and unload the old library you only have to make a call to <a href="http://msdn.microsoft.com/en-us/library/windows/desktop/ms679712(v=vs.85).aspx">CoFreeUnusedLibraries</a>(). </p>  <p>Tip - be sure to check out <a href="http://msdn.microsoft.com/en-us/library/windows/desktop/ms678413(v=vs.85).aspx">CoFreeUnusedLibrariesEx</a>() also…</p>
