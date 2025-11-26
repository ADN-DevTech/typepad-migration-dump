---
layout: "post"
title: "OAuth Server Maintenance"
date: "2016-07-15 09:23:36"
author: "Madhukar Moogala"
categories:
  - "Announcements"
  - "Forge"
  - "Stephen Preston"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/07/oauth-server-maintenance.html "
typepad_basename: "oauth-server-maintenance"
typepad_status: "Publish"
---

<p style="text-align: justify;">By <a href="http://adndevblog.typepad.com/cloud_and_mobile/stephen-preston/">Stephen Preston</a></p>
<p style="text-align: justify;">(Updated with maintenance date).</p>
<p style="text-align: justify;">We plan to apply an update to the Forge OAuth server on July 30th 2016 (at approximately 4pm PDT). When the upgrade takes place, all 2-legged Access Tokens will be invalidated and will have to be refreshed. In addition, the duration for 2-legged Access Tokens will be temporarily reduced from 24 hours to 30 minutes the day before the upgrade (and reinstated back to 24 hours afterwards).</p>
<p style="text-align: justify;">If you have correctly implemented seamless Access Token refreshing in your code (as you should have done :-) ), then you don&#39;t need to take any action. However, if you have not, or if you are making an assumption that the Access Token duration is always 24 hours, then you should update your application now to prevent it failing.</p>
<p style="text-align: justify;">3-legged OAuth is not affected by this update.</p>
