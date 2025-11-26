---
layout: "post"
title: "Forge Viewer：Data Visualization エクステンション"
date: "2021-01-13 00:00:44"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/01/forge-viewerdata-visualization-extension.html "
typepad_basename: "forge-viewerdata-visualization-extension"
typepad_status: "Publish"
---

<p><a href="https://adndevblog.typepad.com/technology_perspective/2015/08/about-internet-of-things.html" rel="noopener" target="_blank"><strong>IoT（Internet Of Things）</strong></a>技術の定着とともに、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2019/08/degital-twin.html" rel="noopener" target="_blank">デジタルツインやスマート ビルディング</a></strong>といった形で日本でもセンサー情報の利用が本格化しています。</p>
<p>オートデスクは 2015 年にクラウド ベースの IoT プラットフォームを提供していた SeeControl を<strong><a href="https://adndevblog.typepad.com/technology_perspective/2015/08/about-see-control.html" rel="noopener noreferrer" target="_blank">買収</a></strong>して、その後の <a href="https://knowledge.autodesk.com/search-result/caas/CloudHelp/cloudhelp/ENU/FUSIONCONNECT-Help/What-Is-Fusion-Connect-html.html" rel="noopener" target="_blank"><strong>Fusion Connect</strong></a> に流用しています。2017 年の DevCon では、Forge IoT として API 化のロードマップを<strong><a href="https://adndevblog.typepad.com/technology_perspective/2017/11/devcon-2017-forge-roadmap.html" rel="noopener noreferrer" target="_blank">発表</a></strong>していましたが、このクラウド バックエンド&#0160; サービス提供の計画は、残念ながら、白紙撤回されています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026be4310fae200d-pi" style="display: inline;"><img alt="Past_iot_strategy" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026be4310fae200d image-full img-responsive" src="/assets/image_316973.jpg" title="Past_iot_strategy" /></a></p>
<p>このため、今まで、IoT に関して Forge 側で一貫したインターフェースや手法を用意していたわけではありませんでした。</p>
<p>Forge Viewer 上へのセンサーデータやセンサー位置の可視化には、Forge Viewer のベースになっている <strong><a href="https://threejs.org/" rel="noopener" target="_blank">Three.js</a></strong>&#0160; JavaScript ライブラリ を活用して、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2020/12/forge-viewer-overlay-and-scene-builder.html" rel="noopener" target="_blank">Forge Viewer：オーバーレイとシーン ビルダー</a></strong> や <strong><a href="https://adndevblog.typepad.com/technology_perspective/2020/12/forge-viewer-markup-along-dbid.html" rel="noopener" target="_blank">Forge Viewer：dbid に沿ったマークアップの表示</a></strong> でご紹介した方法等で表現する必要があります。また、Forge アプリとセンサー デバイスとのコミュニケーションには、独自に Socket IO 等でデバイスとの接続確立も必要になってきます。</p>
<p><a class="asset-img-link" href="https://forge-rcdb.autodesk.io/configurator?id=58adee163e6f342cf1e92dae" rel="noopener" style="display: inline;" target="_blank"><img alt="Ioy_sample" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026be430dbea200d image-full img-responsive" src="/assets/image_901110.jpg" title="Ioy_sample" /></a></p>
<p>さて、今年の Autodesk University 2020 では、IoT に対するオートデスクの戦略の変更が反映されたものとなりました。</p>
<div class="ellip"><a href="https://adsknews.autodesk.com/pressrelease/tandem-brings-digitial-twin-to-bim" rel="noopener" target="_blank"><strong>Autodesk Tandem™</strong></a> サービスや Forge Viewer 用の <strong>Data Visualization エクステンション</strong>の発表です。後者は、<a href="https://www.autodesk.com/autodesk-university/ja/class/Forge-rotomatsufuVisual-Insights-moteruneinotetanoshijuehuaForge-Viewer" rel="noopener" target="_blank"><strong>SD471531 </strong></a><a href="https://www.autodesk.com/autodesk-university/ja/class/Forge-rotomatsufuVisual-Insights-moteruneinotetanoshijuehuaForge-Viewer" rel="noopener" target="_blank"><strong>Forge ロードマップ：Visual Insights-モデル内のデータの視覚化</strong></a> でご案内したとおりです。</div>
<div class="ellip">&#0160;</div>
<div class="ellip">オートデスクは、センサーデバイスの接続、管理を担うバックエンド クラウド システムの構築とサービス・API の提供ではなく、主に Web ブラウザ上でのセンサー情報の可視化に特化したソリューション提供に舵を切っています。バックエンド システムには Azure や AWS が既に公開しているものを使っていただき、センサーデータのみをそこから取得、可視化しようとするものです。</div>
<div class="ellip">&#0160;</div>
<div class="ellip">また、Forge Viewer 上の可視化に際しても、利用するモデルを BIM モデルに限定し、デジタルツインの実現にフォーカスします。</div>
<div class="ellip">&#0160;</div>
<div class="ellip">Data Visualization エクステンションを利用したサンプルは、<a href="https://hyperion.autodesk.io/" rel="noopener" target="_blank"><strong>https://hyperion.autodesk.io</strong></a> が公開されています。</div>
<p><a class="asset-img-link" href="https://hyperion.autodesk.io/" rel="noopener" style="display: inline;" target="_blank"><img alt="Data_visualization" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e9847c4c200b image-full img-responsive" src="/assets/image_107435.jpg" title="Data_visualization" /></a></p>
<p>また、同エクステンションのドキュメントも、一部、<a href="https://forge.autodesk.com/en/docs/viewer/v7/reference/Extensions/DataVisualization/" rel="noopener" target="_blank">https://forge.autodesk.com/en/docs/viewer/v7/reference/Extensions/DataVisualization/</a> で公開が始まっています。今後、先のサンプルコードやバックエンド システムとの接続に関するドキュメント公開も予定されています。なお、現在の内容は、今後、変更されていく可能性があります。</p>
<p>現時点の Data Visualization エクステンションでは、次の機能を提供しようとしています。</p>
<ul>
<li>センサードット</li>
<li><a href="https://ja.wikipedia.org/wiki/%E3%83%92%E3%83%BC%E3%83%88%E3%83%9E%E3%83%83%E3%83%97" rel="noopener" target="_blank">ヒートマップ</a></li>
<li><a href="https://azure.microsoft.com/ja-jp/services/time-series-insights/" rel="noopener" target="_blank">タイムライン</a></li>
</ul>
<p>Data Visualization エクステンションを利用することで、 時間のかかる Three.js や HTML 上のコツ習得やノウハウ蓄積を必要とすることなしに、高度な実装を得ることが出来るようになるはずです。</p>
<p>情報が更新されましたら、随時、このブログでご案内していく予定です。</p>
<p>By Toshiaki Isezaki</p>
