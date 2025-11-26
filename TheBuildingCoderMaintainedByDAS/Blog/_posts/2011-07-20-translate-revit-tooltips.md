---
layout: "post"
title: "Translate Revit Tooltips"
date: "2011-07-20 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "Automation"
  - "Events"
  - "External"
  - "Plugin"
  - "Ribbon"
  - "User Interface"
  - "Utilities"
original_url: "https://thebuildingcoder.typepad.com/blog/2011/07/translate-revit-tooltips.html "
typepad_basename: "translate-revit-tooltips"
typepad_status: "Publish"
---

<p>Kean Walmsley recently started exploring the exciting and useful idea of hooking up machine translation to AutoCAD, initially to 

<a href="http://through-the-interface.typepad.com/through_the_interface/2011/07/automatic-translation-of-autocad-tooltips-using-net.html">
auto-translate tooltips</a>. 

As Kean points out, you can get all AutoCAD tooltips translated into one of 35 different languages, which is a great help for people learning or using the product in regions for which we don't currently localize. 
Here's a tooltip in Arabic, for instance:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833014e89f79e48970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833014e89f79e48970d" alt="Arabic AutoCAD tooltip" title="Arabic AutoCAD tooltip" src="/assets/image_34ee2a.jpg" border="0" /></a> <br />

</center>

<p>Kean quickly followed up the original proof of concept with an 

<a href="http://through-the-interface.typepad.com/through_the_interface/2011/07/caching-translations-of-autocad-tooltips-using-net.html">
improved implementation</a> including caching and other features.

<p>Thanks to the fact that many Autodesk products are now sharing a number of basic platform tools, it is possible to use the same tooltip translation mechanism for Revit.

<p>This tool mainly uses AdWindows.dll, the .NET assembly providing the Autodesk.Windows namespace, which provides part of the ribbon user interface implementation.

<p>As Rudolf Honke already pointed out in a 

<a href="http://thebuildingcoder.typepad.com/blog/automation">
number of posts on UI Automation</a>, the .NET framework provides a lot of powerful functionality for manipulating and hooking into the ribbon.

<p>To catch the tooltips being displayed, we can subscribe to the Autodesk.Windows.ComponentManager.ToolTipOpened event, which is fired for all tooltips.

<p>Here is a snapshot of an initial prototype of the tooltip translation, running in an English version of Revit and causing it to display its tooltips in Italian:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833015433d790d6970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833015433d790d6970c image-full" alt="Italian Revit tooltip" title="Italian Revit tooltip" src="/assets/image_3eb7fc.jpg" border="0" /></a> <br />

</center>

<p>The current prototype implementation makes use of a very simplistic front end in which you explicitly specify the source and target languages:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833015433d79231970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833015433d79231970c" alt="Source and target language form" title="Source and target language form" src="/assets/image_4fc7ce.jpg" border="0" /></a> <br />

</center>

<p>Obviously, in a more foolproof and user friendly version, the source language would be determined automatically from the Revit application, and the target version would at least default to the OS language.

<p>Currently, this prototype for Revit is working quite well.
The source has only small differences to the AutoCAD implementation.
Revit's way to work with tooltips differs slightly from AutoCAD, in that most of the ribbon items need to be resolved on the first run.

<p>We are planning to clean this up some more, which should be pretty straightforward. 
Having a separate source file included in both projects is probably enough, and there is no need for a separately compiled component. 

<p>Reporting errors is currently handled in AutoCAD by posting to the command line, and in Revit by displaying a task dialogue. 
That will need to be resolved, presumably by defining a 'ProcessWebException' or 'display error' call-back, or something. 

<p>Obviously, this tool has little to do with the Revit API, actually, and the functionality may possibly even be achievable from a stand-alone external executable.
It does have to load and access the functionality in the AdWindows.dll .NET assembly, though.
The easiest (and maybe only?) way to do that is to implement it as an add-in.

<p>You can translate back into English by setting the target language to English.
The AutoCAD version implements a 'None' option which also does this.

<p>The source and target languages are managed by storing the language code in global class variables.
We have not implemented indexing into the arrays of language names and codes, because these indices would become invalid in offline mode with just the pre-existing or deployed translations on disk to work from.

<p>So, without further ado, let's take a look at the source code for this utility in its current untested form.
First of all, here is the very simple external command implementation:

