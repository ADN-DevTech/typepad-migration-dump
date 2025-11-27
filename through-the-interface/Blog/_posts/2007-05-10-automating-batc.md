---
layout: "post"
title: "Automating batch printing of DWF files using Design Review 2008"
date: "2007-05-10 11:26:18"
author: "Kean Walmsley"
categories:
  - "Batch processing"
  - "DWF"
  - "Plotting"
original_url: "https://www.keanw.com/2007/05/automating_batc.html "
typepad_basename: "automating_batc"
typepad_status: "Publish"
---

<p>We sometimes receive questions on how best to automate the printing of DWF files. Autodesk Design Review 2008 now has a new <a href="http://usa.autodesk.com/adsk/servlet/mform?siteID=123112&amp;id=8837987">Batch Print Plug-in</a> enabling just this.</p>

<p>Once you've installed the plug-in, you'll be able to use the Batch Print Wizard in Design Review (on the file menu, once a DWF has been loaded). This Wizard allows you to configure a batch job for Design Review to process and save it to a BPJ (Batch Print Job) file. This BPJ (which is basically a very simple XML file) can then be used to drive the batch print process automatically.</p>

<p>The next logical step is clearly to create the BPJ programmatically, which can be done using a number of XML libraries (MS XML etc.) or simply by writing out a text file using your favourite string output routines.</p>

<p>So let's look at creating the BPJ file &quot;manually&quot; using the Batch Print Wizard.</p>

<p>Here's the second screen you'll see, once you've skipped the welcome:</p>

<p><a onclick="window.open(this.href, '_blank', 'width=654,height=399,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/2007/05/10/batch_print_wizard_2.png"><img title="Batch_print_wizard_2" height="183" alt="Batch_print_wizard_2" src="/assets/batch_print_wizard_2.png" width="300" border="0" /></a> </p>

<p>When you've selected the files you wish to print and added them to the right-hand side using the &quot;&gt;&quot; button, you can move on to the next page:</p>

<p><a onclick="window.open(this.href, '_blank', 'width=654,height=399,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/2007/05/10/batch_print_wizard_3.png"><img title="Batch_print_wizard_3" height="183" alt="Batch_print_wizard_3" src="/assets/batch_print_wizard_3.png" width="300" border="0" /></a> </p>

<p>This is where you modify the print setup for each of your DWFs, by clicking on the item and hitting &quot;Print Setup&quot;, or just double-clicking the item.</p>

<p><a onclick="window.open(this.href, '_blank', 'width=654,height=399,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/2007/05/10/batch_print_wizard_4.png"><img title="Batch_print_wizard_4" height="183" alt="Batch_print_wizard_4" src="/assets/batch_print_wizard_4.png" width="300" border="0" /></a> </p>

<p>And then you're ready to save the BPJ file (or just &quot;Print&quot; directly).</p>

