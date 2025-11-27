---
layout: "post"
title: "Inventor: Using reference keys to bind back to live topology objects"
date: "2013-01-03 10:57:05"
author: "Vladimir Ananyev"
categories:
  - "Inventor"
  - "Vladimir Ananyev"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/01/inventor-using-reference-keys-to-bind-back-to-live-topology-objects.html "
typepad_basename: "inventor-using-reference-keys-to-bind-back-to-live-topology-objects"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/manufacturing/vladimir-ananyev.html">Vladimir Ananyev</a></p>
<p><strong>Issue</strong></p>
<p>How to get reference keys from topology objects and&#0160; later bind back to them?</p>
<p><a name="section2"></a><strong>Solution</strong></p>
<p>General Information:</p>
<p>Inventor requires internal associativity to be able to update models correctly when parametric or geometry changes are made to a model. For example, if you create a work plane parallel to an existing face of the model, the work plane needs to remain associative to that face. This means when the model is changed in a way that causes the face to change, the work plane needs to recognize this change and update itself. </p>
<p>There are two things that are required for this to take place. First, the work plane needs to recognize that the geometry it is dependent on has changed and second it needs to be able to find the new geometry so it can update correctly. This same functionality is also required by a referencing application to be associative to the Inventor model.</p>
<p>Inventor and Apprentice have some methods to allow for the notification of changes. For example, the document object supports the RevisionId property, which returns a GUID. This value is incremented every time the file is saved and represents the last saved revision of the file. The referencing application can save this value in its file and use it the next time it opens the same Inventor file. When it opens the Inventor file it can get the current RevisionId value and compare it with the saved value. If these are different then the file has been saved since it was last referenced by the application and it can be assumed the model was modified.</p>
<p>The second step of the associativity process is the most difficult. Inventor solves this problem through the use of &quot;reference keys&quot;. A reference key is a persistent handle to an object that can be saved by the reference application and used between sessions. The reference key itself is a set of bytes that the reference application can save into its file. At a later time the reference application can provide Inventor the reference key and it will then return the live object pointed to by this key, even if the object has been modified since the key was originally obtained.</p>
<p>An important point to emphasize is that this additional information; the counter and the reference keys are not saved in the Inventor file. They are saved in the referencing application’s file. An important design requirement for the reference key mechanism was that the reference application should not have to save information in the Inventor file. There may be many cases when the person using the Inventor file in the reference application will not have permission to save the file. Because the reference application maintains all of the data they can continue to receive updated versions of the model through new files and the associativity mechanism will still work.</p>
<p>Below is the workflow used when saving and opening the referencing application file:</p>
<p><strong>Save</strong></p>
<p>1.&#0160; Create the reference key for the object. (The key is returned as an array of bytes.)</p>
<p>2.&#0160; Save the key information into file.</p>
<p><strong>Open</strong></p>
<p>1.&#0160; Read the key information from the file.</p>
<p>2.&#0160; Bind back to the original topology by passing the key information to either Apprentice or Inventor. It then passes back the &quot;live&quot; object.</p>
<p>The reference keys just described are a type of reference key called &quot;flat&quot; keys. A reference key contains the information required for Inventor to resolve the lookup of the object. A flat key contains the full description necessary to resolve the object and it can be quite large. In cases where an application will need to save many reference keys an enhancement to this mechanism has been made so the overall size of the key information saved is reduced. </p>
<p>These small keys are what Inventor uses internally. To support these small keys, Inventor maintains an internal table containing information that allows it to optimize the amount of information stored in the key. One big difference between Inventor using these small keys and a reference application using them is that the table is created and maintained by Inventor and as discussed earlier, the reference application may not have write access to the Inventor file.</p>
<p>The design of the reference key mechanism was done so that it does not require write access to the Inventor file. A mechanism has been created that allows you to use the short keys without needing to write to the Inventor file. In this case, instead of storing the table in the Inventor file, the referencing application stores the table for Inventor in its file. This table is referred to in the API as the Key Context.</p>
<p>&#0160;</p>
<p><strong>General Workflow:</strong></p>
<p>Here is the general workflow for saving and binding back to &quot;live objects&quot; using reference keys.</p>
<p><strong>Save</strong></p>
<p>1.&#0160; Create key context. (The key context is returned as 32-bit integer that acts as a handle to the key context.)</p>
<p>2.&#0160; Create the key for the face specifying the context to create it with. (The key is returned as an array of bytes.)</p>
<p>3.&#0160; Create byte arrays to store the key and the key context.</p>
<p>4.&#0160; Save the key information and the key context information in the byte arrays, these arrays can then be stored by the client referencing applications for binding back later. </p>
<p><strong>Bind Back:</strong></p>
<p>1.&#0160; Get the byte arrays that store the key context and the key.</p>
<p>2.&#0160; Provide Apprentice/Inventor with these byte arrays.</p>
<p>3.&#0160; Bind back to the original topology by passing the key information and key context handle. It then passes back the &quot;live&quot; object.</p>
<p>Please see code samples attached to this post. In order to execute the samples, first, open a part document and select some face object. Then run the sample which displays a dialog box. </p>
<p>Press the &quot;Access Key&quot; button, this gets the reference key from the selected face and saves it along with the key context in global arrays.</p>
<p>In C++ program reference keys are saved along with the key context in global CSafeArrayLong variables (CSafeArrayLong is a template class found in the file SafeArrayUtil.h. For Windows 7 default location is “C:\Users\Public\Documents\Autodesk\Inventor 2013\SDK\Developer Tools\Include\”. This file defines template classes that provide wrapper methods to easily make use of SAFEARRAYs).</p>
<p>After obtaining the reference key for the selected face, the &quot;Bind key&quot; button launches a new Inventor session, loads the part document and then with the help of the saved reference key and key context binds back to the live face and selects it. </p>
<p><strong>Additional Information:</strong></p>
<p>The reference keys are obtained from a particular document (e.g. part or assembly), i.e. they apply to the context of a particular document. For example, if you have a part document, you can get the reference key for any BRep object that exists in this part document, save it along with the key context and later bind back using the reference key manager of the same part document to get back the live object in the part document. You will not be able to use the saved reference key and key context (from the part document) to bind back to a live object in an assembly which contains an occurrence of the part document. </p>
<p>If you want to bind back to any BRep object (proxy object) in the assembly, you would have to get the reference key from any BRep object in the assembly definition, get the reference key manager from the assembly document, use this key manager to get the key context. You could then save this key and the key context and when binding back use the assembly document&#39;s key manager to do so. </p>
<p>The reference key manager obtained from the assembly document will handle the proxy topology objects, also, if you have assembly features that modify an occurrence, the assembly now owns a copy of the surface body corresponding to the overridden occurrence (i.e. assembly owns a surface body proxy that might contain some additional face proxy objects as a result of assembly features). The reference key manager (of the assembly) will also handle these additional face proxy objects.</p>
<p>If you have assembly features that override an occurrence, there isn&#39;t currently a way to map from the overridden topology to the topology on the raw leaf body. The native object of the proxy corresponding to an overridden topology object is not defined. So, you will not be able to ask for an overridden proxy topology object for its native object and bind back to this native object in the context of the part document (assuming it was originally obtained from a part). This will work fine; if assembly features have not been created that have modified the occurrence. </p>
<p>Therefore, in essence, you will be able to use the assembly&#39;s reference key manager to obtain reference keys and bind back to live proxy topology objects no matter if assembly features have been created or not. </p>
<p>In addition, if no assembly features have been created, you will be able to ask a proxy for its native object, get the reference key for this native object (using the reference key manager of the part) and also bind back to the native object in the part. But, if assembly features have been created, then getting the native object and from it the reference key and then binding back will not work.</p>
<p><a href="http://adndevblog.typepad.com/files/csharp.zip">Download CSharp</a></p>
<p><span class="asset  asset-generic at-xid-6a0167607c2431970b017d3f74beb8970c">
<span class="asset  asset-generic at-xid-6a0167607c2431970b017c3545cf14970b"><a href="http://adndevblog.typepad.com/files/c.zip">Download C++</a></span></span></p>
<p><span class="asset  asset-generic at-xid-6a0167607c2431970b017d3f74beb8970c"><span class="asset  asset-generic at-xid-6a0167607c2431970b017c3545cf14970b">
<span class="asset  asset-generic at-xid-6a0167607c2431970b017c3545cf93970b"><a href="http://adndevblog.typepad.com/files/vbnet.zip">Download VBNET</a></span><br /></span></span></p>
