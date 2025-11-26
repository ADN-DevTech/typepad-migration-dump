---
layout: "post"
title: "Add-in not loaded during a journal replay"
date: "2012-05-16 12:25:14"
author: "Mikako Harada"
categories:
  - "Mikako Harada"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2012/05/add-in-not-loaded-during-a-journal-replay.html "
typepad_basename: "add-in-not-loaded-during-a-journal-replay"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/mikako-harada.html" target="_self" title="Mikako Harada">Mikako Harada</a></p>
<p>If you are trying to use the journal replay feature of Revit for Automatic Quality Assurance of your add-ins, you may notice that the&#0160;add-ins using the manifest mechanism for registration won&#39;t get loaded when replaying a journal file. However, add-ins that are registered in the Revit.ini do get loaded during playback.&#0160;</p>
<p>This is as designed.&#0160;&#0160;Revit does not&#0160;look at the Addins folder during journal replay.&#0160; This is on purpose: it would interfere with testing if the registered add-ins on every developer/QA machine could affect results.</p>
<p>Revit does&#0160;look to the local folder where the journal is stored.&#0160; If we construct a test with a journal, an .addin, and other things (models, etc.) all in the same folder, it should serve the purpose.</p>
