---
layout: "post"
title: "Accelerating in Munich"
date: "2016-01-20 12:40:15"
author: "Kean Walmsley"
categories:
  - "APS (Forge)"
  - "AutoCAD"
  - "AutoCAD .NET"
  - "AutoCAD I/O"
  - "PaaS"
  - "Personal"
original_url: "https://www.keanw.com/2016/01/accelerating-in-munich.html "
typepad_basename: "accelerating-in-munich"
typepad_status: "Publish"
---

<p>It’s been a hectic – but very fun – few days in Munich, working with a group of 60-70 developers from all over Europe who are getting started with Autodesk’s cloud services. I’m particularly focused on those working with AutoCAD I/O, which means probably 5-6 companies&#0160; in total.</p>
<p>Here’s a quick summary of some of the problems that have come up, many of which are for pure AutoCAD…</p>
<p><strong>Draw Order warning</strong></p>
<p>One developer has a long-running custom operation that presents the following message:</p>
<blockquote>
<p><em>AutoCAD Warning: This operation may take a long time. To greatly improve performance, the system can disregard Draw Order. Disregard Draw Order for this operation?</em></p>
</blockquote>
<p>The fix for this turned out to be simple: setting DRAWORDERCTL to 0 fixed the problem.</p>
<p><strong>UI refresh during long-running operations</strong></p>
<p>Someone experienced an issue with their UI not refreshing: a classic issue with code that keeps the UI thread occupied. I explained a couple of approaches to address this: using async/await to have the compiler package up the additional code and execute it later (my preferred approach, these days) or calling System.Windows.Forms.Application.DoEvents();.</p>
<p><strong>Creating dynamic dimension constraints</strong></p>
<p>Another problem related to generating dimension constraints through code. When these are created via .NET they’re “annotational” by default: the developer wanted them to be “dynamic”, but couldn’t find an API to make this happen. We did find that it could be toggled via the properties window, so I managed to find <a href="http://help.autodesk.com/view/ACD/2016/ENU/?guid=GUID-3BA78692-AC13-4649-8C21-2BE7C4ECED62" target="_blank">a way to switch the dimension using COM Automation</a>. To avoid including a type library reference in the project I added a little snippet of code that did this dynamically (yes, an extremely overloaded term in this context):</p>
<div style="font-size: 8pt; font-family: courier new; background: white; color: black; line-height: 140%;">
<p style="margin: 0px;"><span style="color: blue;">dynamic</span> obj = dimension.AcadObject;</p>
<p style="margin: 0px;"><span style="color: blue;">var</span> dyn = obj.DimConstrForm;</p>
<p style="margin: 0px;">obj.DimConstrForm = <span style="color: blue;">true</span>;</p>
<p style="margin: 0px;">&#0160;</p>
</div>
<p>What’s a little perturbing is that it didn’t work without the second line, which only reads the dynamic property – not calling it shouldn’t actually change the behaviour of the code, but it turns out to be required. I assume this is a slight quirk based on the fact the property really isn’t a simple Boolean: changing the value does some structural stuff that you can check using ArxDbg. So perhaps getting the property before setting it – which the properties window does, of course – is needed.</p>
<p><strong>Custom AppPackage loading in AutoCAD I/O</strong></p>
<p>On the AutoCAD I/O front we had some interesting times analysing loading issues for custom AppPackages. An important first step is to get the DLL loadable in the Core Console, but that doesn’t always mean it’ll work inside AutoCAD I/O. An important additional step is to make sure the unzipped AppPackage will be found and loaded by the Autoloader: placing it in a local plugin-loading location – such as <em>%appdata%\Autodesk\ApplicationPlugins</em> – should allow you to identify whether the plugin can be Autoloaded properly. If that works locally but fails on AutoCAD I/O, then it gets tricky… one issue we saw, this week, related to dependencies: stripping those out helped allowed the application to load and we could build it back up from there.</p>
<p>There have been some really nice products starting to emerge from the Accelerator, whether using View &amp; Data or AutoCAD I/O. One that I particularly like involves an AutoCAD I/O-based service downloading cadastral data and generating the parcel geometry for download as a DWG file.</p>
<p>It’s really motivating to see so much excitement and interest in using our cloud APIs. People are engaging on real projects that will deliver serious value for their customers. Wonderful to see!</p>
<p>Thankfully it’s not all work and no play, though: here’s a quick snap of a number of us having dinner last night at <a href="http://www.nasca-cafe.de" target="_blank">a Peruvian restaurant in Munich</a>…</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201b8d194325b970c-pi"><img alt="Dinner at Nasca" border="0" height="312" src="/assets/image_582402.jpg" style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin: 30px auto 0px; display: block; padding-right: 0px; border-width: 0px;" title="Dinner at Nasca" width="504" /></a></p>
