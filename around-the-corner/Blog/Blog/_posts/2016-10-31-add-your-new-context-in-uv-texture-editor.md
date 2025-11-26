---
layout: "post"
title: "Add your new context in UV Texture Editor"
date: "2016-10-31 12:28:00"
author: "Zhong Wu"
categories:
  - "C++"
  - "Maya"
  - "Modeling"
  - "Plug-in"
  - "Samples"
  - "UI"
  - "Zhong Wu"
original_url: "https://around-the-corner.typepad.com/adn/2016/11/add-your-new-context-in-uv-texture-editor.html "
typepad_basename: "add-your-new-context-in-uv-texture-editor"
typepad_status: "Publish"
---

<p>Well, it’s pretty easy to add a command into Maya, and I also believe most of our plugin developers know how to add a context/tool within Maya (Context is actually presented with the name “tool” in Maya), if you are not familiar with that, please refer the <a href="http://help.autodesk.com/view/MAYAUL/2017/ENU/?guid=__files_Command_plugins_htm">Command plug-ins section of help doc</a> for the details. In general, contexts in Maya are modes which define how mouse interaction will be interpreted. A context can execute commands, modify the current selection, or perform drawing operations, etc. In addition, the context can draw the cursor differently denoting the context. </p>  <p>But how to support a context in Maya UV Texture Editor? Maya 2016 introduced 2 proxy classes to help your achieve that, <a href="http://help.autodesk.com/view/MAYAUL/2017/ENU/?guid=__cpp_ref_class_m_px_tex_context_html">MPxTexContext</a> and <a href="http://help.autodesk.com/view/MAYAUL/2017/ENU/?guid=__cpp_ref_class_m_px_poly_tweak_u_v_interactive_command_html">MPxPolyTweakUVInteractiveCommand</a>, let’s first take a look at them. </p>  <p>MPxTexContext is derived from class MPxContext, and it should work similar as MPxContext, but this is for you to define any contexts that working on UV Editor. The methods including some standard virtual methods that require to be overridden as follow, they are doing same thing as those in MPxContext, nothing mysterious:</p>  <pre class="brush:cpp;toolbar: false;">virtual MStatus doPress (MEvent &amp;event, MHWRender::MUIDrawManager &amp;drawMgr, const MHWRender::MFrameContext &amp;context)  
virtual MStatus doRelease (MEvent &amp;event, MHWRender::MUIDrawManager &amp;drawMgr, const MHWRender::MFrameContext &amp;context) 
virtual MStatus doDrag (MEvent &amp;event, MHWRender::MUIDrawManager &amp;drawMgr, const MHWRender::MFrameContext &amp;context) 
virtual MStatus doPtrMoved (MEvent &amp;event, MHWRender::MUIDrawManager &amp;drawMgr, const MHWRender::MFrameContext &amp;context) </pre>

<p>Besides above virtual methods, the class also provides some helper methods as follow, you can get the meaning by the method name, if not, check the SDK Doc should provide you everything:</p>

<pre class="brush:cpp;toolbar: false;">void 	viewToPort (double xView, double yView, short &amp;xPort, short &amp;yPort) const
void 	portToView (short xPort, short yPort, double &amp;xView, double &amp;yView) const
void 	viewRect (double &amp;left, double &amp;right, double &amp;bottom, double &amp;top) const
void 	portSize (double &amp;width, double &amp;height) const</pre>

<p><i></i></p>

<p>The last one that need to be mentioned is getMarqueeSelection, it’s used when the user performs a selection within the uv editor, you will see the usage within the sample later.</p>

<pre class="brush:cpp;toolbar: false;">static bool  getMarqueeSelection (double xMin, double yMin, double xMax, double yMax, const MSelectionMask &amp;mask, bool bPickSingle, bool bIgnoreSelectionMode, MSelectionList &amp;selectionList)</pre>

<p>Ok, now let’s take a quick check for class MPxPolyTweakUVInteractiveCommand, it’s the base class for UV editing interactive commands on polygonal objects. According to the doc, the purpose of this tool command class is to simplify the process of moving UVs on a polygonal object. The user is only required to provide the new positions of the UVs that being modified, and finalize at the end of editing. </p>

