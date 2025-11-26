---
layout: "post"
title: "Standalone BasicFileInfo and ExtractPartAtom"
date: "2018-04-05 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "Algorithm"
  - "Data Access"
  - "External"
  - "Family"
  - "Utilities"
original_url: "https://thebuildingcoder.typepad.com/blog/2018/04/standalone-basicfileinfo-and-extractpartatom-method.html "
typepad_basename: "standalone-basicfileinfo-and-extractpartatom-method"
typepad_status: "Publish"
---

<p>I had an interesting discussion with
Håvard Dagsvik of <a href="https://www.symetri.com">Symetri</a> on
the use of <code>TransmissionData</code> and standalone access to the <code>BasicFileInfo</code>, without the need for a valid Revit API context, in the course of which Håvard shared his Revit-independent <code>GetFamilyXMLData</code> method implementation, replacing
the Revit API <a href="http://www.revitapidocs.com/2018.1/d477cf8f-0dfe-4055-a787-315c84ef5530.htm"><code>Family</code> <code>ExtractPartAtom</code> method</a>:</p>

<ul>
<li><a href="#2">No document required for <code>TransmissionData</code> access</a> </li>
<li><a href="#3"><code>TransmissionData</code> requires a valid Revit API context</a> </li>
<li><a href="#4">Standalone <code>GetFamilyXmlData</code> method replacing <code>ExtractPartAtom</code></a> </li>
<li><a href="#5">Windows explorer <code>BasicFileInfo</code> right click utility</a> </li>
</ul>

<h4><a name="2"></a>No Document Required for TransmissionData Access</h4>

<p><strong>Question:</strong> I fear I know the answer to this one already, but I will try to ask you anyway &nbsp; :-)</p>

<p>When reading <code>TransmissionData</code> or <code>BasicFileInfo</code>, you don't need a document:</p>

<p><center></p>

<p><a class="asset-img-link"  href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c95e2a71970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c95e2a71970b img-responsive" style="width: 292px; display: block; margin-left: auto; margin-right: auto;" alt="Link Manager" title="Link Manager" src="/assets/image_880451.jpg" /></a><br /></p>

<p></center></p>

<p>From the <code>TransmissionData</code>, I can access the <code>ExternalFileReference</code> via <code>GetLastSavedReferenceData</code>, etc.</p>

<p>This works fine:</p>

<p><center></p>

<p><a class="asset-img-link"  href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c95e2a76970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c95e2a76970b img-responsive" alt="ExternalFileReference" title="ExternalFileReference" src="/assets/image_d614b0.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>But, if I implement the same functionality in a standalone executable and use my own TransmissionData function...</p>

<p><center></p>

<p><a class="asset-img-link"  href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c95e2a82970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c95e2a82970b img-responsive" alt="TransmissionData" title="TransmissionData" src="/assets/image_6b9d5a.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>It throws this exception on the first method that uses the Revit API:</p>

<p><center></p>

<p><a class="asset-img-link"  href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301bb0a015360970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301bb0a015360970d img-responsive" style="width: 361px; display: block; margin-left: auto; margin-right: auto;" alt="Revit API required" title="Revit API required" src="/assets/image_780bec.jpg" /></a><br /></p>

<p></center></p>

<p>No Revit process involved here, of course.</p>

<p>It's most probably as you have written before:</p>

<blockquote>
  <p>In general, as noted in both there and in the summary above, it is not possible to access Revit data or make any use whatsoever of the Revit API from outside of Revit.</p>
  
  <p>Moreover, you need to be in a valid Revit API context to make any Revit API calls.</p>
</blockquote>

<p>Maybe these methods still need the Revit <code>Application</code> or <code>UIApplication</code> in some way &ndash; or they are just locked for some other reason.</p>

<p>Or, should this work and it is just as the message says:</p>

<blockquote>
  <p>... one of the dependencies to RevitAPI.dll is not found?</p>
</blockquote>

<p>I'm 99% sure it finds RevitAPI.dll.</p>

<p>After all, this seems like metadata that logically could be available without Revit.</p>

<h4><a name="3"></a>TransmissionData Requires a Valid Revit API Context</h4>

<p><strong>Answer:</strong> Yes indeed, you do know the answer: use of the Revit API <code>TransmissionData</code> object requires a valid Revit API context.</p>

<p>In other words, you can read data from an unopened Revit document, but you can only do so from within a valid Revit session.</p>

<p>However, there are many ways of getting at the basic file info without use of the Revit API, e.g.,
<a href="http://thebuildingcoder.typepad.com/blog/2017/06/determining-rvt-file-version-using-python.html">determining RVT file version using Python</a>, or
one of the other approaches discussed there.</p>

<h4><a name="4"></a>Standalone GetFamilyXmlData Method Replacing ExtractPartAtom</h4>

<p><strong>Response:</strong> That's great; I will definitely try to make use of Victor's raw data approach on <code>BasicFileInfo</code>.</p>

<p>Is it thinkable that <code>TransmissionData</code> and <code>BasicFileInfo</code> doesn't really need Revit?</p>

