---
layout: "post"
title: "Source &amp; destination database's are the same while Wblocking in AutoCAD"
date: "2012-06-29 16:28:09"
author: "Fenton Webb"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Fenton Webb"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/06/source-destination-databases-are-the-same-while-wblocking-in-autocad.html "
typepad_basename: "source-destination-databases-are-the-same-while-wblocking-in-autocad"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>  <p><b>Issue</b></p>  <p>Why is it that the source and destination databases are the same in 'otherWblock' method of AcRxEventReactor? This happens while wblocking an entire database.</p>  <p><b>Solution</b></p>  <p>This behavior is as designed. For performance reasons, there are certain cases where it will wblockClone() into the same database, write the cloned objects into the drawing file and then erase the cloned objects.</p>  <p>If you do not wish so then you have to call for AcDbDatabase::forceWblockDatabaseCopy().   <br /></p>  <p>Note: This method is non-functional unless called from within the AcRxEventReactor::wblockNotice() or   <br />AcEditorReactor::wblockNotice() sent out for the wblock operation on which this method is to force the database copy.</p>  <p>For a more detailed description please refer to ObjectARX help files.</p>
