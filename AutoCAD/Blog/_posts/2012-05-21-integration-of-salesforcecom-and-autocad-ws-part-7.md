---
layout: "post"
title: "Integration of Salesforce.com and AutoCAD WS - Part 7"
date: "2012-05-21 21:46:00"
author: "Daniel Du"
categories:
  - "AutoCAD"
  - "Cloud"
  - "Daniel Du"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/integration-of-salesforcecom-and-autocad-ws-part-7.html "
typepad_basename: "integration-of-salesforcecom-and-autocad-ws-part-7"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/daniel-du.html">Daniel Du</a></p>  <p><img alt="" src="/assets/image_845925.jpg" width="259" height="81" /></p>  <p>In <a href="http://adndevblog.typepad.com/autocad/2012/05/integration-of-salesforcecom-and-autocad-ws-part-6.html" target="_blank">part 6</a>, we transferred the DWG attachment to AutoCAD WS storage, in this post, we will try to open it in AutoCAD WS online editor. </p>  <p>In <a href="http://adndevblog.typepad.com/autocad/2012/05/integration-of-salesforcecom-and-autocad-ws-part-4.html" target="_blank">part 4</a>, we introduced how to pass parameters between visual force page and apex controller, please pay attention to the code in bold:</p>  <pre style="line-height: normal" class="csharpcode"><span class="kwrd"><font style="font-size: 8pt" color="#000000">&lt;</font></span><font color="#000000"><font style="font-size: 8pt"><span class="html">script</span> <span class="attr">type</span><span class="kwrd">=&quot;text/javascript&quot;</span><span class="kwrd">&gt;</span> 
 <strong><span class="kwrd">function</span> openInWS(url){
   window.open(url);
 }
</strong><span class="kwrd">&lt;/</span><span class="html">script</span></font><span class="kwrd"><font style="font-size: 8pt">&gt;</font></span></font></pre>

<pre style="line-height: normal" class="csharpcode"><span class="kwrd"><font style="font-size: 8pt" color="#000000">&lt;</font></span><font color="#000000"><font style="font-size: 8pt"><span class="html">apex:column</span> <span class="attr">id</span><span class="kwrd">=&quot;colOpenInAcadWS&quot;</span> 
       <span class="attr">rendered</span><span class="kwrd">=&quot;{!CONTAINS(item.ContentType, 'application/x-dwg')}&quot;</span><span class="kwrd">&gt;</span>
    <span class="kwrd">&lt;</span><span class="html">p</span> <span class="attr">id</span><span class="kwrd">=&quot;{!count}.openInAcadWS&quot;</span><span class="kwrd">&gt;&lt;/</span><span class="html">p</span><span class="kwrd">&gt;</span>
    <span class="kwrd">&lt;</span><span class="html">apex:commandLink</span> <span class="attr">action</span><span class="kwrd">=&quot;{!transferToWSStorage}&quot;</span> 
         <span class="attr">title</span><span class="kwrd">=&quot;Open In AutoCAD WS&quot;</span>   <br />         <span class="attr">id</span><span class="kwrd">=&quot;btnOpenInAutoCADWs&quot;</span>
         <span class="attr">value</span><span class="kwrd">=&quot;Open In AutoCAD WS&quot;</span> 
         <span class="attr">styleClass</span><span class="kwrd">=&quot;btn&quot;</span>  <span class="attr">style</span><span class="kwrd">=&quot;text-decoration:none&quot;</span>
         <strong><span class="attr">oncomplete</span><span class="kwrd">=&quot;openInWS('{!openInAutocadWSUrl}');&quot;</span></strong> <span class="kwrd">&gt;</span>
        <span class="kwrd">&lt;</span><span class="html">apex:param</span> <span class="attr">name</span><span class="kwrd">=&quot;selectedAttachmentId&quot;</span> 
              <span class="attr">value</span><span class="kwrd">=&quot;{!item.Id}&quot;</span><span class="kwrd">/&gt;</span>
    <span class="kwrd">&lt;/</span><span class="html">apex:commandLink</span><span class="kwrd">&gt;</span>
    <span class="kwrd">&lt;</span><span class="html">apex:pageBlock</span> <span class="attr">id</span><span class="kwrd">=&quot;hiddenBlock&quot;</span> <span class="attr">rendered</span><span class="kwrd">=&quot;false&quot;</span><span class="kwrd">&gt;</span>
    <span class="kwrd">&lt;/</span><span class="html">apex:pageBlock</span><span class="kwrd">&gt;</span>
