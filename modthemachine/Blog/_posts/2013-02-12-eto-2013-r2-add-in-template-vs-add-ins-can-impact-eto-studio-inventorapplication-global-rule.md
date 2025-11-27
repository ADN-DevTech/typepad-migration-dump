---
layout: "post"
title: "ETO 2013 R2 &ndash; Add-In template, VS Add-Ins can impact ETO Studio &amp; InventorApplication global rule"
date: "2013-02-12 23:05:17"
author: "Wayne Brill"
categories:
  - "Engineer-To-Order"
  - "Wayne"
original_url: "https://modthemachine.typepad.com/my_weblog/2013/02/eto-2013-r2-add-in-template-vs-add-ins-can-impact-eto-studio-inventorapplication-global-rule.html "
typepad_basename: "eto-2013-r2-add-in-template-vs-add-ins-can-impact-eto-studio-inventorapplication-global-rule"
typepad_status: "Publish"
---

<p>Here are a couple of things about ETO 2013 R2 that I have found recently.&#0160;</p>
<p>1. If you have used the Inventor ETO Add-In template you may find that the ribbon tab for the Add-In does not display properly when Inventor is launched through ETO Studio. When Intent is started from Visual Studio, it does extensive processing in its OnReady handler. It reads command-line arguments and they tell it to load or create a model and establish communications with VS. Because of this the event handlers in the Add-In can be called later compared to when they are called when Inventor is started normally.&#0160;</p>
<p>To avoid this behavior you can call the code that creates the ribbon in the IntentInitialized function. Here is a slightly modified version of the Add-In code. (In IntentModel.cs) This change fixed the problem in one case. (Keep in mind that the the Add-In template should be considered as a starting point).</p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;"><span style="color: green;">// This will be called if and when Intent </span></p>
<p style="margin: 0px;"><span style="color: green;">//gets initialized by the host.</span></p>
<p style="margin: 0px;"><span style="color: blue;">void</span> IntentInitialized</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; (<span style="color: #2b91af;">IntentAPI</span> intentAPI, <span style="color: blue;">object</span> hostAPI)</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160;<span style="color: green;">// Set up model event handlers if desired.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">IHostAPI</span> host = (<span style="color: #2b91af;">IHostAPI</span>)hostAPI;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">ModelEvents</span> ModelEvent =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; intentAPI.ModelEvents;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; host.Events.BeforeModelLoad +=</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">new</span> <span style="color: #2b91af;">EventHandler</span>(Events_BeforeModelLoad);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; ModelEvent.BeforeRender +=</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">new</span> <span style="color: #2b91af;">EventHandler</span>&lt;<span style="color: #2b91af;">BeforeModelRenderArgs</span>&gt;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (ModelEvent_BeforeRender);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; ModelEvent.AfterRender +=</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">new</span> <span style="color: #2b91af;">EventHandler</span>&lt;<span style="color: #2b91af;">AfterModelRenderedArgs</span>&gt;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (ModelEvent_AfterRender);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Added to resolve problem with AddIn </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// ribbon when debugging</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (intentAPI.Models.Count &gt; 0)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; m_gui.Activate();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">}</p>
</div>
<p>&#0160;</p>
<p>2. We have found in one case that ETO Studio is adversely effected by the Visual Studio Add-In named ReSharper. Some of the symptoms are the following:</p>
<blockquote>
<p><em>Error Message when opening the ETO Studio project: “The parameter is incorrect”. </em></p>
</blockquote>
<blockquote>
<p><em>Edit Rule Error - “Design for selected part not loaded in Visual Studio Environment. Please make sure the project containing the design is open” </em></p>
</blockquote>
<blockquote>
<p><em>ETO Model browser sees the designs under root, but cannot open them. Clicking “Edit Design” gives the same error as above. </em></p>
</blockquote>
<blockquote>
<p><em>Design Navigator shows no User Designs</em></p>
<p>&#0160;</p>
</blockquote>
<p>The behavior has been reported to Autodesk ETO engineering. For now the suggestion is to disable the Visual Studio Add-In.</p>
<p>&#0160;</p>
<p>3. There is now an InventorApplication global Rule. Before that, you had to use hidden %%InventorApplication. Here you see the ETO Console in Visual Studio using the Inventor API:</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017d41026dca970c-pi"><img alt="image" border="0" height="126" src="/assets/image_472796.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="463" /></a></p>
<p>-Wayne</p>
