---
layout: "post"
title: "System.BadImageFormatException on Launch of Revit 2012"
date: "2012-09-19 13:23:19"
author: "Saikat Bhattacharya"
categories:
  - ".NET"
  - "Revit"
  - "Saikat Bhattacharya"
original_url: "https://adndevblog.typepad.com/aec/2012/09/systembadimageformatexception-on-launch-of-revit-2012.html "
typepad_basename: "systembadimageformatexception-on-launch-of-revit-2012"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/saikat-bhattacharya.html">Saikat Bhattacharya</a></p>  <p>An ADN partner, once had a issue reported by their add-in user/customer in which they received an &quot;External Tool Failure&quot; dialog on launching a specific flavor of Revit 2012 with the add-in installed. The exception was of System.BadImageFormatException type. The confusing bit was that the same add-in developed by the partner used to work for the same version of Revit Architecture (RAC) which was installed previously, on the same system.</p>  <p>After careful investigations, it was clear that the reason for this unexpected behavior was the settings in the Revit.exe.config under the &lt;startup useLegacyV2RuntimeActivationPolicy&gt; tag. When the settings were as follows, there was no exception reported:</p>  <p><em>&lt;startup useLegacyV2RuntimeActivationPolicy=&quot;true&quot;&gt;      <br />&lt;supportedRuntime version=&quot;v4.0&quot; /&gt;       <br />&lt;/startup&gt; </em></p>  <p>But when the &lt;supportedRuntime&gt; tag was removed from the Revit.exe.config file, launching of Revit 2012 threw the System.BadImageFormatException error:</p>  <p><em>&lt;startup useLegacyV2RuntimeActivationPolicy=&quot;true&quot;&gt;      <br />&lt;/startup&gt; </em></p>  <p>On further investigations, it was evident that setting the value of the useLegacyV2RuntimeActivationPolicy to .NET framework 2.0 (or not providing any value like has been mentioned above), throws the BadImageFormatException for any Revit plug-in targeted to work with Revit with target framework 4.0. </p>  <p>The <em>RevitVSTAConfig.exe </em>tool that is shipped with Revit 2012 helps in toggling between .NET 2 and .NET 4 by editing the xml in the config file. This toggle helps prepare Revit to work with VSTA (by changing the runtime framework to 2.0) and undo the change as well – when Revit needs to work with the installed add-ins which target .NET framework 4.0. </p>  <p>The following setting is the default setting in Revit.exe.config file:</p>  <p><em>&lt;startup useLegacyV2RuntimeActivationPolicy=&quot;true&quot;&gt;      <br />&lt;supportedRuntime version=&quot;v4.0&quot;/&gt;       <br />&lt;/startup&gt; </em></p>
