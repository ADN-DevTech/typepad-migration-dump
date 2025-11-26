---
layout: "post"
title: "AutoCAD 2025: Update Your PackageContents.xml with RuntimeRequirements"
date: "2024-06-11 18:23:00"
author: "Madhukar Moogala"
categories:
  - ".NET"
  - "AutoCAD"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2024/06/autocad-2025-update-your-packagecontentsxml-with-runtimerequirements.html "
typepad_basename: "autocad-2025-update-your-packagecontentsxml-with-runtimerequirements"
typepad_status: "Publish"
---

<p>
    <script src="https://cdn.jsdelivr.net/gh/google/code-prettify@master/loader/run_prettify.js?skin=sunburst"></script>
  </p>
  <p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p>
  <p>If you are using <a href="https://help.autodesk.com/view/OARX/2025/ENU/?guid=GUID-5E50A846-C80B-4FFD-8DD3-C20B22098008">PlugIn-Autoloader</a>
    mechanism to install custom applications, this important update is for you
  </p>
  <p>With the release of AutoCAD 2025, Autoloader engine checks for <code>RuntimeRequirements</code> for applications.
    In other words, you need to specify the series bounds for your application if the <code>ComponentEntry</code> \
    <code>AppType</code> is
    <code>.NET</code> to load in AutoCAD 2025.
  </p>
  <p><code>RuntimeRequirements</code> has two attributes which defines supportablity of your application component on
    AutoCAD
  </p>
  <p>The following description holds true for any other application but not for .NET plugins.</p>
  <table class="table">
    <caption style="padding: 10px; caption-side: bottom;">RuntimeRequirements Attributes</caption>
    <thead class="thead-dark">
      <tr>
        <th scope="col">Attribute</th>
        <th scope="col">Description</th>
      </tr>
    </thead>
    <tbody>
      <tr>
        <th scope="row">SeriesMin</th>
        <td>Defines the minimum product release number that the set of components supports.The value can be
          a major
          version number (R24) or a specific version (R24.1). The version number can be found in the Windows Registry or
          obtained with the <code>ACADVER</code> system variable in AutoCAD-based products.If this attribute and
          SeriesMaxare not
          specified, it is assumed all components are compatible with all product releases. If you omit this value, any
          version before that specified by the SeriesMaxattribute is allowed.
        </td>
      </tr>
      <tr>
        <th scope="row">SeriesMax</th>
        <td>Defines the maximum product release number that the set of components supports.If you omit this
          value, any
          version after that specified by the SeriesMin attribute is allowed.
        </td>
      </tr>
    </tbody>
  </table>
  <h3>What Changed?</h3>
  <p>If the <code>SeriesMax</code> is not set for a particular component in the bundle PackageContents.xml (via the
    RuntimeRequirements element) the Autoloader assumes it is a compatible component and loads it. However, since the
    API breaks because of the .NET upgrade, the app may crash while loading.
  </p>
  <p>
    So, it is important to specify the <code>SeriesMax</code> attribute in the <code>RuntimeRequirements</code>
    element.
  </p>
  <p>Here is the sample code <code>PackageContents.xml</code> to specify the series bounds for your .NET based
    applications targeting AutoCAD 2025
  </p>
  <h4>EDIT:</h4>
  <p>Thanks to <a href="https://forums.autodesk.com/t5/net/troubleshooting-packagecontents-xml-in-autocad-2025/m-p/12873585/highlight/true#M82787">Nedeljko
      Sovljanski</a> it was plain oversight to place RuntimeRequirements at incorrect scope.</p>
  <pre class="prettyprint">      <code class="language-xml">
    &lt;?xml version="1.0" encoding="UTF-8"?&gt;
    &lt;ApplicationPackage SchemaVersion="1.0" Name="HelloWorld" 
    AppVersion="2024.1.0" Description="A basic HelloWorld application"
     Author="Madhukar Moogala" ProductCode="90bcc406-78d2-4667-bef8-12c02e1ab25b"&gt;
       &lt;CompanyDetails Name="APS" Url="" Email="" /&gt;
       &lt;Components&gt;
          &lt;ComponentEntry AppName="HelloWorld" 
          ModuleName="HelloWorld.dll" 
          AppDescription="A simple hello" 
          AppType=".Net" 
          LoadOnAutoCADStartup="True"&gt;
             &lt;RuntimeRequirements OS="Win64" SeriesMin="R20.0" SeriesMax="R24.3" /&gt;
             &lt;Commands GroupName="HelloMGD"&gt;
                &lt;Command Local="HELLO" Global="HELLO" /&gt;
             &lt;/Commands&gt;
          &lt;/ComponentEntry&gt;
          &lt;ComponentEntry AppName="HelloWorld" 
          ModuleName="HelloWorld_NET8.0.dll" 
          AppDescription="A simple hello" 
          AppType=".Net" 
          LoadOnAutoCADStartup="True"&gt;
             &lt;RuntimeRequirements OS="Win64" SeriesMin="R25.0" SeriesMax="R25.0" /&gt;
             &lt;Commands GroupName="HelloMGD"&gt;
                &lt;Command Local="HELLO" Global="HELLO" /&gt;
             &lt;/Commands&gt;
          &lt;/ComponentEntry&gt;
       &lt;/Components&gt;
    &lt;/ApplicationPackage&gt;    
    </code>
    </pre>
