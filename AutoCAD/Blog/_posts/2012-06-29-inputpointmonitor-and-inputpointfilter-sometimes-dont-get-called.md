---
layout: "post"
title: "Inputpointmonitor and Inputpointfilter sometimes don't get called"
date: "2012-06-29 17:00:23"
author: "Gopinath Taget"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Gopinath Taget"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/06/inputpointmonitor-and-inputpointfilter-sometimes-dont-get-called.html "
typepad_basename: "inputpointmonitor-and-inputpointfilter-sometimes-dont-get-called"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/gopinath-taget.html">Gopinath Taget</a></p>
<p>If you uncheck the &quot;Display Hyperlink Cursor and Shortcut menu&quot; item on the <strong>User Preferences</strong> page of the <strong>Options</strong> dialog, your input point monitors and filters might not receive notifications. How can you force them to be called in this situation?</p>
<p>To work around this problem, you will have to invoke the AcEdInputPointManager::turnOnForcedPick() method of your global input point manager. This is demonstrated in the cmdForcedPickOn() function of the inputpoint sample in the ObjectARX SDK.</p>
