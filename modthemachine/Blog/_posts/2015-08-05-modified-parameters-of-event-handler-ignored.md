---
layout: "post"
title: "Modified parameters of event handler ignored"
date: "2015-08-05 20:35:06"
author: "Adam Nagy"
categories:
  - "Adam"
  - "Inventor"
original_url: "https://modthemachine.typepad.com/my_weblog/2015/08/modified-parameters-of-event-handler-ignored.html "
typepad_basename: "modified-parameters-of-event-handler-ignored"
typepad_status: "Publish"
---

<p>A developer just ran into an issue with&#0160;<strong>OnFileResolution</strong> when handled inside an application using <strong>Inventor</strong> <strong>Apprentice</strong>. No matter what value they set the&#0160;<strong>FullFileName</strong> parameter to, it got ignored.</p>
<p><a href="http://modthemachine.typepad.com/my_weblog/wayne-brill.html" target="_self">Wayne</a> already blogged about <a href="http://modthemachine.typepad.com/my_weblog/2012/07/set-embed-interop-types-to-false-to-avoid-problems-with-events.html" target="_self">running into issues with events</a>, but I&#39;ve only experienced it in a way that the event was not caught at all. This time it is caught but the parameter change has no effect. The same trick helps here too though: make sure that <strong>Embed Interop Types</strong> is set to <strong>False</strong>. &#0160;</p>
<p>-Adam&#0160;</p>
