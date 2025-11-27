---
layout: "post"
title: "Fusion API: Usage of Mouse Event"
date: "2016-09-21 05:07:18"
author: "Xiaodong Liang"
categories: []
original_url: "https://adndevblog.typepad.com/manufacturing/2016/09/fusion-api-usage-of-mouse-event.html "
typepad_basename: "fusion-api-usage-of-mouse-event"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>  <p>Custom actions with mouse are widely used in an add-in. Fusion provides MouseEvents that monitors the events. The custom Command object contains the corresponding functions such as Command.mouseClick(), Command.mouseMove() and Command.mouseWheel() etc. By this methods, the add-in will hook the mouse behaviors and grab the data for custom workflow.</p>  <p><span lang="EN-US">The typical steps to use MouseEvent are (take Python as an example)</span></p>  <p>1. Define the custom class (say MyMouseClickHandler) of the corresponding event which derives from adsk.core.MouseEventHandle </p>  <pre><code>
# Event handler for the mouseClick event.
<span style="background-color: #ffff00">class MyMouseClickHandler(adsk.core.MouseEventHandler):</span>
    def __init__(self):
        super().__init__()
    def notify(self, args):
        try:
            eventArgs = adsk.core.MouseEventArgs.cast(args)
            
            <span style="background-color: #ffff00">pstn = eventArgs.position </span>           
            txtBox = eventArgs.firingEvent.sender.commandInputs.itemById('clickResults')
            txtBox.text = str(pstn.x) + ', ' + str(pstn.y)
        except:
            ui = adsk.core.Application.get().userInterface
            if ui:
                ui.messageBox('Failed:\n{}'.format(traceback.format_exc()))
</code></pre>

<p>2. In the class of ButtonDefinition.commandCreated, new an object of MyMouseClickHandler, and delegate with Command.mouseClick</p>

<pre><code>
class SampleCommandCreatedEventHandler(adsk.core.CommandCreatedEventHandler):
    def __init__(self):
        super().__init__()
    def notify(self, args):
        try:        
            eventArgs = adsk.core.CommandCreatedEventArgs.cast(args)
            cmd = eventArgs.command
            inputs = cmd.commandInputs
          
            inputs.addTextBoxCommandInput('clickResults', 'Click', '', 1, True)
            
            # Connect to the execute event.
            onExecute = SampleCommandExecuteHandler()
            cmd.execute.add(onExecute)
            handlers.append(onExecute)
            
           <span style="background-color: #ffff00"> # Connect to mouse click event.
            onMouseClick = MyMouseClickHandler()
            cmd.mouseClick.add(onMouseClick)
            handlers.append(onMouseClick)</span>
           
        except:
            ui = adsk.core.Application.get().userInterface
            if ui:
                ui.messageBox('Failed:\n{}'.format(traceback.format_exc()))
</code></pre>

<p>Then, in <span lang="EN-US">MyMouseClickHandler, you can grab the data of clicking and pass them to the next workflow.</span></p>

<p><span lang="EN-US">This is a demo project of Python. It delegates all mouse events, and display the data in the dialog. It is written by my colleague<a href="http://forums.autodesk.com/t5/user/viewprofilepage/user-id/1638728"> Jack Li</a> in engineer team (Thanks Jack).</span></p>

<p><span lang="EN-US"><span class="asset  asset-generic at-xid-6a0167607c2431970b01b7c8958217970b img-responsive"><a href="http://adndevblog.typepad.com/files/mousesample.zip">Download Python MouseSample</a></span></span></p>

<p><span lang="EN-US">I converted the code to <a class="zem_slink" title="C++" href="http://en.wikipedia.org/wiki/C%2B%2B" rel="wikipedia" target="_blank">C++</a> project. </span></p>

<p><span lang="EN-US"><span class="asset  asset-generic at-xid-6a0167607c2431970b01b8d21f525d970c img-responsive"><a href="http://adndevblog.typepad.com/files/mousesample-1.zip">Download C++ MouseSample</a></span></span></p>

<p><span lang="EN-US">The steps are similar:</span></p>

<p><span lang="EN-US">1. Define&#160; the custom class of the event (MyMouseClickHandler)</span></p>

<pre><code>
class MyMouseClickHandler : public MouseEventHandler
{
public:
	void notify(const Ptr&amp; eventArgs) override
	{


		Ptr firingEvent = eventArgs-&gt;firingEvent();
		if (!firingEvent)
			return;

		Ptr<command></command> command = firingEvent-&gt;sender();
		if (!command)
			return;

		Ptr inputs = command-&gt;commandInputs();
		if (!inputs)
			return;

		 
		//get the data of the clicking position
		std::string str = &quot;{&quot;;
		str += std::to_string(eventArgs-&gt;position()-&gt;x());
		str += &quot;,&quot;;
		str += std::to_string(eventArgs-&gt;position()-&gt;y());
		str += &quot;}&quot;;
		
		//display the data in the custom dialog
		Ptr  txtBox = inputs-&gt;itemById(&quot;clickResults&quot;);
		txtBox-&gt;text(str);

	}
};
</code></pre>

<p><span lang="EN-US">2. Delegate an object of MyMouseClickHandler with Command.mouseClick</span></p>

<pre><code>
// CommandCreated event handler.
class OnCommandCreatedEventHandler : public CommandCreatedEventHandler
{
public:
	void notify(const Ptr&amp; eventArgs) override
	{
		if (eventArgs)
		{
			Ptr<command></command> cmd = eventArgs-&gt;command();
			if (cmd)
			{
				// Connect to the CommandExecuted event.
				Ptr onExec = cmd-&gt;execute();
				if (!onExec)
					return;
				bool isOk = onExec-&gt;add(&amp;onExecuteHander_);
				if (!isOk)
					return;				 

				// Define the inputs.
				Ptr inputs = cmd-&gt;commandInputs();
				if (!inputs)
					return;

				//add inputs 
				inputs-&gt;addTextBoxCommandInput(&quot;clickResults&quot;, &quot;Click&quot;, &quot;&quot;,1,true);
				 

				//add mouse events

				// Connect to the MouseEvent.
				cmd-&gt;mouseClick()-&gt;add(&amp;onMouseClickHandler_); 
				 			 

			}
		}
	}
private:
	OnExecuteEventHander onExecuteHander_;
	MyMouseClickHandler onMouseClickHandler_;
	 
 } cmdCreated_;
</code></pre>

<p>&lt;</p>

<p><span lang="EN-US">Hope it could help C++ programmer. </span></p>
