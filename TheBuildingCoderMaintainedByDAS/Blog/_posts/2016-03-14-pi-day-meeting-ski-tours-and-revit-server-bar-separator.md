---
layout: "post"
title: "Pi Day, Meeting, Ski Tour, Revit Server Bar Separator"
date: "2016-03-14 06:00:00"
author: "Jeremy Tammik"
categories:
  - "Data Access"
  - "Dynamo"
  - "Fun"
  - "Photo"
  - "Server"
  - "Travel"
original_url: "https://thebuildingcoder.typepad.com/blog/2016/03/pi-day-meeting-ski-tours-and-revit-server-bar-separator.html "
typepad_basename: "pi-day-meeting-ski-tours-and-revit-server-bar-separator"
typepad_status: "Publish"
---

<p>You may be surprised to hear that today is Pi Day &nbsp; :-) &ndash; as well as the birthday of my S.O.S. or significant other's son.</p>

<p>I had an exciting week with the ADN team meeting in London followed by a ski tour during the weekend.</p>

<p>To round this off, I'll also tuck in one little Revit API item here for you:</p>

<ul>
<li><a href="#2">Happy Pi Day and Dan's birthday</a></li>
<li><a href="#3">EMEA ADN team meeting in London</a></li>
<li><a href="#4">Ski tours in the Alvier group</a></li>
<li><a href="#5">Revit Server model path requires bar separator</a></li>
</ul>

<h4><a name="2"></a>Happy Pi Day and Dan's Birthday</h4>

<p>Today is a very special day, at least according to the weird anti-standard American way of writing dates &ndash;
cf. <a href="https://en.wikipedia.org/wiki/ISO_8601">ISO 8601</a> for the sensible alternative  :-)</p>

<p>March 14, 2016 is written as 3/14/16, with a notable similarity to the four-digit approximation to &pi;, 3.1416 &cong; 3.14159265358979...</p>

<p>This is pointed out by the <a href="http://dynamobim.org">Dynamo</a> article on using
the <a href="https://en.wikipedia.org/wiki/Buffon%27s_needle">Buffon’s needle algorithm</a>
to <a href="http://dynamobim.org/happy-%CF%80-day/?linkId=22264657">approximate &pi; in Revit</a>.</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b8d1af5de3970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b8d1af5de3970c image-full img-responsive" alt="Dynamo using Buffon's needle to approximate &pi;" title="Dynamo using Buffon's needle to approximate &pi;" src="/assets/image_5f6768.jpg" border="0" /></a><br /></p>

<p></center></p>

<p>It also happens to be Dan's sixteenth birthday:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c8251d6c970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c8251d6c970b img-responsive" style="width: 100px; " alt="Happy birthday, Dan!" title="Happy birthday, Dan!" src="/assets/image_2c2636.jpg" /></a><br /></p>

<p></center></p>

<h4><a name="3"></a>EMEA ADN Team Meeting in London</h4>

<p>Back to work, we held our European ADN Team Meeting in London last week,
with <a href="http://thebuildingcoder.typepad.com/blog/2016/03/trial-period-floating-license-entitlement-api-and-sketchup-grevit.html#4">many exciting topics to discuss</a>.</p>

<p>Lacking a free meeting room in the new Autodesk offices in Soho, and there being just four of us to find space for, we rented an apartment near Bow Street and Covent Garden for a couple of days:</p>

<p>Here in a <a href="https://www.flickr.com/photos/jeremytammik/albums/72157663346443223">photo album</a> of the pictures I took:</p>

<p><center>
<a data-flickr-embed="true"  href="https://www.flickr.com/photos/jeremytammik/albums/72157663346443223" title="European ADN Team Meeting"><img src="/assets/25349932560_c97148600b_n.jpg" width="320" height="240" alt="European ADN Team Meeting"></a><script async src="//embedr.flickr.com/assets/client-code.js" charset="utf-8"></script>
</center></p>

<p>I already mentioned two little side-effect details that came up,
a <a href="http://the3dwebcoder.typepad.com/blog/2016/03/team-meeting-and-deploy-to-heroku-button.html">deploy to Heroku button</a> for
any GitHub-hosted web app, and some ideas on
an <a href="http://the3dwebcoder.typepad.com/blog/2016/03/researching-an-optimal-api-documentation-workflow.html">optimal API documentation workflow</a> for
the future publication of
the <a href="http://forge.autodesk.com">Forge</a> API references and samples.</p>

<p>Expect more to come in both those areas.</p>

<h4><a name="4"></a>Ski Tours in the Alvier Group</h4>

<p>I returned from London to embark on the next in a whole series of ski tours, these ones in
the <a href="https://en.wikipedia.org/wiki/Alvier_(mountain)">Alvier</a> range
of mountains in the far east of Switzerland.</p>

<p>We were always in or at the upper limit of the fog and clouds, struggling to arrive above them and occasionally succeeding, e.g., on the summits:</p>

<p><center>
<a data-flickr-embed="true"  href="https://www.flickr.com/photos/jeremytammik/albums/72157665838861025" title="Ski Tours in the Alvier Group"><img src="/assets/25701816196_271738b3ab_n.jpg" width="320" height="240" alt="Ski Tours in the Alvier Group"></a><script async src="//embedr.flickr.com/assets/client-code.js" charset="utf-8"></script></center></p>

<h4><a name="5"></a>Revit Server Model Path Requires Bar Separator</h4>

