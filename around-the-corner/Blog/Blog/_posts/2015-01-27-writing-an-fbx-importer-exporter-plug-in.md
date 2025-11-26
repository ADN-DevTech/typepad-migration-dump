---
layout: "post"
title: "Writing an FBX importer / Exporter plug-in"
date: "2015-01-27 04:00:11"
author: "Cyrille Fauvel"
categories:
  - "Animation"
  - "C++"
  - "Cyrille Fauvel"
  - "FBX"
  - "JSON"
  - "Linux"
  - "Mac"
  - "OpenGL"
  - "Plug-in"
  - "Visual Studio"
  - "Windows"
  - "XCode"
original_url: "https://around-the-corner.typepad.com/adn/2015/01/writing-an-fbx-importer-exporter-plug-in.html "
typepad_basename: "writing-an-fbx-importer-exporter-plug-in"
typepad_status: "Publish"
---

<p>While I am working on a WEBGL project, I wanted to share some hints I had to work through with you. The FBX documentation isn&#39;t clear on every steps, is a bit out of date even if it fairly easy to identify the pieces to update and spread all over the place. I hope that post will help on this.</p>
<center><img alt="" src="/assets/glTF_300.jpg" /></center>
<p>The project I am working on is a <a href="https://github.com/cyrillef/FBX-glTF" target="_self">glTF importer / exporter</a>. <a href="https://github.com/KhronosGroup/glTF" target="_self">glTF</a> is a specification (under development) for WEBGL asset (i.e.&#0160;glTF - the runtime asset format for WebGL, and OpenGL ES). What is nice with that specification is that it is a format that would load onto the various javascript WEBGL viewer without the need to program too much on the javascript side. So you would not need to learn any particular javascript viewing technology such as Three.js, Montage,js, Babylon.js, ...</p>
<p>In this post, I am going to explain the exporter code, but the importer code is very close with few exception such as deriving from FbxReader vs FbxWriter for an exporter.</p>
<ol>
<li>First, we create a new DLL project using &#39;Multithreaded DLL&#39; code generation. Visual Studio 2013 will create a Unicode project by default while the FBX SDK is MBCS still. Here you may change the character set to MBCS to avoid to have to convert back and force string from MBCS &lt;-&gt; UTF8/16.</li>
<li>Next, we create a plug-in class derived from FbxPlugin<br />
<pre class="brush: cpp; toolbar: false;">class IOglTF : public FbxPlugin {
	FBXSDK_PLUGIN_DECLARE(IOglTF) ;

public:
	static const char *PLUGIN_NAME ;
	static const char *PLUGIN_VERSION ;

protected:
	explicit IOglTF (const FbxPluginDef &amp;pDefinition, FbxModule pLibHandle) : FbxPlugin (pDefinition, pLibHandle) {
	}

	// Implement Fbxmodules::FbxPlugin
	virtual bool SpecificInitialize () ;
	virtual bool SpecificTerminate () { return (true) ; }

} ;
</pre>
<p>and the implementation (here we register the importer and export class that we will implement later)</p>
<pre class="brush: cpp; toolbar: false;">FBXSDK_PLUGIN_IMPLEMENT(IOglTF) ;

/*static*/ const char *IOglTF::PLUGIN_NAME =&quot;IO-glTF&quot; ;
/*static*/ const char *IOglTF::PLUGIN_VERSION =&quot;0.1.0&quot; ;

bool IOglTF::SpecificInitialize () {
	int registeredCount =0 ;
	int gltfReaderId =0, gltfWriterId =0 ;
	GetData ().mSDKManager-&gt;GetIOPluginRegistry ()-&gt;RegisterWriter (gltfWriter::Create_gltfWriter, gltfWriter::gltfFormatInfo, gltfWriterId, registeredCount, gltfWriter::FillIOSettings) ;
	GetData ().mSDKManager-&gt;GetIOPluginRegistry ()-&gt;RegisterReader (gltfReader::Create_gltfReader, gltfReader::gltfFormatInfo, gltfReaderId, registeredCount, gltfReader::FillIOSettings) ;
	return (true) ;
}</pre>
</li>
<li>Finally, to complete the plug-in part itself, we implement and export the FBXPluginRegistration() function:<br />
<pre class="brush: cpp; toolbar: false;">extern &quot;C&quot; {
	// The DLL is owner of the plug-in
	static IOglTF *pPlugin =nullptr ;

	// This function will be called when an application will request the plug-in
#if defined(_WIN64) || defined (_WIN32)
	__declspec(dllexport) void FBXPluginRegistration (FbxPluginContainer &amp;pContainer, FbxModule pLibHandle) {
#else
	void FBXPluginRegistration (FbxPluginContainer &amp;pContainer, FbxModule pLibHandle) {
#endif
		if ( pPlugin == nullptr ) {
			// Create the plug-in definition which contains the information about the plug-in
			FbxPluginDef pluginDef ;
			pluginDef.mName =FbxString (IOglTF::PLUGIN_NAME) ;
			pluginDef.mVersion =FbxString (IOglTF::PLUGIN_VERSION) ;

			// Create an instance of the plug-in.  The DLL has the ownership of the plug-in
			pPlugin =IOglTF::Create (pluginDef, pLibHandle) ;

			// Register the plug-in
			pContainer.Register (*pPlugin) ;
		}
	}
}
</pre>
</li>
<li>At step 2., we used the following symbols: Create_gltfWriter, gltfFormatInfo, FillIOSettings, that we need to implement now. These functions are used to describe what the importer/exporter classes are for, what file format they can handle, describe their IO options, etc... Here is the class declaration derived from FbxWriter<br />
<pre class="brush: cpp; toolbar: false;">class gltfWriter : public FbxWriter {
private:
	... my class members ...

public:
	gltfWriter (FbxManager &amp;pManager, int id) ;
	virtual ~gltfWriter () ;

	virtual bool FileCreate (char *pFileName) ;
	virtual bool FileClose () ;
	virtual bool IsFileOpen () ;
	virtual void GetWriteOptions () ;
	virtual bool Write (FbxDocument *pDocument) ;
	virtual bool PreprocessScene (FbxScene &amp;scene) ;
	virtual bool PostprocessScene (FbxScene &amp;scene) ;

	static FbxWriter *Create_gltfWriter (FbxManager &amp;manager, FbxExporter &amp;exporter, int subID, int pluginID) ;
	static void *gltfFormatInfo (FbxWriter::EInfoRequest request, int id) ;
	static void FillIOSettings (FbxIOSettings &amp;ios) ;

} ;
</pre>
<p>and the basic implementation</p>
<pre class="brush: cpp; toolbar: false;">gltfWriter::gltfWriter (FbxManager &amp;pManager, int id)
	: FbxWriter(pManager, id, FbxStatusGlobal::GetRef ())
{ 
}

gltfWriter::~gltfWriter () {
	FileClose () ;
}

bool gltfWriter::FileCreate (char *pFileName) {
	FbxString fileName =FbxPathUtils::Clean (pFileName) ;
	FbxString path (FbxGetApplicationDirectory ()) ;
	if ( !FbxPathUtils::Create (FbxPathUtils::GetFolderName (fileName)) )
		return (GetStatus ().SetCode (FbxStatus::eFailure, &quot;Cannot create folder!&quot;), false) ;
	
	&lt;open your file here&gt;

	return (IsFileOpen ()) ;
}

bool gltfWriter::FileClose () {
	&lt;flush and close your file here&gt;
	return (true) ;
}

bool gltfWriter::IsFileOpen () {
	&lt;is your file open?&gt;
}

void gltfWriter::GetWriteOptions () {
}

bool gltfWriter::Write (FbxDocument *pDocument) {
	FbxScene *pScene =FbxCast(pDocument) ;
	if ( !pScene )
		return (GetStatus ().SetCode (FbxStatus::eFailure, &quot;Document not supported!&quot;), false) ;

	if ( !PreprocessScene (*pScene) )
		return (false) ;

	FbxDocumentInfo *pSceneInfo =pScene-&gt;GetSceneInfo () ;

	&lt;write data in your file here&gt;

	if ( !PostprocessScene (*pScene) )
		return (false) ;
	return (true) ;
}


bool gltfWriter::PreprocessScene (FbxScene &amp;scene) {
	FbxNode *pRootNode =scene.GetRootNode () ;
	...
	return (true) ;
}

bool gltfWriter::PostprocessScene (FbxScene &amp;scene) {
	...
	return (true) ;
}

// Create your own writer - Your writer will get a pPluginID and pSubID. 
/*static*/ FbxWriter *gltfWriter::Create_gltfWriter (FbxManager &amp;manager, FbxExporter &amp;exporter, int subID, int pluginID) {
	FbxWriter *writer =FbxNew (manager, pluginID) ; // Use FbxNew instead of new, since FBX will take charge its deletion
	writer-&gt;SetIOSettings (exporter.GetIOSettings ()) ;
	return (writer) ;
}

// Get extension, description or version info about MyOwnWriter
/*static*/ void *gltfWriter::gltfFormatInfo (FbxWriter::EInfoRequest request, int id) {
	static const char *sExt [] = { &quot;gltf&quot;, 0 } ;
	static const char *sDesc [] = { &quot;glTF for WebGL (*.gltf)&quot;, 0 } ;
	static const char *sVersion [] = { &quot;0.8&quot;, 0 } ;
	static const char *sInfoCompatible [] = { &quot;-&quot;, 0 } ;
	static const char *sInfoUILabel [] = { &quot;-&quot;, 0 } ;

	switch ( pRequest ) {
		case FbxWriter::eInfoExtension:
			return (sExt) ;
		case FbxWriter::eInfoDescriptions:
			return (sDesc) ;
		case FbxWriter::eInfoVersions:
			return (sVersion) ;
		case FbxWriter::eInfoCompatibleDesc:
			return (sInfoCompatible) ;
		case FbxWriter::eInfoUILabel:
			return (sInfoUILabel) ;
		default:
			return (0) ;
	}
}

/*static*/ void gltfWriter::FillIOSettings (FbxIOSettings &amp;pIOS) {
	// Here you can write your own FbxIOSettings and parse them.
	// Example at: http://help.autodesk.com/view/FBX/2015/ENU/?guid=__files_GUID_75CD0DC4_05C8_4497_AC6E_EA11406EAE26_htm
	...
}
</pre>
</li>
</ol>
<p>This is all you need to see the exporter to appear in any FBX host application, or FBX extension which loads plug-ins. For example, if you write an application and want to load plug-in you would add code like this in your host code:</p>
<pre class="brush: cpp; toolbar: false;">FbxIOSettings *pIOSettings =FbxIOSettings::Create (_sdkManager, IOSROOT) ;
pIOSettings-&gt;SetBoolProp (IMP_FBX_MATERIAL, true) ;
...
pIOSettings-&gt;SetBoolProp (EXP_FBX_EMBEDDED, false) ;
...
_sdkManager-&gt;SetIOSettings (pIOSettings) ;

// Load plug-ins from the executable directory
FbxString path =FbxGetApplicationDirectory () ;
#if defined(_WIN64) || defined(_WIN32)
FbxString extension (&quot;dll&quot;) ;
#elif defined(__APPLE__)
FbxString extension (&quot;dylib&quot;) ;
#else // __linux
FbxString extension (&quot;so&quot;) ;
#endif
_sdkManager-&gt;LoadPluginsDirectory (path.Buffer ()/*, extension.Buffer ()*/) ;
_sdkManager-&gt;FillIOSettingsForReadersRegistered (*pIOSettings) ;
_sdkManager-&gt;FillIOSettingsForWritersRegistered (*pIOSettings) ;
</pre>
<p>Full source code posted on my github account <a href="https://github.com/cyrillef/FBX-glTF" target="_self">here</a></p>
