---
layout: "post"
title: "Do the Maya array data structures store their elements contiguously?"
date: "2013-01-15 00:15:00"
author: "Cyrille Fauvel"
categories:
  - "C++"
  - "Cyrille Fauvel"
  - "Maya"
original_url: "https://around-the-corner.typepad.com/adn/2013/01/do-the-maya-array-data-structures-store-their-elements-contiguously.html "
typepad_basename: "do-the-maya-array-data-structures-store-their-elements-contiguously"
typepad_status: "Publish"
---

<p>Maya array data structures such as MFloatArray, MIntArray, etc... store their elements contiguously in memory like std::vector does, but&#0160;that is an implementation detail and not guaranteed by the API.</p>
<p>
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017c35c190bd970b-pi" style="display: inline;"><img alt="Array" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017c35c190bd970b" src="/assets/image_af2944.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Array" /></a>At the time of the post and back to early release of Maya up to Maya 2013 Ext 2, the elements are currently stored contiguously.&#0160;So code based on that assumption may work today but might fail to do so in some future release of Maya. In practice that is unlikely to happen, but you never know.</p>
