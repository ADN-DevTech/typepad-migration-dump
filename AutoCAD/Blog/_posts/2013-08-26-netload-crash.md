---
layout: "post"
title: "NETLOAD crash"
date: "2013-08-26 06:17:44"
author: "Adam Nagy"
categories:
  - ".NET"
  - "Adam Nagy"
  - "AutoCAD"
original_url: "https://adndevblog.typepad.com/autocad/2013/08/netload-crash.html "
typepad_basename: "netload-crash"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>A developer reported a crash when using the NETLOAD command. Just before NETLOAD dialog should come up AutoCAD shuts down. In case of demand loading instead of directly loading the AddIn this error happened when trying to execute a command from the AddIn:</p>
<pre>System.ArgumentNullException: Value cannot be null.
Parameter name: key
at System.Collections.Generic.Dictionary`2.FindEntry(TKey key)
at System.Collections.Generic.Dictionary`2.TryGetValue(TKey key, TValue&amp; value)
at Autodesk.AutoCAD.Runtime.PerDocumentCommandClass.Invoke(MethodInfo mi, Boolean bLispFunction)
at Autodesk.AutoCAD.Runtime.CommandClass.CommandThunk.Invoke()</pre>
<p>Once the video drivers got updated on the laptop the above issues went away and everything seems to work fine now. This example is just there to show what strange issues can be caused by outdated drivers on the system.</p>
