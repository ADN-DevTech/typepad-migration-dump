---
layout: "post"
title: "AcEdXrefFileLock::consistencyCheckLocal() and performance over network"
date: "2012-09-05 02:16:25"
author: "Philippe Leefsma"
categories:
  - "AutoCAD"
  - "ObjectARX"
  - "Philippe Leefsma"
original_url: "https://adndevblog.typepad.com/autocad/2012/09/acedxreffilelockconsistencychecklocal-and-performance-over-network.html "
typepad_basename: "acedxreffilelockconsistencychecklocal-and-performance-over-network"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/philippe-leefsma.html" target="_self">Philippe Leefsma</a></p>  <p><b>Q:</b></p>    <p>A performance issue was brought to our attention by users on a LAN. Our software calls AcEdXrefFileLock::consistencyChecklocal() frequently, and multiple calls from multiple users appear to be bogging down their LAN. I am trying to find what is causing the performance degradation, as it does not appear to be a problem on all networks. Can you give me any detailed information about how this consistency check is done?</p>    <p><a name="section2"></a></p>  <p><b>A:</b></p>    <p>Here is how AcEdXrefFileLock::consistencyChecklocal() implementation works. Keep in mind that I can't reveal the details of implementation so I'll do it in a descriptive way.</p>  <p>This function is to take a selected xref block table record ID and determine its xref file is still in sync with what were used at drawing load time. It also determines if any other BTRs referencing the same xref file is also in sync. It is done by checking its time/date stamp and file size at load time against the actual ones during the function call time. If it is out of sync, then all the BTR IDs that reference the xref file are returned in an array.</p>  <p>Perhaps the naming of the function creates the confusion of thinking it is only checking files locally. It doesn't mean that. It means it will check selected files versus all files which is done by AcEdXrefFileLock::consistencyCheck().</p>  <p>In general, accessing files over a network versus locally will have performance issues no matter how fast the network is. Network speed comparison of local and network is an obviously reason. Some other issues become a factor as well, such as network connection validation, authentication, data validation, etc.</p>  <p>I think you may want to print some messages at the command line or using some other means of informing the user that something is going to take a while, say when you have a lot of xrefs files that are not local.</p>
