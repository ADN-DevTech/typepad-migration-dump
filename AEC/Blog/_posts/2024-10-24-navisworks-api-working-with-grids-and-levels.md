---
layout: "post"
title: "Navisworks API: Working with Grids And Levels"
date: "2024-10-24 11:44:27"
author: "Naveen Kumar"
categories:
  - ".NET"
  - "Naveen Kumar"
  - "Navisworks"
original_url: "https://adndevblog.typepad.com/aec/2024/10/navisworks-api-working-with-grids-and-levels.html "
typepad_basename: "navisworks-api-working-with-grids-and-levels"
typepad_status: "Publish"
---

<p>
<script src="https://cdn.jsdelivr.net/gh/google/code-prettify@master/loader/run_prettify.js?skin=sunburst"></script>
</p>
<p>By <a href="https://adndevblog.typepad.com/aec/Naveen-Kumar.html" target="_self">Naveen Kumar</a></p>
<p>In Navisworks, grids and levels are valuable tools for exploring a scene, providing spatial context for your location and the placement of objects within the environment.</p>
<p>You can access grids and levels through the "Grids &amp; Levels" panel in the View tab. Now, let’s utilize the Navisworks API to work with grids and levels.</p>
<p>In this blog post, we will explore the following topics:</p>
<ul>
<li>Turns grids on or off</li>
<li>Customizing grid line colors based on the camera’s position</li>
<li>Configuring the grid mode</li>
<li>Setting a specific display level when "Fixed" grid mode is selected</li>
</ul>
<p><strong>Turns grids on or off</strong></p>
<pre class="prettyprint">// Access the active document in Navisworks
   Document doc = Autodesk.Navisworks.Api.Application.ActiveDocument;
              
// Retrieve grid options from the Navisworks application
   GridsOptions gridOptions = Autodesk.Navisworks.Api.Application.Options.Grids;
                 
// Enable grid visibility (set to 'false' to hide the grid)
   gridOptions.Enabled = true;

// If X-ray mode is switched off, transparent gridlines hidden by objects will not be displayed.
   gridOptions.XRayMode = true;

</pre>
<p><strong>Customizing grid line colors based on the camera’s position</strong></p>
<pre class="prettyprint">// Customize grid line colors based on the camera's position
   gridOptions.AboveColor = new Color(1, 0, 0);  
   gridOptions.BelowColor = new Color(0, 1, 0);  
   gridOptions.OtherColor = new Color(1, 1, 1);      

</pre>
<p><strong>Configuring the grid mode</strong></p>
<pre class="prettyprint">// Access the Grids object from the active document
DocumentGrids docGrids = doc.Grids;

// Set the grid render mode to show both above and below grid lines
docGrids.RenderMode = GridsRenderMode.AboveAndBelow; 

</pre>
<p><strong>Setting a specific display level when "Fixed" grid mode is selected</strong></p>
<pre class="prettyprint">GridSystem gridSystem = docGrids.ActiveSystem;
// Set the grid render mode to "Fixed" and restrict grid visibility to a specific level 
// e.g., Level 3
// ensuring the grid remains fixed at that level during navigation.             
   GridLevelCollection gridLevelCollection = gridSystem.Levels;
   GridLevel requiredGridLevel = gridLevelCollection.Where(level => level.DisplayName=="Level 3").First();

// Set grid render mode to "Fixed" if Level 3 is found
   if (requiredGridLevel != null)
     { 
       docGrids.RenderMode = GridsRenderMode.Locked;
       docGrids.SetSystemLockedLevel(gridSystem, requiredGridLevel);
     }

</pre>
