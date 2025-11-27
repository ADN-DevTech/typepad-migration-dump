---
layout: "post"
title: "Implementing MapReduce to de-skew large images in parallel using Google App Engine"
date: "2013-10-04 11:47:44"
author: "Kean Walmsley"
categories:
  - "Concurrent programming"
  - "Geometry"
  - "HTML"
  - "PaaS"
  - "Python"
  - "SaaS"
original_url: "https://www.keanw.com/2013/10/implementing-mapreduce-to-de-skew-large-images-in-parallel-using-google-app-engine.html "
typepad_basename: "implementing-mapreduce-to-de-skew-large-images-in-parallel-using-google-app-engine"
typepad_status: "Publish"
---

<p>I’ve been looking for an interesting problem to solve using <a href="http://en.wikipedia.org/wiki/MapReduce" target="_blank">MapReduce</a> for some time now. I’ve been curious about the paradigm and how it can be applied to churn through large sets of data across multiple processing cores: something that’s especially relevant as we need to distribute processing – whether to cores that are local or up in the cloud – in order to improve software performance.</p>
<p>I talked about much of this when <a href="http://through-the-interface.typepad.com/through_the_interface/2008/01/harnessing-f-as.html" target="_blank">I looked at F#’s Asynchronous Workflows</a> way back when (nearly 6 years ago – ouch).</p>
<p>MapReduce frameworks – and there are a number out there: in this case we’re using <a href="http://gaepythondoc.appspot.com/dataprocessing" target="_blank">Google’s “experimental” version</a>, but Amazon also hosts <a href="http://en.wikipedia.org/wiki/Apache_Hadoop" target="_blank">the Hadoop framework</a> to run <a href="http://aws.amazon.com/elasticmapreduce/" target="_blank">Elastic MapReduce</a> on <a href="http://aws.amazon.com/ec2" target="_blank">EC2</a> and <a href="http://aws.amazon.com/s3" target="_blank">S3</a> – work on the principle of chunking big data-processing tasks into lots of tiny ones that can be run independently (the “map” piece) and have the results combined at the end (the “reduce” piece).</p>
<p>The <a href="http://through-the-interface.typepad.com/through_the_interface/2013/08/applications-for-linear-algebra-straightening-out-perspective.html" target="_blank">series</a> I’ve <a href="http://through-the-interface.typepad.com/through_the_interface/2013/08/update-on-the-project-to-de-skew-images-inside-autocad.html" target="_blank">been</a> <a href="http://through-the-interface.typepad.com/through_the_interface/2013/08/generating-png-files-for-de-skewed-portions-of-perspective-images.html" target="_blank">working</a> on to <a href="http://through-the-interface.typepad.com/through_the_interface/2013/09/moving-pure-python-code-into-ironpython.html" target="_blank">to</a> <a href="http://through-the-interface.typepad.com/through_the_interface/2013/09/executing-python-code-to-de-skew-images-inside-autocad-using-ironpython.html" target="_blank">de-skew</a> <a href="http://through-the-interface.typepad.com/through_the_interface/2013/09/selecting-an-area-of-a-perspective-image-to-de-skew-inside-autocad-using-html5-and-javascript.html" target="_blank">perspective</a>&#0160;<a href="http://through-the-interface.typepad.com/through_the_interface/2013/09/creating-a-web-service-to-de-skew-images-using-google-app-engine.html" target="_blank">photos</a> and bring the results into AutoCAD seemed a reasonable opportunity to investigate this technology. The nature of this particular task is such that we can transform parts of the image in parallel and then generate the output pixels in parallel, too.</p>
<p>With Google App Engine’s MapReduce, your map() function takes some data input (and in GAE there are various input readers, whether taking lines of text from a file, files from a .zip or entities from a data-store), works out the results and returns a key-value pair. The key helps identify like results: all values with a certain key associated will be grouped together during an interim shuffle stage (it’s really MapShuffleReduce, but then the shuffling happens invisibly and so doesn’t get a specific mention ;-) and then your reduce() function is called with a key and a list of the values that were generated for that key.</p>
<p>Looking specifically at our example of de-skewing images: in this case we’re chaining together a couple of MapReduce pipelines. The first takes rows of pixels in the input image and transforms them to the target coordinate system, and the second works through and generates the rows of pixels for the output image that will then get “reduced” into a resultant PNG.</p>
<p>We start by focusing on a subset of the original image to work with:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2019affc4656c970c-pi" target="_blank"><img alt="We only care about a certain area of the source image" border="0" height="271" src="/assets/image_96994.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="We only care about a certain area of the source image" width="450" /></a></p>
<p>The “transform” pipeline then takes this – with a separate “task” for each row in the area of the input image that we care about – and transforms the pixel information into our target coordinate system (where the skewed area is rectangular):</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2019affc446e6970b-pi" target="_blank"><img alt="Transform pipeline" border="0" height="267" src="/assets/image_649788.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="Transform pipeline" width="410" /></a></p>
<p>The “output” pipeline uses a “task” to generate each row of the output image:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2019affc446f4970b-pi" target="_blank"><img alt="Output pipeline" border="0" height="230" src="/assets/image_350017.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="Output pipeline" width="470" /></a></p>
<p>The results of the first pipeline need to be in place before we can start the second – as the second is going to query them when creating its own results – but I could even imagine a scenario where there’s some overlap managed between the two. It might be possible to delay certain operations in the second until the required data is in place, for example, but it would certainly take more effort to coordinate.</p>
<p>The original code has already been adjusted to implement a crude caching mechanism for each task in the output pipeline: when they need a row of input data they haven’t already asked for (which will happen a lot: as you can see the input rows on the left a very skewed compared with the output pixels we care about) this will be read in on-demand. This saves from us reading in the whole image for each instance of the mapper – or sharing memory across multiple mappers, which would reduce flexibility if it’s even possible – and different output rows will need a different combination of input rows, in any case. This caching mechanism could probably be extended to wait for rows that are not yet ready, but – again – more coordination work would be needed.</p>
<p>And on the subject of coordination… splitting these tasks up, marshalling the data around, shuffling the results and reducing them into something coherent… all this takes resources. There has to be sufficient benefit in doing this for it to be worth the overhead, so for small images I’d imagine keeping a linear code-path and then only using this technique where the benefits justify the resource investment (i.e. with images that the linear implementation can’t easily cope with). I haven’t done much analysis on where this line is, but it has to be there somewhere.</p>
<p>Before showing you the code, I should probably mention that I’ve almost certainly done things that are sub-optimal. GAE has various ways of storing and passing data: I use <a href="https://developers.google.com/appengine/docs/python/ndb" target="_blank">the NDB Datastore</a> to create the input records that defines the tasks for the two pipelines – using the DatastoreInputReader to query them – but then I use <a href="https://developers.google.com/appengine/docs/python/memcache" target="_blank">memcache</a> to store the results of each of the map() functions (as the results otherwise get serialized via JSON for the shuffle and reduce phase) which I then pick up later on when they’re needed. The standard BlobstoreOutputWriter is used to generate our PNG in <a href="https://developers.google.com/appengine/docs/python/blobstore" target="_blank">the Blobstore</a>.</p>
<p>These decisions were mostly made through an unfortunate combination of expediency and ignorance: it took me quite some time to get this far and I know the implementation is far from perfect. In fact, for larger images some of the datastore input records are simply not being found – which seems to be better when I reduce the batch size for the input reader, but still isn’t 100% – but the principles of how the task has been split up are sound, even if the configuration of the processing mechanism could probably benefit from further tweaking.</p>
<p>So this is very much a work in progress (even though it remains to be seen whether I’ll spend much more time on it than I have thus far – I think I’ve learned as much as I need to about GAE’s implementation of MapReduce, for now).</p>
<p>In terms of how the code works currently: it seems to work tolerably well, but it’s not quick. But then I haven’t thrown much by way of the cloud’s resources at it: it’d be interesting to see whether assigning lots more “shards” to process the mapping tasks for each pipeline would lead to things happening more quickly – or even having the work done via more powerful back-end instances, as alluded to last time. I’ve chosen not to spend time looking at that, as I’ve so far been more interested in the theoretical problem of implementing MapReduce, rather than the practical one of having it work well enough to actually use. :-)</p>
<p>Anyway, here’s the Python code, for people to take a look at:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">import</span><span style="line-height: 140%;"> os</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">import</span><span style="line-height: 140%;"> urllib</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">import</span><span style="line-height: 140%;"> webapp2</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">import</span><span style="line-height: 140%;"> cgi</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">import</span><span style="line-height: 140%;"> logging</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">import</span><span style="line-height: 140%;"> pickle</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">import</span><span style="line-height: 140%;"> time</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">from</span><span style="line-height: 140%;"> deskew </span><span style="line-height: 140%; color: blue;">import</span><span style="line-height: 140%;"> *</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">from</span><span style="line-height: 140%;"> image </span><span style="line-height: 140%; color: blue;">import</span><span style="line-height: 140%;"> image2writer</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">from</span><span style="line-height: 140%;"> io </span><span style="line-height: 140%; color: blue;">import</span><span style="line-height: 140%;"> BytesIO</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">from</span><span style="line-height: 140%;"> google</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">appengine</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">ext </span><span style="line-height: 140%; color: blue;">import</span><span style="line-height: 140%;"> blobstore</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">from</span><span style="line-height: 140%;"> google</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">appengine</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">ext</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">webapp </span><span style="line-height: 140%; color: blue;">import</span><span style="line-height: 140%;"> blobstore_handlers</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">from</span><span style="line-height: 140%;"> google</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">appengine</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">ext </span><span style="line-height: 140%; color: blue;">import</span><span style="line-height: 140%;"> ndb</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">from</span><span style="line-height: 140%;"> google</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">appengine</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">api </span><span style="line-height: 140%; color: blue;">import</span><span style="line-height: 140%;"> memcache</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">from</span><span style="line-height: 140%;"> mapreduce </span><span style="line-height: 140%; color: blue;">import</span><span style="line-height: 140%;"> base_handler</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">from</span><span style="line-height: 140%;"> mapreduce </span><span style="line-height: 140%; color: blue;">import</span><span style="line-height: 140%;"> mapreduce_pipeline</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">from</span><span style="line-height: 140%;"> mapreduce </span><span style="line-height: 140%; color: blue;">import</span><span style="line-height: 140%;"> operation </span><span style="line-height: 140%; color: blue;">as</span><span style="line-height: 140%;"> op</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">from</span><span style="line-height: 140%;"> mapreduce </span><span style="line-height: 140%; color: blue;">import</span><span style="line-height: 140%;"> shuffler</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">from</span><span style="line-height: 140%;"> mapreduce </span><span style="line-height: 140%; color: blue;">import</span><span style="line-height: 140%;"> context</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">from</span><span style="line-height: 140%;"> mapreduce </span><span style="line-height: 140%; color: blue;">import</span><span style="line-height: 140%;"> model</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">class</span><span style="line-height: 140%;"> MainHandler(webapp2</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">RequestHandler):</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">def</span><span style="line-height: 140%;"> get(self):</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; upload_url = blobstore</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">create_upload_url(</span><span style="line-height: 140%; color: #a31515;">&#39;/upload&#39;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; sro = self</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">response</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">out</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; sro</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">write(</span><span style="line-height: 140%; color: #a31515;">&#39;&lt;html&gt;&lt;body&gt;&#39;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; sro</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">write(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&#39;&lt;form action=&quot;%s&quot; method=&quot;POST&quot; enctype=&quot;multipart/form-data&quot;&gt;&#39;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; % upload_url)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; sro</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">write(</span><span style="line-height: 140%; color: #a31515;">&#39;Upload File:&#39;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; sro</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">write(</span><span style="line-height: 140%; color: #a31515;">&#39;&lt;input type=&quot;file&quot; name=&quot;file&quot;&gt;&lt;br/&gt;&#39;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; sro</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">write(</span><span style="line-height: 140%; color: #a31515;">&#39;Top left: &#39;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; sro</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">write(</span><span style="line-height: 140%; color: #a31515;">&#39;&lt;input type=&quot;number&quot; name=&quot;xtl&quot; value=&quot;82&quot;&gt;&#39;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; sro</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">write(</span><span style="line-height: 140%; color: #a31515;">&#39;&lt;input type=&quot;number&quot; name=&quot;ytl&quot; value=&quot;73&quot;&gt;&lt;br/&gt;&#39;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; sro</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">write(</span><span style="line-height: 140%; color: #a31515;">&#39;Bottom left: &#39;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; sro</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">write(</span><span style="line-height: 140%; color: #a31515;">&#39;&lt;input type=&quot;number&quot; name=&quot;xbl&quot; value=&quot;81&quot;&gt;&#39;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; sro</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">write(</span><span style="line-height: 140%; color: #a31515;">&#39;&lt;input type=&quot;number&quot; name=&quot;ybl&quot; value=&quot;103&quot;&gt;&lt;br/&gt;&#39;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; sro</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">write(</span><span style="line-height: 140%; color: #a31515;">&#39;Top right: &#39;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; sro</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">write(</span><span style="line-height: 140%; color: #a31515;">&#39;&lt;input type=&quot;number&quot; name=&quot;xtr&quot; value=&quot;105&quot;&gt;&#39;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; sro</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">write(</span><span style="line-height: 140%; color: #a31515;">&#39;&lt;input type=&quot;number&quot; name=&quot;ytr&quot; value=&quot;69&quot;&gt;&lt;br/&gt;&#39;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; sro</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">write(</span><span style="line-height: 140%; color: #a31515;">&#39;Bottom right: &#39;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; sro</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">write(</span><span style="line-height: 140%; color: #a31515;">&#39;&lt;input type=&quot;number&quot; name=&quot;xbr&quot; value=&quot;105&quot;&gt;&#39;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; sro</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">write(</span><span style="line-height: 140%; color: #a31515;">&#39;&lt;input type=&quot;number&quot; name=&quot;ybr&quot; value=&quot;102&quot;&gt;&lt;br/&gt;&#39;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; sro</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">write(</span><span style="line-height: 140%; color: #a31515;">&#39;Width over height: &#39;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; sro</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">write(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&#39;&lt;input type=&quot;number&quot; name=&quot;fac&quot; step=&quot;0.1&quot; value=&quot;1.0&quot;&gt;&lt;br/&gt;&#39;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; sro</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">write(</span><span style="line-height: 140%; color: #a31515;">&#39;&lt;input type=&quot;submit&quot; name=&quot;submit&quot; value=&quot;Submit&quot;&gt;&#39;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; sro</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">write(</span><span style="line-height: 140%; color: #a31515;">&#39;&lt;/form&gt;&lt;/body&gt;&lt;/html&gt;&#39;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">class</span><span style="line-height: 140%;"> UploadHandler(blobstore_handlers</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">BlobstoreUploadHandler):</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">def</span><span style="line-height: 140%;"> post(self):</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;"># Get the posted PNG file in the variable img1</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; upload_files = self</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">get_uploads(</span><span style="line-height: 140%; color: #a31515;">&#39;file&#39;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> len(upload_files) == 0:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; self</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">redirect(</span><span style="line-height: 140%; color: #a31515;">&quot;/&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; blob_info = upload_files[0]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; blob_reader = blobstore</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">BlobReader(blob_info)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; img1 = blob_reader</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">read()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; blob_reader</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">close()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; filekey = str(blob_info</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">key())</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;"># Get the various coordinate inputs and the width factor</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; xtl = int(cgi</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">escape(self</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">request</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">get(</span><span style="line-height: 140%; color: #a31515;">&#39;xtl&#39;</span><span style="line-height: 140%;">)))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; ytl = int(cgi</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">escape(self</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">request</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">get(</span><span style="line-height: 140%; color: #a31515;">&#39;ytl&#39;</span><span style="line-height: 140%;">)))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; xbl = int(cgi</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">escape(self</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">request</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">get(</span><span style="line-height: 140%; color: #a31515;">&#39;xbl&#39;</span><span style="line-height: 140%;">)))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; ybl = int(cgi</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">escape(self</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">request</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">get(</span><span style="line-height: 140%; color: #a31515;">&#39;ybl&#39;</span><span style="line-height: 140%;">)))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; xtr = int(cgi</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">escape(self</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">request</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">get(</span><span style="line-height: 140%; color: #a31515;">&#39;xtr&#39;</span><span style="line-height: 140%;">)))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; ytr = int(cgi</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">escape(self</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">request</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">get(</span><span style="line-height: 140%; color: #a31515;">&#39;ytr&#39;</span><span style="line-height: 140%;">)))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; xbr = int(cgi</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">escape(self</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">request</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">get(</span><span style="line-height: 140%; color: #a31515;">&#39;xbr&#39;</span><span style="line-height: 140%;">)))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; ybr = int(cgi</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">escape(self</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">request</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">get(</span><span style="line-height: 140%; color: #a31515;">&#39;ybr&#39;</span><span style="line-height: 140%;">)))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; xscale = float(cgi</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">escape(self</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">request</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">get(</span><span style="line-height: 140%; color: #a31515;">&#39;fac&#39;</span><span style="line-height: 140%;">)))</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; tl = xtl, ytl</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; bl = xbl, ybl</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; tr = xtr, ytr</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; br = xbr, ybr</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;"># Some constants</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; orgx,orgy,orgu = </span><span style="line-height: 140%; color: #a31515;">&#39;x1&#39;</span><span style="line-height: 140%;">,</span><span style="line-height: 140%; color: #a31515;">&#39;x2&#39;</span><span style="line-height: 140%;">,</span><span style="line-height: 140%; color: #a31515;">&#39;x3&#39;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; outx,outy,outu = </span><span style="line-height: 140%; color: #a31515;">&#39;y1&#39;</span><span style="line-height: 140%;">,</span><span style="line-height: 140%; color: #a31515;">&#39;y2&#39;</span><span style="line-height: 140%;">,</span><span style="line-height: 140%; color: #a31515;">&#39;y3&#39;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; orgd = {orgx, orgy, orgu}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; outd = {outx, outy, outu}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; orgrows = tuple(sorted(orgd))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; outrows = tuple(sorted(orgd))</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; logging</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">info(</span><span style="line-height: 140%; color: #a31515;">&quot;Setting up the math&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; H = coord_map_matrix(orgx, outx, orgd, outd,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tl, bl, tr, br, xscale)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; logging</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">info(</span><span style="line-height: 140%; color: #a31515;">&quot;Loading the image from file&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;"># Calculate the width and height of the skewed portion of our</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;"># input image and then calculate the output height as the</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;"># diagonal distance (which gives more than enough pixels)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; orgwid = max(abs(tr[0]-tl[0]), abs(br[0]-bl[0]))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; orghgt = max(abs(br[1]-tr[1]), abs(bl[1]-tl[1]))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; outhgt = floor(max(sqrt(orgwid**2 + orghgt**2), orghgt))</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;"># Get the extents of our input area with a bit of padding</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; ext=((min(xtl,xbl,xtr,xbr)-2,min(ytl,ybl,ytr,ybr)-2),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (max(xtl,xbl,xtr,xbr)+2,max(ytl,ybl,ytr,ybr)+2))</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;"># Load the image data into memory and then map it out to</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;"># records in the NDB datastore</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; img = image</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">bytes2image(img1)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; imgkey, futures = image2map(img, outhgt, ext, filekey, orgrows)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;"># Once done, we need to wait on each of our async writes</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;"># (wait() is not enough - we also need to count the records</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; ytotal = len(futures)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;"> f </span><span style="line-height: 140%; color: blue;">in</span><span style="line-height: 140%;"> futures:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; f</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">wait()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; attempts = 0</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; threshold = 20</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; q = TransformData</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">query(ancestor=imgkey)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; cnt = q</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">count()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> cnt == ytotal:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; logging</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">info(</span><span style="line-height: 140%; color: #a31515;">&quot;We have enough TransformData objects (%d)&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ytotal)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">else</span><span style="line-height: 140%;">:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; logging</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">info(</span><span style="line-height: 140%; color: #a31515;">&quot;We DON&#39;T have enough TransformData objects (%d)&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; cnt)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;"># Launch our transformation pipeline: this starts with a </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;"># map-reduce to transform the points, followed by another to</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;"># output the resulting image</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; transpipe = TransformPipeline(outx, outy, outhgt, tl, bl, tr, br,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pickle</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">dumps(H), xscale, ext,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; filekey, imgkey</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">urlsafe())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; transpipe</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">start()</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; self</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">redirect(</span><span style="line-height: 140%; color: #a31515;">&quot;/serve/&quot;</span><span style="line-height: 140%;"> + filekey)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">class</span><span style="line-height: 140%;"> ServeHandler(blobstore_handlers</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">BlobstoreDownloadHandler):</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">def</span><span style="line-height: 140%;"> get(self, resource):</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">while</span><span style="line-height: 140%;"> True:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; q = ResultsData</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">query(ResultsData</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">file == resource)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; cnt = q</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">count()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> cnt == 0:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; logging</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">info(</span><span style="line-height: 140%; color: #a31515;">&quot;We don&#39;t yet have results (%s)&quot;</span><span style="line-height: 140%;"> % resource)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; time</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">sleep(5)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">else</span><span style="line-height: 140%;">:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">break</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; res = q</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">get()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; blob_info = blobstore</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">BlobInfo</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">get(res</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">image)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; self</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">send_blob(blob_info)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; res</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">key</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">delete()</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">app = webapp2</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">WSGIApplication(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; [</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (</span><span style="line-height: 140%; color: #a31515;">&#39;/&#39;</span><span style="line-height: 140%;">, MainHandler),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (</span><span style="line-height: 140%; color: #a31515;">&#39;/upload&#39;</span><span style="line-height: 140%;">, UploadHandler),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (</span><span style="line-height: 140%; color: #a31515;">&#39;/serve/([^/]+)?&#39;</span><span style="line-height: 140%;">, ServeHandler)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; ],</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; debug=True)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">def</span><span style="line-height: 140%;"> image2map(image, outhgt, ext, filekey, row_labels):</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;"># Get the extents of the portion we&#39;re interested in, along</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;"># with the width and height of the overall image</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; min,max = ext</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; h = len(image)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; w = len(image[0])</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; rx, ry, ru = row_labels</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; xrng = range(min[0],max[0]+1,1)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; yrng = range(min[1],max[1]+1,1)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ytotal = max[1]+1-min[1]</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; id = ImageData(file=filekey, input_rows=ytotal,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; output_rows=int(outhgt))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; imgkey = id</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">put()</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; logging</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">info(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;Will create {0} records for each ({1} to {2})&quot;</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">format(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; ytotal, min[1], max[1]+1))</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; createprogmeter(</span><span style="line-height: 140%; color: #a31515;">&quot;Extracting points from the image&quot;</span><span style="line-height: 140%;">, int(ytotal))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; futures = []</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;"># Loop over y: we&#39;re going to create one TransformData and one</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;"># color record for each y value in the input image section</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;"> y </span><span style="line-height: 140%; color: blue;">in</span><span style="line-height: 140%;"> yrng:</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;"># Reset the accumulators for our point an color data</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; ptsD = (set(row_labels), set())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; ptsF = {}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; colorsD = ({</span><span style="line-height: 140%; color: #a31515;">&#39;r&#39;</span><span style="line-height: 140%;">, </span><span style="line-height: 140%; color: #a31515;">&#39;g&#39;</span><span style="line-height: 140%;">, </span><span style="line-height: 140%; color: #a31515;">&#39;b&#39;</span><span style="line-height: 140%;">}, set())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; colorsF = {}</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;"># Loop over x, storing a row of point and color data in our</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;"># various locations before writing them out</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;"> x </span><span style="line-height: 140%; color: blue;">in</span><span style="line-height: 140%;"> xrng:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> x &lt; w </span><span style="line-height: 140%; color: blue;">and</span><span style="line-height: 140%;"> y &lt; h:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pt = (x,y)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ptsD[1]</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">add(pt)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ptsF[(rx, pt)] = x</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ptsF[(ry, pt)] = y</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ptsF[(ru, pt)] = 1</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; col = image[y][x]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> type(col) </span><span style="line-height: 140%; color: blue;">is</span><span style="line-height: 140%;"> int:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; red, green, blue = col, col, col</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">else</span><span style="line-height: 140%;">:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; red, green, blue = col</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; colorsD[1]</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">add(pt)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; colorsF[(</span><span style="line-height: 140%; color: #a31515;">&#39;r&#39;</span><span style="line-height: 140%;">, pt)] = red</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; colorsF[(</span><span style="line-height: 140%; color: #a31515;">&#39;g&#39;</span><span style="line-height: 140%;">, pt)] = green</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; colorsF[(</span><span style="line-height: 140%; color: #a31515;">&#39;b&#39;</span><span style="line-height: 140%;">, pt)] = blue</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;"># Write a row of point data as a mappable entry in the DataStore</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; td = TransformData(parent=imgkey, i=y,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pts=mat2rec(mat</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">Mat(ptsD, ptsF)))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; futures</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">append(td</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">put_async())</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;"># Our color data (also by row) will be in the memcache</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; cr = mat2rec(mat</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">Mat(colorsD, colorsF))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; memcache</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">set(</span><span style="line-height: 140%; color: #a31515;">&quot;%s_col%d&quot;</span><span style="line-height: 140%;"> % (filekey, y), cr)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; progress()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; finishprogress()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> imgkey, futures</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;"># Classes for specifying map operations in the DataStore</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">class</span><span style="line-height: 140%;"> ImageData(ndb</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">Model):</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; file = ndb</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">StringProperty()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; input_rows = ndb</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">IntegerProperty()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; output_rows = ndb</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">IntegerProperty()</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">class</span><span style="line-height: 140%;"> TransformData(ndb</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">Model):</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; i = ndb</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">IntegerProperty()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; pts = ndb</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">PickleProperty()</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">class</span><span style="line-height: 140%;"> OutputData(ndb</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">Model):</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; i = ndb</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">IntegerProperty()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; k1 = ndb</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">FloatProperty()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; k2 = ndb</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">FloatProperty()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; y = ndb</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">FloatProperty()</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">class</span><span style="line-height: 140%;"> ResultsData(ndb</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">Model):</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; file = ndb</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">StringProperty()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; image = ndb</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">StringProperty()</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;"># MapReduce pipeline to transform the lines of our input image</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">class</span><span style="line-height: 140%;"> TransformPipeline(base_handler</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">PipelineBase):</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">def</span><span style="line-height: 140%;"> run(self, outx, outy, outhgt, tl, bl, tr, br, H, xscale,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ext, filekey, imgkey):</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; output = </span><span style="line-height: 140%; color: blue;">yield</span><span style="line-height: 140%;"> mapreduce_pipeline</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">MapreducePipeline(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;transform_images&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;main.transform_map&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;main.transform_reduce&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;mapreduce.input_readers.DatastoreInputReader&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;mapreduce.output_writers.BlobstoreOutputWriter&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; mapper_params={</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;entity_kind&quot;</span><span style="line-height: 140%;">: </span><span style="line-height: 140%; color: #a31515;">&quot;main.TransformData&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;batch_size&quot;</span><span style="line-height: 140%;">: 1,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;top_left&quot;</span><span style="line-height: 140%;">: tl,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;bottom_left&quot;</span><span style="line-height: 140%;">: bl,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;top_right&quot;</span><span style="line-height: 140%;">: tr,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;bottom_right&quot;</span><span style="line-height: 140%;">: br,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;x_label&quot;</span><span style="line-height: 140%;">: outx,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;y_label&quot;</span><span style="line-height: 140%;">: outy,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;H&quot;</span><span style="line-height: 140%;">: H</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; },</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; reducer_params={</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;mime_type&quot;</span><span style="line-height: 140%;">: </span><span style="line-height: 140%; color: #a31515;">&quot;text/plain&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; },</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; shards=16)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">yield</span><span style="line-height: 140%;"> ProcessOutput(output, outhgt, tl, bl, tr, br, xscale,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ext, filekey, imgkey)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">def</span><span style="line-height: 140%;"> transform_map(data):</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;"># Get data from our model object</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; i = data</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">i</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; logging</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">info(</span><span style="line-height: 140%; color: #a31515;">&quot;TransformMap called on %d&quot;</span><span style="line-height: 140%;">, i)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; idkey = data</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">key</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">parent()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; key = idkey</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">get()</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">file</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; pts = rec2mat(data</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">pts)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; data</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">key</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">delete()</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;"># Get per-mapper data, too</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ctx = context</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">get()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; params = ctx</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">mapreduce_spec</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">mapper</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">params</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; tl = tuple(params[</span><span style="line-height: 140%; color: #a31515;">&quot;top_left&quot;</span><span style="line-height: 140%;">])</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; bl = tuple(params[</span><span style="line-height: 140%; color: #a31515;">&quot;bottom_left&quot;</span><span style="line-height: 140%;">])</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; tr = tuple(params[</span><span style="line-height: 140%; color: #a31515;">&quot;top_right&quot;</span><span style="line-height: 140%;">])</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; br = tuple(params[</span><span style="line-height: 140%; color: #a31515;">&quot;bottom_right&quot;</span><span style="line-height: 140%;">])</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; orgx = params[</span><span style="line-height: 140%; color: #a31515;">&quot;x_label&quot;</span><span style="line-height: 140%;">]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; orgy = params[</span><span style="line-height: 140%; color: #a31515;">&quot;y_label&quot;</span><span style="line-height: 140%;">]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; rawH = params[</span><span style="line-height: 140%; color: #a31515;">&quot;H&quot;</span><span style="line-height: 140%;">]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; H = pickle</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">loads(str(rawH))</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;"># Transform a single row of points</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; hpts = H * pts</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; opts = mat_move2board(hpts)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;"># Save the results to the memcache</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; reckey = </span><span style="line-height: 140%; color: #a31515;">&quot;%s_pts%d&quot;</span><span style="line-height: 140%;"> % (key, i)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; memcache</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">set(reckey, mat2rec(opts))</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; logging</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">info(</span><span style="line-height: 140%; color: #a31515;">&quot;TransformMap wrote for %d (%s)&quot;</span><span style="line-height: 140%;">, i, reckey)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;"># To give the reducer something to do, let&#39;s see if the row</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;"># contains any of the extents points of our area (we could also</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;"># just calculate this - it&#39;s min = (0,0) &amp; max = (xscale,1),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;"># but it&#39;s more fun to have the reducer calculate it :-)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; res = []</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> tl[1] == i:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; res</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">append((opts[(orgx,tl)],opts[(orgy,tl)]))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> bl[1] == i:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; res</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">append((opts[(orgx,bl)],opts[(orgy,bl)]))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> tr[1] == i:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; res</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">append((opts[(orgx,tr)],opts[(orgy,tr)]))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> br[1] == i:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; res</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">append((opts[(orgx,br)],opts[(orgy,br)]))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">yield</span><span style="line-height: 140%;"> idkey</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">urlsafe(), res</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">def</span><span style="line-height: 140%;"> transform_reduce(key, values):</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; logging</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">info(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;TransformReduce called for {0} with {1} values&quot;</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">format(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; key,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; len(values)))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; idkey = ndb</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">Key(urlsafe=key)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; id = idkey</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">get()</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> len(values) &lt; id</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">input_rows:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; logging</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">info(</span><span style="line-height: 140%; color: #a31515;">&quot;Did not get enough input rows. Terminating.&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">else</span><span style="line-height: 140%;">:</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;"># We get a lits of strings, each of which can be evaluated to</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;"># a list</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; v1 = list(map(eval,values))</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;"># We then have a list of lists, most of which will be empty</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;"># Flatten it down to a list of coordinate pairs that we</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;"># perform a min-max calculation on</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; vs = [item </span><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;"> sublist </span><span style="line-height: 140%; color: blue;">in</span><span style="line-height: 140%;"> v1 </span><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;"> item </span><span style="line-height: 140%; color: blue;">in</span><span style="line-height: 140%;"> sublist]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> len(vs) == 0:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">yield</span><span style="line-height: 140%; color: #a31515;">&quot;&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">else</span><span style="line-height: 140%;">:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">yield</span><span style="line-height: 140%;"> ((min(vs, key = </span><span style="line-height: 140%; color: blue;">lambda</span><span style="line-height: 140%;"> t: t[0])[0],</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; min(vs, key = </span><span style="line-height: 140%; color: blue;">lambda</span><span style="line-height: 140%;"> t: t[1])[1]),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (max(vs, key = </span><span style="line-height: 140%; color: blue;">lambda</span><span style="line-height: 140%;"> t: t[0])[0],</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; max(vs, key = </span><span style="line-height: 140%; color: blue;">lambda</span><span style="line-height: 140%;"> t: t[1])[1]))</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;"># The continuation pipeline that fires once the transformations</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;"># are complete</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">class</span><span style="line-height: 140%;"> ProcessOutput(base_handler</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">PipelineBase):</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">def</span><span style="line-height: 140%;"> run(self, output, outhgt, ltl, lbl, ltr, lbr, xscale, ext,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; filekey, imgkey):</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;"># Get the output of the first MapReduce pipeline, which is</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;"># a textual blog containing the min/max of the area we care</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;"># about in the target coord system</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; parts = output[0]</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">split(</span><span style="line-height: 140%; color: #a31515;">&quot;/&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; blob_key = blobstore</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">BlobKey(parts[len(parts)-1])</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; blob_reader = blobstore</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">BlobReader(blob_key)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; minmax = eval(blob_reader</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">read())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; blobstore</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">delete(blob_key)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;"># Get a usable ket for the parent image record</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; idkey = ndb</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">Key(urlsafe=imgkey)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; logging</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">info(</span><span style="line-height: 140%; color: #a31515;">&quot;TransformReduce returned: {0}&quot;</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">format(minmax))</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;"># Convert the JSON-serialized/de-serialized parameters in a form</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;"># we can use (tuples get converted to lists, interestingly)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; tl = tuple(ltl)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; bl = tuple(lbl)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; tr = tuple(ltr)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; br = tuple(lbr)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; xbl,ybl = bl</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; xtl,ytl = tl</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;"># Calculate the output width and height</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; xmin = minmax[0][0]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; xmax = minmax[1][0]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; ymin = minmax[0][1]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; ymax = minmax[1][1]</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; xext = floor(outhgt * xscale)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; yinc = (ymax-ymin)/outhgt</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; xinc = (xmax-xmin)/xext</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;"># We&#39;re going to loop through our output rows, collecting</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;"># futures for each of our async write operations</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; rowcount = int(outhgt)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; futures = []</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;"> i </span><span style="line-height: 140%; color: blue;">in</span><span style="line-height: 140%;"> range(rowcount):</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; fac = i/outhgt</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; k = (round(xtl+((xbl-xtl)*fac)), round(ytl+((ybl-ytl)*fac)))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; y = ymin + i * yinc</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;"># Create an NDB entity for each row we want to output</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; od = OutputData(parent=idkey, i=i, k1=k[0], k2=k[1], y=y)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; futures</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">append(od</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">put_async())</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;"># Wait for all our future writes to finish</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;"># (still better than a sync call)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;"> f </span><span style="line-height: 140%; color: blue;">in</span><span style="line-height: 140%;"> futures:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; f</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">wait()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; q = OutputData</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">query(ancestor=idkey)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; cnt = q</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">count()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> cnt == rowcount:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; logging</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">info(</span><span style="line-height: 140%; color: #a31515;">&quot;We have enough OutputData objects (%d)&quot;</span><span style="line-height: 140%;">, cnt)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">else</span><span style="line-height: 140%;">:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; logging</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">info(</span><span style="line-height: 140%; color: #a31515;">&quot;We DON&#39;T have enough OutputData objects (%d)&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; cnt)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;"># Create and launch our next MapReduce pipeline, this time to</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;"># output</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; outpipe = OutputPipeline(ext, xmin, xext, xinc, filekey, imgkey)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; outpipe</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">start()</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;"># Some helper functions to serialize our point matrices</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;"># to a format that&#39;s a little more compact</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">def</span><span style="line-height: 140%;"> mat2rec(M):</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; mn = min(M</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">D[1])</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; mx = max(M</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">D[1])</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; dom0 = list(M</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">D[0])</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; rows = []</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;"> x </span><span style="line-height: 140%; color: blue;">in</span><span style="line-height: 140%;"> range(mn[0], mx[0]+1, 1):</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; row = []</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;"> y </span><span style="line-height: 140%; color: blue;">in</span><span style="line-height: 140%;"> range(mn[1], mx[1]+1, 1):</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; tup = []</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; pt = (x,y)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> pt </span><span style="line-height: 140%; color: blue;">in</span><span style="line-height: 140%;"> M</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">D[1]:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;"> t </span><span style="line-height: 140%; color: blue;">in</span><span style="line-height: 140%;"> dom0:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tup</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">append(M[(t,pt)])</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; row</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">append(tuple(tup))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">else</span><span style="line-height: 140%;">:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; row</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">append(None)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; rows</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">append(row)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> (mn, mx, dom0, rows)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">def</span><span style="line-height: 140%;"> rec2mat(rec):</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; mn, mx, dom0, rows = rec</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; dom1 = set()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; f = {}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;"> x </span><span style="line-height: 140%; color: blue;">in</span><span style="line-height: 140%;"> range(mx[0]-mn[0]+1):</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;"> y </span><span style="line-height: 140%; color: blue;">in</span><span style="line-height: 140%;"> range(mx[1]-mn[1]+1):</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; pt = (mn[0]+x,mn[1]+y)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; val = rows[x][y]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> val != None:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; dom1</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">add(pt)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;"> i,t </span><span style="line-height: 140%; color: blue;">in</span><span style="line-height: 140%;"> enumerate(dom0):</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; f[(t,pt)] = val[i]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> Mat((set(dom0),dom1),f)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">def</span><span style="line-height: 140%;"> loadrec(rec, m):</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; mn, mx, dom0, rows = rec</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; dom1 = set()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; f = {}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;"> x </span><span style="line-height: 140%; color: blue;">in</span><span style="line-height: 140%;"> range(mx[0]-mn[0]+1):</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;"> y </span><span style="line-height: 140%; color: blue;">in</span><span style="line-height: 140%;"> range(mx[1]-mn[1]+1):</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; pt = (mn[0]+x,mn[1]+y)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; val = rows[x][y]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> val != None:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; dom1</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">add(pt)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;"> i,t </span><span style="line-height: 140%; color: blue;">in</span><span style="line-height: 140%;"> enumerate(dom0):</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; f[(t,pt)] = val[i]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; m</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">D = (set(dom0), dom1)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; m</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">f = f</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;"># MapReduce pipeline to output rows of our image to a .PNG</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">class</span><span style="line-height: 140%;"> OutputPipeline(base_handler</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">PipelineBase):</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">def</span><span style="line-height: 140%;"> run(self, ext, xmin, xext, xinc, filekey, imgkey):</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; output = </span><span style="line-height: 140%; color: blue;">yield</span><span style="line-height: 140%;"> mapreduce_pipeline</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">MapreducePipeline(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;output_image&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;main.output_map&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;main.output_reduce&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;mapreduce.input_readers.DatastoreInputReader&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;mapreduce.output_writers.BlobstoreOutputWriter&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; mapper_params={</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;entity_kind&quot;</span><span style="line-height: 140%;">: </span><span style="line-height: 140%; color: #a31515;">&quot;main.OutputData&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;batch_size&quot;</span><span style="line-height: 140%;">: 1,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;ext_min&quot;</span><span style="line-height: 140%;">: ext[0],</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;ext_max&quot;</span><span style="line-height: 140%;">: ext[1],</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;xmin&quot;</span><span style="line-height: 140%;">: xmin,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;xext&quot;</span><span style="line-height: 140%;">: xext,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;xinc&quot;</span><span style="line-height: 140%;">: xinc</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; },</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; reducer_params={</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;mime_type&quot;</span><span style="line-height: 140%;">: </span><span style="line-height: 140%; color: #a31515;">&quot;image/png&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; },</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; shards=16)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">yield</span><span style="line-height: 140%;"> FinishUp(output, filekey, imgkey)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">class</span><span style="line-height: 140%;"> FinishUp(base_handler</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">PipelineBase):</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">def</span><span style="line-height: 140%;"> run(self, output, filekey, imgkey):</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;"># Delete the image info entity - no longer needed</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; ndb</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">Key(urlsafe=imgkey)</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">delete()</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;"># Store the location of the output blob in a record</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;"># that&#39;s associated with the input file</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; parts = output[0]</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">split(</span><span style="line-height: 140%; color: #a31515;">&quot;/&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; rd = ResultsData(file=filekey)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; rd</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">image = parts[len(parts)-1]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; rd</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">put()</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">def</span><span style="line-height: 140%;"> output_map(data):</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;"># Get model data</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; i = data</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">i</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; logging</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">info(</span><span style="line-height: 140%; color: #a31515;">&quot;OutputMap called on %d&quot;</span><span style="line-height: 140%;">, i)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; parent = data</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">key</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">parent()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; key = parent</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">get()</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">file</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; k = data</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">k1, data</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">k2</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; y = data</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">y</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;"># Get our per-mapper parameters</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ctx = context</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">get()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; params = ctx</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">mapreduce_spec</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">mapper</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">params</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; xmin = params[</span><span style="line-height: 140%; color: #a31515;">&quot;xmin&quot;</span><span style="line-height: 140%;">]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; xext = params[</span><span style="line-height: 140%; color: #a31515;">&quot;xext&quot;</span><span style="line-height: 140%;">]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; xinc = params[</span><span style="line-height: 140%; color: #a31515;">&quot;xinc&quot;</span><span style="line-height: 140%;">]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; extmin = tuple(params[</span><span style="line-height: 140%; color: #a31515;">&quot;ext_min&quot;</span><span style="line-height: 140%;">])</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; extmax = tuple(params[</span><span style="line-height: 140%; color: #a31515;">&quot;ext_max&quot;</span><span style="line-height: 140%;">])</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ext = (extmin,extmax)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;"># Build a row of colour information for our output image</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; row = []</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; outpts = Mat((set(),set()), {})</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; colors = Mat((set(),set()), {})</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;"> j </span><span style="line-height: 140%; color: blue;">in</span><span style="line-height: 140%;"> range(int(xext)):</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; x = xmin + j * xinc</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; col,k = get_color_ex(k, outpts, colors, ext, x, y, key)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; row</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">append(col)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;"># Delete the originating record</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; data</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">key</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">delete()</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;"># Store the row in the memcache for later retrieval</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; idkey = parent</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">urlsafe()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; memcache</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">set(</span><span style="line-height: 140%; color: #a31515;">&quot;%s_row%d&quot;</span><span style="line-height: 140%;"> % (idkey, i), row)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;"># Yield the fact we&#39;re done for our reduce function</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;"># (the data isn&#39;t especially important)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">yield</span><span style="line-height: 140%;"> idkey, i</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;"># We&#39;ll track the rows that have come through for a particular key</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">def</span><span style="line-height: 140%;"> output_reduce(key, values):</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; logging</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">info(</span><span style="line-height: 140%; color: #a31515;">&quot;OutputReduce called for %s with %d values&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; key, len(values))</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; idkey = ndb</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">Key(urlsafe=key)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; id = idkey</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">get()</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; logging</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">info(</span><span style="line-height: 140%; color: #a31515;">&quot;id.output_rows: %d&quot;</span><span style="line-height: 140%;">, id</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">output_rows)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;"># If we have 90% of rows or more (not great: shouldn&#39;t have</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;"># to put up with any drop off) then let&#39;s continue</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> len(values) &lt; id</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">output_rows * 0.9:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; logging</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">info(</span><span style="line-height: 140%; color: #a31515;">&quot;Did not get enough output rows. Terminating.&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">else</span><span style="line-height: 140%;">:</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;"># Collect our rows, reading them from the memcache</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; rows = []</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;"> i </span><span style="line-height: 140%; color: blue;">in</span><span style="line-height: 140%;"> range(id</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">output_rows):</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; row = memcache</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">get(</span><span style="line-height: 140%; color: #a31515;">&quot;%s_row%d&quot;</span><span style="line-height: 140%;"> % (key, i))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> row != None:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; rows</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">append(row)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;"># Then we write them out via a BytesIO object</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">with</span><span style="line-height: 140%;"> BytesIO() </span><span style="line-height: 140%; color: blue;">as</span><span style="line-height: 140%;"> bio:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; image</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">image2writer(rows, bio)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">yield</span><span style="line-height: 140%;"> bio</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">getvalue()</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;"># Versions of older functions that pull in point and colour</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;"># objects from the memcache as they&#39;re needed</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">def</span><span style="line-height: 140%;"> get_color_ex(k, pts, colors, ext, x, y, filekey):</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; adj = get_adjacent_ex(k,pts,ext,x,y,filekey)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; best = [k </span><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;"> k,v </span><span style="line-height: 140%; color: blue;">in</span><span style="line-height: 140%;"> sorted(adj, key=</span><span style="line-height: 140%; color: blue;">lambda</span><span style="line-height: 140%;"> kv: kv[1])][0]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ret = (getcolors(colors, (</span><span style="line-height: 140%; color: #a31515;">&#39;r&#39;</span><span style="line-height: 140%;">,best), filekey),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; getcolors(colors, (</span><span style="line-height: 140%; color: #a31515;">&#39;g&#39;</span><span style="line-height: 140%;">,best), filekey),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; getcolors(colors, (</span><span style="line-height: 140%; color: #a31515;">&#39;b&#39;</span><span style="line-height: 140%;">,best), filekey)), best</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> ret</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">def</span><span style="line-height: 140%;"> get_adjacent_ex(k, pts, ext, x, y, filekey):</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; kx, ky = k</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ((minx,miny),(maxx,maxy)) = ext</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; xr = range(-1,2,1)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; yr = range(-1,2,1)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; adj = [(kx+i,ky+j) </span><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;"> i </span><span style="line-height: 140%; color: blue;">in</span><span style="line-height: 140%;"> xr </span><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;"> j </span><span style="line-height: 140%; color: blue;">in</span><span style="line-height: 140%;"> yr</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> kx+i &gt; minx </span><span style="line-height: 140%; color: blue;">and</span><span style="line-height: 140%;"> kx+i &lt; maxx </span><span style="line-height: 140%; color: blue;">and</span><span style="line-height: 140%;"> ky+j &gt; miny</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">and</span><span style="line-height: 140%;"> ky+j &lt; maxy]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> [(xy,quad_midpoint_ex(xy,pts,x,y, filekey)) </span><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;"> xy </span><span style="line-height: 140%; color: blue;">in</span><span style="line-height: 140%;"> adj]</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">def</span><span style="line-height: 140%;"> getpts(pts, key, filekey):</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; add_to_cache(key[1], pts, </span><span style="line-height: 140%; color: #a31515;">&quot;pts&quot;</span><span style="line-height: 140%;">, filekey)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> pts[key]</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">def</span><span style="line-height: 140%;"> getcolors(cols, key, filekey):</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; add_to_cache(key[1], cols, </span><span style="line-height: 140%; color: #a31515;">&quot;col&quot;</span><span style="line-height: 140%;">, filekey)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> cols[key]</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">def</span><span style="line-height: 140%;"> add_to_cache(pt, m, id, key):</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; x, y = pt</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; reckey = </span><span style="line-height: 140%; color: #a31515;">&quot;%s_%s%d&quot;</span><span style="line-height: 140%;"> % (key, id, y)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; mat_empty = m</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">D[0] == set()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> mat_empty </span><span style="line-height: 140%; color: blue;">or</span><span style="line-height: 140%; color: blue;">not</span><span style="line-height: 140%;"> pt </span><span style="line-height: 140%; color: blue;">in</span><span style="line-height: 140%;"> m</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">D[1]:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; rec = memcache</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">get(reckey)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> rec == None:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; logging</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">info(</span><span style="line-height: 140%; color: #a31515;">&quot;Failed to read from {0}&quot;</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">format(reckey))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">else</span><span style="line-height: 140%;">:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> mat_empty:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; loadrec(rec, m)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">else</span><span style="line-height: 140%;">:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; mn, mx, dom0, rows = rec</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;"> x1 </span><span style="line-height: 140%; color: blue;">in</span><span style="line-height: 140%;"> range(mx[0]-mn[0]+1):</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;"> y1 </span><span style="line-height: 140%; color: blue;">in</span><span style="line-height: 140%;"> range(mx[1]-mn[1]+1):</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pt = (mn[0]+x1,mn[1]+y1)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; val = rows[x1][y1]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> val != None:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; m</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">D[1]</span><span style="line-height: 140%; color: blue;">.</span><span style="line-height: 140%;">add(pt)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;"> i,t </span><span style="line-height: 140%; color: blue;">in</span><span style="line-height: 140%;"> enumerate(dom0):</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; m[(t,pt)] = val[i]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> m</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">def</span><span style="line-height: 140%;"> quad_midpoint_ex(k, pts, x, y, filekey):</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; kx,ky = k</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; rx,ry = </span><span style="line-height: 140%; color: #a31515;">&#39;y1&#39;</span><span style="line-height: 140%;">,</span><span style="line-height: 140%; color: #a31515;">&#39;y2&#39;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; x0 = getpts(pts, (rx, k), filekey)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; y0 = getpts(pts, (ry, k), filekey)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; x2 = getpts(pts, (rx, (kx+1, ky+1)), filekey)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; y2 = getpts(pts, (ry, (kx+1, ky+1)), filekey)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; midx = x0+((x2-x0)/2)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; midy = y0+((y2-y0)/2)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> sqrt((x-midx)**2 + (y-midy)**2)</span></p>
</div>
<p>There are some other files needed to make this work on GAE – such as <em>app.yaml</em> and <em>mapreduce.yaml</em> – so if anyone is interested in actually giving this a try, please email me or post a comment and I’ll make them available.</p>
