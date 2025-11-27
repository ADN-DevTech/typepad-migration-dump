---
layout: "post"
title: "Drive all revolute joints"
date: "2020-03-25 21:39:04"
author: "Adam Nagy"
categories:
  - "Adam"
  - "Fusion 360"
original_url: "https://modthemachine.typepad.com/my_weblog/2020/03/drive-all-revolute-joints.html "
typepad_basename: "drive-all-revolute-joints"
typepad_status: "Publish"
---

<p>I have two blog posts on driving revolute joints of a model:<br /><a href="https://modthemachine.typepad.com/my_weblog/2016/08/drive-robot-arm-in-fusion.html">Drive robot arm in Fusion</a> and <a href="https://modthemachine.typepad.com/my_weblog/2016/08/drive-robot-arm-in-fusion-update.html">Drive robot arm in Fusion - update</a></p>
<p>However, in those cases you have to select the joints you want to drive and use the keyboard.<br />If you prefer driving all the revolute joints&#39; rotation values through a slider, then here is a sample script you could use.<br />This also takes limits into consideration.</p>
<p><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc688340240a4f5bb52200d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Robot" border="0" class="asset  asset-image at-xid-6a00e553fcbfc688340240a4f5bb52200d image-full img-responsive" src="/assets/image_778484.jpg" title="Robot" /></a></p>
<p><strong>Python</strong> script code:</p>
<pre>#Author-
#Description-

import adsk.core, adsk.fusion, adsk.cam, math, traceback

# Global variable used to maintain a reference to all event handlers.
handlers = []

# Other global variables
joints = None
commandName = &quot;DriveJoints&quot;
app = adsk.core.Application.get()
if app:
    ui = app.userInterface
            
# Event handler for the inputChanged event.
class MyInputChangedHandler(adsk.core.InputChangedEventHandler):
    def __init__(self):
        super().__init__()
    def notify(self, args):
        eventArgs = adsk.core.InputChangedEventArgs.cast(args)
        commandInput = eventArgs.input
        
# Event handler for the executePreview event.
class MyExecutePreviewHandler(adsk.core.CommandEventHandler):
    def __init__(self):
        super().__init__()
    def notify(self, args):
        eventArgs = adsk.core.CommandEventArgs.cast(args)
        inputs = eventArgs.command.commandInputs

        for i in inputs:
            joint = joints.itemByName(i.name)
            rot = i.valueOne
            motion = joint.jointMotion
            motion.rotationValue = rot

        # Make it accept the changes whatever happens
        eventArgs.isValidResult = True
        

class MyCommandCreatedHandler(adsk.core.CommandCreatedEventHandler):    
    def __init__(self):
        super().__init__()        
    def notify(self, args):
        try:
            command = adsk.core.Command.cast(args.command)
            
            # Subscribe to the various command events
            onInputChanged = MyInputChangedHandler()
            command.inputChanged.add(onInputChanged)
            handlers.append(onInputChanged)

            onExecutePreview = MyExecutePreviewHandler()
            command.executePreview.add(onExecutePreview)
            handlers.append(onExecutePreview)
        
            onDestroy = MyCommandDestroyHandler()
            command.destroy.add(onDestroy)
            handlers.append(onDestroy)
            
            inputs = command.commandInputs

            for joint in joints:
                if joint.jointMotion.objectType == adsk.fusion.RevoluteJointMotion.classType():
                    minRot = 0
                    maxRot = math.pi * 2

                    limits = joint.jointMotion.rotationLimits
                    if (limits.isMinimumValueEnabled and limits.isMaximumValueEnabled):
                        minRot = limits.minimumValue
                        maxRot = limits.maximumValue

                    rev = inputs.addFloatSliderCommandInput(
                        commandName + joint.name,
                        joint.name,
                        &#39;rad&#39;,
                        minRot,
                        maxRot,
                        False)
                    rev.valueOne = joint.jointMotion.rotationValue

            command.isAutoExecute = True
                
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
        
        global joints
        joints = design.rootComponent.joints
         
        commandDefinitions = ui.commandDefinitions
        # Check the command exists or not
        cmdDef = commandDefinitions.itemById(commandName)
        if not cmdDef:
            cmdDef = commandDefinitions.addButtonDefinition(
                commandName, commandName, commandName, &#39;&#39;) 

        # Subscribe to events 
        onCommandCreated = MyCommandCreatedHandler()
        cmdDef.commandCreated.add(onCommandCreated)
        # Keep the handler referenced beyond this function
        handlers.append(onCommandCreated)
        
        # Run the command
        inputs = adsk.core.NamedValues.create()
        cmdDef.execute(inputs)

        # Prevent this module from being terminated when the script returns, 
        # because we are waiting for event handlers to fire
        adsk.autoTerminate(False)

    except:
        ui.messageBox(&#39;Failed:\n{}&#39;.format(traceback.format_exc()))</pre>
<p>-Adam</p>
