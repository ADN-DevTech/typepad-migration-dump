---
layout: "post"
title: "Drive robot arm in Fusion"
date: "2016-08-21 19:56:42"
author: "Adam Nagy"
categories:
  - "Adam"
  - "Fusion 360"
original_url: "https://modthemachine.typepad.com/my_weblog/2016/08/drive-robot-arm-in-fusion.html "
typepad_basename: "drive-robot-arm-in-fusion"
typepad_status: "Publish"
---

<p>Note: this article has an <a href="http://modthemachine.typepad.com/my_weblog/2016/08/drive-robot-arm-in-fusion-update.html">update</a></p>
<p>If you want to write a program that drives the model then you can do that&#0160;through driving parameters. I have the following <a href="http://a360.co/2b9PQGI">simple model</a> with <strong>Revolute Joints.&#0160;</strong>These types of joints also have an <strong>Alignment Angle</strong> parameter which we can modify programmatically as well :)</p>
<p><a class="asset-img-link" href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401bb092d317b970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="RobotArmJoints" class="asset  asset-image at-xid-6a00e553fcbfc6883401bb092d317b970d img-responsive" src="/assets/image_999493.jpg" title="RobotArmJoints" /></a></p>
<p>I wanted to drive the model through the arrow buttons on the keyboard. <strong>Fusion API</strong> provides a <strong>keyDown</strong> event through the <strong>Command</strong> object. So in order to use it we need to define a command and run it. Since we are modifying the model before the user clicks &quot;<strong>OK</strong>&quot; on the command dialog, therefore we also need to listen to the <strong>executePreview</strong> event of the <strong>Command</strong>. There we can set the <strong>isValidResult</strong> to <strong>True</strong>. This way if the user clicks &quot;<strong>OK</strong>&quot; then the changes will be kept without us having to redo the changes inside the <strong>execute</strong> event handler. It&#39;s like so because each command automatically starts a transaction which gets undone by default&#0160;right before&#0160;the program creates the final model changes inside the <strong>execute</strong> event handler.&#0160;</p>
<p>See the <strong>Python</strong> code below:</p>
<pre>#Author-
#Description-

import adsk.core, adsk.fusion, adsk.cam, traceback

# Global variable used to maintain a reference to all event handlers.
handlers = []

# Other global variables
commandName = &quot;MoveRobot&quot;
app = adsk.core.Application.get()
if app:
    ui = app.userInterface
    

# Event handler for the keyDown event.
class MyKeyDownHandler(adsk.core.KeyboardEventHandler):
    def __init__(self):
        super().__init__()
    def notify(self, args):
        try:
            eventArgs = adsk.core.KeyboardEventArgs.cast(args)
            keyCode = eventArgs.keyCode    
                
            paramName = &quot;&quot;
            diffVal = 0
            if keyCode == adsk.core.KeyCodes.DownKeyCode:
                paramName = &quot;d41&quot;
                diffVal = -0.1
            elif keyCode == adsk.core.KeyCodes.UpKeyCode:
                paramName = &quot;d41&quot;
                diffVal = 0.1
            elif keyCode == adsk.core.KeyCodes.LeftKeyCode:
                paramName = &quot;d59&quot;
                diffVal = -0.1
            elif keyCode == adsk.core.KeyCodes.RightKeyCode:
                paramName = &quot;d59&quot;
                diffVal = 0.1
    
            design = app.activeProduct
            params = design.allParameters   
            
            param = params.itemByName(paramName)
            newVal = param.value + diffVal
            param.value = newVal
            
            adsk.doEvents() 
        except:
            ui.messageBox(&#39;Failed:\n{}&#39;.format(traceback.format_exc()))              


# Event handler for the executePreview event.
class MyExecutePreviewHandler(adsk.core.CommandEventHandler):
    def __init__(self):
        super().__init__()
    def notify(self, args):
        eventArgs = adsk.core.CommandEventArgs.cast(args)
        
        # Make it accept the changes whatever happens
        eventArgs.isValidResult = True
        

class MyCommandCreatedHandler(adsk.core.CommandCreatedEventHandler):    
    def __init__(self):
        super().__init__()        
    def notify(self, args):
        try:
            command = adsk.core.Command.cast(args.command)
            
            onExecutePreview = MyExecutePreviewHandler()
            command.executePreview.add(onExecutePreview)
            handlers.append(onExecutePreview)
        
            onKeyDown = MyKeyDownHandler()
            command.keyDown.add(onKeyDown)
            handlers.append(onKeyDown)
            
            onDestroy = MyCommandDestroyHandler()
            command.destroy.add(onDestroy)
            handlers.append(onDestroy)
            
            inputs = command.commandInputs
            inputs.addTextBoxCommandInput(
                commandName + &#39;_textBox&#39;, &#39;Usage:&#39;, 
                &#39;Use the arrow buttons to drive the robot arm&#39;, 2, 
                True);
        except:
            ui.messageBox(&#39;Failed:\n{}&#39;.format(traceback.format_exc()))              
            
            
class MyCommandDestroyHandler(adsk.core.CommandEventHandler):
    def __init__(self):
        super().__init__()
    def notify(self, args):
        try:
            commandDefinitions = ui.commandDefinitions
            # Check the command exists or not
            cmdDef = commandDefinitions.itemById(commandName)
            if cmdDef:
                cmdDef.deleteMe                
                
            # When the command is done, terminate the script
            # this will release all globals which will remove all event handlers
            adsk.terminate()
        except:
            ui.messageBox(&#39;Failed:\n{}&#39;.format(traceback.format_exc()))        
            
            
def run(context):
    try:
        product = app.activeProduct
        design = adsk.fusion.Design.cast(product)
        if not design:
            ui.messageBox(&#39;It is not supported in current workspace, please change to MODEL workspace and try again.&#39;)
            return
        commandDefinitions = ui.commandDefinitions
        # Check the command exists or not
        cmdDef = commandDefinitions.itemById(commandName)
        if not cmdDef:
            cmdDef = commandDefinitions.addButtonDefinition(
                commandName, commandName, commandName, &#39;&#39;) 

        onCommandCreated = MyCommandCreatedHandler()
        cmdDef.commandCreated.add(onCommandCreated)
        # Keep the handler referenced beyond this function
        handlers.append(onCommandCreated)
        inputs = adsk.core.NamedValues.create()
        cmdDef.execute(inputs)

        # Prevent this module from being terminated when the script returns, 
        # because we are waiting for event handlers to fire
        adsk.autoTerminate(False)

    except:
        ui.messageBox(&#39;Failed:\n{}&#39;.format(traceback.format_exc()))
</pre>
<p>Here is the script in action:</p>
<p><iframe allowfullscreen="" frameborder="0" height="315" src="https://www.youtube.com/embed/QeK2iHwpkc4" width="420"></iframe></p>
<p>-Adam</p>
