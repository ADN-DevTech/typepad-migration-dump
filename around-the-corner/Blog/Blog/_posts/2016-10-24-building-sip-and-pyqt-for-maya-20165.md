---
layout: "post"
title: "Building SIP, and PyQt for Maya 2016.5"
date: "2016-10-24 05:10:56"
author: "Cyrille Fauvel"
categories:
  - "Cyrille Fauvel"
  - "Linux"
  - "Mac"
  - "Maya"
  - "Python"
  - "Qt"
  - "Vijay Prakash"
  - "Windows"
original_url: "https://around-the-corner.typepad.com/adn/2016/10/building-sip-and-pyqt-for-maya-20165.html "
typepad_basename: "building-sip-and-pyqt-for-maya-20165"
typepad_status: "Publish"
---

<p><strong>This is an updated version for Maya 2016.5&#39;&#0160;PyQt build instructions.</strong></p>
<p>This is an updated version of the Maya 2016 build instructions for PyQt and PySide (<a href="http://around-the-corner.typepad.com/adn/2015/05/building-sip-and-pyqt-for-maya-2016.html" target="_self">here</a>). The very good news is that with Maya 2014 &amp; onward, there is no more need to build PySide as it is coming by default in Maya :)&#0160;</p>
<p>libxml, openSSL, OpenAL,&#0160;python2.7, qt-4.8.5-64 / Maya 2014/2015, qt-4.8.6-64 / Maya 2016&#0160;and tbb&#0160;are also coming by default in the Maya include and lib folder, so unless you have a very specific need, you would not need to rebuild any of those libraries like before. Note as well that there is a &#39;C:\Program Files\Autodesk\Maya2016\support\opensource&#39; folder now which contains some of the community source. <a href="http://www.autodesk.com/lgplsource" target="_self">http://www.autodesk.com/lgplsource</a> is also a location you want to remember for future.</p>
<p>I&#39;ll come back later with instructions to rebuild Qt itself for those who needs to, but again you do not need to build Qt to build PyQt anymore.</p>
<p><a href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017c320cc3ff970b-pi"><img alt="Build-headacke" border="0" src="/assets/image_9559c3.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Build-headacke" /></a></p>
<p>Maya 2016.x also includes most of the tools we need to build PyQt without the need of compiling Qt source &amp; tools - so in this post we will see the easy way to build PyQt.</p>
<p>Download SIP and PyQt source from &#39;<a href="http://www.riverbankcomputing.co.uk" target="_self">http://www.riverbankcomputing.co.uk</a>&#39; - here I downloaded &#39;sip-4.16.7&#39; and &#39;PyQt-win-gpl-4.11.3&#39; (Note that you do not have to use these versions if you prefer using an older version, all you need is to use a version compatible with Qt4.8.6).&#0160;Unzip them in one folder, then you should get something like this:</p>
<h2>Mac</h2>
<p style="padding-left: 30px;"><em>/Users/cyrille/Documents/_Maya2016Scripts/sip-4.18</em><br /><em>/Users/cyrille/Documents/_Maya2016Scripts/PyQt-mac-gpl-4.11.4</em></p>
<p style="padding-left: 30px;"><em>&#39;/Users/cyrille/Documents/_Maya2016Scripts&#39; being my local folder. Now the instructions, and bash scripts to build that stuff.</em></p>
<p>Follow the instructions from the API docs to setup your environment (Developer Resources &gt; API Guide &gt; Setting up your build environment &gt; Mac OS X environment, in the Maya Documentation)&#0160;</p>
<p>Untar&#0160;the /devkit/include/qt-4.8.6-include.tar.gz into /devkit/include/Qt</p>
<p>Copy /Resources/qt.conf into /bin/qt.conf and edit it like this:</p>
<pre class="brush: bash; toolbar: false;">[Paths]
Prefix=
Libraries=../MacOS
Binaries=../bin
Headers=../../../devkit/include/Qt
Data=..
Plugins=../qt-plugins
Translations=../qt-translations
</pre>
<p>Untar the qt-4.8.6-64-mkspecs.tar.gz into $MAYA_LOCATION/Maya.app/Contents/mkspecs. And make sure the qconfig.pri looks like this:</p>
<p>qconfig.pri</p>
<pre class="brush: bash; toolbar: false;">#configuration
CONFIG += release def_files_disabled exceptions no_mocdepend stl x86_64 qt #qt_framework
QT_ARCH = macosx
QT_EDITION = OpenSource
QT_CONFIG +=  minimal-config small-config medium-config large-config full-config no-pkg-config dwarf2 phonon phonon-backend accessibility opengl reduce_exports ipv6 getaddrinfo ipv6ifname getifaddrs png no-freetype system-zlib nis cups iconv openssl corewlan concurrent xmlpatterns multimedia audio-backend svg script scripttools declarative release x86_64 qt #qt_framework

