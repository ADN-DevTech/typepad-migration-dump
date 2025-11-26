---
layout: "post"
title: "Integration of Salesforce.com and AutoCAD WS - Part 3"
date: "2012-05-13 19:15:00"
author: "Daniel Du"
categories:
  - "AutoCAD"
  - "Cloud"
  - "Daniel Du"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/integration-of-salesforcecom-and-autocad-ws-part-3.html "
typepad_basename: "integration-of-salesforcecom-and-autocad-ws-part-3"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/daniel-du.html">Daniel Du</a>&nbsp;</p>
<p><img src="/assets/image_845925.jpg" alt="" width="259" height="81" /></p>
<p>In <a href="http://adndevblog.typepad.com/autocad/2012/05/integration-of-salesforcecom-and-autocad-ws-part-1.html" target="_blank">part 1</a> and <a href="http://adndevblog.typepad.com/autocad/2012/05/integration-of-salesforcecom-and-autocad-ws-part-2.html" target="_blank">part 2</a> we introduced how to setup the browses based development mode tool and desktop based Force.com IDE. In this post, let’s do some actual work.</p>
<p>We have some DWG attachments in a case, what I am trying to do is, to list these DWG attachments and add a button behind of each, when the button is clicked, the DWG attachment should be opened in AutoCAD WS. Let’s list the attachment first in this post.</p>
<p>There is a standard attachment list in case page layout, but I’d rather create my own attachment list so that I can add buttons behind, so I will create a visual force page. and we can put this visual force page to case page layout, we will discuss this in latter part. Let’s say we have 2 DWG attachments for one case. The visual force page is supposed to look like below:</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0168eb674d4d970c-pi"><img style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" src="/assets/image_234686.jpg" border="0" alt="image" width="483" height="104" /></a></p>
<p>Now we are going to implement this page. Firstly let’s create a visual force page, named as OpenInAutocadWS.</p>
<p>Developers can use Visualforce to create a Visualforce page definition. A page definition consists of two primary elements: Visualforce markup and&nbsp; Visualforce controller. Visualforce markup consists of Visualforce tags, HTML, JavaScript, or any other Web-enabled code embedded within a single &lt;apex:page&gt; tag. The markup defines the user interface components that should be included on the page, and the way they should appear.A Visualforce controller is a set of instructions that specify what happens when a user interacts with the components specified in associated Visualforce markup, such as when a user clicks a button or link. Controllers also provide access to the data that should be displayed in a page, and can modify component behavior. A developer can either use a standard controller provided by the Force.com platform, or add custom controller logic with a class written in Apex.</p>
<p>In this page, we are trying to list attachment of case, so we can take advantage the “case” standard controller, we also need to create our custom controller extension to implement our custom logic.</p>

<pre class="csharpcode"><span class="kwrd">&lt;</span><span class="html">apex:page</span> <span class="attr">standardController</span><span class="kwrd">=&quot;Case&quot;</span> 
  <span class="attr">extensions</span><span class="kwrd">=&quot;OpenInAutocadWS_Controller&quot;</span> <span class="kwrd">&gt;</span>

