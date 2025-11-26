---
layout: "post"
title: "Pimp my AutoCAD or Revit Ribbon"
date: "2011-02-25 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "2011"
  - "Automation"
  - "Events"
  - "External"
  - "Ribbon"
  - "User Interface"
original_url: "https://thebuildingcoder.typepad.com/blog/2011/02/pimp-my-autocad-or-revit-ribbon.html "
typepad_basename: "pimp-my-autocad-or-revit-ribbon"
typepad_status: "Publish"
---

<p>Here is another fun possibility to access and manipulate the Revit ribbon explored by Rudolf Honke of

<a href="http://www.acadgraph.de">
acadGraph CADstudio GmbH</a>.

He says:

<p>I've played around and found a possibility to modify the appearance of the AutoCAD and Revit ribbon bars.

<p>This is the Revit ribbon bar in its usual colour scheme:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330147e2ce60c4970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330147e2ce60c4970b image-full" alt="Revit ribbon colour scheme" title="Revit ribbon colour scheme" src="/assets/image_5b2f80.jpg" border="0" /></a> <br />

</center>

<p>Now let's change the panel background image and panel headers:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330147e2ce5fd9970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330147e2ce5fd9970b image-full" alt="Modified background image and panel headers" title="Modified background image and panel headers" src="/assets/image_37674f.jpg" border="0" /></a> <br />

</center>

<p>What about using a gradient fill and changing the tab header font:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330147e2ce5ec4970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330147e2ce5ec4970b image-full" alt="Gradient fill and different tab header font" title="Gradient fill and different tab header font" src="/assets/image_26ca19.jpg" border="0" /></a> <br />

</center>

<p>The style is persistent, even if you tear off the panels:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833014e5f738145970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833014e5f738145970c" alt="Free-floating panels" title="Free-floating panels" src="/assets/image_a004ec.jpg" border="0" /></a> <br />

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330147e2ce5e7e970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330147e2ce5e7e970b" alt="Free-floating panels" title="Free-floating panels" src="/assets/image_3b6b83.jpg" border="0" /></a> <br />

</center>

<p>You can use different styles in different panels:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833014e5f7380d3970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833014e5f7380d3970c" alt="Different styles in different panels" title="Different styles in different panels" src="/assets/image_d55b06.jpg" border="0" /></a> <br />

</center>

<p>So, how to get there?

<p>First, add new references to your VS project:

<ul>
<li>AdWindows.dll
<li>UIFramework.dll
</ul>

<p>You can find them in the same folder as RevitAPI.dll.

<p>Possibly some other references need to be included as well.
Here is a complete list of the references in my project, although some of them may not be needed for UI customizing purposes:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330147e2ce5c93970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330147e2ce5c93970b" alt="UI customisation references" title="UI customisation references" src="/assets/image_d9cad0.jpg" border="0" /></a> <br />

</center>

<p>Now add this to your ExternalApplication class:

<pre class="code">
<span class="blue">using</span> System.Reflection; <span class="green">// for getting the assembly path</span>
<span class="blue">using</span> System.Windows.Media; <span class="green">// for the graphics</span>
<span class="blue">using</span> System.Windows.Media.Imaging;
&nbsp;
<span class="green">// use an alias because Autodesk.Revit.UI </span>
<span class="green">// uses classes which have same names:</span>
&nbsp;
<span class="blue">using</span> adWin = Autodesk.Windows;
</pre>

<p>Then insert this into your OnStartup method:

