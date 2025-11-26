---
layout: "post"
title: "Revit model viewer for iOS - part 2"
date: "2012-06-26 13:51:00"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Cloud"
  - "Mobile"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2012/06/revit-model-viewer-for-ios-part-2.html "
typepad_basename: "revit-model-viewer-for-ios-part-2"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/aec/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>This is a continuation of the previous post&#0160;<a href="http://adndevblog.typepad.com/aec/2012/06/revit-model-viewer-for-ios-part-1.html" target="_blank" title="Revit model viewer for iOS - part 1">Revit model viewer for iOS - part 1</a></p>
<p><strong>The iOS App part</strong></p>
<p>We already created a Revit AddIn that can upload geometry data to a storage service. Now we need an iOS application that can download the data and display it using OpenGL.</p>
<p>I started by creating a new iOS project (iOS &gt;&gt; Application &gt;&gt; Master-Detail Application) The Master list can show the name of the model geometries uploaded to the server and the Detail view can display the selected model&#39;s geometry. I chose to use Storyboards which make it easier to keep all the views together.</p>
<p>Note: to see how OpenGL and GLKit can be used in iOS you could also create an &#39;OpenGL Game&#39; project. This implements displaying a couple of rotating boxes.&#0160;</p>
<p>First I started implementing the server part - talking to the Amazon service and downloading the geometry. For that I downloaded the Amazon SDK for iOS:&#0160;<a href="http://aws.amazon.com/sdkforios/" target="_blank" title="Amazon SDK for iOS">Amazon SDK for iOS</a><br />Then I just needed to add the Framework to my project: Project settings &gt;&gt; Targets &gt;&gt; Summary &gt;&gt; Linked Frameworks and Libraries, then locate the framework in the downloaded folder.</p>
<p>Here is the code that gets the name of the uploaded models:</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">+ (<span style="color: #733ea4;">NSMutableArray</span> *)getItemNames</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">{</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #733ea4;"><span style="color: #000000;">&#0160; </span>AmazonS3Client<span style="color: #000000;"> * s3 =&#0160;</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #733ea4;"><span style="color: #000000;">&#0160; &#0160; [[</span>AmazonS3Client<span style="color: #000000;"> </span><span style="color: #401f7d;">alloc</span><span style="color: #000000;">]&#0160;</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #401f7d;"><span style="color: #000000;">&#0160; &#0160; </span>initWithAccessKey<span style="color: #000000;">:</span><span style="color: #79482e;">ACCESS_KEY_ID</span><span style="color: #000000;"> </span>withSecretKey<span style="color: #000000;">:</span><span style="color: #79482e;">SECRET_KEY</span><span style="color: #000000;">];</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #733ea4;"><span style="color: #000000;">&#0160; </span>NSMutableArray<span style="color: #000000;"> * names = [[</span>NSMutableArray<span style="color: #000000;"> </span><span style="color: #401f7d;">alloc</span><span style="color: #000000;">] </span><span style="color: #401f7d;">init</span><span style="color: #000000;">];</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #be299d;"><span style="color: #000000;">&#0160; </span>@try<span style="color: #000000;">&#0160;</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; {</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; &#0160; <span style="color: #733ea4;">S3ListObjectsRequest</span> * listObjectsRequest =&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #733ea4;"><span style="color: #000000;">&#0160; &#0160; [[</span>S3ListObjectsRequest<span style="color: #000000;"> </span><span style="color: #401f7d;">alloc</span><span style="color: #000000;">] </span><span style="color: #401f7d;">initWithName</span><span style="color: #000000;">:</span><span style="color: #79482e;">MODEL_BUCKET</span><span style="color: #000000;">]; &#0160;</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #733ea4;"><span style="color: #000000;">&#0160; &#0160; </span>S3ListObjectsResponse<span style="color: #000000;"> * response =&#0160;</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; &#0160; &#0160; [s3 <span style="color: #401f7d;">listObjects</span>:listObjectsRequest];</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; &#0160; <span style="color: #733ea4;">NSMutableArray</span> * objectSummaries =&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #733ea4;"><span style="color: #000000;">&#0160; &#0160; &#0160; response.</span>listObjectsResult<span style="color: #000000;">.</span>objectSummaries<span style="color: #000000;">;&#0160;</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; &#0160; <span style="color: #be299d;">for</span> (<span style="color: #733ea4;">S3ObjectSummary</span> * summary <span style="color: #be299d;">in</span> objectSummaries)</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; &#0160; {</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; &#0160; &#0160; [names <span style="color: #401f7d;">addObject</span>:[summary <span style="color: #401f7d;">key</span>]];</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; &#0160; }</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; }</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; <span style="color: #be299d;">@catch</span> (AmazonClientException * exception)&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; {</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; &#0160; [<span style="color: #be299d;">self</span> <span style="color: #30595d;">showAlert</span>:exception.<span style="color: #733ea4;">message</span> <span style="color: #30595d;">withTitle</span>:<span style="color: #d42722;">@&quot;Download Error&quot;</span>];</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; }</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; <span style="color: #be299d;">return</span> names;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">}</p>
<p>We can fill the Master View (a table view) with the list of model names we got with the above function. Then when the user selects one of the models we need to get the data from that item. Here is the code to do that:</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">+ (<span style="color: #733ea4;">NSMutableArray</span> *)getFacets:(<span style="color: #733ea4;">NSString</span> *)withName</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">{</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #be299d;"><span style="color: #000000;">&#0160; </span>@try<span style="color: #000000;">&#0160;</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; {</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #733ea4;"><span style="color: #000000;">&#0160; &#0160; </span>AmazonS3Client<span style="color: #000000;"> * s3 =&#0160;</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #733ea4;"><span style="color: #000000;">&#0160; &#0160; &#0160; [[</span>AmazonS3Client<span style="color: #000000;"> </span><span style="color: #401f7d;">alloc</span><span style="color: #000000;">]&#0160;</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #401f7d;"><span style="color: #000000;">&#0160; &#0160; &#0160; </span>initWithAccessKey<span style="color: #000000;">:</span><span style="color: #79482e;">ACCESS_KEY_ID</span><span style="color: #000000;"> </span>withSecretKey<span style="color: #000000;">:</span><span style="color: #79482e;">SECRET_KEY</span><span style="color: #000000;">];</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #733ea4;"><span style="color: #000000;">&#0160; &#0160; </span>S3GetObjectRequest<span style="color: #000000;"> * request =&#0160;</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #733ea4;"><span style="color: #000000;">&#0160; &#0160; &#0160; [[</span>S3GetObjectRequest<span style="color: #000000;"> </span><span style="color: #401f7d;">alloc</span><span style="color: #000000;">]&#0160;</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; &#0160; &#0160; <span style="color: #401f7d;">initWithKey</span>:withName <span style="color: #401f7d;">withBucket</span>:<span style="color: #79482e;">MODEL_BUCKET</span>];&#0160; &#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; &#0160; <span style="color: #733ea4;">S3GetObjectResponse</span> * response = [s3 <span style="color: #401f7d;">getObject</span>:request];</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; &#0160; <span style="color: #733ea4;">NSData</span> * data = [response <span style="color: #401f7d;">body</span>];</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #008423;"><span style="color: #000000;">&#0160; &#0160; </span>// Convert it to list of points</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #30595d;"><span style="color: #000000;">&#0160; &#0160; </span><span style="color: #be299d;">return</span><span style="color: #000000;"> [</span><span style="color: #be299d;">self</span><span style="color: #000000;"> </span>getFacetsFromData<span style="color: #000000;">:data];</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; }</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; <span style="color: #be299d;">@catch</span> (AmazonClientException * exception)&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; {</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; &#0160; [<span style="color: #be299d;">self</span> <span style="color: #30595d;">showAlert</span>:exception.<span style="color: #733ea4;">message</span> <span style="color: #30595d;">withTitle</span>:<span style="color: #d42722;">@&quot;Download Error&quot;</span>];</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; }</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #733ea4;"><span style="color: #000000;">&#0160; </span><span style="color: #be299d;">return</span><span style="color: #000000;"> [[</span>NSMutableArray<span style="color: #000000;"> </span><span style="color: #401f7d;">alloc</span><span style="color: #000000;">] </span><span style="color: #401f7d;">init</span><span style="color: #000000;">];</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">}</p>
<p>Now we need to display the geometry. In the storyboard I replaced the UIView of the Detail View Controller with GLKView. This is supposed to simplify a couple of things, e.g. now we get a function called&#0160;drawInRect() inside which we can do the drawing part. But that function won&#39;t get called unless we also set up a couple of things for the GLKView. We also need to create an effect that we can use to do the initialization each time before we start drawing:</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">- (<span style="color: #be299d;">void</span>)viewDidLoad</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">{</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #4d8186;"><span style="color: #000000;">&#0160; [</span>_statusButton<span style="color: #000000;"> </span><span style="color: #401f7d;">setTitle</span><span style="color: #000000;">:</span><span style="color: #d42722;">@&quot;Done&quot;</span><span style="color: #000000;">]; &#0160; &#0160;</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #401f7d;"><span style="color: #000000;">&#0160; [</span><span style="color: #be299d;">super</span><span style="color: #000000;"> </span>viewDidLoad<span style="color: #000000;">];</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #733ea4;"><span style="color: #000000;">&#0160; [[</span>NSNotificationCenter<span style="color: #000000;"> </span><span style="color: #401f7d;">defaultCenter</span><span style="color: #000000;">]&#0160;</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; &#0160; <span style="color: #401f7d;">addObserver</span>:<span style="color: #be299d;">self</span> <span style="color: #401f7d;">selector</span>:<span style="color: #be299d;">@selector</span>(didRotate:)&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #d42722;"><span style="color: #000000;">&#0160; &#0160; </span><span style="color: #401f7d;">name</span><span style="color: #000000;">:</span>@&quot;UIDeviceOrientationDidChangeNotification&quot;<span style="color: #000000;"> </span><span style="color: #401f7d;">object</span><span style="color: #000000;">:</span><span style="color: #be299d;">nil</span><span style="color: #000000;">];</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; <span style="color: #733ea4;">GLKView</span> * glView = (<span style="color: #733ea4;">GLKView</span> *)<span style="color: #be299d;">self</span>.<span style="color: #733ea4;">view</span>;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; <span style="color: #733ea4;">EAGLContext</span> * context =&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #401f7d;"><span style="color: #000000;">&#0160; &#0160; [[</span><span style="color: #733ea4;">EAGLContext</span><span style="color: #000000;"> </span>alloc<span style="color: #000000;">] </span>initWithAPI<span style="color: #000000;">:</span>kEAGLRenderingAPIOpenGLES2<span style="color: #000000;">];&#0160;</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; glView.<span style="color: #733ea4;">context</span> = context;&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #401f7d;"><span style="color: #000000;">&#0160; glView.</span><span style="color: #733ea4;">drawableColorFormat</span><span style="color: #000000;"> = </span>GLKViewDrawableColorFormatRGB565<span style="color: #000000;">;</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #401f7d;"><span style="color: #000000;">&#0160; glView.</span><span style="color: #733ea4;">drawableStencilFormat</span><span style="color: #000000;"> = </span>GLKViewDrawableStencilFormat8<span style="color: #000000;">;</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #401f7d;"><span style="color: #000000;">&#0160; glView.</span><span style="color: #733ea4;">drawableDepthFormat</span><span style="color: #000000;"> = </span>GLKViewDrawableDepthFormat16<span style="color: #000000;">;</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; <span style="color: #4d8186;">_baseEffect</span> = [[<span style="color: #733ea4;">GLKBaseEffect</span> <span style="color: #401f7d;">alloc</span>] <span style="color: #401f7d;">init</span>];&#0160; &#0160; &#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">}</p>
<p>To make the drawing faster we can store the geometry in the GPU. Each time the user selects a different model, we get the geometry from the storage service, turn it into an array of vertex and normal coordinates, then store it like so:</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #401f7d;">glGenBuffers<span style="color: #000000;">(</span><span style="color: #2f2fd1;">1</span><span style="color: #000000;">, &amp;</span><span style="color: #4d8186;">vertexBuffer</span><span style="color: #000000;">);</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #79482e;"><span style="color: #401f7d;">glBindBuffer</span><span style="color: #000000;">(</span>GL_ARRAY_BUFFER<span style="color: #000000;">, </span><span style="color: #4d8186;">vertexBuffer</span><span style="color: #000000;">);</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #008423;">//////////////////////////////////////////////////////////////////</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #008423;">// buffer data is in bytes =&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; //</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #008423;">// size of float * number of facets * &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; //</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #008423;">// vertices per facet * values per vertex &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; //</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #008423;">//////////////////////////////////////////////////////////////////</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #79482e;"><span style="color: #401f7d;">glBufferData</span><span style="color: #000000;">(</span>GL_ARRAY_BUFFER<span style="color: #000000;">,&#0160;</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160;&#0160;<span style="color: #be299d;">sizeof</span>(<span style="color: #733ea4;">GLfloat</span>) * facetCount * <span style="color: #2f2fd1;">3</span> * <span style="color: #2f2fd1;">3</span>, vertices,&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160;&#0160;<span style="color: #79482e;">GL_STATIC_DRAW</span>); &#0160; &#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #401f7d;">glGenBuffers<span style="color: #000000;">(</span><span style="color: #2f2fd1;">1</span><span style="color: #000000;">, &amp;</span><span style="color: #4d8186;">normalBuffer</span><span style="color: #000000;">);</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #79482e;"><span style="color: #401f7d;">glBindBuffer</span><span style="color: #000000;">(</span>GL_ARRAY_BUFFER<span style="color: #000000;">, </span><span style="color: #4d8186;">normalBuffer</span><span style="color: #000000;">);</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #79482e;"><span style="color: #401f7d;">glBufferData</span><span style="color: #000000;">(</span>GL_ARRAY_BUFFER<span style="color: #000000;">,&#0160;</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160;&#0160;<span style="color: #be299d;">sizeof</span>(<span style="color: #733ea4;">GLfloat</span>) * facetCount * <span style="color: #2f2fd1;">3</span> * <span style="color: #2f2fd1;">3</span>, normals,&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160;&#0160;<span style="color: #79482e;">GL_STATIC_DRAW</span>); &#0160; &#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #79482e;"><span style="color: #401f7d;">glBindBuffer</span><span style="color: #000000;">(</span>GL_ARRAY_BUFFER<span style="color: #000000;">, </span><span style="color: #2f2fd1;">0</span><span style="color: #000000;">);</span></p>
<p>Then whenever the drawInRect() function is called, where we need to draw our geometry, we can retrieve the stored array of vertices and normals and use that:<span style="font-family: Menlo; font-size: 11px;">&#0160;</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">- (<span style="color: #be299d;">void</span>)glkView:(<span style="color: #733ea4;">GLKView</span> *)view drawInRect:(<span style="color: #733ea4;">CGRect</span>)rect</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">{</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; <span style="color: #be299d;">if</span> (<span style="color: #4d8186;">_faces</span> == <span style="color: #be299d;">nil</span>)</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; {</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #008423;"><span style="color: #000000;">&#0160; &#0160; </span>// if there is nothing to draw let&#39;s just fill</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #008423;"><span style="color: #000000;">&#0160; &#0160; </span>// the background with red</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; &#0160; <span style="color: #401f7d;">glClearColor</span>(<span style="color: #2f2fd1;">1.0</span>, <span style="color: #2f2fd1;">0.0</span>, <span style="color: #2f2fd1;">0.0</span>, <span style="color: #2f2fd1;">1.0</span>);</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #79482e;"><span style="color: #000000;">&#0160; &#0160; </span><span style="color: #401f7d;">glClear</span><span style="color: #000000;">(</span>GL_COLOR_BUFFER_BIT<span style="color: #000000;">);</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #be299d;"><span style="color: #000000;">&#0160; &#0160; </span>return<span style="color: #000000;">;</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; }</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #008423;"><span style="color: #000000;">&#0160; </span>// colorMaterialEnabled = GL_FALSE&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #008423;"><span style="color: #000000;">&#0160; </span>// - uses the material color I set here</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #008423;"><span style="color: #000000;">&#0160; </span>// colorMaterialEnabled = GL_TRUE&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #008423;"><span style="color: #000000;">&#0160; </span>// - uses the material color that comes from the array</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #733ea4;"><span style="color: #000000;">&#0160; </span><span style="color: #4d8186;">_baseEffect</span><span style="color: #000000;">.</span>colorMaterialEnabled<span style="color: #000000;"> = </span><span style="color: #79482e;">GL_FALSE</span><span style="color: #000000;">;</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #733ea4;"><span style="color: #000000;">&#0160; </span><span style="color: #4d8186;">_baseEffect</span><span style="color: #000000;">.</span>light0<span style="color: #000000;">.</span>enabled<span style="color: #000000;"> = </span><span style="color: #79482e;">GL_TRUE</span><span style="color: #000000;">; &#0160;</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #733ea4;"><span style="color: #000000;">&#0160; </span><span style="color: #4d8186;">_baseEffect</span><span style="color: #000000;">.</span>material<span style="color: #000000;">.</span>shininess<span style="color: #000000;"> = </span><span style="color: #2f2fd1;">50</span><span style="color: #000000;">;&#0160;</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #401f7d;"><span style="color: #000000;">&#0160; </span><span style="color: #4d8186;">_baseEffect</span><span style="color: #000000;">.</span><span style="color: #733ea4;">lightingType</span><span style="color: #000000;"> = </span>GLKLightingTypePerPixel<span style="color: #000000;">;</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #008423;"><span style="color: #000000;">&#0160; </span>// GLKit does not seem to have these</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #79482e;"><span style="color: #000000;">&#0160; </span><span style="color: #401f7d;">glEnable</span><span style="color: #000000;">(</span>GL_DEPTH_TEST<span style="color: #000000;">);</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #79482e;"><span style="color: #000000;">&#0160; </span><span style="color: #401f7d;">glEnable</span><span style="color: #000000;">(</span>GL_CULL_FACE<span style="color: #000000;">);</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #401f7d;"><span style="color: #000000;">&#0160; </span>glDepthFunc<span style="color: #000000;">(</span><span style="color: #79482e;">GL_LEQUAL</span><span style="color: #000000;">);</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #2f2fd1;"><span style="color: #000000;">&#0160; </span><span style="color: #401f7d;">glClearColor</span><span style="color: #000000;">(</span>0.0f<span style="color: #000000;">, </span>0.5f<span style="color: #000000;">, </span>0.0f<span style="color: #000000;">, </span>1.0f<span style="color: #000000;">);</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #79482e;"><span style="color: #000000;">&#0160; </span><span style="color: #401f7d;">glClear</span><span style="color: #000000;">(</span>GL_COLOR_BUFFER_BIT<span style="color: #000000;">);</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #79482e;"><span style="color: #000000;">&#0160; </span><span style="color: #401f7d;">glClear</span><span style="color: #000000;">(</span>GL_DEPTH_BUFFER_BIT<span style="color: #000000;">);</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #79482e;"><span style="color: #000000;">&#0160; </span><span style="color: #401f7d;">glClear</span><span style="color: #000000;">(</span>GL_STENCIL_BUFFER_BIT<span style="color: #000000;">);</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #30595d;"><span style="color: #000000;">&#0160; [</span><span style="color: #be299d;">self</span><span style="color: #000000;"> </span>updateTransformation<span style="color: #000000;">];</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #008423;"><span style="color: #000000;">&#0160; </span>// Do the drawing</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #79482e;"><span style="color: #000000;">&#0160; </span><span style="color: #401f7d;">glBindBuffer</span><span style="color: #000000;">(</span>GL_ARRAY_BUFFER<span style="color: #000000;">, </span><span style="color: #4d8186;">vertexBuffer</span><span style="color: #000000;">);</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #008423;"><span style="color: #000000;">&#0160; </span>////////////////////////////////////////////////////////////////////</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #008423;"><span style="color: #000000;">&#0160; </span>// array type, number of values per vertex, value type, &#0160; &#0160; &#0160; &#0160; &#0160; //&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #008423;"><span style="color: #000000;">&#0160; </span>// normalize, &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; //</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #008423;"><span style="color: #000000;">&#0160; </span>// offset between values (0 unless using an interleaved array), &#0160; //</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #008423;"><span style="color: #000000;">&#0160; </span>// pointer to array &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; //</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #008423;"><span style="color: #000000;">&#0160; </span>////////////////////////////////////////////////////////////////////</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #401f7d;"><span style="color: #000000;">&#0160; </span>glVertexAttribPointer<span style="color: #000000;">(</span>GLKVertexAttribPosition<span style="color: #000000;">, </span><span style="color: #2f2fd1;">3</span><span style="color: #000000;">, </span><span style="color: #79482e;">GL_FLOAT</span><span style="color: #000000;">,&#0160;</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: #79482e;">GL_FALSE</span>, <span style="color: #2f2fd1;">0</span>, <span style="color: #2f2fd1;">0</span>);</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #401f7d;"><span style="color: #000000;">&#0160; </span>glEnableVertexAttribArray<span style="color: #000000;">(</span>GLKVertexAttribPosition<span style="color: #000000;">);</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #79482e;"><span style="color: #000000;">&#0160; </span><span style="color: #401f7d;">glBindBuffer</span><span style="color: #000000;">(</span>GL_ARRAY_BUFFER<span style="color: #000000;">, </span><span style="color: #4d8186;">normalBuffer</span><span style="color: #000000;">);</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #401f7d;"><span style="color: #000000;">&#0160; </span>glVertexAttribPointer<span style="color: #000000;">(</span>GLKVertexAttribNormal<span style="color: #000000;">, </span><span style="color: #2f2fd1;">3</span><span style="color: #000000;">, </span><span style="color: #79482e;">GL_FLOAT</span><span style="color: #000000;">,&#0160;</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: #79482e;">GL_FALSE</span>, <span style="color: #2f2fd1;">0</span>, <span style="color: #2f2fd1;">0</span>);</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #401f7d;"><span style="color: #000000;">&#0160; </span>glEnableVertexAttribArray<span style="color: #000000;">(</span>GLKVertexAttribNormal<span style="color: #000000;">);&#0160; &#0160; &#0160; &#0160;</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; <span style="color: #be299d;">long</span> facetCount = <span style="color: #2f2fd1;">0</span>;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; <span style="color: #be299d;">for</span> (<span style="color: #4d8186;">FaceData</span> * face <span style="color: #be299d;">in</span> <span style="color: #4d8186;">_faces</span>)</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; { &#0160; &#0160; &#0160; &#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #733ea4;"><span style="color: #000000;">&#0160; &#0160; </span><span style="color: #4d8186;">_baseEffect</span><span style="color: #000000;">.</span>material<span style="color: #000000;">.</span>diffuseColor<span style="color: #000000;"> =&#0160;</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #401f7d;"><span style="color: #000000;">&#0160; &#0160; </span>GLKVector4Make<span style="color: #000000;">(</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; &#0160; &#0160; face.<span style="color: #4d8186;">red</span> / <span style="color: #2f2fd1;">255</span>, face.<span style="color: #4d8186;">green</span> / <span style="color: #2f2fd1;">255</span>, face.<span style="color: #4d8186;">blue</span> / <span style="color: #2f2fd1;">255</span>, <span style="color: #2f2fd1;">1</span>);</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #401f7d;"><span style="color: #000000;">&#0160; &#0160; [</span><span style="color: #4d8186;">_baseEffect</span><span style="color: #000000;"> </span>prepareToDraw<span style="color: #000000;">];&#0160;</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; &#0160; <span style="color: #401f7d;">glDrawArrays</span>(<span style="color: #79482e;">GL_TRIANGLES</span>, facetCount * <span style="color: #2f2fd1;">3</span>, face.<span style="color: #4d8186;">facets</span>.<span style="color: #733ea4;">count</span> * <span style="color: #2f2fd1;">3</span>); &#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; &#0160; facetCount += face.<span style="color: #4d8186;">facets</span>.<span style="color: #733ea4;">count</span>;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; }</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #401f7d;"><span style="color: #000000;">&#0160; </span>glDisableVertexAttribArray<span style="color: #000000;">(</span>GLKVertexAttribPosition<span style="color: #000000;">);</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #401f7d;"><span style="color: #000000;">&#0160; </span>glDisableVertexAttribArray<span style="color: #000000;">(</span>GLKVertexAttribNormal<span style="color: #000000;">);</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">}</p>
<p>When the user selects a specific model, then apart from storing the geomety information, we also need to set up the view direction so that it is looking at the center of the model:</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #008423;">// Needed to update the aspect when the view dimension changes</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #008423;">// i.e. the user rotates the device - used in didRotate()</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">- (<span style="color: #be299d;">void</span>)updateTransformation</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">{</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #79482e;">#define M_TAU (<span style="color: #2f2fd1;">2</span>*M_PI)</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; <span style="color: #be299d;">float</span> aspect =&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #733ea4;"><span style="color: #000000;">&#0160; &#0160; </span><span style="color: #401f7d;">fabsf</span><span style="color: #000000;">(</span><span style="color: #be299d;">self</span><span style="color: #000000;">.</span>view<span style="color: #000000;">.</span>bounds<span style="color: #000000;">.</span>size<span style="color: #000000;">.</span>width<span style="color: #000000;"> / </span><span style="color: #be299d;">self</span><span style="color: #000000;">.</span>view<span style="color: #000000;">.</span>bounds<span style="color: #000000;">.</span>size<span style="color: #000000;">.</span>height<span style="color: #000000;">);</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #008423;"><span style="color: #000000;">&#0160; </span>// float fovyRadians, float aspect, float nearZ, float farZ</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #733ea4;"><span style="color: #000000;">&#0160; </span>GLKMatrix4<span style="color: #000000;"> mx =&#0160;</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #401f7d;"><span style="color: #000000;">&#0160; &#0160; </span>GLKMatrix4MakePerspective<span style="color: #000000;">(</span><span style="color: #2f2fd1;">0.1</span><span style="color: #000000;"> * </span><span style="color: #79482e;">M_TAU</span><span style="color: #000000;">, aspect, </span><span style="color: #2f2fd1;">2</span><span style="color: #000000;">, -</span><span style="color: #2f2fd1;">1</span><span style="color: #000000;">);</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #733ea4;"><span style="color: #000000;">&#0160; </span><span style="color: #4d8186;">_baseEffect</span><span style="color: #000000;">.</span>transform<span style="color: #000000;">.</span>projectionMatrix<span style="color: #000000;"> = mx;</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">}</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">- (<span style="color: #be299d;">void</span>)initViewDirection</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">{ &#0160; &#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #401f7d;"><span style="color: #000000;">&#0160; </span><span style="color: #4d8186;">distance</span><span style="color: #000000;"> = </span>GLKVector3Distance<span style="color: #000000;">(</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #4d8186;"><span style="color: #000000;">&#0160; &#0160; </span><span style="color: #401f7d;">GLKVector3Make</span><span style="color: #000000;">(</span>_minPt<span style="color: #000000;">.</span><span style="color: #733ea4;">x</span><span style="color: #000000;">, </span>_minPt<span style="color: #000000;">.</span><span style="color: #733ea4;">y</span><span style="color: #000000;">, </span>_minPt<span style="color: #000000;">.</span><span style="color: #733ea4;">z</span><span style="color: #000000;">),&#0160;</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; &#0160; <span style="color: #401f7d;">GLKVector3Make</span>(<span style="color: #4d8186;">_maxPt</span>.<span style="color: #733ea4;">x</span>, <span style="color: #4d8186;">_maxPt</span>.<span style="color: #733ea4;">y</span>, <span style="color: #4d8186;">_maxPt</span>.<span style="color: #733ea4;">z</span>)) * <span style="color: #2f2fd1;">2</span>;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; <span style="color: #733ea4;">GLKVector3</span> centerToEye = <span style="color: #401f7d;">GLKVector3Make</span>(<span style="color: #2f2fd1;">0</span>, -<span style="color: #4d8186;">distance</span>, <span style="color: #2f2fd1;">0</span>);</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; <span style="color: #733ea4;">GLKVector3</span> eye = <span style="color: #401f7d;">GLKVector3Add</span>(<span style="color: #4d8186;">_centerPt</span>, centerToEye);</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #733ea4;"><span style="color: #000000;">&#0160; </span><span style="color: #4d8186;">_baseEffect</span><span style="color: #000000;">.</span>transform<span style="color: #000000;">.</span>modelviewMatrix<span style="color: #000000;"> =&#0160;</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #401f7d;"><span style="color: #000000;">&#0160; </span>GLKMatrix4MakeLookAt<span style="color: #000000;">(</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160;&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; eye.<span style="color: #733ea4;">x</span>, eye.<span style="color: #733ea4;">y</span>, eye.<span style="color: #733ea4;">z</span>,</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160;&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: #4d8186;">_centerPt</span>.<span style="color: #733ea4;">x</span>, <span style="color: #4d8186;">_centerPt</span>.<span style="color: #733ea4;">y</span>, <span style="color: #4d8186;">_centerPt</span>.<span style="color: #733ea4;">z</span>,</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160;&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: #2f2fd1;">0</span>, <span style="color: #2f2fd1;">0</span>, <span style="color: #2f2fd1;">1</span>); &#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #30595d;"><span style="color: #000000;">&#0160; [</span><span style="color: #be299d;">self</span><span style="color: #000000;"> </span>updateTransformation<span style="color: #000000;">];</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">}</p>
<p>In the next post we&#39;ll see how to add view transformations when the user makes pinch, pan or rotate gestures on the device.</p>
