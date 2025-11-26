---
layout: "post"
title: "A TXT File may be a Type List"
date: "2012-03-07 05:00:00"
author: "Jeremy Tammik"
categories:
  - "External"
  - "Family"
  - "Getting Started"
original_url: "https://thebuildingcoder.typepad.com/blog/2012/03/a-txt-file-may-be-a-type-list.html "
typepad_basename: "a-txt-file-may-be-a-type-list"
typepad_status: "Publish"
---

<p>Here is a short note on a little issue to be aware of by Chuck Dodson of

<a href="http://www.sgadesigngroup.com">
SGA Design Group</a>:

<p><strong>Question:</strong> I am stuck trying to figure out the meaning of this error message:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330163028dc38a970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330163028dc38a970d" alt="TXT type list error message" title="TXT type list error message" src="/assets/image_19d6d1.jpg" border="0" /></a><br />

</center>

<p>I am creating a family in code.
I get the same problem if I create it manually.

<p>Do you have any idea what the message is saying?

<p>Looking online, I find comments about Shared Parameter file corruption, but I'm not using any.

<p>The odd thing is, if I rename the family, it loads fine.  
I can drag it from explorer; fine.  
I can load it from the open family file; fine.  
But if I use the Load Family button, I get the message, although it does actually load.

<p>Any ideas?

<p><strong>Answer:</strong> I solved this.  

<p>I was writing a text file (.txt) in the same folder that my families were being created in.  
For some reason, Revit assumes it is a shared parameter file.  

<p>I changed the filename extension of the ASCII files I was writing to DEF instead of TXT and the problem is solved.  

<p>The '(' in the message above is a reference to a parenthesis starting on the first line of the text file.

Thought you might want to know.

<p>Jeremy adds: yes, I did know, in fact, and so do most family developers, but possibly a number of API newbies do not, so I am sure that this is information worth publishing and sharing. 
Thank you very much for that, Chuck!
