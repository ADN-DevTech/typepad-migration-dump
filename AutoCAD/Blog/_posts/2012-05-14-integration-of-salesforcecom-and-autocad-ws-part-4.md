---
layout: "post"
title: "Integration of Salesforce.com and AutoCAD WS - Part 4"
date: "2012-05-14 19:21:00"
author: "Daniel Du"
categories:
  - "AutoCAD"
  - "Cloud"
  - "Daniel Du"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/integration-of-salesforcecom-and-autocad-ws-part-4.html "
typepad_basename: "integration-of-salesforcecom-and-autocad-ws-part-4"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/daniel-du.html">Daniel Du</a></p>  <p><img alt="" src="/assets/image_845925.jpg" width="211" height="66" /></p>  <p>In <a href="http://adndevblog.typepad.com/autocad/2012/05/integration-of-salesforcecom-and-autocad-ws-part-3.html" target="_blank">part 3</a>, I have already list the attachments in visual force page, and add one command link behind each attachment. Now I want to pass parameters to Apex class controllers from visual force page. I need to know which attachment should be opened in AutoCAD WS when user clicks the command link, I am passing the attachment ID as parameter.     <br /></p>  <pre class="csharpcode"><span class="kwrd">&lt;</span><span class="html">script</span> <span class="attr">type</span><span class="kwrd">=&quot;text/javascript&quot;</span><span class="kwrd">&gt;</span> 
 <span class="kwrd">function</span> openInWS(url){
   window.open(url);
 }
<span class="kwrd">&lt;/</span><span class="html">script</span><span class="kwrd">&gt;</span></pre>
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

<pre class="csharpcode"><span class="kwrd">&lt;</span><span class="html">apex:column</span> <span class="attr">id</span><span class="kwrd">=&quot;colOpenInAcadWS&quot;</span> 
       <span class="attr">rendered</span><span class="kwrd">=&quot;{!CONTAINS(item.ContentType, 'application/x-dwg')}&quot;</span><span class="kwrd">&gt;</span>
    <span class="kwrd">&lt;</span><span class="html">p</span> <span class="attr">id</span><span class="kwrd">=&quot;{!count}.openInAcadWS&quot;</span><span class="kwrd">&gt;&lt;/</span><span class="html">p</span><span class="kwrd">&gt;</span>
    <span class="kwrd">&lt;</span><span class="html">apex:commandLink</span> <span class="attr">action</span><span class="kwrd">=&quot;{!transferToWSStorage}&quot;</span> 
         <span class="attr">title</span><span class="kwrd">=&quot;Open In AutoCAD WS&quot;</span>   <br />         <span class="attr">id</span><span class="kwrd">=&quot;btnOpenInAutoCADWs&quot;</span>
         <span class="attr">value</span><span class="kwrd">=&quot;Open In AutoCAD WS&quot;</span> 
         <span class="attr">styleClass</span><span class="kwrd">=&quot;btn&quot;</span>  <span class="attr">style</span><span class="kwrd">=&quot;text-decoration:none&quot;</span>
         <span class="attr">oncomplete</span><span class="kwrd">=&quot;openInWS('{!openInAutocadWSUrl}');&quot;</span> <span class="kwrd">&gt;</span>
        <span class="kwrd">&lt;</span><span class="html">apex:param</span> <span class="attr">name</span><span class="kwrd">=&quot;selectedAttachmentId&quot;</span> 
              <span class="attr">value</span><span class="kwrd">=&quot;{!item.Id}&quot;</span><span class="kwrd">/&gt;</span>
    <span class="kwrd">&lt;/</span><span class="html">apex:commandLink</span><span class="kwrd">&gt;</span>
    <span class="kwrd">&lt;</span><span class="html">apex:pageBlock</span> <span class="attr">id</span><span class="kwrd">=&quot;hiddenBlock&quot;</span> <span class="attr">rendered</span><span class="kwrd">=&quot;false&quot;</span><span class="kwrd">&gt;</span>
    <span class="kwrd">&lt;/</span><span class="html">apex:pageBlock</span><span class="kwrd">&gt;</span>
<span class="kwrd">&lt;/</span><span class="html">apex:column</span><span class="kwrd">&gt;</span></pre>
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

<p>Here I assign an action “transferToWSStorage” to command link, it is defined in controller, it will be executed when command link is clicked. Another visual force tag &lt;apex: param&gt; is added to pass parameters. </p>

<p>To avoid query database twice, I store the attachments into a map, so that it can be retrieved by ID without query database again.</p>

<p>Here is the controller code: </p>
<style type="text/css">













    <!--code { font-family: Courier New, Courier; font-size: 10pt; margin: 0px; }-->
  </style><meta content="text/html; charset=iso-8859-1" http-equiv="Content-Type" /><!-- ======================================================== --><!-- = Java Sourcecode to HTML automatically converted code = --><!-- =   Java2Html Converter 5.0 [2006-02-26] by Markus Gebhard  markus@jave.de   = --><!-- =     Further information: http://www.java2html.de     = -->

