---
layout: "post"
title: "VBA block insertion not requesting attributes"
date: "2013-02-01 09:15:24"
author: "Augusto Goncalves"
categories:
  - "ActiveX"
  - "Augusto Goncalves"
  - "AutoCAD"
original_url: "https://adndevblog.typepad.com/autocad/2013/02/vba-block-insertion-not-requesting-attributes.html "
typepad_basename: "vba-block-insertion-not-requesting-attributes"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/augusto-goncalves.html">Augusto Goncalves</a></p>  <p>If a block has attribute definitions, AutoCAD inserts all attributes with its default values and does not ask for the attribute values. </p>  <p>There is no ready-to-use function that asks for the attributes. Therefore, you will need to write your own function.</p>  <p>After inserting the attribute you can use the AcadBlockReference.GetAttributes method to get all attributes. Then you can iterate the attributes and ask for the values. If you want to use the original prompt string, you have to get the block definition object and to look for the right attribute in it. There you can get the prompt string.</p>  <p>The following function inserts the block 'test' and iterates the attributes. The 'UserForm1' object is a user form which contains:</p>  <p>- An &quot;OK&quot; button which has to be pressed after entering the value for the current attribute and which hides the form   <br />- Three text boxes:    <br />TextBox1 : receives the attribute name (locked)    <br />TextBox2 : receives the attribute prompt string (locked)    <br />TextBox3 : there you can enter the value of the attribute</p>  <p>The second function,&#160; GetPromptString, finds the original prompt string of the attribute:</p> <font color="#000000">   <pre><font color="#0000a0">Sub</font> TestFunction()
   <font color="#0000a0">Dim</font> insPt <font color="#0000a0">As</font> <font color="#0000a0">Variant</font>
   <font color="#0000a0">Dim</font> insert <font color="#0000a0">As</font> <font color="#0000a0">Object</font>
   <font color="#0000a0">Dim</font> attribs <font color="#0000a0">As</font> <font color="#0000a0">Variant</font>
   <font color="#0000a0">Dim</font> attrib <font color="#0000a0">As</font> <font color="#0000a0">Object</font>
   
<font color="#008000">   ' Activate AutoCAD</font>
   AppActivate ThisDrawing.Application.Caption
   
<font color="#008000">   ' Get insertion point for insert entity</font>
   insPt = ThisDrawing.Utility.GetPoint(, &quot;Select insert point: &quot;)
<font color="#008000">   ' Insert it</font>
   <font color="#0000a0">Set</font> insert = ThisDrawing.ModelSpace.InsertBlock(insPt, _ 
       &quot;TEST&quot;, 1#, 1#, 1#, 0#)
   
<font color="#008000">   ' Ask for the attributes</font>
   attribs = insert.GetAttributes
   <font color="#0000a0">For</font> i = <font color="#0000a0">LBound</font>(attribs) <font color="#0000a0">To</font> <font color="#0000a0">UBound</font>(attribs)
      <font color="#0000a0">Set</font> attrib = attribs(i)
      UserForm1.TextBox1.Value = attrib.TagString
      UserForm1.TextBox2.Value = GetPromptString(&quot;TEST&quot;, attrib)
      UserForm1.TextBox3.Value = attrib.TextString
      UserForm1.Show
      <font color="#0000a0">If</font> UserForm1.TextBox3.Value &lt;&gt; &quot;&quot; <font color="#0000a0">Then</font>
         attrib.TextString = UserForm1.TextBox3.Value
      <font color="#0000a0">End</font> <font color="#0000a0">If</font>
   <font color="#0000a0">Next</font> i
<font color="#0000a0">End</font> <font color="#0000a0">Sub</font></pre>

  <pre><font color="#0000a0"></font>
<font color="#0000a0">Public</font> <font color="#0000a0">Function</font> GetPromptString(blockName, attrib) <font color="#0000a0">As</font> <font color="#0000a0">String</font>
<font color="#008000">   ' This function extracts the prompt string</font>
<font color="#008000">   ' of an attribute definition (AcadAttribute).</font>
<font color="#008000">   ' The needed parameters are the block name</font>
<font color="#008000">   ' and the attribute (AcadAttributeReference).</font>
   <font color="#0000a0">Dim</font> blocks <font color="#0000a0">As</font> <font color="#0000a0">Object</font>
   <font color="#0000a0">Dim</font> block <font color="#0000a0">As</font> <font color="#0000a0">Object</font>
   <font color="#0000a0">Dim</font> count <font color="#0000a0">As</font> <font color="#0000a0">Integer</font>
   <font color="#0000a0">Dim</font> i <font color="#0000a0">As</font> <font color="#0000a0">Integer</font>
   <font color="#0000a0">Dim</font> ent <font color="#0000a0">As</font> <font color="#0000a0">Object</font>
   
<font color="#008000">   ' Get the block definition object of block 'blockName'</font>
   <font color="#0000a0">Set</font> block = ThisDrawing.blocks.Item(blockName)
   
<font color="#008000">   ' Find the attribute (specified by 'attrib')</font>
   count = block.count
   <font color="#0000a0">For</font> i = 0 <font color="#0000a0">To</font> count - 1
      <font color="#0000a0">Set</font> ent = block.Item(i)
      <font color="#0000a0">If</font> ent.EntityType = acAttribute <font color="#0000a0">Then</font>
         <font color="#0000a0">If</font> ent.TagString = attrib.TagString <font color="#0000a0">Then</font>
            GetPromptString = ent.PromptString
            <font color="#0000a0">Exit</font> <font color="#0000a0">Function</font>
         <font color="#0000a0">End</font> <font color="#0000a0">If</font>
      <font color="#0000a0">End</font> <font color="#0000a0">If</font>
   <font color="#0000a0">Next</font> i
   GetPromptString = &quot;&quot;
<font color="#0000a0">End</font> <font color="#0000a0">Function</font></pre>
</font>
