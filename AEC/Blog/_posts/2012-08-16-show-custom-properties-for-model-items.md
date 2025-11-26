---
layout: "post"
title: "Show custom properties for model items"
date: "2012-08-16 10:55:00"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "COM"
  - "Navisworks"
original_url: "https://adndevblog.typepad.com/aec/2012/08/show-custom-properties-for-model-items.html "
typepad_basename: "show-custom-properties-for-model-items"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>I would like to show extra properties in the UI when a specific model item is selected.</p>
<p><strong>Solution</strong></p>
<p>With the COM API you can add a Property plugin. There’s a VB6 example in the api folder: &quot;C:\Program Files\Autodesk\Navisworks Manage 2011\api\COM\examples\PLUGIN_05&quot;.</p>
<p>A Property plugin dynamically makes extra properties appear as if they were part of the original CAD model. The properties are never saved into the actual model.</p>
<p>Such properties are visible through native searches and the .NET / COM API’s.</p>
<p>The above is not possible at the moment with the .NET API (NW 2011/2012), however you can create a COM Plugin using .NET as well.</p>
<p>Attached is a .NET version of the above mentioned sample. This too requires gatehouse.mdb and gatehouse.nwd from &quot;C:\Program Files\Autodesk\Navisworks Manage 2011\api\COM\examples&quot; folder for testing.</p>
<p>In case of using Navisworks version other than 2011 you need to update the &quot;Register Plugin.reg&quot; file with the correct registry path.</p>
<p>Here is the sample:&#0160;<span class="asset  asset-generic at-xid-6a0167607c2431970b01676907a004970b"><a href="http://adndevblog.typepad.com/files/nwpropertyplugin_nw2011.zip">Download Nwpropertyplugin_nw2011</a></span></p>
