---
layout: "post"
title: "Crash when not using raw COM interface"
date: "2013-08-30 08:54:50"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/08/crash-when-not-using-raw-com-interface-to-set-property.html "
typepad_basename: "crash-when-not-using-raw-com-interface-to-set-property"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>Let&#39;s say we have the following code:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px; line-height: 120%;"><span style="color: green;">// First select a RectangularPatternFeature then run the code</span></p>
<p style="margin: 0px; line-height: 120%;">CComQIPtr&lt;RectangularPatternFeature&gt; pPattern = </p>
<p style="margin: 0px; line-height: 120%;">&#0160; pInvApp-&gt;ActiveDocument-&gt;SelectSet-&gt;Item[1];</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">for</span> (<span style="color: blue;">int</span> i = 1; i &lt;= pPattern-&gt;PatternElements-&gt;Count; i++)</p>
<p style="margin: 0px; line-height: 120%;">{</p>
<p style="margin: 0px; line-height: 120%;">&#0160; CComPtr&lt;FeaturePatternElement&gt; pElem = </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; pPattern-&gt;PatternElements-&gt;Item[i];</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: green;">// In this case there is no crash</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; pElem-&gt;put_Suppressed(<span style="color: blue;">false</span>); <span style="color: green;">// or true</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: green;">// No matter if I use false or true</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: green;">// both cases will cause an error</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; pElem-&gt;Suppressed = <span style="color: blue;">false</span>; <span style="color: green;">// or true</span></p>
<p style="margin: 0px; line-height: 120%;">}</p>
</div>
<p>The difference between <strong>put_Suppressed(false)</strong> and <strong>Suppressed = false</strong> is that the former is the raw interface which simply provides an <strong>HRESULT</strong> value to let you know if there was an error, while the latter is a wrapper around it that is throwing a <strong>_com_error</strong> exception in case of an error.</p>
<p>You can also check the <strong>rxinventor.tli</strong> file of your project to see the exact implementation of the raw and wrapper functions:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">#pragma</span> implementation_key(29648)</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">inline</span> <span style="color: blue;">void</span> Inventor::RectangularPatternFeature::PutSuppressed ( VARIANT_BOOL _arg1 ) {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; _com_dispatch_method(<span style="color: blue;">this</span>, 0x500030a, DISPATCH_PROPERTYPUT, VT_EMPTY, NULL, </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; L<span style="color: #a31515;">&quot;\x000b&quot;</span>, _arg1);</p>
<p style="margin: 0px; line-height: 120%;">}</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
</div>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">#pragma</span> implementation_key(29649)</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">inline</span> HRESULT Inventor::RectangularPatternFeature::put_Suppressed ( VARIANT_BOOL _arg1 ) {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">return</span> _com_dispatch_raw_method(<span style="color: blue;">this</span>, 0x500030a, DISPATCH_PROPERTYPUT, VT_EMPTY, NULL, </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; L<span style="color: #a31515;">&quot;\x000b&quot;</span>, _arg1);</p>
<p style="margin: 0px; line-height: 120%;">}</p>
</div>
<p>What is the error in our code? The problem is that the first element of a rectangular pattern cannot be suppressed - you can easily verify this in the user interface:</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019aff171c60970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="PatternElementSuppress" class="asset  asset-image at-xid-6a0167607c2431970b019aff171c60970b" src="/assets/image_74884f.jpg" style="width: 450px;" title="PatternElementSuppress" /></a></p>
<p>While <strong>put_Suppressed(false)</strong> simply returns an error code, <strong>Suppressed = false</strong> will throw an error that you would need to catch. You can solve it like this:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px; line-height: 120%;"><span style="color: green;">// First select a RectangularPatternFeature then run the code</span></p>
<p style="margin: 0px; line-height: 120%;">CComQIPtr&lt;RectangularPatternFeature&gt; pPattern = </p>
<p style="margin: 0px; line-height: 120%;">&#0160; pInvApp-&gt;ActiveDocument-&gt;SelectSet-&gt;Item[1];</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">for</span> (<span style="color: blue;">int</span> i = 1; i &lt;= pPattern-&gt;PatternElements-&gt;Count; i++)</p>
<p style="margin: 0px; line-height: 120%;">{</p>
<p style="margin: 0px; line-height: 120%;">&#0160; CComPtr&lt;FeaturePatternElement&gt; pElem = </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; pPattern-&gt;PatternElements-&gt;Item[i];</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: green;">// In this case no crash</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; pElem-&gt;put_Suppressed(<span style="color: blue;">false</span>); <span style="color: green;">// or true</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: green;">// Now we catch the error so nothing will crash</span></p>
<p style="margin: 0px; line-height: 120%;"><span style="color: green;">&#0160; // Note: the real solution would be to start the iteration</span></p>
<p style="margin: 0px; line-height: 120%;"><span style="color: green;">&#0160; // from 2 instead of 1 if we just want to change the </span></p>
<p style="margin: 0px; line-height: 120%;"><span style="color: green;">&#0160; // Suppressed state&#0160;</span><span style="color: green; line-height: 120%; background-color: white; font-size: 8pt;">of the items. I left it on 1 just to </span></p>
<p style="margin: 0px; line-height: 120%;"><span style="color: green; line-height: 120%; background-color: white; font-size: 8pt;">&#0160; // show that the error handling works&#0160;</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">try</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; pElem-&gt;Suppressed = <span style="color: blue;">false</span>; <span style="color: green;">// or true</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; }</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">catch</span> (_com_error e)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: green;">// do something if you want</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; }</p>
<p style="margin: 0px; line-height: 120%;">}</p>
</div>
