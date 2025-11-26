---
layout: "post"
title: "How To Retrieve Ordinals For AEC Exported Functions"
date: "2017-09-14 21:40:00"
author: "Madhukar Moogala"
categories:
  - "AutoCAD"
  - "Madhukar Moogala"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2017/09/how-to-retrieve-ordinals-for-aec-exported-functions.html "
typepad_basename: "how-to-retrieve-ordinals-for-aec-exported-functions"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar 
Moogala</a></p><p>The ordinals for Functions may be useful for RealDWG Apps, one such scenario when realDWG app explodes any AEC entities which are view dependent entities, explode of an entity in particular view may result certain geometry respective to that view, to get desired result one may require to set the view of the entity in the drawing and explode.</p><p>For reference please look at this blog <a href="http://adndevblog.typepad.com/autocad/2015/07/exploding-aec-entities-using-realdwg.html">post</a></p><p>First get OMF SDK for relevant version from <a title="https://adn.autodesk.io/" href="https://adn.autodesk.io/">https://adn.autodesk.io/</a></p><p>Go To Software and download SDK after accepting License Agreement.</p><p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09c372d0970d-pi"><img width="431" height="448" title="OMFSDK" style="display: inline; background-image: none;" alt="OMFSDK" src="/assets/image_579272.jpg" border="0"></a></p><p><br></p><p>Launch Visual Studio Developer Command</p><p>Change to OMFSDK\Lib_$Platform folder</p><p>Execute</p><pre>dumpbin /exports $libname | findstr $functionName
</pre>
<p>For Example </p>
<pre>D:\OMF2018\Lib-x64&gt;dumpbin /EXPORTS AecBase.lib | findstr "drawingPromoterAndIniter"
           897    ?drawingPromoterAndIniter@AecAppDbx@@SAXPEAVAcDbDatabase@@_N@Z (public: static void __cdecl AecAppDbx::drawingPromoterAndIniter(class AcDbDatabase *,bool))
</pre>

<p>"
897
" is Ordinal number</p>
