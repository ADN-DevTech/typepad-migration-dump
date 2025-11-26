---
layout: "post"
title: "Arrange Ribbon buttons into columns"
date: "2012-05-28 08:55:10"
author: "Adam Nagy"
categories:
  - ".NET"
  - "Adam Nagy"
  - "AutoCAD"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/arrange-ribbon-buttons-into-columns.html "
typepad_basename: "arrange-ribbon-buttons-into-columns"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>I would like to organize my Ribbon buttons into 2 columns like this, similar to the "Manage - Applications" panel:</p>

<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b016305eba6d3970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b016305eba6d3970d" alt="Ribbon1" title="Ribbon1" src="/assets/image_529357.jpg" border="0" /></a><br />
<p>How could I do it?</p>
<p><strong>Solution</strong></p>
<p>First of all, having a look at the panels in the CUI dialog gives you an idea how they are built up and also provides the id of the panel, which enables you to check through the API what exact components they contain:</p>

<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0168ebe0f2c0970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b0168ebe0f2c0970c image-full" alt="Ribbon2" title="Ribbon2" src="/assets/image_858952.jpg" border="0" /></a><br />
<p>The below sample also contains a function called IterateContent, which prints out the content of the Ribbon panel whose id you specify.</p>
<p>The list will be printed in the Output window of Visual Studio. Make sure that the "Exceptions" item is unticked in the context menu of that window, otherwise the result will be scattered and a bit difficult to read:</p>

