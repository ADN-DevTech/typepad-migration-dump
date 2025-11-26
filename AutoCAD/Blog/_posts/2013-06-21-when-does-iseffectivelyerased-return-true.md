---
layout: "post"
title: "When does IsEffectivelyErased return True"
date: "2013-06-21 09:30:49"
author: "Fenton Webb"
categories:
  - ".NET"
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "2014"
  - "AutoCAD"
  - "Fenton Webb"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2013/06/when-does-iseffectivelyerased-return-true.html "
typepad_basename: "when-does-iseffectivelyerased-return-true"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>  <p>For a database resident object that is not itself erased, there are three reasons why isEffectivelyErased() could return true:</p>  <ol>   <li>Some owner up the ownership hierarchy is erased.</li>    <li>This object, or some object up the ownership hierarchy is returning an empty ObjectId from its ownerId() method.</li>    <li>An owner cannot be opened for some reason somewhere up the ownership hierarchy.</li> </ol>
