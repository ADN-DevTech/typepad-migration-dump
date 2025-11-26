---
layout: "post"
title: "How to build PyQt5 for Maya 2023"
date: "2022-03-30 17:25:20"
author: "Cheng Xi Li"
categories:
  - "Cheng Xi Li"
  - "Maya"
  - "Python"
original_url: "https://around-the-corner.typepad.com/adn/2022/03/how-to-build-pyqt5-for-maya-2023.html "
typepad_basename: "how-to-build-pyqt5-for-maya-2023"
typepad_status: "Publish"
---

<p>Maya 2023 is shipped with Python 3.9 <strong>only</strong> on all platforms. Although pip is shipped with Maya 2023, <strong>we still can’t use it to install PyQt5 on Mac</strong>. Unlike Maya 2023 on windows, pip installed packages could work. However, it has its own Qt5 libraries installed. You may want to replace them with the ones shipped with Maya. I didn’t encounter any problems during my testing with the original pip packages. If you find any problems with it, you can build your own with this guide.</p>
<p>When I am writing this guide, I am building with <strong>PyQt5-5.15.6</strong>, <strong>sip-6.5.1</strong>, <strong>PyQt5_sip-12.9.1</strong> and <strong>PyQt-builder-1.12.2</strong>.</p>
<h2 id="windows:"><a name="windows:" href="#windows:"></a>Windows:</h2>
<p>We are going to build it with VS2019 which is same as Maya 2023’s developing environment on Windows. If you’ve installed a path needs Administrator Privileges, please run <strong>x64 Native Tools Command Prompt for VS 2019</strong> as Administrator. I’ve installed my Maya 2023 to default location and I’ve extracted devkit to g:\case\devkitBase\2023. Please update it with the paths on your computer. Here are the steps:</p>
<p>1. Subst your maya install folder to a dedicate drive if there is a space inside your maya installation path. I am going to subst it to drive v e.g.</p>
<pre><code>subst v: &quot;C:\Program files\Autodesk\maya2023&quot;
</code></pre><p>2. Add your <strong>Maya2023\Python\scripts and Maya2023\bin</strong> to your system path. If you’ve created a subst drive, please use the path with drive. e.g.</p>
<pre><code>set path=%path%;v:\bin;v:\Python\Scripts
</code></pre><p>3. Copy <strong>include\Python39\Python</strong> inside Maya2023 installation to <strong>Python\include</strong>. You’ll need to create the destination folder.</p>
<p>4. Copy <strong>lib\python39.lib</strong> inside Maya2023 installation to <strong>Python\libs</strong>. You’ll need to create the destination folder. Once you’ve copied the libarary, please duplicate one and name it as <strong>python3.lib</strong>.</p>
<p>5. Go to sip source folder. Use mayapy install sip. e.g.</p><pre><code>v:\bin\mayapy setup.py install
</code></pre><p>6. install PyQt-builder and PyQt5-sip with pip</p><pre><code>mayapy -m pip install PyQt-builder

mayapy -m pip install PyQt5-sip
</code></pre><p>7. Download devkit, unzip the mkspec in the mkspec folder directly into its directory. Unzip the Qt include files into the include directory directly. Rename <strong>include\qtnfc</strong> to <strong>qtnfc.disabled</strong>.</p>
<p>8. Update <strong>mkspecs/common/msvc-desktop.conf</strong> in the mkspecs, enable shell and add COM libraries to build config.</p>
<pre><code>QMAKE_LIBS              = ole32.lib OleAut32.lib

