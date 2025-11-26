---
layout: "post"
title: "NWCreate with .NET Plug-in"
date: "2012-05-16 02:40:33"
author: "Xiaodong Liang"
categories:
  - ".NET"
  - "Navisworks"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/aec/2012/05/nwcreate-with-net-plug-in.html "
typepad_basename: "nwcreate-with-net-plug-in"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>  <p>If you have a dll of nwcreate which exports custom geometries (say mycreate.dll). It will work well if you call it from a standalone EXE (say C#). But when you want to call it from a .NET plug-in, it will crash at LiNwcApiInitialise. </p>  <p>Essentially, NWcreate has already been initialised in the process. The nwcreate.lib is intended for completely standalone applications, and not for use inside the Roamer process. The only sort of third-party code we support inside Roamer is that of third-party file loaders that use lcodpnwcreate.lib/dll. You could use this approach to do something similar. You could write a file loader, then from the .NET plugin ask to load a dummy file, which will invoke the file loader.</p>  <p>Or alternatively, use a separate EXE to create your NWC file, using the standalone nwcreate.lib, and invoke that EXE from your .NET plug-in, This will keep the two processes separate, and all should work fine.</p>  <p>You can also consider to use COM or .NET Remoting to implement this out of process approach: write the server in C++</p>
