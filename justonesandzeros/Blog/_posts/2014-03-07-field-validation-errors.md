---
layout: "post"
title: "Field Validation Errors"
date: "2014-03-07 14:40:58"
author: "Doug Redmond"
categories:
  - "Concepts"
  - "PLM 360"
original_url: "https://justonesandzeros.typepad.com/blog/2014/03/field-validation-errors.html "
typepad_basename: "field-validation-errors"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/PLM4.png" /> <br /><img alt="" src="/assets/Concepts4.png" /></p>
<p>A nice new feature in the latest PLM 360 update is that REST API will show more detailed error information for when a add/update operation fails because of bad field data.&#0160; For example, if you try to update an item with a value of “x” for a numeric field, you will now get an error specifying which field(s) failed and why.</p>
<p>First, let’s go over the generic error case.&#0160; For example, if you try to add a new item to a workgroup you don’t have write access to, here is the JSON error data you will get back.&#0160; The structure corresponds to the <a href="http://help.autodesk.com/view/PLM/ENU/?guid=GUID-5DE1B88C-89A9-418F-A0F4-D159FB87D2A7" target="_blank">APIError</a> class in the API documentation.</p>
<table border="1" cellpadding="2" cellspacing="0" width="470">
<tbody>
<tr>
<td valign="top" width="470">{ <br />&#0160;&#0160;&#0160; &quot;errorCode&quot;:&quot;SEC_FORBIDDEN&quot;, <br />&#0160;&#0160;&#0160; &quot;message&quot;:&quot;Access Denied.&quot;, <br />&#0160;&#0160;&#0160; &quot;params&quot;:null, <br />&#0160;&#0160;&#0160; &quot;url&quot;:null, <br />&#0160;&#0160;&#0160; &quot;errorClass&quot;:&quot;APIError&quot; <br />}</td>
</tr>
</tbody>
</table>
<p><br />Now let’s look at the case where you try setting a field to a value that is not allowed.&#0160; Here is the JSON that you get back, which corresponds to the <a href="http://help.autodesk.com/view/PLM/ENU/?guid=GUID-431E3F3D-1C87-4FA6-AA2A-BB744A86E599" target="_blank">FieldValidation</a> error class.</p>
<table border="1" cellpadding="2" cellspacing="0" width="470">
<tbody>
<tr>
<td valign="top" width="470">{ <br />&#0160;&#0160;&#0160; &quot;errorCode&quot;:&quot;ITEM_VALIDATION_FAILED&quot;, <br />&#0160;&#0160;&#0160;&#0160; &quot;message&quot;:&quot;Item validation failed.&quot;, <br />&#0160;&#0160;&#0160;&#0160; &quot;params&quot;:null, <br />&#0160;&#0160;&#0160;&#0160; &quot;url&quot;:null, <br />&#0160;&#0160;&#0160;&#0160; &quot;errorClass&quot;:&quot;FieldValidationError&quot;, <br />&#0160;&#0160;&#0160;&#0160; &quot;fieldErrors&quot;: [ <br />&#0160;&#0160;&#0160;&#0160; { <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &quot;fieldId&quot;:&quot;CURRENCY&quot;, <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &quot;errorDescription&quot;:&quot;Currency is not a valid currency amount&quot; <br />&#0160;&#0160;&#0160;&#0160; }, <br />&#0160;&#0160;&#0160;&#0160; { <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &quot;fieldId&quot;:&quot;DECIMAL&quot;, <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &quot;errorDescription&quot;:&quot;Decimal must be a valid decimal number with at most 2 decimal digits.&quot; <br />&#0160;&#0160;&#0160; }] <br />}</td>
</tr>
</tbody>
</table>
<hr noshade="noshade" style="color: #5acb04;" />
<p>The first thing to note is that the FieldValidation data is a superset of the APIError data.&#0160; Another way to putting it is that FieldValidation is a subclass of APIError.&#0160; That means that PLM errors follow a similar pattern to Java and .NET Exceptions.&#0160; In those languages, Exception is the base class and specialized subclasses exist for specialized cases (IOException, StackOverflowException, and so on).&#0160; You as a programmer can handle things in multiple ways depending on how specialized the error is.&#0160; In PLM, APIError is the generic case and FieldValidationError is a specialized case.&#0160; More specialized errors may be added in the future, but for now, there is just one case.</p>
<p>Another thing to note is that the errorClass property matches the name of the error class.&#0160; This is to help your code deserialize to the correct object.&#0160; It’s a bit of a circular problem because you need to deserialize the JSON in order to figure out how the JSON should best be deserialized.&#0160;</p>
<p>I recommend a multi-phase approach to handing errors.</p>
<ol>
<li>In the event of an error (HTTP codes in the 400s or 500s), read the HTTP body into a string.</li>
<li>Deserialize the string into an APIError object.</li>
<li>Check the “errorClass” value.</li>
<li>If errorClass is not APIError, deserialize again to the proper error class.</li>
</ol>
<p>After that it&#39;s up to you how you want to use that error data.&#0160; But you definately have more options than you did before.</p>
<hr noshade="noshade" style="color: #5acb04;" />
