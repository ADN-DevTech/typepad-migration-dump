---
layout: "post"
title: "How to rebuild PySide2 for Maya 2017 "
date: "2016-09-13 03:18:55"
author: "Cheng Xi Li"
categories:
  - "Cheng Xi Li"
  - "Maya"
  - "Python"
  - "Qt"
original_url: "https://around-the-corner.typepad.com/adn/2016/09/how-to-rebuild-pyside2-for-maya-2017-.html "
typepad_basename: "how-to-rebuild-pyside2-for-maya-2017-"
typepad_status: "Publish"
---

<p>Maya 2017 is moving to QT 5.6.1 and PySide2. PySide2 is currently still in alpha stage, so it&#39;s not as stable as it could be. We will continue to update it in our service packages. However, if you want to upgrade yourself, you can download and build it easily.</p>
<p>Maya 2017 is built with MSVC2012, so we cannot introduce other vc runtime in our build environment. So we will build QT 5.6.1 with MSVC2012 and the Python shipped with Maya.</p>
<p>It&#39;s a good idea to backup the Pyside2 files shipped with Maya before you are trying to replace them.</p>
<p>Here is a list:</p>
<pre><code>Maya2017/bin/pyside2.dll
Maya2017/bin/pyside2-uic
Maya2017/bin/shiboken2.dll

Maya2017/lib/pyside2.lib
Maya2017/lib/shiboken2.lib

Maya2017/Python/Lib/site-packages/shiboken2.pyd
Maya2017/Python/Lib/site-packages/PySide2
Maya2017/Python/Lib/site-packages/pyside2uic
</code></pre>
<p>Let&#39;s begin to build PySide2 now. First, you&#39;ll need to download the source code of QT 5.6.1 and compile it with MSVC2012 64bit. You can find our modified QT package <a href="http://www.autodesk.com/company/legal-notices-trademarks/open-source-distribution" target="_blank">here</a>&#0160;and check the <a href="http://around-the-corner.typepad.com/adn/2016/09/how-to-build-customized-qt-561-for-maya-2017-on-windows.html">guide for building and installing customized QT 5.6.1 on windows</a>.</p>
<p>Then you&#39;ll need to get the latest code from the PySide2 repo. For example:</p>
<pre><code>git clone https://codereview.qt-project.org/pyside/pyside-setup --branch dev --recursive
</code></pre>
<p>After cloning the repo, we need to prepare for MayaPy. First, create a folder named <strong>Libs</strong> inside of <strong><em>Maya2017/Python</em></strong> folder then copy <strong>python27.lib</strong> from<strong><em> Maya2017/lib</em></strong> into it. Copy <strong>Maya2017/include/python2.7</strong> into <strong>Maya2017/Python </strong>also. After that, we can build Pyside2 with following command in VS2012 x64 native tools :</p>
<pre><code>mayapy.exe setup.py build --ignore-git --qmake=%QT5.6.1_MSVC2012_BuildPath%\qmake.exe --cmake=&quot;C:\Program Files (x86)\CMake\bin\cmake.exe&quot; --jobs=9 --jom
</code></pre>
<p>We are using CMake 3.3.x here. JOM is a parallel build tool for QT, you can download it from QT&#39;s website or inside of QtCreator. Please make sure you&#39;ve added JOM&#39;s path into your path environment variable.</p>
<p>It will take about 10 minutes to build Pyside2. Once it is done, go to <strong>pyside_install</strong> folder and replace the <strong><em>Maya2017\bin</em> </strong>folder with the files inside the <strong>pyside_install\py2.7-qt5.6.1-64bit-release\bin</strong> folder;<strong><em>Maya2017\Python\lib\site-packages</em></strong> with <strong>pyside_install\py2.7-qt5.6.1-64bit-release\lib\site-packages</strong> and <strong>Maya2017\lib</strong> with the files in<strong> pyside_install\py2.7-qt5.6.1-64bit-release\lib.</strong></p>
<p>Please make sure to backup the original file, as if there is any other issues caused by the replacement, you can use the original file to revert this workaround.</p>
