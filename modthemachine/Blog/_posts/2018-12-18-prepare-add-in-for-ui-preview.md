---
layout: "post"
title: "Prepare your add-in for UI Preview"
date: "2018-12-18 13:37:34"
author: "Adam Nagy"
categories:
  - "Adam"
  - "Announcements"
  - "Fusion 360"
original_url: "https://modthemachine.typepad.com/my_weblog/2018/12/prepare-add-in-for-ui-preview.html "
typepad_basename: "prepare-add-in-for-ui-preview"
typepad_status: "Publish"
---

<p>There is a <strong>UI Preview</strong> available in <strong>Fusion 360 </strong>providing a tabbed experience.&#0160;If you enable it then depending on which <strong>UI</strong> section your <strong>add-in</strong> is currently trying to add its buttons to, it might fail since some items were replaced with new ones.</p>
<div class="photo-wrap photo-xid-6a00e553fcbfc68834022ad382111c200c" id="photo-xid-6a00e553fcbfc68834022ad382111c200c" style="display: inline-block; width: 500px;"><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc68834022ad382111c200c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false"><img alt="UiPreview" class="asset  asset-image at-xid-6a00e553fcbfc68834022ad382111c200c img-responsive" src="/assets/image_263928.jpg" title="UiPreview" /></a>
<div class="photo-caption caption-xid-6a00e553fcbfc68834022ad382111c200c" id="caption-xid-6a00e553fcbfc68834022ad382111c200c">UI Preview</div>
</div>
<p>E.g. instead of having a single <strong>toolbar</strong> <strong>panel</strong> for <strong>Sketch</strong> with id &quot;<strong>SketchPanel</strong>&quot; in the &quot;<strong>Design</strong>&quot; environment (id=<strong>FusionSolidEnvironment</strong>), now we have multiple of those, inc. &quot;<strong>Create</strong>&quot; (id=<strong>SketchCreatePanel)</strong>, &quot;<strong>Modify</strong>&quot; (id=<strong>SketchModifyPanel)</strong>, etc</p>
<div class="photo-wrap photo-xid-6a00e553fcbfc68834022ad3a816ce200d" id="photo-xid-6a00e553fcbfc68834022ad3a816ce200d" style="display: inline-block; width: 500px;"><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc68834022ad3a816ce200d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false"><img alt="NewFusionUi" class="asset  asset-image at-xid-6a00e553fcbfc68834022ad3a816ce200d img-responsive" src="/assets/image_352541.jpg" title="NewFusionUi" /></a>
<div class="photo-caption caption-xid-6a00e553fcbfc68834022ad3a816ce200d" id="caption-xid-6a00e553fcbfc68834022ad3a816ce200d">Current vs New UI</div>
</div>
<p>In order to make your <strong>add-in </strong>work in this new environment, you should make sure that if the <strong>classic UI</strong> item is not available then you try using the <strong>new</strong> <strong>UI</strong> item instead:</p>
<pre>modelingWorkspace_ = workspaces_.itemById(&#39;FusionSolidEnvironment&#39;)
toolbarPanels_ = modelingWorkspace_.toolbarPanels
try:
    # try to add it to the classic ui item
    toolbarPanel_ = toolbarPanels_.itemById(&#39;SketchPanel&#39;)
    toolbarControlsPanel_ = toolbarPanel_.controls
except:
    # if it fails, try to add it to the new ui item
    toolbarPanel_ = toolbarPanels_.itemById(&#39;SketchCreatePanel&#39;) 
    toolbarControlsPanel_ = toolbarPanel_.controls
# etc</pre>
<p>You can easily find the list of all <strong>workspaces</strong> and <strong>toolbarPanels</strong> available using a simple <strong>python</strong> script like this:</p>
<pre>def listUIParts():
    try:
        global app, ui
        app = adsk.core.Application.get()
        ui = app.userInterface
        
        fileDialog = ui.createFileDialog()
        fileDialog.isMultiSelectEnabled = False
        fileDialog.title = &quot;Select file to save the information to&quot;
        fileDialog.filter = &#39;Text files (*.txt)&#39;
        fileDialog.filterIndex = 0
        dialogResult = fileDialog.showSave()
        
        if dialogResult == adsk.core.DialogResults.DialogOK:
            filename = fileDialog.filename
        else:
            return

        result = &#39;&#39;
        for ws in ui.workspaces:
            result += &#39;workspace name: &#39; + ws.name + &#39;, id: &#39; + ws.id + &#39;\n&#39;
            
            try:
                for tb in ws.toolbarPanels:
                    result += &#39;  toolbarPanel name: &#39; + tb.name + &#39;, id: &#39; + tb.id + &#39;\n&#39;
            except:
                result += &#39;  toolbarPanels not available\n&#39;
                
        with open(filename, &#39;w&#39;) as outputFile:        
            outputFile.writelines(result)
  
    except:
        if ui:
            ui.messageBox(&#39;Failed:\n{}&#39;.format(traceback.format_exc())) </pre>
<p>For convenience, here is the text file with the result of the above code: <span class="asset  asset-generic at-xid-6a00e553fcbfc68834022ad3c7c6c4200b img-responsive"><a href="https://modthemachine.typepad.com/files/uielements.txt">UIelements.txt</a></span></p>
<p>- Adam</p>
