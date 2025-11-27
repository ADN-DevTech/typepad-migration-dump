---
layout: "post"
title: "Questions about InteractionEventHandler::OnSuspend()"
date: "2012-07-10 02:00:42"
author: "Barbara Han"
categories:
  - "Barbara Han"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/07/questions-about-interactioneventhandleronsuspend.html "
typepad_basename: "questions-about-interactioneventhandleronsuspend"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/barbara-han.html" target="_self">Barbara Han</a></p>  <p><strong>Issue</strong></p>  <p>The InteractionEventHandler has the callback OnSuspend, which is called, for example, when the users rotates the view. And this leads to some questions, which were important to know in another CAD system:    <br />- What kind of objects may be changed from actions (commands/functions) which caused such a suspend notification?</p>  <p>- Which actions / commands are cause these notification? </p>  <p>- Is it possible to create such &quot;transparent&quot; commands via API? </p>  <p><strong>Solution</strong>    <br />There is not a list of which commands will cause the suspend behavior, but it is only those that do not cause any change to the model. It seems that the only commands that currently have this behavior are the various viewing commands. So in most cases a programmer does not need react to this event, you may want to hide a dialog that currently has displayed or hide some client graphics, but nothing is required. When you get the OnResume event the only thing within Inventor that could have changed is the view. If you care about the view orientation you should get it again at this point.    <br />&#160; <br />At this time it is not possible yet to create such command that can suspend user interaction using the API. We may allow this in the future, but it is not currently scheduled.</p>
