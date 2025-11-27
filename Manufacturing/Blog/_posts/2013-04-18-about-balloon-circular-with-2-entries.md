---
layout: "post"
title: "About Balloon &ndash; Circular with 2 Entries"
date: "2013-04-18 20:55:19"
author: "Xiaodong Liang"
categories: []
original_url: "https://adndevblog.typepad.com/manufacturing/2013/04/about-balloon-circular-with-2-entries.html "
typepad_basename: "about-balloon-circular-with-2-entries"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>  <p>In default, the Balloon has only one circle, the number in which indicates the sequence ID of the component in BOM. Inventor allows you to configure more types of the Balloon format. e.g. Circular with 2 entries. And you can edit the format, also the contents of the circle: Balloon Value.</p>  <p>I thought the first column [ITEM] means the number in upper half circle, while [Override] means the number in lower half circle. I was wrong. </p>  <p>If you edit [ITEM], [Override] will change to the same value of [ITEM], and the number in upper half circle is the value. If you edit [Override], [Item] does not change, the number in upper half circle is the override value.</p>  <p>So the editing does nothing with the lower half circle! Why?</p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017eea61641d970d-pi"><img style="border-bottom: 0px; border-left: 0px; display: inline; border-top: 0px; border-right: 0px" title="image" border="0" alt="image" src="/assets/image_7bf342.jpg" width="293" height="409" /></a> </p>  <p>Actually, the lower half circle is defined by the Balloon Style &gt;&gt; Balloon Formatting &gt;&gt; Property Display</p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017d42ed220b970c-pi"><img style="border-bottom: 0px; border-left: 0px; display: inline; border-top: 0px; border-right: 0px" title="image" border="0" alt="image" src="/assets/image_88212d.jpg" width="486" height="394" /></a> </p>  <p>While the editing box of balloon provides you to edit Item only. So the editing is not relevant with the lower half circle at all.</p>  <p>If you want to change the number of the lower half circle, you need to change the QTY in BOM. </p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01901b6408b9970b-pi"><img style="border-bottom: 0px; border-left: 0px; display: inline; border-top: 0px; border-right: 0px" title="image" border="0" alt="image" src="/assets/image_1f6796.jpg" width="490" height="290" /></a> </p>  <p>In addition, the formatting allows you to display any properties you need. </p>  <p>API has all of&#160; the same abilities (from help reference)</p>  <p><em><strong>Balloon.GetBalloonType</strong>:returns the balloon type:</em></p>  <pre><em><strong>Public Enum BalloonTypeEnum
  kCircularWithOneEntryBalloonType</strong> = 48129
  <strong>kCircularWithTwoEntriesBalloonType</strong> = 48130
  <strong>kHexagonBalloonType</strong> = 48131
  <strong>kLinearBalloonType</strong> = 48132
  <strong>kNoneBalloonType</strong> = 48133
  <strong>kSketchedSymbolBalloonType</strong> = 48134
<strong>End Enum</strong></em></pre>

<p><em><strong>Balloon.BalloonValueSets</strong> is the collection of the values with the balloon. Currently, I can only see one value within it.</em></p>

<p><em><strong>BalloonValueSet</strong>.Value: value of the set

    <br /><strong>BalloonValueSet.OverrideValue</strong>: override value of the set

    <br /><strong>BalloonValueSet.ItemNumber</strong>: original sequence number in BOM

    <br /><strong>BalloonValueSet.Static</strong>: this is an interesting Boolean. It&#160; indicates whether the Value property has been overridden. Setting this property to False clears any overrides on the Value, but does not clear the 'override value.' The override value can be cleared by setting the OverrideValue property to a null string. This property is the equivalent of the PartListCell.Static property. </em></p>

<p><em><strong>BalloonStyle.BalloonType:</strong> set formatting of balloon

    <br /><strong>BalloonStyle.Properties</strong>: the properties displayed in the balloon. The string can contain multiple properties separated by a semicolon (;). To specify a file property in the drawing document, use the following format: </em></p>

<p><em>FormatID='{32853F0F-3444-11d1-9E93-0060B03C1CA6}' PropertyID='29'</em></p>

<p><em>To specify a parts list property, use the enum value (long) of </em><a href="Inventor__PropertyTypeEnum.html"><em>PropertyTypeEnum</em></a><em>, with a 'PartsListProperty' keyword. </em></p>

<p><em>To specify a custom property from the model, use the 'ModelCustomProperty' keyword. </em></p>

<p><em>Example: </em></p>

<p><em>'FormatID='{32853F0F-3444-11d1-9E93-0060B03C1CA6}' PropertyID='29'; FormatID='{32853F0F-3444-11d1-9E93-0060B03C1CA6}' PropertyID='27'; PartsListProperty='45576'; ModelCustomProperty ='MyProperty''</em></p>

