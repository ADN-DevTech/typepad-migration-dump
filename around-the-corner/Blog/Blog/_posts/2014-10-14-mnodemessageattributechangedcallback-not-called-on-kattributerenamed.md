---
layout: "post"
title: "MNodeMessage::AttributeChangedCallback() not called on kAttributeRenamed"
date: "2014-10-14 01:01:40"
author: "Cyrille Fauvel"
categories:
  - "C++"
  - "Cyrille Fauvel"
  - "Maya"
  - "Python"
original_url: "https://around-the-corner.typepad.com/adn/2014/10/mnodemessageattributechangedcallback-not-called-on-kattributerenamed.html "
typepad_basename: "mnodemessageattributechangedcallback-not-called-on-kattributerenamed"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d01bb079824e4970d-pi" style="display: inline;"><img alt="Change-My-Name" class="asset  asset-image at-xid-6a0163057a21c8970d01bb079824e4970d img-responsive" src="/assets/image_4813f4.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Change-My-Name" /></a></p>
<p>Consider the following Python example</p>
<pre class="brush: python; toolbar: false;">import maya.OpenMaya as OM
import maya.cmds

def test (msg, plug, otherplug, *clientData):
    if msg &amp; maya.OpenMaya.MNodeMessage.kConnectionMade:
        print &quot;connection made&quot;
    if msg &amp; maya.OpenMaya.MNodeMessage.kConnectionBroken:
        print &quot;connection broken&quot;
    if msg &amp; maya.OpenMaya.MNodeMessage.kAttributeEval:
        print &quot;attribute eval&quot;
    if msg &amp; maya.OpenMaya.MNodeMessage.kAttributeSet:
        print &quot;attribute set&quot;
    if msg &amp; maya.OpenMaya.MNodeMessage.kAttributeLocked:
        print &quot;attribute locked&quot;
    if msg &amp; maya.OpenMaya.MNodeMessage.kAttributeUnlocked:
        print &quot;attribute unlocked&quot;
    if msg &amp; maya.OpenMaya.MNodeMessage.kAttributeAdded:
        print &quot;attribute added&quot;
    if msg &amp; maya.OpenMaya.MNodeMessage.kAttributeRemoved:
        print &quot;attribute removed&quot;
    if msg &amp; maya.OpenMaya.MNodeMessage.kAttributeRenamed:
        print &quot;attribute renamed&quot;
    if msg &amp; maya.OpenMaya.MNodeMessage.kOtherPlugSet:
        print &quot;attribute other plug set&quot;
        
    plugName =str (plug.partialName ())
    print &quot;attrName: &quot;, plugName
    print &quot;&quot;

node =maya.cmds.createNode (&quot;addDoubleLinear&quot;)

sel =OM.MSelectionList ()
sel.add (node)
obj =OM.MObject ()
sel.getDependNode (0, obj)

OM.MNodeMessage.addAttributeChangedCallback (obj, test)

# attribute added
maya.cmds.addAttr (node, sn=&quot;test&quot;)
# connection made; other plug set
maya.cmds.connectAttr (node + &quot;.test&quot;, node + &quot;.i1&quot;)
# attribute set ; attribute eval
maya.cmds.setAttr (node + &quot;.test&quot;, 1)
# no callback triggered
maya.cmds.getAttr (node + &quot;.test&quot;)
maya.cmds.getAttr (node + &quot;.i1&quot;)
# connection broken; other plug set
maya.cmds.disconnectAttr (node + &quot;.test&quot;, node + &quot;.i1&quot;)
# attribute locked
maya.cmds.setAttr (node + &quot;.test&quot;, lock=True)
# attribute unlocked
maya.cmds.setAttr (node + &quot;.test&quot;, lock=False)
# no callback triggered
maya.cmds.renameAttr (node + &quot;.test&quot;, &quot;testing&quot;)
maya.cmds.addAttr (node + &quot;.testing&quot;, e=True, nn=&quot;test2&quot;)
# attribute removed
maya.cmds.deleteAttr (node + &quot;.testing&quot;)
</pre>
<p>it will produce the following output</p>
<pre>attribute added
attrName: test

connection made
attribute other plug set
attrName: test

connection made
attribute other plug set
attrName: i1

attribute set
attrName: test

attribute eval
attrName: test

connection broken
attribute other plug set
attrName: test

connection broken
attribute other plug set
attrName: i1

attribute locked
attrName: test

attribute unlocked
attrName: test

attribute removed
attrName: testing
</pre>
<p>What you can notice is while the SDK doc says the event will be triggered when an attribute has been renamed, the event was not called when we renamed the attribute. From the log above, you can see it never gets invoked when a dynamic attribute name is changed. (a static node attribute cannot be renamed anyway, so it applies only to dynamic attributes)</p>
<p>However this message is triggered while you set/change an attribute name alias using the aliasAttr command. For example, &#39;maya.cmds.aliasAttr (&quot;testAlias&quot;, &quot;addDoubleLinear1.i1&quot;)&#39;, and notice the output of the attribute rename callback.</p>
<p>The message is only sent when the name of an alias is changed. It doesn’t appear any messages are sent when the renameAttr command changes a dynamic attribute’s name. Editing the attribute’s nice name is not considered a rename in either case, it is done a different way.</p>
<p>Unfortunately, there is no way at this time to detect the name changed. This is a documentation defect which needs to be addressed.</p>
<p>(Contribution from Zhong Wu)</p>
<p>&#0160;</p>
