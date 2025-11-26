---
layout: "post"
title: "set layer of sub entities of custom object"
date: "2018-01-08 01:14:45"
author: "Xiaodong Liang"
categories:
  - "2017"
  - "AutoCAD"
  - "ObjectARX"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/autocad/2018/01/set-layer-of-sub-entities-of-custom-object.html "
typepad_basename: "set-layer-of-sub-entities-of-custom-object"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p><a class="twitter-follow-button" data-show-count="false" href="https://twitter.com/coldwood" rel="noopener noreferrer" target="_blank">Follow @Xiaodong Liang</a></p>
<p>Some customers wanted to set sub entities of custom object to the specific layers. When the corresponding layers changed ( such as visibility, color etc), the entities will update accordingly.&#0160;</p>
<p>The solution is very straightforward.&#0160;<span class="s1">worldDraw</span>-&gt;subEntityTraits().setLayer allows us to specify the specific primitives of the custom object to the specific layer.&#0160;This method sets the AcGiSubEntityTraits object to use layerId to specify the layer on which to draw graphics primitives until the next call to this function, or the end of the worldDraw() or viewportDraw() execution.</p>
<p>I did an experiment based on SDK sample PolySample. In asdkpolyobj project &gt;&gt;&#0160;poly.cpp&#0160; &gt;&gt; drawEdges method, add some lines as below.&#0160;&#0160;</p>
<p>&#0160;</p>
<p>
<script src="https://platform.twitter.com/widgets.js"></script>
</p>
<pre><code>
static Acad::ErrorStatus drawEdges(const AsdkPoly*         poly,
                                         AcGiWorldDraw*    worldDraw,
                                         AcGiViewportDraw* vportDraw)
{
    Acad::ErrorStatus es = Acad::eOk;

    // Draw each edge of the polygon as a line. We could have drawn
    // the whole polygon as a polyline, but we want to attach subEntity
    // traits (e.g. which line it is) to each line which will be used
    // for snap.
    //
    // Since we are drawing the polygon line by line, we also have the
    // control over setting the linetype and color of each line (via
    // subEntity traits), but we won&#39;t get that fancy.

	//get the specific layers ID
	AcDbObjectId testLayerId = AcDbObjectId::kNull; 
	AcDbObjectId zeroLayerId = AcDbObjectId::kNull;

	AcDbLayerTable* lTable = NULL;  
	AcDbDatabase *pDb = acdbHostApplicationServices()-&gt;workingDatabase(); 
	Acad::ErrorStatus layer_table_es = pDb-&gt;getSymbolTable(lTable, AcDb::kForRead); 

	if (Acad::eOk == layer_table_es &amp;&amp; lTable)
	{   
		if (lTable-&gt;getAt(_T(&quot;MyTestLayer&quot;), testLayerId) != Acad::eOk) 
			::acutPrintf(_T(&quot;ERROR Getting MyTestLayer\n&quot;));  
		if (lTable-&gt;getAt(_T(&quot;0&quot;), zeroLayerId) != Acad::eOk)
			::acutPrintf(_T(&quot;ERROR Getting 0 Layer\n&quot;));

		lTable-&gt;close(); 
	}

    AcGePoint3dArray vertexArray;
    if ((es = poly-&gt;getVertices3d(vertexArray)) != Acad::eOk) {
        return es;
    }

    AcGePoint3d ptArray[2];

    for (int i = 0; i &lt; vertexArray.length() - 1; i++) {

        if (worldDraw != NULL) {

			 if(i%3==0 ){
				 //if a specific edge. set it to the layer MyTestLayer
				 if(!testLayerId.isNull())
					worldDraw-&gt;subEntityTraits().setLayer(testLayerId); 
				
			 }
			 else {
				 //other edges. set them to the layer 0
				 if (!zeroLayerId.isNull())
					 worldDraw-&gt;subEntityTraits().setLayer(zeroLayerId);
			 }
             worldDraw-&gt;subEntityTraits().setSelectionMarker(i + 1);
        } else {
            assert(Adesk::kFalse);
            //vportDraw-&gt;subEntityTraits().setSelectionMarker(i + 1);
        }

        ptArray[0] = vertexArray[i];
        ptArray[1] = vertexArray[i + 1];

        if (worldDraw != NULL) {
            worldDraw-&gt;geometry().polyline(2, ptArray);
        } else {
            assert(Adesk::kFalse);
            //vportDraw-&gt;geometry().polyline3d(2, ptArray);
        }
    }

    return es;
}
</code></pre>
<p>The snapshots are two tests. One toggle MyTestLayer off. The other change the layer&#39;s color.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2cdae5d970c-pi" style="float: left;"><img alt="Screen Shot 2018-01-08 at 5.12.50 PM" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2cdae5d970c img-responsive" src="/assets/image_623305.jpg" style="margin: 0px 5px 5px 0px;" title="Screen Shot 2018-01-08 at 5.12.50 PM" /></a></p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09e6a4e8970d-pi" style="float: left;"><img alt="Screen Shot 2018-01-08 at 5.08.59 PM" class="asset  asset-image at-xid-6a0167607c2431970b01bb09e6a4e8970d img-responsive" src="/assets/image_672757.jpg" style="margin: 0px 5px 5px 0px;" title="Screen Shot 2018-01-08 at 5.08.59 PM" /></a></p>
