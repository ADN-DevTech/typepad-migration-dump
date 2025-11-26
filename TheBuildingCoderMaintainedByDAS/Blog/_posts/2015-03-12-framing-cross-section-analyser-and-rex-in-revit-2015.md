---
layout: "post"
title: "Framing Cross Section Analyser and REX in Revit 2015"
date: "2015-03-12 06:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "ACA"
  - "Git"
  - "Migration"
  - "REX"
  - "RST"
original_url: "https://thebuildingcoder.typepad.com/blog/2015/03/framing-cross-section-analyser-and-rex-in-revit-2015.html "
typepad_basename: "framing-cross-section-analyser-and-rex-in-revit-2015"
typepad_status: "Publish"
---

<p>Back in December 2013, I discussed

<a href="http://thebuildingcoder.typepad.com/blog/2013/12/security-framing-cross-section-analyser-and-rex.html">
structural cross section analysis</a>,

i.e. determination of the cross section profile of beam, columns, braces, etc., and

<a href="http://thebuildingcoder.typepad.com/blog/2013/12/security-framing-cross-section-analyser-and-rex.html#3">
several completely different approaches</a> one

can take to achieve that.</p>

<p>I also demonstrated how to make use of the powerful functionality provided by the REX toolkit without building the entire add-in on top of the REX framework.</p>

<p>By the way, here are some other earlier discussions of REX:</p>

<!-- 570 601 691 -->
<ul>
<li><a href="http://thebuildingcoder.typepad.com/blog/2011/04/the-rex-sdk.html">The REX SDK</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2011/06/extensions-for-revit-2012.html">Extensions for Revit 2012</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2011/12/rex-content-generator.html">REX Content Generator</a></li>
</ul>

<p>Before getting to this Revit Structure stuff, let me also mention that I made one of my rather infrequent contributions to the

<a href="http://adndevblog.typepad.com/aec">
AEC DevBlog</a> today as well, to explain that about

<a href="http://adndevblog.typepad.com/aec/2015/03/using-autocad-mep-objects-in-accoreconsole.html">using AutoCAD MEP object enablers in AcCoreConsole</a>.</p>

<p>Anyway, back to the cross section analysis; that discussion led to the following question:</p>

<p><strong>Question:</strong>

I would like to create a Revit 2015 REX add-in without using the REX add-in wizard.</p>

<p>I looked at your

<a href="https://github.com/jeremytammik/FramingXsecAnalyzer">
FramingXsecAnalyzer add-in</a>

and

<a href="http://thebuildingcoder.typepad.com/blog/2013/12/security-framing-cross-section-analyser-and-rex.html">
the post</a> describing

it, but don't understand how to start and create a new such project for my own use.</p>

<p>Do you have any sample project I could start with?


<p><strong>Answer:</strong>

The starting point is a completely normal Revit add-in project.</p>

<p>You add the required additional references to that and ensure that the REX framework is found and loaded dynamically.</p>

<p>To migrate the application to a new version, you can start by looking at the 2012 and 2014 versions in the

<a href="http://thebuildingcoder.typepad.com/blog/2013/12/security-framing-cross-section-analyser-and-rex.html#10">
download section</a> of that post.</p>

<p>You can use the GitHub comparison tools to determine what changed between them, e.g.

<a href="https://github.com/jeremytammik/FramingXsecAnalyzer/compare/2012.0.0.0...2014.0.0.0">
FramingXsecAnalyzer/compare/2012.0.0.0...2014.0.0.0</a>.</p>

<p>I migrated the project to Revit 2015 for you.</p>

<p>One important step was obviously to increment the version number in the assembly resolver.</p>

<p>At the beginning of the external command Execute method, we register it like this:</p>

<pre class="code">
&nbsp; <span class="teal">AppDomain</span>.CurrentDomain.AssemblyResolve
&nbsp; &nbsp; += <span class="blue">new</span> <span class="teal">ResolveEventHandler</span>( OnAssemblyResolve );
</pre>

<p>The resolver specifies the assembly version to load:</p>

