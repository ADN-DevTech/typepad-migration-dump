---
layout: "post"
title: "AcquireFiles() not fixing references of renamed files"
date: "2016-01-06 14:47:33"
author: "Wayne Brill"
categories:
  - "Vault"
  - "Wayne Brill"
original_url: "https://adndevblog.typepad.com/manufacturing/2016/01/acquirefiles-not-fixing-references-of-renamed-files.html "
typepad_basename: "acquirefiles-not-fixing-references-of-renamed-files"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/wayne-brill.html" target="_self">Wayne Brill</a></p>  <p>If files are renamed in Vault you may see that AcquireFiles() is not fixing the references. For AutoCAD this would be an XReference that was renamed and for Inventor it will be an ipt that was renamed. </p>  <p>A solution for AutoCAD files is to Add a Reference in your project to a dll named DWGDBXEngineWrapper.dll.&#160; (Add it from this directory)    <br />C:\Program Files\Autodesk\Vault Professional 2016\Explorer     <br />&#160; <br />Also add a using statement in your project: </p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c803990f970b-pi"><img title="image" style="border-left-width: 0px; border-right-width: 0px; background-image: none; border-bottom-width: 0px; padding-top: 0px; padding-left: 0px; display: inline; padding-right: 0px; border-top-width: 0px" border="0" alt="image" src="/assets/image_b615d9.jpg" width="770" height="196" /></a></p>  <p>&#160;</p>  <p>For Inventor:</p>  <p>Apply the Vault 2016 Service pack&#160;&#160; <a href="https://knowledge.autodesk.com/support/vault-products/downloads/caas/downloads/content/autodesk-vault-2016-service-pack-1.html?v=2016">https://knowledge.autodesk.com/support/vault-products/downloads/caas/downloads/content/autodesk-vault-2016-service-pack-1.html?v=2016</a></p>
