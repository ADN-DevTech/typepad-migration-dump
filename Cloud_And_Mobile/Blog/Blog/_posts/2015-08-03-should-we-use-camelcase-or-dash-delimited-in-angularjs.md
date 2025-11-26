---
layout: "post"
title: "Should we use camelCase or dash-delimited in Angular.Js?"
date: "2015-08-03 23:41:37"
author: "Daniel Du"
categories: []
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/08/should-we-use-camelcase-or-dash-delimited-in-angularjs.html "
typepad_basename: "should-we-use-camelcase-or-dash-delimited-in-angularjs"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/cloud_and_mobile/daniel-du.html">Daniel Du</a></p>  <p>I believe many of us learn new stuff by looking at samples or even using them directly instead of reading through documents first. It’s true for me. This confuses me for a while when I am practicing to create a new directive in Angular.Js. I am sharing my experience in case you are run into the same situation. </p>  <p>I am trying to create a new directive, the JS code goes as below: </p>  <pre class="csharpcode">app.directive(<span class="str"><strong><font color="#ff0000">'appInfo'</font></strong></span>, <span class="kwrd">function</span>() { 
  <span class="kwrd">return</span> { 
    restrict: <span class="str">'E'</span>, 
    scope: { 
      info: <span class="str">'='</span> 
    }, 
    templateUrl: <span class="str">'js/directives/appInfo.html'</span> 
  }; 
});</pre>

<p>As you can see, it is definition of new directive, which is named as “appInfo”, and it is an element directive.&#160; Very naturally, I am trying to use this directive in HTML by following code: </p>

<pre class="csharpcode"><span class="kwrd">&lt;</span><span class="html">div</span> <span class="attr">class</span><span class="kwrd">=&quot;card&quot;</span><span class="kwrd">&gt;</span> 
     <span class="kwrd">&lt;</span><span class="html"><strong><font color="#ff0000">appInfo</font></strong></span> <span class="attr">info</span><span class="kwrd">=&quot;move&quot;</span><span class="kwrd">&gt;&lt;/</span><span class="html"><strong><font color="#ff0000">appInfo</font></strong></span><span class="kwrd">&gt;</span> 
<span class="kwrd">&lt;/</span><span class="html">div</span><span class="kwrd">&gt;</span></pre>
<style type="text/css">



.csharpcode, .csharpcode pre
{
	font-size: small;
	color: black;
	font-family: consolas, "Courier New", courier, monospace;
	background-color: #ffffff;
	/*white-space: pre;*/
}
.csharpcode pre { margin: 0em; }
.csharpcode .rem { color: #008000; }
.csharpcode .kwrd { color: #0000ff; }
.csharpcode .str { color: #006080; }
.csharpcode .op { color: #0000c0; }
.csharpcode .preproc { color: #cc6633; }
.csharpcode .asp { background-color: #ffff00; }
.csharpcode .html { color: #800000; }
.csharpcode .attr { color: #ff0000; }
.csharpcode .alt 
{
	background-color: #f4f4f4;
	width: 100%;
	margin: 0em;
}
.csharpcode .lnum { color: #606060; }</style>

<p>But, while I am running it, Bong~, it actually does not work. Why?? Am I doing anything wrong? Am I spell something wrong? I know JavaScript is case sensitive language but HTML is not, but why it does not work even I spell them exactly the same?</p>

<p>Actually the correct way to use this directive is to use dash-case in HTML like below:</p>

<pre class="csharpcode"><span class="kwrd">&lt;</span><span class="html">div</span> <span class="attr">class</span><span class="kwrd">=&quot;card&quot;</span><span class="kwrd">&gt;</span> 
     <span class="kwrd">&lt;</span><span class="html"><strong><font color="#ff0000">app-info</font></strong></span> <span class="attr">info</span><span class="kwrd">=&quot;move&quot;</span><span class="kwrd">&gt;&lt;/</span><span class="html">app-info</span><span class="kwrd">&gt;</span> 
<span class="kwrd">&lt;/</span><span class="html">div</span><span class="kwrd">&gt;</span></pre>
<style type="text/css">



.csharpcode, .csharpcode pre
{
	font-size: small;
	color: black;
	font-family: consolas, "Courier New", courier, monospace;
	background-color: #ffffff;
	/*white-space: pre;*/
}
.csharpcode pre { margin: 0em; }
.csharpcode .rem { color: #008000; }
.csharpcode .kwrd { color: #0000ff; }
.csharpcode .str { color: #006080; }
.csharpcode .op { color: #0000c0; }
.csharpcode .preproc { color: #cc6633; }
.csharpcode .asp { background-color: #ffff00; }
.csharpcode .html { color: #800000; }
.csharpcode .attr { color: #ff0000; }
.csharpcode .alt 
{
	background-color: #f4f4f4;
	width: 100%;
	margin: 0em;
}
.csharpcode .lnum { color: #606060; }</style>

<p>Okay, using dash-delimited style make it more like angular style. But back to the JS file, if you do not use camelCase in directive name and change it to dash-delimited style, it does not work as well. This does not make sense to me, and then I read from the <a href="https://docs.angularjs.org/guide/directive">document of Angular.Js</a>, it talked about this as normalization:</p>

<blockquote>
  <p>“Angular <strong>normalizes</strong> an element's tag and attribute name to determine which elements match which directives. We typically refer to directives by their case-sensitive <a href="http://en.wikipedia.org/wiki/CamelCase">camelCase</a> <strong>normalized</strong> name (e.g.<code>ngModel</code>). However, since HTML is case-insensitive, we refer to directives in the DOM by lower-case forms, typically using <a href="http://en.wikipedia.org/wiki/Letter_case#Computers">dash-delimited</a> attributes on DOM elements (e.g. <code>ng-model</code>).”</p>
</blockquote>

<p>So <strong>conclusion</strong>:</p>

<ul>
  <li>When defining a new directive, the directive name should be in camelCase. </li>

  <li>When using it in HTML, use it in dash-delimited style. </li>
</ul>

<p>It is not a big deal, just something you may miss if you just learn angular from sample. </p>