<pre class="code">
&nbsp; <span class="blue">static</span> System.Reflection.<span class="teal">Assembly</span> OnAssemblyResolve(
&nbsp; &nbsp; <span class="blue">object</span> sender,
&nbsp; &nbsp; <span class="teal">ResolveEventArgs</span> args )
&nbsp; {
&nbsp; &nbsp; <span class="teal">Assembly</span> a = <span class="teal">Assembly</span>.GetExecutingAssembly();
&nbsp;
&nbsp; &nbsp; <span class="blue">return</span> Autodesk.REX.Framework.<span class="teal">REXAssemblies</span>
&nbsp; &nbsp; &nbsp; .Resolve( sender, args, <span class="maroon">&quot;2015&quot;</span>, a );
&nbsp; }
</pre>

<p>The code actually making use of the REX functionality remains completely unchanged:</p>

<pre class="code">
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Use REX to analyse element cross section.</span>
&nbsp; <span class="gray">///</span><span class="green"> This requires a reference to </span>
&nbsp; <span class="gray">///</span><span class="green"> REX.ContentGeneratorLT.dll and prior</span>
&nbsp; <span class="gray">///</span><span class="green"> initialisation of the REX framework.</span>
&nbsp; <span class="gray">///</span><span class="green"> The converter initialisation must reside in</span>
&nbsp; <span class="gray">///</span><span class="green"> a different method than the subscription to</span>
&nbsp; <span class="gray">///</span><span class="green"> the assembly resolver OnAssemblyResolve.</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="blue">void</span> RexXsecAnalyis(
&nbsp; &nbsp; <span class="teal">ExternalCommandData</span> commandData,
&nbsp; &nbsp; <span class="teal">Element</span> e )
&nbsp; {
&nbsp; &nbsp; <span class="green">// Initialise converter</span>
&nbsp;
&nbsp; &nbsp; <span class="teal">RVTFamilyConverter</span> rvt = <span class="blue">new</span> <span class="teal">RVTFamilyConverter</span>(
&nbsp; &nbsp; &nbsp; commandData, <span class="blue">true</span> );
&nbsp;
&nbsp; &nbsp; <span class="green">// Retrieve family type</span>
&nbsp;
&nbsp; &nbsp; <span class="teal">REXFamilyType</span> fam = rvt.GetFamily( e,
&nbsp; &nbsp; &nbsp; <span class="teal">ECategoryType</span>.SECTION_PARAM );
&nbsp;
&nbsp; &nbsp; <span class="green">// Retrieve section data</span>
&nbsp;
&nbsp; &nbsp; <span class="teal">REXFamilyType_ParamSection</span> paramSection = fam
&nbsp; &nbsp; &nbsp; <span class="blue">as</span> <span class="teal">REXFamilyType_ParamSection</span>;
&nbsp;
&nbsp; &nbsp; <span class="teal">REXSectionParamDescription</span> parameters
&nbsp; &nbsp; &nbsp; = paramSection.Parameters;
&nbsp;
&nbsp; &nbsp; <span class="green">// Extract dimensions, section type, tapered</span>
&nbsp; &nbsp; <span class="green">// predicate, etc.</span>
&nbsp; &nbsp; <span class="green">// If different start and end sections are </span>
&nbsp; &nbsp; <span class="green">// required, use DimensionsEnd as well.</span>
&nbsp;
&nbsp; &nbsp; <span class="teal">REXSectionParamDimensions</span> dimensions = parameters
&nbsp; &nbsp; &nbsp; .Dimensions;
&nbsp;
&nbsp; &nbsp; <span class="teal">ESectionType</span> sectionType = parameters
&nbsp; &nbsp; &nbsp; .SectionType;
&nbsp;
&nbsp; &nbsp; <span class="blue">bool</span> tapered = parameters.Tapered;
&nbsp;
&nbsp; &nbsp; <span class="blue">bool</span> start = <span class="blue">true</span>;
&nbsp;
&nbsp; &nbsp; <span class="teal">Contour_Section</span> contour = parameters.GetContour(
&nbsp; &nbsp; &nbsp; start );
&nbsp;
&nbsp; &nbsp; <span class="teal">List</span>&lt;<span class="teal">ContourCont</span>&gt; shape = contour.Shape;
&nbsp;
&nbsp; &nbsp; <span class="teal">Util</span>.InfoMessage( <span class="blue">string</span>.Format(
&nbsp; &nbsp; &nbsp; <span class="maroon">&quot;The selected structural framing element &quot;</span>
&nbsp; &nbsp; &nbsp; + <span class="maroon">&quot;cross section REX section type is &quot;</span>
&nbsp; &nbsp; &nbsp; + <span class="maroon">&quot;{0}.&quot;</span>, sectionType ) );
&nbsp; }
</pre>

