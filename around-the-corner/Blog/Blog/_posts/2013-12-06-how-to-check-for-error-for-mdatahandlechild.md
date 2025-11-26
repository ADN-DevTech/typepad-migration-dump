---
layout: "post"
title: "How to check for error for MDataHandle::child()?"
date: "2013-12-06 09:08:57"
author: "Cyrille Fauvel"
categories:
  - ".Net"
  - "C++"
  - "Custom Nodes"
  - "Cyrille Fauvel"
  - "Maya"
  - "Plug-in"
  - "Python"
original_url: "https://around-the-corner.typepad.com/adn/2013/12/how-to-check-for-error-for-mdatahandlechild.html "
typepad_basename: "how-to-check-for-error-for-mdatahandlechild"
typepad_status: "Publish"
---

<p>First if you&#39;re using the MPlug version, it cannot fail but if you&#39;re using the MObject version and if it fails, it would return an empty MDataHandle. I.e. equivalent to:</p>
<p>&#0160; MDataHandle tmp ;</p>
<p>and its&#0160;type() method should return you kInvalid.&#0160;Unfortunately, if the child attribute is also a compound attribute, type() would also return kInvalid.</p>
<p><a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d019b023fdda3970b-pi" style="display: inline;"><img alt="Scratch-head" class="asset  asset-image at-xid-6a0163057a21c8970d019b023fdda3970b" src="/assets/image_25f199.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Scratch-head" /></a></p>
<p>So what&#39;s up? in order to keep MDataHandle operations fast in Maya, minimal checking is performed, which for most methods means no checking at all. It is up to the caller to ensure that the method being called and the parameters passed to it are all appropriate for the handle. For the child() methods that means that attribute to which the handle currently points is compound and the attribute passed to the method really is one of its children. So you can ensure success by checking for those two conditions before making the call</p>
<p>More generally, if you want to access attribute data with error checking, use an MPlug.</p>