#versioning
QT_VERSION = 4.8.6
QT_MAJOR_VERSION = 4
QT_MINOR_VERSION = 8
QT_PATCH_VERSION = 6

#namespaces
QT_LIBINFIX = 
QT_NAMESPACE = 
QT_NAMESPACE_MAC_CRC = 
</pre>
<p>You also need to create copy of the Qt lib files as fake .dylib files from the /MacOS directory.&#0160;The script below will tell what to do when you first run it. I removed the lengthy &#39;if&#39; from the script on that page, since I now posted the scripts on <a href="https://github.com/cyrillef/Maya-PyQt-Scripts" target="_self">github</a>.</p>
<p>Build &amp; Install SIP running <a href="https://github.com/cyrillef/Maya-PyQt-Scripts/blob/v2016.5/osx/sip">this scripts</a> from the github repo</p>
<p>Build &amp; Install PyQt running <a href="https://github.com/cyrillef/Maya-PyQt-Scripts/blob/v2016.5/osx/pyqt">this scripts</a> from the github repo</p>
<p><strong>Note:</strong> In the shipping version of Maya 2016.5, PyQT will fail to execute because of a hidden phonon symbol. There is a QTBUG reference at <a href="https://bugreports.qt.io/browse/QTBUG-37209" target="_self">https://bugreports.qt.io/browse/QTBUG-37209</a> which would support the change in compiler - exporting symbol issue. There is a Qt source change suggested for the CLANG build to export symbols. You can get the fixed file from <a href="https://github.com/cyrillef/Maya-PyQt-Scripts/archive/v2016.5.tar.gz" target="_self">here</a> in the meantime, but note that it would break the digital signature of Maya on OSX. Copy the file binaries/phonon into &#39;/Applications/Autodesk/maya2016.5/Maya.app/Contents/MacOS&#39; and restart Maya.</p>
<p>You&#39;re done! go to the testing paragraph at the end of the article.</p>
<p>Download the scripts from github <a href="https://github.com/cyrillef/Maya-PyQt-Scripts" target="_self">here</a>.</p>
<h2>Linux</h2>
<p style="padding-left: 30px;"><em>/home/cyrille/Documents/_Maya2016Scripts/sip-4.18</em><br /><em>/home/cyrille/Documents/_Maya2016Scripts/PyQt-mac-gpl-4.11.4</em></p>
<p style="padding-left: 30px;"><em>&#39;/home/cyrille/Documents/_Maya2016Scripts&#39; being my local folder. Now the instructions, and bash scripts to build that stuff.</em></p>
<p>Follow the instructions from the API docs to setup your environment (Developer Resources &gt; API Guide &gt; Setting up your build environment &gt; Linux environments (64 bit), in the Maya Documentation).</p>
<p>Edit your qt.conf file (/usr/autodesk/maya2016.5-x64/bin) like below</p>
<pre>[Paths]<br />Prefix=<br />Libraries=../lib<br />Binaries=../bin<br />Headers=../include/Qt<br />Data=../<br />Plugins=../qt-plugins<br />Translations=../qt-translations</pre>
<p>Untar the /include/qt-4.8.6-include.tar.gz into /include/Qt</p>
<p>Untart the /mkspecs/qt-4.8.6-mkspecs.tar.gz into /mkspecs</p>
<p>Make qmake, moc executables from the Maya bin directory</p>
<pre class="brush: shell; toolbar: false;">sudo chmod aog+x /usr/autodesk/maya2016.5-x64/bin/moc
sudo chmod aog+x /usr/autodesk/maya2016.5-x64/bin/qmake
</pre>
<p>Build &amp; Install SIP running <a href="https://github.com/cyrillef/Maya-PyQt-Scripts/blob/v2016.5/linux/sip">this script</a> from the github repo</p>
<p>Build &amp; Install PyQt running <a href="https://github.com/cyrillef/Maya-PyQt-Scripts/blob/v2016.5/linux/pyqt">this script</a> from the github repo</p>
<p>You&#39;re done! go to the testing paragraph at the end of the article.</p>
<p>Download the scripts from github&#0160;<a href="https://github.com/cyrillef/Maya-PyQt-Scripts" target="_self">here</a>.</p>
<h2>Windows</h2>
<p style="padding-left: 30px;"><em>D:\__sdkext\_Maya2016 Scripts\sip-4.18</em><br /><em>D:\__sdkext\_Maya2016 Scripts\PyQt-win-gpl-4.11.4</em></p>
<p style="padding-left: 30px;"><em>&#39;D:\__sdkext\_Maya2016 Scripts&#39; being my local folder. Now the instructions and scripts to build that stuff.</em></p>
<p>Follow the instructions from the API docs to setup your environment (Developer Resources &gt; API Guide &gt; Setting up your build environment &gt; Windows environment (64‐bit), in the Maya Documentation)</p>
<p>Edit your qt.conf file (C:\Program Files\Autodesk\Maya2016.5\bin) like below</p>
<pre>[Paths]<br />Prefix=<br />Libraries=../lib<br />Binaries=../bin<br />Headers=../include/Qt<br />Data=../<br />Plugins=../qt-plugins<br />Translations=../qt-translations</pre>
<p>Unzip the /include/qt-4.8.6-64-include.tar.gz into /include/Qt</p>
<p>Unzip the /mkspecs/qt-4.8.6-64-mkspecs.tar.gz into /mkspecs</p>
<p>Build &amp; Install SIP running <a href="https://github.com/cyrillef/Maya-PyQt-Scripts/blob/v2016.5/win/sip.bat">this script</a> from the github repo</p>
<p>Build &amp; Install PyQt running <a href="https://github.com/cyrillef/Maya-PyQt-Scripts/blob/v2016.5/win/pyqt.bat">this script</a> from the github repo</p>
<p>Note that I had issues building PyQT on Windows this time. When the makefile was building &#39;pylupdate&#39;, it hangs forever, and complains about an undefined macro &lt;?, grrrr.... The reason is that pyQT makefile now uses that macro which means use the first build dependency file. But nmake does not seems to like it, so I added some code to bypass the makefile rule. See below the call to &#39;%QTDIR%\bin\moc.exe -o moc_translator.cpp translator.h&#39;</p>
<p>You&#39;re done! go to the testing paragraph at the end of the article.</p>
<p>Download the scripts from github&#0160;<a href="https://github.com/cyrillef/Maya-PyQt-Scripts" target="_self">here</a>.</p>
<h2>Testing</h2>
<p>Copy and paste this example in the Maya Script Editor (in a Python tab), and execute the code:</p>
<pre class="brush: python; toolbar: false;">import sys
from PyQt4 import QtGui


class Example(QtGui.QWidget):
    
    def __init__(self):
        super(Example, self).__init__()
        
        self.initUI()
        
    def initUI(self):      

        self.btn = QtGui.QPushButton(&#39;Dialog&#39;, self)
        self.btn.move(20, 20)
        self.btn.clicked.connect(self.showDialog)
        
        self.le = QtGui.QLineEdit(self)
        self.le.move(130, 22)
        
        self.setGeometry(300, 300, 290, 150)
        self.setWindowTitle(&#39;Input dialog&#39;)
        self.show()
        
    def showDialog(self):
        
        text, ok = QtGui.QInputDialog.getText(self, &#39;Input Dialog&#39;, &#39;Enter your name:&#39;)
        
        if ok:
            self.le.setText(str(text))
        
ex = Example()
</pre>
<p>&#0160;If you see the dialog is showing, you are all set :)</p>
<p>&#0160;</p>
