---
layout: "post"
title: "Drag Drop into AutoCAD from an external application"
date: "2012-11-18 03:03:28"
author: "Balaji"
categories:
  - ".NET"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2012/11/drag-drop-into-autocad-from-an-external-application.html "
typepad_basename: "drag-drop-into-autocad-from-an-external-application"
typepad_status: "Publish"
---

<p>If you have a WinForm application with a ListBox displaying the drawing file paths and you wish to perform a drag and drop of those ListBox items into AutoCAD, then here is a code snippet to ensure that AutoCAD takes appropriate action on those dropped items.</p>
<p>Step 1)</p>
<p>Handle the MouseMove event of the ListBox. As an example, if the ListBox is named "fileNamesListBox" then you would add the event handler as :</p>
<p></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">this</span><span style="line-height: 140%;">.fileNamesListBox.MouseMove </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; += </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> System.Windows.Forms.</span><span style="color: #2b91af; line-height: 140%;">MouseEventHandler</span><span style="line-height: 140%;">(</span><span style="color: blue; line-height: 140%;">this</span><span style="line-height: 140%;">.fileNamesListBox_MouseMove);</span></p>
</div>
<p></p>
<p>Step 2)</p>
<p>Inside the MouseMove event handler of the ListBox, call the DoDragDrop with the appropriate parameters. In this case the list of drawing file paths that were selected in the ListBox.</p>
<p></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">private</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> fileNamesListBox_MouseMove(</span><span style="color: blue; line-height: 140%;">object</span><span style="line-height: 140%;"> sender, </span><span style="color: #2b91af; line-height: 140%;">MouseEventArgs</span><span style="line-height: 140%;"> e)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (System.Windows.Forms.</span><span style="color: #2b91af; line-height: 140%;">Control</span><span style="line-height: 140%;">.MouseButtons </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; == System.Windows.Forms.</span><span style="color: #2b91af; line-height: 140%;">MouseButtons</span><span style="line-height: 140%;">.Left)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; System.Windows.Forms.</span><span style="color: #2b91af; line-height: 140%;">DataObject</span><span style="line-height: 140%;"> myDataObject </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> System.Windows.Forms.</span><span style="color: #2b91af; line-height: 140%;">DataObject</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; System.Collections.Specialized.</span><span style="color: #2b91af; line-height: 140%;">StringCollection</span><span style="line-height: 140%;"> dwgFileNames </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> System.Collections.Specialized.</span><span style="color: #2b91af; line-height: 140%;">StringCollection</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">foreach</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">Object</span><span style="line-height: 140%;"> so </span><span style="color: blue; line-height: 140%;">in</span><span style="line-height: 140%;"> fileNamesListBox.SelectedItems)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; dwgFileNames.Add(so.ToString());</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (dwgFileNames.Count &gt; 0)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; myDataObject.SetFileDropList(dwgFileNames);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; System.Windows.Forms.</span><span style="color: #2b91af; line-height: 140%;">DragDropEffects</span><span style="line-height: 140%;"> dropEffect</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; = fileNamesListBox.DoDragDrop</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; (</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; myDataObject, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; System.Windows.Forms.</span><span style="color: #2b91af; line-height: 140%;">DragDropEffects</span><span style="line-height: 140%;">.Copy</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; } </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p></p>
If you now select multiple items (drawing file paths) from the ListBox and drop it inside the AutoCAD command window, the drawings from those file paths should get opened in the AutoCAD Editor.
