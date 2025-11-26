---
layout: "post"
title: "FBMenu stops working after an exception was raised"
date: "2013-10-11 04:07:31"
author: "Cyrille Fauvel"
categories:
  - "Cyrille Fauvel"
  - "MotionBuilder"
  - "Python"
  - "UI"
original_url: "https://around-the-corner.typepad.com/adn/2013/10/fbmenu-stops-working-after-an-exception-was-raised.html "
typepad_basename: "fbmenu-stops-working-after-an-exception-was-raised"
typepad_status: "Publish"
---

<p>When code associated with a custom menu item raises an exception, the menu item can no longer be executed for that session of MotionBuilder.</p>
<p>Steps:</p>
<ol>
<li>Load and execute the script below</li>
<li>From the New Menu, select &#39;SubMenu 1&#39;</li>
<li>Observe in the Python window that an exception is displayed in the output</li>
<li>From the New Menu, select &#39;SubMenu 1&#39;</li>
<li>Observe that nothing is output.</li>
</ol>
<pre class="brush: python; toolbar: false;">from pyfbsdk import *

def eventMenu(control, event):    
    raise Exception(&#39;kill this menu item&#39;)

menuMgr = FBMenuManager()
menuMgr.InsertFirst(None, &quot;New Menu&quot;)
newMenu = menuMgr.GetMenu(&quot;New Menu&quot;)
newMenu.InsertLast(&quot;SubMenu 1&quot;, 11)
newMenu.InsertLast(&quot;SubMenu 2&quot;, 12)

# Registers event handler.
newMenu.OnMenuActivate.Add(eventMenu)</pre>
<p>The problem here is caused by a change in the way MotionBuilder 2014 handles exceptions. If there is an exception in a Python FBEvent Callback, Mobu will automatically unregister this callback to prevent seeing repetitive tracebacks.</p>
<p>There is a message on the MoBu console when Mobu auto removes those &#39;bad&#39; callbacks, something like:
Exception (boost::python::error_already_set) caught, remove Callback!
If you cannot see the console screen on Windows, add the “-console” flag to Mobu’s shortcut.</p>
<p> 
You can restore your unregistered callback by registering it again as a workaround, but I would recommend to put a try/except block around your code to avoid the exception to propagate to MoBu and get your callback removed. This is anyway a good practice to handle errors that way in your code.</p>
