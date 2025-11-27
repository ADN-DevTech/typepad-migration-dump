---
layout: "post"
title: "Developing Autodesk Civil 3D applications with .NET"
date: "2006-08-08 10:13:16"
author: "Kean Walmsley"
categories:
  - "Civil 3D"
original_url: "https://www.keanw.com/2006/08/developing_auto.html "
typepad_basename: "developing_auto"
typepad_status: "Publish"
---

<p><em>Thanks to Scott Wolter from Digital Vellum for suggesting this topic, and to Partha Sarkar (a member of the DevTech team in Bangalore, India) for contributing it...</em></p>

<p>Welcome to the world of developing Autodesk Civil 3D 2007 applications with Visual Studio .NET. Autodesk Civil 3D provides interop assemblies that expose its COM interfaces for use from .NET technologies, such as VB.NET, C#.</p>

<p>Before you start developing .NET applications for Autodesk Civil 3D 2007, I strongly recommend you apply the <a href="http://usa.autodesk.com/adsk/servlet/ps/dl/item?siteID=123112&id=7454845&linkID=9240698">Autodesk Civil 3D 2007 Service Pack 1A</a>, otherwise you may find some issues in referencing the Civil 3D TLB files such as AeccXLand40.tlb, AeccXUiLand40.tlb etc.</p>

<p>I’m now going to look at how to write an application using Visual Studio .NET to generate a Profile (also called a vertical alignment) for a given horizontal alignment with an associated surface.</p>

<p><a href="http://through-the-interface.typepad.com/photos/uncategorized/horizontalalignment.png"></a><a href="http://through-the-interface.typepad.com/photos/uncategorized/civil3dverticalalignment.png"><img class="image-full" title="Civil3dverticalalignment" alt="Civil3dverticalalignment" src="/assets/civil3dverticalalignment.png" border="0" /></a> Figure 1 - A typical profile</p>

<p>Before we get into the nitty-gritty of developing a .NET application for Civil 3D, first let us understand what a <strong>Profile</strong> object is. Profiles are used to visualize the terrain along a route of interest, such as a proposed road or pipeline. There are two distinct Civil 3D entities when using Profiles, the Profile itself and the Profile View. The Profile is the actual section cut through a surface or the proposed design profile and has its associated style and label style. The Profile View is the data surrounding the display of the profile line - grids, axes, labels and desired band styles, which contain data on any side of the grid, horizontal / vertical geometry data etc.</p>

<p>The first step when developing a .NET application for Civil 3D is to add the references to Civil 3D COM libraries such as AeccXLandLib, AeccXUiLandLib etc. If you are planning to work with Corridor objects or Pipe objects, you need to reference the appropriate libraries for Corridor and Pipes.</p>

<p>You need to make sure you add a ProfileView, before you call AddFromSurface and this is how you do it:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Dim</span> oProfileViews <span style="COLOR: blue">As</span> AeccLandLib.AeccProfileViews</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">oProfileViews = oAlignment.ProfileViews</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">oProfileViews.Add(<span style="COLOR: maroon">&quot;Test Profile View&quot;</span>, <span style="COLOR: maroon">&quot;0&quot;</span>, originPt, oVStyle, oBandStyle)</p></div>

<p>The final step is to add the Profile for a given surface, profile type, profile style, sample starting and ending stations, and layer name by calling AddFromSurface:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Dim</span> oProfile <span style="COLOR: blue">As</span> AeccLandLib.AeccProfile</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">oProfile = oAlignment.Profiles.AddFromSurface(<span style="COLOR: maroon">&quot;Existing Ground Profile&quot;</span>, _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; &nbsp;&nbsp; &nbsp; AeccLandLib.AeccProfileType.aeccExistingGround, _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; &nbsp;&nbsp; &nbsp; oProfileStyle, oSurface.Name, startStn, endStn, <span style="COLOR: maroon">&quot;0&quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p></div>

<p class="MsoNormal" style="MARGIN: 0in 0in 0pt; mso-layout-grid-align: none"></p>

