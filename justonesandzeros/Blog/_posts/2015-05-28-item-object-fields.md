---
layout: "post"
title: "Item Object: Fields"
date: "2015-05-28 21:13:30"
author: "Michal Liu"
categories:
  - "PLM 360"
original_url: "https://justonesandzeros.typepad.com/blog/2015/05/item-object-fields.html "
typepad_basename: "item-object-fields"
typepad_status: "Publish"
---

<p>Today let’s put spotlight on the item fields. First we will see how to access each field from the <strong>Item</strong> object. Then we will go through all field types and find the corresponding value type in scripting for each of them.</p>
<hr />
<p><strong>Access Field Value With Field ID</strong>&#0160;</p>
<p>When an item is loaded, all its field properties have been put into it with their <strong>Field ID</strong> as the key. So to access the value of each field, we just need acquire it from the <strong>Item </strong>variable directly using the field ID. For example:</p>
<blockquote>
<p><em>var name = item.NAME;&#0160; // <span style="font-size: xx-small;"><strong>read the value of NAME field from owner item</strong> <br /></span>item.AGE++;&#0160; // <span style="font-size: xx-small;"><strong>increase the value of AGE field in owner item by one</strong></span></em></p>
</blockquote>
<p>PLM scripting is case sensitive, and Field IDs are always in upper case, so neither “<em>item.name”</em> nor “<em>item.Age” </em>will work in this example. Field ID is not editable and must be unique to the workspace. It doesn’t have to be the same as the field name. So if you are not sure about the field ID, you can find it in the field edit window. [Administration -&gt; Workspace Manager -&gt; workspace –&gt; Item Details Tab –&gt; Edit a field by click the pencil icon]</p>
<p><a href="http://justonesandzeros.typepad.com/.a/6a0120a5728249970b01b8d119df55970c-pi"><img alt="field types" border="0" height="386" src="/assets/image_a4e3eb.jpg" style="background-image: none; padding-top: 0px; padding-left: 0px; display: inline; padding-right: 0px; border-width: 0px;" title="field types" width="484" /></a></p>
<hr />
<p><strong>Field Type vs. Data Value Type</strong></p>
<p>As the above screenshot shows, there are many different field types provided. The different field type has different value type in scripting, and some of the field values are editable but some are read-only. To make things clear I put all these information into below table:</p>
<table border="1" cellpadding="0" cellspacing="0" style="border-collapse: collapse; line-height: normal;" width="476"><colgroup><col style="width: 195pt; mso-width-source: userset; mso-width-alt: 9508;" width="260" /><col style="width: 155pt; mso-width-source: userset; mso-width-alt: 7533;" width="206" /><col style="width: 230pt; mso-width-source: userset; mso-width-alt: 11227;" width="307" /></colgroup>
<tbody>
<tr style="height: 15pt;">
<td height="20" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="288"><span style="color: #000000;"><strong>Field Type</strong></span></td>
<td style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="126"><span style="color: #000000;"><strong>Value Type</strong></span></td>
<td style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="60"><span style="color: #000000;"><strong>R/W</strong></span></td>
</tr>
<tr style="height: 15pt;">
<td class="xl65" height="20" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="288">Auto Number</td>
<td class="xl66" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="126"><a href="https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/String" target="_blank"><span style="color: #0000ff;">String</span></a></td>
<td class="xl65" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="60"><span style="color: #000000;">Read-only</span></td>
</tr>
<tr style="height: 15pt;">
<td class="xl65" height="20" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="288"><span style="color: #000000;">Single Line Text</span></td>
<td class="xl66" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="126"><a href="https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/String" target="_blank"><span style="color: #0000ff;">String</span></a></td>
<td class="xl65" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="60"><span style="color: #000000;">R/W</span></td>
</tr>
<tr style="height: 15pt;">
<td class="xl65" height="20" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="288"><span style="color: #000000;">BOM UOM Pick List</span></td>
<td class="xl66" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="126"><a href="https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/String" target="_blank"><span style="color: #0000ff;">String</span></a></td>
<td class="xl65" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="60"><span style="color: #000000;">R/W</span></td>
</tr>
<tr style="height: 15pt;">
<td class="xl65" height="20" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="288"><span style="color: #000000;">Image</span></td>
<td class="xl66" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="126"><a href="https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/String" target="_blank"><span style="color: #0000ff;">String</span></a></td>
<td class="xl65" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="60"><span style="color: #000000;">Read-only</span></td>
</tr>
<tr style="height: 15pt;">
<td class="xl65" height="20" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="288"><span style="color: #000000;">URL</span></td>
<td class="xl66" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="126"><a href="https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/String" target="_blank"><span style="color: #0000ff;">String</span></a></td>
<td class="xl65" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="60"><span style="color: #000000;">R/W</span></td>
</tr>
<tr style="height: 15pt;">
<td class="xl65" height="20" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="288"><span style="color: #000000;">Email</span></td>
<td class="xl66" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="126"><a href="https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/String" target="_blank"><span style="color: #0000ff;">String</span></a></td>
<td class="xl65" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="60"><span style="color: #000000;">R/W</span></td>
</tr>
<tr style="height: 15pt;">
<td class="xl65" height="20" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="288"><span style="color: #000000;">CSV</span></td>
<td class="xl66" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="126"><a href="https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/String" target="_blank"><span style="color: #0000ff;">String</span></a></td>
<td class="xl65" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="60"><span style="color: #000000;">R/W</span></td>
</tr>
<tr style="height: 15pt;">
<td class="xl65" height="20" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="288"><span style="color: #000000;">Flash</span></td>
<td class="xl66" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="126"><a href="https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/String" target="_blank"><span style="color: #0000ff;">String</span></a></td>
<td class="xl65" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="60"><span style="color: #000000;">Read-only</span></td>
</tr>
<tr style="height: 15pt;">
<td class="xl65" height="20" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="288"><span style="color: #000000;">Paragraph</span></td>
<td class="xl66" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="126"><a href="https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/String" target="_blank"><span style="color: #0000ff;">String</span></a></td>
<td class="xl65" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="60"><span style="color: #000000;">R/W</span></td>
</tr>
<tr style="height: 15pt;">
<td class="xl65" height="20" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="288"><span style="color: #000000;">Paragraph w/o Line Breaks</span></td>
<td class="xl66" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="126"><a href="https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/String" target="_blank"><span style="color: #0000ff;">String</span></a></td>
<td class="xl65" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="60"><span style="color: #000000;">R/W</span></td>
</tr>
<tr style="height: 15pt;">
<td class="xl65" height="20" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="288"><span style="color: #000000;">Integer</span></td>
<td class="xl66" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="126"><a href="https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Number" target="_blank"><span style="color: #0000ff;">Number</span></a></td>
<td class="xl65" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="60"><span style="color: #000000;">R/W</span></td>
</tr>
<tr style="height: 15pt;">
<td class="xl65" height="20" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="288"><span style="color: #000000;">Float</span></td>
<td class="xl66" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="126"><a href="https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Number" target="_blank"><span style="color: #0000ff;">Number</span></a></td>
<td class="xl65" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="60"><span style="color: #000000;">R/W</span></td>
</tr>
<tr style="height: 15pt;">
<td class="xl65" height="20" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="288"><span style="color: #000000;">Money</span></td>
<td class="xl66" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="126"><a href="https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Number" target="_blank"><span style="color: #0000ff;">Number</span></a></td>
<td class="xl65" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="60"><span style="color: #000000;">R/W</span></td>
</tr>
<tr style="height: 15pt;">
<td class="xl65" height="20" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="288"><span style="color: #000000;">Date</span></td>
<td class="xl66" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="126"><a href="https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Date" target="_blank"><span style="color: #0000ff;">Date</span></a></td>
<td class="xl65" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="60"><span style="color: #000000;">R/W</span></td>
</tr>
<tr style="height: 15pt;">
<td class="xl65" height="20" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="288"><span style="color: #000000;">Check Box</span></td>
<td class="xl66" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="126"><a href="https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Boolean" target="_blank"><span style="color: #0000ff;">Boolean</span></a></td>
<td class="xl65" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="60"><span style="color: #000000;">R/W</span></td>
</tr>
<tr style="height: 15pt;">
<td class="xl65" height="20" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="288"><span style="color: #000000;">Derived</span></td>
<td class="xl65" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="126"><span style="color: #000000;">Same as source field</span></td>
<td class="xl65" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="60"><span style="color: #000000;">Read-only</span></td>
</tr>
<tr style="height: 17.25pt;">
<td class="xl65" height="23" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="288"><span style="color: #000000;">Single-Selection<span class="font5"><sup>1</sup></span><span class="font0"> Item-Pick-List</span><span class="font5"><sup>2</sup></span></span></td>
<td class="xl65" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="126"><span style="color: #000000;"><strong>Item</strong></span></td>
<td class="xl65" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="60"><span style="color: #000000;">R/W</span></td>
</tr>
<tr style="height: 15pt;">
<td class="xl65" height="20" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="288"><span style="color: #000000;">Multiple-Selection Item-Pick-List</span></td>
<td class="xl66" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="126"><span style="color: #000000;"><a href="https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array" target="_blank"><span style="color: #0000ff;">Array</span></a> of <strong>Item</strong></span></td>
<td class="xl65" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="60"><span style="color: #000000;">R/W</span></td>
</tr>
<tr style="height: 15pt;">
<td class="xl65" height="20" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="288"><span style="color: #000000;">Filtered Item-Pick-List</span></td>
<td class="xl66" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="126"><a href="https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/String" target="_blank"><span style="color: #0000ff;">String</span></a></td>
<td class="xl65" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="60"><span style="color: #000000;">R/W</span></td>
</tr>
<tr style="height: 17.25pt;">
<td class="xl65" height="23" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="288"><span style="color: #000000;">Single-Selection<span class="font0">Value-Pick-List</span><span class="font5"><sup>3</sup></span></span></td>
<td class="xl66" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="126"><a href="https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/String" target="_blank"><span style="color: #0000ff;">String</span></a></td>
<td class="xl65" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="60"><span style="color: #000000;">R/W</span></td>
</tr>
<tr style="height: 15pt;">
<td class="xl65" height="20" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="288"><span style="color: #000000;">Multiple-Selection Value-Pick-List</span></td>
<td class="xl65" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="126"><span style="color: #000000;"><span style="color: #000000;"><a href="https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array" target="_blank"><span style="color: #0000ff;">Array</span></a>&#0160;</span>of <a href="https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/String" target="_blank"><span style="color: #0000ff;">String</span></a></span></td>
<td class="xl65" style="vertical-align: bottom; padding-top: 1px; padding-left: 1px; padding-right: 1px;" width="60"><span style="color: #000000;">R/W</span></td>
</tr>
</tbody>
</table>
<p>Footnotes:</p>
<ol>
<li>The Single-Selection here includes the selectable pick list field types of: “Radio Button”, “Single Selection”, “Show first value as default”, “Retains Last Saved Label”, “With search Filter” and “Latest Version”.</li>
<li>Item-Pick-List is a list of item records from a workspace, which can be chosen in the pick list creation form. Single-selection item pick list returns a single <strong>Item</strong> object. Multiple-selection item pick list returns an Array of <strong>Item</strong> objects. <br /><a href="http://justonesandzeros.typepad.com/.a/6a0120a5728249970b01b8d119e039970c-pi"><img alt="itemPL" border="0" height="249" src="/assets/image_3b7482.jpg" style="background-image: none; padding-top: 0px; padding-left: 0px; display: inline; padding-right: 0px; border-width: 0px;" title="itemPL" width="423" /></a></li>
<li>Value-Pick-List is a list of String values defined by user. Single-selection returns a single String value. Multiple-selection returns an Array of String values. <br /><a href="http://justonesandzeros.typepad.com/.a/6a0120a5728249970b01b7c7904cdf970b-pi"><img alt="valuePL" border="0" height="244" src="/assets/image_c351fe.jpg" style="background-image: none; padding-top: 0px; padding-left: 0px; display: inline; padding-right: 0px; border-width: 0px;" title="valuePL" width="426" /></a></li>
</ol>
<p>&#0160;</p>
<p>-- Michal</p>
