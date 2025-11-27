---
layout: "post"
title: "Manipulate Family Table of Content Center"
date: "2015-12-09 00:04:17"
author: "Xiaodong Liang"
categories:
  - "Inventor"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/12/manipulate-family-table-of-content-center.html "
typepad_basename: "manipulate-family-table-of-content-center"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>  <p>In Inventor UI, we can edit cell, add new columns to the family table. In API, there are equivalent objects and methods.&#160; </p>  <p>The presumption is the family has been copied to your custom library which is read-write enabled. </p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7f76dac970b-pi"><img style="background-image: none; border-right-width: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top-width: 0px; border-bottom-width: 0px; border-left-width: 0px; padding-top: 0px" title="image" border="0" alt="image" src="/assets/image_7562b9.jpg" width="321" height="341" /></a></p>  <p>&#160;</p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1811771970c-pi"><img style="background-image: none; border-right-width: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top-width: 0px; border-bottom-width: 0px; border-left-width: 0px; padding-top: 0px" title="image" border="0" alt="image" src="/assets/image_e6086c.jpg" width="465" height="162" /></a></p>  <p>The demo below dumps gets the specific family, dumps all columns name, adds one new column whose value is string type. Finally it fills in the column with the value ‘dummy’ for all rows. It also intentionally modifies the value of column ‘Grip Len’ of all rows.</p>  <p>Do not forget to save the family after any modification. </p> <!--This is the code, set class to prettyprint so that google-code-prettify will help to format the code. set class to csharp is for csdn and no need to reference google-code-prettify-->  <pre class="csharp prettyprint" name="code">
Sub ModifyFamilyTable()

    Dim oContentCenter As ContentCenter
    Set oContentCenter = ThisApplication.ContentCenter
    
    Dim hexHeadNode As ContentTreeViewNode
    Set hexHeadNode = ThisApplication.ContentCenter.TreeViewTopNode.ChildNodes.Item(&quot;Fasteners&quot;).ChildNodes.Item(&quot;Bolts&quot;).ChildNodes.Item(&quot;Hex Head&quot;)
    
    
    Dim family As ContentFamily
    Dim checkFamily As ContentFamily
    For Each checkFamily In hexHeadNode.Families
        If checkFamily.DisplayName = &quot;DIN EN 24016&quot; Then
            Set family = checkFamily
            Exit For
        End If
    Next

    
     Dim oContentTableColumns As ContentTableColumns
    Set oContentTableColumns = family.TableColumns
    
    'dump all columns
    Dim oContentTableColumn As ContentTableColumn
    For Each oContentTableColumn In oContentTableColumns
    
     Debug.Print &quot;internal name:&quot; &amp; oContentTableColumn.InternalName &amp; &quot;&lt;&lt;&lt;&lt;&gt;&gt;&gt;&gt;display heading:&quot; &amp; oContentTableColumn.DisplayHeading
    
    Next
    
    'add one column
    Dim oNewCol As ContentTableColumn
    Set oNewCol = oContentTableColumns.Add(&quot;myHexHeadNewCol&quot;, &quot;My Hex Head New Property&quot;, kStringType)
    
    'modify cell
    Dim oRow As ContentTableRow
    For Each oRow In family.TableRows
        
        'cell value of new column
        oRow.Item(&quot;myHexHeadNewCol&quot;).Value = &quot;dummy&quot;
        'cell value of a built-in column named 'GD1', display name: &quot;Pitch Diameter&quot;
        'e.g. double it
        'use internal name of the column
        oRow.Item(&quot;KLG&quot;).Value = oRow.Item(&quot;KLG&quot;).Value * 2
    Next
    
    'save change
    Call family.Save

End Sub</pre>

<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7f76db6970b-pi"><img style="background-image: none; border-bottom: 0px; border-left: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top: 0px; border-right: 0px; padding-top: 0px" title="image" border="0" alt="image" src="/assets/image_ece936.jpg" width="362" height="352" /></a></p>
