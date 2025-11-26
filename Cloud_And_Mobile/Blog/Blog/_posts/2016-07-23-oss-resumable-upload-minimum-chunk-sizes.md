---
layout: "post"
title: "OSS Resumable Upload - Minimum Chunk Sizes."
date: "2016-07-23 14:40:57"
author: "Cyrille Fauvel"
categories:
  - "Announcements"
  - "Cyrille Fauvel"
  - "Forge"
  - "Server"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/07/oss-resumable-upload-minimum-chunk-sizes.html "
typepad_basename: "oss-resumable-upload-minimum-chunk-sizes"
typepad_status: "Publish"
---

<p>The minimum chunk sizes recommendation for the OSS resumable endpoint(s) is a recommendation is 5MB (&#0160;and in certain cases, maybe adjusted to be no lower than 2MB in chunk size ).</p>
<p>However, we were not enforcing until now. We will be making changes to OSS to enforce a minimum chunk size ( 2MB chunks ) but we are still recommending a 5MB or greater chunk size.</p>
<p>Only the last chunk (identified by range covering the last byte in the total bytes of the object) will be allowed to be smaller than the 2MB limit.</p>
