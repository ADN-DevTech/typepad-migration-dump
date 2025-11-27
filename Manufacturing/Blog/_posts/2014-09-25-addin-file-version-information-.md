---
layout: "post"
title: "*.addin file version information "
date: "2014-09-25 08:10:39"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/09/addin-file-version-information-.html "
typepad_basename: "addin-file-version-information-"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>The <strong>*.addin</strong> file that enables <strong>Inventor</strong> to find your <strong>AddIn dll</strong> and provides further information about it also specifies which <strong>Inventor</strong> versions the <strong>dll</strong> should be loaded into using one of the <strong>SupportedSoftwareVersionXxx&#0160;</strong>tags.</p>
<p>The version info in the <strong>*.addin</strong> files follows the same logic that was used when the AddIn&#39;s still had to be listed in the registry. Here is some information about that from an older&#0160;<strong>Inventor API Help</strong> file: &#0160;</p>
<p><strong>Add-In Versioning</strong></p>
<p>Most Add-In&#39;s need to confine their use to specific versions of Inventor. For example, an Add-In that takes advantage of any of the new API features in <strong>Inventor 6</strong> will not work in <strong>Inventor 5.3</strong>. Add-In versioning allows the Add-In writer to specify which versions of Inventor the Add-In will be loaded in.</p>
<p>The versioning information is specified in the registry as a value within the Settings key. The names of these String values can be one or more of the following:</p>
<ul>
<li><strong>SupportedSoftwareVersionLessThan</strong>&#0160;</li>
<li><strong>SupportedSoftwareVersionGreaterThan</strong>&#0160;</li>
<li><strong>SupportedSoftwareVersionEqualTo</strong>&#0160;</li>
<li><strong>SupportedSoftwareVersionNotEqualTo</strong></li>
</ul>
<p>The string associated with this value defines the version number. You can specify version numbers using two different forms:<br /> [<strong>Major</strong>].[<strong>Minor</strong>].[<strong>ServicePack</strong>] or [<strong>BuildIdentifier</strong>]</p>
<p>The following are all valid version numbers:</p>
<ul>
<li>&quot;<strong>5.3.2</strong>&quot; - Specifies <strong>5.3 SP2</strong>&#0160;</li>
<li>&quot;<strong>6..</strong>&quot; - Specifies <strong>any version of Inventor 6</strong>&#0160;</li>
<li>&quot;<strong>..2</strong>&quot; - Specifies <strong>any</strong> <strong>service pack 2</strong>. (This isn&#39;t very useful but just demonstrates that any of the Major, Minor, or ServicePack numbers are optional.&#0160;</li>
<li>&quot;<strong>6077000</strong>&quot; - Specifies <strong>build 6077</strong>.</li>
</ul>
<p>The <strong>SupportedSoftwareVersionEqualTo</strong> and <strong>SupportedSoftwareVersionNotEqualTo</strong> values can support multiple version numbers by separating them by a semicolon. For example, <strong>SupportedSoftwareVersionEqualTo</strong> = &quot;<strong>5.3.0;5.3.2</strong>&quot; will only support <strong>5.3</strong> and <strong>5.3 SP2</strong>).</p>
