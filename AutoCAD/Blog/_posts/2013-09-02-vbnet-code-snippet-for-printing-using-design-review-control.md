---
layout: "post"
title: "VB.NET code snippet for printing using Design Review Control"
date: "2013-09-02 13:59:59"
author: "Gopinath Taget"
categories:
  - ".NET"
  - "ActiveX"
  - "DWF"
  - "Gopinath Taget"
original_url: "https://adndevblog.typepad.com/autocad/2013/09/vbnet-code-snippet-for-printing-using-design-review-control.html "
typepad_basename: "vbnet-code-snippet-for-printing-using-design-review-control"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/gopinath-taget.html">Gopinath Taget</a></p>  <p>If you are wondering how you could use the Autodesk Design Review control to print a DWF file in a .NET App, here is a code snippet that will help:</p>  <p><font size="1" face="Calibri">Private Sub SimplePrintButton_Click(</font></p>  <p><font size="1" face="Calibri">ByVal sender As System.Object, </font></p>  <p><font size="1" face="Calibri">ByVal e As System.EventArgs) Handles SimplePrintButton.Click</font></p>  <p><font size="1" face="Calibri">&#160;&#160;&#160;&#160;&#160;&#160;&#160; Dim ctrl As ExpressViewerDll.CExpressViewerControl     <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; ctrl = AxCExpressViewerControl1.GetOcx()</font></p>  <p><font size="1" face="Calibri">&#160;&#160;&#160;&#160;&#160;&#160;&#160; Dim plt As EPlotViewer.IAdEPlotViewer     <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; plt = ctrl.DocumentHandler      <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; plt.SimplePrint(True, True)      <br />End Sub</font></p>  <p>Please do make sure you have the “DwfComposer EplotViewer 1.0 Type Library” COM library referenced in your application.</p>
