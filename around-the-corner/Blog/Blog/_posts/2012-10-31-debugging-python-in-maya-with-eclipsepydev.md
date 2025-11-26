---
layout: "post"
title: "Debugging Python in Maya with Eclipse/Pydev"
date: "2012-10-31 06:50:00"
author: "Cyrille Fauvel"
categories:
  - "Cyrille Fauvel"
  - "Debugging"
  - "Linux"
  - "Mac"
  - "Maya"
  - "Python"
  - "Test"
  - "Tools"
  - "Windows"
original_url: "https://around-the-corner.typepad.com/adn/2012/10/debugging-python-in-maya-with-eclipsepydev.html "
typepad_basename: "debugging-python-in-maya-with-eclipsepydev"
typepad_status: "Publish"
---

<p>This post is a follow-up on the previous post <a href="http://around-the-corner.typepad.com/adn/2012/10/python-debugging-in-maya.html" target="_self">here</a>.</p>
<h2>Installing Eclipse &amp; Pydev</h2>
<p><strong>Note:</strong> On Windows: depending on whether you are on a 64 or 32
bit machine, make sure you install the correct version of Java and Eclipse.</p>
<ul>
<li>Install Java on your
machine if it isn’t yet done <a href="http://www.java.com/">http://www.java.com/<br /></a><a href="http://www.java.com/en/download/ie_manual.jsp?locale=en&amp;host=www.java.com:80">http://www.java.com/en/download/ie_manual.jsp?locale=en&amp;host=www.java.com:80<br /><br /></a></li>
<li>Go to&#0160;<a href="http://www.eclipse.org/downloads/">http://www.eclipse.org/downloads/</a> and then click ‘Eclipse Packages&#39;<br /><br />Choose any of the last ‘Packages’ version. It is not really important which one you download, could be the Java, Classic or C++ -it does not yet matter. Personally, I have chosen the C/C++ package and combined it later with the Java and PHP packages. These packages are ZIP files, so you can uncompress them on top of a previous one if you want.<br />Once installed, Start Eclipse and choose a workspace folder (your favorite place for Maya Python projects).</li>
<li>After Eclipse have fully started, go into -&gt; Help -&gt; Install New Softwares<br />Click on the ‘Add…’ button and fill the form with ‘Name=PyDev – Location=http://pydev.org/updates’</li>
<li>After you accepted the EULA and legal stuff, this should bring PyDev and PyDev Mylyn Integration (optional) into the list. Select them to install them.<br /><br /></li>
<li>Restart Eclipse</li>
</ul>
<p>You are almost ready!</p>
<h2>Configuring Eclipse</h2>
<p>You only need to do this section if you wish to execute a standard Python application or a Maya Standalone application. If you only want to debug Maya Python scripts and Maya plug-ins, feel free to skip these steps.</p>
<ul>
<li>Start Eclipse</li>
<li>Go into -&gt; Window -&gt; Preferences</li>
<li>Go into -&gt; PyDev -&gt; Interpreter – Python<br />
<ul>
<li>Python Interpreters -&gt; New…<br />
<ul>
<li>Interpreter Name = Python Maya</li>
<li>Interpreter Executable = ‘C:\Program Files\Autodesk\Maya2013\bin\mayapy.exe’</li>
</ul>
</li>
</ul>
</li>
<li>Accept all proposed path and include both paths below<br />
<ul>
<li>‘C:\Program Files\Eclipse\plugins\<strong><span style="color: #ff0000;">org.python.pydev_2.6.0.2012062818</span></strong>\pysrc’</li>
<li>‘C:\Program Files\Autodesk\Maya2013\bin\python26.zip’</li>
</ul>
</li>
</ul>
<p>If you also want to configure another interpreter for a standard Python application and have Python installed already, do so now by adding a second interpreter configuration. You can add as many as you want.</p>
<p><a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d016768393cba970b-pi" style="display: inline;"><img alt="Important" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d016768393cba970b" src="/assets/image_8fce08.jpg" title="Important" /></a> Note the &#39;2.6.0.2012062818&#39; key in red above is changing depending of the Pydev version and it is important that you update it if you using a different version in future.</p>
<h2>Preparing
debugger scripts for Maya</h2>
<ul>
<li>Copy the attached Python script (<a href="http://around-the-corner.typepad.com/files/EclipsePydevMaya2013.zip" target="_self">debugmaya.py</a>) into ‘C:\Users\&lt;my profile&gt;\Documents\maya\scripts’<br /><br /><strong><a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d016768393cba970b-pi" style="display: inline;"><img alt="Important" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d016768393cba970b" src="/assets/image_8fce08.jpg" title="Important" /></a> IMPORTANT:</strong> Modify the path in the .py script to reflect the location where you have installed Eclipse (see line #18)</li>
</ul>
<ul>
<li>Copy the attached MEL script (<a href="http://around-the-corner.typepad.com/files/EclipsePydevMaya2013.zip" target="_self">scriptEditorPanel.mel</a>) into ‘C:\Users\&lt;profile&gt;\Documents\maya\2013-x64\prefs\scripts’</li>
<li>Copy the attached image (<a href="http://around-the-corner.typepad.com/files/EclipsePydevMaya2013.zip" target="_self">debug.png</a>) into ‘C:\Users\&lt;profile&gt;\Documents\maya\2013-x64\prefs\icons’</li>
</ul>
<p>You are now ready! You can start Maya</p>
<p><strong>NOTE:</strong> The paths mentioned above are for a Windows 64 bit machine. If you are running on a win32 machine, please adjust the path accordingly.</p>
<p><strong>NOTE:</strong> For other platforms, the procedure is the same excepted for the paths mentioned above.</p>
<h1>Let’s debug now</h1>
<p>First be aware that you can debug
both Maya Python scripts, Maya Python API plug-ins, and PyMel scripts, running
inside Maya or in standalone mode.</p>
<h2>Standalone
application</h2>
<ul>
<li>Start Eclipse</li>
<li>Create a new project
-&gt; File -&gt; New -&gt; Pydev Project (if you can’t see it, then go to -&gt;
File -&gt; New -&gt; Project… then expand the Pydev folder, select ‘Pydev
Project’, and click ‘Next’)
<ul>
<li>Give a ‘Project name’</li>
<li>Select 
<ul>
<li>Project type = Python</li>
<li>Grammar Version = 2.6</li>
<li>Interpreter&#0160; = Python Maya</li>
</ul>
</li>
<li><strong>Note:</strong> If you get
the ‘Open Associated Perspective?’ dialogue box, choose ‘Yes’</li>
<li>Finish</li>
</ul>
</li>
<li>Expand your project
node in the Pydev Package Explorer window</li>
<li>Select the ‘src’
node, right-click -&gt; New -&gt; Pydev Module
<ul>
<li>Give a ‘Name’ to your file (no extension)</li>
<li>Select Template = ‘Module: Main’</li>
<li>Finish</li>
</ul>
</li>
<li>Modify the code to
do something using Maya
<ul>
<li>Use the helloworld.py sample from ‘C:\Program
Files\Autodesk\Maya2013\devkit\applications\scripted’</li>
</ul>
</li>
</ul>
<p>You
are ready to run or debug your code now</p>
<p><strong>Note:</strong> There is nothing different to debug a standard Python
application here. Whether you continue using the mayapy.exe or you switch to
another interpreter. Eclipse can manage several Run/Debug configurations. See
Eclipse documentation for more details.</p>
<h2>Maya
Python script (and PyMel)</h2>
<p>This
time it is a bit more complicated. Since Maya scripts are running inside Maya,
we cannot launch mayapy.exe and expect to see maya.exe executing our script.
Instead, we need to use the remote debugging feature of Eclipse/PyDev. What is
Remote Debugging? In theory this feature is made to debug Python application
running on a different machine than yours. While it is possible to do this as
well with Maya Python, we are only interested to run it on the same machine at
this time. This is to simplify the configuration. However, remember you can
debug a script running in a Maya session running on another machine than yours.
How cool is that? ;)</p>
<p>Anyway,
let’s rock into our local debug Maya script debug session now. First, you
remember how we had to install a script called ‘debugmaya.py’ during install time;
this script will be used to connect Maya to the Eclipse Python remote debugger
server. This script will ‘alert’ Eclipse that someone is executing some Python
code and that we want to pass control to the Eclipse debugger. How to do this?</p>
<ul>
<li>In Eclipse, switch
to the debug perspective<br />
(see -&gt; Window -&gt; Open Perspective -&gt; Others… if you haven’t
configured your favorites yet)</li>
<li>You should now see 2
buttons in the Eclipse toolbar:
</li>
</ul>
<table border="0" cellpadding="0" cellspacing="0">
<tbody>
<tr>
<td valign="top" width="463">
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017ee48be13b970d-pi" style="float: left;">
<img alt="Pydev-stopserver" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017ee48be13b970d" src="/assets/image_f06b83.jpg" style="margin: 0px 5px 5px 0px;" title="Pydev-stopserver" /></a>A red square P button with a tooltip saying ‘Pydev: stop the debugger server’
</td>
<td rowspan="2" valign="top" width="229">
<p>&#0160;
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017c32e7f289970b-pi" style="display: inline;"><img alt="Pydev-server" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017c32e7f289970b" src="/assets/image_e41785.jpg" title="Pydev-server" /></a></p>
</td>
</tr>
<tr>
<td valign="top" width="463">
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017c32e7f556970b-pi" style="float: left;">
<img alt="Pydev-startserver" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017c32e7f556970b" src="/assets/image_9ad4d3.jpg" style="margin: 0px 5px 5px 0px;" title="Pydev-startserver" /></a>A green
  bug P button with a tooltip saying ‘Pydev: start the pydev server’