<p>First, some virtual methods same as in MPxToolCommand that you can override as follow:</p>

<pre class="brush:cpp;toolbar: false;">virtual bool 	isUndoable () const
virtual MStatus 	doIt (const MArgList &amp;args)
virtual MStatus 	cancel ()
virtual MStatus 	finalize ()</pre>

<p>The important one is setUVs, this method can be used during the editing, to set the UV to new value.</p>

<pre class="brush:cpp;toolbar: false;">void 	setUVs (const MObject &amp;mesh, MIntArray &amp;uvList, MFloatArray &amp;uPos, MFloatArray &amp;vPos, const MString *uvSet=NULL)</pre>

<p>Now, to help you understand how to create a context/tool for UV editor, I will show you the sample code to implement a UV grab brush, it’s based on our sample “grabUV” within devkit, but to make it more understandable, I removed the unnecessary part that accepting key-press event “B”, so we do not necessary dependent on Qt anymore.</p>

<p>If you are interested, you can get the full code under your //devkit/plug-ins/, it’s a Qt project, but it’s not upgraded to Qt5 yet, if you are running the Maya version later than 2017, you have to update the code to include the QApplication under <b>qtwidgets</b> folder instead of <b>QtGui</b> to make it work: </p>

<p><i>#include</i><i> </i><i>&lt;qtwidgets/QApplication&gt;</i><i></i></p>

<p><i></i></p>

<p>To implement the context/tool, mainly, we need to create 3 classes that derived from:</p>

<p><b>1, MPxPolyTweakUVInteractiveCommand</b></p>

<p>The sample does not override much for the virtual methods, it will just use the default behavior, but will set the UV values while user is dragging during the context. </p>

<pre class="brush:cpp;toolbar: false;">class UVUpdateCommand : public MPxPolyTweakUVInteractiveCommand
{
public:
    static void *creator() { return new UVUpdateCommand; };
}; </pre>

<p><b>2, MPxTexContext</b></p>

<p>The struct BrushConfig is used to define the brush size. You can modify the size within tool setting. </p>

<p>class grabUVContext is the most important class, here, I mainly override doPtrMoved, doPress, doDrag and doRelease to implementation of UVGrab Brush, I will talk them one by one within the code. Also, drawFeedback is overridden to draw the cycle of brush, so you can see the area that will be impacted by the brush. </p>

<p><a href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d01b8d23ccc7f970c-pi"><img title="clip_image002" style="border-left-width: 0px; border-right-width: 0px; background-image: none; border-bottom-width: 0px; float: none; padding-top: 0px; padding-left: 0px; margin-left: auto; display: block; padding-right: 0px; border-top-width: 0px; margin-right: auto" border="0" alt="clip_image002" src="/assets/image_881ab5.jpg" width="369" height="374" /></a></p>

<p><b></b></p>

<pre class="brush:cpp;toolbar: false;">struct BrushConfig
{
	BrushConfig() : fSize(50.0f) {};
	float size() const { return fSize; }
	void setSize( float size ) { fSize = size; }

private:
	float fSize;
};


class grabUVContext : public MPxTexContext
{
public:

	// constructor
	grabUVContext();

	// destructor
	virtual ~grabUVContext() {} ;

	virtual MStatus	doPress ( MEvent &amp; event, MHWRender::MUIDrawManager&amp; drawMgr, const MHWRender::MFrameContext&amp; context);
	virtual MStatus	doRelease( MEvent &amp; event, MHWRender::MUIDrawManager&amp; drawMgr, const MHWRender::MFrameContext&amp; context);
	virtual MStatus	doDrag ( MEvent &amp; event, MHWRender::MUIDrawManager&amp; drawMgr, const MHWRender::MFrameContext&amp; context); 
	virtual MStatus	doPtrMoved ( MEvent &amp; event, MHWRender::MUIDrawManager&amp; drawMgr, const MHWRender::MFrameContext&amp; context);
	virtual MStatus drawFeedback ( MHWRender::MUIDrawManager&amp; drawMgr, const MHWRender::MFrameContext&amp; context);


