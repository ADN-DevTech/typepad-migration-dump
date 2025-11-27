---
layout: "post"
title: "Using ETO Template in Visual Studio - getting License Error"
date: "2012-08-17 17:30:12"
author: "Wayne Brill"
categories:
  - "Inventor"
  - "Wayne Brill"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/08/using-eto-template-in-visual-studio-getting-license-error.html "
typepad_basename: "using-eto-template-in-visual-studio-getting-license-error"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/wayne-brill.html" target="_self">Wayne Brill</a></p>  <p>Applies to:    <br />Autodesk® Inventor® Engineer-to-Order Series</p>  <p><b>Issue</b></p>  <p>In VS 2010 I am trying to use the ETO Design Library template. I am getting a Licensing Error &quot;Unable to initialize adlm&quot;. I close that error and then a Object reference not set to an instance of an object&quot; appears. What can I do to resolve these errors so I can use the ETO template? </p>  <p><a name="section2"></a></p>  <p><b>Solution</b></p>  <p>The problem can be resolved by adding the ETO bin directory to the system path. (adlmint.dll and adlmint_libFNP.dll are not being found).</p>  <p>On XP go System Properties, Advanced Tab and then Environment Variables. Edit the Path variable and add the ETO Components bin directory. It will be something similar to this &quot;c:\program files\Autodesk\Inventor ETO Components 2012\bin&quot;.</p>  <p>Note: the paths in the System path are separated by semi-colons. ;</p>
