---
layout: "post"
title: "Modifying Deck Profile Type in Floor Type Compound Structure"
date: "2013-01-04 13:31:29"
author: "Mikako Harada"
categories: []
original_url: "https://adndevblog.typepad.com/aec/2013/01/modifying-deck-profile-type-in-floor-type-compound-structure.html "
typepad_basename: "modifying-deck-profile-type-in-floor-type-compound-structure"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/mikako-harada.html" target="_self" title="Mikako Harada">Mikako Harada</a></p>
<p><strong>Issue</strong></p>
<p>I want to assign a different deck profile to a floor type. In UI, you can do this by simply selecting a deck profile: </p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c354fd6c0970b-pi" style="display: inline;"><img alt="DecoProfile100" border="0" class="asset  asset-image at-xid-6a0167607c2431970b017c354fd6c0970b" src="/assets/image_131928.jpg" title="DecoProfile100" /></a></p>
<p>However, if we try to achieve the same thing using the API,&#0160;only the deck profile is updated. The thickness remains with the previous value.&#0160;What am I missing to make this work?&#0160; I used the following call to do this: </p>
<div style="font-family: Consolas; font-size: 10pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue;"><span style="color: #2b91af;">&#0160; </span>Dim</span> compStr <span style="color: blue;">As</span> CompoundStructure = myFloorType.GetCompoundStructure</p>
<p style="margin: 0px;"><span style="color: #2b91af;">&#0160;&#0160;</span>compStr.SetDeckProfileId(0, myProfileType.Id)</p>
<p style="margin: 0px;"><span style="color: #2b91af;">&#0160;&#0160;</span>myFloorType.SetCompoundStructure(compStr)</p>
</div>
<p><strong>Solution</strong> </p>
<p>Unfortunately, as of January, 2013, this is a known issue and needs to be addressed by the product team. </p>
