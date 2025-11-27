---
layout: "post"
title: "A Problem with Attribute Helper and a Fix"
date: "2017-02-01 23:20:47"
author: "Adam Nagy"
categories:
  - "Add-In Creation"
  - "Attributes"
  - "Brian"
  - "Utilities"
original_url: "https://modthemachine.typepad.com/my_weblog/2017/02/a-problem-with-attribute-helper-and-a-fix.html "
typepad_basename: "a-problem-with-attribute-helper-and-a-fix"
typepad_status: "Publish"
---

<p>A partner notified me about a problem he had found with my Attribute Helper add-in that I wasn’t aware of. He told me that if you have the new “Interactive Tutorial” and “Attribute Helper” add-ins both loaded, then Inventor will crash when you shut it down with the dialog below.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401bb09742c2e970d-pi"><img alt="CrashDialog" border="0" height="154" src="/assets/image_62724.jpg" style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin-left: auto; display: block; padding-right: 0px; margin-right: auto; border-width: 0px;" title="CrashDialog" width="478" /></a></p>
<p>&#0160;</p>
<p>As I said, I wasn’t aware of the problem but had a good idea of the cause. I wasn’t following my own advice from <a href="http://modthemachine.typepad.com/my_weblog/2016/03/is-your-add-in-causing-problems-in-inventor-2017.html">this post</a> almost a year ago.&#0160; I made the changes recommended there so that my Deactivate method of the add-in went from this:</p>
<div style="font-size: 10pt; font-family: courier new; background: #eeeeee; color: black; line-height: 140%;">
<p>Public Sub Deactivate() Implements Inventor.ApplicationAddInServer.Deactivate<br />&#0160;&#0160;&#0160; <span style="color: #0000ff;"><strong>&#39; Release objects.</strong></span><br />&#0160;&#0160;&#0160; Marshal.FinalReleaseComObject(g_inventorApplication)<br />&#0160;&#0160;&#0160; g_inventorApplication = Nothing<br /><br />&#0160;&#0160;&#0160; If Not m_attributeButtonDef Is Nothing Then<br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Marshal.FinalReleaseComObject(m_attributeButtonDef)<br />&#0160;&#0160;&#0160; End If<br /><br />&#0160;&#0160;&#0160; If Not m_UIEvents Is Nothing Then<br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Marshal.FinalReleaseComObject(m_UIEvents)<br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; m_UIEvents = Nothing<br />&#0160;&#0160;&#0160; End If<br /><br />&#0160;&#0160;&#0160; System.GC.WaitForPendingFinalizers()<br />&#0160;&#0160;&#0160; System.GC.Collect()<br />End Sub</p>
</div>
<p>To this:</p>
<div style="font-size: 10pt; font-family: courier new; background: #eeeeee; color: black; line-height: 140%;">Public Sub Deactivate() Implements Inventor.ApplicationAddInServer.Deactivate<br />&#0160;&#0160;&#0160; <span style="color: #0000ff;"><strong>&#39; Release objects.</strong></span><br />&#0160;&#0160;&#0160; m_attributeButtonDef = Nothing<br />&#0160;&#0160;&#0160; m_UIEvents = Nothing<br />End Sub</div>
<p>&#0160;</p>
<p>With that change, the crash went away. I hadn’t published a new version of the “Attribute Helper” add-in for a couple of years so I’ve made this change and created a new installer.&#0160; You can get the latest version of <a href="http://modthemachine.typepad.com/AttributeHelperSetup_2.6.zip">“Attribute Helper” here</a>.&#0160; After installing make sure that you’re running the latest version by checking that the version is 2.6, as shown below.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401b7c8d10e5e970b-pi"><img alt="AttributeHelperVersion" border="0" height="115" src="/assets/image_254247.jpg" style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin-left: auto; display: block; padding-right: 0px; margin-right: auto; border-width: 0px;" title="AttributeHelperVersion" width="365" /></a></p>
<p>And if attributes are new to you, you can learn more about what they are <a href="http://modthemachine.typepad.com/my_weblog/2009/07/introduction-to-attributes.html">here</a>.&#0160; The source code for the add-in is also available on <a href="https://github.com/brianekins/AttributeHelper/">GitHub</a>.</p>
<p>-Brian</p>
