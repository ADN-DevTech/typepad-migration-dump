---
layout: "post"
title: "Differentiate Inventor documents"
date: "2015-12-22 03:09:55"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/12/differentiate-inventor-documents.html "
typepad_basename: "differentiate-inventor-documents"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html">Adam Nagy</a></p>
<p>When you create a new document then a new <a href="https://en.wikipedia.org/wiki/Globally_unique_identifier">GUID</a>&#0160;will be assigned to its&#0160;<strong>InternalName</strong> property. This will stay the same for the lifetime of the document no matter what modifications have been done in the document geometry or file properties. This property also remains the same when the file gets copied or renamed. So how to tell the difference between two copied documents&#0160;once they got modified?</p>
<p>You can use the <strong>RevisionId</strong> and <strong>DatabaseRevisionId</strong> property for that depending on what kind of differences you are interested in:</p>
<p><strong>RevisionId</strong></p>
<p class="p1"><em>Gets the GUID that represents the last saved revision of this file. Works as a stamp of the contents of this file.</em></p>
<p><strong>DatabaseRevisionId</strong></p>
<p class="p1"><em>Gets the GUID that represents the last saved revision of database contained in this document. This revision id tracks modifications to the database (such as reference changes, geometry changes, etc.) but does not track file property changes.</em></p>
<p>If two documents have the same <strong>InternalName</strong>, then they were originally the same file but could have been modified in the meantime.</p>
<p>If their&#0160;<strong>DatabaseRevisionId</strong> is the same, then their geometry and references are the same.</p>
<p>If their&#0160;<strong>RevisionId</strong>&#0160;is the same, then everything in the two files are the same.</p>
