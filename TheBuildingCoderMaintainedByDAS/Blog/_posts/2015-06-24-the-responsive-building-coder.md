---
layout: "post"
title: "The Responsive Building Coder"
date: "2015-06-24 05:00:00"
author: "Jeremy Tammik"
categories:
  - "HTML"
  - "I18n"
  - "JavaScript"
  - "Mobile"
  - "News"
  - "Update"
  - "User Interface"
original_url: "https://thebuildingcoder.typepad.com/blog/2015/06/the-responsive-building-coder.html "
typepad_basename: "the-responsive-building-coder"
typepad_status: "Publish"
---

<script src="https://thebuildingcoder.typepad.com/google-code-prettify/run_prettify.js"></script>

<p>I suffer from acute and chronic responsiveness.</p>

<p>That is unpleasant for me, although hopefully pleasing and useful for the rest of humanity.</p>

<p>What I mean is, I tend to answer other people's questions and needs before taking care of my own.</p>

<p>I am forever trying to stop doing that.</p>

<p>One of these days, I certainly will.</p>

<p>The topic I refer to above is not me, personally, though, but The Building Coder blog.</p>

<p>Nowadays, web pages should be based on a
<a href="https://en.wikipedia.org/wiki/Responsive_web_design">responsive web design</a> or
their Google search ranking will suffer.</p>

<p>Kean Walmsley explained the
<a href="http://through-the-interface.typepad.com/through_the_interface/2015/02/reading-from-mobile-devices-time-for-a-redesign.html">
growing usage of mobile devices</a>, leading to the recent
<a href="http://through-the-interface.typepad.com/through_the_interface/2015/02/reading-from-mobile-devices-time-for-a-redesign.html">
responsive redesign of Through the Interface</a>.</p>

<p>Following his lead, I switched The Building Coder to a responsive design now as well, as you can see.</p>

<p>I hope you like it!</p>

<p>Just like Kean's blog, I use the Typepad 'Snap' template with Lato and Open Sans fonts.
The latter requires a reference to the Google font stylesheet.</p>

<p>I struggled a bit with the banner image, especially to suppress the default header text that Typepad generates automatically and puts on top of the image.</p>

<p>I finally found the following simple CSS solution for that:</p>

<pre class="prettyprint">
.jumbotron h1, .jumbotron h2 { display: none }
</pre>

<p>The next main issue I had was with the Microsoft Bing translation widget.</p>

<p>I set it up on their web page and added the resulting HTML and JavaScript to my blog design.</p>

<p>It did weird things, and even after significant tweaking was still displaying two user interfaces on top of each other.</p>

<p>After too much of that mess, I opted for the Google translation service instead.</p>

<p>I hope you like the result, especially if you are on a mobile device.</p>

<p>Please do let me know what you think. Thank you!</p>
