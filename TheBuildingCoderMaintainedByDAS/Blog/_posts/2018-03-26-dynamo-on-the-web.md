---
layout: "post"
title: "Dynamo on the Web?"
date: "2018-03-26 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Climbing"
  - "Cloud"
  - "Dynamo"
  - "Forge"
original_url: "https://thebuildingcoder.typepad.com/blog/2018/03/dynamo-on-the-web.html "
typepad_basename: "dynamo-on-the-web"
typepad_status: "Publish"
---

<p>Do you Dynamo?</p>

<p>Do you have a potential application for <a href="#3">Autodesk Dynamo on the Cloud</a>?</p>

<p>If yes, we want to talk to you.</p>

<p>First, however, I'll mention that I just returned from my nicest and longest ski tour so far this year, four days, in Austria, for a change: the famous Venter Runde in
the <a href="https://en.wikipedia.org/wiki/%C3%96tztal_Alps">Ötztal Alps</a>.</p>

<p>We visited the place
that the Hauslabjoch mummy <a href="https://en.wikipedia.org/wiki/%C3%96tzi">Ötzi</a> was
found and climbed the <a href="https://en.wikipedia.org/wiki/Fineilspitze">Finailspitze (3514 m)</a>,
<a href="https://en.wikipedia.org/wiki/Wei%C3%9Fkugel">Weißkugel (3739 m)</a> and
Fluchtkogel (3500 m) in splendid conditions
(<a href="http://thebuildingcoder.typepad.com/img/588_finailspitze_jeremy.jpg">high resolution</a> <a href="/p/2018/2018-03-24_skitour_venter_runde/thomas/588_finailspitze_jeremy.jpg">^</a>):</p>

<p><center></p>

<p><a class="asset-img-link"  href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c95b50ff970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c95b50ff970b image-full img-responsive" alt="Jeremy on the Finailspitze" title="Jeremy on the Finailspitze" src="/assets/image_407ed6.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<h4><a name="3"></a>Do You want Dynamo on the Web?</h4>

<p>Autodesk is considering making <a href="https://www.autodesk.com/products/dynamo-studio/overview">Dynamo</a>
&ndash; Autodesk’s Computational Design App and Engine &ndash; available as a Forge API.</p>

<p>We want to talk to customers and partners about how they might want to leverage Dynamo on the Cloud.  The opportunity is to take your Dynamo projects and make them available to a much wider audience &ndash; across the company, to business partners, or even as a commercial app &ndash; through a tailored web page (or desktop or mobile app) you create.</p>

<p>We are considering three possible implementations of Dynamo on the Cloud, and would like to know which of them most interests you, why, and how you envision using it:</p>

<ol>
<li><a href="#3.1">Dynamo as part of Forge Design Automation for Revit</a> </li>
<li><a href="#3.2">Headless Dynamo</a> </li>
<li><a href="#3.3">Full Dynamo on the Web</a> </li>
</ol>

<h4><a name="3.1"></a>1. Dynamo as part of Forge Design Automation for Revit</h4>

<p>As you may have heard already, Autodesk is well down the path
developing <a href="http://au.autodesk.com/au-online/classes-on-demand/class-catalog/classes/year-2017/autocad/sd124720#chapter=0">Forge Design Automation for Revit</a>,
aka <a href="http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.28b">Revit I/O</a>.</p>

<p>This is Revit as an 'engine' on the cloud.  No user interface, aka 'headless' &ndash; an engine you can drive using the Revit API we have today.  We are currently working on moving Forge Design Automation for Revit from private to public beta.</p>

<p>Would you like to be able to leverage Dynamo too?  No user interface means you would be programmatically loading a Revit app and your Dynamo project into a Revit instance on the cloud, pushing parameters and getting results &ndash; with your own handling of all user interaction (and maybe even server to server?).</p>

<h4><a name="3.2"></a>2. Headless Dynamo</h4>

<p>Dynamo as a standalone 'headless' engine on the cloud to orchestrate between services.</p>

<p>Dynamo is architected to not just drive Revit, but to power computational design processes that span any number of design tools and web services.  Do you have an application for Dynamo as a computational design engine independent of Revit? Again, this is a 'no UI' Dynamo web service &ndash; you would be responsible for creating and delivering the UI.</p>

<p>The Dynamo team has been building a new computational system that is cloud-native and completely independent from desktop applications. Do you have a set of Forge services you would like to tie together but are not sure how? You can use this to connect to existing Forge services and orchestrate how data flows between your application and services. You can use it as a way to extract logic and features from your desktop applications and re-use them as microservices in the cloud.</p>

<h4><a name="3.3"></a>3. Full Dynamo on the Web</h4>

<p>Dynamo on the Web &ndash; with full user interface as Dynamo Studio users experience today.</p>

<p>You have used Dynamo Studio along with Revit.  Do you have a problem that would be best solved by including Dynamo Studio (as a web service) inside your application of choice?  This would likely enable you to embed the complete Dynamo Studio UI in a web page, desktop or mobile app of your making.</p>

<h4><a name="4"></a>Your Choice</h4>

<p>Over the last few years Autodesk has invested time replumbing Dynamo to make all three of these Dynamo on the Cloud implementations a real possibility.  Now we need to hear which (if any) of these Dynamo on the Cloud you want and why.  Based on your input, both what you want to create and how many Autodesk customers and partners are also interested in these various Dynamo on the Cloud implementations, we’ll decide on how to best move forward delivering Dynamo as a Forge Computational Design API.</p>

<p>Please don’t hold back.  We need to hear from you.  If we don’t hear enough interest from the Dynamo and Forge development community, we might just leave Dynamo on the Cloud on the back shelf for another day.</p>

<p>How to let us know what you want? Just send an email
to <a href="mailto:jim.quanci@autodesk.com">jim.quanci@autodesk.com</a> to
participate in Dynamo as a Service research and discussion, and let him know:</p>

<ul>
<li>Which of the three above potential implementations you are interested in</li>
<li>Why</li>
<li>A few sentences of about your 'vision' of the user experience/app you would like to create</li>
<li>The company you work for</li>
<li>Your depth of experience with Dynamo today (beginner, advanced, 'guru')</li>
<li>Your telephone number, so we can talk live if we want to explore and better understand your potential application of Dynamo on the Cloud</li>
</ul>

<p>Thank you!</p>
