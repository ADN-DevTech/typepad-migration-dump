---
layout: "post"
title: "RevitAPI: how to change Radius of ConnectorElement? - how to get associated family parameter from element parameter?"
date: "2016-02-24 19:05:00"
author: "Aaron Lu"
categories:
  - "Aaron Lu"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2016/02/revitapi-how-to-change-radius-of-connectorelement-how-to-get-associated-family-parameter-from-elemen.html "
typepad_basename: "revitapi-how-to-change-radius-of-connectorelement-how-to-get-associated-family-parameter-from-elemen"
typepad_status: "Publish"
---

<p><a href="http://blog.csdn.net/lushibi/article/details/50696182">中文链接</a></p>
<p><small>By <a href="http://adndevblog.typepad.com/aec/aaron-lu.html" target="_self">Aaron Lu</a></small></p>
<p>Connector Element of some fitting family has a parameter named "Radius", to change its value, we may write code like this:</p>
<pre class="csharp prettyprint">connectorElement.Radius = 0.041;</pre>
<p>But Radius property is read-only, so it can not be compiled or even it can be, exception will be thrown when running it.</p>
<p>Another way is using Parameter:</p>
<pre class="csharp prettyprint">var radiusPara = connectorElement.get_Parameter(BuiltInParameter.CONNECTOR_RADIUS);
radiusPara.Set(0.041);</pre>
<p>But radiusPara here is also read-only (radiusPara.IsReadOnly == true), exception will be thrown too.</p>
<p>As we know that radius is not changeable in UI, rather, we need to change the associated family parameter, e.g. in this case, "Normal Radius"</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1a1a59c970c-pi" style="display: inline;"><img class="asset  asset-image at-xid-6a0167607c2431970b01b8d1a1a59c970c image-full img-responsive" title="SetRadiusOfConnectorElement-en" src="/assets/image_377509.jpg" alt="SetRadiusOfConnectorElement-en" border="0" /></a></p>
<p>Then the question is how to get the associated family parameter?</p>
<p>Following code shows the answer:</p>
<pre class="csharp prettyprint">var sb = new StringBuilder();
foreach (var connectorPara in connectorElement.GetOrderedParameters())
{
    foreach (FamilyParameter familyPara in doc.FamilyManager.Parameters)
    {
        foreach (Parameter associatedPara in familyPara.AssociatedParameters)
        {
            if (connectorPara.Id == associatedPara.Id &amp;&amp; associatedPara.Element.Id == connectorElement.Id)
            {
                //associate parameter found
                sb.AppendLine("'" + associatedPara.Definition.Name + 
                    "(" + (BuiltInParameter)associatedPara.Id.IntegerValue + 
                    ")' &lt;-&gt; '" + familyPara.Definition.Name + "'");
            }
        }
    }
}
TaskDialog td = new TaskDialog("Parameter associations");
td.MainContent = sb.ToString();
td.TitleAutoPrefix = false;
td.Show();</pre>
<p>It will display all parameter associations between ConnectorElement parameters and family parameters, let's see what it does:</p>
<ol>
<li>Use GetOrderedParameters() to get all parameters of connectorElement, iterate each parameter,</li>
<li>Use Document.FamilyManager.Parameters to get all FamilyParameters, iterate each family parameter,</li>
<li>Use FamilyParameter.AssociatedParameters to get all assocated parameters of elements in the document,</li>
<li>Iterate all associated parameters, check whether the parameter of connector element is the same as the associated parameter, and they belong to the same element.</li>
</ol>
<p>Below is the result:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08bc4e0d970d-pi" style="display: inline;"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb08bc4e0d970d img-responsive" title="SetRadiusOfConnectorElement-relations-en" src="/assets/image_7086.jpg" alt="SetRadiusOfConnectorElement-relations-en" border="0" /></a></p>
<p>For now, it is clear on how to change radius:</p>
<ol>
<li>Find the associated family parameter</li>
<li>Set the family parameter using Document.FamilyManager.Set(familyParameter, value)</li>
</ol>
<p>complete code is shown as below:</p>
<pre class="csharp prettyprint">var doc = commandData.Application.ActiveUIDocument.Document;
var uiSel = commandData.Application.ActiveUIDocument.Selection;
ConnectorElement connectorElement = null;
try
{
    var reference = uiSel.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element, "Pick a connector");
    connectorElement = doc.GetElement(reference) as ConnectorElement;

    if (connectorElement != null)
    {
        var radiusPara = connectorElement.get_Parameter(BuiltInParameter.CONNECTOR_RADIUS);
        foreach (FamilyParameter familyPara in doc.FamilyManager.Parameters)
        {
            foreach (Parameter associatedPara in familyPara.AssociatedParameters)
            {
                if (radiusPara.Id == associatedPara.Id &amp;&amp; associatedPara.Element.Id == connectorElement.Id)
                {
                    //associate parameter found
                    using (Transaction transaction = new Transaction(doc))
                    {
                        transaction.Start("Set Radius");
                        doc.FamilyManager.Set(familyPara, 0.041);
                        transaction.Commit();
                    }
                }
            }
        }
    }
}
catch (Autodesk.Revit.Exceptions.OperationCanceledException)
{
}</pre>
<p>&nbsp;</p>