	virtual void	getClassName(MString &amp; name) const;

	float	size() const;
	void	setSize( float size );

private:		   	 

	enum DragMode
	{
		kNormal,
		kBrushSize
	};

	BrushConfig		fBrushConfig;
	bool			fInStroke;

	MPoint			fCurrentPoint;
	MPoint			fLastPoint;
	MPoint			fBrushCenterScreenPoint;
	MPoint			fCurrentScreenPoint;
	MPoint			fLastScreenPoint;
	UVUpdateCommand *fCommand;
	MDagPath		fDagPath;
	MIntArray		fCollectedUVs;
	MGlobal::MSelectionMode	fPreviousSelectionMode;
	MSelectionMask	fPreviousSelectionMask;
}; 

grabUVContext::grabUVContext() :
	fCommand(NULL),
	fInStroke(false)
{

}

float grabUVContext::size() const 
{ 
	return fBrushConfig.size();
}

void grabUVContext::setSize( float size )
{ 
	fBrushConfig.setSize( size ); 
	MToolsInfo::setDirtyFlag(*this); 
}</pre>

<p><b>When User click left mouse, we will record the current mouse position, and collect all the UVs that would be impacted by the brush area. </b></p>

<pre class="brush:cpp;toolbar: false;">MStatus	grabUVContext::doPress ( MEvent &amp; event, MHWRender::MUIDrawManager&amp; drawMgr, const MHWRender::MFrameContext&amp; context)
{
	if ( event.mouseButton() != MEvent::kLeftMouse || 
		!event.isModifierNone() )
		return MS::kFailure;

	fInStroke = true;

	MPxTexContext::doPress(event, drawMgr, context);

	short x, y;
	event.getPosition( x, y );
	fCurrentScreenPoint = MPoint( x, y );
	fLastScreenPoint = MPoint( x, y );
	fBrushCenterScreenPoint = MPoint( x, y );

	double xView, yView;
	portToView(x, y, xView, yView);	// pos at viewrect coordinate  

	double portW, portH;
	portSize(portW, portH);

	double left, right, bottom, top;
	viewRect(left, right, bottom, top);

	double sizeInView = portW &lt; 1e-5 ? 0.0 : ( fBrushConfig.size() * (right - left) / portW );
	double sizeInViewSquare = sizeInView * sizeInView;


	fCollectedUVs.clear();

	MStatus *returnStatus = NULL;
	MSelectionMask mask = MSelectionMask::kSelectMeshUVs;
	const bool bPickSingle = false;
	MSelectionList	selectionList;

	bool bSelect = MPxTexContext::getMarqueeSelection( x - fBrushConfig.size(), y - fBrushConfig.size(), 
														x + fBrushConfig.size(), y + fBrushConfig.size(), 
														mask, bPickSingle, true, selectionList );

	if (bSelect)
	{
		MObject component;
		selectionList.getDagPath( 0, fDagPath, component );
		fDagPath.extendToShape();

		MFnMesh mesh(fDagPath);

		MString currentUVSetName;
		mesh.getCurrentUVSetName(currentUVSetName);

		MIntArray UVsToTest;
		if( component.apiType() == MFn::kMeshMapComponent )
		{
			MFnSingleIndexedComponent compFn( component );
			compFn.getElements( UVsToTest );

			for (unsigned int i = 0; i &lt; UVsToTest.length(); ++i)
			{
				float u, v;
				MStatus bGetUV = mesh.getUV(UVsToTest[i], u, v, ¤tUVSetName);
				if (bGetUV == MS::kSuccess)
				{
					float distSquare = ( u - xView ) * ( u - xView ) + ( v - yView ) * ( v - yView );
					if ( distSquare &lt; sizeInViewSquare )
						fCollectedUVs.append(UVsToTest[i]);
				}
			}
        }

		// position in view(world) space. 
		fLastPoint = MPoint( xView, yView, 0.0 );
		fCurrentPoint = MPoint( xView, yView, 0.0 );
	}
    
	return MS::kSuccess;
}</pre>