<pre class="code">
<span class="blue">public</span> <span class="teal">Result</span> OnStartup( <span class="teal">UIControlledApplication</span> a )
{
&nbsp; <span class="blue">try</span>
&nbsp; {
&nbsp; &nbsp; adWin.<span class="teal">RibbonControl</span> ribbon 
&nbsp; &nbsp; &nbsp; = adWin.<span class="teal">ComponentManager</span>.Ribbon;
&nbsp;
&nbsp; &nbsp; <span class="teal">ImageSource</span> imgbg = <span class="blue">new</span> <span class="teal">BitmapImage</span>( 
&nbsp; &nbsp; &nbsp; <span class="blue">new</span> <span class="teal">Uri</span>( <span class="teal">Path</span>.Combine( 
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Path</span>.GetDirectoryName( 
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Assembly</span>.GetExecutingAssembly().Location ), 
&nbsp; &nbsp; &nbsp; &nbsp; <span class="maroon">&quot;yourBackGroundPicture.jpg&quot;</span> ),
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">UriKind</span>.Relative ) );
&nbsp;
&nbsp; &nbsp; <span class="green">// define an image brush</span>
&nbsp;
&nbsp; &nbsp; <span class="teal">ImageBrush</span> picBrush = <span class="blue">new</span> <span class="teal">ImageBrush</span>(); 
&nbsp; &nbsp; picBrush.ImageSource = imgbg;
&nbsp; &nbsp; picBrush.AlignmentX = <span class="teal">AlignmentX</span>.Left;
&nbsp; &nbsp; picBrush.AlignmentY = <span class="teal">AlignmentY</span>.Top;
&nbsp; &nbsp; picBrush.Stretch = <span class="teal">Stretch</span>.None;
&nbsp; &nbsp; picBrush.TileMode = <span class="teal">TileMode</span>.FlipXY;
&nbsp;
&nbsp; &nbsp; <span class="green">// define a linear brush from top to bottom</span>
&nbsp;
&nbsp; &nbsp; <span class="teal">LinearGradientBrush</span> gradientBrush 
&nbsp; &nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">LinearGradientBrush</span>(); 
&nbsp;
&nbsp; &nbsp; gradientBrush.StartPoint 
&nbsp; &nbsp; &nbsp; = <span class="blue">new</span> System.Windows.<span class="teal">Point</span>( 0, 0 );
&nbsp;
&nbsp; &nbsp; gradientBrush.EndPoint 
&nbsp; &nbsp; &nbsp; = <span class="blue">new</span> System.Windows.<span class="teal">Point</span>( 0, 1 );
&nbsp;
&nbsp; &nbsp; gradientBrush.GradientStops.Add( 
&nbsp; &nbsp; &nbsp; <span class="blue">new</span> <span class="teal">GradientStop</span>( <span class="teal">Colors</span>.White, 0.0 ) );
&nbsp;
&nbsp; &nbsp; gradientBrush.GradientStops.Add( 
&nbsp; &nbsp; &nbsp; <span class="blue">new</span> <span class="teal">GradientStop</span>( <span class="teal">Colors</span>.Blue, 1 ) );
&nbsp;
&nbsp; &nbsp; <span class="green">// change the tab header font</span>
&nbsp;
&nbsp; &nbsp; ribbon.FontFamily = <span class="blue">new</span> System.Windows.Media
&nbsp; &nbsp; &nbsp; .<span class="teal">FontFamily</span>( <span class="maroon">&quot;Bauhaus 93&quot;</span> ); 
&nbsp;
&nbsp; &nbsp; ribbon.FontSize = 10;
&nbsp;
&nbsp; &nbsp; <span class="green">// iterate through the tabs and their panels</span>
&nbsp;
&nbsp; &nbsp; <span class="blue">foreach</span>( adWin.<span class="teal">RibbonTab</span> tab <span class="blue">in</span> ribbon.Tabs )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="blue">foreach</span>( adWin.<span class="teal">RibbonPanel</span> panel <span class="blue">in</span> tab.Panels )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; panel.CustomPanelTitleBarBackground 
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; = gradientBrush;
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; panel.CustomPanelBackground 
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; = picBrush; <span class="green">// use your picture</span>
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="green">//panel.CustomPanelBackground </span>
&nbsp; &nbsp; &nbsp; &nbsp; <span class="green">//&nbsp; = gradientBrush; // use your gradient fill</span>
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; }
&nbsp; &nbsp; adWin.<span class="teal">ComponentManager</span>.UIElementActivated += <span class="blue">new</span> 
&nbsp; &nbsp; &nbsp; <span class="teal">EventHandler</span>&lt;adWin.<span class="teal">UIElementActivatedEventArgs</span>&gt;( 
&nbsp; &nbsp; &nbsp; &nbsp; ComponentManager_UIElementActivated );
&nbsp; }
&nbsp; <span class="blue">catch</span>( <span class="teal">Exception</span> ex )
&nbsp; {
&nbsp; &nbsp; winform.<span class="teal">MessageBox</span>.Show( 
&nbsp; &nbsp; &nbsp; ex.StackTrace + <span class="maroon">&quot;\r\n&quot;</span> + ex.InnerException, 
&nbsp; &nbsp; &nbsp; <span class="maroon">&quot;Error&quot;</span>, winform.<span class="teal">MessageBoxButtons</span>.OK );
&nbsp;
&nbsp; &nbsp; <span class="blue">return</span> <span class="teal">Result</span>.Failed;
&nbsp; }
&nbsp; <span class="blue">return</span> <span class="teal">Result</span>.Succeeded;
}
</pre>

<p>You can access the items inside the panels like this:

<pre class="code">
&nbsp; <span class="blue">foreach</span>( adWin.<span class="teal">RibbonItem</span> item 
&nbsp; &nbsp; <span class="blue">in</span> panel.Source.Items )
&nbsp; {
&nbsp; &nbsp; . . .
&nbsp; }
</pre>

<p>Note that each tab, panel, or item provides event handlers, so you can subscribe to their events like this:

<pre class="code">
&nbsp; tab.Activated 
&nbsp; &nbsp; += <span class="blue">new</span> <span class="teal">EventHandler</span>(tab_Activated);
&nbsp;
&nbsp; panel.PropertyChanged 
&nbsp; &nbsp; += <span class="blue">new</span> PropertyChangedEventHandler(
&nbsp; &nbsp; &nbsp; panel_PropertyChanged);
&nbsp;
&nbsp; item.PropertyChanged 
&nbsp; &nbsp; +=<span class="blue">new</span> PropertyChangedEventHandler(
&nbsp; &nbsp; &nbsp; item_PropertyChanged);
</pre>

<p>There are many undiscovered properties in the Revit Ribbon classes, so feel free to explore them on your own (and tell us your discoveries).

<p>By the way, AutoCAD 2011 also includes the AdWindows.dll assembly, but there is no UIFramework.dll in the AutoCAD program folder (using AutoCAD MEP 2011).

<p>Using the AdWindows.dll assembly, we can access the ribbon control like this:

<pre class="code">
<span class="blue">using</span> Autodesk.Windows;

RibbonControl ribbon = ComponentManager.Ribbon;
</pre>

<p>Since both AutoCAD and Revit include the AdWindows.dll assembly, there are actually two ways to access to the ribbon control in Revit;
either using AdWindows.dll or UIFramework.dll, respectively:

<pre class="code">
&nbsp; <span class="green">// like in AutoCAD:</span>
&nbsp;
&nbsp; RibbonControl ribbon = ComponentManager.Ribbon; 
&nbsp;
&nbsp; <span class="green">// also possible, but requires </span>
&nbsp; <span class="green">// UIFramework.dll reference, </span>
&nbsp; <span class="green">// so we could better use the first method:</span>
&nbsp;
&nbsp; UIFramework.RevitRibbonControl ribbon 
&nbsp; &nbsp; = UIFramework.RevitRibbonControl.RibbonControl;
</pre>

<p>Additionally, one can also use Reflection to retrieve the Ribbon bar from a RibbonPanel p created in the 'normal' way via its p.RibbonControl method:

<pre class="code">
<span class="blue">private</span> Autodesk.Windows.<span class="teal">RibbonPanel</span> 
&nbsp; GetPanelFromPanel( 
&nbsp; &nbsp; Autodesk.Revit.UI.<span class="teal">RibbonPanel</span> panel )
{
&nbsp; <span class="teal">FieldInfo</span> fi
&nbsp; &nbsp; = panel.GetType().GetField( 
&nbsp; &nbsp; &nbsp; <span class="maroon">&quot;m_RibbonPanel&quot;</span>,
&nbsp; &nbsp; &nbsp; <span class="teal">BindingFlags</span>.Instance
&nbsp; &nbsp; &nbsp; | <span class="teal">BindingFlags</span>.NonPublic );
&nbsp;
&nbsp; <span class="blue">if</span>( <span class="blue">null</span> != fi )
&nbsp; {
&nbsp; &nbsp; Autodesk.Windows.<span class="teal">RibbonPanel</span> p 
&nbsp; &nbsp; &nbsp; = fi.GetValue( panel ) 
&nbsp; &nbsp; &nbsp; <span class="blue">as</span> Autodesk.Windows.<span class="teal">RibbonPanel</span>;
&nbsp;
&nbsp; &nbsp; <span class="blue">return</span> p;
&nbsp; }
&nbsp; <span class="blue">return</span> <span class="blue">null</span>;
}
</pre>

<p>So, one can customize the AutoCAD ribbon in a similar way:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833014e864dc149970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833014e864dc149970d image-full" alt="Pimp the AutoCAD ribbon" title="Pimp the AutoCAD ribbon" src="/assets/image_292153.jpg" border="0" /></a> <br />

</center>

<p>This can be performed by the following little piece of code:

<pre class="code">
<span class="blue">public</span> <span class="blue">void</span> Initialize()
{
&nbsp; RibbonControl ribbon = ComponentManager.Ribbon;
&nbsp;
&nbsp; <span class="teal">ImageSource</span> imgbg = <span class="blue">new</span> <span class="teal">BitmapImage</span>(
&nbsp; &nbsp; <span class="blue">new</span> <span class="teal">Uri</span>( <span class="teal">Path</span>.Combine( <span class="teal">Path</span>.GetDirectoryName( 
&nbsp; &nbsp; &nbsp; <span class="teal">Assembly</span>.GetExecutingAssembly().Location ), 
&nbsp; &nbsp; &nbsp; <span class="maroon">&quot;yourBackGroundPicture.jpg&quot;</span> ), 
&nbsp; &nbsp; &nbsp; <span class="teal">UriKind</span>.Relative ) );
&nbsp;
&nbsp; <span class="green">// define an image brush</span>
&nbsp;
&nbsp; <span class="teal">ImageBrush</span> picBrush = <span class="blue">new</span> <span class="teal">ImageBrush</span>(); 
&nbsp; picBrush.ImageSource = imgbg;
&nbsp; picBrush.AlignmentX = <span class="teal">AlignmentX</span>.Left;
&nbsp; picBrush.AlignmentY = <span class="teal">AlignmentY</span>.Top;
&nbsp; picBrush.Stretch = <span class="teal">Stretch</span>.None;
&nbsp; picBrush.TileMode = <span class="teal">TileMode</span>.FlipXY;
&nbsp;
&nbsp; <span class="green">// define a linear brush from top to bottom</span>
&nbsp;
&nbsp; <span class="teal">LinearGradientBrush</span> myLinearGradientBrush 
&nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">LinearGradientBrush</span>(); 
&nbsp;
&nbsp; myLinearGradientBrush.StartPoint 
&nbsp; &nbsp; = <span class="blue">new</span> System.Windows.<span class="teal">Point</span>( 0, 0 );
&nbsp;
&nbsp; myLinearGradientBrush.EndPoint 
&nbsp; &nbsp; = <span class="blue">new</span> System.Windows.<span class="teal">Point</span>( 0, 1 );
&nbsp;
&nbsp; myLinearGradientBrush.GradientStops.Add( 
&nbsp; &nbsp; <span class="blue">new</span> <span class="teal">GradientStop</span>( <span class="teal">Colors</span>.White, 0.0 ) );
&nbsp;
&nbsp; myLinearGradientBrush.GradientStops.Add( 
&nbsp; &nbsp; <span class="blue">new</span> <span class="teal">GradientStop</span>( <span class="teal">Colors</span>.Blue, 1 ) );
&nbsp;
&nbsp; <span class="green">// change the tab header font</span>
&nbsp;
&nbsp; ribbon.FontFamily = <span class="blue">new</span> <span class="teal">FontFamily</span>( <span class="maroon">&quot;Bauhaus 93&quot;</span> ); 
&nbsp; ribbon.FontSize = 10;
&nbsp;
&nbsp; <span class="green">// now iterate through the tabs and their panels</span>
&nbsp;
&nbsp; <span class="blue">foreach</span>( RibbonTab tab <span class="blue">in</span> ribbon.Tabs )
&nbsp; {
&nbsp; &nbsp; <span class="blue">foreach</span>( <span class="teal">RibbonPanel</span> panel <span class="blue">in</span> tab.Panels )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; panel.CustomPanelTitleBarBackground 
&nbsp; &nbsp; &nbsp; &nbsp; = myLinearGradientBrush;
&nbsp;
&nbsp; &nbsp; &nbsp; panel.CustomPanelBackground = picBrush;
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="green">//panel.CustomPanelBackground </span>
&nbsp; &nbsp; &nbsp; <span class="green">//&nbsp; = myLinearGradientBrush; </span>
&nbsp; &nbsp; }
&nbsp; }
}
</pre>

