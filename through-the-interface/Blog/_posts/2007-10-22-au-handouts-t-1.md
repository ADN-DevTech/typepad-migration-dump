---
layout: "post"
title: "AU Handouts: There's More to .DWG Than AutoCAD® - Part 2"
date: "2007-10-22 17:12:39"
author: "Kean Walmsley"
categories:
  - "AU"
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Training"
  - "Visual Studio"
original_url: "https://www.keanw.com/2007/10/au-handouts-t-1.html "
typepad_basename: "au-handouts-t-1"
typepad_status: "Publish"
---

<p><em>[This post continues from <a href="http://through-the-interface.typepad.com/through_the_interface/2007/10/au-handouts-the.html">the last post</a>, which I've been back and modified slightly since it was posted.]</em></p>

<p><strong>Using the DWG Thumbnail in a simple application</strong></p>

<p>Thumbnail images, when they exist inside a drawing, live in a predictable place at the beginning of the file. This makes it possible for a module – such as an ActiveX control – to extract the thumbnail information when pointed at a DWG file and generate an image from it. All done without the need for RealDWG (which also does this, but with a much heavier runtime component).</p>

<p>A number of 3rd party tools are available to do this – I’d suggest searching on Google – and our team provides one to ADN members, along with a number of other ActiveX controls for display of layers, linetypes, hatch patterns, slides, etc. etc.:</p>

<p><a href="http://adn.autodesk.com/adn/servlet/item?siteID=4814862&amp;id=9628824&amp;linkID=4900509">http://adn.autodesk.com/adn/servlet/item?siteID=4814862&amp;id=9628824&amp;linkID=4900509</a></p>

<p>To make use of the DWG Thumbnail control in your application, you need to add it to your Visual Studio Toolbox, by right-clicking on it, selecting “Choose Items…” and using the COM Components tab to browse to the DwgThumbnail.ocx file. On my system the file is under “C:\Program Files\Autodesk\ADN AutoCAD Utilities\Utilities\AutoCAD Controls”.</p>

<p><a onclick="window.open(this.href, '_blank', 'width=571,height=403,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/2007/10/22/dwg_thumbnail_adding_to_toolbox.png"><img title="Dwg_thumbnail_adding_to_toolbox" height="176" alt="Dwg_thumbnail_adding_to_toolbox" src="/assets/dwg_thumbnail_adding_to_toolbox.png" width="250" border="0" /></a> <a onclick="window.open(this.href, '_blank', 'width=204,height=156,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/2007/10/22/dwg_thumbnail_in_toolbox.png"><img title="Dwg_thumbnail_in_toolbox" height="57" alt="Dwg_thumbnail_in_toolbox" src="/assets/dwg_thumbnail_in_toolbox.png" width="75" border="0" /></a></p>

<p><em>Figure 7 – adding the DWG Thumbnail control to your Visual Studio Toolbox</em></p>

<p>Now you can add control to your form and implement some logic to set the DwgFileName property to the location of your DWG file.</p>

<p><strong>Embedding DWG TrueView in an HTML page or dialog</strong></p>

<p>DWG TrueView can be embedded in an HTML page or dialog and controlled via its COM Automation interface, whether from HTML scripting (JavaScript, VBScript) or another choice of scripting language, if embedded in a dialog. There is a large caveat: this is not a fully supported mode of working with DWG TrueView, so don’t have very high hopes. Simple embedding should work, but if you’re trying to do anything more complex you’re likely to run into problems.</p>

<p>Here’s an HTML fragment, which shows how to embed the DWG TrueView ActiveX control:</p>

<p>[from <a href="https://projectpoint.buzzsaw.com/constructionmanagement/public/testDWG.htm?public">https://projectpoint.buzzsaw.com/constructionmanagement/public/testDWG.htm?public</a>]</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: red; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&lt;</span><span style="COLOR: maroon">object</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; id<span style="COLOR: blue">=&quot;dwgViewerCtrl&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; classid<span style="COLOR: blue">=&quot;clsid:6C7DC044-FB1E-4140-9223-052E5ABE7D24&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; height<span style="COLOR: blue">=&quot;100%&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; width<span style="COLOR: blue">=&quot;100%&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; data<span style="COLOR: blue">=&quot;DATA:application/x-oleobject;BASE64,RMB9bB77QEGSIwUuWr59JAAHAAARTAAArjAAAA==</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&quot;&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">object</span><span style="COLOR: blue">&gt;</span></p></div>

<p>It’s also straightforward to embed DWG TrueView in your .NET application, just like the DWG Thumbnail control: from your Visual Studio Toolbox, right-click and select “Choose Items…”, selecting the “COM Components” tab and then the “DWGVIEWRCtrl” item from the list of controls:</p>

<p><a onclick="window.open(this.href, '_blank', 'width=571,height=403,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/2007/10/22/trueview_to_toolbox.png"><img title="Trueview_to_toolbox" height="176" alt="Trueview_to_toolbox" src="/assets/trueview_to_toolbox.png" width="250" border="0" /></a> <a onclick="window.open(this.href, '_blank', 'width=212,height=161,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/2007/10/22/trueview_in_toolbox.png"><img title="Trueview_in_toolbox" height="56" alt="Trueview_in_toolbox" src="/assets/trueview_in_toolbox.png" width="75" border="0" /></a> </p>

<p><em>Figure 8 – adding the DWG TrueView control to your Visual Studio Toolbox</em></p>

<p>Now you simply need to add it to your form, and add some logic to select the filename to pass into the PutSourcePath() method of the control (either via a browse button, a text box or a hard-coded path).</p>

<p>To compare the capabilities of these two controls, we’re now going to build a simple application that embeds the DWG Thumbnail control alongside DWG TrueView.</p>

<p>After adding the controls to our Toolbox, we design a simple form (ComparisonForm), with a single button to “Load” a DWG file (LoadButton). This button browses for a DWG and loads it into the DWG Thumbnail control and into DWG TrueView.</p>

<p>Here’s the code:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Public</span> <span style="COLOR: blue">Class</span> ComparisonForm</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">Private</span> <span style="COLOR: blue">Sub</span> LoadButton_Click _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; (<span style="COLOR: blue">ByVal</span> sender <span style="COLOR: blue">As</span> System.Object, _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">ByVal</span> e <span style="COLOR: blue">As</span> System.EventArgs) _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">Handles</span> LoadButton.Click</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">Dim</span> dlg <span style="COLOR: blue">As</span> <span style="COLOR: blue">New</span> System.Windows.Forms.OpenFileDialog()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; dlg.InitialDirectory = _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;System.Environment.CurrentDirectory</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; dlg.Filter = _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: maroon">&quot;DWG files (*.dwg)|*.dwg|All files (*.*)|*.*&quot;</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">Dim</span> oc <span style="COLOR: blue">As</span> Cursor = <span style="COLOR: blue">Me</span>.Cursor</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">Dim</span> fn <span style="COLOR: blue">As</span> <span style="COLOR: blue">String</span> = <span style="COLOR: maroon">&quot;&quot;</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">If</span> dlg.ShowDialog() = Windows.Forms.DialogResult.OK <span style="COLOR: blue">Then</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Me</span>.Cursor = Cursors.WaitCursor</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;fn = dlg.FileName()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Me</span>.Refresh()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">End</span> <span style="COLOR: blue">If</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">If</span> fn &lt;&gt; <span style="COLOR: maroon">&quot;&quot;</span> <span style="COLOR: blue">Then</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Me</span>.AxDwgThumbnail1.DwgFileName = fn</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Me</span>.AxDwgThumbnail1.Refresh()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Me</span>.AxAcCtrl1.PutSourcePath(fn)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">End</span> <span style="COLOR: blue">If</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">Me</span>.Cursor = oc</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">End</span> <span style="COLOR: blue">Sub</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">End</span> <span style="COLOR: blue">Class</span></p></div>

<p>When we run the application and select our DWG, we see it loaded into both controls (the Thumbnail loads almost instantaneously, the DWG file takes longer to load into DWG TrueView, of course, as DWG TrueView also needs to be instantiated behind the scenes):</p>

<p><a onclick="window.open(this.href, '_blank', 'width=753,height=456,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/2007/10/22/dwg_thumbnail_and_trueview.png"><img title="Dwg_thumbnail_and_trueview" height="181" alt="Dwg_thumbnail_and_trueview" src="/assets/dwg_thumbnail_and_trueview.png" width="300" border="0" /></a> </p>

<p><em>Figure 9 – embedding the DWG Thumbnail control alongside DWG TrueView</em></p>
