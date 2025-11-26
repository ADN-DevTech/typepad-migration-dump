---
layout: "post"
title: "Localization strategies for 3ds Max Plug-ins"
date: "2013-06-06 01:33:00"
author: "Cyrille Fauvel"
categories:
  - "3ds Max"
  - "Cyrille Fauvel"
  - "Plug-in"
  - "Plugin of the Month"
  - "Tools"
original_url: "https://around-the-corner.typepad.com/adn/2013/06/localization-strategies-for-3ds-max-plug-ins.html "
typepad_basename: "localization-strategies-for-3ds-max-plug-ins"
typepad_status: "Publish"
---

<p>
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d0192aac09ca5970d-pi" style="display: inline;"><img alt="Characters" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d0192aac09ca5970d" src="/assets/image_dc53f1.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Characters" /></a><br />With the coming localized Autodesk Exchange stores, it is becoming more important for plug-in developers to localize their software. The exchange store provides a unique opportunity to be in front of customers all over the world. The best experience for a customer is to provide your software in their localized language.</p>
<p>
This post is meant to provide the resources to help you understand the localization of 3ds Max plug-ins and supporting customization files.</p>
<h2>
C++ based plug-ins</h2>
<p>
3ds Max is now using the Unicode string handling procedures, so you must first make sure your plug-ins are fully Unicode compliant. For localization of the strings, there are two different approaches to localization for 3ds Max.</p>
<h3> 
Unicode</h3>
<p>
Your C++ based plugins are now required to be compiled as supporting Unicode strings. There is a complete section detailing all the aspects of this here:</p>
<p><a href="http://docs.autodesk.com/3DSMAX/16/ENU/3ds-Max-SDK-Programmer-Guide/files/GUID-F5D23A57-F714-496A-BEE6-A98BD6917822.htm" target="_self">
http://docs.autodesk.com/3DSMAX/16/ENU/3ds-Max-SDK-Programmer-Guide/files/GUID-F5D23A57-F714-496A-BEE6-A98BD6917822.htm</a></p>
<h3>
Language Pack methodology</h3>
<p>
Note that 3ds Max is now making use of language packs since the 2013 release as the primary localization technique. This technology requires “binary translation”. There are a variety of tools out there that can help with the binary approach to translation (including the Visual Studio resource editor, but it is not very convenient for a large number of strings).  One such commercial tool is by SDL called <a href="http://www.sdl.com/products/sdl-passolo/index.html" target="_self">Passolo</a>. The binary translation approach is recommended because it will work with the 3ds Max installation and startup process that supports all languages where 3ds Max is localized and requires only a single call in your source code that performs the language selection.</p>
<p>
Language packs are based on Microsoft technology called Windows Multilingual User Interface (MUI). You can find details on this technology here:</p>
<p><a href="http://msdn.microsoft.com/en-us/goglobal/bb978454" target="_self">
http://msdn.microsoft.com/en-us/goglobal/bb978454</a></p>
<p>
The most comprehensive explanation for recent 3ds Max versions (2013 and later) using the MUI language pack technology is provided in the 3ds Max SDK help information. See this topic for details:</p>
<p><a href="http://docs.autodesk.com/3DSMAX/16/ENU/3ds-Max-SDK-Programmer-Guide/files/GUID-82AEACF1-2E25-49A4-AD31-52CC5F00B755.htm" target="_self">
http://docs.autodesk.com/3DSMAX/16/ENU/3ds-Max-SDK-Programmer-Guide/files/GUID-82AEACF1-2E25-49A4-AD31-52CC5F00B755.htm</a></p>
<p>
Here you will find information about what is included with a 3ds Max based language pack. This includes both the plug-in localization resource binaries, and also supporting files including MAXScript, UI files, and configuration file information.</p>
<p>
An example of this approach is provided here:<br /><a href="http://adnsparks.autodesk.com/adn/servlet/item?siteID=6681818&amp;id=18271211&amp;linkID=6927897" target="_self">
http://adnsparks.autodesk.com/adn/servlet/item?siteID=6681818&amp;id=18271211&amp;linkID=6927897</a></p>
<p>or here <a href="http://around-the-corner.typepad.com/files/3ds_max_language_packs_supplement_edition.pdf" target="_self">3ds_max_language_packs_supplement_edition.pdf (857.6K)</a></p>
<h3>
Resource File methodology</h3>
<p>
You may instead choose to use the older technique of localizing your resource files and building separate resource only DLLs for each language. Here you will maintain separate resource files, each with the translated strings. During the startup, you will determine the language and the load the appropriate language DLL. Complete details are described in the 3ds Max SDK help here:</p>
<p><a href="http://docs.autodesk.com/3DSMAX/16/ENU/3ds-Max-SDK-Programmer-Guide/files/GUID-97DBB58E-FA66-4522-A43F-B9BADB132009.htm" target="_self">
http://docs.autodesk.com/3DSMAX/16/ENU/3ds-Max-SDK-Programmer-Guide/files/GUID-97DBB58E-FA66-4522-A43F-B9BADB132009.htm</a></p>
<p>
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d019102f84360970c-pi" style="display: inline;"><img alt="Worldflags" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d019102f84360970c" src="/assets/image_63352e.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Worldflags" /></a></p>
<h2>MAXScript</h2>
<p>
MacroScripts that are used for CUI scripting aspects in 3ds Max has localization support. There is a topic here that describes the technique for category globalization:</p>
<p><a href="http://docs.autodesk.com/3DSMAX/16/ENU/MAXScript-Help/files/GUID-23013A3F-2F69-4970-9ECE-D53A8A3DED28.htm" target="_self">
http://docs.autodesk.com/3DSMAX/16/ENU/MAXScript-Help/files/GUID-23013A3F-2F69-4970-9ECE-D53A8A3DED28.htm</a></p>
<p>
Then you can also provide localized strings via compile script resources. This process is described here:</p>
<p><a href="http://docs.autodesk.com/3DSMAX/16/ENU/MAXScript-Help/files/GUID-4E926577-61C6-463E-9238-D422EFC03718.htm" target="_self">
http://docs.autodesk.com/3DSMAX/16/ENU/MAXScript-Help/files/GUID-4E926577-61C6-463E-9238-D422EFC03718.htm</a></p>
<p>
Note that there are also changes to support Unicode and file I/O. MAXScript Unicode details are provided here:</p>
<p><a href="http://docs.autodesk.com/3DSMAX/16/ENU/MAXScript-Help/files/GUID-30A3165F-752E-423B-A662-46B4A78E89F5.htm" target="_self">http://docs.autodesk.com/3DSMAX/16/ENU/MAXScript-Help/files/GUID-30A3165F-752E-423B-A662-46B4A78E89F5.htm</a></p>
<h2>
.NET API</h2>
<p>
For a managed plug-in using the .NET API, you would simply use the satellite assembly approach that is provided by the .NET Framework. This requires you to build a separate satellite assembly for each language and supports a hierarchical approach to language and region specific language. For details, see here:</p>
<p><a href="http://msdn.microsoft.com/en-us/library/f45fce5x(v=vs.100).aspx" target="_self">
http://msdn.microsoft.com/en-us/library/f45fce5x(v=vs.100).aspx</a></p>
<p>
To be consistent with other 3ds Max assemblies, it is recommended to add this attribute to your <em>.\Properties\AssemblyInfo.cs</em> file:</p>
<pre class="brush: csharp; toolbar: false;">[assembly: NeutralResourcesLanguage(&quot;en-US&quot;,
           UltimateResourceFallbackLocation.MainAssembly)]</pre>
