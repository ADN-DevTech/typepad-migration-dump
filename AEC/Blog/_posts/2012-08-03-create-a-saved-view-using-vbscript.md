---
layout: "post"
title: "Create a saved view using VBScript"
date: "2012-08-03 10:16:31"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "COM"
  - "Navisworks"
original_url: "https://adndevblog.typepad.com/aec/2012/08/create-a-saved-view-using-vbscript.html "
typepad_basename: "create-a-saved-view-using-vbscript"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>If you want to create a saved view using VBScript then the following code should help:</p>
<p><span style="font-family: &#39;courier new&#39;, courier; color: #007f7f;">&#39;create new document </span><br /><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #0000ff;"> set</span> navis_doc = CreateObject(<span style="color: #ff0000;">&quot;NavisWorks.Document&quot;</span>) </span><br /> <br /><span style="font-family: &#39;courier new&#39;, courier; color: #007f7f;"> &#39;make sure it&#39;s visible </span><br /><span style="font-family: &#39;courier new&#39;, courier;"> navis_doc.visible = <span style="color: #0000ff;">true </span></span><br /> <br /><span style="font-family: &#39;courier new&#39;, courier; color: #007f7f;"> &#39;open document </span><br /><span style="font-family: &#39;courier new&#39;, courier;"> navis_doc.OpenFile(<span style="color: #ff0000;">&quot;C:\examples\gatehouse.nwd&quot;</span>) </span><br /> <br /><span style="font-family: &#39;courier new&#39;, courier; color: #007f7f;"> &#39;get state object </span><br /><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #0000ff;"> set</span> navis_state = navis_doc.state() </span><br /> <br /><span style="font-family: &#39;courier new&#39;, courier; color: #007f7f;"> &#39;create saved view from current view </span><br /><span style="font-family: &#39;courier new&#39;, courier; color: #007f7f;"> &#39; since VBScript does not support enum&#39;s therefore either use the </span><br /><span style="font-family: &#39;courier new&#39;, courier; color: #007f7f;"> &#39; enum value (11 = eObjectType_nwOpView) directly or use </span><br /><span style="font-family: &#39;courier new&#39;, courier; color: #007f7f;"> &#39; GetEnum() to get it </span><br /><span style="font-family: &#39;courier new&#39;, courier;"> enumVal = navis_state.GetEnum(<span style="color: #ff0000;">&quot;eObjectType_nwOpView&quot;</span>) </span><br /><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #0000ff;"> set</span> navis_view = navis_state.ObjectFactory(enumVal) </span><br /><span style="font-family: &#39;courier new&#39;, courier;"> navis_view.name = <span style="color: #ff0000;">&quot;MySavedView&quot; </span></span><br /><span style="font-family: &#39;courier new&#39;, courier;"> navis_view.anonview = navis_state.CurrentView </span><br /><span style="font-family: &#39;courier new&#39;, courier;"> navis_state.SavedViews().Add(navis_view) </span><br /> <br /><span style="font-family: &#39;courier new&#39;, courier; color: #007f7f;"> &#39;make sure app stays open with no refs </span><br /><span style="font-family: &#39;courier new&#39;, courier;"> navis_doc.stayopen</span></p>
