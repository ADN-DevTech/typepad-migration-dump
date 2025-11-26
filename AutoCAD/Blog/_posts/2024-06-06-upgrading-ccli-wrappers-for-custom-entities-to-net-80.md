---
layout: "post"
title: "Upgrading C++/CLI Wrappers for Custom Entities to .NET 8.0"
date: "2024-06-06 20:50:00"
author: "Madhukar Moogala"
categories:
  - ".NET"
  - "AutoCAD"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2024/06/upgrading-ccli-wrappers-for-custom-entities-to-net-80.html "
typepad_basename: "upgrading-ccli-wrappers-for-custom-entities-to-net-80"
typepad_status: "Publish"
---

<p>
    <script src="https://cdn.jsdelivr.net/gh/google/code-prettify@master/loader/run_prettify.js?skin=sunburst">
    </script>
  </p>
  <p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p>
  <p>AutoCAD 2025 started supporting .NET 8.0 platform, if you have managed wrapper or interop API for custom entities,
    you need to upgrade them from NET 4.x to NET 8.0</p>
  <p>Upgrading managed wrappers is three fold process</p>
  <li>
    <ol>First, you need to upgrade arx or dbx project (*.vcxproj) to target new SDK version</ol>
    <ol>Second, you need to upgrade the wrapper project (C++\CLI) to NET 8.0</ol>
    <ol>Third, you need to upgrade the client side plugin project from NET 4x to NET 8.0</ol>
  </li>
  <p>Here is the step by step guide to upgrade the managed wrapper project from NET 4.x to NET 8.0</p>

  <ol>
    <li>
      <p><strong>Update the .vcxproj File</strong>:</p>
      <ul>
        <li>Replace <code>&lt;CLRSupport&gt;true&lt;/CLRSupport&gt;</code> properties with
          <code>&lt;CLRSupport&gt;NetCore&lt;/CLRSupport&gt;</code>.
        </li>
        <li>Replace <code>&lt;TargetFrameworkVersion&gt;</code> properties with
          <code>&lt;TargetFramework&gt;net8.0&lt;/TargetFramework&gt;</code>.
        </li>
      </ul>
    </li>
    <li>
      <p><strong>Remove .NET Framework References</strong>:</p>
      <ul>
        <li>Remove any references to <code>System</code>, <code>System.Core</code>,<code>System.Data</code>,
          <code>System.Windows.Forms</code>, and <code>System.Xml</code> from your project.
        </li>
      </ul>
    </li>
    <li>
      <p><strong>Update API Usage</strong>:</p>
      <ul>
        <li>Review your .cpp files and update any APIs that are unavailable in .NET.</li>
      </ul>
    </li>
    <li>
      <p><strong>WPF and Windows Forms Usage</strong>:</p>
      <ul>
        <li>C++/CLI projects can use Windows Forms and WPF APIs.</li>
        <li>Add explicit framework references to the UI libraries.</li>
      </ul>
      <p>To use Windows Forms APIs, add this reference to the <em>.vcxproj</em> file: </p>
      <pre><code class="lang-xml"><span class="hljs-comment">&lt;!-- Reference all of Windows Forms --&gt;</span>
    <span class="hljs-tag">&lt;<span class="hljs-name">FrameworkReference</span> <span class="hljs-attr">Include</span>=<span class="hljs-string">"Microsoft.WindowsDesktop.App.WindowsForms"</span> /&gt;</span>
    </code></pre>
      <p>To use WPF APIs, add this reference to the <em>.vcxproj</em> file:</p>
      <pre><code class="lang-xml"><span class="hljs-comment">&lt;!-- Reference all of WPF --&gt;</span>
    <span class="hljs-tag">&lt;<span class="hljs-name">FrameworkReference</span> <span class="hljs-attr">Include</span>=<span class="hljs-string">"Microsoft.WindowsDesktop.App.WPF"</span> /&gt;</span>
    </code></pre>
      <p>To use both Windows Forms and WPF APIs, add this reference to the <em>.vcxproj</em> file:</p>
      <pre><code class="lang-xml"><span class="hljs-comment">&lt;!-- Reference the entirety of the Windows desktop framework:     Windows Forms, WPF, and the types that provide integration between them --&gt;</span>
    <span class="hljs-tag">&lt;<span class="hljs-name">FrameworkReference</span> <span class="hljs-attr">Include</span>=<span class="hljs-string">"Microsoft.WindowsDesktop.App"</span> /&gt;</span>
    </code></pre>
    </li>
  </ol>
  <p>Remember that there are some limitations when migrating C++/CLI projects to .NET Core, such as not being able to
    compile to an executable directly and Windows-only support for C++/CLI. <a href="https://learn.microsoft.com/en-us/dotnet/core/porting/cpp-cli">Make sure to check the documentation for more
      details</a></p>
  <ol>
    <li>
      <p><strong>Upgrading a .NET 4.x Project to .NET 8.0</strong></p>
      <ol>
        <li>
          <p><strong>Open the Project File:</strong></p>
          <ul>
            <li>
              <p>In Visual Studio, navigate to Solution Explorer.</p>
            </li>
            <li>
              <p>Right-click on the <code>ManagedTest</code> project and select "Upgrade"</p>
              <p>Refer: How to use <a href="[Overview of the .NET Upgrade Assistant - .NET Core | Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/core/porting/upgrade-assistant-overview?WT.mc_id=dotnet-35129-website">upgrade
                  assistant</a>)</p>
            </li>
          </ul>
        </li>
        <li>
          <p><strong>Update the Project Configuration:</strong></p>
          <ul>
            <li>Locate the <code>&lt;TargetFramework&gt;</code> property and change its value to
              <code>net8.0-windows</code>. This specifies that the project targets the latest .NET 8.0 framework for
              Windows.
            </li>
            <li>Find the <code>&lt;Platforms&gt;</code> property and set it to <code>x64</code>. This indicates that the
              project is built for the 64-bit architecture.</li>
          </ul>
        </li>
        <li>
          <p><strong>Configure Assembly Search Paths:</strong></p>
          <ul>
            <li>Locate the <code>&lt;AssemblySearchPaths&gt;</code> property (it might not exist yet).</li>
            <li>Add the path to your ObjectARX SDK directory within the property's value. This ensures that .NET
              references from the SDK are found during compilation. Here's an example structure:</li>
          </ul>
          <p>XML</p>
          <pre>            <code>&lt;AssemblySearchPaths&gt;$(OutDir);$(ReferencePath);C:<span class="hljs-symbol">\P</span>ath<span class="hljs-symbol">\T</span>o<span class="hljs-symbol">\Y</span>our<span class="hljs-symbol">\O</span>bjectARX<span class="hljs-symbol">\S</span>DK&lt;/AssemblySearchPaths&gt;
            </code>
          </pre>
          <ul>
            <li>Replace <code>C:\Path\To\Your\ObjectARX\SDK</code> with the actual path to your ObjectARX SDK
              installation.</li>
          </ul>
        </li>
      </ol>
    </li>
    <li>
      <p><strong>Optional: Update Root Namespace (if necessary):</strong></p>
      <ul>
        <li>If your project uses a specific root namespace to organize its files, locate the
          <code>&lt;RootNamespace&gt;</code> property and ensure it matches your project directory name.
        </li>
      </ul>
    </li>
    <li>
      <p><strong>Save the Project File:</strong></p>
      <ul>
        <li>Save the changes made to the project file (<code>ManagedTest.csproj</code>).</li>
      </ul>
    </li>
  </ol>
  <p>Here is the sample project file for your reference</p>
  <pre class="prettyprint">    <span style="color: rgb(0, 128, 115);">&lt;?</span><span style="color: rgb(230, 97, 112); font-weight: bold;">xml</span><span style="color: rgb(0, 128, 115);"> </span><span style="color: rgb(0, 128, 115);">version</span><span style="color: rgb(210, 205, 134);">=</span><span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(0, 125, 69);">1.0</span><span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(0, 128, 115);"> </span><span style="color: rgb(0, 128, 115);">encoding</span><span style="color: rgb(210, 205, 134);">=</span><span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(0, 196, 196);">utf-8</span><span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(0, 128, 115);">?&gt;</span>
    <span style="color: rgb(255, 137, 6);">&lt;</span><span style="color: rgb(246, 193, 208);">Project</span> Sdk<span style="color: rgb(210, 205, 134);">=</span><span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(0, 196, 196);">Microsoft.NET.Sdk</span><span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(255, 137, 6);">&gt;</span>
    <span style="color: rgb(255, 137, 6);">&lt;</span><span style="color: rgb(246, 193, 208);">PropertyGroup</span><span style="color: rgb(255, 137, 6);">&gt;</span>
    <span style="color: rgb(255, 137, 6);">&lt;</span><span style="color: rgb(246, 193, 208);">OutputType</span><span style="color: rgb(255, 137, 6);">&gt;</span>Library<span style="color: rgb(251, 132, 0);">&lt;/</span><span style="color: rgb(246, 193, 208);">OutputType</span><span style="color: rgb(251, 132, 0);">&gt;</span>
    <span style="color: rgb(255, 137, 6);">&lt;</span><span style="color: rgb(246, 193, 208);">RootNamespace</span><span style="color: rgb(255, 137, 6);">&gt;</span>ManagedTest<span style="color: rgb(251, 132, 0);">&lt;/</span><span style="color: rgb(246, 193, 208);">RootNamespace</span><span style="color: rgb(251, 132, 0);">&gt;</span>
    <span style="color: rgb(255, 137, 6);">&lt;</span><span style="color: rgb(246, 193, 208);">TargetFramework</span><span style="color: rgb(255, 137, 6);">&gt;</span>net8.0-windows<span style="color: rgb(251, 132, 0);">&lt;/</span><span style="color: rgb(246, 193, 208);">TargetFramework</span><span style="color: rgb(251, 132, 0);">&gt;</span>
    <span style="color: rgb(255, 137, 6);">&lt;</span><span style="color: rgb(246, 193, 208);">GenerateAssemblyInfo</span><span style="color: rgb(255, 137, 6);">&gt;</span>false<span style="color: rgb(251, 132, 0);">&lt;/</span><span style="color: rgb(246, 193, 208);">GenerateAssemblyInfo</span><span style="color: rgb(251, 132, 0);">&gt;</span>
    <span style="color: rgb(255, 137, 6);">&lt;</span><span style="color: rgb(246, 193, 208);">ObjectARXPATH</span> Condition<span style="color: rgb(210, 205, 134);">=</span><span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(0, 196, 196);">'$(ObjectARXPATH)' == ''</span><span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(255, 137, 6);">&gt;</span>D:\ArxSdks\arx2025<span style="color: rgb(251, 132, 0);">&lt;/</span><span style="color: rgb(246, 193, 208);">ObjectARXPATH</span><span style="color: rgb(251, 132, 0);">&gt;</span>
    <span style="color: rgb(255, 137, 6);">&lt;</span><span style="color: rgb(246, 193, 208);">AssemblySearchPaths</span><span style="color: rgb(255, 137, 6);">&gt;</span>$(ObjectARXPATH)\inc\;$(AssemblySearchPaths)<span style="color: rgb(251, 132, 0);">&lt;/</span><span style="color: rgb(246, 193, 208);">AssemblySearchPaths</span><span style="color: rgb(251, 132, 0);">&gt;</span>
    <span style="color: rgb(255, 137, 6);">&lt;</span><span style="color: rgb(246, 193, 208);">Platforms</span><span style="color: rgb(255, 137, 6);">&gt;</span>x64<span style="color: rgb(251, 132, 0);">&lt;/</span><span style="color: rgb(246, 193, 208);">Platforms</span><span style="color: rgb(251, 132, 0);">&gt;</span>
    <span style="color: rgb(251, 132, 0);">&lt;/</span><span style="color: rgb(246, 193, 208);">PropertyGroup</span><span style="color: rgb(251, 132, 0);">&gt;</span>
    <span style="color: rgb(255, 137, 6);">&lt;</span><span style="color: rgb(246, 193, 208);">ItemGroup</span><span style="color: rgb(255, 137, 6);">&gt;</span>
    <span style="color: rgb(255, 137, 6);">&lt;</span><span style="color: rgb(246, 193, 208);">FrameworkReference</span> Include<span style="color: rgb(210, 205, 134);">=</span><span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(0, 196, 196);">Microsoft.WindowsDesktop.App</span><span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(255, 137, 6);">/&gt;</span>
    <span style="color: rgb(251, 132, 0);">&lt;/</span><span style="color: rgb(246, 193, 208);">ItemGroup</span><span style="color: rgb(251, 132, 0);">&gt;</span>
    <span style="color: rgb(255, 137, 6);">&lt;</span><span style="color: rgb(246, 193, 208);">ItemGroup</span><span style="color: rgb(255, 137, 6);">&gt;</span>
    <span style="color: rgb(255, 137, 6);">&lt;</span><span style="color: rgb(246, 193, 208);">ProjectReference</span> Include<span style="color: rgb(210, 205, 134);">=</span><span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(0, 196, 196);">..\ManagedWrapper\ManagedWrapper.vcxproj</span><span style="color: rgb(2, 208, 69);">"</span> <span style="color: rgb(255, 137, 6);">/&gt;</span>
    <span style="color: rgb(251, 132, 0);">&lt;/</span><span style="color: rgb(246, 193, 208);">ItemGroup</span><span style="color: rgb(251, 132, 0);">&gt;</span>
    <span style="color: rgb(255, 137, 6);">&lt;</span><span style="color: rgb(246, 193, 208);">ItemGroup</span><span style="color: rgb(255, 137, 6);">&gt;</span>
    <span style="color: rgb(255, 137, 6);">&lt;</span><span style="color: rgb(246, 193, 208);">Reference</span> Include<span style="color: rgb(210, 205, 134);">=</span><span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(0, 196, 196);">AcDbMgd</span><span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(255, 137, 6);">&gt;</span>
    <span style="color: rgb(255, 137, 6);">&lt;</span><span style="color: rgb(246, 193, 208);">Private</span><span style="color: rgb(255, 137, 6);">&gt;</span>False<span style="color: rgb(251, 132, 0);">&lt;/</span><span style="color: rgb(246, 193, 208);">Private</span><span style="color: rgb(251, 132, 0);">&gt;</span>
    <span style="color: rgb(251, 132, 0);">&lt;/</span><span style="color: rgb(246, 193, 208);">Reference</span><span style="color: rgb(251, 132, 0);">&gt;</span>
    <span style="color: rgb(255, 137, 6);">&lt;</span><span style="color: rgb(246, 193, 208);">Reference</span> Include<span style="color: rgb(210, 205, 134);">=</span><span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(0, 196, 196);">AcMgd</span><span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(255, 137, 6);">&gt;</span>
    <span style="color: rgb(255, 137, 6);">&lt;</span><span style="color: rgb(246, 193, 208);">Private</span><span style="color: rgb(255, 137, 6);">&gt;</span>False<span style="color: rgb(251, 132, 0);">&lt;/</span><span style="color: rgb(246, 193, 208);">Private</span><span style="color: rgb(251, 132, 0);">&gt;</span>
    <span style="color: rgb(251, 132, 0);">&lt;/</span><span style="color: rgb(246, 193, 208);">Reference</span><span style="color: rgb(251, 132, 0);">&gt;</span>
    <span style="color: rgb(255, 137, 6);">&lt;</span><span style="color: rgb(246, 193, 208);">Reference</span> Include<span style="color: rgb(210, 205, 134);">=</span><span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(0, 196, 196);">AcCoreMgd</span><span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(255, 137, 6);">&gt;</span>
    <span style="color: rgb(255, 137, 6);">&lt;</span><span style="color: rgb(246, 193, 208);">Private</span><span style="color: rgb(255, 137, 6);">&gt;</span>False<span style="color: rgb(251, 132, 0);">&lt;/</span><span style="color: rgb(246, 193, 208);">Private</span><span style="color: rgb(251, 132, 0);">&gt;</span>
    <span style="color: rgb(251, 132, 0);">&lt;/</span><span style="color: rgb(246, 193, 208);">Reference</span><span style="color: rgb(251, 132, 0);">&gt;</span>
    <span style="color: rgb(251, 132, 0);">&lt;/</span><span style="color: rgb(246, 193, 208);">ItemGroup</span><span style="color: rgb(251, 132, 0);">&gt;</span>
    <span style="color: rgb(251, 132, 0);">&lt;/</span><span style="color: rgb(246, 193, 208);">Project</span><span style="color: rgb(251, 132, 0);">&gt;</span>   
  </pre>
<p>The Github project is available at <a href="https://github.com/MadhukarMoogala/ManagedCircle">ManagedCircle</a></p>
