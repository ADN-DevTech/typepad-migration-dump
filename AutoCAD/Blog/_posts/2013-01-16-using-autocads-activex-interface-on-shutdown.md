---
layout: "post"
title: "Using AutoCAD's ActiveX interface on Shutdown"
date: "2013-01-16 10:58:04"
author: "Augusto Goncalves"
categories:
  - "ActiveX"
  - "Augusto Goncalves"
  - "AutoCAD"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/using-autocads-activex-interface-on-shutdown.html "
typepad_basename: "using-autocads-activex-interface-on-shutdown"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/augusto-goncalves.html">Augusto Goncalves</a></p>  <p>When accessing the ActiveX interface of AutoCAD in an ObjectARX application, we may try use the IAcadApplication and IAcadPreferences interfaces to set a user profile at AutoCAD startup and shutdown.</p>  <p>Consider the scenario: on the AcRx::kInitAppMsg it is stored the actual user profile and set another profile. On AcRx::kUnloadAppMsg we may want to restore the previous profile. It's not a problem to set the profile on kInitAppMsg, but on kUnloadAppMsg I'm not able to get the active AutoCAD ActiveX object using GetActiveObject(). The call to GetActiveObject() returns MK_E_UNAVAILABLE instead of S_OK. </p>  <p>The problem is that AutoCAD unregisters its ActiveX application object before unloading the ObjectARX modules; therefore, we cannot access AutoCAD's ActiveX interface on the kUnloadAppMsg notification. The kPreQuitMsg doesn't work, either. </p>  <p>When AutoCAD quits, it performs the following steps:</p>  <p>1. Unregisters the ActiveX application object (removes it from the running object table).   <br />2. Sends kPreQuitMsg to every loaded ObjectARX application.    <br />3. Sends kUnloadAppMsg to every ObjectARX application before unloading them.</p>  <p>Therefore, you cannot use AutoCAD's ActiveX interface on kUnloadAppMsg. You can work around this by using a reactor. You could react on the AcEditorReactor::databaseToBeDestroyed() notification, but the result of this is that you will reset the profile every time the user loads or creates a new drawing, so you have to set your profile when a new drawing is loaded or created.</p>
