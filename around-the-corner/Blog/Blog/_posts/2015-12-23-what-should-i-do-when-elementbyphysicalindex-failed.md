---
layout: "post"
title: "What should I do when elementByPhysicalIndex failed?"
date: "2015-12-23 19:14:32"
author: "Cheng Xi Li"
categories:
  - "C++"
  - "Cheng Xi Li"
  - "Maya"
original_url: "https://around-the-corner.typepad.com/adn/2015/12/what-should-i-do-when-elementbyphysicalindex-failed.html "
typepad_basename: "what-should-i-do-when-elementbyphysicalindex-failed"
typepad_status: "Publish"
---

<p>&#0160;&#0160;&#0160;&#0160; The other day, I encountered a problem when I tried to write a sample for Maya 2016.</p>
<p>&#0160;&#0160;&#0160;&#0160; When I was trying to get an element in an array using a physical index, it worked fine most of the time. But it failed, when I was trying to get the elements inside a CV array. This time it returned an incorrect number of elements and thrown an error when I was accessing the array. It turned out that physical index is safe to use for user created nodes but for Maya internal objects like CV, we should use logical index instead:</p>
<blockquote>&#0160;&#0160; indices = OpenMaya.MIntArray() <br />&#0160;&#0160; plug.getExistingArrayAttributeIndices(indices) <br />&#0160;&#0160; for plugArrayIndex in indices: <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; arrayPlug = plug.elementByLogicalIndex(plugArrayIndex)</blockquote>
<p>&#0160;&#0160;&#0160;&#0160; The code above is using the logical index like we do in MEL. It is safer than physical index and can be used for all kinds of Maya internal array objects :)</p>
<p>&#0160;&#0160;&#0160;&#0160; Do you find any traps in your Maya programming? Please share with us!</p>
