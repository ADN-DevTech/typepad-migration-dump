---
layout: "post"
title: "Forge API outage"
date: "2016-12-07 12:01:30"
author: "Madhukar Moogala"
categories:
  - "Announcements"
  - "Forge"
  - "Stephen Preston"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/12/data-management-outage.html "
typepad_basename: "data-management-outage"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/cloud_and_mobile/stephen-preston/">Stephen Preston</a> (@_stephenpreston)</p>
<p>(Update 12/8/16 4.40pm Pacific Time)</p>
<p>We wanted to provide you with some additional information about this recent outage of the Forge Data Management and Model Derivative APIs.&#0160; These services began experiencing slowdowns around 8pm PST on Tues 12/6. The service degradation gradually worsened, until, by 1am PST Wed 12/7 we decided that it was necessary to take the service off line in order to take remedial action.</p>
<p><br />The affected service was an internal system that controls access permissions to data in our storage layer. Without this service running, our Data Management and Model Derivative APIs were unable to store new data or access existing data. While addressing this issue, our priority was to take extreme care to ensure that no customer data was lost - even though this resulted in a longer resolution time.</p>
<p><br />By approximately 1am PST Thurs 12/8 we had fixed the problem, restored access to customer data and started processing a backlog of transactions. We continue to monitor this service and are confident the code changes we made will prevent this problem from happening again. As a result of this incident, we have also identified areas where we can improve our internal processed and how we can better communicate with you when we are having problems.&#0160; One area we will be prioritizing is to enhance our <a href="https://developer.autodesk.com/en/support/api-status" rel="noopener noreferrer" target="_blank">API Health Dashboard</a> to make it easier for you understand the health status of our APIs, and to receive more detailed information about any issues we are having. &#0160;</p>
<p>The Forge Team</p>
<p>&#0160;</p>
<p>(Update 12/8/16 7am Pacific Time)</p>
<p>The Forge Data Management and Model Derivative APIs are now back online, including access to data via the API for Fusion Team, A360 and BIM 360 Docs. The Forge team apologizes for this outage and any problems it has caused for you and your customers.<br /><br />We are now investigating all aspects of this outage to prevent it from happening in the future.</p>
<p>&#0160;</p>
<p>(Posted 12/7/16 12pm Pacific Time).</p>
<p>The Forge Data Management and Model Derivative APIs are currently unavailable.&#0160; The Forge team apologizes for the current outage and any problems it has caused for you and your customers. Our team is working diligently on the problem and will fix it as soon as possible.&#0160;</p>
<p>The outage was caused by a problem in our service that controls secure access to data that is stored in our storage services. This impacts access to data via Forge Data Management API and the Model Derivative API.&#0160; This outage effects files created to view, translated to another format or for access to model meta data.&#0160; Access to data via the API for Fusion Team, A360 and BIM 360 Docs is also effected.&#0160; We will notify you as soon as the problem is fixed, and will follow up with information on what we are doing to prevent it from happening in the future. &#0160;<br /><br />The Forge Team</p>