</td>
</tr>
</tbody>
</table>
<ul>
<li>Click on the ‘green
bug P’ button
<ul>
<li>On the ‘Console’ panel, you should now read ‘Debug Server at
port: 5678’</li>
<li>You should also see a debugger instance in the ‘Debug’
panel</li>
</ul>
</li>
<li>Now the remote
debugger is started, you can now start Maya</li>
<li>Open the Maya Script
Editor and select a Python tab and write the following code as an example (or
any code you’d like to debug)
<ul>
<li>import
sys</li>
<li>print
sys.path</li>
</ul>
</li>
<li>Launch the debugger
using the new ‘Debug’ button in the Script Editor (near the ‘Execute All’
button)<br />
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017d3d169edc970c-pi" style="display: inline;"><img alt="Pydev-mayastartdebug" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017d3d169edc970c" src="/assets/image_9611a6.jpg" title="Pydev-mayastartdebug" /></a><br />If
everything was configured properly, you should see a few lines of text
displayed in the Maya history pane, after that it looks like Maya has frozen
but don’t panic, switch to Eclipse and see that Eclipse is pausing on a line
saying:<br />
<ul>
<li>pydevd.settrace()</li>
<li>This is the ‘alert’ signal from the remote application to
the debugger. This leaves you a chance to prepare your debugging session before
any code is executed. For example, you can set breakpoints in your code. </li>
</ul>
</li>
<li>However, if you pay
attention to the Maya history pane and the debugger panel, you’ll notice that
the Pydev debugger tells you two things:
<ul>
<li>Pydev debugger – &lt;module&gt; [&lt;maya console&gt;:NN]</li>
<li>Maya console – “#
pydev debugger: Unable to find real location for: &lt;maya console&gt;”</li>
</ul>
<p>This
is because the code which is in the Maya Script Editor has no physical file,
and Pydev needs a file to retrieve the code for the debugger.</p>
</li>
</ul>
<ul>
<li>Go into your
‘C:\Users\&lt;profile&gt;\Documents\maya\2013-x64\scripts’, you should find a
file named ‘__debug.py’. Drag and drop it into Eclipse. You should see the code
you put into your Maya Python tab in the Script Editor. Now, in the left grayed
margin either dbl-click or right-click to add a breakpoint on the print
statement and press F8 to resume execution. The debugger should now stop right
there, and now you have full access to your Maya/Python environments for
debugging purposes.</li>
</ul>
<p>You
can now debug a Maya script as well as a PyMel script.</p>
<h2>Maya
Python plug-ins </h2>
<p>Almost
the same as a ‘Python Script’, the only differences are:</p>
<ul>
<li>You load/unload
plug-ins as any other plug-ins</li>
<li>You need to modify
your plug-in script and add 2 lines at the top of the file</li>
</ul>
<p>import debugmaya<br />debugmaya.startDebug()</p>
<p><strong>If
your debugger is already started, skip the next 3 steps and go directly into
Maya</strong></p>
<ul>
<li>In Eclipse, switch
to the debug perspective<br />
(see -&gt; Window -&gt; Open Perspective -&gt; Others… if you haven’t
configured your favorites yet)</li>
<li>You should now see 2
buttons in the Eclipse toolbar:
</li>
</ul>
<table border="0" cellpadding="0" cellspacing="0">
<tbody>
<tr>
<td valign="top" width="463">
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017ee48be13b970d-pi" style="float: left;">
<img alt="Pydev-stopserver" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017ee48be13b970d" src="/assets/image_f06b83.jpg" style="margin: 0px 5px 5px 0px;" title="Pydev-stopserver" /></a>A red square P button with a tooltip saying ‘Pydev: stop the debugger server’
</td>
<td rowspan="2" valign="top" width="229">
<p>&#0160;
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017c32e7f289970b-pi" style="display: inline;"><img alt="Pydev-server" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017c32e7f289970b" src="/assets/image_e41785.jpg" title="Pydev-server" /></a></p>
</td>
</tr>
<tr>
<td valign="top" width="463">
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017c32e7f556970b-pi" style="float: left;">
<img alt="Pydev-startserver" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017c32e7f556970b" src="/assets/image_9ad4d3.jpg" style="margin: 0px 5px 5px 0px;" title="Pydev-startserver" /></a>A green
  bug P button with a tooltip saying ‘Pydev: start the pydev server’
