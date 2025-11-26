---
layout: "post"
title: "Get and Set Family Category and Parameters"
date: "2009-07-04 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2010"
  - "Family"
  - "Parameters"
original_url: "https://thebuildingcoder.typepad.com/blog/2009/07/get-and-set-family-category-and-parameters.html "
typepad_basename: "get-and-set-family-category-and-parameters"
typepad_status: "Publish"
---

<p>I am receiving a lot of questions about parameters in family documents, so I will be posting several solutions in this area in the next few days.
The first one is short and simple and suited for a rapid Saturday post.
There is so much news coming that I do want to cram it in right away.
This is a question from Jose Fandos of 

<a href="http://www.andekan.com">
Andekan LLC</a>:</p>

<p><strong>Question:</strong>
How can we get and set the family category and the family parameters seen in the "Family Category and Parameters" Revit dialogue box?
This dialogue can be displayed by selecting Create > Category and Parameters in the family editor:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833011570b92ae2970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="at-xid-6a00e553e168978833011570b92ae2970c" alt="Family Category and Parameters dialogue" title="Family Category and Parameters dialogue" src="/assets/image_841c7d.jpg" border="0"  /></a>

</center>

<p>The parameters include the OmniClass number, for instance.
We are not looking for the type or symbol parameters, and we need this access from the family editor, not from the project editor.</p>

<p><strong>Answer:</strong>
I used the RvtMgdDbg tool to explore a family file to answer your query. 
Using that, I can step into RvtMgdDbg > Snoop Application... > Application > Active Document > Owner Family > Parameters to see the family parameters, including the OmniClass number with the built-in parameter enumeration value OMNICLASS_CODE:</p>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833011571ae3e2e970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="at-xid-6a00e553e168978833011571ae3e2e970b image-full" alt="Snoop OmniClass number" title="Snoop OmniClass number" src="/assets/image_26b2a1.jpg" border="0"  /></a>

<p>The family category is also available as a property on this owner family object, but it is read-only.</p>

<p>Here is the external command Execute method that I implemented to demonstrate read access to both of these properties and write access to the OmniClass number:</p>

<pre class="code">
<span class="teal">BuiltInParameter</span> _bip = <span class="teal">BuiltInParameter</span>.OMNICLASS_CODE;
&nbsp;
<span class="blue">public</span> <span class="teal">IExternalCommand</span>.<span class="teal">Result</span> Execute( 
&nbsp; <span class="teal">ExternalCommandData</span> commandData, 
&nbsp; <span class="blue">ref</span> <span class="blue">string</span> message, 
&nbsp; <span class="teal">ElementSet</span> elements )
{
&nbsp; <span class="teal">Application</span> app = commandData.Application;
&nbsp; <span class="teal">Document</span> doc = app.ActiveDocument;
&nbsp;
&nbsp; <span class="blue">if</span>( !doc.IsFamilyDocument )
&nbsp; {
&nbsp; &nbsp; message 
&nbsp; &nbsp; &nbsp; = <span class="maroon">&quot;Please run this command in a family document.&quot;</span>;
&nbsp; }
&nbsp; <span class="blue">else</span>
&nbsp; {
&nbsp; &nbsp; <span class="teal">Family</span> f = doc.OwnerFamily;
&nbsp; &nbsp; <span class="teal">Category</span> c = f.FamilyCategory;
&nbsp; &nbsp; <span class="teal">Parameter</span> p = f.get_Parameter( _bip );
&nbsp;
&nbsp; &nbsp; <span class="teal">Debug</span>.Print( 
&nbsp; &nbsp; &nbsp; <span class="maroon">&quot;Category '{0}', OmniClassNumber {1}&quot;</span>, 
&nbsp; &nbsp; &nbsp; c.Name, p.AsString() );
&nbsp;
&nbsp; &nbsp; p.Set( <span class="maroon">&quot;Jeremy&quot;</span> );
&nbsp;
&nbsp; &nbsp; <span class="teal">Debug</span>.Print( <span class="maroon">&quot;Modified OmniClassNumber {0}&quot;</span>, 
&nbsp; &nbsp; &nbsp; f.get_Parameter( _bip ).AsString() );
&nbsp; }
&nbsp; <span class="blue">return</span> <span class="teal">IExternalCommand</span>.<span class="teal">Result</span>.Failed;
}
</pre>

<p>Note that I am returning Failed from the Execute method, so the transaction that Revit is managing for my command will be aborted, so the changes I made will not actually be committed.</p>

<p>Next week I plan to discuss work-arounds for the challenges surrounding shared parameters in family files, so stay tuned.
Happy weekend to all!</p>
