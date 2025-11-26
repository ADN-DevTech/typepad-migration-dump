---
layout: "post"
title: "Access Central File TransmissionData on Revit Server"
date: "2011-11-21 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2012"
  - "Data Access"
  - "Server"
original_url: "https://thebuildingcoder.typepad.com/blog/2011/11/access-transmissiondata-of-central-file-on-revit-server.html "
typepad_basename: "access-transmissiondata-of-central-file-on-revit-server"
typepad_status: "Publish"
---

<p>We already discussed using the TransmissionData class to 

<a href="http://thebuildingcoder.typepad.com/blog/2011/05/list-linked-files-and-transmissiondata.html">
list linked files</a> and 

<a href="http://thebuildingcoder.typepad.com/blog/2011/10/using-the-writetransmissiondata-method.html">
make changes using the WriteTransmissionData method</a>.

<p>Here is another question on this topic answered by my colleague Joe Ye:

<p><strong>Question:</strong> I want to access the TransmissionData object in a central file stored in the Revit Server. How can I achieve that via the Revit API, please?

<p>I tried the suggestions listed in the developer guide and none of them worked for me.

<p>I also tried using IsValidUserVisibleFullServerPath on various permutations of the server name, path and model name as reported in my journal file, but it returned false on all my attempts.

<p><strong>Answer:</strong> To read the TransmissionData object, you need to call the static method TransmissionData.ReadTransmissionData. 
It requires a ModelPath object. 
We will show you how to construct the ModelPath object that refers to the central file.

<p>There are two ways to construct a ModelPath object.

<h4>Way #1</h4>

<ul>
<li>Compose the user-visible path string of the central file, e.g. using the string returned by ModelPathUtils.GetRevitServerPrefix plus the relative path.
Note: The folder separator used in the relative path is a forward slash '/'. 
The Revit 2012 API help documentation RevitAPI.chm mistakenly uses a backslash to separate folders. 
The correct separator is a forward slash.
<li>Create a ModelPath object via the ModelPathUtils.ConvertUserVisiblePathToModelPath method. 
Pass in the string composed in the previous step.
<li>Read the transmission data via the TransmissionData.ReadTransmissionData method. 
Pass in the ModelPath obtained in the previous step.
</ul>

<p>Let's look at accessing the following central file on a Revit Server:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330153935ae78a970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330153935ae78a970b image-full" alt="Revit Server central file" title="Revit Server central file" src="/assets/image_e7504b.jpg" border="0" /></a><br />

</center>

<p>Here is the code showing the implementation of way #1:

<pre class="code">
[<span class="teal">TransactionAttribute</span>( <span class="teal">TransactionMode</span>.Manual )]
<span class="blue">public</span> <span class="blue">class</span> <span class="teal">RevitCommand</span> : <span class="teal">IExternalCommand</span>
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
&nbsp; &nbsp; trans.Start( <span class="maroon">&quot;ExComm&quot;</span> );
&nbsp;
&nbsp; &nbsp; <span class="blue">string</span> visiblePath = <span class="teal">ModelPathUtils</span>
&nbsp; &nbsp; &nbsp; .GetRevitServerPrefix() + <span class="maroon">&quot;/testmodel.rvt&quot;</span>;
&nbsp;
&nbsp; &nbsp; <span class="teal">ModelPath</span> serverPath = <span class="teal">ModelPathUtils</span>
&nbsp; &nbsp; &nbsp; .ConvertUserVisiblePathToModelPath( 
&nbsp; &nbsp; &nbsp; &nbsp; visiblePath );
&nbsp;
&nbsp; &nbsp; <span class="teal">TransmissionData</span> transData = <span class="teal">TransmissionData</span>
&nbsp; &nbsp; &nbsp; .ReadTransmissionData( serverPath );
&nbsp;
&nbsp; &nbsp; <span class="blue">if</span>( transData != <span class="blue">null</span> )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="green">// Access the data in the transData here.</span>
&nbsp; &nbsp; }
&nbsp; &nbsp; <span class="blue">else</span>
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="teal">TaskDialog</span>.Show( <span class="maroon">&quot;Transmission Data&quot;</span>,
&nbsp; &nbsp; &nbsp; &nbsp; <span class="maroon">&quot;The document does not have any transmission data&quot;</span> );
&nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; trans.Commit();
&nbsp;
&nbsp; &nbsp; <span class="blue">return</span> <span class="teal">Result</span>.Succeeded;
&nbsp; }
}
</pre>

<h4>Way #2</h4>

<p>Use this if your program knows the local server name. 
This is mostly not recommended, since the server name may be changed manually from the user interface by the Revit user.

<ul>
<li>Create a ServerPath object using ServerPath constructor taking the server name IP and the relative path without the initial forward slash.
<p>Please note that the first parameter is the server name, not the string returned by the ModelPathUtils.GetRevitServerPrefix.
<p>The second parameter does not include the initial forward slash. See the following sample code. 
The folder separator is a forward slash '/' here as well.

<li>Read the TransmissionData object via the TransmissionData.ReadTransmissionData method, passing in the ServerPath obtained in the previous step.
</ul>

<p>Here is the code showing the implementation of way#2.

<pre class="code">
[<span class="teal">TransactionAttribute</span>( <span class="teal">TransactionMode</span>.Manual )]
<span class="blue">public</span> <span class="blue">class</span> <span class="teal">RevitCommand</span> : <span class="teal">IExternalCommand</span>
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
&nbsp; &nbsp; trans.Start( <span class="maroon">&quot;ExComm&quot;</span> );
&nbsp;
&nbsp; &nbsp; <span class="teal">ServerPath</span> serverPath = <span class="blue">new</span> <span class="teal">ServerPath</span>(
&nbsp; &nbsp; &nbsp; <span class="maroon">&quot;SHACNG035WQRP&quot;</span>, <span class="maroon">&quot;testmodel.rvt&quot;</span> );
&nbsp;
&nbsp; &nbsp; <span class="teal">TransmissionData</span> transData = <span class="teal">TransmissionData</span>
&nbsp; &nbsp; &nbsp; .ReadTransmissionData( serverPath );
&nbsp;
&nbsp; &nbsp; <span class="blue">if</span>( transData != <span class="blue">null</span> )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="green">// Access the data in the transData here.</span>
&nbsp; &nbsp; }
&nbsp; &nbsp; <span class="blue">else</span>
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="teal">TaskDialog</span>.Show( <span class="maroon">&quot;Transmission Data&quot;</span>,
&nbsp; &nbsp; &nbsp; &nbsp; <span class="maroon">&quot;The document does not have any transmission data&quot;</span> );
&nbsp; &nbsp; }
&nbsp; &nbsp; trans.Commit();
&nbsp;
&nbsp; &nbsp; <span class="blue">return</span> <span class="teal">Result</span>.Succeeded;
&nbsp; }
}
</pre>
