---
layout: "post"
title: "How to build customized QT 5.6.1 for Maya 2017 on Windows"
date: "2016-09-27 19:56:54"
author: "Cheng Xi Li"
categories:
  - "Cheng Xi Li"
  - "Qt"
  - "Windows"
original_url: "https://around-the-corner.typepad.com/adn/2016/09/how-to-build-customized-qt-561-for-maya-2017-on-windows.html "
typepad_basename: "how-to-build-customized-qt-561-for-maya-2017-on-windows"
typepad_status: "Publish"
---

<p>I&#39;ve written an article about <a href="http://around-the-corner.typepad.com/adn/2016/09/how-to-rebuild-pyside2-for-maya-2017-.html" target="_blank">how to build PySide2 </a>earlier. Some partners were asking about how to build QT 5.6.1 This is a guide for building QT 5.6.1 on windows.</p>
<p>Prerequisite:</p>
<ul>
<li>Visual Studio 2012 Update 4 or Update 5</li>
<li>QT 5.6.1</li>
<li>Ruby 2.1.7(32 bits)</li>
<li>Perl for OpenSSL</li>
<li>JOM (recommended)</li>
</ul>
<h3>&#0160;</h3>
<h3>Build Steps:</h3>
<p>&#0160;</p>
<h3>1. Download and unzip the tarball from the <a href="http://www.autodesk.com/company/legal-notices-trademarks/open-source-distribution" target="_blank">Autodesk open source distribution page</a></h3>
<p>Depending on how you extracted the tar file, you may need to remove the ending digits (ie. 000644) in the extracted filename. I am using 7zip and it doesn&#39;t have this issue. Copy it into your qt folder for later usage.</p>
<h3>&#0160;</h3>
<h3>2. Download the Qt distribution labeled <a href="http://download.qt.io/development_releases/prebuilt/icu/prebuilt/msvc2012/" target="_blank">ICU53_1</a></h3>
<p>There are several files available, the one we need is <strong>icu_53_1_msvc_2012_64_devel.7z</strong>. After downloading, extract it and copy the <strong>icu53_1</strong> folder to your QT directory (eg. C:\QT).</p>
<h3>&#0160;</h3>
<h3>3. Download <a href="https://www.openssl.org/" target="_blank">OpenSSL</a>(<a href="https://github.com/openssl/openssl/releases/tag/OpenSSL_1_0_1l" target="_blank">1.0.1l</a>) and build</h3>
<p>You can download it from OpenSSL&#39;s website. Extract it into the QT directory, you&#39;ll need to build it yourself before using it to build QT. Follow the instruction in the INSTALL.W64.</p>
<h3>&#0160;</h3>
<h3>4. Update the Build Environment</h3>
<p>Open and edit BuildQt561-VS2012.cmd and update the path. Notice that for Python, we&#39;d recommend to use a 64 bit Python build with Visual Studio 2012 to avoid any runtime issues. But Official 64 bit Python 2.7 should also be fine to build QT.</p>
<h3>&#0160;</h3>
<h3>5. Configure</h3>
<p>There are several configurations in our document (howToBuildQtOnWindows_m2017.txt), I&#39;ve built both debug and release version of QT. Please execute BuildQt561-VS2012.cmd in a command shell before doing following steps.</p>
<pre><code>cd c:\qt
mkdir qt-build-561
cd qt-build-561
..\qt-adsk-5.6.1\configure -prefix C:\qt\qt-adsk-5.6.1.out -debug-and-release -force-debug-info -opensource -icu -opengl desktop -directwrite -openssl -plugin-sql-sqlite -I c:\qt\openssl\1.0.1l-vc11-nossl3\include -L c:\qt\openssl\1.0.1l-vc11-nossl3 -no-warnings-are-errors -nomake examples -nomake tests
</code></pre>
<p>I copied my OpenSSL build into c:\qt\openssl\1.0.1l-vc11-nossl3. The OpenSSL include folder path should point to <strong>inc32 </strong>folder inside your OpenSSL source package. I made some changes to the path according to the document, you don&#39;t have to do it yourself.</p>
<p>The prefix for parameter is for the final step. You need to specify a directory for the final output package.</p>
<p>For more details about the parameters, please refer to the “how-to” inside the QT package.</p>
<h3>&#0160;</h3>
<h3>6. Make</h3>
<p>If you want to use all of your cores to build it, it is better to get JOM from QT and put its path in the PATH environment variable. Otherwise you can run nmake directly.</p>
<pre><code>jom &gt; build.log

or

nmake
</code></pre>
<h3>&#0160;</h3>
<h3>7. Make install</h3>
<p>Just run <strong><em>nmake install</em></strong>, it will create a final output in the prefix directory in Step 5.</p>
<p>Now that you have built QT 5.6.1 for Maya, you could try to build examples in your QT\Examples folder with your customized QT. When finished, you can put the built programs into the Autodesk QT folder\bin and it should be working properly. Enjoy:)</p>
