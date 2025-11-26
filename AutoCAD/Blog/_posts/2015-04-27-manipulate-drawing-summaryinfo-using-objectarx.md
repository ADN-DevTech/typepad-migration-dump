---
layout: "post"
title: "Manipulate drawing SummaryInfo using ObjectARX"
date: "2015-04-27 22:59:09"
author: "Balaji"
categories:
  - ".NET"
  - "2013"
  - "2014"
  - "2015"
  - "2016"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2015/04/manipulate-drawing-summaryinfo-using-objectarx.html "
typepad_basename: "manipulate-drawing-summaryinfo-using-objectarx"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>You may want to update the "Last saved by" and "Revision number" properties and other custom properties associated with the drawing. The AcDbDatabaseSummaryInfo class of the ObjectARX SDK will help do that. The equivalent of this class in the AutoCAD .Net API is the "DatabaseSummaryInfo" structure. But unlike the C++ API, some of the properties such as "LastSavedBy" and "RevisionNumber" are read-only in .Net. You can also use the COM API to retrieve and set the properties. This is especially useful if you are driving AutoCAD from an external application or using VBA.</p>
<p>After the "Last saved by" property is changed, it is important to save the database using a different name. If not, the AutoCAD's save command will automatically use the system login name and set the "Last saved by" property.</p>
<p>Here is the ObjectARX C++ code snippet to set the "Last saved by" and "Revision number" properties while retrieving the other properties.&nbsp;</p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color: black; overflow: auto; width: 99.5%;">
<pre style="margin: 0em;"> Acad::ErrorStatus es;    </pre>
<pre style="margin: 0em;"> AcDbDatabaseSummaryInfo *pInfo;    </pre>
<pre style="margin: 0em;"> AcDbDatabase *pCurDb = NULL;    </pre>
<pre style="margin: 0em;"> ACHAR* info;    </pre>
<pre style="margin: 0em;"> ACHAR* key;    </pre>
<pre style="margin: 0em;"> ACHAR* value; </pre>
<pre style="margin: 0em;"> <span style="color: #0000ff;">int</span><span style="color: #000000;">  customQty; </span></pre>
<pre style="margin: 0em;"> <span style="color: #0000ff;">int</span><span style="color: #000000;">  index;  </span></pre>
<pre style="margin: 0em;"> pCurDb = acdbHostApplicationServices()-&gt;workingDatabase(); </pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> <span style="color: #008000;">// Get a pointer to the workingDatabase() </span></pre>
<pre style="margin: 0em;"> <span style="color: #008000;">// summary information  </span></pre>
<pre style="margin: 0em;"> es = acdbGetSummaryInfo(pCurDb, pInfo);   </pre>
<pre style="margin: 0em;"> acutPrintf(L<span style="color: #a31515;">"\\nSummary information for this drawing:"</span><span style="color: #000000;"> ); </span></pre>
<pre style="margin: 0em;"> es = pInfo-&gt;getTitle(info);  </pre>
<pre style="margin: 0em;"> <span style="color: #0000ff;">if</span><span style="color: #000000;"> (info)</span></pre>
<pre style="margin: 0em;"> <span style="color: #000000;">{</span>   </pre>
<pre style="margin: 0em;"> 	acutPrintf(L<span style="color: #a31515;">"\\nTitle = %s"</span><span style="color: #000000;"> , info);   </span></pre>
<pre style="margin: 0em;"> <span style="color: #000000;">}</span>    </pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> es = pInfo-&gt;getSubject(info);    </pre>
<pre style="margin: 0em;"> <span style="color: #0000ff;">if</span><span style="color: #000000;"> (info)    </span></pre>
<pre style="margin: 0em;"> <span style="color: #000000;">{</span> </pre>
<pre style="margin: 0em;"> 	acutPrintf(L<span style="color: #a31515;">"\\nSubject matter = %s"</span><span style="color: #000000;"> , info);   </span></pre>
<pre style="margin: 0em;"> <span style="color: #000000;">}</span>    </pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> es = pInfo-&gt;getAuthor(info);    </pre>
<pre style="margin: 0em;"> <span style="color: #0000ff;">if</span><span style="color: #000000;"> (info)</span></pre>
<pre style="margin: 0em;"> <span style="color: #000000;">{</span>        </pre>
<pre style="margin: 0em;"> 	acutPrintf(L<span style="color: #a31515;">"\\nAutor = %s"</span><span style="color: #000000;"> , info);    </span></pre>
<pre style="margin: 0em;"> <span style="color: #000000;">}</span>    </pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> es = pInfo-&gt;getKeywords(info);    </pre>
<pre style="margin: 0em;"> <span style="color: #0000ff;">if</span><span style="color: #000000;"> (info)   </span></pre>
<pre style="margin: 0em;"> <span style="color: #000000;">{</span>    </pre>
<pre style="margin: 0em;"> 	acutPrintf(L<span style="color: #a31515;">"\\nKeywords = %s"</span><span style="color: #000000;"> , info);   </span></pre>
<pre style="margin: 0em;"> <span style="color: #000000;">}</span>   </pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> es = pInfo-&gt;getComments(info);   </pre>
<pre style="margin: 0em;"> <span style="color: #0000ff;">if</span><span style="color: #000000;"> (info)   </span></pre>
<pre style="margin: 0em;"> <span style="color: #000000;">{</span>       </pre>
<pre style="margin: 0em;"> 	acutPrintf(L<span style="color: #a31515;">"\\nComments = %s"</span><span style="color: #000000;"> , info);    </span></pre>
<pre style="margin: 0em;"> <span style="color: #000000;">}</span>    </pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> es = pInfo-&gt;setLastSavedBy(L<span style="color: #a31515;">"Captain CAD"</span><span style="color: #000000;"> );    </span></pre>
<pre style="margin: 0em;"> es = pInfo-&gt;getLastSavedBy(info); </pre>
<pre style="margin: 0em;"> acutPrintf(L<span style="color: #a31515;">"\\nLast saved by = %s"</span><span style="color: #000000;"> , info);   </span></pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> es = pInfo-&gt;getHyperlinkBase(info);  </pre>
<pre style="margin: 0em;"> <span style="color: #0000ff;">if</span><span style="color: #000000;"> (info)   </span></pre>
<pre style="margin: 0em;"> <span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> acutPrintf(L<span style="color: #a31515;">"\\nLink Location = %s"</span><span style="color: #000000;"> , info);    </span></pre>
<pre style="margin: 0em;"> <span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> es = pInfo-&gt;setRevisionNumber(L<span style="color: #a31515;">"1"</span><span style="color: #000000;"> ); </span></pre>
<pre style="margin: 0em;"> es = pInfo-&gt;getRevisionNumber(info);    </pre>
<pre style="margin: 0em;"> acutPrintf(L<span style="color: #a31515;">"\\nRevision number = %s"</span><span style="color: #000000;"> , info);    </span></pre>
<pre style="margin: 0em;"> customQty = pInfo-&gt;numCustomInfo();    </pre>
<pre style="margin: 0em;"> <span style="color: #0000ff;">if</span><span style="color: #000000;"> (customQty &gt; 0)    </span></pre>
<pre style="margin: 0em;"> <span style="color: #000000;">{</span>        </pre>
<pre style="margin: 0em;"> 	acutPrintf(L<span style="color: #a31515;">"\\nCustom Summary Information:\\n"</span><span style="color: #000000;"> );        </span></pre>
<pre style="margin: 0em;"> 	acutPrintf(L<span style="color: #a31515;">"\\nKey\\t\\tValue\\n"</span><span style="color: #000000;"> );        </span></pre>
<pre style="margin: 0em;"> 	<span style="color: #0000ff;">for</span><span style="color: #000000;"> (index = 0; index &lt; customQty; index++)        </span></pre>
<pre style="margin: 0em;"> 	<span style="color: #000000;">{</span>            </pre>
<pre style="margin: 0em;"> 		pInfo-&gt;getCustomSummaryInfo(index, key, value);            </pre>
<pre style="margin: 0em;"> 		<span style="color: #0000ff;">if</span><span style="color: #000000;"> (key)            </span></pre>
<pre style="margin: 0em;"> 		<span style="color: #000000;">{</span>               </pre>
<pre style="margin: 0em;"> 			acutPrintf(L<span style="color: #a31515;">"\\n%s"</span><span style="color: #000000;"> , key);            </span></pre>
<pre style="margin: 0em;"> 		<span style="color: #000000;">}</span>            </pre>
<pre style="margin: 0em;"> 		<span style="color: #0000ff;">if</span><span style="color: #000000;"> (value)          </span></pre>
<pre style="margin: 0em;"> 		<span style="color: #000000;">{</span> </pre>
<pre style="margin: 0em;"> 			acutPrintf(L<span style="color: #a31515;">"\\t\\t%s"</span><span style="color: #000000;"> , value);</span></pre>
<pre style="margin: 0em;"> 		<span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;"> 		acdbFree(key); </pre>
<pre style="margin: 0em;"> 		acdbFree(value);</pre>
<pre style="margin: 0em;"> 	<span style="color: #000000;">}</span>    </pre>
<pre style="margin: 0em;"> <span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;"> <span style="color: #0000ff;">else</span></pre>
<pre style="margin: 0em;"> <span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> 	acutPrintf(L<span style="color: #a31515;">"\\n\\nDrawing does not contain</span></pre>
<pre style="margin: 0em;"> 		any Custom SummaryInformation<span style="color: #a31515;">");   </span></pre>
<pre style="margin: 0em;"> <span style="color: #000000;">}</span> </pre>
<pre style="margin: 0em;"> es = acdbPutSummaryInfo(pInfo);    </pre>
<pre style="margin: 0em;"> acdbFree(info); </pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> pCurDb-&gt;saveAs(ACRX_T(<span style="color: #a31515;">"D:\\\\Temp\\\\MyTestArx.dwg"</span><span style="color: #000000;"> ));</span></pre>
<pre style="margin: 0em;">&nbsp;</pre>
</div>
<!-- End block -->
<p>To set the properties using COM API, here is a code snippet :</p>
<p></p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> <span&#39;oAcadApp As IAcadApplication ...</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> oAcadApp.ActiveDocument.Database.SummaryInfo.LastSavedBy = <span style="color:#a31515">&quot;Autodesk&quot;</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> MsgBox(oAcadApp.ActiveDocument.Database.SummaryInfo.LastSavedBy)</pre>
<pre style="margin:0em;"> oAcadApp.ActiveDocument.SaveAs(<span style="color:#a31515">&quot;D:\\Temp\\MyTest.dwg&quot;</span><span style="color:#000000"> )</pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
