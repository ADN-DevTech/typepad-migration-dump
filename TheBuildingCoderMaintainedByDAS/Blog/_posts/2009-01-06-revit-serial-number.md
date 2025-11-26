---
layout: "post"
title: "Revit Serial Number"
date: "2009-01-06 05:00:00"
author: "Jeremy Tammik"
categories:
  - "External"
original_url: "https://thebuildingcoder.typepad.com/blog/2009/01/revit-serial-number.html "
typepad_basename: "revit-serial-number"
typepad_status: "Publish"
---

<p>Here is a short and simple question with a simple answer.</p>

<p><strong>Question:</strong> How can I determine the Revit product's serial number?</p>

<p><strong>Answer:</strong> The Revit serial number is available in the Licpath.lic file, which is a text file located in the Revit program folder.
The content of the file is something similar to the following, where the string after # SN is the serial number:</p>

<pre>
## Autodesk Revit
## License Information
# SN 123-45678910
# NSN 000-00000000
# Standalone
</pre>
