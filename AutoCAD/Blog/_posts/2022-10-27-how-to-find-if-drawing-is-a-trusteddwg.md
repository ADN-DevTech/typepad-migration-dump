---
layout: "post"
title: "How to Find if Drawing is a TrustedDWG"
date: "2022-10-27 19:54:00"
author: "Madhukar Moogala"
categories:
  - ".NET"
  - "AutoCAD"
original_url: "https://adndevblog.typepad.com/autocad/2022/10/how-to-find-if-drawing-is-a-trusteddwg.html "
typepad_basename: "how-to-find-if-drawing-is-a-trusteddwg"
typepad_status: "Publish"
---

<p>
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?skin=sunburst"></script>
</p>
<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p><p><br></p><p>TrustedDWG is a key function of AutoCAD and AutoCAD LT that analyses DWG files as they are being opened. The function checks to see if the DWG file was last saved with an Autodesk product or by a software developer who is licensed to use the RealDWG toolkit. If the file does <em>not</em> pass the TrustedDWG analysis, it will inform you in various ways that the DWG file may not be compatible, nor guarantee its integrity when used with AutoCAD or AutoCAD LT.</p><p>The visibility of these warnings is controlled by the <a href="https://knowledge.autodesk.com/support/autocad/learn-explore/caas/CloudHelp/cloudhelp/2023/ENU/AutoCAD-Core/files/GUID-91E890BF-AEBD-4BCE-9663-1AFD595AA5E4-htm.html">DWGCHECK</a> system variable. DWGCHECK is an integer variable, and is saved in the registry</p><p>When a drawing is not a TrustedDWG, and you open the drawing in AutoCAD or LT, you get this message or pop dialog.</p><p><br></p><p><em>Non Autodesk DWG. This DWG file was saved by a software application that was not developed or licensed by Autodesk. Autodesk cannot guarantee the application compatibility or integrity of this file.</em></p><p><em><br></em></p><p><a href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02aed93eb147200d-pi"><img width="472" height="359" title="image" style="display: inline; background-image: none;" alt="image" src="/assets/image_707642.jpg" border="0"></a></p><p>Using API <a href="https://help.autodesk.com/view/OARX/2022/ENU/?guid=GUID-C5AB6F74-A01C-4CC7-9A1C-EEBC53176272">DwgFileWasSavedByAutodeskSoftware</a></p><p><br></p>

<pre class="prettyprint">        public void IsTrustedDWG()
        {
            Document doc = CoreApp.DocumentManager.MdiActiveDocument;
            if (doc == null) return;
            Editor ed = doc.Editor;
            var presult = ed.GetString(new PromptStringOptions("Enter Drawing File Path"));
            if (presult.Status != PromptStatus.OK) return;
            var db = HostApplicationServices.WorkingDatabase;
            Database sideDb = new Database(false, true);
            sideDb.ReadDwgFile(presult.StringResult, System.IO.FileShare.Read, true, null);
            HostApplicationServices.WorkingDatabase = sideDb;
            bool isTrustedDWG = sideDb.DwgFileWasSavedByAutodeskSoftware;
            if (isTrustedDWG)
            {
                ed.WriteMessage("Is Trusted DWG\n");
            }
            HostApplicationServices.WorkingDatabase = db;

        }

</pre>
