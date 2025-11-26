---
layout: "post"
title: "Changing Workset ID"
date: "2013-01-30 23:03:28"
author: "Mikako Harada"
categories:
  - ".NET"
  - "Mikako Harada"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2013/01/changing-workset-id.html "
typepad_basename: "changing-workset-id"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/mikako-harada.html" target="_self" title="Mikako Harada">Mikako Harada</a> </p>
<p>Here is one question that we received from a Japanese customer: </p>
<p><strong>Issue</strong></p>
<p>I want to change an workset ID of an element. Is it possible?&#0160;&#0160;I see Element.WorkserId property is ready-only.&#0160; </p>
<p><strong>Solusion</strong></p>
<p>You can change the element’s workset via <br />element.Parameter[BuilitInParameter. ELEM_PARTITION_PARAM].</p>
<p>Here is the code snippet that demonstrates how to get all workset ids and set a new id to a given element.</p>
<div style="font-family: Consolas; font-size: 10pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: #2b91af;">&#0160;&#0160;&#0160;&#0160;1</span>&#0160;&#0160; Reference reference = </p>
<p style="margin: 0px;"><span style="color: #2b91af;">&#0160;&#0160;&#0160;&#0160;2</span>&#0160;&#0160; &#0160; <span style="color: blue;">this</span>.Selection.PickObject(ObjectType.Element);</p>
<p style="margin: 0px;"><span style="color: #2b91af;">&#0160;&#0160;&#0160;&#0160;3</span>&#0160;&#0160; Element elem = <span style="color: blue;">this</span>.Document.GetElement(reference.ElementId);</p>
<p style="margin: 0px;"><span style="color: #2b91af;">&#0160;&#0160;&#0160;&#0160;4</span>&#0160;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">&#0160;&#0160;&#0160;&#0160;5</span>&#0160;&#0160; <span style="color: blue;">if</span>(elem == <span style="color: blue;">null</span>)</p>
<p style="margin: 0px;"><span style="color: #2b91af;">&#0160;&#0160;&#0160;&#0160;6</span>&#0160;&#0160; &#0160; &#0160; <span style="color: blue;">return</span>;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">&#0160;&#0160;&#0160;&#0160;7</span>&#0160;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">&#0160;&#0160;&#0160;&#0160;8</span>&#0160;&#0160; WorksetId wid = elem.WorksetId;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">&#0160;&#0160;&#0160;&#0160;9</span>&#0160;&#0160; TaskDialog.Show(<span style="color: #a31515;">&quot;worksetid&quot;</span>, wid.ToString());</p>
<p style="margin: 0px;"><span style="color: #2b91af;">&#0160;&#0160;&#0160;10</span>&#0160;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">&#0160;&#0160;&#0160;11</span>&#0160;&#0160; <span style="color: green;">// you can access workset parameter via </span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">&#0160;&#0160;&#0160;12</span>&#0160;&#0160; <span style="color: green;">// ELEM_PARTITION_PARAM </span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">&#0160;&#0160;&#0160;13</span>&#0160;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">&#0160;&#0160;&#0160;14</span>&#0160;&#0160; Parameter wsparam = </p>
<p style="margin: 0px;"><span style="color: #2b91af;">&#0160;&#0160;&#0160;15</span>&#0160;&#0160; &#0160; elem.get_Parameter(BuiltInParameter.ELEM_PARTITION_PARAM);</p>
<p style="margin: 0px;"><span style="color: #2b91af;">&#0160;&#0160;&#0160;16</span>&#0160;&#0160; <span style="color: blue;">if</span>(wsparam == <span style="color: blue;">null</span>)</p>
<p style="margin: 0px;"><span style="color: #2b91af;">&#0160;&#0160;&#0160;17</span>&#0160;&#0160; &#0160; &#0160; <span style="color: blue;">return</span>;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">&#0160;&#0160;&#0160;18</span>&#0160;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">&#0160;&#0160;&#0160;19</span>&#0160;&#0160; FilteredWorksetCollector collector = </p>
<p style="margin: 0px;"><span style="color: #2b91af;">&#0160;&#0160;&#0160;20</span>&#0160;&#0160; &#0160; <span style="color: blue;">new</span> FilteredWorksetCollector(<span style="color: blue;">this</span>.Document); </p>
<p style="margin: 0px;"><span style="color: #2b91af;">&#0160;&#0160;&#0160;21</span>&#0160;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">&#0160;&#0160;&#0160;22</span>&#0160;&#0160; <span style="color: green;">// find all user worksets </span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">&#0160;&#0160;&#0160;23</span>&#0160;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">&#0160;&#0160;&#0160;24</span>&#0160;&#0160; collector.OfKind(WorksetKind.UserWorkset); </p>
<p style="margin: 0px;"><span style="color: #2b91af;">&#0160;&#0160;&#0160;25</span>&#0160;&#0160; IList&lt;Workset&gt; worksets = collector.ToWorksets(); </p>
<p style="margin: 0px;"><span style="color: #2b91af;">&#0160;&#0160;&#0160;26</span>&#0160;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">&#0160;&#0160;&#0160;27</span>&#0160;&#0160; Transaction tran = </p>
<p style="margin: 0px;"><span style="color: #2b91af;">&#0160;&#0160;&#0160;28</span>&#0160;&#0160; &#0160; <span style="color: blue;">new</span> Transaction(<span style="color: blue;">this</span>.Document, <span style="color: #a31515;">&quot;change workset id&quot;</span>);</p>
<p style="margin: 0px;"><span style="color: #2b91af;">&#0160;&#0160;&#0160;29</span>&#0160;&#0160; tran.Start();</p>
<p style="margin: 0px;"><span style="color: #2b91af;">&#0160;&#0160;&#0160;30</span>&#0160;&#0160; <span style="color: blue;">foreach</span>(Workset ws <span style="color: blue;">in</span> worksets)</p>
<p style="margin: 0px;"><span style="color: #2b91af;">&#0160;&#0160;&#0160;31</span>&#0160;&#0160; {</p>
<p style="margin: 0px;"><span style="color: #2b91af;">&#0160;&#0160;&#0160;32</span>&#0160;&#0160; &#0160; &#0160; wsparam.Set(ws.Id.IntegerValue);</p>
<p style="margin: 0px;"><span style="color: #2b91af;">&#0160;&#0160;&#0160;33</span>&#0160;&#0160; }</p>
<p style="margin: 0px;"><span style="color: #2b91af;">&#0160;&#0160;&#0160;34</span>&#0160;&#0160; tran.Commit();</p>
<p style="margin: 0px;"><span style="color: #2b91af;">&#0160;&#0160;&#0160;35</span>&#0160;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">&#0160;&#0160;&#0160;36</span>&#0160;&#0160; wid = elem.WorksetId;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">&#0160;&#0160;&#0160;37</span>&#0160;&#0160; TaskDialog.Show(<span style="color: #a31515;">&quot;worksetid&quot;</span>, wid.ToString());</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
</div>
