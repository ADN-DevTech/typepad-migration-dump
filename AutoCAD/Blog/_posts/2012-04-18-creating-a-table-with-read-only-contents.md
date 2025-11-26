---
layout: "post"
title: "Creating a table with read-only contents"
date: "2012-04-18 02:14:53"
author: "Balaji"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "Balaji Ramamoorthy"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/04/creating-a-table-with-read-only-contents.html "
typepad_basename: "creating-a-table-with-read-only-contents"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>The contents of a table can be made read-only by setting the cell state of the table cells.</p>
<p>Here is a sample code that creates a table with the cell state set to "AcDb::kCellStateContentReadOnly<span style="font-size: x-small;">"</span></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;&nbsp;1</span>&nbsp;<span style="line-height: 140%;">AcGePoint3d insPoint;</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;&nbsp;2</span>&nbsp;<span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> ret = acedGetPoint</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;&nbsp;3</span>&nbsp;<span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; (</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;&nbsp;4</span>&nbsp;<span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; NULL, </span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;&nbsp;5</span>&nbsp;<span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; _T(</span><span style="color: #a31515; line-height: 140%;">&quot;\nEnter insertion point: &quot;</span><span style="line-height: 140%;">),</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;&nbsp;6</span>&nbsp;<span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; asDblArray(insPoint)</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;&nbsp;7</span>&nbsp;<span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; );</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;&nbsp;8</span>&nbsp;<span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;">(ret != RTNORM)</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;&nbsp;9</span>&nbsp;<span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;10</span>&nbsp;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;11</span>&nbsp;<span style="line-height: 140%;">Acad::ErrorStatus es;</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;12</span>&nbsp;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;13</span>&nbsp;<span style="line-height: 140%;">AcDbTable *pTable = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> AcDbTable();</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;14</span>&nbsp;<span style="line-height: 140%;">AcDbDictionary *pDict = NULL;</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;15</span>&nbsp;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;16</span>&nbsp;<span style="line-height: 140%;">AcDbDatabase *pDb </span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;17</span>&nbsp;<span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; = acdbHostApplicationServices()-&gt;workingDatabase();</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;18</span>&nbsp;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;19</span>&nbsp;<span style="line-height: 140%;">es = pDb-&gt;getTableStyleDictionary(pDict,AcDb::kForRead);</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;20</span>&nbsp;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;21</span>&nbsp;<span style="line-height: 140%;">AcDbObjectId styleId;</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;22</span>&nbsp;<span style="line-height: 140%;">es = pDict-&gt;getAt(_T(</span><span style="color: #a31515; line-height: 140%;">&quot;Standard&quot;</span><span style="line-height: 140%;">), styleId);</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;23</span>&nbsp;<span style="line-height: 140%;">es = pDict-&gt;close();</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;24</span>&nbsp;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;25</span>&nbsp;<span style="line-height: 140%;">pTable-&gt;setTableStyle(styleId);</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;26</span>&nbsp;<span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> rows = 3;</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;27</span>&nbsp;<span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> cols = 2;</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;28</span>&nbsp;<span style="line-height: 140%;">pTable-&gt;setSize(rows, cols);</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;29</span>&nbsp;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;30</span>&nbsp;<span style="line-height: 140%;">ACHAR content[10];</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;31</span>&nbsp;<span style="color: blue; line-height: 140%;">for</span><span style="line-height: 140%;">(</span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> row = 0; row &lt; rows; row++)</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;32</span>&nbsp;<span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;33</span>&nbsp;<span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">for</span><span style="line-height: 140%;">(</span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> col = 0; col &lt; cols; col++)</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;34</span>&nbsp;<span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;35</span>&nbsp;<span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; acutSPrintf(content, ACRX_T(</span><span style="color: #a31515; line-height: 140%;">&quot;%d-%d&quot;</span><span style="line-height: 140%;">), row+1, col+1); </span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;36</span>&nbsp;<span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; es = pTable-&gt;setTextString(row, col, content); </span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;37</span>&nbsp;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;38</span>&nbsp;<span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">// Set the cell state to read-only</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;39</span>&nbsp;<span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; es = pTable-&gt;setCellState</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;40</span>&nbsp;<span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; (</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;41</span>&nbsp;<span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; row, </span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;42</span>&nbsp;<span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; col, </span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;43</span>&nbsp;<span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; AcDb::kCellStateContentReadOnly</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;44</span>&nbsp;<span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; ); </span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;45</span>&nbsp;<span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;46</span>&nbsp;<span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;47</span>&nbsp;<span style="line-height: 140%;">pTable-&gt;generateLayout();&nbsp; </span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;48</span>&nbsp;<span style="line-height: 140%;">pTable-&gt;setPosition(insPoint);</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;49</span>&nbsp;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;50</span>&nbsp;<span style="line-height: 140%;">AcDbBlockTable *pBlockTable;</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;51</span>&nbsp;<span style="line-height: 140%;">pDb-&gt;getSymbolTable(pBlockTable, AcDb::kForRead);</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;52</span>&nbsp;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;53</span>&nbsp;<span style="line-height: 140%;">AcDbBlockTableRecord *pBlockTableRecord;</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;54</span>&nbsp;<span style="line-height: 140%;">pBlockTable-&gt;getAt</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;55</span>&nbsp;<span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; (</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;56</span>&nbsp;<span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; ACDB_MODEL_SPACE, </span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;57</span>&nbsp;<span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; pBlockTableRecord, </span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;58</span>&nbsp;<span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; AcDb::kForWrite</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;59</span>&nbsp;<span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; );</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;60</span>&nbsp;<span style="line-height: 140%;">pBlockTable-&gt;close();</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;61</span>&nbsp;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;62</span>&nbsp;<span style="line-height: 140%;">pBlockTableRecord-&gt;appendAcDbEntity(pTable);</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;63</span>&nbsp;<span style="line-height: 140%;">pBlockTableRecord-&gt;close();</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;64</span>&nbsp;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">&nbsp;&nbsp;&nbsp;65</span>&nbsp;<span style="line-height: 140%;">pTable-&gt;close();</span></p>
</div>
