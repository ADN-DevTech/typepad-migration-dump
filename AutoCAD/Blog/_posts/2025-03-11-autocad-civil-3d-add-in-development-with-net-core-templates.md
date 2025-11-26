---
layout: "post"
title: "AutoCAD &amp; Civil 3D Add-in Development with .NET Core Templates"
date: "2025-03-11 21:33:00"
author: "Madhukar Moogala"
categories:
  - ".NET"
  - "AutoCAD"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2025/03/autocad-civil-3d-add-in-development-with-net-core-templates.html "
typepad_basename: "autocad-civil-3d-add-in-development-with-net-core-templates"
typepad_status: "Publish"
---

<p>     <script src="https://cdn.jsdelivr.net/gh/google/code-prettify@master/loader/run_prettify.js?skin=sunburst"></script>   </p>   <p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p>    <p>     Developing AutoCAD and Civil 3D add-ins requires setting up boilerplate code, managing dependencies, and ensuring     compatibility with the latest .NET versions. To simplify this process, we created a .NET Core-based project template     that allows developers to quickly scaffold AutoCAD and Civil 3D add-ins using a single CLI instruction.   </p>   <h3>How to Use the Template</h3>   <pre class="prettyprint">
    <code>
      set "AcadDir=C:\Program Files\Autodesk\AutoCAD 2025\"
      dotnet new civil --rootNamespace=CivilCmd -o TestCivilProject
      cd TestCivilProject
      dotnet restore
      dotnet build
    </code>
  </pre>
  <p>This command generates a Civil 3D add-in project with the specified root namespace and output directory.</p>

  <h3>
    What’s Inside the Template?
  </h3>
  <ol>
    <li>
      A pre-configured .csproj file targeting .NET Core/.NET 8.0 for AutoCAD 2025+ compatibility.
    </li>
    <li>
      An implementation of IExtensionApplication for AutoCAD commands.
    </li>
    <li>
      A sample command class with a simple Civil 3D API example.
    </li>
  </ol>
  <h3>Extending the Template</h3>
  <p>
    The template is designed to be easily extensible. You can add new commands, implement new interfaces, and reference
    external libraries as needed. The template also includes a sample command class that demonstrates how to interact
    with the Civil 3D API.
  </p>

  <ol>
    <li>Additional dependencies (e.g., Newtonsoft.Json, AutoCAD NuGet packages).</li>
    <li>Unit tests using xUnit or NUnit.</li>
    <li>Logging mechanisms like Serilog.</li>
  </ol>
  <a href="https://github.com/MadhukarMoogala/CivilClassLibTemplate" target="_blank" style="display: inline-flex; align-items: center; background-color: #24292e; color: white; padding: 10px 15px; border-radius: 5px; text-decoration: none; font-weight: bold;">
    <img src="/assets/GitHub-Mark.png" alt="GitHub" width="20" height="20" style="margin-right: 8px;">
    View on GitHub
  </a>
