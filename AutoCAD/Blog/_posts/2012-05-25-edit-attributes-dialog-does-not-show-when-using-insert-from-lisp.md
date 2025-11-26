---
layout: "post"
title: "Edit Attributes dialog does not show when using INSERT from Lisp"
date: "2012-05-25 10:49:31"
author: "Wayne Brill"
categories:
  - "2012"
  - "AutoCAD"
  - "Wayne Brill"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/edit-attributes-dialog-does-not-show-when-using-insert-from-lisp.html "
typepad_basename: "edit-attributes-dialog-does-not-show-when-using-insert-from-lisp"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/wayne-brill.html" target="_self">Wayne Brill</a></p>  <p><b>Issue</b></p>  <p>I have the following Lisp code and it used to work fine - i.e. it showed the Edit Attributes dialog when a block with attributes was being inserted.</p>  <pre>(defun c:myinsert ( / )
  (setvar &quot;ATTREQ&quot; 1)
  (setvar &quot;ATTDIA&quot; 1)
  (initdia)
  (command &quot;-insert&quot; &quot;MyBlock&quot; &quot;0,0&quot; 1 1 0.0)  
)</pre>

<p>However, in AutoCAD 2012 the functionality seems broken as only the command line version of Edit Attributes is available.</p>

<p>How could I make the Edit Attributes dialog appear?</p>

<p><a name="section2"></a></p>

<p><b>Solution</b></p>

<p>This behavior has been reported and hopefully the fix will be available in the first service pack of AutoCAD 2012.</p>

<p>You may find the following workaround acceptable in the meantime:</p>

<pre>(defun c:myinsert ( / attreq  )
  (setq attreq  (getvar &quot;ATTREQ&quot;))
  (setvar &quot;ATTREQ&quot; 0)
  (command &quot;_.-insert&quot; &quot;myblock&quot; &quot;0,0&quot; 1 1 0)
  (setvar &quot;ATTREQ&quot; attreq)
  (if (= attreq 1)
      (command &quot;_ddatte&quot; (entlast))
  )
)</pre>
