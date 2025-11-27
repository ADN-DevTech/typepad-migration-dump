---
layout: "post"
title: "Adding a custom tab to AutoCAD's options dialog using .NET - Part 2"
date: "2008-11-21 06:37:00"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Notification / Events"
  - "User interface"
original_url: "https://www.keanw.com/2008/11/adding-a-cust-1.html "
typepad_basename: "adding-a-cust-1"
typepad_status: "Publish"
---

<p>This post extends the approach shown in <a href="http://through-the-interface.typepad.com/through_the_interface/2008/11/adding-a-custom.html">this previous post</a> to implement a realistic editing and storage mechanism for application settings. It uses the .NET PropertyGrid control to display a custom class, allowing editing of a number of properties. This class is also serializable, which means we can use the .NET Framework to save it out to an XML file on disk. Some readers may have their own approaches to saving custom application settings, whether in the Registry or elsewhere: this post is primarily about displaying properties rather than providing a definitive &quot;how to&quot; for storing custom application settings. I chose a path of relatively low resistance, which will hopefuly prove interesting to some of the people reading the post.</p>

<p><a href="http://msdn.microsoft.com/">MSDN</a> contains <a href="http://msdn.microsoft.com/en-us/library/aa302326.aspx">a useful page</a> on implementing the PropertyGrid in your project, but there are lots of other helpful pages you'll find on <a href="http://www.codeproject.com/">The Code Project</a> and other sites.</p>

<p>Here's a <a href="http://through-the-interface.typepad.com/through_the_interface/files/OptionsDlg2.zip">project containing the code from this post</a>, in case you'd prefer not to create it yourself.</p>

<p>As in the first part of the series, we need to add a User Control to our project. Within this control we'll add a single PropertyGrid, drawn to the full extents of the control (I found that drawing it to fill the container and then setting &quot;Anchor&quot; to &quot;Top, Bottom, Left, Right&quot; worked better than setting &quot;Dock&quot; to &quot;Fill&quot;). Thinking about it, it would probably work just to create a PropertyGrid in code and pass that into the constructor of the TabbedDialogExtension object, but doing it this way allows us to make use of the designer to play around with the control's properties at design-time, rather than making the settings dynamically at runtime.</p>

<p>Here's an idea of what the design should look like of our user control containing the property grid (nothing very impressive or exciting, at this stage - I'm basically just including it for completeness :-):</p>

<p><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/User%20control%20layout%20for%20options%20tab%20with%20property%20grid.png"><img height="316" alt="User control layout for options tab with property grid" src="/assets/User%20control%20layout%20for%20options%20tab%20with%20property%20grid_thumb.png" width="417" border="0" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" /></a> </p>

<p>I customized the layout of the PropertyGrid somewhat - modifying the font and the background colour of the categories - but you will see that from the below snapshots or from the sample project.</p>

<p>Next we need to add some code. Here's the code behind this control, where we use the &quot;value changed&quot; event to signal that our tab's data is &quot;dirty&quot; and may require saving:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">using</span><span style="LINE-HEIGHT: 140%"> System.Windows.Forms;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.ApplicationServices;</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">namespace</span><span style="LINE-HEIGHT: 140%"> OptionsDlg</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">{</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">public</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">partial</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">class</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">OptionsTabControl</span></span><span style="LINE-HEIGHT: 140%"> : </span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">UserControl</span></span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; {</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">public</span><span style="LINE-HEIGHT: 140%"> OptionsTabControl()</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; {</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;InitializeComponent();</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; }</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">private</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">void</span><span style="LINE-HEIGHT: 140%"> propertyGrid_PropertyValueChanged(</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">object</span><span style="LINE-HEIGHT: 140%"> sender,</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;System.Windows.Forms.</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">PropertyValueChangedEventArgs</span></span><span style="LINE-HEIGHT: 140%"> e</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; )</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; {</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">TabbedDialogExtension</span></span><span style="LINE-HEIGHT: 140%">.SetDirty(</span><span style="COLOR: blue; LINE-HEIGHT: 140%">this</span><span style="LINE-HEIGHT: 140%">, </span><span style="COLOR: blue; LINE-HEIGHT: 140%">true</span><span style="LINE-HEIGHT: 140%">);</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; }</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; }</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">}</span></p></div>

