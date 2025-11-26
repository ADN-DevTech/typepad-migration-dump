---
layout: "post"
title: "Auto-Run an Add-In for Design Automation"
date: "2018-09-26 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Accelerator"
  - "Automation"
  - "DA4R"
  - "Data Access"
  - "Forge"
  - "SDK Samples"
original_url: "https://thebuildingcoder.typepad.com/blog/2018/09/auto-run-an-add-in-for-design-automation.html "
typepad_basename: "auto-run-an-add-in-for-design-automation"
typepad_status: "Publish"
---

<p>Still at the Forge Accelerator in Rome and looking further into
the <a href="https://autodesk-forge.github.io">Forge</a>
<a href="https://forge.autodesk.com/en/docs/design-automation/v2/overview">Design Automation API</a> for Revit.</p>

<p>As mentioned yesterday, it is not yet available or documented, except to a closely restricted private beta.
For more information on its current status, please refer to
<a href="https://fieldofviewblog.wordpress.com/revit">Mikako Harada's discussion of Design Automation for Revit</a>.</p>

<p>However, you can stiil start preparing your add-in for the day when it comes:</p>

<ul>
<li><a href="#2">Aspects to consider</a> </li>
<li><a href="#3">Implementing DB application and accessing the Revit <code>Application</code> object</a> </li>
<li><a href="#4">DB application add-in manifest</a> </li>
<li><a href="#5">Next steps</a></li>
<li><a href="#6">Download</a></li>
</ul>

<h4><a name="2"></a> Aspects to Consider</h4>

<p>Here are some aspects to consider:</p>

<ul>
<li>No user interface</li>
<li>Ensure that no warnings are displayed <!-- <br/>&ndash; done, cf. yesterday's discussion on <a href="http://thebuildingcoder.typepad.com/blog/2018/09/swallowing-stairsautomation-warnings.html">swallowing StairsAutomation warnings</a> --></li>
<li>No references to RevitAPIUI, Windows Forms, or other user interface related assemblies</li>
<li>Driven automatically with input received via JSON files and model path of RVT document to process</li>
<li>The app is responsible for opening the model itself</li>
</ul>

<p>Yesterday, as a first example step,
we <a href="http://thebuildingcoder.typepad.com/blog/2018/09/swallowing-stairsautomation-warnings.html">modified the StairsAutomations sample to avoid displaying any warnings</a>,
so the second item listed above is handled.</p>

<p>Today, we'll address most of the remaining ones:</p>

<p>We'll move the execution away from an external command and trigger it from the <code>ApplicationInitialized</code> event instead.</p>

<p>In fact, we'll entirely remove all references to <code>RevitAPIUI.dll</code>.</p>

<p>We'll also open the model file ourselves.</p>

<p>In Forge, a different system will be used, so you cannot later use the <code>ApplicationInitialized</code> event there.
Design Automation for Revit continues doing setup past the point at which <code>ApplicationInitialized</code> is raised.
For the time being, though, we can use it to just mimic the 'run automatically' behaviour.</p>

<h4><a name="3"></a> Implementing DB Application and Accessing the Revit Application Object</h4>

<p>The trickiest step for me was finding out how to access the Revit <code>Application</code> object using only the <code>IExternalDBApplication</code> interface, because that is apparently not documented anywhere at all.</p>

<p>I finally found the solution in a previous blog post
on <a href="http://thebuildingcoder.typepad.com/blog/2015/03/automatically-open-a-project-on-startup.html">automatically opening a project on start-up</a> &ndash;
the <code>sender</code> argument passed in to the <code>ApplicationInitialized</code> can be cast to <code>Application</code>.</p>

<p>That enables me to implement the entirely UI-independent DB application to drive the stairs creation utility class like this:</p>

