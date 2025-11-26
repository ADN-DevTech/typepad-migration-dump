---
layout: "post"
title: "I18n, Forge Accelerators, AEC and BIM360 Focus"
date: "2021-01-19 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "360"
  - "Accelerator"
  - "Architecture"
  - "AU"
  - "BIM"
  - "Cloud"
  - "Forge"
  - "Getting Started"
  - "I18n"
original_url: "https://thebuildingcoder.typepad.com/blog/2021/01/i18n-forge-accelerators-aec-and-bim360-focus.html "
typepad_basename: "i18n-forge-accelerators-aec-and-bim360-focus"
typepad_status: "Publish"
---

<p>An especially intresting Forge accelerator is coming up, and how to handle Revit add-in language resources:</p>

<ul>
<li><a href="#2">Upcoming Forge accelerators</a></li>
<li><a href="#3">AEC focused Forge accelerator Waldspirale</a></li>
<li><a href="#4">Internationalisation using .NET language resources</a></li>
</ul>

<h4><a name="2"></a> Upcoming Forge Accelerators</h4>

<p>We currently have two <a href="https://forge.autodesk.com/accelerator-program">upcoming Forge accelerators</a> scheduled:</p>

<ul>
<li>Machu Picchu, Virtual Accelerator, January 25-29, 2021 &ndash; <a href="https://www.eventbrite.com/e/autodesk-virtual-forge-accelerator-machu-picchu-january-25-29-2021-registration-131468575047">apply now</a></li>
<li>Waldspirale, Virtual Accelerator, February 22-26, 2021 &ndash; <a href="https://www.eventbrite.com/e/acc-focused-autodesk-virtual-accelerator-waldspirale-feb-22-26-2021-registration-131597280007">apply now</a></li>
</ul>

<p>Joining a Forge accelerator is the most effective way to get up to speed on Forge in general and also
to <a href="https://thebuildingcoder.typepad.com/blog/2020/04/2021-migration-add-in-language-and-bim360-login.html#2">answer any and all BIM360 related questions</a> you
may be pondering.</p>

<p>As a knowledgeable and architecturally interested person might guess, the February event is AEC oriented.</p>

<h4><a name="3"></a> AEC Focused Forge Accelerator Waldspirale</h4>

<p>An AEC focused online Forge accelerator is coming up February 22-26.</p>

<p>It is named "Waldspirale" in honour of
the Austrian artist, environmentalist and architect <a href="https://en.wikipedia.org/wiki/Friedensreich_Hundertwasser">Friedensreich Hundertwasser</a> and
his <a href="https://en.wikipedia.org/wiki/Waldspirale">105-apartment building of the same name</a> in Darmstadt, Germany.</p>

<p>We are hence looking for proposals that aim to solve problems in design, construction and building life cycle management.</p>

<p>We very much look forward to discussing and supporting your many creative ideas and helping you to jump start your development with Forge during the week-long event.   </p>

<p>In addition to the APIs that are already publicly available, we are also expecting to have some new sets of BIM 360 APIs to play with by then <sup><a href="#3.1">(*1)</a></sup>:</p>

<ul>
<li>Assets API </li>
<li>Data Connector API</li>
</ul>

<p>If you are interested in using these new APIs, this event will be a great opportunity to learn and develop while "sitting" with development teams.</p>

<p>If you would like to know what will be possible with the Assets feature and its API, please check out the recordings of the recent Autodesk University presentations:</p>

<ul>
<li><a href="https://www.autodesk.com/autodesk-university/class/Tracking-Assets-and-Equipment-BIM-360-2020">Tracking Assets and Equipment in BIM 360</a></li>
<li><a href="https://www.autodesk.com/autodesk-university/class/BIM-360-API-Update-and-Beyond-2020">BIM 360 API Update and Beyond</a>
&ndash; the Assets API overview starts at 16:15</li>
</ul>

<p>In addition, the <a href="https://help.autodesk.com/view/BIM360D/ENU/?guid=BIM360D_Insight_data_extractor_html">Data Connector</a> feature enables you to extract data at an account level with Executive privilege.
This API will allow you to schedule extraction jobs and access extracted data.   </p>

<p>In case of interest, here are 
more <a href="https://thebuildingcoder.typepad.com/blog/2021/01/forge-at-au-and-open-source-property-access.html#3">full articles and video recordings of the Forge classes at AU classes</a>.</p>

<p>Please submit your proposal
to <a href="https://forge.autodesk.com/accelerator-program">forge.autodesk.com/accelerator-program</a>.</p>

<p>If you would like to find out more about the new APIs in order to detail your proposal, feel free to indicate so in your proposal.
We will get back to you and discuss further in your specific context. </p>

<p><a name="3.1"></a>
(*1) Disclaimer: until we actually see the API officially released, the public release date might change. </p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330278800eacbe200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330278800eacbe200d image-full img-responsive" alt="AEC focused Forge accelerator Waldspirale" title="AEC focused Forge accelerator Waldspirale" src="/assets/image_6f1d27.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<h4><a name="4"></a> Internationalisation Using .NET Language Resources</h4>

<p>Before closing for today, let's quickly also address a desktop Revit API issue.</p>

<p><a href="https://forums.autodesk.com/t5/user/viewprofilepage/user-id/767846">Lukáš Kohout</a>, Czech Architect and Revit Developer, very kindly solved 
a <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
about internationalisation or I18n
on <a href="https://forums.autodesk.com/t5/revit-api-forum/how-to-convert-a-revit-plugin-from-english-to-other-languages/m-p/10008918">how to convert a Revit plugin from English to other languages</a>:</p>

<p><strong>Question:</strong> I have been developing Revit plugins for the past two years in English language.</p>

<p>Now, I intend to convert them into other languages to make them useful for people all around the world.</p>

<p>I would appreciate if anybody could help me with the procedure for doing so.</p>

<p><strong>Answer:</strong> You can use Resources with string tables as described in the StockOverflow discussion
on <a href="https://stackoverflow.com/questions/1142802/how-to-use-localization-in-c-sharp">how to use localisation in C#</a>.</p>

<p>Many thanks to Lukáš for the very helpful pointer.</p>
