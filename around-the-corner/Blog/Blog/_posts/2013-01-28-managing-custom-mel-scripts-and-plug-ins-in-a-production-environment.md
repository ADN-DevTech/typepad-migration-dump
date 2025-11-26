---
layout: "post"
title: "Managing Custom MEL Scripts and Plug-ins in a Production Environment"
date: "2013-01-28 07:25:00"
author: "Cyrille Fauvel"
categories:
  - "Cyrille Fauvel"
  - "Maya"
  - "MEL"
original_url: "https://around-the-corner.typepad.com/adn/2013/01/managing-custom-mel-scripts-and-plug-ins-in-a-production-environment.html "
typepad_basename: "managing-custom-mel-scripts-and-plug-ins-in-a-production-environment"
typepad_status: "Publish"
---

<h2>Introduction</h2>
<p>Managing numerous and complex MEL scripts and plug-ins can become difficult and inconsistent in production environments.</p>
<p>The following steps will ensure that your MEL scripts get sourced and your plug-ins will get loaded without error and with consistency throughout your production environment.</p>
<h3>1) Recommended method for MEL scripting:</h3>
<p>// Create several local procs in a .mel file.</p>
<p>// At the end of the file write one global proc that connects/calls to all these local procs.</p>
<p>// Name the global proc with the same name as that of the .mel file.</p>
<p>// Put the .mel file in one of the places listed under MAYA_SCRIPT_PATH.</p>
<p>The file testProc.mel should look like:</p>
<pre class="brush:cpp; toolbar:false;">proc proc1() {
  print ("\n proc1");
}

proc proc2() {
  print ("\n proc2");
}

global proc testProc() {
  print ("\n testProc");
  proc1;
  proc2;
}
</pre>
<p>Now keep in mind that the order DOES MATTER here. If the global proc was at the beginning of the script then this won't work. Notice that the source statement does not appear anywhere yet. This is the way most of the scripts that come with the Maya are written. Adding a new MEL file for every new global proc makes things predictable and reliable.</p>
<h3>2) Adding another global proc to the end of the file:</h3>
<p>If you want to add another global proc in the SAME file and make sure that it is available just like other global procs your testProc.mel should look like this:</p>
<pre class="brush:cpp; toolbar:false;">proc proc1() {
  print ("\n proc1");
}

proc proc2() {
  print ("\n proc2");
}

global proc testProc() {
  print ("\n testProc");
  proc1;
  proc2;
}

global proc testProc1() {
  print ("\n testProc1");
  testProc;
  proc1;
  proc2;
}
</pre>
<p>If you try to call testProc1 after starting a new session of Maya, it will not be recognized. One way to fix this is to source the file. This will make all the global procs in that file available. What you do is:</p>
<p># Add this line inside your userSetup.mel:<br />source testProc;</p>
<p>2a) However this defeats the purpose of having the automated system of making  available all the global procs sitting in the files that are in MAYA_SCRIPT_PATH. So, the following will work even without the source statement in the userSetup.mel:</p>
<pre class="brush:cpp; toolbar:false;">proc proc1() {
  print ("\n proc1");
}

proc proc2() {
  print ("\n proc2");
}

global proc testProc1() {
  print ("\n testProc1");
  proc1;
  proc2;
}

global proc testProc() {
  print ("\n testProc");
  testProc1;
}</pre>
<p>The above will work even without the source statement. Notice that although there are 2 global procs in the same file, testProc is still the top proc in terms of hierarchy since the flow of control goes like: testProc --&gt; testProc1 --&gt; proc1, proc2. Again, the order DOES MATTER.</p>
<p>2b) Another way of doing this would be:</p>
<pre class="brush:cpp; toolbar:false;">proc proc1() {
  print ("\n proc1");
}

proc proc2() {
  print ("\n proc2");
}

global proc testProc2() {
  print ("\n testProc2");
  testProc1;
  proc1;
  proc2;
}

global proc testProc1() {
  print ("\n testProc1");
  proc1;
  proc2;
}

