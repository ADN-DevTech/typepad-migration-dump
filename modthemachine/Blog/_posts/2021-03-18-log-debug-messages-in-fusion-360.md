---
layout: "post"
title: "Log debug messages in Fusion 360"
date: "2021-03-18 15:07:55"
author: "Adam Nagy"
categories:
  - "Adam"
  - "Fusion 360"
  - "Python"
original_url: "https://modthemachine.typepad.com/my_weblog/2021/03/log-debug-messages-in-fusion-360.html "
typepad_basename: "log-debug-messages-in-fusion-360"
typepad_status: "Publish"
---

<p>The usual way to debug your <a href="https://www.python.org/">Python</a> <strong>add-in</strong> is to start it from <a href="https://code.visualstudio.com/">Visual Studio Code</a>, and use <a href="https://docs.python.org/3/library/functions.html#print">print()</a> to write messages to the <a href="https://code.visualstudio.com/Docs/editor/debugging">Debug Console</a>:</p>
<p><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc688340263e99781dc200b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="DebugPrint" class="asset  asset-image at-xid-6a00e553fcbfc688340263e99781dc200b img-responsive" src="/assets/image_328460.jpg" title="DebugPrint" /></a></p>
<p>However, sometimes you might just want some insight into what&#39;s going on inside your <strong>add-in</strong> without starting a debugging session.</p>
<p>One obvious way is to write information into a <strong>log file</strong>. Depending on your needs that might be the thing to do.</p>
<p>That will not provide <strong>real-time</strong> information though: you have to open the file in order to see what was written into it.</p>
<p>Another option is to write to the &quot;<strong>Text Commands</strong>&quot; palette of <strong>Fusion 360</strong>. If you don&#39;t mind a bit of delay for the messages to appear then just call <strong>writeText()</strong> function of the <strong>palette</strong>, otherwise <strong>also call</strong> <a href="https://help.autodesk.com/view/fusion360/ENU/?guid=GUID-743C88FB-CA3F-44B0-B0B9-FCC378D0D782">adsk.doEvents()</a></p>
<p>You could create a couple of simple classes to implement what&#39;s needed:</p>
<pre>class UiLogger:
    def __init__(self, forceUpdate):  
        app = adsk.core.Application.get()
        ui  = app.userInterface
        palettes = ui.palettes
        self.textPalette = palettes.itemById(&quot;TextCommands&quot;)
        self.forceUpdate = forceUpdate
        self.textPalette.isVisible = True 
    
    def print(self, text):       
        self.textPalette.writeText(text)
        if (self.forceUpdate):
            adsk.doEvents() 

class FileLogger:
    def __init__(self, filePath): 
        try:
            open(filePath, &#39;a&#39;).close()
        
            self.filePath = filePath
        except:
            raise Exception(&quot;Could not open/create file = &quot; + filePath)

    def print(self, text):
        with open(self.filePath, &#39;a&#39;) as txtFile:
            txtFile.writelines(text + &#39;\r\n&#39;)</pre>
<p>Then you can create an instance of either of them and start logging messages. E.g. using the <strong>UiLogger</strong>:</p>
<pre>logger = UiLogger(True)
# ...
logger.print(&quot;Some message&quot;)
</pre>
<p><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc688340278801cba78200d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="TextCommands" class="asset  asset-image at-xid-6a00e553fcbfc688340278801cba78200d img-responsive" src="/assets/image_624528.jpg" title="TextCommands" /></a></p>
<p>Or use the <strong>FileLogger</strong></p>
<pre>logger = FileLogger(&quot;/Users/nagyad/Documents/log.txt&quot;)
# ...
logger.print(&quot;Some message&quot;)</pre>
<p><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc68834026bdec4d1cc200c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="LogFile" class="asset  asset-image at-xid-6a00e553fcbfc68834026bdec4d1cc200c img-responsive" src="/assets/image_470070.jpg" title="LogFile" /></a></p>
<p>That <strong>palette</strong> is not only for writing messages but can also be used to run <strong>Python</strong> scripts - see <a href="https://modthemachine.typepad.com/my_weblog/2020/01/run-commands-from-the-text-commands-panel.html">Run commands from the &quot;Text Commands&quot; panel</a></p>
