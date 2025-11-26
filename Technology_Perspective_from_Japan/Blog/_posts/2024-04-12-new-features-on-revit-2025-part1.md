---
layout: "post"
title: "Revit 2025 新機能 ～ その１"
date: "2024-04-12 01:36:56"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/04/new-features-on-revit-2025-part1.html "
typepad_basename: "new-features-on-revit-2025-part1"
typepad_status: "Publish"
---

<p>今回は、Autodesk Revit 2025 の専門分野に共通するコア機能に関連する新機能をご紹介いたします。</p>
<p><strong>新しい Revit ホーム: テクニカル プレビュー</strong></p>
<p>[新しい Revit ホーム]切り替えボタンを使用して、より多くの機能を備えた新しいホームページをお試しください。<br />新しい Revit ホーム ページでは、モデルやバージョンを表示、並べ替え、および検索できるようになりました。</p>
<ul>
<li>グリッド ビューとリスト ビューが使用可能<a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3affbeb200b-pi" style="display: inline;"><img alt="Revit2025_02_02a" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3affbeb200b image-full img-responsive" src="/assets/image_446842.jpg" title="Revit2025_02_02a" /></a></li>
<li>ピン固定されたプライマリ モデルは常に表示<a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3affbdf200b-pi" style="display: inline;"><img alt="Revit2025_02_02" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3affbdf200b image-full img-responsive" src="/assets/image_105756.jpg" title="Revit2025_02_02" /></a></li>
<li>検索機能<a class="asset-img-link" href="/assets/image_675821.jpg" style="display: inline;"><img alt="Revit2025-02-01" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3ac35ee200c image-full img-responsive" src="/assets/image_675821.jpg" title="Revit2025-02-01" /></a></li>
<li>更新日(すべて、今日、先週、先月、過去 3 ヵ月)とタイプ(ファミリとモデル)でフィルタ</li>
</ul>
<p>&#0160;</p>
<hr />
<p><strong>シート コレクション </strong></p>
<p>新しい<strong>シート コレクション</strong>を使用すると、モデル内の任意のシートを Revit 全体で参照できるフレキシブルなグループ(ビュー、集計表、フィルタなど)に割り当てることができます。</p>
<p>これまでも、プロジェクトブラウザのブラウザ構成の設定で、シートをグループ化・並べ替えすることはできましたが、あくまでもプロジェクトブラウザ上での表示に特化していました。</p>
<p>シート コレクションを作成するには、プロジェクト ブラウザ(シート)で、[新規シート コレクション]をクリックします。<br />すべてのシートには、モデル内のすべてのコレクションを一覧表示するシート コレクションのパラメータが含まれています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3affbf2200b-pi" style="display: inline;"><img alt="Revit2025_02_05" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3affbf2200b image-full img-responsive" src="/assets/image_247351.jpg" title="Revit2025_02_05" /></a></p>
<p>例えば、ビューフィルタでシートコレクションの値を利用することができます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3affbf8200b-pi" style="display: inline;"><img alt="Revit2025_02_07" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3affbf8200b image-full img-responsive" src="/assets/image_58214.jpg" title="Revit2025_02_07" /></a></p>
<hr />
<p><strong>STEP ファイルを読み込む/書き出す</strong></p>
<p>CAD ファイルに対するサポートが STEP ファイル タイプまで拡張しました。STEP ファイルの読み込みやリンク、または 3D ビューの STEP ファイルへの書き出しができます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3b04210200d-pi" style="display: inline;"><img alt="Revit2025_02_08" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3b04210200d image-full img-responsive" src="/assets/image_747205.jpg" title="Revit2025_02_08" /></a></p>
<hr />
<p><strong>複数の位置合わせ</strong></p>
<p>複数のキーノート、文字注記、タグを位置合わせして配置できるようになりました。</p>
<p>複数の要素を選択して、[修正]タブ-&gt;[位置合わせ]パネルをクリックし、位置合わせオプションを選択します。</p>
<p><a class="asset-img-link" href="/assets/image_720838.jpg" style="display: inline;"><img alt="Revit2025-02-09" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3affc04200b image-full img-responsive" src="/assets/image_720838.jpg" title="Revit2025-02-09" /></a></p>
<hr />
<p><strong>PDF へバックグラウンドで書き出す</strong></p>
<p>PDF 書き出しはバックグラウンドで実行できます。PDF 書き出し処理の実行中に、モデルでの作業を続行できます。</p>
<p>以前は、Revit から PDF を書き出す場合、作業を続行するには Revit セッションで PDF 書き出し処理を完了する必要がありました。今回の更新で PDF 書き出し処理をバックグラウンド処理として実行できるようになり、書き出し処理が完了するまでの間モデルでの作業を続行できます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3affc0a200b-pi" style="display: inline;"><img alt="Revit2025_02_010" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3affc0a200b img-responsive" src="/assets/image_318380.jpg" title="Revit2025_02_010" /></a></p>
<hr />
<p><strong>コーディネーション モデルの変更</strong></p>
<p>Revit 2024 で追加された新機能、「<a href="https://help.autodesk.com/view/RVT/2025/JPN/?guid=GUID-2D77DCD8-A4C7-48F7-995F-237693350747">コーディネーション モデルをリンク</a>」した後、[コーディネーション モデルの変更]機能を使用して、Autodesk Docs からリンクされたコーディネーション モデルの 2 つのバージョン間の変更をいつでも確認できるようになりました。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3affc17200b-pi" style="display: inline;"><img alt="Revit2025_02_011" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3affc17200b image-full img-responsive" src="/assets/image_615330.jpg" title="Revit2025_02_011" /></a></p>
<hr />
<p>By Ryuji Ogasawara</p>
<p>&#0160;</p>
