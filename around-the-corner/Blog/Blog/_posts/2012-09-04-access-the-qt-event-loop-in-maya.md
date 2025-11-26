---
layout: "post"
title: "Access the Qt event loop in Maya"
date: "2012-09-04 23:00:00"
author: "Cyrille Fauvel"
categories:
  - "C++"
  - "Cyrille Fauvel"
  - "Maya"
  - "Python"
  - "Qt"
  - "UI"
original_url: "https://around-the-corner.typepad.com/adn/2012/09/access-the-qt-event-loop-in-maya.html "
typepad_basename: "access-the-qt-event-loop-in-maya"
typepad_status: "Publish"
---

<p>Maya uses a customized 
version of Nokia&#39;s Open Source Qt 4.7.1 framework (for Maya 2013). </p>
<p>If you want to use Qt 
in your Maya plug-ins then you will have to use the same modified version as 
Maya. Most of the Qt modified libs ship with the Maya distribution (located in $MAYA_LOCATION/lib). So in theory you would not need build the libraries yourself unless you need more. Then it will require that you build a local copy of Qt from the customized 
source. </p>
<p>A copy of the 
customized Qt 4.7.1 source is available from Autodesk&#39;s Open Source web-site (<a href="http://www.autodesk.com/lgplsource" target="_blank">http://www.autodesk.com/lgplsource</a>) whereas the compressed file in the Maya distribution (the compressed file in the $MAYA_LOCATION/include/Qt folder) contains header file only. The WEB version includes text files 
describing how to configure, build and install Qt for each platform supported by 
Maya.</p>
<p>
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017c3189af9b970b-pi" style="display: inline;"><img alt="Qt4logo" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017c3189af9b970b" src="/assets/image_8dc4d8.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Qt4logo" width="150" /></a></p>
<p>I would also encourage you to read <a href="http://around-the-corner.typepad.com/adn/2012/07/all-things-qt.html" target="_self">Kristine&#39;s article</a> on Qt, PyQt, and PySide before anything.</p>
<p>If you were writing 
your own Qt application from scratch, you would need to create your own 
QCoreApplication or QApplication instance to handle your application&#39;s event 
loop. When writing a Maya plug-in, you must instead use Maya&#39;s own application 
object which can be retrieved using Qt&#39;s <em>qApp</em> macro.</p>
<pre class="brush: cpp">#include &lt;QtCore/QCoreApplication&gt;
QCoreApplication *app = qApp;</pre>
<p>From there you can use the Qt installEventFilter() method to trap all Application event.</p>
<pre class="brush: cpp">void QObject::installEventFilter (QObject *filterObj)</pre>
<p>In
standalone mode, Maya does only create a basic QCoreApplication. I.e. no
GUI, that is the most that Maya will do in batch mode. But Maya will nicely use any Qt application already running, so you need to create your
own Qt appplication before initializing Maya standalone libraries and then you can access the event loop as usual.</p>
<p>
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d01761784b84f970c-pi" style="display: inline;"><img alt="Logo_faq_tiny_50_pyqt" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d01761784b84f970c" src="/assets/image_838c8a.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Logo_faq_tiny_50_pyqt" /></a></p>
<table>
<tbody>
<tr>
<td><a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017c318d9569970b-pi" style="display: inline;"></a>
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017c318d9569970b-pi" style="display: inline;"><img alt="Pyside-rounded-corners" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017c318d9569970b" src="/assets/image_0cb584.jpg" title="Pyside-rounded-corners" width="80" /></a></td>
<td>Note on PySide - while you will need to compile the PySide libraries yourself before using it, PySide solution is not different from what was written in C++ above. Same approach / same code almost. Before you ask the question - why Autodesk does not provide the compiled PySide libraries? To keep it simple, that is because of a legal reason.</td>
</tr>
<tr>
<td><a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017c318d9e84970b-pi" style="display: inline;"><img alt="Pyqt" class="asset  asset-image at-xid-6a0163057a21c8970d017c318d9e84970b" src="/assets/image_efed74.jpg" style="width: 80px;" title="Pyqt" /></a><br /></td>
<td>Same for PyQt, the code would not be much different, but you will still need to compile the PyQt libraries yourself.</td>
</tr>
</tbody>
</table>
