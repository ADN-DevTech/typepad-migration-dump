---
layout: "post"
title: "Fusion add-in path"
date: "2015-11-02 17:51:46"
author: "Adam Nagy"
categories:
  - "Adam"
  - "C++"
  - "Fusion 360"
  - "JavaScript"
  - "Python"
original_url: "https://modthemachine.typepad.com/my_weblog/2015/11/fusion-add-in-path.html "
typepad_basename: "fusion-add-in-path"
typepad_status: "Publish"
---

<p>Some functions might require the full path to a given file. If you know it&#39;s relative path to the add-in then the following could help. The below samples show how to get the add-in folder in different programming languages - these samples are based on the <strong>API</strong>&#0160;online help sample: <a href="http://fusion360.autodesk.com/learning/learning.html?caaskey=caas/CloudHelp/cloudhelp/ENU/Fusion-360-API/files/ExportManager-Sample-htm.html" target="_self" title="">http://fusion360.autodesk.com/learning/learning.html?caaskey=caas/CloudHelp/cloudhelp/ENU/Fusion-360-API/files/ExportManager-Sample-htm.html</a></p>
<p><strong>Python</strong></p>
<pre>def getAddInFolder():
    import os
    folderPath = os.path.dirname(os.path.realpath(__file__))
    
    return folderPath</pre>
<p><strong>JavaScript</strong></p>
<pre>function getAddInFolder() {<br />
    var url = window.location.pathname;
    var des = decodeURI(url);

    // remove the / at beginning
    if (navigator.platform.match(&#39;Win&#39;)) {
        des = des.substr(1);
    }<br />
    var index = des.lastIndexOf(&#39;/&#39;);
    var dir = des.substring(0, index);<br />
    return dir;
}</pre>
<p><strong>C++</strong></p>
<pre>std::string getAddInFolder()
{
#if defined(_WINDOWS) || defined(_WIN32) || defined(_WIN64)
    HMODULE hModule = NULL;
    if (!GetModuleHandleExA(
        GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS | 
        GET_MODULE_HANDLE_EX_FLAG_UNCHANGED_REFCOUNT,
        (LPCSTR) ≥tDllPath, 
        &amp;hModule))
        return &quot;&quot;;

    char winTempPath[2048];
    ::GetModuleFileNameA (hModule, winTempPath, 2048);

    std::string strPath = winTempPath;
    size_t stPos = strPath.rfind(&#39;\\&#39;);
    return strPath.substr(0, stPos);
#else
    Dl_info info;
    dladdr((void*) getDllPath, ∈fo);

    std::string strPath = info.dli_fname;
    int stPos = (int)strPath.rfind(&#39;/&#39;);
    if(stPos != -1)
        return strPath.substr(0, stPos);
    else
        return &quot;&quot;;
#endif
}</pre>
<p>-Adam</p>
