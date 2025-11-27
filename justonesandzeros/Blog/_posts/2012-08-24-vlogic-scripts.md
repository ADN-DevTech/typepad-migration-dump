---
layout: "post"
title: "vLogic Scripts"
date: "2012-08-24 14:45:24"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
original_url: "https://justonesandzeros.typepad.com/blog/2012/08/vlogic-scripts.html "
typepad_basename: "vlogic-scripts"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/TipsAndTricks3.png" /></p>
<p>I request came in for some examples of how to do Vault operations using vLogic.&#0160; So here you go.&#0160; You can never have too much sample code.&#0160; </p>
<p><a href="http://justonesandzeros.typepad.com/Files/vLogicScripts/Commands/Search.ps1" target="_blank">Search.ps</a> - This script runs an Item search.</p>
<p><a href="http://justonesandzeros.typepad.com/Files/vLogicScripts/Commands/SetItemProperty.ps1">SetItemProperty.ps1</a> - This script sets a property value on a set of selected Items.</p>
<p><a href="http://justonesandzeros.typepad.com/Files/vLogicScripts/Commands/RunChangeOrderActivity.ps1" target="_blank">RunChangeOrderActivity.ps1</a> - This script changes the state of a Change Order.</p>
<p>These scripts are basically ports of other API code examples floating around.&#0160; All I did was convert from C# or VB.NET to <a class="zem_slink" href="http://en.wikipedia.org/wiki/Windows_PowerShell" rel="wikipedia" target="_blank" title="Windows PowerShell">PowerShell</a> syntax.&#0160; </p>
<p>I would like to point out that using a script doesn’t mean its any easier to program than a compiled app.&#0160; If you find a Vault API function difficult in VB.NET, then you will still find it difficult in PowerShell.&#0160; And vice versa.&#0160; Like most things, there is a trade-off.&#0160; For some things, scripting works great.&#0160; For other things, a compiled app is better.</p>
<p>I’m still new to PowerShell myself, but I’m hoping to come up with a tutorial or two on using vLogic.</p>
<p><img alt="" src="/assets/TipsAndTricks3-1.png" /></p>
