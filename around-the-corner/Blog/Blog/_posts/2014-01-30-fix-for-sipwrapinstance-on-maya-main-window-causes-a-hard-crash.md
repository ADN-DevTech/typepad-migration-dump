---
layout: "post"
title: "Fix for \"sip.wrapinstance on Maya main window causes a hard crash\""
date: "2014-01-30 06:42:28"
author: "Cyrille Fauvel"
categories:
  - "C++"
  - "Cyrille Fauvel"
  - "Maya"
  - "MotionBuilder"
  - "Python"
  - "Qt"
  - "UI"
  - "Windows"
original_url: "https://around-the-corner.typepad.com/adn/2014/01/fix-for-sipwrapinstance-on-maya-main-window-causes-a-hard-crash.html "
typepad_basename: "fix-for-sipwrapinstance-on-maya-main-window-causes-a-hard-crash"
typepad_status: "Publish"
---

<p>Few month back, I wrote <a href="http://around-the-corner.typepad.com/adn/2013/04/building-sip-and-pyqt-for-maya-2014.html" target="_self">instructions to build PyQT for Maya 2014</a>, but couple of weeks later someone came with a problem in using SIP with Maya. The issue was that Maya is crashing when someone tries to get the Maya handle and bind it using sip. I.e.:</p>
<pre class="brush: python; toolbar: false;">import sip as _sip
import PyQt4.QtCore as _QtCore
import PyQt4.QtGui as _QtGui
import maya.OpenMayaUI as _OpenMayaUI

mainWindow = _sip.wrapinstance(long(_OpenMayaUI.MQtUtil.mainWindow()), _QtCore.QObject)</pre>
<p>Hopefully, a colleague of mine Yves Boudreault investigated the issue and found a fix. The error resides in the sip code that is extracting address into a &quot;unsigned long&quot; instead of &quot;unsigned long long&quot;. I.e.: siplib.c.in</p>
<pre class="brush: cpp; toolbar: false;">/*
 * Wrap an instance.
 */
static PyObject *wrapInstance(PyObject *self, PyObject *args)
{
    unsigned long addr;
    sipWrapperType *wt;

    if (PyArg_ParseTuple(args, &quot;kO!:wrapinstance&quot;, &amp;addr, &amp;sipWrapperType_Type, &amp;wt))
        return sip_api_convert_from_type((void *)addr, wt-&gt;type, NULL);

    return NULL;
}
</pre>
<p>and this is because Maya runs on x64 vs win32. The fix is easy, see below:</p>
<p>Fixed code for 64 bit ( unsigned long&quot; -&gt; &quot;unsigned long long&quot; and &quot;kO!:wrapinstance&quot; -&gt; &quot;KO!:wrapinstance&quot; )</p>
<pre class="brush: cpp; toolbar: false;">/*
 * Wrap an instance.
 */
static PyObject *wrapInstance(PyObject *self, PyObject *args)
{
    unsigned long long addr;
    sipWrapperType *wt;

    if (PyArg_ParseTuple(args, &quot;KO!:wrapinstance&quot;, &amp;addr, &amp;sipWrapperType_Type, &amp;wt))
        return sip_api_convert_from_type((void *)addr, wt-&gt;type, NULL);

    return NULL;
}
</pre>
<p>The fix has been posted on the Riverbank&#39;s pyQt mailing list.</p>
