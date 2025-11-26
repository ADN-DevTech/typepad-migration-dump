---
layout: "post"
title: "Iterating Over an Unordered Set Property"
date: "2011-02-07 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "Data Access"
  - "Geometry"
  - "VB"
original_url: "https://thebuildingcoder.typepad.com/blog/2011/02/iterating-over-an-unordered-set-property.html "
typepad_basename: "iterating-over-an-unordered-set-property"
typepad_status: "Publish"
---

<p>Here is a simple question that recently came up on iterating over the panel cells in a curtain grid:

<p><strong>Question:</strong> I'm noticing some odd behaviour when dealing with CurtainCell.CurveLoops and CurtainCell.PlanarizedCurveLoops taken from CurtainGrid.Cells.  It appears that one of the loops is missing and another one of the loops is duplicated.

<p>I am iterating over the cells using the following code and displaying a message listing the loop edge coordinates:

<pre class="code">
<span class="blue">Dim</span> cell <span class="blue">As</span> CurtainCell
&nbsp;
<span class="blue">For</span> i <span class="blue">As</span> <span class="blue">Integer</span> = 0 <span class="blue">To</span> cg.Cells.Size - 1
&nbsp; cell = cg.Cells(i)
&nbsp;
&nbsp; msg += vbCrLf + vbCrLf + <span class="maroon">&quot;i=&quot;</span> + i.ToString
&nbsp; msg += vbCrLf + <span class="maroon">&quot;CurveLoops&quot;</span>
&nbsp;
&nbsp; <span class="blue">Dim</span> iCounter <span class="blue">As</span> <span class="blue">Integer</span> = 0
&nbsp;
&nbsp; <span class="blue">For</span> <span class="blue">Each</span> cArr <span class="blue">As</span> CurveArray <span class="blue">In</span> cell.CurveLoops
&nbsp;
&nbsp; &nbsp; <span class="blue">For</span> <span class="blue">Each</span> c <span class="blue">As</span> Curve <span class="blue">In</span> cArr
&nbsp;
&nbsp; &nbsp; &nbsp; iCounter += 1
&nbsp;
&nbsp; &nbsp; &nbsp; msg += vbCrLf + iCounter.ToString _
&nbsp; &nbsp; &nbsp; &nbsp; + <span class="maroon">&quot;. &quot;</span> + Util.CurveToString(c)
&nbsp;
&nbsp; &nbsp; <span class="blue">Next</span>
&nbsp;
&nbsp; <span class="blue">Next</span>
&nbsp;
<span class="blue">Next</span>
&nbsp;
MsgBox(msg)
</pre>

<p>Here are the cells I am examining:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330148c8684d13970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330148c8684d13970c image-full" alt="Curtain grid cells" title="Curtain grid cells" src="/assets/image_098bd4.jpg" border="0" /></a> <br />

</center>

<p>The resulting dialogue box displays the cell curve loop coordinates like this:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330147e25f5136970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330147e25f5136970b" alt="Cell curve loop coordinates" title="Cell curve loop coordinates" src="/assets/image_038bd5.jpg" border="0" /></a> <br />

</center>

<p>As you can see, one loop is missing and the other is duplicated.

<p>Is this normal behaviour?

<p>Also, I noticed that every time I run the query the order in which they are received changes.

<p><strong>Answer:</strong> First, I converted your code to iterate using for each instead:

<pre class="code">
<span class="blue">Dim</span> cg <span class="blue">As</span> CurtainGrid = w.CurtainGrid
&nbsp;
<span class="blue">Dim</span> msg <span class="blue">As</span> <span class="blue">String</span> = <span class="maroon">&quot;# Cells = &quot;</span> _
&nbsp; + cg.Cells.Size.ToString
&nbsp;
<span class="blue">Dim</span> cell <span class="blue">As</span> CurtainCell
<span class="blue">Dim</span> i <span class="blue">As</span> <span class="blue">Integer</span> = 0
&nbsp;
<span class="blue">For</span> <span class="blue">Each</span> cell <span class="blue">In</span> cg.Cells
&nbsp;
&nbsp; msg += vbCrLf + vbCrLf + <span class="maroon">&quot;i=&quot;</span> + i.ToString
&nbsp; msg += vbCrLf + <span class="maroon">&quot;CurveLoops&quot;</span>
&nbsp;
&nbsp; <span class="blue">Dim</span> iCounter <span class="blue">As</span> <span class="blue">Integer</span> = 0
&nbsp;
&nbsp; <span class="blue">For</span> <span class="blue">Each</span> cArr <span class="blue">As</span> CurveArray <span class="blue">In</span> cell.CurveLoops
&nbsp;
&nbsp; &nbsp; <span class="blue">For</span> <span class="blue">Each</span> c <span class="blue">As</span> Curve <span class="blue">In</span> cArr
&nbsp;
&nbsp; &nbsp; &nbsp; iCounter += 1
&nbsp;
&nbsp; &nbsp; &nbsp; msg += vbCrLf + iCounter.ToString _
&nbsp; &nbsp; &nbsp; &nbsp; + <span class="maroon">&quot;. &quot;</span> + Util.CurveToString(c)
&nbsp;
&nbsp; &nbsp; <span class="blue">Next</span>
&nbsp;
&nbsp; <span class="blue">Next</span>
&nbsp;
&nbsp; i += 1
&nbsp;
<span class="blue">Next</span>
&nbsp;
MsgBox(msg)
</pre>

<p>Lo and behold, the problem is resolved, and here are the correct curve loops and their coordinates:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330147e25f5202970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330147e25f5202970b" alt="Cell curve loop coordinates using Foreach" title="Cell curve loop coordinates using Foreach" src="/assets/image_aa8677.jpg" border="0" /></a> <br />

</center>

<p>The problem has nothing to do with whether you use For and indexing or For Each, though.
For Each loops operate in the same manner as a For i = loop.
The indexing is provided by extension methods from Linq.

<p>The problem is due to the fact that each call to CurtainGrid.Cells returns a new set.

<p>The set returned is unordered, so there is no guarantee that one set will have its elements in the same order as another.

<p>In your original code, you were calling cg.Cells(i) and thus requesting a new differently ordered set to be returned on each iteration step of the loop.

<p>You can get a reliable indexed access if you obtain and store one single reference to the set once, rather than accessing it in each iteration within the loop:

<pre class="code">
<span class="blue">Dim</span> cgSet <span class="blue">As</span> CurtainCellSet = cg.Cells

<span class="blue">For</span> i <span class="blue">As</span> <span class="blue">Integer</span> = 0 <span class="blue">To</span> cgSet.Size - 1
&nbsp; <span class="blue">Dim</span> cell <span class="blue">As</span> CurtainCell = cgSet(i)

  ...
</pre>

<p>I have seen several examples in the Revit API where you have to be very aware of the effect of calling a property and storing the result.

<p>In most cases so far, the issue has been calling a property, storing the result in a variable, modifying the value of that variable, and expecting the underlying property to have changed. That is not the case, since the variable just stores a copy of the original value, and the original is not modified by modifying the copy stored in the variable.

<p>In this case, it is the other way round, sort of: each call to the property returns a different result, so we have to store the property value from one single call in a constant variable to ensure that it remains unchanged during the iteration process.
