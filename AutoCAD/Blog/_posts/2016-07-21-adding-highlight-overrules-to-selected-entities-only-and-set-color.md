---
layout: "post"
title: "Adding highlight overrules to selected entities only and set color"
date: "2016-07-21 01:28:34"
author: "Deepak A S Nadig"
categories:
  - ".NET"
  - "Deepak A S Nadig"
original_url: "https://adndevblog.typepad.com/autocad/2016/07/adding-highlight-overrules-to-selected-entities-only-and-set-color.html "
typepad_basename: "adding-highlight-overrules-to-selected-entities-only-and-set-color"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/deepak-nadig.html" target="_self">Deepak Nadig</a></p>
<p>Here is a&nbsp;method to add highlight overrules to selected objects only. Interactively on highlighting the objects to which overrules are added,color needs to change</p>
<p>The code sample below adds the HighlightOverrrule to selected objects and also enables to set highlight color to each object after adding the overrule</p>
<p>To test:&nbsp;&nbsp;</p>
<p>1) First, run the 'SelectObjectsToAddOverrule' command, to select the required objects.</p>
<p>_hlOverRule.SetIdFilter(ids.ToArray()) will restrict addition of overrule to the selected objects only. Color of the selected objects will be changed to red during and after selection(_colorIndex =1)&nbsp;</p>
<p>2) Set the colorIndex value &nbsp;by running the command ' ChangeHighlightColor' . Now, interactively highlight any selected object to see color change</p>
<p>(Note : Interactively, highlighting &nbsp;objects which were not selected,will have no effect as overrule is not added to them)&nbsp;</p>
<p>&nbsp;</p>
<pre>
<p>public class MyHighlightOverrule : HighlightOverrule<br /> {<br /> private int _colorIndex = 1;<br /> private int _oldColorIndex;</p>
<p>public MyHighlightOverrule()<br /> {<br /> AddOverrule(RXClass.GetClass(typeof(Entity)), this, true);<br /> }<br /> public int ColorIndex<br /> {<br /> set { _colorIndex = value; }<br /> get { return _colorIndex; }<br /> }<br /> public override void Highlight(Entity entity, FullSubentityPath subId, bool highlightAll)<br /> {<br /> Database db = entity.Database;<br /> Document dwg = Application.DocumentManager.MdiActiveDocument;</p>
<p>using (DocumentLock dl = dwg.LockDocument())<br /> {<br /> using (Transaction tran = db.TransactionManager.StartTransaction())<br /> {<br /> entity.UpgradeOpen();<br /> _oldColorIndex = entity.ColorIndex;<br /> entity.ColorIndex = _colorIndex;<br /> entity.DowngradeOpen();<br /> }<br /> }<br /> base.Highlight(entity, subId, highlightAll);<br /> }<br /> }<br /> public class Commands<br /> {<br /> private static MyHighlightOverrule _hlOverRule = null;<br /> [CommandMethod("SelectObjectsToAddOverrule")]<br /> public void SelectObjectsToAddOverrule()<br /> {<br /> Document doc = Application.DocumentManager.MdiActiveDocument;<br /> Editor ed = doc.Editor;<br /> Transaction tr = doc.TransactionManager.StartTransaction();<br /> using (tr)<br /> {<br /> if (_hlOverRule == null)<br /> {<br /> _hlOverRule = new MyHighlightOverrule();<br /> }<br /> // Loop until or completed<br /> List&lt;ObjectId&gt; ids = new List&lt;ObjectId&gt;();<br /> List&lt;FullSubentityPath&gt; paths = new List&lt;FullSubentityPath&gt;();<br /> PromptNestedEntityResult rs;<br /> do<br /> {<br /> rs = ed.GetNestedEntity(String.Format("\nSelect entity to highlight color"));<br /> if (rs.Status == PromptStatus.OK)<br /> {<br /> ids.Add(rs.ObjectId);<br /> FullSubentityPath path = FullSubentityPath.Null;<br /> path = GetSubEntityPath(rs);<br /> if (path != FullSubentityPath.Null)<br /> paths.Add(path);<br /> }<br /> } while (rs.Status == PromptStatus.OK);<br /> _hlOverRule.SetIdFilter(ids.ToArray());<br /> tr.Commit();<br /> }<br /> ed.Regen();<br /> }<br /> private static FullSubentityPath GetSubEntityPath(PromptNestedEntityResult rs)<br /> {<br /> // Extract relevant information from the prompt object<br /> ObjectId selId = rs.ObjectId;<br /> List&lt;ObjectId&gt; objIds = new List&lt;ObjectId&gt;(rs.GetContainers());<br /> // Reverse the "containers" list<br /> objIds.Reverse();<br /> // Now append the selected entity<br /> objIds.Add(selId);<br /> // Retrieve the sub-entity path for this entity<br /> SubentityId subEnt = new SubentityId(SubentityType.Null, System.IntPtr.Zero);<br /> FullSubentityPath path = new FullSubentityPath(objIds.ToArray(), subEnt);<br /> // Open the outermost container, relying on the open transaction...<br /> Entity ent = objIds[0].GetObject(OpenMode.ForRead) as Entity;<br /> // ... and highlight the nested entity<br /> if (ent == null)<br /> return FullSubentityPath.Null;<br /> // Return the sub-entity path <br /> return path;<br /> }<br /> [CommandMethod("ChangeHighlightColor")]<br /> public void ChangeHighlightColor()<br /> {<br /> Document dwg = Autodesk.AutoCAD.ApplicationServices.<br /> Application.DocumentManager.MdiActiveDocument;<br /> Editor ed = dwg.Editor;<br /> PromptIntegerOptions opt = new PromptIntegerOptions(<br /> "\nEnter color index (a number form 0 to 7):");<br /> PromptIntegerResult res = ed.GetInteger(opt);<br /> if (res.Status == PromptStatus.OK)<br /> {<br /> _hlOverRule.ColorIndex = res.Value;<br /> }<br /> }<br /> }</p>
</pre>
<p>Screencast of the code testing is below :&nbsp;</p>
<p><iframe allowfullscreen="" frameborder="0" height="620" src="https://screencast.autodesk.com/Embed/Timeline/afaab734-a69b-4953-9a8f-d56d4f8f1adf" webkitallowfullscreen="" width="640"></iframe>&nbsp;</p>
