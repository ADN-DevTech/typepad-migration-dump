---
layout: "post"
title: "Setting a user event within Maya"
date: "2012-07-25 02:41:00"
author: "Kristine Middlemiss"
categories:
  - "Kristine Middlemiss"
  - "Maya"
  - "MEL"
  - "Python"
original_url: "https://around-the-corner.typepad.com/adn/2012/07/setting-a-user-event-within-maya.html "
typepad_basename: "setting-a-user-event-within-maya"
typepad_status: "Publish"
---

<p>I was looking through some interesting questions that have come into ADN in the past, and thought I would share this one, it was about the possibility of setting a user event with a scriptJob (Maya Command).</p>
<p>There are no MEL commands to create user events and the scriptJob command cannot be used to wait for them.</p>
<p>However, you can do both in a Python script using API calls.</p>
<p>Here&#39;s how you would register a new user event type called &#39;myEvent&#39;:</p>
<p>&#0160;&#0160;&#0160;&#0160;<span style="color: #0000ff;">import</span> maya.OpenMaya <span style="color: #0000ff;">as</span> om<br />&#0160;&#0160;&#0160; om.MUserEventMessage.registerUserEvent<span style="color: #ff0000;">(</span><span style="color: #80c0ff;">&#39;myEvent&#39;</span><span style="color: #ff0000;">)</span></p>
<p>To have a function called &#39;myFunc&#39; execute whenever the event occurs:</p>
<p>&#0160;&#0160;&#0160;&#0160;<span style="color: #0000ff;">def</span> myFunc<span style="color: #ff0000;">(</span>data<span style="color: #ff0000;">):</span><br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; print<span style="color: #ff0000;">(</span><span style="color: #80c0ff;">&#39;Got a myEvent event!&#39;</span><span style="color: #ff0000;">)</span></p>
<p>&#0160;&#0160;&#0160; callbackId =om.MUserEventMessage.addUserEventCallback<span style="color: #ff0000;">(</span><span style="color: #80c0ff;">&#39;myEvent&#39;</span>, myFunc<span style="color: #ff0000;">)</span></p>
<p>To send a &#39;myEvent&#39; event:</p>
<p>&#0160;&#0160;&#0160; om.MUserEventMessage.postUserEvent<span style="color: #ff0000;">(</span><span style="color: #80c0ff;">&#39;myEvent&#39;</span><span style="color: #ff0000;">)</span></p>
<p>To remove the callback function when done:</p>
<p>&#0160;&#0160; &#0160;om.MUserEventMessage.removeCallback<span style="color: #ff0000;">(</span>callbackId<span style="color: #ff0000;">)</span></p>
<p>Enjoy</p>
<p>~Kristine</p>