<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b016305eba7c5970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b016305eba7c5970d image-full" alt="Ribbon3" title="Ribbon3" src="/assets/image_199331.jpg" border="0" /></a><br />
<p>Now that you can see the structure of the panel, you can easily organize your items the same way. Here is the full source code:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Imports</span><span style="line-height: 140%;"> Autodesk.AutoCAD.Runtime</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Imports</span><span style="line-height: 140%;"> acApp = Autodesk.AutoCAD.ApplicationServices.</span><span style="line-height: 140%; color: #2b91af;">Application</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Imports</span><span style="line-height: 140%;"> Autodesk.Windows</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Imports</span><span style="line-height: 140%;"> System.Drawing</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Imports</span><span style="line-height: 140%;"> System.IO</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Class</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Class1</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">Implements</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">IExtensionApplication</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">Public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Sub</span><span style="line-height: 140%;"> Initialize() </span><span style="line-height: 140%; color: blue;">Implements</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">IExtensionApplication</span><span style="line-height: 140%;">.Initialize</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; IterateContent()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; CreateRibbon()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Sub</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">Public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Sub</span><span style="line-height: 140%;"> Terminate() </span><span style="line-height: 140%; color: blue;">Implements</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">IExtensionApplication</span><span style="line-height: 140%;">.Terminate</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Sub</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">Private</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Sub</span><span style="line-height: 140%;"> IterateContentRecursive(</span><span style="line-height: 140%; color: blue;">ByVal</span><span style="line-height: 140%;"> obj </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Object</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #2b91af;">Debug</span><span style="line-height: 140%;">.Indent()</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> t </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Type</span><span style="line-height: 140%;"> = obj.GetType()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #2b91af;">Debug</span><span style="line-height: 140%;">.Print(t.Name)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">Try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> coll </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> RibbonItemCollection = _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; t.InvokeMember( _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #a31515;">"Items"</span><span style="line-height: 140%;">, Reflection.</span><span style="line-height: 140%; color: #2b91af;">BindingFlags</span><span style="line-height: 140%;">.GetProperty, _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">Nothing</span><span style="line-height: 140%;">, obj, </span><span style="line-height: 140%; color: blue;">Nothing</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">For</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Each</span><span style="line-height: 140%;"> item </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> RibbonItem </span><span style="line-height: 140%; color: blue;">In</span><span style="line-height: 140%;"> coll</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; IterateContentRecursive(item)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">Next</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">Catch</span><span style="line-height: 140%;"> ex </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> System.</span><span style="line-height: 140%; color: #2b91af;">MissingMethodException</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Try</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #2b91af;">Debug</span><span style="line-height: 140%;">.Unindent()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Sub</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">Private</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Sub</span><span style="line-height: 140%;"> IterateContent()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> ribCntrl </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> RibbonControl = ComponentManager.Ribbon</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">' get the "Manage - Applications" panel</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> panel </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> RibbonPanel = _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ribCntrl.FindPanel(</span><span style="line-height: 140%; color: #a31515;">"ID_PanelScriptsAndMacros"</span><span style="line-height: 140%;">, </span><span style="line-height: 140%; color: blue;">False</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; IterateContentRecursive(panel.Source)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Sub</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">Private</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Sub</span><span style="line-height: 140%;"> CreateRibbon()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> ribCntrl </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> RibbonControl = ComponentManager.Ribbon</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> ribTab </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">New</span><span style="line-height: 140%;"> RibbonTab()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ribTab.Title = </span><span style="line-height: 140%; color: #a31515;">"TestRibbon"</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ribTab.Id = </span><span style="line-height: 140%; color: #a31515;">"TestRibbon"</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ribCntrl.Tabs.Add(ribTab)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ribTab.IsActive = </span><span style="line-height: 140%; color: blue;">True</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; addContent(ribTab)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Sub</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">Private</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Sub</span><span style="line-height: 140%;"> addContent(</span><span style="line-height: 140%; color: blue;">ByVal</span><span style="line-height: 140%;"> ribTab </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> RibbonTab)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> ribSourcePanelTest </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">New</span><span style="line-height: 140%;"> RibbonPanelSource</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ribSourcePanelTest.Title = </span><span style="line-height: 140%; color: #a31515;">"Test Panel"</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> ribPanel </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">New</span><span style="line-height: 140%;"> RibbonPanel()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ribPanel.Source = ribSourcePanelTest</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ribTab.Panels.Add(ribPanel)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">'***Create buttons </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> ribButtonBig </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> RibbonButton = </span><span style="line-height: 140%; color: blue;">New</span><span style="line-height: 140%;"> RibbonButton</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ribButtonBig.IsToolTipEnabled = </span><span style="line-height: 140%; color: blue;">True</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ribButtonBig.ToolTip = </span><span style="line-height: 140%; color: #a31515;">"Big button vertical"</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ribButtonBig.Orientation = Windows.Controls.Orientation.Vertical</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ribButtonBig.LargeImage = LoadImage(</span><span style="line-height: 140%; color: blue;">My</span><span style="line-height: 140%;">.Resources.question)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ribButtonBig.Size = RibbonItemSize.Large</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ribButtonBig.Text = </span><span style="line-height: 140%; color: #a31515;">"Big button vertical"</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ribButtonBig.CommandParameter = </span><span style="line-height: 140%; color: #a31515;">"BigButtonCommand "</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ribButtonBig.ShowText = </span><span style="line-height: 140%; color: blue;">True</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ribButtonBig.Image = LoadImage(</span><span style="line-height: 140%; color: blue;">My</span><span style="line-height: 140%;">.Resources.question)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ribButtonBig.CommandHandler = </span><span style="line-height: 140%; color: blue;">New</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">AdskCommandHandler</span><span style="line-height: 140%;">()</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> ribButtonSmall1 </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> RibbonButton = </span><span style="line-height: 140%; color: blue;">New</span><span style="line-height: 140%;"> RibbonButton</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ribButtonSmall1.IsToolTipEnabled = </span><span style="line-height: 140%; color: blue;">True</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ribButtonSmall1.ToolTip = </span><span style="line-height: 140%; color: #a31515;">"Small button horizontal"</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ribButtonSmall1.Orientation = Windows.Controls.Orientation.Horizontal</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ribButtonSmall1.LargeImage = LoadImage(</span><span style="line-height: 140%; color: blue;">My</span><span style="line-height: 140%;">.Resources.question)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ribButtonSmall1.Size = RibbonItemSize.Standard</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ribButtonSmall1.Text = </span><span style="line-height: 140%; color: #a31515;">"Small button horizontal"</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ribButtonSmall1.CommandParameter = </span><span style="line-height: 140%; color: #a31515;">"SmallButton1 "</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ribButtonSmall1.ShowText = </span><span style="line-height: 140%; color: blue;">True</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ribButtonSmall1.Image = LoadImage(</span><span style="line-height: 140%; color: blue;">My</span><span style="line-height: 140%;">.Resources.questionsmall)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ribButtonSmall1.CommandHandler = </span><span style="line-height: 140%; color: blue;">New</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">AdskCommandHandler</span><span style="line-height: 140%;">()</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> ribButtonSmall2 </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> RibbonButton = ribButtonSmall1.Clone()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ribButtonSmall2.CommandParameter = </span><span style="line-height: 140%; color: #a31515;">"SmallButton2 "</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> ribButtonSmall3 </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> RibbonButton = ribButtonSmall1.Clone()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ribButtonSmall3.CommandParameter = </span><span style="line-height: 140%; color: #a31515;">"SmallButton3 "</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> ribButtonSmall4 </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> RibbonButton = ribButtonSmall1.Clone()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ribButtonSmall4.CommandParameter = </span><span style="line-height: 140%; color: #a31515;">"SmallButton4 "</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> ribButtonSmall5 </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> RibbonButton = ribButtonSmall1.Clone()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ribButtonSmall5.CommandParameter = </span><span style="line-height: 140%; color: #a31515;">"SmallButton5 "</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> ribButtonSmall6 </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> RibbonButton = ribButtonSmall1.Clone()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ribButtonSmall6.CommandParameter = </span><span style="line-height: 140%; color: #a31515;">"SmallButton6 "</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">'add the buttons in an organized way:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">' first the big button then the small ones placed in two separate columns</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> subPanel1 </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">New</span><span style="line-height: 140%;"> RibbonRowPanel</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; subPanel1.Items.Add(ribButtonSmall1)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; subPanel1.Items.Add(</span><span style="line-height: 140%; color: blue;">New</span><span style="line-height: 140%;"> RibbonRowBreak)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; subPanel1.Items.Add(ribButtonSmall2)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; subPanel1.Items.Add(</span><span style="line-height: 140%; color: blue;">New</span><span style="line-height: 140%;"> RibbonRowBreak)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; subPanel1.Items.Add(ribButtonSmall3)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> subPanel2 </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">New</span><span style="line-height: 140%;"> RibbonRowPanel</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; subPanel2.Items.Add(ribButtonSmall4)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; subPanel2.Items.Add(</span><span style="line-height: 140%; color: blue;">New</span><span style="line-height: 140%;"> RibbonRowBreak)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; subPanel2.Items.Add(ribButtonSmall5)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; subPanel2.Items.Add(</span><span style="line-height: 140%; color: blue;">New</span><span style="line-height: 140%;"> RibbonRowBreak)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; subPanel2.Items.Add(ribButtonSmall6)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ribSourcePanelTest.Items.Add(ribButtonBig)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ribSourcePanelTest.Items.Add(subPanel1)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ribSourcePanelTest.Items.Add(subPanel2)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Sub</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">Private</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Shared</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Function</span><span style="line-height: 140%;"> LoadImage(</span><span style="line-height: 140%; color: blue;">ByVal</span><span style="line-height: 140%;"> imageToLoad </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Bitmap</span><span style="line-height: 140%;">) _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> Windows.Media.Imaging.BitmapImage</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> pic </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Bitmap</span><span style="line-height: 140%;"> = imageToLoad</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> ms </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">New</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">MemoryStream</span><span style="line-height: 140%;">()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; pic.Save(ms, Imaging.</span><span style="line-height: 140%; color: #2b91af;">ImageFormat</span><span style="line-height: 140%;">.Png)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> bi </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">New</span><span style="line-height: 140%;"> Windows.Media.Imaging.BitmapImage()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; bi.BeginInit()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; bi.StreamSource = ms</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; bi.EndInit()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">Return</span><span style="line-height: 140%;"> bi</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Function</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Class</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Class</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">AdskCommandHandler</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">Implements</span><span style="line-height: 140%;"> System.Windows.Input.ICommand</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">Public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Function</span><span style="line-height: 140%;"> CanExecute(</span><span style="line-height: 140%; color: blue;">ByVal</span><span style="line-height: 140%;"> parameter </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Object</span><span style="line-height: 140%;">) _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Boolean</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Implements</span><span style="line-height: 140%;"> System.Windows.Input.ICommand.CanExecute</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">Return</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">True</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Function</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">Public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Event</span><span style="line-height: 140%;"> CanExecuteChanged( _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">ByVal</span><span style="line-height: 140%;"> sender </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Object</span><span style="line-height: 140%;">, </span><span style="line-height: 140%; color: blue;">ByVal</span><span style="line-height: 140%;"> e </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> System.</span><span style="line-height: 140%; color: #2b91af;">EventArgs</span><span style="line-height: 140%;">) _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">Implements</span><span style="line-height: 140%;"> System.Windows.Input.ICommand.CanExecuteChanged</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">Public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Sub</span><span style="line-height: 140%;"> Execute(</span><span style="line-height: 140%; color: blue;">ByVal</span><span style="line-height: 140%;"> parameter </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Object</span><span style="line-height: 140%;">) _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">Implements</span><span style="line-height: 140%;"> System.Windows.Input.ICommand.Execute</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> ribBtn </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> RibbonButton = </span><span style="line-height: 140%; color: blue;">TryCast</span><span style="line-height: 140%;">(parameter, RibbonButton)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">If</span><span style="line-height: 140%;"> ribBtn </span><span style="line-height: 140%; color: blue;">IsNot</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Nothing</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #2b91af;">acApp</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument.SendStringToExecute( _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ribBtn.CommandParameter, </span><span style="line-height: 140%; color: blue;">True</span><span style="line-height: 140%;">, </span><span style="line-height: 140%; color: blue;">False</span><span style="line-height: 140%;">, </span><span style="line-height: 140%; color: blue;">True</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">If</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Sub</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Class</span></p>
</div>
<p>And here is the result:</p>

<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0168ebe0f409970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b0168ebe0f409970c" alt="Ribbon4" title="Ribbon4" src="/assets/image_518861.jpg" border="0" /></a><br />