<pre class="code">
<span class="blue">namespace</span> TransTipsRvt
{
&nbsp; [<span class="teal">Transaction</span>( <span class="teal">TransactionMode</span>.ReadOnly )]
&nbsp; <span class="blue">public</span> <span class="blue">class</span> <span class="teal">Command</span> : <span class="teal">IExternalCommand</span>
&nbsp; {
&nbsp; &nbsp; <span class="blue">public</span> <span class="teal">Result</span> Execute(
&nbsp; &nbsp; &nbsp; <span class="teal">ExternalCommandData</span> commandData,
&nbsp; &nbsp; &nbsp; <span class="blue">ref</span> <span class="blue">string</span> message,
&nbsp; &nbsp; &nbsp; <span class="teal">ElementSet</span> elements )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="teal">TransTips</span>.Run();
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">return</span> <span class="teal">Result</span>.Succeeded;
&nbsp; &nbsp; }
&nbsp; }
}
</pre>

<p>All the add-in does is run the TransTips class static method Run.
Since it does not touch the Revit database at all, it can use a read-only transaction mode.

<p>The TransTips Run method queries the web service for the available languages, displays the form prompting for the source and target language, and then hijacks the tooltips for translation. 
The latter is identical to Kean's implementation:

<pre class="code">
<span class="blue">public</span> <span class="blue">static</span> <span class="blue">void</span> Run()
{
&nbsp; <span class="teal">List</span>&lt;<span class="blue">string</span>&gt; codes = GetLanguageCodes();
&nbsp;
&nbsp; <span class="teal">List</span>&lt;<span class="blue">string</span>&gt; names =
&nbsp; &nbsp; <span class="blue">new</span> <span class="teal">List</span>&lt;<span class="blue">string</span>&gt;( GetLanguageNames( codes ) );
&nbsp;
&nbsp; <span class="green">// Get the name corresponding to a language code</span>
&nbsp;
&nbsp; <span class="blue">string</span> srcName =
&nbsp; &nbsp; names[codes.FindIndex( x =&gt; x.Equals( _srcLang ) )];
&nbsp;
&nbsp; <span class="blue">string</span> trgName =
&nbsp; &nbsp; names[codes.FindIndex( x =&gt; x.Equals( _trgLang ) )];
&nbsp;
&nbsp; <span class="teal">TransTipsForm</span> dlg =
&nbsp; &nbsp; <span class="blue">new</span> <span class="teal">TransTipsForm</span>( names, srcName, trgName );
&nbsp;
&nbsp; <span class="blue">if</span>( <span class="teal">DialogResult</span>.OK == dlg.ShowDialog() )
&nbsp; {
&nbsp; &nbsp; _srcLang = codes[ names.FindIndex(
&nbsp; &nbsp; &nbsp; x =&gt; x.Equals( dlg.SourceLanguage ) ) ];
&nbsp;
&nbsp; &nbsp; _trgLang = codes[ names.FindIndex(
&nbsp; &nbsp; &nbsp; x =&gt; x.Equals( dlg.TargetLanguage ) ) ];
&nbsp; }
&nbsp; HijackTooltips();
}
</pre>

<p>Here is the 

<span class="asset  asset-generic at-xid-6a00e553e168978833014e89fc58e9970d"><a href="http://thebuildingcoder.typepad.com/files/transtipsrvt02-2.zip">complete Visual Studio solution</a></span>,

as well as a 

<span class="asset  asset-generic at-xid-6a00e553e168978833015433d79500970c"><a href="http://thebuildingcoder.typepad.com/files/transtipsrvt.dll">compiled DLL</a></span> and 

<span class="asset  asset-generic at-xid-6a00e553e168978833015433d7959c970c"><a href="http://thebuildingcoder.typepad.com/files/transtipsrvt.addin">add-in manifest file</a></span>,

in case you wish to test this without compiling it yourself.

<p>Here is a last snapshot from my final test run before posting, displaying the 'Modify' tooltip of an English Revit in Korean:

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833015390043d88970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833015390043d88970b image-full" alt="Korean Revit tooltips" title="Korean Revit tooltips" src="/assets/image_48c8a5.jpg" border="0" /></a> <br />

</center>

<p><strong>Addendum:</strong> Please also look at Kean Walmsley's updated single solution which will build 

<a href="http://through-the-interface.typepad.com/through_the_interface/2011/07/translating-tooltips-in-both-autocad-and-revit-using-net.html">
TransTips for both AutoCAD and Revit</a> (including 

built versions of the DLLs), 

<a href="http://through-the-interface.typepad.com/through_the_interface/2011/07/translating-tooltips-inside-autocad-inventor-and-revit-using-net.html">
TransTips for AutoCAD, Inventor and Revit</a>, and later for

<a href="http://through-the-interface.typepad.com/through_the_interface/2011/08/translating-tooltips-in-autocad-inventor-revit-and-3ds-max-using-net.html">
AutoCAD, Inventor, Revit and 3ds Max</a>.
