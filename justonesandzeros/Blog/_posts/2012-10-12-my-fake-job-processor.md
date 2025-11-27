---
layout: "post"
title: "My Fake Job Processor"
date: "2012-10-12 08:25:24"
author: "Doug Redmond"
categories:
  - "Hack"
original_url: "https://justonesandzeros.typepad.com/blog/2012/10/my-fake-job-processor.html "
typepad_basename: "my-fake-job-processor"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Hack3.png" /></p>
<p>This article is such a hack that I created a new category to mark the occasion.&#0160; It goes without saying (but I’m going to say it anyway) that Autodesk isn’t going to provide support for what I’m about to say.</p>
<p>In talking with people about various Vault customizations, I find that sometimes it would be useful to get at functionality provided by a job handler.&#0160; For example, you want to create a DWF.&#0160; Theoretically, it should be possible to call directly into the DWF create job handler, so I decided to play around a bit and see if I could get it working for real.</p>
<p>Before I go into the details, let’s get abstract.&#0160; Plug-in architectures usually follow the same pattern.&#0160; There is the the hosting app, the plug-in and an interface connecting the two.&#0160; Each side is not supposed to care about what’s on the other side of the interface.</p>
<p>My test app takes advantage of that abstraction.&#0160; The DWF handler, for example shouldn’t care if it’s running inside Job Processor or not.&#0160; The handler just processes jobs.&#0160; So all I need to do is pretend to be the job processor.&#0160; I load the handler, create the DWF create job, pass it to the handler, and get a DWF file as a result.... in theory.</p>
<hr noshade="noshade" style="color: #deff59;" />
<p><strong>My test app      <br /></strong>The first step is to figure out which DLL to load.&#0160; Looking at JobProcessor.exe.config, I see a bunch of &lt;jobHandler&gt; elements, which tell me which job types are handled by which classes.&#0160; For this example, I decided to create DWF files.&#0160; The .config file tells me that they are all handled by the class “Connectivity.Explorer.JobHandlerDwfPublish.InventorDwfJobHandler” in the assembly “Connectivity.Explorer.JobHandlerDwfPublish”.&#0160; So let’s load Connectivity.Explorer.JobHandlerDwfPublish.dll using the System.Reflection libraries.</p>
<table border="1" cellpadding="2" cellspacing="0" width="470">
<tbody>
<tr>
<td valign="top" width="470">
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="font-size: 9pt; color: #008000;">// load the assembly</span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span style="font-family: Consolas;"><span><span style="color: #2b91af;"><span style="font-size: 9pt;">Assembly</span></span></span><span><span style="font-size: 9pt;"><span style="color: #000000;"> assembly = </span><span><span style="color: #2b91af;">Assembly</span></span><span style="color: #000000;">.LoadFile(</span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="color: #000000;"><span style="mso-spacerun: yes;"><span style="font-size: 9pt;">&#0160;&#0160;&#0160; </span></span><span style="font-size: 9pt;">System.IO.</span></span><span style="font-size: 9pt;"><span><span style="color: #2b91af;">Path</span></span><span style="color: #000000;">.Combine(EXPLORER_FOLDER,</span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="mso-spacerun: yes;"><span style="color: #000000;"><span style="font-size: 9pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span></span><span style="font-size: 9pt;"><span><span style="color: #a31515;">&quot;Connectivity.Explorer.JobHandlerDwfPublish.dll&quot;</span></span><span style="color: #000000;">));</span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="font-size: 9pt; color: #000000;">&#0160;</span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="font-size: 9pt; color: #008000;">// create a new handler object</span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span style="font-family: Consolas;"><span><span style="color: #2b91af;"><span style="font-size: 9pt;">IJobHandler</span></span></span><span><span style="font-size: 9pt; color: #000000;"> handler = assembly.CreateInstance(</span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="mso-spacerun: yes;"><span style="color: #000000;"><span style="font-size: 9pt;">&#0160;&#0160;&#0160; </span></span></span><span style="font-size: xx-small;"><span><span style="color: #a31515;">&quot;Connectivity.Explorer.JobHandlerDwfPublish.InventorDwfJobHandler&quot;</span></span><span style="color: #000000;">,</span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="mso-spacerun: yes;"><span style="color: #000000;"><span style="font-size: 9pt;">&#0160;&#0160;&#0160; </span></span></span><span style="font-size: 9pt;"><span><span style="color: #0000ff;">true</span></span><span style="color: #000000;">, </span><span><span style="color: #2b91af;">BindingFlags</span></span><span style="color: #000000;">.CreateInstance, </span><span><span style="color: #0000ff;">null</span></span><span style="color: #000000;">, </span><span><span style="color: #0000ff;">null</span></span><span style="color: #000000;">, </span><span><span style="color: #0000ff;">null</span></span><span style="color: #000000;">, </span><span><span style="color: #0000ff;">null</span></span><span style="color: #000000;">)</span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="mso-spacerun: yes;"><span style="color: #000000;"><span style="font-size: 9pt;">&#0160;&#0160;&#0160; </span></span></span><span style="font-size: 9pt;"><span><span style="color: #0000ff;">as</span></span><span style="color: #000000;"> </span><span><span style="color: #2b91af;">IJobHandler</span></span><span style="color: #000000;">;</span></span></span></span></p>
</td>
</tr>
</tbody>
</table>
<p>Now I have the IJobHandler object, I need to pass in an IJob (which tells the handler which operation to run on which file) and an IJobProcessorServices (which provides context information).&#0160; The IJobProcessorServices class was pretty easy to implement just by looking at the interface.&#0160; For the IJob implementation, I needed to figure out what the parameter list was.&#0160; I found this out by queuing up a DWF job from the Vault client.&#0160; Then I used the API to see what the parameters were on the job.</p>
<table border="1" cellpadding="2" cellspacing="0" width="470">
<tbody>
<tr>
<td valign="top" width="470">
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="font-size: 9pt; color: #008000;">// create the context</span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span style="font-family: Consolas;"><span><span style="color: #2b91af;"><span style="font-size: 9pt;">IJobProcessorServices</span></span></span><span><span style="font-size: 9pt;"><span style="color: #000000;"> context = </span><span><span style="color: #0000ff;">new</span></span><span style="color: #000000;"> </span><span><span style="color: #2b91af;">JobContext</span></span><span style="color: #000000;">(</span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="color: #000000;"><span style="mso-spacerun: yes;"><span style="font-size: 9pt;">&#0160;&#0160;&#0160; </span></span><span style="font-size: 9pt;">vaultName, mgr.SecurityService.SecurityHeader.UserId,</span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="color: #000000;"><span style="mso-spacerun: yes;"><span style="font-size: 9pt;">&#0160;&#0160;&#0160; </span></span><span style="font-size: 9pt;">mgr.SecurityService.SecurityHeader.Ticket,</span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="color: #000000;"><span style="mso-spacerun: yes;"><span style="font-size: 9pt;">&#0160;&#0160;&#0160; </span></span><span style="font-size: 9pt;">uri);</span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="font-size: 9pt; color: #000000;">&#0160;</span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="font-size: 9pt; color: #008000;">// create the job</span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span style="font-family: Consolas;"><span><span style="color: #2b91af;"><span style="font-size: 9pt;">IJob</span></span></span><span><span style="font-size: 9pt;"><span style="color: #000000;"> job = </span><span><span style="color: #0000ff;">new</span></span><span style="color: #000000;"> </span><span><span style="color: #2b91af;">DwfJob</span></span><span style="color: #000000;">(vaultName, partFile.Id);</span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="font-size: 9pt; color: #000000;">&#0160;</span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="font-size: 9pt; color: #008000;">// run the job</span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="font-size: 9pt; color: #000000;">handler.Execute(context, job);</span></span></span></p>
</td>
</tr>
</tbody>
</table>
<p><a href="http://justonesandzeros.typepad.com/images/2012/FakeJobProcessor/FakeJobProcessor.zip" target="_blank">Click here to download the entire project</a></p>
<hr noshade="noshade" style="color: #deff59;" />
<p><strong>Results      <br /></strong>So did the app actually work?&#0160; Yes and no.&#0160; At first, my app was a stand-alone EXE.&#0160; The app would always fail when running the Execute command.&#0160; Remember how, in theory, the handler shouldn’t care about the hosting app?&#0160; That’s not true for the DWF handler.&#0160; It definitely cares about the app context and refuses to run if the environment hasn’t been initialized properly.</p>
<p>My next attempt was to run it as a custom Vault Explorer command.&#0160; The idea is that Vault Explorer will perform whatever operation is needed to make the job handler happy.&#0160; My command actually worked.&#0160; There is brief dialog that pops up and disappears during the Execute.&#0160; But otherwise, it runs as expected.&#0160; </p>
<p>I have no idea if SyncProperties or UpdateRevisionBlock handlers will work with this hack.</p>
<p><img alt="" src="/assets/Hack3-1.png" /></p>
