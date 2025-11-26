---
layout: "post"
title: "Linking RealDWG SDK C++ rcexelib.obj in Visual Studio Debug mode"
date: "2012-09-19 09:54:33"
author: "Fenton Webb"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "Fenton Webb"
  - "RealDWG"
original_url: "https://adndevblog.typepad.com/autocad/2012/09/linking-realdwg-sdk-c-rcexelibobj-in-visual-studio-debug-mode.html "
typepad_basename: "linking-realdwg-sdk-c-rcexelibobj-in-visual-studio-debug-mode"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>  <p>If you are building a RealDWG application using the C++ API then you will have to link your application with the RealDWG SDK rcexelib.obj so that your application will run.</p>  <p>The thing is with all Autodesk products, we give them to you compiled in Visual Studio ‘Release’ mode, not in ‘Debug’ mode therefore you have to be careful when trying to run in Debug mode.</p>  <p>The reason you have to be careful is because the default Visual Studio project setup for a Debug project is that it:</p>  <ol>   <li>Defines <strong>_DEBUG</strong> preprocessor </li>    <li>Sets the Runtime Library as <strong>Multithreaded Debug DLL</strong></li> </ol>  <p>If you try and build your RealDWG application in Debug mode with these settings, the linker will have some issues because the RealDWG SDK that you are linking to is built with the <strong>NDEBUG</strong> preprocessor symbol and <strong>Multithreaded DLL</strong>. This conflict will either give errors/warnings when building and ultimately crash your application when run.</p>  <p>Therefore, to get your RealDWG to build properly in debug mode, you need to make sure you have <strong>_DEBUG</strong> not set and also make sure you have <strong>Multithreaded DLL </strong>set as the Runtime Library…</p>  <p><img src="/assets/image.png" /></p>
