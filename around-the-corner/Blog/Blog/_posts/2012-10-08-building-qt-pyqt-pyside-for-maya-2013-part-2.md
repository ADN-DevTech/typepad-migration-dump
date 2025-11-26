---
layout: "post"
title: "Building Qt, PyQt, PySide for Maya 2013 - Part 2"
date: "2012-10-08 05:01:00"
author: "Cyrille Fauvel"
categories:
  - "C++"
  - "Cyrille Fauvel"
  - "GCC"
  - "Linux"
  - "Mac"
  - "Maya"
  - "Python"
  - "Qt"
  - "Tools"
  - "UI"
  - "Visual Studio"
  - "Windows"
  - "XCode"
original_url: "https://around-the-corner.typepad.com/adn/2012/10/building-qt-pyqt-pyside-for-maya-2013-part-2.html "
typepad_basename: "building-qt-pyqt-pyside-for-maya-2013-part-2"
typepad_status: "Publish"
---

<p>This is a continuation of the previous <a href="http://around-the-corner.typepad.com/adn/2012/10/building-qt-pyqt-pyside-for-maya-2013.html" target="_self">post</a> and this time we&#0160;will&#0160;focus on building PySide.</p>
<p>PySide [http://www.pyside.org/ or&#0160;http://qt-project.org/wiki/PySide/] is a python binding to the Qt library. Because Maya uses Qt internally, you can use the PySide modules in Maya python scripts to create custom UI. PySide does not have the same licensing as Maya, Qt, or Python. Please consult the PySide website for information about licensing for PySide.</p>
<p>Download Shiboken &amp; PySide:&#0160;<a href="http://qt-project.org/wiki/PySideDownloads" target="_self">http://qt-project.org/wiki/PySideDownloads</a></p>
<p>Download Cmake:&#0160;<a href="http://www.cmake.org/cmake/resources/software.html">http://www.cmake.org/cmake/resources/software.html</a></p>
<p>Maya 2013 uses Qt4.7.1 which is binary compatible with the latest version of PySide-1.1.2 (at time of writing, October 2012).</p>
<h2>Building Qt</h2>
<p>Like for PyQt, you need to build Qt from the Autodesk modified source, so before you start read the previous <a href="http://around-the-corner.typepad.com/adn/2012/10/building-qt-pyqt-pyside-for-maya-2013.html" target="_self">post</a>&#0160;on how to build Qt. There is nothing different here for building PySide.</p>
<h2>Building Shiboken and PySide</h2>
<p>Like SIP for PyQt, PySide needs a supplementary library to interface C++ code to Python, and this is Shiboken. The few first version of PySide was using Boost which is a great library for Python and many other things, but Boost has a large footprint compared to Shiboken, and PySide quickly moved to Shiboken instead of Boost.</p>
<h2><a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017d3c48ddbf970c-pi" style="display: inline;"><img alt="Pyside-rounded-corners" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017d3c48ddbf970c" src="/assets/image_9de4db.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Pyside-rounded-corners" /></a><br />Building Shiboken</h2>
<p>If you go to the PySide page you&#39;ll get instructions on <a href="http://www.pyside.org/docs/pyside-git/howto-build/shiboken.html#" target="_self">how to build Shiboken</a> for all platforms. But I found these instructions&#0160;are either wrong, too complicated, requires unneeded dependencies and all over the place. Because I hate to pollute my developer machine with all unnecessary tools, I reduced the build procedure to the minimum needed. That also make sense because I am not building PySide everyday.</p>
<p>However you will need to download Cmake&#0160;to build PySide and Shiboken. The Cmake utility is available <a href="http://www.cmake.org/cmake/resources/software.html" target="_self">here</a>. Download and unzip Cmake in a folder or install Cmake using the installer as you prefer. You may also want to install few additional libraries on Linux even they aren&#39;t strictly needed, but would avoid true-wrong error messages.</p>
<p style="padding-left: 30px;">yum install libxml2-devel libxslt-devel</p>
<p>For the rest of the post,&#0160;I assume you installed Cmake on the following folders:</p>
<ul>
<li>Windows x64 =&#0160;c:\cmake-2.8.9-win32-x86</li>
<li>OSX x86 64 bit = /Users/myHomeDir/cmake</li>
<li>Linux = ~/cmake</li>
</ul>
<p>To avoid script issues later on OSX, please go in the cmake folder and rename the &quot;CMake 2.8.9.app&quot; into&#0160;&quot;CMake-2.8.9.app&quot;. Apparently scripts do not like spaces even using the escape sequence.</p>
<h3><span style="text-decoration: underline;"><strong>Windows x64</strong></span></h3>
<p>The following instructions only apply to 64‐bit builds of Windows Shiboken. You will need to have Visual Studio 2010 SP1 with x64 tools installed. These instructions may also work with Visual Studio Express 2010 Edition. (qt-project.org instructions <a href="http://qt-project.org/wiki/Building_PySide_on_Windows" target="_self">here</a>)</p>
<ol>
<li>Extract the archive to a folder (e.g. C:\shiboken-1.1.2)</li>
<li>Start up a Visual Studio 2010 x64 Win64 Command Prompt</li>
<li>cd C:\shiboken-1.1.2</li>
<li>Setup the environment<br /><br />set CMAKE=c:\cmake-2.8.9-win32-x86\bin\cmake.exe<br />set QMAKE=c:\qt-adsk-4.7.1\qmake.exe<br />set MAYA_LOCATION=C:\Program Files\Autodesk\Maya2013<br />set PYTHON_EXEC=&quot;%MAYA_LOCATION%\bin\mayapy.exe&quot;<br />set PYTHON_INC=&quot;%MAYA_LOCATION%\include\python2.6&quot;<br />set PYTHON_LIB=&quot;%MAYA_LOCATION%\lib\python26.lib&quot;<br /><br /></li>
<li>Create a directory _build<br /><br />mkdir _build<br />cd \shiboken-1.1.2\_build<br /><br /></li>
<li>Generate makefiles using Cmake<br /><br />%CMAKE% -G &quot;NMake Makefiles&quot; -DQT_QMAKE_EXECUTABLE=%QMAKE% -DBUILD_TESTS=False -DPYTHON_EXECUTABLE=%PYTHON_EXEC% -DPYTHON_INCLUDE_DIR=%PYTHON_INC% -DPYTHON_LIBRARY=%PYTHON_LIB% -DCMAKE_BUILD_TYPE=Release ..<br /><br /></li>
<li>nmake</li>
<li>nmake install/fast<br /><br /><em>this will install files into &#39;C:\Program Files\shiboken&#39; and not in Maya&#39;s Python folder<br />&#0160;</em></li>
<li>copy &quot;C:\Program Files\shiboken\lib\site-packages\shiboken.pyd&quot; &quot;C:\Program Files\Autodesk\Maya2013\Python\Lib\site-packages&quot;</li>
<li>copy &quot;C:\Program Files\shiboken\bin\shiboken-python2.6.dll&quot;&#0160;&quot;C:\Program Files\Autodesk\Maya2013\Python\Lib\site-packages&quot;</li>
</ol>
<p>You&#39;re done.</p>
<h3><strong><span style="text-decoration: underline;">OSX x86 64 bit</span></strong></h3>
<p>The following instructions only apply to Mac 64‐bit builds of PyQt.&#0160;Note that in case you build on Lion (10.7) that you need the MacOS Snow Leopard SDK (10.6) to build Qt and Maya plug-ins.&#0160;(qt-project.org instructions&#0160;<a href="http://qt-project.org/wiki/Building_PySide_on_Linux" target="_self">here</a>)</p>
<ol>
<li>untar shiboken-latest.tar.bz2</li>
<li>cd shiboken-1.1.2</li>
<li>Setup the environment<br /><br />export CMAKE=/Users/<em>myHomeDir</em>/cmake/CMake-2.8.9.app/Contents/bin/cmake<br />export QMAKE=/Users/<em>myHomeDir</em>/qt-4.7.1/bin/qmake<br />export MAYA_LOCATION=/Applications/Autodesk/maya2013<br />export PYTHON_EXEC=$MAYA_LOCATION/Maya.app/Contents/bin/mayapy<br />export PYTHON_SITE=$MAYA_LOCATION/Maya.app/Contents/Frameworks/Python.framework<br />export PYTHON_INC=$PYTHON_SITE/Headers<br />export PYTHON_LIB=/usr/lib/libpython2.6.dylib<br /><br /></li>
<li>Create directories _output and _build<br /><br />mkdir _output<br />mkdir _build<br />cd _build<br /><br /></li>
<li>Generate makefiles using Cmake<br /><br />$CMAKE -DQT_QMAKE_EXECUTABLE=$QMAKE -DBUILD_TESTS=False -DPYTHON_EXECUTABLE=$PYTHON_EXEC -DPYTHON_INCLUDE_DIR=$PYTHON_INC -DPYTHON_LIBRARY=$PYTHON_LIB -DCMAKE_BUILD_TYPE=Release -DENABLE_ICECC=0 -DCMAKE_OSX_SYSROOT=/Developer/SDKs/MacOSX10.6.sdk -DCMAKE_INSTALL_PREFIX=/Users/<em>myHomeDir</em>/shiboken-1.1.2/_output ..<br /><br /></li>
<li>make</li>
<li>make install/fast<br /><br /><em>At this time Shiboken is installed in &#39;/Users/myHomeDir/shiboken-1.1.2/_output&#39; and not in Maya&#39;s Python folder. Because binaries will need special treatments to work in Maya, please refer to the special Mac OSX install procedure at the end of this post after having built PySide below.</em></li>
</ol>
<h3><strong><span style="text-decoration: underline;">Linux x86 64 bit</span></strong></h3>
<p>The following instructions only apply to Linux 64‐bit builds of PySide.&#0160;The first thing described in the Maya 2013 documentation for building on Linux is that you need the GCC 4.1.2 compiler. See&#0160;<a href="http://docs.autodesk.com/MAYAUL/2013/ENU/Maya-API-Documentation/files/Setting_up_your_build_environment_Linux_compiler_requirement.htm" target="_self">here</a>&#0160;- details to rebuild GCC for Maya 2013 are in my previous post.&#0160;(qt-project.org instructions&#0160;<a href="http://qt-project.org/wiki/Building_PySide_on_Linux" target="_self">here</a>)</p>
<ol>
<li>untar shiboken-latest.tar.bz2</li>
<li>cd shiboken-1.1.2</li>
<li>Setup the environment<br /><br />export CMAKE=~/cmake/bin/cmake<br />export QTDIR=/usr/local/Trolltech/Qt-4.7.1<br />export QMAKE=$QTDIR/bin/qmake<br />export MAYA_LOCATION=/usr/autodesk/maya2013-x64<br />export PYTHON_EXEC=$MAYA_LOCATION/bin/mayapy<br />export PYTHON_INC=$MAYA_LOCATION/include/python2.6<br />export PYTHON_LIB=$MAYA_LOCATION/lib/libpython2.6.so<br />export CMAKE_C_COMPILER=/opt/gcc412/bin/gcc412<br />export CMAKE_CXX_COMPILER=/opt/gcc412/bin/g++412<br /><br /></li>
<li>Create directories _output and _build<br /><br />mkdir _output<br />mkdir _build<br />cd _build<br /><br /></li>
<li>Generate makefiles using Cmake<br /><br />$CMAKE -DQT_QMAKE_EXECUTABLE=$QMAKE -DBUILD_TESTS=False -DPYTHON_EXECUTABLE=$PYTHON_EXEC -DPYTHON_INCLUDE_DIR=$PYTHON_INC -DPYTHON_LIBRARY=$PYTHON_LIB -DCMAKE_BUILD_TYPE=Release -DENABLE_ICECC=0 -DCMAKE_INSTALL_PREFIX=~/shiboken-1.1.2/_output -DCMAKE_C_COMPILER=$CMAKE_C_COMPILER -DCMAKE_CXX_COMPILER=$CMAKE_CXX_COMPILER&#0160;..<br /><br /></li>
<li>make</li>
<li>make install/fast<br /><br /><em>At this time Shiboken is installed in &#39;~/shiboken-1.1.2/_output&#39; and not in Maya&#39;s Python folder.<br /><br /></em></li>
<li>cd $MAYA_LOCATION/lib/python2.6/site-packages<br />cp ~/shiboken-1.1.2/_output/lib/libshiboken-python2.6.so.1.1.2 .<br />ln -s&#0160;libshiboken-python2.6.so.1.1.2&#0160;libshiboken-python2.6.so.1.1<br />ln -s&#0160;libshiboken-python2.6.so.1.1&#0160;libshiboken-python2.6.so<br />cp&#0160;~/shiboken-1.1.2/_output/lib/python2.6/site-packages/shiboken.so .</li>
<li>Create a file named shiboken.conf in /etc/ld.so.conf.d and add the following line<br /><br />/usr/autodesk/maya2013-x64/lib/python2.6/site-packages<br /><br /></li>
<li>sudo ldconfig</li>
</ol>
<h2>Building PySide</h2>
<p>Like for Shiboken, instructions on the PySide page are over complicated to my taste. While it is fine to use with minor adjustments, find below some instructions which I used to build PySide without downloading tools I do not need in my everyday work.</p>
<h3><strong><span style="text-decoration: underline;">Windows x64</span></strong></h3>
<p>The following instructions only apply to 64‐bit builds of Windows PySide. You will need to have Visual Studio 2010 SP1 with x64 tools installed. These instructions may also work with Visual Studio Express 2010 Edition.&#0160;(qt-project.org instructions&#0160;<a href="http://qt-project.org/wiki/Building_PySide_on_Windows" target="_self">here</a>)</p>
<p>One last minute update - the Maya 2013 Extension includes qmake.exe in its bin folder (I.e.: &#39;C:\Program Files\Autodesk\Maya2013.5\bin&#39;), so delete or rename it while you build PySide or you&#39;ll get strange errors later.</p>
<ol>
<li>Extract the archive to a folder (e.g. C:\pyside-qt4.8+1.1.2)</li>
<li>Start up a Visual Studio 2010 x64 Win64 Command Prompt</li>
<li>cd C:\pyside-qt4.8+1.1.2</li>
<li>Setup the environment<br /><br />set CMAKE=c:\cmake-2.8.9-win32-x86\bin\cmake.exe<br />set QMAKE=c:\qt-adsk-4.7.1\qmake.exe<br />set SHIBOKEN=&quot;C:\Program Files\shiboken&quot;<br />subst s: /d<br />subst s: %SHIBOKEN%<br />set SHIBOKEN_DIR=s:\lib\cmake\Shiboken-1.1.2<br /><br /></li>
<li>Create a directory _build<br /><br />mkdir _build<br />cd \pyside-qt4.8+1.1.2\_build<br /><br /></li>
<li>Generate makefiles using Cmake<br /><br />%CMAKE% -G &quot;NMake Makefiles&quot; -DQT_QMAKE_EXECUTABLE=%QMAKE% -DBUILD_TESTS=False -DCMAKE_BUILD_TYPE=Release -DShiboken_DIR=%SHIBOKEN_DIR%&#0160;..<br /><br /></li>
<li>nmake</li>
<li>nmake install<br /><br /><em>this will install files into &#39;C:\Program Files\pysidebindings&#39; and not in Maya&#39;s Python folder<br />&#0160;</em></li>
<li>Create a folder PySide in Maya&#39;s Python folder<br /><br />mkdir &quot;C:\Program Files\Autodesk\Maya2013\Python\Lib\site-packages\PySide&quot;<br /><br /></li>
<li>copy &quot;C:\Program Files\shiboken\bin\shiboken-python2.6.dll&quot;&#0160;&quot;C:\Program Files\Autodesk\Maya2013\Python\Lib\site-packages\PySide&quot;</li>
<li>copy &quot;C:\Program Files\pysidebindings\bin\pyside-python2.6.dll&quot;&#0160;&quot;C:\Program Files\Autodesk\Maya2013\Python\Lib\site-packages\PySide&quot;</li>
<li>copy &quot;C:\Program Files\pysidebindings\lib\site-packages\PySide\*.*&quot;&#0160;&quot;C:\Program Files\Autodesk\Maya2013\Python\Lib\site-packages\PySide&quot;</li>
</ol>
<p>You&#39;re done.</p>
<h3><strong><span style="text-decoration: underline;">OSX x86 64 bit</span></strong></h3>
<p>The following instructions only apply to Mac 64‐bit builds of PyQt.&#0160;Note that in case you build on Lion (10.7) that you need the MacOS Snow Leopard SDK (10.6) to build Qt and Maya plug-ins.&#0160;(qt-project.org instructions&#0160;<a href="http://qt-project.org/wiki/Building_PySide_on_Linux" target="_self">here</a>)</p>
<ol>
<li>untar pyside-latest.tar.bz2</li>
<li>cd pyside-qt4.8+1.1.2</li>
<li>Setup the environment<br /><br />export CMAKE=/Users/<em>myHomeDir</em>/cmake/CMake-2.8.9.app/Contents/bin/cmake<br />export QTDIR=/Users/<em>myHomeDir</em>/qt-4.7.1<br />export QMAKE=$QTDIR/bin/qmake<br />export SHIBOKEN_DIR=/Users/<em>myHomeDir</em>/shiboken-1.1.2/_output<br /><br /></li>
<li>Create directories _build and _output<br /><br />mkdir _output<br />mkdir _build<br />cd _build<br /><br /></li>
<li>Generate makefiles using Cmake<br /><br />$CMAKE -DQT_QMAKE_EXECUTABLE=$QMAKE -DBUILD_TESTS=False -DCMAKE_BUILD_TYPE=Release -DCMAKE_OSX_SYSROOT=/Developer/SDKs/MacOSX10.6.sdk&#0160;-DShiboken_DIR=$SHIBOKEN_DIR -DCMAKE_INSTALL_PREFIX=/Users/<em>myHomeDir</em>/pyside-qt4.8+1.1.2/_output -DALTERNATE_QT_INCLUDE_DIR=$QTDIR/include&#0160;..<br /><br /></li>
<li>make</li>
<li>make install<br /><br /><em>At this time PySide is installed in &#39;/Users/myHomeDir/pyside-qt4.8+1.1.2/_output&#39; and not in Maya&#39;s Python folder. Because binaries will need special treatments to work in Maya, please refer to the special Mac OSX install procedure at the end of this post below.</em></li>
</ol>
<h3><span style="text-decoration: underline;"><strong>Linux x86 64 bit</strong></span></h3>
<p>The following instructions only apply to Linux 64‐bit builds of PySide.&#0160;The first thing described in the Maya 2013 documentation for building on Linux is that you need the GCC 4.1.2 compiler. See&#0160;<a href="http://docs.autodesk.com/MAYAUL/2013/ENU/Maya-API-Documentation/files/Setting_up_your_build_environment_Linux_compiler_requirement.htm" target="_self">here</a>&#0160;- details to rebuild GCC for Maya 2013 are in my previous post.&#0160;(qt-project.org instructions&#0160;<a href="http://qt-project.org/wiki/Building_PySide_on_Linux" target="_self">here</a>)</p>
<ol>
<li>untar pyside-latest.tar.bz2</li>
<li>cd pyside-qt4.8+1.1.2</li>
<li>Setup the environment<br /><br />export CMAKE=~/cmake/bin/cmake<br />export QMAKE=/usr/local/Trolltech/Qt-4.7.1/bin/qmake<br />export MAYA_LOCATION=/usr/autodesk/maya2013-x64<br />export SHIBOKEN_DIR=~/shiboken-1.1.2/_output/lib/cmake/Shiboken-1.1.2<br />export CMAKE_C_COMPILER=/opt/gcc412/bin/gcc412<br />export CMAKE_CXX_COMPILER=/opt/gcc412/bin/g++412<br />export LD_LIBRARY_PATH=$MAYA_LOCATION/lib</li>
<li>Create directories _build and _output<br /><br />mkdir _output<br />mkdir _build<br />cd _build<br /><br /></li>
<li>Generate makefiles using Cmake<br /><br />$CMAKE -DQT_QMAKE_EXECUTABLE=$QMAKE -DBUILD_TESTS=False -DCMAKE_BUILD_TYPE=Release -DShiboken_DIR=$SHIBOKEN_DIR -DCMAKE_C_COMPILER=$CMAKE_C_COMPILER -DCMAKE_CXX_COMPILER=$CMAKE_CXX_COMPILER -DCMAKE_INSTALL_PREFIX=~/pyside-qt4.8+1.1.2/_output ..<br /><br /></li>
<li>make</li>
<li>make install<br /><br /><em>At this time PySide is installed in &#39;~/pyside-qt4.8+1.1.2/_output&#39; and not in Maya&#39;s Python folder.<br /><br /></em></li>
<li>cd $MAYA_LOCATION/lib/python2.6/site-packages<br />mkdir PySide<br />cd PySide<br />cp ~/pyside-qt4.8+1.1.2/_output/lib/libpyside-python2.6.so.1.1.2 .<br />ln -s&#0160;libpyside-python2.6.so.1.1.2&#0160;libpyside-python2.6.so.1.1<br />ln -s&#0160;libpyside-python2.6.so.1.1&#0160;libpyside-python2.6.so<br />cp&#0160;~/pyside-qt4.8+1.1.2/_output/lib/python2.6/site-packages/PySide/* .</li>
<li>Create a file named pyside.conf in /etc/ld.so.conf.d and add the following line<br /><br />/usr/autodesk/maya2013-x64/lib/python2.6/site-packages/PySide<br /><br /></li>
<li>sudo ldconfig</li>
</ol>
<h2>Special instructions to finalize install on Mac OSX</h2>
<p>Ok, we now assume both Shiboken and PySide successfully built into&#0160;<em>&#39;/Users/myHomeDir/shiboken-1.1.2/_output&#39; </em>and&#0160;<em><em>&#39;/Users/myHomeDir/pyside-qt4.8+1.1.2/_output&#39;</em></em>.</p>
<p>Rather than a long description, I copied below my install script, you only need to adjust one (or two) path accordingly to your machine set-up.</p>
<ol>
<li>Copy&amp;Paste the script below into a script file (for example&#0160;my-pyside-install-script)</li>
<li>Edit the paths marked in code below to fit your machine set-up</li>
<li>sudo ./my-pyside-install-script</li>
</ol>
<h4><span style="text-decoration: underline;"><strong>Code to copy and paste into a bash script</strong></span></h4>
<pre class="brush: cpp; highlight: [3, 4, 9, 10]; toolbar: false;">#!/usr/bin/env bash

export MYHOME=/Users/myHomeDir
export DOCS=$MYHOME
export MAYA_LOCATION=/Applications/Autodesk/maya2013
export PYTHON_SITE=$MAYA_LOCATION/Maya.app/Contents/Frameworks/Python.framework/Versions/Current/lib/python2.6/site-packages

# Config shiboken / PySide
export SHIBOKEN=$DOCS/shiboken-1.1.2/_output/lib
export PYSIDE=$DOCS/pyside-qt4.8+1.1.2/_output/lib

cd $PYTHON_SITE
cp $SHIBOKEN/libshiboken-python2.6.1.1.2.dylib .
ln -s libshiboken-python2.6.1.1.2.dylib libshiboken-python2.6.1.1.dylib
ln -s libshiboken-python2.6.1.1.dylib libshiboken-python2.6.dylib
cp $SHIBOKEN/python2.6/site-packages/shiboken.so .

mkdir PySide
cd PySide

cp $SHIBOKEN/libshiboken-python2.6.1.1.2.dylib .
ln -s libshiboken-python2.6.1.1.2.dylib libshiboken-python2.6.1.1.dylib
ln -s libshiboken-python2.6.1.1.dylib libshiboken-python2.6.dylib

cp $PYSIDE/libpyside-python2.6.1.1.2.dylib .
ln -s libpyside-python2.6.1.1.2.dylib libpyside-python2.6.1.1.dylib
ln -s libpyside-python2.6.1.1.dylib libpyside-python2.6.dylib

cp $PYSIDE/python2.6/site-packages/PySide/* .

# The following 4 lines are one single line each to execute properly
for mod in QtCore QtDeclarative QtDesigner QtDesignerComponents QtGui QtHelp QtMultimedia QtNetwork QtOpenGL QtScript QtScriptTools QtSql QtSvg QtWebKit QtXml QtXmlPatterns phonon; do find /Applications/Autodesk/maya2013/Maya.app/Contents/Frameworks/Python.framework/Versions/Current/lib/python2.6/site-packages/PySide -name &quot;*.so&quot; -exec install_name_tool -change ${mod}.framework/Versions/4/${mod} @executable_path/${mod} {} \;;done;

for mod in QtCore QtDeclarative QtDesigner QtDesignerComponents QtGui QtHelp QtMultimedia QtNetwork QtOpenGL QtScript QtScriptTools QtSql QtSvg QtWebKit QtXml QtXmlPatterns phonon; do find /Applications/Autodesk/maya2013/Maya.app/Contents/Frameworks/Python.framework/Versions/Current/lib/python2.6/site-packages/PySide -name &quot;*.dylib&quot; -exec install_name_tool -change ${mod}.framework/Versions/4/${mod} @executable_path/${mod} {} \;;done;

for mod in libpyside-python2.6.1.1.dylib libshiboken-python2.6.1.1.dylib ; do find /Applications/Autodesk/maya2013/Maya.app/Contents/Frameworks/Python.framework/Versions/Current/lib/python2.6/site-packages/PySide -name &quot;*.so&quot; -exec install_name_tool -change ${mod} @executable_path/../Frameworks/Python.framework/Versions/Current/lib/python2.6/site-packages/PySide/${mod} {} \;;done;

for mod in libpyside-python2.6.1.1.dylib libshiboken-python2.6.1.1.dylib ; do find /Applications/Autodesk/maya2013/Maya.app/Contents/Frameworks/Python.framework/Versions/Current/lib/python2.6/site-packages/PySide -name &quot;*.dylib&quot; -exec install_name_tool -change ${mod} @executable_path/../Frameworks/Python.framework/Versions/Current/lib/python2.6/site-packages/PySide/${mod} {} \;;done;
</pre>
<h2>Test PySide in Maya</h2>
<p>Simply copy and paste this code into a Maya Python tab in the script editor and run it. If it works, you are all set :)</p>
<pre class="brush: python; toolbar: false;">import sys
from PySide import QtCore, QtGui

class FormExample(QtGui.QWidget):
    def __init__(self):
        super(FormExample, self).__init__()
        self.initUI()
        
    def initUI(self):
        QtGui.QToolTip.setFont(QtGui.QFont(&#39;SansSerif&#39;, 10))
        self.setToolTip(&#39;This is my &lt;b&gt;QWidget&lt;/b&gt; widget tooltip&#39;)
        
        btn = QtGui.QPushButton(&#39;My Button&#39;, self)
        btn.setToolTip(&#39;This is my &lt;b&gt;QPushButton&lt;/b&gt;&#39;)
        btn.resize(btn.sizeHint())
        btn.move(50, 50)       
        
        self.setGeometry(300, 300, 250, 150)
        self.setWindowTitle(&#39;My Form Example&#39;)    
        self.show()
        
ex = FormExample()
</pre>
<p>ok we&#39;re done now :)</p>
<p><a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017ee407a0b6970d-pi" style="display: inline;"><img alt="Tired" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017ee407a0b6970d" src="/assets/image_027a83.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Tired" /></a><br /><br /></p>
