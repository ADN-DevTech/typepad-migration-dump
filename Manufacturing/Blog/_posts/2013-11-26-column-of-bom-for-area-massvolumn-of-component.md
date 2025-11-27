---
layout: "post"
title: "column of BOM for &ldquo;Area&rdquo;of component"
date: "2013-11-26 04:02:00"
author: "Xiaodong Liang"
categories:
  - "iLogic"
  - "Inventor"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/11/column-of-bom-for-area-massvolumn-of-component.html "
typepad_basename: "column-of-bom-for-area-massvolumn-of-component"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>  <p><strong>Question</strong>:    <br />Is there any way to get an area for the entire assembly on the BOM?&#160; If you go to iProperties --&gt; Physical it is shown there. But how to pull it from there and have it as Column in BOM? Can it be done using API ?</p>  <p><strong>Solution</strong>:    <br />Firstly, currently, no API to add/remove column of BOM. You have to add it manually in advance. The following is the steps to achieve your requirement. This also applies to Mass/Volume or other iProperties. </p>  <p>1. In the component file (such as a part), add one custom iProperty manually e.g. named&#160; “Area”. The type is Text.</p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b01ab6925970d-pi"><img style="background-image: none; border-bottom: 0px; border-left: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top: 0px; border-right: 0px; padding-top: 0px" title="image" border="0" alt="image" src="/assets/image_578fd0.jpg" width="546" height="284" /></a></p>  <p>2. add a iLogic rule with the component file. It will get the Area value from iProperty, and update the custom iProperty. </p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b01ab1ed0970b-pi"><img style="background-image: none; border-bottom: 0px; border-left: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top: 0px; border-right: 0px; padding-top: 0px" title="image" border="0" alt="image" src="/assets/image_ad64dd.jpg" width="453" height="204" /></a></p>  <pre><font color="#808080"><em><font size="1"><span style="color: ">'</span><span style="color: ">current document</span></font></em></font><span style="color: ">
</span><font size="1"><span style="color: "><font color="#800000">doc</font></span><span style="color: "> </span><span style="color: "><strong>=</strong></span><span style="color: "> </span><span style="color: "><font color="#800080"><strong>ThisDoc</strong></font></span><span style="color: ">.</span><span style="color: "><font color="#800080"><strong>Document</strong></font></span></font><span style="color: ">
</span><font color="#808080"><em><font size="1"><span style="color: ">'</span><span style="color: ">unit manager of the document</span></font></em></font><span style="color: ">
</span><font size="1"><span style="color: "><font color="#800000">oUOM</font></span><span style="color: "> </span><span style="color: "><strong>=</strong></span><span style="color: "> </span><span style="color: "><font color="#800000">doc</font></span><span style="color: ">.</span><span style="color: "><font color="#800000">UnitsOfMeasure</font></span></font><span style="color: ">
</span><font color="#808080"><em><font size="1"><span style="color: ">'</span><span style="color: ">get the value of Area</span></font></em></font><span style="color: ">
</span><font size="1"><span style="color: "><font color="#800000">surfaceArea</font></span><span style="color: "> </span><span style="color: "><strong>=</strong></span><span style="color: "> </span><span style="color: "><font color="#800080"><strong>iProperties</strong></font></span><span style="color: ">.</span><span style="color: "><font color="#800080"><strong>Area</strong></font></span></font><span style="color: ">
</span><font color="#808080"><em><font size="1"><span style="color: ">'</span><span style="color: ">round the value with 4 valid numbers (optional)</span></font></em></font><span style="color: ">
</span><font size="1"><span style="color: "><font color="#800000">surfaceArea</font></span><span style="color: "> </span><span style="color: "><strong>=</strong></span><span style="color: "> </span><strong><span style="color: "><font color="#800080">Round</font></span><span style="color: ">(</span></strong><span style="color: "><font color="#800000">surfaceArea</font></span><span style="color: ">, </span><strong><span style="color: ">4</span><span style="color: ">)</span></strong></font><font size="1"><span style="color: "> 
</span><font color="#808080"><em><span style="color: ">'</span><span style="color: ">convert the value to a string with the unit of the document</span></em></font></font><span style="color: ">
</span><font size="1"><span style="color: "><font color="#800000">surfaceArea</font></span><span style="color: "> </span><span style="color: "><strong>=</strong></span><span style="color: "> </span><span style="color: "><font color="#800000">surfaceArea</font></span><span style="color: ">.</span><span style="color: "><font color="#800000">ToString</font></span><span style="color: "><strong>()</strong></span><span style="color: "> </span><span style="color: "><font color="#ff0000"><strong>+</strong></font></span><span style="color: "> </span><font color="#008080"><span style="color: ">&quot;</span><span style="color: "> </span><span style="color: ">&quot;</span></font><span style="color: "> </span><span style="color: "><font color="#ff0000"><strong>+</strong></font></span><span style="color: "> </span><span style="color: "><font color="#800000">oUOM</font></span><span style="color: ">.</span><span style="color: "><font color="#800000">GetStringFromType</font></span><span style="color: "><strong>(</strong></span><span style="color: "><font color="#800000">oUOM</font></span><span style="color: ">.</span><span style="color: "><font color="#800000">LengthUnits</font></span><span style="color: "><strong>)</strong></span><span style="color: "> </span><span style="color: "><font color="#ff0000"><strong>+</strong></font></span><span style="color: "> </span><font color="#008080"><span style="color: ">&quot;</span><span style="color: ">^2</span><span style="color: ">&quot;</span></font></font><font size="1"><span style="color: ">   
</span><font color="#808080"><em><span style="color: ">'</span><span style="color: ">update the custom property &quot;Area&quot; we created</span></em></font></font><span style="color: ">
</span><font size="1"><span style="color: "><font color="#800080"><strong>iProperties</strong></font></span><span style="color: ">.</span><strong><span style="color: "><font color="#800080">Value</font></span><span style="color: ">(</span></strong><font color="#008080"><span style="color: ">&quot;</span><span style="color: ">Custom</span><span style="color: ">&quot;</span></font><span style="color: ">, </span><font color="#008080"><span style="color: ">&quot;</span><span style="color: ">Area</span><span style="color: ">&quot;</span></font><span style="color: "><strong>)</strong></span><span style="color: "> </span><span style="color: "><strong>=</strong></span><span style="color: "> </span><span style="color: "><font color="#800000">surfaceArea</font></span></font></pre>





<p>3. add this rule with the iLogic trigger “Part Geometry Change”thus when the model changes, the rule will be executed automatically and update the custom property.</p>

<p>&#160;</p>

<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b01ab1227970b-pi"><img style="background-image: none; border-bottom: 0px; border-left: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top: 0px; border-right: 0px; padding-top: 0px" title="image" border="0" alt="image" src="/assets/image_3a37cf.jpg" width="425" height="311" /></a></p>

<p>&#160;</p>

<p>4. In the assembly &gt;&gt; BOM, add one custom column named “Area”.This column will read the custom property from the component.</p>

<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b01aaadab970c-pi"><img style="background-image: none; border-bottom: 0px; border-left: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top: 0px; border-right: 0px; padding-top: 0px" title="image" border="0" alt="image" src="/assets/image_e1ff54.jpg" width="472" height="242" /></a></p>

<p>&#160;</p>

<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b01aaadb2970c-pi"><img style="background-image: none; border-bottom: 0px; border-left: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top: 0px; border-right: 0px; padding-top: 0px" title="image" border="0" alt="image" src="/assets/image_2e0411.jpg" width="442" height="166" /></a></p>
