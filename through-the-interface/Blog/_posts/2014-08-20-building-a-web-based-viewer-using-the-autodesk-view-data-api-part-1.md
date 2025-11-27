---
layout: "post"
title: "Building a web-based viewer using the Autodesk View &amp; Data API &ndash; Part 1"
date: "2014-08-20 18:37:56"
author: "Kean Walmsley"
categories:
  - "Autodesk"
  - "PaaS"
  - "SaaS"
  - "Web/Tech"
original_url: "https://www.keanw.com/2014/08/building-a-web-based-viewer-using-the-autodesk-view-data-api-part-1.html "
typepad_basename: "building-a-web-based-viewer-using-the-autodesk-view-data-api-part-1"
typepad_status: "Publish"
---

<p>Over the next few posts we’re going to take a look at the steps needed to build the <a href="http://autode.sk/m3w" target="_blank">Steampunk Morgan Viewer</a>, <a href="http://through-the-interface.typepad.com/through_the_interface/2014/07/my-first-autodesk-360-viewer-sample.html" target="_blank">my first sample</a> using Autodesk’s new View &amp; Data API. In today’s post we’re going to look at the steps needed to host content to be served up to instances of the viewer. In a subsequent post we’ll look at the client-side implementation, connecting to and streaming down content and controlling how it gets viewed.</p>
<p>So let’s start with some basics. The first thing you need to do when working with the new View &amp; Data API is to get your <strong>access key</strong>. You get that from <a href="http://developer.autodesk.com" target="_blank">the new developer portal</a>:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201a73e055033970d-pi" target="_blank"><img alt="developer.autodesk.com" border="0" height="327" src="/assets/image_345466.jpg" style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin: 20px auto; display: block; padding-right: 0px; border: 0px;" title="developer.autodesk.com" width="430" /></a></p>
<p>Clicking on the middle button will allow you to enter the information Autodesk needs to generate a key for you:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201a511f9f7e4970c-pi" target="_blank"><img alt="Requesting your access key" border="0" height="327" src="/assets/image_34509.jpg" style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin: 20px auto; display: block; padding-right: 0px; border: 0px;" title="Requesting your access key" width="430" /></a></p>
<p>Once approved, you’ll be able to access your consumer key and secret via the developer portal. This is information your application will need to pass to the View &amp; Data API when it needs to use it.</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201a511f9f7e6970c-pi" target="_blank"><img alt="Your access key" border="0" height="327" src="/assets/image_824496.jpg" style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin: 20px auto; display: block; padding-right: 0px; border: 0px;" title="Your access key" width="430" /></a></p>
<p>(The above key and secret are several characters shorter than yours will be: I’ve gone and cut a chunk out of the middle of each: please feel free to apply for your own. :-)</p>
<p>We’ll talk more about this topic in the next post, but one thing that’s worth pointing out, right away, is that the key and secret should <strong>not</strong> be embedded as part of client-resident modules (local JavaScript files, DLLs, etc.). Best practice is to implement your own web-service that calls into Autodesk’s API to get an authorization code and return it to the caller to be used in subsequent calls into Autodesk’s API. But again – we’ll see this specifically in the next post.</p>
<p>We now have the access information we need to use the View &amp; Data API. Content uploaded to Autodesk storage is only accessible by an application using the same access information that was used to upload it, so we need to use our key and secret to upload our content.</p>
<p>But let’s take a quick step back. Where does this content come from? In my case <a href="http://through-the-interface.typepad.com/through_the_interface/2014/07/steampunking-a-morgan-3-wheeler-using-fusion-360.html" target="_blank">I used Fusion 360 to prepare the content for my application</a>, but you can upload content in any of about 70 or so <strong>file formats:</strong></p>
<p>ipt, neu, stla, stl, xlsx, jt, jpg, skp, prt, dwf, xls, png, sldasm, step, dwg, zip, nwc, model, sim, stp, ste, f3d, pdf, iges, dwt, catproduct, csv, igs, sldprt, cgr, 3dm, sab, obj, pptx, cam360, jpeg, bmp, exp, ppt, doc, wire, ige, rcp, txt, dae, x_b, 3ds, rtf, rvt, g, sim360, iam, asm, dlv3, x_t, pps, session, xas, xpr, docx, catpart, stlb, tiff, nwd, sat, fbx, smb, smt, ifc, dwfx, tif.</p>
<p>We’re going to want to upload our Fusion 360 export (an .F3D file) for it to be viewed in our application. Our application only wants to view a single model, which makes things easier. We’re going to use a command-line HTTP tool called <a href="http://curl.haxx.se" target="_blank">cURL</a> to make calls into the web-service we need to post content, performing this as a one-time process. If you have a more dynamic use case, then this process could very easily be replicated in code.</p>
<p>The steps we’re going to follow are all posted <a href="http://developer.api.autodesk.com/documentation/v1/vs_quick_start.html" target="_blank">here</a>, but anyway: it’s better to go through it twice than not at all. :-)</p>
<p>&#0160;</p>
<p><strong><span style="font-size: medium;">Getting an access token</span></strong></p>
<p>To perform transactions with the View &amp; Data API, we need to pass a valid access token. We get one based on the customer key (<span style="font-family: &#39;Courier New&#39;;">client_id</span>) and the secret (<span style="font-family: &#39;Courier New&#39;;">client_secret</span>). Access tokens typically only last 30 minutes, so you may well need multiple tokens during a session. Which is fine: they are transient, in any case, and the main thing is the key and secret they were based on is the same.</p>
<div style="font-size: 8pt; font-family: courier new; background: white; color: black; line-height: 140%;">
<p style="margin: 0px;">curl --data &quot;client_id=K8whhq86fnoYqw4GXAW0ID1hH&amp;client_secret=DC2cBoXIy8&amp;grant_type=client_credentials&quot; https://developer.api.autodesk.com/authentication/v1/authenticate --header &quot;Content-Type: application/x-www-form-urlencoded&quot; – -k</p>
</div>
<p>[You may need to copy and paste this into a text editor to see the whole thing, as my blog’s format doesn’t like the long first line.]</p>
<p>You should get a JSON fragment back containing the access token:</p>
<div style="font-size: 8pt; font-family: courier new; background: white; color: black; line-height: 140%;">
<p style="margin: 0px;">{&quot;token_type&quot;:&quot;Bearer&quot;,&quot;expires_in&quot;:1799,&quot;access_token&quot;:&quot;9RVLOhMtO29QoTkgPkZ6KBN3ywkx&quot;}</p>
</div>
<p>Now that we have the token we can use to make API calls, there are a number of activities to perform. One optional activity is to <a href="http://developer.api.autodesk.com/documentation/v1/viewing_service.html#viewing-service-supported-api" target="_blank">check the valid file formats</a>, but we’re going to skip that and assume the file we want to upload is in an accepted format.</p>
<p>In terms of the steps to perform, we need to: create a container for our data (also called a bucket), upload a file into that container and then register it with the viewing service.</p>
<p>&#0160;</p>
<p><span style="font-size: medium;"><strong>Creating a bucket</strong></span></p>
<p>Buckets can be transient (24 hour), temporary (30 day) or persistent in nature. The type you create will depend on your use case. For our application we want a persistent bucket, which means the data it contains will stay around as long as the service exists (although if it doesn’t get accessed for 2 or more years then it might get archived).</p>
<p>Bucket names need to be unique across the system – as they form the root of the <a href="http://en.wikipedia.org/wiki/Uniform_resource_name" target="_blank">URN</a> that will be used to identify content – so you won’t be able to call yours “steambuck”:</p>
<div style="font-size: 8pt; font-family: courier new; background: white; color: black; line-height: 140%;">
<p style="margin: 0px;">curl -k --header &quot;Content-Type: application/json&quot; --header &quot;Authorization: Bearer 9RVLOhMtO29QoTkgPkZ6KBN3ywkx&quot; --data &#39;{&quot;bucketKey&quot;:&quot;steambuck&quot;,&quot;policy&quot;:&quot;persistent&quot;}&#39; https://developer.api.autodesk.com/oss/v1/buckets</p>
</div>
<p>In response we receive this confirmation:</p>
<div style="font-size: 8pt; font-family: courier new; background: white; color: black; line-height: 140%;">
<p style="margin: 0px;">{&quot;key&quot;:&quot;steambuck&quot;,&quot;owner&quot;:&quot;K8whhq86fnoYqw4GXAW0ID1hH&quot;,</p>
<p style="margin: 0px;">&quot;createDate&quot;:1403681174529,&quot;permissions&quot;:[{&quot;serviceId&quot;:&quot;K8whhq86fnoYqw4GXAW0ID1hH&quot;,&quot;access&quot;:&quot;full&quot;}],</p>
<p style="margin: 0px;">&quot;policyKey&quot;:&quot;persistent&quot;}</p>
</div>
<p>&#0160;</p>
<p><span style="font-size: medium;"><strong>Uploading our file</strong></span></p>
<p>Once we have a container for it, we can upload our data. The “tricky” bit here is to find out the size of the file we want to upload. I either use the <span style="font-family: &#39;Courier New&#39;;">dir</span> command (if on Windows) or <span style="font-family: &#39;Courier New&#39;;">ls –l</span> (if on OS X) for this. You will also need to provide the full file path or be in the folder containing it, of course. (The file I uploaded is named <em>SpM3W7.f3d</em>, as it’s the 7th version of the Steampunk Morgan 3 Wheeler, in case you’re wondering. :-)</p>
<div style="font-size: 8pt; font-family: courier new; background: white; color: black; line-height: 140%;">
<p style="margin: 0px;">curl --header &quot;Authorization: Bearer 9RVLOhMtO29QoTkgPkZ6KBN3ywkx&quot; --header &quot;Content-Length: 82984198&quot; -H &quot;Content-Type:application/octet-stream&quot; --header &quot;Expect:&quot; --upload-file &quot;SpM3W7.f3d&quot; -X PUT https://developer.api.autodesk.com/oss/v1/buckets/steambuck/objects/SpM3W7.f3d -k</p>
</div>
<p>We get back a confirmation of the upload which includes the URN and the <a href="http://en.wikipedia.org/wiki/SHA-1" target="_blank">hash</a> of the file.</p>
<div style="font-size: 8pt; font-family: courier new; background: white; color: black; line-height: 140%;">
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160; &quot;bucket-key&quot; : &quot;steambuck&quot;,</p>
<p style="margin: 0px;">&#0160; &quot;objects&quot; : [ {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &quot;location&quot; : &quot;https://developer.api.autodesk.com/oss/v1/buckets/steambuck/objects/SpM3W7.f3d&quot;,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &quot;size&quot; : 82984198,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &quot;key&quot; : &quot;SpM3W7.f3d&quot;,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &quot;id&quot; : &quot;urn:adsk.objects:os.object:steambuck/SpM3W.f3d&quot;,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &quot;sha-1&quot; : &quot;724a4a7353132bf803cb907248043ed5873f2c01&quot;,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &quot;content-type&quot; : &quot;application/octet-stream&quot;</p>
<p style="margin: 0px;">&#0160; } ]</p>
<p style="margin: 0px;">}</p>
</div>
<p>&#0160;</p>
<p><span style="font-size: medium;"><strong>Registering our file with the viewing service</strong></span></p>
<p>Now that our file is uploaded, we can register it with the viewing service. This basically asks that the file gets translated into the internal format that will get streamed down to the WebGL viewer.</p>
<p>The inputs for this part of the process are the access token (of course) and the URN of the file we’ve uploaded. The URN needs to be <a href="http://en.wikipedia.org/wiki/Base64" target="_blank">Base64</a> encoded: there’s a handy website that allows you to <a href="http://www.base64encode.org" target="_blank">encode</a> and <a href="http://www.base64decode.org" target="_blank">decode</a> Base64.</p>
<p>[If you take the below string, for instance - <span style="font-family: &#39;Courier New&#39;; font-size: xx-small;">dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6c3RlYW1idWNrL1NwTTNXNy5mM2Q=</span> – you’ll find that decoding it returns the string “<span style="font-family: &#39;Courier New&#39;; font-size: xx-small;">urn:adsk.objects:os.object:steambuck/SpM3W7.f3d</span>”… yes, the “urn:” is embedded, so do watch out for that.]</p>
<div style="font-size: 8pt; font-family: courier new; background: white; color: black; line-height: 140%;">
<p style="margin: 0px;">curl -k -H &quot;Content-Type: application/json&quot; -H &quot;Authorization:Bearer 9RVLOhMtO29QoTkgPkZ6KBN3ywkx&quot; -i -d &#39;{&quot;urn&quot;:&quot;dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6c3RlYW1idWNrL1NwTTNXNy5mM2Q=&quot;}&#39; https://developer.api.autodesk.com/viewingservice/v1/register</p>
</div>
<p>The response you get back just indicates the translation has been requested:</p>
<div style="font-size: 8pt; font-family: courier new; background: white; color: black; line-height: 140%;">
<p style="margin: 0px;">HTTP/1.1 200 OK</p>
<p style="margin: 0px;">Access-Control-Allow-Credentials: true</p>
<p style="margin: 0px;">Access-Control-Allow-Origin: *</p>
<p style="margin: 0px;">Content-Type: application/json; charset=utf-8</p>
<p style="margin: 0px;">x-ads-app-identifier: platform-viewing-1.6.2.788.069ca82-production</p>
<p style="margin: 0px;">x-ads-duration: 390 ms</p>
<p style="margin: 0px;">x-ads-error-id:</p>
<p style="margin: 0px;">x-ads-startup-time: Wed Jun 25 06:28:52 UTC 2014</p>
<p style="margin: 0px;">Content-Length: 20</p>
<p style="margin: 0px;">Connection: keep-alive</p>
<p style="margin: 0px;">{&quot;Result&quot;:&quot;Success&quot;}</p>
</div>
<p>Which is fine, but we will also need to check the status of a particular translation job. We can do this using this call:</p>
<div style="font-size: 8pt; font-family: courier new; background: white; color: black; line-height: 140%;">
<p style="margin: 0px;">curl -k -i -H &quot;Authorization: Bearer 9RVLOhMtO29QoTkgPkZ6KBN3ywkx&quot; -X GET https://developer.api.autodesk.com/viewingservice/v1/dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6c3RlYW1idWNrL1NwTTNXNy5mM2Q=</p>
</div>
<p>This returns the status of the current job in a rather long-winded piece of JSON, the important pieces of which are the “<span style="font-family: &#39;Courier New&#39;;">progress</span>” and “<span style="font-family: &#39;Courier New&#39;;">success</span>” entries.</p>
<p>That’s the basic process for getting content hosted for use in the viewer. There’s <a href="http://developer.api.autodesk.com/documentation/v1/vs_quick_start.html#step-5-set-up-references-between-multiple-files" target="_blank">an additional step</a> needed for maintaining references between files, but having a self-contained model meant we didn’t need to worry about that, at least.</p>
<p>In the next post we’ll look at hooking this hosted content into our client-side viewer.</p>