<pre class="code">
<span style="color:blue;">using</span>&nbsp;System;
<span style="color:blue;">using</span>&nbsp;Autodesk.Revit.DB;
<span style="color:blue;">using</span>&nbsp;Autodesk.Revit.DB.Events;
<span style="color:blue;">using</span>&nbsp;Autodesk.Revit.ApplicationServices;

<span style="color:blue;">namespace</span>&nbsp;Revit.SDK.Samples.StairsAutomation.CS
{
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Implement&nbsp;the&nbsp;Revit&nbsp;add-in&nbsp;IExternalDBApplication&nbsp;interface</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">DbApp</span>&nbsp;:&nbsp;<span style="color:#2b91af;">IExternalDBApplication</span>
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;_model_path&nbsp;=&nbsp;<span style="color:#a31515;">&quot;C:/a/vs/StairsAutomation/CS/Stairs_automation_2019_1.rvt&quot;</span>;

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;The&nbsp;implementation&nbsp;of&nbsp;the&nbsp;automatic&nbsp;stairs&nbsp;creation.</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">void</span>&nbsp;Execute(&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;document&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Create&nbsp;an&nbsp;automation&nbsp;utility&nbsp;with&nbsp;a&nbsp;hardcoded&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;stairs&nbsp;configuration&nbsp;number</span>

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">StairsAutomationUtility</span>&nbsp;utility
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:#2b91af;">StairsAutomationUtility</span>.Create(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;document,&nbsp;stairsConfigs[stairsIndex]&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Generate&nbsp;the&nbsp;stairs</span>

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;utility.GenerateStairs();

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;++stairsIndex;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;stairsIndex&nbsp;&gt;&nbsp;4&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;stairsIndex&nbsp;=&nbsp;0;
&nbsp;&nbsp;&nbsp;&nbsp;}

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">void</span>&nbsp;OnApplicationInitialized(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">object</span>&nbsp;sender,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ApplicationInitializedEventArgs</span>&nbsp;e&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Sender&nbsp;is&nbsp;an&nbsp;Application&nbsp;instance:</span>

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Application</span>&nbsp;app&nbsp;=&nbsp;sender&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">Application</span>;

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc&nbsp;=&nbsp;app.OpenDocumentFile(&nbsp;_model_path&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;doc&nbsp;==&nbsp;<span style="color:blue;">null</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">throw</span>&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">InvalidOperationException</span>(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;Could&nbsp;not&nbsp;open&nbsp;document.&quot;</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Execute(&nbsp;doc&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:#2b91af;">ExternalDBApplicationResult</span>&nbsp;OnStartup(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ControlledApplication</span>&nbsp;a&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;ApplicationInitialized&nbsp;cannot&nbsp;be&nbsp;used&nbsp;in&nbsp;Forge!</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;a.ApplicationInitialized&nbsp;+=&nbsp;OnApplicationInitialized;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:#2b91af;">ExternalDBApplicationResult</span>.Succeeded;
&nbsp;&nbsp;&nbsp;&nbsp;}

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:#2b91af;">ExternalDBApplicationResult</span>&nbsp;OnShutdown(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ControlledApplication</span>&nbsp;a&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:#2b91af;">ExternalDBApplicationResult</span>.Succeeded;
&nbsp;&nbsp;&nbsp;&nbsp;}

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">int</span>&nbsp;stairsIndex&nbsp;=&nbsp;0;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">int</span>[]&nbsp;stairsConfigs&nbsp;=&nbsp;{&nbsp;0,&nbsp;3,&nbsp;4,&nbsp;1,&nbsp;2&nbsp;};
&nbsp;&nbsp;}
}
</pre>

<p>Note the absence of all references to the <code>Autodesk.Revit.UI</code> namespace.</p>

<h4><a name="4"></a> DB Application Add-In Manifest</h4>

<p>Now that we implement no external command, Revit complains that no external command is found:</p>

<p><center></p>

<p><a class="asset-img-link"  href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833022ad3b3e085200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833022ad3b3e085200b img-responsive" style="width: 366px; display: block; margin-left: auto; margin-right: auto;" alt="External command not found" title="External command not found" src="/assets/image_157cb2.jpg" /></a><br /></p>

