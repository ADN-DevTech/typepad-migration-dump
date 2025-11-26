---
layout: "post"
title: "Good Practice: Encoding Your File Name When Uploading and Setting Reference with View and Data API"
date: "2015-09-30 13:35:00"
author: "Shiya Luo"
categories:
  - "Javascript"
  - "Shiya Luo"
  - "Storage"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/09/good-practice-encoding-your-file-name-when-uploading-and-setting-reference-with-view-and-data-api.html "
typepad_basename: "good-practice-encoding-your-file-name-when-uploading-and-setting-reference-with-view-and-data-api"
typepad_status: "Publish"
---

<p>There are some pitfalls in the upload and set references pipeline that you should watch out for.</p>
<p>When dealing with file uploads and set references, the file name will sometimes corrupt your upload if it contains space or other special characters. It&#39;s best to URL encode the file name.</p>
<p>When you set references between files, you can swap out different versions of a child file, set the reference, go through the translation again, and the new version will show up in the viewer. If you still want to keep the older version, though, uploading a new file with the same name will overwrite the old file. That</p>
<p>Two things you can do to counter these two scenarios</p>
<p>1. URL encode the file.</p>
<p>2. Date stamp your upload, so each of your upload has a unique name.</p>
<p>Here&#39;s some sample code written in JavaScript for reference:</p>
<pre class="brush:csharp;"><code>/**
 * @param  {string}	fileName a file name with extension
 * @return {string}	url encoded file name with date and extension
 */
function encodeFileName(fileName) {
	if (typeof fileName !== &quot;string&quot;) {
		return null;
	}
	var fileExtensionIndex = fileName.lastIndexOf(&quot;.&quot;);
	if (fileExtensionIndex &lt; 0) {
		fileExtensionIndex = fileName.length;
	}
	var fileExtension = fileName.substring(fileExtensionIndex, fileName.length);
	var fileName = fileName.substring(0, fileExtensionIndex);
	fileName = encodeURIComponent(fileName) + Date.now() + fileExtension;
	return fileName;
}

/**
 * @param  {string} file name encoded using encodeFileName
 * @return {string} orignial file name
 */
function decodeFileName(fileName) {
	if (typeof fileName !== &quot;string&quot;) {
		return null;
	}
	var fileExtensionIndex = fileName.lastIndexOf(&quot;.&quot;);
	if (fileExtensionIndex &lt; 0) {
		fileExtensionIndex = fileName.length;
	}
	var fileExtension = fileName.substring(fileExtensionIndex, fileName.length);
	fileName = fileName.substring(0, fileExtensionIndex - 13);
	fileName = decodeURIComponent(fileName) + fileExtension;
	return fileName;
}</code></pre>
<p>Copy the code into your JavaScript console and try it out!</p>
<p>Now, if I have a file name that says <code>&quot;平面图 $∂.dwg&quot;</code>, after the encoding the name will be <code>&quot;%E5%B9%B3%E9%9D%A2%E5%9B%BE%20%24%E2%88%821442527104059.dwg&quot;</code>. Much more URL-friendly!</p>
<p>Notice how the .dwg extension is still there. This is how the translation service determines which pipeline it should go through, be it AutoCAD, Revit, Navisworks, etc.</p>
