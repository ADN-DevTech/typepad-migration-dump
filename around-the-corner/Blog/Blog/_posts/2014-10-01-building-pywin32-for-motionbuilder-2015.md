---
layout: "post"
title: "Building pywin32 for MotionBuilder 2015"
date: "2014-10-01 01:10:44"
author: "Cyrille Fauvel"
categories:
  - "Cyrille Fauvel"
  - "Maya"
  - "MotionBuilder"
  - "Python"
  - "Visual Studio"
original_url: "https://around-the-corner.typepad.com/adn/2014/10/building-pywin32-for-motionbuilder-2015.html "
typepad_basename: "building-pywin32-for-motionbuilder-2015"
typepad_status: "Publish"
---

<p>Recently a customer came to ask why pywin32 do not work on Maya or MotionBuilder. The answer is very simple, Maya and MotionBuilder are compiled the lastest compiler to get the best performances and the latest technology possible. Maya itself is partially compiler with the Intel C++ compiler and Visual Studio 2012 SP4, whereas MotionBuilder uses Visual Studio 2012 SP4 only. The issue is that pywin32 and many of the Python libraries still use an older compiler, like sometimes the Visual Studio 2005 compiler. Like its name says, nearly 10 years old.</p>
<p>Each Visual Studio compiler version comes with CRT, STL, MFC, ATL libraries which are not binary compatible with others. They can eventually be loaded in memory concurrently, but cannot &#39;exchange&#39; (or manage) memory blocks allocated by another version. Doing that would most of the time result in a crash. For that reason, you need to recompile these libraries for both products using their &#39;native&#39; compiler. You cannot get an already compiled package from someone else, if the package is not of the right compiler version.</p>
<p>In this article, I&#39;ll describe the steps I used to recompile the pywin32 library for MotionBuilder 2015. Note that I did not try to port pywin32 to Visual Studio 2012, but just to recompile it as much I could in a simple way. (These steps would also work for Maya 2015, you would only need to change the last 2 lines of the last script)</p>
<p><strong>One last comment</strong> - I used Windows 8.1 for building the package, if you have a previous version of Windows and a previous version than the Microsoft SDK 8 - some changes may be required. Based on comments I received so far from Pierre-Marc Simard ( EIDOS Montreal ), I tried to include some of the required changes in the notes below. Pierre-Marc was very keen to try and reports all the differences he saw on his system. One thing he said, was that he had to run the build few times to resolve some errors magically. I&#39;ll try this later next week,and will report differences I can see. But Windows 8.x and SDK 8 sounds quite easy. &#0160;</p>
<h2>Downloads</h2>
<p>First, you need to download and install Python 2.7.3 x64 on your machine - that is the python version MotionBuilder is using. But unlike Maya, MoBu does not provide a command line version of its VS2012 python version, so this is why we need to install Python. Use the default &#39;C:\Python27&#39; folder to install. You can get the x64 build from <a href="https://www.python.org/download/releases/2.7.3/" target="_self">there</a>.</p>
<p>Next, get the pywin32 source code from&#0160;<a href="http://sourceforge.net/projects/pywin32/?source=navbar" target="_self">http://sourceforge.net/projects/pywin32/?source=navbar</a>, and unzip the source into a folder of your choice. I used &#39;C:\temp\pywin32-219&#39; and used the <a href="http://sourceforge.net/projects/pywin32/files/pywin32/Build%20219/pywin32-219.zip/download" target="_self">latest version</a> at the time of this post. Download source from <a href="http://sourceforge.net/projects/pywin32/files/pywin32/Build%20219/pywin32-219.zip/download" target="_self">here</a>.</p>
<p><a href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017c320cc3ff970b-pi"><img alt="Build-headacke" border="0" src="/assets/image_9559c3.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Build-headacke" /></a></p>
<h2>Missing headers</h2>
<p>To build pywin32, you need couple of additional headers which may not be present on your system.</p>
<p>a- The Outlook 2010: MAPI Header Files - Mapix.h. Download the <a href="http://www.microsoft.com/en-us/download/details.aspx?id=12905" target="_self">zip file</a>, and&#0160;copy all except mapi.h and mspst.h in &quot;C:\Program Files (x86)\Windows Kits\8.0\Include\um&quot;</p>
<p>b- <a href="http://support2.microsoft.com/default.aspx?scid=kb;en-us;291794" target="_self">The MFCMAPI headers</a>. I downloaded them from&#0160;<a href="http://mfcmapi.codeplex.com/SourceControl/latest">http://mfcmapi.codeplex.com/SourceControl/latest</a>, and copied EdkGuid.h and EdkMdb.h in &quot;C:\Program Files (x86)\Windows Kits\8.0\Include\um&quot;</p>
<h2>Code changes required to build on VS2012</h2>
<p>Now the few things to edit before running the build.</p>
<p>If you are on Windows 8 or later, go in &#39;C:\temp\pywin32-219\com\win32comext\axdebug\src\stdafx.h&#39;, and comment out the __MIDL___MIDL_itf_dbgprop_0000_0001 and __MIDL___MIDL_itf_dbgprop_0000_0002 enums.</p>
<pre class="brush: cpp; toolbar: false; first-line: 29;">#if defined(__REQUIRED_RPCNDR_H_VERSION__)
// for some strange reason, these no longer exist in dbgprop.h !?!?
// enum __MIDL___MIDL_itf_dbgprop_0000_0001
    // {	DBGPROP_ATTRIB_NO_ATTRIB	= 0,
	// DBGPROP_ATTRIB_VALUE_IS_INVALID	= 0x8,
	// DBGPROP_ATTRIB_VALUE_IS_EXPANDABLE	= 0x10,
	// DBGPROP_ATTRIB_VALUE_READONLY	= 0x800,
	// DBGPROP_ATTRIB_ACCESS_PUBLIC	= 0x1000,
	// DBGPROP_ATTRIB_ACCESS_PRIVATE	= 0x2000,
	// DBGPROP_ATTRIB_ACCESS_PROTECTED	= 0x4000,
	// DBGPROP_ATTRIB_ACCESS_FINAL	= 0x8000,
	// DBGPROP_ATTRIB_STORAGE_GLOBAL	= 0x10000,
	// DBGPROP_ATTRIB_STORAGE_STATIC	= 0x20000,
	// DBGPROP_ATTRIB_STORAGE_FIELD	= 0x40000,
	// DBGPROP_ATTRIB_STORAGE_VIRTUAL	= 0x80000,
	// DBGPROP_ATTRIB_TYPE_IS_CONSTANT	= 0x100000,
	// DBGPROP_ATTRIB_TYPE_IS_SYNCHRONIZED	= 0x200000,
	// DBGPROP_ATTRIB_TYPE_IS_VOLATILE	= 0x400000,
	// DBGPROP_ATTRIB_HAS_EXTENDED_ATTRIBS	= 0x800000
    // };
