---
layout: "post"
title: "Revit 2021 の新機能 その 3"
date: "2020-04-24 01:19:50"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2020/04/new-features-on-revit-2021-part3.html "
typepad_basename: "new-features-on-revit-2021-part3"
typepad_status: "Publish"
---

<p><a href="https://adndevblog.typepad.com/technology_perspective/2020/04/new-features-on-revit-2021-part2.html">前回に</a>引き続き、今回は、Revit 2021 の構造設計分野の新機能と機能向上についてご紹介致します。</p>
<p><strong>新しい標準 3D 鉄筋形状</strong></p>
<p>鉄筋の始終端でフックを回転させて、3D 鉄筋形状を定義できるようになりました。鉄筋の椅子(サンダー)や他の 3D 鉄筋をモデル化し、集計表に完全な製造データを抽出します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a5238103200b-pi" style="display: inline;"><img alt="Revit2021_19" class="asset  asset-image at-xid-6a0167607c2431970b0240a5238103200b img-responsive" src="/assets/image_821047.jpg" title="Revit2021_19" /></a></p>
<p class="asset-video"><iframe allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/WNl9y4kJydI?feature=oembed" width="500"></iframe></p>
<p>API では、指定した端点にフックの回転角度を設定できるように、Rebar クラスと RebarShape クラスに下記のメソッドが追加されました。</p>
<ul>
<li>GetHookRotationAngle()</li>
<li>SetHookRotationAngle()</li>
</ul>
<p>&#0160;</p>
<p><strong>円弧形状の鉄筋を接合するカプラー</strong></p>
<p>鉄筋カプラーを使用して、接している円弧形状の鉄筋を円形コンクリート構造内で接合できるようになりました。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b025d9b4975dd200c-pi" style="display: inline;"><img alt="Revit2021_20" class="asset  asset-image at-xid-6a0167607c2431970b025d9b4975dd200c img-responsive" src="/assets/image_211625.jpg" title="Revit2021_20" /></a></p>
<p class="asset-video"><iframe allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/Fziq8d8zGt4?feature=oembed" width="500"></iframe></p>
<p>&#0160;</p>
<p><strong>鉄筋カプラーなしの端部処理</strong></p>
<p>鉄筋カプラーを使用せずに鉄筋端部に端部処理を追加できます。<br />つまり、鉄筋カプラーで鉄筋を接合することなく、プロジェクト内の任意の鉄筋に端部処理を指定できます。</p>
<p class="asset-video"><iframe allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/782vjAAilj8?feature=oembed" width="500"></iframe></p>
<p>API では、カプラーなしの端部処理を作成できるように、Rebar.SetEndTreatmentTypeId() メソッドが追加されました。</p>
<p>&#0160;</p>
<p><strong>より簡単なフック長さの調整</strong></p>
<p>プロジェクト内の各鉄筋に対してフック長さを簡単に指定できるようになりました。<br />フック タイプを複製して、そのタイプの長さを変更する必要はありません。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b025d9b497d4f200c-pi" style="display: inline;"><img alt="Revit2021_21" class="asset  asset-image at-xid-6a0167607c2431970b025d9b497d4f200c img-responsive" src="/assets/image_831510.jpg" title="Revit2021_21" /></a></p>
<p class="asset-video"><iframe allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/o6lS9cZz7oc?feature=oembed" width="500"></iframe></p>
<p>API では、Rebar クラスに下記のメソッドが追加されました。</p>
<ul>
<li>EnableHookLengthOverride()</li>
<li>IsHookLengthOverrideEnabled()</li>
<li>GetOverridableHookParameters</li>
</ul>
<p>&#0160;</p>
<p><strong>詳細な鉄骨モデリング用の新しいスチフナの接合タイプ</strong></p>
<p>スチフナの接合が Revit 鉄骨接合リストに含まれました。<br />この新しい接合タイプは、入力梁または柱に溶接された、梁または柱の断面を補強する 1 枚または 2 枚の鋼板で構成されています。</p>
<p class="asset-video"><iframe allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/6TnMIPLTW_s?feature=oembed" width="500"></iframe></p>
<p>&#0160;</p>
<p><strong>3D ソリッド メッシュ筋の視覚化</strong></p>
<p>メッシュ筋やカスタム メッシュ筋を、ソリッドまたは前面に表示して 3D ビューで表示できます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a52388ee200b-pi" style="display: inline;"><img alt="Revit2021_22" class="asset  asset-image at-xid-6a0167607c2431970b0240a52388ee200b img-responsive" src="/assets/image_599892.jpg" title="Revit2021_22" /></a></p>
<p class="asset-video"><iframe allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/KBQyZviBHuk?feature=oembed" width="500"></iframe></p>
<p>API では、FabricSheet クラスに下記のメソッドが追加されました。</p>
<ul>
<li>FabricSheet.IsSolidInView()</li>
<li>FabricSheet.<span style="color: #111111;">SetSolidInView</span>()</li>
<li>FabricSheet.IsUnobscuredInView()</li>
<li>FabricSheet.SetUnobscuredInView()</li>
</ul>
<p>次回は、MEP 設計分野の新機能と機能向上についてご紹介いたします。</p>
<p>By Ryuji Ogasawara</p>
