---
layout: "post"
title: "Exception while debugging AutoCAD from a .NET project in a multi-project VS 2010 solution"
date: "2013-02-15 14:09:17"
author: "Gopinath Taget"
categories:
  - ".NET"
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Gopinath Taget"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2013/02/exception-while-debugging-autocad-from-a-net-project-in-a-multi-project-vs-2010-solution.html "
typepad_basename: "exception-while-debugging-autocad-from-a-net-project-in-a-multi-project-vs-2010-solution"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/gopinath-taget.html">Gopinath Taget</a></p>  <p>This problem only occurs when we have multiple projects in a VS 2010 solution with a mix of C++ and .NET projects in the solution. Specifically, if we have a .NET project as an active project and we configure the debugger to launch AutoCAD, the debugger will launch AutoCAD from the active .NET project. The exception occurs only when the debugger is launched from a .NET project. It does not occur if the debugger is launched from the C++ projects.</p>  <p>The exception will look something like &quot;Managed Debugging Assistant 'PInvokeStackImbalance' has detected a problem in...&quot;</p>  <p>According to Microsoft, this issue occurs because of changes in interoperability services in .NET 4.0 compared to previous version (.NET 3.5). Specifically, this behavior results from an effort &quot;<i>To improve performance in interoperability with unmanaged code, incorrect calling conventions in a platform invoke now cause the application to fail. In previous versions, the marshaling layer resolved these errors up the stack</i>&quot;.</p>  <p>According to Microsoft, here are the recommendations when this exception occurs: </p>  <p><i>Debugging your applications in Microsoft Visual Studio 2010 will alert you to these errors so you can correct them. </i></p>  <p><i>If you have binaries that cannot be updated, you can include the </i><a href="http://msdn.microsoft.com/en-us/library/ff361650.aspx"><i>&lt;NetFx40_PInvokeStackResilience&gt;</i></a><i> element in your application's configuration file to enable calling errors to be resolved up the stack as in earlier versions. However, this may affect the performance of your application.</i></p>  <p>For more information, please take a look at the documentation for &quot;Platform invoke&quot; feature under &quot;Interoperability&quot; section in the following link:</p>  <p><a href="http://msdn.microsoft.com/en-us/library/ee941656(v=VS.100).aspx">http://msdn.microsoft.com/en-us/library/ee941656(v=VS.100).aspx</a></p>  <p>So setting the NetFx40_PInvokeStackResilience in acad.exe.config file (found in the AutoCAD install folder) will resolve this exception:</p>  <p><i>&lt;configuration&gt;</i><i> </i></p>  <p><i>&lt;startup useLegacyV2RuntimeActivationPolicy=&quot;true&quot;&gt;</i></p>  <p><i>&lt;supportedRuntime version=&quot;v4.0&quot;/&gt;</i></p>  <p><i>&lt;/startup&gt;</i></p>  <p><i>&lt;!--All assemblies in AutoCAD are fully trusted so there's no point generating publisher evidence--&gt;</i></p>  <p><i>&lt;runtime&gt; </i></p>  <p><i>&lt;generatePublisherEvidence enabled=&quot;false&quot;/&gt; </i></p>  <p><i>&lt;NetFx40_PInvokeStackResilience enabled=&quot;1|0&quot;/&gt; </i></p>  <p><i>&lt;/runtime&gt;</i></p>  <p><i>&lt;/configuration&gt;</i></p>
