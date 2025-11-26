---
layout: "post"
title: "オーストラリア リージョン指定の変更について"
date: "2024-11-13 00:31:43"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/11/changes-are-coming-autodesk-platform-services-aps-australia-region.html "
typepad_basename: "changes-are-coming-autodesk-platform-services-aps-australia-region"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860d91ae7200b-pi" style="display: inline;"><img alt="Aps2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860d91ae7200b image-full img-responsive" src="/assets/image_932129.jpg" title="Aps2" /></a></p>
<p><a href="https://adndevblog.typepad.com/technology_perspective/2024/03/australia-data-center.html" rel="noopener" target="_blank">オーストラリア データセンター</a> の記事でご案内したオーストラリア データセンターについて、APS API を使用して<strong>オーストラリア リージョン</strong>に保存されているデータにアクセス、または、アクセスする予定をお持ちの方に重要なお知らせがあります。</p>
<p>既に開発済 / 開発予定のアプリが、オーストラリア リージョンのデータセンターで稼働する Autodesk Construction Cloud（ACC）をサポートする、あるいは、Object Storage Service（OSS）API を使用して Bucket を作成し、オーストラリア リージョンのデータセンターにデータを保存する場合、オーストラリア リージョンのデータにアクセスする一部の API に変更を加えています。</p>
<p>オーストラリア リージョン データのプライマリ ストレージとして、Autodesk Docs、Autodesk Build、Autodesk BIM Collaborate/Pro、および AutodeskTakeoff の一般提供をアナウンスしました。このアナウンスにともない、ACC API、Data Management API、Model Derivative API、Webhooks API、Viewer SDK の Beta 版をリリースしています。</p>
<h4>変更点</h4>
<p>今後、オーストラリア リージョンを示すためにの指定値が、Beta 時の&#0160;<strong>APAC</strong> から&#0160;<strong>AUS&#0160;</strong>に変更されます。これにより、保存されるデータの場所がより明確になり、ビジネス ユニットに合わせて調整されるようになります。現在、一部のエンドポイントでは、region フィールドの値として APAC が使用されている場合があります。今後、region 値としての APAC は非推奨になりますのでご注意ください。</p>
<p>リージョン サポートに関して注意すべきいくつかの変更点があります。</p>
<ul>
<li><strong>ACC API</strong>&#0160;と<strong>自動リージョン ルーティング</strong> -- すべての ACC API で自動リージョン ルーティングがサポートされるようになりました。米国で使用するエンドポイントは、すべてのリージョンで機能します。これにより、将来のリージョン拡大のためのコーディングが容易になります。ヨーロッパ リージョン用に個別のエンドポイントを持つ古い管理 API は、下位互換性のために引き続き機能します。<br />&#0160;</li>
<li><strong>リクエスト時</strong>のリージョン -- <strong>region フィールド</strong>を指定する必要があるエンドポイントの場合、現在は&#0160; <strong>AUS</strong> と <strong>APAC</strong> の両方が機能しますが、APAC は非推奨の値です。明示的なリージョン値が必要なエンドポイントを使用している場合は、ご注意ください。これは、<strong>Data Management/Model Derivative/Webhooks API </strong>と<strong> Viewer</strong> に適用されます。また、オプションの region ヘッダーを受け取る <strong>ACC API</strong> にも適用することが出来ます。（注: リクエストで AUS を使用する場合、レスポンスのリージョン値は AUS になります。リクエストでAPACを使用する場合、レスポンスはAPACになります。 ）<br />&#0160;</li>
<li><strong>レスポンス時</strong>のリージョン – エンドポイント呼び出しからのレスポンス ボディには、AUS への移行中に&#0160;<strong>APAC&#0160;</strong>または&#0160;<strong>AUS&#0160;</strong>が含まれる場合があります。コードに情報が必要な場合は、遷移が完了するまで、<span style="text-decoration: underline;">アプリのコ</span><u>ード内の両方の値と照合</u>することをお勧めします。<br />&#0160;</li>
<li><strong>Viewer</strong> - Viewer バージョン <strong>7.100.0</strong> 以降では、Autodesk Docs のモデルについて、オーストラリアを含む自動リージョン ルーティングがサポートされています。&#0160;</li>
</ul>
<h4>変更はいつ行われますか?</h4>
<p>上記の変更は、以下のタイムラインに従って実施される予定です。混乱を避けるために、必要な変更を加えるよう尾根ファイします。</p>
<ul>
<li>現在 (2024 年 11 月 4 日現在) – リクエスト時のリージョン指定は、AUS と APAC の両方を受け入れます。<br />&#0160;</li>
<li><strong>2025年3月1日</strong>から、レスポンス時のリージョンが AUS に変わります。例:&#0160;<a  _istranslated="1"  _mstmutation="1" href="https://aps.autodesk.com/en/docs/data/v2/reference/http/hubs-GET/" rel="noopener" target="_blank">GET Hub</a> &#0160;</li>
</ul>
<h4>追加情報</h4>
<p>オーストラリア リージョン向けの製品と利用可能な製品・ツールに関する情報は、次のリソースから見つけることができます。</p>
<ul>
<li><a  _msthash="109"  _msttexthash="125740121" href="https://help.autodesk.com/view/DOCS/ENU/?guid=Regional_Data_Storage" rel="noopener" target="_blank">Autodesk Construction Cloud - 地域データ ストレージに関する FAQ（英語）</a></li>
</ul>
<p>オーストラリア リージョンの提供に関連する API の変更についてご不明な点がございましたら、お気軽に<a  _istranslated="1" href="https://aps.autodesk.com/contact-support">お問い合わせください</a>。</p>
<p>※ 本記事は <a href="https://aps.autodesk.com/blog/changes-are-coming-autodesk-platform-services-aps-australia-region" rel="noopener" target="_blank">Changes are coming for Autodesk Platform Services (APS) in the Australia Region | Autodesk Platform Services</a>&#0160;から転写・意訳・補足したものです。</p>
<p>By Toshiaki Isezaki</p>
