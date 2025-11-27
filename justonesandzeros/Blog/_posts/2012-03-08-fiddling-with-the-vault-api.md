---
layout: "post"
title: "Fiddling with the Vault API"
date: "2012-03-08 10:32:57"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
original_url: "https://justonesandzeros.typepad.com/blog/2012/03/fiddling-with-the-vault-api.html "
typepad_basename: "fiddling-with-the-vault-api"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/TipsAndTricks3.png" /></p>
<p>For a while, I’ve been looking for a good way to detect if <a href="http://justonesandzeros.typepad.com/blog/2010/03/file-transfer-as-binary-data.html" target="_blank">binary transfer</a> is set up properly for a Vault app.&#0160; I’ve finally found that way, and its name is <a href="http://fiddler2.com/fiddler2/" target="_blank">Fiddler</a>.</p>
<p>If you don’t know what Fiddler is, it’s a tool that lets you monitor HTTP traffic on your computer.&#0160; It’s the type of utility that has millions of uses, but I’ll be focusing only on using it to help with Vault programming.&#0160; Since Vault uses web services for server communication, you can use Fiddler to verify that your files are transferring properly.</p>
<p>One of the aspects of the Vault API is that you need to configure things very specifically in order to get files to transfer in a binary mode.&#0160; If you get a step wrong, then files get transferred as text.&#0160;&#0160; But your app still works, so it’s a tricky problem to locate.&#0160; I’ll show you how to use Fiddler to verify the binary transfer.</p>
<hr noshade="noshade" style="color: #ff5a00;" />
<p>Steps:</p>
<ol>
<li>Install <a href="http://fiddler2.com/fiddler2/" target="_blank">Fiddler</a>. </li>
<li>Open Fiddler. </li>
<li>Run your app.&#0160; Make sure to have a real computer name as the Vault server; do not use “localhost” or “127.0.0.1”. </li>
<li>Upload or download a file.&#0160; The size of the file does not matter, but I suggest a binary file, such as an image. </li>
<li>In Fiddler, locate the entry that corresponds to the API call that performed the file transfer.&#0160; <ol>
<li>The left pane, shows all the HTTP requests.&#0160; Vault API calls will show the URL of the web service. </li>
<li>The right top pane shows the outgoing request.&#0160; For Vault API calls, this has the raw SOAP data, including the function name and parameter set. </li>
<li>The bottom right pane shows the incoming response.&#0160; For Vault API calls, this has the raw SOAP data, this has the function name and return values. </li>
<li>You can use either pane on the right to find the function name.&#0160; It’s best to use the Text View and scroll to the bottom of the pane.&#0160; The element directly inside &lt;soap:Body&gt; is the function name. </li>
</ol> </li>
<li>If you are doing an upload, you want to check the outgoing pane at the right-top.&#0160; If you are doing a download, you want to check the incoming pane at the right-bottom. </li>
<li>If you configured things correctly, there should be a section at the bottom indicating a MIME attachment.&#0160; There may also be a gibberish representation of the binary data.      <br />If you configured things incorrectly, there will be no MIME attachment.&#0160; The file data will show as a large text string in a function parameter or return value. </li>
</ol> 
<hr noshade="noshade" style="color: #ff5a00;" />
<p>If things are good, Fiddler should show something like this:    <br /><a href="http://justonesandzeros.typepad.com/images/2012/Fiddler/GoodResult.png" target="_blank"><img alt="" src="/assets/GoodResult_scaled.png" /></a> <br />(click image for larger view)</p>
<p>&#0160;</p>
<p>If things are bad, Fiddler will show something like this:    <br /><a href="http://justonesandzeros.typepad.com/images/2012/Fiddler/BadResult.png" target="_blank"><img alt="" src="/assets/badresult_scaled.png" /></a> <br />(click image for larger view)</p>
<p>&#0160;</p>
<p>NOTE:&#0160; When things are configured incorrectly, with MTOM not enabled, you can use the XML View in Fiddler.&#0160; When MTOM is correctly turned on, then the XML View is blank.</p>
<p><img alt="" src="/assets/TipsAndTricks3-1.png" /></p>
