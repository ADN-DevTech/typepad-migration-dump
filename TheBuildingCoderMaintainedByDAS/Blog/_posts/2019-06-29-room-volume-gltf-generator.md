---
layout: "post"
title: "Room Volume glTF Generator"
date: "2019-06-29 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Accelerator"
  - "Export"
  - "Forge"
  - "Geometry"
original_url: "https://thebuildingcoder.typepad.com/blog/2019/06/room-volume-gltf-generator.html "
typepad_basename: "room-volume-gltf-generator"
typepad_status: "Publish"
---

<p>I travelled home last night from the Barcelona Forge accelerator and continued working on
the <a href="https://thebuildingcoder.typepad.com/blog/2019/06/improved-room-closed-shell-directshape-for-forge-viewer.html">room volume exporter</a>.</p>

<p>As suggested by <a href="https://forge.autodesk.com/author/michael-beale">Michael Beale</a>,
I now implemented support
for <a href="https://en.wikipedia.org/wiki/GlTF">glTF, the GL Transmission Format</a>.</p>

<p>As described yesterday, I generate generic model <code>DirectShape</code> elements to represent the room volume in the Forge viewer.</p>

<p>I initially generated them using solids returned by the Revit API <code>GetClosedShell</code> method.
However, these not work properly in the Forge viewer.</p>

<p>Therefore, I implemented a triangulation of those solids and generate new ones from that.
They work fine.</p>

<p>While I have the triangulation at hand, it is easy to also generate data for glTF.</p>

<p>That is now implemented in
<a href="https://github.com/jeremytammik/RoomVolumeDirectShape">RoomVolumeDirectShape</a>
<a href="https://github.com/jeremytammik/RoomVolumeDirectShape/releases/tag/2020.0.0.8">release 2020.0.0.8</a>.</p>

<p>It still needs some final tweaks to feed it straight into a glTF viewer, e.g.,
<a href="https://github.com/magicien/GLTFQuickLook">magicien's GLTFQuickLook</a> for Mac,
but we're getting there.</p>

<p>A few more little items to wrap up the Barcelona topic:</p>

<ul>
<li>Kean Walmsley's report on <a href="https://www.keanw.com/2019/06/this-years-forge-accelerator-in-barcelona.html">this year's Forge Accelerator in Barcelona</a></li>
<li>My two favourite restaurants in Barcelona, both in Poblenou, 20 minutes' walk from the Autodesk office:
<ul>
<li><a href="http://www.elspescadors.com">Fish &ndash; Restaurant Els Pescadors</a></li>
<li><a href="http://www.aguaribay-bcn.com">Vegetarian &ndash; Aguaribay</a></li>
</ul></li>
</ul>

<!-- <br/><i>Els Pescadors, situat a Barcelona, és un restaurant especialitzat en arròs i peix des de 1980. Els nostres plats estan elaborats amb producte de proximitat, ecològic i fresc. Vinguin a viure l'experiència de gaudir d'un àpat tranquil i gustós al nucli antic de Poblenou.</i> -->

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a46b8239200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a46b8239200c img-responsive" style="width: 500px; display: block; margin-left: auto; margin-right: auto;" alt="Forge team lunch on the beach" title="Forge team lunch on the beach" src="/assets/image_adfc45.jpg" /></a><br /></p>

<p></center></p>
