---
layout: "post"
title: "Maya API thread safe?"
date: "2012-09-19 01:19:08"
author: "Cyrille Fauvel"
categories:
  - "C++"
  - "Concurrent programming"
  - "Custom Nodes"
  - "Cyrille Fauvel"
  - "GCC"
  - "Linux"
  - "Mac"
  - "Maya"
  - "UI"
  - "Visual Studio"
  - "Windows"
  - "XCode"
original_url: "https://around-the-corner.typepad.com/adn/2012/09/maya-api-thread-safe.html "
typepad_basename: "maya-api-thread-safe"
typepad_status: "Publish"
---

<p>In general it is not safe to assume any Maya API is threadsafe if it is not documented as being so. There can be internal caching or state being set, meaning functions that may seem threadsafe are in fact not. An example is MFnMesh::getVertexNormal(). These normals are evaluated lazily, meaning the first call to this function may trigger a full recompute of all normals, so two calls simultaneously may have bad consequences. One clunky way around this is to do one dummy call first to ensure the data is up to date. After that parallel queries should be fine. But I would not recommend anyone to depend on that behavior as Maya implementation may change in future and that trick might not work anymore. People in Maya engineering who nicely reviewed that post for me (thx to them :), are actively working on improving Maya&#39;s performances and their work may impact how things work internally. For that reason just assume none of these API are threadsafe and instead&#0160;you should stick to what is
explicitly documented as being threadsafe.
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017d3bd6f942970c-pi" style="display: inline;"><img alt="Thread3" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017d3bd6f942970c" src="/assets/image_e8fb9c.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Thread3" /></a></p>
<p>If you want to do intersection operations, note that the closest point and intersect methods on MFnMesh are not threadsafe. However MMeshIntersector is threadsafe. Intersection operations on Nurbs and Subdivs are also not threadsafe.</p>
<p>The MPoint/MVector/MMatrix/MArray classes are thread-safe only in the sense that simultaneous accesses to distinct objects are safe, and simultaneous read accesses to shared objects are safe. If multiple threads access a single object, and at least one thread may potentially write, then the user is responsible for ensuring mutual exclusion between the threads during the object accesses.</p>
<p>
In terms of APIs, it is fine to use any threading API including native threads (pthreads/win32), OpenMP, or TBB. In Maya 2008, we added a threading API. (See classes MThreadPool, MThreadAsync, MMutexLock, MSpinLock, and example plug-ins threadTestCmd, threadTestWithLocksCmd). This API is designed to mimic a native threading API like pthreads/win32 threads, so existing code using native threads can be migrated over to it. The benefit of using the Maya API is that the code is run using the existing internal Maya thread pool. These internal threads respect thread count controls in Maya (eg the threadCount command) and also handle nested threading.</p>
<p>
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d01774487785c970d-pi" style="display: inline;"><img alt="Tbb-2" class="asset  asset-image at-xid-6a0163057a21c8970d01774487785c970d" src="/assets/image_8d70e1.jpg" style="width: 170px; display: block; margin-left: auto; margin-right: auto;" title="Tbb-2" /></a><br />The latter point means that it is possible to invoke a threaded region from inside another threaded region without causing oversubscription, as the threads are managed by a task manager (actually it is based on Intel&#39;s Threading Building Blocks technology). You do not have to use the Maya threading API, but if you don&#39;t you should ensure that the plug-in respects the current Maya thread count. (The MEL command &quot;threadCount -q -n&quot; will return the current number of threads to be used). This ensures that if the user wants to run multiple Maya sessions at the same time in single threaded mode, eg for rendering, your plug-in will play nice and also run single threaded. In general, use TBB for parallel complex situation in your code. Your TBB code will play nicely in the Maya threading environment as Maya TBB core will manager your threads as if they were Maya’s. But, I would recommend you to be careful when using TBB for some fairly easy loops as TBB may not necessarily&#0160;give you better results than single threaded code. </p>
<p><a href="http://www.threadingbuildingblocks.org/" target="_self">Intel&#39;s TBB</a> is the recommended alternative for Maya over all multithreading options you have,&#0160;and it is used internally in Maya. There should be no problem writing plug-ins using this API, but it is best to use the same version as that being used by Maya (versions available on that <a href="http://around-the-corner.typepad.com/adn/2012/06/maya-compiler-versions.html" target="_self">post</a>). It supports nested threading, and also provides a variety of threadsafe containers and a thread-aware memory allocator. Any comments or suggestions welcome - we want to make it as easy as possible to write multithreaded plug-ins for Maya without forcing you into one particular API or methodology.</p>
<p>
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017c31c4a9f4970b-pi" style="display: inline;"><img alt="Bras-de-fer" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017c31c4a9f4970b" src="/assets/image_a14ca0.jpg" style="margin: 0px auto 5px; display: block;" title="Bras-de-fer" width="170px" /></a></p>
<p>We would recommend using Intel&#39;s
TBB over OpenMP for these reasons:</p>
<ol>
<li>The Intel&#39;s TBB library has a broader scope of functionality that OpenMP.</li>
<li>It&#39;s a library only implementation, so it does not depend on the compiler support &quot;special&quot; pragmas.</li>
<li>In recent Maya development, we have been using almost exclusively TBB internally (because of point 1 and 2) to multi-thread parts of Maya.&#0160;So, indirectly using Intel TBB in Maya has been empirically proven…</li>
</ol>
<p>The advantage of OpenMP is that it is often simpler to use for simple cases. For example, parallelizing a loop often amounts to adding a single pragma. But, for slightly more complex cases TBB wins (tasks, thread-safe containers, locks, etc...).</p>
<p>
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017c31a85959970b-pi" style="display: inline;"><img alt="Openmp" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017c31a85959970b" src="/assets/image_dbdb90.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Openmp" width="150px" /></a></p>
<p>Note that OpenMP is fine as well, but not recommended. It is an easy way to add data parallelism to a plug-in. Internally Maya uses the Intel OpenMP library on all platforms as we have found it delivers very good performance, but it should be ok to use the VC++ version as well. Just be sure to be consistent, i.e. do not mix compilers where OpenMP constructs are implemented in each, since an OpenMP region declared by the Intel compiler will ignore any OpenMP locks declared by the Microsoft compiler and vice versa. If using VC++ OpenMP you again need to keep the thread count in sync with Maya. With the Intel compiler this is not necessary as Maya manages it internally.</p>
<p>
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017d3bd6ffdd970c-pi" style="display: inline;"><img alt="Thread2" class="asset  asset-image at-xid-6a0163057a21c8970d017d3bd6ffdd970c" src="/assets/image_2b4525.jpg" style="width: 250px; display: block; margin-left: auto; margin-right: auto;" title="Thread2" /></a></p>
<p>This article has been inspired by Martin Watt&#39; whitepaper available <a href="http://images.autodesk.com/adsk/files/multithreading_plug-ins_for_maya.pdf" target="_self">here</a>.</p>
