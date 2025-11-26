---
layout: "post"
title: "Autodesk Flow のグラフ エンジン サービス"
date: "2024-05-15 00:42:49"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/05/autodesk-flow-graph-rngine-service.html "
typepad_basename: "autodesk-flow-graph-rngine-service"
typepad_status: "Publish"
---

<div class="field field--name-field-primary-image field--type-image field--label-hidden field__item"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02dad0c32544200d-pi" style="display: inline;"><img alt="Graph" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02dad0c32544200d image-full img-responsive" src="/assets/image_357912.jpg" title="Graph" /></a></div>
<div class="clearfix text-formatted field field--name-field-body field--type-text-with-summary field--label-hidden field__item">
<p>オートデスク プラットフォーム ビジョンでご覧になった方もいらっしゃると思いますが、中核となるインダストリー クラウドには、建設業向けの Autodesk Forma、製造業向けの Autodesk Fusion、そして今回、メディア・エンターテインメント行向けの Autodesk Flow に Flow Graph Engine サービス導入されました。</p>
<p>Flow Graph Engine サービスを使用すると、Bifrost グラフをクラウドで評価出来るようになるため、以前はローカルでしか実行できなかった時間のかかる演算処理を軽減することが可能です。Autodesk Maya には、将来的に Maya 内部から直接、同機能へのアクセスを提供する予定ですが、開発者は、業界標準の REST API を使用して、パイプラインから直接 Bifrost グラフを自動化および評価出来るようになっています。</p>
<p>なお、本機能は Public Beta の扱いになるため、API ドキュメントには <a href="https://aps.autodesk.com/developer/overview/flow-graph-engine-api" rel="noopener" target="_blank">Flow Graph Engine API</a>&#0160;リンクから直接アクセスする必要があります。ドキュメントには、サービスの基本を示す&#0160;<a  _istranslated="1" href="https://github.com/autodesk-platform-services/aps-flowgraphengine-bifrost-js-sample" rel="noopener" target="_blank">コード サンプル</a>も含まれています。フィードバックやご意見は、こちらの <a  _istranslated="1" href="https://feedback.autodesk.com/key/flowgraphengine" rel="noopener" target="_blank">Beta ポータル </a>からお寄せください。</p>
<p>一般的な考え方は、Bifrost グラフを取得してクラウドで評価するというものです。機能を詳しく見てみましょう。まず、Bifrost グラフが JSON ファイル内に含まれています。グラフを評価するには、入力にジオメトリやボリュームデータなどを含める必要があります。このコード サンプルでは、入力シーンのジオメトリを読み込むために USD 形式を使用していますが、グラフの要件に応じて、Bifrost でサポートされている任意の形式を使用することもできます。また、出力を指定して演算結果をダウンロードします。</p>
<p>サービスを将来にわたって拡張するために、「エグゼキューター」の概念があることがわかります。APS Design Automation サービスに精通している場合、これは &quot;エンジン&quot; の概念に似ています。現在、Flow グラフ エンジンは Bifrost の「エグゼキューター」のみをサポートしていますが、作業を開始する際には、他のエグゼキュータがサービスの対象として検討されている点もご承知いただければと思います。これは、オートデスクが皆様からのフィードバックを得たいと考えている分野の１つです。（他にどんなM&amp;Eの「エグゼキューター」の利用が得策なのか？例えば、リトポロジーツールに取り組んでいますが、これも通常、非常に計算負荷の高いツールです。このスタイルのサービスに意味がありますか？繰り返しになりますが、<a  _istranslated="1" href="https://feedback.autodesk.com/key/flowgraphengine" rel="noopener" target="_blank">Beta ポータル</a> からフィードバックをお寄せください。）</p>
<p>Autodesk Flow イニシアチブの詳細については、 <a href="https://www.autodesk.com/company/autodesk-platform/me">Autodesk Flow | Cloud-based Platform for Media &amp; Entertainment</a>&#0160;ページをご参照ください。同ページには、これらのエキサイティングなプラットフォーム開発の進捗状況と進化に関する最新情報を入手するためのリンクもあります。</p>
<p>ご参考まで。Bifrost グラフは Maya で直接作成することが出来ます。Bifrost と、これらのグラフを作成する Maya の機能の詳細については、次のドキュメント リンクをご参照ください。<br />• Bifrost の概要: <a href="https://help.autodesk.com/view/BIFROST/JPN/?guid=Bifrost_Common_working_in_bifrost_html">Bifrost ヘルプ | Bifrost での作業 | Autodesk</a><br />• Maya の Bifrost: <a href="https://help.autodesk.com/view/BIFROST/JPN/?guid=Bifrost_MayaPlugin_bifrost_for_maya_html">Bifrost ヘルプ | Bifrost for Maya | Autodesk</a><br />• Bifrost サンプル グラフ: <a href="https://help.autodesk.com/view/BIFROST/JPN/?guid=Bifrost_MayaPlugin_get_started_with_sample_html">Bifrost ヘルプ | サンプル グラフを使用する | Autodesk</a></p>
</div>
<p>※ 本記事は <a href="https://aps.autodesk.com/blog/introducing-first-me-oriented-aps-service-flow-graph-engine" rel="noopener" target="_blank">Introducing the first M&amp;E oriented APS service – Flow Graph Engine! | Autodesk Platform Services</a> から転写・意訳したものです。</p>
<p>By Toshiaki Isezaki</p>
