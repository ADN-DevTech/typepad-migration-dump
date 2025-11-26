---
layout: "post"
title: "RevitAPI: ScheduleDefinition.GetSchedulableFields() returns more fields than UI"
date: "2015-04-14 00:28:43"
author: "Aaron Lu"
categories:
  - "Aaron Lu"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2015/04/revitapi-scheduledefinitiongetschedulablefields-returns-more-fields-than-ui.html "
typepad_basename: "revitapi-scheduledefinitiongetschedulablefields-returns-more-fields-than-ui"
typepad_status: "Publish"
---

<p><a href="http://blog.csdn.net/lushibi/article/details/44753977">中文链接</a></p>
<p><small>By <a href="http://adndevblog.typepad.com/aec/aaron-lu.html" target="_self">Aaron Lu</a></small></p>
<script src="https://adndevblog.typepad.com/files/run_prettify-3.js" type="text/javascript"></script>
<p>In Revit, when we create schedule, Revit will provide a list of fields for us to choose,</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c773f488970b-pi" style="display: inline;"><img alt="ScheduleDefinition.GetSchedulableFields-PipeSchedule-EN" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c773f488970b image-full img-responsive" src="/assets/image_252161.jpg" title="ScheduleDefinition.GetSchedulableFields-PipeSchedule-EN" /></a></p>
<p>The corresponding way to get a list of fields in API is using method ViewSchedule.ScheduleDefinition.GetSchedulableFields(), but somehow it returns more fields than we see in the UI, what are the invisible guys?</p>
<p>It turns out that in the UI, besides the visible fields, we can also add shared or project parameter as field by clicking &quot;Add Parameter&quot;, which is why there are more fields return by API.</p>
<p>So the question is: how to know those fields are invisible in the list in UI?</p>
<p>Answer: by checking the ParameterId of SchedulableField is greater than 0 or not, if it is greater, then it is shared or project parameter.</p>
<pre class="csharp prettyprint">var fields = viewSchedule.Definition.GetSchedulableFields();
foreach (var field in fields)
{
    if (field.ParameterId.IntegerValue &lt; 0)
    {
        // parameter visible in the UI
    }
    else
    {
        // shared or project parameter
    }
}</pre>
<p>&#0160;</p>
