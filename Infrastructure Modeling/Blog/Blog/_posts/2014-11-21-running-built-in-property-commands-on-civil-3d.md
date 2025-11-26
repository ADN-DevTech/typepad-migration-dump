---
layout: "post"
title: "Running built-in property commands on Civil 3D"
date: "2014-11-21 07:44:15"
author: "Augusto Goncalves"
categories:
  - ".NET"
  - "Augusto Goncalves"
  - "AutoCAD Civil 3D 2015"
original_url: "https://adndevblog.typepad.com/infrastructure/2014/11/running-built-in-property-commands-on-civil-3d.html "
typepad_basename: "running-built-in-property-commands-on-civil-3d"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/augusto-goncalves.html">Augusto Goncalves</a></p>  <p>In some cases we just want to invocate Civil 3D property dialogs for specific entities, let’s say an Alignment. But most commands are not customizable via APIs. Here the alternative idea is set the selection and then call the command. The following code uses AutoCAD Editor.Command API, available on 2015+</p>  <pre style="font-size: 12px; font-family: consolas; background: white; color: black"><span style="color: blue">private</span>&#160;<span style="color: blue">static</span>&#160;<span style="color: blue">void</span> ShowAlignProp(ObjectId id)<br />{<br />&#160; <span style="color: green">// test type as this command will not work for other types</span><br />&#160; <span style="color: blue">if</span> (id.ObjectClass != <span style="color: #2b91af">RXClass</span>.GetClass(<span style="color: blue">typeof</span>(<span style="color: #2b91af">Alignment</span>))) <span style="color: blue">return</span>;<br /> <br />&#160; <span style="color: #2b91af">Editor</span> ed = <span style="color: #2b91af">Application</span>.DocumentManager.MdiActiveDocument.Editor;<br />&#160; ed.SetImpliedSelection(<span style="color: blue">new</span> ObjectId[] { id }); <span style="color: green">// set selection</span><br />&#160; ed.Command(<span style="color: blue">new</span>&#160;<span style="color: blue">string</span>[] { <span style="color: #a31515">&quot;_AeccEditAlignmentProperties&quot;</span> }); <span style="color: green">// run command</span><br />&#160; ed.SetImpliedSelection(<span style="color: blue">new</span> ObjectId[0]); <span style="color: green">// clear selection</span><br />}</pre>
