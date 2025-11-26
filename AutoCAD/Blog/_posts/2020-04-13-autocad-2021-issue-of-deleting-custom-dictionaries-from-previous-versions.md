---
layout: "post"
title: "AutoCAD 2021 issue of deleting custom dictionaries from previous versions"
date: "2020-04-13 04:17:08"
author: "Deepak A S Nadig"
categories: []
original_url: "https://adndevblog.typepad.com/autocad/2020/04/autocad-2021-issue-of-deleting-custom-dictionaries-from-previous-versions.html "
typepad_basename: "autocad-2021-issue-of-deleting-custom-dictionaries-from-previous-versions"
typepad_status: "Draft"
---

<p>We have our users reporting an issue with AutoCAD 2021 deleting custom dictionaries from previous versions(steps that can be used to recreate - as reported by a user).</p>
<p>Chances are you have already come across this after looking into AutoCAD 2021.<br />This issue is currently being addressed by the Engineering. We will keep this post updated once the issue is fixed.</p>
<p>Meanwhile, one&#0160;<strong>workaround</strong>&#0160;is to set the system variable LISPSYS = 0 and restart<br />AutoCAD 2021, this will prevent AutoCAD from deleting the previous custom dictionaries.</p>
<p>Click&#0160;<a href="https://knowledge.autodesk.com/support/autocad/learn-explore/caas/CloudHelp/cloudhelp/2021/ENU/AutoCAD-Core/files/GUID-1853092D-6E6D-4A06-8956-AD2C3DF203A3-htm.html">here</a>&#0160;for more on LISPSYS,</p>
<p>Steps to recreate the issue :</p>
<p>1) Open AutoCAD 2020 or an earlier version.<br />2) Open a blank drawing using the acad.dwt template.<br />3) Load the visual lisp extended functions using&#0160;<em>(vl-load-com)</em>&#0160;in commandline<br />4) Create a custom dictionary.<br />&#0160; &#0160; &#0160;- Type in the following:&#0160;<em>(vlax-ldata-put &quot;CUSTOM&quot; &quot;KEY&quot; &quot;VALUE&quot;)</em><br />&#0160; &#0160; &#0160;- The return value in the command line should be &quot;VALUE&quot;.<br />5) Verify the dictionary key has been set.<br />&#0160; &#0160; &#0160;- Type in the following:&#0160;<em>(vlax-ldata-get &quot;CUSTOM&quot; &quot;KEY&quot;)</em><br />&#0160; &#0160; &#0160;- The return value should be &quot;VALUE&quot;.<br />6) Save drawing as DictionaryTest.dwg and close the file.</p>
<p>7) Open DictionaryTest.dwg in AutoCAD 2021 and Load&#0160;<em>(vl-load-com)</em><br />8) Verify the dictionary key:&#0160;<em>(vlax-ldata-get &quot;CUSTOM&quot; &quot;KEY&quot;)</em><br />&#0160; &#0160; - The return value is nil.</p>
