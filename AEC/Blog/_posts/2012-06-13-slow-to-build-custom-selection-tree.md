---
layout: "post"
title: "Slow to build custom selection tree"
date: "2012-06-13 02:33:27"
author: "Xiaodong Liang"
categories:
  - ".NET"
  - "Navisworks"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/aec/2012/06/slow-to-build-custom-selection-tree.html "
typepad_basename: "slow-to-build-custom-selection-tree"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>  <p>Issue</p>  <p>I wanted to build a Selection Tree like Navisworks GUI. The code of .NET API traverses the whole model and add each model item to the tree node. But it is too slow, especially for a large model. Is there any way to improve the speed?</p>  <p>Solution</p>  <p>The standard Windows tree controls are very poor at handling very large trees, and as such are often unsuitable for representing large selection trees. Traversing the entire hierarchy will be slow for large models using recursion, and added to the use of the Windows tree control, the slow performance is as expected. </p>  <p>We suggest doing something similar to the <b>AppInfo</b> sample by storing the ModelItem with the TreeNode and only looking at the children when the node is expanded, that way you limit the effect of the expansion speed to only examine children when it is needed.</p>
