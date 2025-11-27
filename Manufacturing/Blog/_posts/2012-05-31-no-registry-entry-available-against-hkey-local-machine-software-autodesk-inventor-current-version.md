---
layout: "post"
title: "No Registry Entry Available against HKEY_LOCAL_MACHINE_SOFTWARE-&gt;Autodesk-&gt;Inventor-&gt;Current Version"
date: "2012-05-31 21:32:08"
author: "Barbara Han"
categories:
  - "Barbara Han"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/05/no-registry-entry-available-against-hkey_local_machine_software-autodesk-inventor-current-version.html "
typepad_basename: "no-registry-entry-available-against-hkey_local_machine_software-autodesk-inventor-current-version"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/barbara-han.html" target="_self">Barbara Han</a></p>  <p>Beginning from Inventor 2012, we won't write/modify the keys in HKLM because users may not have permission to modify those keys, this is why we make Inventor UAC-friendly. So if your application acquires the executable application, i.e. Inventor.exe or ApprenticeRegsvr.exe, from “HKEY_LOCAL_MACHINE_SOFTWARE-&gt;Autodesk-&gt;Inventor-&gt;Current Version” for Inventor 2012 or afterward releases, it will fail.</p>  <p>However, another register key - HKEY_CURRENT_USER\Software\Autodesk\Inventor\CurrentVersion represents the right data. You can find Inventor.exe from the Executable key, the ApprenticeRegsvr.exe is not there, but it is typically saved in Bin or Bin32 folder under Inventor installation path, and the Executable key points out the Inventor installation path, so you can find the ApprenticeRegsvr.exe easily too.   </p>
