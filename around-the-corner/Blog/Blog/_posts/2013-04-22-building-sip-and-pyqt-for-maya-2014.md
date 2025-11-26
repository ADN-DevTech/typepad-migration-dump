---
layout: "post"
title: "Building SIP, and PyQt for Maya 2014"
date: "2013-04-22 07:06:40"
author: "Cyrille Fauvel"
categories:
  - "Cyrille Fauvel"
  - "Linux"
  - "Mac"
  - "Maya"
  - "Python"
  - "Qt"
  - "Windows"
original_url: "https://around-the-corner.typepad.com/adn/2013/04/building-sip-and-pyqt-for-maya-2014.html "
typepad_basename: "building-sip-and-pyqt-for-maya-2014"
typepad_status: "Publish"
---

<p><strong>This is an updated version for Maya 2014&#39;&#0160;PyQt build instructions.</strong></p>
<p>This is an updated version of the Maya 2013 build instructions for PyQt and PySide (<a href="http://around-the-corner.typepad.com/adn/2012/10/building-qt-pyqt-pyside-for-maya-2013.html" target="_self">here</a>). The very good news is that with Maya 2014, there is no more need to build PySide as it is coming by default in Maya :)&#0160;</p>
<p>libxml, openSSL, OpenAL,&#0160;python2.7, qt-4.8.2-64, and tbb&#0160;are also coming by default in the Maya include and lib folder, so unless you have a very specific need, you would not need to rebuild any of those libraries like before. Note as well that there is a &#39;C:\Program Files\Autodesk\Maya2014\support\opensource&#39; folder now which contains some of the community source. <a href="http://www.autodesk.com/lgplsource" target="_self">http://www.autodesk.com/lgplsource</a> is also a location you want to remember for future.</p>
<p>I&#39;ll come back later with instructions to rebuild Qt itself for those who needs to, but again you do not need to build Qt to build PyQt anymore.</p>
<p><a href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017c320cc3ff970b-pi"><img alt="Build-headacke" border="0" src="/assets/image_9559c3.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Build-headacke" /></a></p>
<p>
Maya 2014 also includes most of the tools we need to build PyQt without the need of compiling Qt source &amp; tools - so in this post we will see the easy way to build PyQt.</p>
<p>Download SIP and PyQt source from &#39;<a href="http://www.riverbankcomputing.co.uk" target="_self">http://www.riverbankcomputing.co.uk</a>&#39; - here I downloaded &#39;sip-4.14.5&#39; and &#39;PyQt-win-gpl-4.10&#39;. Unzip them in one folder, then you should get something like this:</p>
<h2>Mac</h2>
<p style="padding-left: 30px;"><em>/Users/cyrille/Documents/_Maya2014Scripts/sip-4.14.5</em><br /><em>/Users/cyrille/Documents/_Maya2014Scripts/PyQt-mac-gpl-4.10</em></p>
<p style="padding-left: 30px;"><em>&#39;/Users/cyrille/Documents/_Maya2014Scripts&#39; being my local folder. Now the instructions, and bash scripts to build that stuff.</em></p>
<p>Follow the instructions from the API docs to setup your environment
(Developer Resources &gt; API Guide &gt; Setting up your build environment &gt;
Mac OS X environment, in the Maya Documentation)&#0160;</p>
<p>Untar&#0160;the /devkit/include/qt-4.8.2-include.tar.gz into /devkit/include/Qt</p>
<p>Copy /Resources/qt.conf into /bin/qt.conf and edit it like this:</p>
<pre class="bush: bash; toolbar: false;">[Paths]
Prefix=
Libraries=../MacOS
Binaries=../bin
Headers=../../../devkit/include/Qt
Data=..
Plugins=../qt-plugins
Translations=../qt-translations
</pre>
<p>Untar the qt-4.8.2-64-mkspecs.tar.gz into $MAYA_LOCATION/Maya.app/Contents/bin/mkspecs Unfortunately, this file is not present in the Maya Mac&#0160;distribution, so you either need to get it from your Window or Linux Maya distribution, or from the Qt Mac source. Make sure the qconfig.pri looks like this:</p>
<p>qconfig.pri</p>
<pre class="brush: bash; toobar: false;">#configuration
CONFIG += release def_files_disabled exceptions no_mocdepend stl x86_64 qt #qt_framework
QT_ARCH = macosx
QT_EDITION = OpenSource
QT_CONFIG +=  minimal-config small-config medium-config large-config full-config no-pkg-config dwarf2 phonon phonon-backend accessibility opengl reduce_exports ipv6 getaddrinfo ipv6ifname getifaddrs png no-freetype system-zlib nis cups iconv openssl corewlan concurrent xmlpatterns multimedia audio-backend svg script scripttools declarative release x86_64 qt #qt_framework

