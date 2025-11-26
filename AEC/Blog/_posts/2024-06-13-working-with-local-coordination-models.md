---
layout: "post"
title: "Working with Local Coordination Models"
date: "2024-06-13 02:06:13"
author: "Naveen Kumar"
categories:
  - ".NET"
  - "Naveen Kumar"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2024/06/working-with-local-coordination-models.html "
typepad_basename: "working-with-local-coordination-models"
typepad_status: "Publish"
---

<p>
<script src="https://cdn.jsdelivr.net/gh/google/code-prettify@master/loader/run_prettify.js?skin=sunburst"></script>
</p>
<p><span style="font-family: &#39;times new roman&#39;, times;">By <a href="https://adndevblog.typepad.com/aec/Naveen-Kumar.html" target="_self">Naveen Kumar</a></span></p>
<p><span style="font-family: &#39;times new roman&#39;, times;">To link a local Coordination Model in Revit, you can use Revit&#39;s API to automate the process. The goal is to retrieve all existing Coordination Model instances in your document and check their details to find the one you need. If it isn&#39;t already present, you can trigger a command to insert a new local Coordination Model.</span></p>
<p><span style="font-family: &#39;times new roman&#39;, times;">First, you access the active document through the `UIDocument` class. Using the `<strong>DirectContext3DDocumentUtils.GetDirectContext3DHandleInstances</strong>` method, you get all the Coordination Model instances in the document. This method returns a collection of element IDs, which you then loop through to examine each instance.</span></p>
<p><span style="font-family: &#39;times new roman&#39;, times;">For each element, you retrieve its type element and check the Path parameter to see if it matches the Coordination Model you&#39;re looking for. If the desired model isn&#39;t found, you can post a command to open the dialog for inserting a local Coordination Model.</span></p>
<p><span style="font-family: &#39;times new roman&#39;, times;">The presented code sample effectively illustrates this procedure. Initially, it retrieves the active document and proceeds to retrieve the instances of Coordination Models. Each instance undergoes thorough examination to identify the Path parameter. In case the desired Coordination Model is not found, users have the option to execute a command that will prompt the insertion of a local Coordination Model. This command can be initiated through either `<strong>PostableCommand.CoordinationModelLocal</strong>` for Revit versions from 2024 onwards, or `<strong>PostableCommand.CoordinationModel</strong>` for pre-Revit 2023 versions.</span><br /><br /><br /></p>
<pre class="prettyprint">UIDocument activeDoc = commandData.Application.ActiveUIDocument;
Document doc = activeDoc.Document;
ICollection&lt;ElementId&gt; instanceIds = DirectContext3DDocumentUtils.GetDirectContext3DHandleInstances(doc, new ElementId(BuiltInCategory.OST_Coordination_Model));
foreach (var id in instanceIds)
{
    Element elem = doc.GetElement(id);
    if (null != elem)
    {
        Element typeElem = doc.GetElement(elem.GetTypeId());
        if (null != typeElem)
        {
            Parameter param = typeElem.LookupParameter(&quot;Path&quot;);
            string path = param.AsValueString();
            // Check if this is the CM you&#39;re looking for by evaluating &#39;path&#39;
        }
    }
}

// If the CM is not found, post a command to insert a local CM
RevitCommandId coordinationModeCmdId = RevitCommandId.LookupPostableCommandId(PostableCommand.CoordinationModelLocal);
if (coordinationModeCmdId != null)
{
    commandData.Application.PostCommand(coordinationModeCmdId);
}
</pre>
