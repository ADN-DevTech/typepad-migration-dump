---
layout: "post"
title: "DGNIMPORT to a new document and document switching"
date: "2015-12-15 01:09:40"
author: "Balaji"
categories:
  - ".NET"
  - "2015"
  - "2016"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2015/12/dgnimport-to-a-new-drawing-and-document-switching.html "
typepad_basename: "dgnimport-to-a-new-drawing-and-document-switching"
typepad_status: "Publish"
---

<a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html">By Balaji Ramamoorthy</a>
<p>Recently, one of my colleague had this query :</p>
<ul>
<li>I am importing the DGN (not to the current drawing). This creates a new drawing and I want to switch to the new drawing and perform other commands. What is the best way to switch the current drawing to the other open drawing.</li>
</ul>
<p>Here is a code snippet to do that.</p>
<p></p>
<p>The command that invokes the "DGNIMPORT", makes the newly imported document as Active. After the document is made active, you are in the new document’s context and to verify it, the editor.writemessage will output the message in the new document's command prompt.</p>
<p></p>
<pre>
[CommandMethod("Test1", CommandFlags.Session)]
public async static void Test1Method()
{
    DocumentCollection dm = Application.DocumentManager;
    Editor ed = 
    Application.DocumentManager.MdiActiveDocument.Editor;
    try
    {
        PromptResult promptDgnPath
                = ed.GetString("\nEnter DGN file name: ");
        if (promptDgnPath.Status != PromptStatus.OK)
            return;

        try
        {
            await dm.ExecuteInCommandContextAsync(
                async (obj) =>
                {
                    await ed.CommandAsync(new object[] 
                    { 
                        "_.-DGNIMPORT", 
                        promptDgnPath.StringResult, 
                        "Default", 
                        "Master", 
                        "Standard" 
                    });
                },
                null
            );
        }
        catch (System.Exception ex)
        {
            ed.WriteMessage("\nException: {0}\n", ex.Message);
        }

        DocumentCollection docs = Application.DocumentManager;
        foreach (Document doc in docs)
        {
            if (doc.Name.StartsWith("Drawing"))
            {
                Application.DocumentManager.MdiActiveDocument 
                                                        = doc;
                break;
            }
        }
        ed.WriteMessage("Hello !!");
    }
    catch (System.Exception ex)
    {
        ed.WriteMessage(ex.Message);
    }
}
</pre>
