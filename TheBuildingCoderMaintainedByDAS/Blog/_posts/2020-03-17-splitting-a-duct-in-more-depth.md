---
layout: "post"
title: "Splitting a Duct in More Depth"
date: "2020-03-17 06:00:00"
author: "Jeremy Tammik"
categories:
  - "Element Creation"
  - "Family"
  - "RME"
  - "User Interface"
original_url: "https://thebuildingcoder.typepad.com/blog/2020/03/splitting-a-duct-in-more-depth.html "
typepad_basename: "splitting-a-duct-in-more-depth"
typepad_status: "Publish"
---

<p>We recently shared a brief note on using the <code>BreakCurve</code> API for <a href="https://thebuildingcoder.typepad.com/blog/2020/03/split-pipe-and-headless-revit.html">splitting a pipe</a>.</p>

<p>In a <a href="https://thebuildingcoder.typepad.com/blog/2020/03/split-pipe-and-headless-revit.html#comment-4835671086">comment</a> on that,
Matt Taylor pointed out a much more comprehensive discussion in
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
asking <a href="https://forums.autodesk.com/t5/revit-api-forum/is-there-any-api-available-to-split-a-duct-programmatically/td-p/6926621">is there any API available to split a duct programmatically?</a>.</p>

<p>That is not just an example, but an entire tutorial, so I think it is very useful to share here as well for all to enjoy:</p>

<p><strong>Question:</strong> I need to split a duct programmatically ? Is there any API available to do so?</p>

<p><strong>Answer:</strong> I explained how this can be achieved using the <code>BreakCurve</code> method in my comments
on <a href="https://forums.autodesk.com/t5/revit-api-forum/duct-splitting/m-p/6784012">duct splitting</a></p>

<p><strong>Response:</strong> I tried using the <code>BreakCurve</code> method for splitting a duct, but it throws an exception at the time of execution.</p>

<p>Can you please guide me on how to pass the third parameter, <code>XYZ</code> <code>ptBreak</code>?</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a4f2e363200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a4f2e363200d image-full img-responsive" alt="BreakCurve issue" title="BreakCurve issue" src="/assets/image_275d7d.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p><strong>Answer:</strong> Well, I think the error says it all! The point isn't on the duct curve.</p>

<p>Here's a simple example that breaks a duct 2 feet along its length.</p>

