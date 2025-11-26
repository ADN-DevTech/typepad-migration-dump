---
layout: "post"
title: "Revit 2016 レンダリング・エンジン Autodesk Raytracer"
date: "2015-06-19 01:53:13"
author: "Ryuji Ogasawara"
categories:
  - "クラウドサービス"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/06/revit-2016-autodesk-raytracer.html "
typepad_basename: "revit-2016-autodesk-raytracer"
typepad_status: "Publish"
---

<p>Revit 2016 では、3D ビューを静止画でレンダリングする際に、NVIDIA Mental ray と Autodesk Raytracer の 2つのレンダリング エンジンのいずれかを選択できるようになりました。NVIDIA Mental ray は、従来から Revit に搭載されている 3D レンダリング技術です。</p>
<p>Autodesk Raytracer は、物理ベースのレンダリング技術により、物体の表面に直接あたる光源だけではなく、表面から跳ね返る間接光の照り返しまでを反映するグローバルイルミネーションという技術を採用しております。<br />またレンダリング処理には、 GPU ではなく CPU を利用するように設計されています。ラップトップ PC から大規模な高性能マシンのクラスタ環境にも柔軟に対応できるように、安定性と拡張性が重視されています。そのため、A360 のクラウドレンダリングにおいても、このレンダリング技術が利用されております。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d12a81c1970c-pi" style="display: inline;"><img alt="Revit2016 Rendering Settings" class="asset  asset-image at-xid-6a0167607c2431970b01b8d12a81c1970c img-responsive" src="/assets/image_189647.jpg" title="Revit2016 Rendering Settings" /></a><br /><br /></p>
<p>次のレンダリング画像は、解像度品質を「中」に設定して、Mental ray と Raytracer でそれぞれレンダリングしたものです。物体表面の反射に違いがあることがわかります。</p>
<p><strong>NVIDIA Mental ray</strong><br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d12a81d5970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Revit2016 - MentalRay - Rendering" class="asset  asset-image at-xid-6a0167607c2431970b01b8d12a81d5970c img-responsive" src="/assets/image_891904.jpg" title="Revit2016 - MentalRay - Rendering" /></a><br /><br /><strong>Autodesk Raytracer&#0160;</strong><br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb084535dd970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Revit2016 - RayTracer - Rendering" class="asset  asset-image at-xid-6a0167607c2431970b01bb084535dd970d img-responsive" src="/assets/image_135218.jpg" title="Revit2016 - RayTracer - Rendering" /></a><br /><br /></p>
<p>Raytracer でのレンダリングは、NVIDIA mental ray で使用できる品質設定と背景オプションの一部はサポートしておりませんが、レンダリング エンジンの選択肢が増えたことで、より設計者のイメージに合うエンジンを適宜、使い分けてご利用いただけます。<br />ご興味のある方は、ぜひお試しください。</p>
<p>By Ryuji Ogasawara</p>
