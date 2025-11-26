---
layout: "post"
title: "Updating a Profile mode using Profile.UpdateMode"
date: "2013-01-07 00:59:23"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2012"
  - "AutoCAD Civil 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2013/01/updating-a-profile-mode-using-profileupdatemode.html "
typepad_basename: "updating-a-profile-mode-using-profileupdatemode"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>AutoCAD Civil
3D <strong>Profile</strong> type has a property named <em><strong>UpdateMode</strong></em> which can be used to get or set
the update mode of the profile.</p>
<p>Here is a
note from the Civil 3D API reference document help file on Profile.UpdateMode property
which I would like to draw your attention -&#0160;</p>
<p><span style="color: #bf5f00;">Gets for
Superimposed, EG, CorridorFeature only. <strong>Sets for surface profiles only</strong>.
Specifies whether the profile updates automatically to reflect changes in
surface elevation. </span></p>
<p><span style="color: #bf5f00;">System.InvalidOperationException
is thrown when setting the UpdataMode which profile&#39;s type is not EG.</span> </p>
<p>&#0160;</p>
<p>Here is a C#
code snippet which demonstrates the usage of this API -</p>
<p>&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Profile</span><span style="line-height: 140%;"> profile = trans.GetObject(profileId, </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForRead) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Profile</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (profile.ProfileType == </span><span style="color: #2b91af; line-height: 140%;">ProfileType</span><span style="line-height: 140%;">.EG)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\nSelected Profile is of Type : &quot;</span><span style="line-height: 140%;"> + profile.ProfileType.ToString());</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\nUpdateMode Before Change :&quot;</span><span style="line-height: 140%;"> + profile.UpdateMode.ToString());</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// update using Profile.UpdateMode</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// Gets for Superimposed, EG, CorridorFeature only. </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// Sets for surface profiles only. </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; profile.UpgradeOpen();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; profile.UpdateMode = </span><span style="color: #2b91af; line-height: 140%;">ProfileUpdateType</span><span style="line-height: 140%;">.Dynamic;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\nUpdateMode After Change :&quot;</span><span style="line-height: 140%;"> + profile.UpdateMode.ToString());</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\nSelected Profile is of Type : &quot;</span><span style="line-height: 140%;"> + profile.ProfileType.ToString() + </span><span style="color: #a31515; line-height: 140%;">&quot;&#0160; Can not update the Mode&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"><br /></span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c356417aa970b-pi" style="display: inline;"><img alt="Profile_Update" class="asset  asset-image at-xid-6a0167607c2431970b017c356417aa970b" src="/assets/image_d91488.jpg" title="Profile_Update" /></a><br /><br /></span></p>
</div>
