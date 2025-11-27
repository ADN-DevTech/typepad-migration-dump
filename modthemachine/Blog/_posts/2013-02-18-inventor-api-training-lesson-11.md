---
layout: "post"
title: "Inventor API Training &ndash; Lesson 11"
date: "2013-02-18 18:47:49"
author: "Wayne Brill"
categories:
  - "Inventor"
  - "Training Material"
  - "Wayne"
original_url: "https://modthemachine.typepad.com/my_weblog/2013/02/inventor-api-training-lesson-11.html "
typepad_basename: "inventor-api-training-lesson-11"
typepad_status: "Publish"
---

<p>Here is section eleven of the Inventor API training. In this section we cover how to use the API to print and translate files. There are&#0160; 21 sections to this training and we decided that they did not need to be posted in order. (<a href="http://modthemachine.typepad.com/my_weblog/2013/02/inventor-api-training-lesson-1.html" target="_blank">Section one is here</a>). I am going to alternate posts between sections 1-10 and 10-21.</p>
<p><strong>Printing and Translating Files</strong></p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017d412423de970c-pi"><img alt="image" border="0" height="260" src="/assets/image_628576.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="344" /></a></p>
<p>Here is the agenda for this section. Be sure to see the Lab instructions at the end of this section. (completed sample is available)</p>
<p><em>PrintManager Object <br />DrawingPrintManager <br />Printing Example <br />File Translation <br />Accessing Translators Options <br />DataIO Object</em></p>
<p><strong>PrintManager Object</strong></p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017ee897fce6970d-pi"><img alt="image" border="0" height="327" src="/assets/image_292935.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="441" /></a></p>
<p>The PrintManager object can be accessed from the different types of documents such as an part or assembly. Also Drawings and Apprentice support special print managers that provide additional printing capabilities.These PrintManager objects provide that same settings that are available in the Print command dialogs in the user interface.</p>
<p><strong>PrintManager - Main Properties <br /></strong>Here are the main properties and methods of the PrintManager object.</p>
<p><em>NumberOfCopies</em>: Default value to 1 <br /><em>PaperHeight / PaperWidth</em>: Sets the paper dimensions. This property is only used when the PaperSize property is set to kPaperSizeCustom <br /><em>Printer</em>:&#0160; gets and sets the name of the printing device</p>
<p><em>PrintToFile</em>(String FileName): Prints to the specified file using the current property settings <br /><em>SubmitPrint</em>: Prints the file using the current property settings</p>
<p><br /><strong>DrawingPrintManager Object</strong> <br />The DrawingPrintManager object derives from PrintManager and has some additional properties and methods specific to drawing documents.</p>
<p><em>AllColorsAsBlack:</em> No colors in the print <br /><em>PrintRange</em>: Can be set to kPrintCurrentSheet, kPrintAllSheets, kPrintSheetRange. <br /><em>ScaleMode / Scale</em>:&#0160; Gets and sets how the scale of the print is defined. <br /><em>RemoveLineWeights:</em> All lines will have the same default width</p>
<p><em>GetSheetRange / SetSheetRange</em>:&#0160; Gets/sets the sheet range to print.</p>
<p><strong>Print Sample VB.NET</strong></p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;"><span style="color: blue;">Public</span> <span style="color: blue;">Sub</span> PrintSample()</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oDrawDoc <span style="color: blue;">As</span> <span style="color: #2b91af;">DrawingDocument</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oDrawDoc = mApp.ActiveDocument</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oPrintMgr <span style="color: blue;">As</span> <span style="color: #2b91af;">DrawingPrintManager</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oPrintMgr = oDrawDoc.PrintManager</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">With</span> oPrintMgr</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; .Printer = <span style="color: #a31515;">&quot;\\ADSOREPS1\oreprn001&quot;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; .NumberOfCopies = 1</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; .ScaleMode = _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">PrintScaleModeEnum</span>.kPrintFullScale</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; .PaperSize = _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">PaperSizeEnum</span>.kPaperSize11x17</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; .SubmitPrint()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">With</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">End</span> <span style="color: blue;">Sub</span></p>
</div>
<p><strong>&#0160;</strong></p>
<p><strong>File Translation </strong></p>
<p><strong>Simple Translation using Open and SaveAs</strong> <br />You can use the Document.SaveAs and Documents.Open methods to translate documents in and out of Inventor. The extension of the filename supplied will be used to determine which translator to use. With this approach default settings are used. The default settings are the settings last used when interactively translating a file. In this code snippet an stp file is opened and a document is saved as and AutoCAD dwg file.</p>
<p><em>Documents.Open( “C:\Temp\Test.stp” ) <br />Document.SaveAs( “C:\Temp\New.dwg”, True )</em></p>
<p><strong>File Translation using the translator Add-Ins</strong> <br />If you want to control the settings when translating files you use the Translator Add-Ins. The main properties and methods of translator add-ins are these:</p>
<p><em>HasOpenOptions</em>: Returns whether or not the translator has options available for opening files. The three arguments (DataMedium, TranslationContext, NameValueMap ) contain settings you can use.</p>
<p><em>Open</em>: Opens (translates) a file. This method takes four objects as arguments that control how the open will work.</p>
<blockquote>
<p><em>DataMedium</em>: Use the FileName property to set the file name to open.</p>
</blockquote>
<blockquote>
<p><em>TranslationContext</em>: For file, set Type to kFileBrowseIOMechanism you can place the translated file into an existing document with the OpenIntoExisting property</p>
</blockquote>
<blockquote>
<p><em>NameValueMap</em>: Use this to set the options for the open</p>
</blockquote>
<blockquote>
<p><em>Object</em>: The object that will be created - usually a document</p>
</blockquote>
<p><em>HasSaveCopyAsOptions</em>: Returns whether or not the translator has options available for saving files. The three arguments (Object, TranslationContext, NameValueMap ) will contain settings you can use.</p>
<p><em>SaveCopyAs</em>: Save the document to the specified data-source. This method takes the following arguments:</p>
<blockquote>
<p><em>Object</em>: The document to be saved <br /><em>TranslationContext</em>: For file set Type to&#0160; kFileBrowseIOMechanism&#0160;</p>
</blockquote>
<blockquote>
<p><em>NameValueMap</em>: Use this to set the options for the translation <br /><em>DataMedium</em>: Use the FileName property to set the file name to save</p>
<p>&#0160;</p>
</blockquote>
<p>Each of the translator Add-Ins have a GUID. You can use this identifier (String) with the <em>ItemById</em> method of the <em>ApplicationAddIns</em> property of the Inventor Application object to instantiate the translator. The following example gets the STEP translator and uses the HasOpenOptions() method to print the options (that could be set before using the Open method) to the immediate window.</p>
<p><strong>Accessing Translator’s Open Options – VB.NET</strong></p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;"><span style="color: blue;">Public</span> <span style="color: blue;">Sub</span> GetTranslatorOpenOptions()</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39;Using the GUID of the STEP Translator</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> clsId <span style="color: blue;">As</span> <span style="color: blue;">String</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; clsId = <span style="color: #a31515;">&quot;{90AF7F40-0C01-11D5-8E83-0010B541CD80}&quot;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oTranslator <span style="color: blue;">As</span> <span style="color: #2b91af;">TranslatorAddIn</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oTranslator = _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; mApp.ApplicationAddIns.ItemById(clsId)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> medium <span style="color: blue;">As</span> <span style="color: #2b91af;">DataMedium</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; medium = _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; mApp.TransientObjects.CreateDataMedium</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; medium.FileName = &quot;C:\MyInventorDoc.xxx&quot;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; medium.MediumType = _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">MediumTypeEnum</span>.kFileNameMedium</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> context <span style="color: blue;">As</span> <span style="color: #2b91af;">TranslationContext</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; context = mApp.TransientObjects.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; CreateTranslationContext</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> options <span style="color: blue;">As</span> <span style="color: #2b91af;">NameValueMap</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; options = mApp.TransientObjects.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; CreateNameValueMap</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> index <span style="color: blue;">As</span> <span style="color: blue;">Integer</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">If</span> oTranslator.HasOpenOptions _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (medium, context, options) <span style="color: blue;">Then</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">For</span> index = 1 <span style="color: blue;">To</span> options.Count</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">Debug</span>.Print(options.Name(index) _</p>
<p style="margin: 0px;">&amp; <span style="color: #a31515;">&quot; = &quot;</span> &amp; options.Value(options.Name(index)))</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Next</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">End</span> <span style="color: blue;">Sub</span></p>
</div>
<p><strong>Accessing Translator’s Save Options - VB.NET</strong></p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;"><span style="color: blue;">Public</span> <span style="color: blue;">Sub</span> GetTranslatorSaveAsOptions()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; Using GUID of the DWF Translator</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> clsId <span style="color: blue;">As</span> <span style="color: blue;">String</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; clsId = <span style="color: #a31515;">&quot;{0AC6FD95-2F4D-42CE-8BE0-8AEA580399E4}&quot;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oTranslator <span style="color: blue;">As</span> <span style="color: #2b91af;">TranslatorAddIn</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oTranslator =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; mApp.ApplicationAddIns.ItemById(clsId)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> context <span style="color: blue;">As</span> <span style="color: #2b91af;">TranslationContext</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; context =</p>
<p style="margin: 0px;">&#0160;&#0160; mApp.TransientObjects.CreateTranslationContext</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; context.Type =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">IOMechanismEnum</span>.kUnspecifiedIOMechanism</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> options <span style="color: blue;">As</span> <span style="color: #2b91af;">NameValueMap</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; options =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; mApp.TransientObjects.CreateNameValueMap</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> SourceObject <span style="color: blue;">As</span> <span style="color: blue;">Object</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; SourceObject = mApp.ActiveDocument</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> index <span style="color: blue;">As</span> <span style="color: blue;">Integer</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">If</span> oTranslator.HasSaveCopyAsOptions _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (SourceObject, context, options) <span style="color: blue;">Then</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">For</span> index = 1 <span style="color: blue;">To</span> options.Count</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">Debug</span>.Print(options.Name(index) &amp;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&quot; = &quot;</span> &amp; options.Value(options.Name(index)))</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Next</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">End</span> <span style="color: blue;">Sub</span></p>
</div>
<p><strong>Inventor 2013: Built-in Translator Add-Ins GUIDs</strong></p>
<p>DWF:&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {0AC6FD95-2F4D-42CE-8BE0-8AEA580399E4} <br />PDF:&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {0AC6FD96-2F4D-42CE-8BE0-8AEA580399E4} <br />DWFx:&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {0AC6FD97-2F4D-42CE-8BE0-8AEA580399E4} <br />JT:&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {16625A0E-F58C-4488-A969-E7EC4F99CACD} <br />i-drop:&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {21DB88B0-BFBF-11D4-8DE6-0010B541CAA8} <br />CATIA Part:&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {2FEE4AE5-36D3-4392-89C7-58A9CD14D305} <br />SolidWorks:&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {402BE503-725D-41CB-B746-D557AB83BAF1} <br />Pro/E:&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {46D96B7A-CF8A-49C9-8703-2F40CFBDF547} <br />STL:&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {533E9A98-FC3B-11D4-8E7E-0010B541CD80} <br />DWF:&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {55EBD0FA-EF60-4028-A350-502CA148B499} <br />Pro/E Granite:&#0160;&#0160;&#0160; {66CB2667-73AD-401C-A531-64EC701825A1} <br />IDF:&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {6C5BBC04-5D6F-4353-94B1-060CD6554444} <br />SAT:&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {89162634-02B6-11D5-8E80-0010B541CD80} <br />CATIA Product: {8A88FC01-0C32-4B3E-BE12-DDC8DF6FFF18} <br />Pro/E Neutral&#0160;&#0160;&#0160;&#0160; {8CEC09E3-D638-4E8F-A6E1-0D1E1A5FC8E3} <br />CATIA Import:&#0160;&#0160;&#0160; {8D1717FA-EB24-473C-8B0F-0F810C4FC5A8}Parasolid Text:&#0160;&#0160; {8F9D3571-3CB8-42F7-8AFF-2DB2779C8465} <br />STEP:&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {90AF7F40-0C01-11D5-8E83-0010B541CD80} <br />IGES:&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {90AF7F44-0C01-11D5-8E83-0010B541CD80} <br />UGS NX:&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {93D506C4-8355-4E28-9C4E-C2B5F1EDC6AE} <br />Content Center&#0160;&#0160; {A547F528-D239-475F-8FC6-8F97C4DB6746} <br />Parasolid Binary:{A8F8F8E5-BBAB-4F74-8B1B-AC011251F8AC} <br />Drag &amp; Drop&#0160;&#0160;&#0160;&#0160;&#0160; {B95D705C-E915-4A5B-A498-E73AC98923A2} <br />DWG:&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {C24E3AC2-122E-11D5-8E91-0010B541CD80} <br />DXF:&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {C24E3AC4-122E-11D5-8E91-0010B541CD80} <br />Alias:&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {DC5CD10A-F6D1-4CA3-A6E3-42A6D646B03E}</p>
<p><strong>DataIO Object <br /></strong>Some Inventor objects support a DataIO object. You can use this object to get input and output of formatted data. Use the GetInputFormats / GetOutputFormats methods to get the list of formats supported by a DataIO objects (ex DXF, DWG for sketches, FlatPattern, XML for iProperties, SAT for worksurfaces,…)</p>
<p><strong>DataIO VB.NET examples</strong></p>
<p>&#0160;</p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Public</span> <span style="color: blue;">Sub</span> ExportWorkSurface()</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oWorkSurfaces <span style="color: blue;">As</span> <span style="color: #2b91af;">WorkSurfaces</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oWorkSurfaces = mApp.ActiveDocument.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ComponentDefinition.WorkSurfaces</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oDataIO <span style="color: blue;">As</span> <span style="color: #2b91af;">DataIO</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oDataIO = oWorkSurfaces(1).</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; SurfaceBodies(1).DataIO</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oDataIO.WriteDataToFile _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #a31515;">&quot;ACIS SAT&quot;</span>, <span style="color: #a31515;">&quot;C:\Temp\result.sat&quot;</span>)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">Sub</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Public</span> <span style="color: blue;">Sub</span> ExportSketchDXF()</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oSketch <span style="color: blue;">As</span> <span style="color: #2b91af;">PlanarSketch</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oSketch = mApp.ActiveDocument.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ComponentDefinition.Sketches(1)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oDataIO <span style="color: blue;">As</span> <span style="color: #2b91af;">DataIO</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oDataIO = oSketch.DataIO</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Call</span> oDataIO.WriteDataToFile _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #a31515;">&quot;DXF&quot;</span>, <span style="color: #a31515;">&quot;C:\Temp\dxfout.dxf&quot;</span>)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">Sub</span></p>
</div>
<p><strong>Lab: Access the Translator Add-In options</strong> <br />Create a windows forms application. On the form have a ListView, a ComboBox and a Button. In the ComboBox allow the user to select the name of a Translator. When the button is clicked populate the ListView with the open options and save options for the selected Translator Add-In. Here are examples of the completed Lab. (VB.NET and C#)&#0160;</p>
<p>&#0160; <span class="asset  asset-generic at-xid-6a00e553fcbfc68834017c36f4d914970b"><a href="http://modthemachine.typepad.com/files/inventor_training_module_11_samples.zip">Download Inventor_Training_Module_11_Samples</a></span></p>
<p>-Wayne</p>
