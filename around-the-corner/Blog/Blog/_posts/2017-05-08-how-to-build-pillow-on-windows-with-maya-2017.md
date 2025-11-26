---
layout: "post"
title: "How to build Pillow on windows with Maya 2017/2018"
date: "2017-05-08 00:39:12"
author: "Cheng Xi Li"
categories:
  - "Maya"
  - "Python"
original_url: "https://around-the-corner.typepad.com/adn/2017/05/how-to-build-pillow-on-windows-with-maya-2017.html "
typepad_basename: "how-to-build-pillow-on-windows-with-maya-2017"
typepad_status: "Publish"
---

<p>Pillow is a Python image library with several other libraries built-in. Because it is originally built with VC2008, we can&#39;t use the wheel package with Maya. We&#39;ll need to build it manually. I wrote this blog for Maya 2017 at first. For Maya 2018, please use replace building environment with VS2015 and Windows SDK 8.1.</p>
<h2 id="MeKACAIkM6Y">Requirements</h2>
<p>VS2012 update 4</p>
<p>CMake &gt; 3.3<br /> <br /> Python 2.7.11 source code</p>
<p>Windows SDK 7.1A</p>
<p>Preparation</p>
<p>Before we build Pillow, we need to install setup tools.<br /> <br /> Run an <strong>VS2012 x64 Native Tools Command Prompt</strong> with administrator privilege. Set following environment variables:</p>
<pre id="MeKACAMnmrA">Path=%path%;Maya2017/bin<br />Python=Maya2017/bin</pre>
<p>Replace Maya2017 with your Maya 2017 installation path. Then make a copy of<strong> mayapy.exe</strong> in the same folder renamed as <strong>python.exe. </strong>We are going to run build commands in this command prompt later.<br /> <br /> Download the following packages from python packages:<br /> <br /> <em><strong>six, packaging, pyparsing, appdirs, setuptools, olefile, nose(for testing only)</strong></em><br /> <br /> Install them in order, using the following commands:</p>
<pre id="MeKACAWu8lc">mayapy setup.py build<br />mayapy setup.py install</pre>
<p>Copy the include folder inside python source into <strong><em>Maya2017/Python</em></strong> and copy<strong><em> Maya2017/libs/python27.lib</em></strong> into <strong><em>Maya2017/Python/Libs.</em></strong><br /> If the <strong><em>Maya/2017/Python/Libs</em></strong> folder doesn&#39;t exist, you&#39;ll have to create it manually. I&#39;ll recommend that you to do it at the end. I&#39;ll explain it later.</p>
<h2 id="MeKACAtMndl">Build the Pillow imaging libraries</h2>
<p>Here is how I built the Pillow-4.1.0 with Maya2017 and it is built with several libraries to support different image formats. For Maya 2018, you&#39;ll need to use VS2015 and some libraries are changed in Pillow 5.1/5.2. Some libraries don&#39;t provide a VS2015 solution, you could upgrade VS2012 solutions to 2015 by changing the project configuration, (e.g. toolset is Visual Studio 2015 and Platform SDK 8.1).<br /> <br /> The build script isn&#39;t working well, so we have to build those libraries manually. You can find the required libraries inside <strong><em>winbuild/config.py</em></strong> . Please download them first.<br /> <br /> We are going to build with MinSizeRel or Release configuration on x64 platform by default. If it is a different configuration, I&#39;ll explain it. The <em>/</em> is root path of each corresponding libraries&#39; source code.<br /> <br /> <strong>Mandatory libraries</strong></p>
<h3 id="MeKACA7RrAN"><span style="font-size: 11pt;">Zlib1.2.11</span></h3>
<p>Open the solution file inside /<em><strong>contrib/vstudio/vc11(for Maya 2018, please use vc14), </strong></em>build the zlibstat project with ReleaseWithoutASM and x64 platform. Copy and rename zlibstat.lib <strong><em>/zlib.lib.</em></strong><br /> <br /> <strong>JPEGsr9b/9c</strong><br /> <br /> Copy win32.mak from Windows SDK 7.1A/include into /. Run the following command in command prompt.</p>
<pre id="MeKACAN2oKc">nmake -f makefile.vc setup-v10</pre>
<p>It will generate a VS2010 solution file(jpeg.sln). Open and upgrade it with VS2012. After building it, copy jpeg.lib to /.</p>
<p>If you are using Pillow 5.2, you&#39;ll need JPEGsr9c. It doesn&#39;t support visual studio 2012/2015. You&#39;ll need to download a Visual studio 2017 and make the project 2015 and no longer requires win32.mak.<br /> <br /> <strong>Optional libraries</strong><br /> <br /> <strong>TIFF 4.0.7/4.0.9</strong><br /> <br /> Use CMake to generate a VS2012 solution and build the tiff project. Default configuration doesn&#39;t have AdobeDeflate and Jpeg Compression support.&#0160; You can modify the config file to make tiff library support it. Zlib and jpeg configurations could be missing too.Please open advanced settings in CMake and set locations to the libraries we&#39;ve built earlier. I found this when trying to build Pillow 5.2 with a forum user, here is a screenshot of his settings on his computer.</p>
<p><img alt="tiff_cmake.PNG" src="https://forums.autodesk.com/t5/image/serverpage/image-id/521235iC4CD267D17F283E7/image-size/large?v=1.0&amp;px=999" title="tiff_cmake.PNG" /></p>
<p>Thanks him for providing this feedback and let me use his screenshot:)</p>
<p>When it is build, the <strong>tiff.lib</strong> should be copied into <strong><em>/libtiff</em></strong>. If not, please copy it. <br /> Copy <strong><em>/build/libtiff/tiffconf.h</em></strong> to <strong><em>/libtiff. </em></strong><br /> Copy <strong>tiff.dll</strong> to Maya2017/bin or you can copy it into PIL folder after building Pillow later. If there is a tiffxx.dll, please copy it too.</p>
<p>If you find unistd.h is missing, please find a dummy one from the internet. I used a file from stackoverflow and put it into VC&#39;s include folder. For VS2015, you may get a snprintf error, please remove the declarations of them in tiff libraries.</p>
<p><br /> <strong>FreeType 2.7.1</strong><br /> <br /> Open and upgrade <em><strong>/builds/windows/vc2010/freetype.sln</strong></em> with VS2012. After building it, copy and rename <em><strong>/objs/vc2012/x64/freetype271.lib</strong></em> to <strong><em>/include/freetype.lib.</em></strong></p>
<p>&#0160;</p>
<p><strong>lcms 2.2.7</strong><br /> <br /> Open <strong><em>/Projects/VC2012/lcms2.sln</em></strong>, build <strong>lcms2</strong> project. Copy and rename<strong><em> /lib/MS/lcms2.lib</em></strong> to <em><strong>/include/lcms2.lib.</strong></em></p>
<p>Copy lcms2.dll to Maya2017/bin or you can copy it into PIL folder after building Pillow later.</p>
<p><br /> <br /> <strong>webp 0.6.0/0.6.1</strong><br /> <br /> Build it with following command:</p>
<p>nmake -f Makefile.vc CFG=release-static RTLIBCFG=static OBJDIR=output all</p>
<p>Copy files from <strong><em>/output/release-static/x64/lib</em></strong> to<strong><em> Pillow-4.1.0/</em></strong>. And copy <em><strong>/src/webp</strong></em> folder to<strong><em> Pillow-4.1.0/libImaging</em></strong><br /> </p>
<p><strong>Openjpeg 2.1.2/openjpeg 2.3.0</strong><br /> <br /> Use CMake to generate a VS2012 solution and build openjp2 project. Create a folder named openjpeg-2.1.2(case sensitive) in /bin, Copy following files from <strong>/src/lib/openjp2</strong> into it.</p>
<pre id="MeKACArJVa1">openjpeg.h<br />opj_config.h<br />opj_stdint.h</pre>
<p>Copy openjp2.lib into Pillow4.1.0/ and copy openjp2.dll to Maya2017/bin or you can copy it into PIL folder after building Pillow later.</p>
<h2 id="MeKACAnfFKk"><strong>Build Pillow</strong></h2>
<p>We can start to build Pillow now. Open setup.py and you&#39;ll find several root variables starting from line 107. Please put the path to the lib file in it. Latest ImageQuant won&#39;t be compiled with VS(allthough it claimed supporting VS2015/Vs2017 in msvc branch, but it uses OpenMP 3.0 which VS2015/2017 doesn&#39;t support when I am writing this blog), so we are ignoring it. The path of JPEG2K should be the path contains openjpeg-2.1.2/openjpeg-2.3.0 folder.</p>
<p>Here is how my libraries look like in Pillow setup.py(pillow 5.1)</p>
<p><span style="font-family: courier new, courier; font-size: 11pt;">JPEG_ROOT = &quot;C:\\pillow-depends\\jpeg-9b&quot;</span><br /><span style="font-family: courier new, courier; font-size: 11pt;">JPEG2K_ROOT = &quot;C:\\pillow-depends\\openjpeg-2.3.0\\bin&quot;</span><br /><span style="font-family: courier new, courier; font-size: 11pt;">ZLIB_ROOT = &quot;C:\\pillow-depends\\zlib-1.2.11&quot;</span><br /><span style="font-family: courier new, courier; font-size: 11pt;">IMAGEQUANT_ROOT = None</span><br /><span style="font-family: courier new, courier; font-size: 11pt;">TIFF_ROOT = &quot;C:\\pillow-depends\\tiff-4.0.9\\libtiff&quot;</span><br /><span style="font-family: courier new, courier; font-size: 11pt;">FREETYPE_ROOT = &quot;C:\\pillow-depends\\freetype-2.9\\include&quot;</span><br /><span style="font-family: courier new, courier; font-size: 11pt;">LCMS_ROOT = &quot;C:\\pillow-depends\\lcms2-2.7\\include&quot;</span></p>
<p>The locations of libraries:</p>
<p><span style="font-family: courier new, courier;">Directory of C:\pillow-depends\freetype-2.9\include</span></p>
<p><span style="font-family: courier new, courier;">07/09/2018 04:14 PM 44,574 freetype.lib</span></p>
<p><br /><span style="font-family: courier new, courier;">Directory of C:\pillow-depends\jpeg-9b</span></p>
<p><span style="font-family: courier new, courier;">07/09/2018 03:27 PM 4,487,122 jpeg.lib</span><br /><br /></p>
<p><span style="font-family: courier new, courier;">Directory of C:\pillow-depends\lcms2-2.7\include</span></p>
<p><span style="font-family: courier new, courier;">07/09/2018 05:26 PM 76,442 lcms2.lib</span><br /><span style="font-family: courier new, courier;">&#0160;</span></p>
<p><span style="font-family: courier new, courier;">Directory of C:\pillow-depends\tiff-4.0.9\libtiff</span></p>
<p><span style="font-family: courier new, courier;">07/09/2018 04:02 PM 35,950 tiff.lib</span><br /><br /></p>
<p><span style="font-family: courier new, courier;">Directory of C:\pillow-depends\zlib-1.2.11</span></p>
<p><span style="font-family: courier new, courier;">07/09/2018 03:29 PM 728,368 zlib.lib</span><br /><br /></p>
<p><span style="font-family: courier new, courier;">Directory of c:\pillow-depends\openjpeg-2.3.0\bin\openjpeg-2.3.0</span></p>
<p><span style="font-family: courier new, courier;">10/05/2017 06:23 AM 59,369 openjpeg.h</span><br /><span style="font-family: courier new, courier;">07/09/2018 04:24 PM 342 opj_config.h</span><br /><span style="font-family: courier new, courier;">10/05/2017 06:23 AM 2,171 opj_stdint.h</span></p>
<p><span style="font-family: courier new, courier;">Directory of E:\Pillow-5.1.0</span></p>
<p><span style="font-family: courier new, courier;">07/09/2018 05:42 PM 1,441,052 libwebp.lib</span><br /><span style="font-family: courier new, courier;">07/09/2018 05:41 PM 701,406 libwebpdecoder.lib</span><br /><span style="font-family: courier new, courier;">07/09/2018 04:25 PM 12,796 openjp2.lib</span><br /><span style="font-family: courier new, courier;">07/09/2018 05:34 PM 942,016 webp.lib</span><br /><span style="font-family: courier new, courier;">07/09/2018 05:34 PM 29,000 webpdemux.lib</span></p>
<p><span style="font-family: courier new, courier;">Directory of E:\Pillow-5.1.0\src\libImaging\webp</span></p>
<p><span style="font-family: courier new, courier;">11/29/2017 04:15 AM 3,972 config.h.in</span><br /><span style="font-family: courier new, courier;">11/29/2017 04:01 AM 23,150 decode.h</span><br /><span style="font-family: courier new, courier;">11/29/2017 04:01 AM 15,606 demux.h</span><br /><span style="font-family: courier new, courier;">11/29/2017 04:01 AM 27,481 encode.h</span><br /><span style="font-family: courier new, courier;">11/29/2017 04:01 AM 3,863 format_constants.h</span><br /><span style="font-family: courier new, courier;">11/29/2017 04:01 AM 22,824 mux.h</span><br /><span style="font-family: courier new, courier;">11/29/2017 04:01 AM 3,150 mux_types.h</span><br /><span style="font-family: courier new, courier;">11/29/2017 04:01 AM 1,665 types.h</span></p>
<p>I recommend to enable DEBUG flag and copy the Python include folder after running the script. It will print debug output and automatically stops when python.h is not found. It is very useful to see if any paths are inappropriate. If everything is fine, please copy the include folder from Python 2.7.11 to Maya2017/Python.<br /> <br /> The rest is simple, same as other python libraries:</p>
<pre id="MeKACAy0tAB">mayapy setup.py build<br />mayapy setup.py install</pre>
<p>You can test it with test-install.py. There should be 8 errors because the tiff library doesn&#39;t support certain types. <br /> <br /> If you have a failed case in test_image.getim.py, it isn&#39;t a library issue. The test case sometimes won&#39;t work, so open the test case and modify line 13 from int to (int, long) will fix the failing case.<br /> <br /> Enjoy it now.</p>
