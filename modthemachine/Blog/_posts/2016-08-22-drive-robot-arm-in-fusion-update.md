---
layout: "post"
title: "Drive robot arm in Fusion - update"
date: "2016-08-22 16:06:07"
author: "Adam Nagy"
categories:
  - "Adam"
  - "Fusion 360"
original_url: "https://modthemachine.typepad.com/my_weblog/2016/08/drive-robot-arm-in-fusion-update.html "
typepad_basename: "drive-robot-arm-in-fusion-update"
typepad_status: "Publish"
---

<p>Actually, modifying the <strong>Alignment Angle</strong>&nbsp;wasn't the best thing to do in my <a href="http://modthemachine.typepad.com/my_weblog/2016/08/drive-robot-arm-in-fusion.html">previous article</a>. I should have played with the&nbsp;<span class="s1"><strong>RevoluteJointMotion.</strong></span><span class="s1"><strong>rotationValue&nbsp;</strong>instead. That's what the "<strong>Drive Joints</strong>" command is modifying as well. And this way the <strong>Joint Limits</strong> will be honoured too :)</span></p>
<p><span class="s1"> <a class="asset-img-link" href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401b7c88a8cc4970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false" style="display: inline;"><img class="asset  asset-image at-xid-6a00e553fcbfc6883401b7c88a8cc4970b img-responsive" title="DriveJoints" src="/assets/image_282609.jpg" alt="DriveJoints" /></a></span></p>
<p>I also improved things a bit so that now you can select the two <strong>Revolute Joints</strong> that you want to drive using the command:</p>
<pre>#Author-
#Description-

import adsk.core, adsk.fusion, adsk.cam, traceback

# Global variable used to maintain a reference to all event handlers.
handlers = []

# Other global variables
commandName = "MoveRobot"
app = adsk.core.Application.get()
if app:
    ui = app.userInterface
    
revoluteJoint1 = None
revoluteJoint2 = None
isReverseUpDown = False
isReverseLeftRight = False
revolutionStep = 0.1

# Event handler for the keyDown event.
class MyKeyDownHandler(adsk.core.KeyboardEventHandler):
    def __init__(self):
        super().__init__()
    def notify(self, args):
        try:
            eventArgs = adsk.core.KeyboardEventArgs.cast(args)
            keyCode = eventArgs.keyCode    
                
            if keyCode == adsk.core.KeyCodes.UpKeyCode:
                if isReverseUpDown:
                    diffVal = -revolutionStep
                else:
                    diffVal = revolutionStep
                motion = revoluteJoint1.jointMotion
                motion.rotationValue = motion.rotationValue + diffVal
            elif keyCode == adsk.core.KeyCodes.DownKeyCode:
                if isReverseUpDown:
                    diffVal = revolutionStep
                else:
                    diffVal = -revolutionStep               
                motion = revoluteJoint1.jointMotion
                motion.rotationValue = motion.rotationValue + diffVal
            elif keyCode == adsk.core.KeyCodes.LeftKeyCode:
                if isReverseLeftRight:
                    diffVal = -revolutionStep
                else:
                    diffVal = revolutionStep                
                motion = revoluteJoint2.jointMotion
                motion.rotationValue = motion.rotationValue + diffVal
            elif keyCode == adsk.core.KeyCodes.RightKeyCode:
                if isReverseLeftRight:
                    diffVal = revolutionStep
                else:
                    diffVal = -revolutionStep                
                motion = revoluteJoint2.jointMotion
                motion.rotationValue = motion.rotationValue + diffVal
            
            # Refresh the view to show the change
            vp = app.activeViewport
            vp.refresh()
            
        except:
            ui.messageBox('Failed:\n{}'.format(traceback.format_exc()))              

# Event handler for the inputChanged event.
class MyInputChangedHandler(adsk.core.InputChangedEventHandler):
    def __init__(self):
        super().__init__()
    def notify(self, args):
        eventArgs = adsk.core.InputChangedEventArgs.cast(args)

        commandInput = eventArgs.input
        if commandInput.id == commandName + '_step':
            global revolutionStep
            revolutionStep = commandInput.value
        elif commandInput.id == commandName + '_reverseUpDown':
            global isReverseUpDown
            isReverseUpDown = commandInput.value
        elif commandInput.id == commandName + '_reverseLeftRight':
            global isReverseLeftRight
            isReverseLeftRight = commandInput.value
        
        
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
            
            # Subscribe to the various command events
            onInputChanged = MyInputChangedHandler()
            command.inputChanged.add(onInputChanged)
            handlers.append(onInputChanged)

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
                commandName + '_usage', 
                'Usage:', 
                'Use the arrow buttons to drive the robot arm', 2, 
                True);
            inputs.addValueInput(
                commandName + '_step', 
                'Rotation step: ',
                'deg',
                adsk.core.ValueInput.createByReal(revolutionStep))                
            inputs.addBoolValueInput(
                commandName + '_reverseUpDown',
                'Reverse Up/Down direction',
                True,
                '',
                isReverseUpDown)
            inputs.addBoolValueInput(
                commandName + '_reverseLeftRight',
                'Reverse Left/Right direction',
                True,
                '',
                isReverseLeftRight) 
                
        except:
            ui.messageBox('Failed:\n{}'.format(traceback.format_exc()))              
            
            
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
            ui.messageBox('Failed:\n{}'.format(traceback.format_exc()))        
            
            
def run(context):
    try:
        product = app.activeProduct
        design = adsk.fusion.Design.cast(product)
        if not design:
            ui.messageBox('It is not supported in current workspace, please change to MODEL workspace and try again.')
            return
            
        # Get selected Revolute Joints to work on 
        selections = app.userInterface.activeSelections
        if selections.count != 2:
            ui.messageBox("The 2 revolute joints you want to control need to be selected before running the command!")
            return
        
        global revoluteJoint1, revoluteJoint2
        revoluteJoint1 = selections.item(0).entity
        revoluteJoint2 = selections.item(1).entity            
            
        commandDefinitions = ui.commandDefinitions
        # Check the command exists or not
        cmdDef = commandDefinitions.itemById(commandName)
        if not cmdDef:
            cmdDef = commandDefinitions.addButtonDefinition(
                commandName, commandName, commandName, '') 

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
        ui.messageBox('Failed:\n{}'.format(traceback.format_exc()))
</pre>
<p><a class="asset-img-link" href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401b7c88a8ce7970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false" style="display: inline;"><img class="asset  asset-image at-xid-6a00e553fcbfc6883401b7c88a8ce7970b img-responsive" title="MoveRobotCommand" src="/assets/image_68353.jpg" alt="MoveRobotCommand" /></a></p>
<p>-Adam</p>
