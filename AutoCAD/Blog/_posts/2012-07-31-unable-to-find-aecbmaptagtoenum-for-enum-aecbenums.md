---
layout: "post"
title: "Unable to find AecbmapTagToEnum for enum AecbEnums..."
date: "2012-07-31 21:43:54"
author: "Barbara Han"
categories:
  - "Barbara Han"
  - "RealDWG"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/unable-to-find-aecbmaptagtoenum-for-enum-aecbenums.html "
typepad_basename: "unable-to-find-aecbmaptagtoenum-for-enum-aecbenums"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/barbara-han.html" target="_self">Barbara Han</a></p>  <p>In several places, I found some people said that they got “Unable to find AecbmapTagToEnum for enum AecbEnums...” error when opening some dwg file and didn’t know how to resolve this problem, so I posted the answer here to help.</p>  <p>The AecbMapTagToEnum is probably defined by AutoCAD MEP. If the MEP Object Enablers are installed on your machine, and you call CoInitialize() at the beginning of the program considering the fact that MEP OEs need COM support, then this problem would be gone.</p>  <p>The latest version of RealDWG SDK have included almost all of Autodesk vertical OEs. If you are using older versions of RealDWG SDK and those OEs are not included in RealDWG SDK installation package, you can install those OEs manually. They are downloadable from:   <br /><a href="http://usa.autodesk.com/adsk/servlet/index?siteID=123112&amp;id=2753223&amp;linkID=9240618">http://usa.autodesk.com/adsk/servlet/index?siteID=123112&amp;id=2753223&amp;linkID=9240618</a></p>  <p>acdbModelerStart() should be called as well to prevent some other problems. </p>  <p>The following is a code sample that you can refer to:</p>  <p><font size="1" face="Courier New">// MEP drawings have issues without this      <br />::CoInitialize(NULL); </font></p>  <p><font size="1" face="Courier New">acdbSetHostApplicationServices(&amp;gDumpDwgHostApp);      <br />long lcid = 0x00000409; // English       <br />acdbValidateSetup(lcid); </font></p>  <p><font size="1" face="Courier New">// Could also be useful to call this, though things      <br />// seem to work even without it       <br />acdbModelerStart();</font></p>
