---
layout: "post"
title: "iLogic search path for custom references"
date: "2014-06-23 14:59:10"
author: "Philippe Leefsma"
categories:
  - "Inventor"
  - "Philippe Leefsma"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/06/ilogic-search-path-for-custom-references.html "
typepad_basename: "ilogic-search-path-for-custom-references"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/philippe-leefsma.html" target="_self">Philippe Leefsma</a></p>  <p>It is possible to use functionalities from custom dlls in your iLogic rules, you just need to declare the <strong><font color="#ff0000">AddReference “adn.dll”</font></strong> (replaced by the name of your dll obviously) at the top of your rule.</p>  <p>iLogic will look in the following places to find your dll:</p>  <p>1) The folder of the document running the rule   <br />2) The workspace path of the current project    <br />3) The library paths of the current project    <br />4) The external rule directories set in Tools &gt;&gt; Options &gt;&gt; iLogic Configuration &gt;&gt; External Rule Directories    <br />5) The reference path set in Tools &gt;&gt; Options &gt;&gt; iLogic Configuration &gt;&gt; iLogic Addin DLLs Directory     <br /></p>  <p>In all these locations, <u>it only looks in the directory itself. It doesn’t go into subdirectories.</u></p>
