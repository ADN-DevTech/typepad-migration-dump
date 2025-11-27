---
layout: "post"
title: "Creating a 3D viewer for our Apollonian service using iOS &ndash; Part 3"
date: "2012-06-01 09:01:00"
author: "Kean Walmsley"
categories:
  - "iOS"
  - "JSON"
  - "Mobile"
  - "REST"
original_url: "https://www.keanw.com/2012/06/creating-a-3d-viewer-for-our-apollonian-service-using-ios-part-3-1.html "
typepad_basename: "creating-a-3d-viewer-for-our-apollonian-service-using-ios-part-3-1"
typepad_status: "Publish"
---

<p>After starting <a href="http://through-the-interface.typepad.com/through_the_interface/2012/05/creating-a-3d-viewer-for-our-apollonian-service-using-ios-part-1.html" target="_blank">the sub-series</a> <a href="http://through-the-interface.typepad.com/through_the_interface/2012/05/creating-a-3d-viewer-for-our-apollonian-service-using-ios-part-2.html" target="_blank">focused on iOS</a>, I held off completing it until I could actually test the code on a physical iOS device. A big thanks to Andy Chang from our Toronto office for getting me set up with the ADP membership and my iPad 2 added to the list of usable development devices.</p>
<p>I won’t talk about the steps needed to provision apps for iOS devices – there seems to be enough information available on the web for that – but I will say it ended up being less complicated than I expected. That’s not to say it’s easy to package things up for posting to the App Store – I haven’t gone through that, myself – but just deploying the app to a physical device for testing was reasonably straightforward.</p>
<p>That said, there was some non-trivial work needed to get an app working on both the iOS simulator and a physical device, mainly due to our use of the iSGL3D. The issue came, in this case, from the fact that <a href="https://groups.google.com/group/isgl3d/browse_thread/thread/549712a3c4ab97be" target="_blank">iSGL3D has a problem with ARC (Automatic Reference Counting)</a>. The path of least resistance ended up being to <a href="http://blog.boreal-kiss.net/2011/03/15/how-to-create-universal-static-libraries-on-xcode-4/" target="_blank">build it into a universal, static library</a>, which could then be used to build an iOS app that targets both the iOS Simulator (which is i386-based, as it works on the Mac) and the physical, ARM-based iOS device.</p>
<p>I’m still not very happy with the lighting I was able to get working using iSGL3D: the “out-of-the-box” lighting capabilities – presumably built into the base material types – were much nicer with Three.js or Rajawali. I did my best by reading up on <a href="http://isgl3d.com/tutorials/3/tutorial_2_lighting_and_shading" target="_blank">iSGL3D lighting</a>, <a href="http://www.glprogramming.com/red/chapter05.html" target="_blank">OpenGL lighting</a> and even going <a href="http://www.youtube.com/watch?v=RjA_sC4bCAM" target="_blank">back to basics on diffuse, ambient and specular lighting</a>, but the results weren’t as good as I’d have liked. This is probably down to me, rather than the iSGL3D framework, per se, but still.</p>
<p>Ah yes, and I also integrated <a href="http://stackoverflow.com/questions/7905432/how-to-get-orientation-dependent-height-and-width-of-the-screen" target="_blank">some code to determine the size of the screen depending on the orientation</a> – mainly so I could implement a tap-based UI for changing level (tap at the top: go up a level, tap at the bottom: go down a level).</p>
<p>Here’s the latest version in action on the iOS simulator (which turns out to be comparable to the physical device, graphics-wise, even if the framerate is indeed much lower):</p>
<p>
<object data="http://through-the-interface.typepad.com/presentations/jingswfplayer.swf" height="363" id="scPlayer" type="application/x-shockwave-flash" width="470">
<param name="movie" value="http://through-the-interface.typepad.com/presentations/jingswfplayer.swf" />
<param name="quality" value="high" />
<param name="bgcolor" value="#FFFFFF" />
<param name="flashVars" value="thumb=http://through-the-interface.typepad.com/presentations/ApollonianFirstFrame7.jpg&amp;containerwidth=470&amp;containerheight=363&amp;content=http://through-the-interface.typepad.com/presentations/Apollonian%20Viewer%2for%20iOS%20with%20lighting.swf&amp;blurover=false" />
<param name="allowFullScreen" value="true" />
<param name="scale" value="showall" />
<param name="allowScriptAccess" value="always" />
<param name="base" value="http://through-the-interface.typepad.com/presentations/" /> Unable to display content. Adobe Flash is required.
</object>
</p>
<p>Here’s <a href="http://through-the-interface.typepad.com/files/ApollonianViewer-iOS2.zip" target="_blank">the updated source project</a> and the updated Objective-C code for the main implementation file:</p>
<p class="p4">//</p>
<p class="p4">//<span>&#0160; </span>ApollonianViewer.m</p>
<p class="p4">//<span>&#0160; </span>Apollonian Viewer</p>
<p class="p4">//</p>
<p class="p4">//<span>&#0160; </span>Created by Kean on 5/4/12.</p>
<p class="p4">//<span>&#0160; </span>Copyright 2012 Autodesk. All rights reserved.</p>
<p class="p4">//</p>
<p class="p2">&#0160;</p>
<p class="p1"><span class="s1">#import </span>&quot;ApollonianViewer.h&quot;</p>
<p class="p1"><span class="s1">#import </span>&quot;UIApplication+AppDimensions.h&quot;</p>
<p class="p2">&#0160;</p>
<p class="p3"><span class="s2">@implementation</span> ApollonianViewer</p>
<p class="p2">&#0160;</p>
<p class="p4">// Our level number</p>
<p class="p2">&#0160;</p>
<p class="p3"><span class="s2">int</span> _level = <span class="s7">5</span>;</p>
<p class="p3"><span class="s2">int</span> minLevel = <span class="s7">1</span>;</p>
<p class="p3"><span class="s2">int</span> maxLevel = <span class="s7">10</span>;</p>
<p class="p2">&#0160;</p>
<p class="p3"><span class="s2">bool</span> _accessing = <span class="s2">false</span>;</p>
<p class="p2">&#0160;</p>
<p class="p4">// Our data member for the received data</p>
<p class="p2">&#0160;</p>
<p class="p3"><span class="s3">NSMutableData</span> * _receivedData = <span class="s2">NULL</span>;</p>
<p class="p2">&#0160;</p>
<p class="p4">// A response has been received from our web-service call</p>
<p class="p2">&#0160;</p>
<p class="p3">- (<span class="s2">void</span>)connection:(<span class="s3">NSURLConnection</span> *)connection</p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span>didReceiveResponse:(<span class="s3">NSURLResponse</span> *)response</p>
<p class="p3">{</p>
<p class="p4"><span class="s4"><span>&#0160; </span></span>// Initialise our member variable receiving data</p>
<p class="p2">&#0160;</p>
<p class="p5"><span class="s4"><span>&#0160; </span></span><span class="s2">if</span><span class="s4"> (</span>_receivedData<span class="s4"> == </span><span class="s2">NULL</span><span class="s4">)</span></p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span><span class="s5">_receivedData</span> = [[<span class="s3">NSMutableData</span> <span class="s6">alloc</span>] <span class="s6">init</span>];</p>
<p class="p6"><span class="s4"><span>&#0160; </span></span>else</p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160; </span>[</span>_receivedData<span class="s4"> </span><span class="s6">setLength</span><span class="s4">:</span><span class="s7">0</span><span class="s4">];</span></p>
<p class="p3">}</p>
<p class="p2">&#0160;</p>
<p class="p4">// Data has been received from our web-service call</p>
<p class="p2">&#0160;</p>
<p class="p3">- (<span class="s2">void</span>)connection:(<span class="s3">NSURLConnection</span> *)connection</p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span>didReceiveData:(<span class="s3">NSData</span> *)data</p>
<p class="p3">{</p>
<p class="p4"><span class="s4"><span>&#0160; </span></span>// Append the received data to our member</p>
<p class="p2"><span>&#0160; </span></p>
<p class="p5"><span class="s4"><span>&#0160; </span>[</span>_receivedData<span class="s4"> </span><span class="s6">appendData</span><span class="s4">:data];</span></p>
<p class="p3">}</p>
<p class="p2">&#0160;</p>
<p class="p4">// The web-service connection failed</p>
<p class="p2">&#0160;</p>
<p class="p3">- (<span class="s2">void</span>)connection:(<span class="s3">NSURLConnection</span> *)connection</p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span>didFailWithError:(<span class="s3">NSError</span> *)error</p>
<p class="p3">{</p>
<p class="p4"><span class="s4"><span>&#0160; </span></span>// Report an error in the log</p>
<p class="p2"><span>&#0160; </span></p>
<p class="p1"><span class="s4"><span>&#0160; </span></span><span class="s6">NSLog</span><span class="s4">(</span>@&quot;Connection failed: %@&quot;<span class="s4">, [error </span><span class="s6">description</span><span class="s4">]);</span></p>
<p class="p2">&#0160;</p>
<p class="p3"><span>&#0160; </span><span class="s3">UIAlertView</span>* alert =</p>
<p class="p8"><span class="s4"><span>&#0160;&#0160;&#0160; </span>[[[</span>UIAlertView<span class="s4"> </span><span class="s6">alloc</span><span class="s4">]</span></p>
<p class="p1"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></span><span class="s6">initWithTitle</span><span class="s4">:</span>@&quot;Apollonian Viewer&quot;</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s6">message</span>:</p>
<p class="p1"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>@&quot;Unable to access the web-service. &quot;</p>
<p class="p1"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>&quot;Please check you have internet connectivity.&quot;</p>
<p class="p7"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>delegate<span class="s4">:</span><span class="s2">self</span></p>
<p class="p7"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>cancelButtonTitle<span class="s4">:</span><span class="s8">@&quot;Close&quot;</span></p>
<p class="p7"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>otherButtonTitles<span class="s4">:</span><span class="s2">nil</span></p>
<p class="p7"><span class="s4"><span>&#0160;&#0160;&#0160; </span>] </span>autorelease<span class="s4">];</span></p>
<p class="p3"><span>&#0160; </span>[alert <span class="s6">show</span>];</p>
<p class="p2">&#0160;</p>
<p class="p5"><span class="s4"><span>&#0160; </span></span>_accessing<span class="s4"> = </span><span class="s2">false</span><span class="s4">;</span></p>
<p class="p3">}</p>
<p class="p2">&#0160;</p>
<p class="p3">- (<span class="s2">void</span>)alertView</p>
<p class="p3"><span>&#0160; </span>:(<span class="s3">UIAlertView</span> *)alertView</p>
<p class="p3"><span>&#0160; </span>clickedButtonAtIndex:(<span class="s3">NSInteger</span>)buttonIndex</p>
<p class="p3">{</p>
<p class="p3"><span>&#0160; </span><span class="s2">if</span> (buttonIndex == <span class="s7">0</span>) <span class="s6">exit</span> (<span class="s7">0</span>); <span>&#0160;</span></p>
<p class="p3">}</p>
<p class="p2">&#0160;</p>
<p class="p3">- (<span class="s2">void</span>)extract_spheres</p>
<p class="p3"><span>&#0160; </span>:(<span class="s3">NSString</span> *)responseString</p>
<p class="p3"><span>&#0160; </span>onlyOuter: (<span class="s3">Boolean</span>)outer</p>
<p class="p3">{</p>
<p class="p4"><span class="s4"><span>&#0160; </span></span>// Extract JSON data from our response string</p>
<p class="p2"><span>&#0160; </span></p>
<p class="p3"><span>&#0160; </span><span class="s3">NSData</span> * jsonData =</p>
<p class="p3"><span>&#0160; </span>[responseString</p>
<p class="p7"><span class="s4"><span>&#0160;&#0160; </span></span>dataUsingEncoding<span class="s4">:</span>NSUTF8StringEncoding<span class="s4">];</span></p>
<p class="p2"><span>&#0160; </span></p>
<p class="p4"><span class="s4"><span>&#0160; </span></span>// Extract an array from our JSON data</p>
<p class="p2"><span>&#0160; </span></p>
<p class="p3"><span>&#0160; </span><span class="s3">NSError</span> * e = <span class="s2">nil</span>;</p>
<p class="p3"><span>&#0160; </span><span class="s3">NSArray</span> * jsonArray =</p>
<p class="p8"><span class="s4"><span>&#0160;&#0160;&#0160; </span>[</span>NSJSONSerialization</p>
<p class="p7"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>JSONObjectWithData<span class="s4">: jsonData</span></p>
<p class="p7"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>options<span class="s4">: </span>NSJSONReadingMutableContainers</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s6">error</span>: &amp;e</p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span>];</p>
<p class="p2"><span>&#0160; </span></p>
<p class="p3"><span>&#0160; </span><span class="s2">if</span> (!jsonArray)</p>
<p class="p3"><span>&#0160; </span>{</p>
<p class="p1"><span class="s4"><span>&#0160;&#0160;&#0160; </span></span><span class="s6">NSLog</span><span class="s4">(</span>@&quot;Error parsing JSON: %@&quot;<span class="s4">, e);</span></p>
<p class="p3"><span>&#0160; </span>}</p>
<p class="p6"><span class="s4"><span>&#0160; </span></span>else</p>
<p class="p3"><span>&#0160; </span>{</p>
<p class="p4"><span class="s4"><span>&#0160;&#0160;&#0160; </span></span>// Loop through our JSON array, extracting spheres</p>
<p class="p2"><span>&#0160;&#0160;&#0160; </span></p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span><span class="s2">for</span> (<span class="s3">NSDictionary</span> *item <span class="s2">in</span> jsonArray)</p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span>{</p>
<p class="p4"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>// We&#39;ll need this data for each sphere</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s2">double</span> x, y, z, radius;</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s2">int</span> level;</p>
<p class="p2"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p class="p4"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>// We use a single NSNumber to extract the data</p>
<p class="p2"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s3">NSNumber</span> *num;</p>
<p class="p2"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span>num = [item <span class="s6">objectForKey</span>:<span class="s8">@&quot;X&quot;</span>];</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span>x = [num <span class="s6">doubleValue</span>];</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span>num = [item <span class="s6">objectForKey</span>:<span class="s8">@&quot;Y&quot;</span>];</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span>y = [num <span class="s6">doubleValue</span>];</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span>num = [item <span class="s6">objectForKey</span>:<span class="s8">@&quot;Z&quot;</span>];</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span>z = [num <span class="s6">doubleValue</span>];</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span>num = [item <span class="s6">objectForKey</span>:<span class="s8">@&quot;R&quot;</span>];</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span>radius = [num <span class="s6">doubleValue</span>];</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span>num = [item <span class="s6">objectForKey</span>:<span class="s8">@&quot;L&quot;</span>];</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span>level = [num <span class="s6">intValue</span>];</p>
<p class="p2"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p class="p4"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>// Only create spheres for those at the edge of the</p>
<p class="p4"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>// outer sphere</p>
<p class="p2"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s2">double</span> length = <span class="s6">sqrt</span>(x*x + y*y + z*z);</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s2">if</span> (!outer || (length + radius &gt; <span class="s7">0.99f</span>))</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span>[<span class="s2">self</span> <span class="s9">createSphere</span>:radius <span class="s9">x</span>:x <span class="s9">y</span>:y <span class="s9">z</span>:z <span class="s9">level</span>:level];</p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span>}</p>
<p class="p2">&#0160;</p>
<p class="p4"><span class="s4"><span>&#0160;&#0160;&#0160; </span></span>// Trigger the rotation updates</p>
<p class="p2"><span>&#0160;&#0160;&#0160; </span></p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span>[<span class="s2">self</span> <span class="s9">schedule</span>:<span class="s2">@selector</span>(tick:)];</p>
<p class="p3"><span>&#0160; </span>}</p>
<p class="p3">}</p>
<p class="p2">&#0160;</p>
<p class="p4">// The call to our web-service has completed</p>
<p class="p2">&#0160;</p>
<p class="p3">- (<span class="s2">void</span>)connectionDidFinishLoading</p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span>:(<span class="s3">NSURLConnection</span> *)connection</p>
<p class="p3">{</p>
<p class="p4"><span class="s4"><span>&#0160; </span></span>// Release the connection</p>
<p class="p2"><span>&#0160; </span></p>
<p class="p3"><span>&#0160; </span>[connection <span class="s6">release</span>];</p>
<p class="p2"><span>&#0160; </span></p>
<p class="p4"><span class="s4"><span>&#0160; </span></span>// Get the response string from our data member then</p>
<p class="p4"><span class="s4"><span>&#0160; </span></span>// release it</p>
<p class="p2"><span>&#0160; </span></p>
<p class="p3"><span>&#0160; </span><span class="s3">NSString</span> * responseString =</p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span>[[<span class="s3">NSString</span> <span class="s6">alloc</span>]</p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></span><span class="s6">initWithData</span><span class="s4">:</span>_receivedData</p>
<p class="p7"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>encoding<span class="s4">:</span>NSUTF8StringEncoding</p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span>];</p>
<p class="p5"><span class="s4"><span>&#0160; </span>[</span>_receivedData<span class="s4"> </span><span class="s6">release</span><span class="s4">];</span></p>
<p class="p2">&#0160;</p>
<p class="p9"><span class="s4"><span>&#0160; </span>[</span><span class="s2">self</span><span class="s4"> </span>extract_spheres<span class="s4">:responseString </span>onlyOuter<span class="s4">:</span><span class="s2">true</span><span class="s4">];</span></p>
<p class="p2"><span>&#0160; </span></p>
<p class="p5"><span class="s4"><span>&#0160; </span></span>_accessing<span class="s4"> = </span><span class="s2">false</span><span class="s4">;</span></p>
<p class="p3">}</p>
<p class="p2">&#0160;</p>
<p class="p3">- (<span class="s2">void</span>)ask_for_spheres:(<span class="s2">int</span>)level</p>
<p class="p3">{</p>
<p class="p5"><span class="s4"><span>&#0160; </span></span><span class="s2">if</span><span class="s4"> (</span>_accessing<span class="s4">)</span></p>
<p class="p6"><span class="s4"><span>&#0160;&#0160;&#0160; </span></span>return<span class="s4">;</span></p>
<p class="p2"><span>&#0160; </span></p>
<p class="p5"><span class="s4"><span>&#0160; </span></span>_accessing<span class="s4"> = </span><span class="s2">true</span><span class="s4">;</span></p>
<p class="p2"><span>&#0160; </span></p>
<p class="p4"><span class="s4"><span>&#0160; </span></span>// First we clear any existing spheres</p>
<p class="p2"><span>&#0160; </span></p>
<p class="p5"><span class="s4"><span>&#0160; </span>[</span>_container<span class="s4"> </span><span class="s9">clearAll</span><span class="s4">];</span></p>
<p class="p2">&#0160;</p>
<p class="p4"><span class="s4"><span>&#0160; </span></span>// Set up our web-service call</p>
<p class="p2"><span>&#0160; </span></p>
<p class="p3"><span>&#0160; </span><span class="s3">NSURL</span> * url =</p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span>[<span class="s3">NSURL</span></p>
<p class="p7"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>URLWithString<span class="s4">:</span></p>
<p class="p8"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span>[</span>NSString</p>
<p class="p7"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>stringWithFormat<span class="s4">:</span></p>
<p class="p1"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>@&quot;http://apollonian.cloudapp.net/api/spheres/1/%d&quot;<span class="s4">,</span></p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span>level</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span>]</p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span>];</p>
<p class="p2"><span>&#0160; </span></p>
<p class="p8"><span class="s4"><span>&#0160; </span></span>NSMutableURLRequest<span class="s4"> *request =</span></p>
<p class="p8"><span class="s4"><span>&#0160;&#0160;&#0160; </span>[</span>NSMutableURLRequest</p>
<p class="p7"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>requestWithURL<span class="s4">:url</span></p>
<p class="p7"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>cachePolicy<span class="s4">:</span>NSURLRequestUseProtocolCachePolicy</p>
<p class="p7"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>timeoutInterval<span class="s4">:</span><span class="s7">60.0</span></p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span>];</p>
<p class="p2"><span>&#0160; </span></p>
<p class="p3"><span>&#0160; </span>[request <span class="s6">setHTTPMethod</span>:<span class="s8">@&quot;GET&quot;</span>];</p>
<p class="p2"><span>&#0160; </span></p>
<p class="p3"><span>&#0160; </span><span class="s3">NSURLConnection</span> * connection =</p>
<p class="p7"><span class="s4"><span>&#0160;&#0160;&#0160; </span>[[</span><span class="s3">NSURLConnection</span><span class="s4"> </span>alloc<span class="s4">]</span>initWithRequest<span class="s4">:request </span>delegate<span class="s4">:</span><span class="s2">self</span><span class="s4">];</span></p>
<p class="p3"><span>&#0160; </span><span class="s2">if</span> (connection)</p>
<p class="p3"><span>&#0160; </span>{</p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span><span class="s5">_receivedData</span> = [[<span class="s3">NSMutableData</span> <span class="s6">data</span>] <span class="s6">retain</span>];</p>
<p class="p3"><span>&#0160; </span>}</p>
<p class="p3">}</p>
<p class="p2">&#0160;</p>
<p class="p4">// Our main scene initialization method</p>
<p class="p2">&#0160;</p>
<p class="p3">- (<span class="s2">id</span>) init</p>
<p class="p3">{ <span>&#0160;</span></p>
<p class="p3"><span>&#0160; </span><span class="s2">if</span> ((<span class="s2">self</span> = [<span class="s2">super</span> <span class="s9">init</span>]))</p>
<p class="p3"><span>&#0160; </span>{</p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span><span class="s5">_preSpin</span> = <span class="s2">true</span>;</p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span><span class="s5">_paused</span> = <span class="s2">false</span>;</p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160; </span></span>_rotation<span class="s4"> = </span><span class="s7">0.0</span><span class="s4">;</span></p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160; </span></span>_initialSpinAmount<span class="s4"> = </span><span class="s7">2</span><span class="s4">;</span></p>
<p class="p2"><span>&#0160;&#0160;&#0160; </span></p>
<p class="p4"><span class="s4"><span>&#0160;&#0160;&#0160; </span></span>// Create recognizers handling scene-level gestures</p>
<p class="p2"><span>&#0160;&#0160;&#0160; </span></p>
<p class="p4"><span class="s4"><span>&#0160;&#0160;&#0160; </span></span>// Tap</p>
<p class="p2"><span>&#0160;&#0160;&#0160; </span></p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160; </span></span>_tapGestureRecognizer<span class="s4"> =</span></p>
<p class="p8"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span>[[</span>UITapGestureRecognizer<span class="s4"> </span><span class="s6">alloc</span><span class="s4">]</span></p>
<p class="p7"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>initWithTarget<span class="s4">:</span><span class="s2">self</span></p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s6">action</span>:<span class="s2">@selector</span>(tapGesture:)</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span>];</p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160; </span></span>_tapGestureRecognizer<span class="s4">.</span><span class="s3">delegate</span><span class="s4"> = </span><span class="s2">self</span><span class="s4">; <span>&#0160;&#0160; </span></span></p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160; </span>[[</span>Isgl3dDirector<span class="s4"> </span><span class="s9">sharedInstance</span><span class="s4">]</span></p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></span><span class="s9">addGestureRecognizer</span><span class="s4">:</span>_tapGestureRecognizer</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s9">forNode</span>:<span class="s2">nil</span></p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span>];</p>
<p class="p2"><span>&#0160;&#0160;&#0160; </span></p>
<p class="p4"><span class="s4"><span>&#0160;&#0160;&#0160; </span></span>// Swipe</p>
<p class="p2">&#0160;</p>
<p class="p4"><span class="s4"><span>&#0160;&#0160;&#0160; </span></span>// (Add a recognizer for each of 4 directions)</p>
<p class="p2">&#0160;</p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160; </span></span>_swipeLeftGestureRecognizer<span class="s4"> =</span></p>
<p class="p8"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span>[[</span>UISwipeGestureRecognizer<span class="s4"> </span><span class="s6">alloc</span><span class="s4">]</span></p>
<p class="p7"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>initWithTarget<span class="s4">:</span><span class="s2">self</span></p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s6">action</span>:<span class="s2">@selector</span>(swipeGesture:)</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span>];</p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160; </span></span>_swipeLeftGestureRecognizer<span class="s4">.</span><span class="s3">delegate</span><span class="s4"> = </span><span class="s2">self</span><span class="s4">;</span></p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160; </span>[</span>_swipeLeftGestureRecognizer</p>
<p class="p7"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>setDirection<span class="s4">:</span>UISwipeGestureRecognizerDirectionLeft</p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span>]; <span>&#0160;&#0160; </span></p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160; </span>[[</span>Isgl3dDirector<span class="s4"> </span><span class="s9">sharedInstance</span><span class="s4">]</span></p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></span><span class="s9">addGestureRecognizer</span><span class="s4">:</span>_swipeLeftGestureRecognizer</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s9">forNode</span>:<span class="s2">nil</span></p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span>];</p>
<p class="p2"><span>&#0160;&#0160;&#0160; </span></p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160; </span></span>_swipeRightGestureRecognizer<span class="s4"> =</span></p>
<p class="p8"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span>[[</span>UISwipeGestureRecognizer<span class="s4"> </span><span class="s6">alloc</span><span class="s4">]</span></p>
<p class="p7"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>initWithTarget<span class="s4">:</span><span class="s2">self</span></p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s6">action</span>:<span class="s2">@selector</span>(swipeGesture:)</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span>];</p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160; </span></span>_swipeRightGestureRecognizer<span class="s4">.</span><span class="s3">delegate</span><span class="s4"> = </span><span class="s2">self</span><span class="s4">;</span></p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160; </span>[</span>_swipeRightGestureRecognizer</p>
<p class="p7"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>setDirection<span class="s4">:</span>UISwipeGestureRecognizerDirectionRight</p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span>]; <span>&#0160;&#0160; </span></p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160; </span>[[</span>Isgl3dDirector<span class="s4"> </span><span class="s9">sharedInstance</span><span class="s4">]</span></p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></span><span class="s9">addGestureRecognizer</span><span class="s4">:</span>_swipeRightGestureRecognizer</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s9">forNode</span>:<span class="s2">nil</span></p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span>];</p>
<p class="p2"><span>&#0160;&#0160;&#0160; </span></p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160; </span></span>_swipeUpGestureRecognizer<span class="s4"> =</span></p>
<p class="p8"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span>[[</span>UISwipeGestureRecognizer<span class="s4"> </span><span class="s6">alloc</span><span class="s4">]</span></p>
<p class="p7"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>initWithTarget<span class="s4">:</span><span class="s2">self</span></p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s6">action</span>:<span class="s2">@selector</span>(swipeGesture:)</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span>];</p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160; </span></span>_swipeUpGestureRecognizer<span class="s4">.</span><span class="s3">delegate</span><span class="s4"> = </span><span class="s2">self</span><span class="s4">;</span></p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160; </span>[</span>_swipeUpGestureRecognizer</p>
<p class="p7"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>setDirection<span class="s4">:</span>UISwipeGestureRecognizerDirectionUp</p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span>]; <span>&#0160;&#0160; </span></p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160; </span>[[</span>Isgl3dDirector<span class="s4"> </span><span class="s9">sharedInstance</span><span class="s4">]</span></p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></span><span class="s9">addGestureRecognizer</span><span class="s4">:</span>_swipeUpGestureRecognizer</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s9">forNode</span>:<span class="s2">nil</span></p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span>];</p>
<p class="p2">&#0160;</p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160; </span></span>_swipeDownGestureRecognizer<span class="s4"> =</span></p>
<p class="p8"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span>[[</span>UISwipeGestureRecognizer<span class="s4"> </span><span class="s6">alloc</span><span class="s4">]</span></p>
<p class="p7"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>initWithTarget<span class="s4">:</span><span class="s2">self</span></p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s6">action</span>:<span class="s2">@selector</span>(swipeGesture:)</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span>];</p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160; </span></span>_swipeDownGestureRecognizer<span class="s4">.</span><span class="s3">delegate</span><span class="s4"> = </span><span class="s2">self</span><span class="s4">;</span></p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160; </span>[</span>_swipeDownGestureRecognizer</p>
<p class="p7"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>setDirection<span class="s4">:</span>UISwipeGestureRecognizerDirectionDown</p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span>]; <span>&#0160;&#0160; </span></p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160; </span>[[</span>Isgl3dDirector<span class="s4"> </span><span class="s9">sharedInstance</span><span class="s4">]</span></p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></span><span class="s9">addGestureRecognizer</span><span class="s4">:</span>_swipeDownGestureRecognizer</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s9">forNode</span>:<span class="s2">nil</span></p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span>];</p>
<p class="p2"><span>&#0160;&#0160;&#0160; </span></p>
<p class="p4"><span class="s4"><span>&#0160;&#0160;&#0160; </span></span>// Pinch</p>
<p class="p2"><span>&#0160;&#0160;&#0160; </span></p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160; </span></span>_pinchGestureRecognizer<span class="s4"> =</span></p>
<p class="p8"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span>[[</span>UIPinchGestureRecognizer<span class="s4"> </span><span class="s6">alloc</span><span class="s4">]</span></p>
<p class="p7"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>initWithTarget<span class="s4">:</span><span class="s2">self</span></p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s6">action</span>:<span class="s2">@selector</span>(pinchGesture:)</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span>];</p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160; </span></span>_pinchGestureRecognizer<span class="s4">.</span><span class="s3">delegate</span><span class="s4"> = </span><span class="s2">self</span><span class="s4">; <span>&#0160;&#0160; </span></span></p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160; </span>[[</span>Isgl3dDirector<span class="s4"> </span><span class="s9">sharedInstance</span><span class="s4">]</span></p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></span><span class="s9">addGestureRecognizer</span><span class="s4">:</span>_pinchGestureRecognizer</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s9">forNode</span>:<span class="s2">nil</span></p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span>];</p>
<p class="p2"><span>&#0160;&#0160;&#0160; </span></p>
<p class="p4"><span class="s4"><span>&#0160;&#0160;&#0160; </span></span>// Rotate</p>
<p class="p2"><span>&#0160;&#0160;&#0160; </span></p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160; </span></span>_rotationGestureRecognizer<span class="s4"> =</span></p>
<p class="p8"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span>[[</span>UIRotationGestureRecognizer<span class="s4"> </span><span class="s6">alloc</span><span class="s4">]</span></p>
<p class="p7"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>initWithTarget<span class="s4">:</span><span class="s2">self</span></p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s6">action</span>:<span class="s2">@selector</span>(rotationGesture:)</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span>];</p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160; </span></span>_rotationGestureRecognizer<span class="s4">.</span><span class="s3">delegate</span><span class="s4"> = </span><span class="s2">self</span><span class="s4">; <span>&#0160;&#0160; </span></span></p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160; </span>[[</span>Isgl3dDirector<span class="s4"> </span><span class="s9">sharedInstance</span><span class="s4">]</span></p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></span><span class="s9">addGestureRecognizer</span><span class="s4">:</span>_rotationGestureRecognizer</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s9">forNode</span>:<span class="s2">nil</span></p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span>];</p>
<p class="p2"><span>&#0160;&#0160;&#0160; </span></p>
<p class="p9"><span class="s4"><span>&#0160;&#0160;&#0160; </span>[</span><span class="s2">self</span><span class="s4"> </span>ask_for_spheres<span class="s4">:</span><span class="s5">_level</span><span class="s4">];</span></p>
<p class="p2"><span>&#0160;&#0160;&#0160; </span></p>
<p class="p4"><span class="s4"><span>&#0160;&#0160;&#0160; </span></span>// Move the default camera to the initial position</p>
<p class="p2"><span>&#0160;&#0160;&#0160; </span></p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span>[<span class="s2">self</span>.<span class="s5">camera</span> <span class="s9">setPosition</span>:<span class="s1">iv3</span>(<span class="s7">0</span>, <span class="s7">0</span>, -<span class="s7">4</span>)];</p>
<p class="p2"><span>&#0160;&#0160;&#0160; </span></p>
<p class="p4"><span class="s4"><span>&#0160;&#0160;&#0160; </span></span>// Create a container for our spheres</p>
<p class="p2"><span>&#0160;&#0160;&#0160; </span></p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160; </span></span>_container<span class="s4"> = [</span><span class="s2">self</span><span class="s4">.</span>scene<span class="s4"> </span><span class="s9">createNode</span><span class="s4">];</span></p>
<p class="p2">&#0160;</p>
<p class="p4"><span class="s4"><span>&#0160;&#0160;&#0160; </span></span>// We&#39;ll maintain an array of materials for our</p>
<p class="p4"><span class="s4"><span>&#0160;&#0160;&#0160; </span></span>// levels. Define the colors for those levels</p>
<p class="p2"><span>&#0160;&#0160;&#0160; </span></p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span><span class="s3">NSArray</span> * colors =</p>
<p class="p7"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span>[</span><span class="s3">NSArray</span><span class="s4"> </span>arrayWithObjects<span class="s4">:</span></p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s10">/* white */</span> <span>&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s8">@&quot;FFFFFF&quot;</span>,</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s10">/* red */</span> <span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s8">@&quot;FF0000&quot;</span>,</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s10">/* yellow */</span><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s8">@&quot;FFFF00&quot;</span>,</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s10">/* green */</span> <span>&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s8">@&quot;00FF00&quot;</span>,</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s10">/* cyan */</span><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s8">@&quot;00FFFF&quot;</span>,</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s10">/* blue */</span><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s8">@&quot;0000FF&quot;</span>,</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s10">/* magenta */</span> <span>&#0160;&#0160;&#0160; </span><span class="s8">@&quot;FF00FF&quot;</span>,</p>
<p class="p4"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>/* dark gray */<span class="s4"> <span>&#0160; </span></span><span class="s8">@&quot;A9A9A9&quot;</span><span class="s4">,</span></p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s10">/* gray */</span><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s8">@&quot;808080&quot;</span>,</p>
<p class="p4"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>/* light gray */<span class="s4"><span>&#0160; </span></span><span class="s8">@&quot;D3D3D3&quot;</span><span class="s4">,</span></p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s10">/* white */</span> <span>&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s8">@&quot;FFFFFF&quot;</span>, <span class="s2">nil</span></p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span>];</p>
<p class="p2">&#0160;</p>
<p class="p4"><span class="s4"><span>&#0160;&#0160;&#0160; </span></span>// Create and populate the array of materials</p>
<p class="p2"><span>&#0160;&#0160;&#0160; </span></p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span><span class="s5">_materials</span> = [[<span class="s3">NSMutableArray</span> <span class="s6">alloc</span>] <span class="s6">init</span>];</p>
<p class="p2"><span>&#0160;&#0160;&#0160; </span></p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span><span class="s2">for</span> (<span class="s2">int</span> i=<span class="s7">0</span>; i &lt; <span class="s7">12</span>; i++)</p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span>{</p>
<p class="p4"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>// Anything we don&#39;t have a color for will be white</p>
<p class="p2"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s3">NSString</span> *col =</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span>(i &lt;= <span class="s7">10</span>) ? [colors <span class="s6">objectAtIndex</span>:i] : <span class="s8">@&quot;FFFFFF&quot;</span>;</p>
<p class="p2"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p class="p4"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>// We have two entries per material - use one for diffuse</p>
<p class="p4"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>// and specular, th eother for ambient</p>
<p class="p2"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>Isgl3dColorMaterial<span class="s4"> * mat =</span></p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span>[[</span>Isgl3dColorMaterial<span class="s4"> </span><span class="s6">alloc</span><span class="s4">]</span></p>
<p class="p9"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>initWithHexColors<span class="s4">: col</span></p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s9">diffuse</span>: <span class="s8">@&quot;222222&quot;</span></p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s9">specular</span>: col</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s9">shininess</span>:<span class="s7">0.1</span></p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span>];</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span>[<span class="s5">_materials</span> <span class="s6">addObject</span>:mat];</p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span>}</p>
<p class="p2">&#0160;</p>
<p class="p4"><span class="s4"><span>&#0160;&#0160;&#0160; </span></span>// Create a single sphere mesh</p>
<p class="p2"><span>&#0160;&#0160;&#0160; </span></p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160; </span></span>_sphereMesh<span class="s4"> =</span></p>
<p class="p9"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span>[[</span><span class="s5">Isgl3dSphere</span><span class="s4"> </span><span class="s6">alloc</span><span class="s4">] </span>initWithGeometry<span class="s4">:</span><span class="s7">1</span><span class="s4"> </span>longs<span class="s4">:</span><span class="s7">16</span><span class="s4"> </span>lats<span class="s4">:</span><span class="s7">16</span><span class="s4">];</span></p>
<p class="p2"><span>&#0160;&#0160;&#0160; </span></p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span><span class="s2">float</span> leftColors[<span class="s7">3</span>] = { <span class="s7">0.1</span>, <span class="s7">0.4</span>, <span class="s7">0.1</span> };</p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span><span class="s2">float</span> rightColors[<span class="s7">3</span>] = { <span class="s7">0.4</span>, <span class="s7">0.1</span>, <span class="s7">0.4</span> };</p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span><span class="s2">double</span> d = -<span class="s7">7</span>;</p>
<p class="p2">&#0160;</p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span><span class="s5">Isgl3dLight</span> * left =</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span>[<span class="s5">Isgl3dLight</span> <span class="s9">lightWithColorArray</span>:leftColors];</p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span>left.<span class="s5">position</span> = <span class="s1">iv3</span>(-d/<span class="s7">3</span>,d,-d);</p>
<p class="p9"><span class="s4"><span>&#0160;&#0160;&#0160; </span>left.</span><span class="s5">lightType</span><span class="s4"> = </span>DirectionalLight<span class="s4">;</span></p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span>[left <span class="s9">setDirection</span>:<span class="s7">1</span> <span class="s9">y</span>:-<span class="s7">1</span> <span class="s9">z</span>:<span class="s7">1</span>];<span> </span></p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span>[<span class="s2">self</span>.<span class="s5">scene</span> <span class="s9">addChild</span>:left];</p>
<p class="p2"><span>&#0160;&#0160;&#0160; </span></p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span><span class="s5">Isgl3dLight</span> * right =</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span>[<span class="s5">Isgl3dLight</span> <span class="s9">lightWithColorArray</span>:rightColors];</p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span>right.<span class="s5">position</span> = <span class="s1">iv3</span>(d,d,d*<span class="s7">3</span>);</p>
<p class="p9"><span class="s4"><span>&#0160;&#0160;&#0160; </span>right.</span><span class="s5">lightType</span><span class="s4"> = </span>DirectionalLight<span class="s4">;</span></p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span>[right <span class="s9">setDirection</span>:<span class="s7">1</span> <span class="s9">y</span>:-<span class="s7">1</span> <span class="s9">z</span>:<span class="s7">1</span>];<span> </span></p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span>[<span class="s2">self</span>.<span class="s5">scene</span> <span class="s9">addChild</span>:right];</p>
<p class="p2"><span>&#0160; </span></p>
<p class="p4"><span class="s4"><span>&#0160;&#0160;&#0160; </span></span>// Set the scene&#39;s ambient color</p>
<p class="p2"><span>&#0160;&#0160;&#0160; </span></p>
<p class="p9"><span class="s4"><span>&#0160;&#0160;&#0160; </span>[</span><span class="s2">self</span><span class="s4"> </span>setSceneAmbient<span class="s4">:</span><span class="s8">@&quot;444444&quot;</span><span class="s4">]; <span>&#0160;&#0160; </span></span></p>
<p class="p3"><span>&#0160; </span>}</p>
<p class="p6"><span class="s4"><span>&#0160; </span></span>return<span class="s4"> </span>self<span class="s4">;</span></p>
<p class="p3">}</p>
<p class="p2">&#0160;</p>
<p class="p4">// Create a single sphere at the desired position with</p>
<p class="p4">// the desired radius and level</p>
<p class="p2">&#0160;</p>
<p class="p3">- (<span class="s2">void</span>)createSphere</p>
<p class="p3"><span>&#0160; </span>:(<span class="s2">double</span>)radius</p>
<p class="p3"><span>&#0160; </span>x:(<span class="s2">double</span>)x y:(<span class="s2">double</span>)y z:(<span class="s2">double</span>)z</p>
<p class="p3"><span>&#0160; </span>level:(<span class="s2">int</span>)level</p>
<p class="p3">{</p>
<p class="p4"><span class="s4"><span>&#0160; </span></span>// Create the sphere based on our single mesh</p>
<p class="p2"><span>&#0160; </span></p>
<p class="p5"><span class="s4"><span>&#0160; </span></span>Isgl3dMeshNode<span class="s4"> * sphere =</span></p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160; </span>[</span>_container</p>
<p class="p9"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>createNodeWithMesh<span class="s4">:</span><span class="s5">_sphereMesh</span></p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s9">andMaterial</span>:[<span class="s5">_materials</span> <span class="s6">objectAtIndex</span>:level]</p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span>];</p>
<p class="p2"><span>&#0160; </span></p>
<p class="p4"><span class="s4"><span>&#0160; </span></span>// Position and scale it</p>
<p class="p2"><span>&#0160; </span></p>
<p class="p3"><span>&#0160; </span>sphere.<span class="s5">position</span> = <span class="s1">iv3</span>(x, y, z);</p>
<p class="p3"><span>&#0160; </span>[sphere <span class="s9">setScale</span>:radius];</p>
<p class="p3">}</p>
<p class="p2">&#0160;</p>
<p class="p3">- (<span class="s2">void</span>) dealloc</p>
<p class="p3">{</p>
<p class="p4"><span class="s4"><span>&#0160; </span></span>// Make sure we release our materials and sphere mesh</p>
<p class="p2"><span>&#0160; </span></p>
<p class="p5"><span class="s4"><span>&#0160; </span>[</span>_materials<span class="s4"> </span><span class="s6">release</span><span class="s4">];</span></p>
<p class="p5"><span class="s4"><span>&#0160; </span>[</span>_sphereMesh<span class="s4"> </span><span class="s6">release</span><span class="s4">];</span></p>
<p class="p2"><span>&#0160;&#0160;&#0160; </span></p>
<p class="p7"><span class="s4"><span>&#0160; </span>[</span><span class="s2">super</span><span class="s4"> </span>dealloc<span class="s4">];</span></p>
<p class="p3">}</p>
<p class="p2">&#0160;</p>
<p class="p4">// Respond to our timer tick by rotating the model</p>
<p class="p2">&#0160;</p>
<p class="p3">- (<span class="s2">void</span>) tick:(<span class="s2">float</span>)dt</p>
<p class="p3">{</p>
<p class="p4"><span class="s4"><span>&#0160; </span></span>// Rotate around the appropriate axis</p>
<p class="p2">&#0160;</p>
<p class="p5"><span class="s4"><span>&#0160; </span></span><span class="s2">if</span><span class="s4"> (!</span>_paused<span class="s4"> &amp;&amp; !</span>_preSpin<span class="s4">)</span></p>
<p class="p3"><span>&#0160; </span>{</p>
<p class="p4"><span class="s4"><span>&#0160;&#0160;&#0160; </span></span>// Reset any rotation around Z, first</p>
<p class="p2"><span>&#0160;&#0160;&#0160; </span></p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160; </span></span><span class="s2">if</span><span class="s4"> (</span><span class="s6">abs</span><span class="s4">(</span>_container<span class="s4">.</span>rotationZ<span class="s4">) &gt; </span><span class="s7">0</span><span class="s4">)</span></p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span>{</p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>_container<span class="s4">.</span>rotationZ<span class="s4"> = </span><span class="s7">0</span><span class="s4">;</span></p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s5">_rotation</span> = <span class="s7">0</span>;</p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span>}</p>
<p class="p2"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160; </span></span><span class="s2">if</span><span class="s4"> (</span>_spinAroundY<span class="s4">)</span></p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span>{</p>
<p class="p4"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>// If spinning around Y, reset any X rotation</p>
<p class="p2"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></span><span class="s2">if</span><span class="s4"> (</span><span class="s6">abs</span><span class="s4">(</span>_container<span class="s4">.</span>rotationX<span class="s4">) &gt; </span><span class="s7">0</span><span class="s4">)</span></p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>_container<span class="s4">.</span>rotationX<span class="s4"> = </span><span class="s7">0</span><span class="s4">;</span></p>
<p class="p2"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>_container<span class="s4">.</span>rotationY<span class="s4"> += </span>_spinIncrement<span class="s4">;</span></p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span>}</p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span><span class="s2">else</span></p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span>{</p>
<p class="p4"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>// If spinning around X, reset any Y rotation</p>
<p class="p2"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></span><span class="s2">if</span><span class="s4"> (</span><span class="s6">abs</span><span class="s4">(</span>_container<span class="s4">.</span>rotationY<span class="s4">) &gt; </span><span class="s7">0</span><span class="s4">)</span></p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>_container<span class="s4">.</span>rotationY<span class="s4"> = </span><span class="s7">0</span><span class="s4">;</span></p>
<p class="p2"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>_container<span class="s4">.</span>rotationX<span class="s4"> += </span>_spinIncrement<span class="s4">;</span></p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span>}</p>
<p class="p3"><span>&#0160; </span>}</p>
<p class="p3">}</p>
<p class="p2">&#0160;</p>
<p class="p4">// Method to specify combination of gesture recognizers</p>
<p class="p2">&#0160;</p>
<p class="p3">- (<span class="s2">BOOL</span>)gestureRecognizer:</p>
<p class="p3">(<span class="s3">UIGestureRecognizer</span> *)gestureRecognizer</p>
<p class="p3">shouldRecognizeSimultaneouslyWithGestureRecognizer:</p>
<p class="p3">(<span class="s3">UIGestureRecognizer</span> *)otherGestureRecognizer</p>
<p class="p3">{</p>
<p class="p4"><span class="s4"><span>&#0160; </span></span>// If the gesture recognizers are on different views,</p>
<p class="p4"><span class="s4"><span>&#0160; </span></span>// don&#39;t allow simultaneous recognition</p>
<p class="p2"><span>&#0160; </span></p>
<p class="p3"><span>&#0160; </span><span class="s2">if</span> (gestureRecognizer.<span class="s3">view</span> != otherGestureRecognizer.<span class="s3">view</span>)</p>
<p class="p6"><span class="s4"><span>&#0160;&#0160;&#0160; </span></span>return<span class="s4"> </span>NO<span class="s4">;</span></p>
<p class="p2"><span>&#0160; </span></p>
<p class="p4"><span class="s4"><span>&#0160; </span></span>// Also stop combination of rotation with other gestures</p>
<p class="p2"><span>&#0160; </span></p>
<p class="p3"><span>&#0160; </span><span class="s2">if</span> ((gestureRecognizer == <span class="s5">_rotationGestureRecognizer</span>) ||</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span>(otherGestureRecognizer == <span class="s5">_rotationGestureRecognizer</span>))</p>
<p class="p6"><span class="s4"><span>&#0160;&#0160;&#0160; </span></span>return<span class="s4"> </span>NO<span class="s4">;</span></p>
<p class="p2"><span>&#0160; </span></p>
<p class="p6"><span class="s4"><span>&#0160; </span></span>return<span class="s4"> </span>YES<span class="s4">;</span></p>
<p class="p3">}</p>
<p class="p2">&#0160;</p>
<p class="p4">// Action methods for our gestures</p>
<p class="p2">&#0160;</p>
<p class="p4">// Tap-pause/play</p>
<p class="p2">&#0160;</p>
<p class="p3">- (<span class="s2">void</span>)tapGesture</p>
<p class="p3"><span>&#0160; </span>:(<span class="s3">UITapGestureRecognizer</span> *)gestureRecognizer</p>
<p class="p3">{</p>
<p class="p4"><span class="s4"><span>&#0160; </span></span>// If the tap happens near the top or bottom edge,</p>
<p class="p4"><span class="s4"><span>&#0160; </span></span>// change level</p>
<p class="p2"><span>&#0160; </span></p>
<p class="p3"><span>&#0160; </span><span class="s2">int</span> maxDistFromEdge = <span class="s7">50</span>;</p>
<p class="p2">&#0160;</p>
<p class="p3"><span>&#0160; </span><span class="s3">CGPoint</span> tapPoint =</p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span>[gestureRecognizer <span class="s6">locationInView</span>:gestureRecognizer.<span class="s3">view</span>];</p>
<p class="p3"><span>&#0160; </span><span class="s3">CGSize</span> screenSize = [<span class="s3">UIApplication</span> <span class="s9">currentSize</span>];</p>
<p class="p2"><span>&#0160; </span></p>
<p class="p3"><span>&#0160; </span><span class="s2">if</span> (tapPoint.<span class="s3">y</span> &lt; maxDistFromEdge)</p>
<p class="p3"><span>&#0160; </span>{</p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160; </span></span><span class="s2">if</span><span class="s4"> (</span>_level<span class="s4"> &lt; </span>maxLevel<span class="s4">)</span></p>
<p class="p9"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span>[</span><span class="s2">self</span><span class="s4"> </span>ask_for_spheres<span class="s4">:++</span><span class="s5">_level</span><span class="s4">];</span></p>
<p class="p3"><span> </span><span>&#0160; </span>}</p>
<p class="p3"><span>&#0160; </span><span class="s2">else</span> <span class="s2">if</span> (tapPoint.<span class="s3">y</span> &gt; screenSize.<span class="s3">height</span> - maxDistFromEdge)</p>
<p class="p3"><span>&#0160; </span>{</p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160; </span></span><span class="s2">if</span><span class="s4"> (</span>_level<span class="s4"> &gt; </span>minLevel<span class="s4">)</span></p>
<p class="p9"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span>[</span><span class="s2">self</span><span class="s4"> </span>ask_for_spheres<span class="s4">:--</span><span class="s5">_level</span><span class="s4">];</span></p>
<p class="p3"><span>&#0160; </span>}</p>
<p class="p6"><span class="s4"><span>&#0160; </span></span>else</p>
<p class="p3"><span>&#0160; </span>{</p>
<p class="p4"><span class="s4"><span>&#0160;&#0160;&#0160; </span></span>// Toggle pause (if we have already had at least one spin)</p>
<p class="p2"><span>&#0160;&#0160;&#0160; </span></p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span><span class="s2">if</span> (!<span class="s5">_preSpin</span>)</p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>_paused<span class="s4"> = !</span>_paused<span class="s4">;</span></p>
<p class="p3"><span>&#0160; </span>}</p>
<p class="p3">}</p>
<p class="p2">&#0160;</p>
<p class="p4">// Swipe-spin</p>
<p class="p2">&#0160;</p>
<p class="p3">- (<span class="s2">void</span>)swipeGesture</p>
<p class="p3"><span>&#0160; </span>:(<span class="s3">UISwipeGestureRecognizer</span> *)gestureRecognizer</p>
<p class="p3">{</p>
<p class="p3"><span>&#0160; </span><span class="s2">switch</span>(gestureRecognizer.<span class="s3">direction</span>)</p>
<p class="p3"><span>&#0160; </span>{ <span>&#0160;&#0160; </span></p>
<p class="p7"><span class="s4"><span>&#0160;&#0160;&#0160; </span></span><span class="s2">case</span><span class="s4"> </span>UISwipeGestureRecognizerDirectionDown<span class="s4">:</span></p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s2">if</span> (</p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>_preSpin<span class="s4"> || </span>_spinAroundY<span class="s4"> ||</span></p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span>(!</span>_spinAroundY<span class="s4"> &amp;&amp; </span>_spinIncrement<span class="s4"> &gt; </span><span class="s7">0</span><span class="s4">)</span></p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span>)</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span>{</p>
<p class="p4"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>// Reset the axis and spin amount</p>
<p class="p2"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s5">_spinAroundY</span> = <span class="s2">false</span>;</p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>_spinIncrement<span class="s4"> = -</span>_initialSpinAmount<span class="s4">;</span></p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span>}</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s2">else</span></p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span>{</p>
<p class="p4"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>// Speed up the rate of spin</p>
<p class="p2"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s2">if</span> (<span class="s6">abs</span>(<span class="s5">_spinIncrement</span> * <span class="s7">2</span>) &lt; <span class="s7">10</span>)</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s5">_spinIncrement</span> *= <span class="s7">2</span>;</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span>}</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s5">_preSpin</span> = <span class="s2">false</span>;</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s5">_paused</span> = <span class="s2">false</span>;</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s2">break</span>;</p>
<p class="p2"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p class="p7"><span class="s4"><span>&#0160;&#0160;&#0160; </span></span><span class="s2">case</span><span class="s4"> </span>UISwipeGestureRecognizerDirectionUp<span class="s4">:</span></p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s2">if</span> (</p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>_preSpin<span class="s4"> || </span>_spinAroundY<span class="s4"> ||</span></p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span>(!</span>_spinAroundY<span class="s4"> &amp;&amp; </span>_spinIncrement<span class="s4"> &lt; </span><span class="s7">0</span><span class="s4">)</span></p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span>)</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span>{</p>
<p class="p4"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>// Reset the axis and spin amount</p>
<p class="p2"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s5">_spinAroundY</span> = <span class="s2">false</span>;</p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>_spinIncrement<span class="s4"> = </span>_initialSpinAmount<span class="s4">;</span></p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span>}</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s2">else</span></p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span>{</p>
<p class="p4"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>// Speed up the rate of spin</p>
<p class="p2"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s2">if</span> (<span class="s6">abs</span>(<span class="s5">_spinIncrement</span> * <span class="s7">2</span>) &lt; <span class="s7">10</span>)</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s5">_spinIncrement</span> *= <span class="s7">2</span>;</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span>}</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s5">_preSpin</span> = <span class="s2">false</span>;</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s5">_paused</span> = <span class="s2">false</span>;</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s2">break</span>;</p>
<p class="p2"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p class="p7"><span class="s4"><span>&#0160;&#0160;&#0160; </span></span><span class="s2">case</span><span class="s4"> </span>UISwipeGestureRecognizerDirectionLeft<span class="s4">:</span></p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s2">if</span> (</p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>_preSpin<span class="s4"> || !</span>_spinAroundY<span class="s4"> ||</span></p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span>(</span>_spinAroundY<span class="s4"> &amp;&amp; </span>_spinIncrement<span class="s4"> &gt; </span><span class="s7">0</span><span class="s4">)</span></p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span>)</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span>{</p>
<p class="p4"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>// Reset the axis and spin amount</p>
<p class="p2"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s5">_spinAroundY</span> = <span class="s2">true</span>;</p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>_spinIncrement<span class="s4"> = -</span>_initialSpinAmount<span class="s4">;</span></p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span>}</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s2">else</span></p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span>{</p>
<p class="p4"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>// Speed up the rate of spin</p>
<p class="p2"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s2">if</span> (<span class="s6">abs</span>(<span class="s5">_spinIncrement</span> * <span class="s7">2</span>) &lt; <span class="s7">10</span>)</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s5">_spinIncrement</span> *= <span class="s7">2</span>;</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span>}</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s5">_preSpin</span> = <span class="s2">false</span>;</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s5">_paused</span> = <span class="s2">false</span>;</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s2">break</span>;</p>
<p class="p2"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p class="p7"><span class="s4"><span>&#0160;&#0160;&#0160; </span></span><span class="s2">case</span><span class="s4"> </span>UISwipeGestureRecognizerDirectionRight<span class="s4">:</span></p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s2">if</span> (</p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>_preSpin<span class="s4"> || !</span>_spinAroundY<span class="s4"> ||</span></p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span>(</span>_spinAroundY<span class="s4"> &amp;&amp; </span>_spinIncrement<span class="s4"> &lt; </span><span class="s7">0</span><span class="s4">)</span></p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span>)</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span>{</p>
<p class="p4"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>// Reset the axis and spin amount</p>
<p class="p2"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s5">_spinAroundY</span> = <span class="s2">true</span>;</p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>_spinIncrement<span class="s4"> = </span>_initialSpinAmount<span class="s4">;</span></p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span>}</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s2">else</span></p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span>{</p>
<p class="p4"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span>// Speed up the rate of spin</p>
<p class="p2"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s2">if</span> (<span class="s6">abs</span>(<span class="s5">_spinIncrement</span> * <span class="s7">2</span>) &lt; <span class="s7">10</span>)</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s5">_spinIncrement</span> *= <span class="s7">2</span>;</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span>}</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s5">_preSpin</span> = <span class="s2">false</span>;</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s5">_paused</span> = <span class="s2">false</span>;</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s2">break</span>;</p>
<p class="p2"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p class="p6"><span class="s4"><span>&#0160;&#0160;&#0160; </span></span>default<span class="s4">:</span></p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s2">break</span>;</p>
<p class="p3"><span>&#0160; </span>}</p>
<p class="p3">}</p>
<p class="p2">&#0160;</p>
<p class="p4">// Pinch-zoom</p>
<p class="p2">&#0160;</p>
<p class="p3">- (<span class="s2">void</span>)pinchGesture</p>
<p class="p3"><span>&#0160; </span>:(<span class="s3">UIPinchGestureRecognizer</span> *)gestureRecognizer</p>
<p class="p3">{</p>
<p class="p3"><span>&#0160; </span><span class="s2">if</span> (</p>
<p class="p7"><span class="s4"><span>&#0160;&#0160;&#0160; </span>[gestureRecognizer </span>state<span class="s4">] == </span>UIGestureRecognizerStateBegan<span class="s4"> ||</span></p>
<p class="p7"><span class="s4"><span>&#0160;&#0160;&#0160; </span>[gestureRecognizer </span>state<span class="s4">] == </span>UIGestureRecognizerStateChanged</p>
<p class="p3"><span>&#0160; </span>)</p>
<p class="p3"><span>&#0160; </span>{</p>
<p class="p4"><span class="s4"><span>&#0160;&#0160;&#0160; </span></span>// Adjust the camera position based on the zoom scale</p>
<p class="p2"><span>&#0160;&#0160;&#0160; </span></p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span>[<span class="s2">self</span>.<span class="s5">camera</span> <span class="s9">setZ</span>:<span class="s2">self</span>.<span class="s5">camera</span>.<span class="s5">z</span> * (<span class="s7">1</span>/gestureRecognizer.<span class="s3">scale</span>)];</p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span>[gestureRecognizer <span class="s6">setScale</span>:<span class="s7">1</span>];</p>
<p class="p3"><span>&#0160; </span>}</p>
<p class="p3">}</p>
<p class="p2">&#0160;</p>
<p class="p4">// Rotate-rotate :-)</p>
<p class="p2">&#0160;</p>
<p class="p3">- (<span class="s2">void</span>)rotationGesture</p>
<p class="p8"><span class="s4"><span>&#0160; </span>:(</span>UIRotationGestureRecognizer<span class="s4"> *)gestureRecognizer</span></p>
<p class="p3">{</p>
<p class="p3"><span>&#0160; </span><span class="s2">if</span> (</p>
<p class="p7"><span class="s4"><span>&#0160;&#0160;&#0160; </span>[gestureRecognizer </span>state<span class="s4">] == </span>UIGestureRecognizerStateBegan<span class="s4"> ||</span></p>
<p class="p7"><span class="s4"><span>&#0160;&#0160;&#0160; </span>[gestureRecognizer </span>state<span class="s4">] == </span>UIGestureRecognizerStateChanged</p>
<p class="p3"><span>&#0160; </span>)</p>
<p class="p3"><span>&#0160; </span>{</p>
<p class="p4"><span class="s4"><span>&#0160;&#0160;&#0160; </span></span>// Adjust the rotation around Z based on the rotation amount</p>
<p class="p2"><span>&#0160;&#0160;&#0160; </span></p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160; </span></span><span class="s2">if</span><span class="s4"> (</span>_paused<span class="s4"> || </span>_preSpin<span class="s4">)</span></p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span>{</p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span><span class="s5">_rotation</span> += (gestureRecognizer.<span class="s3">rotation</span> * <span class="s7">180.0</span> / <span class="s1">M_PI</span>);</p>
<p class="p5"><span class="s4"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span>[</span>_container<span class="s4"> </span><span class="s9">setRotationZ</span><span class="s4">:</span>_rotation<span class="s4">];</span></p>
<p class="p3"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span>[gestureRecognizer <span class="s6">setRotation</span>:<span class="s7">0</span>];</p>
<p class="p3"><span>&#0160;&#0160;&#0160; </span>}</p>
<p class="p3"><span>&#0160; </span>}</p>
<p class="p3">}</p>
<p class="p2"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p class="p6">@end</p>
<p>That&#39;s it for the sub-series on iOS (for now, at least) and for the overall series on cloud &amp; mobile (from a technical perspective). In the next post, we’ll take a look back at the series and comment on the journey we’ve been on for the last month or so.</p>