<p>As you see, only the access to the ribbon control differs, but most of the code is identical to the Revit version.

<p>Since we can access the ribbon bar identically in both AutoCAD and Revit using AdWindows.dll, one could easily build a common module for ribbon operations in general.

<p>Also, an event listener can be achieved for both CAD systems:

<pre class="code">
ComponentManager.UIElementActivated 
&nbsp; += <span class="blue">new</span> <span class="teal">EventHandler</span>&lt;Autodesk.Windows
&nbsp; &nbsp; .<span class="teal">UIElementActivatedEventArgs</span>&gt;(
&nbsp; &nbsp; &nbsp; ComponentManager_UIElementActivated );
&nbsp;
<span class="blue">void</span> ComponentManager_UIElementActivated( 
&nbsp; <span class="blue">object</span> sender, 
&nbsp; Autodesk.Windows.<span class="teal">UIElementActivatedEventArgs</span> e )
{
&nbsp; <span class="green">// e.UiElement.PersistId says which item has been pressed</span>
}
</pre>

<p>By the way, this is my first AutoCAD / Revit hybrid contribution.
This might be interesting for AutoCAD developers too, I think.
But after all, it's just playing around...

<p>To wrap this up, here is a fully functional example of a Revit external application to play around with which exercises some of this functionality,