<div class="java" align="left">
  <table border="0" cellspacing="0" cellpadding="3" bgcolor="#ffffff"><tbody>
      <tr><!-- start source code -->
        <td valign="top" nowrap="nowrap" align="left"><code><font color="#7f0055"><b>public </b></font><font color="#000000">with sharing </font><font color="#7f0055"><b>class&#160; <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </b></font><font color="#000000">OpenInAutocadWS_Controller </font><font color="#000000">{</font> 

            <br /><font color="#ffffff"></font>

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#7f0055"><b>public </b></font><font color="#000000">List&lt;Attachment&gt; listAttachment&#160; <br />&#160;&#160;&#160; </font><font color="#000000">{</font><font color="#000000">get;set;</font><font color="#000000">}</font> 

            <br />

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#7f0055"><b>public </b></font><font color="#000000">Map&lt;String, Attachment&gt; mapAttachment&#160;&#160; <br />&#160;&#160;&#160; </font><font color="#000000">{</font><font color="#000000">get; set;</font><font color="#000000">}</font> 

            <br />

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#7f0055"><b>public </b></font><font color="#000000">Case currentCase </font><font color="#000000">{</font><font color="#000000">get; set;</font><font color="#000000">}</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160; </font>

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#7f0055"><b>public </b></font><font color="#000000">string openInAutocadWSUrl</font><font color="#000000">{</font><font color="#000000">get;set;</font><font color="#000000">}</font>&#160; <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font>

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160; </font><font color="#3f7f5f">//constructor</font>&#160; <br /><font color="#ffffff">&#160;&#160;&#160;&#160; </font><font color="#7f0055"><b>public </b></font><font color="#000000">OpenInAutocadWS_Controller&#160; <br />&#160;&#160;&#160;&#160;&#160; </font><font color="#000000">(</font><font color="#000000">ApexPages.StandardController controller</font><font color="#000000">) {</font> 

            <br /><font color="#ffffff"></font>

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#3f7f5f">//get the controller instance</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#000000">currentCase = </font><font color="#000000">(</font><font color="#000000">Case</font><font color="#000000">)</font><font color="#000000">controller 
              <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; .getRecord</font><font color="#000000">()</font><font color="#000000">;</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font>

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#3f7f5f">//initialize the attachments list</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#000000">listAttachment = </font><font color="#7f0055"><b>new </b></font><font color="#000000">List&lt;Attachment&gt;</font><font color="#000000">()</font><font color="#000000">;</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font>

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#3f7f5f">//get all the case related attachments </font>

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#000000">listAttachment = 
              <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#000000">[</font><font color="#000000">Select Id, ContentType, Name, Body&#160;&#160; <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; from Attachment&#160;&#160; <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; where ParentId = :currentCase.Id&#160;&#160; <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; and Name like </font><font color="#ff6100">'%D</font><font color="#000000">WG</font><font color="#ff6100">'];</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font>

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#3f7f5f">//save into a map for latter use</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#000000">mapAttachment = 
              <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#7f0055"><b>new </b></font><font color="#000000">Map&lt;String, Attachment&gt;</font><font color="#000000">()</font><font color="#000000">;</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#7f0055"><b>for</b></font><font color="#000000">(</font><font color="#000000">Attachment item : listAttachment </font><font color="#000000">) {</font>&#160; <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#3f7f5f">//trim() is important!!!!&#160;&#160; <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; //Where the hell blanks comes from?!&#160;&#160; <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; //It took me almost two days to debug... </font>

            <br /><font color="#000000">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; mapAttachment.put</font><font color="#000000">( 
              <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#000000">String.valueOf</font><font color="#000000">(</font><font color="#000000">item.Id</font><font color="#000000">)</font><font color="#000000">.trim</font><font color="#000000">()</font><font color="#000000">,&#160; <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; item</font><font color="#000000">)</font><font color="#000000">;</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#000000">System.Debug</font><font color="#000000">(</font><font color="#ff6100">'ID</font><font color="#000000">=</font><font color="#ff6100">' + </font><font color="#000000">item.Id&#160; <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; + </font><font color="#ff6100">'Na</font><font color="#000000">me=</font><font color="#ff6100">' + </font><font color="#000000">item.name&#160; <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; + </font><font color="#ff6100">' i</font><font color="#000000">s added to map</font><font color="#ff6100">');</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#000000">}</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font>

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#000000">}</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160; </font>

            <br /><font color="#ffffff">&#160;&#160;&#160; </font>

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#7f0055"><b>public </b></font><font color="#000000">string selectedAttachmentId 
              <br />&#160;&#160;&#160; </font><font color="#000000">{</font><font color="#000000">get; set; </font><font color="#000000">}</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160; </font>

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#7f0055"><b>public </b></font><font color="#7f0055"><b>void </b></font><font color="#000000">transferToWSStorage</font><font color="#000000">()</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#000000">{</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160; </font>

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#3f7f5f">//trim() is important!!!!&#160; <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; //Where the hell blanks comes from?!&#160; <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; //It took me almost two days to debug...</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#000000">selectedAttachmentId =&#160;&#160; <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; ApexPages.currentPage</font><font color="#000000">() 
              <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#000000">.getParameters</font><font color="#000000">() 
              <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#000000">.get</font><font color="#000000">(</font><font color="#ff6100">'se</font><font color="#000000">lectedAttachmentId</font><font color="#ff6100">').</font><font color="#000000">trim</font><font color="#000000">()</font><font color="#000000">;</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font>

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#3f7f5f">//I have already get the selected attachment ID, </font>

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#3f7f5f">//this is to get the attachement from map by id</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#000000">Attachment attach =&#160; <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; getAttachmentById</font><font color="#000000">(</font><font color="#000000">selectedAttachmentId</font><font color="#000000">)</font><font color="#000000">;</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160; <br /><code><font color="#3f7f5f">&#160;&#160;&#160;&#160;&#160;&#160;&#160; //TO DO: Transfer the attachment to AutoCAS WS&#160; <br />&#160;&#160;&#160;&#160;&#160;&#160; // and get file open url in AutoCAD WS</font></code></font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160; </font>

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#3f7f5f">//TO DO: set openInAutocadWSUrl&#160; <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; //so that it can be opened in AutoCAD WS</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#000000">openInAutocadWSUrl = ‘https://www.autocadws.com/…’;</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font>

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#000000">}</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160; </font>

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#3f7f5f">//get the attachment from map by id</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#7f0055"><b>public </b></font><font color="#000000">Attachment getAttachmentById 
              <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#000000">(</font><font color="#000000">String attachmentId</font><font color="#000000">){</font> 

            <br /><font color="#ffffff"></font>

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#3f7f5f">//This blocks me almost 2 days,&#160; <br />&#160;&#160;&#160;&#160;&#160;&#160; //just because the trim() issue!</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#000000">Attachment att = mapAttachment 
              <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; .get</font><font color="#000000">(</font><font color="#000000">attachmentId</font><font color="#000000">)</font><font color="#000000">;</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#7f0055"><b>return </b></font><font color="#000000">att;</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font>

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#000000">}</font> 

            <br /><font color="#000000">}</font></code> </td>
