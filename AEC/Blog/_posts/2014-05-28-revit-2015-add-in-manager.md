---
layout: "post"
title: "Revit 2015 Add-In Manager"
date: "2014-05-28 11:43:46"
author: "Augusto Goncalves"
categories:
  - "Augusto Goncalves"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2014/05/revit-2015-add-in-manager.html "
typepad_basename: "revit-2015-add-in-manager"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/augusto-goncalves.html">Augusto Goncalves</a></p>  <p>There is a small change on the Revit 2015 SDK regarding the Add-In Manager: it now comes with just the .dll and .addin files, we removed the MSI installer.</p>  <p><strong>Important: </strong>make sure you download the latest release of the SDK from the <a title="http://www.autodesk.com/developrevit" href="http://www.autodesk.com/developrevit">Developer Center</a> (later than May 14, 2014).</p>  <p>Here are the steps to use it:</p>  <ol>   <li>Copy the .dll and .addin files from the Revit SDK folder (<font color="#808080">\Revit 2015 SDK\Add-In Manager\</font>) file to Revit Addin folder (i.e. <font color="#808080">C:\Users\ &lt;&lt;USERNAME&gt;&gt;\AppData\Roaming\Autodesk\Revit\Addins\2015\</font>). </li>    <li>Adjust the &lt;Assembly&gt; attribute to the correct path, or leave just file name. </li>    <li>Inside Revit, it should appear at the Addins tab under External Tools menu, as shown below.<a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fd126e40970b-pi"><img title="rvt_2015" style="border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px; display: inline" border="0" alt="rvt_2015" src="/assets/image_845425.jpg" width="322" height="180" /></a> </li> </ol>
