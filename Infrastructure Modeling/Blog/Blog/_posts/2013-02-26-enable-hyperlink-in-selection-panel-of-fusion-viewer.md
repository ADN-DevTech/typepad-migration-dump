---
layout: "post"
title: "Enable Hyperlink in Selection panel of Fusion Viewer"
date: "2013-02-26 01:04:29"
author: "Daniel Du"
categories:
  - "AIMS 2012"
  - "AIMS 2013"
  - "Daniel Du"
original_url: "https://adndevblog.typepad.com/infrastructure/2013/02/enable-hyperlink-in-selection-panel-of-fusion-viewer.html "
typepad_basename: "enable-hyperlink-in-selection-panel-of-fusion-viewer"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/daniel-du.html">Daniel Du</a></p>  <p>If you are migrating your application from Ajax Viewer(aka. Basic web layout) to Fusion Viewer(aka. Flexible web layout), you may notice that the hyperlink in selection panel does not work, it displays as raw HTML tags.</p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017d41491e76970c-pi"><img style="background-image: none; border-right-width: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top-width: 0px; border-bottom-width: 0px; border-left-width: 0px; padding-top: 0px" title="image" border="0" alt="image" src="/assets/image_b435d4.jpg" width="444" height="138" /></a></p>  <p>(In Fusion Viewer, not as expected )</p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee8bd0e8e970d-pi"><img style="background-image: none; border-right-width: 0px; margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top-width: 0px; border-bottom-width: 0px; border-left-width: 0px; padding-top: 0px" title="image" border="0" alt="image" src="/assets/image_0a9e04.jpg" width="436" height="107" /></a></p>  <p>(in Ajax Viewer, works well)</p>  <p>Here is the solution, you need to edit SelectionPanel.js in C:\Program Files\Autodesk\Autodesk Infrastructure Web Server Extension 2013\www\fusion\widgets. Search “renderFeature”(Around 384) and change code as below:</p>  <pre style="line-height: normal; font-family: ; background: white; color: "><font face="新宋体"><strong><font color="#000000"><font style="font-size: 7.8pt">htmlDecode:</font></font><font style="font-size: 7.8pt"><span style="color: "><font color="#0000ff">function</font></span></font></strong></font><font style="font-size: 7.8pt"><font face="新宋体"><strong><font color="#000000">(str){
	</font><span style="color: "><font color="#0000ff">var</font></span><font color="#000000"> s = </font><span style="color: "><font color="#800000">&quot;&quot;</font></span></strong></font><font face="新宋体"><strong><font color="#000000">;
	</font><span style="color: "><font color="#0000ff">if</font></span><font color="#000000">(str.length == 0) </font><span style="color: "><font color="#0000ff">return</font></span><font color="#000000">&#160;</font><span style="color: "><font color="#800000">&quot;&quot;</font></span></strong></font><font face="新宋体"><strong><font color="#000000">;
	s =&#160; str.replace(/&amp;gt;/g,&#160;&#160; </font><span style="color: "><font color="#800000">&quot;&gt;&quot;</font></span></strong></font><font face="新宋体"><strong><font color="#000000">);
	s =&#160; s.replace(/&amp;lt;/g,&#160;&#160;&#160;&#160; </font><span style="color: "><font color="#800000">&quot;&lt;&quot;</font></span></strong></font><font face="新宋体"><strong><font color="#000000">);
	s =&#160; s.replace(/&amp;nbsp;/g,&#160;&#160; </font><span style="color: "><font color="#800000">&quot; &quot;</font></span></strong></font><font face="新宋体"><strong><font color="#000000">);
	s =&#160; s.replace(/'/g,&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font><span style="color: "><font color="#800000">&quot;\'&quot;</font></span></strong></font><font face="新宋体"><strong><font color="#000000">);
	s =&#160; s.replace(/&amp;quot;/g,&#160;&#160; </font><span style="color: "><font color="#800000">&quot;\&quot;&quot;</font></span></strong></font><font face="新宋体"><strong><font color="#000000">);
	s = s.replace(/&lt;br&gt;/g,&#160;&#160;&#160;&#160;&#160; </font><span style="color: "><font color="#800000">&quot;\n&quot;</font></span></strong></font><font face="新宋体"><strong><font color="#000000">);
	</font><span style="color: "><font color="#0000ff">return</font></span></strong></font><font face="新宋体"><font color="#000000"><strong> s;
},</strong>

