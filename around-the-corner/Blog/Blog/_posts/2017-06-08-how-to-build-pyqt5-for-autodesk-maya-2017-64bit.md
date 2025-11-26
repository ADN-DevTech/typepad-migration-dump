---
layout: "post"
title: "How to build PyQt5 for Autodesk Maya 2017 64bit"
date: "2017-06-08 23:45:15"
author: "Cheng Xi Li"
categories:
  - "Cheng Xi Li"
  - "Maya"
  - "Qt"
original_url: "https://around-the-corner.typepad.com/adn/2017/06/how-to-build-pyqt5-for-autodesk-maya-2017-64bit.html "
typepad_basename: "how-to-build-pyqt5-for-autodesk-maya-2017-64bit"
typepad_status: "Publish"
---

Written by Cyrille Fauvel – Autodesk Developer Network (April 2013) <br/>

Updated by Vijaya Prakash – Autodesk Developer Network (November 2016)<br/>

                     Chengxi Li - Autodesk Developer Network (June 2017) <br/>

<br/>

<br/>

Building SIP, and PyQt for Maya 2017 PyQt [https://www.riverbankcomputing.co.uk] is a python binding to the Qt library. Because Maya uses Qt internally, you can use the PyQt modules in Maya python scripts to create custom UI. PyQt does not have the same licensing as Maya, Qt, or Python. Please consult the PyQt website for information about licensing for PyQt.<br/>

<br/>

Download PyQt: <a href="https://www.riverbankcomputing.com/software/pyqt/download">https://www.riverbankcomputing.com/software/pyqt/download</a><br/>

<br/>

Download SIP: <a href="https://www.riverbankcomputing.com/software/sip/download">https://www.riverbankcomputing.com/software/sip/download</a><br/>

<br/>

The following are instructions for building a copy of the PyQt modules that have been known to work with Maya.<br/>

<br/>

Maya 2017 uses Qt5.6.1 which is binary compatible with the latest version of PyQt – 5.7 / SIP - 4.18.1<br/>

<br/>

Note that it’s important to use the Maya modified version of the Qt source code. A copy of the customized Qt 5.6.1 source is available from Autodesk's Open Source web-site (https://www.autodesk.com/lgplsource) and includes text files describing how to configure, build and install Qt for each platform supported by Maya.<br/>

<br/>

However, be aware that with Maya 2017, there is no more need to build PySide as it is coming by default in Maya, nor you have to rebuild Qt itself as the main Qt tools to build PyQt are now included in the Maya distributions (I.e. qmake, moc, …)<br/>

libxml, openSSL, OpenAL, python2.7, qt-5.6.1, and tbb are also coming by default in the Maya include and lib folders, so unless you have a very specific need, you would not need to rebuild any of those libraries like before. Note as well that there is a 'MAYA_LOCATION/support/opensource' folder now which contains some of the community source.<br/>

<br/>

Important: Maya 2017 now ships without the devkit, include and mkspecs folders. You can get the Maya 2017 devkit from the Autodesk App Store <a href="https://apps.autodesk.com/MAYA/en/Detail/Index?id=8656206734503135164&amp;appLang=en&amp;os=Win64">here </a>for Windows, OSX, and Linux. Download the devkit and unzip the files into your Maya root folder. Make sure to read the instructions to install the devkit, include and mkspecs folders properly on your system.<br/>

<br/>

The scripts used in this document are now also posted on <a href="https://github.com/cyrillef/Maya-PyQt-Scripts">Github</a>.<br/>

<br/>

Download SIP and PyQt source from '<a href="https://www.riverbankcomputing.co.uk">https://www.riverbankcomputing.co.uk</a>' - here I downloaded 'sip- 4.18.1' and 'PyQt5_gpl-5.7'. Unzip them in one folder, then you should get something like this:<br/>

<div data-section-style='11' style='max-width:100%'><img src='/assets/image_a2e93e.jpg' id='WOCACA5TCqF' alt='' width='800' height='118'></img></div><br/>

<h2 id='WOCACAXDivU'>Mac</h2>

<br/>

/Users/cyrille/Documents/_Maya2017Scripts/sip-4.18.1 <br/>

/Users/cyrille/Documents/_Maya2017Scripts/PyQt5_gpl-5.7 '<br/>

/Users/cyrille/Documents/_Maya2017Scripts' being my local folder. <br/>

<br/>

Here are the instructions and scripts for building SIP and PyQt.<br/>

<br/>

Follow the instructions from the API docs to setup your environment (Developer Resources &gt; API Guide &gt; Setting up your build environment &gt; Mac OS X environment, in the Maya Documentation) <br/>

<br/>

If you would like to use Xcode 6.1.1 to compile it and you are having multiple installation of Xcode. Please backup /Applications/Xcode.app and use Xcode 6.1.1 to replace it. You can restore it after installation.<br/>

Note that Xcode 6.1.1 won't open on Mac OS X Sierra(10.12), but it is still able to build PyQt5.<br/>

<br/>

Use xcode-select to change active xcode like below:<br/>

<pre id='WOCACAP81R8'>sudo xcode-select -switch /Applications/Xcode.app/Contents/Developer</pre>

<br/>

The qt.conf file uses <b>MAYA_LOCATION</b> and <b>DEVKIT_LOCATION</b> to locate the expected header/library files. Therefore, users must set both environment variables before building the PyQt5.<br/>

<br/>

DEVKIT_LOCATION should point to the directory where the devkit include, mkspecs, cmake directories are located.<br/>

<br/>

Modify devkit/bin/qt.conf as below: <br/>

<pre id='WOCACA36HK1'>[Paths]<br>Prefix=<br>Libraries=$(MAYA_LOCATION)/MacOS<br>Binaries=$(DEVKIT_LOCATION)/devkit/bin<br>Headers=$(DEVKIT_LOCATION)/include/Qt<br>ArchData=$(DEVKIT_LOCATION)<br>Data=$(DEVKIT_LOCATION)<br>HostData=$(DEVKIT_LOCATION)<br>HostBinaries=$(DEVKIT_LOCATION)/devkit/bin<br>HostLibraries=$(MAYA_LOCATION)/MacOS</pre>

Untar the include/qt-5.6.1-include.tar.gz into /include/Qt<br/>

<br/>

Untar the qt-5.6.1-mkspecs.tar.gz into /Applications/Autodesk/maya2017/mkspecs. Make sure the qconfig.pri looks like this:<br/>

<br/>

<b>qconfig.pri</b><br/>

<pre id='WOCACAqGrlR'>#configuration<br> CONFIG += release def_files_disabled exceptions no_mocdepend stl x86_64 qt #qt_framework <br>QT_ARCH = macosx <br>QT_EDITION = OpenSource <br>QT_CONFIG += minimal-config small-config medium-config large-config full-config no-pkg-config dwarf2 phonon phonon-backend accessibility opengl reduce_exports ipv6 getaddrinfo ipv6ifname getifaddrs png no-freetype system-zlib nis cups iconv openssl corewlan concurrent xmlpatterns multimedia audio-backend svg script scripttools declarative release x86_64 qt #qt_framework<br>#versioning <br>QT_VERSION = 5.6.1 <br>QT_MAJOR_VERSION = 5 <br>QT_MINOR_VERSION = 6 <br>QT_PATCH_VERSION = 1<br><br>#namespaces <br>QT_LIBINFIX =<br>QT_NAMESPACE = <br>QT_NAMESPACE_MAC_CRC =</pre>

<h3 id='WOCACAqLy00'>Build &amp; Install SIP</h3>

Please use the script below, you can also find it in GitHub.<br/>

<pre id='WOCACAnUhTO'>#!/usr/bin/env bash<br> <br>MAYAQTBUILD="`dirname \"$0\"`" # Relative<br>export MAYAQTBUILD="`( cd \"$MAYAQTBUILD\" &amp;&amp; pwd )`" # Absolutized and normalized<br>pushd $MAYAQTBUILD<br> <br>export SIPDIR=$MAYAQTBUILD/sip-4.18.1<br>export MAYA_LOCATION=/Applications/Autodesk/maya2017<br> <br>pushd $SIPDIR<br>$MAYA_LOCATION/Maya.app/Contents/bin/mayapy ./configure.py --arch=x86_64<br>make<br>sudo make install<br>popd<br><br>popd</pre>

<h3 id='WOCACAbdMfY'>Build &amp; Install PyQt</h3>

Please use the script below, you can also find it in GitHub.<br/>

<pre id='WOCACAsva27'>#!/usr/bin/env bash<br> <br>MAYAQTBUILD="`dirname \"$0\"`" # Relative<br>export MAYAQTBUILD="`( cd \"$MAYAQTBUILD\" &amp;&amp; pwd )`" # Absolutized and normalized<br>pushd $MAYAQTBUILD<br> <br>export MAYA_LOCATION=/Applications/Autodesk/maya2017/Maya.app/Contents<br>export DEVKIT_LOCATION=/Applications/Autodesk/maya2017<br>export QTDIR=$DEVKIT_LOCATION/devkit<br>export QMAKESPEC=$DEVKIT_LOCATION/mkspecs/macx-clang<br>export INCDIR_QT=$DEVKIT_LOCATION/include/Qt<br>export LIBDIR_QT=$MAYA_LOCATION/MacOS<br><br>error=0<br>if [ ! -f $QMAKESPEC/qmake.conf ];<br>then<br>  echo "You need to install qt-5.6.1-mkspecs.tar.gz in $QTDIR/mkspecs !"<br>  error=1<br>fi<br>if [ ! -f $INCDIR_QT/QtCore/qdir.h ];<br>then<br>  echo "You need to uncompress $MAYA_LOCATION/devkit/include/qt-5.6.1-include.tar.gz in $INCDIR_QT !"<br>  error=1<br>fi<br># qt.conf - /Applications/Autodesk/maya2017/Maya.app/Contents/Resources<br>if [ ! -f $QTDIR/bin/qt.conf ];<br>then<br>  echo "You need to copy $QTDIR/Resources/qt.conf in $QTDIR/bin !"<br>  error=1<br>fi<br> <br>test=`grep 'Data=$(DEVKIT_LOCATION)' $QTDIR/bin/qt.conf`<br>if [ -z "$test" ];<br>then<br>  echo "You need to edit $QTDIR/bin/qt.conf to use 'Data=\$(DEVKIT_LOCATION)'"<br>  error=1<br>fi<br>test=`grep 'Headers=$(DEVKIT_LOCATION)/include/Qt' $QTDIR/bin/qt.conf`<br>if [ -z "$test" ];<br>then<br>  echo "You need to edit $QTDIR/bin/qt.conf to use 'Headers=\$(DEVKIT_LOCATION)/include/Qt'"<br>  error=1<br>fi<br>test=`grep 'Libraries=$(MAYA_LOCATION)/MacOS' $QTDIR/bin/qt.conf`<br>if [ -z "$test" ];<br>then<br>  echo "You need to edit $QTDIR/bin/qt.conf to use 'Libraries=\$(MAYA_LOCATION)/MacOS'"<br>  error=1<br>fi<br><br>if [ $error -eq 1 ];<br>then<br>    exit<br>fi<br> <br>export DYLD_LIBRARY_PATH=$MAYA_LOCATION/MacOS<br>export DYLD_FRAMEWORK_PATH=$MAYA_LOCATION/Frameworks<br> <br>export SIPDIR=$MAYAQTBUILD/sip-4.18.1<br>export PYQTDIR=$MAYAQTBUILD/PyQt5_gpl-5.7<br> <br>export SIP_EXE=$MAYA_LOCATION/Frameworks/Python.framework/Versions/2.7/bin/sip<br>export SIP_INCLUDE=$MAYA_LOCATION/Frameworks/Python.framework/Versions/2.7/include/python2.7<br><br>pushd $PYQTDIR<br>export PATH=$QTDIR/bin:$PATH<br><br>echo <br>echo Environment<br>echo -----------<br>set<br>echo -----------<br>echo QT Settings<br>echo -----------<br>qmake -query<br>echo -----------<br>echo<br>$MAYA_LOCATION/bin/mayapy ./configure.py QMAKE_MAC_SDK=macosx10.9 QMAKE_RPATHDIR+=$LIBDIR_QT --sip=$SIP_EXE --sip-incdir=$SIP_INCLUDE -w --no-designer-plugin <br>make -j 8<br>sudo make install<br>popd<br><br>popd</pre>

Note that I am compiling against Mac OS X SDK 10.9 which is same as the developer environment. If you want to compile against other versions, please modify the script(macosx10.9).<br/>

<br/>

You're done! Please check the testing paragraph at the end of the article.<br/>

<h2 id='WOCACASsndo'>Linux</h2>

<br/>

/home/cyrille/Documents/_Maya2017Scripts/sip-4.18.1 <br/>

/home/cyrille/Documents/_Maya2017Scripts/PyQt5_gpl-5.7<br/>

'/home/cyrille/Documents/_Maya2017Scripts' being my local folder. <br/>

<br/>

Here are the instructions and scripts for building SIP and PyQt.<br/>

<br/>

Follow the instructions from the API docs to setup your environment (Developer Resources &gt; API Guide &gt; Setting up your build environment &gt; Linux environments (64 bit), in the Maya Documentation).<br/>

<br/>

The qt.conf file uses <b>MAYA_LOCATION</b> and <b>DEVKIT_LOCATION</b> to locate the expected header/library files. Therefore, users must set both environment variables before building the PyQt5.<br/>

<br/>

DEVKIT_LOCATION should point to the directory where the devkit include, mkspecs, cmake directories are located.<br/>

<br/>

Please backup your qt.conf first, you'll need to restore it after building PyQt5. Replace …/bin/qt.conf with below:<br/>

<pre id='WOCACAJmOWM'>[Paths] <br>Prefix= <br>Libraries=$(MAYA_LOCATION)/lib <br>Binaries=$(DEVKIT_LOCATION)/bin<br>Headers=$(DEVKIT_LOCATION)/include/Qt <br>ArchData=$(DEVKIT_LOCATION) <br>Data=$(DEVKIT_LOCATION) <br>HostData=$(DEVKIT_LOCATION) <br>HostBinaries=$(DEVKIT_LOCATION)/bin</pre>

Untar the /include/qt-5.6.1-include.tar.gz into /include/Qt<br/>

<br/>

 Untar the /mkspecs/qt-5.6.1-mkspecs.tar.gz into /mkspecs<br/>

<br/>

Make qmake, moc executables from the Maya bin directory <br/>

<pre id='WOCACABD19M'>sudo chmod aog+x /usr/autodesk/maya2017/bin/moc <br>sudo chmod aog+x /usr/autodesk/maya2017/bin/qmake</pre>

<h3 id='WOCACAXQGER'>Build &amp; Install SIP </h3>

Please use the script below, you can also find it in GitHub.<br/>

<pre id='WOCACALSWdl'>#!/usr/bin/env bash<br> <br>MAYAQTBUILD="`dirname \"$0\"`" # Relative<br>export MAYAQTBUILD="`( cd \"$MAYAQTBUILD\" &amp;&amp; pwd )`" # Absolutized and normalized<br>pushd $MAYAQTBUILD<br> <br>export SIPDIR=$MAYAQTBUILD/sip-4.18.1<br>export MAYA_LOCATION=/usr/autodesk/maya2017<br> <br>pushd $SIPDIR<br>$MAYA_LOCATION/bin/mayapy ./configure.py<br>make<br>sudo make install<br>popd<br><br>popd</pre>

<h3 id='WOCACA8oCVq'>Build &amp; Install PyQt</h3>

Please use the script below, you can also find it in GitHub.<br/>

<pre id='WOCACAY3NNd'>#!/usr/bin/env bash<br> <br>MAYAQTBUILD="`dirname \"$0\"`" # Relative<br>export MAYAQTBUILD="`( cd \"$MAYAQTBUILD\" &amp;&amp; pwd )`" # Absolutized and normalized<br>pushd $MAYAQTBUILD<br> <br>export MAYA_LOCATION=/usr/autodesk/maya2017<br>export QTDIR=$MAYA_LOCATION<br>export DEVKIT_LOCATION=$MAYA_LOCATION<br>export QMAKESPEC=$QTDIR/mkspecs/linux-g++-64<br>export INCDIR_QT=$MAYA_LOCATION/include/Qt<br>export LIBDIR_QT=$QTDIR/lib<br><br>error=0<br>if [ ! -f $QMAKESPEC/qmake.conf ];<br>then<br>  echo "You need to install qt-5.6.1-mkspecs.tar.gz in $QTDIR/mkspecs !"<br>  error=1<br>fi<br>if [ ! -f $INCDIR_QT/QtCore/qdir.h ];<br>then<br>  echo "You need to uncompress $MAYA_LOCATION/include/qt-5.6.1-include.tar.gz in $INCDIR_QT !"<br>  error=1<br>fi<br># qt.conf - $QTDIR/bin/qt.conf<br>if [ ! -f $QTDIR/bin/qt.conf ];<br>then<br>  echo "You need to copy $QTDIR/Resources/qt.conf in $QTDIR/bin !"<br>  error=1<br>fi<br><br># The grep string should be in single quote('), if it is in double quote (""), <br># shell will expand the variable, hence the intension of the below grep will fail <br>test=`grep 'Headers=$(DEVKIT_LOCATION)/include/Qt' $QTDIR/bin/qt.conf`<br>if [ -z "$test" ];<br>then<br>  echo "You need to edit $QTDIR/bin/qt.conf to use 'Headers=$(DEVKIT_LOCATION)/include/Qt'"<br>  error=1<br>fi<br><br>if [ $error -eq 1 ];<br>then<br>    exit<br>fi<br> <br>export SIPDIR=$MAYAQTBUILD/sip-4.18.1<br>export PYQTDIR=$MAYAQTBUILD/PyQt5_gpl-5.7<br> <br>pushd $PYQTDIR<br>export PATH=$QTDIR/bin:$PATH<br>$QTDIR/bin/mayapy ./configure.py LIBDIR_QT=$LIBDIR_QT INCDIR_QT=$INCDIR_QT MOC=$QTDIR/bin/moc -w --no-designer-plugin <br>make -j 8<br>sudo make install<br>popd<br><br>popd</pre>

You're done! Please check the testing paragraph at the end of the article.<br/>

<br/>

<h1 id='WOCACARaAdB'>Windows</h1>

<br/>

D:\__sdkext\_Maya2017 Scripts\sip-4.18.1 <br/>

D:\__sdkext\_Maya2017 Scripts\PyQt5_gpl-5.7<br/>

D:\__sdkext\_Maya2017 Scripts being my local folder. <br/>

<br/>

Here are the instructions and scripts for building SIP and PyQt.<br/>

<br/>

Follow the instructions from the API docs to setup your environment (Developer Resources &gt; API Guide &gt; Setting up your build environment &gt; Windows environment (64-bit), in the Maya Documentation)<br/>

<br/>

Please backup your qt.conf first, you'll need to restore it after building PyQt5. Replace …/bin/qt.conf with below:<br/>

<pre id='WOCACAfXEGg'>[Paths]<br>Prefix=$(MAYA_LOCATION)<br>Libraries=lib <br>Binaries=bin <br>Headers=include/Qt<br>Data=.<br>Plugins=qt-plugins <br>Translations=qt-translations <br>Qml2Imports=qml</pre>

<br/>

Unzip the /include/qt-5.6.1-include.tar.gz into /include/Qt <br/>

<br/>

Unzip the /mkspecs/qt-5.6.1-mkspecs.tar.gz into /mkspecs<br/>

<br/>

Please run following build scripts with VS2012 x64 Native Tools Command  Prompt. If your Maya installed in folders required administrator privilege(e.g. Program files), please run the command prompt as Administrator.<br/>

<h3 id='WOCACADYP5x'>Environment Setup</h3>

<pre id='WOCACA8HecY'>@echo off<br><br>set MAYAVERSION=2017<br>set ADSKQTVERSION=5.6.1<br>set SIPVERSION=4.18.1<br>set PYQTVERSION=5.7<br>set MAYADRIVE=m:<br>set BUILDDRIVE=v:<br><br>if exist %MAYADRIVE%\nul subst %MAYADRIVE% /d<br>subst %MAYADRIVE% "C:\Program Files\Autodesk\Maya%MAYAVERSION%"<br>set MAYA_LOCATION=%MAYADRIVE%<br><br>set MAYAPYQTBUILD=%~dp0<br>rem Removing trailing \<br>set MAYAPYQTBUILD=%MAYAPYQTBUILD:~0,-1%<br><br>if exist %BUILDDRIVE%\nul subst %BUILDDRIVE% /d<br>subst %BUILDDRIVE% "%MAYAPYQTBUILD%"<br><br>set SIPDIR=%BUILDDRIVE%\sip-%SIPVERSION%<br>set PYQTDIR=%BUILDDRIVE%\PyQt5_gpl-%PYQTVERSION%<br>rem set ADSKQTDIR=%BUILDDRIVE%\qt-%ADSKQTVERSION%<br>set QTDIR=%MAYA_LOCATION%<br><br>set PATH=%QTDIR%\bin;%PATH%<br>set MSVC_VERSION=2012<br>set MSVC_DIR=C:\Program Files (x86)\Microsoft Visual Studio 11.0<br>set QMAKESPEC=%QTDIR%\mkspecs\win32-msvc%MSVC_VERSION%<br>set _QMAKESPEC_=win32-msvc%MSVC_VERSION%<br><br>if ["%LIBPATH%"]==[""] call "%MSVC_DIR%\VC\vcvarsall" amd64<br><br>set INCLUDE=%INCLUDE%;%MAYA_LOCATION%\include\python2.7<br>set LIB=%LIB%;%MAYA_LOCATION%\lib</pre>

<h3 id='WOCACAUq70U'>Build &amp; Install SIP</h3>

Please use the script below, you can also find it in GitHub.<br/>

<pre id='WOCACAKPSgn'>@echo off<br>set XXX=%~dp0<br>if ["%MAYAPYQTBUILD%"]==[""] call "%XXX%setup.bat"<br><br>pushd %SIPDIR%<br>rem "%MAYA_LOCATION%\bin\mayapy" configure-ng.py --spec %_QMAKESPEC_%<br>"%MAYA_LOCATION%\bin\mayapy" configure.py<br>nmake<br>nmake install<br>popd</pre>

<h3 id='WOCACA8dTeC'>Build &amp; Install PyQt</h3>

Please use the script below, you can also find it in GitHub.<br/>

<pre id='WOCACAOVIUV'>@echo off<br>set XXX=%~dp0<br>if ["%MAYAPYQTBUILD%"]==[""] call "%XXX%setup.bat"<br><br>set QMAKESPEC=%QTDIR%\mkspecs\%_QMAKESPEC_%<br>if not exist "%QMAKESPEC%\qmake.conf" (<br>    echo "You need to uncompress %MAYA_LOCATION%\mkspecs\qt-5.6.1-mkspecs.tar.gz !"<br>    goto end<br>)<br>if not exist "%MAYA_LOCATION%\include\Qt\QtCore\qdir.h" (<br>    echo "You need to uncompress %MAYA_LOCATION%\include\qt-5.6.1-include.tar.gz in %MAYA_LOCATION%\include\Qt !"<br>    goto end<br>)<br>findstr /L /C:"Headers=include/Qt" "%MAYA_LOCATION%\bin\qt.conf" &gt;nul 2&gt;&amp;1<br>if ERRORLEVEL 1 (<br>    echo "You need to edit %MAYA_LOCATION%\bin\qt.conf to use 'Headers=include/Qt'"<br>    goto end<br>)<br>findstr /L /C:"-lqtmain -lshell32" "%QTDIR%\mkspecs\common\msvc-desktop.conf" &gt;nul 2&gt;&amp;1<br>if ERRORLEVEL 1 (<br>    echo "You need to edit %QTDIR%\mkspecs\common\msvc-desktop.conf to use 'QMAKE_LIBS_QT_ENTRY     = -lqtmain -lshell32'"<br>    goto end<br>)<br>if not exist "%MAYA_LOCATION%\include\Qt\qtnfc.disabled" (<br>    echo "You need to rename %MAYA_LOCATION%\include\Qt\qtnfc to %MAYA_LOCATION\include\Qt\qtnfc.disabled"<br>    goto end<br>)<br>    <br>pushd %PYQTDIR%<br><br>"%MAYA_LOCATION%\bin\mayapy" configure.py --spec %QMAKESPEC% LIBDIR_QT="%QTDIR%\lib" INCDIR_QT="%QTDIR%\include\Qt" MOC="%QTDIR%\bin\moc.exe" --sip="%QTDIR%\Python\sip.exe" --sip-incdir="%QTDIR%\Python\include" -w --no-designer-plugin<br>nmake<br>nmake install<br>popd<br><br>:end</pre>

<br/>

You're done! Please check the testing paragraph at the end of the article.<br/>

<br/>

<h1 id='WOCACAco0Tn'>Testing </h1>

Copy and paste this example in the Maya Script Editor (in a Python tab), and execute the code:<br/>

<pre id='WOCACAPKhbu'>import sys <br>from PyQt5.QtWidgets import (QWidget, QToolTip, QPushButton) <br>from PyQt5.QtGui import QFont     <br> <br>class Example(QWidget):<br>    def __init__(self):<br>        super(Example,self).__init__()<br>        self.initUI()<br><br>    def initUI(self):<br>        QToolTip.setFont(QFont('SansSerif', 10))<br>        self.setToolTip('This is a &lt;b&gt;QWidget&lt;/b&gt; widget')<br>        btn = QPushButton('Button', self)<br>        btn.setToolTip('This is a &lt;b&gt;QPushButton&lt;/b&gt; widget')<br>        btn.resize(btn.sizeHint())<br>        btn.move(50, 50) <br>        self.setGeometry(300, 300, 300, 200)<br>        self.setWindowTitle('Tooltips')<br>        self.show()<br>        <br>ex = Example() </pre>
