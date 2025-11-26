---
layout: "post"
title: "Fusion 360 API での JavaScript サポートについて"
date: "2018-03-05 00:40:52"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2018/03/about-javascript-support-on-fusion-360-api.html "
typepad_basename: "about-javascript-support-on-fusion-360-api"
typepad_status: "Publish"
---

<p>少し情報が古くなってしまいますが、Fusion 360 API でサポートされていた JavaScript が昨年 7 月の Fusion 360 アップデートから削除されていますので、改めてご案内しておきます。</p>
<p>過去のブログ記事 <a href="http://adndevblog.typepad.com/technology_perspective/2015/11/fusion-360-api-choose-development-language.html" rel="noopener noreferrer" target="_blank">Fusion 360 API：開発言語の選択</a>&#0160;等でもご案内していたとおり、従来、Fusion 360 のスクリプトやアドイン開発でサポートする言語には、JavaScript、Python、C++の 3 種類が存在していました。ただし、実装の都合上、JavaScript のみが Fusion 360 の外部プロセスで実行される仕組みになっていたので、残念ながら、他の言語で記述されたコードに比べると、その実行スピードが遅くなってしまう、という欠点がありました。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2dfbfe3970c-pi" style="display: inline;"><img alt="Fusion_360_api_comparison" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2dfbfe3970c image-full img-responsive" src="/assets/image_285187.jpg" title="Fusion_360_api_comparison" /></a></p>
<p>その影響もあって、Python に比較して利用者の数に伸び悩みがあったようです。抜本的な構造上の改良も難しいことから、非常に残念ながら、2017 年 7 月のリリースから、JavaScript をメインテナンス モードへ移行して Python と C++のみのサポートにする決定がされたようです。</p>
<p>これにともなって、現在お使いいただける Fusion 360では、スクリプトやアドインの新規作成時に利用する 「スクリプトまたはアドインを新規作成」ダイアログからは選択可能なプログラム言語項から JavaScript のみが削除されています。 <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09f8bcb1970d-pi" style="display: inline;"><img alt="New_script_and_addin" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb09f8bcb1970d image-full img-responsive" src="/assets/image_981425.jpg" title="New_script_and_addin" /></a></p>
<p>同様に、Fusion 360 の基本設定の 一般 &gt;&gt; API の「～既定の言語」項からも、JavaScript が削除されています。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c95579e7970b-pi" style="display: inline;"><img alt="Default_api_settings" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c95579e7970b image-full img-responsive" src="/assets/image_913711.jpg" title="Default_api_settings" /></a><br />なお、この JavaScript サポートのメインテナンス モードへの移行後も、既に作成済の JavaScript コードが利用出来なくなるわけではありません。既存のコードは [スクリプトとアドイン] ダイアログの <strong>＋</strong> ボタンからスクリプト、ないしはアドインを追加して実行することが可能です。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c9557a53970b-pi" style="display: inline;"><img alt="Add_script_or_addin" class="asset  asset-image at-xid-6a0167607c2431970b01b7c9557a53970b img-responsive" src="/assets/image_32077.jpg" style="width: 400px;" title="Add_script_or_addin" /></a></p>
<p>By Toshiaki Isezaki&#0160;</p>
