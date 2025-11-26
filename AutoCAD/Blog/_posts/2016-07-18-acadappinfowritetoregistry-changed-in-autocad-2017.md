---
layout: "post"
title: "AcadAppInfo::writeToRegistry changed in AutoCAD 2017"
date: "2016-07-18 20:30:00"
author: "Virupaksha Aithal"
categories:
  - "AutoCAD"
  - "Virupaksha Aithal"
original_url: "https://adndevblog.typepad.com/autocad/2016/07/acadappinfowritetoregistry-changed-in-autocad-2017.html "
typepad_basename: "acadappinfowritetoregistry-changed-in-autocad-2017"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/virupaksha-aithal.html" target="_self">Virupaksha Aithal</a></p>
<p>In AcadAppInfo::writeToRegistry, the Boolean parameters are removed in AutoCAD 2017. This is because AutoCAD is not guaranteed to run with elevated permissions always and hence writing to current machine is always problematic through API. Now writeToRegistry works like writeToRegistry(false, true) of AutoCAD 2016. (Write to current user only and in AutoCAD section).</p>
