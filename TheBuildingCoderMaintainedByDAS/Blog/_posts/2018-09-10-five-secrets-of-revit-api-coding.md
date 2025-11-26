---
layout: "post"
title: "Five Secrets of Revit API Coding"
date: "2018-09-10 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "Climbing"
  - "Events"
  - "External"
  - "Getting Started"
  - "Macro"
  - "Modeless"
  - "Philosophy"
  - "Photo"
  - "Transaction"
original_url: "https://thebuildingcoder.typepad.com/blog/2018/09/five-secrets-of-revit-api-coding.html "
typepad_basename: "five-secrets-of-revit-api-coding"
typepad_status: "Publish"
---

<p>Joshua Lumley pointed out the recording he made for his BILT submission on five secrets of Revit API C# coding.</p>

<p>Before getting to that, here are a couple of pictures from this last weekend's mountain tour:</p>

<ul>
<li><a href="#2">Ruessigrat, Brotmesser and Matthorn</a> </li>
<li><a href="#3">Five secrets of Revit API Coding</a> </li>
</ul>

<h4><a name="2"></a> Ruessigrat, Brotmesser and Matthorn</h4>

<p>I crossed the Ruessigrat, Brotmesser and Matthorn ridge in splendid weather on a very nice scrambling hike with Alex:</p>

<p>It can be done without a rope, although we did in fact use a rope and the route's one and only bolt for security at the narrowest point.</p>

<p>Crossing the Brotmesser (bread knife) part of the Ruessigrat ridge:</p>

<p><center></p>

<p><a class="asset-img-link"  href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833022ad3903056200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833022ad3903056200d img-responsive" style="width: 300px; display: block; margin-left: auto; margin-right: auto;" alt="Ruessigrat" title="Ruessigrat" src="/assets/image_95a087.jpg" /></a><br /></p>

<p></center></p>

<p>Looking back along the ridge:</p>

<p><center></p>

<p><a class="asset-img-link"  href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833022ad390305d200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833022ad390305d200d img-responsive" style="width: 300px; display: block; margin-left: auto; margin-right: auto;" alt="Brotmesser part of Ruessigrat" title="Brotmesser part of Ruessigrat" src="/assets/image_5b3175.jpg" /></a><br /></p>

<p></center></p>

<p>Mountain panorama around the Matthorn ridge:</p>

<p><center></p>

<p><a class="asset-img-link"  href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833022ad369fd4f200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833022ad369fd4f200c image-full img-responsive" alt="Mountain panorama around the Matthorn ridge" title="Mountain panorama around the Matthorn ridge" src="/assets/image_58d7f5.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>For more pictures, please refer to 
the <a href="https://flic.kr/s/aHsmiejZvb">Matthorn via Ruessgrat photo album</a>.</p>

<h4><a name="3"></a> Five Secrets of Revit API Coding</h4>

<p>Last year, Joshua Lumley published a half-hour recording
on <a href="https://youtu.be/KHMwd4U_Lrs">five secrets of Revit API C# coding addressing ribbon, button, macro, external events and modeless dockable add-in</a> in
Support of his abstract submission to the BILT ANZ 2018 conference in Brisbane:</p>

<p><center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/KHMwd4U_Lrs" frameborder="0" allow="autoplay; encrypted-media" allowfullscreen></iframe>
</center></p>

<p>He very kindly 'spilled the beans', disclosing what secrets are discussed at what time points:</p>

<ol>
<li>Never initiate your coding project from an external IDE. Rather use Revit Macro Manager, then switch to a third-party IDE such as Visual Studio. This process sets everything up for you with relative paths which is critical if end user has not installed Revit on C drive. 3m:55s</li>
<li>Use <code>Invoke</code> to run external commands instead of embedding references. This will prevent many hours watching the Revit splash screen when restarting. 6m:55s</li>
<li>Always use modeless forms rather than modal ones, i.e., <code>Show</code> instead of <code>ShowDialog</code>. This allows for freedom of movement for the end user and opens the potential for creating a dockable panel in the future. 12m:40s</li>
<li>Like Russian dolls, encase your database interactions inside transactions, which are in turn encased inside a try-catch, which in turn is encased inside an external event.  This nesting is the hardest thing to get your head around but absolutely necessary. 17m.15s</li>
<li>Iterate rapidly, and seek peer review as early in the project as possible. I've found canvassing somebody else’s opinion early, can radically change the initial design intent and 'mental image' I had of my add-in. </li>
</ol>

<p>Very many thanks to Joshua for sharing his extensive Revit API experience, both here and in many other helpful comments and discussion threads!</p>
