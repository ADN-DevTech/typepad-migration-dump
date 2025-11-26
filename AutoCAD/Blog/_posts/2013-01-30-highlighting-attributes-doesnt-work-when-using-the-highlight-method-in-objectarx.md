---
layout: "post"
title: "Highlighting Attributes doesn't work when using the highlight() method in ObjectARX"
date: "2013-01-30 15:09:06"
author: "Fenton Webb"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Fenton Webb"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/highlighting-attributes-doesnt-work-when-using-the-highlight-method-in-objectarx.html "
typepad_basename: "highlighting-attributes-doesnt-work-when-using-the-highlight-method-in-objectarx"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>  <p><b>Issue</b></p>  <p>When I call pAttribute-&gt;highlight(); it returns Acad::eOk but the attribute is not highlighted on screen. </p>  <p>I have also tried opening the block reference and highlighting the attribute via:</p>  <p>AcDbFullSubentPath subPath;   <br />subPath.objectIds().append(pAttribute-&gt;id());    <br />blkRef-&gt;highlight(subPath);    <br />this method highlights the block reference and all attributes.</p>  <p>Is there any way to highlight only the attribute?</p>  <p><a name="section2"></a></p>  <p><b>Solution</b></p>  <p>There is a defect which prevents the AcDbAttribute::highlight() from working correctly.</p>  <p>Here is a workaround...</p>  <pre><p>//////////////////////////////////////////////////////////////////////////////<br />// This is command 'TEST, by Fenton Webb [Mar/11/2002], DevTech, Autodesk<br />void asdktest()<br />{<br /> ads_name ename;<br /> ads_point pt;<br /> // pick an entity to check<br /> int res = acedEntSel (_T(&quot;\nPick a block with attributes : &quot;), ename, pt);<br /> // if the user didn't cancel<br /> if (res == RTNORM)<br /> {<br />&#160; AcDbObjectId objId;<br />&#160; // convert the ename to an object id<br />&#160; acdbGetObjectId (objId, ename);<br />&#160; // open the entity for read<br />&#160; AcDbObjectPointer&lt;AcDbBlockReference&gt;blockRef (objId, AcDb::kForRead);<br />&#160; // if ok<br />&#160; if (blockRef.openStatus () == Acad::eOk)<br />&#160; {<br />&#160;&#160; // create an attribute iterator so we can check to see if any attribute asre attached<br />&#160;&#160; AcDbObjectIterator *pAttributeIterator = blockRef-&gt;attributeIterator ();<br />&#160;&#160; // if it allocated ok<br />&#160;&#160; if (pAttributeIterator != NULL)<br />&#160;&#160; {&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <br />&#160;&#160;&#160; AcDbObjectId&#160;&#160; ObjId;<br />&#160;&#160;&#160; AcDbAttribute *pAttribute = NULL;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <br />&#160;&#160;&#160; // now loop through them<br />&#160;&#160;&#160; for (int count=0; !pAttributeIterator-&gt;done(); pAttributeIterator-&gt;step(), ++count)<br />&#160;&#160;&#160; {<br />&#160;&#160;&#160;&#160; // get the object id of the attribute<br />&#160;&#160;&#160;&#160; acdbGetAdsName(ename, pAttributeIterator-&gt;objectId());<br />&#160;&#160;&#160;&#160; <br />&#160;&#160;&#160;&#160; // highlight every other one<br />&#160;&#160;&#160;&#160; if (!(count % 2))<br />&#160;&#160;&#160;&#160; {<br />&#160;&#160;&#160;&#160;&#160; // try and highlight it<br />&#160;&#160;&#160;&#160;&#160; acedRedraw (ename, 3);<br />&#160;&#160;&#160;&#160; }<br />&#160;&#160;&#160; }<br />&#160;&#160;&#160; // delete the iterator<br />&#160;&#160;&#160; delete pAttributeIterator;<br />&#160;&#160; }<br />&#160; }<br />&#160; else<br />&#160; {<br />&#160;&#160; AfxMessageBox(_T(&quot;This is not a block reference&quot;));<br />&#160; }<br /> }<br />}</p></pre>
