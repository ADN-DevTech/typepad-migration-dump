---
layout: "post"
title: "Workaround for Maya 2018 WPF sample not showing again after closing it"
date: "2018-06-28 23:57:58"
author: "Cheng Xi Li"
categories:
  - ".Net"
  - "Cheng Xi Li"
  - "Maya"
  - "WPF"
original_url: "https://around-the-corner.typepad.com/adn/2018/06/workaround-for-maya-2018-wpf-sample-not-showing-again-after-closing-it.html "
typepad_basename: "workaround-for-maya-2018-wpf-sample-not-showing-again-after-closing-it"
typepad_status: "Publish"
---

<p>Having trouble with WPF sample in 2018 .net devkit?&#0160; It is reported that dagexplorer command won&#39;t reopen a closed window in Maya 2018. This could be fixed with following modification:</p>
<p>&#0160;</p>
<p>Replace</p>
<p>&#0160;</p>
<p><em><strong>MGlobal.executeCommand($@&quot;catch (`showWindow &quot;&quot;{hostTitle}&quot;&quot;`);&quot;);</strong></em></p>
<p>&#0160;</p>
<p>with</p>
<p><em><strong>&#0160;</strong></em></p>
<p><em><strong>MGlobal.executeCommand($@&quot;catch (`workspaceControl -e -visible true &quot;&quot;{hostTitle}&quot;&quot;`);&quot;);</strong></em><br clear="none" /><br clear="none" /></p>
<p>To make the window showing again.</p>
