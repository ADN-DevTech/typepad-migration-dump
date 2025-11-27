---
layout: "post"
title: "My first Autodesk 360 viewer sample"
date: "2014-07-16 18:22:51"
author: "Kean Walmsley"
categories:
  - "Autodesk"
  - "Graphics system"
  - "HTML"
  - "JavaScript"
  - "Morgan"
  - "PaaS"
  - "REST"
  - "SaaS"
  - "Web/Tech"
original_url: "https://www.keanw.com/2014/07/my-first-autodesk-360-viewer-sample.html "
typepad_basename: "my-first-autodesk-360-viewer-sample"
typepad_status: "Publish"
---

<p>As <a href="http://through-the-interface.typepad.com/through_the_interface/2014/07/steampunking-a-morgan-3-wheeler-using-fusion-360.html" target="_blank">mentioned last week</a>, I’ve been having fun with Fusion 360 to prepare a model to be displayed in <a href="http://through-the-interface.typepad.com/through_the_interface/2014/05/a-sneak-peek-at-the-new-autodesk-360-viewer.html" target="_blank">the new Autodesk 360 viewer</a>. The sample is now ready to view, although I’m not yet quite ready to post the code directly here, mainly because the API isn’t yet publicly usable.</p>
<p>Here’s <a href="http://autode.sk/m3w" target="_blank">the app for you to take for a spin</a>, as it were.<a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201a511e2df3c970c-pi" target="_blank"><img alt="Steampunk Morgan Viewer" border="0" height="364" src="/assets/image_361342.jpg" style="background-image: none; padding-top: 0px; padding-left: 0px; margin: 20px 0px 0px; display: inline; padding-right: 0px; border: 0px;" title="Steampunk Morgan Viewer" width="470" /></a></p>
<p>The Autodesk 360 Viewing &amp; Data API is currently being piloted by a few key partners, and hopefully we’ll soon be broadening the scope to allow others to get involved (we first have to iron out any issues that might impact scalability, of course).</p>
<p>But let me give you a few pointers about what I did and what to expect when developing your own web applications that connect with this technology.</p>
<p>Firstly, as a classic set of web-services that require authentication, there’s a need to request and provide an authentication token. You do this by calling a particular web-service API with your client ID and secret: standard authentication stuff. But this means you’re going to need some kind of server-resident code to get a token: it’s a <span style="text-decoration: underline;"><em>really</em></span> bad idea to embed your client ID &amp; secret in client-side HTML or JavaScript.</p>
<p>To implement a server-side API, I’ve used <a href="http://nodejs.org" target="_blank">Node.js</a>. It’s the first time I’ve done so, but it was really easy to do (and I’d been meaning to give it a try).</p>
<p>For my hosting infrastructure, I went with <a href="https://www.heroku.com" target="_blank">Heroku</a> (thanks, <a href="http://twitter.com/camwest" target="_blank">Cameron</a>!). This is a lightweight, scalable hosting environment that has some really cool integrations: you host your code on <a href="https://github.com" target="_blank">GitHub</a> and deploy directly from there to your Heroku instance via the command-line. Integrating Node.js and <a href="http://newrelic.com" target="_blank">NewRelic</a> (for application monitoring) took just a few commands and minor code changes. It was really easy to do.</p>
<p>Heroku provides a basic level of usage per application for free – 750 CPU hours annually, I believe. I’m running this app right now on a single instance – it’s really only performing authentication and serving up static HTML, the heavy lifting is done via the code hosted on AWS that feeds data to the connected viewer – but I can scale this up according to usage. It will be interesting to see how the site gets used: NewRelic will provide me with that level of detail, I hope.</p>
<p>I found the Steampunk HTML UI for the sample on <a href="http://www.dmxzone.com/go/18220/an-image-viewer-with-the-dmxzone-universal-css-transforms-library/" target="_blank">this page</a>, making heavy use of <a href="http://en.wikipedia.org/wiki/Cascading_Style_Sheets" target="_blank">CSS</a> transforms. I contacted the author, <a href="http://twitter.com/danwellman" target="_blank">Dan Wellman</a>, who very generously provided the PSD file he’d used to generate the various web assets. Which meant I didn’t have to tweak the generated files, I could use PhotoShop directly to re-generate my own assets with the required changes. This was really nice of you, Dan – thanks for the kindness!</p>
<p>We’ll take a look at the code in more detail in a future post, as well as the steps to get the model accessible from the embedded viewer. (A quick outline: you need to upload the file – in my case a .F3D archive exported from Fusion 360 – and then fire off a translation request, all using an authorization token generated using your client data, allowing an app using the same client data to access it in future. All this can be done using <a href="http://en.wikipedia.org/wiki/CURL" target="_blank">cURL</a> at the command-line – if doing a one-off translation as I have done – or programmatically if you need something more dynamic.)</p>
<p>In terms of my use of the Viewer API: I had hoped to use component isolation to highlight different parts of the model, but as the original model was imported from Alias with a very flat component hierarchy (with <em><span style="text-decoration: underline;">tens of thousands</span></em> of bodies), there was basically no hope of me doing this without a large team of modelling specialists at my disposal. So I opted for swapping the view when the various buttons are clicked, instead. Which actually works reasonably well, so I’m happy sticking with with this approach, for now.</p>
