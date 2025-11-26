---
layout: "post"
title: "Opening and Activating Document in an Event Handler"
date: "2015-03-05 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Events"
  - "External"
  - "Git"
  - "Idling"
  - "News"
  - "VB"
  - "XAML"
original_url: "https://thebuildingcoder.typepad.com/blog/2015/03/opening-and-activating-document-in-an-event-handler.html "
typepad_basename: "opening-and-activating-document-in-an-event-handler"
typepad_status: "Publish"
---

<p>I have been a busy beaver with the Revit API on the

<a href="http://forums.autodesk.com/t5/revit-api/bd-p/160">discussion forum</a> in

the past few days, and on

<a href="https://github.com/jeremytammik">GitHub</a> as well.</p>

<p>I want to take a closer look at two related issues that date back a little bit further:</p>

<ul>
<li><a href="http://forums.autodesk.com/t5/revit-api/switching-active-documents-is-not-allowed-during-api-event/m-p/5498500">
Switching active documents is not allowed during API event handling</a></li>

<li><a href="http://forums.autodesk.com/t5/revit-api/how-to-load-a-rvt-file-automatically-when-start-the-revit/m-p/5500878">
Loading a RVT project file automatically on start-up</a></li>
</ul>

<!--
<ul>
<li><a href="#2">Open and activate a document during an API event</a></li>
<li><a href="#3">Loading a RVT project file automatically on start-up</a></li>
</ul>
-->

<p>Let's discuss the first today and save the second for later.</p>

<p>Before getting to that, I also want to mention the Autodesk Foundation that is currently celebrating its one-year anniversary.</p>


<a name="2"></a>

<h4>Autodesk Foundation</h4>

<p>The Autodesk Foundation is the first foundation to focus investment exclusively on the people and organizations using design for impact.
<b>Impact design</b> is a growing trend among designers who are using their talents to create a better world.
The fields of public interest, social impact, community, humanitarian, and sustainable design—together making up impact design—are all on a critical growth path as more people use the power of design for positive social and environmental impact.</p>

<p>Here is a <a href="https://www.youtube.com/watch?v=JV0HyXjpMi0">three and a half minute video</a> explaining more about the Autodesk Foundation and impact design and describing how:</p>

<ul>
<li>15 impact design organizations were supported by the Autodesk Foundation. One said, “In less than a year, the Autodesk Foundation has become one of the most sophisticated foundations working in the field.”</li>
<li>2700 nonprofits were supported by employees and/or the Autodesk Foundation with funding, technology, and volunteer time.</li>
<li>1500 Autodeskers joined in making a positive impact at work, at home, and in the community.</li>
</ul>

<center>
<iframe width="420" height="236" src="https://www.youtube.com/embed/JV0HyXjpMi0" frameborder="0" allowfullscreen></iframe>
</center>

<p>Back to the Revit API...</p>


<a name="3"></a>

<h4>Open and Activate a Document During an API Event</h4>

<p>This query on

<a href="http://forums.autodesk.com/t5/revit-api/switching-active-documents-is-not-allowed-during-api-event/m-p/5498500">
switching active documents is not allowed during API event handling</a> was

raised by

<a href="https://github.com/LiFeleSs">Luigi Trabacchin</a>.</p>

<p>If you wish to fast-forward straight to the solution, it consists in using an external event and XAML in VB.NET, and is demonstrated by the

<a href="https://github.com/LiFeleSs/RevitOpenAndActivateDuringIdle">VB.NET RevitOpenAndActivateDuringIdle sample on GitHub</a>.</p>

<p>Here is the edited discussion leading up to this result:</p>

<p><strong>Question:</strong>

This really self-explanatory exception is thrown when I try to do an Application.OpenandActivateDocument, the second time (when I have no active documents it works),</p>

<p>The point is the event I'm trying to do this from, in the application Idling event handler.</p>

<p>I really expected to be allowed to do that from there.</p>

<p><strong>Answer:</strong>

Does this discussion on

<a href="http://thebuildingcoder.typepad.com/blog/2012/12/closing-the-active-document.html">
closing the active document</a> help

in any way?</p>



<p><strong>Response:</strong>

Hello Jeremy I went through your blog thoroughly before posting and that article has been read.</p>

<p>I tried to close the document but I get another error, I think process start can help, but I dislike that option since I don't get any reference to the document ... maybe I can get through open documents and find the one with that filename...</p>

<p>First call works from second on it doesn't.</p>

<p>I made a solution

<a href="https://github.com/LiFeleSs/RevitOpenAndActivateDuringIdle">RevitOpenAndActivateDuringIdle</a> to

investigate the problem with the community.</p>


<p><strong>Answer</strong> by Arno&scaron;t L&ouml;bel:

Naturally, I have to step in, for I do not like Jeremy’s suggested solution all that much (and he knows it :-)</p>

<p>Although opening a new active document is not allowed from event handlers when there already is a document open in Revit, there may be an alternative. I believe we have implemented External Events so they do not share this restriction with events. Thus, for the user who otherwise used the Idling event, switching to an External Event could be a suitable solution. And it is both supported and safe.</p>

<p>Of course, just as you say, opening a DB document has no restrictions. You should be always able to do it in virtually any situations.</p>

<p>There is some help on using External Events in the SDK samples and Revit API help file.</p>

<p><strong>Response:</strong>

I've found an example about that on Jeremy's blog, my source for inspiration (thanks Jeremy) and updated my solution to not use the Idling event but to raise my shiny new external event, and it works like a charm!</p>
<p>I didn't realize the external events purpose; I thought it was a way to leverage Idling handling...</p>
<p>So thanks for pointing me in the right direction!</p>

<p>I updated the

<a href="https://github.com/LiFeleSs/RevitOpenAndActivateDuringIdle">GitHub repository</a>,

if you want to include it in the samples...</p>

<p><strong>Answer:</strong>

Congratulations on resolving your problem, and thank you for sharing the result.</p>

<p>I would love to share your sample with the rest of the Revit add-in developer community...</p>

<p>Could you be a bit more specific about what it actually demonstrates and how, now that all the issues you were encountering are resolved and you are clear about the use of external events?</p>

<p><strong>Response:</strong>

Very good!
You can share my example as long as you want; in fact it is publicly available on

<a href="https://github.com/LiFeleSs/RevitOpenAndActivateDuringIdle">GitHub</a>.</p>

<p>The last version of it demonstrates how using an ExternalEvent solves getting the "Switching active documents is not allowed during API event handling" exception trying to open a document (or more than one) during a call from a command or an event such Idling.</p>

<p>And also it demonstrates how to declare and call the external event.</p>

<p>The previous versions could be also quite useful to some Revit API users because it shows how to get a task executed during the Idling event, but needs some rearrangement because it was built to demonstrate the other issue.</p>
