---
layout: "post"
title: "Model Derivative API：IFC >> SVF/SVF2 変換の遷移"
date: "2025-03-05 00:09:02"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2025/03/model-derivative-api-transition-on-ifc-svfsvf2-translation.html "
typepad_basename: "model-derivative-api-transition-on-ifc-svfsvf2-translation"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3cb9eef200c-pi" style="display: inline;"><img alt="Ifc_translation" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3cb9eef200c img-responsive" src="/assets/image_415692.jpg" title="Ifc_translation" /></a></p>
<p>BIM の着実な広まりとともに、中間ファイルとして IFC ファイルの流通も増えています。この流れから IFC ファイルを APS Viewer で表示するケースも増加傾向にあります。</p>
<p>言うまでもなく、APS Viewer に 3D モデルを表示するには、Model Derivative API で SVF または SVF2 に変換する必要があります。この際、advanced オプションが用意されている変換では、オプションの指定によって、変換される内容を変化させることが出来ます。</p>
<p>ここでは、 IFC &gt;&gt; SVF/SVF2 変換で、なぜ複数の advanced オプションが用意されているのか、なぜ変換結果が異なるのかについて、簡単な経緯に触れておきたいと思います。</p>
<p>もともと、オートデスク製品では、Navisworks と Revit の製品チーム毎に IFC ファイルの入力機能を独自実装しています。モデル コーディネーションの実現を主眼としている Navisworks には、早い段階から IFC の読み込み機能がサポートされていました。</p>
<p>Autodesk Forge 登場後、最初に Model Derivative API に組み込まれた IFC &gt;&gt; SVF 変換の実装は、この Navisworks 由来のものです。（当時は SVF2 はまだ存在していませんでした。）一方、BIM オーサリング ツールである Revit にも、IFC の読み込みが独自実装されるかたちで導入されています。</p>
<p>Navisworks と Revit では、メタデータの扱いも含め、製品の性格上、IFC 読み込みで重要視される優先項目が異なっています。このため、いずれかの製品で IFC ファイルを読み込むと、異なる状態になる結果が生じていました。逆に、Model Derivative API ででの変換時に、Navisworks、あるいは、Revit に IFC ファイルを読み込んだ際の挙動が求められる事態も起こりました。&#0160;</p>
<p>そこで導入されたのが、次のブログ記事でご案内したこともある&#0160;<strong>switchLoader</strong> advanced オプションです。このオプションを指定することで、Navisworks 由来の変換で SVF 生成するか、Revit 由来の変換で SVF 生成するかを切るかえられるようになりました。</p>
<ul>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2019/04/model-derivative-ifc-pipeline-call-change.html" rel="noopener" target="_blank">Model Derivative API での IFC ファイル変換の変更について</a></li>
</ul>
<p>Navisworks、Revit ともに、IFC 読み込みの実装は問題点の解決を含む微調整を経て変化してきています。ただ、 デスクトップ製品（Navisworks、Revit）の IFC 読み込み実装に由来していたため、デスクトップ製品側に施した改善を Model Derivative API の IFC &gt;&gt; SVF/SVF2 変換に反映するのに時間がかかる傾向がありました。Model Derivative API 用にパフォーマンス調整等が別途必要になるためです。</p>
<p>通常、Model Derivative API 変換では、ファイル形式毎に異なる変換に、共通した Autodesk Translation Framework（ATF）を使用しています。ただ、前述のとおり、IFC &gt;&gt; SVF/SVF2 変換は例外となっていました。</p>
<p>その後、新しい IFC 仕様をサポートしていく必要もあり、新たに新設された <strong>conversionMethod</strong> advanced オプションで IFC &gt;&gt; SVF/SVF2 変換の実装を切り替えられるようになっています。現在では、<strong>conversionMethod</strong> オプションに次の値を指定することが出来るようになっています。</p>
<ul>
<li>Navisworks エンジン：<strong>legacy</strong>（IFC 2x3 サポート）</li>
<li>Revit エンジン：<strong>modern</strong>（IFC 2x3、IFC 4 サポート）</li>
<li>更新 Revit エンジン：<strong>v3</strong>（IFC 2x3、IFC 4、IFC 4.3 サポート）</li>
<li>APS 専用 エンジン：<strong>v4</strong>（IFC 2x3、IFC 4、IFC 4.3 サポート）</li>
</ul>
<p>なお、この&#0160;<strong>conversionMethod</strong> advanced オプションでも Navisworks、Revit の変換を切り替えることが出来るようになったため、従来、切り替えのために用意されていた&#0160;<strong>switchLoader</strong> advanced オプションは廃止扱いになっています。</p>
<p>公式ドキュメントの <strong><a href="https://aps.autodesk.com/en/docs/model-derivative/v2/reference/http/jobs/job-POST/" rel="noopener" target="_blank">POST Create Translation Job</a></strong> エンドポイント リファレンスでは、IFC 入力の箇所に次のように記されています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3cc121a200c-pi" style="display: inline;"><img alt="Ifc_advanced_option" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3cc121a200c img-responsive" src="/assets/image_143779.jpg" title="Ifc_advanced_option" /></a></p>
<p>また、Navisworks のオンラインヘルプの&#0160;<strong><a href="https://help.autodesk.com/view/NAV/2025/JPN/?guid=WN_Autodesk_Translation_Framework_For_IFC_Import" rel="noopener" target="_blank">IFC インポート用 Autodesk Translation Framework</a></strong> では、次のように説明しています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3cc123f200c-pi" style="display: inline;"><img alt="Navisworks_ifc" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3cc123f200c image-full img-responsive" src="/assets/image_518700.jpg" title="Navisworks_ifc" /></a></p>
<p>今後、Model Derivative API の IFC &gt;&gt; SVF/SVF2 変換では、<strong>conversionMethod</strong> の&#0160;<strong>v4&#0160;</strong>にフォーカスして改良が盛り込まれていくことになります。APS Viewer での表示を前提する場合には、可能であれば、<strong>conversionMethod</strong> advanced オプションを <strong>v4</strong> に指定した <a href="https://adndevblog.typepad.com/technology_perspective/2020/11/svf-and-svf2.html" rel="noopener" target="_blank"><strong>SVF2</strong></a> の変換をお勧めします。</p>
<p>なお、<strong>v4</strong> 登場前の IFC 変換の詳細は、次の記事（英語）でもご紹介しています。</p>
<ul>
<li><a href="https://aps.autodesk.com/blog/faq-and-tips-ifc-translation-model-derivative-api" rel="noopener" target="_blank">FAQ and Tips for IFC translation of Model Derivative API</a></li>
</ul>
<p>By Toshiaki Isezaki</p>
