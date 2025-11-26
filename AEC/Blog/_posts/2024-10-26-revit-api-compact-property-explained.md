---
layout: "post"
title: "Revit API: Compact Property Explained"
date: "2024-10-26 15:20:02"
author: "Naveen Kumar"
categories:
  - ".NET"
  - "Naveen Kumar"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2024/10/revit-api-compact-property-explained.html "
typepad_basename: "revit-api-compact-property-explained"
typepad_status: "Publish"
---

<p>
<script src="https://cdn.jsdelivr.net/gh/google/code-prettify@master/loader/run_prettify.js?skin=sunburst"></script>
</p>
<p>By <a href="https://adndevblog.typepad.com/aec/Naveen-Kumar.html" target="_self">Naveen Kumar</a></p>
<p>The Compact File command in Revit plays a crucial role in reducing project file sizes and improving overall efficiency. As projects progress, they often accumulate unused or outdated data, which can lead to larger file sizes and potentially compromise performance.</p>
<p>When you save your Revit file, the Compact File command acts like a purge, systematically removing obsolete data and reorganizing the remaining content. This feature is especially beneficial for older projects that have undergone numerous modifications and have a lengthy saving history, as it can significantly reduce file size by eliminating unnecessary data. Regularly utilizing the Compact File command helps ensure your projects remain efficient, manageable, and run smoothly.</p>
<p>However, it’s important to understand that for new models or files with minimal changes, the Compact File command may have a limited impact. Its true power is revealed in complex, long-term projects that have experienced extensive modifications. In these scenarios, employing the Compact File command can dramatically improve performance and overall manageability.</p>
<p>In the Revit API, the <strong>&#39;Compact&#39;</strong> property is available within the <strong>SaveOptions</strong> and <strong>SaveAsOptions</strong> classes. When these options are utilized in the <strong>Save</strong> or <strong>SaveAs</strong> methods, the Compact File command is executed, providing a seamless way to optimize your project files programmatically.</p>
<p><span style="font-size: 12pt;"><strong>The SaveAs API and the Compact Property</strong></span></p>
<p>When using the SaveAs method, the model is always compacted automatically, regardless of whether the &#39;compact&#39; property is selected. Thus, in this case, the &#39;compact&#39; property becomes irrelevant, as file compaction occurs by default with every save operation.</p>
<p>This automatic compaction ensures that every time you save a file, outdated elements are removed, and the file remains optimized, contributing to better performance when working with the model.<br /><br /><strong><span style="font-size: 12pt;">The Save API and the Compact Property</span><br /></strong></p>
<p>In the Save API, Revit applies its internal logic to determine whether to trigger the compaction process when the &#39;compact&#39; property is enabled. However, if the &#39;compact&#39; property is not selected, Revit bypasses this logic and does not perform any file compaction.</p>
<ul>
<li><strong>If the &#39;compact&#39; property is enabled</strong>, Revit assesses the data streams in the file to determine if compaction is necessary. This assessment helps the system decide which outdated elements can be removed.<br /><br /></li>
<li><strong>If the &#39;compact&#39; property is not enabled</strong>, the compaction process is skipped, which may result in retaining unnecessary data and potentially larger file sizes as the project continues to evolve.</li>
</ul>
<p>One suggestion is to use the regenerate all function to update all elements, and then use the &quot;Save&quot; with the &quot;Compact&quot; property to verify if the file size decreases.</p>
<p>&#0160;</p>