<span class="asset  asset-generic at-xid-6a00e553e168978833014e5f737e7f970c"><a href="http://thebuildingcoder.typepad.com/files/pimpmyrevit2.zip">PimpMyRevit</a></span>.

It includes the Visual Studio solution, an add-in manifest, and the external application implementation source code, which looks like this in its entirety:</a>

<pre class="code">
<span class="blue">using</span> System;
<span class="blue">using</span> System.IO;
<span class="blue">using</span> System.Reflection;
<span class="blue">using</span> winform = System.Windows.Forms;
<span class="blue">using</span> System.Windows.Media;
<span class="blue">using</span> System.Windows.Media.Imaging;
<span class="blue">using</span> Autodesk.Revit.Attributes;
<span class="blue">using</span> Autodesk.Revit.UI;
<span class="blue">using</span> adWin = Autodesk.Windows;
&nbsp;
<span class="blue">namespace</span> PimpMyRevit
{
&nbsp; [<span class="teal">Regeneration</span>( <span class="teal">RegenerationOption</span>.Manual )]
&nbsp; <span class="blue">public</span> <span class="blue">class</span> <span class="teal">UIApp</span> : <span class="teal">IExternalApplication</span>
&nbsp; {
&nbsp; &nbsp; <span class="blue">public</span> <span class="teal">Result</span> OnStartup( <span class="teal">UIControlledApplication</span> a )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="blue">try</span>
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; adWin.<span class="teal">RibbonControl</span> ribbon 
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; = adWin.<span class="teal">ComponentManager</span>.Ribbon;
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">ImageSource</span> imgbg = <span class="blue">new</span> <span class="teal">BitmapImage</span>( 
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">new</span> <span class="teal">Uri</span>( <span class="teal">Path</span>.Combine( 
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Path</span>.GetDirectoryName( 
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Assembly</span>.GetExecutingAssembly().Location ), 
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="maroon">&quot;yourBackGroundPicture.jpg&quot;</span> ),
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">UriKind</span>.Relative ) );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="green">// define an image brush</span>
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">ImageBrush</span> picBrush = <span class="blue">new</span> <span class="teal">ImageBrush</span>(); 
&nbsp; &nbsp; &nbsp; &nbsp; picBrush.ImageSource = imgbg;
&nbsp; &nbsp; &nbsp; &nbsp; picBrush.AlignmentX = <span class="teal">AlignmentX</span>.Left;
&nbsp; &nbsp; &nbsp; &nbsp; picBrush.AlignmentY = <span class="teal">AlignmentY</span>.Top;
&nbsp; &nbsp; &nbsp; &nbsp; picBrush.Stretch = <span class="teal">Stretch</span>.None;
&nbsp; &nbsp; &nbsp; &nbsp; picBrush.TileMode = <span class="teal">TileMode</span>.FlipXY;
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="green">// define a linear brush from top to bottom</span>
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">LinearGradientBrush</span> gradientBrush 
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">LinearGradientBrush</span>(); 
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; gradientBrush.StartPoint 
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; = <span class="blue">new</span> System.Windows.<span class="teal">Point</span>( 0, 0 );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; gradientBrush.EndPoint 
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; = <span class="blue">new</span> System.Windows.<span class="teal">Point</span>( 0, 1 );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; gradientBrush.GradientStops.Add( 
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">new</span> <span class="teal">GradientStop</span>( <span class="teal">Colors</span>.White, 0.0 ) );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; gradientBrush.GradientStops.Add( 
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">new</span> <span class="teal">GradientStop</span>( <span class="teal">Colors</span>.Blue, 1 ) );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="green">// change the tab header font</span>
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; ribbon.FontFamily = <span class="blue">new</span> System.Windows.Media
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; .<span class="teal">FontFamily</span>( <span class="maroon">&quot;Bauhaus 93&quot;</span> ); 
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; ribbon.FontSize = 10;
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="green">// iterate through the tabs and their panels</span>
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">foreach</span>( adWin.<span class="teal">RibbonTab</span> tab <span class="blue">in</span> ribbon.Tabs )
&nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">foreach</span>( adWin.<span class="teal">RibbonPanel</span> panel <span class="blue">in</span> tab.Panels )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; panel.CustomPanelTitleBarBackground 
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; = gradientBrush;
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; panel.CustomPanelBackground 
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; = picBrush; <span class="green">// use your picture</span>
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="green">//panel.CustomPanelBackground </span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="green">//&nbsp; = gradientBrush; // use your gradient fill</span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; adWin.<span class="teal">ComponentManager</span>.UIElementActivated += <span class="blue">new</span> 
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">EventHandler</span>&lt;adWin.<span class="teal">UIElementActivatedEventArgs</span>&gt;( 
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; ComponentManager_UIElementActivated );
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; <span class="blue">catch</span>( <span class="teal">Exception</span> ex )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; winform.<span class="teal">MessageBox</span>.Show( 
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; ex.StackTrace + <span class="maroon">&quot;\r\n&quot;</span> + ex.InnerException, 
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="maroon">&quot;Error&quot;</span>, winform.<span class="teal">MessageBoxButtons</span>.OK );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">return</span> <span class="teal">Result</span>.Failed;
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; <span class="blue">return</span> <span class="teal">Result</span>.Succeeded;
&nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; <span class="blue">public</span> <span class="teal">Result</span> OnShutdown( <span class="teal">UIControlledApplication</span> a )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; adWin.<span class="teal">ComponentManager</span>.UIElementActivated 
&nbsp; &nbsp; &nbsp; &nbsp; -= ComponentManager_UIElementActivated;
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">return</span> <span class="teal">Result</span>.Succeeded;
&nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; <span class="blue">void</span> ComponentManager_UIElementActivated( 
&nbsp; &nbsp; &nbsp; <span class="blue">object</span> sender, 
&nbsp; &nbsp; &nbsp; Autodesk.Windows.<span class="teal">UIElementActivatedEventArgs</span> e )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="green">// e.UiElement.PersistId says which item has been pressed</span>
&nbsp; &nbsp; }
&nbsp; }
}
</pre>