<span class="kwrd">&lt;</span><span class="html">apex:form</span> <span class="attr">id</span><span class="kwrd">=&quot;frm&quot;</span><span class="kwrd">&gt;</span>
  <span class="kwrd">&lt;</span><span class="html">apex:pageBlock</span> <span class="attr">id</span><span class="kwrd">=&quot;pb&quot;</span><span class="kwrd">&gt;</span>
   <span class="kwrd">&lt;</span><span class="html">apex:variable</span> <span class="attr">value</span><span class="kwrd">=&quot;{!0}&quot;</span> <span class="attr">var</span><span class="kwrd">=&quot;count&quot;</span> <span class="kwrd">/&gt;</span>
   <span class="kwrd">&lt;</span><span class="html">apex:pageBlockTable</span> 
      <span class="attr">value</span><span class="kwrd">=&quot;{!listAttachment}&quot;</span> 
      <span class="attr">var</span><span class="kwrd">=&quot;item&quot;</span> <span class="attr">id</span><span class="kwrd">=&quot;pbt&quot;</span><span class="kwrd">&gt;</span>
      <span class="kwrd">&lt;</span><span class="html">apex:column</span> <span class="attr">headerValue</span><span class="kwrd">=&quot;Name&quot;</span><span class="kwrd">&gt;</span>
         {!item.Name}
      <span class="kwrd">&lt;/</span><span class="html">apex:column</span><span class="kwrd">&gt;</span>
      <span class="kwrd">&lt;</span><span class="html">apex:column</span> <span class="attr">headerValue</span><span class="kwrd">=&quot;Download&quot;</span><span class="kwrd">&gt;</span>
        <span class="rem">&lt;!-- {!item.Id} –&gt;</span>
        <span class="kwrd">&lt;</span><span class="html">a</span> <span class="attr">href</span><span class="kwrd">=&quot;https://c.na9.content.force.com
          /servlet/servlet.FileDownload?file
          ={!item.Id}&quot;</span><span class="kwrd">&gt;</span>download<span class="kwrd">&lt;/</span><span class="html">a</span><span class="kwrd">&gt;</span>
      <span class="kwrd">&lt;/</span><span class="html">apex:column</span><span class="kwrd">&gt;</span>

      <span class="kwrd">&lt;</span><span class="html">apex:column</span> <span class="attr">id</span><span class="kwrd">=&quot;colOpenInAcadWS&quot;</span>
        <span class="attr">rendered</span><span class="kwrd">=&quot;{!CONTAINS(item.ContentType, <br />                       'application/x-dwg')}&quot;</span><span class="kwrd">&gt;</span>
        <span class="kwrd">&lt;</span><span class="html">p</span> <span class="attr">id</span><span class="kwrd">=&quot;{!count}.openInAcadWS&quot;</span><span class="kwrd">&gt;&lt;/</span><span class="html">p</span><span class="kwrd">&gt;</span>
        <span class="kwrd">&lt;</span><span class="html">apex:commandLink</span> <br />             <span class="attr">title</span><span class="kwrd">=&quot;Open In AutoCAD WS&quot;</span>  
             <span class="attr">id</span><span class="kwrd">=&quot;btnOpenInAutoCADWs&quot;</span>
             <span class="attr">value</span><span class="kwrd">=&quot;Open In AutoCAD WS&quot;</span> 
             <span class="attr">styleClass</span><span class="kwrd">=&quot;btn&quot;</span>  <br />             <span class="attr">style</span><span class="kwrd">=&quot;text-decoration:none&quot;</span>
        <span class="kwrd">&lt;/</span><span class="html">apex:commandLink</span><span class="kwrd">&gt;</span>
        <span class="kwrd">&lt;</span><span class="html">apex:pageBlock</span> <span class="attr">id</span><span class="kwrd">=&quot;hiddenBlock&quot;</span> <br />              <span class="attr">rendered</span><span class="kwrd">=&quot;false&quot;</span><span class="kwrd">&gt;</span>
        <span class="kwrd">&lt;/</span><span class="html">apex:pageBlock</span><span class="kwrd">&gt;</span>
        <span class="kwrd">&lt;</span><span class="html">apex:variable</span> <span class="attr">value</span><span class="kwrd">=&quot;{!count+1}&quot;</span> <br />                       <span class="attr">var</span><span class="kwrd">=&quot;count&quot;</span> <span class="kwrd">/&gt;</span>
      <span class="kwrd">&lt;/</span><span class="html">apex:column</span><span class="kwrd">&gt;</span>
     <span class="kwrd">&lt;/</span><span class="html">apex:pageBlockTable</span><span class="kwrd">&gt;</span>
    <span class="kwrd">&lt;/</span><span class="html">apex:pageBlock</span><span class="kwrd">&gt;</span>
<span class="kwrd">&lt;/</span><span class="html">apex:form</span><span class="kwrd">&gt;</span>
<span class="kwrd">&lt;/</span><span class="html">apex:page</span><span class="kwrd">&gt;</span></pre>
<style type="text/css">