<p>As always, the current version is available from the

<a href="https://github.com/jeremytammik/FramingXsecAnalyzer">FramingXsecAnalyzer GitHub repository</a>,

and the version discussed here is

<a href="https://github.com/jeremytammik/FramingXsecAnalyzer/releases/tag/2015.0.0.0">release 2015.0.0.0</a>.</p>

<p>This kind of interactive parameter selection and filtering may obviously be very useful for many other applications as well.</p>

<p>I tested it on the following I column:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b8d0e9f6df970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b8d0e9f6df970c img-responsive" style="width: 136px; " alt="I cross section" title="I cross section" src="/assets/image_18aa62.jpg" /></a><br />

</center>

<p>It reports:</p>

<pre>
The selected structural framing element cross section
section view cut plane face has 1 loop and is thus 'open'.

The selected structural framing element cross section
REX section type is I.
</pre>

<p>The REX modules are demand loaded between these two log messages.</p>

<p>Looking in the Visual Studio debug output window, you can see the following activity going on in the background:</p>

<pre>
'Revit.exe' (Managed (v4.0.30319)): Loaded 'C:\Windows\Microsoft.Net\assembly\GAC_MSIL\Autodesk.REX.Framework\v4.0_2014.0.0.0__51e16e3b26b42eda\Autodesk.REX.Framework.dll'
'Revit.exe' (Managed (v4.0.30319)): Loaded 'C:\Windows\assembly\GAC_MSIL\Autodesk.Common.AResourcesControl\1.0.0.0__ff3304d4f320ee59\Autodesk.Common.AResourcesControl.dll'
'Revit.exe' (Managed (v4.0.30319)): Loaded 'C:\Program Files\Common Files\Autodesk Shared\Extensions 2015\Framework\Components\AREXContentGenerator\REX.ContentGeneratorLT.dll'
'Revit.exe' (Managed (v4.0.30319)): Loaded 'C:\Windows\Microsoft.Net\assembly\GAC_MSIL\Autodesk.REX.Framework\v4.0_2015.0.0.0__51e16e3b26b42eda\Autodesk.REX.Framework.dll'
'Revit.exe' (Managed (v4.0.30319)): Loaded 'C:\Program Files\Common Files\Autodesk Shared\Extensions 2015\Framework\Foundation\REX.Foundation.dll'
'Revit.exe' (Managed (v4.0.30319)): Loaded 'C:\Program Files\Common Files\Autodesk Shared\Extensions 2015\Framework\Foundation\REX.Foundation.Forms.dll'
'Revit.exe' (Managed (v4.0.30319)): Loaded 'C:\Program Files\Common Files\Autodesk Shared\Extensions 2015\Products\Revit\AREXRevitMngr.dll'
'Revit.exe' (Managed (v4.0.30319)): Loaded 'C:\Program Files\Common Files\Autodesk Shared\Extensions 2015\Framework\Foundation\REX.Geometry.dll'
'Revit.exe' (Managed (v4.0.30319)): Loaded 'C:\Program Files\Common Files\Autodesk Shared\Extensions 2015\Framework\Engine\REX.Engine.dll'
'Revit.exe' (Managed (v4.0.30319)): Loaded 'C:\Program Files\Common Files\Autodesk Shared\Extensions 2015\Framework\Engine\REX.Preferences.dll'
'Revit.exe' (Managed (v4.0.30319)): Loaded 'C:\Program Files\Common Files\Autodesk Shared\Extensions 2015\Framework\Engine\REX.System.dll'
'Revit.exe' (Managed (v4.0.30319)): Loaded 'C:\Program Files\Common Files\Autodesk Shared\Extensions 2015\Framework\Engine\REX.UI.WPF.dll'
'Revit.exe' (Managed (v4.0.30319)): Loaded 'C:\Program Files\Common Files\Autodesk Shared\Extensions 2015\Framework\Engine\REX.UI.dll'
'Revit.exe' (Managed (v4.0.30319)): Loaded 'C:\Program Files\Common Files\Autodesk Shared\Extensions 2015\Framework\Engine\REX.UI.Forms.dll'
'Revit.exe' (Managed (v4.0.30319)): Loaded 'C:\Program Files\Common Files\Autodesk Shared\Extensions 2015\Framework\Engine\en-US\REX.UI.resources.dll'
'Revit.exe' (Managed (v4.0.30319)): Loaded 'C:\Program Files\Common Files\Autodesk Shared\Extensions 2015\Framework\Engine\en-US\REX.System.resources.dll'
'Revit.exe' (Managed (v4.0.30319)): Loaded 'C:\Program Files\Common Files\Autodesk Shared\Extensions 2015\Framework\Engine\REX.Mathematics.dll'
'Revit.exe' (Managed (v4.0.30319)): Loaded 'C:\Program Files\Common Files\Autodesk Shared\Extensions 2015\Framework\Foundation\REX.Geometry.Structure.dll'
'Revit.exe' (Managed (v4.0.30319)): Loaded 'C:\Program Files\Common Files\Autodesk Shared\Extensions 2015\Framework\Foundation\REX.Geometry.Revit.dll'
</pre>

