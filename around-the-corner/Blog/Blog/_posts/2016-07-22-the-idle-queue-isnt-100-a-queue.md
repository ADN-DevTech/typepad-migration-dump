---
layout: "post"
title: "The idle queue isn't 100% a queue"
date: "2016-07-22 04:37:00"
author: "Cheng Xi Li"
categories:
  - "Cheng Xi Li"
  - "Maya"
  - "Python"
original_url: "https://around-the-corner.typepad.com/adn/2016/07/the-idle-queue-isnt-100-a-queue.html "
typepad_basename: "the-idle-queue-isnt-100-a-queue"
typepad_status: "Publish"
---

<h1>&#160;</h1>  <p>&#160;&#160;&#160; This week, I ran into a strange problem with a multi-threading Python case from a partner.</p>  <pre><code>def foo():
for i in range(10):
    print i

# call from main thread
foo()

# call from separate thread
import threading
mythread = threading.Thread(target=foo)
mythread.start()
</code></pre>

<p>&#160;</p>

<p>The output is quite strange:</p>

<pre><code>0
1
2
3
4
5
6
7
8
9

9
8
7
6
5
4
3
2
1
0
</code></pre>

<p>&#160;&#160;&#160; The child thread's output order is reversed, how did that happen?</p>

<p>&#160;&#160;&#160; After spending some time with our engineers, we found that Maya will treat the higher priority queue as a stack for better performance. The Python I/O output is considered as a high priority event and it is in the high priority idle queue. </p>

<p>&#160;&#160;&#160; If you are using multi-threading in your code or idle queue, you may want to take this into consideration.</p>
