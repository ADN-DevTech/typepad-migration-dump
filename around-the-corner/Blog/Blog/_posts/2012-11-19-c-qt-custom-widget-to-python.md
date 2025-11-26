---
layout: "post"
title: "C++ Qt custom widget to Python"
date: "2012-11-19 12:06:43"
author: "Cyrille Fauvel"
categories:
  - "C++"
  - "Cyrille Fauvel"
  - "Maya"
  - "MEL"
  - "Plug-in"
  - "Python"
  - "Qt"
  - "UI"
  - "Windows"
original_url: "https://around-the-corner.typepad.com/adn/2012/11/c-qt-custom-widget-to-python.html "
typepad_basename: "c-qt-custom-widget-to-python"
typepad_status: "Publish"
---

<p>While it is rather easy to create a composite widget using Python and/or a true custom widget using Python (see examples in PyQt distribution /examples/designer/plugins/widgets), it becomes very unintuitive when you use C++ :(</p>
<p>For this post we will be using a true custom Widget and go step by step on what you need to do to get it working and exposed in Maya. For this we will be using a clock (with seconds)</p>
<p>
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017ee56508f9970d-pi" style="display: inline;"><img alt="Clock" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017ee56508f9970d" src="/assets/image_87e856.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Clock" /></a></p>
<p>The first thing is to create the Widget itself. That step is pretty straight forward. Create your .h and .cpp file to make up your control.</p>
<pre class="brush:cpp; toolbar:false">//-----------------------------------------------------------------------------
#pragma once

#include &lt;QWidget&gt;
#include &lt;QtDesigner/QDesignerExportWidget&gt;

class QDESIGNER_WIDGET_EXPORT FullAnalogClock : public QWidget {
	Q_OBJECT

public:
	FullAnalogClock (QWidget *parent =NULL) ;

protected:
	virtual void paintEvent (QPaintEvent *event) ;

} ;</pre>
<p>What is important here is to note the presence of </p>
<pre>QDESIGNER_WIDGET_EXPORT</pre>
<p>This one is important if you want your control to be available and usable in the QtDesigner tool.</p>
<p>Because I prefer to avoid using qmake, I do include the metadata moc file and the end of my implementation file and generate it as a step rule in Visual Studio. But at this stage, it is perfectly ok to build your C++ control the Qt way...
</p>
<pre class="brush:cpp; toolbar:false">//-----------------------------------------------------------------------------
#include &quot;StdAfx.h&quot;
#include &quot;FullAnalogClock.h&quot;

FullAnalogClock::FullAnalogClock (QWidget *parent) : QWidget(parent) {
	QTimer *timer =new QTimer (this) ;
	connect (timer, SIGNAL(timeout ()), this, SLOT(update ())) ;
	timer-&gt;start (1000) ;
	setWindowTitle (tr(&quot;Analog Clock&quot;)) ;
	resize (200, 200) ;
	setVisible (true) ;
	setGeometry (100, 100, 200, 200);
}

void FullAnalogClock::paintEvent (QPaintEvent *) {
	static const QPoint hourHand [3] ={
		QPoint (7, 8),
		QPoint (-7, 8),
		QPoint (0, -40)
	} ;
	static const QPoint minuteHand [3] ={
		QPoint (7, 8),
		QPoint (-7, 8),
		QPoint (0, -70)
	} ;
	static const QPoint secondHand [2] ={
		QPoint (0, 8),
		QPoint (0, -70)
	} ;
	QColor hourColor (127, 0, 127) ;
	QColor minuteColor (0, 127, 127, 191) ;
	QColor secondColor (127, 127, 0, 191) ;
	int side =qMin (width (), height ()) ;
	QTime time =QTime::currentTime () ;

	QPainter painter (this) ;
	painter.setRenderHint (QPainter::Antialiasing) ;
	painter.translate (width () / 2, height () / 2) ;
	painter.scale (side / 200.0, side / 200.0) ;
	// Hour
	painter.setPen (Qt::NoPen) ;
	painter.setBrush (hourColor) ;
	painter.save () ;
	painter.rotate (30.0 * ((time.hour () + time.minute () / 60.0))) ;
	painter.drawConvexPolygon (hourHand, 3) ;
	painter.restore () ;
	painter.setPen (hourColor) ;
	for ( int i =0 ; i &lt; 12 ; ++i ) {
		painter.drawLine (88, 0, 96, 0) ;
		painter.rotate (30.0) ;
	}
	// Minute
	painter.setPen (Qt::NoPen) ;
	painter.setBrush (minuteColor) ;
	painter.save () ;
	painter.rotate (6.0 * (time.minute () + time.second () / 60.0)) ;
	painter.drawConvexPolygon (minuteHand, 3) ;
	painter.restore () ;
	painter.setPen (minuteColor) ;
	for ( int j =0 ; j &lt; 60 ; ++j ) {
		if ( (j % 5) != 0 )
			painter.drawLine (92, 0, 96, 0) ;
		painter.rotate (6.0) ;
	}
	// Second
	painter.setPen (secondColor) ;
	//painter.setBrush (secondColor) ;
	painter.setBrush (Qt::NoBrush) ;
	painter.save () ;
	painter.rotate (6.0 * time.second ()) ;
	painter.drawLine (secondHand [0], secondHand [1]) ;
	//painter.drawConvexPolygon (minuteHand, 3) ;
	painter.restore () ;
}

#include &quot;FullAnalogClock.moc&quot;
</pre>
<p>Now, the first thing that we prepared for, is to support the QtDesigner in case you want to be able to add, edit your control in a GUI environment. To do this, we need to make your DLL a Qt Plug-in. To do that, you need to have these symbols defined.</p>
<pre class="brush:cpp; toolbar:false">#define QT_PLUGIN
#define QT_SHARED
#define QT_NO_DEBUG 1
#define QDESIGNER_EXPORT_WIDGETS</pre>
<p>and have a Qt plug-in class defined</p>
<pre class="brush:cpp; toolbar:false">//-----------------------------------------------------------------------------
#pragma once

#include &lt;QtDesigner/QDesignerCustomWidgetInterface&gt;

class FullAnalogClockPlugin : public QObject, public QDesignerCustomWidgetInterface {
	Q_OBJECT
	Q_INTERFACES(QDesignerCustomWidgetInterface)
private:
	bool initialized ;

public:
	FullAnalogClockPlugin (QObject *parent =NULL) ;

	bool isContainer () const ;
	bool isInitialized () const ;
	QIcon icon () const ;
	QString domXml () const ;
	QString group () const ;
	QString includeFile () const ;
	QString name () const ;
	QString toolTip () const ;
	QString whatsThis () const ;
	QWidget *createWidget (QWidget *parent) ;
	void initialize (QDesignerFormEditorInterface *core) ;

} ;</pre>
<p>While the implementation of that class isn&#39;t very difficult there are few thing important like the name(), group() members which will tell the QtDesigner how to categorize your control in its UI. And the macro Q_EXPORT_PLUGIN2 to make your DLL a Qt plug-in.</p>
<pre class="brush:cpp; toolbar:false">//-----------------------------------------------------------------------------
#include &quot;StdAfx.h&quot;
#include &quot;FullAnalogClock.h&quot;
#include &quot;FullAnalogClockPlugin.h&quot;

#include &lt;QtPlugin%gt;

//-----------------------------------------------------------------------------
FullAnalogClockPlugin::FullAnalogClockPlugin (QObject *parent) : QObject(parent) {
	initialized =false ;
}

void FullAnalogClockPlugin::initialize (QDesignerFormEditorInterface * /* core */) {
	if ( initialized )
		return ;
	initialized =true ;
}

bool FullAnalogClockPlugin::isInitialized () const {
    return initialized ;
}

QWidget *FullAnalogClockPlugin::createWidget (QWidget *parent) {
	return new FullAnalogClock (parent) ;
}

QString FullAnalogClockPlugin::name () const {
	return &quot;FullAnalogClock&quot; ;
}

QString FullAnalogClockPlugin::group () const {
	return &quot;Display Widgets&quot; ;
}

QIcon FullAnalogClockPlugin::icon () const {
	return QIcon () ;
}

QString FullAnalogClockPlugin::toolTip () const {
	return &quot;FullAnalogClock clock&quot; ;
}

QString FullAnalogClockPlugin::whatsThis () const {
	return &quot;FullAnalogClock clock&quot; ;
}

bool FullAnalogClockPlugin::isContainer () const {
	return false ;
}

QString FullAnalogClockPlugin::domXml () const {
	return &quot;&lt;ui language=\&quot;c++\&quot;&gt;\n&quot;
		&quot; &lt;widget class=\&quot;FullAnalogClock\&quot; name=\&quot;FullAnalogClock\&quot;&gt;\n&quot;
		&quot;  &lt;property name=\&quot;geometry\&quot;&gt;\n&quot;
		&quot;   &lt;rect&gt;\n&quot;
		&quot;    &lt;x&gt;0&lt;/x&gt;\n&quot;
		&quot;    &lt;y&gt;0&lt;/y&gt;\n&quot;
		&quot;    &lt;width&gt;100&lt;/width&gt;\n&quot;
		&quot;    &lt;height&gt;100&lt;/height&gt;\n&quot;
		&quot;   &lt;/rect&gt;\n&quot;
		&quot;  &lt;/property&gt;\n&quot;
		&quot; &lt;/widget&gt;\n&quot;
		&quot;&lt;/ui&gt;&quot;;
}

QString FullAnalogClockPlugin::includeFile () const {
	return &quot;FullAnalogClock.h&quot; ;
}

Q_EXPORT_PLUGIN2(FullAnalogClockPlugin, FullAnalogClockPlugin)

#include &quot;FullAnalogClockPlugin.moc&quot;</pre>
<p>Build and copy your resulting dll into the Maya or your Qt &#39;qt-plugins\designer&#39; directory. And it should appear and work into your QtDesigner. It will also work if you create a .ui file using QtDesginer and load that .ui in Maya using the loadUI MEL command, I.e.:</p>
<pre class="brush:cpp; toolbar:false">$uiName= `loadUI -uiFile &quot;e:/clock.ui&quot;`;
showWindow $uiName;
</pre>
<p>and/or</p>
<pre class="brush:cpp; toolbar:false">$uiName= `loadUI -uiFile &quot;e:/clock.ui&quot;`;
dockControl -allowedArea &quot;all&quot; -area &quot;right&quot; -floating off -content $uiName -label &quot;Custom Dock1&quot;;
</pre>
<p>Now this is where it becomes tricky. Ok, but what about Python? if you use the Maya command loadUI, no change, but if you decide to use PyQt for example - that won&#39;t work as is. This is because PyQt needs a sip wrapper to work with Qt Widget written in C++. See this <a href="http://around-the-corner.typepad.com/adn/2012/10/building-qt-pyqt-pyside-for-maya-2013.html" target="_self">post</a> for more details.</p>
<p>The first thing is to create a .sip file</p>
<pre class="brush:cpp; toolbar:false">// Define the SIP wrapper to the FullAnalogClock library.

%Module(name=FullAnalogClock, keyword_arguments=&quot;Optional&quot;) 

%Import QtGui/QtGuimod.sip

%If (Qt_4_7_1 -)

class FullAnalogClock : public QWidget {

%TypeHeaderCode
#include &lt;FullAnalogClock.h&gt;
%End

public:
	FullAnalogClock(QWidget *parent /TransferThis/ = 0);
protected:
	virtual void resizeEvent(QResizeEvent *);
	virtual void paintEvent(QPaintEvent *e);
};
%End
</pre>
<p>Beyond the %Module, %Import - what is really important is to have the 2 virtuals resizeEvents() and paintEvents() present. Otherwise your control will not appear as SIP won&#39;t be able to make the connection from PyQt.</p>
<p>Next is to create a config file to build the SIP wrapper.</p>
<pre class="brush:python; toolbar:false">import os
import sipconfig
from PyQt4 import pyqtconfig

# The name of the SIP build file generated by SIP and used by the build
# system.
build_file = &quot;FullAnalogClock.sbf&quot;

# Get the PyQt configuration information.
config = pyqtconfig.Configuration()

# Get the extra SIP flags needed by the imported PyQt modules.  Note that
# this normally only includes those flags (-x and -t) that relate to SIP&#39;s
# versioning system.
pyqt_sip_flags = config.pyqt_sip_flags

# Run SIP to generate the code.  Note that we tell SIP where to find the qt
# module&#39;s specification files using the -I flag.
##os.system(&quot; &quot;.join([config.sip_bin, &quot;-c&quot;, &quot;.&quot;, &quot;-b&quot;, build_file, &quot;-I&quot;, config.pyqt_sip_dir, pyqt_sip_flags, &quot;FullAnalogClock.sip&quot;]))
config.sip_bin =&quot;s:\\Python\\sip&quot;
test =&quot; &quot; . join([config.sip_bin, &quot;-c&quot;, &quot;.&quot;, &quot;-b&quot;, build_file, &quot;-I&quot;, &quot;\&quot;&quot; + config.pyqt_sip_dir + &quot;\&quot;&quot;, pyqt_sip_flags, &quot;FullAnalogClock.sip&quot;])
os.system (test)

# We are going to install the SIP specification file for this module and
# its configuration module.

installs = []
installs.append([&quot;FullAnalogClock.sip&quot;, os.path.join(config.default_sip_dir, &quot;FullAnalogClock&quot;)])
installs.append([&quot;FullAnalogClockconfig.py&quot;, config.default_mod_dir])

# Create the Makefile.  The QtGuiModuleMakefile class provided by the
# pyqtconfig module takes care of all the extra preprocessor, compiler and
# linker flags needed by the Qt library.
makefile = pyqtconfig.QtGuiModuleMakefile(
    configuration=config,
    build_file=build_file,
    installs=installs
)

# Add the library we are wrapping.  The name doesn&#39;t include any platform
# specific prefixes or extensions (e.g. the &quot;lib&quot; prefix on UNIX, or the
# &quot;.dll&quot; extension on Windows).
makefile.extra_libs = [&quot;FullAnalogClock&quot;]
#makefile.CFLAGS.append(&quot;-l..&quot;)
#makefile.LFLAGS.append(&quot;-L../x64/Release&quot;)
# Generate the Makefile itself.
makefile.generate()

# Now we create the configuration module.  This is done by merging a Python
# dictionary (whose values are normally determined dynamically) with a
# (static) template.
content = {
    # Publish where the SIP specifications for this module will be
    # installed.
    &quot;FullAnalogClock_sip_dir&quot;:    config.default_sip_dir,

    # Publish the set of SIP flags needed by this module.  As these are the
    # same flags needed by the qt module we could leave it out, but this
    # allows us to change the flags at a later date without breaking
    # scripts that import the configuration module.
    &quot;FullAnalogClock_sip_flags&quot;:  pyqt_sip_flags
}

# This creates the FullAnalogClockconfig.py module from the FullAnalogClockconfig.py.in
# template and the dictionary.
sipconfig.create_config_module(&quot;FullAnalogClockconfig.py&quot;, &quot;FullAnalogClockconfig.py.in&quot;, content)
</pre>
<p>You also need a FullAnalogClockconfig.py.in, but this one can be empty or contains the following:</p>
<pre class="brush:python; toolbar:false">from PyQt4 import pyqtconfig

# These are installation specific values created when FullAnalogClock was configured.
# The following line will be replaced when this template is used to create
# the final configuration module.
# @SIP_CONFIGURATION@

class Configuration(pyqtconfig.Configuration):
    &quot;&quot;&quot;The class that represents FullAnalogClock configuration values.
    &quot;&quot;&quot;
    def __init__(self, sub_cfg=None):
        &quot;&quot;&quot;Initialise an instance of the class.

        sub_cfg is the list of sub-class configurations.  It should be None
        when called normally.
        &quot;&quot;&quot;
        # This is all standard code to be copied verbatim except for the
        # name of the module containing the super-class.
        if sub_cfg:
            cfg = sub_cfg
        else:
            cfg = []

        cfg.append(_pkg_config)

        pyqtconfig.Configuration.__init__(self, cfg)

class FullAnalogClockModuleMakefile(pyqtconfig.QtGuiModuleMakefile):
    &quot;&quot;&quot;The Makefile class for modules that %Import FullAnalogClock.
    &quot;&quot;&quot;
    def finalise(self):
        &quot;&quot;&quot;Finalise the macros.
        &quot;&quot;&quot;
        # Make sure our C++ library is linked.
        self.extra_libs.append(&quot;FullAnalogClock&quot;)

        # Let the super-class do what it needs to.
        pyqtconfig.QtGuiModuleMakefile.finalise(self)
</pre>
<p>and last do the build</p>
<pre class="brush:cpp; toolbar:false">@echo off

set MAYA_LOCATION=C:\Program Files\Autodesk\Maya2013.5
set SIP=E:\sip-4.13.3
set PYQT=E:\PyQt-win-gpl-4.9.4
set MSVC_DIR=C:\Program Files (x86)\Microsoft Visual Studio 10.0
set MSVC_VERSION=2010
set QTDIR=v:\qt-adsk-4.7.1
set QMAKESPEC=win32-msvc%MSVC_VERSION%

subst s: /d
subst s: &quot;%MAYA_LOCATION%&quot;
set MAYA_LOCATION=s:

subst v: /d
subst v: &quot;E:\__sdkext\_Maya2013-5 Scripts&quot;

call &quot;%MSVC_DIR%\VC\vcvarsall.bat&quot; amd64

set INCLUDE=%INCLUDE%;%MAYA_LOCATION%\include\python2.6;%MAYA_LOCATION%\Python\include;%MAYA_LOCATION%\include\Qt;%MAYA_LOCATION%\include\QtCore;%MAYA_LOCATION%\include\QtGui;..
set LIB=%LIB%;%MAYA_LOCATION%\lib;..\..\Movie\x64\Release

e:
cd &quot;E:\Projects\CP2520\AnalogClock\sip&quot;
&quot;%MAYA_LOCATION%\bin\mayapy&quot; ./configure.py 

nmake
nmake install
</pre>
<p>Now go and run in Maya, you&#39;re done. The FullAnalogClock.pyd and FullAnalogClockconfig.py should have been copied into the Maya &#39;Python/Lib/site-packages&#39; directory. One little thing when you in Maya, you still need to teach Maya where to the find the dll. The following code should so it simply:</p>
<pre class="brush:python; toolbar:false">import sys, os
sys.path.append(&quot;C:/Program Files/Autodesk/Maya2013.5/qt-plugins/designer&quot;)
os.environ[&#39;PATH&#39;] =os.environ[&#39;PATH&#39;] + &quot;;C:/Program Files/Autodesk/Maya2013.5/qt-plugins/designer&quot;
</pre>
<p>The full code:</p>
<pre class="brush:python; toolbar:false">import sys, os
sys.path.append(&quot;C:/Program Files/Autodesk/Maya2013.5/qt-plugins/designer&quot;)
os.environ[&#39;PATH&#39;] =os.environ[&#39;PATH&#39;] + &quot;;C:/Program Files/Autodesk/Maya2013.5/qt-plugins/designer&quot;

import sip
from PyQt4 import QtCore, QtGui
import FullAnalogClock

import os, re, sys
import maya.cmds as cmds
import maya.OpenMayaUI as mui

global app
global videop

class videoPlayer(QtGui.QDialog):
	def __init__(self, parent  = None):
		QtGui.QWidget.__init__( self, parent )
		self.parent = parent
		self.setSizePolicy(QtGui.QSizePolicy.Expanding, QtGui.QSizePolicy.Preferred)
		self.resize(500,500)

		self.clock = FullAnalogClock.FullAnalogClock(self)
		self.clock.resize(100, 100)
		self.clock.setVisible(True)

		layout = QtGui.QHBoxLayout(self)
		layout.addWidget(self.clock)
		self.setLayout(layout)

def test():
	global app
	app = QtGui.QApplication.instance()
	ptr = mui.MQtUtil.mainWindow()
	win = sip.wrapinstance(long(ptr), QtCore.QObject)

	global videop
	videop = videoPlayer(win)
	videop.show()
	return videop

test()
</pre>
<p>And you&#39;ll get the following :)</p>
<p style="text-align: center;">
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017d3df06dca970c-pi" style="display: inline;"><img alt="Clock-result" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017d3df06dca970c image-full" src="/assets/image_abc0f9.jpg" title="Clock-result" /></a><br /><em>Result with a a clock created using SIP/PyQt code (top)</em><br /><em> and using the .ui &amp; MEL command loadUI (bottom)</em></p>
<p>&#0160;</p>
