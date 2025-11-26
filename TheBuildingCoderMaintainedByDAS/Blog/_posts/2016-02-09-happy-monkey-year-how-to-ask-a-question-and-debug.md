---
layout: "post"
title: "Happy Monkey, How To Ask a Question and Debug"
date: "2016-02-09 05:00:00"
author: "Jeremy Tammik"
categories:
  - "External"
  - "Getting Started"
  - "News"
  - "Philosophy"
original_url: "https://thebuildingcoder.typepad.com/blog/2016/02/happy-monkey-year-how-to-ask-a-question-and-debug.html "
typepad_basename: "happy-monkey-year-how-to-ask-a-question-and-debug"
typepad_status: "Publish"
---

<p>I seem to become more and more fanatically didactical as time goes on.</p>

<p>I guess I answer too many questions, and am irritated when they are not asked in an optimal way.</p>

<p>One important point, of course, as in
all <a href="https://en.wikipedia.org/wiki/Communication">communication</a>,
is to formulate your message with the receiver in mind.</p>

<p>I explained that for two specific cases today, and thought I would mention my suggestions here for future reference, after wishing everybody</p>

<ul>
<li><a href="#2">Happy New Year of the Monkey!</a></li>
<li><a href="#3">How to debug a complex issue</a></li>
<li><a href="#4">How to ask a question</a></li>
</ul>

<h4><a name="2"></a>Happy New Year of the Monkey!</h4>

<p>Our Chinese colleagues are already in full celebration of the Chinese New Year.
It began yesterday, February 8.
In fact, the whole of this week is a holiday in China and our offices there.</p>

<p>In case of interest, check out the <a href="http://thebuildingcoder.typepad.com/blog/2012/01/chinese-new-year-impressions.html">Chinese New Year impressions</a> from the Year of the Dragon back in 2012.</p>

<p>This year is Monkey, charming, charismatic and extremely inventive.</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b8d19d6264970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b8d19d6264970c image-full img-responsive" alt="Happy New Year of the Monkey!" title="Happy New Year of the Monkey!" src="/assets/image_eda7c9.jpg" border="0" /></a><br /></p>

<p></center></p>

<p>This <a href="http://www.chinahighlights.com/travelguide/chinese-zodiac/monkey.htm">Chinese Zodiac</a> provides descriptions of the different types of monkey and enables you to look up your own Chinese Zodiac sign.</p>

<p>The Chinese New Year is also called Spring Festival.</p>

<p>After the holiday, Spring will come, the start of one more year.</p>

<p>Happy New Year!</p>

<h4><a name="3"></a>How to Debug a Complex Issue</h4>

<p>People frequently raise questions about some programming problem in their Revit add-ins and try to find help for that from others.</p>

<p>This help is often hard to provide due to the complexity of the issue.</p>

<p>Such a point arose again today in the Revit API discussion
on <a href="http://forums.autodesk.com/t5/revit-api/api-constraint-management/td-p/6021191/jump-to/first-unread-message">API constraint management</a>.</p>

<p>Here is my recommendation on how to approach this, which I now also added as a post-post-scriptum to the <a href="http://thebuildingcoder.typepad.com/blog/about-the-author.html#1b">instructions for a reproducible test case</a>:</p>

<p>I would love to dive in and try to help you debug this, but I am sorry to say I do not have the time.</p>

<p>You will have to continue exploring it yourself.</p>

<p>All I can suggest is to keep at it.</p>

<p>One approach to debugging a problem like this is:</p>

<ol>
<li>Simplify it down to something absolutely trivial and stupid that is guaranteed to work &ndash; dumb it down.</li>
<li>Once that is working, add the required complications one by one until it either works completely or fails.</li>
</ol>

<p>Once you have determined the exact point of failure, you can narrow that down further and create a <u>minimal</u> <a href="http://thebuildingcoder.typepad.com/blog/about-the-author.html#1b">reproducible case</a>.</p>

<p>With the minimal reproducible case in hand, I can either take a look myself of pass it on to the Revit development team for further analysis.</p>

<p>Thank you!</p>

<p><strong>Addendum:</strong> You might also want to read another example of how to handle this kind of issue that just came up in the Revit API discussion forum thread
on <a href="http://forums.autodesk.com/t5/revit-api/retrive-in-place-mass-reference-level/m-p/6050735">using DMU to retrieve in-place mass reference level</a>.</p>

<h4><a name="4"></a>How to Ask a Question</h4>

<p>In a related vein, I often receive requests by email or otherwise to solve a programming issue, or, in some cases, assess the possibility of implementing a programmatic solution for some given task.</p>

<p>Here is my most recent reaction to such a request:</p>

<ul>
<li>Ask a question on this in the <a href="http://forums.autodesk.com/t5/revit-api/bd-p/160">Revit API discussion forum</a> instead of asking me directly.</li>
<li>Make sure that your issue is formulated in a generic way.</li>
<li>Strip out as much  complexity as possible. <a href="https://en.wikipedia.org/wiki/KISS_principle">KISS</a>!</li>
</ul>

<p>Posting an issue in public enables me to take a look at it, my colleagues as well, and the entire Revit developer community also.</p>

<p>My standard motivation blurb for goes like this:</p>

<blockquote>
  <p>I do nothing at all that cannot be shared and published, and share and publish absolutely everything I work on.</p>
  
  <p>I therefore avoid discussing questions like this, and actually all questions whatsoever, one-on-one.</p>
  
  <p>I prefer discussing everything in public and enabling the entire community to contribute and share.</p>
  
  <p>That has several advantages. Among others, your peers can join in and help you, and our conversation becomes visible to others, to help them resolve their issues as well.</p>
</blockquote>

<p>In addition to formulating a generic question and publishing it on the forum, a minimal <a href="http://thebuildingcoder.typepad.com/blog/about-the-author.html#1b">reproducible case</a> can also help a lot.</p>

<p>Put together an absolutely minimal example that clearly demonstrates the problem and also makes it easy to check whether a solution really achieves the desired result.</p>

<p>That will help people understand the issue better and motivate them much more to explore and chip in and help.</p>
