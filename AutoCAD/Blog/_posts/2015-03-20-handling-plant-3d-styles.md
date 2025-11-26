---
layout: "post"
title: "Handling Plant 3D Styles"
date: "2015-03-20 10:50:18"
author: "Augusto Goncalves"
categories: []
original_url: "https://adndevblog.typepad.com/autocad/2015/03/handling-plant-3d-styles.html "
typepad_basename: "handling-plant-3d-styles"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/augusto-goncalves.html">Augusto Goncalves</a></p>  <p>Plant 3D use styles to managed how parts and assets are shown on the project. Most of that information is stored on the dictionaries, more specifically under NOD&gt;Autodesk_PNP.</p>  <p>But the supported way to handle the styles is by the direct APIs, such as the following methods. One important note: this is valid for current/active drawings.</p>  <pre style="font-size: 13px; font-family: consolas; background: white; color: black"><span style="color: #2b91af">PnIDStyleUtils</span>.GetStyleIdFromName</pre>

<pre style="font-size: 13px; font-family: consolas; background: white; color: black"><span style="color: #2b91af">ProjectSymbolStyleUtils</span>.CopyStyle<br /></pre>

<pre style="font-size: 13px; font-family: consolas; background: white; color: black"><br /></pre>
