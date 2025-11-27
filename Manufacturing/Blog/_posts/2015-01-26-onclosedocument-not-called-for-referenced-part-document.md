---
layout: "post"
title: "OnCloseDocument not called for referenced part document"
date: "2015-01-26 17:24:30"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/01/onclosedocument-not-called-for-referenced-part-document.html "
typepad_basename: "onclosedocument-not-called-for-referenced-part-document"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html">Adam Nagy</a></p>
<p>If you have a part document open on it&#39;s own and a currently open assembly references it too, then if you close the part document in the UI then OnCoseDocument is not called.</p>
<p>This is pointed out in the API help file as well under the <strong>Remarks</strong> section of <strong>ApplicationEvents.OnCloseDocument Event</strong>:</p>
<p><span style="color: #0000bf;">This means if the file is referenced from an assembly, OnOpenDocument will not fire if a loaded file referenced in an openassembly is opened again, as internally this file is already open. Conversely, <strong>if a document is closed, the document is not really closed unless that was the final view and the document is not referenced by any other open documents.</strong></span></p>
<p>If you want to investigate when various events get fired, you can use &quot;C:\Users\Public\Documents\Autodesk\Inventor 2015\SDK\DeveloperTools\Tools\<strong>EventWatcher</strong>&quot; sample project.</p>