<p>I saved mine as &quot;c:\My Documents\MyFiles.bpj&quot;, and here are its contents, with additional whitespace added for readability:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&lt;</span><span style="COLOR: maroon">configuration_file</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &lt;</span><span style="COLOR: maroon">DWF_File</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; </span><span style="COLOR: red">FileName</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">C:\My Documents\\First.dwf</span>&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; </span><span style="COLOR: red">PageSize</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">11.0 x 8.5 in</span>&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; </span><span style="COLOR: red">NoOfSections</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">2</span>&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; </span><span style="COLOR: red">Print_to_scale</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">100</span>&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; </span><span style="COLOR: red">User_defined_Zoom_Factor</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">100</span>&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; </span><span style="COLOR: red">Print_Style</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">0</span>&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; </span><span style="COLOR: red">Print_What</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">0</span>&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; </span><span style="COLOR: red">Fit_To_Paper</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">1</span>&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; </span><span style="COLOR: red">Paper_Size</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">9</span>&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; </span><span style="COLOR: red">Paper_Size_Width</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">2100</span>&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; </span><span style="COLOR: red">Paper_Size_Height</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">2970</span>&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; </span><span style="COLOR: red">Orientation</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">2</span>&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; </span><span style="COLOR: red">Number_of_copies</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">1</span>&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; </span><span style="COLOR: red">PrinterName</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">\\beihpa001\BEIPRN003</span>&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; </span><span style="COLOR: red">Page_Range</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">1</span>&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; </span><span style="COLOR: red">Print_Range_Str</span><span style="COLOR: blue">=</span>&quot;&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; </span><span style="COLOR: red">Reverse_Order</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">0</span>&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; </span><span style="COLOR: red">Collate</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">0</span>&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; </span><span style="COLOR: red">printColor</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">0</span>&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; </span><span style="COLOR: red">printAlignment</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">4</span>&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; </span><span style="COLOR: red">Use_DWF_Paper_Size</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">0</span>&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; </span><span style="COLOR: red">PaperName</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">A4</span>&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; </span><span style="COLOR: red">useHPIP</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">0</span>&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; </span><span style="COLOR: red">HPIPMediaID</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">-1</span>&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; </span><span style="COLOR: red">HPIPExcludeEModel</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">1</span>&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; </span><span style="COLOR: red">HPIPPaperName</span><span style="COLOR: blue">=</span>&quot;&quot;<span style="COLOR: blue">/&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &lt;</span><span style="COLOR: maroon">DWF_File</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; </span><span style="COLOR: red">FileName</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">C:\My Documents\\Second.dwf</span>&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; </span><span style="COLOR: red">PageSize</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">11.0 x 8.5 in</span>&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; </span><span style="COLOR: red">NoOfSections</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">2</span>&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; </span><span style="COLOR: red">Print_to_scale</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">100</span>&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; </span><span style="COLOR: red">User_defined_Zoom_Factor</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">100</span>&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; </span><span style="COLOR: red">Print_Style</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">0</span>&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; </span><span style="COLOR: red">Print_What</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">0</span>&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; </span><span style="COLOR: red">Fit_To_Paper</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">1</span>&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; </span><span style="COLOR: red">Paper_Size</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">9</span>&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; </span><span style="COLOR: red">Paper_Size_Width</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">2100</span>&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; </span><span style="COLOR: red">Paper_Size_Height</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">2970</span>&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; </span><span style="COLOR: red">Orientation</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">2</span>&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; </span><span style="COLOR: red">Number_of_copies</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">1</span>&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; </span><span style="COLOR: red">PrinterName</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">\\beihpa001\BEIPRN003</span>&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; </span><span style="COLOR: red">Page_Range</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">1</span>&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; </span><span style="COLOR: red">Print_Range_Str</span><span style="COLOR: blue">=</span>&quot;&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; </span><span style="COLOR: red">Reverse_Order</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">0</span>&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; </span><span style="COLOR: red">Collate</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">0</span>&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; </span><span style="COLOR: red">printColor</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">0</span>&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; </span><span style="COLOR: red">printAlignment</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">4</span>&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; </span><span style="COLOR: red">Use_DWF_Paper_Size</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">0</span>&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; </span><span style="COLOR: red">PaperName</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">A4</span>&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; </span><span style="COLOR: red">useHPIP</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">0</span>&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; </span><span style="COLOR: red">HPIPMediaID</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">-1</span>&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; </span><span style="COLOR: red">HPIPExcludeEModel</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">1</span>&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; </span><span style="COLOR: red">HPIPPaperName</span><span style="COLOR: blue">=</span>&quot;&quot;<span style="COLOR: blue">/&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">configuration_file</span><span style="COLOR: blue">&gt;</span></p></div>

<p>This is clearly straightforward to create programmatically from your application. The next question is how best to automate the print process, now we have our BPJ.</p>

<p>The simplest way is to call the Design Review executable with the BPJ file as a command-line argument:</p>

<p><a onclick="window.open(this.href, '_blank', 'width=669,height=158,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/2007/05/10/batch_print_using_design_review.png"><img title="Batch_print_using_design_review" height="70" alt="Batch_print_using_design_review" src="/assets/batch_print_using_design_review.png" width="300" border="0" /></a> </p>

