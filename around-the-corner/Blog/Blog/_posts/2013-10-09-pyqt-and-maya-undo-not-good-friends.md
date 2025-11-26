---
layout: "post"
title: "PySide/PyQt and Maya Undo not good friends"
date: "2013-10-09 06:18:51"
author: "Cyrille Fauvel"
categories:
  - "Cyrille Fauvel"
  - "Maya"
  - "MEL"
  - "Python"
  - "Qt"
original_url: "https://around-the-corner.typepad.com/adn/2013/10/pyqt-and-maya-undo-not-good-friends.html "
typepad_basename: "pyqt-and-maya-undo-not-good-friends"
typepad_status: "Publish"
---

<p>Calling Maya commands from PySide/PyQt using MEL and/or their Python wrappers will display strange messages on the Maya console during undo/redo operation, and oftenly creates concerns if this a bug, or something gets wrong.</p>
<p>
<a class="asset-img-link" style="display: inline;" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d019affc5c2a2970d-pi"><img class="asset  asset-image at-xid-6a0163057a21c8970d019affc5c2a2970d" style="display: block; margin-left: auto; margin-right: auto;" title="Not-good-enough" src="/assets/image_4f7e43.jpg" alt="Not-good-enough" /></a><br />Not at all - that is a side effect of how the command is called by the different interpreters. Consider this Python PySide/PyQt UI script:</p>
<pre class="brush: python; toolbar: false;">import sys
from PySide import QtCore, QtGui
from maya import cmds, mel

class FormExample(QtGui.QWidget):
    def __init__(self):
        super(FormExample, self).__init__()
        btn = QtGui.QPushButton('My Button', self)
        btn.resize(btn.sizeHint())
        btn.move(50, 50)
        btn.clicked.connect(self.onMyButtonClicked)      
        self.setWindowTitle('My Form Example')    
        self.show()
        
    def onMyButtonClicked(self):
        mel.eval("polySphere -ch on -o on -r 1.3;")
        # prints // Undo: polySphere -ch on -o on -r 1.3 //
         
win = FormExample()
</pre>
<p>In the PySide/PyQt version, Qt is executing the onMyButtonClicked() method which then calls mel.eval() with a command string. So MEL is being asked to execute a command string, which is what gets printed out on Undo.</p>
<p>Now consider this Python/Maya UI script:</p>
<pre class="brush: python; toolbar: false;">import sys
from maya import cmds, mel

class FormExample:
    def __init__(self):
        if cmds.window("FormExample", q=True, exists=True):
            cmds.deleteUI("FormExample")
        cmds.window("FormExample")
        cmds.columnLayout()
        cmds.button(label="My Button", command=self.onMyButtonClicked)
        cmds.window("FormExample", visible=True, e=True)

    def onMyButtonClicked(self, *args):
        mel.eval("polySphere -ch on -o on -r 1.3;")
        # prints // Undo: &lt;bound method FormExample.onMyButtonClicked of &lt;__main__.FormExample instance at 0x00000000310AAC08&gt;&gt; //

win = FormExample()
</pre>
<p>In this MEL version, the onMyButtonClicked() function is a callback which is being executed directly by MEL. So in this case MEL is being asked to execute the onMyButtonClicked() function, which has been given to it as a function object, not a string. Here understand undo records the call to 'command=self.onMyButtonClicked' vs. the MEL eval() command. Does not mean the command in eval is not record, but it is embedded in the first one.</p>
<p>Now replace:</p>
<pre class="brush: python; toolbar: false;">mel.eval("polySphere -ch on -o on -r 1.3;")</pre>
<p>by</p>
<pre class="brush: python; toolbar: false;">cmds.polySphere (ch=True, o=True, r=1.3)
# now prints // Undo:&nbsp; // in the PySide/PyQt example</pre>
<p>While you now understand why the Maya UI will still print the lengthy '//
Undo: &lt;bound method FormExample.onMyButtonClicked of &lt;__main__.FormExample instance at 0x00000000310AAC08&gt;&gt; //' message, you would see the PySide/PyQt sample printing nothing. I.e. "". This is because in Python the "string representation" of that function object is "".</p>
<p>
<a class="asset-img-link" style="display: inline;" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d019affc5d882970d-pi"><img class="asset  asset-image at-xid-6a0163057a21c8970d019affc5d882970d" style="display: block; margin-left: auto; margin-right: auto;" title="Bad-friend-friendship" src="/assets/image_d6dff0.jpg" alt="Bad-friend-friendship" /></a><br /><br /></p>
