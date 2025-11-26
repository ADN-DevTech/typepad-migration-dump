---
layout: "post"
title: "RevitAPI: How to get the available parameters used in ScheduleFilter for a specific category?"
date: "2015-01-29 00:25:47"
author: "Aaron Lu"
categories:
  - "Aaron Lu"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2015/01/revitapi-how-to-know-the-parameters-used-in-schedulefilter-for-a-specific-category.html "
typepad_basename: "revitapi-how-to-know-the-parameters-used-in-schedulefilter-for-a-specific-category"
typepad_status: "Publish"
---

<p><a href="http://blog.csdn.net/lushibi/article/details/43272137">中文链接</a></p>
<p><small>By <a href="http://adndevblog.typepad.com/aec/aaron-lu.html" target="_self">Aaron Lu</a></small></p>
<script src="https://adndevblog.typepad.com/files/run_prettify-3.js" type="text/javascript"></script>
<p>In Revit we can create schedule with schedule filter so that only the elements passed the filter will be shown in the schedule. e.g. I can let the schedule only show the elements on &quot;level 1&quot;.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb07e4ec49970d-pi" style="display: inline;"><img alt="S1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb07e4ec49970d img-responsive" src="/assets/image_484979.jpg" title="S1" /> </a><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0cacb47970c-pi" style="display: inline;"><img alt="S2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d0cacb47970c image-full img-responsive" src="/assets/image_55373.jpg" title="S2" /></a><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb07e4ec49970d-pi" style="display: inline;"><br /></a></p>
<p>From the above screenshots, we can see that the parameters available for filtering varies with the category we choose, so is there any way to get all the parameters that can be filterable for a specific category?</p>
<p>The answer is Yes, we can make use of <span style="color: #a94a76;">TableView.GetAvailableParameters</span> method, which is mainly used to get the parameters can be schedulable for specific category, as long as the parameter (built-in parameter) is schedulable, it can be filtered. So the example can be:</p>
<pre class="csharp prettyprint">var availableParameterIds = TableView.GetAvailableParameters
    (RevitDoc, new ElementId(BuiltInCategory.OST_DuctTerminal));
foreach (var pid in availableParameterIds)
{
    var builtinParameter = (BuiltInParameter)pid.IntegerValue;
    //work with the BuiltInParameter
}
</pre>
<p>&#0160;</p>
