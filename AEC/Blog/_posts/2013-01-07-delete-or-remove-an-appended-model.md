---
layout: "post"
title: "delete or remove an appended model"
date: "2013-01-07 21:28:27"
author: "Xiaodong Liang"
categories:
  - ".NET"
  - "COM"
  - "Navisworks"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/aec/2013/01/delete-or-remove-an-appended-model.html "
typepad_basename: "delete-or-remove-an-appended-model"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>  <p><strong>Issue</strong>    <br />Is it posible to delete or remove an appended model? I cant find any obvious was through the .NET api. </p>  <p><strong>Solution</strong></p>  <p>Suppose we have one document in which there is only one modal from one file. We can run Append or Merge to add more files. So we would have “several files” in one document.Currently, only COM API provides the ability to remove the models.&#160; It can delete any files like you select the files in UI and press delete. The whole contents of the deleted file will be removed. The code is as below,&#160; assume we want to delete the 3rd file in the document.</p>  <div style="font-family: courier new; background: white; color: black; font-size: 9pt">   <p style="margin: 0px"><span style="line-height: 140%; color: blue">public</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: blue">override</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: blue">int</span><span style="line-height: 140%"> Execute(</span><span style="line-height: 140%; color: blue">params</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: blue">string</span><span style="line-height: 140%">[] parameters)</span></p>    <p style="margin: 0px"><span style="line-height: 140%">{</span>&#160;</p>    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; Nw.Document doc =        <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; Nw.Application.ActiveDocument;</span>&#160;</p>    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; doc.CurrentSelection.CopyFrom(doc.Models[2].       <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; RootItem.AncestorsAndSelf);</span>&#160;</p>    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; ComApi.</span><span style="line-height: 140%; color: #2b91af">InwOpState10</span><span style="line-height: 140%"> state =        <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; ComApiBridge.ComApiBridge.State;</span>&#160;</p>    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; state.DeleteSelectedFiles();</span>&#160;</p>    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">return</span><span style="line-height: 140%"> 0;</span></p>    <p style="margin: 0px"><span style="line-height: 140%">}</span></p>    <p style="margin: 0px">&#160;</p> </div>
