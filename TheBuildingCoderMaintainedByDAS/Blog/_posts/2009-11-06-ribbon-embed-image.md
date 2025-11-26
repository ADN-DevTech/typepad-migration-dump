---
layout: "post"
title: "Ribbon Embed Image"
date: "2009-11-06 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "Ribbon"
  - "SDK Samples"
  - "User Interface"
  - "Win32"
original_url: "https://thebuildingcoder.typepad.com/blog/2009/11/ribbon-embed-image.html "
typepad_basename: "ribbon-embed-image"
typepad_status: "Publish"
---

<p>Previous posts discussed the 

<a href="http://thebuildingcoder.typepad.com/blog/2009/05/revit-2010-ribbon-api.html">
Revit 2010 Ribbon API</a>,

the associated Ribbon SDK Ribbon sample, and my use of it for the 

<a href="http://thebuildingcoder.typepad.com/blog/2009/08/mep-sample-ribbon-panel.html">
MEP sample ribbon panel</a>.

Krispy5 very friendlily posted a series of 

<a href="http://thebuildingcoder.typepad.com/blog/2009/08/mep-sample-ribbon-panel.html#comment-6a00e553e1689788330120a6a18e08970c">
comments</a> to 

the latter describing a detailed solution to embed the images used in the ribbon buttons into the Revit external application executable assembly.
I implemented a sample application RibbonEmbedImage to make Krispy's solution more readable and readily available.
This sample is obviously less a demonstration of any Revit API functionality; 
rather it shows how to make use of Visual Studio and the .NET framework to handle bitmaps as embedded resources.

<p>

<span class="asset  asset-generic at-xid-6a00e553e1689788330120a6b11eb7970c"><a href="http://thebuildingcoder.typepad.com/files/ribbonembedimage-2.zip">RibbonEmbedImage</a></span>

implements the following:

<Ul>
<li>An embedded bitmap resource with its build action set to 'Embedded Resource'.
<li>Krispy's method GetEmbeddedImage to read the embedded bitmap resource and return a BitmapSource instance.
<li>A method AddRibbonPanel to create a custom ribbon panel labelled "Ribbon Embed Image" containing a push button labelled "Hello" and displaying the embedded resource bitmap as its image.
<li>An external application calling AddRibbonPanel in its OnStartup method.
<li>An external command implementing a trivial Execute method which is invoked by the Hello button.
</ul>

<p>This is what the resulting custom ribbon panel looks like when I drag it off the ribbon to a free floating state:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330120a656ffd8970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330120a656ffd8970b" alt="RibbonEmbedImage sample custom ribbon panel" title="RibbonEmbedImage sample custom ribbon panel" src="/assets/image_76363f.jpg" border="0"  /></a> <br />

</center>

<p>Let's look at the source code of the items mentioned above one by one.
There is no code to display for the bitmap, but as said, it needs to have its build action set to 'Embedded Resource'.

<p>Here are the namespaces we make use of:

<pre class="code">
<span class="blue">using</span> System;
<span class="blue">using</span> System.Collections.Generic;
<span class="blue">using</span> System.Diagnostics;
<span class="blue">using</span> System.IO;
<span class="blue">using</span> System.Reflection;
<span class="blue">using</span> System.Windows.Media.Imaging;
<span class="blue">using</span> Autodesk.Revit;
</pre>

<p>Besides the standard references to System and RevitAPI, these will require references to the following assemblies:

<ul>
<li>PresentationCore
<li>WindowsBase
</ul>

<p>The application defines its own namespace RibbonEmbedImage, and within it two classes named App and Command, i.e. the following global source code structure:

<pre class="code">
<span class="blue">namespace</span> RibbonEmbedImage
{
&nbsp; <span class="blue">public</span> <span class="blue">class</span> <span class="teal">App</span> : <span class="teal">IExternalApplication</span>
&nbsp; {
&nbsp; }
&nbsp;
&nbsp; <span class="blue">public</span> <span class="blue">class</span> <span class="teal">Command</span> : <span class="teal">IExternalCommand</span>
&nbsp; {
&nbsp; }
}
</pre>

<p>Within the App class, we first of all define a constant message to be used as the ribbon panel button tooltip 
and displayed by the external command invoked by the button:

<pre class="code">
<span class="blue">public</span> <span class="blue">const</span> <span class="blue">string</span> Message =
&nbsp; <span class="maroon">&quot;Ribbon Embed Image says 'Hello world'&quot;</span>
&nbsp; + <span class="maroon">&quot; via an embedded bitmap resource.&quot;</span>;
</pre>

<p>Then we add the definition of Krispy's method GetEmbeddedImage to read the embedded bitmap resource and return a BitmapSource instance:

