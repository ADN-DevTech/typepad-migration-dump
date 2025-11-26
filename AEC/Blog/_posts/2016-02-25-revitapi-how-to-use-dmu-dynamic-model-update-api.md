---
layout: "post"
title: "RevitAPI: How to use DMU (Dynamic Model Update) api?"
date: "2016-02-25 01:31:00"
author: "Aaron Lu"
categories: []
original_url: "https://adndevblog.typepad.com/aec/2016/02/revitapi-how-to-use-dmu-dynamic-model-update-api.html "
typepad_basename: "revitapi-how-to-use-dmu-dynamic-model-update-api"
typepad_status: "Publish"
---

<p><a href="http://blog.csdn.net/lushibi/article/details/50629022">中文链接</a></p>
<p><small>By <a href="http://adndevblog.typepad.com/aec/aaron-lu.html" target="_self">Aaron Lu</a></small></p>
<p><br /> We know Revit's parametric modeling feature allows us to change an&nbsp;element and all related elements will be changed automatically to keep the model consistent, e.g. move a wall, the window or door on the wall will move accordingly.</p>
<p>But sometimes we want to add more association relations between elements, so that we have some customized propagation behavior, e.g. when extend a wall, another wall will become shorter, or when a component in link document is moved, another component in current document will also be moved. In this kind of situation, we can use DMU (Dynamic Model Update).</p>
<p>&nbsp;</p>
<p>What is DMU? to be simple, it is a kind of event, or we can say it is an implementation of&nbsp;Sub-Pub design pattern, i.e. we register a callback function and when some event happens, the callback will be invoked. In DMU, the callback function is encapsulated in an interface named IUpdater, an updater is an instance of class which implements IUpdater.</p>
<h2>Register</h2>
<p>First let's see the method(s) on how to register an updater, the signatures are:</p>
<pre class="csharp prettyprint">public class UpdaterRegistry : IDisposable
{
    public static void RegisterUpdater(IUpdater updater);
    public static void RegisterUpdater(IUpdater updater, bool isOptional);
    public static void RegisterUpdater(IUpdater updater, Document document);
    public static void RegisterUpdater(IUpdater updater, Document document, bool isOptional);
}</pre>
<p>Arguments and their meanings are:</p>
<table border="1">
<tbody>
<tr>
<th>Argument</th>
<th>Meaning</th>
</tr>
<tr>
<td>IUpdater updater</td>
<td>The instance with a callback function which will be invoked when something happens.</td>
</tr>
<tr>
<td>bool isOptional</td>
<td>Whether it is optional or not, true means Revit won't care if the updater is registered or not, false means the updater is very important, when it is missing, Revit will pop up warning dialog.</td>
</tr>
<tr>
<td>Document document</td>
<td>Related docuument, if specify, then the updater will only apply to this document, otherwise, it will apply to the whole Revit application.</td>
</tr>
</tbody>
</table>
<h2>Implement IUpdater</h2>
<p>Looking at the arguments of the above methods, we know that we should first create an instance of IUpdater.</p>
<p>How to implement IUpdater?&nbsp;Below is an code&nbsp;example:</p>
<pre class="csharp prettyprint">public class ParameterUpdater : IUpdater
{
    UpdaterId _uid;
    public ParameterUpdater(Guid guid)
    {
        _uid = new UpdaterId(new AddInId(
            new Guid("c1f5f009-8ba9-4f1d-b0fb-ba41a0f69942")), // addin id
            guid); // updater id
    }

    public void Execute(UpdaterData data)
    {
        Func&lt;ICollection&lt;ElementId&gt;, string&gt; toString = ids =&gt; ids.Aggregate("", (ss, id) =&gt; ss + "," + id).TrimStart(',');
        var sb = new StringBuilder();
        sb.AppendLine("added:" + toString(data.GetAddedElementIds()));
        sb.AppendLine("modified:" + toString(data.GetModifiedElementIds()));
        sb.AppendLine("deleted:" + toString(data.GetDeletedElementIds()));
        TaskDialog.Show("Changes", sb.ToString());
    }

    public string GetAdditionalInformation()
    {
        return "N/A";
    }

    public ChangePriority GetChangePriority()
    {
        return ChangePriority.FreeStandingComponents;
    }

    public UpdaterId GetUpdaterId()
    {
        return _uid;
    }

    public string GetUpdaterName()
    {
        return "ParameterUpdater";
    }
}</pre>
<p>Note that:</p>
<ul>
<li>Creation of UpdaterId: the first argument is a Guid, which is guid of the addon/plugin registering the updater, which should be the same as the ClientId or AddinId defined in .addin file. e.g. below is the addin file of an ExternalCommand, so the first argument should be c1f5f009-8ba9-4f1d-b0fb-ba41a0f69942. The second argument is the guid of the updater itself, we can create one using the built-in guid generation tool of visual studio.
<pre class="html prettyprint">&lt;?xml version="1.0" encoding="utf-8" standalone="no"?&gt;
&lt;RevitAddIns&gt;
  &lt;AddIn Type="Command"&gt;
    &lt;Name&gt;CommandB&lt;/Name&gt;
    &lt;ClientId&gt;c1f5f009-8ba9-4f1d-b0fb-ba41a0f69942&lt;/ClientId&gt;
    &lt;Assembly&gt;D:\ADN\Test\bin\Debug\CommandB.dll&lt;/Assembly&gt;
    &lt;FullClassName&gt;ApplicationB.CommandB&lt;/FullClassName&gt;
    &lt;VendorId&gt;ADSK&lt;/VendorId&gt;
  &lt;/AddIn&gt;
