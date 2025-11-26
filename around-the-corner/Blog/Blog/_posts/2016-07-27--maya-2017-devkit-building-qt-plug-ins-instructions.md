---
layout: "post"
title: "Maya 2017 devkit - Building Qt plug-ins instructions"
date: "2016-07-27 10:13:21"
author: "Cyrille Fauvel"
categories:
  - "C++"
  - "Cyrille Fauvel"
  - "Maya"
  - "Plug-in"
  - "Qt"
  - "Samples"
original_url: "https://around-the-corner.typepad.com/adn/2016/07/-maya-2017-devkit-building-qt-plug-ins-instructions.html "
typepad_basename: "-maya-2017-devkit-building-qt-plug-ins-instructions"
typepad_status: "Publish"
---

<p>For the Qt plug-ins, these are the recent changes made that are not reflected in the documentation but are important to know.</p>
<p>Using the qmake executable and its qt.conf file from the devkit archive is recommended. The use of this qt.conf file uses MAYA_LOCATION and DEVKIT_LOCATION to locate the expected header/library files. Therefore, users must set both environment variables before building the samples.</p>
<p>DEVKIT_LOCATION should point to the directory where the devkit include, mkspecs, cmake directories are located.</p>
<p>After running qmake, a makefile is created and named per pluigin with the .mak extension.</p>
<p>A top level Makefile.qt is provided that can be used to build the default Qt plug-ins.</p>
<p>For more details (exactly what will change in the API Guide), please see the text in <span style="color: #ff0000;">red</span> below.</p>
<p>&#0160;</p>
<p>&#0160;</p>
<h2>Building the plug-in</h2>
<p>&lt;image007.png&gt;</p>
<p>As noted at the start of this section, Maya ships with customized versions of the Qt libraries and header files. It is important to ensure that the directory containing Maya&#39;s version of the headers appears in your include path before those which you may have in a separate Qt installation elsewhere on your system, and that the directory containing Maya&#39;s version of the libraries appears in your library path before any others.</p>
<p>It is important to use the version of qmake that is provided with the <span style="color: #ff0000;">Maya Developer Kit</span>. After running qmake, a <span style="color: #ff0000;">makefile</span> is generated that can be used to build the Qt plug-in.</p>
<p>To build the Qt plug-ins provided with the Maya Developer Kit, follow these steps:</p>
<ol>
<li>Use the following Qt archives from the Maya installation, and extract them in place.</li>
</ol>
<p>You can also obtain the same files from the Maya Developer Kit:</p>
<ol>
<ul>
<li>include/qt-5.6.1-include.tar.gz</li>
<li>mkspecs/qt-5.6.1-mkspecs.tar.gz</li>
<li>lib/cmake/qt-5.6.1-cmake.tar.gz</li>
</ul>
</ol>
<ol>
<li>Obtain the plug-in example files from the Maya Developer Kit; for example, the helixQtCmd.cpp, helixQtCmd.h, and helixQtCmd.pro files for the <em>helixQtCmd</em> example.</li>
<li>Create a helixQtCmd directory.<br /><br />Copy the aforementioned helixQtCmd.* files to the helixQtCmd directory.<br /><br /></li>
<li>Copy the qtconfig file (from the devkit/plug-ins directory) to the helixQtCmd directory.</li>
<li>Set your MAYA_LOCATION environment variable to point to your Maya installation, <span style="color: #ff0000;">and the DEVKIT_LOCATION environment variable to point to the directory where the devkit include, mkspecs, cmake directories are located.</span></li>
<li>Navigate to the helixQtCmd directory.</li>
<li>Run the version of <em>qmake</em> that is <em>provided with the <span style="color: #ff0000;">Maya Developer Kit archive</span></em>.<br /><br /><span style="color: #ff0000;">$DEVKIT_LOCATION/devkit/bin/qmake helixQtCmd.pro</span><br /><br /><span style="color: #ff0000;">A makefile is created as a result and named per plug-in with .mak extension.</span><br /><br />make –f helixQtCmd.mak<br />nmake /f helixQtCmd.mak<br /><br /><span style="color: #ff0000;">A top level Makefile.qt is provided that can be used to build the default Qt plug-ins.</span></li>
</ol>
<ol start="8">
<li>Run make using the generated Makefile as follows:</li>
</ol>
<ol>
<ul>
<li><span style="color: #ff0000;">Linux: make –f Makefile.qt</span></li>
<li><span style="color: #ff0000;">Mac OS X: make –f Makefile.qt</span></li>
<li><span style="color: #ff0000;">Windows: nmake /f Makefile.qt</span></li>
</ul>
</ol>
<p>NOTE:</p>
<p>Makefile requires a qmake project file ending with the extension .pro. Shown below is helixQtCmd.pro, the project file provided in the Developer Kit for the helixQtCmd plug-in. It provides a good example of the straightforward nature of the majority of project files.</p>
<p>include(qtconfig)TARGET = helixQtCmdHEADERS += helixQtCmd.hSOURCES += helixQtCmd.cppLIBS += -lOpenMayaUI</p>
<p>The <em>TARGET</em> setting contains the name of the plug-in, excluding its platform-specific extension.</p>
<p>The <em>HEADERS</em> setting contains a space-separated list of all the header files which are part of the plug-in.</p>
<p>The <em>SOURCES</em> setting contains a space-separated list of all the source files which are part of the plug-in.</p>
<p>By default, the plug-in is automatically linked to Maya&#39;s <em>Foundation</em> and <em>OpenMaya</em> libraries. If the plug-in needs other libraries, then they should be added to the <em>LIBS</em> setting. Library names should be preceded by &#39;-l&#39; while additional library directories should be specified using &#39;-L&#39;. For example:</p>
<p>LIBS += -L/usr/local/lib -lxml</p>
<p>For more complex needs, please refer to Qt&#39;s qmake documentation.</p>
<p>Maya uses a customized version of Digia&#39;s Qt framework and ships with its own versions of all the libraries and header files you need to use Qt in your plug-ins. On Linux and OS X the libraries are in Maya&#39;s <em>lib</em> directory. On Windows the link libraries are in Maya&#39;s <em>lib</em> folder and the DLLs are in Maya&#39;s <em>bin</em> folder. On all platforms the header files are shipped as a compressed archive (e.g. qt-5.6.1-include.tar.gz) in the <em>include</em> directory of your Developer Kit installation. Before doing any Qt plug-in work, uncompress the archive into a directory where you have write permission and be sure to include that directory ahead of any others in your include path.</p>
<p>You can create custom UI for Maya using Qt Designer. For Windows and Linux users, Qt Designer is installed with Maya. For Mac OS X users, you can find Qt Designer directly at the Qt Development Tools website; or, you can build Qt from source.</p>
<p>To obtain <span style="color: #ff0000;">Qt Designer</span>, you can also install Digia&#39;s standard version of Qt on your system or download a copy of the customized Qt source from Autodesk&#39;s Open Source web-site (<a href="http://www.autodesk.com/lgplsource">http://www.autodesk.com/lgplsource</a>) and building the tools yourself. If you choose the latter option the download includes text files describing how to configure, build and install Qt for each platform supported by Maya.</p>
