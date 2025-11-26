---
layout: "post"
title: "AUTOCAD OEM: NMAKE: fatal error U1073"
date: "2017-05-10 21:36:00"
author: "Madhukar Moogala"
categories:
  - "AutoCAD OEM"
original_url: "https://adndevblog.typepad.com/autocad/2017/05/autocad-oem-nmake-fatal-error-u1073.html "
typepad_basename: "autocad-oem-nmake-fatal-error-u1073"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p> <p>&nbsp;</p> <p>This is error is not uncommon to AUTOCAD OEM and it not issue with latest AutoCAD OEM 2018, I do get queries on this error hence I though I will blog.</p> <p>May this error is more surface now because newer operating systems might have disabled 8dot3filename by default.</p> <p>You must enable 8.3 Name Creation on NTFS Partitions to allow the OEM makewizard engine to work with long file names or with spaces and nonstandard characters in the file name during conversion. To do so:</p> <p>You can query for the volume in which your planning to build OEM, if it is enabled or not.  <p>for example :&nbsp; type following example at an elevated command prompt,&nbsp; <p><pre>fsutil 8dot3name query D:</pre>
<p>In my case it is not enabled; 
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8f70ffa970b-pi"><img title="8dot3name-disable" style="border-left-width: 0px; border-right-width: 0px; background-image: none; border-bottom-width: 0px; padding-top: 0px; padding-left: 0px; display: inline; padding-right: 0px; border-top-width: 0px" border="0" alt="8dot3name-disable" src="/assets/image_868042.jpg" width="411" height="109"></a> 
<p>So now we will enable 8dot3file name on our Volume D:, so the command would be<br><pre>fsutil behavior set sisable8dot3 D: 0</pre>

<p>&nbsp;</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8f70ffe970b-pi"><img title="8dot3name-Enable" style="border-left-width: 0px; border-right-width: 0px; background-image: none; border-bottom-width: 0px; padding-top: 0px; padding-left: 0px; display: inline; padding-right: 0px; border-top-width: 0px" border="0" alt="8dot3name-Enable" src="/assets/image_762047.jpg" width="414" height="67"></a></p>Or alternatively you enable on all drives <br><pre>fsutil.exe behavior set disable8dot3 0</pre>
<p>&nbsp; <p>Refer this for more information 
<p><a href="https://technet.microsoft.com/en-us/library/ff621566(v=ws.11).aspx">https://technet.microsoft.com/en-us/library/ff621566(v=ws.11).aspx</a>