<p><b>When user release the mouse button, it will call the fCommand-&gt;finalize() to finish</b><b> the editing of uv.</b><b> </b><b></b><b></b></p>

<pre class="brush:cpp;toolbar: false;">MStatus	grabUVContext::doRelease( MEvent &amp; event, MHWRender::MUIDrawManager&amp; drawMgr, const MHWRender::MFrameContext&amp; context)
{
	fInStroke = false;

	if (fCommand)
		fCommand-&gt;finalize();
	fCommand = NULL;

	MPxTexContext::doRelease(event, drawMgr, context);
	return MS::kSuccess;
}</pre>

<p><b>While user is dragging, it will calculate the new values for fCollectedUVs that we collected during doPress() method, and then use fCommand-&gt;setUVs() to set all the new UV values.</b></p>

<p><b></b></p>

<pre class="brush:cpp;toolbar: false;">MStatus	grabUVContext::doDrag ( MEvent &amp; event, MHWRender::MUIDrawManager&amp; drawMgr, const MHWRender::MFrameContext&amp; context)
{
	if (event.mouseButton() != MEvent::kLeftMouse || 
		!event.isModifierNone() )
		return MS::kFailure;

	MPxTexContext::doDrag(event, drawMgr, context);

	short x, y;
	event.getPosition( x, y );
	fLastScreenPoint = fCurrentScreenPoint;
	fCurrentScreenPoint = MPoint( x, y );

	double xView, yView;
	portToView(x, y, xView, yView);	// pos at viewrect coordinate

	fLastPoint = fCurrentPoint;
	fCurrentPoint = MPoint( xView, yView, 0.0 );



	fBrushCenterScreenPoint = MPoint( x, y );

	MFloatArray uUVsExported;
	MFloatArray vUVsExported;

	const MVector vec = fCurrentPoint - fLastPoint;

	if (!fCommand)
	{
		fCommand = (UVUpdateCommand *)(newToolCommand());
	}
	if (fCommand)
	{
		MFnMesh mesh(fDagPath);
		MString currentUVSetName;
		mesh.getCurrentUVSetName(currentUVSetName);

		int nbUVs = mesh.numUVs(currentUVSetName);
		MDoubleArray pinData;
		MUintArray uvPinIds;
		MDoubleArray fullPinData;
		mesh.getPinUVs(uvPinIds, pinData, ¤tUVSetName);
		int len = pinData.length();

		fullPinData.setLength(nbUVs);
		for (unsigned int i = 0; i &lt; nbUVs; i++) {
			fullPinData[i] = 0.0;
		}
		while( len-- &gt; 0 ) {
			fullPinData[uvPinIds[len]] = pinData[len];
		}

		MFloatArray uValues;
		MFloatArray vValues;
		float pinWeight = 0;
		for (unsigned int i = 0; i &lt; fCollectedUVs.length(); ++i)
		{
			float u, v;
			MStatus bGetUV = mesh.getUV(fCollectedUVs[i], u, v, ¤tUVSetName);
			if (bGetUV == MS::kSuccess)
			{
				pinWeight = fullPinData[fCollectedUVs[i]];
				u += (float)vec[0]*(1-pinWeight);
				v += (float)vec[1]*(1-pinWeight);
				uValues.append( u );
				vValues.append( v ); 
			}
		}
		fCommand-&gt;setUVs( mesh.object(), fCollectedUVs, uValues, vValues, ¤tUVSetName );
	}

	return MS::kSuccess;
}</pre>

<p><b>When move the mouse, we just record the current point for </b><b>fCurrentScreenPoint, fLastScreenPoint and fBrushCenterScreenPoint.</b><b></b></p>

<pre class="brush:cpp;toolbar: false;">MStatus grabUVContext::doPtrMoved( MEvent &amp; event, MHWRender::MUIDrawManager&amp; drawMgr, const MHWRender::MFrameContext&amp; context)
{
	MPxTexContext::doPtrMoved(event, drawMgr, context);

	double portW, portH;
	portSize(portW, portH);

	short x, y;
	event.getPosition( x, y );

	y = short(portH) - y;

	fCurrentScreenPoint = MPoint( x, y );
	fLastScreenPoint = MPoint( x, y );
	fBrushCenterScreenPoint = MPoint( x, y );

	return MS::kSuccess;
}</pre>

