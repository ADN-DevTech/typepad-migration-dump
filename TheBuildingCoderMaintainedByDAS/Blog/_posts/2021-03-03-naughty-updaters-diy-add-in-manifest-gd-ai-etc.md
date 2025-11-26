---
layout: "post"
title: "Naughty Updaters, DIY Add-In Manifest, GD, AI, etc."
date: "2021-03-03 05:00:00"
author: "Jeremy Tammik"
categories:
  - "AI"
  - "Deployment"
  - "DMU"
  - "Dynamo"
  - "Failure"
  - "Installation"
original_url: "https://thebuildingcoder.typepad.com/blog/2021/03/naughty-updaters-diy-add-in-manifest-gd-ai-etc.html "
typepad_basename: "naughty-updaters-diy-add-in-manifest-gd-ai-etc"
typepad_status: "Publish"
---

<p>Lots of exciting discussion going on in the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> and elsewhere:</p>

<ul>
<li><a href="#2">No redemption for naughty updaters</a></li>
<li><a href="#3">DIY Add-in manifest</a></li>
<li><a href="#4">Generative design in C&#35;</a></li>
<li><a href="#5">AI identifies and classifies BIM elements in 2D sketch</a></li>
</ul>

<h4><a name="2"></a> No Redemption for Naughty Updaters</h4>

<p>An interesting aspect of the DMU dynamic model updater framework was raised in
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/notify-when-iupdater-is-disabled-by-revit-error-amp-re-enable/m-p/10114949">notifying when <code>IUpdater</code> is disabled by Revit error and re-enabling</a>,
and clarified for us by Scott Conover:</p>

<p><strong>Question:</strong> Is there any way to be notified when an IUpdater is disabled by Revit error and re-enable it?</p>

<p>I have a several IUpdaters in my add-in of which a user can disable or enable by clicking an associated button. For example, there is a pushbutton A for IUpdaterA, which the Push Button image shows the status of the IUpdater.</p>

<p>On PushButton Click:</p>

<pre class="code">
  if (UpdaterRegistry.IsUpdaterRegistered(updater.GetUpdaterId()))
  {
    UpdaterRegistry.UnregisterUpdater(updater.GetUpdaterId());
    pushButtonA.LargeImage = Off;
  }
  else
  {
    UpdaterRegistry.RegisterUpdater(updater);
    UpdaterRegistry.EnableUpdater(updater.GetUpdaterId());
    // ... Add Triggers, etc. (omitted here for conciseness)
    pushButtonA.LargeImage = On;
  }
</pre>

<p>This works fine when the user manually clicks and turns on or off the IUpdater.
However, when something during the session or project environment causes the IUpdater execution to fail throwing an error, the button does not respond to re-enabling the IUpdater after it has been disabled:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833027880194ddd200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833027880194ddd200d img-responsive" alt="Updater experienced a problem" title="Updater experienced a problem" src="/assets/image_54ec1a.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>So, two questions:</p>

<ul>
<li>When this error is thrown and disable Updater is clicked, is there a way to tie this back to change the pushButtonA img to OFF?</li>
<li>As seen in the code, <code>UpdaterRegistry.EnableUpdater</code> is used but it doesn't seem to enable the <code>IUpdater</code> back up after it has been disabled through the error dialog. How can one re-enable it?</li>
</ul>

<p>This is not to say one should always want to re-enable a disabled IUpdater as there was a reason it was disabled, but in some cases under discretion it may be due to something resolvable like loading in a missing family that was needed, etc.
In those situations, it would be ideal to resolve the missing item, and re-enable the IUpdater back up.</p>

<p>Thank you.</p>

<p><strong>Answer:</strong> Thank you for the interesting question.</p>

<p>If all else fails, have you tried to unregister the updater and reinitialise it completely from scratch?</p>

<p>Or, even more extremely, maybe even change its GUID, so that every failed updater GUID is discarded, and a new one issued on every failure?</p>

<p>Anyway, I have asked the development team whether they have any constructive suggestions for you.</p>

<p>They respond: this is asking the question in the wrong way.</p>

<p>If it's critical to keep the Updater functional, it's on the implementer to ensure that exceptions are not passed back to Revit.</p>

<p>Of course, there are runtime things (System exceptions) that they may not want to catch and deal with, but if the exceptions are thrown from Revit API calls when the updater tries to do its work, I'd suggest the updater catch and deal with them.</p>

