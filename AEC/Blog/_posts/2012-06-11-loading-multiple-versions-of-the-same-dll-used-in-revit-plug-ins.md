---
layout: "post"
title: "Loading multiple versions of the same DLL used in Revit plug-ins"
date: "2012-06-11 16:09:34"
author: "Saikat Bhattacharya"
categories:
  - ".NET"
  - "Revit"
  - "Saikat Bhattacharya"
original_url: "https://adndevblog.typepad.com/aec/2012/06/loading-multiple-versions-of-the-same-dll-used-in-revit-plug-ins.html "
typepad_basename: "loading-multiple-versions-of-the-same-dll-used-in-revit-plug-ins"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/saikat-bhattacharya.html" target="_self">Saikat Bhattacharya</a></p>
<p>If we have two versions of a plugin both of which uses different versions of a common DLL, after running the older version of the plug-in loaded in Revit, it loads up the older version of the dependent common type library. After this, if we run the other plug-in which references another version of the same dependent DLL, Revit throws an exception. How can we resolve this?</p>
<p>To give an example, lets us create two plugins, A and B and both reference a common DLL called <em>LibraryL</em> which is used with the option &quot;Copy Local&quot; to be true in Visual Studio. The problem comes when we refactor <em>LibraryL</em> in plug-in B solution.</p>
<p>Now if we run plugin A first, it loads old version of Library L (it was copied locally in its bin\Debug folder). Next, we try to run plugin B, which needs library L, but since its already loaded in Revit, the older version of this library is used and plugin B cannot work since it needs the new Library L to work.</p>
<p>What is often desired is that each plug-in uses its local copy of the library L instead of reusing the one of an other plug-in. How can we make this possible?</p>
<p>To resolve this, we should create strong name for the common DLLs to work with simultaneously - merely having a different version number for the common type library does not work.</p>
<p><em><strong>Note</strong>: Since Revit API dlls are not strong named, the following approach only works if the common type library project themselves do not reference the Revit API dlls. </em></p>
<p>If the common type library projects are not themselves referencing the Revit API dlls, we can generate strong names for both the versions of the same common DLL to be able to work&#0160; with them simultaneously. Following is what should be done:</p>
<ul>
<li>Use sn.exe tool to generate unique strong name keys for different versions of the common DLL (LibraryL project in your case).&#0160; In the Start Menu program, click on Visual Studio Tools and open the Visual Studio x64 Win64 Command Prompt and type in *<strong>sn.exe –k c:\Temp\test1.snk</strong>*. You can read up more on the parameters but as overview we are using this command to use the sn.exe and specifying that we want to create a key and save it in a file called test1.snk in the temp folder. </li>
<li>Now, move this test1.snk into the LibraryL project. In the AssemblyInfo.cs file of project LibraryL, add the following attribute called <em>AssemblyKeyFileAttribute</em> and include the strong name key file name (test1.snk) file that we have just moved to. For example, the following screenshot shows how the attribute should read like. </li>
</ul>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b016767a9ff05970b-pi"><img alt="clip_image001" border="0" height="167" src="/assets/image_792893.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: block; float: none; margin-left: auto; margin-right: auto; padding-top: 0px; border: 0px;" title="clip_image001" width="344" /></a></p>
<ul>
<li>Now build ProjectA and close it </li>
<li>Open Project B, make the changes in method name in LibraryL and make corresponding changes to Project B code. </li>
<li>Use the sn.exe tool to generate another unique key, name it test2.snk and move that key to the LibraryL project. </li>
<li>Now in LibraryL’s assemblyInfo.cs file, change the newly created <em>AssemblyKeyFileAttribute</em> to point to the test2.snk file. </li>
<li>Build ProjectB and run it in debug mode </li>
<li>In Revit UI, since you have the manifest files already being created, click on Command A and then you will see the task dialog and then click on command B and you will be able to see the task dialog again (and not the error we were getting before). </li>
</ul>
<p>The updated complete VS project (with sn keys) is provided <span class="asset  asset-generic at-xid-6a0167607c2431970b016767aa0484970b"><a href="http://adndevblog.typepad.com/files/_revitplugins-2.zip">here</a>.</span></p>
<p>Just a note that when you run <em>sn.exe</em>, you might get a *<strong>keyset not defined</strong>* error. This error occurs with the Cryptographic Service Provider. And to resolve it, you can type in *<strong>sn.exe –c</strong>* to reset to default values and then use the *<strong>sn.exe –k &lt;key filename&gt;</strong>* command to generate the strong name key.</p>
<p>This entire behavior is not specific to Revit nor its API but is generic .NET behavior. If you need more information on this, you can read up on the many articles online (and on MSDN) related to this.</p>
