---
layout: "post"
title: "What’s new in Inventor 2021 API release"
date: "2020-06-03 01:25:27"
author: "Chandra Shekar Gopal"
categories:
  - "Chandra Shekar Gopal"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2020/06/whats-new-in-inventor-2021-api-release.html "
typepad_basename: "whats-new-in-inventor-2021-api-release"
typepad_status: "Publish"
---

<p>by <a href="https://adndevblog.typepad.com/manufacturing/chandra-shekar-gopal.html" rel="noopener" target="_blank">Chandra shekar Gopal</a></p>
<p>Inventor 2021 was released worldwide in March 2020. Autodesk Inventor is committed to improvements on every release. Having said that, Inventor 2021 API release consists of many improvements and enhancements. This blog post will cover the 4 major areas considered for improvements. We also have a video is you prefer to watch, instead of read: <a href="https://www.youtube.com/watch?v=dtWpkKAAnzM">https://www.youtube.com/watch?v=dtWpkKAAnzM</a></p>
<p>&#0160;</p>
<p><strong>1. </strong><strong>General API</strong></p>
<p>• <strong>SaveOptions.SaveFilesInLibraryFolders property</strong> - When users are dealing with commonly used components like iPart factory or Standard parts. It requires to save these files in library folders. Enabling this option allows to save files in library folders implicitly. Please note that after saving files in library folder,files are not allowed to modify via Inventor API.</p>
<p>• <strong>ModelingSettings.AutoFindErrorsAfterManualRepair property</strong> – On enabling AutoFindErrorsAfterManualRepair property, it finds errors automatically even after manual repair.</p>
<p>• <strong>7 DefaultToSaveForxxx properties and 7 PromptSaveForxxx properties in SaveOptions object</strong> - While saving files in library folders, there are 7 different prompts and 7 default conditions considered. To control all 7 prompts and 7 default conditions, respective 7 DefaultToSaveForxxx properties and 7 PromptSaveForxxx properties are introduced in Inventor 2021 depicted in below UI image.</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0264e2dfe3f4200d-pi" style="display: inline;"><img alt="Application options" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0264e2dfe3f4200d img-responsive" src="/assets/image_dd1e99.jpg" style="margin-right: auto; margin-left: auto; display: block;" title="Application options" /></a></p>
<p>• <strong>GeneralOptions.ShowHomeBaseOnStartup property</strong> - ShowHomeBaseOnStartup API allows to show “My home” tab on start up.</p>
<p>• <strong>ExpressionList.AllowCustomValues property</strong> – It allows to get and set whether to allow the custom values for parameter. On enabling this property, custom values or numbers with multiple units can be added into multi value parameter.</p>
<blockquote>
<p><em>Sub Test_AllowCustomValues()</em></p>
<p><em>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Dim oDoc As PartDocument</em></p>
<p><em>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Set oDoc = ThisApplication.ActiveDocument</em></p>
<p>&#0160;</p>
<p><em>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Dim oDef As PartComponentDefinition</em></p>
<p><em>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Set oDef = oDoc.ComponentDefinition</em></p>
<p>&#0160;</p>
<p><em>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Dim oUserParam As UserParameter</em></p>
<p><em>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Set oUserParam = oDef.Parameters.UserParameters.Item(1)</em></p>
<p>&#0160;</p>
<p><em>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oUserParam.ExpressionList.AllowCustomValues = False</em></p>
<p><em>End Sub</em></p>
</blockquote>
<p>• <strong>MeasureTools.GetAnglePrecision, SetAnglePrecision, GetLengthPrecision and SetLengthPrecision methods</strong> - It was a request from a customer who wants to set AnglePrecision and LengthPrecision in Measure tools and is now implemented in Inventor 2021. Along with setting precision, AnglePrecision and LengthPrecision can be retrieved as well.</p>
<p>• <strong>TransientBrep.GetIdenticalBodies</strong> - Another new method called GetIdenticalBodies is introduced to identify the identical bodies using TransientBrep object.</p>
<p>• <strong>ThemeManager object</strong> - In ThemeManager Object, there are 2 new Improvements. One is to get active theme and another one to set theme to either (Light or Dark)</p>
<p>• <strong>ViewFrame object</strong> - New ViewFrame object is introduced in Inventor 2021 API which allows to specify the size and location of view frame.</p>
<p>• <strong>ImportedRVTComponent object </strong>- ImportedRVTComponent object allows to import Revit component into Inventor data.</p>
<p>• <strong>Export method for iAssemblyFactory, iPartFactory, ParameterTable, PositionalRepresentations and iFeatureTable</strong> - Export method is extended to extract tables from iAssemblyFactory, iPartFactory, ParameterTable, PositionalRepresentation and iFeatureTable into external file like Excel, text file etc.,</p>
<p>• <strong>Removed FileFormatEnum: kdBASEIIIFormat, kdBASEIVFormat and kMicrosoftAccessFormat</strong></p>
<p>• <strong>Date properties return only Date instead of Time</strong><strong> - </strong>In earlier Inventor versions, dates were returning with time (like “2019/06/07 6:42:43 PM”). Now, it is simplified to return only date in YYYY/MM/DD format (Like “2019/06/07”).</p>
<p>&#0160;</p>
<p><strong>2. </strong><strong>Part Document API</strong></p>
<p>• <strong>SilhouetteCurves.Add method with SilhouetteCurve.Body, ExcludedFaces, ExcludeInternalFaces, ExcludeStraightFaces properties</strong> - Exposed SilhouetteCurves.AddSilhouette method, SilhouetteCurve.Body, ExcludedFaces, ExcludeInternalFaces, ExcludeStraightFaces properties and Remove the SilhouetteCurves.Add method and FacesOrBody, IncludeCoincidentSilhouettes properties to make silhouette curve APIs consistent with UI calculation algorithm.</p>
<blockquote>
<p><em>Sub Test_AddSilhouette()</em></p>
<p>&#0160;</p>
<p><em>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Dim oDoc As PartDocument</em></p>
<p><em>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Set oDoc = ThisApplication.ActiveDocument</em></p>
<p>&#0160;</p>
<p><em>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Dim oDef As PartComponentDefinition</em></p>
<p><em>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Set oDef = oDoc.ComponentDefinition</em></p>
<p>&#0160;</p>
<p><em>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Dim oBody As SurfaceBody</em></p>
<p><em>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Set oBody = oDef.SurfaceBodies.Item(1)</em></p>
<p>&#0160;</p>
<p><em>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Dim oSketch As Sketch3D</em></p>
<p><em>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Set oSketch = oDef.Sketches3D.Add</em></p>
<p><em>&#0160;&#0160;&#0160;&#0160;&#0160; </em></p>
<p><em>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Dim oDirection As Face</em></p>
<p><em>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Set oDirection = ThisApplication.CommandManager.Pick(kPartFacePlanarFilter, &quot;Select a face&quot;)</em></p>
<p>&#0160;</p>
<p><em>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;Call oSketch.SilhouetteCurves.AddSilhouette(oBody, oDirection)</em></p>
<p><em>End Sub</em></p>
</blockquote>
<p>• <strong>HoleFeatures.CreateClearanceInfo and HoleClearanceInfo.SetClearanceInfo methods </strong>- Another request from a customer to implement methods to create hole clearance info and setting clearance info for a hole feature. New HoleFeatures.CreateClearanceInfo and HoleClearanceInfo.SetClearanceInfo methods to better support to set clearance info for hole feature.</p>
<p>• <strong>FlangeFeatures.CreateFlangeDefinition with FlangeDefinition.FlangeAngleReferencePlane property</strong> - exposed FlangeDefinition.FlangeAngleReference property to specify flange angle using a face or workplane while creating flange definition.</p>
<p>• <strong>SurfaceBody.OrientedMinimumRangeBox property</strong> - OrientedMinimumRangebox in SurfaceBody object which gives overall size of the surface body.</p>
<p>• <strong>SheetMetalComponentDefinition.SetBodySheetMetalStyle2 property</strong> – It is an additional method is introduced to set surfacebody sheet metal style even though computation errors exist.</p>
<p>• <strong>UnwrapResultAlignmentEnum.kXZPlaneAlignment and kYZPlaneAlignment enumerator</strong> - Additionally, kXZPlanealignment and kYZPlanealignment enums are added to UnwrapResultAlignmentEnum.</p>
<p>&#0160;</p>
<p><strong>3. </strong><strong>Assembly Document API</strong></p>
<p>• <strong>ComponentOccurrence.Replace2 method</strong> - Replace2 function is enhanced to save edits and keep adaptivity after replacing the components.</p>
<p>&#0160;</p>
<p><strong>4. </strong><strong>Drawing Document API</strong></p>
<p>• <strong>DrawingView.DesignViewAssociative, DesignViewRepresentation and hide the DrawingView.ActiveDesignViewRepresentation properties</strong> - Two new properties are added to DrawingView object. One is to get associativity of drawing view (True/False) and another one is to get associated view of drawing view. In earlier version, ActiveDesignViewRepresentation property is removed from DrawingView object.</p>
<p>• <strong>SheetFormat.FitViewsToSheet property </strong>- New SheetFormat.FitViewsToSheet property and SheetFormats.AddWithOptions method allows to specify whether the drawing views should fit the sheet when they are created from sheet format.</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0264e2dfe3f8200d-pi" style="display: inline;"><img alt="Sheet format" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0264e2dfe3f8200d img-responsive" src="/assets/image_6b6eb4.jpg" style="margin-right: auto; margin-left: auto; display: block;" title="Sheet format" /></a></p>
<blockquote>
<p><em>Dim oOptions As NameValueMap</em></p>
<p><em>Set oOptions = ThisApplication.TransientObjects.CreateNameValueMap</em></p>
<p>&#0160;</p>
<p><em>Call oOptions.Add(&quot;FitViewsToSheet&quot;, True)</em></p>
<p>&#0160;</p>
<p><em>Dim oSheetFormat As SheetFormat</em></p>
<p><em>Set oSheetFormat = oDoc.SheetFormats.AddWithOptions(oSheet, &quot;Custom sheet&quot;, oOptions)</em></p>
<p>&#0160;</p>
</blockquote>
<p>• <strong>DimensionTypeEnum.kParallelDiametricDimensionType enumerator </strong>- New DimensionTypeEnum.kParallelDiametricDimensionType enumerator is added to support parallel diameter dimension.</p>
<p>• <strong>GeneralDimensions.AddLinear2 method</strong> - GeneralDimensions.AddLinear2 method is introduced to support new dimension type kAlignedToCurveDimensionType and kNormalToCurveDimensionType which allow to specify alignment geometry to rotate the dimension alignment.</p>
<p>• <strong>DrawingStylesManager.ReplaceStyles method</strong> - This is also request from a one of the customers. DrawingStylesManager.ReplaceStyles method allows to replace the drawing styles.</p>
<p>• <strong>SketchBlockDefinition.Description property </strong>- SketchBlockDefinition.Description property allows to get and set the description for a sketch block definition.</p>
<p>• <strong>SketchHatchRegion object </strong>– For the first time, object is introduced to add Sketch Hatch through Inventor API (2021).</p>
<blockquote>
<p><em>Sub Test_SketchHatchRegion()</em></p>
<p><em>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Dim oDoc As DrawingDocument</em></p>
<p><em>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Set oDoc = ThisApplication.ActiveDocument</em></p>
<p>&#0160;</p>
<p><em>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Dim oSketch As DrawingSketch</em></p>
<p><em>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Set oSketch = oDoc.ActiveSheet.Sketches.Item(1)</em></p>
<p>&#0160;</p>
<p><em>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Dim oProfile As Profile</em></p>
<p><em>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Set oProfile = oSketch.Profiles.AddForSolid(True)</em></p>
<p>&#0160;</p>
<p><em>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Dim oRegion As SketchHatchRegion</em></p>
<p><em>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Set oRegion = oSketch.SketchHatchRegions.Add(oProfile, , 15, 0.5)</em></p>
<p><em>End Sub</em></p>
</blockquote>
<p>• <strong>HoleTableColumn.UnitsFormatting object </strong>- New HoleTableColumn.UnitsFormatting object allows to query and edit the units formatting for hole table column.</p>
<p>&#0160;</p>
<p>By the way, Inventor 2021 engine is also available in Design Automation for Inventor. If you are interested in transferring Inventor plugin into cloud application, here is opportunity to do. To explore more about this, go through below video link.</p>
<p><a href="https://www.youtube.com/watch?v=RRgahfnnbWo">https://www.youtube.com/watch?v=RRgahfnnbWo</a></p>
