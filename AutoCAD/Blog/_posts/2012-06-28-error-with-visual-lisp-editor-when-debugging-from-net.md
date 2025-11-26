---
layout: "post"
title: "Error with Visual LISP editor when debugging from .NET"
date: "2012-06-28 14:12:38"
author: "Wayne Brill"
categories: []
original_url: "https://adndevblog.typepad.com/autocad/2012/06/error-with-visual-lisp-editor-when-debugging-from-net.html "
typepad_basename: "error-with-visual-lisp-editor-when-debugging-from-net"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/wayne-brill.html" target="_self">Wayne Brill</a></p>  <p>When debugging from a .NET and running the VLIDE an error may occur. A work around is to change a registry setting. (MDA - Managed Debugging Assistants = 0). To set this registry key use a tool like regedit and set the string key &quot;MDA&quot; to 0. </p>  <p>HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\<strong>.</strong>NETFramework&#160;&#160; </p>  <p>If you do not find the MDA string key, create one and set its value to 0. </p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017615e9080d970c-pi"><img style="background-image: none; border-right-width: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top-width: 0px; border-bottom-width: 0px; border-left-width: 0px; padding-top: 0px" title="image" border="0" alt="image" src="/assets/image_97192.jpg" width="473" height="121" /></a></p>  <p>&#160;</p>  <p>You could also create a .reg file and set its contents as follows:</p>  <p>Windows Registry Editor Version 5.00    <br />[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\<strong>.</strong>NETFramework]     <br />&quot;MDA&quot;=&quot;0&quot;</p>
