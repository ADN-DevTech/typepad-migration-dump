---
layout: "post"
title: "Ribbon Panel Title Conflict"
date: "2011-03-09 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2011"
  - "Automation"
  - "Data Access"
  - "Ribbon"
  - "User Interface"
original_url: "https://thebuildingcoder.typepad.com/blog/2011/03/ribbon-panel-title-conflict.html "
typepad_basename: "ribbon-panel-title-conflict"
typepad_status: "Publish"
---

<p>Here is another exploration into the inner workings of the Revit ribbon by Rudolf Honke of

<a href="http://www.acadgraph.de">
acadGraph CADstudio GmbH</a>. 

He says:

<p>If you try to add a new Ribbon panel whose title is already used by another Add-In panel, it will throw an exception.
You therefore have to ensure that no other third party panel uses the same text as you do.

<p>The Revit API method GetRibbonPanels will not find panels that are not yet loaded, and there is no fixed order in which plug-in will be loaded.
Therefore, even if a plug-in is installed after yours, it might still be loaded before and could have 'occupied' your 'own' panel title, which would cause your plug-in to fail if the panel title conflict is not handled appropriately, for instance by renaming it on the fly.

<p>Here is an example in which I loaded RevitLookup twice and renamed the displayed text of the second instance:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833014e5fb8a1a5970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833014e5fb8a1a5970c" alt="Ribbon panel naming conflict resolution" title="Ribbon panel naming conflict resolution" src="/assets/image_9d0f05.jpg" border="0" /></a> <br />

</center>

<p>This may be a real concern, especially if your panel title is something generic such as 'Tools'.

<p>To make sure, you need to iterate through the already loaded panels and adapt your title if necessary before creating it.

<p>This affects the 'Title' property; I'm not sure whether the 'Name' needs to be unique, too.

<p>Here is some sample code to illustrate this;
in your external application, you can define a global variable:

<pre class="code">
&nbsp; <span class="blue">private</span> <span class="teal">UIControlledApplication</span> m_revit = <span class="blue">null</span>;
</pre>

<p>It can be initialised when starting the application:

<pre class="code">
&nbsp; <span class="blue">public</span> <span class="teal">Result</span> OnStartup( <span class="teal">UIControlledApplication</span> a )
&nbsp; {
&nbsp; &nbsp; m_revit = a;
&nbsp;
&nbsp; &nbsp; <span class="green">/*</span>
<span class="green">&nbsp; &nbsp; (do something useful here)</span>
<span class="green">&nbsp; &nbsp; */</span>
&nbsp;
&nbsp; &nbsp; <span class="blue">return</span> <span class="teal">Result</span>.Succeeded;
&nbsp; }
</pre>

<p>Then we can implement the following method to check whether a panel title has already been taken:

<pre class="code">
&nbsp; <span class="blue">private</span> <span class="blue">bool</span> IsPanelTitleUsed(
&nbsp; &nbsp; <span class="blue">string</span> panelTitle )
&nbsp; {
&nbsp; &nbsp; <span class="teal">List</span>&lt;<span class="teal">RibbonPanel</span>&gt; loadedPluginPanels
&nbsp; &nbsp; &nbsp; = m_revit.GetRibbonPanels();
&nbsp;
&nbsp; &nbsp; <span class="blue">foreach</span>( <span class="teal">RibbonPanel</span> p <span class="blue">in</span> loadedPluginPanels )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( p.Title.Equals( panelTitle ) )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">return</span> <span class="blue">true</span>;
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; }
&nbsp; &nbsp; <span class="blue">return</span> <span class="blue">false</span>;
&nbsp; }
</pre>

<p>If we decide to change our panel title, for example by appending an integer counter suffix, we obviously have to continue checking whether it is available until we find a title that has not been taken yet.

<p>The cause of this exception when trying to create RibbonPanels of the same name is visible in the StackTrace:

<ul>
<li>Autodesk.Revit.UI.UIApplication.verifyPanelNameExclusive(Tab tab, String newPanelName)
<li>Autodesk.Revit.UI.UIApplication.CreateRibbonPanel(Tab tab, String panelName)
<li>Autodesk.Revit.UI.UIApplication.CreateRibbonPanel(String panelName)
<li>Autodesk.Revit.UI.UIControlledApplication.CreateRibbonPanel(String panelName)
</ul>

<p>After I called the CreateRibbonPanel method, the Title property is not initialized:

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833014e5fb8a0e6970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833014e5fb8a0e6970c image-full" alt="Ribbon panel name and null title" title="Ribbon panel name and null title" src="/assets/image_96a4c0.jpg" border="0" /></a> <br />

</center>

<p>The title value stays null, even if you think that you see it in the UI.

<p>So <b>the name is your title</b> except you assign a different one:

<pre class="code">
rvtRibbonPanel.Title = "Dummy";
</pre>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833014e86938778970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833014e86938778970d image-full" alt="Ribbon panel name and title differ" title="Ribbon panel name and title differ" src="/assets/image_ca6478.jpg" border="0" /></a> <br />

</center>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330147e3138737970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330147e3138737970b image-full" alt="Ribbon panel name and title differ" title="Ribbon panel name and title differ" src="/assets/image_152ec8.jpg" border="0" /></a> <br />

</center>

<p>Having different names, titles <b>can</b> consist of the same string:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833014e5fb89f3a970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833014e5fb89f3a970c" alt="Ribbon panels with identical names" title="Ribbon panels with identical names" src="/assets/image_75fcb9.jpg" border="0" /></a> <br />

</center>

<p>Using UISpy, by the way, shows that the Revit AutomationElements can be retrieved by name (see AutomationId), not by visible text.
Here are the properties of the left panel in the image above:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833014e869385b8970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833014e869385b8970d image-full" alt="Ribbon panel properties in UISpy" title="Ribbon panel properties in UISpy" src="/assets/image_b9acad.jpg" border="0" /></a> <br />

</center>

<p>Here are the right-hand panel properties:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833014e86938465970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833014e86938465970d image-full" alt="Ribbon panel properties in UISpy" title="Ribbon panel properties in UISpy" src="/assets/image_5fb89f.jpg" border="0" /></a> <br />

</center>

<p>For both of these, the Name property is an empty string, while the AutomationId property still distinguishes them as "CustomCtrl_%ADD_INS_TAB%RevitLookup" versus "CustomCtrl_%ADD_INS_TAB%RevitLookup 2".

<p>Many thanks to Rudolf for this important warning and interesting inside-the-ribbon information.
