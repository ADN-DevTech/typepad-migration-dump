---
layout: "post"
title: "Errors and Restrictions"
date: "2009-11-16 07:55:40"
author: "Doug Redmond"
categories:
  - "Must Read"
  - "Tips and Tricks"
original_url: "https://justonesandzeros.typepad.com/blog/2009/11/errors-and-restrictions.html "
typepad_basename: "errors-and-restrictions"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/TipsAndTricks3.png" /></p>
<p><strong>Update:</strong>&#0160; A better version of the below code has been provied in <a href="http://justonesandzeros.typepad.com/blog/2011/11/getting-the-restriction-codes.html" target="_self">another article</a>.&#0160; The new code gets both the error code and any restriction codes.</p>
<hr noshade="noshade" style="color: #ff5a00;" />
<h3><strong>Errors</strong></h3>
<p>You are probably reading the error code incorrectly.&#0160; I&#39;m guessing that you are getting it from the Message text of an Exception that gets thrown when you make an API call.&#0160; Although the Message usually contains the code, it&#39;s not the &quot;correct&quot; place to get it.&#0160;</p>
<p>The correct thing to do is catch the SoapException, then check the Details property, which is a bunch of XML data.&#0160; The Vault error code can be found between the &lt;sl:errorcode&gt; tags. Also, some errors will be accompanied by extra information in the form of a string array, found between the &lt;sl:param&gt; tags. Each parameter will have its own tag and &quot;pid&quot;, which is an index value starting at 1. Most errors will not have extra parameters.&#0160; See the <strong>Error Codes</strong> page in the API documentation for more information.</p>
<p>If you don&#39;t get a SoapException, it means that the error is not coming from the Vault server.&#0160; Something else is causing the error, such as a network error.</p>
<hr noshade="noshade" style="color: #ff5a00;" />
<h3><strong>Restrictions</strong></h3>
<p>Restrictions can be thought of as extended error information.&#0160; In the case of an Exception from the Vault server, there is one and only one error code.&#0160; Restrictions, however, can have multiple codes.&#0160; This is useful for complex operations where multiple things can fail at once, and you want to display everything to the user in a single dialog.</p>
<p>If the error code is 1092, 1387, or 1633, then you need to examine the restriction information to find out what the problem is.&#0160; For all other errors, there is no restriction information.</p>
<p>Just like with errors, you need to examine the Details XML in the Soap Exception.&#0160; The <strong>Restriction Codes</strong> page in the API documentation has more information.</p>
<hr noshade="noshade" style="color: #ff5a00;" />
<h3><strong>Sample Code</strong></h3>
<p>Here is the correct way to read the error code.</p>
<p>C#:</p>
<table border="1" cellpadding="2" cellspacing="0" width="400">
<tbody>
<tr>
<td valign="top" width="450">
<p class="MsoNormal" style="margin-bottom: 0pt; line-height: normal;"><span style="font-size: 10pt; color: blue; font-family: &quot;Courier New&quot;;">public</span><span style="font-size: 10pt; font-family: &quot;Courier New&quot;;"> <span style="color: blue;">static</span> <span style="color: blue;">string</span> GetErrorCodeString(<span style="color: #2b91af;">Exception</span> e)               <br /></span><span style="font-size: 10pt; font-family: &quot;Courier New&quot;;">{              <br /></span><span style="font-size: 10pt; font-family: &quot;Courier New&quot;;"><span>&#0160;&#0160;&#0160; </span><span style="color: #2b91af;">SoapException</span> se = e <span style="color: blue;">as</span> <span style="color: #2b91af;">SoapException</span>;               <br /></span><span style="font-size: 10pt; font-family: &quot;Courier New&quot;;"><span>&#0160;&#0160;&#0160; </span><span style="color: blue;">if</span> (se != <span style="color: blue;">null</span>)               <br /></span><span style="font-size: 10pt; font-family: &quot;Courier New&quot;;"><span>&#0160;&#0160;&#0160; </span>{               <br /></span><span style="font-size: 10pt; font-family: &quot;Courier New&quot;;"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="color: blue;">try                <br /></span></span><span style="font-size: 10pt; font-family: &quot;Courier New&quot;;"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span>{               <br /></span><span style="font-size: 10pt; font-family: &quot;Courier New&quot;;"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="color: blue;">return</span> se.Detail[<span style="color: #a31515;">&quot;sl:sldetail&quot;</span>][<span style="color: #a31515;">&quot;sl:errorcode&quot;</span>].InnerText.Trim();               <br /></span><span style="font-size: 10pt; font-family: &quot;Courier New&quot;;"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span>}               <br /></span><span style="font-size: 10pt; font-family: &quot;Courier New&quot;;"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="color: blue;">catch                <br /></span></span><span style="font-size: 10pt; font-family: &quot;Courier New&quot;;"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span>{ }               <br /></span><span style="font-size: 10pt; font-family: &quot;Courier New&quot;;"><span>&#0160;&#0160;&#0160; </span>}               <br /></span><span style="font-size: 10pt; font-family: &quot;Courier New&quot;;"><span>&#0160;&#0160;&#0160; </span><span style="color: blue;">return</span> <span style="color: blue;">null</span>;               <br /></span><span style="font-size: 10pt; line-height: 115%; font-family: &quot;Courier New&quot;;">}</span></p>
</td>
</tr>
</tbody>
</table>
<p>&#0160;</p>
<p>VB:</p>
<table border="1" cellpadding="2" cellspacing="0" width="450">
<tbody>
<tr>
<td valign="top" width="450">
<p class="MsoNormal" style="margin-bottom: 0pt; line-height: normal; mso-layout-grid-align: none;"><span style="font-size: 10pt; color: blue; font-family: &quot;Courier New&quot;; mso-no-proof: yes;">Public</span><span style="font-size: 10pt; font-family: &quot;Courier New&quot;; mso-no-proof: yes;"> <span style="color: blue;">Shared</span> <span style="color: blue;">Function</span> GetErrorCodeString(<span style="color: blue;">ByVal</span> e <span style="color: blue;">As</span> Exception) <span style="color: blue;">As</span> <span style="color: blue;">String               <br /></span></span><span style="font-size: 10pt; font-family: &quot;Courier New&quot;; mso-no-proof: yes;"><span style="mso-spacerun: yes;">&#0160;&#0160;&#0160; </span><span style="color: blue;">If</span> (<span style="color: blue;">TypeOf</span> e <span style="color: blue;">Is</span> SoapException) <span style="color: blue;">Then               <br /></span></span><span style="font-size: 10pt; font-family: &quot;Courier New&quot;; mso-no-proof: yes;"><span style="mso-spacerun: yes;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="color: blue;">Dim</span> se <span style="color: blue;">As</span> SoapException = e              <br /></span><span style="font-size: 10pt; font-family: &quot;Courier New&quot;; mso-no-proof: yes;"><span style="mso-spacerun: yes;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="color: blue;">Try               <br /></span></span><span style="font-size: 10pt; font-family: &quot;Courier New&quot;; mso-no-proof: yes;"><span style="mso-spacerun: yes;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="color: blue;">Return</span> se.Detail(<span style="color: #a31515;">&quot;sl:sldetail&quot;</span>)(<span style="color: #a31515;">&quot;sl:errorcode&quot;</span>).InnerText.Trim()              <br /></span><span style="font-size: 10pt; font-family: &quot;Courier New&quot;; mso-no-proof: yes;"><span style="mso-spacerun: yes;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="color: blue;">Catch               <br /></span></span><span style="font-size: 10pt; font-family: &quot;Courier New&quot;; mso-no-proof: yes;"><span style="mso-spacerun: yes;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="color: blue;">End</span> <span style="color: blue;">Try               <br /></span></span><span style="font-size: 10pt; font-family: &quot;Courier New&quot;; mso-no-proof: yes;"><span style="mso-spacerun: yes;">&#0160;&#0160;&#0160; </span><span style="color: blue;">End</span> <span style="color: blue;">If               <br /></span></span><span style="font-size: 10pt; font-family: &quot;Courier New&quot;; mso-no-proof: yes;"><span style="mso-spacerun: yes;">&#0160;&#0160;&#0160; </span><span style="color: blue;">Return</span> <span style="color: blue;">Nothing               <br /></span></span><span style="font-size: 10pt; color: blue; line-height: 115%; font-family: &quot;Courier New&quot;; mso-no-proof: yes;">End</span><span style="font-size: 10pt; line-height: 115%; font-family: &quot;Courier New&quot;; mso-no-proof: yes;"> <span style="color: blue;">Function</span></span></p>
</td>
</tr>
</tbody>
</table>
<p><img alt="" src="/assets/TipsAndTricks3-1.png" /></p>
