---
layout: "post"
title: "Using CAcUiNumericEdit - Is it possible to enter values in feet &amp; inches and get the data back in inches?"
date: "2012-09-12 05:34:31"
author: "Philippe Leefsma"
categories:
  - "AutoCAD"
  - "ObjectARX"
  - "Philippe Leefsma"
original_url: "https://adndevblog.typepad.com/autocad/2012/09/using-cacuinumericedit-is-it-possible-to-enter-values-in-feet-inches-and-get-the-data-back-in-inches.html "
typepad_basename: "using-cacuinumericedit-is-it-possible-to-enter-values-in-feet-inches-and-get-the-data-back-in-inches"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/philippe-leefsma.html" target="_self">Philippe Leefsma</a></p>  <p><b>Q:</b></p>  <p>Using <em>CAcUiNumericEdit</em> it's easy to enter feet and inches into the editbox, but I also get feet and inches as my return value... How can I convert from feet and inches to inches? </p>  <p><b>A:</b></p>  <p>To convert from feet and inches to a decimal value you can use <em>acdbDisToF</em> API</p>
