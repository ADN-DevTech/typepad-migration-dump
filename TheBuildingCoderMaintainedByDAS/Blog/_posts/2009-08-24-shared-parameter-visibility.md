---
layout: "post"
title: "Shared Parameter Visibility"
date: "2009-08-24 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Debugging"
  - "Parameters"
  - "Settings"
  - "User Interface"
original_url: "https://thebuildingcoder.typepad.com/blog/2009/08/shared-parameter-visibility.html "
typepad_basename: "shared-parameter-visibility"
typepad_status: "Publish"
---

<p>Here is an interesting question and a workaround for a problem accessing the visibility property on shared parameters, raised by Jon Smith of

<a href="http://www.constructionindustrysolutions.com">
Construction Industry Solutions COINS</a>:

<p><strong>Question:</strong> We need to be able to tell if a parameter is visible or invisible, so we can honour that setting in our own interface and not display it if set to invisible.
We try to do this as follows:

<pre>
ExternalDefinition externalDef 
  = param.Definition as ExternalDefinition;

if( externalDef == null || externalDef.Visible )
{
  // show the parameter
}
</pre>

<p>However, all definitions returned by this method for any parameter whatsoever are InternalDefinition, including for shared parameters that have been marked as invisible.
The InternalDefinition does not have the Visible property exposed.

<p>It also seems, from looking in the Visual Studio watch window, that the base Definition class does contain a visible property  (whose value is correct), but it is internal and not exposed:

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330120a50d1689970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="at-xid-6a00e553e1689788330120a50d1689970b image-full" alt="Parameter visibility" title="Parameter visibility" src="/assets/image_183e79.jpg" border="0"  /></a>

</center>

<p><strong>Answer:</strong> Yes, there is a known issue accessing the definition class of a shared parameter. 
The Parameter.Definition property on a shared parameter erroneously returns an InternalDefinition, even though it actually should be returning an ExternalDefinition.
This issue is being looked into and will be resolved soon.
It may be possible to implement a workaround to get the proper external definition through the sequence Application.OpenSharedParameterFile &gt; DefinitionFile &gt; DefinitionGroup &gt; ExternalDefinition.

<p>Happily, if Visual Studio is able to display the visibility property that you are interested in, then you should also be able to access it from your application source code using .NET 

<a href="http://en.wikipedia.org/wiki/Reflection_%28computer_science%29">
reflection</a>.

<p><strong>Response:</strong> Excellent - thanks for the information on the issue with the Parameter.Definition property, and thanks for pointing me towards using reflection. 
We have created the following function that solves the problem for the current release:

<pre class="code"> 
<span class="blue">public</span> <span class="blue">static</span> <span class="blue">bool</span> isParameterVisible( <span class="teal">Parameter</span> p )
{
&nbsp; <span class="blue">bool</span> bParameterIsVisible = <span class="blue">true</span>;
&nbsp;
&nbsp; <span class="blue">try</span>
&nbsp; {
&nbsp; &nbsp; <span class="teal">BindingFlags</span> flags 
&nbsp; &nbsp; &nbsp; = <span class="teal">BindingFlags</span>.Instance 
&nbsp; &nbsp; &nbsp; | <span class="teal">BindingFlags</span>.FlattenHierarchy 
&nbsp; &nbsp; &nbsp; | <span class="teal">BindingFlags</span>.Public 
&nbsp; &nbsp; &nbsp; | <span class="teal">BindingFlags</span>.NonPublic 
&nbsp; &nbsp; &nbsp; | <span class="teal">BindingFlags</span>.InvokeMethod;
&nbsp;
&nbsp; &nbsp; <span class="teal">Type</span> t = <span class="blue">typeof</span>( <span class="teal">Definition</span> );

&nbsp; &nbsp; <span class="blue">object</span> result = t.InvokeMember( 
&nbsp; &nbsp; &nbsp; <span class="maroon">&quot;get_Visible&quot;</span>, flags, <span class="blue">null</span>, p.Definition, <span class="blue">null</span> );
&nbsp;
&nbsp; &nbsp; <span class="blue">if</span>( <span class="blue">null</span> != result &amp;&amp; result <span class="blue">is</span> <span class="teal">Boolean</span> )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; bParameterIsVisible = (<span class="teal">Boolean</span>) result;
&nbsp; &nbsp; }
&nbsp; }
&nbsp; <span class="blue">catch</span>( System.<span class="teal">Exception</span> )
&nbsp; {
&nbsp; &nbsp; <span class="green">// in case of any problems, assume parameter is visible</span>
&nbsp; }
&nbsp; <span class="blue">return</span> bParameterIsVisible;
}
</pre>

<p>Many thanks to Jon for this cool little workaround!
