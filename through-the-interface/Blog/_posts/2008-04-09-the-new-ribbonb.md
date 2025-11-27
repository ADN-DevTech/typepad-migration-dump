---
layout: "post"
title: "The New RibbonBar API in AutoCAD 2009"
date: "2008-04-09 09:00:00"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "User interface"
original_url: "https://www.keanw.com/2008/04/the-new-ribbonb.html "
typepad_basename: "the-new-ribbonb"
typepad_status: "Publish"
---

<p><em>Thank you to Sreekar Devatha, from DevTech India, for writing this article for the recently published ADN Platform Technologies Customization Newsletter. This article talks about the new Ribbon API referenced in this overview of the </em><a href="http://through-the-interface.typepad.com/through_the_interface/2008/03/new-apis-in-aut.html" target="_blank"><em>new APIs in AutoCAD 2009</em></a><em>. A complete sample demonstrating the use of this API is provided as part of the ObjectARX 2009 SDK, under </em>samples/dotNet/Ribbon<em>.</em></p>

<h5>Introduction</h5>

<p>Most of the AutoCAD® UI was redesigned in this release. Ribbon, Menu browser and Tooltips are some of the prominent UI features to list. As you might already know the UI enhancements are based on the new Windows® Presentation Foundation (WPF) programming model introduced by Microsoft. So, let’s start with a small introduction to WPF and then we'll move on to the finer points of customizing the Ribbon bar.</p>

<h5>What is WPF?</h5>

<p>Windows Presentation Foundation (WPF) is a programming model introduced by Microsoft to build rich Windows client applications. </p>

<p>This graphical subsystem introduced in .NET Framework 3.0 provides a clear separation between appearance and behavior of applications. You generally use eXtensible Application Markup Language (XAML) to implement the appearance of an application while using managed programming languages (code-behind) to implement its behavior. XAML is the new XML-based UI definition language from Microsoft, and as such is a core part of WPF.</p>

<p>Without wasting too much time on WPF let us move quickly on to the Ribbon APIs. If you are new to WPF then you could go through the basics of WPF using the links below before starting with the Ribbon APIs.</p>

<p><a href="http://msdn2.microsoft.com/en-us/library/ms754130.aspx" target="_blank">Windows Presentation Foundation - MSDN</a></p>

<p><a href="http://www.codeproject.com/KB/WPF/" target="_blank">Windows Presentation Foundation - CodeProject</a></p>

<p><a href="http://windowsclient.net/default.aspx" target="_blank">Microsoft WindowsClient.NET</a> </p>

<h5>AutoCAD Ribbon</h5>

<p>Before diving into the Ribbon APIs it's necessary to understand the Ribbon layout and its terminology which are covered in this and the following section.</p>

<p>The AutoCAD Ribbon provides a single, compact placement for operations that are relevant to the current workspace. It overcomes the need to display multiple toolbars, reducing clutter in the application window. The Ribbon maximizes the area available for work using a single compact interface. </p>

<p>The Ribbon was built using WPF (part of .NET Framework 3.0) and a comprehensive set of APIs have been provided by Autodesk to help external developers customize it.</p>

<h5>Ribbon Layout</h5>

<p>The different components of the AutoCAD Ribbon are shown below. Also, depicted in the snapshot are the classes corresponding to each component.</p>

<p><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/Ribbon%20layout.png"><img height="72" alt="Ribbon layout" src="/assets/Ribbon%20layout_thumb.png" width="244" border="0" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" /></a> </p>

<p><strong>Figure: </strong>the AutoCAD Ribbon and its layout</p>

<p>The Ribbon control is the top level control which contains everything in the Ribbon. It is composed of a series of panels, which are organized into tabs labeled by task.</p>

<p>Ribbon tabs control the display and order of Ribbon panels on the Ribbon. You add Ribbon tabs to a workspace to control which Ribbon tabs are displayed on the Ribbon. Ribbon tabs do not contain any commands or controls like a Ribbon panel does; instead, they manage the display of Ribbon panels on the Ribbon. Once a Ribbon tab is created, a panel can then be added to it. Ribbon tabs are of two types, standard and contextual. Standard tabs are always displayed while contextual tabs are displayed based on a particular context: for instance a Block Editor Tab is displayed while editing a Block.</p>

