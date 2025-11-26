---
layout: "post"
title: "SDI mode deprecated in AutoCAD"
date: "2012-07-20 13:12:00"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "AutoCAD"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/sdi-mode-deprecated-in-autocad.html "
typepad_basename: "sdi-mode-deprecated-in-autocad"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>I&#39;ve just run into the topic <strong>Support MDI Operations</strong> under <strong>ObjectARX Application Interoperability Guidelines &gt; ObjectARX Programming Practices</strong> in the <strong>ObjectARX Reference</strong>, which says:</p>
<p><span style="background-color: #e6e6e6;">ObjectARX applications must support MDI mode. SDI mode has been deprecated and will be removed in a future release.</span></p>
<p>How long can I still use SDI mode?</p>
<p><strong>Solution</strong></p>
<p>SDI mode has been deprecated for quite a few releases now and might get removed in any of the future versions. We cannot tell for sure which exact version that might be.</p>
<p>Therefore, we would encourage you to update your code so that it supports and runs in MDI mode.</p>
