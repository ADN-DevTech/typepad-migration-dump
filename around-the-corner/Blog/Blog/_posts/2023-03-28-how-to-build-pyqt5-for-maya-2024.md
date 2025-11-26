---
layout: "post"
title: "How to build PyQt5 for Maya 2024"
date: "2023-03-28 18:48:03"
author: "Cheng Xi Li"
categories:
  - "Cheng Xi Li"
  - "Maya"
  - "Python"
  - "Qt"
original_url: "https://around-the-corner.typepad.com/adn/2023/03/how-to-build-pyqt5-for-maya-2024.html "
typepad_basename: "how-to-build-pyqt5-for-maya-2024"
typepad_status: "Publish"
---

<div style="font-family: &#39;Roboto Condensed&#39;, Tauri, &#39;Hiragino Sans GB&#39;, &#39;Microsoft YaHei&#39;, STHeiti, SimSun, &#39;Lucida Grande&#39;, &#39;Lucida Sans Unicode&#39;, &#39;Lucida Sans&#39;, &#39;Segoe UI&#39;, AppleSDGothicNeo-Medium, &#39;Malgun Gothic&#39;, Verdana, Tahoma, sans-serif; font-size: 15px; overflow-x: hidden; overflow-y: auto; margin: 0px !important; padding: 20px; background-color: #ffffff; color: #222222; line-height: 1.6; -webkit-font-smoothing: antialiased; background: #ffffff;">
<p style="margin: 1em 0px; word-wrap: break-word;">Maya 2024 is shipped with Python 3.10 <strong>only</strong> on all platforms. Although pip is shipped with Maya 2024, <strong>we still can&#39;t use it to install PyQt5 on Mac or Linux</strong>. On windows, pip installed packages could work. However, it has its own Qt5 libraries installed and it may lead to problems. You may want to replace them with the ones shipped with Maya. I didn&#39;t encounter any problems during my testing with the original pip packages. If you find any problems with it, you can build your own with this guide.</p>
<p style="margin: 1em 0px; word-wrap: break-word;">When I am writing this guide, I am building with <strong>PyQt5-5.15.9</strong>, <strong>sip-6.7.7</strong>, <strong>PyQt5_sip-12.11.1</strong> and <strong>PyQt-builder-1.14.1</strong>.</p>
<h2 id="windows:" style="clear: both; font-size: 1.8em; font-weight: bold; margin: 1.275em 0px 0.85em; border-bottom-width: 1px; border-bottom-style: solid; border-bottom-color: #e6e6e6;"><a href="#windows:" name="windows:" style="text-decoration: none; vertical-align: baseline; color: #3269a0;"></a>Windows:</h2>
<p style="margin-top: 0px; margin: 1em 0px; word-wrap: break-word;">We are going to build it with VS2022 which is same as Maya 2024&#39;s developing environment on Windows. If you&#39;ve installed a path needs Administrator Privileges, please run <strong>x64 Native Tools Command Prompt for VS 2022</strong> as Administrator. I&#39;ve installed my Maya 2024 to default location and I&#39;ve extracted devkit to g:\case\devkitBase\2024. Please update it with the paths on your computer. Here are the steps:</p>
<p style="margin: 1em 0px; word-wrap: break-word;">1. Subst your maya install folder to a dedicate drive if there is a space inside your maya installation path. I am going to subst it to drive v e.g.</p>
<pre style="border-top-left-radius: 3px; border-top-right-radius: 3px; border-bottom-right-radius: 3px; border-bottom-left-radius: 3px; word-wrap: break-word; border: 1px solid #cccccc; overflow: auto; padding: 0.5em;"><code data-origin="&lt;pre&gt;&lt;code&gt;subst v: &quot;C:\Program files\Autodesk\maya2024&quot;
&lt;/code&gt;&lt;/pre&gt;" style="border: 1px solid #cccccc; display: block; font-family: Consolas, Inconsolata, Courier, monospace; font-weight: bold; white-space: pre; margin: 0px 2px; border-top-left-radius: 3px; border-top-right-radius: 3px; border-bottom-right-radius: 3px; border-bottom-left-radius: 3px; word-wrap: break-word; padding: 0px 5px; font-size: 1em; letter-spacing: -1px;">subst v: &quot;C:\Program files\Autodesk\maya2024&quot;
</code></pre>
<p style="margin: 1em 0px; word-wrap: break-word;">2. Add your <strong>Maya2024\Python\scripts and Maya2024\bin</strong> to your system path. If your&#39;ve created a subst drive, please use the path with drive. e.g.</p>
<pre style="border-top-left-radius: 3px; border-top-right-radius: 3px; border-bottom-right-radius: 3px; border-bottom-left-radius: 3px; word-wrap: break-word; border: 1px solid #cccccc; overflow: auto; padding: 0.5em;"><code data-origin="&lt;pre&gt;&lt;code&gt;set path=%path%;v:\bin;v:\Python\Scripts
&lt;/code&gt;&lt;/pre&gt;" style="border: 1px solid #cccccc; display: block; font-family: Consolas, Inconsolata, Courier, monospace; font-weight: bold; white-space: pre; margin: 0px 2px; border-top-left-radius: 3px; border-top-right-radius: 3px; border-bottom-right-radius: 3px; border-bottom-left-radius: 3px; word-wrap: break-word; padding: 0px 5px; font-size: 1em; letter-spacing: -1px;">set path=%path%;v:\bin;v:\Python\Scripts
</code></pre>
<p style="margin: 1em 0px; word-wrap: break-word;">3. Copy <strong>include\Python310\Python</strong> inside Maya2024 installation to <strong>Python\include</strong>. You&#39;ll need to create the destination folder.</p>
<p style="margin: 1em 0px; word-wrap: break-word;">4. Copy <strong>lib\python310.lib</strong> inside Maya2024 installation to <strong>Python\libs</strong>. You&#39;ll need to create the destination folder. Once you&#39;ve copied the libarary, please duplicate one and name it as <strong>python3.lib</strong>.</p>
<p style="margin: 1em 0px; word-wrap: break-word;">5. Go to sip source folder. Use mayapy install sip. e.g.</p>
<pre style="border-top-left-radius: 3px; border-top-right-radius: 3px; border-bottom-right-radius: 3px; border-bottom-left-radius: 3px; word-wrap: break-word; border: 1px solid #cccccc; overflow: auto; padding: 0.5em;"><code data-origin="&lt;pre&gt;&lt;code&gt;
v:\bin\mayapy setup.py install
&lt;/code&gt;&lt;/pre&gt;" style="border: 1px solid #cccccc; display: block; font-family: Consolas, Inconsolata, Courier, monospace; font-weight: bold; white-space: pre; margin: 0px 2px; border-top-left-radius: 3px; border-top-right-radius: 3px; border-bottom-right-radius: 3px; border-bottom-left-radius: 3px; word-wrap: break-word; padding: 0px 5px; font-size: 1em; letter-spacing: -1px;">v:\bin\mayapy setup.py install
</code></pre>
<p style="margin: 1em 0px; word-wrap: break-word;">6. install PyQt-builder and PyQt5-sip with pip</p>
<pre style="border-top-left-radius: 3px; border-top-right-radius: 3px; border-bottom-right-radius: 3px; border-bottom-left-radius: 3px; word-wrap: break-word; border: 1px solid #cccccc; overflow: auto; padding: 0.5em;"><code data-origin="&lt;pre&gt;&lt;code&gt;
mayapy -m pip install PyQt-builder

