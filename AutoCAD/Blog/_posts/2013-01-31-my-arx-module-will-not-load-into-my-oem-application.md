---
layout: "post"
title: "My ARX Module will not load into my OEM Application"
date: "2013-01-31 16:58:01"
author: "Fenton Webb"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Fenton Webb"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/my-arx-module-will-not-load-into-my-oem-application.html "
typepad_basename: "my-arx-module-will-not-load-into-my-oem-application"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>  <p><b>Issue</b></p>  <p>I have created my ARX module with the OEM libraries, added it to the list in the 'My Modules' tab in the OEM Make Wizard, but when I run the product, this module can't be loaded.&#160; What is wrong?</p>  <p><a name="section2"></a></p>  <p><b>Solution</b></p>  <p>1)&#160; Make certain the ARX is indeed built with the OEM version of ObjectARX SDK included in the Arx subfolder of the OEM installation on your hard drive, and after stamping, make sure it depends on your &lt;prod&gt;.exe file.&#160; Use the dependency walker (Depends.exe) to check this.</p>  <p>2)&#160; Again, make certain the module is added to the list in the 'My Modules' tab in the Make Wizard.&#160; A rebuild of the 'resources' is required when changes are made to the modules list.</p>  <p>3) if you are still stuck, start your OEM product in the Visual Studio debugger – do you see any issues displayed in the Output window? </p>  <p>4) If item 3 gives no hits, download the Windows SDK and utilize GFlags.exe by “Running as Administrator”… Turn “Loader Snaps” on for acad.exe, then retry your debugging session as in (3)</p>  <p>5) Failing that, try loading your ARX application in AOEM.exe to see if that works, if it does, then there is something wrong with your binding process.</p>
