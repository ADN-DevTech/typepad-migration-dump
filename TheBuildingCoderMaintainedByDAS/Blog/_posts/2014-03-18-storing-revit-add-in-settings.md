---
layout: "post"
title: "Storing Revit Add-in Settings"
date: "2014-03-18 06:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "Data Access"
  - "Settings"
  - "Storage"
original_url: "https://thebuildingcoder.typepad.com/blog/2014/03/storing-revit-add-in-settings.html "
typepad_basename: "storing-revit-add-in-settings"
typepad_status: "Publish"
---

<p>Here is another tip from the Revit API discussion forum that seems worthwhile cleaning up and making easy to find, on

<a href="http://forums.autodesk.com/t5/Revit-API/How-to-store-plugin-Preferences/m-p/4844497">
how to store plugin preferences</a>,

raised and discussed by

<a href="http://forums.autodesk.com/t5/user/viewprofilepage/user-id/1951637">
Dimi</a>,

<a href="http://forums.autodesk.com/t5/user/viewprofilepage/user-id/1090898">
peterjegan</a> and

<a href="http://forums.autodesk.com/t5/user/viewprofilepage/user-id/774564">
ollikat</a>:</p>

<p><strong>Question:</strong> I am trying to find a way to store user preferences for my plugin.</p>

<p>These preferences are file paths, import options etc. so that the next time the user runs the plugin, all the choices he made the fist time he used the plugin stay the same.</p>

<p>Are there any guidelines how to achieve this, or API functionality that helps?</p>


<p><strong>Answer:</strong> If the settings you wish to store are Revit project specific, you can save them in the RVT document in

<a href="http://thebuildingcoder.typepad.com/blog/2011/04/extensible-storage.html">
Extensible Storage</a> via

the Revit API. This would fit a scenario where a user wants to have different settings in different projects. Here are some more

<a href="http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.23">
extensible storage topics</a>.</p>

<p>If your settings contain general add-in related information that has no direct relationship with the Revit project, it might be better to use the dedicated Visual Studio C# feature made exactly for this purpose:

<a href="http://msdn.microsoft.com/en-us/library/aa730869(v=vs.80).aspx">
Using settings in C#</a>.

<p>This is very easy and effective system for storing add-in data.</p>

<p>Some people who want more control develop their own system, but I suggest starting here.</p>


<p><strong>Response:</strong> The C#  Visual Studio User Settings system suggested above worked fine.</p>

<p>Nevertheless, in my case, it's even easier, since I am storing user settings for Windows Forms entries.</p>

<p>You can easily associate user settings with Windows Form components inside the Form Designer:

<a href="http://msdn.microsoft.com/en-us/library/wabtadw6%28v=vs.110%29.aspx">
How to: Create Application Settings Using the Designer</a>.</p>

<p>This means even less code for reading and writing.</p>

<p>The only caveat I found is that you have to call save before closing the form, otherwise the settings will be lost:</p>

<pre class="code">
  Properties.Settings.Default.Save();
</pre>
