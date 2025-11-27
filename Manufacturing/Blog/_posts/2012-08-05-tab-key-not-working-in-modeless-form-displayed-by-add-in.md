---
layout: "post"
title: "Tab Key not working in modeless form displayed by add-in"
date: "2012-08-05 22:39:07"
author: "Xiaodong Liang"
categories:
  - "Inventor"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/08/tab-key-not-working-in-modeless-form-displayed-by-add-in.html "
typepad_basename: "tab-key-not-working-in-modeless-form-displayed-by-add-in"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>  <p><b>Issue</b></p>  <p>I have an add-in that displays a modeless form. I have some text boxes on this form. The tab should cause the focus to switch between the controls on the form. Instead the Tab key has no effect. </p>  <p><a name="section2"></a></p>  <p><b>Solution</b></p>  <p>Enter, ESC and CANCEL do not work in modeless dialog is as designed, since your dialog is child of Inventor MainFrame window. The message pump in this case is owned by Inventor. Therefore, all the keystroke messages are taken by the Inventor application and not dispatched to the modeless dialog box. You can workaround this by implementing windows hook WH_GETMESSAGE for modeless dialog. Please refer to following Microsoft KB article. </p>  <p><a title="http://support.microsoft.com/kb/233263" href="http://support.microsoft.com/kb/233263">http://support.microsoft.com/kb/233263</a></p>  <p>Or, display the form as a modal dialog using ShowDialog() instead of Show().&#160; </p>