<p>Have fun with trying different pictures and colours.
By the way, one could write an add-in which stores different 'styles' in a xml file...
Revit for boys, Revit for girls, blue theme or pink theme ;-)

<p>One notice: you have to re-invoke the function after all add-ins have been loaded completely, because it obviously does not affect the panels that are created after the call.
One could put the ribbon control into a global variable that can be used by the OnDocumentOpened event or something like that after all panels have been populated.

<p>Here is my Revit started up after loading this external application, with some of the add-ins affected and others left in their original pristine state:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833014e5f737d5e970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833014e5f737d5e970c image-full" alt="Partially pimp Revit add-ins ribbon tab" title="Partially pimp Revit add-ins ribbon tab" src="/assets/image_33eb72.jpg" border="0" /></a> <br />

</center>

<h4>Ribbon Bar Transformations</h4>

<p>Some additions to the pimping theme.

<p>You can apply all kinds of transformations to the ribbon bar, including translation, scaling, and rotation. You can turn it upside down, or throw it into space somewhere...

<p>After the transformation, the ribbon buttons remain just as functional as before, e.g. tool tips are displayed and the buttons can be selected as usual.

<p>Because the Ribbon bar is a WPF element, it can be transformed this way:

<pre class="code">
<span class="blue">using</span> adWin = Autodesk.Windows;

