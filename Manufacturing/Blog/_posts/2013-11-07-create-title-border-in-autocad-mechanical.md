---
layout: "post"
title: "Create title border in AutoCAD Mechanical"
date: "2013-11-07 10:55:08"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "AutoCAD Mechanical"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/11/create-title-border-in-autocad-mechanical.html "
typepad_basename: "create-title-border-in-autocad-mechanical"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>There is a comprehensive .NET sample using the AutoCAD Mechanical COM API in the SDK under &quot;<strong>acadmapi\sample\dotNet</strong>&quot; called &quot;<strong>netTitleBorder</strong>&quot;:&#0160;<a href="http://usa.autodesk.com/adsk/servlet/index?id=14952981&amp;siteID=123112" target="_self">http://usa.autodesk.com/adsk/servlet/index?id=14952981&amp;siteID=123112</a></p>
<p>Here is also a simpler VBA code that shows how the relevant API works:</p>
<pre>Public Sub CreateTitleBorder()

&#39; Requires a reference to:
&#39; - Autodesk SymBBAuto Type Library
&#39; - Autodesk AutoCAD Mechanical Type Library

&#39;Get Application Object
Dim oApp As AcadmApplication
Set oApp = ThisDrawing.Application.GetInterfaceObject( _<br />  &quot;AcadmAuto.AcadmApplication&quot;)

&#39;Reference to the Symbol Manager
Dim symMgr As McadSymbolBBMgr
Set symMgr = ThisDrawing.Application.GetInterfaceObject( _<br />  &quot;SymBBAuto.McadSymbolBBMgr&quot;)

&#39;Reference to the TitleBorder Manager
Dim oTitleBMng As McadTitleBorderMgr
Set oTitleBMng = symMgr.TitleBorderMgr

&#39;Create New Descriptor
Dim oDescriptor As McadTitleBorderDescriptor
Set oDescriptor = oTitleBMng.NewDescriptor

&#39;Set Position
oDescriptor.Position(0) = 0
oDescriptor.Position(1) = 41.7
oDescriptor.Position(2) = 0

&#39;Fill up Border properties
oDescriptor.Border.Name = &quot;ANSI_C&quot;

oDescriptor.Border.Extends(0)(0) = 1E+20
oDescriptor.Border.Extends(0)(1) = 1E+20
oDescriptor.Border.Extends(0)(2) = 1E+20

oDescriptor.Border.Extends(1)(0) = -1E+20
oDescriptor.Border.Extends(1)(1) = -1E+20
oDescriptor.Border.Extends(1)(2) = -1E+20

oDescriptor.Border.Position(0) = 0
oDescriptor.Border.Position(1) = 41.7
oDescriptor.Border.Position(2) = 0

&#39;Fill up Title Properties
oDescriptor.Title.Name = &quot;ANSI_TITUS&quot;
oDescriptor.Title.Position(0) = 21.5
oDescriptor.Title.Position(1) = 42.443
oDescriptor.Title.Position(2) = 0

&#39;Create Title Attribute Values
Dim Attributes(0 To 19, 0 To 1) As String

Attributes(0, 0) = &quot;GEN-TITLE-APPM&quot;: Attributes(0, 1) = &quot;My Boss&quot;
Attributes(1, 0) = &quot;GEN-TITLE-ISSD&quot;: Attributes(1, 1) = &quot;02/02/2008&quot;
Attributes(2, 0) = &quot;GEN-TITLE-NAME&quot;: Attributes(2, 1) = _<br />  &quot;Administrator&quot;
Attributes(3, 0) = &quot;GEN-TITLE-CTRN&quot;: Attributes(3, 1) = &quot;12358746&quot;
Attributes(4, 0) = &quot;GEN-TITLE-DACT&quot;: Attributes(4, 1) = &quot;&quot;
Attributes(5, 0) = &quot;GEN-TITLE-CHKD&quot;: Attributes(5, 1) = &quot;01/01/2008&quot;
Attributes(6, 0) = &quot;GEN-TITLE-SCA&quot;: Attributes(6, 1) = &quot;1&#39;0&quot;&quot;=1&#39;0&quot;&quot;&quot;
Attributes(7, 0) = &quot;GEN-TITLE-DES1&quot;: Attributes(7, 1) = &quot;Title1&quot;
Attributes(8, 0) = &quot;GEN-TITLE-REV&quot;: Attributes(8, 1) = &quot;rev1&quot;
Attributes(9, 0) = &quot;GEN-TITLE-WT&quot;: Attributes(9, 1) = &quot;Weight&quot;
Attributes(10, 0) = &quot;GEN-TITLE-DES2&quot;: Attributes(10, 1) = &quot;Subtitle1&quot;
Attributes(11, 0) = &quot;GEN-TITLE-SIZ&quot;: Attributes(11, 1) = &quot;C&quot;
Attributes(12, 0) = &quot;GEN-TITLE-APPD&quot;: Attributes(12, 1) = &quot;05/02/2008&quot;
Attributes(13, 0) = &quot;GEN-TITLE-ISSM&quot;: Attributes(13, 1) = &quot;Johnny&quot;
Attributes(14, 0) = &quot;GEN-TITLE-DWG&quot;: Attributes(14, 1) = &quot;Drawing1&quot;
Attributes(15, 0) = &quot;GEN-TITLE-DAT&quot;: Attributes(15, 1) = &quot;02/12/2008&quot;
Attributes(16, 0) = &quot;GEN-TITLE-FSCM&quot;: Attributes(16, 1) = &quot;-0125412&quot;
Attributes(17, 0) = &quot;GEN-TITLE-CHKM&quot;: Attributes(17, 1) = &quot;Joe&quot;
Attributes(18, 0) = &quot;GEN-TITLE-NR&quot;: Attributes(18, 1) = &quot;-0123654&quot;
Attributes(19, 0) = &quot;GEN-TITLE-SHEET&quot;: Attributes(19, 1) = &quot;Sheet1&quot;

oDescriptor.Title.Attributes = Attributes

&#39;Set Scale
oDescriptor.TitleBorderScale = 1

&#39;Create New Context
Dim oContext As McadTitleBorderContext
Set oContext = oTitleBMng.NewContext

&#39;oContext.AutomaticPlacement = True
&#39;oContext.BaseScale = True
&#39;oContext.DwgDefault = True
&#39;oContext.MoveObjects = True
&#39;oContext.Rescale = True
&#39;oContext.RetrieveFromAsmProp = True
&#39;oContext.RetrieveFromPartRef = True
&#39;oContext.ThawAllLayers = True

&#39;Create Title Border
Dim oTitleBorder As McadTitleBorder
Set oTitleBorder = oTitleBMng.CreateTitleBorder(oDescriptor, oContext)

End Sub</pre>
<p>The result:</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b00c7a7b8970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Mfgnews2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b019b00c7a7b8970b image-full" src="/assets/image_c105bf.jpg" title="Mfgnews2" /></a></p>
<p>&#0160;</p>