<span class="kwrd">&lt;/</span><span class="html">apex:column</span></font><span class="kwrd"><font style="font-size: 12pt">&gt;</font></span></font></pre>

<p>In controller: </p>
<style type="text/css">






    <!--code { font-family: Courier New, Courier; font-size: 10pt; margin: 0px; }-->
  </style><meta content="text/html; charset=iso-8859-1" http-equiv="Content-Type" /><!-- ======================================================== --><!-- = Java Sourcecode to HTML automatically converted code = --><!-- =   Java2Html Converter 5.0 [2006-02-26] by Markus Gebhard  markus@jave.de   = --><!-- =     Further information: http://www.java2html.de     = -->

<div class="java" align="left">
  <table border="0" cellspacing="0" cellpadding="3" bgcolor="#ffffff"><tbody>
      <tr><!-- start source code -->
        <td valign="top" nowrap="nowrap" align="left"><code><font color="#3f7f5f">//set openInAutocadWSUrl so that it can be 
              <br />//opened in AutoCAD WS</font> 

            <br /><font color="#000000">openInAutocadWSUrl = client 
              <br />&#160;&#160;&#160;&#160; .getOpenDrawingUrl</font><font color="#000000">(</font><font color="#000000">dstRelativePath</font><font color="#000000">)</font><font color="#000000">;</font></code> </td>
<!-- end source code --></tr>
    </tbody></table>
</div>
<!-- =       END of automatically generated HTML code       = --><!-- ======================================================== -->

<p>A command link apex tag invokes an action of apex controller. In the controller, we transferred the DWG attachment to AutoCAD WS storage, and build the URL to open in WS online editor. Once the action is completed, a JavaScript function “oncomplete” will be invoked, we just use window.open() to launch the URL in a new window. We have already transferred the DWG to AutoCAD storage, now we are going to build the launching URL.</p>

<p>This is the implementation of getOpenDrawingURL, it returns the URL to open DWG files in AutoCAD WS online editor:</p>
<style type="text/css">




    <!--code { font-family: Courier New, Courier; font-size: 10pt; margin: 0px; }-->
  </style><meta content="text/html; charset=iso-8859-1" http-equiv="Content-Type" /><!-- ======================================================== --><!-- = Java Sourcecode to HTML automatically converted code = --><!-- =   Java2Html Converter 5.0 [2006-02-26] by Markus Gebhard  markus@jave.de   = --><!-- =     Further information: http://www.java2html.de     = -->

<div class="java" align="left">
  <table border="0" cellspacing="0" cellpadding="3" bgcolor="#ffffff"><tbody>
      <tr><!-- start source code -->
        <td valign="top" nowrap="nowrap" align="left"><code><font color="#ffffff">&#160; </font><font color="#3f7f5f">//===========Open drawing&#160; begin ===============</font> 

            <br /><font color="#ffffff">&#160; </font>

            <br /><font color="#ffffff">&#160; </font><font color="#3f7f5f">//get the url to open in AutoCAD WS</font> 

            <br /><font color="#ffffff">&#160; </font><font color="#7f0055"><b>public </b></font><font color="#000000">string getOpenDrawingUrl</font><font color="#000000">(</font><font color="#000000">string path</font><font color="#000000">){</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160; </font>

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#000000">OpenInWSHandler openHander = </font><font color="#7f0055"><b>new </b></font><font color="#000000">OpenInWSHandler</font><font color="#000000">()</font><font color="#000000">;</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#000000">String openInAutocadWSUrl = openHander 
              <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; .Open</font><font color="#000000">(</font><font color="#000000">userName, password, path</font><font color="#000000">)</font><font color="#000000">;</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160; </font>

            <br /><font color="#ffffff">&#160;&#160;&#160; </font><font color="#7f0055"><b>return </b></font><font color="#000000">openInAutocadWSUrl;</font> 

            <br /><font color="#ffffff">&#160; </font><font color="#000000">}</font> 

            <br /><font color="#ffffff">&#160; </font>

            <br /><font color="#ffffff">&#160; </font><font color="#3f7f5f">//===========Open drawing&#160; end ===============</font></code> </td>
