---
layout: "post"
title: "Revit 2026 新機能 ～ その１"
date: "2025-04-11 01:24:20"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2025/04/new-features-on-revit-2026-part1.html "
typepad_basename: "new-features-on-revit-2026-part1"
typepad_status: "Publish"
---

<p>今回は、Autodesk Revit 2026 の専門分野に共通するコア機能に関連する新機能をご紹介いたします。</p>
<p><strong>テクニカル プレビュー: アクセラレーテッド グラフィックス</strong></p>
<p>3D ビューと 2D ビューのナビゲーション パフォーマンスが大幅に向上し、設計をより迅速にレビューできるようになりました。</p>
<p>このテクニカル プレビューを利用すると、Revit の新しいグラフィックス プラットフォームにいち早くアクセスし、グラフィックス カードに重点を置いた、より高速なナビゲーションと最適化されたハードウェア使用率を実現できます。</p>
<p>アクセラレーテッド グラフィックスはビューごとに有効化され、モデルやビュー自体は変更されません。ビューまたはモデルを閉じると、そのビューではアクセラレーテッド グラフィックス モードが無効になります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860e7615e200b-pi" style="display: inline;"><img alt="Revit2026_02_02" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860e7615e200b image-full img-responsive" src="/assets/image_363005.jpg" title="Revit2026_02_02" /></a></p>
<p><a class="asset-img-link" href="/assets/image_725178.jpg" style="display: inline;"><img alt="Revit2026_02_01" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3d0adc4200c img-responsive" src="/assets/image_725178.jpg" title="Revit2026_02_01" /></a></p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3d0ae30200c-pi" style="display: inline;"><img alt="Revit2026_02_05" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3d0ae30200c img-responsive" src="/assets/image_384828.jpg" title="Revit2026_02_05" /></a></p>
<hr />
<p><strong>新しい[リンクを管理]ダイアログ</strong></p>
<p>新しい[リンクを管理]ダイアログには、9 種類のリンク データ(Revit モデル、IFC リンク、CAD 形式、DWF マークアップ、点群、地盤面、PDF、イメージ、コーディネーション モデル)と、3 種類の読み込みデータ(CAD、イメージ、PDF)がツリー構造にグループ化されて表示されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860e76175200b-pi" style="display: inline;"><img alt="Revit2026_02_03" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860e76175200b img-responsive" src="/assets/image_21138.jpg" title="Revit2026_02_03" /></a></p>
<hr />
<p><strong>IFC リンクの機能強化</strong></p>
<p>IFC ファイルのモデルへのリンクが、旧リリースの Revit より最大 50% 高速になりました。</p>
<p>さらに、IFC モデルをリンクする際のコントロールが強化されました。リンクのダイアログで、ドロップダウン リストを使用して配置オプションを選択します。IFC リンクは、書き出し時に許容されるのと同じデータムと方向で配置できます。既定の位置の場合、リンクの原点が Revit 内部の原点に配置されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3d0add8200c-pi" style="display: inline;"><img alt="Revit2026_02_04" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3d0add8200c img-responsive" src="/assets/image_39243.jpg" title="Revit2026_02_04" /></a></p>
<hr />
<p><strong>地形ソリッドの点のしきい値</strong></p>
<p>モデルでリンクした地形ソリッドとネイティブの地形ソリッドを作成するのに使用する点の数をコントロールします。</p>
<p>Revit.ini ファイルの 2 つの設定によって、モデル内の地形ソリッドで使用する点の数をコントロールします。</p>
<ul>
<li>LinkToposolidMaxPointThreshold: 地盤面をモデルにリンクする際に使用します。</li>
<li>NativeToposolidMaxPointThreshold: 読み込んだ DWG またはテキスト ファイルから地形ソリッド要素を作成する際に使用します。</li>
</ul>
<p>Revit.ini ファイルでこれらの値を設定します。有効な範囲は 10,000 ～ 50,000 です。地形ソリッド要素の作成に使用する点の数が多いほど、表現はより正確になりますが、地形ソリッドを編集するときやモデルをナビゲートするときのパフォーマンスに影響を与える可能性があります。デフォルト値はどちらの設定でも 20,000 点です。</p>
<p>Revit.ini ファイルの設定値を変更すると、変更後に作成された地形ソリッドに設定が適用されます。変更前に作成された地形ソリッドは影響を受けません。</p>
<hr />
<p><strong>図面枠の尺度の優先設定</strong></p>
<p>シートに異なる尺度の複数のビューが含まれている場合、図面枠タイプ パラメータである[尺度の優先設定(複数の値)]の値を設定できるようになりました。<br />尺度の値の優先設定パラメータには文字のみを含めることができます。数値はサポートされていません。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860e761b6200b-pi" style="display: inline;"><img alt="Revit2026_02_06" class="asset  asset-image at-xid-6a0167607c2431970b02e860e761b6200b img-responsive" src="/assets/image_278927.jpg" title="Revit2026_02_06" /></a></p>
<hr />
<p><strong>コーディネーション モデルの機能強化</strong></p>
<p>グラフィックスの外観、要素の表示設定、Revit キャンバス内でコーディネーション モデルの直接配置とビューが可能な機能など、コーディネーション モデルの機能が強化されました。</p>
<p>各コーディネーション モデルに色を割り当てることで、異なるモデルをすばやく区別できます。コーディネーション モデル要素の各カテゴリに特徴的な色を使用して、識別しやすくすることもできます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860fe586f200d-pi" style="display: inline;"><img alt="Revit2026_02_07" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860fe586f200d img-responsive" src="/assets/image_94128.jpg" title="Revit2026_02_07" /></a></p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860e761c8200b-pi" style="display: inline;"><img alt="Revit2026_02_08" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860e761c8200b img-responsive" src="/assets/image_711102.jpg" title="Revit2026_02_08" /></a></p>
<hr />
<p>By Ryuji Ogasawara</p>
