---
layout: "post"
title: "Fusion 360 の更新と Fusion 360 Ultimate"
date: "2014-11-21 03:14:37"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2014/11/fusion-360-update-and-fusion-360-ultimate.html "
typepad_basename: "fusion-360-update-and-fusion-360-ultimate"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c70bd06c970b-pi" style="display: inline;"><img alt="Fusion_360_ultimate" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c70bd06c970b image-full img-responsive" src="/assets/image_789092.jpg" title="Fusion_360_ultimate" /></a></p>
<p>Fusion 360 が更新されて、従来の Fusion 360 に加えて <strong>Fusion 360 Ultimate</strong> が登場しました。Fusion 360 は、契約期間で利用していただく、いわゆる Subscription モデルの有償クラウド サービスです。ただ、販売はドルのみで日本円ので提供がないため、ここでは機能差のみに言及しておきます。同様に、工作機械を用いた金型なの切削に用いる CAM 機能についても、あまり一般的でないため言及はしません。</p>
<p><strong>&#0160;&#0160;&#0160;&#0160;Fusion 360</strong></p>
<ul>
<li>モデリング（ダイレクト、パラメトリック、T-スプライン）</li>
<li>図面</li>
<li>レンダリング</li>
<li>CAM 2.5 軸加工</li>
</ul>
<p><strong>&#0160;&#0160;&#0160;&#0160;Fusion 360 Ultimate</strong></p>
<ul>
<li>上記 Fusion 360 の全機能プラス …</li>
<li>CAM 複数軸加工</li>
<li>アニメーション</li>
</ul>
<p>さて、Fusion 360 の基本的な機能は、<strong><a href="http://adndevblog.typepad.com/technology_perspective/2014/01/fusion-360-cloud-service-update.html" target="_blank">過去のブログ記事</a>&#0160;</strong>で触れました。今回の更新では、T-Spline を用いたフリーフォーム編集から得られたモデルに対して、履歴途中の編集が、再計算によって最終結果に反映されるようになりました。Fusion 360 は、クラウド サービスならではの速いペースで機能更新を繰り返していますが、徐々に操作性や機能が洗練されてきたように感じます。</p>
<p>この機能について、パラメトリック編集も含めた動画を作成しましたので見てみてください。この例では、名刺のようなカードホルダーをフリーフォーム編集で作成後、再度、フリーフォーム編集に戻って、シェル化された形状が再計算される点を紹介しています。また、少しですが Fusion 360 Ultimate のアニメーション作成についてもご覧いただけます。</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="281" src="http://www.youtube.com/embed/Deb-Uq2gDu0?feature=oembed" width="500"></iframe>&#0160;</p>
<p>さて、今回の更新では、<a href="http://adndevblog.typepad.com/technology_perspective/2014/09/fusion-360-api.html" target="_blank"><strong>こちら</strong></a>&#0160;で紹介した JavaScript に加えて、<a href="http://ja.wikipedia.org/wiki/Python" target="_blank"><strong>Python</strong></a> と呼ばれるスクリプト言語がサポートされています。Python は、JavaScript 同様、Web 開発の世界でよく知られた開発言語で、シンプルな構造がプログラミング初心者の方に好まれているようです。どちらの言語で開発されたスクリプトも、従来と同じ、Scripts メニューから呼び出すことが出来ます。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb07b0ea3f970d-pi" style="display: inline;"><img alt="Launch_script_,manager" class="asset  asset-image at-xid-6a0167607c2431970b01bb07b0ea3f970d img-responsive" src="/assets/image_680648.jpg" style="width: 260px;" title="Launch_script_,manager" /> </a></p>
<p style="text-align: left;">スクリプトの実行やデバッグ、編集画面の呼び出しに利用する Script Manager は、JavaScript と Python 共有になっています。画面上では異なるアイコンが利用されていて、全く同じ内容のサンプルが提供されていので、プログラム自体の違いも比較することが出来るはずです。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb07b0ea46970d-pi" style="display: inline;"><img alt="Script_manager" class="asset  asset-image at-xid-6a0167607c2431970b01bb07b0ea46970d img-responsive" src="/assets/image_910677.jpg" style="width: 260px;" title="Script_manager" /></a></p>
<p>オートデスクのデスクトップ製品では、アドインなどの開発に C++ 言語や、C#、VB.NET の言語が利用されていますが、Fusion 360 のカスタマイズに Web 系のオープンソース言語が採用されるのも、Fusion 360 が Web やクラウドを有効に活用する世代に受け入れられる証なのかも知れません。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d09597dc970c-pi" style="display: inline;"><img alt="Spyder" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d09597dc970c image-full img-responsive" src="/assets/image_541243.jpg" title="Spyder" /></a></p>
<p>Fusion 360 は、<a href="http://www.autodesk.com/fusion" target="_blank">http://www.autodesk.com/fusion</a> から無償で体験版（Free Trial）をお試しいただくことが出来ます。</p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