QMAKE_LIBS_QT_ENTRY     = -lqtmain -lshell32
</code></pre><p>9.  Go to PyQt5 source folder. Use following command to install PyQt5. Please update the devkit path with the one on your computer.</p>
<pre><code>sip-install --jobs 32 --no-designer-plugin --spec g:\case\devkitBase\2023\mkspecs\win32-msvc --qmake g:\case\devkitBase\2023\devkit\bin\qmake.exe --verbose
</code></pre><p>The jobs count shouldn’t be more than the count of your logical processors. I am using a 5950x which has 32 logical processors so the jobs is 32 here. If you don’t want debug output, please remove the <strong>—verbose</strong> flag in the end.</p>
<p>Vola, it works.</p>
<h2 id="mac:"><a name="mac:" href="#mac:"></a>Mac:</h2>
<p>If you are going to install it on MacOSX Catalina or above, please install the latest Xcode 11. I’ve installed Maya 2023 to default path and extract devkit and PyQt5 source codes into <strong>~/Documents/pyqt5.2023/</strong>. Please update the path in the scripts if you are using a different location. I am building it on Xcode 11 with Big Sur. If you had encountered issue on a different building environment, please let us know.</p>
<p>1. Download devkit, unzip the mkspec in the mkspec folder directly into its directory. Unzip the Qt include files into the include directory directly. Rename <strong>include\qtnfc</strong> to <strong>qtnfc.disabled</strong>.</p>
<p>2. Install required Python packages.</p>
<pre><code>sudo ./mayapy -m pip install toml 
sudo ./mayapy -m pip install packaging
</code></pre><p>3. Go to sip source code folder. Build and install sip. e.g.</p>
<pre><code>sudo /Applications/Autodesk/Maya2023/Maya.app/Contents/bin/mayapy setup.py install
</code></pre><p>4. Go to PyQt5_sip folder, install PyQt5_sip</p>
<pre><code>sudo /Applications/Autodesk/Maya2023/Maya.app/Contents/bin/mayapy setup.py install
</code></pre><p>5. Go to PyQt builder folder, install PyQt builder.</p>
<pre><code>sudo /Applications/Autodesk/Maya2023/Maya.app/Contents/bin/mayapy setup.py install
</code></pre><p>6. Use following script to build PyQt5. Please update the path with your Maya 2023 installation path.</p>
<pre><code>#!/usr/bin/env bash
export PYTHONHOME=/Applications/Autodesk/maya2023/Maya.app/Contents/Frameworks/Python.framework/Versions/Current
export DYLD_LIBRARY_PATH=/Applications/Autodesk/maya2023/Maya.app/Contents/MacOS:$DYLD_LIBRARY_PATH
export DYLD_FRAMEWORK_PATH=/Applications/Autodesk/maya2023/Maya.app/Contents/Frameworks:$DYLD_FRAMEWORK_PATH
/Applications/Autodesk/Maya2023/Maya.app/Contents/Frameworks/Python.framework/Versions/Current/bin/sip-install --jobs 16 --no-designer-plugin --spec ~/PyQt5.2023/devkit/mkspecs/macx-clang --qmake ~/PyQt5.2023/devkit/devkit/bin/qmake --verbose
</code></pre><p>Copy these scripts into a script file inside the directory of PyQt5 source files and execute it with sudo. If you can’t execute it, please use <strong>chmod +x <em>filename</em> </strong> to make it executable.</p>
<p>That’s it.</p>
<h2 id="linux"><a name="linux" href="#linux"></a>Linux</h2>
<p>You can use pip to install PyQt5. e.g.</p>
<pre><code>sudo mayapy -m pip install PyQt5
</code></pre><h2 id="test-your-installation"><a name="test-your-installation" href="#test-your-installation"></a>Test your installation</h2>
<p>After install PyQt5, you can use following script to test your installation.</p>
<pre><code>import sys
from PyQt5.QtWidgets import (QWidget, QToolTip, QPushButton)
from PyQt5.QtGui import QFont    

class Example(QWidget):    
    def __init__(self):
        super(Example,self).__init__()       
        self.initUI()


    def initUI(self):        
        QToolTip.setFont(QFont(&#39;SansSerif&#39;, 10))        
        self.setToolTip(&#39;This is a &lt;b&gt;QWidget&lt;/b&gt; widget&#39;)        
        btn = QPushButton(&#39;Button&#39;, self)
        btn.setToolTip(&#39;This is a &lt;b&gt;QPushButton&lt;/b&gt; widget&#39;)
        btn.resize(btn.sizeHint())
        btn.move(50, 50)               
        self.setGeometry(300, 300, 300, 200)
        self.setWindowTitle(&#39;Tooltips&#39;)    
        self.show()      

ex = Example()
</code></pre>
