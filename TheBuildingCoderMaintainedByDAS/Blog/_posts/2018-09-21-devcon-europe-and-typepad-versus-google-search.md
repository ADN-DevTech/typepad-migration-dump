---
layout: "post"
title: "DevCon Europe and Typepad versus Google Search"
date: "2018-09-21 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Accelerator"
  - "AU"
  - "DevCon"
  - "Events"
  - "Forge"
  - "HTML"
  - "JavaScript"
original_url: "https://thebuildingcoder.typepad.com/blog/2018/09/devcon-europe-and-typepad-versus-google-search.html "
typepad_basename: "devcon-europe-and-typepad-versus-google-search"
typepad_status: "Publish"
---

<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>

<p>I am making final travel preparations for the Forge accelerator in Rome next week, and need to start preparing for DevCon Europe as well.</p>

<p>As always, when you have no time to spare, something else urgent cropped up as well requiring immediate attention:</p>

<ul>
<li><a href="#2">Forge DevCon Europe Coming</a> </li>
<li><a href="#3">Hijacking Typepad search input for Google site search</a> </li>
</ul>

<h4><a name="2"></a> Forge DevCon Europe Coming</h4>

<p>As Kean Walmsley just pointed out, we
are <a href="http://keanw.com/2018/09/counting-down-to-forge-devcon-europe.html">counting down to Forge DevCon Europe</a>,
the second European edition of the <a href="https://forge.autodesk.com/devcon-2018">Forge DevCon</a>.</p>

<p>It is being held in Darmstadt on October 16th, the day before this year’s AU Germany.</p>

<p>This is a free, English-language event where you can learn all about the capabilities that are currently (and soon will be) available through
the <a href="https://autodesk-forge.github.io">Forge platform</a> (the
entire DevCon event is in English language, whereas the Autodesk University following it is in German).</p>

<p><center></p>

<p><a class="asset-img-link"  href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833022ad3b2d722200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833022ad3b2d722200b img-responsive" alt="Autodesk Forge Devcon Europe 2018" title="Autodesk Forge Devcon Europe 2018" src="/assets/image_6151d3.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>Here are some reasons to attend, provided by the Forge team:</p>

<ul>
<li>Get to Know Forge &ndash; 
Take a deep dive into the Forge Viewer, BIM 360 API, Reality Capture, and industry specific solutions created with Forge APIs.
Plus, preview just-released Forge updates to integrations, end-to-end workflow capabilities, and much more.</li>
<li>Disruptive Technologies &ndash; 
AR, VR, machine learning and generative design can be brought to life using Forge. Learn how, and see how easy it is to get started.</li>
<li>Web Programming &ndash; 
Delve into topics from web application security to architecture to testing best practices, or brush up on the basics you need to transition from desktop to web programming.</li>
<li>Forge Industry Stories &ndash; 
Take in cross-category case studies showing how Autodesk partners and customers used Forge to solve real business problems, and what they learned along the way.</li>
<li>Construction Technology with Forge + BIM 360 &ndash; 
Learn how Forge and BIM 360 have been used to create countless apps across the construction industry, helping teams innovate, digitize job sites, minimize data loss and increase efficiency.</li>
</ul>

<p>I will be there, together with Kean and many other world-wide colleagues.</p>

<p>You can <a href="https://www.rayseven.com/r7/runtime/autodesk/devcon2018/registration.visitor.php">register today via this link</a>.</p>

<p>Also, again like Kean, my next stop is next week in Rome at
the <a href="http://autodeskcloudaccelerator.com">Forge accelerator</a>.</p>

<p>I hope you can make it to one of our upcoming events as well.</p>

<h4><a name="3"></a> Hijacking Typepad Search Input for Google Site Search</h4>

<p>Matt Taylor submitted
a <a href="http://thebuildingcoder.typepad.com/blog/2018/09/roadmap-ci-for-rtf-geometry-library-limitations.html#comment-4106874384">comment</a>
on <a href="http://thebuildingcoder.typepad.com/blog/2018/09/roadmap-ci-for-rtf-geometry-library-limitations.html">yesterday's blog post</a> pointing
out that the search functionality stopped working:</p>

<blockquote>
  <p>Just a heads-up that the search box on your blog doesn't appear to be working.</p>
  
  <p>I tested in IE and Chrome.</p>
  
  <p>It used to work...</p>
  
  <p>I'll normally add the site to the search like this in a search engine:</p>
  
  <p><code>site:http://thebuildingcoder.typepad.com floor</code></p>
  
  <p>... Which is what I did when it didn't work.</p>
</blockquote>

<p>The problem is caused by the built-in Typepad search module.</p>

<p>Just for your information, the entire blog source code is also available in parallel in
the <a href="https://github.com/jeremytammik/tbc">tbc GitHub repository</a>,
so you can always download from there to your own system and search there with full control and flexibility.</p>

<p>After some in-depth research and JavaScript twiddling, I am now opening my own Google search window in a new tab in parallel with the dysfunctional Typepad search.</p>

<p>I dove into the blog source code to determine and retrieve the search input text box and submit button and hijack it to open a second Google search window doing just that.</p>

<p>Here is the code that I added to the site to achieve this:</p>

<pre class="prettyprint">
  &lt;script type="text/javascript"&gt;
  window.onload = function(){
    var form = document.getElementById('search-blog');
    var submit = form.getElementsByClassName('btn')[0];
    var searchinput = form.getElementsByClassName('form-control')[0];
    submit.addEventListener('click', function(event) {
      var s = searchinput.value;
      s = s.replace(/ /g, '+');
      var url = 'https://www.google.com/search?q='
        + s + '&as_sitesearch=thebuildingcoder.typepad.com';
      //window.location.href=url;
      var win = window.open(url, '_blank');
      win.focus();
    });
  }
  &lt;/script&gt;
</pre>

<p>I initially tried to override the Typepad functionality completely using <code>window.location.href</code>, but that failed.</p>

<p>Therefore, I open the second Google site search window in parallel and set the focus to that instead.</p>

<p>By the way, I found
this <a href="https://moz.com/blog/the-ultimate-guide-to-the-google-search-parameters">ultimate guide to the Google search parameters</a> useful
to determine what URL arguments to add, i.e., <code>as_sitesearch</code>.</p>

<p>Finally, I discussed the issue with Typepad, saying:</p>

<blockquote>
  <p>the built-in typepad search module does not work.
  i now implemented my own javascript version to open a google search engine in a separate tab.
  what can i do to make the built-in typepad search module work as intended?
  to see what i mean, please go to <a href="http://thebuildingcoder.typepad.com">thebuildingcoder.typepad.com</a>,
  type <code>IExternalCommandAvailability</code> in the search input text box and click 'Submit'.
  the built-in search reports that no results are found:</p>
  
  <p><em>No Results Found &ndash; Sorry, there were no results returned for “IExternalCommandAvailability” &ndash; please check your spelling, or try something less specific.</em></p>
  
  <p>i open a new tab with a google search engine limited to thebuildingcoder site, and it reports many results.</p>
</blockquote>

<p>Typepad answers:</p>

<blockquote>
  <p>Thank you for reaching out to us. We are aware search results are not displaying as expected, and we are currently working on an update to the search feature. We apologize for the inconvenience. If you have any other questions, please let us know.</p>
</blockquote>

<p>I have no other questions, thank you, Typepad.</p>

<p>Thank you very much, Matt, for pointing this out!</p>
