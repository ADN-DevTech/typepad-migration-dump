---
layout: "post"
title: "Getting to know the Project Draw API"
date: "2008-06-30 07:12:00"
author: "Kean Walmsley"
categories:
  - "AJAX"
  - "HTML"
  - "SaaS"
  - "Web/Tech"
  - "XML"
original_url: "https://www.keanw.com/2008/06/getting-to-know.html "
typepad_basename: "getting-to-know"
typepad_status: "Publish"
---

<p>Last week David Falck, over at the Autodesk Labs team, provided me with an API key to have a play with the <a href="http://labs.blogs.com/its_alive_in_the_lab/2008/06/looking-for-web.html">API to Project Draw</a>. This post documents what I did, and how it all worked.</p>

<p>Note: I'm not actually doing any coding whatsoever in this post: I used the handy <a href="http://draw.labs.autodesk.com/ADDraw/apitester.html">API test page</a> provided by the Autodesk Labs team, and so didn't need to write a single line of code.</p>

<p>So, first of all, you'll need an API key. Here's the one I got from the Draw team:</p>

<p><span style="font-size: 0.8em;">th1SIsnOTreAlLyMyapIkeyOfCourSE</span></p>

<p>Just kidding. I've changed the above key for the purpose of this post: if you want a key, get your own. :-P</p>

<p>Seriously, though: just email <a href="mailto:labs.draw@autodesk.com"><span style="color: #0066cc;">labs.draw@autodesk.com</span></a>, and they will generate and send you your very own key.</p>

<p>OK, once you have a key, you can navigate to the test page and start playing around. I started by copying my key (the real one, of course) into the startSession test rig, and I then hit <em>Initiate</em>:</p>

<p><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/Project%20Draw%20API%20-%20startSession.png"><img height="236" alt="Project Draw API - startSession" src="/assets/Project%20Draw%20API%20-%20startSession_thumb.png" width="463" border="0" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" /></a></p>

<p>The Project Draw API is exposed via <a href="http://en.wikipedia.org/wiki/Representational_State_Transfer">REST</a>, as opposed to <a href="http://en.wikipedia.org/wiki/SOAP">SOAP</a> (another popular - if more heavyweight, from a client perspective - mechanism for exposing Web Services), which means you pass everything the service needs to identify the session etc., in each call made to Project Draw. So you will need to use your API key along with the session ID provided in the response received for the API call we just made for each further use of the Project Draw API.</p>

<p>The other way to start a Project Draw session is via the setXML web method, but we'll come to that later. For now we don't have any XML to provide, so we're going to open the blank session and create some geometry from scratch.</p>

<p>Copy the URL provided in the response to your clipboard and paste it into a new browser window. You should get a nice, new Project Draw page upon which to draw some geometry:</p>

<p><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/Project%20Draw%20-%20new%20session.png"><img height="352" alt="Project Draw - new session" src="/assets/Project%20Draw%20-%20new%20session_thumb.png" width="456" border="0" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" /></a> </p>

<p>I drew some simple geometry using the Project Draw interface and saved it using <em>File</em> -&gt; <em>Save</em>. This is an important step: if you don't save to the server, then clearly any calls to access the session's geometry via the Web Service will not retrieve anything very interesting.</p>

<p>Here's my model - please note that I just threw some filled objects onto the canvas, and had no intention of drawing a symbol with religious or nationalistic overtones (there's something vaguely <a href="http://en.wikipedia.org/wiki/Freemasonry">masonic</a> about the image, I have to admit, but it's nothing I drew consciously, honestly... which should be obvious from the colours I used, if nothing else :-)</p>

<p><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/Project%20Draw%20-%20session%20with%20geometry.png"><img height="352" alt="Project Draw - session with geometry" src="/assets/Project%20Draw%20-%20session%20with%20geometry_thumb.png" width="456" border="0" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" /></a> </p>

<p>Once we've saved our geometry, we can head back to the API test page and see what we can do with it.</p>

<p>Firstly, we can retrieve the XML representation of this geometry, by calling the getXML web method:</p>

<p><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/Project%20Draw%20API%20-%20getXML.png"><img height="328" alt="Project Draw API - getXML" src="/assets/Project%20Draw%20API%20-%20getXML_thumb.png" width="457" border="0" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" /></a> </p>

<p>You can copy &amp; paste the returned XML fragment into a text file, if you wish. The XML returned for my feeble artistic efforts can be accessed <a href="http://through-the-interface.typepad.com/through_the_interface/files/MyDrawSession.xml">here</a>.</p>

<p>Secondly, we can retrieve a rasterized view of our design, in PNG format:</p>

<p><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/Project%20Draw%20API%20-%20getRaster.png"><img height="321" alt="Project Draw API - getRaster" src="/assets/Project%20Draw%20API%20-%20getRaster_thumb.png" width="457" border="0" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" /></a> </p>

<p>This provides us with a nice, high-quality raster of our design:</p>

<p><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/Project%20Draw%20-%20rasterized%20geometry.png"><img height="302" alt="Project Draw - rasterized geometry" src="/assets/Project%20Draw%20-%20rasterized%20geometry_thumb.png" width="420" border="0" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" /></a> </p>

<p>Now that we've extracted our XML content and generated a pretty image from it, we can (and indeed <strong>must</strong>) delete the session:</p>

<p><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/Project%20Draw%20API%20-%20deleteSession.png"><img height="221" alt="Project Draw API - deleteSession" src="/assets/Project%20Draw%20API%20-%20deleteSession_thumb.png" width="452" border="0" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" /></a> </p>

<p>Now that we have some XML content - which we could have generated/transformed/manipulated programmatically in some way - we can use it to create a new session, via the setXML web method:</p>

<p><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/Project%20Draw%20-%20setXML.png"><img height="394" alt="Project Draw - setXML" src="/assets/Project%20Draw%20-%20setXML_thumb.png" width="452" border="0" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" /></a> </p>

<p>That's it for today's quick tour of the Project Draw API. As you should be able to see from this procedure - and from the XML representation of the design I created - it's altogether possible to populate and manipulate content in the Project Draw service programmatically, and - as the API is simply HTTP - you're able to do so from the programming language of your choice (something we did not touch on at all today).</p>

<p>A final, quick note: if there's something more you'd like to see exposed in the Project Draw API, don't be shy - <a href="mailto:labs.draw@autodesk.com">let us know</a>. The Autodesk Labs team is really keen to get external feedback on this technology.</p>
