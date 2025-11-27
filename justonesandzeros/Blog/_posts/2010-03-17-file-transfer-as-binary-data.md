---
layout: "post"
title: "File Transfer as Binary Data"
date: "2010-03-17 07:27:26"
author: "Doug Redmond"
categories:
  - "Must Read"
  - "Tips and Tricks"
original_url: "https://justonesandzeros.typepad.com/blog/2010/03/file-transfer-as-binary-data.html "
typepad_basename: "file-transfer-as-binary-data"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/TipsAndTricks2.png" /></p>
<p><strong>Introduction</strong> <br />Communication with the Vault server takes the form of SOAP commands over HTTP.&#0160; As you may know, SOAP is XML data, and XML is text.&#0160; Does that mean the program you wrote uploads and downloads files as text data?</p>
<p>By default, the answer is yes, your program converts files to text when transferring files.&#0160; This can be a big performance problem if your program has to upload thousands of files.&#0160; The conversion to base64 eats up CPU cycles and increases the transfer size by about 30%.</p>
<p><strong>Setting up your project</strong> <br />If you want to transfer your files in binary mode, there are several steps you need to go through:</p>
<ol>
<li>Download and install Microsoft Web Service Extensions (<strong>WSE</strong>) 3.0.&#0160; You can find it on the Microsoft site.&#0160; (I&#39;d post a direct link, but they keep changing it). </li>
<li>If you are using Visual Studio 2005, select the &quot;Integrate with Visual Studio&quot; option during the install. </li>
<li>In your project, create a reference to <strong>Microsoft.Web.Services3</strong>. </li>
<li>If you are using Vault 2012 or higher, skip to step 8.&#0160; </li>
<li>Generate your web services. </li>
<li>If you are using Visual Studio 2008 there are some extra steps:  <ol>
<li>In the Solution Explorer, activate the Show All Files option. </li>
<li>Expand the web reference and locate the Reference.cs (or Reference.vb) file and open it. </li>
<li>Change the base class of the Service object from &quot;System.Web.Services.Protocols.SoapHttpClientProtocol&quot; to &quot;Microsoft.Web.Services3.WebServicesClientProtocol&quot; </li>
<li>(optional) Add Wse to the end of the class name and constructor. </li>
<li>Repeat as needed for the other web services.&#0160; However most people just fix up the Document Service.</li>
</ol> </li>
<li>In your code, use the web service classes with &quot;Wse&quot; at the end. </li>
<li>Create an app.config XML file if your project does not already have one. </li>
<li>Merge in the following XML:  <br /><span style="color: #800000;">&lt;configuration&gt;&#0160;&#0160;&#0160; <br />&#0160; &lt;configSections&gt;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <br />&#0160;&#0160;&#0160; &lt;section name=&quot;microsoft.web.services3&quot;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; type=&quot;Microsoft.Web.Services3.Configuration.WebServicesConfiguration, Microsoft.Web.Services3, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35&quot; /&gt;&#0160;&#0160;&#0160;&#0160; <br />&#0160; &lt;/configSections&gt;&#0160;&#0160;&#0160; <br />&#0160; &lt;microsoft.web.services3&gt;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <br />&#0160;&#0160;&#0160; &lt;messaging&gt;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <br />&#0160;&#0160;&#0160;&#0160;&#0160; &lt;maxMessageLength value=&quot;51200&quot; /&gt;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <br />&#0160;&#0160;&#0160;&#0160;&#0160; &lt;mtom clientMode=&quot;On&quot; /&gt; <br />&#0160;&#0160;&#0160; &lt;/messaging&gt;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <br />&#0160; &lt;/microsoft.web.services3&gt;   <br />&lt;/configuration&gt;</span> </li>
<li>When you build your project, app.config gets renamed to [EXE name].config.&#0160; Make sure you deploy this with your application. </li>
<li>You are done. </li>
</ol>
<p><strong> <br />Testing  <br /></strong>Gee, that&#39;s a lot of steps.&#0160; If any of them are not done property, the system will automatically revert to text transfer of your files.&#0160; So how do test things to make sure that files are being transferred as binary data?</p>
<p><strong>UPDATE:</strong>&#0160; There is a tool called Fiddler which provides a better way to test than the technique below.&#0160; See <a href="http://justonesandzeros.typepad.com/blog/2012/03/fiddling-with-the-vault-api.html" target="_self">this article</a> for instructions on how to test using Fiddler.</p>
<p>Here is the approach I use.&#0160; It&#39;s not very good.&#0160; If you find a better way, please let me, and everybody else, know in the comments section.<br />The basic principle is to have IIS log the bytes transferred and see if that matches the size of a file that you upload or download with your application.</p>
<ol>
<li>Open up your IIS manager on your Vault server machine.&#0160; <br />Note: I&#39;m using IIS 5. Things may be different depending on your version. </li>
<li>Select the web site running Vault. </li>
<li>Right click and select <strong>Properties</strong>. </li>
<li>Go to the <strong>Web Site</strong> tab. </li>
<li>Check the <strong>Enable Logging</strong> box.  <br /><img alt="" src="/assets/IIS1.png" /> </li>
<li>Click on the <strong>Properties</strong> button. </li>
<li>Go to the <strong>Extended Properties</strong> tab. </li>
<li>Check the <strong>Extended Properties</strong> box. </li>
<li>Check the <strong>Bytes Sent</strong> and <strong>Bytes Received</strong> boxes.  <br /><img alt="" src="/assets/IIS2.png" /> </li>
<li>Click OK on the dialogs. </li>
<li>Find a file that is at least 10 MB large. </li>
<li>Upload the file to Vault using your application.&#0160; Or add it to Vault and download it using your application. </li>
<li>Go to the IIS log and open it.&#0160; (ex.&#0160; C:\WINDOWS\system32\Logfiles\W3SVC1\ex100316.log) </li>
<li>You should see an entry to DocumentService.asmx that has a large number of bytes transferred.&#0160; For example:  <br /><span style="font-size: xx-small;">20:28:02 127.0.0.1 POST /AutodeskDM/Services/DocumentService.asmx 200 2675 <strong>14074058</strong></span> </li>
<li>If that large number is about the size of the uploaded file, then things work.&#0160; Otherwise something is not working right and you will need to double check your application.&#0160; <br />In my case, 14,074,058 is close to the size of the file I uploaded (14,068,867).&#0160; The IIS log will always be slightly higher due to HTTP and SOAP headers. </li>
</ol>
<p>If you want to deliberately break things to test the negative case, you can do this easily without even needing to re-compile.&#0160; Just edit your .config file and set the &lt;mtom&gt; clientMode setting to &quot;Off&quot;.&#0160; Upload or download the file again and check the file size in the IIS log.&#0160; It should be significantly bigger.&#0160; For example, my test showed 18,763,057 as the transfer size, which is 33% larger than the file.</p>
