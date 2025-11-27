---
layout: "post"
title: "Updated version of WinRT Apollonian Viewer for Windows 8 RTM"
date: "2012-10-25 09:58:30"
author: "Kean Walmsley"
categories:
  - "Mobile"
  - "SaaS"
  - "WinRT"
original_url: "https://www.keanw.com/2012/10/updated-version-of-winrt-apollonian-viewer-for-windows-8-rtm.html "
typepad_basename: "updated-version-of-winrt-apollonian-viewer-for-windows-8-rtm"
typepad_status: "Publish"
---

<p>As part of my preparation for AU 2012, I’ve been working on updating the various Windows 8 samples I’ll be showing to work with the RTM version of the OS. The first to be migrated was the Apollonian Viewer for WinRT, which is part of <a href="http://through-the-interface.typepad.com/through_the_interface/2012/06/cloud-mobile-series-summary.html" target="_blank">the cloud &amp; mobile series</a> from earlier in the year.</p>  <p>The project was easy enough to get working: I mostly had to update to version 2.3.0 (or <a href="http://sharpdx.org/news/new-version-2-3-1" target="_blank">2.3.1</a>, as this was the latest available) of <a href="http://sharpdx.org/" target="_blank">SharpDX</a> and made a few minor code changes (there was previously a SafeDispose() function that seems to have disappeared and a Colors class has been renamed Color) on top of the changes that were made in <a href="http://through-the-interface.typepad.com/through_the_interface/2012/05/creating-a-3d-viewer-for-our-apollonian-service-using-winrt-part-2.html" target="_blank">my update to the original post</a>.</p>  <p>Here’s <a href="http://through-the-interface.typepad.com/files/ApollonianViewerWinRT4.zip" target="_blank">the updated source project</a>.</p>  <p>And here’s an animated GIF (which doesn’t look quite as nice or smooth as it does in reality, but hey) of the viewer in action:</p>  <p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2017d3cf8ece0970c-pi"><img style="background-image: none; border-right-width: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top-width: 0px; border-bottom-width: 0px; border-left-width: 0px; padding-top: 0px" title="Apollonian Viewer on WinRT" border="0" alt="Apollonian Viewer on WinRT" src="/assets/image_457992.jpg" width="470" height="352" /></a></p>
