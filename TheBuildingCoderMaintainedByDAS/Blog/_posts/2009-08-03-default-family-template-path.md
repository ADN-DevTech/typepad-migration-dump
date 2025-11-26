---
layout: "post"
title: "Default Family Template Path"
date: "2009-08-03 07:00:00"
author: "Jeremy Tammik"
categories:
  - "External"
  - "User Interface"
original_url: "https://thebuildingcoder.typepad.com/blog/2009/08/default-family-template-path.html "
typepad_basename: "default-family-template-path"
typepad_status: "Publish"
---

<p><strong>Question:</strong>
Is any API exposed to read and write the default family template path?
It is displayed under 'Default path for family template files' in the RAC 2010 options dialogue:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330115724c6f26970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="at-xid-6a00e553e1689788330115724c6f26970b" alt="Default family template path" title="Default family template path" src="/assets/image_295669.jpg" border="0"  /></a>

</center>

<p><strong>Answer:</strong>
Unfortunately, there is currently no way to set the path in the options dialog at runtime through the Revit API.
You can read it from the Revit.ini file, though.
It is stored under the FamilyTemplatePath key in the [Directories] section.</p>