<!-- end source code --></tr>
    </tbody></table>
</div>
<!-- =       END of automatically generated HTML code       = --><!-- ======================================================== -->

<p>You probably have known, to access Apex objects from visual force page, we can use {! apexPublicVarible}. For example, I will open the DWG file in AutoCAD WS, after get the file URL I will open it with JavaScript. </p>

<p>An attribute is defined in Apex Class:</p>

<blockquote>
  <p><code><font color="#ffffff"></font><font color="#7f0055"><b>public </b></font><font color="#000000">string openInAutocadWSUrl</font><font color="#000000">{</font><font color="#000000">get;set;</font><font color="#000000">}</font> </code></p>
</blockquote>

<p>To get the value from apex class controller and use as parameter of JavaScript: </p>

<blockquote>
  <p><span class="attr">oncomplete</span><span class="kwrd">=&quot;openInWS('{!openInAutocadWSUrl}');&quot;</span></p>
</blockquote>

<p>Please pay attention to the singe quote in parameter.</p>

<p>I am using an &lt;Apex : param&gt; tag to pass attachment ID to Apex class controller.&#160; </p>

<p><span class="kwrd">&lt;</span><span class="html">apex:param</span> <span class="attr">name</span><span class="kwrd">=&quot;selectedAttachmentId&quot;</span> 

  <br /><span class="attr">value</span><span class="kwrd">=&quot;{!item.Id}&quot;</span><span class="kwrd">/&gt;</span> </p>

<p>The value can be retrieved from Apex class by following code: </p>

<p><code><font color="#ffffff"></font><font color="#000000">selectedAttachmentId = 
      <br />ApexPages.currentPage</font><font color="#000000">() 
      <br /></font><font color="#000000">.getParameters</font><font color="#000000">() 
      <br /></font><font color="#000000">.get</font><font color="#000000">(</font><font color="#ff6100">'se</font><font color="#000000">lectedAttachmentId</font><font color="#ff6100">').</font><font color="#000000">trim</font><font color="#000000">()</font><font color="#000000">;</font> </code></p>

<p>Actually the code is pretty simple, but a very small bug blocked me almost 2 days! As careless as me, I accidentally type a SPACE in value, so the value passed to the controller is not correct, no wander I cannot get the correct item by <code><font color="#000000">mapAttachment</font> </code>(attchemendId); </p>

<p><span class="kwrd">&lt;</span><span class="html">apex:param</span> <span class="attr">name</span><span class="kwrd">=&quot;selectedAttachmentId&quot;</span>&#160; <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span class="attr">value</span><span class="kwrd">=&quot;[<font style="background-color: #ffff00">SPACE</font>]{!item.Id}&quot;</span><span class="kwrd">/&gt;</span> </p>

<p>It is not a big deal, but is very difficult to debug! I will talk about debug in next post.</p>

<p>Stay tuned and have fun!</p>
