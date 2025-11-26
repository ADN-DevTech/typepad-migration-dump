---
layout: "post"
title: "Tech Summit and More AutoExport Considerations"
date: "2013-06-04 04:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "Algorithm"
  - "Events"
  - "Export"
  - "External"
  - "Idling"
  - "Print"
  - "Utilities"
original_url: "https://thebuildingcoder.typepad.com/blog/2013/06/tech-summit-and-more-autoexport-considerations.html "
typepad_basename: "tech-summit-and-more-autoexport-considerations"
typepad_status: "Publish"
---

<p>Today is the day I have been working towards with my

<a href="http://thebuildingcoder.typepad.com/blog/2013/05/my-cloud-based-2d-editor-implementation-status.html#2">
cloud-based round-trip 2D Revit model editing project</a>,

which I am presenting this afternoon (cf. the 30-minute

<a href="http://thebuildingcoder.typepad.com/room_editor_preview/index.html">pre-recorded video</a>).

<p>Before I get to that, I would like to pass on the following thoughts and suggestions on yesterday's

<a href="http://thebuildingcoder.typepad.com/blog/2013/06/auto-pdf-print-from-revit-2014.html">
auto PDF batch printing system</a> from

Constantin Gherasim of

<a href="http://www.genivar.com">Genivar Inc.</a> on

a similar project of his own, up in use and running for over a year already, expanding on what he already said in his

<a href="http://thebuildingcoder.typepad.com/blog/2013/06/auto-pdf-print-from-revit-2014.html?cid=6a00e553e16897883301901cf01086970b#comment-6a00e553e16897883301901cf01086970b">
comment</a> yesterday:</p>


<a name="2"></a>

<h4>Thoughts on the Genivar AutoExport System</h4>

<p>As I mentioned, this is another Revit "AutoExport" utility, using a similar approach as Dan's: an overnight batch exporter based on the Windows task scheduler and the Revit Idling event.</p>

<p>I would like to share some information on the similarities and differences in the hope that it be useful for others as well.</p>

<p>In order to see what the interface looks like, here is a screen snapshot of it showing the list of files to export:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301901cfa92c6970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301901cfa92c6970b image-full" alt="Autoexport configuration and file list" title="Autoexport configuration and file list" src="/assets/image_139d2e.jpg" border="0" /></a><br />

</center>

<p>Here is the tab handling the DWF format export settings:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330192aab8f87b970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330192aab8f87b970d image-full" alt="Autoexport DWF format export settings" title="Autoexport DWF format export settings" src="/assets/image_c87189.jpg" border="0" /></a><br />

</center>

<p>The utility uses a configuration file, which is stored in XML format:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833019102f0a447970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833019102f0a447970c image-full" alt="XML configuration file" title="XML configuration file" src="/assets/image_4cdda2.jpg" border="0" /></a><br />

</center>

<p>It is controlled through a couple of registry keys which are preconfigured for the task by an AutoIT script:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833019102f0a284970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833019102f0a284970c image-full" alt="Registry keys" title="Registry keys" src="/assets/image_474091.jpg" border="0" /></a><br />

</center>

<p>The first registry key specifies the XML configuration file path.
The second one is a pseudo Boolean flag (in fact it's a string), which controls whether the utility is run or not at the Revit startup (this is necessary when using Revit out of this setup).</p>

<p>The exported formats are PDF, DWG, DXF, DWF and DGN.
As a new extension project, I would like to add support for the IFC format.</p>

<p>The way it works is pretty similar with the tool presented by Mr Tartaglia.
Here is a detailed explanation:</p>

<p>The setup:</p>

<p>1) A scheduled task on a server starts a dedicated remote session each midnight, by logging on a specific account on a specific machine on the network (ideally on the LAN), machine which is dedicated to this task, therefore it's "On" all the time and has all the resources available for the task.</p>

<p>2) When the logon process is completed, a main AutoIT script placed in the StartUp folder of the logged account, triggers successively two other auxiliary AutoIT scripts. The reason for the two other scripts is the fact that two distinct exporting processes are required for two different teams, each with its own configuration file (obviously the setup can be extended, there could be a third and a fourth session, and so on, depending on how long it takes for one process and knowing that the time we have it’s eventually between midnight and 7:00 AM).</p>

<p>3) The first auxiliary script writes to the registry the path for a specific configuration file (which is maintained somewhere on the network), writes "True" to a specific key in the registry named "RunOnStartup", and then it starts Revit (with a dummy document for Revit 2012).</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301901cfa8b46970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301901cfa8b46970b image-full" alt="Workflow 1" title="Workflow 1" src="/assets/image_2576aa.jpg" border="0" /></a><br />

