---
layout: "post"
title: "RevitAPI: How to create wall sweep?"
date: "2016-04-07 23:30:00"
author: "Aaron Lu"
categories:
  - "Aaron Lu"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2016/04/revitapi-how-to-create-wall-sweep.html "
typepad_basename: "revitapi-how-to-create-wall-sweep"
typepad_status: "Publish"
---

<p><a href="http://blog.csdn.net/lushibi/article/details/48476885">中文链接</a></p>
<p><small>By <a href="http://adndevblog.typepad.com/aec/aaron-lu.html" target="_self">Aaron Lu</a></small></p>
<p>Wall Sweep's API class is WallSweep, but there is no method like NewWallSweep under Document.Create object, so how to create it via API?</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1a1acf2970c-pi" style="display: inline;"><img class="asset  asset-image at-xid-6a0167607c2431970b01b8d1a1acf2970c image-full img-responsive" title="Wallsweep-en" src="/assets/image_931429.jpg" alt="Wallsweep-en" border="0" /></a></p>
<p>The answer is using static method Create of WallSweep:</p>
<pre class="csharp prettyprint">public static WallSweep Create(Wall wall, ElementId wallSweepType, WallSweepInfo wallSweepInfo);</pre>
<p>Code example:</p>
<pre class="csharp prettyprint">var doc = commandData.Application.ActiveUIDocument.Document;
var uiSel = commandData.Application.ActiveUIDocument.Selection;

try
{
    var reference = uiSel.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element, "Pick wall");
    var wall = doc.GetElement(reference) as Wall;
    ElementType wallSweepType = new FilteredElementCollector(doc)
        .OfCategory(BuiltInCategory.OST_Cornices)
        .WhereElementIsElementType()
        .Cast().FirstOrDefault();
    if (wallSweepType != null)
    {
        var wallSweepInfo = new WallSweepInfo(WallSweepType.Sweep, false);
        wallSweepInfo.Distance = 2;
        using (Transaction transaction = new Transaction(doc))
        {
            transaction.Start("create wall sweep");
            WallSweep.Create(wall, wallSweepType.Id, wallSweepInfo);
            transaction.Commit();
        }
    }
    else
    {
        TaskDialog.Show("ERROR", "no wall sweep type is found");
    }
}
catch (Autodesk.Revit.Exceptions.OperationCanceledException)
{
}</pre>
<ol>
<li>First, pick a wall as the host</li>
<li>then filter out all the wallsweep type and choose the first one</li>
<li>create the argument object WallSweepInfo&nbsp;by&nbsp;calling the constructor of WallSweepInfo, note that we can continue to customize it&nbsp;by setting&nbsp;its properties, e.g. in this example, by calling 'wallSweepInfo.Distance = 2' to set the distance of the wall sweep to the bottom of the hosted wall</li>
<li>Finally, call WallSweep.Create function to create the wall sweep</li>
</ol>
<p>&nbsp;</p>
