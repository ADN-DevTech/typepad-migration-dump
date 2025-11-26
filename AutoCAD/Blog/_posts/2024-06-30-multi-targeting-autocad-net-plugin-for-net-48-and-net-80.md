---
layout: "post"
title: "Multi-targeting AutoCAD .NET Plugin for .NET 4.8 and .NET 8.0"
date: "2024-06-30 19:10:00"
author: "Madhukar Moogala"
categories:
  - ".NET"
  - "AutoCAD"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2024/06/multi-targeting-autocad-net-plugin-for-net-48-and-net-80.html "
typepad_basename: "multi-targeting-autocad-net-plugin-for-net-48-and-net-80"
typepad_status: "Publish"
---

<p>
    <script src="https://cdn.jsdelivr.net/gh/google/code-prettify@master/loader/run_prettify.js?skin=sunburst">
    </script>
  </p>
  <p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p>
  <p>Recently, we received a query from an ADN partner on how to organize code to target multiple frameworks, such as
    .NET 4.x and .NET 8.0.</p>
  <h3>Let's take an ARX SDK sample project, EllipseJig.</h3>
  <p>EllipseJig is a simple .NET project that demonstrates the use of Jig APIs to create ellipses. The project is
    available in the ObjectARX SDK samples</p>

  <h4>Here is the project structure</h4>
  <pre class="prettyprint">  <code class="language-xml">
    &lt;Project Sdk="Microsoft.NET.Sdk"&gt;
    &lt;PropertyGroup&gt;
        &lt;AssemblyName&gt;JigSample&lt;/AssemblyName&gt;
        &lt;OutputType&gt;Library&lt;/OutputType&gt;
        &lt;RootNamespace&gt;JigSample&lt;/RootNamespace&gt;
        &lt;TargetFramework&gt;net8.0-windows&lt;/TargetFramework&gt;
        &lt;GenerateAssemblyInfo&gt;False&lt;/GenerateAssemblyInfo&gt;
        &lt;Platforms&gt;x64&lt;/Platforms&gt;
        &lt;Configurations&gt;ACAD2024;ACAD2025&lt;/Configurations&gt;
        &lt;BaseOutputPath&gt;bin&lt;/BaseOutputPath&gt;
    &lt;/PropertyGroup&gt;
    &lt;PropertyGroup Condition="'$(Configuration)' == 'ACAD2025'"&gt;
        &lt;TargetFramework&gt;net8.0-windows&lt;/TargetFramework&gt;
        &lt;!-- uncomment to use Forms or WPF--&gt;
        &lt;!--&lt;UseWindowsForms&gt;true&lt;/UseWindowsForms&gt;
      &lt;UseWPF&gt;true&lt;/UseWPF&gt;--&gt;
        &lt;AssemblySearchPaths&gt;D:\ArxSdks\Arx2025\inc\;$(AssemblySearchPaths)&lt;/AssemblySearchPaths&gt;
    &lt;/PropertyGroup&gt;
    &lt;PropertyGroup Condition="'$(Configuration)' == 'ACAD2024'"&gt;
        &lt;TargetFramework&gt;net48&lt;/TargetFramework&gt;
        &lt;AssemblySearchPaths&gt;D:\ArxSdks\Arx2024\inc\;$(AssemblySearchPaths)&lt;/AssemblySearchPaths&gt;
    &lt;/PropertyGroup&gt;
    &lt;ItemGroup Condition="'$(Configuration)' == 'ACAD2025'"&gt;
        &lt;FrameworkReference Include="Microsoft.WindowsDesktop.App"/&gt;
    &lt;/ItemGroup&gt;
    &lt;ItemGroup&gt;
        &lt;Reference Include="AcDbMgd"&gt;
            &lt;Private&gt;False&lt;/Private&gt;
        &lt;/Reference&gt;
        &lt;Reference Include="acmgd"&gt;
            &lt;Private&gt;False&lt;/Private&gt;
        &lt;/Reference&gt;
        &lt;Reference Include="accoremgd"&gt;
            &lt;Private&gt;False&lt;/Private&gt;
        &lt;/Reference&gt;
    &lt;/ItemGroup&gt;
&lt;/Project&gt;
  </code>
  </pre>

  <p>Github Project : <a href="https://github.com/MadhukarMoogala/Ellipsejig">Ellipsejig</a></p>

  <h4>Note:</h4>
  <p>We encourage you to use Git branching to organize and maintain releases instead of organizing projects to target
    multiple frameworks. This is the same approach we adopt with the AutoCAD source code, which has billions of lines of
    code.</p>

  <h4>In the image below, our main branch is always the latest and newest state and content.</h4>
  <p><a href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3b8205f200b-pi"><img width="594" height="110" title="Github-ACAD-Flow" style="display: inline; background-image: none;" alt="Github-ACAD-Flow" src="/assets/image_781166.jpg" border="0"></a></p><p>When it's time to switch to a new release, we branch "main" into "release2025." After that, "main" will become
    "2026," and so on.</p>
  <p>So, if your main branch is currently supporting AutoCAD 2024, you would branch this off into "release2024" and then
    change "main" to support AutoCAD 2025.</p>

  <p>For example, please find the Github project : <a href="https://github.com/MadhukarMoogala/ManagedCircle">ManagedCircle</a>
  </p>
  <p>There are two branches:</p>
  <ul>
    <li>master - which is the current release 2025,</li>
    <li>another branch for AutoCAD 2024 (.NET F4x)</li>
  </ul>
  <p>If I have to roll out a fix to AutoCAD 2024, I will simply switch to that branch and work on it.</p>
