---
layout: "post"
title: "Wait for oemmakewizard.exe to finish when used from a batch file"
date: "2012-07-31 06:28:00"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "AutoCAD OEM"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/wait-for-oemmakewizardexe-to-finish-when-used-from-a-batch-file.html "
typepad_basename: "wait-for-oemmakewizardexe-to-finish-when-used-from-a-batch-file"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>I migrated my project from OEM 2008 to 2011 and now my batch file that builds these projects do not work properly. In case of OEM 2008 when I started the wizard the execution in the batch file stopped and only continued once the OEM Make Wizard finished. This is important for me because in the batch file I want to further process the built project. However with OEM 2011 the execution continues and does not wait for the Wizard to finish, so the additional parts in the batch file fail because the project is not created yet. I also tried using the START /WAIT function in the batch file but that did not help either. What could I do?</p>
<p><strong>Solution</strong></p>
<p>It does seem to be the case that when calling oemmakewizard.exe from a batch file the execution continues straight away.</p>
<p>One solution would be to keep checking the oemmakewizard.exe process among the running tasks, and only continue once it finished. This can be done using batch commands. To poll the system less about the list of runing processes we can wait a second in the loop. Since Windows does not have a SLEEP function we would need to use something else, e.g. CHOICE or PING</p>
<p><span style="background-color: #e6e6e6;">@echo off</span><br /> <br /><span style="background-color: #e6e6e6;"> echo.Starting build</span><br /> <br /><span style="background-color: #e6e6e6;"> &quot;C:\Program Files\Autodesk\AutoCAD OEM 2011\Toolkit\oemmakewizard.exe&quot; OemTest 013260521 /BALL</span><br /> <br /><span style="background-color: #e6e6e6;"> :wait</span><br /><span style="background-color: #e6e6e6;"> choice /t 1 /d Y &gt; nul:</span><br /><span style="background-color: #e6e6e6;"> tasklist /FI &quot;IMAGENAME eq oemmakewizard.exe&quot; | find /I &quot;oemmakewizard.exe&quot; &gt; nul:</span><br /><span style="background-color: #e6e6e6;"> if not errorlevel 1 goto wait</span><br /> <br /><span style="background-color: #e6e6e6;"> echo.Build finished</span></p>
