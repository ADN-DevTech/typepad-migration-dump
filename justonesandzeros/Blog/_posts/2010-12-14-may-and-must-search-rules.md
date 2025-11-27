---
layout: "post"
title: "'May' and 'Must' Search Rules"
date: "2010-12-14 09:14:32"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
original_url: "https://justonesandzeros.typepad.com/blog/2010/12/may-and-must-search-rules.html "
typepad_basename: "may-and-must-search-rules"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/TipsAndTricks2.png" /></p>
<p>The <a href="http://justonesandzeros.typepad.com/blog/2010/12/advanced-advanced-find.html">Advanced Advanced Find</a> application shows how to make use of the &quot;search rule&quot; concept.&#0160; This is one of the very few features that&#39;s available only in the API.&#0160; For some reason, the out-of-the-box search dialog does not expose this feature.</p>
<p>Now that I have a sample out on this feature, I&#39;d like to explain a bit more on how to use it.&#0160; In the API there are 3 rule types:&#0160; May, Must and MustNot.&#0160; MustNot doesn&#39;t work, so I&#39;ll only focus on May and Must.&#0160; In programming terms, I like to think of May and Must statements as OR and AND conditions respectively.&#0160;</p>
<p>The Must (AND) behavior is what is exposed in the UI find dialog, and it&#39;s the behavior from versions earlier than 2011.&#0160; The May (OR) behavior is new for Vault 2011.&#0160; At least it is new at the Search Condition level.&#0160;</p>
<p>It is possible to use the word &quot;or&quot; in the value of the search condition (see picture below).&#0160; It does result in OR behavior, but that trick can only be used on a single property.&#0160; The May rule is the only way to OR together different properties.&#0160;&#0160;&#0160;&#0160; <img alt="" src="/assets/FindDialog.png" /></p>
<p><strong>&#0160;</strong></p>
<p><strong>Usage:      <br /></strong>In order for the search to work properly, the following requirements must be met:</p>
<ul>
<li>The search contains at least 1 Must condition </li>
<li>May conditions must have at least one other May condition.&#0160; In other words, you can have 0 May conditions or more than 1 May conditions.&#0160; If there is only 1 May condition, it is ignored. </li>
</ul>
<p><strong>&#0160;</strong></p>
<p><strong>Order of operations:      <br /></strong>The order that the conditions are in does not matter.&#0160; The reason is that all the May conditions are grouped together and OR&#39;ed against each other.&#0160; The result is then AND&#39;ed to the Must statements.</p>
<p>For example, the collection of conditions <span style="color: #ff0000;">{ May X, Must Y, May Z}</span>&#0160; can be rewritten as <span style="color: #ff0000;">(X or Z) and Y</span>.&#0160; Since everything is grouped together in this manner, the search conditions can be in any order.</p>
