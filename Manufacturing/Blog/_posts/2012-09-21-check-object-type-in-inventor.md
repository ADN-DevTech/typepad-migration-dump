---
layout: "post"
title: "check object type in Inventor"
date: "2012-09-21 02:11:00"
author: "Xiaodong Liang"
categories:
  - "Inventor"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/09/check-object-type-in-inventor.html "
typepad_basename: "check-object-type-in-inventor"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p><strong>Issue     <br /></strong>Some properties or methods return an object. But I did not know how to check its type. e.g. I would like to determine the Proxy ObjectType returned from Constraint.EntityOne (i.e. If oConst.EntityOne = kWorkPointProxyObject</p>
<p><strong>Solution</strong></p>
<p>Two ways you could use. Please refer to the code below. One uses TypeOf. Another get the Object.ObjectType which is a value of the enu ObjectTypeEnumm.</p>
<pre>Sub tesObjectTypeEnumt()

   Dim oAsmDoc As AssemblyDocument
    Set oAsmDoc = ThisApplication.ActiveDocument

     Dim oAsmCompDef As AssemblyComponentDefinition
     Set oAsmCompDef = oAsmDoc.ComponentDefinition

     Dim oConstraints As AssemblyConstraints
    Set oConstraints = oAsmCompDef.Constraints

     Dim oCons As AssemblyConstraint
     Set oCons = oConstraints(1)
        
     Dim oEntityOne As Object
     Set oEntityOne = oCons.EntityOne

     &#39;way1
     If TypeOf oEntityOne Is EdgeProxy Then
     End If

     &#39;way2
     Dim objType As ObjectTypeEnum
     objType = oEntityOne.Type
     If objType = kEdgeProxyObject Then
     
     End If
    
    
End Sub</pre>