#versioning
QT_VERSION = 4.8.2
QT_MAJOR_VERSION = 4
QT_MINOR_VERSION = 8
QT_PATCH_VERSION = 2

#namespaces
QT_LIBINFIX = 
QT_NAMESPACE = 
QT_NAMESPACE_MAC_CRC = 

QT_GCC_MAJOR_VERSION = 4
QT_GCC_MINOR_VERSION = 2
QT_GCC_PATCH_VERSION = 1
</pre>
<p>You also need to create copy of the Qt lib files as fake .dylib files from the /MacOS directory.&#0160;The
script below will give you the commands to run to do that.</p>
<p>Build &amp; Install SIP</p>
<pre class="brush: shell; toolbar: false;">#!/usr/bin/env bash

MAYAQTBUILD=&quot;`dirname \&quot;$0\&quot;`&quot; # Relative
export MAYAQTBUILD=&quot;`( cd \&quot;$MAYAQTBUILD\&quot; &amp;&amp; pwd )`&quot; # Absolutized and normalized
cd $MAYAQTBUILD

export SIPDIR=$MAYAQTBUILD/sip-4.14.5
export MAYA_LOCATION=/Applications/Autodesk/maya2014

cd $SIPDIR
$MAYA_LOCATION/Maya.app/Contents/bin/mayapy ./configure.py --arch=x86_64
make
sudo make install
</pre>
<p>Build &amp; Install PyQt</p>
<pre class="brush: shell; toolbar: false;">#!/usr/bin/env bash

MAYAQTBUILD=&quot;`dirname \&quot;$0\&quot;`&quot; # Relative
export MAYAQTBUILD=&quot;`( cd \&quot;$MAYAQTBUILD\&quot; &amp;&amp; pwd )`&quot; # Absolutized and normalized
cd $MAYAQTBUILD

export MAYA_LOCATION=/Applications/Autodesk/maya2014
export QTDIR=$MAYA_LOCATION/Maya.app/Contents
export QMAKESPEC=$QTDIR/mkspecs/macx-g++
export INCDIR_QT=$MAYA_LOCATION/devkit/include/Qt
export LIBDIR_QT=$QTDIR/MacOS

if [ ! -f $QMAKESPEC/qmake.conf ];
then
  echo &quot;You need to install qt-4.8.2-64-mkspecs.tar.gz in $QTDIR/mkspecs !&quot;
  exit
fi
if [ ! -f $INCDIR_QT/QtCore/qdir.h ];
then
  echo &quot;You need to uncompress $MAYA_LOCATION/devkit/include/qt-4.8.2-include.tar.gz in $INCDIR_QT !&quot;
  exit
fi
# qt.conf - /Applications/Autodesk/maya2014/Maya.app/Contents/Resources
if [ ! -f $QTDIR/bin/qt.conf ];
then
  echo &quot;You need to copy $QTDIR/Resources/qt.conf in $QTDIR/bin !&quot;
  exit
fi

test=`grep &quot;Data=../..&quot; $QTDIR/bin/qt.conf`
if [ ! -z &quot;$test&quot; ];
then
  echo &quot;You need to edit $QTDIR/bin/qt.conf to use &#39;Data=..&#39;&quot;
  exit