&nbsp; adWin.<span class="teal">RibbonControl</span> ribbon 
&nbsp; &nbsp; = adWin.<span class="teal">ComponentManager</span>.Ribbon;
&nbsp;
&nbsp; <span class="teal">TransformGroup</span> group = <span class="blue">new</span> <span class="teal">TransformGroup</span>();
&nbsp; <span class="green">//group.Children.Add(new RotateTransform(-2));</span>
&nbsp; group.Children.Add( <span class="blue">new</span> <span class="teal">TranslateTransform</span>( 
&nbsp; &nbsp; ribbon.ActualWidth * 0.25, 160 ) );
&nbsp;
&nbsp; group.Children.Add( <span class="blue">new</span> <span class="teal">ScaleTransform</span>( 
&nbsp; &nbsp; scale, scale ) );
&nbsp;
&nbsp; group.Children.Add( <span class="blue">new</span> <span class="teal">SkewTransform</span>( 
&nbsp; &nbsp; 15, -3 ) );
&nbsp;
&nbsp; ribbon.RenderTransform = group;
</pre>

<p>Feel free to test different combinations.
Below are some examples.
To begin with, here is The Ribbon bar in its default state:

 
<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833014e864dbea4970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833014e864dbea4970d image-full" alt="Revit ribbon bar in default state" title="Revit ribbon bar in default state" src="/assets/image_28b964.jpg" border="0" /></a> <br />

