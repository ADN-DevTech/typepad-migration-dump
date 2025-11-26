---
layout: "post"
title: "Unit Testing AutoCAD with NUnit + AcCoreConsole"
date: "2025-06-24 16:18:00"
author: "Madhukar Moogala"
categories:
  - ".NET"
  - "AutoCAD"
original_url: "https://adndevblog.typepad.com/autocad/2025/06/unit-testing-autocad-with-nunit-accoreconsole.html "
typepad_basename: "unit-testing-autocad-with-nunit-accoreconsole"
typepad_status: "Publish"
---

<p>     <script src="https://cdn.jsdelivr.net/gh/google/code-prettify@master/loader/run_prettify.js?skin=sunburst"></script>   </p>   <p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p>    <h2>Why Unit Test AutoCAD Code?</h2>   <p>     If you’re developing on the <strong>AutoCAD .NET API (AcDb managed layer)</strong>—handling things like blocks,     entities, transactions, dynamic properties, or geometry—you probably want:   </p>   <ul>     <li><strong>Inspires confidence</strong> that your logic works across DWG files</li>     <li><strong>Repeatable tests</strong> that run in isolation</li>     <li><strong>Fast feedback</strong> without relying on GUI interaction</li>   </ul>   <p>That’s exactly what this project enables.</p>   <blockquote>This is not for UI-level testing (dialogs, palettes, CUI). It's for AcDb-level logic.</blockquote>    <h2>What This Project Enables</h2>   <p>This project shows how to:</p>   <ul>     <li> Run <strong>unit tests on DWG files</strong> via <code>accoreconsole.exe</code></li>     <li> Use <strong>NUnit/NUnitLite</strong> to test AutoCAD logic like circles, lines, blocks, and more</li>     <li> Generate HTML reports with <strong>ExtentReports</strong></li>     <li> Run tests in:       <ul>         <li>AutoCAD GUI (on active or selected DWG)</li>         <li>AcCoreConsole (headless)</li>         <li>Side databases (for isolated testing)</li>       </ul>     </li>   </ul>    <h2>How It Works</h2>   <p>     Tests inherit from a base class <code>DrawingTestBase</code>, which handles:   </p>   <ul>     <li>Opening a DWG (via command-line args or prompt)</li>     <li>Creating transactions</li>     <li>Managing AutoCAD <code>Database</code> instances</li>     <li>Detecting whether running inside <code>accoreconsole.exe</code> or full GUI</li>   </ul>   <p>You trigger tests using the <code>RunCADtests</code> command, either:</p>    <h3> In AcCoreConsole:</h3>   <pre class="prettyprint lang-csharp"><code>accoreconsole.exe /i sample.dwg /s TestRun.scr</code></pre>

  <h3> In AutoCAD GUI:</h3>
  <pre class="prettyprint lang-csharp"><code>NETLOAD your test DLL
Command: RunCADtests
→ Optionally select a drawing if one isn't open
→ Tests run and output an HTML report</code></pre>

  <h2>Sample Test</h2>
  <p>Here’s a basic test for verifying a circle’s radius:</p>
  <pre class="prettyprint lang-csharp">
    <code>[TestFixture, Apartment(ApartmentState.STA)]
public class CircleTests : DrawingTestBase
{
    [Test]
    public void CircleRadiusTest()
    {
        var circle = GetFirstCircle();
        Assert.That(circle.Radius, Is.EqualTo(50));
    }
}</code></pre>

  <p>You can write similar tests for:</p>
  <ul>
    <li>Blocks (dynamic/static)</li>
    <li>Line geometry</li>
    <li>Property evaluation</li>
    <li>Transaction behavior</li>
  </ul>

  <h2>Project Structure</h2>
  <ul>
    <li><code>/Tests</code>: Place test classes here (<code>CircleTests.cs</code>, <code>LineTests.cs</code>, etc.)</li>
    <li><code>/TestInfrastructure</code>: Contains base classes and utilities</li>
    <li><code>/TestRun.scr</code>: AutoCAD script to load and invoke tests</li>
    <li><code>RunTest.bat</code>: Launches <code>accoreconsole</code> with a selected DWG and script</li>
  </ul>

  <h2>Getting Started</h2>
  <pre class="prettyprint lang-bash"><code>git clone https://github.com/MadhukarMoogala/coreconsolerunner.git
cd coreconsolerunner
msbuild /t:build coreconsolerunner.sln</code></pre>

  <p>Modify the script/batch to point to your DWG and test assembly. Run using <code>accoreconsole</code> or AutoCAD
    GUI.</p>

  <h2>Inspired By</h2>
  <p>Special thanks to:</p>
  <ul>
    <li><a href="https://github.com/CADbloke/CADtest" target="_blank" rel="noopener">CADbloke/CADtest</a></li>
    <li><a href="https://civilwhiz.com/docs/autocad-civil-3d-plugin-unit-test-with-nunit/" target="_blank" rel="noopener">AutoCAD/Civil
        3D plugin unit test with NUnit – Civil WHIZ</a></li>
  </ul>

  <h2>Footnote</h2>
  <p>
    This approach makes it easier to bring modern testing practices to AutoCAD plugin development—
    <strong>especially AcDb logic</strong>. If you're building custom entities, automations, or design validations, give
    this a try.
  </p>
