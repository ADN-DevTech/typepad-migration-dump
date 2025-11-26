---
layout: "post"
title: "Revit 2019 の新機能 その3"
date: "2018-05-18 01:26:42"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2018/05/new-features-on-revit-2019-part3.html "
typepad_basename: "new-features-on-revit-2019-part3"
typepad_status: "Publish"
---

<p>Revit 2019 の新機能についてご紹介しております。今回は、建築設計分野の新機能、更新内容、API の対応状況を解説いたします。</p>
<p><span style="font-size: 14pt;"><strong>背景塗り潰しパターン</strong></span></p>
<p>モデル要素のグラフィックス表現を定義する際に、前景色と塗り潰しパターンに加えて、背景色と塗り潰しパターンを指定できるようになりました。これらのパターンを使用してビュー内の要素を視覚的に区別することで、プロジェクト ドキュメントの読みやすさを向上させたり、会社や業界標準に適合させることができます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0223c84baf9f200c-pi" style="display: inline;"><img alt="Revit 2019 Part3-1" class="asset  asset-image at-xid-6a0167607c2431970b0223c84baf9f200c img-responsive" src="/assets/image_519055.jpg" title="Revit 2019 Part3-1" /></a></p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0223c84baf9a200c-pi" style="display: inline;"><img alt="Revit 2019 Part3-2" class="asset  asset-image at-xid-6a0167607c2431970b0223c84baf9a200c img-responsive" src="/assets/image_741752.jpg" title="Revit 2019 Part3-2" /></a><br /><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0224df33627b200b-pi" style="float: left;"></a></p>
<p class="asset-video"><iframe allow="autoplay; encrypted-media" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/nVxCTCNLc0E?feature=oembed" width="500"></iframe></p>
<p>API では、既存のパターンやカラープロパティは廃止予定となります。<br />新しいプロパティは、それぞれ「背景」と「前景」 のパターンとカラーに対応します。</p>
<p>影響されるクラス:</p>
<ul>
<li>Materials</li>
<li>OverrideGraphicSettings</li>
<li>FilledRegionType</li>
</ul>
<p>&#0160;</p>
<p><span style="font-size: 14pt;"><strong>マテリアルの外観</strong></span></p>
<p>モデルおよびファミリのマテリアルとともに使用できる新しい外観アセットが用意されています。これらの新しいアセットは、Autodesk レンダリング エンジンでの使用に最適化された物理ベースの定義を使用します。高品質でリアルな視覚効果を、レンダリングされたイメージおよびリアリスティック、またはレイ トレース表示スタイルを使用しているビューに提供します。アップグレードされたモデルは、マテリアルの旧バージョンの外観アセットを引き続き使用します。ビューとレンダリングされたイメージの品質を向上させるには、旧バージョンのアセットを新しいアセットと置き換えます。</p>
<p>旧バージョンのクルミ材を使用した外観アセットのテーブル</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0224df33630b200b-pi" style="display: inline;"><img alt="Revit 2019 Part3-3" class="asset  asset-image at-xid-6a0167607c2431970b0224df33630b200b img-responsive" src="/assets/image_236084.jpg" title="Revit 2019 Part3-3" /></a></p>
<p>新しいクルミ材を使用した外観アセットのテーブル</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0224e03a5420200d-pi" style="display: inline;"><img alt="Revit 2019 Part3-4" class="asset  asset-image at-xid-6a0167607c2431970b0224e03a5420200d img-responsive" src="/assets/image_765097.jpg" title="Revit 2019 Part3-4" /></a></p>
<p class="asset-video"><iframe allow="autoplay; encrypted-media" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/fpiv9ucixpc?feature=oembed" width="500"></iframe></p>
<p>API では、マテリアルの外観アセットに含まれているプロパティを編集できるようになります。 [マテリアル ブラウザ]ダイアログの[外観]タブに表示されるプロパティが対象です。<br />AppearanceAssetEditScope は、アプリケーションが外観アセットの編集セッションを作成・維持するためのスコープです。</p>
<p>&#0160;</p>
<p><span style="font-size: 14pt;"><strong>非トリミング 3D パース ビュー</strong></span></p>
<p>3D ビューのモデリング プロセスを改善するために、ビュー コントロール バーの[ビューをトリミング]ツールをパース ビュー内でオフにできるようになりました。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0224e03a54ba200d-pi" style="display: inline;"><img alt="Revit 2019 Part3-5" class="asset  asset-image at-xid-6a0167607c2431970b0224e03a54ba200d img-responsive" src="/assets/image_421863.jpg" title="Revit 2019 Part3-5" /></a></p>
<p class="asset-video"><iframe allow="autoplay; encrypted-media" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/eJLRXE9tMhk?feature=oembed" width="500"></iframe></p>
<p>&#0160;</p>
<p><span style="font-size: 14pt;"><strong>集計表のブラウザ構成機能</strong></span></p>
<p>Revit 2018 の更新プログラムで追加されたサブスクリプションユーザー向けの機能が標準で使えるようになりました。作業方法に合わせてプロジェクト ブラウザをカスタマイズし、集計表のフィルタ、グループ化、並べ替えを行うことができます(ビューとシートについても同様です)。 プロパティまたはカスタム パラメータに基づいて、最大 3 つのフィルタ レベル、最大 6 つのグループ化レベル、並べ替え条件を、集計表/数量に対して定義することができます。</p>
<p class="asset-video"><iframe allow="autoplay; encrypted-media" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/D0uqpSpWH1c?feature=oembed" width="500"></iframe></p>
<p>API では、BrowserOrganization クラスを使用して、プロジェクトブラウザ上での集計表を整理できるようになります。<br />BrowserOrganizationType.Schedules&#0160;で集計表のためのブラウザ構成を定義します。<br />BrowserOrganization.GetCurrentBrowserOrganizationForSchedules()&#0160;メソッドで、プロジェクトブラウザで使用されている現在の BrowserOrganization を取得します。</p>
<p>&#0160;</p>
<p><span style="font-size: 14pt;"><strong>手すり分割機能</strong></span></p>
<p>スケッチ モード以外でも、手すり要素で[分割]ツールを使用できるようになりました。手すりを分割すると、互いに独立したパス スケッチを持つ要素が作成されます。</p>
<p class="asset-video"><iframe allow="autoplay; encrypted-media" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/1cfXNwUPPRY?feature=oembed" width="500"></iframe></p>
<p>API でもいくつかのプロパティが追加されております。<br />RailingType.RailStructure プロパティで、RailingType の一部として非連続の手すりにアクセスできるようになります。<br />RailingType.BalusterPlacement プロパティで、指定した RailingType に関連する手すり子と支柱の 配置の情報にアクセスできるようになります。</p>
<p>&#0160;</p>
<p><span style="font-size: 14pt;"><strong>垂直方向の文字位置合わせ</strong></span></p>
<p>[文字]ツールを使用してモデルに注釈を付ける場合は、文字の位置合わせを文字注記の上、中央、または下に揃えることができます。</p>
<p class="asset-video"><iframe allow="autoplay; encrypted-media" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/okSK4e7a0bA?feature=oembed" width="500"></iframe></p>
<p>TextNoteOptions.VerticalAlignment プロパティは、TextNote.Create() メソッドで作成された文字注記の垂直な位置合わせを指定できるようになります。</p>
<p>次回は、構造設計分野の新機能と更新内容、API の対応状況についてご紹介いたします。</p>
<p>By Ryuji Ogasawara</p>
