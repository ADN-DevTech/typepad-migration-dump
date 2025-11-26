---
layout: "post"
title: "Timestamp mismatch on Autodesk OAuth"
date: "2015-02-04 03:39:26"
author: "Augusto Goncalves"
categories:
  - "Augusto Goncalves"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/02/timestamp-mismatch-on-autodesk-oauth.html "
typepad_basename: "timestamp-mismatch-on-autodesk-oauth"
typepad_status: "Publish"
---

<p>By Augusto Goncalves</p>
<p>When authenticating against Autodesk servers using your key &amp; secret, you may get the following message on the response content:</p>
<blockquote>
<p><em>xoauth_problem=system_error&amp;xoauth_additional_error_info= timestamp_mismatch&amp;oauth_error_message=This%20 message%20has%20a%20timestamp%20of%201%2F30 %2F2015%204%3A07%3A30%20PM%2C%20which%20is %20beyond%20the%20allowable%20clock%20skew%20for %20in%20the%20future.</em></p>
</blockquote>
<p>Which can be URL decoded into:</p>
<blockquote>
<p><em>xoauth_problem=system_error&amp;xoauth_additional_error_info= timestamp_mismatch&amp;oauth_error_message=This message has a timestamp of 1/30/2015 4:07:30 PM, which is beyond the allowable clock skew for in the future.</em></p>
</blockquote>
<p>Which basically means the clock of your computer (or server) where the code is running is not correct. To fix it, simply adjust the clock.</p>
