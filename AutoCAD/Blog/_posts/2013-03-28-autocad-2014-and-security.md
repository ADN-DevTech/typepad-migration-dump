---
layout: "post"
title: "AutoCAD 2014 and Security"
date: "2013-03-28 11:55:23"
author: "Gopinath Taget"
categories:
  - ".NET"
  - "2014"
  - "ActiveX"
  - "AutoCAD"
  - "Fenton Webb"
  - "Gopinath Taget"
  - "LISP"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2013/03/autocad-2014-and-security.html "
typepad_basename: "autocad-2014-and-security"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/gopinath-taget.html">Gopinath Taget</a></p>
<p>Have you downloaded and installed <a href="http://www.autodesk.com/products/autodesk-autocad/overview">AutoCAD 2014</a> yet? If you did, you will notice a new set of <a href="http://docs.autodesk.com/ACD/2014/ENU/files/GUID-9C7E997D-28F8-4605-8583-09606610F26D.htm">security features</a> in AutoCAD that will not let you load custom applications that are not in a secure, trusted path. This behavior is controlled with <a href="http://docs.autodesk.com/ACD/2014/ENU/files/GUID-541566C6-2738-49DD-87C3-C1490E924A02.htm">SECURELOAD</a> and <a href="http://docs.autodesk.com/ACD/2014/ENU/files/GUID-2FB4611D-F141-48D5-9B6E-460EB59351AF.htm">TRUSTEDPATHS</a> settings. </p>
<p>The easiest and best way to deploy your apps is to install your applications as <a href="http://docs.autodesk.com/ACD/2014/ENU/files/GUID-5E50A846-C80B-4FFD-8DD3-C20B22098008.htm">Application bundles</a> as the bundle paths are automatically treated as trusted paths. Check out this<a href="http://adndevblog.typepad.com/autocad/2013/01/autodesk-autoloader-white-paper.html" target="_self"> Autodesk Autoloader white paper</a> for detailed information on its technology.</p>
<p>If this is not an option for you, you could try <a href="http://en.wikipedia.org/wiki/Code_signing">digitally signing</a> your application. For this you will need a digital certificate from a well know <a href="http://en.wikipedia.org/wiki/Certificate_authority">certificate authority</a>. AutoCAD 2014 will load digitally signed applications even if they are not in a trusted path. </p>
<p>One note for .NET developers. Digitally signing your application is different from strong naming your applications. <a href="http://blogs.msdn.com/b/junfeng/archive/2006/03/11/549355.aspx">Here</a> is a very informative FAQ on the differences.</p>
