---
layout: "post"
title: "Building Qt, PyQt, PySide for Maya 2013 - Part 1"
date: "2012-10-03 00:29:00"
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
original_url: "https://around-the-corner.typepad.com/adn/2012/10/building-qt-pyqt-pyside-for-maya-2013.html "
typepad_basename: "building-qt-pyqt-pyside-for-maya-2013"
typepad_status: "Publish"
---

<p>To complete the <a href="http://around-the-corner.typepad.com/adn/2012/07/all-things-qt.html" target="_self">article</a> written by Kristine last month, I could not resist to go through the process of building these libraries myself on the 3 platforms Windows x64, Linux and MacOS (I tend not to work on win32 anymore). Unfortunately, the documentation provided by Autodesk isn&#39;t always very clear. Not I mean it is wrong or incorrect, but sometimes assumes you either know these Qt underneath requirements and/or Autodesk internal build procedures.</p>
<h2>
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017c320cc3ff970b-pi" style="display: inline;"><img alt="Build-headacke" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017c320cc3ff970b" src="/assets/image_9559c3.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Build-headacke" /></a><br />Building Qt</h2>
<p>Whatever you decide to do, you will need to build Qt using the Autodesk modified version of Qt. Many time people ask if they can upgrade the Maya Qt to a newer version. The answer is unfortunately NO. You can modify and backport Qt fix into the Maya modified version, but unfortunately you need to use Qt 4.7.1 base for Maya 2013. A
copy of the customized Qt 4.7.1 source is available from Autodesk&#39;s Open Source
web-site (<a href="http://www.autodesk.com/lgplsource">http://www.autodesk.com/lgplsource</a>)
and includes text files describing how to configure, build and install Qt for each
platform supported by Maya.</p>
<p>You will also need to set up your environment accordingly to the API docs
(Developer Resources &gt; API Guide &gt; Setting up your build environment in the Maya Documentation). And use the correct <a href="http://docs.autodesk.com/MAYAUL/2013/ENU/Maya-API-Documentation/files/Setting_up_your_build_environment_Compiler_Requirements.htm" target="_self">compilers</a>.</p>
<ul>
<li><a href="http://docs.autodesk.com/MAYAUL/2013/ENU/Maya-API-Documentation/files/Setting_up_your_build_environment_Mac_OS_X_environment.htm" target="_self">Mac OS X environment</a></li>
<li><a href="http://docs.autodesk.com/MAYAUL/2013/ENU/Maya-API-Documentation/files/Setting_up_your_build_environment_Linux_environments_32bit_and_64bit.htm" target="_self">Linux environments (64 bit)</a></li>
<li><a href="http://docs.autodesk.com/MAYAUL/2013/ENU/Maya-API-Documentation/files/Setting_up_your_build_env_Windows_env_32bit_and_64bit.htm" target="_self">Windows
environment (32‐bit and 64‐bit)</a></li>
</ul>
<p>And follow the instructions for building Qt coming from the following files included in qt-4.7.1-modified_for_maya_2013.zip:</p>
<ul>
<li>howToBuildQtOnMac_m2013.txt for MacOs</li>
<li>howToBuildQtOnLinux_m2013.txt for Linux x64</li>
<li>howToBuildQtOnWindows_m2013.txt for Windows win32 and x64</li>
</ul>
<p><span style="text-decoration: underline;">howToBuildQtOnMac_m2013.txt</span> -&#0160;Let&#39;s start with <strong><em>Mac</em></strong>. First, it is important to use the Snow Leopard 10.6 SDK, but if like me you already upgraded to the Lion version, you will need to modify the build configuration instructions like below. Instead of&#0160;</p>
<p style="padding-left: 30px;">./configure -prefix /Users/<em>myHomeDir</em>/qt-4.7.1 -arch x86_64 -debug-and-release -no-rpath -silent -no-qt3support</p>
<p>do</p>
<p style="padding-left: 30px;">./configure -prefix /Users/<em>myHomeDir</em>/qt-4.7.1 -arch x86_64 -debug-and-release -no-rpath -silent -no-qt3support <strong>-sdk /Developer/SDKs/MacOSX10.6.sdk</strong></p>
<p>If you do not do this, not only the Qt build will fail, but you would use the wrong SDK version. You could eventually decide you prefer to change the Qt source code like on this Qt patch <a href="https://trac.macports.org/attachment/ticket/30262/qt4-mac-lion.diff" target="_self">post </a>to build on Lion, but remember that you should be using the 10.6 SDK version to build Maya 2013 plug-ins. And in case you decide to use the 10.7 SDK version anyway, you&#39;ll probably have other issue later in Maya and/or build Qt/PyQt.</p>
<p>Other than this SDK issue, the instructions for building Qt on the Mac are ok.</p>
<p>For the rest of the post, we will assume Qt is installed on&#0160;/Users/<em>myHomeDir</em>/qt-4.7.1</p>
<p><span style="text-decoration: underline;">howToBuildQtOnLinux_m2013.txt</span> -&#0160;<strong><em>Linux</em></strong> is another story. Not only Linux exists on many different distributions, but all the tools / libraries you need aren&#39;t necessarily present on your distribution. The Maya documentation is telling you all you need to know in case the machine is setup as a developer machine already with all the GCC, Qt, Maya requirements present.</p>
<p>The first thing described in the Maya 2013 documentation for building on Linux is that you need the GCC 4.1.2 compiler. See <a href="http://docs.autodesk.com/MAYAUL/2013/ENU/Maya-API-Documentation/files/Setting_up_your_build_environment_Linux_compiler_requirement.htm" target="_self">here</a>. While the Maya documentation is saying all you need to build GCC, there are few things which aren&#39;t mentioned which can cause you problems if you aren&#39;t familiar with Linux.</p>
<p> First <a href="http://gcc.gnu.org/install/" target="_self">compiling GCC</a> has some <a href="http://gcc.gnu.org/install/prerequisites.html" target="_self">prerequisites</a> which require you to install couple of packages. If you unsure of what you are doing, the simplest would be to log as superuser and install this two group packages. Probably more than what you strictly need, but an easy way to get it right :)</p>
<p style="padding-left: 30px;">yum groupinstall &quot;Developer Tools&quot;<br />yum groupinstall &quot;Developer Libraries&quot;</p>
<p>While we are at yum&#39;ing, you will also need 2 other packages for building Qt, so let&#39;s do it now</p>
<p style="padding-left: 30px;">yum install
glibc-devel.i686<br />yum install libXext-devel</p>
<p>Now, while you build GCC, you may got an error about MAKEINFO not present on your machine even if it is installed properly. In that case, the simplest is to go in the gcc-build directory and edit the Makefile file in &#39;vi&#39; or any text editor. Search for MAKEINFO in that file, and a replace the long line with
missing information&#0160;by:</p>
<p style="padding-left: 30px;">MAKEINFO = /usr/bin/makeinfo</p>
<p>After that follow the instructions, everything should be ok from there.</p>
<p>For the rest of the post, we will assume Qt is installed on&#0160;/usr/local/Trolltech/Qt‐4.7.1</p>
<p><span style="text-decoration: underline;">howToBuildQtOnWindows_m2013.txt</span>&#0160;-&#0160;While for&#0160;<strong><em>Windows</em></strong>&#0160;everything looks straight forward, I mean the only important thing is to have Visual Studio 2010 Service Pack 1 (or the Visual C++ Express 2010 Edition with Service Pack 1), it is not entirely true.</p>
<p>Linux and Mac instructions just tell you to install openssl or verify it is present on your machine. For Windows, the instructions to build Qt mention that you need to download a file named openssl-1.0.0e.zip - but no mention where that file is, and why it is needed.</p>
<p>The
OpenSSL modified source is supposed to be included in the Maya modified archive or on the LPGL Autodesk page but at the time of this post it isn&#39;t. I am not sure of the exact reason, but the fact it is isn&#39;t posted makes impossible for someone outside Autodesk to rebuild a modified version of Qt for Windows that can be used safely in Maya. As far I know the OpenSSL modified source code is required for the Autodesk SyncHub module used for Autodesk&#39; Cloud solutions and interrop operations between Maya, Mudbox, etc... It affects directly QtNetwork.dll, and you would need this modified OpenSSL source if you want to recompile this DLL and post it into Maya bin directory. If you just need to build Qt for compiling other modules, but do not need to post a modified version or QtNetwork.dll, then you do not need that OpenSSL modified version. You can simply ignore the instructions about OpenSSL as I did for the rest of the post.</p>
<p>As a side note, I also wanted to mention that I did not use the JOM tools as specified in the instructions to avoid to use the Nokia Qt SDK, but instead stick using nmake. Making use of JOM speed up the build process, but this is extra few steps which aren&#39;t really needed. You can find JOM <a href="ftp://ftp.qt.nokia.com/jom/" target="_self">here</a>&#0160;as well in case you want. In any case, the Windows instructions make mention of a 2010.05 version, but the URL mentioned in the document does not work. Visit&#0160;<a href="http://qt.digia.com/Product/Qt-SDK/">http://qt.digia.com/Product/Qt-SDK/</a>&#0160;for more details if you interested using it. I did not,</p>
<p>Now that we decided not to use JOM but nmake and that we know that the OpenSSL modified source aren&#39;t available, you can follow the instructions in the document and skip the ones which make reference to either both of JOM and OpenSSL. Note the documentation does not say to install Qt like for the Mac and Linux intructions, so do not forget that step by doing a &#39;nmake install&#39; at the end of the build. Otherwise you&#39;ll get error in the PyQt build.</p>
<p>For the rest of the post, we will assume Qt is installed on c:\qt-adsk-4.7.1<br />(It is always a good idea to use a subst drive rather than c: but this is a detail)</p>
<h2>
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017d3c3be87f970c-pi" style="display: inline;"><img alt="Pyqt4" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017d3c3be87f970c" src="/assets/image_47e075.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Pyqt4" /></a><br />Building SIP and&#0160;PyQt</h2>
<p>Rather than duplicating the required modified instructions here and in the PDF document available <a href="http://images.autodesk.com/adsk/files/pyqtmaya2013.pdf" target="_self">here</a>&#0160;on the Autodesk WEB site, I updated the instructions directly in the PDF document.</p>
<p>One last minute update - the Maya 2013 Extension includes qmake.exe in its bin folder (I.e.: &#39;C:\Program Files\Autodesk\Maya2013.5\bin&#39;), so delete or rename it while you rebuild PyQt or you&#39;ll get strange errors.</p>
<h2>Building PySide</h2>
<p>
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017d3c3becad970c-pi" style="display: inline;"><img alt="Pyside1" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017d3c3becad970c" src="/assets/image_89d485.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Pyside1" /></a></p>
<p>Meet in our next post for building PySide - <a href="http://around-the-corner.typepad.com/adn/2012/10/building-qt-pyqt-pyside-for-maya-2013-part-2.html" target="_self">Building Qt, PyQt, PySide for Maya 2013 - Part 2</a></p>
