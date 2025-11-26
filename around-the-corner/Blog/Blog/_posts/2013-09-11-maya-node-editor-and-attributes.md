---
layout: "post"
title: "Maya Node Editor and Attributes"
date: "2013-09-11 11:37:19"
author: "Cyrille Fauvel"
categories:
  - "Custom Nodes"
  - "Cyrille Fauvel"
  - "Maya"
original_url: "https://around-the-corner.typepad.com/adn/2013/09/maya-node-editor-and-attributes.html "
typepad_basename: "maya-node-editor-and-attributes"
typepad_status: "Publish"
---

<p>The Node Editor takes a number of features that were previously divided between Maya&#39;s Hypershade, the Hypergraph and the Connection Editor, and merges them into a single user-friendly workspace. The Node Editor gives us a much simpler way of making advanced connections between any nodes or attributes in your Maya scene.</p>
<p>
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d019aff53297e970b-pi" style="display: inline;"><img alt="Node-Editor" class="asset  asset-image at-xid-6a0163057a21c8970d019aff53297e970b" src="/assets/image_b5bf83.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Node-Editor" /></a><br />With the Node Editor you can graph, pin and clear nodes from the Node Editor... create nodes directly within the Node Editor, create / connect node attributes...</p>
<p>But while the interface is more modern, more powerfull and saves time. When it comes to custom node, you may get in trouble to have your custom node attributes to show-up in the default mode. Here are some tips for getting attributes to show-up in the Maya Node Editor.</p>
<p>  There is an algorithm that determines if an attribute is “Interesting”. It is essentially a set of “If Then” statements testing aspects of an attribute.  To make an attribute appear interesting you should:</p>
<ul>
<li>Make sure that the attribute is NOT hidden.</li>
<li>Make the attribute is writeable (If output OR input)</li>
<li>If the attribute is still not showing up:
<ul>
<li>Make the attribute keyable</li>
<li>Make the attribute dynamic</li>
</ul>
</li>
</ul>
<p>There are currently  4 modes in the Node Editor determining how attributes get displayed:</p>
<ul>
<li>No Attributes</li>
<li>Connected Attributes</li>
<li>“Interesting Attributes”</li>
<li>All Attributes (using the RMB)</li>
</ul>
<p>&#0160;</p>
