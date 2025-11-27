---
layout: "post"
title: "Deadlock using AcquireFiles() Vault 2014"
date: "2013-05-30 14:55:34"
author: "Wayne Brill"
categories:
  - "Vault"
  - "Wayne Brill"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/05/deadlock-using-acquirefiles-vault-2014.html "
typepad_basename: "deadlock-using-acquirefiles-vault-2014"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/wayne-brill.html" target="_self">Wayne Brill</a></p>  <p>There is a known issue where the AcquireFiles() function can stop responding. (This will be addressed in a future service pack).&#160; </p>  <p>Here is some detail about this issue: The Vault Development Framework (VDF) needs to be initialized and told whether it is operating in the context of a UI application or not (if not specified, it will make the assumption that this *is* a UI application).&#160; During download some of the “file reference update” extension handlers (e.g. Inventor, AutoCAD specific code) will do their work on the main UI thread (if the application is indeed a UI based application).&#160; If the VDF thinks it is operating in the context of a UI application but it is not, (2014 JobProcessor) then you *may* (not always—it depends on whether or not file references need to be updated, and what type of file you are downloading) end up with a deadlock during AcquireFiles.</p>  <p>In 2013, the JobProcessor was one UI application (and thus initializing the VDF as a UI application was appropriate).&#160; In 2014, the JobProcessor has a UI component (JobProcessor.exe) and a non-UI component (Connectivity.JobProcessor.Delegate.Host.exe).&#160; The Connectivity.JobProcessor.Delegate.Host.exe is where the job handlers run.&#160; </p>  <p><strong>Solution:</strong></p>  <p>As early as possible in your Job Handler’s implementation of IJobHandler.Execute, add the following line:    <br /><em>Autodesk.DataManagement.Client.Framework.Library.Initialize(false /* ui Application */);</em></p>  <p><strong>Note</strong>: Console applications should add this line also (Under the right circumstances, it can encounter this same deadlock).</p>
