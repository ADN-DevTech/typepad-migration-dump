---
layout: "post"
title: "View Template Include Setting"
date: "2018-11-01 06:00:00"
author: "Jeremy Tammik"
categories:
  - "Data Access"
  - "Element Relationships"
  - "Settings"
  - "View"
original_url: "https://thebuildingcoder.typepad.com/blog/2018/11/view-template-include-setting.html "
typepad_basename: "view-template-include-setting"
typepad_status: "Publish"
---

<p>A quick note to highlight a solution shared by Teocomi to solve a longstanding question in
the <a href="https://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
on <a href="https://forums.autodesk.com/t5/revit-api/view-template-quot-include-quot/m-p/5410347">view template 'include'</a>:</p>

<p><strong>Question:</strong> Does the Revit API provide any access to the view template 'include' settings defined by the check boxes in this form?</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833022ad39d3fb0200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833022ad39d3fb0200d image-full img-responsive" alt="View template include checkboxes" title="View template include checkboxes" src="/assets/image_73a7d6.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p><strong>Answer:</strong> I can get the 'includes' via <code>viewTemplate.GetNonControlledTemplateParameterIds</code>.</p>

<p>The method returns a list of parameter ids, and you can then use <code>viewTemplate.Parameters</code> to map them.</p>

<p>The same also works for setting them, cf. the following example:</p>

<pre class="code">
&nbsp;&nbsp;<span style="color:green;">//&nbsp;Create&nbsp;a&nbsp;list&nbsp;so&nbsp;that&nbsp;I&nbsp;can&nbsp;use&nbsp;linq</span>

&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;viewparams&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">Parameter</span>&gt;();
&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">Parameter</span>&nbsp;p&nbsp;<span style="color:blue;">in</span>&nbsp;viewTemplate.Parameters&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;viewparams.Add(&nbsp;p&nbsp;);

&nbsp;&nbsp;<span style="color:green;">//&nbsp;Get&nbsp;parameters&nbsp;by&nbsp;name&nbsp;(safety&nbsp;checks&nbsp;needed)</span>

&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;modelOverrideParam&nbsp;=&nbsp;viewparams
&nbsp;&nbsp;&nbsp;&nbsp;.Where(&nbsp;p
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&gt;&nbsp;p.Definition.Name&nbsp;==&nbsp;<span style="color:#a31515;">&quot;V/G&nbsp;Overrides&nbsp;Model&quot;</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.First();

&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;viewScaleParam&nbsp;=&nbsp;viewparams
&nbsp;&nbsp;&nbsp;&nbsp;.Where(&nbsp;p
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&gt;&nbsp;p.Definition.Name&nbsp;==&nbsp;<span style="color:#a31515;">&quot;View&nbsp;Scale&quot;</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.First();

&nbsp;&nbsp;<span style="color:green;">//&nbsp;Set&nbsp;includes</span>

&nbsp;&nbsp;viewTemplate.SetNonControlledTemplateParameterIds(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">ElementId</span>&gt;&nbsp;{
&nbsp;&nbsp;modelOverrideParam.Id,&nbsp;viewScaleParam.Id&nbsp;}&nbsp;);
</pre>

<p>Thank you, Teocomi, for sharing this!</p>
