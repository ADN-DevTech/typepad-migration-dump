---
layout: "post"
title: "Specifying a File path correctly in MEL/Python"
date: "2016-08-17 01:46:20"
author: "Vijaya Prakash"
categories:
  - "FBX"
  - "Maya"
  - "MEL"
  - "Vijay Prakash"
original_url: "https://around-the-corner.typepad.com/adn/2016/08/specifying-a-file-path-correctly-in-melpython.html "
typepad_basename: "specifying-a-file-path-correctly-in-melpython"
typepad_status: "Publish"
---

<p>Whenever you write a MEL script or a Python script that contains a file path, you should pay attention to whether it is a relative file path or an absolute file path. In case of POSIX, it is very clear that the path separator is always forward slash (/). In case of Windows, you have to be bit careful on file path separator.<br /> <br /> If you specify a path on Windows, you have either to use forward slash (/) or double backward slash (\\). Don’t use single back-slash (\), because Windows will interpret it as an escape sequence. For example, if you specify a path like “C:\vijay”, Windows will consider it as “C:ijay”, because \v is the escape sequence. <br /> <br /> A Maya user faced the same issue when he tried to import the FBX file using the FBXImport command and received an error. The error is caused by the malformed file path, and resulting Windows escape sequence errors. In the end, it has nothing to do with FBXImport command or Maya, but in that context it may be easily overlooked as a formatting error.<br /> <br /> Below are the scenarios that will explain the issue more clearly.<br /> <br /> <strong>--- Error Scenario ---</strong><br /> string $strDir=&quot;C:\vijay\output.fbx&quot;;<br /> FBXImport -f $strDir;<br /> // Error: Use syntax: FBXImport -f &quot;filename&quot; [-t take_index_zero_based] [-e extract_folder] //</p>
<p><a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d01b7c8880175970b-pi" style="display: inline;"><img alt="Fbximport-path-error" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d01b7c8880175970b image-full img-responsive" src="/assets/image_05ab5a.jpg" title="Fbximport-path-error" /></a></p>
<p>&#0160;</p>
<p>--- <strong>Working Scenarios</strong> ---<br /> <br /> string $strDir=&quot;C:\\vijay\\output.fbx&quot;;<br /> FBXImport -f $strDir;<br /> // Logfile: &quot;C:\Users\prakasv\Documents\maya\FBX\Logs\2016.1\maya2016imp.log&quot; //<br /> // Result: Success //<br /> <br /> string $strDir=&quot;C:/vijay/output.fbx&quot;;<br /> FBXImport -f $strDir;<br /> // Logfile: &quot;C:\Users\prakasv\Documents\maya\FBX\Logs\2016.1\maya2016imp.log&quot; // <br /> // Result: Success //</p>
