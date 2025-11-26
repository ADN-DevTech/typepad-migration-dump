---
layout: "post"
title: "Loading 3rd party ARXs into an OEM product"
date: "2013-02-01 11:44:39"
author: "Fenton Webb"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD OEM"
  - "Fenton Webb"
original_url: "https://adndevblog.typepad.com/autocad/2013/02/loading-3rd-party-arxs-into-an-oem-product.html "
typepad_basename: "loading-3rd-party-arxs-into-an-oem-product"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>  <p><b>Issue</b></p>  <p>Can I load a third party ARX module into my OEM product? For instance, an ARX that checks a hardware lock.</p>  <p><a name="section2"></a></p>  <p><b>Solution</b></p>  <p>No you can't. All ARX modules that are loaded into an OEM product MUST be bound specifically to that product. This means that they must be compiled and linked with the OEM version of ObjectARX.</p>  <p>The only exceptions to this are DBX modules, or Object Enablers. As an OEM developer, you have the option to allow unsecured DBX modules to be loaded into your product. If this is enabled (it is enabled by default), then your product can load third party DBX modules that will enable your product to display custom entities in the drawing instead of leaving them as proxy entities. However, if you do not want to allow object enablers to be used in your product, you can disable this feature in the OEM Make Wizard.</p>
