---
layout: "post"
title: "Exploring Fusion Design Model Hierarchy Using the Manufacturing Data Model API (Not Fusion Software)"
date: "2025-07-16 10:30:11"
author: "Chandra Shekar Gopal"
categories:
  - "Chandra Shekar Gopal"
  - "Cloud"
  - "Fusion 360"
original_url: "https://adndevblog.typepad.com/manufacturing/2025/07/exploring-fusion-design-model-hierarchy-using-the-manufacturing-data-model-api-not-fusion-software.html "
typepad_basename: "exploring-fusion-design-model-hierarchy-using-the-manufacturing-data-model-api-not-fusion-software"
typepad_status: "Publish"
---

<p>by&#0160;<a href="https://adndevblog.typepad.com/manufacturing/chandra-shekar-gopal.html">Chandra shekar Gopal</a>,&#0160;&#0160;</p>
<h3>Why Explore Model Hierarchies?</h3>
<p>When working with <strong>Fusion designs</strong>, understanding the complete structure of your assemblies, components, and subcomponents is crucial for effective manufacturing data analysis, automation, and downstream workflows.</p>
<blockquote>
<p>⚡ <strong>Important:</strong> This workflow retrieves your <strong>Fusion design’s Model Hierarchy directly from the cloud using the Manufacturing Data Model API, not by opening Fusion software on your machine.</strong> This ensures headless, scalable data extraction for your server, web, or pipeline integrations.</p>
</blockquote>
<h3>What this Sample Demonstrates</h3>
<p>Using this sample application, you will:</p>
<ul>
<li>✅ <strong>Authenticate using Autodesk APS (formerly Forge) OAuth 3.0</strong></li>
<li>✅ <strong>Retrieve your design’s model hierarchy securely via the Manufacturing Data Model API</strong></li>
<li>✅ <strong>Visualize assemblies, subassemblies, and references recursively without launching Fusion locally</strong></li>
<li>✅ <strong>Gain deep insights into your Fusion design structure for analysis and automation</strong></li>
</ul>
<h3>📂 Access the Source Code</h3>
<p>You can find the <strong>full source code for this project on GitHub</strong>:</p>
<p>➡️ <a href="https://github.com/chandraRus/Retrieving-Model-Heirarchy-of-Fusion-Design-using-Manufacturing-Data-Model-API" rel="noopener" target="_blank">Retrieving Model Hierarchy of Fusion Design using Manufacturing Data Model API (GitHub)</a></p>
<h3>🛠️ Setting Up Your Environment</h3>
<h3>1️⃣ Install Dependencies</h3>
<p>Run in your <strong>Package Manager Console</strong>:</p>
<pre><code>Install-Package Microsoft.AspNetCore.Authentication.Cookies
Install-Package Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation
Install-Package Microsoft.AspNetCore.Session
Install-Package Microsoft.Extensions.Http
Install-Package System.Text.Json
Install-Package GraphQL.Client
Install-Package GraphQL.Client.Serializer.SystemTextJson</code></pre>
<h3>2️⃣ Create and Configure Your APS App</h3>
<ul>
<li>Create a new app on the <a href="https://aps.autodesk.com" rel="noopener" target="_blank">APS Portal</a>.</li>
<li>Retrieve your <strong>ClientId</strong> and <strong>ClientSecret</strong>.</li>
<li>Set the <strong>Callback URL</strong> to:
<pre>http://localhost:3000/callback/oauth</pre>
⚠️ Ensure the callback URL in your app and code match exactly.</li>
</ul>
<p>Example:<br /><br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3d86129200c-pi" style="display: inline;"><img alt="Credentials" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3d86129200c image-full img-responsive" src="/assets/image_866ec9.jpg" title="Credentials" /></a><br /><br /></p>
<h3>3️⃣ Add OAuth Credentials</h3>
<p>Add to your <code>appsettings.json</code>:</p>
<pre><code>{
  &quot;OAuth&quot;: {
    &quot;ClientId&quot;: &quot;your-client-id&quot;,
    &quot;ClientSecret&quot;: &quot;your-client-secret&quot;,
    &quot;RedirectUri&quot;: &quot;http://localhost:3000/callback/oauth&quot;,
    &quot;TokenUrl&quot;: &quot;https://developer.api.autodesk.com/authentication/v2/token&quot;,
    &quot;Scopes&quot;: &quot;data:read data:write data:create&quot;
  }
}</code></pre>
<h3>4️⃣ Run the Application</h3>
<p>Start your server:</p>
<pre><code>dotnet run</code></pre>
<p>Navigate to:</p>
<pre>http://localhost:3000</pre>
<p>Log in using your Autodesk account to authorize the app.</p>
<h3>🔍 Understanding the Workflow</h3>
<p>This app uses a clean workflow to retrieve and explore your design data directly from the cloud:</p>
<ol>
<li>Authenticate securely with OAuth 3.0</li>
<li>Navigate your Team Hub and projects</li>
<li>Select Fusion Design Items or Basic Items (Inventor, Revit, Navisworks)</li>
<li>View the hierarchy and structure of selected files</li>
<li>Recursively retrieve full model hierarchies via the Manufacturing Data Model API (not Fusion software)</li>
</ol>
<h3>🚩 What is Displayed?</h3>
<p>✅ <strong>DesignItem:</strong> Fusion design files (e.g., Utility Knife, Sheet Metal Example, Tutorial1)<br />✅ <strong>BasicItem:</strong> Other Autodesk products (Inventor, Revit, Navisworks, DWF)</p>
<h3>🔗 The GraphQL Query</h3>
<p>The app uses the following <strong>GraphQL query via the Manufacturing Data Model API</strong> to retrieve your model hierarchy:</p>
<pre><code>query GetModelHierarchy($hubName: String!, $projectName: String!, $componentName: String!) {
  hubs(filter: { name: $hubName }) {
    results {
      name
      projects(filter: { name: $projectName }) {
        results {
          name
          items(filter: { name: $componentName }) {
            results {
              ... on DesignItem {
                name
                tipRootComponentVersion {
                  id
                  name
                  allOccurrences {
                    results {
                      parentComponentVersion { id }
                      componentVersion { id name }
                    }
                    pagination { cursor }
                  }
                }
              }
            }
          }
        }
      }
    }
  }
}</code></pre>
<p>✅ Retrieves the <strong>root component and references</strong><br />✅ Recursively gathers <strong>child components and structure</strong><br />✅ <strong>Does not require Fusion to run locally</strong>, enabling scalable, automated data retrieval</p>
<h3>📝 Conclusion</h3>
<p>Using this <strong>Manufacturing Data Model API sample</strong>, you can:</p>
<ul>
<li>✅ Authenticate securely via APS OAuth</li>
<li>✅ Retrieve your model hierarchy efficiently without Fusion installed</li>
<li>✅ Visualize your design structure for analysis and automation</li>
</ul>
<p>Once configured, you can seamlessly explore your Fusion design hierarchy, unlocking data-driven workflows for quoting, BOM generation, and automated documentation.</p>
<h3>📚 Learn More</h3>
<ul>
<li><a href="https://aps.autodesk.com/en/docs/mfgdataapi/v2/developers_guide/overview/" rel="noopener" target="_blank">APS Manufacturing Data Model API Documentation</a></li>
</ul>
