---
layout: "post"
title: "Design Automation API for 3ds Max Beta"
date: "2019-08-21 00:10:59"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2019/08/design-automation-api-for-3ds-max.html "
typepad_basename: "design-automation-api-for-3ds-max"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a477a0ce200c-pi" style="display: inline;"><img alt="3dsmaxda" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a477a0ce200c image-full img-responsive" src="/assets/image_17727.jpg" title="3dsmaxda" /></a></p>
<p>Design Automation API v3 には、 v2 でサポートされている AutoCAD コアエンジンに加え、Revit、Inventor、3ds Max のコアエンジンがサポートされる予定です。また、Beta ステージの状態ですが、遠くない将来、正式にリリースされることが予告されています。</p>
<p>いままで、CAD&#0160; をエンジンとしたバッチ処理にフォーカスされてきましたが、唯一、3ds Max がメディア &amp; エンターテインメント製品から導入されていくので、今回は Design Automation API for 3ds Max についてご紹介しておきたいと思います。</p>
<hr />
<h4>Design Automation API for 3ds Max で何が可能か?</h4>
<p style="padding-left: 40px;">3ds Max 製品には、すでに 3ds Max Batchと呼ばれるツールが含まれています。 このツールは、タスクのローカル自動化を可能にし、設計自動化システムを使用する前に自動化をテストするのに適した方法です。 3ds Max 2020 の バッチの詳細については、<strong><a href="http://help.autodesk.com/view/3DSMAX/2020/JPN/?guid=GUID-0968FF0A-5ADD-454D-B8F6-1983E76A4AF9" rel="noopener" target="_blank">こちら </a></strong>をご確認ください。</p>
<p style="padding-left: 40px;">3ds Max Batch ツールで実行出来る処理は、Design Automation API for 3ds Max のコアエンジンでも実行させることが出来ます。また、自動化処理にプラグイン アプリケーションを含めることも出来ます。 通常、自動化には MAXScript を利用しますが、コード自体は C ++ や .NET プラグインに存在できます。 もちろん、MAXScript には自動化コードを含めることもできます。 Design Automation API for 3ds Max は、スクリプト コードにパラメータを渡す機能もサポートしています。スクリプト コードは、オートメーション コードに渡すこともできます。 これにより、自動化ジョブに構成とデータを提供すりことで非常に柔軟なワークフローが可能になります。 例えば、ユーザから情報を取得するコンフィギュレーター Web サイトを作成する場合、Design Automation API for 3ds Max を使用して Max モデルを自動生成して返すことができます。 Model Derivative API と Forge Viewer を使用して、生成されたモデルをプレビューすることもできます。</p>
<h4>サポートされる 3ds Max バージョンは?</h4>
<p style="padding-left: 40px;">現時点では、3ds Max 2020（&quot;Autodesk.3dsMax+2020&quot;）、3ds Max 2019（&quot;Autodesk.3dsMax+2019&quot;）、3ds Max 2018（&quot;Autodesk.3dsMax+2018&quot;）がサポートされています。</p>
<h4>制限事項は?</h4>
<ul>
<li>実行には完全性の低い状態で実行できるはずです。</li>
<li>実行は、整合性の低いフォルダに自己完結している必要があります。</li>
<li>作業フォルダ、または、AppData / localLowの外部にファイルを書き込まないでください。</li>
<li>作業フォルダ、AppData / localLow、または AppBundle フォルダ以外のファイルを読み取ったり実行しようとしないでください。</li>
<li>プラグインでインターネットとのやり取りは失敗します。</li>
<li>Arnold によるレンダリングには透かしがあります。</li>
</ul>
<h4>習得はどこから?</h4>
<p style="padding-left: 40px;">Forge ポータルから Design Automation API for 3ds Max のドキュメント（英語のみ）をご参照いただけます。</p>
<p style="padding-left: 80px;"><a href="https://forge.autodesk.com/en/docs/design-automation/v3/developers_guide/overview/" rel="noopener" target="_blank" title="https://forge.autodesk.com/en/docs/design-automation/v3/developers_guide/overview/">https://forge.autodesk.com/en/docs/design-automation/v3/developers_guide/overview/</a></p>
<p style="padding-left: 40px;">Node JS を利用したサンプルは、次の GitHub リポジトリに記載されています。</p>
<p style="padding-left: 80px;"><a href="https://github.com/Autodesk-Forge/design.automation.3dsmax-nodejs-basic" rel="noopener" target="_blank" title="https://github.com/Autodesk-Forge/design.automation.3dsmax-nodejs-basic">https://github.com/Autodesk-Forge/design.automation.3dsmax-nodejs-basic</a></p>
<p style="padding-left: 80px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4c5de5c200b-pi" style="display: inline;"><img alt="Thumbnail" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4c5de5c200b image-full img-responsive" src="/assets/image_119481.jpg" title="Thumbnail" /></a></p>
<hr />
<p>Design Automation API for 3ds Max は、AutoCAD や Revit 、Inventorなど、他の Design Automation API v3 と同じ endpoint を共有していますので、どれか 1 つの利用経験があれば、概念同様、RESTful API を使った Forge サーバーとのコミュニケーションは同じように理解、実装することが可能です。物理的に 3ds Max 固有となるのは、プラグイン実装など、AppBundle 実装になります。</p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
