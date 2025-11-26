---
layout: "post"
title: "ClientId Versus AddInId"
date: "2010-06-11 13:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "2011"
  - "Installation"
  - "Utilities"
original_url: "https://thebuildingcoder.typepad.com/blog/2010/06/clientid-versus-addinid.html "
typepad_basename: "clientid-versus-addinid"
typepad_status: "Publish"
---

<p>Looking at many other developers' add-in manifest files here at the AEC DevLab in Waltham, I just noticed that the 

<a href="http://thebuildingcoder.typepad.com/blog/2010/04/addin-manifest-and-guidize.html#4">
ClientId tag</a>

that I have been using in my manifest files has actually been renamed to AddInId to be more consistent.
Most other developers are using the AddInId tag instead of the ClientId one.
The ClientId tag still works, but maybe I should start using AddInId myself as well in future.

<p>I updated my 

<a href="http://thebuildingcoder.typepad.com/blog/2010/04/addin-manifest-and-guidize.html#5">
guidize utility</a> to

populate and update the contents of both the AddInId and ClientId tags.
Here is version 2 which does that, both the 

<span class="asset  asset-generic at-xid-6a00e553e168978833013483f8a044970c"><a href="http://thebuildingcoder.typepad.com/files/guidize_src_2.zip">complete source code and Visual Studio solution file</a></span> and also the 

<span class="asset  asset-generic at-xid-6a00e553e168978833013483f89143970c"><a href="http://thebuildingcoder.typepad.com/files/guidize_exe_2.zip">ready-built executable</a></span> for non-programmers.
