---
layout: "post"
title: "Token Expiry and Online Revit API Docs"
date: "2016-10-17 06:00:00"
author: "Jeremy Tammik"
categories:
  - "AppStore"
  - "BIM"
  - "Docs"
  - "Forge"
  - "Getting Started"
  - "Hackathon"
  - "Photo"
  - "Travel"
original_url: "https://thebuildingcoder.typepad.com/blog/2016/10/token-expiry-and-online-revit-api-docs.html "
typepad_basename: "token-expiry-and-online-revit-api-docs"
typepad_status: "Publish"
---

<p>My vacation ended and I am now in Porto, putting the last touches to my presentation material for 
the <a href="http://www.rtcevents.com/rtc2016eur">RTC Revit Technology Conference Europe</a> and 
the <a href="https://www.facebook.com/ISEPBIM">ISEPBIM</a> Forge and BIM workshops at <a href="http://www.isep.ipp.pt">ISEP</a>, 
the <a href="http://www.isep.ipp.pt">Instituto Superior de Engenharia do Porto</a>.</p>

<p><center>
<a data-flickr-embed="true"  href="https://www.flickr.com/photos/jeremytammik/albums/72157671812736103" title="Porto"><img src="/assets/30085212350_47a567224a_n.jpg" width="320" height="240" alt="Porto"></a><script async src="//embedr.flickr.com/assets/client-code.js" charset="utf-8"></script>
</center></p>

<p>Here are today's Revit API and Forge news items:</p>

<ul>
<li><a href="#2">Updated Online Revit API Docs</a></li>
<li><a href="#3">Handling Forge token expiry</a></li>
</ul>

<h4><a name="2"></a>Updated Online Revit API Docs</h4>

<p>Gui Talarico updated the <a href="http://thebuildingcoder.typepad.com/blog/2016/08/online-revit-api-docs-and-convex-hull.html#2">online Revit API documentation</a></p>

<p><center>
<span style="font-size: 120%; font-weight: bold">
<a href="http://www.revitapidocs.com">www.RevitAPIdocs.com</a>
</span>
</center></p>

<p>In his own words:</p>

<blockquote>
  <p>In case you haven't seen it yet, I just wanted to share with you the latest version of <a href="http://www.revitapidocs.com">RevitAPIdocs.com</a>:</p>
  
  <ul>
  <li>Redesigned home page.</li>
  <li>Consolidated search for all APIs: search results highlight if entry is not part of the active 'Year' and redirect you (i.e., to leaders in Revit 2015).</li>
  <li>Result filtering by type (Class, Method, etc.).</li>
  <li>Built in autocomplete engine to help users find most relevant entries &ndash; it will improve further over time!</li>
  </ul>
</blockquote>

<p>Here is an animation showing a quick overview of the new features:</p>

<p><center></p>

<p><img src="/assets/revitapidocs_newfeat.gif" alt="Revit API Docs new features"></p>

<!----
<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c8a21093970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c8a21093970b image-full img-responsive" alt="Revit API Docs new features" title="Revit API Docs new features" src="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c8a21093970b-800wi" border="0" /></a><br />
---->

</center>

The revamped search result landing page looks like this:

<center>

<img src="/assets/revitapidocs_landing2.gif" alt="Revit API Docs search landing page">

<!----
<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c8a210b1970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c8a210b1970b image-full img-responsive" alt="Revit API Docs landing page" title="Revit API Docs landing page" src="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c8a210b1970b-800wi" border="0" /></a><br />
---->

<p></center></p>

<p>Many thanks to Gui for providing this really important tool!</p>

<h4><a name="3"></a>Handling Forge Token Expiry</h4>

<p><strong>Question:</strong> I am working on an app for
the <a href="http://autodeskforge.devpost.com">Forge and AppStore hackathon challenge</a>.</p>

<p>I am using the <a href="https://models.autodesk.io">models.autodesk.io online tool</a> for token generation.</p>

<p>However, my access token is getting expired.</p>

<p>Please help resolve the issue.</p>

<p><strong>Answer by Cyrille Fauvel:</strong> <a href="https://models.autodesk.io">models.autodesk.io</a> is a web site which demonstrates how to easily translate a model using your own consumer and secret key.</p>

<p>It is not intended to be used as an access token generator, because it is highly discouraged to transmit your keys over Internet, even through an encrypted and secured <code>https</code> connection.</p>

<p>Instead, you should write your own code to generate the access token yourself, just like <code>models.autodesk.io</code> does. The relevant part is located 
in <a href="https://github.com/cyrillef/models.autodesk.io/blob/master/server/lmv-token.js#L43">lmv-token.js around line 43</a>.</p>

<p>As you have noticed, the token expires after a while.</p>

<p>This sample does not take care of refreshing it, as this site is to be used manually for a single translation.</p>

<p>In your own implementation, you will need to check the <code>expires_in</code> field to know when the token will expire and make sure to refresh it before it does so as shown
in <a href="https://github.com/cyrillef/extract.autodesk.io/blob/master/server/lmv-token.js#L29">lmv-token.js around line 29</a>.</p>

<p>Thank you, Cyrille, for the complete and succinct answer.</p>
