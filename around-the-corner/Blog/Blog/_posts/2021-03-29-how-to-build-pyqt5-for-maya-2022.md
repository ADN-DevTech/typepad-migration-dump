---
layout: "post"
title: "How to build PyQt5 for Maya 2022"
date: "2021-03-29 20:04:47"
author: "Cheng Xi Li"
categories:
  - "Cheng Xi Li"
  - "Maya"
  - "Python"
  - "Qt"
original_url: "https://around-the-corner.typepad.com/adn/2021/03/how-to-build-pyqt5-for-maya-2022.html "
typepad_basename: "how-to-build-pyqt5-for-maya-2022"
typepad_status: "Publish"
---

<p>Maya 2022 is shipped with Python 3.7 and Python 2.7 on Windows/Linux. Since Python 2 is no longer supported by the Python community, we encourge you to update to Python 3. This guide will only cover how to build and install PyQt5 for Maya 2022 with Python 3.</p>

<p>Although pip is shipped with Maya 2022, <strong>we can use it to install PyQt5 on Linux only</strong>. For Windows and Mac, we'll still need to build it manually.</p>

<p>When I am writing this guide, I am building with <strong>PyQt5-5.15.4.dev2103021428</strong> and <strong>sip-6.0.3.dev2103021424</strong>.  You can get them from <a href="https://riverbankcomputing.com/software/pyqt/download">Riverbank computing</a>'s website.</p>

<h2>Windows:</h2>

<p>We are going to build it with VS2019 which is same as Maya 2022's developing environment on Windows. If you've installed a path needs Administrator Privileges, please run <strong>x64 Native Tools Command Prompt for VS 2019</strong> as Administrator. I've installed my Maya 2022 to default location and I've extracted devkit to g:\case\devkitBase\2022. Please update it with the paths on your computer. Here are the steps:</p>

<ol>
<li><p>Subst your maya install folder to a dedicate drive if there is a space inside your maya installation path. I am going to subst it to drive v e.g.</p>

<pre><code>subst v: "c:\program files\autodesk\maya2022"
</code></pre></li>
<li><p>Add your <strong>maya2022\Python37\scripts and maya2022\bin</strong> to your system path. If your've created a subst drive, please use the path with drive. e.g.</p>

<pre><code>set path=%path%;v:\bin;v:\Python37\Scripts
</code></pre></li>
<li><p>Copy <strong>include\Python37\Python</strong> inside Maya2022 installation as <strong>Python37\include</strong>.</p></li>
<li><p>Copy <strong>lib\python37.lib</strong> inside Maya2022 installation to <strong>Python37\libs</strong>. You'll need to create the destination folder. Once you've copied the libarary, please duplicate one and name it as <strong>python3.lib</strong>.</p></li>
<li><p>Use mayapy to install packaging and toml. e.g.</p>

<pre><code>mayapy -m pip install toml
mayapy -m pip install packaging
</code></pre></li>
<li><p>Go to sip source folder. Use mayapy to install sip. e.g.</p>

<pre><code>v:\bin\mayapy setup.py install
</code></pre></li>
<li><p>install PyQt-builder and PyQt5-sip with pip</p>

<pre><code>mayapy -m pip install PyQt-builder
mayapy -m pip install PyQt5-sip
</code></pre></li>
<li><p>Download devkit, unzip the mkspec in the mkspec folder directly into its directory. Unzip the Qt include files into the include directory directly. Rename <strong>include\qtnfc</strong> to <strong>qtnfc.disabled</strong>.</p></li>
<li><p>Update <strong>mkspecs/common/msvc-desktop.conf</strong> in the mkspecs, enable shell and add COM libraries to build config.</p>

<pre><code>QMAKE_LIBS              = ole32.lib OleAut32.lib
QMAKE_LIBS_QT_ENTRY     = -lqtmain -lshell32
</code></pre></li>
<li><p>Go to PyQt5 source folder. Use following command to install PyQt5. Please update the devkit path with the one on your computer.</p>

<pre><code>sip-install --jobs 32 --no-designer-plugin --spec g:\case\devkitBase\2022\mkspecs\win32-msvc --qmake g:\case\devkitBase\2022\devkit\bin\qmake.exe --verbose
</code></pre>

<p>The jobs count shouldn't be more than the count of your logical processors. I am using a 5950x which has 32 logical processors so the jobs is 32 here. If you don't want debug output, please remove the <strong>--verbose</strong> flag in the end.</p></li>
</ol>

<p>Vola, it works.</p>

<h2>Mac:</h2>

<p>If you are going to install it on MacOSX Catalina, please install the latest Xcode 11. As I am writing this guide, there will be warning when using Xcode 12 to build and install PyQt5. Since Xcode 12 is required by BigSur, please install it on MacOSX Catalina or Mojave if possible. I've installed Maya 2022 to default path and extract devkit and PyQt5 source codes into <strong>~/Documents/pyqt5.2022/</strong>. Please update the path in the scripts if you are using a different location. </p>

