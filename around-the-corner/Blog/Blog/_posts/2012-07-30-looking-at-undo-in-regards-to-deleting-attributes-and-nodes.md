---
layout: "post"
title: "Looking at undo in regards to deleting attributes and nodes"
date: "2012-07-30 02:08:00"
author: "Kristine Middlemiss"
categories:
  - "C++"
  - "Kristine Middlemiss"
  - "Linux"
  - "Mac"
  - "Maya"
  - "MEL"
  - "Python"
  - "Windows"
original_url: "https://around-the-corner.typepad.com/adn/2012/07/looking-at-undo-in-regards-to-deleting-attributes-and-nodes.html "
typepad_basename: "looking-at-undo-in-regards-to-deleting-attributes-and-nodes"
typepad_status: "Publish"
---

<p>Today, I want to talk about the recommended way of handling the deletion of attributes and nodes within Maya through the wise guidance of Dean Edmonds (Autodesk Maya API Architect). We will examine this potential solution for this scenario:</p>
<p style="text-align: left;"><a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d016768d4beec970b-pi" style="display: inline;"><img alt="Image" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d016768d4beec970b image-full" src="/assets/image_67d02d.jpg" title="Image" /></a><br />Let’s say you have plug-in that has a connection to a user defined attributes on other objects in a scene. You may write some code that detects that if the node is deleted, then the user defined attributes on the connected objects are deleted.</p>
<p>One way to do this maybe through the passThroughToOne() method and in there you traverse the graph to the connected nodes. Then using the executeCommandOnIdle() method run the MEL command deleteAttr on the unwanted attribute, which supports undo.</p>
<p><a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017743b00a30970d-pi" style="display: inline;"><img alt="Image" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017743b00a30970d image-full" src="/assets/image_016bde.jpg" title="Image" /></a><br />Let’s examine some better ways, as well as looking at the downfalls. Let&#39;s define some terminology first. When I say &quot;deleteNode command&quot; I mean whatever command caused your node to be deleted. When I say &quot;deleteAttr command&quot; I mean the &#39;deleteAttr&#39; your node is trying to execute when it is deleted.</p>
<p>Potentially you could use executeCommandOnIdle(), however this would cause a problem. If you executed the deleteAttr command immediately then it should end up being included in the same undo chunk as the deleteNode command.</p>
<p>So the first question is to ask is why are you using executeCommandOnIdle() rather than just executeCommand() and see if there are other ways of addressing those problems.</p>
<p>However if you really do need to do the deleteAttr command using executeCommandOnIdle() then you can use the &#39;undoInfo -stateWithoutFlush off&#39; command to temporarily disable the undo queue just before doing the deletion, then &#39;undoInfo -stateWithoutFlush on&#39; immediately after. E.g:</p>
<p>&#0160;&#0160;&#0160; MString cmd = MString( &quot;undoInfo -swf off; deleteAttr &quot;) + attrName + &quot;; undoInfo -swf on;&quot;;<br />&#0160;&#0160;&#0160; MGlobal::executeCommandOnIdle(cmd);</p>
<p>The one thing to point out though, is you need to be very careful, that no other scene-modifying commands execute while the undo queue is off.</p>
<p>You will then need a way to restore the deleted attribute when the deleteNode command is undone. There are a couple of ways of doing that. The most flexible is probably to create an MPxCommand which does nothing in its doIt() but restores the attribute in its redoIt(). So when your node is deleted, it would do something like this:</p>
<p>&#0160;&#0160;&#0160; MString cmd = MString(&quot;myRestoreAttr &quot;) + attrName;<br />&#0160;&#0160;&#0160; MGlobal::executeCommand(cmd, false, true /* add it to the undo queue */);</p>
<p>&#0160;&#0160;&#0160; cmd = MString( &quot;undoInfo -swf off; deleteAttr &quot;) + attrName + &quot;; undoInfo -swf on;&quot;;<br />&#0160;&#0160;&#0160; MGlobal::executeCommandOnIdle(cmd);</p>
<p>When the node is deleted, &#39;myRestoreAttr&#39; command will be executed. It won&#39;t do anything but it will be added to the same undo chunk as the deleteNode command.</p>
<p>Then, during the next idle cycle, the attribute will be deleted.</p>
<p>When the deleteNode command is undone, it will undo the &#39;myRestoreAttr&#39; command, which will add the attr back onto the target node.</p>
<p>There are some potential problems with this approach:</p>
<p>1) The timing might not work out. The attribute needs to be restored before the deleteNode command attempts to reconnect your node to it. That will all depend upon how you detect the node deletion. Hopefully one of MNodeMessage::addNodeAboutToDeleteCallback() or addNodePreRemovalCallback() will do the trick.</p>
<p>2) The attribute disconnection done by the deleteNode command may use the attribute&#39;s internal pointer for the reconnection, not its name, in which case Maya will probably crash. To get around that, rather than using a &#39;deleteAttr&#39; command to delete the attribute, use another MPxCommand which uses an MDGModifier to delete it. It would share that MDGModifier with the &#39;myRestoreAttr&#39; command which could then use it to undo the attribute deletion. That way it&#39;s still the same original attribute being restored and Maya&#39;s internal pointers to it will still be valid.</p>
<p>3) There&#39;s no guarantee that the idle command will have had a chance to execute before the node deletion is undone. That means that &#39;myRestoreAttr&#39; may be called while the attribute is still there. Note that the MDGModifier solution to problem 2 above will take care of this since undoing an MDGModifier which hasn&#39;t been done yet will have no effect.</p>
<p>4) The fact that the idle command may not be called before the node deletion is undone also means that it might execute *after* the node deletion has been undone. So if you are not careful you will end up trying to delete the attribute while your node is still connected to it. How you address that depends upon how your code is set up, but it should be pretty straightforward.</p>
<p>Enjoy,</p>
<p>Kristine</p>
