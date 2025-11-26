---
layout: "post"
title: "WBLOCK - A peek under the hood"
date: "2012-05-17 18:37:31"
author: "Gopinath Taget"
categories:
  - ".NET"
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Gopinath Taget"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/wblock-a-peek-under-the-hood.html "
typepad_basename: "wblock-a-peek-under-the-hood"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/gopinath-taget.html">Gopinath Taget</a></p>
<p>WBLOCK honors hard references (as it always has). This means hard owner and hard pointer references would be cloned during the WBLOCK process. If an object that has been chosen for WBLOCK has a hard reference to a second object,&#0160;the second object is copied during the wblockClone operation (which is used by the WBLOCK command) as well. But this was a potential problem in early releases of AutoCAD (Pre-AutoCAD 2000). Why? The problem with the WBLOCK command was that it first wblockClone&#39;s objects into a temporary in-memory database, then calls save() to save the temporary database to disk. But save honors only the ownership references (hard owner and soft owner). This means one kind of hard reference called the hard pointer reference will not be honored by save and that means objects pointed to by hard pointer references would not be saved. Put another way, objects that have been cloned during wblockClone are not saved to the drawing file if their owners haven&#39;t been cloned during wblockClone.</p>
<p>To avoid this problem with wblockClone, dictionaries and custom objects (Inheriting from AcDbObject) now automatically file a hardpointer id of their owners. In this way, the complete ownership hierarchy is preserved, and save() can save them all.</p>
