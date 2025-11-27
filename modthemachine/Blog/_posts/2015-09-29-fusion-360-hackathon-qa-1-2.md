---
layout: "post"
title: "Fusion 360 Hackathon - Q&A #1 #2"
date: "2015-09-29 10:15:29"
author: "Adam Nagy"
categories:
  - "Adam"
  - "Fusion 360"
original_url: "https://modthemachine.typepad.com/my_weblog/2015/09/fusion-360-hackathon-qa-1-2.html "
typepad_basename: "fusion-360-hackathon-qa-1-2"
typepad_status: "Publish"
---

<p>So far we had two online presentations and two 1 hour Q&amp;A sessions where developers could come and ask about anything related to Fusion 360 API.</p>
<p>Here is a concise version of the discussions from the first 2 Q&amp;A sessions Brian and I had.&#0160;</p>
<p><em>Keep in mind that Fusion has a new release every months or so, and therefore if you&#39;re reading this article at a later time, some of the missing functionalities might be available.&#0160;</em></p>
<p><strong>Q: In case I develop in C++ on Windows, can you publish the application for the Mac platform too?</strong>&#0160;</p>
<p>A: We cannot for two reasons: &#0160;<br /> 1) we would need your source code to compile it on our side and that could cause legal issues&#0160;<br /> 2) we would probably break Apple/Xcode EULA by compiling projects for someone else using our developer account&#0160;&#0160;&#0160;&#0160;</p>
<p><strong>Q: Can I do the same geometry queries on sketch features as on BREP elements, e.g. asking for area and inner points?</strong>&#0160;</p>
<p>A: The sketch related object that could have area properties is the Profile object since it defines a closed planar area.&#0160; Currently this object only supports topology information and doesn’t expose any area functionality.&#0160;</p>
<p><strong>Q: Can I check whether a point is an inner point of a geometry (so somewhat the inverse of getting an inner point)?</strong>&#0160;</p>
<p>A: For BRepFace it is possible to find out if a point lies on the face.&#0160; This is done using the isParameterOnFace method of the SurfaceEvalutator that you can get from a face.&#0160;&#0160;&#0160;</p>
<p><strong>Q: Is there any object equivalent to the RegionProperties object in the Inventor API?</strong>&#0160;</p>
<p>A: No there is not.&#0160;&#0160;</p>
<p><strong>Q: Is there some sort of UI guideline when creating apps?</strong>&#0160;</p>
<p>A: It is work-in-progress. Brian will also add a section to the help file on where to place your command buttons on the toolbar depending on the functionality they provide.&#0160;&#0160;&#0160;</p>
<p><strong>Q: Is there a simple way of sorting the browser tree?</strong>&#0160;</p>
<p>A: No, and there is no direct access to the browser tree at all.&#0160;</p>
<p><strong>Q: Which tool could be recommended to display calculated scalar and vector fields in the Fusion 360 API (like client graphics in the Inventor API)?</strong>&#0160;</p>
<p>A: There is nothing for that at the moment as there is no client graphics API in Fusion yet.&#0160;&#0160;&#0160;</p>
<p><strong>Q: Can I add buttons to places other than the toolbar - e.g. to the browser?&#0160;</strong>&#0160;</p>
<p>A: No.&#0160;</p>
<p><strong>Q: Is a custom command automatically linked to the &quot;edit feature&quot; in the history bar (timeline), so it relaunches with the last settings when a user wants to do changes (like e.g. with press/pull command)</strong>&#0160;</p>
<p>A: The timeline only shows features, which means that what you would need is a client feature, but that is not available at the moment.&#0160;&#0160;</p>
<p><strong>Q: Is there any way to display set of triangles, e.g. for a mesh?</strong>&#0160;</p>
<p>A: The only way is sketch lines at the moment, but depending on the amount of those, it could be costly.&#0160;</p>
<p><strong>Q: Can I do Boolean operations on the solid bodies?</strong>&#0160;</p>
<p>A: You would need to use the combine feature for that. You could not do it just in-memory using transient objects.&#0160;</p>
<p><strong>Q: Is it possible to access non fusion files (e.g. xml or json) from the data panel project or do they have to be stored locally (e.g. with the installation).&#0160;</strong>&#0160;</p>
<p>A: You can only open design files and only in the UI - i.e. you cannot open them in the background just to read some information from them. Locally stored files can be read in the background as well - see &quot;<strong>Parameter I/O</strong>&quot; from <a href="https://apps.autodesk.com/FUSION/en/Detail/Index?id=appstore.exchange.autodesk.com%3aparameterio_macos%3aen">https://apps.autodesk.com/FUSION/en/Detail/Index?id=appstore.exchange.autodesk.com%3aparameterio_macos%3aen</a>&#0160;&#0160;&#0160;</p>
<p><strong>Q: Can I include extra files with the installation package of the app?</strong>&#0160;</p>
<p>A: Yes you can. We have sample apps on the Autodesk Exchange store which do that too, e.g. &quot;<strong>Parameter I/O</strong>&quot; sample. &#0160;</p>
<p><strong>Q: Can you get the data from a different project than the one the user is currently using. (something like setting up a library with components and metadata that I than want to share with others)</strong>&#0160;</p>
<p>A: The API currently only provides access to your own projects but this should be available in the future so you have access to all of the same data as you see in the Data Panel.&#0160;&#0160;&#0160;</p>
<p><strong>Q: Could you explain the purpose of text commands bar in the workspace? Can we use it to add points, sketches?</strong>&#0160;&#0160;</p>
<p>A: These are for internal testing.&#0160; Currently it’s not possible to access API created commands through the text commands.&#0160;</p>
<p><strong>Q: Are there any plans for more advanced support of external render engines? I mean event system, ability to draw in render workspace, more organized material API, etc</strong>&#0160;</p>
<p>A: We want to continue to expose through the API the functionality you see in the user-interface.&#0160; There are some areas related to rendering where the API is incomplete.&#0160; Beyond that, there has not been any discussion of additional API only functionality to expose.&#0160;</p>
<p><strong>Q: How to turn on &#39;do not capture design history&#39;?</strong>&#0160;</p>
<p>A: The Python code below sets the preference to create a Direct Modeling design, creates a new design, and then sets it back to the original value.&#0160;</p>
<pre>def createDirectModelDocument(): 
    ui = None 
    try: 
        app = adsk.core.Application.get() 
        ui  = app.userInterface 
 
        # Get the Fusion specific Design preferences.         
        desPrefs = app.preferences.productPreferences.itemByName(&#39;Design&#39;) 
 
        # Save the current value of the setting. 
        initialValue = desPrefs.defaultDesignType 
 
        # Set the value to create a direct edit design.         
        desPrefs.defaultDesignType = \ 
                 adsk.fusion.DefaultDesignTypeOptions.DirectDesignTypeOption 
 
        # Create a new document. 
        app.documents.add(adsk.core.DocumentTypes.FusionDesignDocumentType) 
 
        # Set the preference back to the initial value.         
        desPrefs.defaultDesignType = initialValue 
         
        ui.messageBox(&#39;Success&#39;) 
    except: 
        if ui: 
            ui.messageBox(&#39;Failed:\n{}&#39;.format(traceback.format_exc()))</pre>
<p><strong>Q: What happens if you turn on 3D sketches (FusionProductPreferences is3DSketchingAllowed)?</strong>&#0160;</p>
<p>A: The preference to allow 3D sketches is controlling the UI and doesn’t have any impact on the API.&#0160; Sketches in Fusion are always 3D and if these setting this preference to not allow 3D forces the user to always draw in the X-Y plane of the active sketch.&#0160;</p>
<p><strong>Note:</strong> Brian will have a presentation <a href="http://fusion360hackathon.com/webinars" target="_self">today as well</a>. Today&#39;s topic is &quot;<strong>Introduction to the Fusion object model</strong>&quot;</p>
<p>-Adam</p>
