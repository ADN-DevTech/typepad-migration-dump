---
layout: "post"
title: "Modelling Small Details"
date: "2016-02-01 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Content"
  - "DWG"
  - "Family"
  - "Geometry"
original_url: "https://thebuildingcoder.typepad.com/blog/2016/02/modelling-small-details.html "
typepad_basename: "modelling-small-details"
typepad_status: "Publish"
---

<p>I am back from
the <a href="http://www.bimprogramming.com">BIM Programming</a>
<a href="http://thebuildingcoder.typepad.com/blog/2016/01/bim-programming-madrid-and-spanish-connectivity.html">conference and workshops in Madrid</a> and
rather flooded with overdue stuff, so here is just a quick note on how to you can model small details in Revit, if you really need to, courtesy
of Jose Ignacio Montes of <a href="http://avatarbim.com">Avatar BIM</a>.</p>

<p>As you are perfectly well aware, Revit will not allow you to model things smaller than 1/8<sup>th</sup> of an inch directly in the project environment, as we have seen by trying to create smaller and smaller line and direct shape elements until an exception is thrown:</p>

<ul>
<li><a href="http://thebuildingcoder.typepad.com/blog/2009/07/think-big-in-revit.html">Think Big in Revit</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2014/05/directshape-performance-and-minimum-size.html">DirectShape Performance and Minimum Size</a></li>
</ul>

<p>Jose presents a simple workaround using an imported DWG file:</p>

<p>Este es un veierteaguas de un remate de cubierta. Lleva dos tornillos que se repiten en más sitios, por lo que son 'detail items' insertados.</p>

<p><em>This shows a gutter cover. It has two little screws that are repeated in other places, so they are inserted as detail items.</em></p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301bb08b47612970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301bb08b47612970d image-full img-responsive" alt="Revit design with small details" title="Revit design with small details" src="/assets/image_2121b3.jpg" border="0" /></a><br /></p>

<p></center></p>

<p>El tornillo está formado por una máscara de líneas invisibles y un DWG importado con todo su detalle.</p>

<p><em>The screw is formed by a mask of invisible lines and an imported DWG with all its detail.</em></p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301bb08b4764d970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301bb08b4764d970d img-responsive" alt="Masking lines" title="Masking lines" src="/assets/image_16acbd.jpg" /></a><br /></p>

<p></center></p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b8d199da4a970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b8d199da4a970c img-responsive" alt="Detailed screw DWG" title="Detailed screw DWG" src="/assets/image_12a9e0.jpg" /></a><br /></p>

<p></center></p>

<p>La familia de Clestra Metropoline usa el mismo sistema, pero dentro de un perfil de muro cortina. Este es un truco muy útil porque a poca gente se le ocurre meter un DWG dentro de un profile!</p>

<p><em>Our Clestra Metropoline family uses the same system, but within a curtain wall profile. This is a very useful trick that few people are aware of, to embed a DWG within a profile!</em></p>

<p>Lo mismo puede hacerse con ventanas, puertas o cualquier otro elemento que tenga detalles muy finos.</p>

<p><em>You can use the same idea with windows, doors or any other element that includes very fine detail.</em></p>

<p>Samples:</p>

<ul>
<li><a href="http://thebuildingcoder.typepad.com/files/modulo_vierteaguas.rfa">MODULO_VIERTEAGUAS.rfa</a> &ndash; gutter cover family</li>

<li><a href="http://thebuildingcoder.typepad.com/files/clestra_metropoline.rvt">CLESTRA_METROPOLINE.rvt</a> &ndash; Clestra Metropoline curtain wall profile</li>
</ul>

<p>Many thanks to Jose for sharing this nice approach!</p>
