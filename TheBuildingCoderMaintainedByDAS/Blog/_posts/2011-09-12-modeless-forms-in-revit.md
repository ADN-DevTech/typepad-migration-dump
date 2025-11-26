---
layout: "post"
title: "Modeless Forms in Revit"
date: "2011-09-12 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "Algorithm"
  - "Automation"
  - "Events"
  - "External"
original_url: "https://thebuildingcoder.typepad.com/blog/2011/09/modeless-forms-in-revit.html "
typepad_basename: "modeless-forms-in-revit"
typepad_status: "Publish"
---

<p>Today the moon is full. 
I celebrated it last night already, sitting with a friend and having a fire on a hill with a 270 degree view to the east, west and south. 
Very beautiful!

<p>This full moon is special, because it is the occasion of the Chinese 

<a href="http://en.wikipedia.org/wiki/Mid-Autumn_Festival">
Mid-Autumn Festival</a>. 
It "parallels the autumnal equinox of the solar calendar, when the moon is at its fullest and roundest".</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833014e8b7a6fed970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833014e8b7a6fed970d image-full" alt="Mid-autumn full moon festival 2011" title="Mid-autumn full moon festival 2011" src="/assets/image_c704ef.jpg" border="0" /></a> <br />

</center>

<p>This Chinese festival will even be celebrated in Switzerland tonight, as the 

<a href="http://www.basel.ch/mondfest_plakat_web2.pdf">
Mondefest Basel 2011</a>, 

to honour the city partnership of Basel and Shanghai.


<a name="1"></a>

<h4>Accessing Revit from Outside an API Call-back Context</h4>

<p>Back to the Revit API, here is a question that keeps cropping up again and again, prompting a summary of some basic aspects of interacting with Revit from a modeless form or external application, with an overview and pointers back to some of the previous posts and samples concerning this.

<p><strong>Question:</strong> How can I interact with Revit from a modeless form or external application?
I need to be able to switch back and forth between Revit and my form without closing it.

<p><strong>Answer:</strong> The Revit API is entirely designed to work only within pre-defined call-backs issued by Revit.

<p>Therefore, you can only keep your form open while continuing to work in Revit in either one of two ways:

<ul>
<li>Display your form from a Revit external command, but make it modeless. Then the command can complete and return control to Revit, while the form remains visible. As soon as the command has returned, though, you can no longer make use of the Revit API.
<li>Display your form from an external application, not from a Revit external command. In that case you obviously also have no access to the Revit API.
</ul>

<p>The reasons and more background information on the current situation are given in the discussions of

<a href="http://thebuildingcoder.typepad.com/blog/2010/04/asynchronous-api-calls-and-idling.html">
asynchronous API calls and idling</a>,

<a href="http://thebuildingcoder.typepad.com/blog/2011/01/modeless-door-lister-flaws.html">
modeless door lister flaws</a>, and

the further posts that they point to.

<p>As explained there, you cannot make use of the Revit API from an external application or a modeless context. 

<p>However, the API also provides the possibility to implement a workaround for this limitation, the Idling event.
It enables you to drive Revit from outside indirectly by posting an event to your Idling event handler, which has full access to the API and complete read and write access to the entire application object and all its documents.

<p>To use this, subscribe to the Revit Idling event and raise a signal from your external application or modeless dialogue. In the Idling event handler, check for the raised signal and execute whatever functionality you need. How this can be done is demonstrated and described in full detail in my sample to

<a href="http://thebuildingcoder.typepad.com/blog/2010/06/display-webcam-image-on-building-element-face.html">
display a live webcam image on a building element face</a>.

<p>Another example, where Revit is driven from a modeless dialogue box, e.g. from a context outside of any Revit API call-back, also using the Idling event to "get back in" to the Revit API context, is given by my

<a href="http://thebuildingcoder.typepad.com/blog/2010/07/modeless-loose-connectors.html">
modeless loose connector navigator</a> sample.

<p>The original version was written for Revit 2011. I recently 

<a href="http://thebuildingcoder.typepad.com/blog/2011/08/modeless-loose-connector-navigator-update.html">
updated and improved it for Revit 2012</a> as 

well.

<p>Additionally, a generic pattern for this process is described in 

<a href="http://thebuildingcoder.typepad.com/blog/2010/11/pattern-for-semi-asynchronous-idling-api-access.html">
pattern for semi asynchronous idling API access</a>.

<p>Unfortunately, like any other call-back, Idling costs time and should therefore obviously be used with great care.
Use it sparingly and cautiously.
