---
layout: "post"
title: "Revit 2015 R2 と Revit SDK"
date: "2014-10-27 23:44:56"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2014/10/revit-2015-r2-and-revit-sdk.html "
typepad_basename: "revit-2015-r2-and-revit-sdk"
typepad_status: "Publish"
---

<div id="section2">
<p>Maintenance Subscription や&#0160;Desktop Subscription にご加入いたただいているお客様に、Autodesk Revit 2015 R2 の提供が開始されました。Autodesk Revit 2015 R2 は、Subscription センターからダウンロードしていただくことが出来ます。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb07a0e781970d-pi" style="display: inline;"><img alt="Revit2015r2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb07a0e781970d image-full img-responsive" src="/assets/image_215059.jpg" title="Revit2015r2" /></a></p>
<p>Revit 2015 R2 は、世界中のユーザから寄せられた要望や機能を、従来の Revit 2015 に追加したバージョンです。つまり、いくつかの新機能が搭載されています。ダウンロードした自己回答形式の exe ファイルを実行することで、インストール済の既存の Revit 2015 に上書きする形でインストールすることができます。</p>
<p>インストールが完了した Revit を起動しても、バージョン情報には Revit 2015 R2 とは記載されません。唯一、ビルド番号が、20140905_0730(x64) となります。また、下記のスクリーンショットからは、UR4 ベースであることもわかります。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d085bf6e970c-pi" style="display: inline;"><img alt="About" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d085bf6e970c image-full img-responsive" src="/assets/image_614914.jpg" title="About" /></a></p>
<p>Revit 2015 の新機能について、ここでは、3つのカテゴリに分けて概要のみをお伝えしておきます。</p>
<p><strong>パワー</strong>:</p>
<ul>
<li>Revit に統合された Project Solon で、Green Building Studioに搭載されたダッシュボードにて予測されたエネルギー パフォーマンスの情報をより視覚的にカスタマイズ可能。</li>
<li>
<p>Revitでリンク IFC モデルしたモデルに対して、Revit モデル内のいくつかのファミリのホストやスナップ、寸法や位置合わせが可能。</p>
</li>
<li>
<p>通常確認のできない拘束関係も寸法位置合わせ拘束関係を「拘束のリピール」モードビューで表示。</p>
</li>
<li>
<p>Revit とともにインストールされるデザイン計算用の Dynamo インタフェースで、ビジュアルなプログラミングを探求。これによって、Revit API のパワーをより簡単に拡張可能。</p>
</li>
</ul>
<p><strong>パフォーマンス</strong>:&#0160;</p>
<ul>
<li>複雑な地形面、サブ領域や建築舗装（道路）の編集や再作図が高速に。</li>
<li>大規模モデルの解析時にメモリ消費を抑えて Revit 内でのエネルギー解析モデルのパフォーマンスがより向上。</li>
<li>複数のファミリ インスタンスを含むビューの更新をより高速に。</li>
<li>ビュー内で Revit リンクの表示が高速化。</li>
</ul>
<p><strong>生産性</strong>:&#0160;</p>
<ul>
<li>パース ビューでの作業で、パース ビューでモデリング機能が使用可能に。これにより、ビューを変更することなく、素早い調整が可能に。</li>
<li>複数の壁結合を一緒に編集することで時間を節約。</li>
<li>タイプ選択とすべてのドロップ ダウン リストで、検索機能群による、より素早くコンテンツを検索。</li>
<li>ハイパーリンク ビューによりRevit からネイティブな PDF ファイルの出力を行った際に、各PDF図面間にリンク付が可能に。</li>
<li>ユーザ インタフェースの改良により、構造要素で簡単な対話操作が可能に。.</li>
<li>全ての2次元図面から配筋配置操作が可能に。また複数の鉄骨梁に対して終点位置の変更が可能。</li>
</ul>
<p>さて、Revit 2015 R2 には、API 上にも新機能を含む改良が反映されています。 もっとも分かり易いのは、寸法に対する API 操作です。従来、Revit API では、作図した寸法文字の位置などを変更することが出来るようになっています。これを利用するには、Revit 2015 R2 に加えて、Revit 2015 R2 用に更新された Revit SDK を利用する必要があります。</p>
<p>Revit 2015 R2 用の Revit SDK は、&#0160;<strong>Revit 2015 SDK for Subscription Release</strong>&#0160; の名前で <strong>Revit Developer Center</strong>（<strong><a href="http://www.autodesk.co.jp/developrevit" target="_blank">http://www.autodesk.co.jp/developrevit</a></strong>）に公開されていますので、適宜ダウンロードしてみてください。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c6fdc96a970b-pi" style="display: inline;"><img alt="Sdk_download" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c6fdc96a970b image-full img-responsive" src="/assets/image_551061.jpg" title="Sdk_download" /></a></p>
<p>新しい Revit SDK には、前述の寸法機能を反映した&#0160;DimensionLeaderEnd プロジェクトが Samples フォルダに含まれています。その他、Revit API 上の更新点の詳細については、SDK のインストール後に展開される Revit Platform API Changes and Additions-Subscription Release.docx をご参照ください。</p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
</div>
