---
layout: "post"
title: "How to connect Perception Neuron with Maya (Part 2)"
date: "2015-11-09 17:35:33"
author: "Zhong Wu"
categories:
  - "Animation"
  - "Autodesk"
  - "C++"
  - "Maya"
  - "Motion Capture"
  - "Plug-in"
  - "Zhong Wu"
original_url: "https://around-the-corner.typepad.com/adn/2015/11/how-to-connect-perception-neuron-with-maya-part-2.html "
typepad_basename: "how-to-connect-perception-neuron-with-maya-part-2"
typepad_status: "Publish"
---

<p>In the last post, I talked about the NeuronDataReader SDK and the general process to use these APIs, now we will talk about how to make a Maya plug-in client to receive and parse the animation data. </p>  <p>With this project, we are going to create a Maya plug-in which will:</p>  <ol>   <li>fetch the animation data from Axis Neuron application to Maya,</li>    <li>create a Neuron specific skeleton, and assign the animation data to the skeleton,</li>    <li>retarget the animation from the skeleton to another characterized model.</li> </ol>  <p>The workflow would be something like:</p>  <ol>   <li>Register the frame data callback to fetch BVH binary stream data of the skeleton. Note that the callback is invoked by a separated thread of NeuronDataReader lib. We will cache the data within the callback.</li>    <li>Create MPxThreadedDeviceNode-derived node to fetch the data from cache, use Maya system lock MSpinLock to avoid data access conflict. Set the animation data to output array of translation and rotation.</li>    <li>Create Neuron specific skeleton, and connect each items in translation and rotation array to the specific neuron joint.</li>    <li>Retarget the animation from the skeleton to another characterized model. </li> </ol>  <p>Let’s check the details on each step.</p>  <p><b>Step 1 -</b> We already mentioned in my previous post how to receive data from the Axis Neuron application using the NeuronDataReader SDK. Here, we need to define the callback first, but remember that the callback should be invoked within a separated working thread, you cannot handle any UI related work within the callback directly. Here we will cache the data of current frame, and our device thread will try to fetch the frame data later. Of course, we need to lock the resource to mutually exclusive access to the shared frame data by 2 different threads, I will talk more about this in step 2. </p>  <p>Here is the some code samples:</p>  <p><b>--- Define the skeleton data structure for every frame:</b></p>  <pre class="brush:cpp;toolbar: false;">struct FrameData{
    int nFrame;
    float data[60][6];
};</pre>

<p><b>--- Declare the current frame data as a static member of NeuronForMayaDevice:</b></p>

<pre class="brush:cpp;toolbar: false;">static FrameData           curFrameData;</pre>

<p><b>--- Declare the callback to receive the frame data:</b></p>

<pre class="brush:cpp;toolbar: false;">static void myFrameDataReceived(void* customedObj, SOCKET_REF sender, BvhDataHeaderEx* header, float* data); </pre>

<p><b>--- Implementation of myFrameDataReceived callback</b></p>

<pre class="brush:cpp;toolbar: false;">void NeuronForMayaDevice::myFrameDataReceived(void* customedObj, SOCKET_REF sender, BvhDataHeaderEx* header, float* data)
{
    if( !bLive )
        return;

    BOOL withDisp = header-&gt;WithDisp;
    BOOL withReference = header-&gt;WithReference;
    UINT32 count = header-&gt;DataCount;

    static int nFrame = 0;
    // add mutex 
    spinLock.lock();
    // push the data for each frame into a queue
    curFrameData.nFrame = nFrame++;
    for(UINT32 i = 0; i &lt; 60; ++i )
        for( UINT32 j = 0; j&lt; 6; j++ )
            curFrameData.data[i][j] = data[i*6+j]; 

    spinLock.unlock();
}</pre>

<p>Besides registering the callbacks, we also added a customized command “<b>NeuronForMayaCmd</b>” to connect or disconnect to the Axis Neuron application with TCP/IP protocol,</p>

