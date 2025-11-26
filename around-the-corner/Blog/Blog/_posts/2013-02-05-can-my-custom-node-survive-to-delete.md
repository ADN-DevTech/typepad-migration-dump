---
layout: "post"
title: "Can my custom node survive to delete"
date: "2013-02-05 11:06:34"
author: "Cyrille Fauvel"
categories:
  - ".Net"
  - "C++"
  - "Custom Nodes"
  - "Cyrille Fauvel"
  - "Maya"
  - "Plug-in"
  - "Python"
original_url: "https://around-the-corner.typepad.com/adn/2013/02/can-my-custom-node-survive-to-delete.html "
typepad_basename: "can-my-custom-node-survive-to-delete"
typepad_status: "Publish"
---

<p>When you use MDagModifier to delete the last shape under a transform, the transform is automatically deleted as well. That is the normal Maya behavior.&#0160;It helps to keep the scene clear of orphaned transforms and is consistent with MDagModifier::createNode(), which will automatically create a parent if none is specified when creating the node.</p>
<p>However, you need to know that&#0160;MDagModifier::reparentNode() would not delete empty parents when that happens. And the&#0160;&#39;delete&#39; command doesn&#39;t behave the same way since it uses the internal equivalent of MDGModifier to delete the node, which leaves the parent intact.</p>
<p>
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017d40cb9734970c-pi" style="display: inline;"><img alt="Donotdeleteme" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017d40cb9734970c" src="/assets/image_afeace.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Donotdeleteme" /></a><br />Said that if you want to protect a node from being deleted automatically by Maya when such situation occurs you can do so by using the MPxNode::setExistWithoutInConnections() and MPxNode::setExistWithoutOutConnections() methods which will tell Maya to not delete your node if you lose all your upstream and/or downstream connections.</p>
<p>&#0160;</p>
<p>&#0160;</p>
