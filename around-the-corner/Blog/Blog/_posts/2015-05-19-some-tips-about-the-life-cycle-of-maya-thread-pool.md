---
layout: "post"
title: "Some tips about the life cycle of Maya thread pool"
date: "2015-05-19 22:06:00"
author: "Zhong Wu"
categories:
  - "C++"
  - "Maya"
  - "Windows"
  - "Zhong Wu"
original_url: "https://around-the-corner.typepad.com/adn/2015/05/some-tips-about-the-life-cycle-of-maya-thread-pool.html "
typepad_basename: "some-tips-about-the-life-cycle-of-maya-thread-pool"
typepad_status: "Publish"
---

<p>Maya provides a class named MThreadPool to help developers to create or reuse a thread pool, if you want to know more about this, you can check out the online SDK <a href="http://help.autodesk.com/view/MAYAUL/2015/ENU/?guid=__files_GUID_2BEF58D3_6162_4235_A6CE_3D8B0742A0AE_htmand http://help.autodesk.com/view/MAYAUL/2015/ENU/?guid=__cpp_ref_class_m_thread_pool_html" target="_blank">here</a>.</p>
<p>If you want to know how to use MThreadPool, besides the above link, you can check out our 2 samples, <strong>threadTestCmd</strong> and <strong>threadTestWithLocksCmd</strong> in the Maya devkit.</p>
<p>In the 2 samples mentioned above, you will see that MThreadPool is actually released twice by using MThreadPool::release(). The big question for me was why do you need to release it twice?</p>
<pre class="brush: cpp; toolbar: false;">// pool is reference counted. Release reference to current thread instance
MThreadPool::release();

// release reference to whole pool which deletes all threads
MThreadPool::release();</pre>
<p>Whenever the thread pool is created, it actually sets the count reference to 1, instead of 0, so when we call MThreadPool::init(), the reference count will be increased to 2. So if you release it once, it just decrease the reference count to 1, which will not kill the thread pool. This is the reason why you have to always release it 2 times to destroy it. </p>
<p>The SDK online help says "Since creation and deletion of threads is expensive, it is a good idea to make use of the thread pool where possible, and try to keep it around between invocations of the plug-in rather than recreate it each call.". Maya is trying to keep the thread pool alive during the life cycle to improve performances, and the thread pool always survives unless you manually decrease the reference count to 0.&nbsp;&nbsp;&nbsp;&nbsp; <br />&nbsp;&nbsp;&nbsp;&nbsp;</p>
<p><img style="float: none; margin-left: auto; display: block; margin-right: auto;" src="/assets/121742053214203.jpg" alt="" width="400" height="225" /></p>
