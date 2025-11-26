---
layout: "post"
title: "Happy New Year with RevitExtensions"
date: "2022-01-04 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "2022"
  - "Climbing"
  - "Utilities"
original_url: "https://thebuildingcoder.typepad.com/blog/2022/01/happy-new-year-with-revitextensions.html "
typepad_basename: "happy-new-year-with-revitextensions"
typepad_status: "Publish"
---

<p>Off we go into a new adventurous year of BIM programming:</p>

<ul>
<li><a href="#2">Happy New Year</a></li>
<li><a href="#3">RevitExtensions</a></li>
</ul>

<h4><a name="2"></a> Happy New Year</h4>

<p>Happy New Year and welcome to 2022!</p>

<p>I spent a pleasant New Year's Day climbing the Wildhauser Schafberg in warm and dry weather.</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302942f922afd200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302942f922afd200c img-responsive" style="width: 300px; display: block; margin-left: auto; margin-right: auto;" alt="Jeremy figurehead on Wildhauser Schafberg" title="Jeremy figurehead on Wildhauser Schafberg" src="/assets/image_e6b748.jpg" /></a><br /></p>

<p></center></p>

<p>Afterwards, a friend pointed out this rather humorous New Year's greeting from 1883.
It renders better in its original German version than in English:</p>

<p style="text-align: center; font-weight: bold">Neujahrsgebet</p>

<p style="text-align:center">Herr, setze dem Überfluss Grenzen
<br/>und lasse die Grenzen überflüssig werden.
<br/>Lasse die Leute kein falsches Geld machen
<br/>und auch Geld keine falschen Leute.
<br/>Nimm den Ehefrauen das letzte Wort
<br/>Und erinnere die Ehemänner an ihr erstes.
<br/>Schenke unseren Freunden mehr Wahrheit
<br/>und der Wahrheit mehr Freunde.
<br/>Gib den Regierenden ein besseres Deutsch
<br/>Und den Deutschen eine bessere Regierung.
<br/>Herr, sorge dafür, dass wir alle in den Himmel kommen
<br/>Aber nicht sofort!</p>

<p style="text-align:center; font-style: italic">Lord, please border abundance
<br/>and make borders superfluous.
<br/>Don't let people make bad money
<br/>and don't let money make bad people.
<br/>Take the last word away from the wives
<br/>And remind the husbands of their first.
<br/>Give more truth to our friends
<br/>and more friends to the truth.
<br/>Give the rulers a better German
<br/>And better rulers to the Germans.
<br/>Lord, please may we all go to heaven
<br/>but not right away!</p>

<p style="text-align:right; font-style: italic">Parish priest Hermann Josef Kappen of Lamberti church in Münster, 1883</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330282e13d0732200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330282e13d0732200b img-responsive" style="width: 500px; display: block; margin-left: auto; margin-right: auto;" alt="Neujahrsgruss 1883" title="Neujahrsgruss 1883" src="/assets/image_0d1f7d.jpg" /></a><br /></p>

<p></center></p>

<h4><a name="3"></a> RevitExtensions</h4>

<p>In the last post of the previous year, I mentioned 
Roman <a href="https://github.com/Nice3point">Nice3point</a>, his huge contributions
to <a href="https://github.com/jeremytammik/RevitLookup">RevitLookup</a> in the past few months,
his <a href="https://thebuildingcoder.typepad.com/blog/2021/12/revittemplates-update-170.html">RevitTemplates update 1.7.0</a>
and the invitation to provide feedback on them.</p>

<p>Let's move into this new year with yet another contribution and invitation from Roman:</p>

<p>Hi guys, it's time to pump the Revit API.
I started developing a library that will make it easier to write code using extensions.
In general, instead of <code>Method(Method(Method(Method(Method()))))</code>, you can write <code>Method.Method.Method.Method.Method</code>.
And, of course, I added a couple of new methods and overloads that are not in the API.
Working with the Ribbon and *Utils classes has been greatly simplified.
If you have any suggestions for improving the API, write to me about it in
the</p>

<p style="text-align:center"><a href="https://github.com/Nice3point/RevitExtensions">RevitExtensions GitHub repository</a>.</p>

<blockquote>
  <p>Improve your experience with Revit API now</p>
  
  <p>Extensions minimize the writing of repetitive code, add new methods not included in the native Revit API, and also allow you to write chained methods without worrying about API versioning:</p>
  
  <pre class="code">
    new ElementId(123469)
      .ToElement(document)
      .GetParameter(BuiltInParameter.DOOR_HEIGHT)
      .AsDouble()
      .ToMillimeters()
      .Round()
  </pre>
  
  <p>Extensions include annotations to help ReShaper parse your code and signal when a method may return null, or the value returned by the method is not used in your code.</p>
</blockquote>

<p>Many thanks again to Roman for all his tremendous work supporting and enhancing Revit API development!</p>

<p>And, again, Happy New Year to all!</p>
