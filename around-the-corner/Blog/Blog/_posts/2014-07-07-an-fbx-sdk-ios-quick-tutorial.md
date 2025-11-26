---
layout: "post"
title: "An FBX SDK iOS quick tutorial"
date: "2014-07-07 06:25:00"
author: "Cyrille Fauvel"
categories:
  - "Cyrille Fauvel"
  - "FBX"
  - "iOS"
  - "Samples"
  - "XCode"
original_url: "https://around-the-corner.typepad.com/adn/2014/07/an-fbx-sdk-ios-quick-tutorial.html "
typepad_basename: "an-fbx-sdk-ios-quick-tutorial"
typepad_status: "Publish"
---

<p>I recently had a question on the FBX iOS SDK and while I had to use it on a ReCap Photo project, I wanted to share my experience with you. The SDK is the one used to write the FBXReview application on iOS. The FBX development team is working on an Android version right now and should come in a future release.</p>
<p><a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d01a3fd2c18fa970b-pi" style="display: inline;"><img alt="FbxReview" class="asset  asset-image at-xid-6a0163057a21c8970d01a3fd2c18fa970b img-responsive" src="/assets/image_a6f385.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="FbxReview" /></a></p>
<p>To minimize the efforts of writing our first iOS application using the FBX iOS SDK, we will reuse the ImportScene FBX sample and transpose it to iOS Cocoa vs a Windows or MacOS console application. When you install the FBX iOS SDK, the installer does not install any samples, documentation, etc... but you can install the MacOS version to get at leaast the source code even if there is no iOS Cocoa project coming with it.</p>
<h2>Starting a New Project</h2>
<p>We will create a blank project as the starting point for our FBX project.</p>
<ol>
<li>Install the FBX iOS SDK from <a href="http://www.autodesk.com/developfbx" target="_self">http://www.autodesk.com/developfbx</a> <br />(latest release at the time of this post is 2015.1) <br />The installer will copy files in a non-standard location /Applications/Autodesk/FBX SDK/2015.1</li>
<li>Start Xcode<br /> (using version 5.3 in this example)</li>
<li>Create a new iOS single view project (Universal)<br /> With this version of the FBX SDK (i.e. 2015.1) the simulator library does not contain the 64 bit version, so you need to change your Build Settings accordingly for ‘Architectures’ and ‘Other Linker Flags’</li>
<ol type="a">
<li>For ‘Architectures’, press the + sign on Debug and Release to add different configuration for building simulator or a real device app, and change the default standard architecture to $(ARCHS_STANDARD_32_BIT) for the simulator configurations.<br /> <strong><span style="color: #ff0000;">Note this step will not be needed in future releases as this bug has been already fixed.</span></strong></li>
</ol></ol>
<p><a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d01a73de6da77970d-pi" style="display: inline;"><img alt="Architectures" class="asset  asset-image at-xid-6a0163057a21c8970d01a73de6da77970d img-responsive" src="/assets/image_98ced7.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Architectures" /></a></p>
<ol><ol start="2" type="a">
<li>For ‘Other Linker Flags’, do almost the same thing but reference the libraries depending of the Architecture.</li>
</ol></ol>
<p><a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d01a73de6dbab970d-pi" style="display: inline;"><img alt="OtherLinkerFlags" class="asset  asset-image at-xid-6a0163057a21c8970d01a73de6dbab970d img-responsive" src="/assets/image_4f054a.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="OtherLinkerFlags" /></a></p>
<ol start="4">
<li>Add into the Project Framework dependency list the ‘libiconv.dylib’</li>
<li>Next is to tell Xcode where to find the header files. Go in ‘User Header Search Paths’ and add the following path &quot;/Applications/Autodesk/FBX SDK/2015.1/include&quot; (do not forget the &quot;). And set the ‘Always Search User Path’ to true.</li>
<li>Open your ViewController.h and change the interfece to <br />@interface ViewController : UITableViewController</li>
<li>Rename your ViewController.m into ViewController.mm</li>
<li>Open your ViewController.mm and add<br /> #import &quot;fbxsdk.h&quot;</li>
<li>Press Cmd+B to build your project. It should compile and link fine.</li>
</ol>
<h2>Copy the existing ImportScene sample code into the project</h2>
<p>For our example, I just copied an existing FBX sample and slightly modified it to my needs. There is no major modification and 99% of the code is working as is. The only modification made where to adapt the code from a console app to a Cocoa app. The modification are made in the main.cxx.</p>
<h2>Few Tips</h2>
<p>Because the FBX iOS SDK is a C++ API, you need to remember few things to avoid problems:</p>
<ul>
<li>rename your Cocoa files to .mm vs .m, otherwise Xcode will compile the source as C code vs C++ and that will generate many errors.</li>
<li>strings are C++ strings (&#39;char *&#39;) vs NSString, so you need to convert them back and force. You can use these techniques for example:</li>
<ul>
<li>NSString *st =[[NSString alloc] initWithFormat:@&quot;%s&quot;, myString]</li>
<li>NSString *st =[[NSString alloc] initWithCString:myString encoding:NSUTF8StringEncoding]</li>
<li>FbxString st ([myString cStringUsingEncoding:[NSString defaultCStringEncoding]])</li>
</ul>
<li>most of the FBX sample are console applications which aren&#39;t valid app for iOS. For a quick port, you can trap all stdout and redirect them to&#0160;an UITextView control. See code in GenericViewController.mm fct:viewDidLoad line #50 #80, 81, 82<br />
<pre class="brush: c; toolbar: false; first-line: 44; highlight: [50, 80, 81, 82]">- (void)viewDidLoad {
	[super viewDidLoad] ;
	
	NSArray *paths =NSSearchPathForDirectoriesInDomains (NSDocumentDirectory, NSUserDomainMask, YES) ;
	NSString *documentsPath =[paths objectAtIndex:0] ;
	NSString *filename =[NSString stringWithFormat:@&quot;%@/output.txt&quot;, documentsPath] ;
	FILE *fp =freopen ([filename cStringUsingEncoding:[NSString defaultCStringEncoding]], &quot;w&quot;, stdout) ;
	
	switch ( __what ) {
		case 1: // Global Light Settings
			DisplayGlobalLightSettings (&amp;(self._fbxObject-&gt;_scene)-&gt;GetGlobalSettings ());
			break ;
		case 2: // Global Camera Settings
			DisplayGlobalCameraSettings (&amp;(self._fbxObject-&gt;_scene)-&gt;GetGlobalSettings ()) ;
			break ;
		case 3: // Global Time Settings
			DisplayGlobalTimeSettings (&amp;(self._fbxObject-&gt;_scene)-&gt;GetGlobalSettings ()) ;
			break ;
		case 4: // Hierarchy
			DisplayHierarchy (self._fbxObject-&gt;_scene) ;
			break ;
		case 5: // Node Content
			DisplayContent (self._fbxObject-&gt;_scene) ;
			break ;
		case 6: // Pose
			DisplayPose (self._fbxObject-&gt;_scene) ;
			break ;
		case 7: // Animation
			DisplayAnimation (self._fbxObject-&gt;_scene) ;
			break ;
		case 8: // Generic Information
			DisplayGenericInfo (self._fbxObject-&gt;_scene) ;
			break ;
	}
	fclose (fp) ;
	
	std::ifstream txtFile ([filename cStringUsingEncoding:[NSString defaultCStringEncoding]]) ;
	std::string contents ((std::istreambuf_iterator(txtFile)), std::istreambuf_iterator()) ;
	_text.text =[NSString stringWithCString:contents.data () encoding:NSUTF8StringEncoding] ;
}
</pre>
</li>
</ul>
<p>&#0160;</p>
<p>The completed sample is posted on GitHub at this <a href="https://github.com/cyrillef/FBX-iOS-ImportScene" target="_self">location</a>.&#0160;</p>
