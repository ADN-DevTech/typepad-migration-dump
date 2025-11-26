---
layout: "post"
title: "Manipulate Simulation Appearance and Task Type"
date: "2015-09-29 19:21:56"
author: "Xiaodong Liang"
categories:
  - ".NET"
  - "Navisworks"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/aec/2015/09/manipulate-simulation-appearance-and-task-type.html "
typepad_basename: "manipulate-simulation-appearance-and-task-type"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>From help of product:&#0160;</p>
<p>The task type defines how the items attached to the task will be displayed during simulation. For example, a default construction sequence would start with all items hidden, as the task starts the attached items will be displayed in transparent green, then as the task ends the attached items will be displayed as they are in the normal model display. Task types themselves can be defined and new types created on the&#0160;<a href="http://help.autodesk.com/cloudhelp/2016/ENU/Navisworks/files/GUID-102BBA3C-0C3B-46E7-AE9D-238794E0123B.htm">Configure Tab</a>.</p>
<p id="GUID-A52E3947-462A-48DA-900C-66CB3F312DA7__WS73099CC142F48755213995EE11EDA28F7D6-7529">Each task has a task type associated with it, which specifies how the items attached to the task are treated (and displayed) at the start and end of the task during simulation. The available options are:</p>
<ul id="GUID-A52E3947-462A-48DA-900C-66CB3F312DA7__WS1A9193826455F5FF-7F10494411F03A24F24-7E24">
<li>None&#0160;- the items attached to the task will not change.</li>
<li>Hide&#0160;- the items attached to the task will be hidden.</li>
<li>Model Appearance&#0160;- the items attached to the task will be displayed as they are defined in the model. This may be the original CAD colors or, if you have applied color and transparency overrides in&#0160;Autodesk Navisworks, then these will be displayed.</li>
<li>Appearance Definitions&#0160;- enables you to choose from the list of&#0160;Appearance Definitions, including ten predefined appearances and any custom appearances you have added.</li>
</ul>
<p>&#0160;</p>
<p>&#0160;</p>
<p>The corresponding API are:</p>
<ul>
<li>SimulationTaskType:A Simulation Task Type informs the Simulation Engine how models attached to a TimelinerTask should be represented at different times during the simulation</li>
<li>SimulationAppearance: referenced by SimulationTaskTypes objects</li>
<li>DocumentTimeliner.SimulationAppearanceAddCopy: Adds a copy of the SimulationAppearance at the end of the collection to the DocumentTimeliner</li>
<li>DocumentTimeliner.SimulationTaskTypeAddCopy: Adds a copy of the SimulationTaskType at the end of the collection to the DocumentTimeliner</li>
</ul>
<p>This is a code pieces that demos how to create a custom appearance and a custom task type. In this task type, its start status links to the&#0160;custom appearance.</p>
<p>&#0160;</p>
<p>&#0160; <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0879fba4970d-pi" style="display: inline;"><img alt="2015-9-30 10-07-10" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb0879fba4970d image-full img-responsive" src="/assets/image_604474.jpg" title="2015-9-30 10-07-10" /></a></p>
<script src="https://adndevblog.typepad.com/files/run_prettify-3.js" type="text/javascript"></script>
<pre class="csharp prettyprint">     
using Nw = Autodesk.Navisworks.Api;
using Tl = Autodesk.Navisworks.Api.Timeliner;

   public override int Execute(params string[] parameters)
        { 
            Document oDoc = Autodesk.Navisworks.Api.Application.ActiveDocument; 

            try
            {
                Nw.Document doc = Nw.Application.ActiveDocument;
                Nw.DocumentParts.IDocumentTimeliner tl = doc.Timeliner;
                Tl.DocumentTimeliner tl_doc = (Tl.DocumentTimeliner)tl;

                
                Tl.SimulationAppearance newSimAppearance = 
         new Tl.SimulationAppearance(&quot;myAppearance&quot;, new Color(1, 1, 1), 0.5);
                 tl_doc.SimulationAppearanceAddCopy(newSimAppearance);

                 Tl.SimulationTaskType newTaskType = new Tl.SimulationTaskType();
                 newTaskType.DisplayName = &quot;myTaskType&quot;;
                 newTaskType.StartStatus.AppearanceMode = Tl.SimulationAppearanceMode.UserAppearance;
                 newTaskType.StartStatus.SimulationAppearanceName = &quot;myAppearance&quot;;
                 tl_doc.SimulationTaskTypeAddCopy(newTaskType);
                     
                 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
         
            return 0;
        }<br /><br />
</pre>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d15fcaf2970c-pi" style="display: inline;"><img alt="2015-9-30 10-21-21" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d15fcaf2970c img-responsive" src="/assets/image_739456.jpg" title="2015-9-30 10-21-21" /></a></p>
