---
layout: "post"
title: "Integration of Salesforce.com and AutoCAD WS - Part 6"
date: "2012-05-20 20:42:00"
author: "Daniel Du"
categories:
  - "AutoCAD"
  - "Cloud"
  - "Daniel Du"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/integration-of-salesforcecom-and-autocad-ws-part-6.html "
typepad_basename: "integration-of-salesforcecom-and-autocad-ws-part-6"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/daniel-du.html">Daniel Du</a></p>  <p><img alt="" src="/assets/image_845925.jpg" width="259" height="81" /></p>  <p>In <a href="http://adndevblog.typepad.com/autocad/2012/05/integration-of-salesforcecom-and-autocad-ws-part-3.html" target="_blank">part3</a>, <a href="http://adndevblog.typepad.com/autocad/2012/05/integration-of-salesforcecom-and-autocad-ws-part-4.html" target="_blank">part 4</a> and <a href="http://adndevblog.typepad.com/autocad/2012/05/integration-of-salesforcecom-and-autocad-ws-part-5.html" target="_blank">part 5</a> we introduced how to list the DWG attachments and how to debug in force.com application. in this post, we will go ahead to transfer the DWG attachment to AutoCAD WS storage. </p>  <p>AutoCAD WS <a href="http://www.autocadws.com/blog/announcing-autocad-ws-apis/" target="_blank">announced</a> its API. Currently it is still the fist step, includes connect to AutoCAD WS account, manipulate files on AutoCAD WS storage, and launch a drawing in WS online editor. These APIs rely on the industry standard http based <a href="http://en.wikipedia.org/wiki/WebDAV">WebDAV</a> protocol which can be used in a variety of programming languages such as C#, C++, Java and JavaScript to access AutoCAD WS’s functionality from within web browsers, desktop applications and server-side components. Today, we will do it in Apex code. </p>  <p>AutoCAD WS team produced two tutorials with full source code samples showing how to use the new set of APIs using either <a href="http://www.autocadws.com/tutorials/autocad-ws-apis-c-client/" target="_blank">C#</a> or <a href="http://www.autocadws.com/tutorials/autocad-ws-apis-javascript-client/" target="_blank">JavaScript</a>. It may helps you to understand the mechanism so that we can create a similar one in Apex. </p>  <p>For our requirement, to transfer DWG attachments to AutoCAD WS and open it in WS online editor, I would not implement all functions like the one in <a href="http://www.autocadws.com/tutorials/autocad-ws-apis-c-client/" target="_blank">C#</a> or <a href="http://www.autocadws.com/tutorials/autocad-ws-apis-javascript-client/" target="_blank">Javascript,</a> Anther difference is the sample tutorial is trying to upload files from local drive to AutoCAD WS storage, it does apply to our scenario. What we want to do is, transfer files from one cloud to another, so we need to this part ourselves. </p>  <p>I created a class named as WebdavManager_WebdavClient. The constructor of the WebdavClient class expects three arguments:</p>  <ol>   <li>url: The Webdav server addres. For example, if you want to connect to Autocad WS server, use: ‘https://dav.autocadws.com’ </li>    <li>username: the username of your account in the Webdav server </li>    <li>password: the password of your account in the Webdav server </li> </ol>  <p>The following code creates new WebdavClient class, connecting to Autocad WS Webdav server, with the username ‘someuser’ and the password ’123456′.</p>  <pre class="csharpcode">WebdavManager_WebdavClient client =
  <span class="kwrd">new</span> WebdavManager_WebdavClient(
            <span class="str">'https://dav.autocadws.com/'</span>,
            <span class="str">'someuser'</span>, <span class="str">'123456'</span>);</pre>
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

<p>Next step is to create a method to transfer DWG file stream, it uses HttpRequest object and “PUT” method. The signature is:</p>

<pre class="csharpcode"><span class="kwrd">public</span> boolean Put(<span class="kwrd">string</span> remoteFilePath, <br />                   Attachment attach)</pre>
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

