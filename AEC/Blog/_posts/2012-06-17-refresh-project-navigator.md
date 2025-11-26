---
layout: "post"
title: "Refresh project navigator"
date: "2012-06-17 20:39:43"
author: "Mikako Harada"
categories:
  - ".NET"
  - "AutoCAD Architecture"
  - "Mikako Harada"
original_url: "https://adndevblog.typepad.com/aec/2012/06/refresh-project-navigator.html "
typepad_basename: "refresh-project-navigator"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/mikako-harada.html" target="_self" title="Mikako Harada">Mikako Harada</a></p>
<p><strong>Issue</strong></p>
<p>Using&#0160;AutoCAD Architecture (ACA)&#0160;.NET API, is there any way through the code to refresh the project navigator so that it will show the latest folder/files after I make a new drawing in my program? Alternatively, but less desirable, is there a function that will close then re-open the project navigator? This will seem to also have the side effect of refreshing the data.</p>
<p><strong>Solution </strong></p>
<p>ACA .NET Project API is not exposed to the level that we wish to have. Current usage is largely at the file system level, and&#0160;API functionality which related UI is very limited. A workaround is to call a command _AecRefreshProject from your program.&#0160;</p>
<p>As an FYI, below is a summary of currently exposed API regarding Project API:</p>
<p><span style="text-decoration: underline;">What you can do with Project API</span></p>
<ul>
<li>Able to create and work with projects</li>
<li>All support is at the file system level</li>
<li>No UI interaction and you must be careful to initiate API calls appropriately because the UI may not interact well with your API calls.</li>
</ul>
<p><span style="text-decoration: underline;">Project API&#39;s</span></p>
<ul>
<li>Autodesk.Aec.Project – The main project class.</li>
<li>Autodesk.Aec.ProjectBaseManager – The system wide Project Manager. This contains many utility methods to manipulate projects.</li>
<li>Autodesk.Aec.ProjectBaseServices – A helper class used to obtain the single ProjectBaseManager object.</li>
<li>Autodesk.Aec.ProjectConfiguration – Contains information about the project (such as the name, description, and number).</li>
</ul>
<p><span style="text-decoration: underline;">UI Interaction from API requires the use of commands</span></p>
<ul>
<li>_AecProjectNavigator – This a documented command that will display the project navigator palette. If there is no &quot;current&quot; project, the command will also run the Project Browser dialog.</li>
<li>_AecRefreshProject – This refreshes the palette. Note that the API has a different method to reload a project, but is different than this command. This command does the same as the refresh project button on the palette.</li>
<li>_AecSetCurrentProject – This is an important command for the API user. This is currently the only way to properly set a project as current from the API. </li>
<li>_AecCloseProjectNavigator – Closes the project navigator palette. Note it only closes the dialog and the same project is still open and current.</li>
<li>_AecProjectNavigatorToggle – Simply toggles the project navigator palette on or off.</li>
</ul>
<p>&#0160;</p>
