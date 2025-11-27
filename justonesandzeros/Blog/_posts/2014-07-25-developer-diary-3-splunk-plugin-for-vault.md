---
layout: "post"
title: "Developer Diary #3: Splunk plugin for Vault"
date: "2014-07-25 16:49:54"
author: "Doug Redmond"
categories:
  - "Sample Applications"
  - "Vault"
original_url: "https://justonesandzeros.typepad.com/blog/2014/07/developer-diary-3-splunk-plugin-for-vault.html "
typepad_basename: "developer-diary-3-splunk-plugin-for-vault"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Vault4.png" /> <br /><img alt="" src="/assets/SampleApp4.png" /></p>
<p>Every now and then I write an app only to find out at the end that the app doesn’t work well.&#0160; This is one of those cases.&#0160; I’m going to post the app anyway because I believe the code may be of use to somebody.&#0160; Splunk is a useful tool.&#0160; I just wasn’t able to do what I wanted given the information that Vault is printing to the logs.&#0160; Also, I spent a lot of time on it, so I should get a blog post out of it at least.</p>
<p>Here is the app.&#0160; I was hoping for something that shows the download history of a file.</p>
<p><img alt="" src="/assets/HistoryWindow.png" /></p>
<p>It would be pretty cool if it worked, but there are too many cases where downloads will not show up in the list.&#0160; If your users download one file at a time, this will work great.&#0160; But if they download multiple files at once (and they do), then this app completely falls apart.&#0160; In my early tests, I only download one file at a time.&#0160; Oops.</p>
<p>The underlying problem is that the Vault log files are structured around “operations” and not around the “objects” being acted on.&#0160;&#0160; Vault likes to operate on multiple objects at once for performance reasons.&#0160; There is no clean way to log all the objects in one log entry, so that information just doesn’t get logged. </p>
<hr noshade="noshade" style="color: #013181;" />
<p><strong>Requirements:</strong></p>
<ul>
<li>Vault Workgroup or Professional 2015</li>
<li>Vault admin rights</li>
<li>Splunk (free or enterprise)</li>
</ul>
<p><a href="http://justonesandzeros.typepad.com/Apps/VaultSplunk/SplunkVault-1.0.1.0-bin.zip">Click here for the application</a>&#0160; <br /><a href="http://justonesandzeros.typepad.com/Apps/VaultSplunk/SplunkVault-1.0.1.0-src.zip">Click here for the source code</a></p>
<p><span style="font-size: xx-small;">As with all the samples on this site, the </span><a href="http://justonesandzeros.typepad.com/blog/disclaimer.html"><span style="font-size: xx-small;">legal disclaimer</span></a><span style="font-size: xx-small;"> applies.</span></p>
<hr noshade="noshade" style="color: #013181;" />
<p><strong>Splunk Setup:</strong></p>
<p>OK, there are a lot of steps here.&#0160; But the added benefit is that you get Splunk up and running which provides a lot of ways for you to manage your Vault data.&#0160;</p>
<ol>
<li>Install Splunk.&#0160; I don’t recommend installing it on the Vault Server.</li>
<li>On the Vault Server, open up the IIS manager.</li>
<li>Go to the logging settings and make sure that “URI Query” is selected. <br /><img alt="" src="/assets/iisSettings.png" /></li>
<li>Restart IIS if needed.</li>
<li>Sanity Check:&#0160; Perform some operations from the Vault Explorer client.&#0160; The new rows should show query strings describing the server calls.&#0160; Example:&#0160;
<p>2014-07-25 18:28:28 10.143.48.29 POST /AutodeskDM/Services/v19/PropertyService.svc <strong>op=GetProperties&amp;uid=2&amp;vaultName=Vault&amp;sessID=143994478&amp;app=VP</strong> 80 - 10.143.48.22 Mozilla/4.0+(compatible;+MSIE+6.0;+MS+Web+Services+Client+Protocol+4.0.30319.18408) 200 0 0 9 </p>
</li>
<li>Open up Splunk.</li>
<li>Click on the “Add Data” button on the main dashboard.</li>
<li>Choose “IIS logs” as the data type and follow the instructions.&#0160; Multiple steps are involved in moving log data across servers, so I’ll leave it to Splunk to explain that part.</li>
<li>Sanity Check:&#0160; From the Splunk homepage, click on the “Search and Reporting” button.&#0160; Click on data summary and select your Vault server host.&#0160; You should be able to see the contents of the IIS logs.</li>
<li>In Splunk, to to Settings-&gt;Fields.</li>
<li>Select “Add New” for Field Extractions.&#0160; To create a new field for reading the Vault Operation from the query string.<ol>
<li>Destination App: search</li>
<li>Name:&#0160; EXTRACT-OPERATION (or whatever you want)</li>
<li>Apply To: sourcetype</li>
<li>Name:&#0160; IIS Logs (may be different for your Splunk config)</li>
<li>Type: inline</li>
<li>Extraction/Transform:&#0160; <strong>op=(?P&lt;OPERATION&gt;.+?)[&amp;\s] <br /><img alt="" src="/assets/extractField.png" /></strong></li>
</ol></li>
<li>Add another field extraction.&#0160; This time you are extracting the File ID.&#0160; Set Extraction/Transform to <strong>fileIterationId=(?P&lt;FILE_ID&gt;.+?)[&amp;\s]</strong></li>
<li>Add another field extraction.&#0160; This time you are extracting the User ID.&#0160; Set Extraction/Transform to <strong>uid=(?P&lt;USERID&gt;.+?)[&amp;\s]</strong></li>
<li>Sanity Check:&#0160; Open up the Vault client and download some files that are not already in you working folder.&#0160; When the entries show up in Splunk, expand the entry.&#0160; You should see that OPERATION, FILE_ID and USERID, as rows. <br /><img alt="" src="/assets/extractResults.png" /></li>
<li>If you are using the free version of Splunk, you will need to configure the REST API to allow anonymous login.&#0160; <br />Go to <strong>$SPLUNK_HOME/etc/system/local/server.conf</strong> and add the following line in the [General] section:&#0160; <strong>allowRemoteLogin = always</strong></li>
<li>If you are using the enterprise version of Splunk, you should create a basic user just for reading this data.&#0160; SplunkVault does not encrypt the Splunk username/password information.</li>
<li>Download the SplunkVault app from the download link above.</li>
<li>Extract the zip in the folder C:\ProgramData\Autodesk\Vault 2015\Extensions.&#0160; You now have a SplunkVault folder under Extensions.&#0160; I didn’t build an installer for this one.</li>
<li>Restart Vault Explorer and login as an administrator.</li>
<li>Go to Tools-&gt;Splunk Vault Settings and fill out the fields.<ol>
<li>Splunk Port is the port of the API, which is different from the web page port.&#0160; 8089 is the default API port.</li>
<li>If you are using the free version, the user should be ‘admin’ and the password can be any value.</li>
<li>If you are using the enterprise version, the user and password should be the low-access user you set up in an earlier step. <br /><img alt="" src="/assets/settings.png" /></li>
</ol></li>
<li>Save the settings.</li>
<li>Download some files that are not in your local folder.</li>
<li>Right click on a file, and select Download Activity.&#0160; You should see an entry for the recent download.&#0160; If not, wait a few minutes and try again.</li>
<li>You are done at this point.</li>
</ol><hr noshade="noshade" style="color: #013181;" />
