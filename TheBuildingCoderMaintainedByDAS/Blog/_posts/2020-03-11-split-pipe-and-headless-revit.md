---
layout: "post"
title: "Split Pipe"
date: "2020-03-11 06:00:00"
author: "Jeremy Tammik"
categories:
  - "RME"
  - "User Interface"
original_url: "https://thebuildingcoder.typepad.com/blog/2020/03/split-pipe-and-headless-revit.html "
typepad_basename: "split-pipe-and-headless-revit"
typepad_status: "Publish"
---

<p>A short note on splitting pipes. 
This pretty standard functionality in the Revit MEP user interface can be a bit tricky to find in the API:</p>

<p><strong>Question:</strong> The UI provides the split command (SL) to split a pipe into two without losing other connected elements.
How can I achieve the same in API?</p>

<p><strong>Answer:</strong> You can use the <code>PlumbingUtils</code> <code>BreakCurve</code> method.
This is also available for duct work in <code>MechanicalUtils</code> <code>BreakCurve</code>.</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a4f0d2d3200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a4f0d2d3200d img-responsive" style="width: 481px; display: block; margin-left: auto; margin-right: auto;" alt="Split pipe retaining connections" title="Split pipe retaining connections" src="/assets/image_663b96.jpg" /></a><br /></p>

<p></center></p>
