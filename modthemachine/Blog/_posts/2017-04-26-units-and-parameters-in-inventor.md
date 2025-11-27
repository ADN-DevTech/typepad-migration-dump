---
layout: "post"
title: "Units and Parameters in Inventor"
date: "2017-04-26 20:30:56"
author: "Adam Nagy"
categories:
  - "Brian"
  - "Inventor"
original_url: "https://modthemachine.typepad.com/my_weblog/2017/04/units-and-parameters-in-inventor.html "
typepad_basename: "units-and-parameters-in-inventor"
typepad_status: "Publish"
---

<p>I recently saw a question asking about how to determine if a parameter is of a certain unit type and thought it would be worth writing a bit more here for everyone’s benefit.</p>
<p>First, let’s look at the object model for Parameters, which is shown below on the left.&nbsp; You’ll notice that there’s a base class “Parameter” object and several other types derived from it; DerivedParameter, ModelParameter, TableParameter, ReferenceParameter, and UserParameter.&nbsp; These correspond to the groupings you see in the Parameter dialog.&nbsp; In the picture on the right you can see two model parameters which were created when I placed two dimension constraints in a sketch to control the sketch size. These are referred to as “driving” dimensions because these drive the model and if I change their values the model will update.&nbsp; Model dimensions are also created when you create modeling features that are driven by a parameter.&nbsp; There are two reference parameters which were created when I added dimension constraints to a sketch that would have overconstrained the sketch.&nbsp; These dimensions are being controlled by the sketch and are referred to as “driven” dimensions.&nbsp; The parameters will update as the model changes.&nbsp; I also created three parameters that I named “Length”, “Message”, and “IsItRaining”.&nbsp; These are user parameters because they’re created by the user.&nbsp; There can also be other types besides numeric like in the example below with Text and Boolean parameters.&nbsp; And the final type represented below are table parameters.&nbsp; These are created when you import an Excel spreadsheet to create parameters.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401b7c8f1d675970b-pi"><img style="background-image: none; padding-top: 0px; padding-left: 0px; display: inline; padding-right: 0px; border-width: 0px;" title="params" src="/assets/image_440854.jpg" alt="params" width="1044" height="478" border="0" /></a></p>
<p>So, those are the types of parameters that Inventor supports.&nbsp; Depending on the type, there are certain things you can and can’t do with a parameter.&nbsp; For example, you can’t delete a model parameter, because its tied to a modeling entity and will exist as long as that entity exists.&nbsp; A table parameter can’t be deleted either because it’s controlled by the referenced Excel spreadsheet.&nbsp; You can’t edit the value of a reference or table parameter because the value is controlled by the model or the value in the spreadsheet.&nbsp; The point is that all of these are different types of the Parameter object and they each have slightly different behaviors but they all support the functionality of the Parameter object.&nbsp; The parameter object supports quite a few methods and properties and depending on the specific type of parameter, not all of them may function the same.&nbsp; For example you can use the Value property to get and set the value of a model parameter but for a reference parameter the Value property behaves as read-only.&nbsp;</p>
<p>The question that sparked this response was related to the units of a parameter.&nbsp; The units of a parameter don’t define its object type but the unit is just a property of a parameter. In fact for user parameters, the user can easily change the units of a parameter. The value of a parameter can be of three different categories; numeric, text, or Boolean.&nbsp; The Parameter object supports the Units property which returns a String indicating the unit category.&nbsp; For a parameter that has a text value the Units property will return “Text” and for a Boolean value it will return “Boolean”.&nbsp;</p>
<p>By far, the most common unit category is a numeric value.&nbsp; The Units property for a numeric value can return an almost infinite number of results.&nbsp; It returns the current unit string that represents the current unit assigned to that parameter.&nbsp; For example, the most common kind of numeric unit is length and could be returned as “mm”, “cm”,, “m”, “in”, “yd”, “mi” (for miles), etc.&nbsp; These are all valid units for lengths.&nbsp; For model and reference parameters, the units used to define length uses the default length units of the document as defined in the Document Settings dialog, as shown below.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401b7c8f1d67d970b-pi"><img style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin-left: auto; display: block; padding-right: 0px; margin-right: auto; border-width: 0px;" title="DocUnits" src="/assets/image_627934.jpg" alt="DocUnits" width="297" height="322" border="0" /></a></p>
<p>For user parameters you can define any unit type you want.&nbsp; For lengths there are a known set of length types that Inventor will allow you to use. I think I listed most of them above. The same is true for the other base unit types like angles, mass, and time.&nbsp; However, there are a lot of units types that are a combination of other types so their can be a huge number of combinations.&nbsp; For example, area is defined using any of the length types like “in * in” or “cm * cm”.&nbsp; In fact it will even allow “in * cm” as a valid area.&nbsp; I’m not sure what that means but it just demonstrates the flexibility.&nbsp; More complex types that aren’t even listed in the units list in Inventor can be defined by the user like acceleration using “m/s/s” or “m/s^2”.&nbsp; The point being that there isn’t a small set of unit types that can be used for numeric parameter values.&nbsp;</p>
<p>In the API there is an enum with the most common unit types that can be used in some cases but you can also always use the String equivalent and in many cases have to use the String representation of the unit type because it’s not in the enum.&nbsp; For example you can use kInchLengthUnits from the UnitTypesEnum or “in” or “inch”.</p>
<p>The original question was about how to determine if a parameter is defining a length or an angle value. You can use the Units property of the parameter to get back the current units of the parameter. Remember that this will return “Text”, “Boolean”, or a string defining the unit type of a numeric parameter. The trick is to be able to correctly handle the numeric parameter because a length or angle can be defined in many different ways.&nbsp; In most cases you don’t care if they’ve chosen to use cm, inch, or mile but just want to know if it’s a length or not.&nbsp; There are some other utilities in Inventor that can help to determine this.</p>
<p>The UnitsOfMeasure object provides several utility methods related to units.&nbsp; You can take advantage of these when working with parameters. You get the UnitsOfMeasure object from the Document using its UnitsOfMeasure property.&nbsp; I’ll let you look at the <a href="http://help.autodesk.com/view/INVNTOR/2018/ENU/?guid=GUID-026FB34F-60A7-4F2A-A708-190EAA6A1162">documentation for the UnitsOfMeasure object</a> to see all of what the UnitsOfMeasure supports but I want to focus on one its the more obscure methods right now, which is the CompatibleUnits method.&nbsp; Using this method we can answer the question above.&nbsp; What this method does is test two different values to see if their units are “compatible” or of the same category of unit.&nbsp; If we want to know if a parameter defines a length we can use this to see if it’s compatible with a value we know is a length.&nbsp; If this returns True, we know it defines a length.&nbsp; Here’s a VBA example to check to see if a parameter is a length.</p>
<pre><code>Dim param As Parameter
Set param = partDoc.ComponentDefinition.Parameters.Item(“SomeParameter”)