<pre class="brush:cpp;toolbar: false;">class NeuronForMayaCmd : public MPxCommand
{
public:
    NeuronForMayaCmd() {  mDeviceName=&quot;&quot;; mStart=false;};
    virtual         ~NeuronForMayaCmd(); 
    MStatus         doIt( const MArgList&amp; args );
    static void*    creator();
    static MSyntax  newSyntax();

private:
	MStatus	    parseArgs( const MArgList&amp; args );
    static SOCKET_REF socketInfo;
    MString         mDeviceName;
    bool            mStart;
};</pre>

<p>Here is the implementation of the customized command “<b>NeuronForMayaCmd</b>”: </p>

<pre class="brush:cpp;toolbar: false;">#define kStartFlag       &quot;-s&quot;
#define kStartFlagLong   &quot;-start&quot;

#define kDeviceNameFlag       &quot;-dn&quot;
#define kDeviceNameFlagLong   &quot;-device name&quot;

SOCKET_REF NeuronForMayaCmd::socketInfo = NULL;

NeuronForMayaCmd::~NeuronForMayaCmd() {}

void* NeuronForMayaCmd::creator()
{
    return new NeuronForMayaCmd();
}

MSyntax
NeuronForMayaCmd::newSyntax()
{
    MSyntax syntax;
    syntax.addFlag(kStartFlag, kStartFlagLong, MSyntax::kBoolean);
    syntax.addFlag(kDeviceNameFlag, kDeviceNameFlagLong, MSyntax::kString );
    return syntax;
}

MStatus
NeuronForMayaCmd::parseArgs( const MArgList&amp; args )
{
    MStatus status = MStatus::kSuccess;
    MArgDatabase argData(syntax(), args);

    if (argData.isFlagSet(kStartFlag))
        status = argData.getFlagArgument(kStartFlag, 0, mStart);

    if( argData.isFlagSet(kDeviceNameFlag))
        status = argData.getFlagArgument(kDeviceNameFlag, 0, mDeviceName );

    return status;

}

MStatus NeuronForMayaCmd::doIt( const MArgList&amp; args )
{
    MStatus status;

    status = parseArgs( args ); 
    if( status != MStatus::kSuccess)
    {
        MGlobal::displayError( &quot;parameters are not correct.&quot; );
        return status;
    }

    MSelectionList sl;
    sl.add( mDeviceName );
    MObject deviceObj;
    status = sl.getDependNode(0, deviceObj );
    if(status != MStatus::kSuccess )
    {
        MGlobal::displayError(&quot;Please create your device first.&quot;);
        return status;
    }
    MFnDependencyNode fnDevice(deviceObj);
    MString ip = fnDevice.findPlug( &quot;inputIp&quot;, &amp;status ).asString();
    int     port = fnDevice.findPlug(&quot;inputPort&quot;, &amp;status).asInt();


    if( mStart ){
        // to register the 3 callback to fetch data from Neuron
        BRRegisterFrameDataCallback(NULL, NeuronForMayaDevice::myFrameDataReceived );
        BRRegisterCommandDataCallback(NULL, NeuronForMayaDevice::myCommandDataReceived );
        BRRegisterSocketStatusCallback (NULL, NeuronForMayaDevice::mySocketStatusChanged );

        socketInfo = BRConnectTo(const_cast<char   *> (ip.asChar()), port);
        if(socketInfo == NULL )
            MGlobal::displayError(&quot;Failed to connect to device.&quot;);
    }
    else
    {
        // stop socket
        BRCloseSocket( socketInfo);
    }
    
    return MStatus::kSuccess;
} </pre>

<p><b>Step 2 -</b> To fetch the animation data, we create a customized device node (NeuronForMayaDevice) that is derived from MPxThreadedDeviceNode. This is the recommended way to receive data from any device, you can get the detail information on how to use this class at <a href="http://help.autodesk.com/view/MAYAUL/2016/ENU/?guid=__cpp_ref_class_m_px_threaded_device_node_html"><i>SDK online help</i></a><i><u>.</u></i></p>

