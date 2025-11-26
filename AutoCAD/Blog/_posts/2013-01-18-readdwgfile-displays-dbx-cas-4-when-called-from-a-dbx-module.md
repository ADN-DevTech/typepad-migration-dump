---
layout: "post"
title: "readDwgFile Displays DBX CAS 4 When Called From A DBX Module"
date: "2013-01-18 18:17:05"
author: "Gopinath Taget"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Gopinath Taget"
  - "ObjectARX"
  - "RealDWG"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/readdwgfile-displays-dbx-cas-4-when-called-from-a-dbx-module.html "
typepad_basename: "readdwgfile-displays-dbx-cas-4-when-called-from-a-dbx-module"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/gopinath-taget.html">Gopinath Taget</a></p>  <p>DBX modules, or 'Object Enablers' are designed for for the sole purpose of defining a custom object/entity in ObjectARX.&#160; The module should define one or more custom object classes that each contain custom members and override the required methods to suit particular purpose.&#160; In general, interacting with external databases or objects in the same database that are not directly related (via ObjectID link) to the custom object is not supported.</p>  <p>The theory is that an entity's definition or behavior should not be dependent on objects outside the database, but just a few key objects in the same database.&#160; For instance, a LINE entity should be fully defined within its module, and its behavior not be dependent on any outside data other than objects such as layer and linetype, whose relationships are established via AcDbObjectId derivatives.&#160; There are, of course, exceptions to this rule but it is important to try to adhere to the protocol.</p>  <p>The only direct workaround for this is to call readDwgFile() from an ARX module.&#160; You can export a global function in the ARX which you can call from the DBX module, assuming it is loaded (test using using Win32 API: GetModuleHandle()). </p>
