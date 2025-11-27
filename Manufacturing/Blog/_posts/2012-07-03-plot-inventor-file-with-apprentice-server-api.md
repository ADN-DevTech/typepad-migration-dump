---
layout: "post"
title: "Plot Inventor File with Apprentice Server API"
date: "2012-07-03 00:15:36"
author: "Barbara Han"
categories:
  - "Barbara Han"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/07/plot-inventor-file-with-apprentice-server-api.html "
typepad_basename: "plot-inventor-file-with-apprentice-server-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/barbara-han.html" target="_self">Barbara Han</a></p>  <p><b>Issue</b></p>  <p>I have some Inventor files (e.g. IDW files) in Vault, and I want to plot them in a specific sequence from my Vault extension application, but I don't find the plot function in Vault API help document. Is it possible to plot file without Inventor installed?</p>  <p><a name="section2"></a></p>  <p><b>Solution</b></p>  <p>Vault self has not plot API. What you see from Vault Explorer or other Client is implemented by Vault Client application. </p>  <p>Inventor’s Apprentice server supports plot API. Apprentice server API is available from Inventor or Inventor View. It’s possible to plot without Inventor installed, but you need to have Inventor view installed. The Inventor view is free tool. You can download it from: <a href="http://www.autodesk.com/inventorview">www.autodesk.com/inventorview</a></p>  <p>Here is a VB.NET sample of calling plot function of apprentice server API:</p>  <p><font size="1" face="Courier New">Dim oDoc As ApprenticeServerDrawingDocument = oApprenticeApp.Open(“C:\Temp\MyDrawing.idw”)     <br />Dim oPrintMng As ApprenticeDrawingPrintManager = oDoc.PrintManager      <br />oPrintMng.Printer = &quot;Autodesk DWF Writer&quot;      <br />oPrintMng.NumberOfCopies = 1      <br />oPrintMng.SubmitPrint</font>    <br />You will need to modify the “Printer” property to your own printer’s name. Also you can try to change other PrintManager properties, such as PaperSize, Height, Width etc., which I’m not talking about here.</p>