<p>Finally, to return to the Revit API, here is the summary of a lengthy discussion of a question that was raised and solved by Ted Kovacs last week in
the <a href="http://forums.autodesk.com/t5/revit-api/bd-p/160">Revit API discussion forum</a> thread
on <a href="http://forums.autodesk.com/t5/revit-api/help-me-jeremy-tammik-or-anyone-else-familiar-with-rest-api/m-p/6053451">Revit Server model path</a>:</p>

<p><strong>Question:</strong> Using bits and pieces I found on the web along with some original coding, I put together a script that enumerates all of the projects on Revit Server using a REST API call to a Revit host server, and then automates running Autodesk's command line RevitServerTools utility to update an accelerator's cache.  I have been using it on Revit Server 2015 with great success.  However, the same script refuses to work on Revit Server 2016, and I can't figure out why.</p>

<p>I narrowed it down to the function below that handles the REST request, and found that it is not returning anything, just zero length string.</p>

<p>Does the 2016 URL need to be formatted differently?</p>

<p>Some other problem?</p>

<p>The 
<a href="http://thebuildingcoder.typepad.com/files/rsn_cache_update_and_model_export_script.txt">complete script</a> is attached if it helps or if anyone is interested.</p>

<p><strong>Answer:</strong> Have you looked at these examples on The Building Coder demonstrating various aspects of programmatically accessing Revit Server?</p>

<ul>
<li><a href="http://thebuildingcoder.typepad.com/blog/2013/08/revit-server-api-access-and-vbscript.html#3">VBScript Access to the Revit Server REST API</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2013/12/saving-a-new-central-file-to-revit-server.html">Saving a New Central File to Revit Server</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2014/01/rest-post-request-to-revit-server-2014.html">REST POST Request to Revit Server 2014</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2014/08/accessing-a-revit-server-central-model-path.html">Accessing a Revit Server Central Model Path</a></li>
</ul>

<p>The last one may very well have to do directly with your problem.</p>

<p>Another exploration of this is provided by Eric Stimmel's sample
of <a href="http://blog.ericstimmel.com/2012/05/08/revit-server-model-list">Revit Server model list using the REST API</a>.</p>

<p><strong>Answer from the development team:</strong> I took a quick look at this.
I couldn’t get it to work even on 2015, so I think there must be some small syntactical difference between the URL listed in the email and the actual URL he uses (e.g. why the apparent space preceding <code>/Content</code>?).
Could you have him just send the actual URL string he’s using that’s successful in 2015 and not successful in 2016?</p>

<p><strong>Response:</strong> For 2015, the working URL is:</p>

<pre>
http://RevitHost/RevitServerAdminRESTService2015/A​dminRESTService.svc/ /Contents
</pre>

<p>This intentionally includes a space.</p>

<p>For 2016, the (not working) URL is the same, only the "2015" part is changed to "2016".</p>

<p>The URL above is used for getting the contents of the root of the Revit server.  Your developer asked "why the apparent space preceding <code>/Content</code>".  The space is what you use when you need the contents of the root rather than the contents of a folder, or so I understand.  Here is the foundation for that belief.</p>

<ol>
<li>A space is used in the example that you point to above.</li>
<li>I found other references on the web suggesting a space was needed in order to get the contents from the root.</li>
<li>When I used a space in my code to get the root contents on a 2015 server, it works, and I cannot get it to work without the space.</li>
</ol>

<p>This is not the only URL I use.  After I get the contents of the root, I recursively get the contents of each and every folder and subfolder in order to enumerate all the models contained in every folder on the Revit server.  That second URL <em>does</em> in fact still work on 2016. It is the same URL as above, only the single space before "/Contents" is replaced with the name of a specific folder.  For a 2016 server, the URL would be something like</p>

<pre>
http://RevitHost/RevitServerAdminRESTService2016/A​dminRESTService.svc/MyFolder/Contents
</pre>

<p>With this in mind, I would restate my question as "What URL are you supposed to use to get the contents of the root of a 2016 Revit Server?".
I need the contents of the root in order to know what folder names to use the second URL on, as these folders are added, removed, and renamed over time so I cannot hard code them.</p>

<p><strong>Answer:</strong> That is decidedly weird.</p>

<p>Please try it without the space, preferably by simply deleting it, or, if need be, by replacing it with an underscore or something.</p>

<p>I am not aware of any of the conventions you refer to, and I am aware of heaps of problems caused by spaces in file paths.</p>

<p>The fact that it works with a space in 2015 is strange, and it could very well be the reason it fails in 2016.</p>

<p><strong>Response:</strong> I found the solution.</p>

<p>You were on the right track though, even though that didn't ultimately help me find the solution any sooner.</p>

<p>While having a space in the URL worked fine in 2015 and not on 2016, replacing the space with a pike '|' symbol works on both versions.</p>

<p>I found the answer within the Revit 2016 SDK, which includes a Revit Viewer example written in C++.</p>

<p>This is the working 2016 URL to get the contents of the root</p>

<pre>
http://RevitHost/RevitServerAdminRESTService2016​/​A​dminRESTService.svc/|/contents
</pre>

<p><strong>Answer:</strong> Congratulations on solving the problem!</p>

<p>Thank you for confirming.</p>

<p>Actually, I now see that this is also documented in the Revit Server REST API documentation in the PDF file named 'Revit Server REST API Reference.pdf' in the Revit SDK 'Revit Server SDK' subfolder.</p>

<p>It clearly states:</p>

<blockquote>
  <p>Since slashes '\' and backslashes '/' are URL special characters, object paths are formatted as:</p>
</blockquote>

<pre>
Server root: '|'
Folder Path: 'folderName1[|folderName2[...]]'
Model Path: '[folderName1[|folderName2[...]|]]modelName.rvt'
</pre>
