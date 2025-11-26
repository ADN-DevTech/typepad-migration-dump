---
layout: "post"
title: "RevitAPI: PipeScheduleType is missing for a rte file"
date: "2015-05-18 02:46:24"
author: "Aaron Lu"
categories:
  - "Aaron Lu"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2015/05/revitapi-pipescheduletype-is-missing-for-a-rte-file.html "
typepad_basename: "revitapi-pipescheduletype-is-missing-for-a-rte-file"
typepad_status: "Publish"
---

<p><a href="http://blog.csdn.net/lushibi/article/details/45824135">中文链接</a></p>
<p><small>By <a href="http://adndevblog.typepad.com/aec/aaron-lu.html" target="_self">Aaron Lu</a></small></p>
<script src="https://adndevblog.typepad.com/files/run_prettify-3.js" type="text/javascript"></script>
<p>I&#39;ve gotten a strange problem for Revit 2015, in the project file created from a rte file sent from a customer, there is no PipeScheduleType at all,</p>
<p>which means all PipeType.Class is null, even if there are a lot of pipe segments in the document.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c78c1f10970b-pi" style="display: inline;"><img alt="PipeSegment" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c78c1f10970b image-full img-responsive" src="/assets/image_145001.jpg" title="PipeSegment" /></a></p>
<p>After communicating with developer, we confirmed it is a defect caused by file upgrading.</p>
<p>The only solution now is to create a pipe segment by manual, which is not accessible via API because PipeSegment.Create method requires a PipeScheduleId. After the pipe segment is created, a pipe schedule type will be created automatically. To make it work, we should then change pipe segment of pipe type to the new one. Like this:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d115b3c3970c-pi" style="display: inline;"><img alt="PipeSegment-Change" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d115b3c3970c image-full img-responsive" src="/assets/image_493424.jpg" title="PipeSegment-Change" /></a></p>
<p>However, after doing that, PipeType.Class is still null! What&#39;s going on?</p>
<p>&#0160;</p>
<p>Again, developer helped us: PipeType.Class property will be moved to Pipe, so we can use Pipe&#39;s Parameter to get the schedule type, below is how:</p>
<pre class="csharp prettyprint">Autodesk.Revit.DB.Plumbing.Pipe pipe = null;
var pipeClass = pipe.get_Parameter(BuiltInParameter.RBS_PIPE_CLASS_PARAM);
var pipeScheduleTypeId = pipeClass.AsElementId();
</pre>
<p><br /> But how to change PipeSegment of PipeType via API? Here it is:</p>
<pre class="csharp prettyprint">ElementId theSegmentId = new ElementId(1803995);
PipeType pipeType = doc.GetElement(new ElementId(1660690)) as PipeType;
var rpm = pipeType.RoutingPreferenceManager;
var groupType = RoutingPreferenceRuleGroupType.Segments;
RoutingPreferenceRule rule = new RoutingPreferenceRule(theSegmentId, <br />    &quot;description&quot;);
using (Transaction transaction = new Transaction(RevitDoc))
{
    transaction.Start(&quot;Change pipe segment&quot;);
    rpm.AddRule(groupType, rule);
    transaction.Commit();
}
</pre>
<div>Keynotes:</div>
<div>
<ul>
<li>First, get RoutingPreferenceManager from PipeType.RoutingPreferenceManager</li>
<li>Then create a new RoutingPreferenceRule</li>
<li>Last, Add the rule using AddRule method</li>
</ul>
</div>
<p>&#0160;</p>
