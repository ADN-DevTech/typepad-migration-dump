---
layout: "post"
title: "How to delete the scriptJob if it is created with parent (-p) flag after specified QWidget is closed"
date: "2016-06-27 23:20:10"
author: "Cheng Xi Li"
categories:
  - "Cheng Xi Li"
  - "Maya"
  - "Python"
  - "Qt"
original_url: "https://around-the-corner.typepad.com/adn/2016/06/how-to-delete-the-scriptjob-if-it-is-created-with-parent-p-flag-after-specified-qwidget-is-closed.html "
typepad_basename: "how-to-delete-the-scriptjob-if-it-is-created-with-parent-p-flag-after-specified-qwidget-is-closed"
typepad_status: "Publish"
---

<p>After creating a Qt window with PySide.you may want to assign a scriptJob window to it and remove the job after the window is closed with -p flag. For example in the following code:</p>
<p>&#0160;</p>
<pre><code>import maya.cmds as cmds
import maya.OpenMayaUI as omui 
import PySide.QtCore as QtCore
import PySide.QtGui as QtGui
from shiboken import wrapInstance 

mayaPtr = omui.MQtUtil.mainWindow() 
mayaWindow = wrapInstance(long(mayaPtr), QtGui.QWidget) 
scriptJobWindow = QtGui.QMainWindow(parent = mayaWindow) 
scriptJobWindow.setObjectName(&#39;scriptJobWindow&#39;) 
scriptJobWindow.setWindowTitle(&#39;scriptJobWindow&#39;)
scriptJobWindow.show()

def scriptJob(): 
    print &quot;scriptJob callback&quot; 

jobNum = cmds.scriptJob(p=&quot;scriptJobWindow&quot;, e= [&quot;SelectionChanged&quot;,scriptJob])
</code></pre>
<p>But you may be surprised to see that, even if the -p flag was specified, the scriptJob is still exists despite window being closed with the close button. How does that happen?</p>
<pre><code>scriptJob -lj;
// Result: 0:  &quot;-permanent&quot; &quot;-event&quot; &quot;PostSceneRead&quot; &quot;generateUvTilePreviewsPostSceneReadCB&quot;
 .....
 80: p=&#39;scriptJobWindow&#39;, e=[&#39;SelectionChanged&#39;, &lt;function scriptJob at 0x000002E020AF3898&gt;]
</code></pre>
<p>If you use Spy++ or similar tools, you&#39;ll find that the QWidget is still hanging out there.</p>
<p><a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d01b7c8744b29970b-pi" style="display: inline;"><img alt="Image" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d01b7c8744b29970b image-full img-responsive" src="/assets/image_abe907.jpg" title="Image" /></a></p>
<p><br />Turns out that, when a QWidget is closed, it is simply hidden instead of being destroyed. In this case you may want to change its behavior, from just being hidden, to be destroyed. The solution is simple, add WA_DeleteOnClose attribute to QWidget.</p>
<pre><code>import maya.cmds as cmds
import maya.OpenMayaUI as omui 
import PySide.QtCore as QtCore
import PySide.QtGui as QtGui
from shiboken import wrapInstance 

mayaPtr = omui.MQtUtil.mainWindow() 
mayaWindow = wrapInstance(long(mayaPtr), QtGui.QWidget) 
scriptJobWindow = QtGui.QMainWindow(parent = mayaWindow) 
scriptJobWindow.setObjectName(&#39;scriptJobWindowWithDelete&#39;) 
scriptJobWindow.setWindowTitle(&#39;scriptJobWindowWithDelete&#39;)

#Set WA_DeleteOnClose attribute
scriptJobWindow.setAttribute(QtCore.Qt.WA_DeleteOnClose) 

scriptJobWindow.show() 

def scriptJob(): 
    print &quot;scriptJob callback&quot; 

jobNum = cmds.scriptJob(p=&quot;scriptJobWindowWithDelete&quot;, e= [&quot;SelectionChanged&quot;,scriptJob])
</code></pre>
<p>Now, when closed, the window is destroyed and the scriptJob is removed automatically.</p>
