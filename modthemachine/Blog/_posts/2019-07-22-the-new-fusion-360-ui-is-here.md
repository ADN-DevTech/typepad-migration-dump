---
layout: "post"
title: "The new Fusion 360 UI is here!"
date: "2019-07-22 10:52:06"
author: "Adam Nagy"
categories:
  - "Adam"
  - "Announcements"
  - "Fusion 360"
original_url: "https://modthemachine.typepad.com/my_weblog/2019/07/the-new-fusion-360-ui-is-here.html "
typepad_basename: "the-new-fusion-360-ui-is-here"
typepad_status: "Publish"
---

<p>A new <strong>Fusion 360</strong> toolbar <strong>UI</strong> is becoming official soon, including changes to the existing <strong>API</strong>. To ensure that your <strong>add-ins</strong> are still compatible, we highly encourage you to update your <strong>add-in</strong> using the new <strong>API</strong>: <a href="https://docs.google.com/document/d/174ISmaXs-60JRtn8bt4Sbr6Z3jqzkyoSizVD9YwChOQ/edit?usp=sharing">https://docs.google.com/document/d/174ISmaXs-60JRtn8bt4Sbr6Z3jqzkyoSizVD9YwChOQ/edit?usp=sharing</a></p>
<p>For convenience I&#39;m also pasting here the content:</p>
<pre><strong>Tabbed Toolbar API</strong>

UserInterface class changes
<a href="http://help.autodesk.com/view/fusion360/ENU/?guid=GUID-52303e26-c1c2-4a1c-8c1a-a3c199e6db63">http://help.autodesk.com/view/fusion360/ENU/?guid=GUID-52303e26-c1c2-4a1c-8c1a-a3c199e6db63</a>

app = adsk.core.Application.get()
ui  = app.userInterface

Indicates if Fusion 360 is running with in the old toolbar or new toolbar:

# bool isTabbedToolbar { get; }
runningTabbedToolbar = ui.isTabbedToolbar

Gets all of the toolbar tabs. This returns all of the tabs available, regardless of which workspace or product they&#39;re associated with.

# ToolbarTabList allToolbarTabs { get; }
Similar example:  see commandDefinitions, like:  ui.commandDefinitions
allToolbarTabsList = ui.allToolbarTabs

this gets all of the toolbar tabs associated with the specified product. The full list of available products can be obtained by using the Application.supportedProductTypes property.

