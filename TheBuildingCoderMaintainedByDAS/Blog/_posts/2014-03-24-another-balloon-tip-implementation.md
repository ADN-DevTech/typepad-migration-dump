---
layout: "post"
title: "Another Balloon Tip Implementation"
date: "2014-03-24 06:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "User Interface"
  - "Utilities"
original_url: "https://thebuildingcoder.typepad.com/blog/2014/03/another-balloon-tip-implementation.html "
typepad_basename: "another-balloon-tip-implementation"
typepad_status: "Publish"
---

<p>Alexander Ignatovich, or Игнатович Александр, responds to the recent discussion on

<a href="http://thebuildingcoder.typepad.com/blog/2014/03/using-balloon-tips-in-revit.html">
using balloon tips in Revit</a> and says:

<blockquote>
<p>I want to share another solution for balloon tips for custom messages, without using the unsupported AdWindows library.</p>

<p>Just see the project attached in

<span class="asset  asset-generic at-xid-6a00e553e16897883301a5118cf925970c img-responsive"><a href="http://thebuildingcoder.typepad.com/files/yetanotherballoontip.zip">YetAnotherBalloonTip.zip</a></span>

:-)</p>
</blockquote>

<p>Alexander's solution provides three different sample commands:</p>

<ul>
<li>Simple balloon</li>
<li>Warning balloon</li>
<li>Balloon from another thread</li>
</ul>

<p>The implementation is packaged in a separate self-contained class named NotifyBox, so instantiating a simple balloon tip is really very simple indeed, in one single constructor call:</p>

<pre class="code">
<span class="blue">using</span> Autodesk.Revit.Attributes;
<span class="blue">using</span> Autodesk.Revit.DB;
<span class="blue">using</span> Autodesk.Revit.UI;
<span class="blue">using</span> IVC.NotifyBox.Controls;
<span class="blue">using</span> IVC.NotifyBox.ViewModel.Enums;
&nbsp;
<span class="blue">namespace</span> YetAnotherBaloons
{
&nbsp; [<span class="teal">Transaction</span>( <span class="teal">TransactionMode</span>.Manual )]
&nbsp; <span class="blue">public</span> <span class="blue">class</span> <span class="teal">StartSimpleBaloonCommand</span> : <span class="teal">IExternalCommand</span>
&nbsp; {
&nbsp; &nbsp; <span class="blue">public</span> <span class="teal">Result</span> Execute(
&nbsp; &nbsp; &nbsp; <span class="teal">ExternalCommandData</span> commandData,
&nbsp; &nbsp; &nbsp; <span class="blue">ref</span> <span class="blue">string</span> message,
&nbsp; &nbsp; &nbsp; <span class="teal">ElementSet</span> elements )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="teal">NotifyBox</span>.Show( <span class="maroon">&quot;Hello&quot;</span>, <span class="maroon">&quot;Hello from &quot;</span>
&nbsp; &nbsp; &nbsp; &nbsp; + <span class="maroon">&quot;Investicionnaya Venchurnaya Companiya ;-)&quot;</span>,
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">NotificationDuration</span>.Short );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">return</span> <span class="teal">Result</span>.Succeeded;
&nbsp; &nbsp; }
&nbsp; }
}
</pre>

<p>An argument enables you to specify the duration.</p>

<p>The resulting balloon tip looks like this, and fades away after a moment:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301a3fcdd50ca970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301a3fcdd50ca970b img-responsive" style="width: 407px; " alt="Simple balloon tip" title="Simple balloon tip" src="/assets/image_05c1db.jpg" /></a><br />

</center>

<p>Another argument allows you to specify an icon, e.g. to implement a warning balloon tip:</p>

<pre class="code">
&nbsp; <span class="blue">public</span> <span class="blue">class</span> <span class="teal">StartWarningBaloonCommand</span> : <span class="teal">IExternalCommand</span>
&nbsp; {
&nbsp; &nbsp; <span class="blue">public</span> <span class="teal">Result</span> Execute(
&nbsp; &nbsp; &nbsp; <span class="teal">ExternalCommandData</span> commandData,
&nbsp; &nbsp; &nbsp; <span class="blue">ref</span> <span class="blue">string</span> message,
&nbsp; &nbsp; &nbsp; <span class="teal">ElementSet</span> elements )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="teal">NotifyBox</span>.Show( <span class="maroon">&quot;Warning&quot;</span>,
&nbsp; &nbsp; &nbsp; &nbsp; <span class="maroon">&quot;Warning! Something is not perfect :)&quot;</span>,
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">NotificationIcon</span>.Warning,
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">NotificationDuration</span>.Medium );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">return</span> <span class="teal">Result</span>.Succeeded;
&nbsp; &nbsp; }
&nbsp; }
</pre>

<p>The resulting balloon tip includes an icon:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301a3fcdd50d4970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301a3fcdd50d4970b img-responsive" style="width: 403px; " alt="Balloon tip with a warning icon" title="Balloon tip with a warning icon" src="/assets/image_f8fddc.jpg" /></a><br />

</center>

<p>Since the balloon tip class is completely independent of Revit, it can obviously be called from a different thread as well:</p>

<pre class="code">
&nbsp; <span class="blue">public</span> <span class="blue">class</span> <span class="teal">StartBaloonFromAnotherThreadCommand</span> : <span class="teal">IExternalCommand</span>
&nbsp; {
&nbsp; &nbsp; <span class="blue">public</span> <span class="teal">Result</span> Execute(
&nbsp; &nbsp; &nbsp; <span class="teal">ExternalCommandData</span> commandData,
&nbsp; &nbsp; &nbsp; <span class="blue">ref</span> <span class="blue">string</span> message,
&nbsp; &nbsp; &nbsp; <span class="teal">ElementSet</span> elements )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; System.Threading.Tasks.<span class="teal">Task</span>.Factory.StartNew( () =&gt;
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Thread</span>.Sleep( <span class="teal">TimeSpan</span>.FromSeconds( 3 ) );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">NotifyBox</span>.Show( <span class="maroon">&quot;Warning&quot;</span>,
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="maroon">&quot;This message is from another thread!&quot;</span>,
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">NotificationIcon</span>.Warning,
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">NotificationDuration</span>.Medium );
&nbsp; &nbsp; &nbsp; } );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">return</span> <span class="teal">Result</span>.Succeeded;
&nbsp; &nbsp; }
&nbsp; }
</pre>

<p>Many thanks to Alexander for sharing this!</p>
