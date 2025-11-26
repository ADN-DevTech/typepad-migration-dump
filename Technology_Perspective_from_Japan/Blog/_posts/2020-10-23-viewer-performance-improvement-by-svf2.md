---
layout: "post"
title: "Public Beta：SVF2 による Forge Viewer パフォーマンスの向上"
date: "2020-10-23 00:14:10"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2020/10/viewer-performance-improvement-by-svf2.html "
typepad_basename: "viewer-performance-improvement-by-svf2"
typepad_status: "Publish"
---

<div class="node__content adskf__section-group">
<div class="node__body">
<div class="field-body">
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bde9da5e6200c-pi" style="display: inline;"><img alt="Svf2_blog_0" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bde9da5e6200c image-full img-responsive" src="/assets/image_846584.jpg" title="Svf2_blog_0" /></a></p>
<p>昨年の Autodesk University 2019 Las Vegasでは、コードネーム「OTG」と呼ばれる新しい Forge Viewer フォーマットについてご案内しました。このフォーマットは BIM 360 サービスで導入されたもので、Forge DevCon 2019&#0160; Las Vegas の基調講演でも、この取り組みについての短く <a href="https://youtu.be/c8-AxaoHDlk?t=1147" rel="noopener" target="_blank"><strong>こちら</strong></a> で言及しています。</p>
<p>今回、この新しいフォーマットを多くの方に評価いただけるよう、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2020/09/svf2-private-beta-tester.html" rel="noopener" target="_blank">Private Beta</a></strong> から Public Beta フェーズに移行することになりました。ベータ版であることを念頭に置いていただいた上でご評価いただき、<strong><a href="https://forge.autodesk.com/en/support/get-help" rel="noopener" target="_blank">Forge-Help</a></strong> までフィードバックをお寄せください。パフォーマンスが大幅に向上したモデルやそうでないモデルの例をお持ちの方は、ぜひ、ご意見をお聞かせください。</p>
<p>ご評価いただける場合には、Forge Viewer でジオメトリの詳細をご確認ください。Web ブラウザのデバッグコンソールを開き、ジオメトリサイズなどを把握いただくことが出来ます。下記は、Google Chrome で F12 キーを使ってコンソール出力を開いた例です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bde9da5fe200c-pi" style="display: inline;"><img alt="01" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bde9da5fe200c image-full img-responsive" src="/assets/image_327376.jpg" title="01" /></a></p>
<p>現在、SVF2 では、可能な場合、複数の Viewable 間でも同じ形状を持つ要素のメッシュを共有するようになっています。この最適化により、SVF2 フォーマットは Viewable のストレージ サイズを大幅に低減して、表示と読み込みのパフォーマンスを高速化しています。ただし、SVF2 への変換には時間がかかるのでご注意ください。</p>
<p>SVF2 のサポート用に、次の Model Derivative API の endpoint が更新されています。</p>
<ul>
<li>GET&#0160;&#0160; &#0160;&#0160; https://developer.api.autodesk.com/modelderivative/v2/designdata/formats</li>
<li>POST&#0160;&#0160; &#0160;https://developer.api.autodesk.com/modelderivative/v2/designdata/job</li>
<li>GET&#0160;&#0160; &#0160;&#0160; https://developer.api.autodesk.com/modelderivative/v2/designdata/:urn/manifest<br />- または -</li>
<li>GET&#0160;&#0160; &#0160;&#0160; https://developer.api.autodesk.com/modelderivative/v2/regions/eu/designdata/:urn/manifest</li>
</ul>
<p>また、Viewer で SVF2 が読み込まれているかどうか確認する Forge Viewer API、<strong><a href="https://forge.autodesk.com/en/docs/viewer/v7/reference/Viewing/Model/#issvf2" rel="noopener" target="_blank">Viewing.Model.isSVF2()</a></strong> が用意されています。なお、Public Beta 期間中は SVF2 フォーマットの生成は無料です。</p>
<p>SVF2 の利用方法を説明した最新のドキュメント（英語）も、次の URL からご覧いただけます。</p>
<ul>
<li>はじめに：<a href="https://forge.autodesk.com/en/docs/model-derivative/v2/developers_guide/notes/" rel="noopener" target="_blank">https://forge.autodesk.com/en/docs/model-derivative/v2/developers_guide/notes/</a></li>
<li>重要な情報：<a href="https://forge.autodesk.com/en/docs/model-derivative/v2/developers_guide/basics/preperation/" rel="noopener" target="_blank">https://forge.autodesk.com/en/docs/model-derivative/v2/developers_guide/basics/preperation/</a></li>
<li>Field Guide：<a href="https://forge.autodesk.com/en/docs/model-derivative/v2/developers_guide/field-guide/" rel="noopener" target="_blank">https://forge.autodesk.com/en/docs/model-derivative/v2/developers_guide/field-guide/</a></li>
<li>GET :urn/manifest：<a href="https://forge.autodesk.com/en/docs/model-derivative/v2/reference/http/urn-manifest-GET/" rel="noopener" target="_blank">https://forge.autodesk.com/en/docs/model-derivative/v2/reference/http/urn-manifest-GET/</a></li>
</ul>
<p>SVF2 は従来の SVF フォーマットの拡張なので、変換には Job リクエスト作成時の出力フォーマットのタイプを &#39;SVF&#39; から &#39;SVF2&#39; に変更するだけです。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026be41c84f2200d-pi" style="display: inline;"><img alt="Svf2_01_0" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026be41c84f2200d image-full img-responsive" src="/assets/image_355334.jpg" title="Svf2_01_0" /></a></p>
<p>Beta 期間中に新しい SVF2 フォーマットを表示するには、V既存の iewer コードを少し変更する必要があります。まず、Forge Viewer ライブラリの 7.25 以降を実行していることをご確認ください。次に、Viewer の初期化オプションを次のように変更してください（ env と api パラメータを指定）。</p>
<ul>
<li>env: <strong>MD20ProdUS</strong> (US) または <strong>MD20ProdEU</strong> (EMEA)</li>
<li>api: <strong>D3S</strong></li>
</ul>
<p>最後に、パフォーマンス向上をご理解いただけるいくつかのテスト結果をご案内したいと思います。左がSVF2、右がSVFです。</p>
<table border="1" class="Table">
<tbody>
<tr style="height: 45px;">
<td nowrap="nowrap" style="width: 189.28px; height: 45px;" valign="bottom">
<p><strong>モデル</strong></p>
</td>
<td colspan="2" nowrap="nowrap" style="text-align: center; width: 210.4px; height: 45px;" valign="bottom">
<p>asm.zip</p>
</td>
<td rowspan="9" style="width: 357.6px; height: 423px;" valign="top">
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e9705297200b-pi" style="display: inline;"><img alt="Ex1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e9705297200b img-responsive" src="/assets/image_873158.jpg" title="Ex1" /></a></p>
</td>
</tr>
<tr style="height: 45px;">
<td nowrap="nowrap" style="width: 189.28px; height: 45px;" valign="bottom">
<p><strong>モデル ファイル サイズ</strong></p>
</td>
<td colspan="2" nowrap="nowrap" style="text-align: center; width: 210.4px; height: 45px;" valign="bottom">
<p>62.8 MB</p>
</td>
</tr>
<tr style="height: 45px;">
<td nowrap="nowrap" style="width: 189.28px; height: 45px;" valign="bottom">
<p><strong>変換タイプ</strong></p>
</td>
<td nowrap="nowrap" style="text-align: center; width: 88.8px; height: 45px;" valign="bottom">
<p>SVF2</p>
</td>
<td nowrap="nowrap" style="text-align: center; width: 115.68px; height: 45px;" valign="bottom">
<p>SVF</p>
</td>
</tr>
<tr style="height: 45px;">
<td nowrap="nowrap" style="width: 189.28px; height: 45px;" valign="bottom">
<p><strong>変換時間</strong></p>
</td>
<td nowrap="nowrap" style="text-align: center; width: 88.8px; height: 45px;" valign="bottom">
<p>83.04 秒</p>
</td>
<td nowrap="nowrap" style="text-align: center; width: 115.68px; height: 45px;" valign="bottom">
<p>53.97 秒</p>
</td>
</tr>
<tr style="height: 45px;">
<td nowrap="nowrap" style="width: 189.28px; height: 45px;" valign="bottom">
<p><strong>Viewable のロード時間</strong></p>
</td>
<td nowrap="nowrap" style="text-align: center; width: 88.8px; height: 45px;" valign="bottom">
<p>0.77 秒</p>
</td>
<td nowrap="nowrap" style="text-align: center; width: 115.68px; height: 45px;" valign="bottom">
<p>1.16 秒</p>
</td>
</tr>
<tr style="height: 45px;">
<td nowrap="nowrap" style="width: 189.28px; height: 45px;" valign="bottom">
<p><strong>合計ジオメトリ サイズ</strong></p>
</td>
<td nowrap="nowrap" style="text-align: center; width: 88.8px; height: 45px;" valign="bottom">
<p>24.084 MB</p>
</td>
<td nowrap="nowrap" style="text-align: center; width: 115.68px; height: 45px;" valign="bottom">
<p>34.559 MB</p>
</td>
</tr>
<tr style="height: 45px;">
<td nowrap="nowrap" style="width: 189.28px; height: 45px;" valign="bottom">
<p><strong>メッシュ数</strong></p>
</td>
<td nowrap="nowrap" style="text-align: center; width: 88.8px; height: 45px;" valign="bottom">
<p style="text-align: center;">307</p>
</td>
<td nowrap="nowrap" style="text-align: center; width: 115.68px; height: 45px;" valign="bottom">
<p style="text-align: center;">549</p>
</td>
</tr>
<tr style="height: 45px;">
<td nowrap="nowrap" style="width: 189.28px; height: 45px;" valign="bottom">
<p><strong>GPU 上のメッシュ数</strong></p>
</td>
<td nowrap="nowrap" style="text-align: center; width: 88.8px; height: 45px;" valign="bottom">
<p style="text-align: center;">307</p>
</td>
<td nowrap="nowrap" style="text-align: center; width: 115.68px; height: 45px;" valign="bottom">
<p style="text-align: center;">549</p>
</td>
</tr>
<tr style="height: 63px;">
<td nowrap="nowrap" style="width: 189.28px; height: 63px;" valign="bottom">
<p><strong>GPU 上の</strong></p>
<p><strong>ジオメトリ消費メモリ</strong></p>
</td>
<td nowrap="nowrap" style="text-align: center; width: 88.8px; height: 63px;" valign="bottom">
<p style="text-align: center;">25150610</p>
</td>
<td nowrap="nowrap" style="text-align: center; width: 115.68px; height: 63px;" valign="bottom">
<p style="text-align: center;">36053286</p>
</td>
</tr>
</tbody>
</table>
<p>&#0160;</p>
<table border="1" class="Table" style="width: 99.3485%; margin-left: auto; margin-right: auto;">
<tbody>
<tr>
<td nowrap="nowrap" style="width: 24.8092%;" valign="bottom">
<p><strong>モデル</strong></p>
</td>
<td colspan="2" nowrap="nowrap" style="height: 45px; width: 28.117%; text-align: center;" valign="bottom">
<p>.rvt</p>
</td>
<td rowspan="9" style="height: 405px; width: 45.6743%;" valign="top">
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bde9daa14200c-pi" style="display: inline;"><img alt="Ex2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bde9daa14200c img-responsive" src="/assets/image_800167.jpg" title="Ex2" /></a></p>
</td>
</tr>
<tr>
<td nowrap="nowrap" style="width: 24.8092%;" valign="bottom">
<p><strong>モデル ファイル サイズ</strong></p>
</td>
<td colspan="2" nowrap="nowrap" style="height: 45px; width: 28.117%; text-align: center;" valign="bottom">
<p>92.8 MB</p>
</td>
</tr>
<tr>
<td nowrap="nowrap" style="width: 24.8092%;" valign="bottom">
<p><strong>変換タイプ</strong></p>
</td>
<td nowrap="nowrap" style="height: 45px; width: 12.341%; text-align: center;" valign="bottom">
<p>SVF2</p>
</td>
<td nowrap="nowrap" style="height: 45px; width: 15.7761%; text-align: center;" valign="bottom">
<p>SVF</p>
</td>
</tr>
<tr>
<td nowrap="nowrap" style="width: 24.8092%;" valign="bottom">
<p><strong>変換時間</strong></p>
</td>
<td nowrap="nowrap" style="height: 45px; width: 12.341%; text-align: center;" valign="bottom">
<p>488.49 秒</p>
</td>
<td nowrap="nowrap" style="height: 45px; width: 15.7761%; text-align: center;" valign="bottom">
<p>385.35 秒</p>
</td>
</tr>
<tr>
<td nowrap="nowrap" style="width: 24.8092%;" valign="bottom">
<p><strong>Viewable のロード時間</strong></p>
</td>
<td nowrap="nowrap" style="height: 45px; width: 12.341%; text-align: center;" valign="bottom">
<p>0.60 秒</p>
</td>
<td nowrap="nowrap" style="height: 45px; width: 15.7761%; text-align: center;" valign="bottom">
<p>1.51 秒</p>
</td>
</tr>
<tr>
<td nowrap="nowrap" style="width: 24.8092%;" valign="bottom">
<p><strong>合計ジオメトリ サイズ</strong></p>
</td>
<td nowrap="nowrap" style="height: 45px; width: 12.341%; text-align: center;" valign="bottom">
<p>22.589 MB</p>
</td>
<td nowrap="nowrap" style="height: 45px; width: 15.7761%; text-align: center;" valign="bottom">
<p>166.515 MB</p>
</td>
</tr>
<tr>
<td nowrap="nowrap" style="width: 24.8092%;" valign="bottom">
<p><strong>メッシュ数</strong></p>
</td>
<td nowrap="nowrap" style="height: 45px; width: 12.341%; text-align: center;" valign="bottom">
<p style="text-align: center;">3362</p>
</td>
<td nowrap="nowrap" style="height: 45px; width: 15.7761%; text-align: center;" valign="bottom">
<p style="text-align: center;">29646</p>
</td>
</tr>
<tr>
<td nowrap="nowrap" style="width: 24.8092%;" valign="bottom">
<p><strong>GPU 上のメッシュ数</strong></p>
</td>
<td nowrap="nowrap" style="height: 45px; width: 12.341%; text-align: center;" valign="bottom">
<p style="text-align: center;">3362</p>
</td>
<td nowrap="nowrap" style="height: 45px; width: 15.7761%; text-align: center;" valign="bottom">
<p style="text-align: center;">10000</p>
</td>
</tr>
<tr>
<td nowrap="nowrap" style="width: 24.8092%;" valign="bottom">
<p><strong>GPU 上の</strong></p>
<p><strong>ジオメトリ消費メモリ</strong></p>
</td>
<td nowrap="nowrap" style="height: 45px; width: 12.341%; text-align: center;" valign="bottom">
<p style="text-align: center;">22556672</p>
</td>
<td nowrap="nowrap" style="height: 45px; width: 15.7761%; text-align: center;" valign="bottom">
<p style="text-align: center;">101428240</p>
</td>
</tr>
</tbody>
</table>
</div>
</div>
</div>
<p>By Toshiaki Isezaki</p>