</td>
</tr>
</tbody>
</table>
<ul>
<li>Click on the ‘green
bug P’ button<br />
<ul>
<li>On the ‘Console’ panel, you should now read ‘<strong>Debug Server at port: 5678</strong>’</li>
<li>You should also see a debugger instance in the ‘Debug’
panel
</li>
</ul>
</li>
<li>Now the remote
debugger is started, you can now start Maya</li>
<li>Go into the Plug-in
Manager and load your Python scripted plug-in (I used the sineNode.py sample
from the devkit folder)</li>
<li>Switch to Eclipse
and see that Eclipse is pausing on the same line we described in the previous
chapter.</li>
<li>Load your plug-in
code into Eclipse and put breakpoints in initializePlugin() and in the
sineNode.__init__() function. Press F8 to resume.<br />The
debugger should stop immediately into initializePlugin() at the position you
set the breakpoint. Press F8 again to resume execution.<br />Open
the Script Editor, and in a MEL tab execute the following code</li>
<p>createNode “spSineNode”<br /><br />
The debugger should stop into sineNode.__init__()
at the position you set the breakpoint. Press F8 again to resume execution.</p>
<li>In the Plug-in
Manager, unload and load the Python scripted plug-in again, and see the
debugger stopping again and again at your breakpoints</li>
</ul>
<p>You can now debug Maya scripted plug-ins.</p>
<h2>Advanced concepts</h2>
<h3>import
&amp; reload()</h3>
<p>One
important concept in Python is that import is executed only once. If you modify
your code, you need to use the function reload() to force Maya to refresh the
code changes. The debugmaya.py comes with an ImporterController which will take
care of cleaning your Python environment so you do not have to care about this
at all. However, you should be aware of what is done in the background. When
you load a Python module, Python keeps an internal reference in a dictionary to
avoid having to reload the same file definition multiple time. While it is good
for performance and cross references, it is a disaster for someone who wants to
debug the code, because it would mean to restart the application each time. To
avoid this problem, the debugger creates a singleton instance of the controller
and the controller makes a copy of the references dictionary. Then it replaces
the built-in import function. From there any new imported modules would be
reloaded each time when requested, but not the ones which were already loaded
before the debugger started.</p>
<h3>Debugging Python Maya native files</h3>
<p>All
Python Maya native files are zipped in the Python26.zip file. If you want to
step through that code you need to either modify PyDev to extract the file on
the fly, or you may explode Python26.zip file in
‘C:\Users\&lt;profile&gt;\Documents\maya\2013-x64\scripts’. Both solution will
work. But I do recommend to do this only when you need to debug Python native
files and only temporarily.&#0160;</p>
<h3>Will I be able to
step-in Maya Python function and API?</h3>
<p>
No, you can only step
through your own code. Maya functions and API are almost all written in C/C++.
Unless a Maya component was written in Python language and exposed in a
physical file, you wouldn’t be able to step through that code.</p>
<h3>Can
this solution be used in previous releases of Maya?</h3>
<p>Yes,
absolutely. All Maya versions with Python support can use this technique and add debugging support to Python. That includes releases starting from Maya 8.5.</p>
