---
layout: "post"
title: "IncludeLibraryContents to include Content Center parts with AcquireFiles()"
date: "2014-03-14 10:51:27"
author: "Wayne Brill"
categories:
  - "Vault"
  - "Wayne Brill"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/03/includelibrarycontents-to-include-content-center-parts-with-acquirefiles.html "
typepad_basename: "includelibrarycontents-to-include-content-center-parts-with-acquirefiles"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/wayne-brill.html" target="_self">Wayne Brill</a></p>  <p>Recently I had a case where the question was how to download content center parts. I was using code similar to the code in the example on this <a href="http://adndevblog.typepad.com/manufacturing/2013/06/checkout-checkin-a-file-in-vault-2014.html" target="_blank">page</a> and the content center parts were downloading fine from one Vault. In another Vault the content center parts were not getting downloaded. The difference was that in one Vault the Content Center parts were in a Folder and in the other Vault they were in a library. The solution was to include the library with the AcquireFilesSettings by making “IncludeLibraryContents=True”. Here is a snippet that shows this:</p>  <div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt">   <p style="margin: 0px">oSettings.OptionsRelationshipGathering. _</p>    <p style="margin: 0px">FileRelationshipSettings.IncludeLibraryContents = <span style="color: blue">True</span></p> </div>
