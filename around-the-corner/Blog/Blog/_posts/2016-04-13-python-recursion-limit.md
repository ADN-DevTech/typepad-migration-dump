---
layout: "post"
title: "Python Recursion Limit"
date: "2016-04-13 03:32:33"
author: "Vijaya Prakash"
categories:
  - "FBX"
  - "Maya"
  - "Python"
  - "Vijay Prakash"
original_url: "https://around-the-corner.typepad.com/adn/2016/04/python-recursion-limit.html "
typepad_basename: "python-recursion-limit"
typepad_status: "Publish"
---

<p><span style="font-size: 10pt; font-family: Verdana, sans-serif;">While executing third party plugins or your own plugins which is written in python, have you ever faced the “maximum recursion depth exceeded” error? Python lacks the tail recursion optimizations common in functional languages like LISP. </span></p>
<p><span style="font-size: 8.0pt; font-family: &#39;Verdana&#39;,&#39;sans-serif&#39;;"> <a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d01b7c8338a29970b-pi" style="display: inline;"><img alt="Recur-limit" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d01b7c8338a29970b img-responsive" src="/assets/image_78356f.jpg" title="Recur-limit" /></a><br /></span></p>
<p><span style="font-size: 10pt; font-family: Verdana, sans-serif;">Let’s say, you want to add the specified node to the node array in FBX using recursive function, we can use the below approach to travel the whole scene. However, if your scene contains more than 1000 nodes, you will get the recursion limit issue(maximum recursion depth exceeded). .</span></p>
<pre class="brush: cpp; toolbar: false;"># Add the specified node to the node array. Also, add recursively
# all the parent node of the specified node to the array.

def AddNodeRecursively(pNodeArray, pNode):
    if pNode:
        AddNodeRecursively(pNodeArray, pNode.GetParent())
        found = False 
        for node in pNodeArray:
            if node.GetName() == pNode.GetName():
                found = True
        if not found:
            # Node not in the list, add it
            pNodeArray += [pNode]
</pre>
<p>So, you have to use sys.setrecursionlimit() method to fix this issue in python.</p>
<pre class="brush: cpp; toolbar: false;"># Add the specified node to the node array. Also, add recursively
# all the parent node of the specified node to the array.

import sys
sys.setrecursionlimit(5000)

def AddNodeRecursively(pNodeArray, pNode):
    if pNode:
        AddNodeRecursively(pNodeArray, pNode.GetParent())
        found = False 
        for node in pNodeArray:
            if node.GetName() == pNode.GetName():
                found = True
        if not found:
            # Node not in the list, add it
            pNodeArray += [pNode]
</pre>