mayapy -m pip install PyQt5-sip
&lt;/code&gt;&lt;/pre&gt;" style="border: 1px solid #cccccc; display: block; font-family: Consolas, Inconsolata, Courier, monospace; font-weight: bold; white-space: pre; margin: 0px 2px; border-top-left-radius: 3px; border-top-right-radius: 3px; border-bottom-right-radius: 3px; border-bottom-left-radius: 3px; word-wrap: break-word; padding: 0px 5px; font-size: 1em; letter-spacing: -1px;">mayapy -m pip install PyQt-builder
mayapy -m pip install PyQt5-sip
</code></pre>
<p style="margin: 1em 0px; word-wrap: break-word;">7. Download devkit, unzip the mkspec in the mkspec folder directly into its directory. Unzip the Qt include files into the include directory directly. Rename <strong>include\qtnfc</strong> to <strong>qtnfc.disabled</strong>.</p>
<p style="margin: 1em 0px; word-wrap: break-word;">8. Update <strong>mkspecs/common/msvc-desktop.conf</strong> in the mkspecs, enable shell and add COM libraries to build config.</p>
<pre style="border-top-left-radius: 3px; border-top-right-radius: 3px; border-bottom-right-radius: 3px; border-bottom-left-radius: 3px; word-wrap: break-word; border: 1px solid #cccccc; overflow: auto; padding: 0.5em;"><code data-origin="&lt;pre&gt;&lt;code&gt;QMAKE_LIBS              = ole32.lib OleAut32.lib