# ToolbarTabList toolbarTabsByProductType(string productType);
supportedProductTypesList = app.supportedProductTypes
print(supportProductTypesList)
# Example output:  (&#39;CAMProductType&#39;, &#39;AnimationProductType&#39;, &#39;DesignProductType&#39;, &#39;FusionDrawingProductType&#39;, &#39;SimStudiesProductType&#39;)
tabsListForProduct = ui.toolbarTabsByProductType(&#39;DesignProductType&#39;)

<strong>New! ToolbarTabList class</strong>

This is a collection of ToolbarTab objects

Returns the specified tab using an index into the collection.
# ToolbarTab item(uint index);

Returns the ToolbarTab of the specified ID.
# ToolbarTab itemById(string id);

Gets the number of toolbar tabs in the collection.
# uint count { get; }

Try to get the Solid tab:
solidTab = allToolbarTabsList.itemById(&#39;SolidTab&#39;)

<strong>New! ToolbarTab class</strong>

Gets ID of tab:
# string id { get; }

Get position of this tab within its toolbar:
# uint index { get; }

Gets whether this tab is currently being displayed in the user interface
# bool isVisible { get; }

Gets the name of the tab as seen in the user interface.
# string name { get; }

Gets the collection containing the panels associated with this tab.
It&#39;s through this collection that you can add new toolbar panels.
# ToolbarTabPanels toolbarPanels { get; }

Gets the parent UserInterface object.
# UserInterface parentUserInterface { get; }    

# Returns the name of the product this toolbar tab is associated with.
# string productType { get; }   

# Make sure that it isn&#39;t null
if solidTab:
    # Found solidTab!  Ok to use it now!
    isSolidTabVisible = solidTab.isVisible
    allSolidTabPanels = solidTab.toolbarPanels

<strong>New! ToolbarTabPanels class</strong> <br />
Collection of ToolbarPanel objects (ToolbarPanel is existing class) 
Unlike ToolbarPanels, ToolbarTabPanels class lets you position the panel in a specific tab in relation to other panels in the toolbar. 
 
Creates a new ToolbarPanel. The panel is initially empty.
# ToolbarPanel add(string id, string name, [Optional] string positionID = &quot;&quot;, [Optional] bool isBefore = true);

Returns the specified toolbar panel using an index into the collection.
# ToolbarPanel item(uint index);

Returns the ToolbarPanel at the specified ID.
ToolbarPanel itemById(string id);

Gets the number of panels.
# uint count { get; }

Add a new panel to a particular tab:
fancyAddinToolbarPanel = allSolidTabPanels.add(&#39;JenCompanyAddin&#39;, &#39;DoImportantFusionThingPanel&#39;)

<strong>ToolbarPanel class (existing class, but 1 new method)</strong>
<a href="http://help.autodesk.com/view/fusion360/ENU/?guid=GUID-0ca48ac9-da95-4623-bf87-150f3729717a">http://help.autodesk.com/view/fusion360/ENU/?guid=GUID-0ca48ac9-da95-4623-bf87-150f3729717a</a>

# string id { get; }
# uint index { get; }
# bool isVisible { get; }
# string name { get; }
# bool deleteMe();
# ToolbarControls controls { get; }
# UserInterface parentUserInterface { get; }
# ToolbarControlList promotedControls { get; }
# ObjectCollection relatedWorkspaces { get; set; }
# productType { get; }

New method - Gets the position this panel is in within the toolbar tab.
# uint indexWithinTab(string tabId); 

Add a control to the initially empty new toolbar panel:
ToolbarControls class:  <a href="https://help.autodesk.com/view/fusion360/ENU/?guid=GUID-df24e158-40e0-4aed-9990-97c470935dbb">https://help.autodesk.com/view/fusion360/ENU/?guid=GUID-df24e158-40e0-4aed-9990-97c470935dbb</a>

fancyAddinToolbarPanelControls = fancyAddinToolbarPanel.controls

Assume JenFancyCommmandDef created already....
<a href="https://help.autodesk.com/view/fusion360/ENU/?guid=GUID-1ed0cfb5-2ad4-4285-9eec-484ef19f2729">https://help.autodesk.com/view/fusion360/ENU/?guid=GUID-1ed0cfb5-2ad4-4285-9eec-484ef19f2729</a>
doSomethingAmazingCmdControl = fancyAddinToolbarPanelControls.addCommand(JenFancyCommmandDef, &#39;&#39;, False)

<strong>Workspace class (existing class, but 1 new method)<br /></strong>
Gets the collection containing the tabs associated with this workspace.
# ToolbarTabs toolbarTabs { get; }

Get a list of all workspaces:
<a href="https://help.autodesk.com/view/fusion360/ENU/?guid=GUID-75ac7274-735c-4a61-af57-1d837c4df217">https://help.autodesk.com/view/fusion360/ENU/?guid=GUID-75ac7274-735c-4a61-af57-1d837c4df217</a>

workspacesList = ui.workspaces
print (workspacesList.count)    

The above example prints 29 total workspaces in Fusion.
Note from Jennifer:  With 29 workspaces that don&#39;t match up with the new Tabbed Toolbar 
&quot;Workspaces&quot; as shown in the Workspace Switcher, I&#39;m not sure how valuable 
workspaces will be for new API writers and tabbed toolbar?

Get a single workspace:
oneWorkspace = workspacesList.itemById(&#39;FusionModelingEnvironment&#39;) 

<a href="https://help.autodesk.com/view/fusion360/ENU/?guid=GUID-33f9ed37-e5c7-4153-ba85-c3254a199dd1">https://help.autodesk.com/view/fusion360/ENU/?guid=GUID-33f9ed37-e5c7-4153-ba85-c3254a199dd1</a>

# Gets the collection containing the tabs associated with this workspace.
# ToolbarTabs toolbarTabs { get; }
modelingEnvToolbarTabs = oneWorkspace.toolbarTabs

<strong>New! ToolbarTabs class<br /></strong>
Collection of ToolbarTab objects

Returns the specified toolbar tab using an index into the collection.
#  ToolbarTab item(uint index);

Returns the ToolbarTab at the specified ID.
# ToolbarTab itemById(string id);

# Gets the number of ToolbarTabs.
# uint count { get; }
</pre>
<p>In the <strong>August Update</strong> (coming around <strong>mid August</strong>), we will be making the new toolbar <strong>UI</strong> the default toolbar for <strong>Fusion 360</strong>. Users will have the option to revert back to the old toolbar, however this option will only be available for a limited period of time. The current plan is to completely transition over to the new toolbar <strong>UI</strong> by the <strong>September</strong> <strong>Update</strong>.&#0160;</p>
<p>If you haven’t tried out the new toolbar <strong>UI</strong> yet, you can turn it on by going to the <strong>Preferences</strong> &gt; <strong>Preview</strong> section and checking the <strong>UI Preview</strong> option. <br />Here is a video of the <a href="https://youtu.be/Zf-fErCl43I">top 5 changes to look for when using the new toolbar UI.&#0160;</a></p>
<p>The commands of existing <strong>add-ins</strong> will be redirected to the most appropriate place, but it&#39;s not always possible. In that case the command buttons will be placed in the <strong>Tools</strong> tab as a default location under the new toolbar <strong>UI</strong>.&#0160; <br />It&#39;s better to use the new <strong>API</strong>&#39;s to make sure your command button goes exactly where you want it 😀</p>
