---
layout: "post"
title: "OpenAPI 仕様をオープンソース化"
date: "2025-05-07 00:42:49"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2025/05/openapi-specs-are-here.html "
typepad_basename: "openapi-specs-are-here"
typepad_status: "Publish"
---

<div class="field field--name-field-primary-image field--type-image field--label-hidden field__item"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860ff2759200d-pi" style="display: inline;"><img alt="Aps-oapi" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860ff2759200d img-responsive" src="/assets/image_312568.jpg" title="Aps-oapi" /></a></div>
<div class="clearfix text-formatted field field--name-field-body field--type-text-with-summary field--label-hidden field__item">
<p>オートデスク SDK チームが、いくつかの REST API の <a href="https://docs.github.com/ja/rest/about-the-rest-api/about-the-openapi-description-for-the-rest-api?apiVersion=2022-11-28" rel="noopener" target="_blank">OpenAPI 仕様</a> 仕様をオープンソース化しています。OpenAPI は、REST API インターフェイスを記述するための仕様です。もし、APS の上に構築している場合には、APS API に対してこれまで以上に迅速に探索、統合、自動化出来るようになります。</p>
<h3>OpenAPIとは？</h3>
<p><a  _istranslated="1" href="https://www.openapis.org/" rel="noopener" target="_blank">OpenAPI</a>は、RESTful API を機械可読形式（YAML または JSON）で記述するために広く採用されている標準です。APIのエンドポイント、リクエスト/レスポンス構造、認証方法などを、ツールと人間の両方が理解できる方法で定義することが出来ます。API がどのように機能するかの青写真のようなものです。</p>
<h3>APS 開発者にとって重要な理由</h3>
<p>OpenAPI 仕様にアクセスすると、例えば、次のようなことが可能になります。</p>
<ul>
<li>ドキュメントを調査せずに、Postman などのツールを使用してエンドポイントをすばやく探索</li>
<li>お使いの言語でクライアント SDK を自動生成</li>
<li>AI および ローコード/ノーコード プラットフォームとの統合を自動生成</li>
<li>リクエスト/レスポンスを検証し、開発の早い段階でエラーをキャッチ</li>
</ul>
<h3>現在利用可能なリソース</h3>
<p>現段階では、最も一般的に使用されるいくつかの APS API の OpenAPI 仕様を公開しています。今後、お客様のフィードバックに基づいてカバーする領域を拡大し、仕様を改善していく予定です。</p>
<p>&#0160;APS API の OpenAPI 仕様は、公式の GitHubリポジトリで公開されています。<a  _istranslated="1" href="https://github.com/autodesk-platform-services/aps-sdk-openapi" rel="noopener" target="_blank"></a></p>
<ul>
<li><strong><a  _istranslated="1" href="https://github.com/autodesk-platform-services/aps-sdk-openapi" rel="noopener" target="_blank">https://github.com/autodesk-platform-services/aps-sdk-openapi</a></strong></li>
</ul>
<h3>次のステップ</h3>
<p>これは、これらの OpenAPI 仕様の使用方法（SDK、Power Automate コネクタ、AI エージェントの生成など）の実際的な例を詳しく説明するブログ投稿シリーズの第１回です。</p>
<p>次回の投稿では、OpenAPI 仕様から&#0160;<strong>Postman コレクション</strong>を生成して使用し、API テスト、コラボレーション、オンボーディングを効率化する方法をご紹介します。</p>
<p>いつものように、フィードバックをお待ちしております。次に必要な仕様や利便性を高める方法をお持ちであれば、ぜひ、共有いただければと思います。</p>
</div>
<p>※ 本記事は <a href="https://aps.autodesk.com/blog/openapi-specs-are-here" rel="noopener" target="_blank">OpenAPI Specs Are Here! | Autodesk Platform Services</a>&#0160;から転写・意訳・補足したものです。</p>
<p>By Toshiaki Isezaki</p>
