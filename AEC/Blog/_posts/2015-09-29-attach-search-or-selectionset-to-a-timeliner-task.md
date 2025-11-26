---
layout: "post"
title: "Attach Search or SelectionSet to a Timeliner Task"
date: "2015-09-29 20:22:04"
author: "Xiaodong Liang"
categories:
  - ".NET"
  - "Navisworks"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/aec/2015/09/attach-search-or-selectionset-to-a-timeliner-task.html "
typepad_basename: "attach-search-or-selectionset-to-a-timeliner-task"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>TimelinerSelection.CopyFrom allows you copy explicit selected items, a Search, a Selection,&nbsp;SelectionSourceCollection.&nbsp;SelectionSourceCollection is a&nbsp;collection of SelectionSources, while&nbsp;SelectionSource represents a reference to a source that can generate a selection on demand, generally a kind of&nbsp;middleware when you want to attach SelectionSet to Timeliner task.</p>
<p>The following code demos the two scenarios. It assumes the document has two Timeliner tasks, current search is available, and at least one SelectionSet exists. The code&nbsp;checks the tasks in first level only. It will attach current Search to the first task, and attach first SelectionSet to the second task.</p>
<p>&nbsp;</p>
<!--Reference google-code-prettify, as google access is limited in China, so I uploaded the js to typepad directly-->
<script type="text/javascript" src="https://adndevblog.typepad.com/files/run_prettify-3.js"></script>
<!--This is the code, set class to prettyprint so that google-code-prettify will help to format the code. set class to csharp is for csdn and no need to reference google-code-prettify-->
<pre class="csharp prettyprint"> 
using Nw = Autodesk.Navisworks.Api;
using Tl = Autodesk.Navisworks.Api.Timeliner;


      public override int Execute(params string[] parameters)
        { 
            Document oDoc = Autodesk.Navisworks.Api.Application.ActiveDocument;

            

            try
            {
                Nw.Document doc = Nw.Application.ActiveDocument;
                Nw.DocumentParts.IDocumentTimeliner tl = doc.Timeliner;
                Tl.DocumentTimeliner tl_doc = (Tl.DocumentTimeliner)tl;

                //assume we check the tasks in first level only

                SavedItem parentItem = tl_doc.TasksRoot;


                if (tl_doc.Tasks.Count &gt; 1)
                {
                    //test with the first and second tasks only

                    //first task, attach a Search Set
                    Tl.TimelinerTask oTask1 = tl_doc.Tasks[0] as Tl.TimelinerTask;

                    if (!doc.CurrentSearch.ToSearch().IsClear)
                    {
                        //if a current search exists
                        SelectionSet oMySet = new SelectionSet(oDoc.CurrentSearch.ToSearch());

                        //attach selection set to the task
                        Tl.TimelinerTask oCopyTask1 = oTask1.CreateCopy();
                        oCopyTask1.Selection.CopyFrom(doc.CurrentSearch.ToSearch());
                        tl_doc.TaskReplaceWithCopy((GroupItem)parentItem, 0, oCopyTask1);
                         
                    }

                    //second task, attach a Selection Set
                    Tl.TimelinerTask oTask2 = tl_doc.Tasks[1] as Tl.TimelinerTask;

                    if (doc.SelectionSets.RootItem.Children.Count &gt; 0)
                    {    
                        SelectionSet oOneSet = doc.SelectionSets.RootItem.Children[0] as SelectionSet ;

                         SelectionSourceCollection oSetSourceColl = new SelectionSourceCollection();
                         SelectionSource oSetSource =doc.SelectionSets.CreateSelectionSource(oOneSet);
                        oSetSourceColl.Add(oSetSource);

                        //attach selection set to the task
                        Tl.TimelinerTask oCopyTask2 = oTask2.CreateCopy();
                        oCopyTask2.Selection.CopyFrom(oSetSourceColl);
                        tl_doc.TaskReplaceWithCopy((GroupItem)parentItem, 1, oCopyTask2);
                    }
                    
                }

               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
         
            return 0;
        }</pre<br /><br /> <a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0879ff79970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb0879ff79970d image-full img-responsive" title="2015-9-30 11-19-50" src="/assets/image_862942.jpg" alt="2015-9-30 11-19-50" border="0" /></a><br />
>