<p>Here's the code for the rest of our application's implementation (stored in a separate .cs file - I called mine Application.cs, although you might prefer to split it into AppSettings.cs and Initialization.cs):</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">using</span><span style="LINE-HEIGHT: 140%"> System;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">using</span><span style="LINE-HEIGHT: 140%"> System.Web.UI;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">using</span><span style="LINE-HEIGHT: 140%"> System.IO;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">using</span><span style="LINE-HEIGHT: 140%"> System.Xml.Serialization;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">using</span><span style="LINE-HEIGHT: 140%"> System.ComponentModel;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.ApplicationServices;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.EditorInput;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.Runtime;</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">[assembly:</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; </span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">ExtensionApplication</span></span><span style="LINE-HEIGHT: 140%">(</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">typeof</span><span style="LINE-HEIGHT: 140%">(OneNeedsOptions.</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Initialization</span></span><span style="LINE-HEIGHT: 140%">)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; )</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">]</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">namespace</span><span style="LINE-HEIGHT: 140%"> OneNeedsOptions</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">{</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">public</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">enum</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Fruit</span></span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; {</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; Orange,</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; Banana,</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; Strawberry,</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; Apple</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; }</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; [</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Serializable</span></span><span style="LINE-HEIGHT: 140%">(),</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">DefaultProperty</span></span><span style="LINE-HEIGHT: 140%">(</span><span style="color: #a31515;"><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;Name&quot;</span></span><span style="LINE-HEIGHT: 140%">)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; ]</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">public</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">class</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">AppSettings</span></span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; {</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// Our internal properties</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">private</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">string</span><span style="LINE-HEIGHT: 140%"> _name = </span><span style="color: #a31515;"><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;Kean Walmsley&quot;</span></span><span style="LINE-HEIGHT: 140%">;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">private</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">string</span><span style="LINE-HEIGHT: 140%"> _url =</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #a31515;"><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;http://blogs.autodesk.com/through-the-interface&quot;</span></span><span style="LINE-HEIGHT: 140%">;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">private</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">DateTime</span></span><span style="LINE-HEIGHT: 140%"> _birthday = </span><span style="COLOR: blue; LINE-HEIGHT: 140%">new</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">DateTime</span></span><span style="LINE-HEIGHT: 140%">(1912, 7, 14);</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">private</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Fruit</span></span><span style="LINE-HEIGHT: 140%"> _fruit = </span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Fruit</span></span><span style="LINE-HEIGHT: 140%">.Strawberry;</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// Their external exposure and categorization/description</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; [</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Description</span></span><span style="LINE-HEIGHT: 140%">(</span><span style="color: #a31515;"><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;The person's name&quot;</span></span><span style="LINE-HEIGHT: 140%">),</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; </span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Category</span></span><span style="LINE-HEIGHT: 140%">(</span><span style="color: #a31515;"><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;Identity&quot;</span></span><span style="LINE-HEIGHT: 140%">)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; ]</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">public</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">string</span><span style="LINE-HEIGHT: 140%"> Name</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; {</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">set</span><span style="LINE-HEIGHT: 140%"> { _name = </span><span style="COLOR: blue; LINE-HEIGHT: 140%">value</span><span style="LINE-HEIGHT: 140%">; }</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">get</span><span style="LINE-HEIGHT: 140%"> { </span><span style="COLOR: blue; LINE-HEIGHT: 140%">return</span><span style="LINE-HEIGHT: 140%"> _name; }</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; }</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; [</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Description</span></span><span style="LINE-HEIGHT: 140%">(</span><span style="color: #a31515;"><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;The blog written by this person&quot;</span></span><span style="LINE-HEIGHT: 140%">),</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; </span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Category</span></span><span style="LINE-HEIGHT: 140%">(</span><span style="color: #a31515;"><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;Stuff I do&quot;</span></span><span style="LINE-HEIGHT: 140%">),</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; </span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">UrlProperty</span></span><span style="LINE-HEIGHT: 140%">()</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; ]</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">public</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">string</span><span style="LINE-HEIGHT: 140%"> Blog</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; {</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">set</span><span style="LINE-HEIGHT: 140%"> { _url = </span><span style="COLOR: blue; LINE-HEIGHT: 140%">value</span><span style="LINE-HEIGHT: 140%">; }</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">get</span><span style="LINE-HEIGHT: 140%"> { </span><span style="COLOR: blue; LINE-HEIGHT: 140%">return</span><span style="LINE-HEIGHT: 140%"> _url; }</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; }</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; [</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Description</span></span><span style="LINE-HEIGHT: 140%">(</span><span style="color: #a31515;"><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;The day this person was born&quot;</span></span><span style="LINE-HEIGHT: 140%">),</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; </span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Category</span></span><span style="LINE-HEIGHT: 140%">(</span><span style="color: #a31515;"><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;Identity&quot;</span></span><span style="LINE-HEIGHT: 140%">)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; ]</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">public</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">DateTime</span></span><span style="LINE-HEIGHT: 140%"> Birthday</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; {</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">set</span><span style="LINE-HEIGHT: 140%"> { _birthday = </span><span style="COLOR: blue; LINE-HEIGHT: 140%">value</span><span style="LINE-HEIGHT: 140%">; }</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">get</span><span style="LINE-HEIGHT: 140%"> { </span><span style="COLOR: blue; LINE-HEIGHT: 140%">return</span><span style="LINE-HEIGHT: 140%"> _birthday; }</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; }</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; [</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Description</span></span><span style="LINE-HEIGHT: 140%">(</span><span style="color: #a31515;"><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;The person's age&quot;</span></span><span style="LINE-HEIGHT: 140%">),</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; </span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Category</span></span><span style="LINE-HEIGHT: 140%">(</span><span style="color: #a31515;"><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;Identity&quot;</span></span><span style="LINE-HEIGHT: 140%">),</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; </span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">ReadOnly</span></span><span style="LINE-HEIGHT: 140%">(</span><span style="COLOR: blue; LINE-HEIGHT: 140%">true</span><span style="LINE-HEIGHT: 140%">)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; ]</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">public</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">int</span><span style="LINE-HEIGHT: 140%"> Age</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; {</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">get</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;{</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">return</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; (</span><span style="COLOR: blue; LINE-HEIGHT: 140%">int</span><span style="LINE-HEIGHT: 140%">)((</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">DateTime</span></span><span style="LINE-HEIGHT: 140%">.Now - _birthday).Days / 365.25);</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;}</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; }</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; [</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Description</span></span><span style="LINE-HEIGHT: 140%">(</span><span style="color: #a31515;"><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;The person's favourite fruit&quot;</span></span><span style="LINE-HEIGHT: 140%">),</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; </span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Category</span></span><span style="LINE-HEIGHT: 140%">(</span><span style="color: #a31515;"><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;Stuff I like&quot;</span></span><span style="LINE-HEIGHT: 140%">)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; ]</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">public</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Fruit</span></span><span style="LINE-HEIGHT: 140%"> FavouriteFruit</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; {</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">set</span><span style="LINE-HEIGHT: 140%"> { _fruit = </span><span style="COLOR: blue; LINE-HEIGHT: 140%">value</span><span style="LINE-HEIGHT: 140%">; }</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">get</span><span style="LINE-HEIGHT: 140%"> { </span><span style="COLOR: blue; LINE-HEIGHT: 140%">return</span><span style="LINE-HEIGHT: 140%"> _fruit; }</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; }</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">const</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">string</span><span style="LINE-HEIGHT: 140%"> filename = </span><span style="color: #a31515;"><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;AppSettings.xml&quot;</span></span><span style="LINE-HEIGHT: 140%">;</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// Our methods for loading and saving the settings</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// Load needs to be static, as we don't yet have</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// an instance</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">public</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">static</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">AppSettings</span></span><span style="LINE-HEIGHT: 140%"> Load()</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; {</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">AppSettings</span></span><span style="LINE-HEIGHT: 140%"> ret = </span><span style="COLOR: blue; LINE-HEIGHT: 140%">null</span><span style="LINE-HEIGHT: 140%">;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">XmlSerializer</span></span><span style="LINE-HEIGHT: 140%"> xs = </span><span style="COLOR: blue; LINE-HEIGHT: 140%">null</span><span style="LINE-HEIGHT: 140%">;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">StreamReader</span></span><span style="LINE-HEIGHT: 140%"> sr = </span><span style="COLOR: blue; LINE-HEIGHT: 140%">null</span><span style="LINE-HEIGHT: 140%">;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">try</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;{</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; xs = </span><span style="COLOR: blue; LINE-HEIGHT: 140%">new</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">XmlSerializer</span></span><span style="LINE-HEIGHT: 140%">(</span><span style="COLOR: blue; LINE-HEIGHT: 140%">typeof</span><span style="LINE-HEIGHT: 140%">(</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">AppSettings</span></span><span style="LINE-HEIGHT: 140%">));</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; sr = </span><span style="COLOR: blue; LINE-HEIGHT: 140%">new</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">StreamReader</span></span><span style="LINE-HEIGHT: 140%">(filename);</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;}</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">catch</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;{</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// File not found: create default settings</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">return</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">new</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">AppSettings</span></span><span style="LINE-HEIGHT: 140%">();</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;}</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">if</span><span style="LINE-HEIGHT: 140%"> (sr != </span><span style="COLOR: blue; LINE-HEIGHT: 140%">null</span><span style="LINE-HEIGHT: 140%">)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;{</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ret = (</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">AppSettings</span></span><span style="LINE-HEIGHT: 140%">)xs.Deserialize(sr);</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; sr.Close();</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;}</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">return</span><span style="LINE-HEIGHT: 140%"> ret;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; }</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// Save will be called on a specific instance</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">public</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">void</span><span style="LINE-HEIGHT: 140%"> Save()</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; {</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">try</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;{</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">XmlSerializer</span></span><span style="LINE-HEIGHT: 140%"> xs =</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">new</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">XmlSerializer</span></span><span style="LINE-HEIGHT: 140%">(</span><span style="COLOR: blue; LINE-HEIGHT: 140%">typeof</span><span style="LINE-HEIGHT: 140%">(</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">AppSettings</span></span><span style="LINE-HEIGHT: 140%">));</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">StreamWriter</span></span><span style="LINE-HEIGHT: 140%"> sw =</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">new</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">StreamWriter</span></span><span style="LINE-HEIGHT: 140%">(filename, </span><span style="COLOR: blue; LINE-HEIGHT: 140%">false</span><span style="LINE-HEIGHT: 140%">);</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; xs.Serialize(sw, </span><span style="COLOR: blue; LINE-HEIGHT: 140%">this</span><span style="LINE-HEIGHT: 140%">);</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; sw.Close();</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;}</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">catch</span><span style="LINE-HEIGHT: 140%"> (System.</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Exception</span></span><span style="LINE-HEIGHT: 140%"> ex)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;{</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Editor</span></span><span style="LINE-HEIGHT: 140%"> ed =</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; </span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Application</span></span><span style="LINE-HEIGHT: 140%">.DocumentManager.MdiActiveDocument.Editor;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.WriteMessage(</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; </span><span style="color: #a31515;"><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;\nUnable to save the application settings: {0}&quot;</span></span><span style="LINE-HEIGHT: 140%">,</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ex</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;}</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; }</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; }</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">class</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Initialization</span></span><span style="LINE-HEIGHT: 140%"> : </span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">IExtensionApplication</span></span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; {</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">static</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">AppSettings</span></span><span style="LINE-HEIGHT: 140%"> _settings = </span><span style="COLOR: blue; LINE-HEIGHT: 140%">null</span><span style="LINE-HEIGHT: 140%">;</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">public</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">void</span><span style="LINE-HEIGHT: 140%"> Initialize()</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; {</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Application</span></span><span style="LINE-HEIGHT: 140%">.DisplayingOptionDialog +=</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">new</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">TabbedDialogEventHandler</span></span><span style="LINE-HEIGHT: 140%">(</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; Application_DisplayingOptionDialog</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; }</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">public</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">void</span><span style="LINE-HEIGHT: 140%"> Terminate()</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; {</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Application</span></span><span style="LINE-HEIGHT: 140%">.DisplayingOptionDialog -=</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">new</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">TabbedDialogEventHandler</span></span><span style="LINE-HEIGHT: 140%">(</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; Application_DisplayingOptionDialog</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; }</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">private</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">static</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">void</span><span style="LINE-HEIGHT: 140%"> OnOK()</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; {</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;_settings.Save();</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; }</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">private</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">static</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">void</span><span style="LINE-HEIGHT: 140%"> OnCancel()</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; {</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;_settings = </span><span style="color: #2b91af;"><span style="COLOR: rgb(43,145,175); LINE-HEIGHT: 140%">AppSettings</span></span><span style="LINE-HEIGHT: 140%">.Load();</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; }</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">private</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">static</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">void</span><span style="LINE-HEIGHT: 140%"> OnHelp()</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; {</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="COLOR: green; LINE-HEIGHT: 140%">// Not currently doing anything here</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; }</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">private</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">static</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">void</span><span style="LINE-HEIGHT: 140%"> OnApply()</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; {</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;_settings.Save();</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; }</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">private</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">static</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">void</span><span style="LINE-HEIGHT: 140%"> Application_DisplayingOptionDialog(</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">object</span><span style="LINE-HEIGHT: 140%"> sender,</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">TabbedDialogEventArgs</span></span><span style="LINE-HEIGHT: 140%"> e</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; )</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; {</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">if</span><span style="LINE-HEIGHT: 140%"> (_settings == </span><span style="COLOR: blue; LINE-HEIGHT: 140%">null</span><span style="LINE-HEIGHT: 140%">)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; _settings = </span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">AppSettings</span></span><span style="LINE-HEIGHT: 140%">.Load();</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">if</span><span style="LINE-HEIGHT: 140%"> (_settings != </span><span style="COLOR: blue; LINE-HEIGHT: 140%">null</span><span style="LINE-HEIGHT: 140%">)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;{</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; OptionsDlg.</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">OptionsTabControl</span></span><span style="LINE-HEIGHT: 140%"> otc =</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">new</span><span style="LINE-HEIGHT: 140%"> OptionsDlg.</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">OptionsTabControl</span></span><span style="LINE-HEIGHT: 140%">();</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; otc.propertyGrid.SelectedObject = _settings;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; otc.propertyGrid.Update();</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">TabbedDialogExtension</span></span><span style="LINE-HEIGHT: 140%"> tde =</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">new</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">TabbedDialogExtension</span></span><span style="LINE-HEIGHT: 140%">(</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;otc,</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">new</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">TabbedDialogAction</span></span><span style="LINE-HEIGHT: 140%">(OnOK),</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">new</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">TabbedDialogAction</span></span><span style="LINE-HEIGHT: 140%">(OnCancel),</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">new</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">TabbedDialogAction</span></span><span style="LINE-HEIGHT: 140%">(OnHelp),</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">new</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">TabbedDialogAction</span></span><span style="LINE-HEIGHT: 140%">(OnApply)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; );</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; e.AddTab(</span><span style="color: #a31515;"><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;My Application Settings&quot;</span></span><span style="LINE-HEIGHT: 140%">, tde);</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;}</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; }</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; }</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">}</span></p></div>