fi
test=`grep &quot;Headers=../../include&quot; $QTDIR/bin/qt.conf`
if [ ! -z &quot;$test&quot; ];
then
&#0160; echo &quot;You need to edit $QTDIR/bin/qt.conf to use &#39;Headers=../../../devkit/include/Qt&#39;&quot;
&#0160; exit
fi
test=`grep &quot;Libraries=../lib&quot; $QTDIR/bin/qt.conf`
if [ ! -z &quot;$test&quot; ];
then
&#0160; echo &quot;You need to edit $QTDIR/bin/qt.conf to use &#39;Libraries =../MacOS&#39;&quot;
&#0160; exit
fi
test=`grep &quot;Plugins = qt-plugins&quot; $QTDIR/bin/qt.conf`
if [ ! -z &quot;$test&quot; ];
then
  echo &quot;You need to edit $QTDIR/bin/qt.conf to use &#39;Plugins=../qt-plugins&#39;&quot;
  exit
fi
test=`grep &quot;Translations = qt-translations&quot; $QTDIR/bin/qt.conf`
if [ ! -z &quot;$test&quot; ];
then
  echo &quot;You need to edit $QTDIR/bin/qt.conf to use &#39;Translations=../qt-translations&#39;&quot;
  exit
fi

for mod in Core Declarative Designer DesignerComponents Gui Help Multimedia Network OpenGL Script ScriptTools Sql Svg WebKit Xml XmlPatterns
do
  if [ ! -f $QTDIR/MacOS/libQt${mod}.dylib ];
  then
    echo &quot;You need to copy a fake Qt$mod dylib - cp $QTDIR/MacOS/Qt$mod $QTDIR/MacOS/libQt${mod}.dylib !&quot;
    #cp $QTDIR/MacOS/Qt$mod $QTDIR/MacOS/libQt${mod}.dylib
    exit
  fi
done
if [ ! -f $QTDIR/MacOS/libphonon.dylib ];
then
  echo &quot;You need to copy a fake phonon dylib - cp $QTDIR/MacOS/phonon $QTDIR/MacOS/libphonon.dylib !&quot;
  #cp $QTDIR/MacOS/phonon $QTDIR/MacOS/libphonon.dylib
  exit
fi

export DYLD_LIBRARY_PATH=$QTDIR/MacOS
export DYLD_FRAMEWORK_PATH=$QTDIR/Frameworks

export SIPDIR=$MAYAQTBUILD/sip-4.14.5
export PYQTDIR=$MAYAQTBUILD/PyQt-mac-gpl-4.10

cd $PYQTDIR
export PATH=$QTDIR/bin:$PATH
$QTDIR/bin/mayapy ./configure.py LIBDIR_QT=$LIBDIR_QT INCDIR_QT=$INCDIR_QT MOC=$QTDIR/bin/moc -w --no-designer-plugin -g
make -j 8
sudo make install
</pre>
<p>You&#39;re done! go to the testing paragraph at the end of the article.</p>
<h2>Linux</h2>
<p style="padding-left: 30px;"><em>/home/cyrille/Documents/_Maya2014Scripts/sip-4.14.5</em><br /><em>/home/cyrille/Documents/_Maya2014Scripts/PyQt-mac-gpl-4.10</em></p>
<p style="padding-left: 30px;"><em>&#39;/home/cyrille/Documents/_Maya2014Scripts&#39; being my local folder. Now the instructions, and bash scripts to build that stuff.</em></p>
<p>Follow
the instructions from the API docs to setup your environment (Developer
Resources &gt; API Guide &gt; Setting up your build environment &gt; Linux
environments (64 bit), in the Maya Documentation).</p>
<p>Edit your qt.conf file (/usr/autodesk/maya2014-x64/bin) like below</p>
<pre>[Paths]<br />Prefix=<br />Libraries=../lib<br />Binaries=../bin<br />Headers=../include/Qt<br />Data=../<br />Plugins=../qt-plugins<br />Translations=../qt-translations</pre>
<p>Untar the /include/qt-4.8.2-include.tar.gz into /include/Qt</p>
<p>Untart the /mkspecs/qt-4.8.2-mkspecs.tar.gz into /mkspecs</p>
<p>Make qmake, moc executables from the Maya bin directory</p>
<pre class="brush: shell; toolbar: false;">sudo chmod aog+x /usr/autodesk/maya2014-x64/bin/moc
sudo chmod aog+x /usr/autodesk/maya2014-x64/bin/qmake
</pre>
<p>Build &amp; Install SIP</p>
<pre class="brush: bash; toolbar: false;">#!/usr/bin/env bash

