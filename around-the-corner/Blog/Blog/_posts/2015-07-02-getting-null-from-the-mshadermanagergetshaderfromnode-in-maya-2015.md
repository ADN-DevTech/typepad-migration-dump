---
layout: "post"
title: "Getting null from the MShaderManager::getShaderFromNode() in Maya 2015"
date: "2015-07-02 20:35:27"
author: "Cheng Xi Li"
categories:
  - "Maya"
original_url: "https://around-the-corner.typepad.com/adn/2015/07/getting-null-from-the-mshadermanagergetshaderfromnode-in-maya-2015.html "
typepad_basename: "getting-null-from-the-mshadermanagergetshaderfromnode-in-maya-2015"
typepad_status: "Publish"
---

<p>Getting null from the MShaderManager::getShaderFromNode()&#0160;in Maya 2015? Here comes one explanation.&#0160;To make a surface shader work in VP2, Maya has to sync the surface shader with its connected shape. It happens automatically with build-in shapes, but for custom meshes defined in a plugin, you need to get the shader through MShaderManager::getShaderFromNode() and set it with MRenderItem::setShader(). It becomes tricky in Maya 2015 since the MShaderManager cannot do this for you automatically and you will get nothing from MShaderManager::getShaderFromNode().Therefore, the only way to make it work is to assign your surface shader with a built-in mesh to make the MShaderManager::getShaderFromNode() work in VP2.</p>
<p>What about Maya 2016? Luckily, it has been improved: the MShaderManager::getShaderFromNode will check whether it has been synced and do the required job for you.&#0160;</p>
