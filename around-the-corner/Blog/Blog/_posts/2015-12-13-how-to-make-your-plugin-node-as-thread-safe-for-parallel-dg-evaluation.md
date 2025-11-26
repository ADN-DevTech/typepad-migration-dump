---
layout: "post"
title: "How to make your plugin node as thread-safe for parallel DG Evaluation"
date: "2015-12-13 22:25:02"
author: "Vijaya Prakash"
categories:
  - "C++"
  - "Maya"
  - "MEL"
  - "Plug-in"
  - "Vijay Prakash"
original_url: "https://around-the-corner.typepad.com/adn/2015/12/how-to-make-your-plugin-node-as-thread-safe-for-parallel-dg-evaluation.html "
typepad_basename: "how-to-make-your-plugin-node-as-thread-safe-for-parallel-dg-evaluation"
typepad_status: "Publish"
---

<p><strong>Take this example:</strong><br /> Let’s take very simple DG setup. A locator whose transform is connected to two compute nodes (spliceMayaNode1 and spliceMayaNode2), each of which computes something heavy. The results are then added together in a third node (spliceMayaNode3) from the plugin, and that result drives a locator.</p>
<p><a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d01bb089e4807970d-pi" style="display: inline;"><img alt="Node-parallel" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d01bb089e4807970d image-full img-responsive" src="/assets/image_0eadc9.jpg" title="Node-parallel" /></a></p>
<p>The nodes use dynamic attributes so you have to use setDependentsDirty() to declare the outputs that are affected by which inputs. If you comment out the code that adds plugs to the output&#0160;array in setDependentsDirty() the nodes spliceMayaNode1 and spliceMayaNode2 can be called from separate threads; however, spliceMayaNode3 will never be executed, presumably because the EvaluationManager won’t think anything can ever make it dirty. However when the output plugs are appended, the nodes spliceMayaNode1 and spliceMayaNode2 are always evaluated from the same thread.&#0160;</p>
<p>To overcome this issue, you have to make your plugin node as thread-safe for parallel DG Evaluation. You can explicitly use the MEL command <a href="http://help.autodesk.com/cloudhelp/2016/ENU/Maya-Tech-Docs/Commands/setNodeTypeFlag.html#flagthreadSafe" target="_blank">setNodeTypeFlag</a>.</p>