QMAKE_LIBS_QT_ENTRY     = -lqtmain -lshell32
&lt;/code&gt;&lt;/pre&gt;" style="border: 1px solid #cccccc; display: block; font-family: Consolas, Inconsolata, Courier, monospace; font-weight: bold; white-space: pre; margin: 0px 2px; border-top-left-radius: 3px; border-top-right-radius: 3px; border-bottom-right-radius: 3px; border-bottom-left-radius: 3px; word-wrap: break-word; padding: 0px 5px; font-size: 1em; letter-spacing: -1px;">QMAKE_LIBS              = ole32.lib OleAut32.lib
QMAKE_LIBS_QT_ENTRY     = -lqtmain -lshell32
</code></pre>
<p style="margin: 1em 0px; word-wrap: break-word;">9. Go to PyQt5 source folder. Use following command to install PyQt5. Please update the devkit path with the one on your computer.</p>
<pre style="border-top-left-radius: 3px; border-top-right-radius: 3px; border-bottom-right-radius: 3px; border-bottom-left-radius: 3px; word-wrap: break-word; border: 1px solid #cccccc; overflow: auto; padding: 0.5em;"><code data-origin="&lt;pre&gt;&lt;code&gt;
sip-install --jobs 32 --no-designer-plugin --spec g:\case\devkitBase\2024\mkspecs\win32-msvc --qmake g:\case\devkitBase\2024\devkit\bin\qmake.exe --verbose
&lt;/code&gt;&lt;/pre&gt;" style="border: 1px solid #cccccc; display: block; font-family: Consolas, Inconsolata, Courier, monospace; font-weight: bold; white-space: pre; margin: 0px 2px; border-top-left-radius: 3px; border-top-right-radius: 3px; border-bottom-right-radius: 3px; border-bottom-left-radius: 3px; word-wrap: break-word; padding: 0px 5px; font-size: 1em; letter-spacing: -1px;">sip-install --jobs 32 --no-designer-plugin --spec g:\case\devkitBase\2024\mkspecs\win32-msvc --qmake g:\case\devkitBase\2024\devkit\bin\qmake.exe --verbose
</code></pre>
<p style="margin: 1em 0px; word-wrap: break-word;">The jobs count shouldn&#39;t be more than the count of your logical processors. I am using a 5950x which has 32 logical processors so the jobs is 32 here. If you don&#39;t want debug output, please remove the <strong>—verbose</strong> flag in the end.</p>
<p style="margin: 1em 0px; word-wrap: break-word;">Vola, it works.</p>
<h2 id="mac:" style="clear: both; font-size: 1.8em; font-weight: bold; margin: 1.275em 0px 0.85em; border-bottom-width: 1px; border-bottom-style: solid; border-bottom-color: #e6e6e6;"><a href="#mac:" name="mac:" style="text-decoration: none; vertical-align: baseline; color: #3269a0;"></a>Mac:</h2>
<p style="margin-top: 0px; margin: 1em 0px; word-wrap: break-word;">If you are going to install it on MacOS Ventura or above, please install the latest Xcode 14. I&#39;ve installed Maya 2024 to default path and extract devkit and PyQt5 source codes into <strong>~/Documents/pyqt5.2024/</strong>. Please update the path in the scripts if you are using a different location. I am building it on Xcode 14 with Ventura. If you had encountered issue on a different building environment, please let us know.</p>
<p style="margin: 1em 0px; word-wrap: break-word;">1. Download devkit, unzip the mkspec in the mkspec folder directly into its directory. Unzip the Qt include files into the include directory directly. Rename <strong>include\qtnfc</strong> to <strong>qtnfc.disabled</strong>.</p>
<p style="margin: 1em 0px; word-wrap: break-word;">2. As the day I&#39;ve written this blog, the sip-install can&#39;t add python libraries correctly on my testing environment with XCode 14.1 on Ventura. So, I&#39;ve added them manually. Open <strong>mkspecs\common\clang-mac.conf</strong>. add</p>
<pre style="border-top-left-radius: 3px; border-top-right-radius: 3px; border-bottom-right-radius: 3px; border-bottom-left-radius: 3px; word-wrap: break-word; border: 1px solid #cccccc; overflow: auto; padding: 0.5em;"><code data-origin="&lt;pre&gt;&lt;code&gt;-lpython3.10 -L/Applications/Autodesk/maya2024/Maya.app/Contents/Frameworks/Python.framework/Versions/Current/lib
&lt;/code&gt;&lt;/pre&gt;" style="border: 1px solid #cccccc; display: block; font-family: Consolas, Inconsolata, Courier, monospace; font-weight: bold; white-space: pre; margin: 0px 2px; border-top-left-radius: 3px; border-top-right-radius: 3px; border-bottom-right-radius: 3px; border-bottom-left-radius: 3px; word-wrap: break-word; padding: 0px 5px; font-size: 1em; letter-spacing: -1px;">-lpython3.10 -L/Applications/Autodesk/maya2024/Maya.app/Contents/Frameworks/Python.framework/Versions/Current/lib
</code></pre>
<p style="margin: 1em 0px; word-wrap: break-word;">to the and of QMAKE_LFLAGS. I tried to use <strong>-F</strong> flag to add framework path and it was failed. I&#39;ve created a temporary Python framework at <strong>/Library/Frameworks/Python.framework</strong> with <strong>ln</strong></p>
<pre style="border-top-left-radius: 3px; border-top-right-radius: 3px; border-bottom-right-radius: 3px; border-bottom-left-radius: 3px; word-wrap: break-word; border: 1px solid #cccccc; overflow: auto; padding: 0.5em;"><code data-origin="&lt;pre&gt;&lt;code&gt;sudo ln -s /Applications/Autodesk/maya2024/Maya.app/Contents/Frameworks/Python.framework/Python Python
&lt;/code&gt;&lt;/pre&gt;" style="border: 1px solid #cccccc; display: block; font-family: Consolas, Inconsolata, Courier, monospace; font-weight: bold; white-space: pre; margin: 0px 2px; border-top-left-radius: 3px; border-top-right-radius: 3px; border-bottom-right-radius: 3px; border-bottom-left-radius: 3px; word-wrap: break-word; padding: 0px 5px; font-size: 1em; letter-spacing: -1px;">sudo ln -s /Applications/Autodesk/maya2024/Maya.app/Contents/Frameworks/Python.framework/Python Python
</code></pre>
<p style="margin: 1em 0px; word-wrap: break-word;">3. Install required Python packages.</p>
<pre style="border-top-left-radius: 3px; border-top-right-radius: 3px; border-bottom-right-radius: 3px; border-bottom-left-radius: 3px; word-wrap: break-word; border: 1px solid #cccccc; overflow: auto; padding: 0.5em;"><code data-origin="&lt;pre&gt;&lt;code&gt;sudo ./mayapy -m pip install ply
sudo ./mayapy -m pip install toml 
sudo ./mayapy -m pip install packaging
&lt;/code&gt;&lt;/pre&gt;" style="border: 1px solid #cccccc; display: block; font-family: Consolas, Inconsolata, Courier, monospace; font-weight: bold; white-space: pre; margin: 0px 2px; border-top-left-radius: 3px; border-top-right-radius: 3px; border-bottom-right-radius: 3px; border-bottom-left-radius: 3px; word-wrap: break-word; padding: 0px 5px; font-size: 1em; letter-spacing: -1px;">sudo ./mayapy -m pip install ply
sudo ./mayapy -m pip install toml 
sudo ./mayapy -m pip install packaging
</code></pre>
<p style="margin: 1em 0px; word-wrap: break-word;">4. Go to sip source code folder. Build and install sip. e.g.</p>
<pre style="border-top-left-radius: 3px; border-top-right-radius: 3px; border-bottom-right-radius: 3px; border-bottom-left-radius: 3px; word-wrap: break-word; border: 1px solid #cccccc; overflow: auto; padding: 0.5em;"><code data-origin="&lt;pre&gt;&lt;code&gt;sudo /Applications/Autodesk/Maya2024/Maya.app/Contents/bin/mayapy setup.py install
&lt;/code&gt;&lt;/pre&gt;" style="border: 1px solid #cccccc; display: block; font-family: Consolas, Inconsolata, Courier, monospace; font-weight: bold; white-space: pre; margin: 0px 2px; border-top-left-radius: 3px; border-top-right-radius: 3px; border-bottom-right-radius: 3px; border-bottom-left-radius: 3px; word-wrap: break-word; padding: 0px 5px; font-size: 1em; letter-spacing: -1px;">sudo /Applications/Autodesk/Maya2024/Maya.app/Contents/bin/mayapy setup.py install
</code></pre>
<p style="margin: 1em 0px; word-wrap: break-word;">5. Go to PyQt5_sip folder, install PyQt5_sip</p>
<pre style="border-top-left-radius: 3px; border-top-right-radius: 3px; border-bottom-right-radius: 3px; border-bottom-left-radius: 3px; word-wrap: break-word; border: 1px solid #cccccc; overflow: auto; padding: 0.5em;"><code data-origin="&lt;pre&gt;&lt;code&gt;sudo /Applications/Autodesk/Maya2024/Maya.app/Contents/bin/mayapy setup.py install
&lt;/code&gt;&lt;/pre&gt;" style="border: 1px solid #cccccc; display: block; font-family: Consolas, Inconsolata, Courier, monospace; font-weight: bold; white-space: pre; margin: 0px 2px; border-top-left-radius: 3px; border-top-right-radius: 3px; border-bottom-right-radius: 3px; border-bottom-left-radius: 3px; word-wrap: break-word; padding: 0px 5px; font-size: 1em; letter-spacing: -1px;">sudo /Applications/Autodesk/Maya2024/Maya.app/Contents/bin/mayapy setup.py install
</code></pre>
<p style="margin: 1em 0px; word-wrap: break-word;">6. Go to PyQt builder folder, install PyQt builder.</p>
<pre style="border-top-left-radius: 3px; border-top-right-radius: 3px; border-bottom-right-radius: 3px; border-bottom-left-radius: 3px; word-wrap: break-word; border: 1px solid #cccccc; overflow: auto; padding: 0.5em;"><code data-origin="&lt;pre&gt;&lt;code&gt;sudo /Applications/Autodesk/Maya2024/Maya.app/Contents/bin/mayapy setup.py install
&lt;/code&gt;&lt;/pre&gt;" style="border: 1px solid #cccccc; display: block; font-family: Consolas, Inconsolata, Courier, monospace; font-weight: bold; white-space: pre; margin: 0px 2px; border-top-left-radius: 3px; border-top-right-radius: 3px; border-bottom-right-radius: 3px; border-bottom-left-radius: 3px; word-wrap: break-word; padding: 0px 5px; font-size: 1em; letter-spacing: -1px;">sudo /Applications/Autodesk/Maya2024/Maya.app/Contents/bin/mayapy setup.py install
</code></pre>
<p style="margin: 1em 0px; word-wrap: break-word;">7. Go to PyQt5 source folder and install PyQt5 with following command.</p>
<pre style="border-top-left-radius: 3px; border-top-right-radius: 3px; border-bottom-right-radius: 3px; border-bottom-left-radius: 3px; word-wrap: break-word; border: 1px solid #cccccc; overflow: auto; padding: 0.5em;"><code data-origin="&lt;pre&gt;&lt;code&gt;sudo /Applications/Autodesk/Maya2024/Maya.app/Contents/Frameworks/Python.framework/Versions/Current/bin/sip-install --jobs 16 --no-designer-plugin --spec ~/PyQt5.2024/devkit/mkspecs/macx-clang --qmake ~/PyQt5.2024/devkit/devkit/bin/qmake --verbose
&lt;/code&gt;&lt;/pre&gt;" style="border: 1px solid #cccccc; display: block; font-family: Consolas, Inconsolata, Courier, monospace; font-weight: bold; white-space: pre; margin: 0px 2px; border-top-left-radius: 3px; border-top-right-radius: 3px; border-bottom-right-radius: 3px; border-bottom-left-radius: 3px; word-wrap: break-word; padding: 0px 5px; font-size: 1em; letter-spacing: -1px;">sudo /Applications/Autodesk/Maya2024/Maya.app/Contents/Frameworks/Python.framework/Versions/Current/bin/sip-install --jobs 16 --no-designer-plugin --spec ~/PyQt5.2024/devkit/mkspecs/macx-clang --qmake ~/PyQt5.2024/devkit/devkit/bin/qmake --verbose
</code></pre>
<p style="margin: 1em 0px; word-wrap: break-word;">That&#39;s it.</p>
<h2 id="linux" style="clear: both; font-size: 1.8em; font-weight: bold; margin: 1.275em 0px 0.85em; border-bottom-width: 1px; border-bottom-style: solid; border-bottom-color: #e6e6e6;"><a href="#linux" name="linux" style="text-decoration: none; vertical-align: baseline; color: #3269a0;"></a>Linux</h2>
<p style="margin-top: 0px; margin: 1em 0px; word-wrap: break-word;">If you are going to install it on MacOS Ventura or above, please install the latest Xcode 14. I&#39;ve installed Maya 2024 to default path and extract devkit and PyQt5 source codes into <strong>~/maya/2024/</strong> separately. Please update the path in the scripts if you are using a different location. I am building it on Almalinux 8.7. If you had encountered issue on a different building environment, please let us know.</p>
<p style="margin: 1em 0px; word-wrap: break-word;">1. Download devkit, unzip the mkspec in the mkspec folder directly into its directory. Unzip the Qt include files into the include directory directly. Rename <strong>include\qtnfc</strong> to <strong>qtnfc.disabled</strong>.</p>
<p style="margin: 1em 0px; word-wrap: break-word;">2. Enable gcc-toolset-11</p>
<pre style="border-top-left-radius: 3px; border-top-right-radius: 3px; border-bottom-right-radius: 3px; border-bottom-left-radius: 3px; word-wrap: break-word; border: 1px solid #cccccc; overflow: auto; padding: 0.5em;"><code data-origin="&lt;pre&gt;&lt;code&gt;scl enable gcc-toolset-11 bash
&lt;/code&gt;&lt;/pre&gt;" style="border: 1px solid #cccccc; display: block; font-family: Consolas, Inconsolata, Courier, monospace; font-weight: bold; white-space: pre; margin: 0px 2px; border-top-left-radius: 3px; border-top-right-radius: 3px; border-bottom-right-radius: 3px; border-bottom-left-radius: 3px; word-wrap: break-word; padding: 0px 5px; font-size: 1em; letter-spacing: -1px;">scl enable gcc-toolset-11 bash
</code></pre>
<p style="margin: 1em 0px; word-wrap: break-word;">3. Duplicate Python 3.10 include. The compiler will find the python libraries in maya2024/include/python3.10. Maya2024 ship it path maya2024/include/Python310/Python. We&#39;ll need to duplicate it to make it work.</p>
<pre style="border-top-left-radius: 3px; border-top-right-radius: 3px; border-bottom-right-radius: 3px; border-bottom-left-radius: 3px; word-wrap: break-word; border: 1px solid #cccccc; overflow: auto; padding: 0.5em;"><code data-origin="&lt;pre&gt;&lt;code&gt;cd /usr/autodesk/maya2024/include
sudo mkdir python3.10
sudo cp ../Python310/Python/* . -r
&lt;/code&gt;&lt;/pre&gt;" style="border: 1px solid #cccccc; display: block; font-family: Consolas, Inconsolata, Courier, monospace; font-weight: bold; white-space: pre; margin: 0px 2px; border-top-left-radius: 3px; border-top-right-radius: 3px; border-bottom-right-radius: 3px; border-bottom-left-radius: 3px; word-wrap: break-word; padding: 0px 5px; font-size: 1em; letter-spacing: -1px;">cd /usr/autodesk/maya2024/include
sudo mkdir python3.10
sudo cp ../Python310/Python/* . -r
</code></pre>
<p style="margin: 1em 0px; word-wrap: break-word;">4. Install required Python packages.</p>
<pre style="border-top-left-radius: 3px; border-top-right-radius: 3px; border-bottom-right-radius: 3px; border-bottom-left-radius: 3px; word-wrap: break-word; border: 1px solid #cccccc; overflow: auto; padding: 0.5em;"><code data-origin="&lt;pre&gt;&lt;code&gt;sudo ./mayapy -m pip install ply
sudo ./mayapy -m pip install toml 
sudo ./mayapy -m pip install packaging
&lt;/code&gt;&lt;/pre&gt;" style="border: 1px solid #cccccc; display: block; font-family: Consolas, Inconsolata, Courier, monospace; font-weight: bold; white-space: pre; margin: 0px 2px; border-top-left-radius: 3px; border-top-right-radius: 3px; border-bottom-right-radius: 3px; border-bottom-left-radius: 3px; word-wrap: break-word; padding: 0px 5px; font-size: 1em; letter-spacing: -1px;">sudo ./mayapy -m pip install ply
sudo ./mayapy -m pip install toml 
sudo ./mayapy -m pip install packaging
</code></pre>
<p style="margin: 1em 0px; word-wrap: break-word;">5. Go to sip source code folder. Build and install sip. e.g.</p>
<pre style="border-top-left-radius: 3px; border-top-right-radius: 3px; border-bottom-right-radius: 3px; border-bottom-left-radius: 3px; word-wrap: break-word; border: 1px solid #cccccc; overflow: auto; padding: 0.5em;"><code data-origin="&lt;pre&gt;&lt;code&gt;sudo /usr/autodesk/maya2024/bin/mayapy setup.py install
&lt;/code&gt;&lt;/pre&gt;" style="border: 1px solid #cccccc; display: block; font-family: Consolas, Inconsolata, Courier, monospace; font-weight: bold; white-space: pre; margin: 0px 2px; border-top-left-radius: 3px; border-top-right-radius: 3px; border-bottom-right-radius: 3px; border-bottom-left-radius: 3px; word-wrap: break-word; padding: 0px 5px; font-size: 1em; letter-spacing: -1px;">sudo /usr/autodesk/maya2024/bin/mayapy setup.py install
</code></pre>
<p style="margin: 1em 0px; word-wrap: break-word;">6. Go to PyQt5_sip folder, install PyQt5_sip</p>
<pre style="border-top-left-radius: 3px; border-top-right-radius: 3px; border-bottom-right-radius: 3px; border-bottom-left-radius: 3px; word-wrap: break-word; border: 1px solid #cccccc; overflow: auto; padding: 0.5em;"><code data-origin="&lt;pre&gt;&lt;code&gt;sudo /usr/autodesk/maya2024/bin/mayapy setup.py install
&lt;/code&gt;&lt;/pre&gt;" style="border: 1px solid #cccccc; display: block; font-family: Consolas, Inconsolata, Courier, monospace; font-weight: bold; white-space: pre; margin: 0px 2px; border-top-left-radius: 3px; border-top-right-radius: 3px; border-bottom-right-radius: 3px; border-bottom-left-radius: 3px; word-wrap: break-word; padding: 0px 5px; font-size: 1em; letter-spacing: -1px;">sudo /usr/autodesk/maya2024/bin/mayapy setup.py install
</code></pre>
<p style="margin: 1em 0px; word-wrap: break-word;">7. Go to PyQt builder folder, install PyQt builder.</p>
<pre style="border-top-left-radius: 3px; border-top-right-radius: 3px; border-bottom-right-radius: 3px; border-bottom-left-radius: 3px; word-wrap: break-word; border: 1px solid #cccccc; overflow: auto; padding: 0.5em;"><code data-origin="&lt;pre&gt;&lt;code&gt;sudo /usr/autodesk/maya2024/bin/mayapy setup.py install
&lt;/code&gt;&lt;/pre&gt;" style="border: 1px solid #cccccc; display: block; font-family: Consolas, Inconsolata, Courier, monospace; font-weight: bold; white-space: pre; margin: 0px 2px; border-top-left-radius: 3px; border-top-right-radius: 3px; border-bottom-right-radius: 3px; border-bottom-left-radius: 3px; word-wrap: break-word; padding: 0px 5px; font-size: 1em; letter-spacing: -1px;">sudo /usr/autodesk/maya2024/bin/mayapy setup.py install
</code></pre>
<p style="margin: 1em 0px; word-wrap: break-word;">8. Go to PyQt5 source folder and install PyQt5 with following command.</p>
<pre style="border-top-left-radius: 3px; border-top-right-radius: 3px; border-bottom-right-radius: 3px; border-bottom-left-radius: 3px; word-wrap: break-word; border: 1px solid #cccccc; overflow: auto; padding: 0.5em;"><code data-origin="&lt;pre&gt;&lt;code&gt;sudo /usr/autodesk/maya2024/bin/sip-install --jobs 16 --no-designer-plugin --spec ~/PyQt5.2024/devkit/mkspecs/macx-clang --qmake ~/PyQt5.2024/devkit/devkit/bin/qmake --verbose
&lt;/code&gt;&lt;/pre&gt;" style="border: 1px solid #cccccc; display: block; font-family: Consolas, Inconsolata, Courier, monospace; font-weight: bold; white-space: pre; margin: 0px 2px; border-top-left-radius: 3px; border-top-right-radius: 3px; border-bottom-right-radius: 3px; border-bottom-left-radius: 3px; word-wrap: break-word; padding: 0px 5px; font-size: 1em; letter-spacing: -1px;">sudo /usr/autodesk/maya2024/bin/sip-install --jobs 16 --no-designer-plugin --spec ~/PyQt5.2024/devkit/mkspecs/macx-clang --qmake ~/PyQt5.2024/devkit/devkit/bin/qmake --verbose
</code></pre>
<p style="margin: 1em 0px; word-wrap: break-word;">We have finished installing PyQt5 for Maya 2024 on Linux.</p>
<h2 id="test-your-installation" style="clear: both; font-size: 1.8em; font-weight: bold; margin: 1.275em 0px 0.85em; border-bottom-width: 1px; border-bottom-style: solid; border-bottom-color: #e6e6e6;"><a href="#test-your-installation" name="test-your-installation" style="text-decoration: none; vertical-align: baseline; color: #3269a0;"></a>Test your installation</h2>
<p style="margin-top: 0px; margin: 1em 0px; word-wrap: break-word;">After install PyQt5, you can use following script to test your installation.</p>
<pre style="border-top-left-radius: 3px; border-top-right-radius: 3px; border-bottom-right-radius: 3px; border-bottom-left-radius: 3px; word-wrap: break-word; border: 1px solid #cccccc; overflow: auto; padding: 0.5em;"><code data-origin="&lt;pre&gt;&lt;code&gt;import sys
from PyQt5.QtWidgets import (QWidget, QToolTip, QPushButton)
from PyQt5.QtGui import QFont    

class Example(QWidget):    
    def __init__(self):
        super(Example,self).__init__()       
        self.initUI()


    def initUI(self):        
        QToolTip.setFont(QFont(&#39;SansSerif&#39;, 10))        
        self.setToolTip(&#39;This is a &amp;lt;b&amp;gt;QWidget&amp;lt;/b&amp;gt; widget&#39;)        
        btn = QPushButton(&#39;Button&#39;, self)
        btn.setToolTip(&#39;This is a &amp;lt;b&amp;gt;QPushButton&amp;lt;/b&amp;gt; widget&#39;)
        btn.resize(btn.sizeHint())
        btn.move(50, 50)               
        self.setGeometry(300, 300, 300, 200)
        self.setWindowTitle(&#39;Tooltips&#39;)    
        self.show()      

ex = Example()
&lt;/code&gt;&lt;/pre&gt;" style="border: 1px solid #cccccc; display: block; font-family: Consolas, Inconsolata, Courier, monospace; font-weight: bold; white-space: pre; margin: 0px 2px; border-top-left-radius: 3px; border-top-right-radius: 3px; border-bottom-right-radius: 3px; border-bottom-left-radius: 3px; word-wrap: break-word; padding: 0px 5px; font-size: 1em; letter-spacing: -1px;">import sys
from PyQt5.QtWidgets import (QWidget, QToolTip, QPushButton)
from PyQt5.QtGui import QFont    

class Example(QWidget):    
    def __init__(self):
        super(Example,self).__init__()       
        self.initUI()


    def initUI(self):        
        QToolTip.setFont(QFont(&#39;SansSerif&#39;, 10))        
        self.setToolTip(&#39;This is a &lt;b&gt;QWidget&lt;/b&gt; widget&#39;)        
        btn = QPushButton(&#39;Button&#39;, self)
        btn.setToolTip(&#39;This is a &lt;b&gt;QPushButton&lt;/b&gt; widget&#39;)
        btn.resize(btn.sizeHint())
        btn.move(50, 50)               
        self.setGeometry(300, 300, 300, 200)
        self.setWindowTitle(&#39;Tooltips&#39;)    
        self.show()      

ex = Example()
</code></pre>
</div>