<pre class="code">
<span class="blue">static</span> <span class="teal">BitmapSource</span> GetEmbeddedImage( <span class="blue">string</span> name )
{
&nbsp; <span class="blue">try</span>
&nbsp; {
&nbsp; &nbsp; <span class="teal">Assembly</span> a = <span class="teal">Assembly</span>.GetExecutingAssembly();
&nbsp; &nbsp; <span class="teal">Stream</span> s = a.GetManifestResourceStream( name );
&nbsp; &nbsp; <span class="blue">return</span> <span class="teal">BitmapFrame</span>.Create( s );
&nbsp; }
&nbsp; <span class="blue">catch</span>
&nbsp; {
&nbsp; &nbsp; <span class="blue">return</span> <span class="blue">null</span>;
&nbsp; }
}
</pre>

<p>It extracts a bitmap resource embedded within the executing assembly and returns a corresponding BitmapSource object.
The 'name' argument is the fully qualified resource name, i.e. includes the application namespace etc.

<p>Next comes the AddRibbonPanel method which creates a custom ribbon panel labelled "Ribbon Embed Image" containing a push button labelled "Hello" and displaying the embedded resource bitmap as its image:

<pre class="code">
<span class="blue">static</span> <span class="blue">void</span> AddRibbonPanel(
&nbsp; <span class="teal">ControlledApplication</span> a )
{
&nbsp; <span class="blue">string</span> path = <span class="teal">Assembly</span>.GetExecutingAssembly().Location;
&nbsp;
&nbsp; <span class="teal">PushButtonData</span> data = <span class="blue">new</span> <span class="teal">PushButtonData</span>(
&nbsp; &nbsp; <span class="maroon">&quot;Hello&quot;</span>, <span class="maroon">&quot;Hello&quot;</span>, path, <span class="maroon">&quot;RibbonEmbedImage.Command&quot;</span> );
&nbsp;
&nbsp; <span class="teal">BitmapSource</span> bitmap = GetEmbeddedImage( 
&nbsp; &nbsp; <span class="maroon">&quot;RibbonEmbedImage.Bitmap1.bmp&quot;</span> );
&nbsp;
&nbsp; data.Image = bitmap;
&nbsp; data.LargeImage = bitmap;
&nbsp; data.ToolTip = Message;
&nbsp;
&nbsp; <span class="teal">RibbonPanel</span> panel = a.CreateRibbonPanel(
&nbsp; &nbsp; <span class="maroon">&quot;Ribbon Embed Image&quot;</span> );
&nbsp;
&nbsp; <span class="teal">RibbonItem</span> item = panel.AddButton( data );
}
</pre>

<p>The only remaining part of the external application class is the implementation of the required interface methods and the call to AddRibbonPanel in the OnStartup method:

<pre class="code">
<span class="blue">public</span> <span class="teal">IExternalApplication</span>.<span class="teal">Result</span> OnStartup( 
&nbsp; <span class="teal">ControlledApplication</span> a )
{
&nbsp; AddRibbonPanel( a );
&nbsp; <span class="blue">return</span> <span class="teal">IExternalApplication</span>.<span class="teal">Result</span>.Succeeded;
}
&nbsp;
<span class="blue">public</span> <span class="teal">IExternalApplication</span>.<span class="teal">Result</span> OnShutdown(
&nbsp; <span class="teal">ControlledApplication</span> a )
{
&nbsp; <span class="blue">return</span> <span class="teal">IExternalApplication</span>.<span class="teal">Result</span>.Succeeded;
}
</pre>

<p>All that is needed for the external command is the implementation of the required interface, i.e. the Execute method.
It reuses the application class Message constant and simply passes it back as a warning message to Revit:

<pre class="code">
&nbsp; &nbsp; <span class="blue">public</span> <span class="teal">IExternalCommand</span>.<span class="teal">Result</span> Execute( 
&nbsp; &nbsp; &nbsp; <span class="teal">ExternalCommandData</span> commandData, 
&nbsp; &nbsp; &nbsp; <span class="blue">ref</span> <span class="blue">string</span> message, 
&nbsp; &nbsp; &nbsp; <span class="teal">ElementSet</span> elements )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; message = <span class="teal">App</span>.Message;
&nbsp; &nbsp; &nbsp; <span class="blue">return</span> <span class="teal">IExternalCommand</span>.<span class="teal">Result</span>.Failed;
&nbsp; &nbsp; }
</pre>

<p>Here is the complete 

<span class="asset  asset-generic at-xid-6a00e553e1689788330120a6b11eb7970c"><a href="http://thebuildingcoder.typepad.com/files/ribbonembedimage-2.zip">RibbonEmbedImage</a></span>

source code and Visual Studio solution.</p>

<p>Many thanks to Krispy for providing this solution!
I hope it will prove useful to everybody needing bitmaps or any other resources in a Revit plug-in, whether it be for ribbon panel buttons or other purposes.