<p>Within the “NeuronForMayaDevice” class, we define 2 output array attributes, outputRotations and outputTranslations. Both of them are array of float3 type, containing the rotation &amp; translation information of all the Neuron Skeleton bones for current frame. They will be connected to Neuron skeleton bones to stream animation data. But, we will talk about this later. </p>

<p>Here I need to mention 2 issues: the 1<sup>st</sup> issue is actually a limitation of Maya; to implement “NeuronForMayaDevice”, we need to call <b><i>setRefreshOutputAttributes</i></b><i>()</i> and <b><i>createMemoryPools</i></b><i>()</i> within the node’ postConstructor() function. The <b>setRefreshOutputAttributes</b>() takes an array, but there is a limitation that the array size only can be 1, so you cannot add attributes <b>outputTranslations</b> and <b>outputRotations</b> into the array directly. To workaround this limitation, I added an assistant attribute named <b>outputData</b> and make both <b>outputTranslations</b> &amp; <b>outputRotations</b> be affected by this attribute. <b>outputData</b> will then be added into <b>setRefreshOutputAttributes</b>(), so when the <b>threadHandler</b> actually push data in the memory pool, the device thread will mark the “<b>outputData</b>” attribute dirty for that pool, and because our <b>outputData</b> attribute affects both <b>outputTranslations</b> and <b>outputRotations</b> attributes, Maya will mark them dirty too, and will call compute from the DG thread if someone needs the data.</p>

<p>Another issue is about the multiple threads access to the frame data buffer; as I mentioned in the previous post, the registered callback works in a separated working thread, so you cannot access UI within the callback. We will cache the skeleton data of current frame into a data buffer, and fetch the data buffer from the device thread. Here, we use the Maya system lock <b>MSpinLock</b> to implement the mechanism. If you want to know why and how to use the API, you can check the <a href="http://help.autodesk.com/view/MAYAUL/2016/ENU/?guid=__files_GUID_2BEF58D3_6162_4235_A6CE_3D8B0742A0AE_htm">Threading and Maya</a> section of SDK help for the details. </p>

<p>The threaded device node looks like this:</p>

<p><a href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d01bb088f9716970d-pi"><img title="clip_image002[4]" style="border-top: 0px; border-right: 0px; background-image: none; border-bottom: 0px; padding-top: 0px; padding-left: 0px; border-left: 0px; display: inline; padding-right: 0px" border="0" alt="clip_image002[4]" src="/assets/image_e6cb7f.jpg" width="743" height="599" /></a></p>

<p><b>--- Declare NeuronForMayaDevice:</b></p>

<pre class="brush:cpp;toolbar: false;">class NeuronForMayaDevice : public MPxThreadedDeviceNode
{
public:
    NeuronForMayaDevice();
    virtual 			~NeuronForMayaDevice();

    virtual void		postConstructor();
    virtual MStatus		compute( const MPlug&amp; plug, MDataBlock&amp; data );
    virtual void		threadHandler();
    virtual void		threadShutdownHandler();

    static void*		creator();
    static MStatus		initialize();

    // 3 callbacks to fetch data from Axis Neuron application
    static void myFrameDataReceived(void* customedObj, SOCKET_REF sender, BvhDataHeaderEx* header, float* data); 
    static void myCommandDataReceived(void* customedObj, SOCKET_REF sender, CommandPack* pack, void* data); 
    static void mySocketStatusChanged(void* customedObj, SOCKET_REF sender, SocketStatus status, char* message); 


public:

    static MObject      inputIp;
    static MObject      inputPort;
    static MObject      inputRecord;
    static MObject      outputTranslate;
    static MObject      outputTranslations;
    static MObject      outputRotations;
    static MSpinLock    spinLock;
static MTypeId      id;

private:
    static FrameData           curFrameData;
    static bool                bRecord;
    static bool                bLive;
};</pre>

<p><b>--- C++ implementation file for NeuronForMayaDevice</b></p>

<pre class="brush:cpp;toolbar: false;">FrameData           NeuronForMayaDevice::curFrameData;
MSpinLock           NeuronForMayaDevice::spinLock;

bool    NeuronForMayaDevice::bLive = false;
bool    NeuronForMayaDevice::bRecord = false;

