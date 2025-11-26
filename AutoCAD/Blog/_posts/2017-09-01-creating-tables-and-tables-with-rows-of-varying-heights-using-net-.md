---
layout: "post"
title: "Creating tables with rows of varying heights using .NET "
date: "2017-09-01 03:47:16"
author: "Deepak A S Nadig"
categories:
  - ".NET"
  - "Deepak A S Nadig"
original_url: "https://adndevblog.typepad.com/autocad/2017/09/creating-tables-and-tables-with-rows-of-varying-heights-using-net-.html "
typepad_basename: "creating-tables-and-tables-with-rows-of-varying-heights-using-net-"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/deepak-nadig.html" target="_self">Deepak Nadig</a></p>
<p>Creating table using the Table.InsertColumns and Table.InsertRows is quite tricky and below are certain scenarios that can be useful :</p>
<p><em><strong>Scenario 1:</strong> </em>Using only<em> table.InsertColumns : </em><br />Along with the specified number of columns, a single default row(without cells) is created at row index 0.</p>
<p><strong><em>Scenario 2:</em></strong> Using only table.InsertRows :<br />Along with the specified number of rows,a single default column is created (with cells) at column index 0.</p>
<p>for example, table.InsertRows(0, 5, 3); creates table as shown below:&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2a62554970c-pi" style="float: left;"><img alt="SingleRowCreation" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2a62554970c img-responsive" height="125" src="/assets/image_477942.jpg" style="margin: 0px 5px 5px 0px;" title="SingleRowCreation" width="87" /></a></p>
<p>&#0160;&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p><em><strong>Scenario 3:</strong> </em>Using index to create rows or columns :<br />Here, the method is table.InsertRows(int row,double height,int rows)<br />int row = row index <br />double height = rows(exculding default row)<br />int rows = number of rows inserted</p>
<p>As in the above example, table.InsertRows(0, 5, 3); creates a table with 4 rows( 3 + 1 default row) and 1 column at index 0.</p>
<p>Since first parameter(index) is 0, every row is inserted at position&#0160;0 and pushes the previously inserted row(if any) below. So we can find the default row at the bottom most position after creation.</p>
<p><em><strong>Scenario 4: &#0160;</strong></em>We can use table.InsertRows in a loop to create rows of varying height :<br />Rows of varying height can be created as follows :&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;</p>
<pre style="color: #d1d1d1; background: #000000;">List<span style="color: #d2cd86;">&lt;</span><span style="color: #e66170; font-weight: bold;">double</span><span style="color: #d2cd86;">&gt;</span> rowHeight <span style="color: #d2cd86;">=</span> <span style="color: #e66170; font-weight: bold;">new</span> List<span style="color: #d2cd86;">&lt;</span><span style="color: #e66170; font-weight: bold;">double</span><span style="color: #d2cd86;">&gt;</span><span style="color: #d2cd86;">(</span><span style="color: #d2cd86;">)</span><span style="color: #b060b0;">;</span>
rowHeight<span style="color: #d2cd86;">.</span>Add<span style="color: #d2cd86;">(</span><span style="color: #008c00;">10</span><span style="color: #d2cd86;">)</span><span style="color: #b060b0;">;</span>
rowHeight<span style="color: #d2cd86;">.</span>Add<span style="color: #d2cd86;">(</span><span style="color: #008c00;">20</span><span style="color: #d2cd86;">)</span><span style="color: #b060b0;">;</span>
rowHeight<span style="color: #d2cd86;">.</span>Add<span style="color: #d2cd86;">(</span><span style="color: #008c00;">30</span><span style="color: #d2cd86;">)</span><span style="color: #b060b0;">;</span>
<span style="color: #e66170; font-weight: bold;">int</span> nRows <span style="color: #d2cd86;">=</span> rowHeight<span style="color: #d2cd86;">.</span>Count<span style="color: #b060b0;">;</span>
<span style="color: #e66170; font-weight: bold;">for</span> <span style="color: #d2cd86;">(</span><span style="color: #e66170; font-weight: bold;">int</span> iRow <span style="color: #d2cd86;">=</span> <span style="color: #008c00;">0</span><span style="color: #b060b0;">;</span> iRow <span style="color: #d2cd86;">&lt;</span> nRows<span style="color: #b060b0;">;</span> iRow<span style="color: #d2cd86;">+</span><span style="color: #d2cd86;">+</span><span style="color: #d2cd86;">)</span>
<span style="color: #b060b0;">{</span>
    table<span style="color: #d2cd86;">.</span>InsertRows<span style="color: #d2cd86;">(</span><span style="color: #008c00;">0</span><span style="color: #d2cd86;">,</span> rowHeight<span style="color: #d2cd86;">[</span>iRow<span style="color: #d2cd86;">]</span><span style="color: #d2cd86;">,</span> <span style="color: #008c00;">1</span><span style="color: #d2cd86;">)</span><span style="color: #b060b0;">;</span>
<span style="color: #b060b0;">}</span>
</pre>
<p>&#0160;Image of the table created for above code :</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2a62733970c-pi" style="float: left;"><img alt="Rows with varying width" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2a62733970c img-responsive" height="154" src="/assets/image_99572.jpg" style="margin: 0px 5px 5px 0px;" title="Rows with varying width" width="77" /></a></p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p><em>NOTE</em> :<span style="font-family: georgia, palatino;"> the default row and column can be deleted if not required using DeleteColumns and DeleteRows API</span></p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c91bb754970b-pi" style="float: left;">&#0160;</a></p>
<div class="csharpcode">&#0160;</div>
