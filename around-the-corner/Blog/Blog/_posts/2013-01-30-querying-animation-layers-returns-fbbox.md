---
layout: "post"
title: "Querying animation layers returns FBBox"
date: "2013-01-30 01:52:00"
author: "Cyrille Fauvel"
categories:
  - "Cyrille Fauvel"
  - "MotionBuilder"
  - "Python"
original_url: "https://around-the-corner.typepad.com/adn/2013/01/querying-animation-layers-returns-fbbox.html "
typepad_basename: "querying-animation-layers-returns-fbbox"
typepad_status: "Publish"
---

<p>This is more a bug report with its workaround than anything else. But this one seems serious enough for me to bring it here. On MotionBuilder 2012 -&gt; 2013 (and on the current PR release), if you attempt to query an animation layer from the current take, an FBBox object will be returned.</p>
<p>
<a class="asset-img-link" style="display: inline;" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017c365f6628970b-pi"><img class="asset  asset-image at-xid-6a0163057a21c8970d017c365f6628970b" style="display: block; margin-left: auto; margin-right: auto;" title="Boxes" src="/assets/image_329aa2.jpg" border="0" alt="Boxes" /></a>But on new scenes (yet to be saved), FBAnimationLayer objects will be returned for the two default animation layers. But for any new layer you add, only FBBox objects will be returned for those. If you then save the scene file, reopen it, FBBox objects are return for ALL of the animation layers. You'll get this issue whenever you try to iterate through the FBScene::Components object list property, or use the FBFindObjectByFullName() method.</p>
<p>To workaround that problem till the issue is fixed, you can instead use the FBAnimationLayer object like this:</p>
<pre class="brush:python; toolbar:no;">from pyfbsdk import *
FBApplication().FileNew()
lTake = FBSystem().Scene.Take[0]
lTake.CreateNewLayer()
lTake.CreateNewLayer()
for i in range (0, lTake.GetLayerCount()):
  print lTake.GetLayer(i)
FBApplication().FileSave(r"myscene.fbx")

FBApplication().FileNew()
FBApplication().FileOpen(r"myscene.fbx")
lTake = FBSystem().Scene.Take[0]
for i in range (0, lTake.GetLayerCount()):
  print lTake.GetLayer(i)
</pre>
