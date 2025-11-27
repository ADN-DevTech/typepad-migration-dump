---
layout: "post"
title: "Getting AutoCAD block attributes from a .NET application"
date: "2006-09-15 22:14:51"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Blocks"
original_url: "https://www.keanw.com/2006/09/getting_autocad.html "
typepad_basename: "getting_autocad"
typepad_status: "Publish"
---

<p>It's been a hectic week, between one thing and another, and I don’t expect it to get better anytime soon: apart from anything else, my wife is due to give birth any day to our second child. We're both excited - and admittedly a little anxious - about it, and our 2 year-old seems to be feeding of that, which means none of us have ended getting much sleep of late. All good preparation, I suppose. :-)</p>

<p>So I decided the path of least resistance for getting a blog entry out today was to look through some of the responses DevTech have sent out to ADN members recently, to find something that might be of interest to a wider audience. A lot of what we do ends up being very specific to individual situations, but we also get requests for code samples that prove to be of general interest (these latter topics are generally turned into DevNotes (technical solutions) and get posted to the ADN website). Over the busy months ahead I'm sure I'll end up feeding many of these through this blog.</p>

<p>One response that caught my eye was this helpful little code sample, written by Varadan (a.k.a. Krishnan Varadarajan), a member of the DevTech team in India.</p>

<p>It asks the user to select block references, and then goes through them, looking for any contained attributes, which it then dumps to the AutoCAD console:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.Runtime;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.ApplicationServices;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.DatabaseServices;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.EditorInput;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">namespace</span> MyApplication</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">class</span> <span style="COLOR: teal">DumpAttributes</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; [<span style="COLOR: teal">CommandMethod</span>(<span style="COLOR: maroon">&quot;LISTATT&quot;</span>)]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> ListAttributes()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Editor</span> ed =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">Application</span>.DocumentManager.MdiActiveDocument.Editor;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Database</span> db =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">HostApplicationServices</span>.WorkingDatabase;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Transaction</span> tr =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; db.TransactionManager.StartTransaction();</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Start the transaction</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">try</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Build a filter list so that only</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// block references are selected</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">TypedValue</span>[] filList = <span style="COLOR: blue">new</span> <span style="COLOR: teal">TypedValue</span>[1] {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">new</span> <span style="COLOR: teal">TypedValue</span>((<span style="COLOR: blue">int</span>)<span style="COLOR: teal">DxfCode</span>.Start, <span style="COLOR: maroon">&quot;INSERT&quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; };</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">SelectionFilter</span> filter =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">new</span> <span style="COLOR: teal">SelectionFilter</span>(filList);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">PromptSelectionOptions</span> opts =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">new</span> <span style="COLOR: teal">PromptSelectionOptions</span>();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; opts.MessageForAdding = <span style="COLOR: maroon">&quot;Select block references: &quot;</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">PromptSelectionResult</span> res =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ed.GetSelection(opts, filter);</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Do nothing if selection is unsuccessful</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">if</span> (res.Status != <span style="COLOR: teal">PromptStatus</span>.OK)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">return</span>;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">SelectionSet</span> selSet = res.Value;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">ObjectId</span>[] idArray = selSet.GetObjectIds();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">foreach</span> (<span style="COLOR: teal">ObjectId</span> blkId <span style="COLOR: blue">in</span> idArray)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">BlockReference</span> blkRef =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;(<span style="COLOR: teal">BlockReference</span>)tr.GetObject(blkId,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">OpenMode</span>.ForRead);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">BlockTableRecord</span> btr =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;(<span style="COLOR: teal">BlockTableRecord</span>)tr.GetObject(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; blkRef.BlockTableRecord,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">OpenMode</span>.ForRead</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ed.WriteMessage(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: maroon">&quot;\nBlock: &quot;</span> + btr.Name</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; btr.Dispose();</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">AttributeCollection</span> attCol =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;blkRef.AttributeCollection;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">foreach</span> (<span style="COLOR: teal">ObjectId</span> attId <span style="COLOR: blue">in</span> attCol)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">AttributeReference</span> attRef =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; (<span style="COLOR: teal">AttributeReference</span>)tr.GetObject(attId,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">OpenMode</span>.ForRead);</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">string</span> str =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; (<span style="COLOR: maroon">&quot;\n&nbsp; Attribute Tag: &quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; + attRef.Tag</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; + <span style="COLOR: maroon">&quot;\n&nbsp; &nbsp; Attribute String: &quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; + attRef.TextString</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;ed.WriteMessage(str);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; tr.Commit();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">catch</span> (Autodesk.AutoCAD.Runtime.<span style="COLOR: teal">Exception</span> ex)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.WriteMessage((<span style="COLOR: maroon">&quot;Exception: &quot;</span> + ex.Message));</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">finally</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; tr.Dispose();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">}</p></div></div>

<p>Here's what happened when I ran it against the &quot;Blocks and Tables - Imperial.dwg&quot; sample drawing that ships with AutoCAD. On running the LISTATT command and selecting the title block on the right of the page, here's what was dumped out:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">Command: <span style="COLOR: red">LISTATT</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">Select block references: 1 found</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">Select block references:</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">Block: ARCHBDR-D</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; Attribute Tag: NAME</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; Attribute String: CORY B.</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; Attribute Tag: NAME</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; Attribute String: BOB M.</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; Attribute Tag: DATE</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; Attribute String:</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; Attribute Tag: X&quot;=X'-X&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; Attribute String: 1/4&quot; =1'</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; Attribute Tag: 0</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; Attribute String: 1</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; Attribute Tag: 0</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; Attribute String: 1</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; Attribute Tag: PROJECT</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; Attribute String: ADDA</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; Attribute Tag: TITLE</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; Attribute String: FLOOR PLANS</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; Attribute Tag: X</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; Attribute String:</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; Attribute Tag: X</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; Attribute String:</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; Attribute Tag: X</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; Attribute String:</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; Attribute Tag: X/XX/XX</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; Attribute String:</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; Attribute Tag: X/XX/XX</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; Attribute String:</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; Attribute Tag: X/XX/XX</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; Attribute String:</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; Attribute Tag: COMMENT</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; Attribute String:</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; Attribute Tag: COMMENT</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; Attribute String:</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; Attribute Tag: COMMENT</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; Attribute String:</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Command:</p></div>
