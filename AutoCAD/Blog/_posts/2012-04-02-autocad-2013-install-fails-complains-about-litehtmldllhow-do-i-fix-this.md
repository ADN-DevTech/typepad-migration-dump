---
layout: "post"
title: "AutoCAD 2013 install fails, complains about LiteHtml.dll&ndash;How do I fix this?"
date: "2012-04-02 11:08:04"
author: "Gopinath Taget"
categories:
  - "2013"
  - "AutoCAD"
  - "Gopinath Taget"
original_url: "https://adndevblog.typepad.com/autocad/2012/04/autocad-2013-install-fails-complains-about-litehtmldllhow-do-i-fix-this.html "
typepad_basename: "autocad-2013-install-fails-complains-about-litehtmldllhow-do-i-fix-this"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/gopinath-taget.html" target="_self">Gopinath Taget</a></p>
<p>This was a problem reported by an an ADN Developer. The solution was also provided by the ADN developer:</p>
<p>If system administrators implement the policy described by Microsoft in <a href="http://support.microsoft.com/kb/2264107">http://support.microsoft.com/kb/2264107</a> and set CWDIllegalInDllSearch to FFFFFFFF, the AutoCAD 2013 installer will fail being unable to load LiteHtml.dll</p>
<p>You need to detect the bad registry setting and warn people that they need to undo it.</p>
<p><strong><em>The following .reg file causes the problem:</em></strong></p>
<p>Windows Registry Editor Version 5.00 <br />[HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager] <br />&quot;CWDIllegalInDllSearch&quot;=dword:FFFFFFFF</p>
<p><strong><em>This following .reg file fixes it:</em></strong></p>
<p>Windows Registry Editor Version 5.00 <br />[HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager] <br />&quot;CWDIllegalInDllSearch&quot;=dword:00000000</p>
