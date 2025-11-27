---
layout: "post"
title: "Run commands from the \"Text Commands\" panel"
date: "2020-01-28 13:15:02"
author: "Adam Nagy"
categories:
  - "Adam"
  - "Fusion 360"
original_url: "https://modthemachine.typepad.com/my_weblog/2020/01/run-commands-from-the-text-commands-panel.html "
typepad_basename: "run-commands-from-the-text-commands-panel"
typepad_status: "Publish"
---

<p>As mentioned <a href="https://modthemachine.typepad.com/my_weblog/2016/01/run-fusion-commands.html">in this article</a> already, you can run registered <strong>Fusion 360</strong> commands programmatically as well.</p>
<p>You can also do it without creating a script or add-in, by using the &quot;<strong>Text Commands</strong>&quot; panel:</p>
<p><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc688340240a4e0eace200d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="TextCommandsPanel" class="asset  asset-image at-xid-6a00e553fcbfc688340240a4e0eace200d img-responsive" src="/assets/image_877707.jpg" title="TextCommandsPanel" /></a></p>
<p>In this case we are using <strong>Python</strong>, so make sure that the appropriate radio button is selected:</p>
<p><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc688340240a50588e1200b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="RunCcommand" class="asset  asset-image at-xid-6a00e553fcbfc688340240a50588e1200b img-responsive" src="/assets/image_442329.jpg" title="RunCcommand" /></a></p>
<pre>import adsk.core, adsk.fusion 
app = adsk.core.Application.get() 
ui = app.userInterface 
cmdDefs = ui.commandDefinitions 
cmdDef = cmdDefs.itemById(&quot;DragKnifeAddInMenuEntry&quot;) 
cmdDef.execute()</pre>
<p>As mentioned in the article I linked to above, you can <a href="https://help.autodesk.com/view/fusion360/ENU/?guid=GUID-d2b85a7e-fd08-11e4-9e07-3417ebd3d5be">get a list of all the commands that were added to the UI</a> and find out the <strong>ID</strong> of the <strong>CommandDefinition</strong> you want to run.<br />You can also get it by listing the <strong>name</strong> and <strong>id</strong> of all the registered <strong>CommandDefinitions</strong>:</p>
<p><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc688340240a4b7bf2d200c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="ListCommands" class="asset  asset-image at-xid-6a00e553fcbfc688340240a4b7bf2d200c img-responsive" src="/assets/image_152368.jpg" title="ListCommands" /></a></p>
<pre>import adsk.core, adsk.fusion 
app = adsk.core.Application.get() 
ui = app.userInterface 
cmdDefs = ui.commandDefinitions 
result = &quot;&quot; 
for cmdDef in cmdDefs: 
  result += &quot;\n&quot; + cmdDef.name + &quot;, &quot; + cmdDef.id 
print(result) </pre>
<p>PS: after typing the line of &quot;<strong>result += ...</strong>&quot; you&#39;ll have to press enter twice to get out of the <strong>for loop</strong> section</p>