MTypeId NeuronForMayaDevice::id( 0x00081051 );

MObject NeuronForMayaDevice::inputIp;
MObject NeuronForMayaDevice::inputPort;
MObject NeuronForMayaDevice::inputRecord;

MObject NeuronForMayaDevice::outputTranslate;
MObject NeuronForMayaDevice::outputTranslations;
MObject NeuronForMayaDevice::outputRotations;



NeuronForMayaDevice::NeuronForMayaDevice() 
{}

NeuronForMayaDevice::~NeuronForMayaDevice()
{
    destroyMemoryPools();
}

void NeuronForMayaDevice::postConstructor()
{
    MObjectArray attrArray;
    attrArray.append( NeuronForMayaDevice::outputTranslate );
    setRefreshOutputAttributes( attrArray );

    createMemoryPools( 24, 1, sizeof(FrameData));
}


void NeuronForMayaDevice::threadHandler()
{
    MStatus status;
    setDone( false );
    while ( ! isDone() )
    {
        // Skip processing if we are not live
        if ( ! isLive() )
            continue;

        MCharBuffer buffer;
        status = acquireDataStorage(buffer);
        if ( ! status )
            continue;

        beginThreadLoop();
        {
            FrameData* frameData = reinterpret_cast<framedata   *>(buffer.ptr());

            // add mutex here
            spinLock.lock();
            frameData-&gt;nFrame = curFrameData.nFrame;
            for(UINT32 i = 0; i &lt; 60; ++i )
                for( UINT32 j = 0; j&lt; 6; j++ )
                    frameData-&gt;data[i][j] = curFrameData.data[i][j];

            spinLock.unlock();
            pushThreadData( buffer );
        }
        endThreadLoop();
    }
    setDone( true );
}

void NeuronForMayaDevice::threadShutdownHandler()
{
    // Stops the loop in the thread handler
    setDone( true );
}

void* NeuronForMayaDevice::creator()
{
    return new NeuronForMayaDevice;
}


MStatus NeuronForMayaDevice::initialize()
{
    MStatus status;
    MFnNumericAttribute numAttr;
    MFnTypedAttribute tAttr;

    MFnStringData fnStringIp;
    MObject stringIp = fnStringIp.create(&quot;127.0.0.1&quot;);
    inputIp = tAttr.create(&quot;inputIp&quot;, &quot;ii&quot;, MFnData::kString, stringIp, &amp;status);
    MCHECKERROR(status, &quot;create input Ip&quot;);
    tAttr.setWritable(true);
    ADD_ATTRIBUTE(inputIp)

    inputPort = numAttr.create(&quot;inputPort&quot;, &quot;ip&quot;, MFnNumericData::kInt, 7001, &amp;status );
    MCHECKERROR(status, &quot;create input Port&quot;);
    numAttr.setWritable(true);
    numAttr.setReadable(false);
    //numAttr.setConnectable(false);
    ADD_ATTRIBUTE(inputPort)

    inputRecord = numAttr.create(&quot;record&quot;, &quot;ird&quot;, MFnNumericData::kBoolean, false, &amp;status );
    MCHECKERROR(status, &quot;create input Record&quot;);
    numAttr.setWritable(true);
    numAttr.setConnectable(false);
    ADD_ATTRIBUTE(inputRecord)

    outputTranslate = numAttr.create(&quot;outputTranslate&quot;, &quot;ot&quot;, MFnNumericData::k3Float, 0.0, &amp;status);
    MCHECKERROR(status, &quot;create outputTranslate&quot;);
    numAttr.setHidden(true);
    ADD_ATTRIBUTE(outputTranslate);

    outputTranslations = numAttr.create(&quot;outputTranslations&quot;, &quot;ots&quot;, MFnNumericData::k3Float, 0.0, &amp;status);
    MCHECKERROR(status, &quot;create outputTranslations&quot;);
    numAttr.setArray(true);
    numAttr.setUsesArrayDataBuilder(true); 
    ADD_ATTRIBUTE(outputTranslations);

    outputRotations = numAttr.create(&quot;outputRotations&quot;, &quot;ors&quot;, MFnNumericData::k3Float, 0.0, &amp;status);
    MCHECKERROR(status, &quot;create outputRotations&quot;);
    numAttr.setArray(true);
    numAttr.setUsesArrayDataBuilder(true); 
    ADD_ATTRIBUTE(outputRotations);

    ATTRIBUTE_AFFECTS( live, outputTranslate);
    ATTRIBUTE_AFFECTS( frameRate, outputTranslate);
    ATTRIBUTE_AFFECTS( inputIp, outputTranslate);
    ATTRIBUTE_AFFECTS( inputPort, outputTranslate);

    ATTRIBUTE_AFFECTS( live, outputTranslations);
    ATTRIBUTE_AFFECTS( frameRate, outputTranslations);
    ATTRIBUTE_AFFECTS( inputIp, outputTranslations);
    ATTRIBUTE_AFFECTS( inputPort, outputTranslations);

    ATTRIBUTE_AFFECTS( live, outputRotations);
    ATTRIBUTE_AFFECTS( frameRate, outputRotations);
    ATTRIBUTE_AFFECTS( inputIp, outputRotations);
    ATTRIBUTE_AFFECTS( inputPort, outputRotations);

    ATTRIBUTE_AFFECTS( outputTranslate, outputTranslations);
    ATTRIBUTE_AFFECTS( outputTranslate, outputRotations);

    return MS::kSuccess;
}


