---
layout: "post"
title: "SortOnVersion in F#"
date: "2010-10-13 15:02:56"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Batch processing"
  - "F#"
  - "WPF"
original_url: "https://www.keanw.com/2010/10/sortonversion-in-f.html "
typepad_basename: "sortonversion-in-f"
typepad_status: "Publish"
---

<p>I had a very nice surprise in my inbox, this morning. Thorsten Meinecke, from <a href="http://www.gtb-ingenieure.de" target="_blank">GTB</a> in Berlin, decided to convert the VB.NET code contained in <a href="http://through-the-interface.typepad.com/through_the_interface/2010/10/sortonversion-scriptpro.html" target="_blank">the last post</a> into an F# script and to share it with this blog’s readership. Thanks, Thorsten! :-)</p>
<p>One thing about it being an F# script (typically stored in a .fsx file) is that it can be loaded and executed directly from the “F# Interactive” (FSI) component in Visual Studio without the need to build it into a project creating an executable. What’s also very nice is that the XAML defining the WPF dialog is embedded directly into the script, making it a simple matter to copy &amp; paste the application into Visual Studio.</p>
<p>Here’s the F# code:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">#I</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: maroon;">@"C:\Program Files\Reference Assemblies\Microsoft\Framework\v3.0"</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">#r</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: maroon;">"PresentationCore.dll"</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">#r</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: maroon;">"PresentationFramework.dll"</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">#r</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: maroon;">"WindowsBase.dll"</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">#r</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: maroon;">"WindowsFormsIntegration.dll"</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">open</span><span style="line-height: 140%;"> System.IO</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">open</span><span style="line-height: 140%;"> System.Windows.Forms</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">open</span><span style="line-height: 140%;"> System.Windows</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">open</span><span style="line-height: 140%;"> System.Windows.Controls</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">open</span><span style="line-height: 140%;"> System.Windows.Markup</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">open</span><span style="line-height: 140%;"> System.Windows.Media</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> xaml =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: maroon;">"&lt;Window</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: maroon;">&nbsp;&nbsp;&nbsp; xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: maroon;">&nbsp;&nbsp;&nbsp; xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: maroon;">&nbsp;&nbsp;&nbsp; Title='Sort On Drawing Version' Height='327' Width='511'</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: maroon;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; WindowStartupLocation='CenterScreen'&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: maroon;">&nbsp;&nbsp;&nbsp; &lt;Grid Height='Auto'&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: maroon;">&nbsp;&nbsp;&nbsp; &lt;Grid.ColumnDefinitions&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: maroon;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;ColumnDefinition Width='270*' /&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: maroon;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;ColumnDefinition Width='270*' /&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: maroon;">&nbsp;&nbsp;&nbsp; &lt;/Grid.ColumnDefinitions&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: maroon;">&nbsp;&nbsp;&nbsp; &lt;Label Grid.ColumnSpan='2' Height='25' HorizontalAlignment='Left'</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: maroon;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Name='Label1' VerticalAlignment='Top'</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: maroon;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Width='77'&gt;Root folder:&lt;/Label&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: maroon;">&nbsp;&nbsp;&nbsp; &lt;TextBox Grid.ColumnSpan='2' Height='26' Margin='73,-1,166,0'</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: maroon;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Name='FolderBox' VerticalAlignment='Top' /&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: maroon;">&nbsp;&nbsp;&nbsp; &lt;Button Height='29' Margin='5,0,0,33' Name='CopyButton'</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: maroon;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; VerticalAlignment='Bottom' IsEnabled='False'&gt;Copy&lt;/Button&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: maroon;">&nbsp;&nbsp;&nbsp; &lt;Button Grid.ColumnSpan='2' Height='28' Margin='0,0,85,0'</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: maroon;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Name='BrowseButton' VerticalAlignment='Top'&nbsp; Width='75'</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: maroon;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; HorizontalAlignment='Right'&gt;Browse...&lt;/Button&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: maroon;">&nbsp;&nbsp;&nbsp; &lt;ListView Grid.ColumnSpan='2' Margin='5,30,6,68'</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: maroon;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Name='FileList'/&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: maroon;">&nbsp;&nbsp;&nbsp; &lt;Button Height='28' HorizontalAlignment='Right' Margin='0,0,6,0'</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: maroon;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Name='ListButton' VerticalAlignment='Top' Width='73'</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: maroon;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; IsEnabled='False' Grid.Column='1'&gt;List&lt;/Button&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: maroon;">&nbsp;&nbsp;&nbsp; &lt;ProgressBar Grid.ColumnSpan='2' Height='28' Margin='5,0,6,2'</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: maroon;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Name='SortProgress' VerticalAlignment='Bottom' /&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: maroon;">&nbsp;&nbsp;&nbsp; &lt;Button Grid.Column='1' Height='29' Margin='0,0,6,33'</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: maroon;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Name='MoveButton' VerticalAlignment='Bottom'</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: maroon;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; IsEnabled='False'&gt;Move&lt;/Button&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: maroon;">&nbsp;&nbsp;&nbsp; &lt;/Grid&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: maroon;">&nbsp; &lt;/Window&gt;"</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> (?) (this : Control) (prop: string) : 'T =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; this.FindName prop :?&gt; 'T</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> (+=) e f = Observable.add f e</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> window = XamlReader.Parse xaml :?&gt; Window</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> label1: Label = window?Label1</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> folderBox: TextBox = window?FolderBox</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> copyButton: Button = window?CopyButton</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> browseButton: Button = window?BrowseButton</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> fileList: ListView = window?FileList</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> listButton: Button = window?ListButton</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> sortProgress: ProgressBar = window?SortProgress</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> moveButton: Button = window?MoveButton</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> getVersion fn =</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> (|StartsWith|_|) arg (s: string) = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> s.StartsWith arg </span><span style="line-height: 140%; color: blue;">then</span><span style="line-height: 140%;"> Some() </span><span style="line-height: 140%; color: blue;">else</span><span style="line-height: 140%;"> None</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> data = Array.create 6 0uy</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">use</span><span style="line-height: 140%;"> fs = File.OpenRead fn</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> fs.Read(data, 0, 6) = 6 </span><span style="line-height: 140%; color: blue;">then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">match</span><span style="line-height: 140%;"> (</span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> System.Text.ASCIIEncoding()).GetString data </span><span style="line-height: 140%; color: blue;">with</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; | StartsWith </span><span style="line-height: 140%; color: maroon;">"MC0.0"</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">-&gt;</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: maroon;">"R1.0"</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; | StartsWith </span><span style="line-height: 140%; color: maroon;">"AC1.2"</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">-&gt;</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: maroon;">"R1.2"</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; | StartsWith </span><span style="line-height: 140%; color: maroon;">"AC1.40"</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">-&gt;</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: maroon;">"R1.4"</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; | StartsWith </span><span style="line-height: 140%; color: maroon;">"AC1.50"</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">-&gt;</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: maroon;">"R2.05"</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; | StartsWith </span><span style="line-height: 140%; color: maroon;">"AC2.10"</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">-&gt;</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: maroon;">"R2.10"</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; | StartsWith </span><span style="line-height: 140%; color: maroon;">"AC2.21"</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">-&gt;</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: maroon;">"R2.21"</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; | StartsWith </span><span style="line-height: 140%; color: maroon;">"AC2.22"</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">-&gt;</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: maroon;">"R2.22"</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; | StartsWith </span><span style="line-height: 140%; color: maroon;">"AC1001"</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">-&gt;</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: maroon;">"R2.22"</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; | StartsWith </span><span style="line-height: 140%; color: maroon;">"AC1002"</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">-&gt;</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: maroon;">"R2.5"</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; | StartsWith </span><span style="line-height: 140%; color: maroon;">"AC1003"</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">-&gt;</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: maroon;">"R2.6"</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; | StartsWith </span><span style="line-height: 140%; color: maroon;">"AC1004"</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">-&gt;</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: maroon;">"R9"</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; | StartsWith </span><span style="line-height: 140%; color: maroon;">"AC1006"</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">-&gt;</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: maroon;">"R10"</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; | StartsWith </span><span style="line-height: 140%; color: maroon;">"AC1009"</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">-&gt;</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: maroon;">"R11"</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; | StartsWith </span><span style="line-height: 140%; color: maroon;">"AC1012"</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">-&gt;</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: maroon;">"R13"</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; | StartsWith </span><span style="line-height: 140%; color: maroon;">"AC1014"</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">-&gt;</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: maroon;">"R14"</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; | StartsWith </span><span style="line-height: 140%; color: maroon;">"AC1015"</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">-&gt;</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: maroon;">"2000"</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; | StartsWith </span><span style="line-height: 140%; color: maroon;">"AC1018"</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">-&gt;</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: maroon;">"2004"</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; | StartsWith </span><span style="line-height: 140%; color: maroon;">"AC1021"</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">-&gt;</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: maroon;">"2007"</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; | StartsWith </span><span style="line-height: 140%; color: maroon;">"AC1024"</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">-&gt;</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: maroon;">"2010"</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; | _ </span><span style="line-height: 140%; color: blue;">-&gt;</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: maroon;">"Unknown"</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">else</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: maroon;">""</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">finally</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; fs.Close()</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> messageBox (s: string) =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; MessageBox.Show(s, </span><span style="line-height: 140%; color: maroon;">"Sort On Drawing Version"</span><span style="line-height: 140%;"> ) |&gt; ignore</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> sortFiles move =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> numSorted = ref 0</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> numSkipped = ref 0</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> fileList.Items.Count = 0 </span><span style="line-height: 140%; color: blue;">then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: maroon;">"Nothing to sort!"</span><span style="line-height: 140%;"> |&gt; messageBox</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; sortProgress.Minimum &lt;- 0.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; sortProgress.Maximum &lt;- fileList.Items.Count - 1 |&gt; float</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; sortProgress.Value &lt;- 0.</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;"> fn </span><span style="line-height: 140%; color: blue;">in</span><span style="line-height: 140%;"> Seq.cast fileList.Items </span><span style="line-height: 140%; color: blue;">do</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> ver = getVersion fn</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> not(System.String.IsNullOrEmpty ver) </span><span style="line-height: 140%; color: blue;">then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> loc = Path.Combine(folderBox.Text, ver)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> not(Directory.Exists loc) </span><span style="line-height: 140%; color: blue;">then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Directory.CreateDirectory loc |&gt; ignore</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> dest = Path.Combine(loc, Path.GetFileName fn)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> not(File.Exists dest) </span><span style="line-height: 140%; color: blue;">then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> move </span><span style="line-height: 140%; color: blue;">then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; File.Move(fn, dest)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; File.Copy(fn, dest)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; incr numSorted</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; incr numSkipped</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; sortProgress.Value &lt;- sortProgress.Value + 1.</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; System.String.Format(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: maroon;">"{0} file{1} {2}, {3} (already existing) file{4} skipped."</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; !numSorted,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; (</span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> !numSorted = 1 </span><span style="line-height: 140%; color: blue;">then</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: maroon;">""</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">else</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: maroon;">"s"</span><span style="line-height: 140%;">),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; (</span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> move </span><span style="line-height: 140%; color: blue;">then</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: maroon;">"moved"</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">else</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: maroon;">"copied"</span><span style="line-height: 140%;">),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; !numSkipped,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; (</span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> !numSkipped = 1 </span><span style="line-height: 140%; color: blue;">then</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: maroon;">""</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">else</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: maroon;">"s"</span><span style="line-height: 140%;"> ) )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; |&gt; messageBox</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; sortProgress.Value &lt;- 0.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; fileList.ItemsSource &lt;- </span><span style="line-height: 140%; color: blue;">null</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">with</span><span style="line-height: 140%;"> ex </span><span style="line-height: 140%; color: blue;">-&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: maroon;">"A problem was found while sorting files: "</span><span style="line-height: 140%;"> + ex.Message</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; |&gt; messageBox</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">browseButton.Click +=</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">fun</span><span style="line-height: 140%;"> _ </span><span style="line-height: 140%; color: blue;">-&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> fbd =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> FolderBrowserDialog(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Description = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: maroon;">"Select the root folder for the DWG version sort:"</span><span style="line-height: 140%;"> )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> Directory.Exists folderBox.Text </span><span style="line-height: 140%; color: blue;">then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; fbd.SelectedPath &lt;- folderBox.Text</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> dr = fbd.ShowDialog()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> dr = DialogResult.OK </span><span style="line-height: 140%; color: blue;">then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; folderBox.Text &lt;- fbd.SelectedPath</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">folderBox.TextChanged +=</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">fun</span><span style="line-height: 140%;"> e </span><span style="line-height: 140%; color: blue;">-&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> tb = e.Source :?&gt; TextBox</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; listButton.IsEnabled &lt;- Directory.Exists tb.Text</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">listButton.Click +=</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">fun</span><span style="line-height: 140%;"> _ </span><span style="line-height: 140%; color: blue;">-&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; fileList.ItemsSource &lt;-</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Directory.GetFiles(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; folderBox.Text, </span><span style="line-height: 140%; color: maroon;">"*.dwg"</span><span style="line-height: 140%;">, SearchOption.AllDirectories )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">with</span><span style="line-height: 140%;"> _ </span><span style="line-height: 140%; color: blue;">-&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: maroon;">"A problem was found accessing sub-folders in this "</span><span style="line-height: 140%;"> +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: maroon;">"location: will simply get the drawings in the root "</span><span style="line-height: 140%;"> +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: maroon;">"folder."</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; |&gt; messageBox</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; fileList.ItemsSource &lt;-</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Directory.GetFiles(folderBox.Text, </span><span style="line-height: 140%; color: maroon;">"*.dwg"</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; copyButton.IsEnabled &lt;- </span><span style="line-height: 140%; color: blue;">true</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; moveButton.IsEnabled &lt;- </span><span style="line-height: 140%; color: blue;">true</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">moveButton.Click += </span><span style="line-height: 140%; color: blue;">fun</span><span style="line-height: 140%;"> _ </span><span style="line-height: 140%; color: blue;">-&gt;</span><span style="line-height: 140%;"> sortFiles </span><span style="line-height: 140%; color: blue;">true</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">copyButton.Click += </span><span style="line-height: 140%; color: blue;">fun</span><span style="line-height: 140%;"> _ </span><span style="line-height: 140%; color: blue;">-&gt;</span><span style="line-height: 140%;"> sortFiles </span><span style="line-height: 140%; color: blue;">false</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">#if</span><span style="line-height: 140%;"> COMPILED</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: gray;">[&lt;System.STAThread&gt;]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: gray;">[&lt;EntryPoint&gt;]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: gray;">let main _ = (new Application()).Run window</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">#else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> Application()).Run window</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">#endif</span></p>
</div>
<!--EndFragment-->
<p>The simple way to run this code, for those unfamiliar with using F# in Visual Studio, is to paste it into a file (usually a .fsx file), select the contents and use Alt-Enter to load it into FSI. That should cause the dialog shown in the previous post to display, and to work in an identical manner.</p>
<p>Thanks again for sharing this code, Thorsten!</p>
