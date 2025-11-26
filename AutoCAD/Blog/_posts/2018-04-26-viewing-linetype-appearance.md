---
layout: "post"
title: "Viewing LineType Appearance"
date: "2018-04-26 22:01:00"
author: "Madhukar Moogala"
categories:
  - ".NET"
  - "AutoCAD"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2018/04/viewing-linetype-appearance.html "
typepad_basename: "viewing-linetype-appearance"
typepad_status: "Publish"
---

<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?skin=sunburst"></script>
<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar 
Moogala</a></p><p>I have recieved this query, user would like to get appearance of LineTypeRecord or LineTypes through API.</p><p>We can use Comments API to gets the Linetype appearance in the form of ascii string.</p>
<p>Code:</p>
<pre class="prettyprint">public static void LineTypeAppearance()
        {
            Database database = HostApplicationServices.WorkingDatabase;
            var ed = AcCore.Application.DocumentManager.MdiActiveDocument.Editor;
            using (Transaction t = database.TransactionManager.StartTransaction())
            {
                var symTable = (SymbolTable)t.GetObject(database.LinetypeTableId,
                                                        OpenMode.ForRead);
                foreach (ObjectId id in symTable)
                {
                    var symbol = (LinetypeTableRecord)t.GetObject(id, OpenMode.ForRead);
                    ed.WriteMessage(string.Format("\nName: {0}\t Appearance: {1}",
                                                    symbol.Name, symbol.Comments));
                }

                t.Commit();
            }
        }
</pre>
<p> Output </p>
<pre>Name: BYBLOCK   Desc:
Name: BYLAYER   Desc:
Name: CONTINUOUS      Desc: Solid line
Name: Wall Base|CENTER     Desc: Center ____ _ ____ _ ____ _ ____ _ ____ _ ____
Name: Wall Base|DASHED     Desc: Dashed __ __ __ __ __ __ __ __ __ __ __ __ __ _
</pre>
