---
layout: "post"
title: "Why doesn&rsquo;t an App&rsquo;s PackageContents.xml have a schema?"
date: "2012-11-29 10:25:23"
author: "Fenton Webb"
categories:
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Fenton Webb"
  - "LISP"
  - "Mac"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/11/why-doesnt-an-apps-packagecontentsxml-have-a-schema.html "
typepad_basename: "why-doesnt-an-apps-packagecontentsxml-have-a-schema"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>  <p>A few people have asked my why we did not post a schema for the PackageContents.xml. We specifically do not have a schema because:</p>  <p>1) We wanted to make a very general design for describing the way components are loaded into a host application, as you know each host application (Revit/AutoCAD/Inventor/Maya/etc etc) are different from each other in their plugin requirements, so we wanted to make sure that the XML could be easily adjusted to suit different needs as each host platform adopted the Exchange store. </p>  <p>2) The PackageContents.xml is meant to be extensible, for example, it can be used to store developer specific application settings.</p>  <p>3) The PackageContents.xml is currently used to load apps into host applications, like AutoCAD, but also in Autodesk to automate building installers and also by the AppManager. We see the XML file being used by 3rd party developers for their own needs too.</p>  <p>Finally, to be clear, each attribute definition inside of the PackageContents.xml must start with an upper-case letter and finish all in lower-case… e.g.</p>  <p>Url='””</p>  <p>not</p>  <p>URL=””</p>
