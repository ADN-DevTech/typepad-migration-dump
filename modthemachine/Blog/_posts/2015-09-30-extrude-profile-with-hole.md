---
layout: "post"
title: "Extrude profile with hole"
date: "2015-09-30 11:37:08"
author: "Adam Nagy"
categories:
  - "Adam"
  - "Fusion 360"
original_url: "https://modthemachine.typepad.com/my_weblog/2015/09/extrude-profile-with-hole.html "
typepad_basename: "extrude-profile-with-hole"
typepad_status: "Publish"
---

<p>During the <a href="http://fusion360hackathon.com/" target="_self">hackathon</a> we got the following question that Brian answered. I added a picture to make it even easier to understand :)</p>
<p><strong>Q:</strong>&#0160;I’m having a problem extruding a sketch with multiple profiles. I can never seem to create holes.&#0160; What am I missing?</p>
<p><strong>A:&#0160;</strong>In your case, there are two profiles. <br />The first consists of the rectangle with the hole (<strong>1</strong>) and the second is just the hole (<strong>2</strong>).&#0160; It’s the same thing you would see in the <strong>UI</strong> if you draw the sketch and then manually create an extrusion. When you move the mouse within the rectangle, but outside of the circle it will highlight one of the profiles.&#0160; Moving the mouse into the circle will highlight the other profile.&#0160; By adding both of them to the collection you were essentially filling the hole. The <strong>C++</strong> code below uses some simple logic based on knowledge about the sketch geometry to pick the outer profile (with two <strong>ProfileLoop</strong>&#39;s: <strong>a</strong> and <strong>b</strong>). This logic will also work if there are multiple holes.</p>
<p><a class="asset-img-link" href="http://a0.typepad.com/6a0112791b8fe628a401b7c7d617e8970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Hole_profile" class="asset  asset-image at-xid-6a0112791b8fe628a401b7c7d617e8970b img-responsive" src="/assets/image_304696.jpg" title="Hole_profile" /></a></p>
<p>Note: the order of the profiles could be different from the one shown in the picture.</p>
<pre>// Put all the profiles in an object collection
// Get the Profiles collection
Ptr&lt;Profiles&gt; pProfiles = sketch-&gt;profiles();
Ptr&lt;ObjectCollection&gt; objectsForExtrude = ObjectCollection::create();
for each (Ptr&lt;Profile&gt; pProfile in pProfiles)
{
  // Check to see if this is the outer rectangular profile or not
  // by checking the number of loops.  The outer profile will have two
  // loops, one for the rectangle and one for the circle. 
  Ptr&lt;ProfileLoops&gt; loops = pProfile-&gt;profileLoops();
             
  if (loops-&gt;count() &gt; 1)
    objectsForExtrude-&gt;add(pProfile);
}</pre>
<p>-Adam</p>