<p>Copy to a text editor or view the source to see the truncated lines in full.</p>

<p>Here is a list of the modules that were loaded:</p>

<ul>
<li>C:\Windows\Microsoft.Net\assembly\GAC_MSIL\Autodesk.REX.Framework\v4.0_2014.0.0.0__51e16e3b26b42eda\Autodesk.REX.Framework.dll</li>
<li>C:\Windows\assembly\GAC_MSIL\Autodesk.Common.AResourcesControl\1.0.0.0__ff3304d4f320ee59\Autodesk.Common.AResourcesControl.dll</li>
<li>C:\Program Files\Common Files\Autodesk Shared\Extensions 2015\Framework\Components\AREXContentGenerator\REX.ContentGeneratorLT.dll</li>
<li>C:\Windows\Microsoft.Net\assembly\GAC_MSIL\Autodesk.REX.Framework\v4.0_2015.0.0.0__51e16e3b26b42eda\Autodesk.REX.Framework.dll</li>
<li>C:\Program Files\Common Files\Autodesk Shared\Extensions 2015\Framework\Foundation\REX.Foundation.dll</li>
<li>C:\Program Files\Common Files\Autodesk Shared\Extensions 2015\Framework\Foundation\REX.Foundation.Forms.dll</li>
<li>C:\Program Files\Common Files\Autodesk Shared\Extensions 2015\Products\Revit\AREXRevitMngr.dll</li>
<li>C:\Program Files\Common Files\Autodesk Shared\Extensions 2015\Framework\Foundation\REX.Geometry.dll</li>
<li>C:\Program Files\Common Files\Autodesk Shared\Extensions 2015\Framework\Engine\REX.Engine.dll</li>
<li>C:\Program Files\Common Files\Autodesk Shared\Extensions 2015\Framework\Engine\REX.Preferences.dll</li>
<li>C:\Program Files\Common Files\Autodesk Shared\Extensions 2015\Framework\Engine\REX.System.dll</li>
<li>C:\Program Files\Common Files\Autodesk Shared\Extensions 2015\Framework\Engine\REX.UI.WPF.dll</li>
<li>C:\Program Files\Common Files\Autodesk Shared\Extensions 2015\Framework\Engine\REX.UI.dll</li>
<li>C:\Program Files\Common Files\Autodesk Shared\Extensions 2015\Framework\Engine\REX.UI.Forms.dll</li>
<li>C:\Program Files\Common Files\Autodesk Shared\Extensions 2015\Framework\Engine\en-US\REX.UI.resources.dll</li>
<li>C:\Program Files\Common Files\Autodesk Shared\Extensions 2015\Framework\Engine\en-US\REX.System.resources.dll</li>
<li>C:\Program Files\Common Files\Autodesk Shared\Extensions 2015\Framework\Engine\REX.Mathematics.dll</li>
<li>C:\Program Files\Common Files\Autodesk Shared\Extensions 2015\Framework\Foundation\REX.Geometry.Structure.dll</li>
<li>C:\Program Files\Common Files\Autodesk Shared\Extensions 2015\Framework\Foundation\REX.Geometry.Revit.dll</li>
</ul>

<p>In a second test, I presented it with a tubular column:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b8d0e9f6d4970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b8d0e9f6d4970c img-responsive" style="width: 217px; " alt="Tube cross section" title="Tube cross section" src="/assets/image_0291dd.jpg" /></a><br />

</center>

<p>In this case, it reports:</p>

<pre>
The selected structural framing element cross section
section view cut plane face has 2 loops and is thus 'closed'.

The selected structural framing element cross section
REX section type is TUBE.
</pre>

<p>I hope this helps.</p>

<p>Have fun!</p>
