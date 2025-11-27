---
layout: "post"
title: "The right way to show modal and modeless dialogs in AutoCAD using .NET"
date: "2008-08-07 17:21:16"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "User interface"
original_url: "https://www.keanw.com/2008/08/the-right-way-t.html "
typepad_basename: "the-right-way-t"
typepad_status: "Publish"
---

<p>My manager is currently sailing back from Honolulu to San Francisco, after his boat came 2nd in its division in the <a href="http://pacificcup.org/">2008 Pacific Cup</a> (congratulations, Jim! :-), so I'm spending more time on management-related activities than I would normally. Which means I'm getting less time to spend on the fun stuff, such as researching blog posts: I'm plundering what I can from my email archives, but my output may feel a little thin over the next week or two.</p>

<p>Here's some information from an internal discussion that I thought might be of general interest.</p>

<p>The question:</p><blockquote><p><em>Why is it important to use Application.ShowModalDialog() or Application.ShowModelessDialog()? The documentation for both these commands says &quot;You must use this method instead of Form.Show[Dialog], which may lead to unexpected behavior.&quot; Any pointers as to what the &quot;unexpected behavior&quot; might be?</em></p></blockquote><p>Here's the answer, provided by a member of the AutoCAD Engineering team:</p><blockquote><p><em>There are a number of reasons to use Application.Show[Modal/Modeless]Dialog() rather than Form.Show[Dialog]:</em></p></blockquote><ol><ol><li><em>Dialogs will automatically pick up the icon of the host product</em></li>

<li><em>Dialog size and position will be persisted automatically</em></li>

<li><em>Other floating AutoCAD windows (e.g. the Properties palette) will be disabled, as needed</em></li>

<li><em>The DIASTAT system variable will be set properly with the exit status of the dialog</em></li></ol></ol>
