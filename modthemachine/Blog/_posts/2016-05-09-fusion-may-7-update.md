---
layout: "post"
title: "Fusion May 7 Update"
date: "2016-05-09 21:50:17"
author: "Adam Nagy"
categories:
  - "Brian"
original_url: "https://modthemachine.typepad.com/my_weblog/2016/05/fusion-may-7-update.html "
typepad_basename: "fusion-may-7-update"
typepad_status: "Publish"
---

<p>This past Saturday a new Fusion update went out.&nbsp; There is some API functionality in that update that enables a lot more types of programs.&nbsp; These new capabilities are attributes and base features.</p> <p>The ability to associate data with any entity is now supported through attributes. Every object that supports attributes now has an 'attributes' property that returns an Attributes object.&nbsp; You can read more about this and why you might want to use it in the <a href="http://help.autodesk.com/cloudhelp/ENU/Fusion-360-API/files/Attributes_UM.htm">User Manual topic for attributes</a>.</p> <p>There is also now support to create base features and to create entities within a base feature. For example, you can use the new Sketches.addToBaseOrFormFeature method to create a new sketch in an existing base feature, or the MeshBody.add method now supports an argument to let you specify a base feature to create the mesh body within.&nbsp; Base features provide a way to create direct-modeling geometry within a parametric model.&nbsp; It’s like an “island” of direct modeling.&nbsp; This is useful in a few cases where certain types of creation are only supported in direct modeling mode but instead of converting the entire design and losing all of the modeling intelligence, you can create a base feature and create the direct modeling geometry within the base feature.</p> <p>One more thing that’s not new API functionality is that the <a href="http://help.autodesk.com/cloudhelp/ENU/Fusion-360-API/files/Commands_UM.htm">User Manual topic for Commands</a> has been re-written and better describes the full capability of creating custom commands.&nbsp; I know that the documentation for creating custom commands wasn’t the best and people were struggling with commands.&nbsp; Hopefully this will help a lot of you.</p>
