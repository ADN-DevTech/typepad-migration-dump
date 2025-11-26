---
layout: "post"
title: "Profile entities by order via EntityAtId"
date: "2016-04-05 07:04:38"
author: "Augusto Goncalves"
categories:
  - ".NET"
  - "Augusto Goncalves"
  - "AutoCAD Civil 3D"
original_url: "https://adndevblog.typepad.com/infrastructure/2016/04/profile-entities-by-order-via-entityatid.html "
typepad_basename: "profile-entities-by-order-via-entityatid"
typepad_status: "Publish"
---

<p>By Augusto Goncalves (<a href="https://twitter.com/augustomaia">@augustomaia</a>)</p>
<p>Unlike Alignment.Entities.GetEntityByOrder, the Profile.Entities only offers a plain enumerator or .EntityAtId. But don&#39;t worry! With the FirstEntity we can iterate through the collection using a loop. The following code snippet show the idea.&#0160;</p>
<p>By the way, the .NET Extension method will not work here due the <a href="https://blogs.msdn.microsoft.com/ericwhite/2009/04/08/why-i-dont-use-the-foreach-extension-method/">stateless paradigm</a>.&#0160;</p>

<div>
<pre style="margin: 0; line-height: 125%;">Profile p = <span style="color: #aaaaaa; font-style: italic;">// get the object here</span>

<span style="color: #00aaaa;">uint</span> index = p.Entities.FirstEntity;
<span style="color: #0000aa;">try</span>
{
  <span style="color: #0000aa;">while</span>(<span style="color: #0000aa;">true</span>)
  {
    <span style="color: #aaaaaa; font-style: italic;">// get current entity</span>
    ProfileEntity profileEntity = p.Entities.EntityAtId(index);
        
    <span style="color: #aaaaaa; font-style: italic;">// do something</span>
    <span style="color: #aaaaaa; font-style: italic;">//</span>
    <span style="color: #aaaaaa; font-style: italic;">//</span>

    <span style="color: #aaaaaa; font-style: italic;">// get the next entity</span>
    index = profileEntity.EntityAfter; <span style="color: #aaaaaa; font-style: italic;">// throw exception</span>
  }

}
<span style="color: #0000aa;">catch</span>
{
  <span style="color: #aaaaaa; font-style: italic;">// EntityAfter will throw an excetion, end of the loop;</span>
}
</pre>
</div>
