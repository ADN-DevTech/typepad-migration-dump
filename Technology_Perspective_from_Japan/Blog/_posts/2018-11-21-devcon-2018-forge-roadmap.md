---
layout: "post"
title: "Forge DevCon Las Vegas 2018 アップデート: Forge ロードマップ"
date: "2018-11-21 23:00:05"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2018/11/devcon-2018-forge-roadmap.html "
typepad_basename: "devcon-2018-forge-roadmap"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3c1a748200b-pi" style="float: right;"></a>Forge DevCon Las Vegas 2018 では、<a class="openInPopup" href="https://autodeskuniversity.smarteventscloud.com/connect/sessionDetail.ww?SESSION_ID=227961"><span class="abbreviation">FDC227961 -&#0160;</span><span class="title">Building for the Future: Forge Road Map</span></a>&#0160;等のセッションで、昨年同様、現在開発中で今後のリリースを予定している Forge Platform API の発表がありました。アナウンスされたすべての API のリリースが確定している訳ではありませんが、ご参考まで、ここ最近に加えられた既存 API への機能追加も含めて、概略のみご紹介しておきます。</p>
<hr />
<p>まず、既存 API に最近加えられた機能、あるいは、改善された機能です。既に<strong><a href="https://forge.autodesk.com/" rel="noopener noreferrer" target="_blank"> Forge ポータル</a></strong>上のドキュメント（Change Log）に記載のあるものについては、項目名にリンクをしていますのでジャンプして詳細をご確認ください。&#0160;</p>
<p><strong>BIM 360 API</strong></p>
<ul>
<li><a href="https://forge.autodesk.com/en/docs/bim360/v1/change_history/issues_v1_changelog/" rel="noopener noreferrer" target="_blank">Issues API</a>（問題、指摘事項）</li>
<li><a href="https://forge.autodesk.com/en/docs/bim360/v1/change_history/rfis_v1_changelog/" rel="noopener noreferrer" target="_blank">RFIs API</a>（情報提供依頼）</li>
<li><a href="https://forge.autodesk.com/en/docs/bim360/v1/change_history/checklists_v1_changelog/" rel="noopener noreferrer" target="_blank">Checklists API</a>（チェックリスト）</li>
</ul>
<p><strong>Viewer（Forge Viewer JavaScript API）</strong></p>
<ul>
<li><a href="https://forge.autodesk.com/en/docs/viewer/v6/change_history/changelog_v6/">PDF（変換後の SVF） ロード時のパフォーマンスを約 30% から最大 80% 高速化</a></li>
<li><a href="https://forge.autodesk.com/en/docs/viewer/v6/change_history/changelog_v4/" rel="noopener noreferrer" target="_blank">2D 忠実度の向上</a></li>
<li><a href="https://forge.autodesk.com/en/docs/viewer/v6/change_history/changelog_v5/" rel="noopener noreferrer" target="_blank">2D モデルのモノクローム表示モード</a></li>
<li><a href="https://forge.autodesk.com/en/docs/viewer/v6/change_history/changelog_v3/" rel="noopener noreferrer" target="_blank">非フォトリアリスティック レンダリング（NPR）</a></li>
<li><a href="https://forge.autodesk.com/en/docs/viewer/v6/change_history/changelog_v3/" rel="noopener noreferrer" target="_blank">3D ファイルで画層（レイヤー）アクセス</a></li>
<li><a href="https://forge.autodesk.com/en/docs/viewer/v6/change_history/changelog_v6/" rel="noopener noreferrer" target="_blank">Split-screen（画面分割）Extension</a></li>
</ul>
<p><strong>Model Derivative API</strong></p>
<ul>
<li><a href="https://forge.autodesk.com/en/docs/model-derivative/v2/change_history/changelog/" rel="noopener noreferrer" target="_blank">大規模メタデータ/プロパティ セットを持つモデルのパフォーマンス向上</a></li>
<li><a href="https://forge.autodesk.com/en/docs/model-derivative/v2/change_history/changelog/" rel="noopener noreferrer" target="_blank">予想最大長（20 MB）を超える大規模メタデータ/プロパティの取得を強制する “forceget” クエリーパラメータの追加</a></li>
<li><a href="https://forge.autodesk.com/en/docs/model-derivative/v2/change_history/changelog/" rel="noopener noreferrer" target="_blank">2D DWG メタデータとプロパティ サポートの強化</a></li>
<li><a href="https://forge.autodesk.com/en/docs/model-derivative/v2/change_history/changelog/" rel="noopener noreferrer" target="_blank">2D DGN（<span class="problematic" id="id2">*</span>.dgn,&#0160;<span class="problematic" id="id4">*</span>.i.dgn）の SVF 変換のサポート</a></li>
<li><a href="https://forge.autodesk.com/en/docs/model-derivative/v2/change_history/changelog/" rel="noopener noreferrer" target="_blank">3D DGN のカスタム プロパティのサポート</a></li>
</ul>
<p><strong>WebHooks API</strong></p>
<ul>
<li>Fusion Lifecycle （Item の Create、Clone、Lock/Unlock、Release の通知のサポート）&#0160;&#0160;</li>
</ul>
<hr />
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad37bf24d200c-pi" style="float: right;"><img alt="Under_dvelopment" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad37bf24d200c img-responsive" src="/assets/image_331092.jpg" style="margin: 0px 0px 5px 5px;" title="Under_dvelopment" /></a>次にご紹介するのは、現在開発中、リリースを予定している API 概要です。まず、昨年のロードマップで示されていた Design Automation API のアップデートです。&#0160;</p>
<p><strong>Design Automation API</strong></p>
<p style="padding-left: 30px;">現在、クラウド上の AutoCAD コアエンジのみをサポートしている Design Automation API（V2） ですが、AutoCAD の他に Inventor、Revit、3ds Max を加えた&#0160;&#0160;V3（バージョン3）が <strong>Public Beta</strong> となります。残念ながら、<span style="background-color: #ffff00;">Revit（Design Automation API for Revit）のみ、Public Beta 化が 2019年1月28日（米国太平洋標準時）に設定されていますので、その点ご注意ください。</span>Public Beta として利用可能な&#0160;Design Automation API for AutiCAD、同 Inventor、同 3ds Max については、<strong><a href="https://forge.autodesk.com/" rel="noopener noreferrer" target="_blank">Forge ポータル</a></strong>上でドキュメントが公開されています。当該ページは、<a href="https://forge.autodesk.com/en/docs/design-automation/v3/developers_guide/overview/" rel="noopener noreferrer" target="_blank">https://forge.autodesk.com/en/docs/design-automation/v3/developers_guide/overview</a>/ となります。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3a1ffd0200d-pi" style="display: inline;"><img alt="Da" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3a1ffd0200d image-full img-responsive" src="/assets/image_433832.jpg" title="Da" /></a>&#0160;</p>
<p><strong>Reality Capture API</strong></p>
<p style="padding-left: 30px;">撮影した複数の写真から、3D メッシュ、点群、オルソ画像などを演算生成する Reality Capture API について、現在、最も多く利用されている建設プロジェクトの進捗管理用途でのチューニングを進めています。具体的には、生成された点群データでも 、より明瞭に変化を把握、指摘出来るよう、点群データ生成の見直しをおこなっています。（Forge Viewer での点群表示は、引き続きサポートされていません。）</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3a200b0200d-pi" style="display: inline;"><img alt="Rc" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3a200b0200d image-full img-responsive" src="/assets/image_249521.jpg" title="Rc" /></a>&#0160;</p>
<p><strong>Data at the Center</strong></p>
<p style="padding-left: 30px;"><strong><a href="https://adndevblog.typepad.com/technology_perspective/2018/11/forge-devcon-las-vegas-2018-update.html" rel="noopener noreferrer" target="_blank">Forge DevCon Las Vegas 2018 アップデート</a></strong> のブログ記事でご案内した点のついて、HFDM と LFDM（ファイル）を融合させると同時に、クラウドを効果的に利用してデータを管理するコンセプトの概要説明がありました。アプリケーションありきはなく、データありきでアプリケーションが従属していくような新しいコンセプトです。現在、Data at the Center を実現するいくつかの API/SDK が開発途上にあります。</p>
<p style="padding-left: 30px;">残念ながら、DevCon 当日にあった当該クラス セッション、<a href="https://autodeskuniversity.smarteventscloud.com/connect/sessionDetail.ww?SESSION_ID=256005" rel="noopener noreferrer" target="_blank">SD256005 - The Future of Data: Forge Data Platform</a>&#0160;や&#0160;&#0160;<a href="https://autodeskuniversity.smarteventscloud.com/connect/sessionDetail.ww?SESSION_ID=231765" rel="noopener noreferrer" target="_blank">The Future of Workflow Forge Workflow Automation</a>&#0160;は当日の収録動画、配布資料（ダウンロード資料）とも用意されていませんが、今後、<a href="http://autodeskcloudaccelerator.com/" rel="noopener noreferrer" target="_blank">Cloud Accelerator</a> イベントなどを通じて、評価の機会が得られることを期待しています。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3c1ab53200b-pi" style="display: inline;"><img alt="Data_at_the_center" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3c1ab53200b image-full img-responsive" src="/assets/image_344642.jpg" title="Data_at_the_center" /></a>&#0160;</p>
<hr />
<p>最後に余談となってしまいますが、Forge DevCon Las Vegas、Autodesk University Las Vegas の開催に先だって、<strong><a href="https://forge.autodesk.com/" rel="noopener noreferrer" target="_blank">Forge ポータル（https://forge.autodesk.com/）</a></strong>もレイアウトやデザインが変更されています。これに伴い、Forge Platform API の各種アイコンも再デザインされています。新しい API の登場と勘違いのなきよう、こちらも、ご注意ください。従来、区別がつきにくかった Data Management API と Design Automation API も差が分かりやすくなっています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3a201d8200d-pi" style="display: inline;"><img alt="New_api_icons" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3a201d8200d image-full img-responsive" src="/assets/image_742284.jpg" title="New_api_icons" /></a></p>
<p>By Toshiaki Isezaki</p>