<p>It may be that once a call-back or interface class starts throwing exceptions, it goes put on the "naughty list".</p>

<p>There may be no way to recover from that in the current session.</p>

<p>Richard <a href="https://forums.autodesk.com/t5/user/viewprofilepage/user-id/1035859">RPThomas108</a> Thomas added an explanation of how the current 'naughty list' approach disabling the updater may lead to (the most knowledgeable) people not using DMU at all:</p>

<p>The only possible approach perhaps, since that is a failure, would be to deal with the failure:</p>

<ul>
<li>Autodesk.Revit.DB.BuiltInFailures.DocumentFailures.DUMisbehavingUpdater</li>
</ul>

<p>If you are able to cancel, rather than user doing it (not sure), you are then able to the disable your updater yourself and know about it.</p>

<p>I've long found this approach to 'suspending' rather than disabling DMU's very problematic.
For example, people have asked me 'why can't you make it so pile coordinates are updated automatically when they are moved?'
In theory, I know I could use a DMU for this, but what happens if it gets disabled halfway through an alteration and the user assumes something is being updated when it is not?
I then potentially have a pile schedule with incorrect coordinates being sent out and the distinct possibility of very expensive work being done in the wrong place (I hope some sanity check would prevent that but you never know).
These are the users that say, "I'm just the guy pressing the button (your button)!"</p>

<p>You need some fail-safe approach, really.
So, I prefer to press a button at a certain point to do a task, then check the results against previous before issue.
Even for the most simplistic code DMU's have the potential to be disabled due to complex interactions, i.e., you can account for your own code, but not that of other DMU's by others and the timings of such (the impacts for the states of elements these have between your interactions).</p>

<p>Thanks to Scott and Richard for their input on this.</p>

<h4><a name="3"></a> DIY Add-In Manifest</h4>

<p>Joshua Lumley added
a <a href="https://thebuildingcoder.typepad.com/blog/2021/02/addin-file-learning-python-and-ifcjs.html#comment-5276653852">comment</a> on
the discussion on
the <a href="https://thebuildingcoder.typepad.com/blog/2021/02/addin-file-learning-python-and-ifcjs.html#2.1">add-in manifest using a relative path</a> showing
how to generate your own add-in manifest XML <code>addin</code> file on the fly:</p>

<blockquote>
  <p>This is the code I use to make the manifest from the CustomMethods of the Deployment Project.</p>
</blockquote>