<p><em>Contextual tabs can appear in two modes:</em></p>

<p><em>Replace mode:</em> In this mode the contextual tab gets added to the standard tabs as a regular tab. When the contextual tab is clicked it becomes active and the panels in the contextual tabs replace the panels in the previously active tab.</p>

<p><em>Append mode:</em> In this mode contextual tabs form another tab set similar to the standard tab set and is displayed side-by-side with the standard tabs and panels. There are two tabs active any time and activating a tab in one tab set does not affect active tab in the other tab set.</p>

<p>Ribbon panels are organized by rows, sub-panels, and panel separators. Rows and sub-panels are used to organize how commands and controls are displayed on the Ribbon panel. A row, similar to a toolbar, determines the order and position that commands and controls appear on the Ribbon panel. Rows run horizontally on a Ribbon panel. If all the commands and controls cannot be displayed on the Ribbon panel, a gray down arrow is displayed for expanding the Ribbon panel. Rows can be divided using a sub-panel which, holds rows to order and position commands and controls. Commands and controls can be added to rows and sub-panels, you can remove the commands and controls that you use infrequently, and rearrange the order of commands and controls. Along with commands and controls, you can also create flyouts that contain multiple commands and only take up the space of a single command.</p>

<h5>Prerequisites</h5>

<ul><li>.NET Framework 3.0 installs WPF – The UI components in AutoCAD were built using .NET 3.0 </li>

<li>Visual Studio 2005 (with/without SP1) </li>

<li><a href="http://www.microsoft.com/downloads/details.aspx?familyid=F54F5537-CC86-4BF5-AE44-F5A1E805680D&amp;displaylang=en" target="_blank">Visual Studio 2005 extensions for .NET Framework 3.0 (WCF &amp; WPF), November 2006 CTP</a> </li>

<li>Visual Studio 2005 or Expression Blend or any text editor like notepad, etc. could be used to edit the XAML files.</li></ul>

<h5>Modules, Namespaces &amp; Classes</h5>

<p>The core UI framework for AutoCAD is present in AdWindows and AcRibbon contains the Ribbon specific implementation. These are managed UI class libraries developed using .NET 3.0 and WPF. Only .NET APIs are available and no C++ wrappers are provided.</p>

<p><strong><em>AdWindows.dll</em></strong></p>

<p>This library implements the framework for the following Autodesk UI features. </p>

<ul><li>Ribbon classes </li>

<li>Autodesk controls </li>

<li>Tooltips </li>

<li>Menu browser </li>

<li>Task dialog, etc.</li></ul>

<p>These are the Ribbon-specific classes under the Autodesk.Windows namespace of this DLL.</p>

<ul><li>RibbonControl </li>

<li>RibbonTab </li>

<li>RibbonPanel </li>

<li>RibbonPanelSource </li>

<li>RibbonRow </li>

<li>RibbonItem </li>

<li>RibbonButton </li>

<li>RibbonDropDownButton </li>

<li>RibbonSeperator </li>

<li>RibbonForm </li>

<li>RibbonHwnd </li>

<li>RibbonRowPanel, etc.</li></ul>

<p>For more details regarding these classes refer the ObjectARX® Managed Reference guide available in the ObjectARX 2009 SDK. </p>

<p><strong><em>AcRibbon.dll</em></strong></p>

<p>This library was actually meant to be an internal-only DLL except for the very few APIs which are&nbsp; discussed in this article below. All other APIs included in the DLL should be considered as internal-only.</p>

<p><em>Classes</em></p>

<ul><li>Palette that hosts the AutoCAD Ribbon control <ul><li><div style="COLOR: black; FONT-FAMILY: courier new"><p>Autodesk.AutoCAD.Ribbon.<span style="COLOR: teal">RibbonPaletteSet</span></p></div></li></ul></li>

<li>AutoCAD Ribbon control <ul><li><div style="COLOR: black; FONT-FAMILY: courier new"><p>Autodesk.AutoCAD.Ribbon.<span style="COLOR: teal">RibbonServices</span>. RibbonPaletteSet.RibbonControl</p></div></li></ul></li></ul>

<p><em>Properties</em></p>

<ul><li>Property to access the default Ribbon host window which is a palette <ul><li><div style="COLOR: black; FONT-FAMILY: courier new"><p>Autodesk.AutoCAD.Ribbon.<span style="COLOR: teal">RibbonServices</span>. RibbonPaletteSet</p></div></li></ul></li></ul>

