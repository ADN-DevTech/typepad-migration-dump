---
layout: "post"
title: "Deleting a GraphicStyle Element"
date: "2012-10-17 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Deletion"
original_url: "https://thebuildingcoder.typepad.com/blog/2012/10/deleting-a-graphicstyle-element.html "
typepad_basename: "deleting-a-graphicstyle-element"
typepad_status: "Publish"
---

<p>Here is a short and sweet deletion question handled by Joe Ye:

<p><strong>Question:</strong> I am trying to delete a GraphicStyle object in a RFA family document.

<p>I tried to achieve this using the following code, but it was not successful: 

<pre class="code">
  Dim elem As Element = m_rvtDoc.Element(
    "5ad9f0cf-6eff-4c63-8a44-9f3a87881dd7-00000b22") 

  m_rvtDoc.Delete(elem)
</pre>

<p>How can I delete this object?


<p><strong>Answer:</strong> Some internal line style objects cannot be deleted, because the model requires their presence.
As you can see in the following image, the Delete and Rename buttons are disabled when Hidden Lines are selected:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833017c329080d3970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833017c329080d3970b image-full" alt="Protected line style objects" title="Protected line style objects" src="/assets/image_f07c60.jpg" border="0" /></a><br />

</center>

<p>Some of the other line types, i.e. GraphicStyle objects, can very well be deleted.

<p>I manually created an own line type in the dialogue shown above. 
Since it is my own, the model does not depend on it.
I can then delete the line type I created using the following external command:

<pre class="code">
[<span class="teal">TransactionAttribute</span>( <span class="teal">TransactionMode</span>.Manual )]
<span class="blue">public</span> <span class="blue">class</span> <span class="teal">Command</span> : <span class="teal">IExternalCommand</span>
{
&nbsp; <span class="blue">public</span> <span class="teal">Result</span> Execute( 
&nbsp; &nbsp; <span class="teal">ExternalCommandData</span> commandData, 
&nbsp; &nbsp; <span class="blue">ref</span> <span class="blue">string</span> messages, 
&nbsp; &nbsp; <span class="teal">ElementSet</span> elements )
&nbsp; {
&nbsp; &nbsp; <span class="teal">UIApplication</span> app = commandData.Application;
&nbsp; &nbsp; <span class="teal">Document</span> doc = app.ActiveUIDocument.Document;
&nbsp;
&nbsp; &nbsp; <span class="teal">Transaction</span> trans = <span class="blue">new</span> <span class="teal">Transaction</span>( doc );
&nbsp; &nbsp; trans.Start( <span class="maroon">&quot;Delete Line Style&quot;</span> );
&nbsp;
&nbsp; &nbsp; <span class="green">//ElementId id = new ElementId( 4889 );</span>
&nbsp; &nbsp; <span class="green">//Element elem = doc.get_Element( id );</span>
&nbsp;
&nbsp; &nbsp; <span class="blue">string</span> guid = <span class="maroon">&quot;6387d73b-1e94-456a-8804&quot;</span>
&nbsp; &nbsp; &nbsp; + <span class="maroon">&quot;-aaaf48a905f0-0000131a&quot;</span>;
&nbsp;
&nbsp; &nbsp; <span class="teal">Element</span> elem = doc.get_Element( guid );
&nbsp;
&nbsp; &nbsp; doc.Delete( elem );
&nbsp;
&nbsp; &nbsp; trans.Commit();
&nbsp;
&nbsp; &nbsp; <span class="blue">return</span> <span class="teal">Result</span>.Succeeded;
&nbsp; }
}
</pre>

<p>By the way, deleting elements obviously requires an open transaction, which needs to be committed after the deletion.
