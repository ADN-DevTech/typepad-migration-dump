---
layout: "post"
title: "Component occurrence cannot be renamed"
date: "2015-01-22 06:39:51"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/01/component-occurrence-cannot-be-renamed.html "
typepad_basename: "component-occurrence-cannot-be-renamed"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html">Adam Nagy</a></p>
<p>In case of some assemblies, e.g. the ones created by <strong>Design Accelerator</strong>, the name of the document or the occurrances in it cannot be changed: you get message &quot;<strong>Request Rename Component cannot be run on document Synchronous Belts</strong>&quot;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb07e01b82970d-pi" style="display: inline;"><img alt="Changename" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb07e01b82970d image-full img-responsive" src="/assets/image_451e01.jpg" title="Changename" /></a></p>
<p><strong>Design Accelerator</strong> does set the <strong>Document.DisabledCommandTypes</strong> to <strong>kNonShapeEditCmdType + kShapeEditCmdType</strong>, which is <strong>32</strong> + <strong>1</strong> = <strong>33</strong>, and<strong>&#0160;</strong>that seems to be the thing that disables those types of changes.</p>
<p>If you test the effect of the various <strong>CommandTypesEnum</strong>&#0160;values concerning the above changes then this is what you get:</p>
<pre>Public Sub SetDisabledCommandTypes()
&#39; Type / Can you rename occurrences? / Can you rename document?
<strong>&#39; kEditMaskCmdType = 57 (&amp;H39) / No / No</strong>
&#39; - <strong>kNonShapeEditCmdType (32)</strong>
&#39; - kUpdateWithReferencesCmdType (16)
&#39; - kFilePropertyEditCmdType (8)
&#39; - kShapeEditCmdType (1)
&#39; kFileOperationsCmdType = 4 / Yes / Yes
&#39; kFilePropertyEditCmdType = 8 / Yes / Yes
<strong>&#39; kNonShapeEditCmdType = 32 (&amp;H20) / No / No</strong>
&#39; kQueryOnlyCmdType = 2 / Yes / Yes
&#39; kReferencesChangeCmdType = 64 (&amp;H40) / Yes / Yes
&#39; kSchemaChangeCmdType = 128 (&amp;H80) / Yes / Yes
&#39; kShapeEditCmdType = 1 / Yes / Yes
&#39; kUpdateWithReferencesCmdType = 16 (&amp;H10) / Yes / Yes

  &#39; You can test the effect of the various enum values here
  ThisApplication.ActiveDocument.DisabledCommandTypes = _
    kNonShapeEditCmdType
End Sub</pre>
<p>If you remove the <strong>kNonShapeEditCmdType</strong>&#0160;flag from the <strong>DisabledCommandTypes</strong>&#0160;property of an assembly document (e.g. by setting it to <strong>0</strong>) then you could rename the document and its occurrences. I do not suggest that you do that though; it&#39;s just to test that that is the only setting that prevents you from doing those changes in a <strong>Design Accelerator</strong> assembly.</p>
