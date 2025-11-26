---
layout: "post"
title: "Access embedded database properties"
date: "2012-08-15 10:49:00"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Navisworks"
original_url: "https://adndevblog.typepad.com/aec/2012/08/access-embedded-database-properties.html "
typepad_basename: "access-embedded-database-properties"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>I&#39;m trying to access embedded database properties inside the Navisworks ActiveX control but I cannot find them. I tried the InwOaNode.Attributes() and GetGUIPropertyNode() but they do not seem to be listed there.</p>
<p><strong>Solution</strong></p>
<p>At the moment those properties are not available inside the ActiveX and .NET controls and inside Freedom.</p>
<p>Among other things, the Navisworks model consists of:</p>
<ol>
<li>A node hierarchy with attached attributes. This is static and is created when the model is first converted from CAD format.&#0160;</li>
<li>Property plugins which add dynamic properties. E.g. Datatools or COM Property plugins.&#0160;</li>
</ol>
<p>What you see depends on where you look.</p>
<ul>
<li>When you look at the properties window in Navisworks you see the combination of both these: (1 and 2)&#0160;</li>
<li>If you use the .NET API you’ll you see the combination of both these: (1 and 2)&#0160;</li>
<li>If you use the COM API to look directly at attributes on nodes you see just the static attributes: (1 only).&#0160;</li>
<li>If You use the COM API GetGUIPropertyNode (see VB examples) you’ll you see the combination of both these: (1 and 2)</li>
</ul>
