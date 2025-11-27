---
layout: "post"
title: "Export profile sketch points to CSV "
date: "2020-12-15 12:58:36"
author: "Adam Nagy"
categories:
  - "Adam"
  - "Fusion 360"
  - "Python"
original_url: "https://modthemachine.typepad.com/my_weblog/2020/12/export-profile-sketch-points-to-csv.html "
typepad_basename: "export-profile-sketch-points-to-csv"
typepad_status: "Publish"
---

<p>There is a <strong>Python</strong> script part of the <strong>Fusion 360</strong> installation that can import points from a <strong>CSV</strong> file into a <strong>spline</strong>:<br /><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc68834026bdeade257200c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="ImportSpline" class="asset  asset-image at-xid-6a00e553fcbfc68834026bdeade257200c img-responsive" src="/assets/image_785201.jpg" title="ImportSpline" /></a><br />We could use the source code of that as a starting point to implement the reverse: export the points of a <strong>spline</strong>. <br /><br />In this case, however, I&#39;m changing it to export the points of a <strong>profile </strong>instead: one consisting of many short sketch lines forming a closed loop.<br /><img alt="Profile" class="asset  asset-image at-xid-6a00e553fcbfc68834026bdeade303200c img-responsive" src="/assets/image_612884.jpg" title="Profile" /><br />Because it&#39;s a closed loop, therefore it&#39;s enough to just export the coordinates of the <strong>start sketch point</strong> of each line as we iterate through the sketch lines.&#0160;</p>
<pre>import adsk.core, adsk.fusion, traceback
import io

def run(context):
    ui = None
    try:
        app = adsk.core.Application.get()
        ui  = app.userInterface
        # Get all components in the active design.
        product = app.activeProduct
        design = adsk.fusion.Design.cast(product)
        title = &#39;Export Spline csv&#39;
        if not design:
            ui.messageBox(&#39;No active Fusion design&#39;, title)
            return

        if app.userInterface.activeSelections.count != 1:
            ui.messageBox(&#39;Select profile before running command&#39;, title)
            return

        profile = app.userInterface.activeSelections.item(0).entity
        if not type(profile) is adsk.fusion.Profile:
            ui.messageBox(&#39;Selected object needs to be a profile&#39;, title)
            return

        dlg = ui.createFileDialog()
        dlg.title = &#39;Save CSV File&#39;
        dlg.filter = &#39;Comma Separated Values (*.csv)&#39;
        if dlg.showSave() != adsk.core.DialogResults.DialogOK :
            return
        
        filename = dlg.filename

        with io.open(filename, &#39;w&#39;, encoding=&#39;utf-8-sig&#39;) as f:
            profile = adsk.fusion.Profile.cast(profile)
            loop = profile.profileLoops.item(0)
            curves = loop.profileCurves
            for curve in curves:
                curve = adsk.fusion.ProfileCurve.cast(curve)
                entity = curve.sketchEntity
                point = entity.startSketchPoint
                point = adsk.fusion.SketchPoint.cast(point)
                geometry = point.geometry
                f.write(str(geometry.x) + &quot;,&quot; + str(geometry.y) + &quot;\n&quot;)

    except:
        if ui:
            ui.messageBox(&#39;Failed:\n{}&#39;.format(traceback.format_exc()))</pre>
<p>In order to use the above code, you can simply create a new <strong>Python Script</strong> in the &quot;<strong>Scripts and Add-Ins</strong>&quot; dialog and replace the code in the generated <strong>Python</strong> file with the one shown above - see&#0160;<a href="https://help.autodesk.com/view/fusion360/ENU/?guid=GUID-9701BBA7-EC0E-4016-A9C8-964AA4838954">Creating a Script or Add-In</a> &#0160;</p>
<p>Here is the result produced by the script:</p>
<p><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc68834026be42ceb4b200d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="TestCsv" class="asset  asset-image at-xid-6a00e553fcbfc68834026be42ceb4b200d img-responsive" src="/assets/image_31698.jpg" title="TestCsv" /></a></p>
<p>-Adam</p>