renderFeature: </font><span style="color: "><font color="#0000ff">function</font></span></font><font face="新宋体"><font color="#000000">() {
</font><span style="color: "><font color="#0000ff">var</font></span><font color="#000000"> layerIdx = </font><span style="color: "><font color="#0000ff">this</font></span></font><font face="新宋体"><font color="#000000">.layerList.selectedIndex;
</font><span style="color: "><font color="#0000ff">var</font></span><font color="#000000"> featureIdx = </font><span style="color: "><font color="#0000ff">this</font></span></font><font face="新宋体"><font color="#000000">.featureList.selectedIndex;
</font><span style="color: "><font color="#0000ff">var</font></span><font color="#000000"> layerObj = </font><span style="color: "><font color="#0000ff">this</font></span></font><font face="新宋体"><font color="#000000">.oSelection.getLayer(layerIdx);
</font><span style="color: "><font color="#0000ff">var</font></span></font><font face="新宋体"><font color="#000000"> nProperties = layerObj.getNumProperties();
</font><span style="color: "><font color="#0000ff">var</font></span></font><font face="新宋体"><font color="#000000"> aNames = layerObj.getPropertyNames();

</font><span style="color: "><font color="#0000ff">var</font></span><font color="#000000"> table = document.createElement(</font><span style="color: "><font color="#800000">'table'</font></span></font><font face="新宋体"><font color="#000000">);

</font><span style="color: "><font color="#0000ff">var</font></span><font color="#000000"> thead = document.createElement(</font><span style="color: "><font color="#800000">'thead'</font></span></font><font face="新宋体"><font color="#000000">);
</font><span style="color: "><font color="#0000ff">var</font></span><font color="#000000"> tr = document.createElement(</font><span style="color: "><font color="#800000">'tr'</font></span></font><font face="新宋体"><font color="#000000">);
</font><span style="color: "><font color="#0000ff">var</font></span><font color="#000000"> th = document.createElement(</font><span style="color: "><font color="#800000">'th'</font></span></font><font face="新宋体"><font color="#000000">);
th.innerHTML = OpenLayers.i18n(</font><span style="color: "><font color="#800000">'attribute'</font></span></font><font face="新宋体"><font color="#000000">);
tr.appendChild(th);
</font><span style="color: "><font color="#0000ff">var</font></span><font color="#000000"> th = document.createElement(</font><span style="color: "><font color="#800000">'th'</font></span></font><font face="新宋体"><font color="#000000">);
th.innerHTML = OpenLayers.i18n(</font><span style="color: "><font color="#800000">'value'</font></span></font><font face="新宋体"><font color="#000000">);
tr.appendChild(th);
thead.appendChild(tr);
table.appendChild(thead);

</font><span style="color: "><font color="#0000ff">var</font></span><font color="#000000"> tbody = document.createElement(</font><span style="color: "><font color="#800000">'tbody'</font></span></font><font face="新宋体"><font color="#000000">);
table.appendChild(tbody);
</font><span style="color: "><font color="#0000ff">for</font></span><font color="#000000"> (</font><span style="color: "><font color="#0000ff">var</font></span></font><font face="新宋体"><font color="#000000"> i=0; i&lt;nProperties; i++) {
	</font><span style="color: "><font color="#0000ff">var</font></span><font color="#000000"> tr = document.createElement(</font><span style="color: "><font color="#800000">'tr'</font></span></font><font face="新宋体"><font color="#000000">);
	</font><span style="color: "><font color="#0000ff">if</font></span></font><font face="新宋体"><font color="#000000"> (i%2) {
		tr.className = </font><span style="color: "><font color="#800000">'oddRow'</font></span></font><font face="新宋体"><font color="#000000">;
	}
	</font><span style="color: "><font color="#0000ff">var</font></span><font color="#000000"> th = document.createElement(</font><span style="color: "><font color="#800000">'th'</font></span></font><font face="新宋体"><font color="#000000">);
	th.innerHTML = aNames[i];
	</font><span style="color: "><font color="#0000ff">var</font></span><font color="#000000"> td = document.createElement(</font><span style="color: "><font color="#800000">'td'</font></span></font><font face="新宋体"><font color="#000000">);
	td.innerHTML <strong>= </strong></font><span style="color: "><font color="#0000ff"><strong>this</strong></font></span></font><font face="新宋体"><font color="#000000"><strong>.htmlDecode(layerObj.getElementValue(featureIdx, i));</strong>
			
	tr.appendChild(th);
	tr.appendChild(td);
	tbody.appendChild(tr);
}
</font><span style="color: "><font color="#0000ff">this</font></span><font color="#000000">.featureDiv.innerHTML = </font><span style="color: "><font color="#800000">''</font></span></font></font><font face="新宋体"><font style="font-size: 7.8pt"><font color="#000000">;
</font><span style="color: "><font color="#0000ff">this</font></span><font color="#000000">.featureDiv.appendChild(table);
}</font></font></font><br /></pre>

<p>The idea is to decode the string value so that it can be rendered correctly. Please pay attention to the comma at the end of the the htmlDecode() function. Of cause you may add this utility function somewhere else to make the whole fusion project more organized, I just add it into SelectionPanel.js for simplicity. </p>

<p>Finally, you need to change the Javascriipt reference to the fusion.js in your template index.html, for example, C:\Program Files\Autodesk\Autodesk Infrastructure Web Server Extension 2013\www\fusion\templates\mapguide\slate\index.html if you are using Slate template: </p>

<p>&lt;script type=&quot;text/javascript&quot; src=&quot;../../../lib/<strong>fusion.js</strong>&quot;&gt;&lt;/script&gt;</p>

<p>&#160;</p>

<p>Please refer to <a href="http://adndevblog.typepad.com/infrastructure/2012/04/debugging-fusion-viewer-or-mobile-viewer-of-aims-in-firebug.html">this post</a> for more information about debugging as well. </p>
