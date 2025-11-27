---
layout: "post"
title: "Getting the Inventor document's software versions along with the saved and created details"
date: "2012-05-15 07:23:27"
author: "Gary Wassell"
categories:
  - "Gary Wassell"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/05/getting-the-inventor-documents-software-versions-along-with-the-saved-and-created-details.html "
typepad_basename: "getting-the-inventor-documents-software-versions-along-with-the-saved-and-created-details"
typepad_status: "Publish"
---

<p><strong>Issue</strong></p>
<p>How do I find the version of the Inventor that was used to create and save a certain Inventor document?</p>
<p><a name="section2"></a></p>
<p><strong>Solution</strong></p>
<p>Open the document.</p>
<p>Run the following macro. It can be used to determine the major, minor, and service pack versions used to create/ save a document.</p>
<pre><p>Private Sub SoftwareVersion()<br />&#0160;&#0160;&#0160; MsgBox ThisApplication.ActiveDocument.SoftwareVersionCreated.Major<br />&#0160;&#0160;&#0160; MsgBox ThisApplication.ActiveDocument.SoftwareVersionCreated.Minor<br />&#0160;&#0160;&#0160; MsgBox ThisApplication.ActiveDocument.SoftwareVersionCreated.ServicePack<br />&#0160;&#0160;&#0160; MsgBox ThisApplication.ActiveDocument.SoftwareVersionSaved.Major<br />&#0160;&#0160;&#0160; MsgBox ThisApplication.ActiveDocument.SoftwareVersionSaved.Minor<br />&#0160;&#0160;&#0160; MsgBox ThisApplication.ActiveDocument.SoftwareVersionSaved.ServicePack<br />End Sub</p></pre>
