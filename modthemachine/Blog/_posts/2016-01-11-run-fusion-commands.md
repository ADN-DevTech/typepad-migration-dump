---
layout: "post"
title: "Run Fusion commands"
date: "2016-01-11 11:15:23"
author: "Adam Nagy"
categories:
  - "Adam"
  - "Fusion 360"
  - "Python"
original_url: "https://modthemachine.typepad.com/my_weblog/2016/01/run-fusion-commands.html "
typepad_basename: "run-fusion-commands"
typepad_status: "Publish"
---

<p>The <a href="http://fusion360.autodesk.com/resources/programminginterface">online help</a>&#0160;already has a sample on <a href="http://help.autodesk.com/cloudhelp/ENU/Fusion-360-API/files/WriteUserInterfaceToFile_Sample.htm">listing all the controls in the user interface</a>&#0160;which also provides the <strong>id</strong> of &#0160;the <strong>CommandDefinition</strong> that the various controls reference. &#0160;You can also get to the sample from many help topics, including &quot;UserInterface.toolbars&quot;</p>
<p class="p1"><span class="s1">This sample is very useful when positioning your controls within existing <strong>Fusion</strong> toolbars and panels and positioning your command adjacent to an existing command</span></p>
<p>However, if you are only interested in the command <strong>names</strong> and their <strong>id</strong>&#39;s then you can get them much simpler.</p>
<p>When you create your command then you create a <strong>CommandDefinition</strong> object and add it to the <strong>UserInterface</strong>.<strong>commandDefinitions</strong> collection.<br />This collection contains the internal command definitions as well. So you can just iterate through it to get the info you want. In <strong>Python</strong> it could be:</p>
<pre>app = adsk.core.Application.get()
ui = app.userInterface

fileDialog = ui.createFileDialog()
fileDialog.isMultiSelectEnabled = False
fileDialog.title = &quot;Specify result filename&quot;
fileDialog.filter = &#39;Text files (*.txt)&#39;
fileDialog.filterIndex = 0
dialogResult = fileDialog.showSave()
if dialogResult == adsk.core.DialogResults.DialogOK:
    filename = fileDialog.filename
else:
    return
    
commandDefinitions = ui.commandDefinitions
result = &quot;&quot;

for commandDefinition in commandDefinitions:
    result += &quot;id = &quot; + commandDefinition.id + &quot;; name = &quot; + commandDefinition.name + &quot;\n&quot;
    
output = open(filename, &#39;w&#39;)
output.writelines(result)
output.close()</pre>
<p>You can search the content of the created file to find the <strong>id</strong> of the <strong>CommandDefinition</strong> you need. Once you have it you can <strong>Execute</strong> it - e.g. in <strong>Python </strong>running the<strong> Export </strong>command:</p>
<pre>cmdDef = ui.commandDefinitions.itemById(&quot;ExportCommand&quot;)
cmdDef.execute()</pre>
<p><a class="asset-img-link" href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401b8d18f94cf970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Exportcommand" class="asset  asset-image at-xid-6a00e553fcbfc6883401b8d18f94cf970c img-responsive" src="/assets/image_494551.jpg" title="Exportcommand" /></a></p>
<p>-Adam</p>
