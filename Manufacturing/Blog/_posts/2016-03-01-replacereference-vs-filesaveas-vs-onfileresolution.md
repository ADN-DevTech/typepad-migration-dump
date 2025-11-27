---
layout: "post"
title: "ReplaceReference vs FileSaveAs vs OnFileResolution"
date: "2016-03-01 04:16:16"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2016/03/replacereference-vs-filesaveas-vs-onfileresolution.html "
typepad_basename: "replacereference-vs-filesaveas-vs-onfileresolution"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>There are multiple ways of dealing with moving and renaming <strong>Inventor</strong> documents and updating the references to them; so that all the relevant files can be found when your design is opened in <strong>Inventor</strong>.</p>
<p>1) <strong>ReplaceReference</strong></p>
<p>Available in <strong>Inventor</strong> and <strong>Apprentice</strong> &#0160;</p>
<p>This requires that the file originally referenced will have the same ancestry as the new file the reference is being changed to. So the files do not have to be exactly the same: the new file could be a modified version of the original.</p>
<p><strong>Note</strong>: This does not seems to work well in all scenarios. E.g. in case of special assembly components like <strong>Cable &amp; Harness</strong> assembly&#39;s part component, it won&#39;t work. Also, inside&#0160;<strong>Apprentice</strong> you cannot save a file that needs migration (ApprenticeServerDocument.<strong>NeedsMigrating</strong>&#0160;= true)</p>
<p>2) <strong>FileSaveAs</strong></p>
<p>Available in <strong>Apprentice&#0160;</strong>only</p>
<p>This functionality helps you change the name and path of a file and also updates&#0160;all the references to it. Obviously, in this case the original file and the newly saved file will be exactly the same. Maybe that&#39;s why it&#39;s more robust and seems to work with any files. &#0160;</p>
<p>The &quot;<strong>Copy Design</strong>&quot; sample of the <strong>Inventor</strong>&#0160;<strong>SDK</strong> shows how to use it: &quot;C:\Users\Public\Documents\Autodesk\Inventor 2016\SDK\UserTools\CopyDesign&quot; (you have to install &quot;<strong>usertools.msi</strong>&quot; first)&#0160;</p>
<p><strong>Note</strong>: if the file needs migration (ApprenticeServerDocument.<strong>NeedsMigrating</strong>&#0160;= true) then the <strong>FileSaveAs</strong> approach won&#39;t work. Also read&#0160;<a href="http://adndevblog.typepad.com/manufacturing/2016/03/using-apprentice-from-vba-ilogic-or-an-add-in.html">Using Apprentice from VBA, iLogic or an Add-In</a>&#0160;</p>
<p>3) <strong>OnFileResolution</strong></p>
<p>Available in <strong>Inventor&#0160;</strong>only</p>
<p>If&#0160;<strong>Inventor</strong> cannot find a file when your document is opened then it will pop up a &quot;<strong>Resolve Link</strong>&quot; dialog.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08c10846970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="ResolveLink" class="asset  asset-image at-xid-6a0167607c2431970b01bb08c10846970d img-responsive" src="/assets/image_86d6fc.jpg" title="ResolveLink" /></a></p>
<p>Its <strong>API</strong> equivalent is the call to the&#0160;<strong>OnFileResolution</strong> event of the <strong>FileAccessEvents</strong> object. This event fires for all the files that <strong>Inventor</strong> needs, even if the referenced files were not moved or renamed. This also seems to work for all documents.&#0160;</p>
