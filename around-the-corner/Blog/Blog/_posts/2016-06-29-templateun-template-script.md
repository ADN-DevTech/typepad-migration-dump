---
layout: "post"
title: "Template/un-template script"
date: "2016-06-29 21:41:45"
author: "Vijaya Prakash"
categories:
  - "Maya"
  - "MEL"
  - "Vijay Prakash"
original_url: "https://around-the-corner.typepad.com/adn/2016/06/templateun-template-script.html "
typepad_basename: "templateun-template-script"
typepad_status: "Publish"
---

<p>A script can be created that will template or un-template the selected object to bind it to a key. Then, if the object is already templated, it will un-template it and vice versa. To do template/un-template, there is no need to write a big script. Maya provides a MEL command (toggle) to template and un-template the selected object using “-template” option.<br /> <br /> toggle -template;<br /> <br /> 1. If objects are already templated, then above MEL command will un-template them.<br /> 2. If Objects are un-templated, then same above command will template them.<br /> <br /> toggle -template -q;<br /> // Result: 0 // <br /> toggle -template;</p>
<p><br /> <a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d01bb0918d5d3970d-pi" style="display: inline;"><img alt="Template" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d01bb0918d5d3970d image-full img-responsive" src="/assets/image_8356ea.jpg" title="Template" /></a><br /><br /></p>
<p><br /> toggle -template -q;<br /> // Result: 1 // <br /> toggle -template;</p>
<p><br /> <a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d01b7c8756108970b-pi" style="display: inline;"><img alt="Untemplate" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d01b7c8756108970b image-full img-responsive" src="/assets/image_b1d009.jpg" title="Untemplate" /></a></p>
<p>As you can see it is easy to create a script to achieve template/un-template of a selected object.</p>
