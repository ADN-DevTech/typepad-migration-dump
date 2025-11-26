---
layout: "post"
title: "MotionBuilder and Qt"
date: "2012-11-21 02:13:12"
author: "Cyrille Fauvel"
categories: []
original_url: "https://around-the-corner.typepad.com/adn/2012/11/motionbuilder-and-qt.html "
typepad_basename: "motionbuilder-and-qt"
typepad_status: "Draft"
---

<p>MotionBuilder UI uses Qt (very much like Maya) but where Maya uses a custom build of Qt, MotionBuilder used a standard Qt build - version 4.7.0 for MotionBuilder 2013.</p>
<p>
1-	Check the current version of QT used by MotionBuilder, see src/Components/ExternalLibraries/qt/src/corelib/global/qglobal.h for QT_VERSION_STR preprocessor.
</p>
<p>2-	Download Qt website (http://qt.nokia.com/downloads/windows-cpp). Check to download the SOURCE package (it is usually the link under the binary .exe package). The source package is named http://get.qt.nokia.com/qt/source/qt-win-opensource-src-4.X.X.zip.
3-	You know need to compile both the 32 bits and 64 bits version of Qt. You will also need to keep the pdbs to be able to debug Qt. I suggest to compile the 32 bits and 64 bits version in their own directory. I don’t know of any easy way to keep both set of binaries in the same dir.
4-	Unzip the Qt zip package in a folder. Name this folder qt-win-opensource-src-4.X.X_win32.
5-	To compile the 32 bits version of Qt:
a.	In Windows Start Menu, choose Programs -&gt; Microsoft Visual Studio 2008 -&gt; Visual Studio Tools -&gt; Visual Studio 2008 Command Prompt. This will pop a console.
b.	Go to your Qt installation directory. cd C:\\ qt-win-opensource-src-4.5.3_win32.
c.	At the prompt type: configure
d.	You will be asked the type of license, choose “o” (open source), then choose “y” (accept this license). This will build Qt with a LGPL license.
e.	Wait a very long time.
f.	When configure is done, at the prompt type: nmake.
g.	Wait a very long time.
6-	Unzip the Qt zip package in a folder. Name this folder qt-win-opensource-src-4.X.X_x64.
7-	To compile the 64 bits version of Qt (you need to be on a 64 bits machine and to have the correct set of compiler):
a.	From your qt-win-opensource-src-4.X.X_win32\bin folder, copy moc.exe and idc.exe into qt-win-opensource-src-4.X.X_x64\bin. This is because the configure.exe program is a 32 bits program and it will need other 32 bits program (moc and idc) to run.
b.	In Windows Start Menu, choose Programs -&gt; Microsoft Visual Studio 2008 -&gt; Visual Studio Tools -&gt; Visual Studio 2008 x64 Cross Tools Command Prompt. This will pop a console.
c.	Go to your Qt installation directory. Type at the prompt: cd C:\\ qt-win-opensource-src-4.X.X_x64.
d.	At the prompt type: configure
e.	You will be asked the type of license, choose “o” (open source), then choose “y” (accept this license). This will build Qt with a LGPL license.
f.	Wait a very long time.
g.	When configure is done, at the prompt type: nmake.
h.	Wait a very long time.
8-	You will now need to create a directory containing Qt headers. These headers will then be added to MoBu. Since Qt does some funky things in its include files it is not sufficient to copy only the include folders. You will need the following folders from the Qt distribution:
a.	\include
b.	src\corelib\: be sure to remove all .cpp files after.
c.	src\corelib\gui\: be sure to remove all .cpp files after.
d.	src\corelib\opengl\: be sure to remove all .cpp files after.
9-	Now you can install everything in MoBu:
10-	Copy the headers you just prepare in \src\Components\ExternalLibs\qt
11-	Copy the 32 bits libs from \lib to \platform\win32\Components\ExternalLibraries\qt\lib
a.	QtCore4.lib, QtCored4.lib
b.	QtGui4.lib, QtGuid4.lib
c.	QtOpenGl4.lib, QtOpenGld4.lib
12-	Copy the 32 bits libs from \lib to \platform\x64\Components\ExternalLibraries\qt\lib
13-	Copy the 32 bits dlls to \ bin\win32
a.	QtCore4.dll, QtCored4.dll
b.	QtGui4.dll, QtGuid4.dll
c.	QtOpenGl4.dll, QtOpenGld4.dll
d.	Don’t forget to also copy the 3 executables used with Qt:</p>
