---
layout: "post"
title: "AcDb::ePermanentlyErased when open cloned object during wblock-entire DWG"
date: "2013-03-08 01:21:00"
author: "Xiaodong Liang"
categories:
  - "AutoCAD"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/autocad/2013/03/acdbepermanentlyerased-when-open-cloned-object-during-wblock-entire-dwg.html "
typepad_basename: "acdbepermanentlyerased-when-open-cloned-object-during-wblock-entire-dwg"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p><strong>Issue     <br /></strong>Is it possible to open cloned objects in AcEditorReactor::beginDeepCloneXlation() or     <br />AcEditorReactor::endDeepClone()     <br />callbacks when WBLOCK - entire database is performed?</p>
<p>Trying to open them in    <br /> AcEditorReactor::beginDeepCloneXlation() or endDeepClone(),    <br />results in getting &quot;ePermanentlyErased&quot; return code. We need to access them in kForWrite mode to make some processing.    </p>
<p><a name="section2"></a></p>
&#0160; <strong>Solution     <br /></strong>The problem is that in the context of doing a wblock on the entire database, AutoCAD does a fast wblock operation, which means that AutoCAD does not really clone the objects but simulates doing it because the goal is to save the &#39;clones&#39; to a file. In order to prevent other users from modifying the clone objects (since the clone and original object are the same object in memory),    <br />AutoCAD returns &quot;ePermanentlyErased&quot; error code when an application tries to open and modify a clone.
<p>&#0160;</p>
<p>You have two options for opening the clone object:   <br /> The first one is to open the original object instead of the clone, and to read information from it. But if you want to modify the object, be aware that both the original and the clone will be affected.</p>
<p>The second option is to disable the fast wblock mechanism by calling AcDbDatabase::<span style="color: #004080;"><strong>forceWblockDatabaseCopy</strong></span>(). Doing this will force AutoCAD to create real clones of the objects. You will then be able to open and modify the clone objects and only them. See the ObjectARX online help for forceWblockDatabaseCopy() usage instructions.</p>