<p><em>where 45576 is the enum value corresponding to kBaseQuantityPartsListProperty.</em></p>

<p>&#160;</p>

<p>Following is some demo codes</p>

<p>Sub changeItem() </p>

<p>&#160;&#160;&#160; ' assume a balloon is selected
  <br />&#160;&#160;&#160; Dim oBalloon As Balloon

  <br />&#160;&#160;&#160; Set oBalloon = ThisApplication.ActiveDocument.SelectSet(1)

  <br />&#160;&#160;&#160; Dim oBalloonType&#160; As BalloonTypeEnum

  <br />&#160;&#160;&#160; Dim oBalloonData As Variant

  <br />&#160;&#160;&#160; Call oBalloon.GetBalloonType(oBalloonType, oBalloonData)

  <br />&#160;&#160;&#160; If oBalloonType = kCircularWithTwoEntriesBalloonType Then

  <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; Dim oBVS As BalloonValueSet

  <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; Set oBVS = oBalloon.BalloonValueSets(1)

  <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; 'edit [Item]

  <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; oBVS.Value = &quot;111&quot;

  <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; 'print [Item] and [Override]

  <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; Debug.Print oBVS.Value

  <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; Debug.Print oBVS.OverrideValue

  <br />&#160;&#160;&#160; End If

  <br />End Sub </p>

<p>Sub changeOverride() </p>

<p>&#160;&#160;&#160; ' assume a balloon is selected
  <br />&#160;&#160;&#160; Dim oBalloon As Balloon

  <br />&#160;&#160;&#160; Set oBalloon = ThisApplication.ActiveDocument.SelectSet(1)

  <br />&#160;&#160;&#160; Dim oBalloonType&#160; As BalloonTypeEnum

  <br />&#160;&#160;&#160; Dim oBalloonData As Variant

  <br />&#160;&#160;&#160; Call oBalloon.GetBalloonType(oBalloonType, oBalloonData)

  <br />&#160;&#160;&#160; If oBalloonType = kCircularWithTwoEntriesBalloonType Then

  <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; Dim oBVS As BalloonValueSet

  <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; Set oBVS = oBalloon.BalloonValueSets(1)

  <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; 'edit [Override]

  <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; oBVS.OverrideValue = &quot;222&quot;

  <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; ''print [Item] and [Override]

  <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; Debug.Print oBVS.Value

  <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; Debug.Print oBVS.OverrideValue

  <br />&#160;&#160;&#160; End If

  <br />End Sub </p>

<p>Sub resetToOriginal() </p>

<p>&#160;&#160;&#160; ' assume a balloon is selected
  <br />&#160;&#160;&#160; Dim oBalloon As Balloon

  <br />&#160;&#160;&#160; Set oBalloon = ThisApplication.ActiveDocument.SelectSet(1)

  <br />&#160;&#160;&#160; Dim oBalloonType&#160; As BalloonTypeEnum

  <br />&#160;&#160;&#160; Dim oBalloonData As Variant

  <br />&#160;&#160;&#160; Call oBalloon.GetBalloonType(oBalloonType, oBalloonData)

  <br />&#160;&#160;&#160; If oBalloonType = kCircularWithTwoEntriesBalloonType Then

  <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; Dim oBVS As BalloonValueSet

  <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; Set oBVS = oBalloon.BalloonValueSets(1)

  <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; Debug.Print &quot;序号原始值: &quot; &amp; oBVS.ItemNumber

  <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; 'reset [Item] to original number in BOM

  <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; oBVS.Static = False

  <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; '''print [Item] and [Override]

  <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; Debug.Print oBVS.Value

  <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; Debug.Print oBVS.OverrideValue

  <br />&#160;&#160;&#160; End If

  <br />End Sub </p>

<p>Sub changeStyleType() </p>

<p>&#160;&#160;&#160; ' get current balloon style from a balloon&#160; <br />&#160;&#160;&#160; Dim oBS As BalloonStyle

  <br />&#160;&#160;&#160;&#160; Set oBS = ThisApplication.ActiveDocument.SelectSet(1).Style

  <br />&#160;&#160;&#160;&#160; 'set type

  <br />&#160;&#160;&#160;&#160; oBS.BalloonType = kCircularWithTwoEntriesBalloonType

  <br />&#160;&#160;&#160;&#160; 'the properties want to display

  <br />&#160;&#160;&#160;&#160; Dim oProperties&#160; As String

  <br />&#160;&#160;&#160;&#160; oProperties = &quot; FormatID='{32853F0F-3444-11d1-9E93-0060B03C1CA6}' PropertyID='29'; FormatID='{32853F0F-3444-11d1-9E93-0060B03C1CA6}' PropertyID='27'&quot;

  <br />&#160;&#160;&#160;&#160; oBS.Properties = oProperties

  <br />End Sub</p>
