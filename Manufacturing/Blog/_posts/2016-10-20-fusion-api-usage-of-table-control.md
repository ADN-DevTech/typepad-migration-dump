---
layout: "post"
title: "Fusion API: Usage of Table Control"
date: "2016-10-20 23:53:30"
author: "Xiaodong Liang"
categories:
  - "Fusion 360"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/manufacturing/2016/10/fusion-api-usage-of-table-control.html "
typepad_basename: "fusion-api-usage-of-table-control"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>Fusion provides various types of controls (CommandInput) for a command. There is a <a href="http://fusion360.autodesk.com/learning/learning.html?guid=GUID-e5c4dbe8-ee48-11e4-9823-f8b156d7cd97">Python demo in API help</a>.</p>
<p><br />Table type is available with API. It can even embed sub controls within the table or table cell, by which we can implement more scenarios of customization. In my practice recently, I got some knowledge of Table. I am writing down for reference. The whole demo codes has been&#0160;posted to</p>
<p><a href="https://github.com/xiaodongliang/Fusion-Table-Control">https://github.com/xiaodongliang/Fusion-Table-Control</a></p>
<p>1. CommandInputs.addTableCommandInput adds a new table to the dialog. It defines the table Id, name, columns number and the width ratio of each column. e.g. the code below specifies the table has 3 columns with same width.<br />&#0160; &#0160; &#0160;tableInput = inputs.addTableCommandInput(commandId +&#39;my_table&#39;, &#39;Table&#39;, 3, &#39;1:1:1&#39;)</p>
<p>2. To add a child control to the cell, you need firstly get tableInput.commandInputs and call <br />&#0160; &#0160;tableInput.addCommandInput (child_control,rowindex, column index)</p>
<p>Note: The API demo above uses childCommandInputs, but this method has been removed. Now please use tableInput.commandInputs</p>
<p>3. If you want to add a child control that locates within the table, instead of a cell, the method is:<br />&#0160; &#0160; &#0160;tableInput.addToolbarCommandInput(child_control)</p>
<p>4. , any input changing of a control will fire InputChangedEventHandler. Typically, we will get out the inputs collection of the dialog, and find out the specific control. But in C++, it will be null if getting inputs collection by eventArgs-&gt;inputs(). Fortuently, a workaround is available as below.&#0160; &#0160;</p>
<pre><code>
class MyCommandInputChange : public InputChangedEventHandler
{
public:
  void notify(const Ptr&amp; eventArgs) override
    {
         Ptr changedInput = eventArgs-&gt;input();

	//One issue: in C++, eventArgs-&gt;inputs() is invalid if the event is fired for the embedded control within a table
         //Ptr inputs = eventArgs-&gt;inputs(); 
	//workaround:
<span style="background-color: #ffff00;">	 Ptr<command></command> command = eventArgs-&gt;firingEvent()-&gt;sender();
	 Ptr inputs = command-&gt;commandInputs();
</span></code></pre>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09487cc0970d-pi" style="display: inline;"><img alt="2016-10-21 14-51-52" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb09487cc0970d img-responsive" src="/assets/image_f0aa79.jpg" title="2016-10-21 14-51-52" /></a></p>