<p><em>Note: </em>You are advised not to use any of the internal APIs as they are unsupported and could be changed or dropped without prior notice.</p>

<h5>Custom Ribbon Tab</h5>

<p>As discussed above in the Ribbon Layout section, we need to create panels with Ribbon items placed on them. Then, these panels should be categorized based on their usage and hosted on your application-specific Ribbon tabs. To demonstrate this we'll now look into the finer points of the API by adding a simple button to a panel and then host the panel on a tab (Custom Tab). </p>

<h5>Button</h5>

<p>In this section we'll add a button to the ribbon bar. If we take a look at the classes listed above we have the RibbonButton class which can be used to create a button to be placed on the Ribbon. So, let’s start with the creation of a RibbonButton instance as below:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: teal">RibbonButton</span> button = <span style="COLOR: blue">new</span> <span style="COLOR: teal">RibbonButton</span>();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">button.Text = <span style="COLOR: maroon">&quot;Click Me&quot;</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">// resourceDictionary</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">// A XAML resource dictionary that defines a ButtonImage</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">button.LargeImage =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; resourceDictionary[<span style="COLOR: maroon">&quot;ButtonImage&quot;</span>] <span style="COLOR: blue">as</span> <span style="COLOR: teal">BitmapImage</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">button.Orientation = <span style="COLOR: teal">Orientation</span>.Vertical;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">button.Size = <span style="COLOR: teal">RibbonItemSize</span>.Large;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">button.ShowText = <span style="COLOR: blue">true</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">button.ShowImage = <span style="COLOR: blue">true</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">button.Id = <span style="COLOR: maroon">&quot;ClickMe_1&quot;</span>;</p></div>

<p>Now, this button instance should be placed on a panel that can then be hosted by a tab. But before actually creating the panel we need a row in which to place the button, as discussed earlier.</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">// Create a Row to add the RibbonButton</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: teal">RibbonRow</span> row = <span style="COLOR: blue">new</span> <span style="COLOR: teal">RibbonRow</span>();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">row.Items.Add(button);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">// Create a Ribbon panel source in which to</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">// place ribbon items</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: teal">RibbonPanelSource</span> panelSource =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">new</span> <span style="COLOR: teal">RibbonPanelSource</span>();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">panelSource.Title = <span style="COLOR: maroon">&quot;Custom Panel&quot;</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">panelSource.Rows.Add(row);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">// Create a panel for holding the panel</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">// source content</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: teal">RibbonPanel</span> panel = <span style="COLOR: blue">new</span> <span style="COLOR: teal">RibbonPanel</span>();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">panel.Source = panelSource;</p></div>

<p>The panel should be hosted on the tab which in turn should be added to the Ribbon control and the equivalent code to achieve this is below:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">// Create a tab to manage the above panel</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: teal">RibbonTab</span> tab = <span style="COLOR: blue">new</span> <span style="COLOR: teal">RibbonTab</span>();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">tab.Title = <span style="COLOR: maroon">&quot;Custom Tab&quot;</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">tab.Id = <span style="COLOR: maroon">&quot;CustomTab&quot;</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">tab.IsContextualTab = <span style="COLOR: blue">false</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">tab.Panels.Add(panel);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">// Now add the tab to AutoCAD Ribbon bar...</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: teal">RibbonControl</span> ribbonControl =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; Autodesk.AutoCAD.Ribbon.<span style="COLOR: teal">RibbonServices</span>.</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; RibbonPaletteSet.RibbonControl;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">ribbonControl.Tabs.Add(tab);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">// ... and activate the tab</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">ribbonControl.ActiveTab = tab;</p></div>

<p>The below snapshot shows the button added to AutoCAD's Ribbon.</p>

<p><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/Custom%20ribbon%20panel%20inside%20AutoCAD%202009.png"><img height="159" alt="Custom ribbon panel inside AutoCAD 2009" src="/assets/Custom%20ribbon%20panel%20inside%20AutoCAD%202009_thumb.png" width="244" border="0" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" /></a></p>

<p><strong>Figure</strong>: the button added to the Ribbon bar</p>