MAYAQTBUILD=&quot;`dirname \&quot;$0\&quot;`&quot; # Relative
export MAYAQTBUILD=&quot;`( cd \&quot;$MAYAQTBUILD\&quot; &amp;&amp; pwd )`&quot; # Absolutized and normalized
cd $MAYAQTBUILD

export SIPDIR=$MAYAQTBUILD/sip-4.14.5
export MAYA_LOCATION=/usr/autodesk/maya2014-x64

cd $SIPDIR
$MAYA_LOCATION/bin/mayapy ./configure.py
make
sudo make install
</pre>
<p>Build &amp; Install PyQt</p>
<pre class="brush: bash; toolbar: false;">#!/usr/bin/env bash

MAYAQTBUILD=&quot;`dirname \&quot;$0\&quot;`&quot; # Relative
export MAYAQTBUILD=&quot;`( cd \&quot;$MAYAQTBUILD\&quot; &amp;&amp; pwd )`&quot; # Absolutized and normalized
cd $MAYAQTBUILD

export MAYA_LOCATION=/usr/autodesk/maya2014-x64
export QTDIR=$MAYA_LOCATION
export QMAKESPEC=$QTDIR/mkspecs/linux-g++-64
export INCDIR_QT=$MAYA_LOCATION/include/Qt
export LIBDIR_QT=$QTDIR/lib

if [ ! -f $QMAKESPEC/qmake.conf ];
then
  echo &quot;You need to install qt-4.8.2-mkspecs.tar.gz in $QTDIR/mkspecs !&quot;
  exit
fi
if [ ! -f $INCDIR_QT/QtCore/qdir.h ];
then
  echo &quot;You need to uncompress $MAYA_LOCATION/include/qt-4.8.2-include.tar.gz in $INCDIR_QT !&quot;
  exit
fi
# qt.conf - /Applications/Autodesk/maya2014/Maya.app/Contents/Resources
if [ ! -f $QTDIR/bin/qt.conf ];
then
  echo &quot;You need to copy $QTDIR/Resources/qt.conf in $QTDIR/bin !&quot;
  exit
fi

test=`grep &quot;Headers=../include/Qt&quot; $QTDIR/bin/qt.conf`
if [ -z &quot;$test&quot; ];
then
  echo &quot;You need to edit $QTDIR/bin/qt.conf to use &#39;Headers=../include/Qt&#39;&quot;
  exit
fi

export SIPDIR=$MAYAQTBUILD/sip-4.14.5
export PYQTDIR=$MAYAQTBUILD/PyQt-x11-gpl-4.10

cd $PYQTDIR
export PATH=$QTDIR/bin:$PATH
$QTDIR/bin/mayapy ./configure.py LIBDIR_QT=$LIBDIR_QT INCDIR_QT=$INCDIR_QT MOC=$QTDIR/bin/moc -w --no-designer-plugin -g
make -j 8
sudo make install
</pre>
<p>You&#39;re done! go to the testing paragraph at the end of the article.</p>
<h2>Windows</h2>
<p style="padding-left: 30px;"><em>D:\__sdkext\_Maya2014 Scripts\sip-4.14.5</em><br /><em>D:\__sdkext\_Maya2014 Scripts\PyQt-win-gpl-4.10</em></p>
<p style="padding-left: 30px;"><em>&#39;D:\__sdkext\_Maya2014 Scripts&#39; being my local folder. Now the instructions and scripts to build that stuff.</em></p>
<p>Follow
the instructions from the API docs to setup your environment (Developer
Resources &gt; API Guide &gt; Setting up your build environment &gt; Windows
environment (64‐bit), in the Maya Documentation)</p>
<p>Edit your qt.conf file (C:\Program Files\Autodesk\Maya2014\bin) like below</p>
<pre>[Paths]<br />Prefix=<br />Libraries=../lib<br />Binaries=../bin<br />Headers=../include/Qt<br />Data=../<br />Plugins=../qt-plugins<br />Translations=../qt-translations</pre>
<p>Unzip the /include/qt-4.8.2-64-include.tar.gz into /include/Qt</p>
<p>Unzip the /mkspecs/qt-4.8.2-64-mkspecs.tar.gz into /mkspecs</p>
<p>Build &amp; Install SIP</p>
<pre class="brush: shell; toolbar: false;">@echo off