MStatus NeuronForMayaDevice::compute( const MPlug&amp; plug, MDataBlock&amp; block )
{
    MStatus status;
    if( plug == outputTranslate || plug.parent() == outputTranslate ||
        plug == outputTranslations ||  plug.parent() == outputTranslations ||
        plug == outputRotations || plug.parent() == outputRotations )
    {
        bLive = isLive();

        MCharBuffer buffer;
        if ( popThreadData(buffer) )
        {
            FrameData* curData = reinterpret_cast<framedata   *>(buffer.ptr());
            printf( &quot;current frame is %d \n &quot;,  curData-&gt;nFrame);
            MArrayDataHandle translationsHandle = block.outputArrayValue( outputTranslations, &amp;status );
            MCHECKERROR(status, &quot;Error in block.outputArrayValue for outputTranslations&quot;);

            MArrayDataBuilder translationsBuilder = translationsHandle.builder( &amp;status );
            MCHECKERROR(status, &quot;Error in translationsBuilder = translationsHandle.builder.\n&quot;);

            MArrayDataHandle rotationsHandle = block.outputArrayValue( outputRotations, &amp;status );
            MCHECKERROR(status, &quot;Error in block.outputArrayValue for outputRotations&quot;);

            MArrayDataBuilder rotationsBuilder = rotationsHandle.builder( &amp;status );
            MCHECKERROR(status, &quot;Error in rotationsBuilder = rotationsHandle.builder.\n&quot;);

            for(UINT32 i=0; i&lt; 60; ++i )
            {
                float3&amp; translate = translationsBuilder.addElement(i, &amp;status).asFloat3();
                MCHECKERROR(status, &quot;ERROR in translate = translationsBuilder.addElement.\n&quot;);
                translate[0] = curData-&gt;data[i][0];
                translate[1] = curData-&gt;data[i][1];
                translate[2] = curData-&gt;data[i][2];

                float3&amp; rotate = rotationsBuilder.addElement(i, &amp;status).asFloat3();
                MCHECKERROR(status, &quot;ERROR in translate = translationsBuilder.addElement.\n&quot;);
                rotate[0] = curData-&gt;data[i][3];
                rotate[1] = curData-&gt;data[i][4];
                rotate[2] = curData-&gt;data[i][5];
            }
            status = translationsHandle.set(translationsBuilder);
            MCHECKERROR(status, &quot;set translationsBuilder failed\n&quot;);

            status = rotationsHandle.set(rotationsBuilder);
            MCHECKERROR(status, &quot;set rotationsBuilder failed\n&quot;);

            block.setClean( plug );

            releaseDataStorage(buffer);
            return ( MS::kSuccess );
        }
        else
        {
            return MS::kFailure;
        }
    }
    return ( MS::kUnknownParameter );
}</pre>

