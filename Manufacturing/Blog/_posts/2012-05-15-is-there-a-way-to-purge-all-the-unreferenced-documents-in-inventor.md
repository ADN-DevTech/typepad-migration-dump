---
layout: "post"
title: "Is there a way to &quot;purge&quot; all the unreferenced documents in Inventor ?"
date: "2012-05-15 07:28:23"
author: "Gary Wassell"
categories:
  - "Gary Wassell"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/05/is-there-a-way-to-purge-all-the-unreferenced-documents-in-inventor.html "
typepad_basename: "is-there-a-way-to-purge-all-the-unreferenced-documents-in-inventor"
typepad_status: "Publish"
---

<p><strong>Issue</strong></p>
<p>I need a way to &quot;purge&quot; all the unreferenced documents. Is there a method available to do this?</p>
<p><a name="section2"></a></p>
<p><strong>Solution</strong></p>
<p>The Documents.CloseAll() method has a Boolean parameter which indicates whether to close only the unreferenced documents. By default the value is false and closes all the documents. Following is the excerpt from the help file explaining the Boolean parameter UnreferencedOnly.</p>
<p>“Public Sub CloseAll( Optional ByVal UnreferencedOnly As Boolean = False)</p>
<p>UnreferencedOnly <br />Optional input Boolean that indicates whether to close only the unreferenced documents. If not specified, a value of False is assumed and all documents are closed.</p>
<p>Examples of unreferenced documents:</p>
<p>Create a new assembly, place an instance of a part &quot;block.ipt&quot; and then delete the instance in the assembly. At this point, block.ipt is an unreferenced document. Set the Suppressed property of a ComponentOccurrence to True within an API Transaction (or a ChangeProcessor). Assuming that the document that this occurrence was referencing is not referenced elsewhere, it becomes an unreferenced document.</p>