// typedef DWORD DBGPROP_ATTRIB_FLAGS;


// enum __MIDL___MIDL_itf_dbgprop_0000_0002
    // {	DBGPROP_INFO_NAME	= 0x1,
	// DBGPROP_INFO_TYPE	= 0x2,
	// DBGPROP_INFO_VALUE	= 0x4,
	// DBGPROP_INFO_FULLNAME	= 0x20,
	// DBGPROP_INFO_ATTRIBUTES	= 0x8,
	// DBGPROP_INFO_DEBUGPROP	= 0x10,
	// DBGPROP_INFO_AUTOEXPAND	= 0x8000000
    // };
// typedef DWORD DBGPROP_INFO_FLAGS;

enum {
   EX_DBGPROP_INFO_ID  =0x0100,
   EX_DBGPROP_INFO_NTYPE  =0x0200,
   EX_DBGPROP_INFO_NVALUE  =0x0400,
   EX_DBGPROP_INFO_LOCKBYTES  =0x0800,
   EX_DBGPROP_INFO_DEBUGEXTPROP  =0x1000
};

#endif
</pre>
<p>Next, go in &#39;C:\temp\pywin32-219\com\win32comext\mapi\src\PyIMAPISession.cpp&#39;, line #778 and change</p>
<pre class="brush: cpp; toolbar: false; first-line: 778;">unsigned long connection;
</pre>
<p>to (Windows 8 or later only)</p>
<pre class="brush: cpp; toolbar: false; first-line: 778;">ULONG_PTR connection;
</pre>
<p>Next, go in &#39;C:\temp\pywin32-219\com\win32comext\mapi\src\PyIMAPITable.cpp&#39;, line #701 and change</p>
<pre class="brush: cpp; toolbar: false; first-line: 701;">_result = (HRESULT )_swig_self-&gt;Advise(_arg0,_arg1,_arg2);
</pre>
<p>to (Windows 8 or later)</p>
<pre class="brush: cpp; toolbar: false; first-line: 701;">_result = (HRESULT )_swig_self-&gt;Advise(_arg0,_arg1,reinterpret_cast&lt;ULONG_PTR *&gt;(_arg2));
</pre>
<p>to (previous Windows versions)</p>
<pre class="brush: cpp; toolbar: false; first-line: 701;">_result = (HRESULT )_swig_self-&gt;Advise(_arg0,_arg1,reinterpret_cast&lt;unsigned long *&gt;(_arg2));
</pre>
<p>and line #1284, from</p>
<pre class="brush: cpp; toolbar: false; first-line:1284;">_result = (HRESULT )_swig_self-&gt;CreateBookmark(_arg0);
</pre>
<p>to</p>
<pre class="brush: cpp; toolbar: false; first-line:1284;">_result = (HRESULT )_swig_self-&gt;CreateBookmark(reinterpret_cast&lt;BookMark *&gt;(_arg0));
</pre>
<h2>Preparing build for VS2012</h2>
<p>Last but not least, pywin32 is not ready for Visual Studio 2012 build, so you need to modify the setup.py file</p>
<p>Next, go in &#39;C:\temp\pywin32-219\setup.py&#39;, line #587-588 and change</p>
<pre class="brush: python; toolbar: false; first-line:587;">        if os.path.isfile(os.path.join(sdk_dir, &quot;include&quot;, &quot;activdbg.h&quot;)):
                kw.setdefault(&#39;extra_compile_args&#39;, []).append(&quot;/DHAVE_SDK_ACTIVDBG&quot;)
</pre>
<p>to (be careful on the indentation)</p>
<pre class="brush: python; toolbar: false; first-line: 587;">        #if os.path.isfile(os.path.join(sdk_dir, &quot;include&quot;, &quot;activdbg.h&quot;)):
        kw.setdefault(&#39;extra_compile_args&#39;, []).append(&quot;/DHAVE_SDK_ACTIVDBG&quot;)
</pre>
<p>line #1062-1065, from</p>
<pre class="brush: python; toolbar: false; first-line:1062;">                vckey = _winreg.OpenKey(_winreg.HKEY_LOCAL_MACHINE, product_key,
                                        0, access)
                val, val_typ = _winreg.QueryValueEx(vckey, &quot;ProductDir&quot;)
                mfc_dir = os.path.join(val, &quot;redist&quot;, plat_dir, mfc_dir)
</pre>
<p>to (be careful on the indentation)</p>
<pre class="brush: python; toolbar: false; first-line: 1062;">                #vckey = _winreg.OpenKey(_winreg.HKEY_LOCAL_MACHINE, product_key,
                #                        0, access)
                #val, val_typ = _winreg.QueryValueEx(vckey, &quot;ProductDir&quot;)
                #mfc_dir = os.path.join(val, &quot;redist&quot;, plat_dir, mfc_dir)
                mfc_dir =r&#39;C:\Program Files (x86)\Microsoft Visual Studio 11.0\VC\redist\x64\Microsoft.VC110.MFC&#39;
</pre>
<p>line #1973-2047, from</p>
<pre class="brush: python; toolbar: false; first-line: 1973;">     WinExt_win32com(&#39;shell&#39;, libraries=&#39;shell32&#39;, pch_header=&quot;shell_pch.h&quot;,
                     windows_h_version = 0x600,
                     sources=(&quot;&quot;&quot;
                         %(shell)s/PyIActiveDesktop.cpp
                         %(shell)s/PyIApplicationDestinations.cpp
                         %(shell)s/PyIApplicationDocumentLists.cpp
                         %(shell)s/PyIAsyncOperation.cpp
                         %(shell)s/PyIBrowserFrameOptions.cpp
                         %(shell)s/PyICategorizer.cpp
                         %(shell)s/PyICategoryProvider.cpp
                         %(shell)s/PyIColumnProvider.cpp
                         %(shell)s/PyIContextMenu.cpp
                         %(shell)s/PyIContextMenu2.cpp
                         %(shell)s/PyIContextMenu3.cpp
                         %(shell)s/PyICopyHook.cpp
                         %(shell)s/PyICurrentItem.cpp
                         %(shell)s/PyICustomDestinationList.cpp
                         %(shell)s/PyIDefaultExtractIconInit.cpp
                         %(shell)s/PyIDeskBand.cpp
                         %(shell)s/PyIDisplayItem.cpp
                         %(shell)s/PyIDockingWindow.cpp
                         %(shell)s/PyIDropTargetHelper.cpp
                         %(shell)s/PyIEnumExplorerCommand.cpp
                         %(shell)s/PyIEnumIDList.cpp
                         %(shell)s/PyIEnumObjects.cpp
                         %(shell)s/PyIEnumResources.cpp
                         %(shell)s/PyIEnumShellItems.cpp
                         %(shell)s/PyIEmptyVolumeCache.cpp
                         %(shell)s/PyIEmptyVolumeCacheCallBack.cpp
                         %(shell)s/PyIExplorerBrowser.cpp
                         %(shell)s/PyIExplorerBrowserEvents.cpp
                         %(shell)s/PyIExplorerCommand.cpp
                         %(shell)s/PyIExplorerCommandProvider.cpp
                         %(shell)s/PyIExplorerPaneVisibility.cpp
                         %(shell)s/PyIExtractIcon.cpp
                         %(shell)s/PyIExtractIconW.cpp
                         %(shell)s/PyIExtractImage.cpp
                         %(shell)s/PyIFileOperation.cpp
                         %(shell)s/PyIFileOperationProgressSink.cpp
                         %(shell)s/PyIIdentityName.cpp
                         %(shell)s/PyIInputObject.cpp
                         %(shell)s/PyIKnownFolder.cpp
                         %(shell)s/PyIKnownFolderManager.cpp
                         %(shell)s/PyINameSpaceTreeControl.cpp
                         %(shell)s/PyIObjectArray.cpp
                         %(shell)s/PyIObjectCollection.cpp
                         %(shell)s/PyIPersistFolder.cpp
                         %(shell)s/PyIPersistFolder2.cpp
                         %(shell)s/PyIQueryAssociations.cpp
                         %(shell)s/PyIRelatedItem.cpp
                         %(shell)s/PyIShellBrowser.cpp
                         %(shell)s/PyIShellExtInit.cpp
                         %(shell)s/PyIShellFolder.cpp
                         %(shell)s/PyIShellFolder2.cpp
                         %(shell)s/PyIShellIcon.cpp
                         %(shell)s/PyIShellIconOverlay.cpp
                         %(shell)s/PyIShellIconOverlayIdentifier.cpp
                         %(shell)s/PyIShellIconOverlayManager.cpp
                         %(shell)s/PyIShellItem.cpp
                         %(shell)s/PyIShellItem2.cpp
                         %(shell)s/PyIShellItemArray.cpp
                         %(shell)s/PyIShellItemResources.cpp
                         %(shell)s/PyIShellLibrary.cpp
                         %(shell)s/PyIShellLink.cpp
                         %(shell)s/PyIShellLinkDataList.cpp
                         %(shell)s/PyIShellView.cpp
                         %(shell)s/PyITaskbarList.cpp
                         %(shell)s/PyITransferAdviseSink.cpp
                         %(shell)s/PyITransferDestination.cpp
                         %(shell)s/PyITransferMediumItem.cpp
                         %(shell)s/PyITransferSource.cpp
                         %(shell)s/PyIUniformResourceLocator.cpp
                         %(shell)s/shell.cpp
                         &quot;&quot;&quot; % dirs).split()),
</pre>
<p>to (be careful on the indentation)</p>
<pre class="brush: python; toolbar: false; first-line: 1973;">    # WinExt_win32com(&#39;shell&#39;, libraries=&#39;shell32&#39;, pch_header=&quot;shell_pch.h&quot;,
                    # windows_h_version = 0x600,
                    # sources=(&quot;&quot;&quot;
                        # %(shell)s/PyIActiveDesktop.cpp
                        # %(shell)s/PyIApplicationDestinations.cpp
                        # %(shell)s/PyIApplicationDocumentLists.cpp
                        # %(shell)s/PyIAsyncOperation.cpp
                        # %(shell)s/PyIBrowserFrameOptions.cpp
                        # %(shell)s/PyICategorizer.cpp
                        # %(shell)s/PyICategoryProvider.cpp
                        # %(shell)s/PyIColumnProvider.cpp
                        # %(shell)s/PyIContextMenu.cpp
                        # %(shell)s/PyIContextMenu2.cpp
                        # %(shell)s/PyIContextMenu3.cpp
                        # %(shell)s/PyICopyHook.cpp
                        # %(shell)s/PyICurrentItem.cpp
                        # %(shell)s/PyICustomDestinationList.cpp
                        # %(shell)s/PyIDefaultExtractIconInit.cpp
                        # %(shell)s/PyIDeskBand.cpp
                        # %(shell)s/PyIDisplayItem.cpp
                        # %(shell)s/PyIDockingWindow.cpp
                        # %(shell)s/PyIDropTargetHelper.cpp
                        # %(shell)s/PyIEnumExplorerCommand.cpp
                        # %(shell)s/PyIEnumIDList.cpp
                        # %(shell)s/PyIEnumObjects.cpp
                        # %(shell)s/PyIEnumResources.cpp
                        # %(shell)s/PyIEnumShellItems.cpp
                        # %(shell)s/PyIEmptyVolumeCache.cpp
                        # %(shell)s/PyIEmptyVolumeCacheCallBack.cpp
                        # %(shell)s/PyIExplorerBrowser.cpp
                        # %(shell)s/PyIExplorerBrowserEvents.cpp
                        # %(shell)s/PyIExplorerCommand.cpp
                        # %(shell)s/PyIExplorerCommandProvider.cpp
                        # %(shell)s/PyIExplorerPaneVisibility.cpp
                        # %(shell)s/PyIExtractIcon.cpp
                        # %(shell)s/PyIExtractIconW.cpp
                        # %(shell)s/PyIExtractImage.cpp
                        # %(shell)s/PyIFileOperation.cpp
                        # %(shell)s/PyIFileOperationProgressSink.cpp
                        # %(shell)s/PyIIdentityName.cpp
                        # %(shell)s/PyIInputObject.cpp
                        # %(shell)s/PyIKnownFolder.cpp
                        # %(shell)s/PyIKnownFolderManager.cpp
                        # %(shell)s/PyINameSpaceTreeControl.cpp
                        # %(shell)s/PyIObjectArray.cpp
                        # %(shell)s/PyIObjectCollection.cpp
                        # %(shell)s/PyIPersistFolder.cpp
                        # %(shell)s/PyIPersistFolder2.cpp
                        # %(shell)s/PyIQueryAssociations.cpp
                        # %(shell)s/PyIRelatedItem.cpp
                        # %(shell)s/PyIShellBrowser.cpp
                        # %(shell)s/PyIShellExtInit.cpp
                        # %(shell)s/PyIShellFolder.cpp
                        # %(shell)s/PyIShellFolder2.cpp
                        # %(shell)s/PyIShellIcon.cpp
                        # %(shell)s/PyIShellIconOverlay.cpp
                        # %(shell)s/PyIShellIconOverlayIdentifier.cpp
                        # %(shell)s/PyIShellIconOverlayManager.cpp
                        # %(shell)s/PyIShellItem.cpp
                        # %(shell)s/PyIShellItem2.cpp
                        # %(shell)s/PyIShellItemArray.cpp
                        # %(shell)s/PyIShellItemResources.cpp
                        # %(shell)s/PyIShellLibrary.cpp
                        # %(shell)s/PyIShellLink.cpp
                        # %(shell)s/PyIShellLinkDataList.cpp
                        # %(shell)s/PyIShellView.cpp
                        # %(shell)s/PyITaskbarList.cpp
                        # %(shell)s/PyITransferAdviseSink.cpp
                        # %(shell)s/PyITransferDestination.cpp
                        # %(shell)s/PyITransferMediumItem.cpp
                        # %(shell)s/PyITransferSource.cpp
                        # %(shell)s/PyIUniformResourceLocator.cpp
                        # %(shell)s/shell.cpp
                        # &quot;&quot;&quot; % dirs).split()),
</pre>
<h2>Building</h2>
<p>Run a VS2012 x64 Native Tool command prompt as Administrator, and execute the following script (for sure make edits as appropriate)</p>
<pre class="brush: shell; toobar: false;">@echo off
 
set MOBUPYWIN32BUILD=%~dp0
set MOBUPYWIN32BUILD=%MOBUPYWIN32BUILD:~0,-1%
if exist v:\nul subst v: /d
subst v: &quot;%MOBUPYWIN32BUILD%&quot;
v:

set PYWIN32_VERSION=pywin32-219

set MAYA_LOCATION=C:\Program Files\Autodesk\Maya2015
if exist m:\nul subst m: /d
subst m: &quot;%MAYA_LOCATION%&quot;
set MAYA_LOCATION=m:

set MOBU_LOCATION=C:\Program Files\Autodesk\MotionBuilder 2015
if exist n:\nul subst n: /d
subst n: &quot;%MOBU_LOCATION%&quot;
set MOBU_LOCATION=n:
 
set MSVC_VERSION=2012
set MSVC_DIR=C:\Program Files (x86)\Microsoft Visual Studio 11.0
rem if [%LIBPATH%]==[] call &quot;%MSVC_DIR%\VC\vcvarsall&quot; amd64
set VS90COMNTOOLS=%VS110COMNTOOLS%
set VS100COMNTOOLS=%VS110COMNTOOLS%
set PATH=%PATH%;C:\Program Files (x86)\Microsoft Visual Studio 11.0\VC
call vcvarsall.bat x86_amd64

set INCLUDE=%INCLUDE%;%MOBU_LOCATION%\OpenRealitySDK\include\python-2.7.3\include
set LIB=%LIB%;%MOBU_LOCATION%\OpenRealitySDK\lib\x64
 
set PYWIN32DIR=v:\%PYWIN32_VERSION%
cd %PYWIN32DIR%

%MAYA_LOCATION%\bin\mayapy.exe setup.py -q build --plat-name=win-amd64
rem C:\Python27\Python setup.py -q build --plat-name=win-amd64
C:\Python27\Python setup.py -q install
xcopy &quot;C:\Python27\Lib\site-packages\*.*&quot; n:\bin\x64\python\site-packages /s /v

</pre>
<h2>Testing</h2>
<p>To test, start MotionBuilder 2015, open the Python Editor and copy paste the following script</p>
<pre class="brush: python; toolbar: false;">import win32com.client
o = win32com.client.Dispatch(&quot;Excel.Application&quot;)
o.Visible = 1
o.Workbooks.Add()
o.Cells(1,1).Value = &quot;Hello&quot;
</pre>
<p>If Excel is showing, you are all set :)</p>