</center>

<p>4) When the Revit Idling event is fired, the AutoExport utility first checks the registry and if the key "RunOnStartup" is "True", it opens the configuration file from the specified path and starts exporting the documents listed in the configuration file to the target folder, specified in the same configuration file (along the export process, it writes a log file).</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301901cfcd1cf970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301901cfcd1cf970b image-full" alt="Workflow 2" title="Workflow 2" src="/assets/image_63dd90.jpg" border="0" /></a><br />

</center>

<p>5) Meanwhile, the first auxiliary script is waiting for the same "RunOnStartup" registry value to be switched to "False" by the AutoExport application, which happens as soon as all the documents listed in the configuration file are processed. As soon as the last exported document is closed, the AutoExport application changes the value for "RunOnStartup" to "False", then the first auxiliary script kills the Revit process by its PID, sends an email (with the last log file attached) to me and to the person which maintains the configuration file, and it stops.</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330192aab8ee7a970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330192aab8ee7a970d image-full" alt="Automated email notification" title="Automated email notification" src="/assets/image_c9cdef.jpg" border="0" /></a><br />

</center>

<p>It's important to mention the fact that leaving the value of "RunOnStartup" key to "False" is the mechanism that ensures that the AutoExport application won't run out of this setup.</p>

<p>6) After a brief delay, the main script triggers the second auxiliary script, which writes to the registry its own values and the whole process (steps 3 to 5) restarts, only with a different configuration file and a different export folder.</p>

<p>7) After the second auxiliary script is over and the Revit process is killed again, the main script closes the remote session on the dedicated machine, which is now waiting for the next AutoExport session in next 24 hours.</p>

<p>This setup has been in place for more than a year now, and basically works without a glitch.
As you can see in the email screen snapshot, it can process up to 25-30 Revit files in one session, which takes about 30 to 40 minutes.</p>

<p>I was quite surprised by the similarity of this project with the one described yesterday, although similar requirements do generally lead to similar solutions.</p>

<p>Then I thought that the approach with the auxiliary scripts through the registry offers a little bit more flexibility and control.</p>

<p>My approach is a little bit more optimistic, since I process multiple RVT files per Task Scheduler Task.</p>

<p>I would also like to mention that maintaining a log file seems to me a good thing to do, which together with the automated email is a nice bonus in my opinion.</p>

<p>These both confer some kind of peace of mind, when you know that you don’t have to wait for somebody to complain that there were problems, since it’s possible to check the night’s job on the mobile phone in the early morning and alert the concerned people!</p>

<p>Of course, these are more or less cosmetic improvements, but when put together, it all seems to me a handy and solid tool.</p>

<p>For me it’s enough that it works and my folks are happy  :-)</p>

<p>Unfortunately this is an in-house application only, so I cannot share it as openly as Mr Tartaglia very graciously did.</p>

<p>I hope these ideas are of use, not necessarily as an improvement, because it depends, to anyone with similar needs and the necessary skills.</p>

<p>I hope I was clear in my explanations and also that you will find this useful.</p>

<p>Thanks for your interest and time.</p>

<p>Thank you, Constantin, for sharing these additional thoughts.</p>


<a name="3"></a>

<h4>Rod's Revit Remote Boot</h4>

<p>In another

<a href="http://thebuildingcoder.typepad.com/blog/2013/06/auto-pdf-print-from-revit-2014.html?cid=6a00e553e168978833019102e8bc2d970c#comment-6a00e553e168978833019102e8bc2d970c">
comment</a> on

yesterday's post,

Barry Ralphs of <a href="http://www.tippingmar.com">Tipping Mar</a> pointed

out an interesting neat and generic use of the Idling event by Rod Howarth:

<a href="http://blog.rodhowarth.com/2011/11/at-last-years-autodesk-university.html">
Revit remote boot</a> &ndash;

a technique to run an API add-in on a particular Revit model without user interaction.


<a name="4"></a>

<h4>Other Important Posts on this Topic</h4>

<p>If you are interested in this kind of automation, you should not miss the old posts on a

<a href="http://thebuildingcoder.typepad.com/blog/2010/11/pattern-for-semi-asynchronous-idling-api-access.html">pattern for semi-asynchronous Idling API access</a> and

<a href="http://thebuildingcoder.typepad.com/blog/2012/11/drive-revit-through-a-wcf-service.html">driving Revit through a WCF service</a>.</p>


<p>Ok, I hope I have not missed anything important now.
Wish me luck for this afternoon!</p>