<ol>
<li><p>Download devkit, unzip the mkspec in the mkspec folder directly into its directory. Unzip the Qt include files into the include directory directly. Rename <strong>include\qtnfc</strong> to <strong>qtnfc.disabled</strong>.</p></li>
<li><p>Go to sip source code folder. Build and install sip. e.g.</p>

<pre><code>sudo /Applications/Autodesk/Maya2022/Maya.app/Contents/bin/mayapy setup.py install
</code></pre></li>
<li><p>Use pip to install PyQt-builder and PyQt5-sip. e.g.</p>

<pre><code>sudo /Applications/Autodesk/Maya2022/Maya.app/Contents/bin/mayapy -m pip install PyQt-builder
sudo /Applications/Autodesk/Maya2022/Maya.app/Contents/bin/mayapy -m pip install PyQt5-sip
</code></pre></li>
<li><p>Use following script to build PyQt5. Please update the path with your Maya 2022 installation path.</p>

<pre><code>#!/usr/bin/env bash
export PYTHONHOME=/Applications/Autodesk/maya2022/Maya.app/Contents/Frameworks/Python.framework/Versions/Current
export DYLD_LIBRARY_PATH=/Applications/Autodesk/maya2022/Maya.app/Contents/MacOS:$DYLD_LIBRARY_PATH
export DYLD_FRAMEWORK_PATH=/Applications/Autodesk/maya2022/Maya.app/Contents/Frameworks:$DYLD_FRAMEWORK_PATH
/Applications/Autodesk/Maya2022/Maya.app/Contents/Frameworks/Python.framework/Versions/Current/bin/sip-install --jobs 16 --no-designer-plugin --spec ~/Documents/pyqt5.2022/devkit/mkspecs/macx-clang --qmake ~/Documents/pyqt5.2022/devkit/devkit/bin/qmake --verbose
</code></pre>

<p>Copy these scripts into a script file inside the directory of PyQt5 source files and execute it with sudo. If you can't execute it, please use <em><em>chmod +x *filename</em> *</em> to make it executable.</p></li>
<li><p>Go to build folder, remove the install-distinfo from the install target in the <strong>Makefile</strong>, e.g.</p>

<pre><code>install:install_subtargets install_uic install_init install_scripts install-distinfo FORCE
</code></pre>

<p>to</p>

<pre><code>install:install_subtargets install_uic install_init install_scripts FORCE
</code></pre></li>
<li><p>Install the build first.</p>

<pre><code>sudo make install
</code></pre></li>
<li><p>Use following script to execute install-distinfo target. Please update it with your local paths.</p>

<pre><code>#!/usr/bin/env bash
export PYTHONHOME=/Applications/Autodesk/maya2022/Maya.app/Contents/Frameworks/Python.framework/Versions/Current
export DYLD_LIBRARY_PATH=/Applications/Autodesk/maya2022/Maya.app/Contents/MacOS:$DYLD_LIBRARY_PATH
export DYLD_FRAMEWORK_PATH=/Applications/Autodesk/maya2022/Maya.app/Contents/Frameworks:$DYLD_FRAMEWORK_PATH
/Applications/Autodesk/Maya2022/Maya.app/Contents/Frameworks/Python.framework/Versions/Current/bin/sip-distinfo --inventory /Users/lichengxi/Documents/pyqt5.2022/PyQt5-5.15.4.dev2103021428/build/inventory.txt --project-root /Users/lichengxi/Documents/pyqt5.2022/PyQt5-5.15.4.dev2103021428 --prefix "" --generator sip-build --console-script pylupdate5=PyQt5.pylupdate_main:main --console-script pyrcc5=PyQt5.pyrcc_main:main --console-script pyuic5=PyQt5.uic.pyuic:main --requires-dist "PyQt5-sip (&gt;=12.8, &lt;13)" /Applications/Autodesk/maya2022/Maya.app/Contents/Frameworks/Python.framework/Versions/Current/lib/python3.7/site-packages/PyQt5-5.15.4.dev2103021428.dist-info
</code></pre>

<p>Copy these scripts into a script file and execute it with sudo.</p></li>
</ol>

<p>That's it.</p>

<h2>Linux</h2>

<p>You can use pip to install PyQt5. e.g.
    sudo mayapy -m pip install PyQt5</p>

<h2>Test your installation</h2>

<p>After install PyQt5, you can use following script to test your installation.</p>

<pre><code>import sys
from PyQt5.QtWidgets import (QWidget, QToolTip, QPushButton)
from PyQt5.QtGui import QFont    

class Example(QWidget):    
    def __init__(self):
        super(Example,self).__init__()       
        self.initUI()


    def initUI(self):        
        QToolTip.setFont(QFont('SansSerif', 10))        
        self.setToolTip('This is a &lt;b&gt;QWidget&lt;/b&gt; widget')        
        btn = QPushButton('Button', self)
        btn.setToolTip('This is a &lt;b&gt;QPushButton&lt;/b&gt; widget')
        btn.resize(btn.sizeHint())
        btn.move(50, 50)               
        self.setGeometry(300, 300, 300, 200)
        self.setWindowTitle('Tooltips')    
        self.show()      

ex = Example()
</code></pre>

<p>If PyQt5 is properly installed, you will see a popup window with a button.</p>
