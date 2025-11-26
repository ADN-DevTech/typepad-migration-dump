---
layout: "post"
title: "Length of dictionary key names"
date: "2012-05-25 17:28:52"
author: "Gopinath Taget"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Gopinath Taget"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/length-of-dictionary-key-names.html "
typepad_basename: "length-of-dictionary-key-names"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/gopinath-taget.html">Gopinath Taget</a></p>  <p>The same rules apply to dictionary keys as to symbol table names: 255 characters or fewer, may be alphanumeric, may contain a dollar symbol ($), underscore (_) or hyphen (-). You can use the ARX function acdbSNValid() to check the validity of a symbol table name. </p>  <p>The allowable length is also dependent on the value of the sysvar &quot;EXTNAMES&quot;. For more details, please refer to the online documentation.</p>
