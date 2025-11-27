---
layout: "post"
title: "System Name vs Display Name"
date: "2012-04-10 14:51:31"
author: "Doug Redmond"
categories:
  - "Concepts"
original_url: "https://justonesandzeros.typepad.com/blog/2012/04/system-name-vs-display-name.html "
typepad_basename: "system-name-vs-display-name"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Concepts3.png" /></p>
<p>This scenario should be familiar if you have used the Vault API.&#0160; Your code relies on a specific configuration which you need to look up in your code.&#0160; For example, you have a UDP and your code needs to look up the ProdDef object.&#0160; How do you do it?&#0160; What piece of information do you use?</p>
<p>Let’s start with the completely wrong answer.&#0160; The <strong>ID</strong> value should not be used.&#0160; It’s not consistent between Vaults, you don’t know the value ahead of time, and the value may change when you migrate to the next Vault release.</p>
<p>The <strong>Display Name</strong> is usable in certain cases, but is not bullet proof.&#0160; The main problem is that the display name can be changed through the Vault settings.&#0160; If your app is specific to a single Vault installation, then you can sometimes get away with this approach.&#0160; If you are developing something for a mass market, then you can’t depend on the display name.</p>
<p><strong>System Name</strong> is the most solid thing to use.&#0160; Once this value is set, it can’t be changed.&#0160; The Vault admins can change the display name all they want, the system name will not change.&#0160;</p>
<p>In Vault 2012 and earlier, the server would create the system name for you using a randomly generated GUID.&#0160; So there is no way you could know that value when you are writing your code.</p>
<p>In Vault 2013, we updated most of the Add functions so that you pass in the system name.&#0160; This way, you can know ahead of time what the system name is.&#0160; You can store that value in you code and be secure in the knowledge that it will locate the correct object.</p>
<p>When calling an Add function, you can’t set the system name to be whatever you want.&#0160; It must be a GUID in the format (lower case) xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx.&#0160; If you have a .Net Guid object, you can just call ToString(“D”) to get that string.&#0160; Even though your system name will not be human readable, but it will be reliable.&#0160; And you are safe from naming collisions (as long as people don’t just cut and paste code from the sample apps on your blog).</p>
<p>Another caveat to this architecture is that you need to add the object through the API.&#0160; If you do it through the UI, then the client will give it a random system name.</p>
<p><img alt="" src="/assets/Concepts3-1.png" /></p>
