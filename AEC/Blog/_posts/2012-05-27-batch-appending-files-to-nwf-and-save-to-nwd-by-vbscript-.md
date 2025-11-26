---
layout: "post"
title: "Batch appending files to nwf and save to nwd by VBScript "
date: "2012-05-27 23:09:40"
author: "Xiaodong Liang"
categories:
  - "COM"
  - "Navisworks"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/aec/2012/05/batch-appending-files-to-nwf-and-save-to-nwd-by-vbscript-.html "
typepad_basename: "batch-appending-files-to-nwf-and-save-to-nwd-by-vbscript-"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p><strong>Issue<br /> </strong>Is there a way through <strong>VBScript </strong>to publish a directory of dwg&#39;s to an single .nwd file?  <br /><br /><strong>Solution</strong><br /> Navisworks can append the same file more than once without warning. You need to use code to check if the file has been existed. roamer.state.CurrentPartition (InwOaPartition) returns the top level access to the current model. if any files are appended, InwOaPartition.Children will tell each file. So you can use InwOaPartition.FileName to check if the file existed or not.   The attached VBS checks if C:\temp\1.dwg1 has been appended to the main nwf.</p>
<p>Please note: the string in VBS is case sensitive.</p>
<p>&#39; ########test.vbs##########</p>
<p><br />&#39;&#39;Usage:<br />&#39;AppendFiles.vbs &lt;main_filename&gt;&#0160; &lt;file_Name1&gt;&#0160; &lt;file_Name2&gt;&#0160; <br />&#39;example:<br />&#39;AppendFiles.vbs &quot;c:\temp\main.nwf&quot; &quot;c:\temp\1.dwg&quot;&#0160; &quot;c:\temp\2.dwg&quot;</p>
<p>&#39; navisworks application<br />dim roamer&#39; <br />&#39; input argument <br />dim arg_in<br />dim file_Name1<br />dim file_Name2<br />dim count<br /><br />&#39; top api object<br />dim oState<br /><br />&#39; root partition<br />dim oPartition<br />&#39; each partition if more than one files within the document<br />dim oEachPartition</p>
<p>&#39; the file has been appended? <br />dim hasDWG1<br /><br />&#0160;<br />&#39; get inputs&#0160; <br />count=WScript.Arguments.Count<br />&#39; document<br />arg_in=WScript.Arguments(0)<br />&#39; file 1<br />file_Name1 =WScript.Arguments(1) <br />&#39; file2<br />file_Name2 =WScript.Arguments(2) <br />&#0160;<br />&#39;create roamer via automation<br />set roamer=createobject(&quot;navisWorks.document&quot;)&#0160;&#0160;&#0160; <br /><br />&#39;open input file<br />roamer.openfile arg_in <br />&#0160;<br />hasDWG1 = false<br />for each&#0160;&#0160; oEachPartition in roamer.state.CurrentPartition.Children <br />&#0160; msgbox &quot;each file in the main nwf: &quot; &amp; oEachPartition.Filename<br />&#0160; if oEachPartition.Filename = &quot;C:\temp\1.dwg&quot; then<br />&#0160;&#0160;&#0160;&#0160; hasDWG1 = true<br />&#0160;&#0160;&#0160;&#0160; msgbox &quot;DWG1 has been appended!&quot;<br />&#0160; end if<br />next<br /><br />&#39; if the file has Not been appended<br />if hasDWG1 = false then<br />&#0160; roamer.appendfile file_Name1 <br />&#0160; msgbox &quot;append DWG1&quot;<br />end if<br />&#0160;<br />&#39; append file 2 without checking<br />roamer.appendfile file_Name2<br /><br />&#39; save to the nwd<br />roamer.saveas&#0160; &quot;c:\temp\test.nwd&quot; <br />&#0160;<br />msgbox &quot;end&quot;</p>
