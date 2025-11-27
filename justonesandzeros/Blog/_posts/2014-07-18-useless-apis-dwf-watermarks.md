---
layout: "post"
title: "Useless APIs - DWF Watermarks"
date: "2014-07-18 17:31:44"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
  - "Vault"
original_url: "https://justonesandzeros.typepad.com/blog/2014/07/useless-apis-dwf-watermarks.html "
typepad_basename: "useless-apis-dwf-watermarks"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Vault4.png" /> <br /><img alt="" src="/assets/TipsAndTricks4.png" /></p>
<p>Just because it’s in the Vault API, doesn’t mean you will be able to build anything cool with it.&#0160; There is plenty of stuff in there that is so specific to Vault Explorer or so difficult to use that it’s basically useless to you as an outside developer.&#0160; I’m feeling that this will probably be an entire series of articles... especially when I get to the Package Service.</p>
<p>So why am I bothering to write an article about something you can’t use?&#0160; Because it’s good to know what you can’t do upfront.&#0160; That way you don’t waste precious time on the impossible.&#0160; GI Joe cartoons taught me that “Knowing is half the battle.”&#0160; Well, following that logic, <strong><em>not-knowing</em></strong> is the other half.&#0160; So let’s get started!!</p>
<hr noshade="noshade" style="color: #d09219;" />
<p>Vault Professional has a cool feature where you can put a <a href="http://underthehood-autodesk.typepad.com/blog/2006/03/watermarking.html" target="_blank">watermark</a> on your DWF files.&#0160; As a developer, you may be thinking that you can make use of this.&#0160; Maybe you want to download these watermarked files or view them in you own app.</p>
<p>No.&#0160; Sorry.&#0160; You can’t do that.</p>
<p>Although the Vault Server keeps track of the watermark settings, it only includes things like color, text, and location.&#0160; The server doesn’t actually generate a file with the watermark on it.&#0160; The watermark is <em>rendered</em> client-side.&#0160; It’s basically another layer drawn on top of the existing content in the DWF viewer.&#0160;</p>
<p>Vault Explorer uses a special DWF viewer that is compiled just for Vault.&#0160; And it doesn’t have an API.&#0160; Basically, the watermark functions in the Vault API are specifically for the out-of-the-box Vault clients and no one else.</p>
<hr noshade="noshade" style="color: #d09219;" />
<p><strong>Useless functions:</strong></p>
<p>All of these functions are in the Item Service.</p>
<ul>
<li>GetAllWatermarks</li>
<li>GetEnableWatermarking</li>
<li>SetEnableWatermarking</li>
<li>UpdateWatermarkDefinitions</li>
</ul>
<hr noshade="noshade" style="color: #d09219;" />
<p><strong>Difficulty:</strong></p>
<p>If you really want to watermark your own DWFs, here are the basic steps:</p>
<ul>
<li>Find a DWF viewer with an API.&#0160; Offhand, I don’t know if any exist.</li>
<li>Grab the DWF for a given Item.</li>
<li>Grab all watermarks from Vault.</li>
<li>Figure out which watermarks apply to the DWF.</li>
<li>Render the watermarks on the DWF view.</li>
</ul>
<hr noshade="noshade" style="color: #d09219;" />
