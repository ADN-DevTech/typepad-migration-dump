---
layout: "post"
title: "How to make the plug-in commands not available in .NET "
date: "2016-05-30 04:03:06"
author: "Virupaksha Aithal"
categories:
  - ".NET"
  - "2010"
  - "AutoCAD"
  - "Virupaksha Aithal"
original_url: "https://adndevblog.typepad.com/autocad/2016/05/how-to-make-the-plug-in-commands-not-available-in-net-.html "
typepad_basename: "how-to-make-the-plug-in-commands-not-available-in-net-"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/virupaksha-aithal.html" target="_self">Virupaksha Aithal</a></p>
<p><strong>Question</strong>: Recently I received a question on how to avoid exposing commands in the custom plug-in when the license verification fails.<br /><strong>Answer</strong>: One of the procedure to achieve this requirement is to do license verification in IExtensionApplication.Initialize() and throwing an exception if license verification fails. After throwing exception, none of the commands in plug-in will be available for the user.</p>
<pre>void IExtensionApplication.Initialize()
{
    //your check...

    //throw LoadFailed error...
    throw new Autodesk.AutoCAD.Runtime.Exception(
        Autodesk.AutoCAD.Runtime.ErrorStatus.LoadFailed);
}
</pre>