</center>




<pre class="code">
&nbsp; adWin.<span class="teal">ComponentManager</span>.Ribbon.RenderTransform 
&nbsp; &nbsp; = <span class="blue">new</span> System.Windows.Media.<span class="teal">ScaleTransform</span>( 
&nbsp; &nbsp; &nbsp; 0.5, 0.75 );
</pre>

 
<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330147e2ce58ca970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330147e2ce58ca970b image-full" alt="Revit ribbon bar with scaling transform" title="Revit ribbon bar with scaling transform" src="/assets/image_33b961.jpg" border="0" /></a> <br />

</center>



<pre class="code">
&nbsp; adWin.<span class="teal">ComponentManager</span>.Ribbon.RenderTransform 
&nbsp; &nbsp; = <span class="blue">new</span> System.Windows.Media.<span class="teal">RotateTransform</span>( 
&nbsp; &nbsp; &nbsp; 5 );
</pre>

 
<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833014e864dbd75970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833014e864dbd75970d image-full" alt="Revit ribbon bar with rotation transform" title="Revit ribbon bar with rotation transform" src="/assets/image_11a193.jpg" border="0" /></a> <br />

</center>



<pre class="code">
&nbsp; adWin.<span class="teal">ComponentManager</span>.Ribbon.RenderTransform 
&nbsp; &nbsp; = <span class="blue">new</span> System.Windows.Media.<span class="teal">TranslateTransform</span>( 
&nbsp; &nbsp; &nbsp; 100, 25 );
</pre>

 
<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833014e5f737a84970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833014e5f737a84970c image-full" alt="Revit ribbon bar with translation transform" title="Revit ribbon bar with translation transform" src="/assets/image_aade21.jpg" border="0" /></a> <br />

