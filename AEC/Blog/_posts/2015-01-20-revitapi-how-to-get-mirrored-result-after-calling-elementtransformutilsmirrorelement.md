---
layout: "post"
title: "RevitAPI: How to get mirrored result after calling ElementTransformUtils.MirrorElement"
date: "2015-01-20 22:16:23"
author: "Aaron Lu"
categories:
  - "Aaron Lu"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2015/01/revitapi-how-to-get-mirrored-result-after-calling-elementtransformutilsmirrorelement.html "
typepad_basename: "revitapi-how-to-get-mirrored-result-after-calling-elementtransformutilsmirrorelement"
typepad_status: "Publish"
---

<p><small>By <a href="http://adndevblog.typepad.com/aec/aaron-lu.html" target="_self">Aaron Lu</a></small></p>
<p><a href="http://blog.csdn.net/lushibi/article/details/42966689">中文链接</a></p>
<script src="https://adndevblog.typepad.com/files/run_prettify-3.js" type="text/javascript"></script>
<p>In current Revit (2015 and before), there are 2 methods to mirror elements, they are:</p>
<p>ElementTransformUtils.MirrorElement and</p>
<p>ElementTransformUtils.MirrorElements</p>
<p>But the problem is they don&#39;t have return value, meaning we can get the mirrored element(s) directly.</p>
<p>However, there is a workaround to accomplish that using Application.DocumentChanged event.</p>
<p>Workflow:</p>
<ol>
<li>Subscribe Application.DocumentChanged event</li>
<li>Mirror element(s)</li>
<li>event raises, in the event handler, get the added new element(s)</li>
</ol>
<p>Example Code, <strong>please note the DocumentChanged event can only be raised after the transaction is committed.</strong></p>
<pre class="csharp prettyprint">public static ICollection&lt;ElementId&gt; MirrorElement(
    Document doc, ElementId elementId, Plane plane)
{
    if (doc == null || plane == null ||
        elementId == ElementId.InvalidElementId ||
        !ElementTransformUtils.CanMirrorElement(doc, elementId))
        throw new ArgumentException(&quot;Argument invalid&quot;);

    ICollection&lt;ElementId&gt; result = new List&lt;ElementId&gt;();
    // create DocumentChanged event handler
    var documentChangedHandler =
        new EventHandler&lt;DocumentChangedEventArgs&gt;(
            (sender, args) =&gt; result = args.GetAddedElementIds());

    // subscribe the event
    doc.Application.DocumentChanged += documentChangedHandler;
    using (Transaction transaction = new Transaction(doc))
    {
        try
        {
            transaction.Start(&quot;Mirror&quot;);
            ElementTransformUtils.MirrorElement(
            doc, elementId, plane);
            transaction.Commit();
        }
        catch (Exception ex)
        {
            TaskDialog.Show(&quot;ERROR&quot;, ex.ToString());
            transaction.RollBack();
        }

        finally
        {
            // unsubscribe the event
            doc.Application.DocumentChanged -= documentChangedHandler;
        }
    }
    return result;
}</pre>
<p><br /> How to use this function</p>
<p>(Following code is to mirror a FamilyInstance near itself, and then display the id(s) of the mirrored elements)</p>
<pre class="csharp prettyprint">var instance = RevitDoc.GetElement(elementId) as FamilyInstance;
if (instance != null)
{
    var transform = instance.GetTransform();
    var mirrored = MirrorElement(RevitDoc, instance.Id, 
        new Plane(transform.BasisX, transform.Origin));
    TaskDialog.Show(&quot;Info&quot;, &quot;Mirror succeeded! New mirrored ids: &quot; 
        + mirrored.Aggregate(&quot;&quot;, (ss, id) =&gt; ss + id + &quot; &quot;));
}</pre>