<p>The executable returns straight away, and you'll see a log file created in the same location as the BPJ (in my case &quot;c:\My Documents\MyFiles.log&quot;), detailing the results of the print job:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Batch Printing initiated with Configuration file: &quot;\My Documents\MyFiles.bpj&quot;. Start Time: Thu May 10 17:16:35 2007</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; Opening DWF file : &quot;C:\My Documents\\First.dwf&quot;. Start Time: Thu May 10 17:16:35 2007</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; DWF file: &quot;C:\My Documents\\First.dwf&quot; Opened successfully. End Time: Thu May 10 17:16:37 2007</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; Status:DWF Open Successful </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; Started Printing DWF File: &quot;C:\My Documents\\First.dwf&quot;. Start Time: Thu May 10 17:16:39 2007</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;Printing To&nbsp; &nbsp; : \\beihpa001\BEIPRN003</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;HPIP set to&nbsp; &nbsp; : False</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;Paper Size&nbsp; &nbsp; : 2100 X 2970</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;Print Range&nbsp; &nbsp; : Current Page</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;No Of Copies&nbsp; : 1</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;Scale&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; : 100 %, Tile Pages (Not Applicable For 3D Sections)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;Print Alignment: LowerLeft (Not Applicable For 3D Sections)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;Orientation&nbsp; &nbsp; : Landscape</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;Print Color&nbsp; &nbsp; : Color</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;Started Printing Section Number : 0. Start Time: Thu May 10 17:16:39 2007</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;Finished Printing Section Number: 0. End Time&nbsp; : Thu May 10 17:16:42 2007</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;Started Printing Section Number : 1. Start Time: Thu May 10 17:16:43 2007</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;Finished Printing Section Number: 1. End Time&nbsp; : Thu May 10 17:16:43 2007</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; Finished Printing DWF file: &quot;C:\My Documents\\First.dwf&quot; . End Time: Thu May 10 17:16:43 2007</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; Opening DWF file : &quot;C:\My Documents\\Second.dwf&quot;. Start Time: Thu May 10 17:16:43 2007</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; DWF file: &quot;C:\My Documents\\Second.dwf&quot; Opened successfully. End Time: Thu May 10 17:16:45 2007 </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; Status:DWF Open Successful </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; Started Printing DWF File: &quot;C:\My Documents\\Second.dwf&quot;. Start Time: Thu May 10 17:16:46 2007</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;Printing To&nbsp; &nbsp; : \\beihpa001\BEIPRN003</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;HPIP set to&nbsp; &nbsp; : False</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;Paper Size&nbsp; &nbsp; : 2100 X 2970</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;Print Range&nbsp; &nbsp; : Current Page</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;No Of Copies&nbsp; : 1</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;Scale&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; : 100 %, Tile Pages (Not Applicable For 3D Sections)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;Print Alignment: LowerLeft (Not Applicable For 3D Sections)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;Orientation&nbsp; &nbsp; : Landscape</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;Print Color&nbsp; &nbsp; : Color</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;Started Printing Section Number : 0. Start Time: Thu May 10 17:16:46 2007</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;Finished Printing Section Number: 0. End Time&nbsp; : Thu May 10 17:16:49 2007</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;Started Printing Section Number : 1. Start Time: Thu May 10 17:16:49 2007</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;Finished Printing Section Number: 1. End Time&nbsp; : Thu May 10 17:16:50 2007</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; Finished Printing DWF file: &quot;C:\My Documents\\Second.dwf&quot; . End Time: Thu May 10 17:16:50 2007</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Batch Printing Finished with Configuration file: &quot;\My Documents\MyFiles.bpj&quot;. End Time: Thu May 10 17:16:50 2007</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Total Time Taken to complete Batch Print Process: 0 hr: 0 Min: 15 Sec.</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p></div>

<p>This log file can be parsed programmatically if it's important for your application to know whether any pages had difficulty printing.</p>