<p><b>Step 3 </b>- So far, we have registered the callback to receive the animation data from the Axis neuron app, and cached the frame data as shared data buffer. Then, we created a device thread node to fetch the data and then pass them to 2 output attributes (<b>outputTranslations</b>, <b>outputRotations</b>). Now, we will create the Neuron specific skeleton, and connect each item in the translation and rotation arrays (<b>outputTranslations</b>, <b>outputRotations</b>) to the corresponding joint in the Neuron skeleton.</p>

<p>Neuron skeleton is a little different than the Maya internal skeleton. It supports 60 joints at most, and is corresponding with its MotionCapture device (Perception Neuron). You can reference the SDK Doc “<b>Appendix B: BVH header template for the hierarchy”</b> for more information. To create the Neuron skeleton, you can use several different ways to do that and make it more flexible. But here we will just create a Maya default skeleton, and export it to a MA file as a reference (by <b>Reference -&gt; Export Selection as Reference</b>), then modify the MA file to generate the MEL procedure according to the Neuron skeleton hierarchy. It is not very flexible but quick and effective at this stage.</p>

<pre class="brush:cpp;toolbar: false;">global proc createNeuronSkeleton(string $characterNodeName)
{

    createNode transform -n ($characterNodeName + &quot;_Character&quot;);
       setAttr &quot;.s&quot; -type &quot;double3&quot; 0.1 0.1 0.1 ;

    createNode joint -n ($characterNodeName + &quot;_Hips&quot;) -p ($characterNodeName + &quot;_Character&quot;);
       addAttr -s false -ci true -sn &quot;ch&quot; -ln &quot;Character&quot; -at &quot;message&quot;;
       setAttr &quot;.ro&quot; 2;
       setAttr &quot;.typ&quot; 1;

    createNode joint -n ($characterNodeName + &quot;_RightUpLeg&quot;) -p ($characterNodeName + &quot;_Hips&quot;);
       addAttr -s false -ci true -sn &quot;ch&quot; -ln &quot;Character&quot; -at &quot;message&quot;;
       setAttr &quot;.ro&quot; 2;
       setAttr &quot;.sd&quot; 2;
       setAttr &quot;.typ&quot; 2;
    …… 
} </pre>

<p>When the Neuron skeleton is created, we need to create a <b>NeuronForMayaDevice</b> node, and connect each item within outputTranslations and outputRotations attributes to the corresponding joint in the Neuron skeleton, the script should be like: </p>

<p>When the Neuron skeleton is created, we need to create a <b>NeuronForMayaDevice</b> node, and connect each item within outputTranslations and outputRotations attributes to the corresponding joint in the Neuron skeleton, the script should be like:&#160; </p>

<pre class="brush:cpp;toolbar: false;">global proc createUnitconversions(string $characterNodeName)
{
    int $i;
    for($i = 0; $i &lt; 60; ++$i)
    {
        createNode unitConversion -n ($characterNodeName + &quot;_unitConversion&quot; + $i);
        setAttr &quot;.cf&quot; 0.017453292519943295;
    }
}

