---
layout: "post"
title: "Using web workers to run intensive browser-side analyses in Autodesk Forma"
date: "2024-06-14 10:33:37"
author: "Kean Walmsley"
categories:
  - "APS (Forge)"
  - "Async"
  - "AU"
  - "Forma"
  - "VASA"
  - "Web/Tech"
original_url: "https://www.keanw.com/2024/06/using-web-workers-to-run-intensive-browser-side-analyses-in-autodesk-forma.html "
typepad_basename: "using-web-workers-to-run-intensive-browser-side-analyses-in-autodesk-forma"
typepad_status: "Publish"
---

<p>The topic for this post relates to a project I’ve been working on for a few weeks, now. We’ve taken a complex set of spatial analyses - that use our favourite voxel-based architectural space analysis Dynamo package, VASA - and have translated them to work directly in the browser.</p>
<p>(This translation process isn’t really the subject of this series, but it’s pretty interesting, nonetheless, and it’s mostly what I was working on during the recent <a href="https://www.keanw.com/2024/06/autodesk-devcon-2024-in-munich.html" target="_blank" rel="noopener">trip to Munich for the DevCon</a>. Most of the Dynamo code was fairly easy to translate, although things like the automatic lacing of nodes meant throwing in some loops (or maps/reduces) from time to time. The trickiest area related to some hardcore Python scripts that were embedded in the graph: I know very little about Numpy, so had to get my head around some of those operations with the help of copious Print() statements - the easiest way to find out what’s happening in Python running inside Dynamo, as far as I can tell. Rhys Goldstein thankfully translated the first of the complex scripts from Python to JavaScript: once I’d tidied that up I could pass the original Python and the translated JavaScript to our corporate instance of ChatGPT to be used as a template for auto-translating the rest. With the exception of one (the most involved, admittedly) it did a very good job and certainly saved me loads of time. Without having a template to work with it had failed miserably, so this was quite an interesting takeaway for me.)</p>
<p>Anyway, so we now have this complex set of analyses translated from Dynamo (and Python) to work inside JavaScript with the VASA WebAssembly package. So far so good. The primary target for these analyses is Autodesk Forma, where we want to take building geometry as input to our analyses and display meaningful information to the user to help with their design work. (The feature to perform detailed design work isn’t yet part of Forma, so we’ve been working with geometry we’ve imported into Forma using Dynamo, which is enough for us to be able to test whether our analyses can be made to work there or not.)</p>
<p>When first implemented, the analyses would execute on the main thread. When the geometry was less complex, this wouldn’t be a problem, but for the larger/more intricate buildings this would lead to the dreaded “Page Unresponsive” warning:</p>
<p><a href="/assets/image_253078.jpg" target="_blank" rel="noopener"><img style="display: block; margin: 30px auto;" title="Page unresponsive, oh no" src="/assets/image_253078.jpg" alt="Page unresponsive, oh no." width="500" height="375" border="0" /></a></p>
<p>JavaScript code runs by default on the browser’s main thread. Long operations will block it, leading to situations like the one shown above. The <a href="https://developer.mozilla.org/en-US/docs/Glossary/Main_thread" target="_blank" rel="noopener">recommended</a> way of addressing this is by using a web worker to execute the code in a background thread, freeing up the main thread to respond to UI events, etc.</p>
<p>Even asynchronous programming - using callbacks, Promises or the <a href="https://en.wikipedia.org/wiki/Syntactic_sugar" target="_blank" rel="noopener">syntactic sugar</a> of async/await - doesn’t use additional threads to execute code. This video - that was recently shared internally by Jørgen Aars Braseth from the Forma team - does a really good job of explaining how various async methods work. It’s well worth the watch.</p>
<p style="text-align: center;"> </p>
<p style="text-align: center;"><iframe title="YouTube video player" src="https://www.youtube.com/embed/eiC58R16hb8?si=3YVJkEPskNEqb-T2" width="500" height="283" frameborder="0" allowfullscreen=""></iframe></p>
<p style="text-align: center;"> </p>
<p>So web workers are the way to address this situation, for sure. It wasn’t super complicated to set up. In our main JavaScript file, we instantiate the worker:</p>
<p> </p>
<div style="color: #cccccc; background-color: #1f1f1f; font-family: Menlo, Monaco, 'Courier New', monospace; font-weight: normal; font-size: 12px; line-height: 18px; white-space: pre;">
<div><span style="color: #569cd6;"> const</span> <span style="color: #4fc1ff;">worker</span> <span style="color: #d4d4d4;">=</span> <span style="color: #569cd6;">new</span> <span style="color: #4ec9b0;">Worker</span><span style="color: #cccccc;">(</span><span style="color: #ce9178;">'compute-metrics-worker.js'</span><span style="color: #cccccc;">);</span></div>
<br />
<div><span style="color: #569cd6;"> function</span> <span style="color: #dcdcaa;">computeMetricsWithWorker</span><span style="color: #cccccc;">(</span><span style="color: #9cdcfe;">data</span><span style="color: #cccccc;">) {</span></div>
<div><span style="color: #c586c0;">   return</span> <span style="color: #569cd6;">new</span> <span style="color: #4ec9b0;">Promise</span><span style="color: #cccccc;">((</span><span style="color: #dcdcaa;">resolve</span><span style="color: #cccccc;">, </span><span style="color: #dcdcaa;">reject</span><span style="color: #cccccc;">) </span><span style="color: #569cd6;">=&gt;</span><span style="color: #cccccc;"> {</span></div>
<div><span style="color: #4fc1ff;">     worker</span><span style="color: #cccccc;">.</span><span style="color: #dcdcaa;">onmessage</span> <span style="color: #d4d4d4;">=</span><span style="color: #cccccc;"> (</span><span style="color: #9cdcfe;">event</span><span style="color: #cccccc;">) </span><span style="color: #569cd6;">=&gt;</span><span style="color: #cccccc;"> {</span></div>
<div><span style="color: #dcdcaa;">       resolve</span><span style="color: #cccccc;">(</span><span style="color: #9cdcfe;">event</span><span style="color: #cccccc;">.</span><span style="color: #4fc1ff;">data</span><span style="color: #cccccc;">);</span></div>
<div><span style="color: #cccccc;">     };</span></div>
<div><span style="color: #4fc1ff;">     worker</span><span style="color: #cccccc;">.</span><span style="color: #dcdcaa;">onerror</span> <span style="color: #d4d4d4;">=</span><span style="color: #cccccc;"> (</span><span style="color: #9cdcfe;">error</span><span style="color: #cccccc;">) </span><span style="color: #569cd6;">=&gt;</span><span style="color: #cccccc;"> {</span></div>
<div><span style="color: #dcdcaa;">       reject</span><span style="color: #cccccc;">(</span><span style="color: #9cdcfe;">error</span><span style="color: #cccccc;">);</span></div>
<div><span style="color: #cccccc;">     };</span></div>
<div><span style="color: #4fc1ff;">     worker</span><span style="color: #cccccc;">.</span><span style="color: #dcdcaa;">postMessage</span><span style="color: #cccccc;">(</span><span style="color: #9cdcfe;">data</span><span style="color: #cccccc;">);</span></div>
<div><span style="color: #cccccc;">   });</span></div>
<div><span style="color: #cccccc;"> }</span></div>
</div>
<p> </p>
<p>In our “compute-metrics-worker.js” file, a little more work is needed. As we’re loading a WASM package via its Emscripten-generated JS loader (this could also be loaded manually and passed as an argument to the web worker, but this seemed like too much effort) we need to wait until the VASA library is fully loaded before running our long-running operation and marshaling back the results.</p>
<p> </p>
<div style="color: #cccccc; background-color: #1f1f1f; font-family: Menlo, Monaco, 'Courier New', monospace; font-weight: normal; font-size: 12px; line-height: 18px; white-space: pre;">
<div><span style="color: #569cd6;"> let</span> <span style="color: #9cdcfe;">eventData</span><span style="color: #cccccc;">;</span></div>
<br />
<div><span style="color: #9cdcfe;"> self</span><span style="color: #cccccc;">.</span><span style="color: #dcdcaa;">onmessage</span> <span style="color: #d4d4d4;">=</span> <span style="color: #569cd6;">async</span><span style="color: #cccccc;"> (</span><span style="color: #9cdcfe;">event</span><span style="color: #cccccc;">) </span><span style="color: #569cd6;">=&gt;</span><span style="color: #cccccc;"> {</span></div>
<div><span style="color: #569cd6;">   const</span><span style="color: #cccccc;"> { </span><span style="color: #4fc1ff;">data</span><span style="color: #cccccc;"> } </span><span style="color: #d4d4d4;">=</span> <span style="color: #9cdcfe;">event</span><span style="color: #cccccc;">;</span></div>
<div><span style="color: #9cdcfe;">   eventData</span> <span style="color: #d4d4d4;">=</span> <span style="color: #4fc1ff;">data</span><span style="color: #cccccc;">;</span></div>
<div><span style="color: #dcdcaa;">   importScripts</span><span style="color: #cccccc;">(</span><span style="color: #ce9178;">"vasa.js"</span><span style="color: #cccccc;">);</span></div>
<div><span style="color: #cccccc;"> };</span></div>
<br />
<div><span style="color: #569cd6;"> let</span> <span style="color: #9cdcfe;">vasa</span><span style="color: #cccccc;">;</span></div>
<br />
<div><span style="color: #569cd6;"> var</span> <span style="color: #9cdcfe;">Module</span> <span style="color: #d4d4d4;">=</span><span style="color: #cccccc;"> {</span></div>
<div><span style="color: #dcdcaa;">   onRuntimeInitialized</span><span style="color: #9cdcfe;">:</span> <span style="color: #569cd6;">async</span><span style="color: #cccccc;"> () </span><span style="color: #569cd6;">=&gt;</span><span style="color: #cccccc;"> {</span></div>
<div><span style="color: #9cdcfe;">     console</span><span style="color: #cccccc;">.</span><span style="color: #dcdcaa;">log</span><span style="color: #cccccc;">(</span><span style="color: #ce9178;">"VASA library loaded"</span><span style="color: #cccccc;">);</span></div>
<div><span style="color: #9cdcfe;">     vasa</span> <span style="color: #d4d4d4;">=</span> <span style="color: #9cdcfe;">Module</span><span style="color: #cccccc;">;</span></div>
<div><span style="color: #c586c0;">     if</span><span style="color: #cccccc;"> (</span><span style="color: #9cdcfe;">eventData</span><span style="color: #cccccc;">) {</span></div>
<div><span style="color: #569cd6;">       const</span> <span style="color: #4fc1ff;">result</span> <span style="color: #d4d4d4;">=</span> <span style="color: #c586c0;">await</span> <span style="color: #dcdcaa;">computeMetrics</span><span style="color: #cccccc;">(</span><span style="color: #9cdcfe;">eventData</span><span style="color: #cccccc;">);</span></div>
<div><span style="color: #9cdcfe;">       self</span><span style="color: #cccccc;">.</span><span style="color: #dcdcaa;">postMessage</span><span style="color: #cccccc;">(</span><span style="color: #4fc1ff;">result</span><span style="color: #cccccc;">);</span></div>
<div><span style="color: #cccccc;">     }</span></div>
<div><span style="color: #cccccc;">   }</span></div>
<div><span style="color: #cccccc;"> }</span></div>
<br />
<div><span style="color: #569cd6;"> async</span> <span style="color: #569cd6;">function</span> <span style="color: #dcdcaa;">computeMetrics</span><span style="color: #cccccc;">(</span><span style="color: #9cdcfe;">data</span><span style="color: #cccccc;">) {</span></div>
<div><span style="color: #d4d4d4;">   ...</span></div>
<div><span style="color: #cccccc;"> }</span></div>
</div>
<p> </p>
<p>The data that is sent across by the calling function is just the set of triangles extracted from the Forma geometry, which can then be voxelised and worked on in the worker. The results will be a set of numeric values that indicate how different locations in the building scores against our metrics.</p>
<p>I was initially concerned about the overhead of marshaling the data - the web worker approach just seemed like it was running quite a bit more slowly. I decided to run the two versions of the code - with the only difference being the use of the web worker, everything else was the same - and compare the results. I ran the code 3 times on the main thread and 3 times using a web worker for each of 20 different apartment models we’re using to test. Here are the results:</p>
<p><a href="/assets/image_387088.jpg" target="_blank" rel="noopener"><img style="display: block; margin: 30px auto;" title="Approximate overhead of using a web worker" src="/assets/image_387088.jpg" alt="Approximate overhead of using a web worker." width="500" height="375" border="0" /></a></p>
<p>The average overhead was under 5%, which in most cases meant less than a second of additional time. Definitely acceptable considering the removal of the “Page Unresponsive” warning.</p>
<p>I’ve had some good news since the DevCon: I’ll be talking about topics such as this along with my Forma colleague Håvard Høiby at AU 2024 in San Diego. We’ll be talking through the basics of building extensions for Forma, as well as a more in-depth look at how Autodesk Research is using VASA to implement our own Forma extensions. It should be fun!</p>
