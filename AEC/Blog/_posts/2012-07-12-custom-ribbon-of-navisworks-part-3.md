---
layout: "post"
title: "Custom Ribbon of Navisworks part 3"
date: "2012-07-12 03:01:20"
author: "Xiaodong Liang"
categories:
  - ".NET"
  - "Navisworks"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/aec/2012/07/custom-ribbon-of-navisworks-part-3.html "
typepad_basename: "custom-ribbon-of-navisworks-part-3"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>(Continued with <a href="http://adndevblog.typepad.com/aec/2012/07/custom-ribbon-of-navisworks-part-2.html">Custom Ribbon of Navisworks part 2</a>)</p>
<p><strong>Locations of xaml and Name File</strong></p>
<p>This plug-in dll should be put under the folder</p>
<p><em>&lt;Navisworks 2012 Installation Path&gt;\Plugins\ADNRibbonDemo</em>. In addition, the xaml and name file must reside in the sub-folder of the plug-in dll. The sub-folder name needs to be a <a href="http://msdn.microsoft.com/en-us/library/ms533052(v=vs.85).aspx">Language Code</a> such as: en-US &gt;&gt; English version, zh-CN &gt;&gt; Chinese (PRC) version.</p>
<p>By default, Navisworks will try to load the xaml and *.name files from the subfolder that corresponds to the current language version. For example, if you are using the Chinese version and there are two sub-folders called en-US and zh-CN. If zh-CN is available and the files do not cause any errors, Navisworks loads the dll with the zh-CN files. Otherwise, Navisworks will check if en-US is available and the files do not cause any problem. If none of files of either version are available or problematic, Navisworks will not load the plug-in dll.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017743490469970d-pi"><img alt="image" border="0" height="204" src="/assets/image_307935.jpg" style="display: inline; border: 0px;" title="image" width="278" /></a></p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0177434904a9970d-pi"><img alt="image" border="0" height="202" src="/assets/image_555068.jpg" style="display: inline; border: 0px;" title="image" width="276" /></a></p>
<p>&#0160;</p>
<p><strong>Priority of Strings </strong></p>
<p>For display names or tooltips, Navisworks check the xaml file first. If it does not specify the strings, it checks the sub-folder for localization and gets names from *.name. If both are not available, it will use the names defined in plug-in class. The advantage of defining button Text in the xaml or name file is that the plug-in can be localized. In addition, the display name of Command can be overridden dynamically by CommandState.OverrideDisplayName in function CanExecuteCommand. For image, Navisworks will check the xaml first, and then the attributes specified in the plug-in class.</p>
<p>e.g. in our sample:</p>
<p>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="font-size: xx-small;">&#0160;Display Names of Tabs</span></p>
<table border="1" cellpadding="0" cellspacing="0">
<tbody>
<tr>
<td width="92">
<p>ribbon elements</p>
</td>
<td width="130">
<p>xaml</p>
</td>
<td width="154">
<p>*.name</p>
</td>
<td width="213">
<p>attributes in compiling code</p>
</td>
<td width="143">
<p>result in ribbon</p>
</td>
</tr>
<tr>
<td width="92">
<p>Tab 1</p>
</td>
<td width="130">&#0160;</td>
<td width="154">
<p>“Tab1 in name file”</p>
</td>
<td width="213">
<p>“Custom Tab 1 - non-localised”</p>
</td>
<td width="143">
<p>“Tab1 in name file”</p>
</td>
</tr>
<tr>
<td width="92">
<p>Tab 2</p>
</td>
<td width="130">
<p>“CustomTab2 in xaml”</p>
</td>
<td width="154">
<p>“Tab2 in name file”</p>
</td>
<td width="213">
<p>“Custom Tab 2 - non-localised”</p>
</td>
<td width="143">
<p>“CustomTab2 in xaml”</p>
</td>
</tr>
</tbody>
</table>
<p>&#0160;</p>
<p><span style="font-size: xx-small;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &#0160;Display Names of Commands</span></p>
<table border="1" cellpadding="0" cellspacing="0">
<tbody>
<tr>
<td width="80">
<p>ribbon elements</p>
</td>
<td width="130">
<p>Dynamic override</p>
</td>
<td width="118">
<p>attributes in compiling code</p>
</td>
<td width="142">
<p>xaml</p>
</td>
<td width="142">
<p>*.name</p>
</td>
<td width="134">
<p>result in ribbon</p>
</td>
</tr>
<tr>
<td width="80">
<p>Button 1</p>
</td>
<td width="130">
<p>“Disable Button3”</p>
<p>or</p>
<p>“Enable Button3”</p>
</td>
<td width="118">&#0160;</td>
<td width="142">
<p>“Button1 in name file”</p>
</td>
<td width="142">
<p>“Button1 in name file”</p>
</td>
<td width="134">
<p>“Disable Button3”</p>
<p>or</p>
<p>“Enable Button3”</p>
</td>
</tr>
<tr>
<td width="80">
<p>Button 2</p>
</td>
<td width="130">&#0160;</td>
<td width="118">&#0160;</td>
<td width="142">&#0160;</td>
<td width="142">&#0160;</td>
<td width="134">&#0160;</td>
</tr>
<tr>
<td width="80">
<p>Button 3</p>
</td>
<td width="130">&#0160;</td>
<td width="118">
<p>“Button 3 non-localized”</p>
</td>
<td width="142">
<p>“button 3”</p>
</td>
<td width="142">
<p>“Button3 in name file”</p>
</td>
<td width="134">
<p>“button 3”</p>
</td>
</tr>
<tr>
<td width="80">
<p>Button 4</p>
</td>
<td width="130">&#0160;</td>
<td width="118">&#0160;</td>
<td width="142">&#0160;</td>
<td width="142">
<p>“Button4 in name file”</p>
</td>
<td width="134">
<p>“Button4 in name file”</p>
</td>
</tr>
<tr>
<td width="80">
<p>Button 5</p>
</td>
<td width="130">&#0160;</td>
<td width="118">&#0160;</td>
<td width="142">&#0160;</td>
<td width="142">
<p>“Button4 in name file”</p>
</td>
<td width="134">
<p>“Button4 in name file”</p>
</td>
</tr>
</tbody>
</table>
<p>&#0160;</p>
<p>Finally you will see the custom ribbon as shown below:</p>
<p>&#0160;</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0167686e0b8d970b-pi"><img alt="image" border="0" height="151" src="/assets/image_306067.jpg" style="display: inline; border: 0px;" title="image" width="571" /></a></p>
<p>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Before toggle Ribbon Tab 2</p>
<p>&#0160;</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017743490535970d-pi"><img alt="image" border="0" height="174" src="/assets/image_328.jpg" style="display: inline; border: 0px;" title="image" width="564" /></a></p>
<p>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; After toggle Ribbon Tab 2&#0160;</p>
<p>Please refer to the sample codes for more details. The sample hard codes the actions of copying the dll, xaml and name file to “<em>C:\Program Files\Autodesk\Navisworks Manage 2012\Plugins</em>” in post-build event. Please adjust with your own path.</p>
<p>&#0160; <span class="asset  asset-generic at-xid-6a0167607c2431970b01b8d135a605970c img-responsive"><a href="http://adndevblog.typepad.com/files/adnribbondemo-2.zip">Download ADNRibbonDemo</a></span><a href="http://adndevblog.typepad.com/files/adnribbondemo.zip"><br /></a></p>
<p><strong>Further reading</strong></p>
<p>The SDK sample called <strong>CustomRibbon</strong> shows some more types of custom buttons.</p>
<p>(End)</p>
