---
layout: "post"
title: "What happened to acrxProductKey() in AutoCAD 2013"
date: "2012-08-27 15:26:48"
author: "Fenton Webb"
categories:
  - "2013"
  - "AutoCAD"
  - "Fenton Webb"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/08/what-happened-to-acrxproductkey-in-autocad-2013.html "
typepad_basename: "what-happened-to-acrxproductkey-in-autocad-2013"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html"><font color="#0066cc">Fenton Webb</font></a></p>  <p>You may have noticed that the acrxProductKey() has disappeared in ObjectARX 2013. This is because of all the product refactoring that happened for the Mac and the new accoreconsole.exe.</p>  <p>To obtain the same information that acrxProductKey() used to return, you should instead now use:</p>  <p><strong>acdbHostApplicationServices()-&gt;getUserRegistryProductRootKey();</strong></p>  <p>In .NET you can access the same property using</p>  <p><strong><font size="1">Autodesk.AutoCAD.DatabaseServices.HostApplicationServices.UserRegistryProductRootKey</font></strong></p>
