---
layout: "post"
title: "Maya Profiler"
date: "2014-10-16 08:44:27"
author: "Naiqi Weng"
categories:
  - "C++"
  - "Debugging"
  - "Maya"
  - "Naiqi Weng"
  - "Plug-in"
  - "Tools"
original_url: "https://around-the-corner.typepad.com/adn/2014/10/maya-profiler.html "
typepad_basename: "maya-profiler"
typepad_status: "Publish"
---

<p>Have you ever wonder why your scene is running so slow? What’s the culprit of the performance degradation? In Maya 2015 extension, we provide a new profiler tool for you to track down performance bottlenecks in your scene by recording the amount of time that each process consumes.</p>
<p><a class="asset-img-link" href="http://player.ooyala.com/iframe.html#pbid=6055f5a2061d4016b11ebf1fa8a7751e&amp;ec=VnbGZrbzqnDJrWSwLfFcjIyKxNeQ-puo&amp;docUrl=http%3A%2F%2Fplayer.ooyala.com%2Fiframe.js%23pbid%3D6055f5a2061d4016b11ebf1fa8a7751e%26ec%3DVnbGZrbzqnDJrWSwLfFcjIyKxNeQ-pu" style="display: inline;" target="mel-profiler"><img alt="Profiler" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d01bb07a4c12b970d image-full img-responsive" src="/assets/image_6badf3.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Profiler" /></a></p>
<p>We also provide programming interfaces to access this feature. Specifically, in MEL/Python, you can use command “profiler” to start and stop the recording of events, add profiling categories and instrument procedures for profiling.&#0160; &#0160;</p>
<p>Furthermore, two new classes,&#0160;MProfiler&#0160;and&#0160;MProfilingScope, have been added that allow you to work with Maya&#39;s&#0160;Profiler. MProfiler&#0160;is a static class which provides access to profiler settings and the profiling result. MProfilingScope&#0160;is used to profile code execution time. Profiling begins with the creation of an MProfilingScope&#0160;instance and ends when the instance is destroyed (e.g. when it goes out of scope at the end of the block in which it was declared). For example, if you want to profile any code snippet, you can instrument it like this:</p>
<p>{</p>
<p>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; MProfilingScope&#0160;profilingScope(testCategory, MProfiler::kColorD_L1, &quot;eventName&quot;, &quot;eventDescription&quot;);</p>
<p>&#0160;&#0160;&#0160;&#0160;&#0160; // The code snippet you want to profile.</p>
<p>}</p>
<p>For more information, you can check out the&#0160;MProfiler&#0160;and MProfilingScope&#0160;class documentation in Maya help online.</p>
<p>I image this would be very beneficial to TDs and sometimes plug-in developers. As this will give you a better idea of which part of evaluation is the bottleneck and help debug problems in your evaluation of Dependency Graph. So happy debugging!</p>
<p>&#0160;</p>
