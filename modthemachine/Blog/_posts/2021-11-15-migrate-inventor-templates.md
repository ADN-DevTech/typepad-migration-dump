---
layout: "post"
title: "Migrate Inventor templates"
date: "2021-11-15 21:27:19"
author: "Adam Nagy"
categories:
  - "Adam"
  - "Drawings"
  - "Inventor"
original_url: "https://modthemachine.typepad.com/my_weblog/2021/11/migrate-inventor-templates.html "
typepad_basename: "migrate-inventor-templates"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc68834026bdf00780f200c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="MigrateTemplate" class="asset  asset-image at-xid-6a00e553fcbfc68834026bdf00780f200c img-responsive" src="/assets/image_292292.jpg" title="MigrateTemplate" /></a></p>
<p>There is a difference between using a&#0160;<strong>document</strong>&#0160;as a&#0160;<strong>template</strong>&#0160;or just simply&#0160;<strong>opening</strong>&#0160;it. Especially in the case of&#0160;<strong>drawing templates</strong>.</p>
<p><strong>Inventor</strong>&#0160;can open&#0160;<strong>older documents&#0160;</strong>created in&#0160;<strong>previous versions&#0160;</strong>of the software and then save them. That&#39;s what&#0160;<strong>document migration&#0160;</strong>is: opening an old document and saving it in the latest version of the software. You&#0160;can find more information about it&#0160;<a href="https://help.autodesk.com/view/INVNTOR/2022/ENU/?guid=GUID-6DEACF61-4350-4874-B6E9-5720C48AED2A">here</a>.</p>
<p><strong>Inventor</strong>&#0160;documents have a property called&#0160;<a href="https://help.autodesk.com/view/INVNTOR/2022/ENU/?guid=GUID-F92A4B9B-33C4-4F16-AA92-33F663753ACF">NeedsMigrating</a>, but it only tells you if the document&#0160;<strong>needs to be saved before modifying</strong>&#0160;its content. It does not say if it can be&#0160;<strong>used as a template</strong>&#0160;or not.</p>
<p>Just to be on the safe side, the best thing is to migrate an older document before trying to use&#0160;it as a template - especially a&#0160;<strong>drawing template</strong>.&#0160;</p>
<p>You can do all this in advance e.g. using&#0160;<a href="https://help.autodesk.com/view/INVNTOR/2022/ENU/?guid=GUID-9D665936-6837-4698-9B83-4CCD16FF8383">Task Scheduler</a>, or&#0160;<strong>just-in-time&#0160;</strong>as well - here&#39;s how.</p>
<p>You can check programmatically the&#0160;<strong>Inventor</strong>&#0160;version the drawing was last saved in. If it has&#0160;<strong>not been migrated</strong>&#0160;yet then you can just&#0160;<strong>open</strong>,&#0160;<strong>save</strong>&#0160;and&#0160;<strong>close</strong>&#0160;it before using it as a&#0160;<strong>template</strong>.</p>
<p>Here is a&#0160;<strong>VBA</strong>&#0160;sample:</p>
<pre>Sub UseTemplate()
  Const templatePath = &quot;C:\Temp\Template2021\Standard.idw&quot;<br /><br />  Dim templateVersion As SoftwareVersion<br />  Set templateVersion = ThisApplication.FileManager.SoftwareVersionSaved(templatePath)<br /><br />  Dim inventorVersion As SoftwareVersion<br />  Set inventorVersion = ThisApplication.SoftwareVersion<br /><br />  If inventorVersion.Major &gt; templateVersion.Major Then<br />    &#39; Migrate drawing template before using it<br />    Dim t As DrawingDocument<br />    Set t = ThisApplication.Documents.Open(templatePath, False)<br /><br />    t.Save<br /><br />    t.Close<br />  End If<br /><br />  Dim doc As Document<br />  Set doc = ThisApplication.Documents.Add(kDrawingDocumentObject, templatePath, True)
End Sub</pre>
