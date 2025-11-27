---
layout: "post"
title: "Attachments and Dependencies FAQ"
date: "2010-11-19 13:21:39"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
original_url: "https://justonesandzeros.typepad.com/blog/2010/11/attachments-and-dependencies-faq.html "
typepad_basename: "attachments-and-dependencies-faq"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/TipsAndTricks2.png" /></p>
<p>I want to explain some of the more confusing aspects of File attachments and dependencies.&#0160;</p>
<p><strong>What is a Dependency?      <br /></strong>It&#39;s just what it sounds like.&#0160; The parent file <em>depends</em> on the child file.&#0160; The parent file can&#39;t work properly if the child file is not present.&#0160; Dependencies should be only created programmatically such as the CAD client or a custom program.</p>
<p><strong>What is an Attachment?      <br /></strong>It a parent/child link where the parent does not depend on the child file.&#0160; This is the only association type that the end user can explicitly create in the Vault Explorer client.&#0160;</p>
<p><strong>What type should I use?      <br /></strong>Sometimes it&#39;s unclear what type to use.&#0160; I recommend asking yourself this question &quot;When viewing the parent file, do I always want the child file to be present?&quot;&#0160; If the answer is yes, then it&#39;s a dependency.&#0160; If the answer is no, then it&#39;s probably an attachment.</p>
<p><strong>How do dependencies affect behavior?      <br /></strong>A lot more business logic is built into dependencies.&#0160; Operations that will break a file reference are restricted.&#0160; For example, you can&#39;t delete a file that has a parent dependency.&#0160; However, you are allowed to do the delete if you are deleting all the parents too.&#0160;</p>
<p>The move and rename operations in Vault Explorer have special client-side logic for updating parent references during these operations.&#0160; If the parent can&#39;t be updated, the operation is blocked.&#0160; However this is all client-side logic.&#0160; The server will allow moves and renames in these cases.&#0160; So it&#39;s up to the client application to insure that file references don&#39;t get broken when the API calls are made.</p>
<p><strong>What is a Design Visualization attachment?      <br /></strong>This is a case where a parent is attached to a child file with the <em>DesignVisualization</em> classification.&#0160; In other words, the child is meant to provide a view for the CAD data.&#0160; The nice thing about this attachment type is that you can create them without increasing the version of the file.</p>
<p>The server will allow you to have design visualizations of any type, however many clients will only recognize DWF files.&#0160; It&#39;s also a good idea to avoid having more than one visualization attachment on a single file.</p>
<p><strong>What does a &quot;source&quot; mean?      <br /></strong>When you add or check-in a file, you set up your attachments and dependencies.&#0160; You pass in the File ID of the child file and you have to provide a &quot;source&quot; for the association.&#0160; A source is basically the application that &quot;owns&quot; the link.&#0160; Other applications are supposed to refrain from modifying the link.&#0160; A null value means that the link is not specific to an application.</p>
<p>The source concept was introduced specifically for the Inventor DWG feature, and I am not aware of any other uses.&#0160; Basically, it tells if an association is owned by Inventor or AutoCAD so that one app doesn&#39;t alter the other app&#39;s associations.    <br />The only legal values for a source are null, &quot;INVENTOR&quot; and &quot;AUTOCAD&quot;.&#0160; Any other value may lead to strange behavior in some clients (the server doesn&#39;t care).</p>
<p>The recommendation is to pass in null for new associations.&#0160; If you are preserving an existing association, just pass in the existing source.&#0160; Inventor and AutoCAD files should be added to Vault via through the CAD applications themselves.</p>