<!-- end source code --></tr>
    </tbody></table>
</div>
<!-- =       END of automatically generated HTML code       = --><!-- ======================================================== -->

<p>I create a class OpenInWSHandler. Firstly we need to pass the username and password to AutoCAD WS authenticate URL to get the authentication token. after we get the token, we can use it to build the &quot;AutoCAD-WS-Open-URL”. Here is the source code: </p>

<div class="java" align="left">
  <table border="0" cellspacing="0" cellpadding="3" bgcolor="#ffffff"><tbody>
      <tr>
        <td valign="top" nowrap="nowrap" align="left"><code><font color="#000000">Public Class OpenInWSHandler </font><font color="#000000">{</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160; </font><font color="#000000">string AUTHENTICATE_URL&#160; <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; = </font><font color="#ff6100">'ht</font><font color="#000000">tps:</font><font color="#3f7f5f">//www.autocadws.com/main/auth';</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160; </font><font color="#000000">string OPEN_IN_WS_URL&#160; <br />&#160;&#160;&#160;&#160;&#160;&#160; = </font><font color="#ff6100">'ht</font><font color="#000000">tps:</font><font color="#3f7f5f">//www.autocadws.com/main/open';</font> 

            <br /><font color="#ffffff"></font>

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160; </font><font color="#3f7f5f">//return the url for open in AcadWS</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160; </font><font color="#7f0055"><b>public </b></font><font color="#000000">String Open</font><font color="#000000">(</font><font color="#000000">string userName, 
              <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; string password, string relativePath</font><font color="#000000">){</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160; </font>

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#000000">String url = </font><font color="#ff6100">'';</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#000000">string token = Authenticate</font><font color="#000000">(</font><font color="#000000">userName, password</font><font color="#000000">)</font><font color="#000000">;</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#3f7f5f">//if(token.length() &gt; 0){</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#000000">url = OPEN_IN_WS_URL + </font><font color="#ff6100">'?p</font><font color="#000000">ath=</font><font color="#ff6100">'&#160; <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; + </font><font color="#000000">relativePath + </font><font color="#ff6100">'&amp;t</font><font color="#000000">oken=</font><font color="#ff6100">' + </font><font color="#000000">token;</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#3f7f5f">//}</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#7f0055"><b>return </b></font><font color="#000000">url;</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160; </font><font color="#000000">}</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160; </font>

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160; </font><font color="#7f0055"><b>public </b></font><font color="#000000">string Authenticate</font><font color="#000000">(</font><font color="#000000">string userName,&#160; <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; string password</font><font color="#000000">)&#160; <br />&#160;&#160;&#160;&#160;&#160; {</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#3f7f5f">// Request must contain &quot;Authorization: Basic&quot; 
              <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; // HTTP header</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#000000">string tok = userName + </font><font color="#990000">':' </font><font color="#000000">+ password ;</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#000000">string hash = EncodingUtil 
              <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; .base64Encode</font><font color="#000000">(</font><font color="#000000">Blob.valueOf</font><font color="#000000">(</font><font color="#000000">tok</font><font color="#000000">))</font><font color="#000000">;&#160; </font>

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#000000">string authorizationHeader = </font><font color="#ff6100">'Ba</font><font color="#000000">sic </font><font color="#ff6100">' + </font><font color="#000000">hash;</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font>

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#000000">HttpRequest req = </font><font color="#7f0055"><b>new </b></font><font color="#000000">HttpRequest</font><font color="#000000">()</font><font color="#000000">;</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#000000">req.setHeader</font><font color="#000000">(</font><font color="#ff6100">'Au</font><font color="#000000">thorization</font><font color="#ff6100">' 
              <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; ,a</font><font color="#000000">uthorizationHeader </font><font color="#000000">)</font><font color="#000000">;</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#000000">req.setHeader</font><font color="#000000">(</font><font color="#ff6100">'to</font><font color="#000000">autocadws</font><font color="#ff6100">', 'tr</font><font color="#000000">ue</font><font color="#ff6100">');</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#000000">req.setMethod</font><font color="#000000">(</font><font color="#ff6100">'GE</font><font color="#000000">T</font><font color="#ff6100">');</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#000000">req.setEndpoint</font><font color="#000000">(</font><font color="#000000">AUTHENTICATE_URL</font><font color="#000000">)</font><font color="#000000">;</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font>

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#000000">Http h = </font><font color="#7f0055"><b>new </b></font><font color="#000000">Http</font><font color="#000000">()</font><font color="#000000">;</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#3f7f5f">// Send the request, and return a response </font>

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#000000">HttpResponse res = h.send</font><font color="#000000">(</font><font color="#000000">req</font><font color="#000000">)</font><font color="#000000">;</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#3f7f5f">//system.assert(false,res.getStatusCode());</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#3f7f5f">//success </font>

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#7f0055"><b>if</b></font><font color="#000000">(</font><font color="#000000">res.getStatusCode</font><font color="#000000">() </font><font color="#000000">&gt;= </font><font color="#990000">200&#160; <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#000000">&amp;&amp; res.getStatusCode</font><font color="#000000">() </font><font color="#000000">&lt; </font><font color="#990000">300</font><font color="#000000">)&#160; <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; {</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#3f7f5f">//system.assert(false, res.getBody());</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#7f0055"><b>return </b></font><font color="#000000">String.valueOf</font><font color="#000000">(</font><font color="#000000">res.getBody</font><font color="#000000">())</font><font color="#000000">;</font>&#160; <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font>&#160; <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#000000">}</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#7f0055"><b>else</b></font><font color="#000000">{</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#7f0055"><b>return </b></font><font color="#ff6100">''; </font><font color="#3f7f5f">//authenticate failed.</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><font color="#000000">}</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font>

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160; </font><font color="#000000">}</font> 

            <br /><font color="#ffffff">&#160;&#160;&#160;&#160;&#160; </font>

            <br /><font color="#000000">}</font></code> </td>
      </tr>
    </tbody></table>
</div>

<p>Since we are trying to access another remote web site <code><font color="#ff6100"><a href="https://www.autocadws.com/">ht</a><font color="#000000">tps:</font><font color="#3f7f5f">//www.autocadws.com/</font>,</font></code>&#160; we also need to to enable Salesforce to access remote site, please check Setup-&gt;Security-&gt;Remote site settings. </p>

<p>We are ready to give it a run, open the visual force page(please note we need to pass a case Id to list the attachments), and click the “Open In AutoCAD WS” button, a few seconds latter, a new window will launch AutoCAD WS online editor to open this DWG file.</p>

<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0167669f2af2970b-pi"><img style="background-image: none; border-right-width: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top-width: 0px; border-bottom-width: 0px; border-left-width: 0px; padding-top: 0px" title="image" border="0" alt="image" src="/assets/image_613185.jpg" width="461" height="318" /></a>&#160;</p>

<p>In next post, I will introduce how to integer this visual force page to your case layout, so that you do not need to input the case id in URL manually.</p>

<p>Good luck and have fun!</p>