<p>One more item that was missing in the above code was an event to identify the click of the button. The following code implements the click event. </p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="FONT-SIZE: 8pt; MARGIN: 0px">button.Click += <span style="COLOR: blue">new</span> <span style="COLOR: teal">RoutedEventHandler</span>(button_Click);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">private</span> <span style="COLOR: blue">static</span> <span style="COLOR: blue">void</span> button_Click(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">object</span> sender, <span style="COLOR: teal">RoutedEventArgs</span> e)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: teal">RibbonButton</span> button = sender <span style="COLOR: blue">as</span> <span style="COLOR: teal">RibbonButton</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">if</span> (button != <span style="COLOR: blue">null</span> &amp;&amp; (button.Id == <span style="COLOR: maroon">&quot;ClickMe_1&quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: teal">MessageBox</span>.Show(<span style="COLOR: maroon">&quot;Click Me clicked &quot;</span>, <span style="COLOR: maroon">&quot;Click Me&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; e.Handled = <span style="COLOR: blue">true</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">}</p></div>

<p>The above code might also be implemented using a combination of XAML and C# code-behind as shown below.</p>

<p>XAML that defines the RibbonTab</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &lt;</span><span style="COLOR: maroon">adw:RibbonTab</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; </span><span style="COLOR: red">x:Key</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">TabXaml</span>&quot;<span style="COLOR: blue"> </span><span style="COLOR: red">Title</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">Custom Tab XAML</span>&quot;<span style="COLOR: blue"> </span><span style="COLOR: red">Id</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">CustomTabXaml</span>&quot;<span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">adw:RibbonPanel</span><span style="COLOR: blue"> &gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&lt;</span><span style="COLOR: maroon">adw:RibbonPanelSource</span><span style="COLOR: blue"> </span><span style="COLOR: red">Title</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">Custom Panel XAML</span>&quot;<span style="COLOR: blue"> &gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &lt;!--</span><span style="COLOR: green">Add a ribbon row</span><span style="COLOR: blue">--&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &lt;!--</span><span style="COLOR: green">Note: You could add only rows</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;to the panel source content</span><span style="COLOR: blue">--&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &lt;</span><span style="COLOR: maroon">adw:RibbonRow</span><span style="COLOR: blue"> </span><span style="COLOR: red">x:Uid</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">adw:RibbonRow_1</span>&quot;<span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; &lt;!--</span><span style="COLOR: green">Add Ribbon Items here</span><span style="COLOR: blue">--&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; &lt;!--</span><span style="COLOR: green">The items could be any RibbonItem derived classes</span><span style="COLOR: blue">--&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; &lt;!--</span><span style="COLOR: green">Like RibbonButton</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; RibbonDropDownButton,</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; RibbonForm, </span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; RibbonHwnd,</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; RibbonLabel</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; RibbonMenuButton,</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; RibbonRowPanel, </span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; RibbonSeperator,</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; RibbonToggleButton</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; or any RibbonItem derived custom controls</span><span style="COLOR: blue">--&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">adw:RibbonButton</span><span style="COLOR: blue"> </span><span style="COLOR: red">Id</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">ClickMe_2</span>&quot;<span style="COLOR: blue"> </span><span style="COLOR: red">ShowText</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">true</span>&quot;<span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&lt;</span><span style="COLOR: maroon">adw:RibbonButton.Orientation</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &lt;</span><span style="COLOR: maroon">Orientation</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; Vertical</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &lt;/</span><span style="COLOR: maroon">Orientation</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&lt;/</span><span style="COLOR: maroon">adw:RibbonButton.Orientation</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&lt;</span><span style="COLOR: maroon">adw:RibbonButton.Image</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &lt;</span><span style="COLOR: maroon">BitmapImage</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; </span><span style="COLOR: red">UriSource</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">Images/bitmap1.bmp</span>&quot;<span style="COLOR: blue">/&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&lt;/</span><span style="COLOR: maroon">adw:RibbonButton.Image</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&lt;</span><span style="COLOR: maroon">adw:RibbonButton.LargeImage</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &lt;</span><span style="COLOR: maroon">BitmapImage</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; </span><span style="COLOR: red">UriSource</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">Images/bitmap1.bmp</span>&quot;<span style="COLOR: blue">/&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&lt;/</span><span style="COLOR: maroon">adw:RibbonButton.LargeImage</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&lt;</span><span style="COLOR: maroon">adw:RibbonButton.Size</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &lt;</span><span style="COLOR: maroon">adw:RibbonItemSize</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; Large</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &lt;/</span><span style="COLOR: maroon">adw:RibbonItemSize</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&lt;/</span><span style="COLOR: maroon">adw:RibbonButton.Size</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&lt;</span><span style="COLOR: maroon">adw:RibbonButton.Text</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; Click Me</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&lt;/</span><span style="COLOR: maroon">adw:RibbonButton.Text</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&lt;</span><span style="COLOR: maroon">adw:RibbonButton.ToolTip</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &lt;</span><span style="COLOR: maroon">src:RibbonToolTip</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; </span><span style="COLOR: red">BasicText</span><span style="COLOR: blue"> = </span>&quot;<span style="COLOR: blue">Click Me basic help</span>&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; </span><span style="COLOR: red">CommandName</span><span style="COLOR: blue"> = </span>&quot;<span style="COLOR: blue">ClickMe</span>&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; </span><span style="COLOR: red">ExtendedURISource</span><span style="COLOR: blue"> =</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;</span>&quot;<span style="COLOR: blue">/MyRibbon;component/Dictionary1.xaml</span>&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; </span><span style="COLOR: red">ExtendedURISourceKey</span><span style="COLOR: blue"> = </span>&quot;<span style="COLOR: blue">ClickMe_ToolTip</span>&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; </span><span style="COLOR: red">HelpSource</span><span style="COLOR: blue"> = </span>&quot;<span style="COLOR: blue">./Help/readme.chm</span>&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; </span><span style="COLOR: red">HelpTopic</span><span style="COLOR: blue"> =</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;</span>&quot;<span style="COLOR: blue">WS1a9193826455f5ff1dbc298511635bea8752e2f</span>&quot;<span style="COLOR: blue">/&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&lt;/</span><span style="COLOR: maroon">adw:RibbonButton.ToolTip</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; &lt;/</span><span style="COLOR: maroon">adw:RibbonButton</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &lt;/</span><span style="COLOR: maroon">adw:RibbonRow</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&lt;/</span><span style="COLOR: maroon">adw:RibbonPanelSource</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;/</span><span style="COLOR: maroon">adw:RibbonPanel</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &lt;/</span><span style="COLOR: maroon">adw:RibbonTab</span><span style="COLOR: blue">&gt;</span></p></div>

<p>C# code-behind to add a button to the ribbon bar using the tab defined in XAML</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="FONT-SIZE: 8pt; MARGIN: 0px">[CommandMethod(<span style="COLOR: maroon">&quot;AddButtonXAML&quot;</span>)]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">public</span> <span style="COLOR: blue">static</span> <span style="COLOR: blue">void</span> AddButtonXAML()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// Create a RibbonTab using the resourceDictionary </span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: teal">RibbonTab</span> tab =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; resourceDictionary[<span style="COLOR: maroon">&quot;TabXaml&quot;</span>] <span style="COLOR: blue">as</span> <span style="COLOR: teal">RibbonTab</span>; </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// Find the ribbon button and add the event</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: teal">RibbonRow</span> row = tab.Panels[0].Source.Rows[0];</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: teal">RibbonItemCollection</span> coll = row.Items;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">foreach</span> (<span style="COLOR: teal">RibbonItem</span> item <span style="COLOR: blue">in</span> coll)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">if</span> (item <span style="COLOR: blue">is</span> <span style="COLOR: teal">RibbonButton</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">RibbonButton</span> button = (<span style="COLOR: teal">RibbonButton</span>)item;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (button.Id == <span style="COLOR: maroon">&quot;ClickMe_2&quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; button.Click +=</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">new</span> <span style="COLOR: teal">RoutedEventHandler</span>(button_Click);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// Now add the tab to AutoCAD Ribbon bar and activate it</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; ribbonControl.Tabs.Add(tab);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; ribbonControl.ActiveTab = tab;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">}</p></div>

<h5>ToolTip</h5>

<p>The next thing you would want to do once you add your objects to the Ribbon bar is to display a tooltip for these objects.</p>

<p>The ToolTip property of the RibbonItem class accepts an object so, we could assign a control object to it to display the control’s content as a tooltip. In this example here we define a Grid control. The control intern uses the Autodesk.Windows.ProgressivePanel class to implement the extended tooltip feature that is available with the AutoCAD tooltips.</p>

<p>XAML</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&lt;</span><span style="COLOR: maroon">Grid</span><span style="COLOR: blue"> </span><span style="COLOR: red">x:Key</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">ClickMe_ToolTip</span>&quot;<span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &lt;</span><span style="COLOR: maroon">StackPanel</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;!--</span><span style="COLOR: green">Header Part</span><span style="COLOR: blue">--&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">StackPanel</span><span style="COLOR: blue"> </span><span style="COLOR: red">Orientation</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">Horizontal</span>&quot;<span style="COLOR: blue"> </span><span style="COLOR: red">Margin</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">5,5,5,5</span>&quot;<span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&lt;</span><span style="COLOR: maroon">TextBlock</span><span style="COLOR: blue"> </span><span style="COLOR: red">Text</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">ClickMe</span>&quot;<span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &lt;</span><span style="COLOR: maroon">TextBlock.FontWeight</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">FontWeight</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;Bold</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; &lt;/</span><span style="COLOR: maroon">FontWeight</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &lt;/</span><span style="COLOR: maroon">TextBlock.FontWeight</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&lt;/</span><span style="COLOR: maroon">TextBlock</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;/</span><span style="COLOR: maroon">StackPanel</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;!--</span><span style="COLOR: green">Basic help information </span><span style="COLOR: blue">--&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">StackPanel</span><span style="COLOR: blue"> </span><span style="COLOR: red">Margin</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">5,5,5,5</span>&quot;<span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&lt;</span><span style="COLOR: maroon">TextBlock</span><span style="COLOR: blue"> </span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="COLOR: red">Text</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">This is basic help of click me command</span>&quot;<span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &lt;</span><span style="COLOR: maroon">TextBlock.TextWrapping</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">TextWrapping</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;Wrap</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; &lt;/</span><span style="COLOR: maroon">TextWrapping</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &lt;/</span><span style="COLOR: maroon">TextBlock.TextWrapping</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&lt;/</span><span style="COLOR: maroon">TextBlock</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;/</span><span style="COLOR: maroon">StackPanel</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;!--</span><span style="COLOR: green">Extended help information </span><span style="COLOR: blue">--&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">adw:ProgressivePanel</span><span style="COLOR: blue"> </span><span style="COLOR: red">Margin</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">5,5,5,5</span>&quot;<span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&lt;</span><span style="COLOR: maroon">StackPanel</span><span style="COLOR: blue">/&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&lt;!--</span><span style="COLOR: green">Click Me Extended Tooltip</span><span style="COLOR: blue">--&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&lt;</span><span style="COLOR: maroon">Grid</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &lt;</span><span style="COLOR: maroon">StackPanel</span><span style="COLOR: blue"> </span><span style="COLOR: red">Orientation</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">Vertical</span>&quot;<span style="COLOR: blue"> </span><span style="COLOR: red">Margin</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">0,0,0,0</span>&quot;<span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">TextBlock</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;Click Me extended ToolTip</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; &lt;/</span><span style="COLOR: maroon">TextBlock</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">Image</span><span style="COLOR: blue"> </span><span style="COLOR: red">Margin</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">40,10,0,0</span>&quot;<span style="COLOR: blue">&nbsp; </span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="COLOR: red">Width</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">150</span>&quot;<span style="COLOR: blue"> </span><span style="COLOR: red">Height</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">150</span>&quot;<span style="COLOR: blue">&nbsp; </span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="COLOR: red">Source</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">/MyRibbon;component/Images/Smiley.png</span>&quot;<span style="COLOR: blue"> /&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &lt;/</span><span style="COLOR: maroon">StackPanel</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&lt;/</span><span style="COLOR: maroon">Grid</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;/</span><span style="COLOR: maroon">adw:ProgressivePanel</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;!--</span><span style="COLOR: green"> Footer Part </span><span style="COLOR: blue">--&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">Line</span><span style="COLOR: blue"> </span><span style="COLOR: red">Stroke</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">Black</span>&quot;<span style="COLOR: blue"> </span><span style="COLOR: red">StrokeThickness</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">2</span>&quot;<span style="COLOR: blue"> </span><span style="COLOR: red">X2</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">250</span>&quot;<span style="COLOR: blue">/&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">StackPanel</span><span style="COLOR: blue"> </span><span style="COLOR: red">Orientation</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">Horizontal</span>&quot;<span style="COLOR: blue"> </span><span style="COLOR: red">Margin</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">5,5,5,5</span>&quot;<span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&lt;</span><span style="COLOR: maroon">Grid</span><span style="COLOR: blue"> </span><span style="COLOR: red">VerticalAlignment</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">Center</span>&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="COLOR: red">HorizontalAlignment</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">Left</span>&quot;<span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &lt;</span><span style="COLOR: maroon">Grid.ColumnDefinitions</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">ColumnDefinition</span><span style="COLOR: blue"> </span><span style="COLOR: red">Width</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">21</span>&quot;<span style="COLOR: blue"> /&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">ColumnDefinition</span><span style="COLOR: blue"> </span><span style="COLOR: red">Width</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">179</span>&quot;<span style="COLOR: blue"> /&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &lt;/</span><span style="COLOR: maroon">Grid.ColumnDefinitions</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &lt;</span><span style="COLOR: maroon">Image</span><span style="COLOR: blue"> </span><span style="COLOR: red">HorizontalAlignment</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">Left</span>&quot;<span style="COLOR: blue"> </span><span style="COLOR: red">Grid.Column</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">0</span>&quot;<span style="COLOR: blue"> </span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="COLOR: red">Width</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">16</span>&quot;<span style="COLOR: blue"> </span><span style="COLOR: red">Height</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">16</span>&quot;<span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">Image.Source</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;/MyRibbon;component/Images/Help.gif</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; &lt;/</span><span style="COLOR: maroon">Image.Source</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &lt;/</span><span style="COLOR: maroon">Image</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &lt;</span><span style="COLOR: maroon">TextBlock</span><span style="COLOR: blue"> </span><span style="COLOR: red">HorizontalAlignment</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">Left</span>&quot;<span style="COLOR: blue"> </span><span style="COLOR: red">Grid.Column</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">1</span>&quot;<span style="COLOR: blue"> </span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="COLOR: red">FontWeight</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">Bold</span>&quot;<span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; Press F1 for more help</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &lt;/</span><span style="COLOR: maroon">TextBlock</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&lt;/</span><span style="COLOR: maroon">Grid</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;/</span><span style="COLOR: maroon">StackPanel</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &lt;/</span><span style="COLOR: maroon">StackPanel</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">Grid</span><span style="COLOR: blue">&gt;</span></p></div>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">button.ToolTip = resourceDictionary[<span style="COLOR: maroon">&quot;ClickMe_ToolTip&quot;</span>];</p></div>

<p>We can do away with this statement above if we define the button in the XAML file by adding the tooltip to RibbonButton in the XAML as below:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><span style="COLOR: blue"><div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&lt;</span><span style="COLOR: maroon">adw:RibbonButton.ToolTip</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &lt;!--</span><span style="COLOR: green"> Define tooltip here, above XAML without</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">&nbsp; &nbsp;&nbsp; &nbsp;x:Key value could be used </span><span style="COLOR: blue">--&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">adw:RibbonButton.ToolTip</span><span style="COLOR: blue">&gt;</span></p></div></span></div>

<p>Here's a snapshot of the extended tooltip:</p>

<p><span style="FONT-SIZE: 10pt; FONT-FAMILY: 'Arial','sans-serif'"><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/Custom%20extended%20tooltip%20in%20AutoCAD%202009.png"><img height="222" alt="Custom extended tooltip in AutoCAD 2009" src="/assets/Custom%20extended%20tooltip%20in%20AutoCAD%202009_thumb.png" width="244" border="0" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" /></a> </span></p>

<p><strong>Figure</strong>: Ribbon object tooltip</p>

<p>Although we can display tooltip using a control as done above, we will not be able to implement the F1 event-handling mechanism using this approach. The ToolTip UI controls like Autodesk.Windows.ToolTip or System.Windows.Controls.ToolTip with F1 event handlers will not help us here because the Ribbon bar does not accept them similar to the way we could not use the Button class to add a button to the Ribbon bar. This particular feature could easily run into an article in itself, so we'll stop at this point to continue in a future article.</p>