<pre class="code">
<span style="color:blue;">void</span>&nbsp;GenerateAddInManifest(
&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;dll_folder,
&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;dll_name&nbsp;)
{
&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;sDir&nbsp;=&nbsp;<span style="color:#2b91af;">Environment</span>.GetFolderPath(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Environment</span>.<span style="color:#2b91af;">SpecialFolder</span>.CommonApplicationData&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;\\Autodesk\\Revit\\Addins&quot;</span>;

&nbsp;&nbsp;<span style="color:blue;">bool</span>&nbsp;exists&nbsp;=&nbsp;<span style="color:#2b91af;">Directory</span>.Exists(&nbsp;sDir&nbsp;);

&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;!exists&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Directory</span>.CreateDirectory(&nbsp;sDir&nbsp;);

&nbsp;&nbsp;<span style="color:#2b91af;">XElement</span>&nbsp;XElementAddIn&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XElement</span>(&nbsp;<span style="color:#a31515;">&quot;AddIn&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XAttribute</span>(&nbsp;<span style="color:#a31515;">&quot;Type&quot;</span>,&nbsp;<span style="color:#a31515;">&quot;Application&quot;</span>&nbsp;)&nbsp;);

&nbsp;&nbsp;XElementAddIn.Add(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XElement</span>(&nbsp;<span style="color:#a31515;">&quot;Name&quot;</span>,&nbsp;dll_name&nbsp;)&nbsp;);
&nbsp;&nbsp;XElementAddIn.Add(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XElement</span>(&nbsp;<span style="color:#a31515;">&quot;Assembly&quot;</span>,&nbsp;dll_folder&nbsp;+&nbsp;dll_name&nbsp;+&nbsp;<span style="color:#a31515;">&quot;.dll&quot;</span>&nbsp;)&nbsp;);
&nbsp;&nbsp;XElementAddIn.Add(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XElement</span>(&nbsp;<span style="color:#a31515;">&quot;AddInId&quot;</span>,&nbsp;<span style="color:#2b91af;">Guid</span>.NewGuid().ToString()&nbsp;)&nbsp;);
&nbsp;&nbsp;XElementAddIn.Add(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XElement</span>(&nbsp;<span style="color:#a31515;">&quot;FullClassName&quot;</span>,&nbsp;dll_name&nbsp;+&nbsp;<span style="color:#a31515;">&quot;.SettingUpRibbon&quot;</span>&nbsp;)&nbsp;);
&nbsp;&nbsp;XElementAddIn.Add(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XElement</span>(&nbsp;<span style="color:#a31515;">&quot;VendorId&quot;</span>,&nbsp;<span style="color:#a31515;">&quot;01&quot;</span>&nbsp;)&nbsp;);
&nbsp;&nbsp;XElementAddIn.Add(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XElement</span>(&nbsp;<span style="color:#a31515;">&quot;VendorDescription&quot;</span>,&nbsp;<span style="color:#a31515;">&quot;Joshua&nbsp;Lumley&nbsp;Secrets,&nbsp;twitter&nbsp;@joshnewzealand&quot;</span>&nbsp;)&nbsp;);

&nbsp;&nbsp;<span style="color:#2b91af;">XElement</span>&nbsp;XElementRevitAddIns&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XElement</span>(&nbsp;<span style="color:#a31515;">&quot;RevitAddIns&quot;</span>&nbsp;);
&nbsp;&nbsp;XElementRevitAddIns.Add(&nbsp;XElementAddIn&nbsp;);

&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:blue;">string</span>&nbsp;d&nbsp;<span style="color:blue;">in</span>&nbsp;<span style="color:#2b91af;">Directory</span>.GetDirectories(&nbsp;sDir&nbsp;)&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;myString_ManifestPath&nbsp;=&nbsp;d&nbsp;+&nbsp;<span style="color:#a31515;">&quot;\\&quot;</span>&nbsp;+&nbsp;dll_name&nbsp;+&nbsp;<span style="color:#a31515;">&quot;.addin&quot;</span>;

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>[]&nbsp;directories&nbsp;=&nbsp;d.Split(&nbsp;<span style="color:#2b91af;">Path</span>.DirectorySeparatorChar&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;<span style="color:blue;">int</span>.TryParse(&nbsp;directories[&nbsp;directories.Count()&nbsp;-&nbsp;1&nbsp;],
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">out</span>&nbsp;<span style="color:blue;">int</span>&nbsp;myInt_FromTextBox&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Install&nbsp;on&nbsp;version&nbsp;2017&nbsp;and&nbsp;above</span>

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;myInt_FromTextBox&nbsp;&gt;=&nbsp;2017&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XDocument</span>(&nbsp;XElementRevitAddIns&nbsp;).Save(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;myString_ManifestPath&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">else</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;<span style="color:#2b91af;">File</span>.Exists(&nbsp;myString_ManifestPath&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">File</span>.Delete(&nbsp;myString_ManifestPath&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
}
</pre>

<p>Many thanks to Joshua for sharing this DIY approach.
I added it to The Building Coder samples, cf.
the <a href="https://github.com/jeremytammik/the_building_coder_samples/compare/2021.0.150.19...2021.0.150.20">diff to the previous version</a>.</p>

<h4><a name="4"></a> Generative Design in C&#35;</h4>

<p>Fernando Malard, CTO at <a href="http://ofcdesk.com">ofcdesk</a>, brought up an interesting question that an unnamed colleague of mine <!-- Kean Walmsley --> kindly clarified:</p>

<p><strong>Question:</strong> Looking for a suggestion about what route to pursue...</p>

<p>We are creating a Revit plugin for a customer that requires a wall panel tiling system in Revit.</p>

<p>The tiling problem involves lots of optimization variables and it would be perfect to be addressed by a Generative algorithm.</p>

<p>Basically, the plugin would walk through the project walls (inside and outside), perform panel tiling, evaluate, do the genetic operations, repeat, etc.</p>

<p>Is it possible to use Revit GD tools via C# API or is it mandatory to use Dynamo?</p>

<p>I know we could pursue the creation of an SGAII or III algorithm in pure C# but Revit/Forge would give us those extremely helpful tools to visualize design options, parameter graphs, etc.</p>

<p>Any advice here?</p>

<p><strong>Answer:</strong> It does sound like a good use of GD.</p>

<p>That said, the GD feature doesn’t have an automation API: you use Dynamo to define the parametric models that it uses. The Dynamo graph can use C# “zero-touch” nodes, if you want it to &ndash; and people more commonly integrate Python code, when they need to &ndash; but that’s just helping flesh out the logic of the graph, it’s not to automate the overall process.</p>

<p>In case it helps, I made a first pass (which is not at all optimal) at doing
a <a href="https://autode.sk/tiling-graph">floor tiling graph for use with Refinery</a> (<a href="https://thebuildingcoder.typepad.com/files/refinerytiling.zip">^</a>).</p>

<p><strong>Response:</strong> Interesting.</p>

<p>Is it possible to trigger the Dynamo graph from Revit and have it running in the background? </p>

<p>Maybe we could create the manager app in C# and call the graph as we need without exposing Dynamo UI to the end user.</p>

<p>I just want to avoid any complexity to the user.</p>

<p>Thanks!</p>

<p><strong>Answer:</strong> The user doesn’t see Dynamo at all: the GD feature does exactly what you’ve described (actually that’s it’s whole point &nbsp; :-)).</p>

<p><strong>Response:</strong> It seems your sample graph loads ok but it shows a missing PolyCurve custom node:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833026bdec16640200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833026bdec16640200c image-full img-responsive" alt="Generative design in C#" title="Generative design in C#" src="/assets/image_9ecfa0.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>I’m running Revit 2021.1.2, Dynamo Core 2.6.1.8786 and Dynamo Revit 2.6.1.8850.</p>

<p>Any additional package I need to install?</p>

<p><strong>Answer:</strong> Ampersand, I believe.</p>

<p><strong>Response:</strong> Exactly, thanks!</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833026bdec16647200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833026bdec16647200c image-full img-responsive" alt="Generative design in C#" title="Generative design in C#" src="/assets/image_d5d744.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<h4><a name="5"></a> AI Identifies and Classifies BIM Elements in 2D Sketch</h4>

<p>Before closing, I'd like to pick up a couple of interesting miscellaneous items I happened to run into, starting with
a <a href="https://www.linkedin.com/posts/mohamed-adel-a3b26160_autodesk-revit-modeling-activity-6769520499216158720-AFS7">LinkedIn post</a>
by Mohamed Adel, BIM Coordinator at SEPCO Electric Power Construction Corporation, Egypt:</p>

<blockquote>
  <p>Using machine learning in modelling is quite an approach which definitely will save hours of work.
  I developed an application that can automatically model from linked AutoCAD file in Revit.
  Using machine learning concept which guide the Revit API to model the proper element.
  In his video, a CAD contains only polylines with nothing to distinguish them from each other. 
  The user provides some initial information in the UI like which family to use in the loadable families, type of system families and working levels.
  For any element it will automatically duplicate the family to a new type with the right dimension that fits the linked polyline.
  For the depth of the floors, the user will choose which type.</p>
</blockquote>

<p><center>
<video width="100%" preload="metadata" muted="muted" src="https://dms.licdn.com/playlist/C4D05AQGre2tKOAOL2w/mp4-720p-30fp-crf28/0/1613979455733?e=1614873600&amp;v=beta&amp;t=q2FZMnMx01AELeo7dzDOJm7L553O4Dj3cxRS4a3uUEQ" autoplay="autoplay"></video>
</center></p>

<p>It looks pretty neat!</p>

<p>Here are a few more:</p>

<ul>
<li>Why did OS/2 Lose to Windows 3?
<br/><a href="https://www.quora.com/Why-did-IBMs-OS-2-project-lose-to-Microsoft-given-that-IBM-had-much-more-resources-than-Microsoft-at-that-time/answers/12576993">Why did IBM's OS/2 project lose to Microsoft, given that IBM had much more resources than Microsoft at that time?</a></li>
<li>Google Sheets as a REST API and React App &ndash;
<br/><a href="https://www.freecodecamp.org/news/react-and-googlesheets">How to turn Google sheets into a REST API and use it with a React application</a></li>
<li><a href="https://www.freecodecamp.org/news/complete-introduction-to-the-most-useful-javascript-array-methods">JavaScript array methods tutorial &ndash; the most useful methods explained with examples</a></li>
<li><a href="https://github.com/flash-oss/allserver">Allserver</a>, a minimalist multi-transport and multi-protocol simple RPC server and (optional) client</li>
</ul>
