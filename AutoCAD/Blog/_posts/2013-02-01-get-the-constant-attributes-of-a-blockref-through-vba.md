---
layout: "post"
title: "Get the constant attributes of a blockref through VBA"
date: "2013-02-01 08:58:09"
author: "Augusto Goncalves"
categories:
  - "ActiveX"
  - "Augusto Goncalves"
  - "AutoCAD"
original_url: "https://adndevblog.typepad.com/autocad/2013/02/get-the-constant-attributes-of-a-blockref-through-vba.html "
typepad_basename: "get-the-constant-attributes-of-a-blockref-through-vba"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/augusto-goncalves.html">Augusto Goncalves</a></p>  <p>How can we get the block name, the attribute values, and find how many times the block is inserted and put this information into a spreadsheet using VBA? Getting the block information from the block definition in the block table.</p>  <p>The following sample demonstrates this by getting all the attributes of all the block references found in model space. You need to create a simple form which contains a listbox named ListBox1 and a button that starts the following procedure:</p> <font color="#000000">   <pre><font color="#0000a0">Private</font> <font color="#0000a0">Sub</font> CommandButton1_Click()
  <font color="#0000a0">Dim</font> elem <font color="#0000a0">As</font> <font color="#0000a0">Object</font>
  <font color="#0000a0">Dim</font> block <font color="#0000a0">As</font> AcadBlock
  <font color="#0000a0">Dim</font> item <font color="#0000a0">As</font> <font color="#0000a0">Object</font>
  <font color="#0000a0">Dim</font> Array1 <font color="#0000a0">As</font> <font color="#0000a0">Variant</font>
  <font color="#0000a0">Dim</font> count <font color="#0000a0">As</font> <font color="#0000a0">Integer</font>
  <font color="#0000a0">Dim</font> MBtest1 <font color="#0000a0">As</font> <font color="#0000a0">String</font>
  <font color="#0000a0">Dim</font> str <font color="#0000a0">As</font> <font color="#0000a0">String</font>
  <font color="#0000a0">For</font> <font color="#0000a0">Each</font> elem <font color="#0000a0">In</font> ThisDrawing.ModelSpace
    <font color="#0000a0">If</font> elem.EntityName = &quot;AcDbBlockReference&quot; <font color="#0000a0">Then</font>
    <font color="#0000a0">If</font> elem.HasAttributes <font color="#0000a0">Then</font>
      Array1 = elem.GetAttributes
      <font color="#0000a0">For</font> count = <font color="#0000a0">LBound</font>(Array1) <font color="#0000a0">To</font> <font color="#0000a0">UBound</font>(Array1)
        <font color="#0000a0">If</font> (Array1(count).EntityName) = &quot;AcDbAttribute&quot; <font color="#0000a0">Then</font>
        MBtest1 = Array1(count).TagString &amp; _
          &quot; - &quot; &amp; Array1(count).TextString
        ListBox1.AddItem MBtest1
        <font color="#0000a0">End</font> <font color="#0000a0">If</font>
      <font color="#0000a0">Next</font> count
<font color="#008000">      'Get the block definition from the block table</font>
      str = elem.Name
      <font color="#0000a0">Set</font> block = ThisDrawing.Blocks.item(str)
      <font color="#0000a0">For</font> <font color="#0000a0">Each</font> item <font color="#0000a0">In</font> block
        str = item.EntityName
<font color="#008000">        'Get the Constant attributes</font>
        <font color="#0000a0">If</font> item.EntityName = &quot;AcDbAttributeDefinition&quot; <font color="#0000a0">Then</font>
        <font color="#0000a0">If</font> item.Mode = acAttributeModeConstant <font color="#0000a0">Then</font>
        ListBox1.AddItem item.TagString &amp; &quot; - &quot; _
          &amp; item.TextString
        <font color="#0000a0">End</font> <font color="#0000a0">If</font>
        <font color="#0000a0">End</font> <font color="#0000a0">If</font>
      <font color="#0000a0">Next</font> item
    <font color="#0000a0">End</font> <font color="#0000a0">If</font>
    <font color="#0000a0">End</font> <font color="#0000a0">If</font>
   <font color="#0000a0">Next</font> elem
<font color="#0000a0">End</font> <font color="#0000a0">Sub</font></pre>
</font>
