---
layout: "post"
title: "新しい Vault Data API のアーリアダプターに!!"
date: "2024-11-18 00:15:28"
author: "Takehiro Kato"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/11/become_an_early_adopter_of_the_new_vault_data_apis.html "
typepad_basename: "become_an_early_adopter_of_the_new_vault_data_apis"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3c3de3e200c-pi" style="display: inline;"><img alt="VaultDataTitle" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3c3de3e200c img-responsive" src="/assets/image_296367.jpg" title="VaultDataTitle" /></a><a class="asset-img-link" href="https://aps.autodesk.com/sites/default/files/2024-11/image-1_0.png" style="display: inline;"><br /></a><a class="asset-img-link" href="https://aps.autodesk.com/sites/default/files/2024-11/image-1_0.png" style="display: inline;"><br /></a></p>
<p>新しい Vault Data API を紹介できることを嬉しく思います。これらの REST-ful API により、Vault からのデータ拡張が可能になり、Vault 2025.2 リリースで利用できるようになりました。</p>
<p>&#0160;</p>
<h2>Vaultとは？</h2>
<p>API について説明する前に、Autodesk Vault について少し学んでみましょう。</p>
<p>Autodesk Vault は、設計チームが設計データを整理、追跡、管理するのに役立つ製品データ管理(PDM)ツールです。 Autodesk Vault PDM ソフトウェアは、設計者やエンジニアが設計データを整理し、ドキュメントを管理し、リビジョンを追跡するのに役立ちます。次のようなメリットがあります。</p>
<ul style="list-style-type: circle;">
<li>一元化されたデータ ソース: 全員が 1 つの組織化されたデータ ソースから作業できるようにします。</li>
<li>バージョン管理: バージョンを管理し、すべてのファイルのバージョンを保持します。</li>
<li>コラボレーション: チーム間のコラボレーションとコミュニケーションを改善します。</li>
<li>ワークフローの自動化: 設計およびエンジニアリングのプロセスを自動化します。</li>
<li>統合: Autodeskの設計ツールや他のCAD システムに統合されます。</li>
<li>ファイル ストレージ: CAD データと非 CAD ドキュメントの両方を保存します。</li>
<li>検索と取得: 素早い検索と取得のためにファイルのプロパティを保存します。</li>
<li>リビジョン管理: リビジョン管理機能により、ユーザーがデザインをより詳細に制御できるようになります。</li>
</ul>
<p><a href="https://www.autodesk.com/jp/products/vault/overview?term=1-YEAR&amp;tab=subscription">Vaultについて詳しく見る</a></p>
<p>&#0160;</p>
<h2>Vault データ API とは?</h2>
<p>Vault Data API を使用すると、Vault サーバーに保存されているデータにプログラムでアクセスして操作できるため、手動の労力が軽減され、デスクトップ アプリケーションを追加する必要がなくなります。 API を使用して、次のような複雑なワークフローや反復的なワークフローを自動化する統合を構築します。</p>
<ul style="list-style-type: circle;">
<li>データの検索と取得</li>
<li>ファイル操作</li>
<li>ジョブ処理</li>
<li>ユーザーセッション</li>
<li>アカウント管理</li>
<li>アイテムの処理と変更オーダー</li>
</ul>
<h2>Vault Data API の利点</h2>
<p>大きな利点の 1 つは接続性です。 Vault Data API を使用すると、開発者は Vault の機能を拡張できます。これにより、Vault データから他のツールやアプリケーションへの接続が拡張されます。 API を使用すると、次のようなさまざまなオートデスク製品に接続できます。</p>
<ul style="list-style-type: circle;">
<li>Vault から Fusion 管理へ</li>
<li>Vault から設計までの自動化</li>
<li>Vault から Autodesk Viewer へ</li>
</ul>
<p>設計、エンジニアリング、製造、生産のCAD 管理者は、データの接続とワークフローの自動化から恩恵を受けることになります。</p>
<h2>Vault Data API を使用する</h2>
<p>Autodesk Platform Serviceのサイト には、新しい Vault Data API を使用してアプリケーションの構築をすぐに始めるのに役立つ、多数のハウツー ガイドとコード サンプルが用意されています。</p>
<ul style="list-style-type: circle;">
<li><a href="https://aps.autodesk.com/en/docs/vaultdataapi/v2/tutorials/endpoint-retrieve-properties/">エンティティのプロパティを取得する</a></li>
<li><a href="https://aps.autodesk.com/en/docs/vaultdataapi/v2/tutorials/endpoint-filtering/">エンドポイントフィルターオプションを使用する</a></li>
<li><a href="https://aps.autodesk.com/en/docs/vaultdataapi/v2/tutorials/viewer-integration/">Autodesk Viewer と Vault を統合する</a></li>
</ul>
<p>&#0160;</p>
<p>また、コード サンプルも以下から入手可能です。</p>
<ul style="list-style-type: circle;">
<li><a href="https://github.com/autodesk-platform-services/vault-data-api-samples/tree/main/vault-data-api-web-demo">Node.js の Vault Data API Web サンプル</a></li>
<li><a href="https://github.com/autodesk-platform-services/vault-data-api-samples/tree/main/VaultDataAPIPowerBIConnector">PowerBI Vault データ API コネクタ</a></li>
<li><a href="https://github.com/autodesk-platform-services/vault-data-api-samples/tree/main/VaultDataAPIDesktopSampleApp">.NET Core 用の Vault Data API デスクトップ サンプル アプリ</a></li>
</ul>
<p>&#0160;</p>
<p>本リリースにおけるVault Data APIは、Vaultデータの読み取りAPIを提供しております。APIの呼び出しに対してVaultクライアントのライセンスは不要となります。Vault Data APIにより、クラウドベースのソリューションとのシームレスな統合が可能となります。ぜひ新しいVault Data APIを使用して、その可能性を体験してください。</p>
<p>&#0160;</p>
<p>※ 本記事は<a href="https://aps.autodesk.com/blog/become-early-adopter-new-vault-data-apis">Become an early adopter of the new Vault Data APIs</a>から転写・意訳・補足したものです。</p>
<p>By Takehiro Kato</p>