Dim uom As UnitsOfMeasure
Set uom = partDoc.UnitsOfMeasure

If uom.CompatibleUnits(param.Expression, param.Units, "1", "cm") Then
    MsgBox(“Is Length”)
Else
    MsgBox(“Is NOT a Length”)
End If
</code></pre>
<p>The CompatibleUnits method compares two values where each value is defined by two inputs.&nbsp; The first is the expression of the value which is a string and could be something like “5” or “5 in” or “5in + 3cm” or “d1” or any number of valid expressions that can be used to define the value of a parameter.&nbsp; The second input is the unit of the parameter which can be any of the of unit types discussed previously (“in”, “cm*cm”, “m/s^2”, etc.).&nbsp; In the example above it’s using the Expression property of the parameter as the first input and its Units property as the second input. For the value to compare, all we need is a value that we know defines a valid length.&nbsp; For the expression portion I use “1”, but it could be any value like “99.999” or “1/2”.&nbsp; For the unit portion I use “cm” but any of the known length units will also work because it doesn’t matter what the specific unit type is as long as it defines a length.</p>
<p>Here are two functions that can be used to easily determine if a parameter is a length or angle. The TestParamFunctions method tests the two functions. Notice that in the functions I first check to make sure the parameter isn’t a Text or Boolean type because the CompatibleUnits method will fail in those cases.</p>
<strong>VBA</strong>
<pre><code>Public Sub TestParamFunctions()
    Dim partDoc As PartDocument
    Set partDoc = ThisApplication.ActiveDocument
    
    Dim param As Parameter
    For Each param In partDoc.ComponentDefinition.Parameters
        If IsLengthParam(param, partDoc) Then
            MsgBox param.name &amp; " is a length."
        ElseIf isAngleParam(param, partDoc) Then
            MsgBox param.name &amp; " is an angle."
        Else
            MsgBox param.name &amp; " is something else."
        End If
    Next
End Sub

Public Function IsLengthParam(param As Parameter, doc As Document) As Boolean
    If param.Units = "Boolean" Or param.Units = "Text" Then
        IsLengthParam = False
        Exit Function
    End If
    
    Dim uom As UnitsOfMeasure
    Set uom = doc.UnitsOfMeasure
    
    If uom.CompatibleUnits(param.expression, param.Units, "1", "cm") Then
        IsLengthParam = True
    Else
        IsLengthParam = False
    End If
End Function

Public Function isAngleParam(param As Parameter, doc As Document) As Boolean
    If param.Units = "Boolean" Or param.Units = "Text" Then
        isAngleParam = False
        Exit Function
    End If
    
    Dim uom As UnitsOfMeasure
    Set uom = doc.UnitsOfMeasure
    
    If uom.CompatibleUnits(param.expression, param.Units, "1", "deg") Then
        isAngleParam = True
    Else
        isAngleParam = False
    End If
End Function
</code></pre>
<br>
<strong>Visual Basic .NET and iLogic</strong>
<pre><code>Public Sub TestParamFunctions()
    Dim invApp As Inventor.Application = GetObject(, "Inventor.Application")
    Dim partDoc As PartDocument = invApp.ActiveDocument

    Dim param As Parameter
    For Each param In partDoc.ComponentDefinition.Parameters
        If IsLengthParam(param, partDoc) Then
            MsgBox(param.Name &amp; " is a length.")
        ElseIf isAngleParam(param, partDoc) Then
            MsgBox(param.Name &amp; " is an angle.")
        Else
            MsgBox(param.Name &amp; " is something else.")
        End If
    Next
End Sub

Public Function IsLengthParam(param As Parameter, doc As Document) As Boolean
    If param.Units = "Boolean" Or param.Units = "Text" Then
        Return False
    End If

    Dim uom As UnitsOfMeasure = doc.UnitsOfMeasure

    If uom.CompatibleUnits(param.Expression, param.Units, "1", "cm") Then
        Return True
    Else
        Return False
    End If
End Function

Public Function isAngleParam(param As Parameter, doc As Document) As Boolean
    If param.Units = "Boolean" Or param.Units = "Text" Then
        Return False
    End If

    Dim uom As UnitsOfMeasure = doc.UnitsOfMeasure

    If uom.CompatibleUnits(param.Expression, param.Units, "1", "deg") Then
        Return True
    Else
        Return False
    End If
End Function
</code></pre>
<p>-Brian</p>
