---
layout: "post"
title: "ViewSchedule.Export method"
date: "2012-06-01 01:12:31"
author: "Katsuaki Takamizawa"
categories:
  - "Katsuaki Takamizawa"
original_url: "https://adndevblog.typepad.com/aec/2012/06/viewscheduleexport-method.html "
typepad_basename: "viewscheduleexport-method"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/katsuaki-takamizawa.html" target="_self">Katsuaki Takamizawa</a></p>
<p>The API access to schedule tables is still limited. But ViewSchedule.Export method is added in Revit 2013. It would be usual to know the method is available.</p>
<p><span style="color: #0000bf;">public void </span>Export(<br />&#0160;<span style="color: #0000bf;">string </span>folder,<br />&#0160;<span style="color: #0000bf;">string </span>name,<br />&#0160;ViewScheduleExportOptions options)</p>
<p>You can create ViewScheduleExportOptions object to specify how to export column headers, delimit fields such as Comma, or Tab (default) and qualify text fields.</p>
<p>And here is a link for the extensive discussion of this topic:</p>
<p><a href="http://thebuildingcoder.typepad.com/blog/2012/05/the-schedule-api-and-access-to-schedule-data.html">http://thebuildingcoder.typepad.com/blog/2012/05/the-schedule-api-and-access-to-schedule-data.html</a></p>
<p>&#0160;</p>
