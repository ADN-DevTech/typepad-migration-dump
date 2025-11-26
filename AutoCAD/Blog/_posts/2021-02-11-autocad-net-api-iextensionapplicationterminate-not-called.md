---
layout: "post"
title: "AutoCAD .NET API: IExtensionApplication.Terminate () Not Called"
date: "2021-02-11 01:54:53"
author: "Madhukar Moogala"
categories:
  - ".NET"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2021/02/autocad-net-api-iextensionapplicationterminate-not-called.html "
typepad_basename: "autocad-net-api-iextensionapplicationterminate-not-called"
typepad_status: "Publish"
---

<p style="background: white"><span style="color:#3c3c3c; font-family:Arial; font-size:10pt"><strong>Issue</strong>
		</span></p><p style="background: white"><span style="color:#3c3c3c; font-family:Arial; font-size:10pt">I am creating and running an add-in application with the AutoCAD .NET API, but after applying the AutoCAD 2020.1 Update, IExtensionApplication.Terminate() is no longer called. 
</span></p><p style="background: white"><span style="color:#3c3c3c; font-family:Arial; font-size:10pt">The same is true for AutoCAD 2020.1.2 Update and AutoCAD 2021 including all updates.
</span></p><p style="background: white"><span style="color:#3c3c3c; font-family:Arial; font-size:10pt">Was there a specification change?
</span></p><p style="background: white"><span style="color:#3c3c3c; font-family:Arial; font-size:10pt"><strong>Solution
</strong></span></p><p style="background: white"><span style="color:#3c3c3c; font-family:Arial; font-size:10pt">The new feature add-in <a href="https://blogs.autodesk.com/autocad/whats-new-autocad-2019-save-web-mobile/">Save to Autodesk Web and Mobile</a> introduced in AutoCAD is conflicting with other plugins unloading mechanism.
</span></p><p style="background: white"><span style="color:#3c3c3c; font-family:Arial; font-size:10pt">We have two solutions to fix this -
</span></p><p style="background: white"><span style="color:#3c3c3c; font-family:Arial; font-size:10pt">If you are not using the SAVETOWEBMOBILE command, uninstalling the <strong>Save</strong> to <strong>Autodesk Web and Mobile</strong> add-in from the Windows Control Panel.
</span></p><p style="background: white"><img src="/assets/image_733671.jpg" alt=""/><span style="color:#3c3c3c; font-family:Arial; font-size:10pt">
		</span></p><p style="text-align: justify">If you are using then enter the command SAVETOWEBMOBILE on AutoCAD Command Line Interface, it will pop up for update.
</p><p style="text-align: justify">Please go ahead and update.
</p><p style="background: white"><img src="/assets/image_153569.jpg" alt=""/><span style="color:#3c3c3c; font-family:Arial; font-size:10pt">
		</span></p>
