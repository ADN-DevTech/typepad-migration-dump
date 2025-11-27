---
layout: "post"
title: "SaveAs with SaveCopyAs False"
date: "2012-12-11 03:34:18"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/12/saveas-with-savecopyas-false.html "
typepad_basename: "saveas-with-savecopyas-false"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>If you want to save the document with a new file name and make it appear so in the user interface as well, just like in case of <strong>Inventor Menu &gt;&gt; Save As &gt;&gt; Save As</strong>,</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017d3eafb933970c-pi" style="display: inline;"><img alt="Inventor" class="asset  asset-image at-xid-6a0167607c2431970b017d3eafb933970c" src="/assets/image_67c695.jpg" title="Inventor" /></a><br />then you need to create a copy of the file with the new name using SaveAs(..., SaveCopyAs=True), open this file and close the original one.</p>
<p>This is exactly what Inventor is doing as well in the background.</p>
<p>Note that SaveAs(..., SaveCopyAs=False) should only be used in case of a new file which has not been saved yet, as it is pointed out in the API help file too:</p>
<p>&quot;SaveCopyAs: Input Boolean that indicates whether the file to be saved is new or is a copy of a previously existing file.&quot;</p>
<p>Here is also a table my colleague created describing the behaviour of the save functions in the API:</p>
<table border="1" cellpadding="2px" cellspacing="0">
<tbody>
<tr>
<td valign="top">
<div><span style="font-family: Calibri, sans-serif; font-size: small;"><span style="color: #1f497d;"><strong>API</strong></span></span></div>
</td>
<td valign="top">
<div><span style="font-family: Calibri, sans-serif; font-size: small;"><span style="color: #1f497d;"><strong>Expected Usage</strong></span></span></div>
</td>
<td valign="top">
<div><span style="font-family: Calibri, sans-serif; font-size: small;"><span style="color: #1f497d;"><strong>Note</strong></span></span></div>
</td>
<td valign="top" width="80">
<div><span style="font-family: Calibri, sans-serif; font-size: small;"><span style="color: #1f497d;"><strong>UI</strong></span></span></div>
</td>
</tr>
<tr>
<td valign="top">
<div><span style="font-family: Calibri, sans-serif; font-size: small;"><span style="color: #1f497d;">Save/Save2</span></span></div>
</td>
<td valign="top">
<div><span style="font-family: Calibri, sans-serif; font-size: small;"><span style="color: #1f497d;">If the document was saved onto disk, this is just like you click the <strong>Save</strong> button in UI, it will save the changes in memory to the file on disk. If the document is unsaved, this will pop up the <strong>Save As</strong> dialog.</span></span></div>
</td>
<td valign="top">
<div><span style="font-family: Calibri, sans-serif; font-size: small;"><span style="color: #1f497d;">&#0160;</span></span></div>
</td>
<td valign="top">
<div><span style="font-family: Calibri, sans-serif; font-size: small;"><span style="color: #1f497d;">Save</span></span></div>
</td>
</tr>
<tr>
<td valign="top">
<div><span style="font-family: Calibri, sans-serif; font-size: small;"><span style="color: #1f497d;">SaveAs(,False)</span></span></div>
</td>
<td valign="top">
<div><span style="font-family: Calibri, sans-serif; font-size: small;"><span style="color: #1f497d;">This is for unsaved/new document only, and similar to the Document.Save but provide the full filename for unsaved document so no <strong>Save As</strong> dialog will pop up.</span></span></div>
</td>
<td valign="top">
<div><span style="font-family: Calibri, sans-serif; font-size: small;"><span style="color: #1f497d;"><strong>Don’t call this for a saved document</strong>, because this behaves different from the UI <strong>Save As</strong> function. The UI <strong>Save As</strong> will create a copy and then close the current open file and open the copied file, but the API won’t.</span></span></div>
</td>
<td valign="top">
<div><span style="font-family: Calibri, sans-serif; font-size: small;"><span style="color: #1f497d;">Save</span></span></div>
</td>
</tr>
<tr>
<td valign="top">
<div><span style="font-family: Calibri, sans-serif; font-size: small;"><span style="color: #1f497d;">SaveAs(,True)</span></span></div>
</td>
<td valign="top">
<div><span style="font-family: Calibri, sans-serif; font-size: small;"><span style="color: #1f497d;">Just save a copy of the document no matter the document is unsaved or saved.</span></span></div>
</td>
<td valign="top">
<div><span style="font-family: Calibri, sans-serif; font-size: small;"><span style="color: #1f497d;">&#0160;</span></span></div>
</td>
<td valign="top">
<div><span style="font-family: Calibri, sans-serif; font-size: small;"><span style="color: #1f497d;">Save Copy As</span></span></div>
<p><span style="font-family: Calibri, sans-serif; font-size: small;"><span style="color: #1f497d;">&#0160;</span></span></p>
</td>
</tr>
</tbody>
</table>
<p>&#0160;</p>
