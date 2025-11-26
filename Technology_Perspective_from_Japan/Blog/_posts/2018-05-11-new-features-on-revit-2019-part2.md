---
layout: "post"
title: "Revit 2019 の新機能 その2"
date: "2018-05-11 01:13:03"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2018/05/new-features-on-revit-2019-part2.html "
typepad_basename: "new-features-on-revit-2019-part2"
typepad_status: "Publish"
---

<p>Revit 2019 の新機能についてご紹介させて頂きます。今回は、専門分野共通の新機能、更新内容、API の対応状況を解説いたします。</p>
<p><span style="font-size: 14pt;"><strong>タブ ビューとタイル ビュー</strong></span></p>
<p>モデルまたはファミリをビューで作業する際、現在の作業をやりやすくするために、必要に応じてビューを整理できるようになりました。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0223c84a4580200c-pi" style="display: inline;"><img alt="Revit 2019 Part2-9" class="asset  asset-image at-xid-6a0167607c2431970b0223c84a4580200c img-responsive" src="/assets/image_318910.jpg" title="Revit 2019 Part2-9" /></a></p>
<p>タブ ビュー<br />作図領域の各ビュー(または集計表、シート)には、独自のタブがあります。タブ ビュー ツールを使用すると、タイル ビューを個別のタブにして、1 つのウィンドウに結合できます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0223c84a44c5200c-pi" style="display: inline;"><img alt="Revit 2019 Part2-1" class="asset  asset-image at-xid-6a0167607c2431970b0223c84a44c5200c img-responsive" src="/assets/image_199825.jpg" title="Revit 2019 Part2-1" /></a></p>
<p>タイル ビュー<br />タイル ビュー ツールを使用すると、作図領域内で個別のウィンドウ(タイル)にタブ ビューを分割できるため、複数のビューを同時に見ることができます。必要に応じて、ビューをあるタイルから別のタイルにドラッグして整理します。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0224e038e193200d-pi" style="display: inline;"><img alt="Revit 2019 Part2-2" class="asset  asset-image at-xid-6a0167607c2431970b0224e038e193200d img-responsive" src="/assets/image_300124.jpg" title="Revit 2019 Part2-2" /></a></p>
<p class="asset-video"><iframe allow="autoplay; encrypted-media" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/RN_N2br8Zj4?feature=oembed" width="500"></iframe></p>
<p>&#0160;</p>
<p><span style="font-size: 14pt;"><strong>マルチ モニタ サポート</strong></span></p>
<p>プロジェクト ビューを Revit アプリケーション ウィンドウから独立したウィンドウに分離できます。必要に応じて別のモニタにウィンドウを移動して、ワークフローをサポートします。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0224df31f4c7200b-pi" style="display: inline;"><img alt="Revit 2019 Part2-3" class="asset  asset-image at-xid-6a0167607c2431970b0224df31f4c7200b img-responsive" src="/assets/image_390900.jpg" title="Revit 2019 Part2-3" /></a></p>
<p class="asset-video"><iframe allow="autoplay; encrypted-media" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/ynG1ijvoyQQ?feature=oembed" width="500"></iframe></p>
<p>&#0160;</p>
<p><span style="font-size: 14pt;"><strong>規則によるビュー フィルタの OR 条件</strong></span></p>
<p>パラメータ値に基づいて要素を識別するビュー フィルタを作成する場合、AND 条件に加えて OR 条件を使用することができるようになりました。意図した結果を取得するために、複数の規則および規則セットの作成、および規則セットのネストができます。この機能改善によって、カテゴリとパラメータ値に基づいた要素を識別する複雑で高度な規則を定義できるようになりました。これらのフィルタをビューに適用して、識別した要素の表示設定またはグラフィックス表示を変更します。</p>
<p><a class="asset-img-link" href="http://a2.typepad.com/6a01bb07a2cb32970d0224e038e1fa200d-pi" style="display: inline;"><img alt="Revit 2019 Part2-8" class="asset  asset-image at-xid-6a01bb07a2cb32970d0224e038e1fa200d img-responsive" src="/assets/image_394318.jpg" style="border: 1px solid #A8A8A8;" title="Revit 2019 Part2-8" /></a></p>
<p class="asset-video"><iframe allow="autoplay; encrypted-media" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/E_qZfmAynxU?feature=oembed" width="500"></iframe></p>
<p>API では、これまで ParameterFilterElement で取得できた FilterRules のリストに代わり、 ElementFilter が返却されます。ElementLogicalFilter.GetFilters() メソッドで、ElementLogicalFilter で構成された ElementFilters のセットを取得できます</p>
<p>&#0160;</p>
<p><span style="font-size: 14pt;"><strong>3D ビューのレベル</strong></span></p>
<p>3D ビューで作業するときに、モデルのレベルを表示および修正することができます。3D ビューで作業するときにレベルは追加できませんが、移動、修正、および削除することができます。ビューのレベルの表示設定を変更するには、[注釈カテゴリ]タブの[表示/グラフィックスの上書き]ダイアログ ボックスを使用します。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0224e038e207200d-pi" style="display: inline;"><img alt="Revit 2019 Part2-4" class="asset  asset-image at-xid-6a0167607c2431970b0224e038e207200d img-responsive" src="/assets/image_996539.jpg" title="Revit 2019 Part2-4" /></a></p>
<p class="asset-video"><iframe allow="autoplay; encrypted-media" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/jdCoE3QtXbY?feature=oembed" width="500"></iframe></p>
<p>&#0160;</p>
<p><span style="font-size: 14pt;"><strong>Revit クラウド ワークシェアリング</strong></span></p>
<p>Revit Cloud Worksharingを使用して、クラウドの Revit モデルでコラボレーションすると、モデルを BIM 360 ドキュメント管理に保存できるようになりました。&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0223c84a452c200c-pi" style="display: inline;"><img alt="Revit 2019 Part2-5" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0223c84a452c200c image-full img-responsive" src="/assets/image_303115.jpg" title="Revit 2019 Part2-5" /></a></p>
<p>API では、C4R モデルを API で開くことができるようになります。</p>
<ul>
<li>Application.OpenDocumentFile(…)</li>
<li>UIApplication.OpenAndActivateDocument(…)</li>
<li>Application.OpenDocumentFile(…, IOpenFromCloudCallback)</li>
<li>UIApplication.OpenAndActivateDocument(…, IOpenFromCloudCallback)</li>
</ul>
<p>またクラウド上の ModelPath を取得できるようになりました。</p>
<ul>
<li>ModelPathUtils.ConvertCloudGUIDsToCloudPath(GUID, GUID)</li>
</ul>
<p>&#0160;</p>
<p>次回は、建築設計分野の新機能と更新内容、API の対応状況についてご紹介いたします。</p>
<p>By Ryuji Ogasawara</p>
