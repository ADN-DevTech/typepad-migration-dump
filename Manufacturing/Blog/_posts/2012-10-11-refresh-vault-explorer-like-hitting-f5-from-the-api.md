---
layout: "post"
title: "Refresh Vault Explorer like hitting &ldquo;F5&rdquo; from the API"
date: "2012-10-11 12:03:18"
author: "Wayne Brill"
categories:
  - "Vault"
  - "Wayne Brill"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/10/refresh-vault-explorer-like-hitting-f5-from-the-api.html "
typepad_basename: "refresh-vault-explorer-like-hitting-f5-from-the-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/wayne-brill.html" target="_self">Wayne Brill</a></p>  <p>Here are two ways to do a refresh from the API. One way when you are using a custom command in the vault explorer is to use the Context ForceRefresh of CommandItemEventArgs which is a parameter of the Execute event hander. Here is code snippet to help explain: </p>  <div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt">   <p style="margin: 0px"><span style="color: blue">void</span> HelloWorldCommandHandler</p>    <p style="margin: 0px">&#160;&#160;&#160; (<span style="color: blue">object</span> s, <span style="color: #2b91af">CommandItemEventArgs</span> e)</p>    <p style="margin: 0px">{</p>    <p style="margin: 0px">&#160;&#160;&#160; e.Context.ForceRefresh = <span style="color: blue">true</span>;</p> </div>  <p>The behavior is the same as if the user pressed the Refresh or F5 key. You can use the Vault SDK HelloWorld sample to test this.</p>  <p>&#160;</p>  <p>The second way when you are using a custom tab view is to use the SelectionChanged event. Notice that the context property of the SelectionChangedEventArgs passed into the handler has a Refresh method. Here is a code snippet:</p>  <div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt">   <p style="margin: 0px">e.Context.Refresh() = <span style="color: blue">true</span>;</p> </div>
