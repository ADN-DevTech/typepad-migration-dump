---
layout: "post"
title: "8000 Error getting file from AVFS &ndash;use .FileServer instead of .DataServer"
date: "2015-08-24 14:16:37"
author: "Wayne Brill"
categories:
  - "Vault"
  - "Wayne Brill"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/08/8000-error-getting-file-from-avfs-use-fileserver-instead-of-dataserver.html "
typepad_basename: "8000-error-getting-file-from-avfs-use-fileserver-instead-of-dataserver"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/wayne-brill.html" target="_self">Wayne Brill</a></p>  <p>In a recent case, code that was downloading a file from an ADMS was getting an 8000 error (TicketInvalid) when that same code was accessing an AVFS and called from another exe. (The code was simulating running from a custom job).</p>  <p><strong>Solution</strong>. (from a colleague in Vault Engineering)</p>  <p>The short answer: </p>  <p>It is not supported to sign in to one AVFS service with a ticket obtained from signing in to a different AVFS service.</p>  <p>For that case, the following strikeout code was wrong and needed to be replaced with the following highlighted code to fix the issue:&#160; <br /><font size="1">&#160; arguments += &quot;/url \&quot;&quot; + loginCred.ServerIdentities.<s>DataServer</s> + &quot;\&quot; &quot;;</font></p>  <pre><font size="1" face="Arial">&#160; arguments += &quot;/url \&quot;&quot; + loginCred.ServerIdentities.<strong>FileServer</strong> + &quot;\&quot; &quot;;</font></pre>

<p>Here are the details:</p>

<p>The <b>DataServer</b> and <b>FileServer</b> is a string representing the location where the ADMS services and AVFS services live respectively.&#160; In the case of an ADMS server, the DataServer and FileServer are the same value because ADMS always have both ADMS services and AVFS services. For an AVFS server, however, the FileServer and DataServer will be different. An AVFS server always knows which ADMS server it belongs to.&#160; </p>

<p>So, in that setup, there were services <b>ADMS1</b> and <b>AVFS1</b> on <b>Server1</b> and you have <b>AVFS2</b> (which talks to ADMS1) on <b>Server2</b>.&#160; </p>

<p>The first process, Process1, logs in to <b>AVFS2</b>.&#160; So, its <b>FileServer</b> will be <b>Server2 </b>and its<b> DataServer </b>will be <b>Server1</b>. From the login it gets a Security header which has a <b>Ticket</b>, <b>T1</b>, which is associated with AVFS2 (that is the important part—that tickets are specific to the AVFS which you logged into).</p>

<p>Then the second process is started - Process2, giving it (among other things) <b>T1</b> and the <b>DataServer </b>(i.e. Server1).&#160; This information is used in the constructor of <b>UserIdTicketCredentials</b>. The constructor expects the url you are giving it is the address of <b>AVFS</b> you want to login to. So this logs you into <b>AVFS1 </b>on Server1 (and I assume this was not intended).</p>

<p>This works (for a bit) because the ticket is valid—even though the ticket is not associated with AVFS1.</p>

<p>Upload/Download tickets are specific to an AVFS (i.e. an upload/download ticket for own AVFS will not work with another AVFS).&#160; When you ask the ADMS service for a upload/download ticket it uses the Security Header ticket to know what AVFS the ticket should be associated with. In this situation, based on the SecurityHeader ticket, it thinks you want a download ticket associated with <b>AVFS2</b>.&#160; </p>

<p>When you get that ticket back and use it, it is being used with <b>AVFS1</b>, and AVFS1 rejects the ticket because it is not a ticket it expects (it is not a ticket associated with AVFS1).&#160; Hence the 8000 error message is correct.</p>

<p>It would be nicer (IMO for troubleshooting purposes) if we just rejected the login from the get-go, but we don’t.</p>

<p>This blog post discusses file transfer:</p>

<p><a title="http://justonesandzeros.typepad.com/blog/2013/07/file-transfer-doing-it-the-hard-way.html" href="http://justonesandzeros.typepad.com/blog/2013/07/file-transfer-doing-it-the-hard-way.html">http://justonesandzeros.typepad.com/blog/2013/07/file-transfer-doing-it-the-hard-way.html</a></p>
