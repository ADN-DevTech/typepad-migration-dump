---
layout: "post"
title: "Restoring a Missing Project Information Element"
date: "2016-12-08 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Element Creation"
  - "Settings"
  - "Utilities"
original_url: "https://thebuildingcoder.typepad.com/blog/2016/12/restoring-a-missing-project-information-element.html "
typepad_basename: "restoring-a-missing-project-information-element"
typepad_status: "Publish"
---

<p>Some people have recently reported that they encountered models lacking the <code>ProjectInfo</code> <a href="http://www.revitapidocs.com/2017/e90b12f3-9bf4-f536-3556-c9944cbf9f38.htm">project information singleton element</a>.</p>

<p>Apparently, it was possible in previous versions of Revit for a faulty or malicious add-in to simply delete this element.</p>

<p>That obviously causes problems for other add-ins and Revit itself, who rely on its presence.</p>

<p>Luckily, it is not hard to fix.</p>

<p>Here is the latest discussion addressing this issue:</p>

<p><strong>Question:</strong> I encountered models missing Project Information, causing errors to be thrown.</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b8d2443b4b970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b8d2443b4b970c image-full img-responsive" alt="Missing Project Information" title="Missing Project Information" src="/assets/image_ed6fd0.jpg" border="0" /></a><br /></p>

<p></center></p>

<p>Can you provide any guidance on a potential fix for this?</p>

<p>Can we perform a Transfer Project Standards like process to copy the project information in from another model?</p>

<p><strong>Answer:</strong> You can use the copy and paste API and
the <a href="http://www.revitapidocs.com/2017/b22df8f6-3fa3-e177-ffa5-ba6c639fb3dc.htm">CopyElements method</a> to
copy in the project information element from some other intact RVT file.</p>

<p>The file can also be fixed via <a href="https://knowledge.autodesk.com/support/revit-products/troubleshooting/caas/sfdcarticles/sfdcarticles/Project-information-button-is-inactive-in-Revit.html">transfer project standards</a>.</p>

<p>We believe this problem to be fixed in Revit 2017 and later version, because API applications are now prevented from (hopefully accidentally) deleting the element.</p>

<p><strong>Response:</strong> Thanks.</p>

<p>I have been successful in using the copy-paste method to automate the process to circumvent the errors and add the missing Project Information.</p>

<p>Thanks again for the help and suggestions!</p>