<p>The interesting stuff is in the AppSettings class: it defines a number of properties (for which I've set default values as they're declared - you could also put them in a constructor, should you so wish), which are then exposed externally. It's these public properties that are interesting, as we've used attributes to indicate how the properties should be categorized, described and whether they're editable. The rest of the class contains the protocol to load and save the settings: we use the .NET Framework to do the heavy lifting of saving the contents to a file (which we've simply called AppSettings.xml, without specifying the location, which means it will be stored wherever your module is located), and loading them back in again.</p>

<p>Here's what the XML content looks like for the default settings, in case you're interested, although you should never really need to worry about it, unless you're interested in allowing more direct modification of the file contents:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">&lt;?</span><span style="color: #a31515;"><span style="COLOR: #a31515; LINE-HEIGHT: 140%">xml</span></span><span style="COLOR: blue; LINE-HEIGHT: 140%">&nbsp;</span><span style="COLOR: red; LINE-HEIGHT: 140%">version</span><span style="COLOR: blue; LINE-HEIGHT: 140%">=</span><span style="LINE-HEIGHT: 140%">&quot;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">1.0</span><span style="LINE-HEIGHT: 140%">&quot;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">&nbsp;</span><span style="COLOR: red; LINE-HEIGHT: 140%">encoding</span><span style="COLOR: blue; LINE-HEIGHT: 140%">=</span><span style="LINE-HEIGHT: 140%">&quot;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">utf-8</span><span style="LINE-HEIGHT: 140%">&quot;</span><span style="COLOR: blue; LINE-HEIGHT: 140%"> ?&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">&lt;</span><span style="color: #a31515;"><span style="COLOR: #a31515; LINE-HEIGHT: 140%">AppSettings</span></span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">&nbsp; </span><span style="COLOR: red; LINE-HEIGHT: 140%">xmlns:xsi</span><span style="COLOR: blue; LINE-HEIGHT: 140%">=</span><span style="LINE-HEIGHT: 140%">&quot;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">http://www.w3.org/2001/XMLSchema-instance</span><span style="LINE-HEIGHT: 140%">&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">&nbsp; </span><span style="COLOR: red; LINE-HEIGHT: 140%">xmlns:xsd</span><span style="COLOR: blue; LINE-HEIGHT: 140%">=</span><span style="LINE-HEIGHT: 140%">&quot;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">http://www.w3.org/2001/XMLSchema</span><span style="LINE-HEIGHT: 140%">&quot;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">&nbsp; &lt;</span><span style="color: #a31515;"><span style="COLOR: #a31515; LINE-HEIGHT: 140%">Name</span></span><span style="COLOR: blue; LINE-HEIGHT: 140%">&gt;</span><span style="LINE-HEIGHT: 140%">Kean Walmsley</span><span style="COLOR: blue; LINE-HEIGHT: 140%">&lt;/</span><span style="color: #a31515;"><span style="COLOR: #a31515; LINE-HEIGHT: 140%">Name</span></span><span style="COLOR: blue; LINE-HEIGHT: 140%">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">&nbsp; &lt;</span><span style="color: #a31515;"><span style="COLOR: #a31515; LINE-HEIGHT: 140%">Blog</span></span><span style="COLOR: blue; LINE-HEIGHT: 140%">&gt;</span><span style="LINE-HEIGHT: 140%">http://blogs.autodesk.com/through-the-interface</span><span style="COLOR: blue; LINE-HEIGHT: 140%">&lt;/</span><span style="color: #a31515;"><span style="COLOR: #a31515; LINE-HEIGHT: 140%">Blog</span></span><span style="COLOR: blue; LINE-HEIGHT: 140%">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">&nbsp; &lt;</span><span style="color: #a31515;"><span style="COLOR: #a31515; LINE-HEIGHT: 140%">Birthday</span></span><span style="COLOR: blue; LINE-HEIGHT: 140%">&gt;</span><span style="LINE-HEIGHT: 140%">1912-07-14T00:00:00</span><span style="COLOR: blue; LINE-HEIGHT: 140%">&lt;/</span><span style="color: #a31515;"><span style="COLOR: #a31515; LINE-HEIGHT: 140%">Birthday</span></span><span style="COLOR: blue; LINE-HEIGHT: 140%">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">&nbsp; &lt;</span><span style="color: #a31515;"><span style="COLOR: #a31515; LINE-HEIGHT: 140%">FavouriteFruit</span></span><span style="COLOR: blue; LINE-HEIGHT: 140%">&gt;</span><span style="LINE-HEIGHT: 140%">Strawberry</span><span style="COLOR: blue; LINE-HEIGHT: 140%">&lt;/</span><span style="color: #a31515;"><span style="COLOR: #a31515; LINE-HEIGHT: 140%">FavouriteFruit</span></span><span style="COLOR: blue; LINE-HEIGHT: 140%">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">&lt;/</span><span style="color: #a31515;"><span style="COLOR: #a31515; LINE-HEIGHT: 140%">AppSettings</span></span><span style="COLOR: blue; LINE-HEIGHT: 140%">&gt;</span></p></div>

