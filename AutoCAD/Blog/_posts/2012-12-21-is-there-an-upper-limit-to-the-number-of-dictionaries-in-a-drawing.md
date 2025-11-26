---
layout: "post"
title: "Is there an upper limit to the number of dictionaries in a drawing?"
date: "2012-12-21 17:55:38"
author: "Gopinath Taget"
categories:
  - ".NET"
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "ActiveX"
  - "AutoCAD"
  - "Gopinath Taget"
  - "LISP"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/12/is-there-an-upper-limit-to-the-number-of-dictionaries-in-a-drawing.html "
typepad_basename: "is-there-an-upper-limit-to-the-number-of-dictionaries-in-a-drawing"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/gopinath-taget.html">Gopinath Taget</a></p>  <p>There is no hard-coded limit to the number of dictionaries you can have in an AcDbDatabase, although it is recommended that they be nested. Inside the named objects dictionary, you should have a top level dictionary using your company's developer ID (typically four characters) as a prefix in order to prevent conflicts with names. Underneath this top-level dictionary, it is recommended that application-specific dictionaries are maintained and that they be nested, if necessary.</p>
