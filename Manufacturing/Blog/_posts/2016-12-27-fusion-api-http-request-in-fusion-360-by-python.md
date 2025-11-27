---
layout: "post"
title: "Fusion API: HTTP Request in Fusion 360 by Python"
date: "2016-12-27 06:58:45"
author: "Xiaodong Liang"
categories:
  - "Fusion 360"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/manufacturing/2016/12/fusion-api-http-request-in-fusion-360-by-python.html "
typepad_basename: "fusion-api-http-request-in-fusion-360-by-python"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>For a session of AU China, I produced a small demo on how to connect designer, customer and vendor by Fusion 360 API and Forge. In the process, I borrowed some codes of HTTP Request from the add-in of <a href="https://www.maketime.io/">MakeTime</a>. This add-in uses a typical Python module: <a href="https://pypi.python.org/pypi/requests#downloads">requests&nbsp;</a>. I think it will be useful to extract the core section for reference.</p>
<p>After downloading the module&nbsp;<a href="https://pypi.python.org/pypi/requests#downloads">requests</a>, import it to the add-in. &nbsp;<br />
</p>
<code>import requests</code></p>
<p>GET Request<br />
<pre><code>
r = requests.get(url_for_get) 
#status code status_code = r.status_code 
#response message 
res_msg = r.text 
</code></pre></p>

<p>Post Request</p>
<pre><code>
#playload_array is the array for the parameters of the post request
#upload_file_stream is the file stream that to be uploaded to the remote server
r = requests.post(url_for_post,data=payload_array,file=upload_file_stream)

#status code
status_code = r.status_code
#response message
res_msg = r.text

</code></pre>
<p>for more detail on request module of Python, please refer to <a href="http://docs.python-requests.org/en/master/">http://docs.python-requests.org/en/master/</a>.</p>
<p>The codes below is a simply sample add-in. It posts some parameters to the server, including the base64 string of the snapshot of current view. It also submits current Fusion 360 file to the server as a file. To verify the workflow, the codes takes advantage of a test server: <a href="http://www.posttestserver.com/">http://www.posttestserver.com/</a></p>
<p>After execution, if the post succeeded, a message will be displayed in the textbox, in which an URL points to the contents and a few lines describing the post. The whole demo package is</p>
<p><span class="asset  asset-generic at-xid-6a0167607c2431970b01b8d24ae3c0970c img-responsive"><a href="http://adndevblog.typepad.com/files/myhttprequest.zip">Download MyHTTPRequest</a></span>&nbsp;&nbsp;</p>
<pre><code>
import adsk.core, adsk.fusion, adsk.cam, traceback
from .HTTPRequest import requests
import base64
import tempfile
import uuid



app = None
ui  = None
commandId = 'CommandInputHTTPRequest'
commandName = 'Command Input HTTP Request'
commandDescription = 'Command Input HTTP Request'

payload_data = dict()

handlers = [] 

class MyCommandDestroyHandler(adsk.core.CommandEventHandler):
    def __init__(self):
        super().__init__()
    def notify(self, args):
        try:
            # When the command is done, terminate the script
            # This will release all globals which will remove all event handlers
            adsk.terminate()
        except:
            if ui:
                ui.messageBox('Failed:\n{}'.format(traceback.format_exc()))

class MyCommandCreatedHandler(adsk.core.CommandCreatedEventHandler):
    def __init__(self):
        super().__init__()
    def notify(self, args):
        try:            
            ui.messageBox('fired!')
            
            <span style="background-color: #ffff00;">url = 'http://posttestserver.com/post.php'</span>
            payload_data['post body string 1'] =  'post body value 1'
            payload_data['post body string 2'] =  'post body value 2'
            payload_data['post body string 3'] =  'post body value 3'
            payload_data['post body string 4'] =  'post body value 4'
            
            # Instantiate Export Manager
            current_design_context = app.activeProduct
            export_manager = current_design_context.exportManager
            
            #temp id for the file name
            transaction_id = str(uuid.uuid1())

            #snapshot of the model
            ui.activeSelections.clear()
            output_snapshot_name = tempfile.mkdtemp()+'//'+ transaction_id +'.jpg'
            app.activeViewport.saveAsImageFile(output_snapshot_name, 300, 300)  
            #base64 string of the image
            encoded_string = ''
            with open(output_snapshot_name, "rb") as image_file:
                encoded_string = base64.b64encode(image_file.read())                
            payload_data['snapshot'] = encoded_string 
            
            
            #upload a Fusion 360 file 
            payload_data['uuid'] = transaction_id + '.f3d'
            output_file_name = tempfile.mkdtemp()+'//'+ transaction_id +'.f3d'
            options = export_manager.createFusionArchiveExportOptions(output_file_name)
            export_manager.execute(options)
            fusion_file = {'file': open(output_file_name, 'rb')}
            
            #upload a step file 
            #payload_data['uuid'] = transaction_id + '.step'
            #output_file_name = tempfile.mkdtemp()+'//'+ transaction_id +'.step'
            #options = export_manager.createSTEPExportOptions(output_file_name)
            #export_manager.execute(options)
            #temp = {'file': open(output_file_name, 'rb')}                           

             # Send to platform
            try:
                message = "Error: "

                # POST response               
                <span style="background-color: #ffff40;">res = requests.post(url, data=payload_data,files=fusion_file)</span>

                # Check status
                if res.status_code == 200:  # success
                    message = "Posting Succeeded! "  + res.text          

                else:  # failure/res.status_code==422           
                    message += str(res.status_code)

            # Connection timed out
            except requests.exceptions.ConnectTimeout:
                message += "Connection timed out."

            # Failed to connect
            except requests.exceptions.ConnectionError:
                message += "Connection erroraa."
            
            #display the message of post response 
            cmd = args.command             
            inputs = cmd.commandInputs                                  
            txtBox = inputs.addTextBoxCommandInput('postresponse', 'Post Response', '', 5, True)
            txtBox.text = message 
                    
        except:
            if ui:
                ui.messageBox('Failed:\n{}'.format(traceback.format_exc()))
                
def run(context):
    ui = None
    try:
        global app
        app = adsk.core.Application.get()
        global ui
        ui = app.userInterface

        global commandId
        global commandName
        global commandDescription

        # Create command defintion
        cmdDef = ui.commandDefinitions.itemById(commandId)
        if not cmdDef:
            cmdDef = ui.commandDefinitions.addButtonDefinition(commandId, commandName, commandDescription)

        # Add command created event
        onCommandCreated = MyCommandCreatedHandler()
        cmdDef.commandCreated.add(onCommandCreated)
        # Keep the handler referenced beyond this function
        handlers.append(onCommandCreated)

        # Execute command
        cmdDef.execute()

        # Prevent this module from being terminate when the script returns, because we are waiting for event handlers to fire
        adsk.autoTerminate(False) 

    except:
        if ui:
            ui.messageBox('Failed:\n{}'.format(traceback.format_exc())) 
</code></pre>
