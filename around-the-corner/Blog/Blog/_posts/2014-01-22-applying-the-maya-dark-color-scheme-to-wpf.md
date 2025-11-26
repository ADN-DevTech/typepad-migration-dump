---
layout: "post"
title: "Applying the Maya Dark Color Scheme to WPF"
date: "2014-01-22 05:23:37"
author: "Cyrille Fauvel"
categories:
  - ".Net"
  - "Cyrille Fauvel"
  - "Maya"
  - "MEL"
  - "Plug-in"
  - "Samples"
  - "UI"
  - "Visual Studio"
  - "Windows"
  - "WPF"
original_url: "https://around-the-corner.typepad.com/adn/2014/01/applying-the-maya-dark-color-scheme-to-wpf.html "
typepad_basename: "applying-the-maya-dark-color-scheme-to-wpf"
typepad_status: "Publish"
---

<p>Sorry it&#39;s been such a long time since we all wrote a blog...we will get better at updating!!! As you know I was travelling a lot in November/December for business reasons, but I also got asked to work on few important Cloud and mobile projects lately that took me away from the R&amp;D aspect of my job. Anyway, it is back to normal now… till next time ;)</p>
<p><a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d019b05202144970d-pi" style="display: inline;"><img alt="Mixing-paint-colors-palette" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d019b05202144970d img-responsive" src="/assets/image_935f37.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Mixing-paint-colors-palette" /></a></p>
<p>Almost 2 years back, almost around this time of the year, I worked on a Maya .Net API prototype as a proof of concept. The main goal of this project was to show it was possible and not too hard to write a Maya .Net API or at least use .Net in Maya. At the beginning it was to become a sample and whitepaper, but it turn out to be different for the good (I did somewhat started to talk about it <a href="http://around-the-corner.typepad.com/adn/2012/05/enabling-net-in-a-maya-plug-in-.html" target="_self">here</a>). Unlike the Python 1.0 API (introduced in Maya 8.5), I wanted to make sure that the .Net API was a true .Net API and targeting .Net/C# developers. As you may know, Dean Edmond the Maya API architect is working on a Python 2.0 API implementation which would ultimately introduce a more ‘Pythonic’ API than its version 1.0. Unfortunately that version 2.0 is not yet completed and is incompatible with version 1.0. I’ll probably come back to that in another post.</p>
<p>Another goal of my Maya .Net API project was to get rid of ‘unnecessary’ coding such as the ‘command’, ‘node’ registration, etc… but instead uses .Net paradigm with metadata &amp; decorators. Therefore all the Maya .Net classes, methods, properties are decorated and available for introspection and reflection. Not only it helps to make the code a lot more clear to write and read, but it gives me access at runtime to information that I needed for another project I haven’t yet started.</p>
<p>Anyway, once the prototype was working with 20% of the API converted to show some example, the project was completed by a larger team and get introduced into Maya 2013.5 (December 2012). One thing I forgot, and this is my fault to be honest, is that you may consider writing compiled or scripted code in C# using the Maya .Net API, but also be interested in using WPF. While it would work nicely, the WPF windows will come as standard WPF windows using your Windows box color scheme definition and not the Maya Dark Color Scheme :(</p>
<p>Consider this MEL example and its WPF equivalent:</p>
<table>
<tbody>
<tr>
<td><a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d01a3fc930dd8970b-pi" style="display: inline;"><img alt="MelDialog" class="asset  asset-image at-xid-6a0163057a21c8970d01a3fc930dd8970b img-responsive" src="/assets/image_f13149.jpg" style="width: 240px;" title="MelDialog" /></a></td>
<td valign="top"><a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d01a3fc931009970b-pi" style="display: inline;"><img alt="WpfWindow" class="asset  asset-image at-xid-6a0163057a21c8970d01a3fc931009970b img-responsive" src="/assets/image_17134a.jpg" style="width: 240px;" title="WpfWindow" /></a></td>
</tr>
</tbody>
</table>
<p>There is a WPF example in the Maya devkit which uses the Maya color scheme, but it is all hardcoded into the window XAML definition and requires you to code colors on each controls of your form :( Moreover, because of the way XAML/WPF is working to define controls, this sample is not matching perfectly the Maya Dark Color Scheme. There is still quite a lot differences, but if you not too worried about that it is ok, till you may got a lot of work on larger dialogs / projects. See below:</p>
<p><a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d019b051fd30b970d-pi" style="display: inline;"><img alt="WpfDevkitSample" class="asset  asset-image at-xid-6a0163057a21c8970d019b051fd30b970d img-responsive" src="/assets/image_3ec6df.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="WpfDevkitSample" /></a></p>
<p>So last week, I took a look to the issue and what could be done. Microsoft did an excellent job in designing WPF, so you can design dialog, controls by APi and/or xaml definition. I know there are sometimes performance issues using WPF, but it is pretty easy to use.</p>
<p>I also reworked the Maya devkit WPF sample to support Maya cameras, Lights, and multiple Mesh nodes with basics shaders in the preview control. The sample can run as a plug-in inside Maya, but as a standalone Maya application too.</p>
<p>WPF colors and layouts are controlled via Styles and Templates. You can create these styles and templates either by code or xaml. My first thought was to bind the Maya internal QT style class to save me time, but there was 2 big roadblocks: First, I immediately saw that it wasn’t that easy to do since controls are organized/layed-out differently. But second, I also saw there was some performance issue too. Yes it would have been dynamic in case the user or an application change a color at teh application/QT level, but I believe that was probably not really necessary. Instead, I used the xaml approach which offers some flexibility too. One is that you can merge many xaml file together and override styles, which means someone can still change the color if he wants too. Second is that you can use the color scheme outside Maya since you do not have any dependancy in Maya and/or QT. This why I’ll provide below a Maya standalone example to demonstrate this.</p>
<p>The first issue we need to resolve is that Maya is not really a .Net application, it only exposes a .Net API. That means it is not even WPF capable on its own. It would need some help to get initialized&#0160;properly, and such a way that it would break any other WPF plug-in already loaded. To do this our DarkScheme assembly will provide a default WPF App object/class to register the style dictionaries we need.</p>
<p>The important thing is to make sure we got the WPF App &amp; Dictionary initialized, and we do this via xaml and the MayaTheme.Initialize() method.</p>
<pre class="brush: xml; toolbar: false;">&lt;Application x:Class=&quot;Autodesk.Maya.App&quot;
             xmlns=&quot;http://schemas.microsoft.com/winfx/2006/xaml/presentation&quot;
             xmlns:x=&quot;http://schemas.microsoft.com/winfx/2006/xaml&quot;&gt;
    &lt;Application.Resources&gt;
        &lt;ResourceDictionary&gt;
            &lt;ResourceDictionary.MergedDictionaries&gt;

                &lt;!-- Set default skin --&gt;
                &lt;ResourceDictionary Source=&quot;Themes\Skins\QadskDarkStyle.xaml&quot;/&gt;
                &lt;ResourceDictionary Source=&quot;Themes\MayaStyle.xaml&quot;/&gt;
                
            &lt;/ResourceDictionary.MergedDictionaries&gt;
        &lt;/ResourceDictionary&gt;
    &lt;/Application.Resources&gt;
&lt;/Application&gt;
</pre>
<p>Our main color scheme dictionary is the QadskDarkStyle.xaml file which will be reviewed later in this post.</p>
<pre class="brush: csharp; toolbar: false;">namespace Autodesk.Maya {

	public class MayaTheme {
		public static Application _app = null;

		public static bool Initialize (Application app) {
			 if ( _app == null &amp;&amp; app == null )
				_app = new App ();
			else if ( app != null )
				_app = app ;

			if ( Application.ResourceAssembly == null )
				Application.ResourceAssembly = typeof (MayaTheme).Assembly;

			return (true);
		}

		public static bool SetMayaIcon (Window window) {
			//string [] test =typeof (MayaTheme).Assembly.GetManifestResourceNames () ;
			// Need to be an embedded resources
			System.IO.Stream file =typeof (MayaTheme).Assembly.GetManifestResourceStream (&quot;MayaTheme.Resources.maya.ico&quot;) ;
			var icon =BitmapFrame.Create (file) ;
			window.Icon =icon ;
			return (true) ;
		}

	}

}
</pre>
<p>Note that this version of Initialize() does not merge the xaml color dictionaries in case you already have an WPF application object running, as I expect that application would have merge the dictionary already, liek this code below.&#0160;But it would be possible to merge them by code since it would not be possible for you to modify someone else application xaml definition. The example below show you how to use the color scheme in a another application such as a Maya standalone application and reference the MayaTheme assembly xaml resources.</p>
<pre class="brush: xml; toolbar: false;">&lt;Application x:Class=&quot;Autodesk.Maya.Samples.MayaWpfStandAlone.App&quot;
             xmlns=&quot;http://schemas.microsoft.com/winfx/2006/xaml/presentation&quot;
             xmlns:x=&quot;http://schemas.microsoft.com/winfx/2006/xaml&quot;
             StartupUri=&quot;DAGExplorer.xaml&quot;
             Startup=&quot;App_Startup&quot;
             Exit=&quot;App_Exit&quot;&gt;
    &lt;Application.Resources&gt;

		&lt;ResourceDictionary&gt;
			&lt;ResourceDictionary.MergedDictionaries&gt;
				&lt;!-- Set default skin --&gt;
				&lt;ResourceDictionary Source=&quot;/MayaTheme;component/Themes/Skins/QadskDarkStyle.xaml&quot; /&gt;
				&lt;ResourceDictionary Source=&quot;/MayaTheme;component/Themes/MayaStyle.xaml&quot; /&gt;
			&lt;/ResourceDictionary.MergedDictionaries&gt;
			&lt;!-- common:SettingConverter x:Key=&quot;SettingConv&quot;&gt;&lt;/common:SettingConverter --&gt;
		&lt;/ResourceDictionary&gt;
		
	&lt;/Application.Resources&gt;
&lt;/Application&gt;
</pre>
<p>But from a Maya plug-in, the only thing you need to do is the following: call the Initialize() method from your plug-in initialize or you plug-in command.</p>
<pre class="brush: csharp; toolbar: false;">// This line is not mandatory, but improves loading performances
[assembly: ExtensionPlugin (typeof (Autodesk.Maya.Samples.MayaWpfPlugin.MyPlugin))]

namespace Autodesk.Maya.Samples.MayaWpfPlugin {

	public class MyPlugin : IExtensionPlugin {

		public bool InitializePlugin () {
			bool b = MayaTheme.Initialize (null);
			return true;
		}

		public bool UninitializePlugin () {
			return true;
		}

		public string GetMayaDotNetSdkBuildVersion () {
			return &quot;&quot;;
		}

	}

}
</pre>
<p>From there, you are almost done. In theory, you&#39;re done, the whole WPF system has now switch to the new colors and control definition. The only thing left is that there is a bug in WPF for the &#39;Window&#39; class. While you can define a Window style/template, WPF does not apply it to your WPF windows. That means your WPF window will come with default background color and default icon while its controls will have the correct settings.</p>
<p>So we need to tell our Window class to use our Window style. It can be done again via API or XAML. I found the XAML way a lot easier, as we only need to add the follwoing &lt;Style&gt; attribute in our XAML window definition. I.e.:</p>
<pre class="brush: xml; toolbar: false;">&lt;Window x:Class=&quot;Autodesk.Maya.Samples.MayaWpfPlugin.DAGExplorer&quot;
        xmlns=&quot;http://schemas.microsoft.com/winfx/2006/xaml/presentation&quot;
        xmlns:x=&quot;http://schemas.microsoft.com/winfx/2006/xaml&quot;
        Title=&quot;DAGExplorer&quot;
        Height=&quot;600&quot;
        Width=&quot;800&quot;
        Style=&quot;{DynamicResource MayaStyle}&quot;
&gt;
</pre>
<p>This will take care of changing the color and icon for you. If the style is missing or not found, it will revert to the standard style current in your application.</p>
<p>And here is the results (MEL and WPF again)</p>
<table>
<tbody>
<tr>
<td><a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d01a3fc930dd8970b-pi" style="display: inline;"><img alt="MelDialog" class="asset  asset-image at-xid-6a0163057a21c8970d01a3fc930dd8970b img-responsive" src="/assets/image_f13149.jpg" style="width: 240px;" title="MelDialog" /></a></td>
<td valign="top"><a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d01a3fc93ddfb970b-pi" style="display: inline;"><img alt="WpfWindowWithStyle" class="asset  asset-image at-xid-6a0163057a21c8970d01a3fc93ddfb970b img-responsive" src="/assets/image_117e0e.jpg" style="width: 240px;" title="WpfWindowWithStyle" /></a></td>
</tr>
</tbody>
</table>
<p>If you find any problem/bug with this implementation, please let me know. I&#39;ll be happy to fix them.</p>
<p>There is also 2 additional WPF samples using the Dark color scheme available as well (actually this is the same sample but running as a plug-in for Maya and/or as a Maya standalone application).</p>
<p><a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d019b05204686970d-pi" style="display: inline;"><img alt="DAGExplorer" class="asset  asset-image at-xid-6a0163057a21c8970d019b05204686970d img-responsive" src="/assets/image_fa14a6.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="DAGExplorer" /></a></p>
<p>You can find the full source code and documentation <a href="https://github.com/ADN-DevTech/Maya-Net-Wpf-DarkScheme" target="_self">here<br /></a><a href="https://github.com/ADN-DevTech/Maya-Net-Wpf-DarkScheme" target="_self">https://github.com/ADN-DevTech/Maya-Net-Wpf-DarkScheme</a></p>