<p>Here is the code to call WebdavManager_WebdavClient.Put method to transfer DWG attachment to AutoCAD WS storage. It is part of transferToWSStorage method, which is introduced in <a href="http://adndevblog.typepad.com/autocad/2012/05/integration-of-salesforcecom-and-autocad-ws-part-4.html" target="_blank">part4</a>.</p>
<style type="text/css">






    <!--code { font-family: Courier New, Courier; font-size: 10pt; margin: 0px; }-->
  </style><meta content="text/html; charset=iso-8859-1" http-equiv="Content-Type" /><!-- ======================================================== --><!-- = Java Sourcecode to HTML automatically converted code = --><!-- =   Java2Html Converter 5.0 [2006-02-26] by Markus Gebhard  markus@jave.de   = --><!-- =     Further information: http://www.java2html.de     = -->

<div class="java" align="left">
  <table border="0" cellspacing="0" cellpadding="3" bgcolor="#ffffff"><tbody>
      <tr><!-- start source code -->
        <td valign="top" nowrap="nowrap" align="left"><code><font color="#7f0055"><b>public </b></font><font color="#7f0055"><b>void </b></font><font color="#000000">transferToWSStorage</font><font color="#000000">()</font> 

            <br /><font color="#000000">{</font> 

            <br /><font color="#ffffff"></font>

            <br /><font color="#ffffff"></font>

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#3f7f5f">//trim() is important!!!! </font>

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#3f7f5f">//Where the hell blanks comes from?! </font>

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#3f7f5f">//It took me almost two days to debug...</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#000000">selectedAttachmentId =&#160; ApexPages.currentPage</font><font color="#000000">()</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160; </font><font color="#000000">.getParameters</font><font color="#000000">()</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160; </font><font color="#000000">.get</font><font color="#000000">(</font><font color="#ff6100">'se</font><font color="#000000">lectedAttachmentId</font><font color="#ff6100">').</font><font color="#000000">trim</font><font color="#000000">()</font><font color="#000000">;</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160; </font>

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#3f7f5f">//I have already get the selected attachment ID, </font>

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#3f7f5f">//this is to get the attachement from map by id</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#000000">Attachment attach = </font>

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160; </font><font color="#000000">getAttachmentById</font><font color="#000000">(</font><font color="#000000">selectedAttachmentId</font><font color="#000000">)</font><font color="#000000">;</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font>

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><strong><font color="#000000">string userName = </font><font color="#ff6100">'&lt;your autocad WS username&gt;</font><font color="#ff6100">';</font> 

              <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#000000">string password = </font><font color="#ff6100">'&lt;your autocad WS password&gt;</font><font color="#ff6100">';</font> 

              <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#3f7f5f">//please check Setup-&gt;Security-&gt;Remote site settings. </font>

              <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#000000">string autocadWsHost = </font><font color="#ff6100">'ht</font><font color="#000000">tps:</font><font color="#3f7f5f">//dav.autocadws.com/';&#160; </font>

              <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#000000">string dstRelativePath = attach.Name;</font> 

              <br /><font color="#ffffff">&#160;&#160;&#160; </font>

              <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#000000">WebdavManager_WebdavClient client </font>

              <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160; </font><font color="#000000">= </font><font color="#7f0055">new </font><font color="#000000">WebdavManager_WebdavClient</font> 

              <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#000000">(</font> 

              <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#000000">autocadWsHost,</font> 

              <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#000000">userName,</font> 

              <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#000000">password</font> 

              <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#000000">)</font><font color="#000000">;</font> 

              <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#000000">client.Put</font><font color="#000000">(</font><font color="#000000">dstRelativePath, attach</font><font color="#000000">)</font><font color="#000000">;&#160;&#160;&#160; </font>

              <br /></strong><font color="#ffffff">&#160;&#160;&#160; </font>

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#3f7f5f">//set openInAutocadWSUrl </font>

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#3f7f5f">//so that it can be opened in AutoCAD WS</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#000000">openInAutocadWSUrl = client</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#000000">.getOpenDrawingUrl</font><font color="#000000">(</font><font color="#000000">dstRelativePath</font><font color="#000000">)</font><font color="#000000">;</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160; </font>

            <br /><font color="#000000">}</font></code> </td>
