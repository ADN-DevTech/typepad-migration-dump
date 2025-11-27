---
layout: "post"
title: "Missing Autodesk Inventor Templates in Visual Studio?"
date: "2025-07-30 04:26:56"
author: "Chandra Shekar Gopal"
categories:
  - "Chandra Shekar Gopal"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2025/07/missing-autodesk-inventor-templates-in-visual-studio.html "
typepad_basename: "missing-autodesk-inventor-templates-in-visual-studio"
typepad_status: "Publish"
---

<p>by&#0160;<a href="https://adndevblog.typepad.com/manufacturing/chandra-shekar-gopal.html">Chandra shekar Gopal</a>,&#0160;&#0160;</p>
<p>If you’ve recently installed <strong>Autodesk Inventor DeveloperTools</strong> but can&#39;t find the expected Inventor Add-In project templates in Visual Studio, you&#39;re not alone. This guide explains why the templates may not appear and how to ensure they’re properly installed and visible—whether you’re working with C++, C#, or VB.NET.</p>
<h3>📦 Inventor Wizards Are Now Part of DeveloperTools</h3>
<p>Since Inventor 2013, the Inventor Wizards are no longer distributed as a separate installer. Instead, they are bundled within the <strong>DeveloperTools</strong> component. Once installed, the wizards are copied to your Visual Studio templates directory (%USERPROFILE%\Documents\Visual Studio 2022\Templates\ProjectTemplates).<br /><br />By default, <strong>DeveloperTools.msi</strong> is available at <strong>C:\Users\Public\Documents\Autodesk\Inventor &lt;version&gt;\SDK</strong> after installation of every Inventor version.</p>
<p><strong>⚠️ Important:</strong> After installing DeveloperTools, all Visual Studio instances must be <strong>closed</strong> before the templates can be recognized.</p>
<h3>📁 Where Are Templates Installed?</h3>
<p>By default, Visual Studio looks for user-defined templates in the following location (if not changed during installation):</p>
<pre><code>%USERPROFILE%\Documents\Visual Studio 2022\Templates\ProjectTemplates</code></pre>
<p>For example, project templates might be located here:</p>
<pre><code>C:\Users\<em>&lt;UserName&gt;</em>\Documents\Visual Studio 2022\Templates\ProjectTemplates</code></pre>
<p>However, if Visual Studio is installed in a custom directory or you’ve changed the template locations under <strong>Tools &gt; Options &gt; Projects and Solutions &gt; Locations</strong>, the wizards may not appear when creating a new project.</p>
<h3>🛠️ How to Make Inventor Templates Visible in Visual Studio</h3>
<ol>
<li>Install <strong>DeveloperTools</strong> from the Inventor SDK.</li>
<li>Ensure all Visual Studio instances are closed after installation.</li>
<li>Check your Visual Studio template location under:<br /><em>Tools &gt; Options &gt; Projects and Solutions &gt; Locations&#0160;<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3d9540a200c-pi" style="display: inline;"><img alt="VS locations" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3d9540a200c image-full img-responsive" src="/assets/image_669579.jpg" title="VS locations" /></a><br /></em></li>
<li>Copy the Inventor wizards/templates from:<br /><code>%USERPROFILE%\Documents\Visual Studio 2022\Templates\ProjectTemplates</code><br />to your custom project templates location (if you&#39;ve changed it).</li>
<li>(Optional) Restart your system for changes to take full effect.</li>
<li>Launch Visual Studio, go to <strong>File &gt; New &gt; Project</strong>, and search for:
<ul>
<li>Inventor AddIn (C#)</li>
<li>Inventor AddIn (VB.NET)</li>
<li>Inventor AddIn (Visual C++)</li>
</ul>
</li>
</ol>
<h3>🧩 Visual Studio and Inventor Wizards by Language</h3>
<h3>🧱 Visual C++</h3>
<p><strong>&#0160;</strong><br />This wizard helps generate Inventor Add-In applications in C++. It creates the <code>AddInServer.cpp/h</code> files which implement the <code>ApplicationAddInServer</code> interface—vital for COM communication with Inventor.</p>
<p><strong>💡 Tip:</strong> If you see a “Platform &#39;x64&#39; was not found” error, install the x64 Compilers and Tools via:<br /><em>Add or Remove Programs &gt; Modify Visual Studio &gt; Individual Components &gt; Visual C++ x64 tools</em></p>
<p>The <em>Inventor Event Sink ATL Class Wizard</em> helps you create new classes to handle Inventor events. These are accessible via <strong>Add &gt; Class</strong> in the project context menu.</p>
<h3>💻 Visual C# and Visual Basic.NET</h3>
<p><strong>&#0160;</strong><br />For both C# and VB.NET, the wizard creates a new Add-In project with the <code>StandardAddInServer.cs</code> or <code>StandardAddInServer.vb</code> file. These classes:</p>
<ul>
<li>Implement the <code>ApplicationAddInServer</code> interface.</li>
<li>Manage COM registration (via static methods).</li>
<li>Define the required Inventor registry settings.</li>
</ul>
<h3>🔐 Why Templates May Still Not Load Automatically</h3>
<p>The Inventor Add-In templates depend on Visual Studio recognizing the default paths. If templates are still missing after installation, you can post a query using below link.</p>
<p>🔗 <a href="https://forums.autodesk.com/t5/inventor-programming-ilogic/bd-p/inventor-programming-ilogic-forum-en" rel="noopener" target="_blank"> Inventor Programming - iLogic, Macros, AddIns &amp; Apprentice Forum </a></p>
<h3>✅ Final Tips</h3>
<ul>
<li>Always restart Visual Studio after changing template locations.</li>
<li>Confirm that DeveloperTools has been installed for the correct Inventor version.</li>
<li>If needed, manually copy templates to the correct path to make them visible.</li>
</ul>
<p>By following these steps, you should have full access to the Autodesk Inventor Add-In templates in Visual Studio and be ready to start developing your next automation or customization project with ease.</p>
