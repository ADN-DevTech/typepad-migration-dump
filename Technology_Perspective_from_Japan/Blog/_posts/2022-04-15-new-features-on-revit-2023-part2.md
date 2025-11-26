---
layout: "post"
title: "Autodesk Revit 2023 の新機能 ～ その2"
date: "2022-04-15 01:38:00"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/04/new-features-on-revit-2023-part2.html "
typepad_basename: "new-features-on-revit-2023-part2"
typepad_status: "Publish"
---

<p><a href="https://adndevblog.typepad.com/technology_perspective/2022/04/new-features-on-revit-2023-part1.html">前回の記事</a>に引き続き Autodesk Revit 2023の新機能として、今回は、専門分野に共通のコア機能に関する新機能と機能改善をご紹介致します。</p>
<p><strong>印刷順序をコントロールする</strong><br />PDF の書き出しと印刷セットでのビューとシートの順序をコントロールできるようになりました。<br />PDF 書き出し設定は、指定の印刷順序とともに保存できます。ビュー/シート選択設定を作成する際に、書き出しの印刷順序を選択します。</p>
<p><a class="asset-img-link" href="/assets/image_913967.jpg" style="display: inline;"><img alt="Revit2023-02-01" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e14fde35200b image-full img-responsive" src="/assets/image_913967.jpg" title="Revit2023-02-01" /></a></p>
<p>いずれかのオプションを選択できます。</p>
<ul>
<li>ブラウザ構成: 選択したビューとシートに使用する既存のブラウザ構成を選択します。</li>
<li>シート番号(昇順): 印刷順序に昇順のシート番号を使用します。選択したビューは、印刷順序でシートの後に配置されます。</li>
<li>手動の順序: 選択内のビューおよびシートをドラッグ アンド ドロップして、印刷順序を定義します。</li>
</ul>
<p><strong>マルチ引出線タグの引出線の機能強化</strong><br />同じタグを使用して、複数の要素にタグを付けることができるようになりました。<br />タグの配置中、または既存のタグにホスト(引出線)を追加する場合は、[ホストを追加/削除]オプションを使用します。</p>
<p><a class="asset-img-link" href="/assets/image_22527.jpg" style="display: inline;"><img alt="Revit2023-02-02" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02942fa500b6200c image-full img-responsive" src="/assets/image_22527.jpg" title="Revit2023-02-02" /></a></p>
<p><strong>作業面をすばやく設定する<br /></strong>新しいリボン ボタンとキーボード ショートカットが追加され、[平面を選択]オプションを使用してビューの作業面をすばやく設定できるようになりました。<br />[設定]ボタンのドロップ ダウン メニューから平面を選択することで、[作業面]ダイアログを開かずに作業面を直接設定できるようになりました。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e14fde4d200b-pi" style="display: inline;"><img alt="Revit2023-02-04" class="asset  asset-image at-xid-6a0167607c2431970b0282e14fde4d200b img-responsive" src="/assets/image_995046.jpg" title="Revit2023-02-04" /></a></p>
<p><strong>3D ビューですべての変位要素にタグを付ける</strong><br />3D ビューの方向をロックして、変位要素にタグを付けたり、キーノートを追加できるようになりました。</p>
<p class="asset-video"><iframe allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="400" src="https://www.youtube.com/embed/boX7AsZAMSY?feature=oembed" width="712"></iframe></p>
<p>ただし、3D ビューでは、次のタグ タイプを使用することはできません。</p>
<ul>
<li>マテリアル タグ</li>
<li>エリアや部屋タグ</li>
<li>スペース タグ</li>
</ul>
<p><strong>2D ビューで要素を変位する</strong><br />[要素を変位]ツールを使用して、モデルの 2D ビューで要素を変位します。要素を 2D ビューで変位する手順は、3D ビューの手順と同じです。</p>
<p>要素が変位されると、モデル内の要素の位置はそのビューでのみ変更されます。これにより、モデルの分解ビューを作成してドキュメント化でき、プロジェクトの仕様や要素の関係を簡単に伝えることができるようになりました。</p>
<p class="asset-video"><iframe allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="400" src="https://www.youtube.com/embed/etd54FFSgEw?feature=oembed" width="712"></iframe></p>
<p><strong>複数の要素を選択してコントロールをアクティブ化する<br /></strong>複数の要素を選択した場合に、ピンや仮寸法などのキャンバス内コントロールは既定では表示されなくなりました。これにより、画面が煩雑にならず、ビュー内をナビゲートする際のパフォーマンスが向上します。</p>
<p><a class="asset-img-link" href="/assets/image_278627.jpg" style="display: inline;"><img alt="Revit2023-02-03" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02942fa500d5200c image-full img-responsive" src="/assets/image_278627.jpg" title="Revit2023-02-03" /></a></p>
<p><strong>ファミリ パラメータによる塗り潰し領域のパターンをコントロール</strong><br />ファミリ パラメータを使用して、塗り潰し領域のパターンをコントロールできます。ファミリがモデルにロードされたら、ファミリ パラメータの値を変更して、塗り潰し領域のパターンをコントロールします。</p>
<p class="asset-video"><iframe allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="400" src="https://www.youtube.com/embed/sspAFhrGoQk?feature=oembed" width="712"></iframe></p>
<p><strong>CAD ファイルのリンクと読み込みのサポート</strong><br />CAD ファイルのリンクと読み込みをサポートするために、複数の改善が行われました。</p>
<p class="asset-video"><iframe allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="400" src="https://www.youtube.com/embed/l4UKFEH3JwY?feature=oembed" width="712"></iframe></p>
<p style="padding-left: 40px;"><strong>リンク</strong><br />Revit モデルへのリンクがサポートされるファイルとして、次のファイル タイプが追加されました。</p>
<ul>
<li>
<ul>
<li>AXM (FormIt)</li>
<li>OBJ</li>
<li>STL</li>
</ul>
</li>
</ul>
<p style="padding-left: 40px;"><strong>ビューでの切り取りを有効化</strong><br />CAD ファイルには、CAD ファイル内の 3D 要素を切り取るためのインスタンス パラメータがあります。インスタンス パラメータが有効な場合は、ビューの切断面が 3D ジオメトリと交差すると、3D ジオメトリが切断面として表示されます。インスタンス パラメータが無効な場合は、切断面が CAD ファイルの 3D ジオメトリと交差していても、CAD ジオメトリはビューに投影として表示されます。</p>
<p style="padding-left: 40px;"><strong>CAD の読み込みの配置</strong><br />サポートされているすべての CAD ファイル タイプは、読み込むときにレベルまたは名前の付いた水平参照面に配置できます。読み込みのダイアログでの[配置先]ドロップダウンを使用して、CAD 読み込みの基準レベルを設定します。ホスト面またはレベルを削除した場合、読み込みの配置位置を維持することができます。</p>
<p><strong>追加のタグ付け可能なカテゴリ</strong><br />タグ付け可能なカテゴリのリストが拡張され、設計をより詳細にドキュメント化できるようになりました。<br />タグ付けできるカテゴリには、次のものがあります。</p>
<ul>
<li>意匠柱</li>
<li>点景</li>
<li>鼻隠し</li>
<li>樋</li>
<li>補助手摺</li>
<li>舗装</li>
<li>スロープ</li>
<li>軒裏</li>
<li>スラブ エッジ</li>
<li>上部手すり</li>
<li>壁の造作材</li>
<li>モデル グループ</li>
<li>RVT リンク</li>
</ul>
<p>By Ryuji Ogasawara</p>
