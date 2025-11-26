---
layout: "post"
title: "Source Code Formatting and Google Prettifier"
date: "2013-05-11 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "External"
  - "HTML"
  - "JavaScript"
  - "JSON"
  - "Utilities"
original_url: "https://thebuildingcoder.typepad.com/blog/2013/05/source-code-formatting-and-google-prettyfier.html "
typepad_basename: "source-code-formatting-and-google-prettyfier"
typepad_status: "Publish"
---

<script src="https://thebuildingcoder.typepad.com/google-code-prettify/run_prettify.js"></script>

<p>As you know, I format my source code to pretty short lines in order to avoid having them truncated by the narrow blog post view column.

<p>I also like to present the code colour coded, as it appears in Visual Studio and many other programmer editors, to make it more readable.</p>

<p>For .NET code, I use

<a href="http://thebuildingcoder.typepad.com/blog/2011/04/updated-sdk-2012-products-and-source-code-colourisation.html#4">
CopySourceAsHtml</a> inside of Visual Studio for that.</p>

<p>I tried using other tools outside of Visual Studio instead in the past, including building my own, but they have one big disadvantage: unless they read and analyse all the referenced .NET assemblies to determine the classes they define, they cannot always tell whether a given word represents a variable or a class.
Classes are highlighted in a different colour in Visual Studio, and I find that pretty helpful.</p>

<p>For other languages, though, it would be really nice to be able to colourise the source code independently.</p>

<p>My colleague Cyrille Fauvel now pointed to two online colourising tools that he has used: the

<a href="http://alexgorbatchev.com/SyntaxHighlighter">syntax highlighter</a> and

<a href="https://code.google.com/p/google-code-prettify">Google prettify</a>.

Both of them are not really useful for C#, for the reasons explained above, but do a really good job on other languages.</p>

<p>I tested the Google prettifier on some JavaScript, HTML and JSON code in my recent post on

<a href="http://thebuildingcoder.typepad.com/blog/2013/05/my-cloud-based-2d-editor-implementation-status.html#5">
my cloud-based editor home page implementation</a> and

am very happy with the results.

<p>For instance, here is a screen snapshot of the main JavaScript snippet before integrating the prettifier:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301901c0cbed7970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301901c0cbed7970b" alt="Before Google Prettify" title="Before Google Prettify" src="/assets/image_41e414.jpg" border="0" /></a><br />

</center>

<p>Afterwards, it looks like this instead:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301901c0cc04d970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301901c0cc04d970b" alt="After Google Prettify" title="After Google Prettify" src="/assets/image_23a252.jpg" border="0" /></a><br />

</center>

<p>I only have to do two things to achieve that.</p>

<p>1. Add a reference to the Google Prettify loader:</p>

<pre class="prettyprint">
&lt;script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js"&gt;
&lt;/script&gt;
</pre>

<p>2. Add the 'prettyprint' class to my HTML 'pre' tags:</p>

<pre class="prettyprint">
&lt;pre class="prettyprint"&gt;
</pre>

<p>That's not much  :-)</p>

<p>It comes in really handy right now, since I will be publishing more JavaScript, HTML and JSON for the final stages of my

<a href="http://thebuildingcoder.typepad.com/blog/2013/05/my-cloud-based-2d-editor-implementation-status.html#2">
cloud-based 2D room editor</a> in

the next few days.</p>

<p>Many thanks to Cyrille for pointing this out!</p>


<a name="2"></a>

<h4>Think Global, Act Local, Control Freak</h4>

<p>After mulling over the above during the night, I decide to take control myself rather than go off and ask Google for help to render every page I post (and pass them every snippet of code to render, by the way).</p>

<p>So I downloaded the minimised version of the Google prettifier and now serve it up locally from The Building Coder typepad page itself.</p>

<p>In other words, I include the following script load statement instead of the one listed above:</p>

<pre class="prettyprint">
&lt;script src="https://thebuildingcoder.typepad.com/google-code-prettify/run_prettify.js"&gt;
&lt;/script&gt;
</pre>

<p>Beyond that, nothing changes.</p>


<a name="3"></a>

<h4>More Magic</h4>

<p>Oh yes, and another magical little thank you to Cyrille for pointing out the Apple Magic Mouse to me.</p>

<p>I have been using it for a few days and am enthused.</p>

<p>I first thought it was a bit too slim for my chunky hand, but that is not the case, and I really love the perfect smoothness and full control it gives, better than any other system I tried.</p>

<p>Thank you again, Cyrille  :-)</p>
