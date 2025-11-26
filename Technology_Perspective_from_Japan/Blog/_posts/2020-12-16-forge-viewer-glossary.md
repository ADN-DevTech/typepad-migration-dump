---
layout: "post"
title: "Forge Viewer 用語"
date: "2020-12-16 00:00:58"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2020/12/forge-viewer-glossary.html "
typepad_basename: "forge-viewer-glossary"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026be42d35b6200d-pi" style="float: left;"><img alt="2020-12-16_17-05-16" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026be42d35b6200d image-full img-responsive" src="/assets/image_494576.jpg" style="margin: 0px 5px 5px 0px;" title="2020-12-16_17-05-16" /></a>Forge Viewer を利用したカスタマイズで、よく使用される用語をまとめてみました。</p>
<p>リファレンス ドキュメントにも利用されているので、これから Forge Viewer を使ったカスタマイズをされるような場合は、ご一読いただくことをお勧めします。</p>
<p>短い動画で Forge Viewer を説明する&#0160; <strong><a href="https://adndevblog.typepad.com/technology_perspective/2020/05/forge-online-viewer-basics.html" rel="noopener" target="_blank">Forge Online - Viewer ソリューションの流れ</a></strong>&#0160;のブログ記事も併せてご確認ください。</p>
<table border="1" style="border-style: dotted; border-color: #4d91bf; float: left;">
<thead>
<tr>
<th style="width: 201.44px;">用語</th>
<th style="width: 559.2px;">内容</th>
</tr>
</thead>
<tbody>
<tr class="row-even">
<td style="width: 201.44px; text-align: center;">
<p><strong>access token</strong><br /><strong>（<a href="https://adndevblog.typepad.com/technology_perspective/2018/11/about-access-token.html" rel="noopener" target="_blank">アクセス トークン</a>）</strong></p>
</td>
<td style="width: 559.2px;">
<div class="line-block">
<div class="line-block">
<div class="line">
<p>アクセストークン(単に「トークン」または「ベアラートークン」と呼ばれることもあります)は、アプリケーションがAPIにアクセスするために使用するクレデンシャルです。<br />認証 API は、認証フローが正常に終了するとアクセストークンを返します。</p>
<p>Forge Viewer は、viewables:read のスコープを持つアクセストークンを必要とします。</p>
</div>
</div>
</div>
</td>
</tr>
<tr class="row-odd">
<td style="width: 201.44px; text-align: center;">
<p><strong>canvas</strong><br /><strong>（カンバス）</strong></p>
</td>
<td style="width: 559.2px;">
<div class="line-block">
<div class="line">Forge Viewer をレンダリングするためのターゲットとして使用される HTML 要素。</div>
</div>
</td>
</tr>
<tr class="row-even">
<td style="width: 201.44px; text-align: center;">
<p><strong>dbid</strong><br /><strong>（DB ID）</strong></p>
</td>
<td style="width: 559.2px;">
<div class="line-block">
<div class="line">プロパティデータベース内のメタデータにリンクするオブジェクトの ID（識別子）。<a href="https://adndevblog.typepad.com/technology_perspective/2019/12/forge-viewer-forge-viewer-event-and-2d-3d.html" rel="noopener" target="_blank">dbid</a> は、Forge Viewer のメソッドを呼び出すためにも必要です。例えば、モデル内のオブジェクトを非表示にする関数は、引数として dbid のリストを指定します。dbid は Model Derivative API での変換処理毎に変更されるため、同じオブジェクトでも常に一定になるわけではありません。</div>
</div>
</td>
</tr>
<tr class="row-odd">
<td style="width: 201.44px; text-align: center;">
<p><strong>document</strong><br /><strong>（ドキュメント）</strong></p>
</td>
<td style="width: 559.2px;">
<div class="line-block">
<div class="line">カンバスにロードして表示するモデルのファイルです。doc はルートへのアクセスを提供し、id で要素を見つける方法を提供します。詳細については、<a class="reference external" href="https://forge.autodesk.com/en/docs/viewer/v7/reference/Viewing/Document" rel="noopener" target="_blank">Document</a>&#0160;を参照してください。</div>
</div>
</td>
</tr>
<tr class="row-even">
<td style="width: 201.44px; text-align: center;">
<p><a href="https://adndevblog.typepad.com/technology_perspective/2019/12/forge-viewer-extension.html" rel="noopener" target="_blank"><strong>extension</strong></a><br /><strong>（エクステンション）</strong></p>
</td>
<td style="width: 559.2px;">
<div class="line-block">
<div class="line">Forge Viewer の動作を拡張または変更するために、実行時にオプションで読み込まれる JavaScript コード。いくつかの拡張機能は Forge Viewer に同梱されており、開発者は特定の使用状況に合わせて独自のエクステンションを記述することができます。</div>
</div>
</td>
</tr>
<tr class="row-odd">
<td style="width: 201.44px; text-align: center;">
<p><strong>fragID</strong><br /><strong>（フラグ ID）</strong></p>
</td>
<td style="width: 559.2px;">
<div class="line-block">
<div class="line">Forge Viewer モデルの個々のフラグメントの ID（識別子）。</div>
</div>
</td>
</tr>
<tr class="row-even">
<td style="width: 201.44px; text-align: center;">
<p><strong>fragment</strong><br /><strong>（フラグメント）</strong></p>
</td>
<td style="width: 559.2px;">
<div class="line-block">
<div class="line">特定の形状とマテリアル（材質）を持つオブジェクトの一部（three.js ポリゴン）。例えば、ドア オブジェクトは、ドアノブ（真鍮素材）とドアフレーム（木製素材）で構成されている場合があります。</div>
<div class="line">&#0160;</div>
</div>
</td>
</tr>
<tr class="row-odd">
<td style="width: 201.44px; text-align: center;">
<p><strong>geometry node</strong><br /><strong>（ジオメトリ ノード）</strong></p>
</td>
<td style="width: 559.2px;">
<div class="line-block">
<div class="line">Forge Viewer でモデルの最小の選択可能な要素（例：ドア）。これはオブジェクトとも呼ばれます。</div>
</div>
</td>
</tr>
<tr class="row-even">
<td style="width: 201.44px; text-align: center;">
<p><strong>ghosting</strong><br /><strong>（ゴースト）</strong></p>
</td>
<td style="width: 559.2px;">
<div class="line-block">
<div class="line">モデル内の選択されたノードを透過させ、他のノードが目立つようにする機能。</div>
</div>
</td>
</tr>
<tr class="row-odd">
<td style="width: 201.44px; text-align: center;">
<p><strong>headless viewer</strong><br /><strong>（<a href="https://adndevblog.typepad.com/technology_perspective/2019/12/forge-viewer-forge-viewer-simple-scene-customize.html" rel="noopener" target="_blank">ヘッドレス ビューア</a>）</strong></p>
</td>
<td style="width: 559.2px;">
<div class="line-block">
<div class="line">&#0160;3D レンダリング カンバスのみのビューア。</div>
</div>
</td>
</tr>
<tr class="row-even">
<td style="width: 201.44px; text-align: center;">
<p><strong>highlighting</strong><br /><strong>（ハイライト）</strong></p>
</td>
<td style="width: 559.2px;">
<div class="line-block">
<div class="line">オブジェクトを目立たせるために描画状態を変更すること。Forge Viewer は、オブジェクトが選択されているときと、ユーザがオブジェクトの上にマウスを置いたときに、オブジェクトを強調表示します。テーマ付けは、Forge アプリで利用可能なオブジェクトを強調表示するもう一つの方法です。</div>
</div>
</td>
</tr>
<tr>
<td style="width: 201.44px; text-align: center;">
<p><strong>isolate</strong><br /><strong>（選択表示）</strong></p>
</td>
<td style="width: 559.2px;">
<div class="line-block">
<div class="line">周囲のオブジェクトを非表示、あるいは、ゴースト状態にして、指定したオブジェクトを単独表示する方法です。</div>
</div>
</td>
</tr>
<tr class="row-odd">
<td style="width: 201.44px; text-align: center;">
<p><strong>markup</strong><br /><strong>（マークアップ）</strong></p>
</td>
<td style="width: 559.2px;">
<div class="line-block">
<div class="line">カンバス内に記入する注釈。ビューアブルを注釈するためのエクステンションを指す場合もあります。</div>
</div>
</td>
</tr>
<tr class="row-even">
<td style="width: 201.44px; text-align: center;">
<p><strong>manifest</strong><br /><strong>（マニフェスト）</strong></p>
</td>
<td style="width: 559.2px;">
<div class="line-block">
<div class="line">
<p>変換プロセスの結果であり、Forge Viewer で使用されるリソース（モデル ジオメトリ、サムネイル、カメラ ビューなど）の構造化されたコレクションです。</p>
<p>GET :urn/manifest エンドポイントを使用してマニフェストを取得します。</p>
<p>Forge Viewer はビューアブルのみをレンダリングできます。</p>
</div>
</div>
</td>
</tr>
<tr class="row-odd">
<td style="width: 201.44px; text-align: center;">
<p><strong>measure</strong><br /><strong>（メジャー、計測）</strong></p>
</td>
<td style="width: 559.2px;">
<div class="line-block">
<div class="line">モデルのサーフェス上の 2 点間の距離を計測する機能、または計測機能を実装するエクステンション。</div>
</div>
</td>
</tr>
<tr class="row-even">
<td style="width: 201.44px; text-align: center;">
<p><strong>orbit</strong><br /><strong>（オービット）</strong></p>
</td>
<td style="width: 559.2px;">
<div class="line-block">
<div class="line">カメラ（視点）を上下左右に移動できる機能。</div>
</div>
</td>
</tr>
<tr class="row-odd">
<td style="width: 201.44px; text-align: center;">
<p><strong>overlay</strong><br /><strong>（オーバーレイ）</strong></p>
</td>
<td style="width: 559.2px;">
<div class="line-block">
<div class="line">ロードしたモデルにカスタム ジオメトリを追加する機能。</div>
</div>
</td>
</tr>
<tr class="row-even">
<td style="width: 201.44px; text-align: center;">
<p><strong>profile</strong><br /><strong>（プロファイル）</strong></p>
</td>
<td style="width: 559.2px;">
<div class="line-block">
<div class="line">Forge Viewer の環境設定とエクステンションを記録した設定コレクション。開発者は独自のカスタム プロファイルを作成することができます。また、Navisworks や Revit などのオートデスクデスクトップ製品と同じユーザ エクスペリエンスを実現するために、Forge Viewer には組み込みプロファイルが用意されています。</div>
</div>
</td>
</tr>
<tr class="row-odd">
<td style="width: 201.44px; text-align: center;">
<p><strong>property database</strong><br /><strong>（プロパティ データベース）</strong></p>
</td>
<td style="width: 559.2px;">
<div class="line-block">
<div class="line">ビューアブルとそのパーツに関連付けられたメタデータ。プロパティデータベースには、各ジオメトリノードのメタデータが含まれています。</div>
</div>
</td>
</tr>
<tr class="row-even">
<td style="width: 201.44px; text-align: center;">
<p><strong>scene</strong><br /><strong>（シーン）</strong></p>
</td>
<td style="width: 559.2px;">
<div class="line-block">
<div class="line">1 つ以上のモデルが配置された環境。モデル、カメラ、ライトは、環境の座標系内に配置したり、変形させたりすることができる。</div>
</div>
</td>
</tr>
<tr class="row-odd">
<td style="width: 201.44px; text-align: center;">
<p><strong>search</strong><br /><strong>（検索）</strong></p>
</td>
<td style="width: 559.2px;">
<div class="line-block">
<div class="line">ビューアブルのメタデータを検索する機能。<br />プロパティデータベースと呼ばれる。</div>
</div>
</td>
</tr>
<tr class="row-even">
<td style="width: 201.44px; text-align: center;">
<p><strong>section(ing)</strong><br /><strong>（断面解析）</strong></p>
</td>
<td style="width: 559.2px;">
<div class="line-block">
<div class="line">3D モデルを切断トして中を見るための機能、または、エクステンション。</div>
</div>
</td>
</tr>
<tr class="row-odd">
<td style="width: 201.44px; text-align: center;">
<p><strong>seed file</strong><br /><strong>（シードファイル）</strong></p>
</td>
<td style="width: 559.2px;">
<div class="line-block">
<div class="line">Forge Viewer で表示する変換前のデザイン ファイル。</div>
</div>
</td>
</tr>
<tr class="row-even">
<td style="width: 201.44px; text-align: center;">
<p><strong>selection</strong><br /><strong>（選択）</strong></p>
</td>
<td style="width: 559.2px;">
<div class="line-block">
<div class="line">モデル内のノードをユーザが選択できる機能。選択を使用して、ユーザが選択できるノードを指定したり、選択したノードを追跡したりすることができます。</div>
</div>
</td>
</tr>
<tr class="row-odd">
<td style="width: 201.44px; text-align: center;">
<p><strong>theming</strong><br /><strong>（テーマ）</strong></p>
</td>
<td style="width: 559.2px;">
<div class="line-block">
<div class="line">色を変えてオブジェクトを強調する手段。</div>
</div>
</td>
</tr>
<tr class="row-even">
<td style="width: 201.44px; text-align: center;">
<p><strong>viewable</strong><br /><strong>（ビューアブル）</strong></p>
</td>
<td style="width: 559.2px;">
<div class="line-block">
<div class="line">マニフェストで参照される3Dモデルまたは2Dシート。</div>
</div>
</td>
</tr>
<tr class="row-odd">
<td style="width: 201.44px; text-align: center;">
<p><strong>viewcube</strong><br /><strong>（ビューキューブ）</strong></p>
</td>
<td style="width: 559.2px;">
<div class="line-block">
<div class="line"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdeab153e200c-pi" style="float: right;"><img alt="Viewcube" class="asset  asset-image at-xid-6a0167607c2431970b026bdeab153e200c img-responsive" src="/assets/image_107531.jpg" style="width: 50px; margin: 0px 0px 5px 5px;" title="Viewcube" /></a>既定でカンバスの右上に表示される正方体で、3D モデルの表示中に使用できるツール。カメラの位置を方向付けるのに役立ちます。</div>
</div>
</td>
</tr>
<tr class="row-even">
<td style="width: 201.44px; text-align: center;">
<p><a href="https://adndevblog.typepad.com/technology_perspective/2017/12/state-api-in-forge-viewer.html" rel="noopener" target="_blank"><strong>viewer state</strong></a><br /><strong>（ビューア状態）</strong></p>
</td>
<td style="width: 559.2px;">
<div class="line-block">
<div class="line">カメラシステムの視点に基づいてモデルの現在のビューを表示します。</div>
</div>
</td>
</tr>
</tbody>
</table>
<p>By Toshiaki Isezaki</p>
