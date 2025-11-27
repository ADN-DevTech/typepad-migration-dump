---
layout: "post"
title: "FAQ - Events"
date: "2011-04-08 08:16:33"
author: "Doug Redmond"
categories:
  - "Concepts"
original_url: "https://justonesandzeros.typepad.com/blog/2011/04/faq-events.html "
typepad_basename: "faq-events"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Concepts3.png" /></p>
<p><strong>What is the Event feature in the Vault 2012 API?      <br /></strong>For Vault 2012, we added a feature called <strong>Web Service Command Events</strong>, which allows you to run custom code before and after certain Vault operations are executed.&#0160; For example, you can perform custom operations after a file gets checked-in.     <strong></strong></p>
<p><strong><br />What is the scope of my event handlers?      <br /></strong>Your handlers will hook to any application on that computer using Autodesk.Connectivity.WebServices.dll.&#0160; Starting in Vault 2012 all Autodesk Vault clients use this DLL for communication with the server.&#0160; So you can hook to applications like Vault Explorer, AutoCAD, Inventor, Civil 3D, and so on.     <br /> <br />Let me restate, that this feature runs <strong>client side</strong>.&#0160; So if you have 100 Vault users, your event code needs to be deployed to 100 computers in order to grab all events.     <strong></strong></p>
<p><strong><br />What Vault products does this feature work with?      <br /></strong>Vault Workgroup, Vault Collaboration and Vault Professional.&#0160; It is not available in base Vault.     <strong></strong></p>
<p><strong><br />What operations can I receive events for?      <br /></strong>Most events can be displayed in a grid:</p>
<table border="1" cellpadding="2" cellspacing="0" width="450">
<tbody>
<tr>
<td valign="top" width="90">&#0160;</td>
<td valign="top" width="90">File</td>
<td valign="top" width="90">Folder</td>
<td valign="top" width="90">Item</td>
<td valign="top" width="90">Change Order</td>
</tr>
<tr>
<td valign="top" width="90">Add</td>
<td valign="top" width="90">Yes</td>
<td valign="top" width="90">Yes</td>
<td valign="top" width="90">Yes</td>
<td valign="top" width="90">Yes</td>
</tr>
<tr>
<td valign="top" width="90">Delete</td>
<td valign="top" width="90">Yes</td>
<td valign="top" width="90">Yes</td>
<td valign="top" width="90">Yes</td>
<td valign="top" width="90">Yes</td>
</tr>
<tr>
<td valign="top" width="90">Reserve for editing</td>
<td valign="top" width="90">Yes (checkout)</td>
<td valign="top" width="90">No</td>
<td valign="top" width="90">Yes          <br />(edit)</td>
<td valign="top" width="90">Yes          <br />(edit)</td>
</tr>
<tr>
<td valign="top" width="90">Commit Changes</td>
<td valign="top" width="90">Yes          <br />(checkin)</td>
<td valign="top" width="90">No</td>
<td valign="top" width="90">Yes          <br />(commit)</td>
<td valign="top" width="90">Yes          <br />(commit)</td>
</tr>
<tr>
<td valign="top" width="90">Change Lifecycle</td>
<td valign="top" width="90">Yes</td>
<td valign="top" width="90">No</td>
<td valign="top" width="90">Yes</td>
<td valign="top" width="90">Yes</td>
</tr>
</tbody>
</table>
<p>In addition to the ones listed in the grid, you can receive events for File Download, Item Rollback Lifecycle, and Item Promote.    <strong></strong></p>
<p><strong><br />What are the events I can hook to for a given operation?      <br /></strong>Each operation fires 3 events:</p>
<ol>
<li><strong>GetRestrictions</strong> - This part allows event handlers to block the operation. </li>
<li><strong>Pre</strong> - This part allows event handlers to run code before the operation takes place. </li>
<li><strong>Post</strong> - This part allows event handlers to run code after the operation has finished. </li>
</ol>
<p><strong><br />When and where do these events fire?      <br /></strong>Here is the basic timeline:     <br /><img alt="" src="/assets/eventTimeline.png" /></p>
<p>If restrictions are found during the GetRestrictioins phase, the operation is blocked and never continues to the Pre.&#0160; If there are no restrictions, things continue to the Pre.&#0160; Unless something unexpected occurs, Pre, Web Service call and Post should always execute.</p>
<p>As you can see, the events happen around the web service call.&#0160; In other words, at the layer where the client communicates with the server.&#0160;</p>
<p>Keep in mind these events may or may not correlate with the UI command that the user invokes. For example, a user performs a Get command on a file in Vault Explorer.&#0160; This may trigger multiple download events if an entire assembly needs to be download.&#0160; Or it may trigger no events if the file on disk is up-to-date.    <strong></strong></p>
<p><strong><br />What can I do in an event handler?      <br /></strong>Inside a GetRestrictions event you can block the operation and provide the reason for the restriction.&#0160; Inside a Pre and a Post event you run extra operations, such as sending off an email or queuing up a Job.</p>
<p><strong><br />What can&#39;t I do in an event handler?      <br /></strong>You can&#39;t change the parameters to or from the operation.&#0160; You can see most of the parameters passed in, but you can&#39;t change them.&#0160; Likewise you see the return result, but you can&#39;t alter it.&#0160; For example, in the Pre AddFile event you can see that the file name parameter is &quot;Part1.dwg&quot;, your code <strong>cannot</strong> change that name to &quot;ZX-0389.dwg&quot;.     <br /> <br />You should also avoid UI in an event handler.&#0160; You don&#39;t know what context your handler is running in.&#0160; For example, you could be running in a command line utility or a service.     <br /> <br />Bubbling up exceptions is not a good idea.&#0160; Again, you don&#39;t know the context you are running in, so an exception might end up shutting down the parent application.&#0160; The safest thing is to wrap each event handler in a try/catch block.     <br /> <br />Lastly, make sure you don&#39;t create and infinite recursion of events.&#0160; For example, during a Pre AddFile event, your code adds a file, which triggers an AddFile event, which causes your code to add a file, and so on.&#0160; Even if your code works fine on its own, remember that there might be other event handlers loaded.&#0160; So your handler might run and operation that triggers another handler, which runs an operation that triggers back to your handler, and so on ad infinitum.&#0160; <strong></strong></p>
<p><strong><br />If the Vault server throws an error do I get a Post event?      <br /></strong>Yes, and you can see the Exception that is thrown.     <strong></strong></p>
<p><strong><br />Am I guaranteed a Post event?      <br /></strong>Not in all cases.&#0160; It&#39;s technically possible for the operation to complete on the server, but your computer loses power right before the Post event.&#0160; However during normal operation, yes, you will always get a Post event.     <strong></strong></p>
<p><strong><br />Is there anyway to enforce that an Event fires?      <br /></strong>No.&#0160; If the computer doesn&#39;t have your handler installed, or your hander fails to load for some reason, the operation will go through without hitting your code.     <strong></strong></p>
<p><strong><br />How do I write an event handler?      <br /></strong>A detailed list of steps can be found in the <strong>Getting Started</strong> section of the API documentation.&#0160; The <strong>RestrictOperations</strong> sample in the SDK has sample code.</p>
<p>Here are the basic steps:</p>
<ol>
<li>Create your DLL project </li>
<li>Reference Auotdesk.Connectivity.WebServices.dll </li>
<li>Add a class that implements IWebServiceExtension </li>
<li>In the OnLoad() implementation, hook to the events that you care about.&#0160; The hooks are on the web services objects themselves.&#0160; (ex.&#0160; DocumentService.AddFileEvents.GetRestrictions ) </li>
<li>Write the event handler code </li>
<li>Deploy </li>
</ol>
<p><strong><br />I wrote a Vault client, will event handlers be hooking to my application?      <br /></strong>If you use Autodesk.Connectivity.WebServices.dll to communicate with the Vault Server, yes.&#0160; So your code should handle cases where operations are blocked by other event handlers.&#0160; In your app, these restrictions will bubble up as an Exception of type Autodesk.Connectivity.Extensibility.Framework.ExtensionException.     <strong></strong></p>
<p><strong><br />Are my handlers specific to a version of Vault?      <br /></strong>Yes.&#0160; For example, your Vault 2012 extension can handle events from the Vault 2012 version of Autodesk.Connectivity.WebServices.dll, but not from any other version of the DLL.     <strong></strong></p>
<p><strong><br />Which Web Service functions fire which events?      <br /></strong>You can see the full mapping in the <strong>Web Service Command Event Mappings</strong> of the Knowledge Base in the Vault SDK documentation.&#0160; There are no cases where a web service function fires multiple events.     <strong></strong></p>
<p><strong><br />Are there other event types?      <br /></strong>Yes, but I&#39;ll cover those in another post.</p>
<p><br /><strong>Update:</strong>&#0160; Article added for <a href="http://justonesandzeros.typepad.com/blog/2011/09/vault-explorer-command-events.html" target="_self">Vault Explorer Command Events</a></p>
<p><img alt="" src="/assets/Concepts3-1.png" /></p>