<p>That they just happen to be packaged within a larger library that overall can`t be used without Revit?</p>

<p>Maybe it is a just licensing policy decision, even though users always have a license.</p>

<p>Meaning, if those methods where refactored out, it could have worked, technically?</p>

<p>If so, let me be the first one who makes the request to get them refactored &nbsp; :-)</p>

<p>For example, take the <code>Family.ExtractPartAtom</code> method.</p>

<p>It exports an XML file with all family types, including parameters and values for each type.</p>

<p>But we don't use it.</p>

<p>First, because I (luckily) didn't realize that the Revit API provides this method.</p>

<p>Secondly, because my own custom method is much faster than <code>ExtractPartAtom</code>.</p>

<p>More importantly, it can be used without Revit, and it doesn't save to an external XML file that we don't need anyway.</p>

<p>It just returns the raw XML string that is parsed to JSON before it enters a database.</p>

<p>Method here:</p>

<pre class="code">
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;Faster&nbsp;ExtractPartAtom&nbsp;reimplementation,</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;independent&nbsp;of&nbsp;Revit&nbsp;API,&nbsp;for&nbsp;standalone&nbsp;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;external&nbsp;use.&nbsp;By&nbsp;Håvard&nbsp;Dagsvik,&nbsp;Symetri.</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">param</span><span style="color:gray;">&nbsp;name</span><span style="color:gray;">=</span><span style="color:gray;">&quot;</span>family_file_path<span style="color:gray;">&quot;</span><span style="color:gray;">&gt;</span><span style="color:green;">Family&nbsp;file&nbsp;path</span><span style="color:gray;">&lt;/</span><span style="color:gray;">param</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">returns</span><span style="color:gray;">&gt;</span><span style="color:green;">XML&nbsp;data</span><span style="color:gray;">&lt;/</span><span style="color:gray;">returns</span><span style="color:gray;">&gt;</span>
<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">string</span>&nbsp;GetFamilyXmlData(
&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;family_file_path&nbsp;)
{
&nbsp;&nbsp;<span style="color:blue;">byte</span>[]&nbsp;array&nbsp;=&nbsp;<span style="color:#2b91af;">File</span>.ReadAllBytes(&nbsp;family_file_path&nbsp;);

&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;string_file&nbsp;=&nbsp;<span style="color:#2b91af;">Encoding</span>.UTF8.GetString(&nbsp;array&nbsp;);

&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;xml_data&nbsp;=&nbsp;<span style="color:blue;">null</span>;

&nbsp;&nbsp;<span style="color:blue;">int</span>&nbsp;start&nbsp;=&nbsp;string_file.IndexOf(&nbsp;<span style="color:#a31515;">&quot;&lt;entry&quot;</span>&nbsp;);

&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;start&nbsp;==&nbsp;-1&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Debug</span>.Print(&nbsp;<span style="color:#a31515;">&quot;XML&nbsp;start&nbsp;not&nbsp;detected:&nbsp;&quot;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;family_file_path&nbsp;);
&nbsp;&nbsp;}
&nbsp;&nbsp;<span style="color:blue;">else</span>
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">int</span>&nbsp;end&nbsp;=&nbsp;string_file.IndexOf(&nbsp;<span style="color:#a31515;">&quot;/entry&gt;&quot;</span>&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;end&nbsp;==&nbsp;-1&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Debug</span>.Print(&nbsp;<span style="color:#a31515;">&quot;XML&nbsp;end&nbsp;not&nbsp;detected:&nbsp;&quot;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;family_file_path&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">else</span>
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;end&nbsp;=&nbsp;end&nbsp;+&nbsp;7;

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">int</span>&nbsp;length&nbsp;=&nbsp;end&nbsp;-&nbsp;start;

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;length&nbsp;&lt;=&nbsp;0&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Debug</span>.Print(&nbsp;<span style="color:#a31515;">&quot;XML&nbsp;length&nbsp;is&nbsp;0&nbsp;or&nbsp;less:&nbsp;&quot;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;family_file_path&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">else</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;xml_data&nbsp;=&nbsp;string_file.Substring(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;start,&nbsp;length&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;xml_data;
}
</pre>

<p>I added Håvard's method to the existing external
command <a href="https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/CmdPartAtom.cs">CmdPartAtom</a>
in <a href="https://github.com/jeremytammik/the_building_coder_samples">The Building Coder samples</a> to try this out, in
<a href="httpshttps://github.com/jeremytammik/the_building_coder_samples/releases/tag/2018.0.138.4">release 2018.0.138.4</a>, and cleaned up the existing code calling the built-in Revit API method <code>ExtractPartAtomFromFamilyFile</code> at the same time.</p>

<h4><a name="5"></a>Windows Explorer BasicFileInfo Right Click Utility</h4>

<p>Håvard also used Victor's code as a base to implement his
own <a href="https://www.screencast.com/t/o78ugUUwY">Windows Explorer <code>BasicFileInfo</code> right click utility</a>, cf.
the <a href="https://youtu.be/WJCsNywPMVU">20-second recording of his RVT BasicFileInfo in Windows Explorer</a>:</p>

<p><center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/WJCsNywPMVU?rel=0" frameborder="0" allow="autoplay; encrypted-media" allowfullscreen></iframe>
</center></p>

<p>Works nicely &nbsp; :-)</p>

<p>Not on all Revit files, though.</p>

<p>Workshared files seem to not work, and a few others.</p>

<p>The data is probably there, just have to tweak it a bit more.</p>

<p>Many thanks to Håvard for raising this question and sharing his standalone implementation!</p>
