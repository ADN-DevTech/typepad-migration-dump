---
layout: "post"
title: "ACA API: Accessing Project Properties"
date: "2015-07-23 09:32:08"
author: "Mikako Harada"
categories:
  - ".NET"
  - "AutoCAD Architecture"
  - "Mikako Harada"
original_url: "https://adndevblog.typepad.com/aec/2015/07/aca-api-accessing-project-properties.html "
typepad_basename: "aca-api-accessing-project-properties"
typepad_status: "Publish"
---

<p><strong>Q</strong>.&#0160; Using AutoCAD Architecture, we want to take information about the Project properties (Name, Number, Description). All the examples I can find online are to create a new project file using the API. I want to read the Project properties for the currently open Project in Architecture through the API. Is there a simple way to do this?&#0160;</p>
<p><strong>A</strong>.&#0160; You can use Autodesk.Aec.ProjectConfiguration class.&#0160; It allows you to access information about the project, such as the name, description, and number.&#0160;Below is a simple code snippet I added to the API sample AecProjectBaseSampleMgd:</p>
<div style="border: #000080 1px solid; color: #000; font-family: Consolas, &#39;Courier New&#39;, Courier, Monospace; font-size: 10pt;">
<div style="background-color: #ffffff; color: #000000; max-height: 300px; overflow: auto; padding: 2px 5px; white-space: nowrap;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;<span style="color: #0000ff;">private</span> <span style="color: #0000ff;">void</span> Config()<br /> &#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;{<br />&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;&#0160;<br /> &#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;<span style="color: #2b91af;">ProjectConfiguration</span> projConfig = proj.Configuration; <span style="color: #008000;">// new ProjectConfiguration(); </span><br /> <br /> &#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;<span style="color: #0000ff;">string</span> s = projConfig.Name + <span style="color: #a31515;">&quot;, &quot;</span>;<br /> &#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;s += projConfig.Number + <span style="color: #a31515;">&quot;, &quot;</span>;<br /> &#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;s += projConfig.Description;<br /> &#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;Print(s);<br /> &#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;}</div>
</div>
<p>For your convenience, I have attached a modified version of the sample: <span class="asset  asset-generic at-xid-6a0167607c2431970b01b8d13c663c970c img-responsive"><a href="http://adndevblog.typepad.com/files/aecprojectbasesamplemgd.zip">Download AecProjectBaseSampleMgd</a></span></p>
<p>To use this class, you will need to reference AecProjectBaseMgd.dll<br /> Attached was built with ACA 2016, using .NET 4.5&#0160;</p>
<p>Mikako</p>