</center>



<pre class="code">
&nbsp; adWin.<span class="teal">ComponentManager</span>.Ribbon.RenderTransform 
&nbsp; &nbsp; = <span class="blue">new</span> System.Windows.Media.<span class="teal">SkewTransform</span>( 
&nbsp; &nbsp; &nbsp; -15, 0 );
</pre>

 
<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330147e2ce5750970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330147e2ce5750970b image-full" alt="Revit ribbon bar with skew transform" title="Revit ribbon bar with skew transform" src="/assets/image_f628ce.jpg" border="0" /></a> <br />

</center>



<pre class="code">
&nbsp; System.Windows.Media.<span class="teal">TransformGroup</span> g 
&nbsp; &nbsp; = <span class="blue">new</span> System.Windows.Media.<span class="teal">TransformGroup</span>();
&nbsp;
&nbsp; g.Children.Add( <span class="blue">new</span> <span class="teal">ScaleTransform</span>( 0.75, 0.75 ) );
&nbsp; g.Children.Add( <span class="blue">new</span> <span class="teal">SkewTransform</span>( -25, 0 ) );
&nbsp; g.Children.Add( <span class="blue">new</span> <span class="teal">TranslateTransform</span>( 50, 10 ) );
&nbsp;
&nbsp; adWin.<span class="teal">ComponentManager</span>.Ribbon.RenderTransform = g;
</pre>

 
<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833014e5f73792a970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833014e5f73792a970c image-full" alt="Revit ribbon bar with several concatenated transformations" title="Revit ribbon bar with several concatenated transformations" src="/assets/image_08acea.jpg" border="0" /></a> <br />

</center>


<p>The black area is the container of the Ribbon; it may be possible to place your own WPF forms into it.
But this topic is more WPF than Revit API related.

<h4>Ribbon Transformations for AutoCAD</h4>

<p>As said, the same thing works in AutoCAD as well. 
This is the AutoCAD version:

<pre class="code">
<span class="blue">using</span> Autodesk.Windows;
<span class="blue">using</span> Autodesk.AutoCAD.Runtime;
<span class="blue">using</span> System.Windows.Media;
&nbsp;
<span class="blue">namespace</span> PimpMyAutoCAD
{
&nbsp; <span class="blue">public</span> <span class="blue">class</span> <span class="teal">UIApp</span> : IExtensionApplication
&nbsp; {
&nbsp; &nbsp; <span class="blue">public</span> <span class="blue">void</span> Initialize()
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="blue">try</span>
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">TransformGroup</span> group = <span class="blue">new</span> <span class="teal">TransformGroup</span>();
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; group.Children.Add( 
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">new</span> <span class="teal">ScaleTransform</span>( 0.96, 1 ) );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; group.Children.Add( 
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">new</span> <span class="teal">SkewTransform</span>( -25, 0 ) );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; group.Children.Add( 
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">new</span> <span class="teal">TranslateTransform</span>( 50, 0 ) );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">ComponentManager</span>.Ribbon.RenderTransform 
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; = group;
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; <span class="blue">catch</span>
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; <span class="blue">public</span> <span class="blue">void</span> Terminate()
&nbsp; &nbsp; {
&nbsp; &nbsp; }
&nbsp; }
}
</pre>

<p>Here is the result of running that:

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330147e2ce55e7970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330147e2ce55e7970b image-full" alt="Pimp my AutoCAD transformation" title="Pimp my AutoCAD transformation" src="/assets/image_8439aa.jpg" border="0" /></a> <br />

</center>