<p></center></p>

<p>That makes perfect sense, of course.</p>

<p>We need to adapt the add-in manifest and inform Revit that we are loading a DB application instead.</p>

<p>As an external application, it requires a <code>Name</code> node:</p>

<p><center></p>

<p><a class="asset-img-link"  href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833022ad39444a9200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833022ad39444a9200d img-responsive" style="width: 369px; display: block; margin-left: auto; margin-right: auto;" alt="External application requires a Name node" title="External application requires a Name node" src="/assets/image_deac6c.jpg" /></a><br /></p>

<p></center></p>

<p>We end up with the following add-in manifest file:</p>

<pre class="code">
<span style="color:blue;">&lt;?</span><span style="color:#a31515;">xml</span><span style="color:blue;">&nbsp;</span><span style="color:red;">version</span><span style="color:blue;">=</span>&quot;<span style="color:blue;">1.0</span>&quot;<span style="color:blue;">&nbsp;</span><span style="color:red;">encoding</span><span style="color:blue;">=</span>&quot;<span style="color:blue;">utf-8</span>&quot;<span style="color:blue;">?&gt;</span>
<span style="color:blue;">&lt;</span><span style="color:#a31515;">RevitAddIns</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">AddIn</span><span style="color:blue;">&nbsp;</span><span style="color:red;">Type</span><span style="color:blue;">=</span>&quot;<span style="color:blue;">DBApplication</span>&quot;<span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">Assembly</span><span style="color:blue;">&gt;</span>StairsAutomation.dll<span style="color:blue;">&lt;/</span><span style="color:#a31515;">Assembly</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">ClientId</span><span style="color:blue;">&gt;</span>4ce08562-a2e1-4cbf-816d-4923e1363a21<span style="color:blue;">&lt;/</span><span style="color:#a31515;">ClientId</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">FullClassName</span><span style="color:blue;">&gt;</span>Revit.SDK.Samples.StairsAutomation.CS.DbApp<span style="color:blue;">&lt;/</span><span style="color:#a31515;">FullClassName</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">Name</span><span style="color:blue;">&gt;</span>StairsAutomation<span style="color:blue;">&lt;/</span><span style="color:#a31515;">Name</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">Description</span><span style="color:blue;">&gt;</span>A&nbsp;utility&nbsp;sample&nbsp;that&nbsp;creates&nbsp;a&nbsp;series&nbsp;of&nbsp;stairs,&nbsp;stairs&nbsp;runs&nbsp;and&nbsp;stairs&nbsp;landings&nbsp;configurations&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;based&nbsp;upon&nbsp;predefined&nbsp;rules&nbsp;and&nbsp;parameters.<span style="color:blue;">&lt;/</span><span style="color:#a31515;">Description</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">VendorId</span><span style="color:blue;">&gt;</span>ADSK<span style="color:blue;">&lt;/</span><span style="color:#a31515;">VendorId</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&lt;/</span><span style="color:#a31515;">AddIn</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&lt;/</span><span style="color:#a31515;">RevitAddIns</span><span style="color:blue;">&gt;</span>
</pre>

<h4><a name="5"></a> Next Steps</h4>

<p>There is not much more left to do now, really.</p>

<p>These are all that come to mind off-hand:</p>

<ul>
<li>Save the resulting model</li>
<li>Do we need to shut down Revit when we are done?</li>
<li>Read the required stair configuration from a JSON input file</li>
<li>Test in the real Forge environment</li>
</ul>

<p>The first three we can address right away...</p>

<h4><a name="6"></a> Download</h4>

<p>Oops, I almost forgot:
You can download the modified SDK sample and examine every step I took in modifying it so far from and in
the <a href="https://github.com/jeremytammik/StairsAutomation">StairsAutomation GitHub repository</a>.</p>