global proc connectNodes(string $characterNodeName, string $neuronDeviceName)
{
    int $i;
    string $NeuronSkelNames[] = {&quot;Character&quot;,&quot;Hips&quot;,&quot;RightUpLeg&quot;,&quot;RightLeg&quot;,&quot;RightFoot&quot;,&quot;LeftUpLeg&quot;,&quot;LeftLeg&quot;,&quot;LeftFoot&quot;,&quot;Spine&quot;,&quot;Spine1&quot;,&quot;Spine2&quot;,&quot;Spine3&quot;,&quot;Neck&quot;,&quot;Head&quot;,&quot;RightShoulder&quot;,&quot;RightArm&quot;,&quot;RightForeArm&quot;,&quot;RightHand&quot;,&quot;RightHandThumb1&quot;,&quot;RightHandThumb2&quot;,&quot;RightHandThumb3&quot;,&quot;RightInHandIndex&quot;,&quot;RightHandIndex1&quot;,&quot;RightHandIndex2&quot;,&quot;RightHandIndex3&quot;,&quot;RightInHandMiddle&quot;,&quot;RightHandMiddle1&quot;,&quot;RightHandMiddle2&quot;,&quot;RightHandMiddle3&quot;,&quot;RightInHandRing&quot;,&quot;RightHandRing1&quot;,&quot;RightHandRing2&quot;,&quot;RightHandRing3&quot;,&quot;RightInHandPinky&quot;,&quot;RightHandPinky1&quot;,&quot;RightHandPinky2&quot;,&quot;RightHandPinky3&quot;,&quot;LeftShoulder&quot;,&quot;LeftArm&quot;,&quot;LeftForeArm&quot;,&quot;LeftHand&quot;,&quot;LeftHandThumb1&quot;,&quot;LeftHandThumb2&quot;,&quot;LeftHandThumb3&quot;,&quot;LeftInHandIndex&quot;,&quot;LeftHandIndex1&quot;,&quot;LeftHandIndex2&quot;,&quot;LeftHandIndex3&quot;,&quot;LeftInHandMiddle&quot;,&quot;LeftHandMiddle1&quot;,&quot;LeftHandMiddle2&quot;,&quot;LeftHandMiddle3&quot;,&quot;LeftInHandRing&quot;,&quot;LeftHandRing1&quot;,&quot;LeftHandRing2&quot;,&quot;LeftHandRing3&quot;,&quot;LeftInHandPinky&quot;,&quot;LeftHandPinky1&quot;,&quot;LeftHandPinky2&quot;,&quot;LeftHandPinky3&quot;};
 	
    for($i = 0; $i &lt; 60; ++$i)
    {
        connectAttr ($neuronDeviceName + &quot;.ots[&quot; + $i + &quot;]&quot;) ($characterNodeName + &quot;_&quot; + $NeuronSkelNames[$i] + &quot;.t&quot;);
        connectAttr ($neuronDeviceName + &quot;.ors[&quot; + $i + &quot;]&quot;) ($characterNodeName + &quot;_unitConversion&quot; + $i + &quot;.i&quot;);
        connectAttr ($characterNodeName + &quot;_unitConversion&quot; + $i + &quot;.o&quot;) ($characterNodeName + &quot;_&quot; + $NeuronSkelNames[$i] + &quot;.r&quot;);;
    }
}</pre>

<p>If you have your Axis Neuron app running, set the IP and port, run command <b>NeuronForMayaCmd</b> to connect to the Axis Neuron app, make it live, you will see the Neuron Skeleton be animated like this:</p>

<p><a href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d01b8d1753e1b970c-pi"><img title="clip_image002[6]" style="border-top: 0px; border-right: 0px; background-image: none; border-bottom: 0px; padding-top: 0px; padding-left: 0px; border-left: 0px; display: inline; padding-right: 0px" border="0" alt="clip_image002[6]" src="/assets/image_43e4f4.jpg" width="743" height="513" /></a></p>

<p><b>Step 4 –</b> At this stage everything should be ready to drive the Neuron skeleton with the real-time Motion Capture data from the device. Now, it is your turn to play with it ;). One of my colleagues created a wonderful Ironman model, and we retargeted the Neuron skeleton to the Ironman model to make it be animated. Here is the result we got. It works well, but not perfect, there are some issues we need to resolve. I will discuss about this in next post.</p>

<p>One more thing, you can access all the <a href="https://github.com/JohnOnSoftware/NeuronForMaya.git">source code</a> from Github if you are interested. </p>

<p><a href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d01b7c7eb6415970b-pi"><img title="clip_image004[4]" style="border-top: 0px; border-right: 0px; background-image: none; border-bottom: 0px; padding-top: 0px; padding-left: 0px; border-left: 0px; display: inline; padding-right: 0px" border="0" alt="clip_image004[4]" src="/assets/image_648318.jpg" width="743" height="500" /></a></p>