<p>
Additionally, you can use this .NET framework technique to perform locale-specific conversions:</p>
<pre class="brush: csharp; toolbar: false;">public static readonly CultureInfo DefaultCulture =new CultureInfo (&quot;en-US&quot;) ;</pre>
<p>
To support the other standard languages, you would typically include the default en-US resources in the DLL assembly that has the code, and the additional target locales have a satellite assembly called <em>&lt;name_of_DLL&gt;.resources.dll</em></p>
<p> 
If you need to support localization in a mixed-mode plugin, where you have some native C++ mixed with managed C++, then there is one technique that can help identify the locale at runtime. For example:</p>
<pre class="brush: csharp; toolbar: false;"> 
CultureInfo^ CommCtrConnector::CurrentUICulture () {
    CultureInfo^ ret =gcnew CultureInfo (MaxSDK::Util::GetLocaleValue ()) ;
    return ret ;
}</pre>
<h2>
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d019102f841a3970c-pi" style="display: inline;"><img alt="Translate-and-localize" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d019102f841a3970c" src="/assets/image_fb8afd.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Translate-and-localize" /></a></h2>
<h2>Other Supporting Files</h2>
<p>
If you also have other supporting files, such has fixed menus or toolbars defined in the CUI system, then you need to localize those separately. Those should be placed in the appropriate language folder during your installation, and 3ds Max will use them when it is running in those languages.</p>
