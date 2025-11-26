---
layout: "post"
title: "Using AutoCAD MEP Object Enablers in AcCoreConsole"
date: "2015-03-12 00:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "AutoCAD Architecture"
  - "AutoCAD MEP"
  - "Jeremy Tammik"
  - "OMF"
original_url: "https://adndevblog.typepad.com/aec/2015/03/using-autocad-mep-objects-in-accoreconsole.html "
typepad_basename: "using-autocad-mep-objects-in-accoreconsole"
typepad_status: "Publish"
---

<p>By

<a href="http://adndevblog.typepad.com/cloud_and_mobile/jeremy-tammik.html">
Jeremy</a>

<a href="http://thebuildingcoder.typepad.com/blog/about-the-author.html">
Tammik</a>.</p>

<p><strong>Question:</strong>

Using AcCoreConsole I'd like to move an object via AcCoreConsole.</p>
<p>The object type in question happens to be an AutoCAD MEP MVBlock.</p>
<p>Is this possible?</p>
<p>Would AcCoreConsole recognise the object as valid or would it be a zombie, i.e. a proxy object?</p>

<p><strong>Answer:</strong>

I checked with the development team and received several confirmations saying that this should work:</p>

<p>AcCoreConsole does recognise MVBlock, and the object can be moved in the same way as in AutoCAD.</p>

<p>The AutoCAD MEP object enablers get loaded on demand in AcCoreConsole.exe. </p>

<p>Here is a test suite proving it:</p>

<p>1. accoreconsole.exe: Just run accoreconsole.exe, following modules were loaded. Please note AEC modules are not loaded:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c76068be970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b7c76068be970b image-full img-responsive" alt="AcCoreConsole Object Enablers" title="AcCoreConsole Object Enablers" src="/assets/image_513456.jpg" border="0" /></a><br />

</center>

<p>2. accoreconsole.exe /i oneBox.dwg: Open a dwg with only one box. A few more AutoCAD modules get loaded, but no AEC modules:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c76068c5970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b7c76068c5970b image-full img-responsive" alt="AcCoreConsole Object Enablers" title="AcCoreConsole Object Enablers" src="/assets/image_459619.jpg" border="0" /></a><br />

</center>

<p>3. accoreconsole.exe /i oneWall.dwg: Open a dwg with only one wall. AEC modules are loaded. Also note that AECB modules are loaded, even though there are no MEP objects in the DWG:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0e9e3fd970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b8d0e9e3fd970c image-full img-responsive" alt="AcCoreConsole Object Enablers" title="AcCoreConsole Object Enablers" src="/assets/image_887807.jpg" border="0" /></a><br />

</center>

<p>Following is the output of list command:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c76068c9970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b7c76068c9970b image-full img-responsive" alt="AcCoreConsole Object Enablers" title="AcCoreConsole Object Enablers" src="/assets/image_809341.jpg" border="0" /></a><br />

</center>

<p>4. accoreconsole.exe /i oneDuct.dwg: Open a dwg with only one duct. AEC and AECB module are loaded:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c76068cd970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b7c76068cd970b image-full img-responsive" alt="AcCoreConsole Object Enablers" title="AcCoreConsole Object Enablers" src="/assets/image_811808.jpg" border="0" /></a><br />

</center>

<p>Following is the output of list command:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08043996970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb08043996970d image-full img-responsive" alt="AcCoreConsole Object Enablers" title="AcCoreConsole Object Enablers" src="/assets/image_629725.jpg" border="0" /></a><br />

</center>

<p>I hope this helps.</p>
