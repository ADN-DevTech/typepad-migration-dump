---
layout: "post"
title: "Building Qt, PyQt, PySide for Maya 2014 - Part 1 of 1"
date: "2013-03-28 04:10:40"
author: "Cyrille Fauvel"
categories:
  - "Cyrille Fauvel"
  - "Linux"
  - "Mac"
  - "Maya"
  - "Python"
  - "Qt"
  - "Windows"
original_url: "https://around-the-corner.typepad.com/adn/2013/03/building-qt-pyqt-pyside-for-maya-2014-part-1-of-1.html "
typepad_basename: "building-qt-pyqt-pyside-for-maya-2014-part-1-of-1"
typepad_status: "Draft"
---

<p>This is an updated version of the Maya 2013 build instructions for PyQt and PySide (<a href="http://around-the-corner.typepad.com/adn/2012/10/building-qt-pyqt-pyside-for-maya-2013.html" target="_self">here</a>). The very good news is that with Maya 2014, there is no more need to build PySide as it is coming by default in Maya :) So unlike for Maya 2013, there won&#39;t be any Part 2 for PySide this time -</p>
<p>libxml, openSSL, OpenAL,&#0160;python2.7, qt-4.8.2-64, and tbb&#0160;are also coming by default in the Maya include and lib folder, so unless you have a very specific need, you would not need to rebuild any of those like before. Note as well that there is a &#39;C:\Program Files\Autodesk\Maya2014\support\opensource&#39; folder now which contains some of the community source.</p>
<p><a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017c320cc3ff970b-pi" style="display: inline;"><img alt="Build-headacke" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017c320cc3ff970b" src="/assets/image_9559c3.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Build-headacke" /></a></p>
<h2>Building Qt</h2>
<p><strong><em>If you do not need another version of Qt than the one coming with Maya, skip that chapter !</em></strong></p>
<p>You don&#39;t really need to build Qt itself unless you need a home-modified version different than the one shipped with Maya. But if you decide to have your home-made Qt version, you will need to use the Maya modified version as a base to build your version. Many time people ask if they can upgrade the Maya Qt libs to newer version. The answer is unfortunately NO. You can modify and backport Qt fix into the Maya modified version, but unfortunately you need to use Qt 4.8.2 base for Maya 2014. Any other configuration will not be supported.</p>
<p> A copy of the customized Qt 4.8.2 source should be available on the Autodesk&#39;s Open Source web-site (<a href="http://www.autodesk.com/lgplsource" target="_self">http://www.autodesk.com/lgplsource</a>) and includes text files describing how to configure, build and install Qt for each platform supported by Maya. However, I am going to share my own scripts here to avoid you some headackes configuring your system.</p>
<p>You will also need to set up your environment accordingly to the API docs (Developer Resources &gt; API Guide &gt; Setting up your build environment in the Maya Documentation). And use the correct&#0160;<a href="http://docs.autodesk.com/MAYAUL/2014/ENU/Maya-API-Documentation/files/Setting_up_your_build_environment_Compiler_Requirements.htm" target="_self">compilers</a>.</p>
<ul>
<li><a href="http://docs.autodesk.com/MAYAUL/2014/ENU/Maya-API-Documentation/files/Setting_up_your_build_environment_Mac_OS_X_environment.htm" target="_self">Mac OS X environment</a></li>
<li><a href="http://docs.autodesk.com/MAYAUL/2014/ENU/Maya-API-Documentation/files/Setting_up_your_build_environment_Linux_environments_32bit_and_64bit.htm" target="_self">Linux environments (64 bit)</a></li>
<li><a href="http://docs.autodesk.com/MAYAUL/2014/ENU/Maya-API-Documentation/files/Setting_up_your_build_env_Windows_env_32bit_and_64bit.htm" target="_self">Windows environment (32‐bit and 64‐bit)</a></li>
</ul>
<p>Note here tha the Windows title is misleading - there is no 32-bit version of Maya 2014</p>
<p>And follow the instructions for building Qt coming from the following files included in qt-4.8.2-modified_for_maya_2014.zip (or in &#39;C:\Program Files\Autodesk\Maya2014\support\opensource\Qt&#39;):</p>
<ul>
<li>howToBuildQtOnMac_m2014.txt for MacOs</li>
<li>howToBuildQtOnLinux_m2014.txt for Linux x64</li>
<li>howToBuildQtOnWindows_m2014.txt for Windows win32 and x64</li>
</ul>
<p><span style="text-decoration: underline;">howToBuildQtOnMac_m2014.txt</span>&#0160;-&#0160;Let&#39;s start with&#0160;<strong><em>Mac</em></strong>. First, it is important to use the Snow Leopard 10.6 SDK, but if like me you already upgraded to the Mountain Lion version, you will need to modify the build configuration instructions to match your environment.&#0160;</p>
<p>If you do not do this, not only the Qt build will fail, but you would use the wrong SDK version. Other than this SDK issue, the instructions for building Qt on the Mac are ok.</p>
<p>Xcode 4.3.3 is the last version which includes the MacOSX10.6.sdk frameworkSDK. If you are using a newer version of Xcode, one will either need to copy the SDK from Xcode 4.3.3 or change the SDK used. Maya 2014 is built using the 10.6 SDK.</p>
<p>For the rest of the post, we will assume Qt is installed on&#0160;/Users/<em>myHomeDir</em>/qt-4.8.2</p>
<p><span style="text-decoration: underline;">howToBuildQtOnLinux_m2014.txt</span>&#0160;-&#0160;<strong><em>Linux</em></strong>&#0160;is another story. Not only Linux exists on many different distributions, but all the tools / libraries you need aren&#39;t necessarily present on your distribution. The Maya documentation is telling you all you need to know in case the machine is setup as a developer machine already with all the GCC, Qt, Maya requirements present.</p>
<p>The first thing described in the Maya 2014 documentation for building on Linux is that you need the GCC 4.1.2 compiler. See&#0160;<a href="http://docs.autodesk.com/MAYAUL/2014/ENU/Maya-API-Documentation/files/Setting_up_your_build_environment_Linux_compiler_requirement.htm" target="_self">here</a>. While the Maya documentation is saying all you need to build GCC, there are few things which aren&#39;t mentioned which can cause you problems if you aren&#39;t familiar with Linux.</p>
<p>First&#0160;<a href="http://gcc.gnu.org/install/" target="_self">compiling GCC</a>&#0160;has some&#0160;<a href="http://gcc.gnu.org/install/prerequisites.html" target="_self">prerequisites</a>&#0160;which require you to install couple of packages. If you unsure of what you are doing, the simplest would be to log as superuser and install this two group packages. Probably more than what you strictly need, but an easy way to get it right :)</p>
<p>yum groupinstall &quot;Developer Tools&quot;<br />yum groupinstall &quot;Developer Libraries&quot;</p>
<p>While we are at yum&#39;ing, you will also need 2 other packages for building Qt, so let&#39;s do it now</p>
<p>yum install glibc-devel.i686<br />yum install libXext-devel</p>
<p>Now, while you build GCC, you may got an error about MAKEINFO not present on your machine even if it is installed properly. In that case, the simplest is to go in the gcc-build directory and edit the Makefile file in &#39;vi&#39; or any text editor. Search for MAKEINFO in that file, and a replace the long line with missing information&#0160;by:</p>
<p>MAKEINFO = /usr/bin/makeinfo</p>
<p>After that follow the instructions, everything should be ok from there.</p>
<p>For the rest of the post, we will assume Qt is installed on&#0160;/usr/local/Trolltech/Qt‐4.8.2</p>
<p><span style="text-decoration: underline;">howToBuildQtOnWindows_m2014.txt</span>&#0160;-&#0160;While for&#0160;<strong><em>Windows</em></strong>&#0160;everything looks straight forward, I mean the only important thing is to have Visual Studio 2010 Service Pack 1 (or the Visual C++ Express 2010 Edition with Service Pack 1), it is not entirely true.</p>
<p>Linux and Mac instructions just tell you to install openssl or verify it is present on your machine. For Windows, the instructions to build Qt mention that you need to download a file named openssl-1.0.1c.zip - but no mention where that file is, and why it is needed. No worries, it is now distributed in Maya include and lib folder and already compiled for you, and the sources are in &#39;C:\Program Files\Autodesk\Maya2014\support\opensource\OpenSSL&#39;.</p>
<p>As a side note, I also wanted to mention that I did not use the JOM tools as specified in the instructions to avoid to use the Nokia Qt SDK, but instead stick using nmake. Making use of JOM speed up the build process, but this is extra few steps which aren&#39;t really needed. You can find JOM&#0160;<a href="ftp://ftp.qt.nokia.com/jom/" target="_self">here</a>&#0160;as well in case you want. In any case, the Windows instructions make mention of a 2010.05 version, but the URL mentioned in the document does not work. Visit&#0160;<a href="http://qt.digia.com/Product/Qt-SDK/">http://qt.digia.com/Product/Qt-SDK/</a>&#0160;for more details if you interested using it. I did not,</p>
<p>Now that we decided not to use JOM but nmake and that we know that the OpenSSL modified source are shipped with the Maya distribution, you can follow the instructions in the document and skip the ones which make reference to either both of JOM and OpenSSL. Note the documentation does not say to install Qt like for the Mac and Linux intructions, so do not forget that step by doing a &#39;nmake install&#39; at the end of the build. Otherwise you&#39;ll get error in the PyQt build.</p>
<p>For the rest of the post, we will assume Qt is installed on c:\qt-adsk-4.8.2<br />(It is always a good idea to use a subst drive rather than c: but this is a detail)</p>
<h2>Building SIP and&#0160;PyQt</h2>
<p>Rather than duplicating the required modified instructions here and in the PDF document available&#0160;<a href="http://images.autodesk.com/adsk/files/pyqtmaya2014.pdf" target="_self">here</a>&#0160;on the Autodesk WEB site, I updated the instructions directly in the PDF document.</p>
<p>The Maya 2014 distribution includes qmake.exe in its bin folder (I.e.: &#39;C:\Program Files\Autodesk\Maya2014\bin&#39;), so delete or rename it while you rebuild PyQt or you&#39;ll get strange errors.</p>
<h2>My build scripts</h2>
<p><strong><span style="text-decoration: underline;">Windows</span></strong></p>
<pre>build All.bat&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;: Runs all the following scripts in order
build setup.bat&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;: Environment variables
build OpenSSL.bat   : Builds openssl, while we do not need to ;)
build Qt.bat            : Builds Qt, don&#39;t need that either for building PyQt
build SIP.bat&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;: Builds SIP - <span style="color: #c00000;">required</span> to build PyQt
build PyQt.bat         : Builds PyQt

openssl-1.01c     &#0160;&#0160;&#0160;&#0160;: Directory which contains OpenSSL source code
qt-adsk-4.8.2         : Directory which contains modified Qt source code
sip-4.14.5        &#0160;&#0160;&#0160;&#0160;&#0160;&#0160;: Directory which contains SIP source code
PyQt-win-gpl-4.10    : Directory which contains PyQt source code</pre>
<p>Assuming you have a directory &quot;e:\somewhere\mydirectory&quot;, and you have the scripts above in it with the subfolders listed above. You need to modify the <strong>&#39;build setup.bat&#39;</strong> and <strong>&#39;build OpenSSL.bat&#39;</strong> to reflect any of your local machine directories. Note that for OpenSSL you will need to install a Perl interpreter (see line #11 in &#39;build OpenSSL.bat&#39;)</p>
<p>If you just want PyQt, execute:<br />&#0160; build SIP.bat<br />&#0160; build PyQt.bat&#0160;</p>
<p>OSX</p>
<p>Linux</p>
<p>&#0160;</p>