global proc testProc() {
}
</pre>
<p>Now call the testProc from inside the userSetup.mel by adding the line:</p>
<pre class="brush:cpp; toolbar:false;">testProc;
</pre>
<p>Just calling the blank proc will make all the other global procs inside the testProc.mel available to you. But you will run into errors if you try to call the testProc1 BEFORE calling testProc.</p>
<h3>3) Dealing with loadPlugin and the commands from the plug-in:</h3>
<p>In this situation the userSetup.mel has MEL code like the following and gives you errors.</p>
<pre class="brush:cpp; toolbar:false;">
loadPlugin A;
source "B.mel";
someCommandFromThePluginA();</pre>
<p>(someCommandFromThePluginA which is sitting inside B.mel that is recognized only when the plugin A is loaded.) ...and you later run the command which comes from the B.mel file. It fails if it references anything from the plugin 'A'.</p>
<p>The solution is:</p>
<pre class="brush:cpp; toolbar:false;">
loadPlugin A;
evalDeferred("source \"B.mel\"");
someCommandFromThePluginA();</pre>
<p>The eval causes the eval'ed string to be compiled at runtime. By that point the plug-in is loaded and available. MEL considers source statements as compiler directives, so they are handled before the script is even run. The only way to get around that is to wrap the source statement in an eval.</p>
<p>Another working example of this would be:</p>
<pre class="brush:cpp; toolbar:false;">
string $plugin = "/usr/autodesk/maya/bin/plug-ins/Mayatomr.so";
loadPlugin($plugin);
int $ret=0;
while ($ret == 0) {
  $ret = `pluginInfo -q -loaded $plugin;`;
}
eval ("Mayatomr -mi -file " + $inFile + " -perframe 2 -padframe 0  -tabstop 8");
</pre>
<p>The loadPlugin and evalDeferred execute right away in batch mode since there is no such thing as "idle" in batch mode.</p>
<p>You can NEVER load a plug-in and expect to be able to use a command from that plug-in directly in the same .mel file.</p>
<p>So does it mean that for the most part, loadPlugin is a trivial to the point of being a "useless" MEL command? Well ...not quite. Most people write wrapper scripts to load the plug-in and then call something else.</p>
<p>In apiAnimCurveTest.mel try something like:</p>
<pre class="brush:cpp; toolbar:false;">
global proc int apiAnimCurveTest () {
  string $soName = "apiAnimCurveTest";
  if (`about -os` == "win64" || `about -os` == "win32" ) {
    $soName = $soName + ".mll";
  } else if (`about -os` == "linux" {
    $soName = $soName + ".so";
  } else {
    $soName = $soName + ".bundle";
  }
  // Make sure we can load our plugin
  if (!`pluginInfo -query -loaded $soName`) {
    if (catch (`loadPlugin $soName`)) {
      print ("### apiAnimCurveTest: Fatal error (startup): unable to load  " + $soName + "\n");
      return (1);
    }
  }
  // Force apiAnimCurveTestReal.mel to be reparsed by mel
  eval ("source apiAnimCurveTestReal.mel");
  int $errors = eval ("apiAnimCurveTestReal");
  return ($errors);
}
</pre>
<p>And then apiAnimCurveTestReal.mel can use the plug-in commands:</p>
<pre class="brush:cpp; toolbar:false;">
global proc int apiAnimCurveTestReal () {
  string $soName = "apiAnimCurveTest";
  if (`about -os` == "win64" || `about -os` == "win32" ) {
    $soName = $soName + ".mll";
  } else if (`about -os` == "linux" {
    $soName = $soName + ".so";
  } else {
    $soName = $soName + ".bundle";
  }
  // Make sure our plugin has been loaded
  if (!`pluginInfo -query -loaded $soName`) {
    print ("### apiAnimCurveTest: Fatal error (startup): unable to load "  + $soName + "\n");
    return (1);
  }
  // Create some animation
  apiAnimateAnObject;
  return (0);
}
</pre>
<p>4) Sourcing and "Unsourcing" global procs.</p>
<p>There might be a time when you want to have 2 versions of a script. i) V1 --&gt; when a certain plug-in is loaded. ii) V2 --&gt; when certain plug-in is not loaded. A good example of this is the AE*.mel scripts that come with the mr plug-in. These scripts override their usual AE counterparts when the plug-in gets loaded.</p>
<p>5) You can use the userSetup.mel as the entry point for deciding what gets loaded. One way of doing this would be to allow Maya to start up and then from inside the userSetup.mel call a global proc that will run a check on which plug-ins are loaded or should be loaded. You can also make use of the environment variables to store certain paths or trigger certain events.</p>
<p>For more working examples look at the integration of scripts and plug-ins inside the bonus tools pack.</p>
<p>&nbsp;</p>
