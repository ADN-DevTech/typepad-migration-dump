---
layout: "post"
title: "Use HiddenCommands() in IExplorerExtension to Hide Vault Explorer commands"
date: "2016-02-12 11:11:20"
author: "Wayne Brill"
categories:
  - "Vault"
  - "Wayne Brill"
original_url: "https://adndevblog.typepad.com/manufacturing/2016/02/use-hiddencommands-in-iexplorerextension-to-hide-vault-explorer-commands.html "
typepad_basename: "use-hiddencommands-in-iexplorerextension-to-hide-vault-explorer-commands"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/wayne-brill.html" target="_self">Wayne Brill</a></p>  <p>You can use the HiddenCommands function to hide commands in Vault Explorer. If this function returns names of commands they will be hidden. To get the names of Vault commands you can use the CommandIds property of the IApplication that is passed into the OnLogOn function:</p>  <div style="font-size: 10pt; font-family: consolas; background: #eeeeee; color: black">   <p style="margin: 0px"><span style="color: blue">public</span> <span style="color: blue">void</span> OnLogOn(<span style="color: #2b91af">IApplication</span> application)</p>    <p style="margin: 0px">{</p>    <p style="margin: 0px">&#160;&#160;&#160; System.Collections.Generic.<span style="color: #2b91af">IEnumerable</span>&lt;<span style="color: blue">string</span>&gt; cmds = application.CommandIds;</p>    <p style="margin: 0px">}</p> </div>  <p>&#160;</p>  <p>Here is an example that uses HiddenCommands() to hide the Checkout, CheckIn and Rename commands.</p>  <div style="font-size: 10pt; font-family: consolas; background: #eeeeee; color: black">   <p style="margin: 0px"><span style="color: blue">public</span> <span style="color: #2b91af">IEnumerable</span>&lt;<span style="color: blue">string</span>&gt; HiddenCommands()</p>    <p style="margin: 0px">{</p>    <p style="margin: 0px">&#160;&#160;&#160; <span style="color: #2b91af">IEnumerable</span>&lt;<span style="color: blue">string</span>&gt; cmdsToHide = <span style="color: blue">new</span> <span style="color: blue">string</span>[] { <span style="color: #a31515">&quot;QuickCheckOut&quot;</span>, <span style="color: #a31515">&quot;CheckIn&quot;</span>, <span style="color: #a31515">&quot;Rename&quot;</span> };</p>    <p style="margin: 0px">&#160;&#160;&#160; <span style="color: blue">return</span> cmdsToHide;</p>    <p style="margin: 0px">}</p> </div>  <p>To test this you can use the HelloWorld SDK sample:</p>  <p><em>C:\Program Files (x86)\Autodesk\Autodesk Vault 2016 SDK\vs12\CSharp\HelloWorld</em></p>  <p>See this <a href="http://justonesandzeros.typepad.com/blog/2011/09/vault-explorer-command-events.html" target="_blank">post</a> for an example code that you can use to display the command id (a string) every time you invoke a command in Vault explorer.</p>