<!-- end source code --></tr>
    </tbody></table>
</div>
<!-- =       END of automatically generated HTML code       = --><!-- ======================================================== -->

<p>Here is the complete code, I use the user credentials to generate&#160; the Authorization header.&#160; The Authorization header is sent to the webdav server in each request,&#160; and the server uses this header&#160; to authorize the user. I also set http header <code><font color="#ff6100">'Co</font><font color="#000000">ntent-Type</font><font color="#990000">'</font></code> to 'application/x-dwg;', indicating it is a DWG file, and set the HTTP method to “PUT” and send the binary stream of DWG file. Please note that the DWG file should be less than 3MB, due to salesforce.com limit. We may need another way to transfer large files, if you have any suggestions, please do let me know, I really appreciate that. </p>
<style type="text/css">



    <!--code { font-family: Courier New, Courier; font-size: 10pt; margin: 0px; }-->
  </style><meta content="text/html; charset=iso-8859-1" http-equiv="Content-Type" /><!-- ======================================================== --><!-- = Java Sourcecode to HTML automatically converted code = --><!-- =   Java2Html Converter 5.0 [2006-02-26] by Markus Gebhard  markus@jave.de   = --><!-- =     Further information: http://www.java2html.de     = -->

<div class="java" align="left">
  <table border="0" cellspacing="0" cellpadding="3" bgcolor="#ffffff"><tbody>
      <tr><!-- start source code -->
        <td valign="top" nowrap="nowrap" align="left"><code><font color="#7f0055"><b>public </b></font><font color="#000000">with sharing </font><font color="#7f0055"><b>class </b></font><font color="#000000">WebdavManager_WebdavClient </font><font color="#000000">{</font> 

            <br /><font color="#ffffff">&#160; </font>

            <br /><font color="#ffffff">&#160; </font><font color="#7f0055"><b>public </b></font><font color="#000000">string host</font><font color="#000000">{</font><font color="#000000">get;set;</font><font color="#000000">}</font> 

            <br /><font color="#ffffff">&#160; </font><font color="#7f0055"><b>public </b></font><font color="#000000">string username </font><font color="#000000">{</font><font color="#000000">get;set;</font><font color="#000000">}</font> 

            <br /><font color="#ffffff">&#160; </font><font color="#7f0055"><b>public </b></font><font color="#000000">string password</font><font color="#000000">{</font><font color="#000000">get;set;</font><font color="#000000">}</font> 

            <br /><font color="#ffffff">&#160; </font>

            <br /><font color="#ffffff">&#160; </font><font color="#7f0055"><b>public </b></font><font color="#000000">WebdavManager_WebdavClient</font><font color="#000000">( 
              <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#000000">string host, 
              <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; string username,&#160; <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; string password</font><font color="#000000">) 
              <br />{</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#7f0055"><b>this</b></font><font color="#000000">.host = host;</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#7f0055"><b>this</b></font><font color="#000000">.username = username;</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#7f0055"><b>this</b></font><font color="#000000">.password = password;</font> 

            <br /><font color="#ffffff">&#160; </font><font color="#000000">}</font>&#160; <br /><font color="#ffffff">&#160; </font>

            <br />

            <br /><font color="#ffffff">&#160; </font><font color="#7f0055"><b>public </b></font><font color="#7f0055"><b>boolean </b></font><font color="#000000">Put</font><font color="#000000">(</font><font color="#000000">string remoteFilePath, 
              <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; Attachment attach</font><font color="#000000">){</font> 

            <br /><font color="#ffffff"></font>

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#000000">HttpRequest req = buildWebServiceRequest 
              <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#000000">( 
              <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#000000">remoteFilePath,&#160; <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; attach.Body 

              <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#000000">)</font><font color="#000000">;</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160; </font>

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#3f7f5f">// Instantiate a new http object </font>

            <br /><font color="#ffffff">&#160;&#160;&#160; </font>

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#000000">Http h = </font><font color="#7f0055"><b>new </b></font><font color="#000000">Http</font><font color="#000000">()</font><font color="#000000">;</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160; </font>

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#3f7f5f">//invoke web service call,</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#3f7f5f">// Send the request, and return a response </font>

            <br /><font color="#ffffff">&#160;&#160;&#160; </font>

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#000000">HttpResponse res = invokeWebService</font><font color="#000000">(</font><font color="#000000">h, req</font><font color="#000000">)</font><font color="#000000">;</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160; </font>

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#3f7f5f">//success ?</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#7f0055"><b>return </b></font><font color="#000000">WebServiceResponseSuccessful</font><font color="#000000">(</font><font color="#000000">res</font><font color="#000000">)</font><font color="#000000">;</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160; </font>

            <br /><font color="#ffffff">&#160; </font><font color="#000000">} </font>

            <br /><font color="#ffffff">&#160; </font>

            <br /><font color="#ffffff">&#160; </font><font color="#7f0055"><b>public </b></font><font color="#000000">HttpRequest buildWebServiceRequest 
              <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#000000">( 
              <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#000000">string remoteFilePath,&#160; <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; Blob body 

              <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#000000">) 
              <br />&#160; {</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160; </font>

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#000000">string endpoint = host+remoteFilePath;</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160; </font>

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#3f7f5f">//use the user credentials to generate&#160; <br />&#160;&#160;&#160; // the Authorization header.</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#3f7f5f">//The Authorization header is sent to&#160; <br />&#160;&#160;&#160; //the webdav server in each request, </font>

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#3f7f5f">//and the server uses this header&#160; <br />&#160;&#160;&#160; //to authorize the user.</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160; </font>

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#000000">string tok = userName + </font><font color="#990000">':' </font><font color="#000000">+ password ;</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#000000">string hash = EncodingUtil 
              <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; .base64Encode</font><font color="#000000">(</font><font color="#000000">Blob.valueOf</font><font color="#000000">(</font><font color="#000000">tok</font><font color="#000000">))</font><font color="#000000">;&#160; </font>

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#000000">string authorizationHeader = </font><font color="#ff6100">'Ba</font><font color="#000000">sic </font><font color="#ff6100">' + </font><font color="#000000">hash;</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160; </font>

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#000000">HttpRequest req = </font><font color="#7f0055"><b>new </b></font><font color="#000000">HttpRequest</font><font color="#000000">()</font><font color="#000000">;</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#000000">req.setHeader</font><font color="#000000">(</font><font color="#ff6100">'Au</font><font color="#000000">thorization</font><font color="#ff6100">',a</font><font color="#000000">uthorizationHeader </font><font color="#000000">)</font><font color="#000000">;</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#000000">req.setHeader</font><font color="#000000">(</font><font color="#ff6100">'Co</font><font color="#000000">ntent-Type</font><font color="#990000">','</font><font color="#000000">application/x-dwg;</font><font color="#ff6100">');&#160;&#160;&#160; </font>

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#000000">req.setMethod</font><font color="#000000">(</font><font color="#ff6100">'PU</font><font color="#000000">T</font><font color="#ff6100">');&#160; </font>

            <br /><font color="#ffffff">&#160;&#160;&#160; </font>

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#000000">req.setEndpoint</font><font color="#000000">(</font><font color="#000000">endPoint</font><font color="#000000">)</font><font color="#000000">;</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160; </font>

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#3f7f5f">//body should less than 3MB</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#3f7f5f">//due to salesforce's limit</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#000000">req.setBodyAsBlob</font><font color="#000000">(</font><font color="#000000">body</font><font color="#000000">)</font><font color="#000000">;</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160; </font>

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#7f0055"><b>return </b></font><font color="#000000">req;</font> 

            <br /><font color="#ffffff">&#160; </font><font color="#000000">}</font> 

            <br /><font color="#ffffff">&#160; </font>

            <br /><font color="#ffffff">&#160; </font><font color="#7f0055"><b>public </b></font><font color="#000000">HttpResponse invokeWebService</font><font color="#000000">(</font><font color="#000000">Http h,&#160; <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; HttpRequest req</font><font color="#000000">){</font> 

            <br /><font color="#ffffff"></font>

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160; </font><font color="#3f7f5f">//Invoke Web Service</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160; </font>

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160; </font><font color="#000000">HttpResponse res = h.send</font><font color="#000000">(</font><font color="#000000">req</font><font color="#000000">)</font><font color="#000000">;</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160; </font><font color="#7f0055"><b>return </b></font><font color="#000000">res;</font> 

            <br /><font color="#ffffff">&#160; </font><font color="#000000">}</font> 

            <br /><font color="#ffffff">&#160; </font>

            <br /><font color="#ffffff">&#160; </font><font color="#7f0055"><b>public </b></font><font color="#7f0055"><b>boolean </b></font><font color="#000000">WebServiceResponseSuccessful 
              <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#000000">(</font><font color="#000000">HttpResponse res</font><font color="#000000">) 
              <br />{</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font>

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#000000">integer statusCode = res.getStatusCode</font><font color="#000000">()</font><font color="#000000">;</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160; </font>

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#3f7f5f">//if status code is 2xx, success </font>

            <br /><font color="#ffffff">&#160;&#160;&#160; </font>

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#7f0055"><b>if</b></font><font color="#000000">(</font><font color="#000000">statusCode &gt;= </font><font color="#990000">200 </font><font color="#000000">&amp;&amp; statusCode &lt; </font><font color="#990000">300</font><font color="#000000">){</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160; </font><font color="#7f0055"><b>return true</b></font><font color="#000000">;</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#000000">}</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#7f0055"><b>else</b></font><font color="#000000">{</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160; </font><font color="#7f0055"><b>return false</b></font><font color="#000000">;</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#000000">}</font> 

            <br /><font color="#ffffff">&#160; </font><font color="#000000">}</font> 

            <br /><font color="#ffffff">&#160; </font>

            <br /><font color="#000000">}</font></code> </td>
<!-- end source code --></tr>
    </tbody></table>
</div>
<!-- =       END of automatically generated HTML code       = --><!-- ======================================================== -->

<p>&#160;</p>

<p>Before you run the code, please check Setup-&gt;Security-&gt;Remote site settings to enable Salesforce to access remote site, add a new remote website to 'https://dav.autocadws.com/': 
  <br /><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0167669dcbf7970b-pi"><img style="background-image: none; border-right-width: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top-width: 0px; border-bottom-width: 0px; border-left-width: 0px; padding-top: 0px" title="image" border="0" alt="image" src="/assets/image_99697.jpg" width="565" height="252" /></a></p>

<p>OK, with that, you can run your visual force page, and click the “Open in AutoCAD WS” button, the DWG file will be transferred to AutoCAD WS, if you open AutoCAD WS at the same time, you will see a new file is uploaded. </p>

<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0168eb9f9448970c-pi"><img style="background-image: none; border-right-width: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top-width: 0px; border-bottom-width: 0px; border-left-width: 0px; padding-top: 0px" title="image" border="0" alt="image" src="/assets/image_622292.jpg" width="559" height="124" /></a></p>

<p>In next post, I will introduce how to open the DWG file in AutoCAD WS editor. </p>

<p>Stay tuned and have fun!</p>