.csharpcode, .csharpcode pre
{
	font-size: small;
	color: black;
	font-family: consolas, "Courier New", courier, monospace;
	background-color: #ffffff;
	/*white-space: pre;*/
}
.csharpcode pre { margin: 0em; }
.csharpcode .rem { color: #008000; }
.csharpcode .kwrd { color: #0000ff; }
.csharpcode .str { color: #006080; }
.csharpcode .op { color: #0000c0; }
.csharpcode .preproc { color: #cc6633; }
.csharpcode .asp { background-color: #ffff00; }
.csharpcode .html { color: #800000; }
.csharpcode .attr { color: #ff0000; }
.csharpcode .alt 
{
	background-color: #f4f4f4;
	width: 100%;
	margin: 0em;
}
.csharpcode .lnum { color: #606060; }</style>

<p>Here is the source code of OpenInAutocadWS_Controller, which is an Apex class. </p>
<style type="text/css">








    <!--code { font-family: Courier New, Courier; font-size: 10pt; margin: 0px; }-->
  </style><meta content="text/html; charset=iso-8859-1" http-equiv="Content-Type" /><!-- ======================================================== --><!-- = Java Sourcecode to HTML automatically converted code = --><!-- =   Java2Html Converter 5.0 [2006-02-26] by Markus Gebhard  markus@jave.de   = --><!-- =     Further information: http://www.java2html.de     = -->

<div class="java" align="left">
  <table border="0" cellspacing="0" cellpadding="3" bgcolor="#ffffff"><tbody>
      <tr><!-- start source code -->
        <td valign="top" nowrap="nowrap" align="left"><code><font color="#7f0055"><b>public </b></font><font color="#000000">with sharing </font><font color="#7f0055"><b>class&#160; <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </b></font><font color="#000000">OpenInAutocadWS_Controller </font><font color="#000000">{</font> 

            <br /><font color="#ffffff"></font>

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#7f0055"><b>public </b></font><font color="#000000">List&lt;Attachment&gt; listAttachment 
              <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#000000">{</font><font color="#000000">get;set;</font><font color="#000000">}</font>&#160;&#160; <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font>

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#3f7f5f">//constructor</font> 

            <br /><font color="#7f0055"><b>&#160;&#160;&#160; public </b></font><font color="#000000">OpenInAutocadWS_Controller&#160;&#160; <br />&#160;&#160;&#160;&#160; </font><font color="#000000">(</font><font color="#000000">ApexPages.StandardController controller</font><font color="#000000">) {</font>&#160; <br /><font color="#ffffff"></font>

            <br /><font color="#3f7f5f">&#160;&#160;&#160;&#160; //get the controller instance</font> 

            <br /><font color="#000000">&#160;&#160;&#160;&#160; currentCase = </font><font color="#000000">(</font><font color="#000000">Case</font><font color="#000000">)</font><font color="#000000">controller.getRecord</font><font color="#000000">()</font><font color="#000000">;</font>&#160; <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font>

            <br /><font color="#3f7f5f">&#160;&#160;&#160;&#160; //initialize the attachments list</font> 

            <br /><font color="#000000">&#160;&#160;&#160;&#160; listAttachment = </font><font color="#7f0055"><b>new </b></font><font color="#000000">List&lt;Attachment&gt;</font><font color="#000000">()</font><font color="#000000">;</font>&#160; <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font>

            <br /><font color="#3f7f5f">&#160;&#160;&#160;&#160; //get all the case related DWG attachments </font>

            <br /><font color="#000000">&#160;&#160;&#160;&#160; listAttachment = 
              <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#000000">[</font><font color="#000000">Select Id, ContentType, Name, Body&#160;&#160; <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; from Attachment&#160;&#160; <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; where ParentId = :currentCase.Id&#160;&#160; <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; and Name like </font><font color="#ff6100">'%D</font><font color="#000000">WG</font><font color="#ff6100">'];</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font>

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#000000">}</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160; </font>

            <br /><font color="#000000">}</font></code> </td>
<!-- end source code --></tr>

      <tr>
        <td valign="top" nowrap="nowrap" align="left">&#160;</td>
      </tr>
    </tbody></table>
</div>

<p>With that code, we created a visualforce page, and bind the attachment information to this page.&#160; In the controller, we use SOQL(Saleforce Object Query Language) to retrieve attachment from salesforce.com database and save it to a list. In Visualforce page, the list is banded to a page bloc table. </p>

<p>OK, are you following me? Did you create the visual force page and list out the attachment successfully? Now user can download the attachment file from following url <span class="kwrd"><a href="https://c.na9.content.force.com /servlet/servlet.FileDownload?file ={!item.Id}">https://c.na9.content.force.com /servlet/servlet.FileDownload?file ={!item.Id}</a>. </span>But for my case, is it not what I want, I would like to open it AutoCAD WS directly without downloading anything. We will discuss this in coming post.</p>

<p>Stay tuned and have fun!</p>
