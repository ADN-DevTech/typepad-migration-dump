---
layout: "post"
title: "Fusion 360 API: Customized Export Dialog"
date: "2017-08-20 23:52:49"
author: "Xiaodong Liang"
categories:
  - "Fusion 360"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/manufacturing/2017/08/fusion-360-api-customized-export-dialog.html "
typepad_basename: "fusion-360-api-customized-export-dialog"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>This is a solution described in the <a href="https://forums.autodesk.com/t5/fusion-360-api-and-scripts/export-dialog-path/td-p/7256064">forum post</a>. The blog is basically for making the related codes be more searchable.&#0160;</p>
<p>The built-in dialog of [Export] is not customizable currently. While the API provides an object FileDialog that can be a workaround. i.e. make your own Export dialog and specify the parameters you would like to have such as file path and file type. This dialog is still a traditional type. I will write another blog on how to make an HTML dialog like the same style of built-in Export dialog.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09bafeb3970d-pi" style="display: inline;"><img alt="Screen Shot 2017-08-21 at 2.49.43 PM" class="asset  asset-image at-xid-6a0167607c2431970b01bb09bafeb3970d img-responsive" src="/assets/image_9b613e.jpg" title="Screen Shot 2017-08-21 at 2.49.43 PM" /></a></p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<pre><code>
import adsk.core, adsk.fusion, traceback

def run(context):
    ui = None
    try:
        app = adsk.core.Application.get()
        ui = app.userInterface
        filedlg = ui.createFileDialog()
        filedlg.initialDirectory = &#39;/Users&#39;
        filedlg.filter = &#39;*.f3d&#39;
        if filedlg.showSave() == adsk.core.DialogResults.DialogOK:
            design = adsk.fusion.Design.cast(app.activeProduct)
            option = design.exportManager.createFusionArchiveExportOptions(filedlg.filename, design.rootComponent)
            design.exportManager.execute(option)
    except:
      if ui:
          ui.messageBox(&#39;Failed:\n{}&#39;.format(traceback.format_exc()))

</code></pre>