set MAYAQTBUILD=%~dp0
set MAYAQTBUILD=%MAYAQTBUILD:~0,-1%
if exist v:\nul subst v: /d
subst v: &quot;%MAYAQTBUILD%&quot;
v:

set SIPDIR=v:\sip-4.14.5
set MSVC_DIR=C:\Program Files (x86)\Microsoft Visual Studio 10.0
if [%LIBPATH%]==[] call &quot;%MSVC_DIR%\VC\vcvarsall&quot; amd64

set MAYA_LOCATION=C:\Program Files\Autodesk\Maya2014
set INCLUDE=%INCLUDE%;%MAYA_LOCATION%\include\python2.7;%MAYA_LOCATION%\Python\include
set LIB=%LIB%;%MAYA_LOCATION%\lib

cd %SIPDIR%
&quot;%MAYA_LOCATION%\bin\mayapy&quot; configure.py
nmake
nmake install
</pre>
<p>Build &amp; Install PyQt</p>
<pre class="brush: shell; toolbar: false;">@echo off

set MAYAQTBUILD=%~dp0
set MAYAQTBUILD=%MAYAQTBUILD:~0,-1%
if exist v:\nul subst v: /d
subst v: &quot;%MAYAQTBUILD%&quot;
v:

set MAYA_LOCATION=C:\Program Files\Autodesk\Maya2014
if exist m:\nul subst m: /d
subst m: &quot;%MAYA_LOCATION%&quot;
set MAYA_LOCATION=m:

set QTDIR=%MAYA_LOCATION%
set MSVC_VERSION=2010
set QMAKESPEC=%QTDIR%\mkspecs\win32-msvc%MSVC_VERSION%
if not exist &quot;%QMAKESPEC%\qmake.conf&quot; (
	echo &quot;You need to uncompress %MAYA_LOCATION%\mkspecs\qt-4.8.2-64-mkspecs.tar.gz !&quot;
	goto :end
)
if not exist &quot;%MAYA_LOCATION%\include\Qt\QtCore\qdir.h&quot; (
	echo &quot;You need to uncompress %MAYA_LOCATION%\include\qt-4.8.2-64-include.tar.gz in %MAYA_LOCATION%\include\Qt !&quot;
	goto :end
)
findstr /L /C:&quot;Headers=../include/Qt&quot; %MAYA_LOCATION%\bin\qt.conf &gt;nul 2&gt;&amp;1
if ERRORLEVEL 1 (
	echo &quot;You need to edit %MAYA_LOCATION%\bin\qt.conf to use &#39;Headers=../include/Qt&#39;&quot;
	goto :end
)

set SIPDIR=v:\sip-4.14.5
set PYQTDIR=v:\PyQt-win-gpl-4.10

set MSVC_DIR=C:\Program Files (x86)\Microsoft Visual Studio 10.0
if [%LIBPATH%]==[] call &quot;%MSVC_DIR%\VC\vcvarsall&quot; amd64

set INCLUDE=%INCLUDE%;%MAYA_LOCATION%\include\python2.7;%MAYA_LOCATION%\Python\include
set LIB=%LIB%;%MAYA_LOCATION%\lib

cd %PYQTDIR%
set PATH=%QTDIR%\bin;%PATH%
&quot;%MAYA_LOCATION%\bin\mayapy&quot; configure.py LIBDIR_QT=%QTDIR%\lib INCDIR_QT=%QTDIR%\include\Qt MOC=%QTDIR%\bin\moc.exe -w --no-designer-plugin
nmake
nmake install

:end
pause
</pre>
<p>You&#39;re done! go to the testing paragraph at the end of the article.</p>
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
