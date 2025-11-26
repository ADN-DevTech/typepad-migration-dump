---
layout: "post"
title: "How to lock Block Table Records from being edited?"
date: "2012-07-18 05:22:30"
author: "Philippe Leefsma"
categories:
  - "AutoCAD"
  - "ObjectARX"
  - "Philippe Leefsma"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/how-to-lock-block-table-records-from-being-edited.html "
typepad_basename: "how-to-lock-block-table-records-from-being-edited"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/philippe-leefsma.html" target="_self">Philippe Leefsma</a></p>  <p><b>Q:</b></p>    <p>It would be nice to have the ability to lock up table records like block definitions so they can't be altered. I know this can be done through the use of reactors, but I don't want our blocks being changed when going out to a client or design firm. This would allow to keep drawing integrity better.</p>    <p><a name="section2"></a></p>  <p><b>A:</b></p>  <p>You are correct with what you say about using reactors - without your application loaded the user can indeed modify the AcDbBlockTableRecord's.</p>  <p>The other way to do what you want is to create your own custom AcDbBlockTableRecord by deriving from AcDbBlockTableRecord and simply vetoing the subOpen() for write. This will work fine - unfortunately, the exception is when your custom AcDbBlockTableRecord DBX is not loaded; in this case AutoCAD does not expect to find Proxy entities residing in the AcDbBlockTable and because of this, implements no proxy support whatsoever. Obviously, without the proxy entity support trying to access the custom AcDblockTableRecord without the DLL host loaded will cause an exception.</p>  <p>At this time, there is no workaround.</p>