<p><b>Draw a cycle with the specified size to present the brush area. </b></p>

<pre class="brush:cpp;toolbar: false;">MStatus grabUVContext::drawFeedback(MHWRender::MUIDrawManager&amp; drawMgr, const MHWRender::MFrameContext&amp; context)
{
	// to draw the brush ring.
	drawMgr.beginDrawable();
	{
		drawMgr.setColor( MColor(1.f, 1.f, 1.f) ); 
		drawMgr.setLineWidth( 2.0f );
		drawMgr.circle2d(MPoint(fBrushCenterScreenPoint.x, fBrushCenterScreenPoint.y), fBrushConfig.size());
	}				
	drawMgr.endDrawable();

	return MS::kSuccess;
}

void grabUVContext::getClassName( MString &amp; name ) const
{
	name.set(&quot;grabUV&quot;);
}</pre>

<p><b></b></p>

<p><b>3. MPxContextCommand</b></p>

<p>This is a context command, and it should be the same as you do with a normal context, it’s used to define the special command for creating contexts. You can refer <a href="http://help.autodesk.com/view/MAYAUL/2017/ENU/?guid=__files_Command_plugins_MPxContextCommand_htm">MPxContextCommand</a> for more details if you are not familiar with. But it’s nothing new, we just use this to create our context of “grabUVContext”.</p>

<p><b></b></p>

<pre class="brush:cpp;toolbar: false;">class grabUVContextCommand : public MPxContextCommand
{
public:	 
					grabUVContextCommand();
	virtual MStatus		doEditFlags();
	virtual MStatus		doQueryFlags();
	virtual MStatus		appendSyntax();
	virtual MPxContext* makeObj();
	static void*		creator();

protected:
	grabUVContext*		fGrabUVContext;
};		    

grabUVContextCommand::grabUVContextCommand() {}

MPxContext* grabUVContextCommand::makeObj()
//
// Description
//    When the context command is executed in maya, this method
//    be used to create a context.
//
{
	fGrabUVContext = new grabUVContext();
	return fGrabUVContext;
}

void* grabUVContextCommand::creator()
{
	return new grabUVContextCommand;
}

MStatus grabUVContextCommand::doEditFlags()
{
	MStatus status = MS::kSuccess;

	MArgParser argData = parser();

	if (argData.isFlagSet(kSizeFlag)) {
		double size;
		status = argData.getFlagArgument(kSizeFlag, 0, size);
		if (!status) {
			status.perror(&quot;size flag parsing failed.&quot;);
			return status;
		}
		fGrabUVContext-&gt;setSize( float(size) );
	}

	return MS::kSuccess;
}

MStatus grabUVContextCommand::doQueryFlags()
{
	MArgParser argData = parser();

	if (argData.isFlagSet(kSizeFlag)) {
		setResult(fGrabUVContext-&gt;size());
	}

	return MS::kSuccess;
}

MStatus grabUVContextCommand::appendSyntax()
{
	MSyntax mySyntax = syntax();

	if (MS::kSuccess != mySyntax.addFlag(kSizeFlag, kSizeFlagLong,
		MSyntax::kDouble)) {
			return MS::kFailure;
	}

	return MS::kSuccess;
}</pre>

<p>If you want to check the main code of this sample demonstrated above, please check my <a href="https://gist.github.com/JohnOnSoftware/159662bbba1953c1a318dcca5878d86b">Github Gist</a>.</p>

<p>Besides the above 3 main classes, you also need to register/unregister the context by registerContextCommand/deregisterContextCommand, should be same as a normal context, and also need to provide the Tool property sheets including grabUVProperties.mel and grabUVValues.mel, if you are not clear about this, check the <a href="http://help.autodesk.com/view/MAYAUL/2017/ENU/?guid=__files_Command_plugins_Tool_property_sheets_htm">Tool property sheets</a> section for the details. </p>
