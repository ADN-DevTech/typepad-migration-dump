---
layout: "post"
title: "Navisworks 2014 API new feature &ndash; One more path to load plugin"
date: "2013-05-15 20:01:57"
author: "Xiaodong Liang"
categories:
  - ".NET"
  - "Navisworks"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/aec/2013/05/navisworks-2014-api-new-feature-one-more-path-to-load-plugin.html "
typepad_basename: "navisworks-2014-api-new-feature-one-more-path-to-load-plugin"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>  <p>In the past, we can only put the plugin assembly under &lt;Navisworks Installation Path&gt;\Plugins. In 2014, you have one more option to deploy the assembly in </p>  <p><strong>&quot;%APPDATA%\Autodesk Navisworks Manage 2014\Plugins</strong></p>  <p>In the DevDays’s PPT, it says the new path is “%USERPROFILE%“. It is wrong. Sorry for this.</p>  <p>For programming, the script of post-build event could be:</p>  <p><em>xcopy /Y &quot;$(TargetDir)*.*&quot; &quot;%APPDATA%\Autodesk Navisworks Manage 2014\Plugins\$(TargetName)\&quot;</em></p>
