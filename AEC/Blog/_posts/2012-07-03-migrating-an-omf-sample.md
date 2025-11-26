---
layout: "post"
title: "Migrating an OMF sample"
date: "2012-07-03 01:36:08"
author: "Katsuaki Takamizawa"
categories:
  - "Katsuaki Takamizawa"
original_url: "https://adndevblog.typepad.com/aec/2012/07/migrating-an-omf-sample.html "
typepad_basename: "migrating-an-omf-sample"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/katsuaki-takamizawa.html" target="_self">Katsuaki Takamizawa</a></p>
<p>I tried to migrate an small 2012 OMF sample for AutoCAD Architecture 2013. This is an ADN sample called “Entity Anchor” which sets an anchor between two entities so that one entity moves, the other follows it. There would be a variety of ways to migrate projects but I was able to migrate this sample following steps.</p>
<p>1. Open the 2012 project in Visual Studio 2010 to convert the project <br />2. Update the include file path for OMF 2013 and ObjectARX 2013 SDK <br />3. Update the library file path for OMF 2013 and ObjectARX 2013 SDK <br />4. Update preprocessor symbols. I looked at Sprinkler sample and did the same thing by adding “WINVER=0x0501;_WIN32_WINNT=0x0501;” to specify the minimum OS is Windows XP. <br />5. Update the additional dependency files. I also looked at Sprinkler sample and did the same thing by setting “acad.lib;adui19.lib;acui19.lib;acge19.lib; <br />&#0160; axdb.lib;rxapi.lib;acdb19.lib;acgiapi.lib; <br />&#0160; AecBase.lib;AecBaseEx.lib;AecArchBase.lib; <br />&#0160; AecModeler.lib;AecResMgr.lib;AecGuiBase.lib; <br />&#0160; AecSystemTools.lib;AecResUi.lib;Ac1st19.lib; <br />&#0160; accore.lib;AecCore.lib;AecApp.lib;%(AdditionalDependencies)”</p>
<p>6. Replace stdafx.h with stdafx.h in OmfUiSprinkler folder in Sprinkler sample. This is an easy way to get the OMF, ObjectARX, and platform headers for 2013. <br />7. AecUiBase is replaced with AecCore class. I needed to resolve compile errors for this change. Please look at the migration document in OMF 2013 SDK for changes in 2013 release. <br />8. In the resource project, I added “en-US” folder to the output path. And I changed the module name to AdcgEntityAnchorRes.dll from AdcgEntityAnchorEnu.dll. This is also required change and written in the migration document. Otherwise, ACA can not find the corresponding resource dll file. <br />9. Build the project</p>
<p>I attached the migrated project&#0160;<span class="asset  asset-generic at-xid-6a0167607c2431970b017742f92303970d"><a href="http://adndevblog.typepad.com/files/entity-anchor-1.zip">Entity Anchor</a></span>&#0160;for your reference. Please draw two walls and run “CreateAnchor” command to anchor a wall and reference another one.</p>