<p>Here is the complete VB.NET sample code:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Imports</span> System</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Imports</span> Autodesk</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Imports</span> Autodesk.AutoCAD</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Imports</span> Autodesk.AutoCAD.EditorInput</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Imports</span> Autodesk.AutoCAD.Runtime</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Imports</span> Autodesk.AutoCAD.ApplicationServices</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Imports</span> Autodesk.AutoCAD.DatabaseServices</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Imports</span> AcadNET = Autodesk.AutoCAD</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Imports</span> AcadCOM = Autodesk.AutoCAD.Interop</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">'To resolve Application ambiguities</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Imports</span> AcadNETApp = Autodesk.AutoCAD.ApplicationServices.Application</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Imports</span> AeccLandLib = Autodesk.AECC.Interop.Land</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Imports</span> AeccUiLandLib = Autodesk.AECC.Interop.UiLand</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Imports</span> AecUIBase = Autodesk.AEC.Interop.UIBase</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Public</span> <span style="COLOR: blue">Class</span> MyCivil3DApp</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">Private</span> m_oAcadApp <span style="COLOR: blue">As</span> AcadCOM.AcadApplication</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">Private</span> m_oAeccApp <span style="COLOR: blue">As</span> AeccUiLandLib.IAeccApplication</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">Private</span> m_oAeccDoc <span style="COLOR: blue">As</span> AeccUiLandLib.IAeccDocument</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">Private</span> m_oAeccDb <span style="COLOR: blue">As</span> AeccLandLib.IAeccDatabase</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; &lt;CommandMethod(<span style="COLOR: maroon">&quot;GP&quot;</span>)&gt; _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">Public</span> <span style="COLOR: blue">Sub</span> GroundProfile()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">Dim</span> ed <span style="COLOR: blue">As</span> Editor = AcadNETApp.DocumentManager.MdiActiveDocument.Editor</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">Try</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;m_oAcadApp = AcadNETApp.AcadApplication</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;m_oAeccApp = m_oAcadApp.GetInterfaceObject(<span style="COLOR: maroon">&quot;AeccXUiLand.AeccApplication&quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">Catch</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;MsgBox(<span style="COLOR: maroon">&quot;Failed to Load the Civil 3D Application!&quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Exit</span> <span style="COLOR: blue">Sub</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">End</span> <span style="COLOR: blue">Try</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">Try</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;m_oAeccDoc = m_oAeccApp.ActiveDocument</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;m_oAeccDb = m_oAeccApp.ActiveDocument.Database</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">Catch</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;MsgBox(<span style="COLOR: maroon">&quot;No document loaded in Civil3D (zero-doc state)!&quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Exit</span> <span style="COLOR: blue">Sub</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">End</span> <span style="COLOR: blue">Try</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">Try</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Dim</span> oSite <span style="COLOR: blue">As</span> AeccLandLib.AeccSite</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;oSite = m_oAeccDb.Sites.Item(0)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Dim</span> oAlignment <span style="COLOR: blue">As</span> AeccLandLib.AeccAlignment</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;oAlignment = oSite.Alignments.Item(0)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Dim</span> originPt <span style="COLOR: blue">As</span> <span style="COLOR: blue">Object</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;originPt = m_oAcadApp.ActiveDocument.Utility.GetPoint(, _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: maroon">&quot;Select the Origin Point for the Profile View&quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Dim</span> oVStyle <span style="COLOR: blue">As</span> AeccLandLib.AeccProfileViewStyle</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">'This value is used from the C3D template styles</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;oVStyle = m_oAeccDb.ProfileViewStyles.Item(<span style="COLOR: maroon">&quot;Major Grids&quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Dim</span> oBandStyle <span style="COLOR: blue">As</span> AeccLandLib.AeccProfileViewBandStyleSet</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">'This value is used from the C3D template styles</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;oBandStyle = m_oAeccDb.ProfileViewBandStyleSets.Item(<span style="COLOR: maroon">&quot;Profile Data &amp; Geometry&quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Dim</span> oProfileViews <span style="COLOR: blue">As</span> AeccLandLib.AeccProfileViews</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;oProfileViews = oAlignment.ProfileViews</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;oProfileViews.Add(<span style="COLOR: maroon">&quot;Test Profile View&quot;</span>, <span style="COLOR: maroon">&quot;0&quot;</span>, originPt, oVStyle, oBandStyle)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Dim</span> oProfileStyle <span style="COLOR: blue">As</span> AeccLandLib.AeccProfileStyle</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;oProfileStyle = m_oAeccDoc.LandProfileStyles.Item(<span style="COLOR: maroon">&quot;Existing Ground&quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Dim</span> oSurface <span style="COLOR: blue">As</span> AeccLandLib.AeccSurface</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">'You can change the name of the Surface here</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;oSurface = m_oAeccDb.Surfaces.Item(<span style="COLOR: maroon">&quot;EG&quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Dim</span> startStn <span style="COLOR: blue">As</span> <span style="COLOR: blue">Double</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Dim</span> endStn <span style="COLOR: blue">As</span> <span style="COLOR: blue">Double</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;startStn = oAlignment.StartingStation</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;endStn = oAlignment.EndingStation</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Dim</span> oProfile <span style="COLOR: blue">As</span> AeccLandLib.AeccProfile</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;oProfile = oAlignment.Profiles.AddFromSurface(<span style="COLOR: maroon">&quot;Existing Ground Profile&quot;</span>, _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; AeccLandLib.AeccProfileType.aeccExistingGround, _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; oProfileStyle, oSurface.Name, startStn, endStn, <span style="COLOR: maroon">&quot;0&quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;m_oAcadApp = <span style="COLOR: blue">Nothing</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;m_oAeccApp = <span style="COLOR: blue">Nothing</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;m_oAeccDoc = <span style="COLOR: blue">Nothing</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;m_oAeccDb = <span style="COLOR: blue">Nothing</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">Catch</span> ex <span style="COLOR: blue">As</span> Exception</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;ed.WriteMessage(<span style="COLOR: maroon">&quot;Error: &quot;</span>, ex.Message &amp; vbCrLf)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">End</span> <span style="COLOR: blue">Try</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">End</span> <span style="COLOR: blue">Sub</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">End</span> <span style="COLOR: blue">Class</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p></div>

<p class="MsoNormal" style="MARGIN: 0in 0in 0pt"></p><br /><p class="MsoNormal" style="MARGIN: 0in 0in 0pt">And that's it for creating a VB.NET application to generate a profile inside Autodesk Civil 3D.</p>
