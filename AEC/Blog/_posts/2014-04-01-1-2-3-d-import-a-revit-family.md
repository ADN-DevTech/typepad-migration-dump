---
layout: "post"
title: "1-2-3-D Import a Revit Family"
date: "2014-04-01 00:35:48"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "Partha Sarkar"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2014/04/1-2-3-d-import-a-revit-family.html "
typepad_basename: "1-2-3-d-import-a-revit-family"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>There is bit of designer in every human being! Everyone thinks in a different way and each of this thought process leads to new ideas. However, most of these ideas don’t see the light of realization because either they are not well communicated or well supported. Here I am talking about ideas to create or build something new. So how do you effectively communicate a design idea? Simple and convincing way to communicate a design idea is to create a 3D model. In today’s World, there are plenty of incredibly powerful 3D modeling tools available to create a digital model of any possible idea. Having worked on many such tools, this time, I thought let me try to shape my idea with a very simple yet powerful 3D model creation and editing tool <a href="http://www.123dapp.com/design">AUTODESK 123D Design</a> I found it’s incredibly simple to learn <a href="http://www.123dapp.com/howto/design">how-to-use</a> 123D Design to shape your imagination. It didn’t take more than 10 minutes for me to build a simple Lamp shade. Here is my idea translated to a 3D model.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73d9ee5f2970d-pi" style="display: inline;"><img alt="Basic_Lamp_Shade" class="asset  asset-image at-xid-6a0167607c2431970b01a73d9ee5f2970d img-responsive" src="/assets/image_114001.jpg" title="Basic_Lamp_Shade" /></a><br />&#0160;</p>
<p>Having done this quickly, my role as designer was almost completed. However, I entered into next level of challenge. How do I show this lamp shade in the room which I had created earlier using <a href="http://www.autodesk.com/products/autodesk-revit-family/overview">Revit</a> ? In this era of cloud and internet, without wasting even a second we try to search www (google it :) &#0160;) and that gave me some useful leads. But I was not very convinced with that. I started to explore with an idea to create a family in Revit from this 123D model. That took some-time, but finally I did it!&#0160;</p>
<p>&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a51193da9c970c-pi" style="display: inline;"><img alt="Revit_AddIn_App" class="asset  asset-image at-xid-6a0167607c2431970b01a51193da9c970c img-responsive" src="/assets/image_618628.jpg" title="Revit_AddIn_App" /></a><br />&#0160;</p>
<p>&#0160; <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a51193dab6970c-pi" style="display: inline;"><img alt="Select_123D_files" class="asset  asset-image at-xid-6a0167607c2431970b01a51193dab6970c img-responsive" src="/assets/image_298902.jpg" title="Select_123D_files" /></a></p>
<p>&#0160;</p>
<p>You can download this simple <span class="asset  asset-generic at-xid-6a0167607c2431970b01a73d9ee65d970d img-responsive"><a href="http://adndevblog.typepad.com/files/123dtorevitfamilyapp.zip">123DToRevitFamilyApp</a></span>&#0160;and the associated <span class="asset  asset-generic at-xid-6a0167607c2431970b01a3fce41da3970b img-responsive"><a href="http://adndevblog.typepad.com/files/123dimportaddin.zip">Addin</a>&#0160;</span>file which will allow the user to select a 123D model and convert to a Revit family. Please make sure to change the dll path in the addin file before you try this plugin tool –</p>
<p>&lt;Assembly&gt;C:\Temp\123DToRevitFamily.dll&lt;/Assembly&gt;&#0160;</p>
<p>You might be super excited to give it a try…don’t wait…before it goes off, check it out!</p>
<p>I would be happy to see your feedback :)</p>