<pre class="code">
&lt;<span style="color:#2b91af;">Transaction</span>(<span style="color:#2b91af;">TransactionMode</span>.Manual)&gt;
&lt;<span style="color:#2b91af;">Regeneration</span>(<span style="color:#2b91af;">RegenerationOption</span>.Manual)&gt;
&lt;<span style="color:#2b91af;">Journaling</span>(<span style="color:#2b91af;">JournalingMode</span>.UsingCommandData)&gt;
<span style="color:blue;">Public</span>&nbsp;<span style="color:blue;">Class</span>&nbsp;<span style="color:#2b91af;">TransactionCommand</span>
&nbsp;&nbsp;<span style="color:blue;">Implements</span>&nbsp;UI.IExternalCommand
&nbsp;&nbsp;<span style="color:blue;">Public</span>&nbsp;<span style="color:blue;">Function</span>&nbsp;Execute(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">ByVal</span>&nbsp;commandData&nbsp;<span style="color:blue;">As</span>&nbsp;UI.ExternalCommandData,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">ByRef</span>&nbsp;message&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:blue;">String</span>,&nbsp;<span style="color:blue;">ByVal</span>&nbsp;elements&nbsp;<span style="color:blue;">As</span>&nbsp;DB.ElementSet)&nbsp;_
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">As</span>&nbsp;UI.Result&nbsp;<span style="color:blue;">Implements</span>&nbsp;UI.IExternalCommand.Execute

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;app&nbsp;<span style="color:blue;">As</span>&nbsp;ApplicationServices.Application&nbsp;=&nbsp;commandData.Application.Application
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;doc&nbsp;<span style="color:blue;">As</span>&nbsp;DB.Document&nbsp;=&nbsp;commandData.Application.ActiveUIDocument.Document
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;docUi&nbsp;<span style="color:blue;">As</span>&nbsp;UI.UIDocument&nbsp;=&nbsp;commandData.Application.ActiveUIDocument

&nbsp;&nbsp;&nbsp;&nbsp;Execute&nbsp;=&nbsp;UI.Result.Failed
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Using</span>&nbsp;transaction&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:blue;">New</span>&nbsp;DB.Transaction(doc,&nbsp;<span style="color:#a31515;">&quot;Break&nbsp;Duct&quot;</span>)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;transaction.Start()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;duct&nbsp;<span style="color:blue;">As</span>&nbsp;DB.Mechanical.Duct&nbsp;=&nbsp;<span style="color:blue;">TryCast</span>(doc.GetElement(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">New</span>&nbsp;DB.ElementId(1789723)),&nbsp;DB.Mechanical.Duct)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;curve&nbsp;<span style="color:blue;">As</span>&nbsp;DB.Curve&nbsp;=&nbsp;<span style="color:blue;">TryCast</span>(duct.Location,&nbsp;DB.LocationCurve).Curve
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;pt0&nbsp;<span style="color:blue;">As</span>&nbsp;DB.XYZ&nbsp;=&nbsp;curve.GetEndPoint(0)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;pt1&nbsp;<span style="color:blue;">As</span>&nbsp;DB.XYZ&nbsp;=&nbsp;curve.GetEndPoint(1)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;vector&nbsp;<span style="color:blue;">As</span>&nbsp;DB.XYZ&nbsp;=&nbsp;pt1.Subtract(pt0).Normalize
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;breakPt&nbsp;<span style="color:blue;">As</span>&nbsp;DB.XYZ&nbsp;=&nbsp;pt0.Add(vector.Multiply(2.0))
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;newDuctId&nbsp;<span style="color:blue;">As</span>&nbsp;DB.ElementId&nbsp;=&nbsp;DB.Mechanical.MechanicalUtils.BreakCurve(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;doc,&nbsp;duct.Id,&nbsp;breakPt)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;transaction.Commit()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">&#39;&nbsp;change&nbsp;our&nbsp;result&nbsp;to&nbsp;successful</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Execute&nbsp;=&nbsp;UI.Result.Succeeded
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">End</span>&nbsp;<span style="color:blue;">Using</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Return</span>&nbsp;Execute
&nbsp;&nbsp;<span style="color:blue;">End</span>&nbsp;<span style="color:blue;">Function</span>
<span style="color:blue;">End</span>&nbsp;<span style="color:blue;">Class</span>
</pre>

<p><strong>Response:</strong> It worked pleasantly.  Smiley Happy</p>

<p>Normally, if I split a duct in the UI, it adds a family instance, e.g., a fitting, between the two separated elements. </p>

<p><code>BreakCurve</code> just breaks the element into two parts at the specified length, which is really good, but I also need a specific family instance to be added between the two elements, just like in the UI.</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833025d9b3d6462200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833025d9b3d6462200c image-full img-responsive" alt="Splitting duct" title="Splitting duct" 
 src="/assets/image_1eb859.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p><strong>Answer:</strong> Well, you probably need to break the duct in two places.</p>

<p>You have the elementIds of the two ducts after one break; you just need to use one of those in your second break.</p>

<p>The second break would have to be enclosed in a second transaction.</p>

<p>Once you have the third elementId, you can get its duct element and use <code>duct.ChangeTypeId</code> to change the type should you desire it.</p>

<p><strong>Response:</strong> My requirement is different.
This is what the <code>BreakCurve</code> method generates:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a5178494200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a5178494200b image-full img-responsive" alt="After using BreakCurve" title="After using BreakCurve" src="/assets/image_08edc2.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>Can I add a fitting between the two duct elements that will act as a separation point?</p>

<p>Here are screenshots showing the situation before and after splitting an element via the UI and API:</p>

<p>Before:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a51784a7200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a51784a7200b image-full img-responsive" alt="Before" title="Before" src="/assets/image_d19c88.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>After, when splitting manually via UI:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a4f2e38f200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a4f2e38f200d image-full img-responsive" alt="After UI split" title="After UI split" src="/assets/image_df1a3c.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>Here, you can see 3 elements.
Two are child elements created by splitting the parent element.
The third one is the fitting that separates the two.</p>

<p>This is the fitting that was added:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833025d9b3d6486200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833025d9b3d6486200c image-full img-responsive" alt="Fitting" title="Fitting" src="/assets/image_68bd99.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>After, when splitting using the <code>BreakCurve</code> API:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833025d9b3d6498200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833025d9b3d6498200c image-full img-responsive" alt="BreakCurve result" title="BreakCurve result"  src="/assets/image_a6aee1.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>So, here the fitting needs to be added between the two duct elements, just like it was using the manual split command.</p>

<p><strong>Answer:</strong> Riiiiiiiight. You need to add a union fitting. That is a duct fitting, and a family instance.</p>

<p>When in doubt about the wording, you can describe the elements using the Revit descriptions or the RevitLookup (API) names.
That reduces the potential for confusion.</p>

<p>I originally used two transactions, but it appears a regen will do the trick:</p>

<pre class="code">
&lt;<span style="color:#2b91af;">Transaction</span>(<span style="color:#2b91af;">TransactionMode</span>.Manual)&gt;
&lt;<span style="color:#2b91af;">Regeneration</span>(<span style="color:#2b91af;">RegenerationOption</span>.Manual)&gt;
&lt;<span style="color:#2b91af;">Journaling</span>(<span style="color:#2b91af;">JournalingMode</span>.UsingCommandData)&gt;
<span style="color:blue;">Public</span>&nbsp;<span style="color:blue;">Class</span>&nbsp;<span style="color:#2b91af;">TransactionCommand</span>
&nbsp;&nbsp;<span style="color:blue;">Implements</span>&nbsp;UI.IExternalCommand
&nbsp;&nbsp;<span style="color:blue;">Public</span>&nbsp;<span style="color:blue;">Function</span>&nbsp;Execute(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">ByVal</span>&nbsp;commandData&nbsp;<span style="color:blue;">As</span>&nbsp;UI.ExternalCommandData,&nbsp;<span style="color:blue;">ByRef</span>&nbsp;message&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:blue;">String</span>,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">ByVal</span>&nbsp;elements&nbsp;<span style="color:blue;">As</span>&nbsp;DB.ElementSet)&nbsp;_
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">As</span>&nbsp;UI.Result&nbsp;<span style="color:blue;">Implements</span>&nbsp;UI.IExternalCommand.Execute

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;app&nbsp;<span style="color:blue;">As</span>&nbsp;ApplicationServices.Application&nbsp;=&nbsp;commandData.Application.Application
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;doc&nbsp;<span style="color:blue;">As</span>&nbsp;DB.Document&nbsp;=&nbsp;commandData.Application.ActiveUIDocument.Document
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;docUi&nbsp;<span style="color:blue;">As</span>&nbsp;UI.UIDocument&nbsp;=&nbsp;commandData.Application.ActiveUIDocument

&nbsp;&nbsp;&nbsp;&nbsp;Execute&nbsp;=&nbsp;UI.Result.Failed
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;newDuctId&nbsp;<span style="color:blue;">As</span>&nbsp;DB.ElementId&nbsp;=&nbsp;<span style="color:blue;">Nothing</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;ductOrig&nbsp;<span style="color:blue;">As</span>&nbsp;DB.Mechanical.Duct&nbsp;=&nbsp;<span style="color:blue;">Nothing</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;duct2&nbsp;<span style="color:blue;">As</span>&nbsp;DB.Mechanical.Duct&nbsp;=&nbsp;<span style="color:blue;">Nothing</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;breakPt&nbsp;<span style="color:blue;">As</span>&nbsp;DB.XYZ&nbsp;=&nbsp;<span style="color:blue;">Nothing</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Using</span>&nbsp;transaction&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:blue;">New</span>&nbsp;DB.Transaction(doc,&nbsp;<span style="color:#a31515;">&quot;Break&nbsp;Duct&nbsp;+&nbsp;Add&nbsp;Fitting&quot;</span>)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;transaction.Start()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ductOrig&nbsp;=&nbsp;<span style="color:blue;">TryCast</span>(doc.GetElement(<span style="color:blue;">New</span>&nbsp;DB.ElementId(1789660)),&nbsp;DB.Mechanical.Duct)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;curve&nbsp;<span style="color:blue;">As</span>&nbsp;DB.Curve&nbsp;=&nbsp;<span style="color:blue;">TryCast</span>(ductOrig.Location,&nbsp;DB.LocationCurve).Curve
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;pt0&nbsp;<span style="color:blue;">As</span>&nbsp;DB.XYZ&nbsp;=&nbsp;curve.GetEndPoint(0)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;pt1&nbsp;<span style="color:blue;">As</span>&nbsp;DB.XYZ&nbsp;=&nbsp;curve.GetEndPoint(1)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;vector&nbsp;<span style="color:blue;">As</span>&nbsp;DB.XYZ&nbsp;=&nbsp;pt1.Subtract(pt0).Normalize
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;breakPt&nbsp;=&nbsp;pt0.Add(vector.Multiply(2.0))
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;newDuctId&nbsp;=&nbsp;DB.Mechanical.MechanicalUtils.BreakCurve(doc,&nbsp;ductOrig.Id,&nbsp;breakPt)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;duct2&nbsp;=&nbsp;<span style="color:blue;">TryCast</span>(doc.GetElement(newDuctId),&nbsp;DB.Mechanical.Duct)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;doc.Regenerate()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;connectorOrig&nbsp;<span style="color:blue;">As</span>&nbsp;DB.Connector&nbsp;=&nbsp;ductOrig.ConnectorManager.Lookup(0)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;connector1&nbsp;<span style="color:blue;">As</span>&nbsp;DB.Connector&nbsp;=&nbsp;duct2.ConnectorManager.Lookup(1)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;familySymbol&nbsp;<span style="color:blue;">As</span>&nbsp;DB.FamilySymbol&nbsp;=&nbsp;<span style="color:blue;">TryCast</span>(doc.GetElement(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">New</span>&nbsp;DB.ElementId(755396)),&nbsp;DB.FamilySymbol)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;famInstance&nbsp;<span style="color:blue;">As</span>&nbsp;DB.FamilyInstance&nbsp;=&nbsp;doc.Create.NewFamilyInstance(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;breakPt,&nbsp;familySymbol,&nbsp;DB.Structure.StructuralType.NonStructural)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;fitting&nbsp;<span style="color:blue;">As</span>&nbsp;DB.MEPModel&nbsp;=&nbsp;famInstance.MEPModel
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;fittingConnector0&nbsp;<span style="color:blue;">As</span>&nbsp;DB.Connector&nbsp;=&nbsp;fitting.ConnectorManager.Lookup(0)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;fittingConnector1&nbsp;<span style="color:blue;">As</span>&nbsp;DB.Connector&nbsp;=&nbsp;fitting.ConnectorManager.Lookup(1)

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;connectorOrig.ConnectTo(fittingConnector1)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;connector1.ConnectTo(fittingConnector0)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;transaction.Commit()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">&#39;&nbsp;change&nbsp;our&nbsp;result&nbsp;to&nbsp;successful</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Return</span>&nbsp;UI.Result.Succeeded
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">End</span>&nbsp;<span style="color:blue;">Using</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Return</span>&nbsp;Execute
&nbsp;&nbsp;<span style="color:blue;">End</span>&nbsp;<span style="color:blue;">Function</span>
<span style="color:blue;">End</span>&nbsp;<span style="color:blue;">Class</span>
</pre>

<p><strong>Response:</strong> It works great.</p>

<p>Yet, I have another issue. This method only works well if we are not modifying the element's orientation. </p>

<p>I tested this on a duct. I first added a duct and implemented the method just after placing an instance of that duct and it worked great.</p>

<p>But, If I rotate the element to its opposite direction and then implementing that methods throws multiple errors. </p>

<p>Do you have any idea how to resolve these?</p>

<p>Here are the instructions to reproduce the issue:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a517850b200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a517850b200b image-full img-responsive" alt="Added duct" title="Added duct" src="/assets/image_86f938.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a5178528200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a5178528200b image-full img-responsive" alt="Rotated duct" title="Rotated duct"  src="/assets/image_8ed2c5.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a5178546200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a5178546200b image-full img-responsive" alt="Split error" title="Split error"  src="/assets/image_8ccd30.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p><strong>Answer:</strong> The family instance needs rotating in the same orientation as the duct curve.</p>

<p>I'm sure there are plenty of examples of how to rotate family instances on this forum. Report back with how you get on!</p>

<p><strong>Response:</strong> I searched but haven't found much on how to adjust (rotate) the duct fitting as per the element direction and shape.</p>

<p>I tried the rotate method, but that just rotates the new instance (duct fitting) to a 180-degree angle.
So, this only works for a 180-degree rotated element.</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a4f2e446200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a4f2e446200d image-full img-responsive" alt="Split issue" title="Split issue"  src="/assets/image_72993a.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p><strong>Answer:</strong> You can find an example showing how to achieve what you ask by @aksaks in
his <a href="https://github.com/akseidel/WTA_FireP/blob/feb6cff675a2143fffb55e65dd95eb9a73b9c553/WTA_FireP/PlunkOClass.cs">GitHub repository</a></p>

<p><strong>Response:</strong> I checked the link.
It provides lots of methods.</p>

<p>Not sure which one to choose.
I tried implementing a few of them but there are many undefined and unknown classes.</p>

<p>Can you please show me a precise way to get the resolution ?</p>

<p><strong>Answer:</strong> There is in fact no need for the rotation.</p>

<p>You can place the duct fitting with the right direction.</p>

<pre class="code">
  <span style="color:blue;">Dim</span>&nbsp;famInstance&nbsp;<span style="color:blue;">As</span>&nbsp;DB.FamilyInstance&nbsp;_
  =&nbsp;doc.Create.NewFamilyInstance(
  &nbsp;&nbsp;breakPt,&nbsp;FamilySymbol,&nbsp;vector,&nbsp;null,
  &nbsp;&nbsp;DB.Structure.StructuralType.NonStructural)
</pre>

<p>Of course, it can't hurt to learn how to rotate an element.</p>

<p><strong>Response:</strong> Thank you so much.
It worked pleasantly</p>

<p>Very many thanks to Matt and Fair59 for their kind support and great patience helping out in such detail and depth!</p>
