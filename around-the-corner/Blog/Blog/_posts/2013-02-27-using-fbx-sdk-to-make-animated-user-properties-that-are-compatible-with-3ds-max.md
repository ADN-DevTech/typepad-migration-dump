---
layout: "post"
title: "Using FBX SDK to make animated user properties that are compatible with 3ds Max"
date: "2013-02-27 11:59:01"
author: "Cyrille Fauvel"
categories:
  - "3ds Max"
  - "C++"
  - "Cyrille Fauvel"
  - "FBX"
  - "MotionBuilder"
original_url: "https://around-the-corner.typepad.com/adn/2013/02/using-fbx-sdk-to-make-animated-user-properties-that-are-compatible-with-3ds-max.html "
typepad_basename: "using-fbx-sdk-to-make-animated-user-properties-that-are-compatible-with-3ds-max"
typepad_status: "Publish"
---

<p>A huge thanks to&#0160;Kevin Vandecar, who supports both FBX and 3ds Max in the ADN M&amp;E team working with me, and provide the original article for this post.</p>
<p>This is a topic coming from one of our FBX SDK customers recently. Basically the animated properties were coming into MotionBuilder correctly, and after being exported from MotionBuilder would also come into 3ds Max properly. However, when coming directly into 3ds Max, the properties were not visible.</p>
<p>First, note that there is an issue identified in the 3ds Max FBX importer that can cause problems when the properties are named with uppercase letters. This is because MAXScript is not case sensitive, and so it cannot resolve those properly during the import process. Normally you should use lower case names to ensure compatibility with 3ds Max.</p>
<p>Next, in order for the animation to be brought in completely, you typically need to have a single animation stack (or “take”) that contains the animation curves. In order for the curve and the animation itself to be reflected in 3ds Max, you must also set the minimum and maximum values. Here is some simple sample code showing an animated double property.
</p>
<pre class="brush: cpp; toolbar: false;">// ...

// First create an FbxMarker to attach the user property to...
FbxNode* nodeMarker= ADN_FBXCreateMarker(scene, &quot;adn_marker&quot;, 10.f, root, FbxMarker::eSphere, FbxColor(255,255,0));

// Setup the animation stack...
FbxAnimStack* animStack = FbxAnimStack::Create(scene, &quot;take 001&quot;);

// Then create and attach the animated property...
AddAnimatedUserProperty(nodeMarker, animStack);
    
// ...

// where, AddAnimatedUserProperty is defined as:
void AddAnimatedUserProperty(FbxNode* node, FbxAnimStack* animStack)
{
    // create a new animation layer for this sequence.
    FbxAnimLayer* animBaseLayer	= FbxAnimLayer::Create(node-&gt;GetScene(), &quot;Anim Cut Layer&quot;);
	animStack-&gt;AddMember(animBaseLayer);

    // Create the property attached to the node.
    FbxProperty	prop = FbxProperty::Create(node, FbxDoubleDT, &quot;adn_double&quot;, &quot;ADN Double Property&quot;);

    // Setup the property
	if(prop.IsValid())
	{
        // Set intial value
		prop.Set(0.0);
        // Set approriate flags (it&#39;s a suer property and also will be animatable.
		prop.ModifyFlag(FbxPropertyAttr::eUser, true);
		prop.ModifyFlag(FbxPropertyAttr::eAnimatable, true);
        // Create the curve
		prop.CreateCurveNode(animBaseLayer);
        // Set the min and maximum values (needed for 3ds Max.)
        prop.SetMinLimit(-2.0);
        prop.SetMaxLimit(2.0);

        // Go back and obtain the curve... This could have been done above from create, but to show how to get it after it exists.
		FbxAnimCurve* fcurve = prop.GetCurve(animBaseLayer, &quot;adn_double&quot;, true);
		if (fcurve)
		{
            // setup the animated values...
			FbxTime time;
			int lKeyIndex = 0;

            // Start the key sequence
			fcurve-&gt;KeyModifyBegin();
			float f = 1.0;
            // Set the animated values
			for(int frame=0; frame &lt; 100; frame++)
			{
				time.SetFrame(frame);
				lKeyIndex = fcurve-&gt;KeyAdd(time);
				if (frame % 2)
					f+=.01f;
				else
					f-=.02f;
				fcurve-&gt;KeySet(lKeyIndex, time, f, FbxAnimCurveDef::eInterpolationCubic);
			}
            // end the key sequence
			fcurve-&gt;KeyModifyEnd();
		}
	}
}

</pre>
<p>As is usual, this is sample code so make sure to put in proper error checking, and test as appropriate in the context of your application.</p>
