---
layout: "post"
title: "Get the prompt string of an attribute through VBA?"
date: "2013-02-01 09:03:21"
author: "Augusto Goncalves"
categories:
  - "ActiveX"
  - "Augusto Goncalves"
  - "AutoCAD"
original_url: "https://adndevblog.typepad.com/autocad/2013/02/get-the-prompt-string-of-an-attribute-through-vba.html "
typepad_basename: "get-the-prompt-string-of-an-attribute-through-vba"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/augusto-goncalves.html">Augusto Goncalves</a></p>  <p>We can get the prompt string of an Attribute from the Attribute Definition contained in the Block Definition. The following sample code demonstrates this:</p> <font color="#000000">   <pre><font color="#0000a0">Private</font> <font color="#0000a0">Sub</font> GetBlockAttributePrompts()
    <font color="#0000a0">Dim</font> elem <font color="#0000a0">As</font> <font color="#0000a0">Object</font>
    <font color="#0000a0">For</font> <font color="#0000a0">Each</font> elem <font color="#0000a0">In</font> ThisDrawing.ModelSpace
        <font color="#0000a0">If</font> elem.EntityName = &quot;AcDbBlockReference&quot; <font color="#0000a0">Then</font>
            <font color="#0000a0">If</font> elem.HasAttributes <font color="#0000a0">Then<br /></font>
<font color="#008000">                'Get The Block Definition</font>
                <font color="#0000a0">Dim</font> block <font color="#0000a0">As</font> AcadBlock
                <font color="#0000a0">Set</font> block = ThisDrawing.Blocks.item(elem.Name)
                
                <font color="#0000a0">Dim</font> prompt <font color="#0000a0">As</font> <font color="#0000a0">String</font>
                prompt = &quot;&quot;
                
                <font color="#0000a0">Dim</font> item <font color="#0000a0">As</font> <font color="#0000a0">Object</font>
                <font color="#0000a0">For</font> <font color="#0000a0">Each</font> item <font color="#0000a0">In</font> block                   
                    <font color="#0000a0">If</font> item.EntityName = &quot;AcDbAttributeDefinition&quot; <font color="#0000a0">Then</font>
                        prompt = prompt + Chr(13) + item.PromptString
                    <font color="#0000a0">End</font> <font color="#0000a0">If</font>
                <font color="#0000a0">Next</font> item
                <font color="#0000a0">Debug.Print</font> prompt
            <font color="#0000a0">End</font> <font color="#0000a0">If</font>
        <font color="#0000a0">End</font> <font color="#0000a0">If</font>
    <font color="#0000a0">Next</font> elem
<font color="#0000a0">End</font> <font color="#0000a0">Sub</font></pre>
</font>
