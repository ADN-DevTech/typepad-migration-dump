---
layout: "post"
title: "Side database issues in Mechanical "
date: "2015-09-23 05:55:51"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "AutoCAD Mechanical"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/09/side-database-issues-in-mechanical-.html "
typepad_basename: "side-database-issues-in-mechanical-"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>Side database (or non-working database) is the database of a drawing loaded into memory in the background, i.e. the database of a drawing which is not visible in the <strong>UI</strong>.</p>
<p>Though <strong>AutoCAD Mechanical</strong>&#0160;supports&#0160;<strong>API</strong> interaction with side databases, you might run into issues if you try to save a side database. So the best is to avoid that by opening up the document as normal and interact with them programmatically that way.</p>
<p>If you still want to modify and save a drawing in the background then you could&#0160;open a new <strong>AutoCAD</strong> instance (acad.exe) in the background that will open the drawing normally and interact with that programmatically (similar how background printing works). You do not have to use <strong>COM Automation</strong> for it: if the process is started with the appropriate parameters then it could run a script, or some add-in could be set up of autoload that would execute the required actions.</p>
<p>Some notes from the <strong>SDK readme&#0160;file</strong>: &#0160;</p>
<p>In <strong>ACM</strong>, the non-working database must be created without associating to the current document. When create <strong>AcDbDatabase</strong> object, in the <strong>AcDbDatabase</strong> constructor, you need to set the second parameter, <strong>noDocument</strong> to <strong>true</strong>.</p>
<p><strong>Non-working Database</strong> <br />· To use the function, <strong>AcDbDatabase::readDwgFile()</strong> to open a non-working database, you have to start a transaction first. <br />· If there are xref drawings attached to a non-working database, then for xref databases to be loaded and resolved, the application must explicitly call the function, <strong>acdbResolveCurrentXRefs()</strong>. This will ensure that all the xref databases are properly loaded. If the xrefs needs to be redirected, then <strong>XLOADCTL</strong> needs to be set to 1 prior to calling <strong>acdbResolveCurrentXRefs</strong>, otherwise the database will not be properly initialized. <br />· To use the function, <strong>amiGetDrawingType()</strong> in a non-working database, the non-working database must be created without associating to the current document. When create <strong>AcDbDatabase</strong> object, in the <strong>AcDbDatabase</strong> constructor, you need to set the second parameter, <strong>noDocument</strong> to <strong>true</strong>. <br />· To use the function, <strong>amiGetKeyFromId()</strong> in a non-working database, you have to open a transaction in the non-working database.</p>
