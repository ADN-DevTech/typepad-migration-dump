---
layout: "post"
title: "TinkerCAD Beta で見る High Frequency Data"
date: "2016-11-02 00:04:16"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2016/11/high-frequency-data-on-tinkercad-beta.html "
typepad_basename: "high-frequency-data-on-tinkercad-beta"
typepad_status: "Publish"
---

<p>以前のブログ記事 <strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/10/future-of-forge.html" target="_blank">Forge の未来</a></strong>&#0160;では、ファイル単位の Low Frequency Data と、クラウド ストレージをメモリのように利用する High Frequency Data についてご紹介しました。High Frequency Data は、あくまでコンセプトの段階ですが、この方法を実現したクラウド サービスがありますので、簡単にご案内しておきましょう。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d231f252970c-pi" style="float: right;"><img alt="Logo-tinkercad" class="asset  asset-image at-xid-6a0167607c2431970b01b8d231f252970c img-responsive" src="/assets/image_917957.jpg" style="width: 64px; margin: 0px 0px 5px 5px;" title="Logo-tinkercad" /></a>そのクラウド サービスとは、このブログ記事のタイトルにもある TinkerCAD です。TinkerCAD は、<strong><a href="https://www.tinkercad.com/" target="_blank">https://www.tinkercad.com/</a></strong> から利用することが出来ます。残念ながら、日本語化されてはいませんが、操作はさほど難しくはありません。</p>
<p>この CAD サービスは、10 代の若年層をターゲット ユーザとした 3D モデラーで、クラウドにデータを置き、Web ブラウザをフロント エンド インタフェースに使用するクラウド サービス（SaaS）です。デザイン中のデザイン データは常にクラウドに自動反映されるため、ユーザ インタフェースには「保存」ボタンのようなものはありません。</p>
<p><strong>シェイプ</strong>と呼ばれるプリミティブな形状をドラッグ＆ドロップで作業面に配置し、グリップ操作で形状を変更しながらモデリングを進めていくことが出来ます。配置したシェイプに Hole（穴） 属性の外観を適用してから他のシェイプと重複するように配置、グループ化することで、Hole 属性部分のシェイプで重なったシェイプを切り抜くことも出来ます。言わばブール演算の「差」にあたる操作が可能です。</p>
<p class="asset-video"><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/MwjWT-EvKSU?start=152&amp;feature=oembed" width="500"></iframe></p>
<p>この TinkerCAD ですが、トップページに BETA と書かれた リンクを見ることが出来るはずです。Autodesk ID でサイトにサインインしてから、この BETA リンクから次期 TinkerCAD を起動して、High Frequency Data の動きを把握してみましょう。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d231f0e0970c-pi" style="display: inline;"><img alt="Tinkercad_beta" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d231f0e0970c image-full img-responsive" src="/assets/image_280949.jpg" title="Tinkercad_beta" /></a></p>
<p>BETA リンクから表示されるページ内で [Create new design with beta] ボタンをクリックすると、新しいエディタ画面が開きます。適宜、プリミティブを配置してモデリングを進めていく中で、画面右上の [Share] ボタンをクリックすると、コラボレーション用の URL が生成されます。この URL を共有相手に送って Web ブラウザで開いてもらうと、コラボレーションが始まります。</p>
<p>あとは相互にシェイプを配置したり、編集すると、編集操作の度にその内容がクラウド上のデザイン データ（ファイルではない）に反映され、さらに、共有相手の画面にもほぼ遅延なく表示されることがわかります。</p>
<p class="asset-video"><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/BO5QlZxUEi8?feature=oembed" width="500"></iframe></p>
<p class="asset-video">Forge Viewer の Live Review（ライブ レビュー）に似た機能ですが、TinkerCAD がビューアではなく、相互書き込みが出来る CAD の要素を持つ点が異なります。Forge では、将来、このような機能を実現、提供すべく検討と実証を重ねています。</p>
<p class="asset-video">By Toshiaki Isezaki</p>
