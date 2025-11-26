---
layout: "post"
title: "Calling ObjectARX functions from a DLL doesn't work in AutoCAD OEM"
date: "2013-02-01 11:29:34"
author: "Fenton Webb"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD OEM"
  - "Fenton Webb"
original_url: "https://adndevblog.typepad.com/autocad/2013/02/calling-objectarx-functions-from-a-dll-doesnt-work-in-autocad-oem.html "
typepad_basename: "calling-objectarx-functions-from-a-dll-doesnt-work-in-autocad-oem"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>  <p><b>Issue</b></p>  <p>When I try to use an ObjectARX function from a DLL, it doesn't seem to execute. This works fine in AutoCAD but not OEM, why?</p>  <p><a name="section2"></a></p>  <p><b>Solution</b></p>  <p>This is the intended behavior. ObjectARX API functions can only be called from ARX modules which have been properly bound to the OEM product. A simple workaround for this is to turn the DLL into an ARX. This can be done by implementing a basic acrxEntryPoint function and linking the DLL with the RXAPI.LIB library. Now you can rename the file to have an ARX extension and it can be bound to the OEM product, allowing the ARX functions to work properly.</p>