<p>A couple of more comments on the code... </p>

<p>We haven't bothered implementing the Help callback, but I've left it in their for your convenience (we could also have passed a null value into the construction of the TabbedDialogExtension object). From the callbacks for OK and Apply we call through to the AppSettings class to save the data; from Cancel we reload the last saved state, effectively cancelling any unsaved changes.</p>

<p>The AppSettings class will need to be accessible from elsewhere in your code (that's the point, really - settings aren't much use unless they're accessed), but I haven't actually shown this. It should simply be a matter of setting the _settings object to be public (or internal), or of exposing the data you care about via properties on the Initialization class.</p>

<p>Here's what tab looks like, once we've built the application, loaded it and launched the OPTIONS command inside AutoCAD:</p>

<p><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/A%20custom%20tab%20with%20a%20property%20grid%20in%20AutoCAD's%20options%20dialog.png"><img height="392" alt="A custom tab with a property grid in AutoCAD's options dialog" src="/assets/image_242905.jpg's%20options%20dialog_thumb.png" width="472" border="0" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" /></a> </p>

<p>As you edit the properties you'll see the controls available suit the property in question: there's a date picker for &quot;Birthday&quot; (which isn't actually my date of birth, by the way: it seems safer not to publish your birthday on the web, these days) and a combo-box for &quot;FavouriteFruit&quot;. I wish there were better display of URLs in the grid, but that appears to be a standard complaint, and beyond the scope of this post. You will notice an &quot;Age&quot; property which has been made read-only as it's calculated from the date field. You'll also notice that the &quot;Name&quot; property is selected by default, because we indicated it as such using the DefaultProperty() attribute of the class.</p>