&lt;/RevitAddIns&gt;</pre>
</li>
<li>Execute function is the callback function, it will be invoked when something happens, we can get enough information from its argument UpdaterData, e.g. UpdaterData.GetDocument() returns the related Document object, GetAddedElementIds() returns the ids of added elements etc.. We can do a lot of things inside the callback function, for example, move or create an element, it is totally up to us.</li>
<li>We can't use Transaction inside Execute method, because Execute is already in a transaction.</li>
</ul>
<div>After implement IUpdater, we now can create and register an updater:</div>
<div>
<pre class="csharp prettyprint">ParameterUpdater _updater = new ParameterUpdater(new Guid("{E305C880-2918-4FB0-8062-EE1FA70FABD6}"));
UpdaterRegistry.RegisterUpdater(_updater, true);</pre>
Here, the guid is created via builtin tool of VS</div>
<div>&nbsp;</div>
<h2>Trigger</h2>
<div>Last thing is to tell Revit when what happens, the updater will be triggered, using AddTrigger method, signatures are:</div>
<div>
<pre class="csharp prettyprint">public class UpdaterRegistry : IDisposable
{
    public static void AddTrigger(UpdaterId id, ElementFilter filter, ChangeType change);
    public static void AddTrigger(UpdaterId id, Document document, ElementFilter filter, ChangeType change);
    public static void AddTrigger(UpdaterId id, Document document, ICollection&lt;ElementId&gt; elements, ChangeType change);
}</pre>
Arguments and meanings: <br />
<table border="1">
<tbody>
<tr>
<th>Argument</th>
<th>Meaning</th>
</tr>
<tr>
<td>UpdaterId id</td>
<td>id of updater</td>
</tr>
<tr>
<td>ElementFilter filter</td>
<td>ElementFilter defines a set of elements, the updater will only be triggered when something happens to those elements</td>
</tr>
<tr>
<td>ICollection&lt;ElementId&gt; elements</td>
<td>The updater will only be triggered when something happens to specific elements by designating the ids of them</td>
</tr>
<tr>
<td>Document document</td>
<td>Only apply to a document</td>
</tr>
<tr>
<td>ChangeType change</td>
<td>Specify the trigger condition, e.g. when parameter changed (Element.GetChangeTypeParameter) or when geometry changed (Element.GetChangeTypeGeometry) or any changes (Element.GetChangeTypeAny) etc.</td>
</tr>
</tbody>
</table>
</div>
<p>&nbsp;</p>
<p>If we want to trigger an updater when Area of an element is changed, we can write code like this:</p>
<pre class="csharp prettyprint">var parameter = element.get_Parameter(BuiltInParameter.ROOM_AREA);
UpdaterRegistry.AddTrigger(_updater.GetUpdaterId(), doc, <br />    new List&lt;ElementId&gt;() { new ElementId(197280)}, <br />    Element.GetChangeTypeParameter(parameter));</pre>
<p>This is the whole DMU api usage workflow, when we change Area of element 197280, the Execute method will be invoked. Of course, we can do anything we want, but make sure there is no&nbsp;dead loop :-)</p>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08bbe7ae970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb08bbe7ae970d image-full img-responsive" title="DMU_Area_Area_change" src="/assets/image_896491.jpg" alt="DMU_Area_Area_change" border="0" /></a></p>
<p>Note: we can use UpdaterRegistry.RemoveAllTriggers or UpdaterRegistry.RemoveDocumentTriggers to remove triggers and UpdaterRegistry.UnregisterUpdater to unregister updaters</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
