---
layout: "post"
title: "Signing Your .Net Add-In"
date: "2008-11-26 18:40:41"
author: "Adam Nagy"
categories:
  - "Add-In Creation"
  - "Brian"
original_url: "https://modthemachine.typepad.com/my_weblog/2008/11/signing-your-net-add-in-1.html "
typepad_basename: "signing-your-net-add-in-1"
typepad_status: "Publish"
---

<p>In my previous posting <a href="http://modthemachine.typepad.com/my_weblog/2008/10/converting-vba-auto-macros-to-an-add-in.html">Converting VBA Auto Macros to an Add-In</a> I described how to create a basic add-in and install it.&#0160; There&#39;s one additional item that I didn&#39;t cover in that posting related to the installation of your add-in.&#0160; When you install your add-in you&#39;ll get the warning shown below.&#0160; It is just a warning and can be ignored, but it&#39;s often misinterpreted as an error and it would be best to avoid it.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc688340105361c356f970b-pi"><img alt="UnsignedAssembly" border="0" height="231" src="/assets/image_703549.jpg" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" width="454" /></a> </p>
<br />
<p>If you read the warning you&#39;ll see that it doesn&#39;t like you to use the /codebase argument with an unsigned assembly.&#0160; We have to use the /codebase argument with an add-in to get it to register correctly so it&#39;s not an option to leave it out.&#0160; The only other option is to sign the assembly, which is what I&#39;ll show you how to do here.</p>
<p><font size="3"><strong>What is Signing?</strong></font>&#0160; <br />Signing is a way of creating a unique ID for your add-in.&#0160; This is also referred to as a &quot;strong name&quot;.&#0160; Without signing your add-in there is the potential, although very minimal, of it conflicting with another .Net assembly.&#0160; A strong name consists of the filename, a key, version, culture (language), and processor type.</p>
<p>The concept of signing can also extended to serve as a security mechanism.&#0160; In addition to providing a unique identifier for the assembly it also identifies the make of the assembly.&#0160; Microsoft calls this <a href="http://technet.microsoft.com/en-us/library/cc750035.aspx">Authenticode</a> and the process is handled by <a href="https://www.verisign.com/products-services/security-services/code-signing/digital-ids-code-signing/index.html">Verisign</a>.&#0160; This type of signing is not required and not something I&#39;ll discuss here.</p>
<p><font size="3"><strong>Creating a Key <br /></strong></font>To sign an assembly you need to have a unique key to sign it with.&#0160; You generate a key using the .Net sn.exe (strong name) utility.&#0160; This utility is located in C:\Program Files\Microsoft Visual Studio 8\SDK\v2.0\Bin.&#0160; I would recommend creating a new directory for the resulting key because a single key can be used to sign all of your add-ins.&#0160; Here&#39;s an example of executing this utility from the command line.&#0160; (This is for Visual Studio 2008.&#0160; If you&#39;re using something else you may need to search for sn.exe.)</p>
<p>&quot;C:\Program Files\Microsoft SDKs\Windows\v6.0A\bin\sn.exe&quot; -k MyKey.snk </p>
<p>This is all shown below.&#0160; Note that because of the spaces in some of the directory names that the double quotes around the full command line are required. </p>
<p>&#0160;<a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc688340105361c3573970b-pi"><img alt="CreateKey" border="0" height="231" src="/assets/image_182492.jpg" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" width="454" /></a> <br /></p>
<p>If you have a version of visual studio besides one of the Express editions, you can use the Visual Studio Command Prompt to make this a bit easier because you won&#39;t need to know where the utility is.&#0160; This creates a cmd window where the paths are set to all of the various .Net related tools.&#0160; If you&#39;re new to Visual Studio, you open a Visual Studio Command Prompt window through the Start menu in the &quot;Visual Studio Tools&quot; list, as shown below. <br /></p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401053624dd38970c-pi"><img alt="CommandPrompt" border="0" height="318" src="/assets/image_955023.jpg" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" width="450" /></a>&#0160; </p>
<p>In the Visual Studio Command Prompt you only need to enter: <br /><br />sn -k MyKey.snk </p>
<br />
<p><font size="3"><strong>Signing the Add-In <br /></strong></font>Now that you have the key, you can use it to sign your add-in.&#0160; To do that run the <strong>Project -&gt; Properties...</strong> command, (the last command in the Project menu).&#0160; Pick the &quot;Signing&quot; tab, click the &quot;Sign the assembly&quot; check box and browse to select your key file.&#0160; Recompile your project and you now have a signed assembly.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc688340105361c357c970b-pi"><img alt="AssemblySigning" border="0" height="314" src="/assets/image_913712.jpg" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" width="454" /></a> </p>
<br />
<p>Now when you run the regasm utility when installing your add-in you should see this and no more errors.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc688340105361c3582970b-pi"><img alt="GoodInstall" border="0" height="230" src="/assets/image_162857.jpg" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" width="454" /></a> </p>
<br />
<p><font size="3"><strong>Using a signed Add-In <br /></strong></font></p>
<p>Using a signed add-in is no different than an unsigned one with one exception.&#0160; To use it you just need to copy it to the computer you want to use it on and register it.&#0160; That&#39;s the same process as an unsigned one, except you won&#39;t get the warning message anymore.&#0160; The difference to be aware of is if you want to replace the existing add-in with a newer version.&#0160; With an unsigned add-in you can just copy the new dll to the computer and everything is fine.&#0160; With a signed add-in you must re-register the add-in.&#0160; Signing ties the registration to the dll version so you need to re-register the add-in to update the version information in the registry to match the current dll.</p>
<p>(Thanks to Neil Munro for a question about the versioning that caused me to investigate this some more and update this posting.)</p>
