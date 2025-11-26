---
layout: "post"
title: "DevDays 2018 で紹介した Forge サンプル"
date: "2018-02-28 00:06:23"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2018/02/forge-samples-shown-in-devdays-2018.html "
typepad_basename: "forge-samples-shown-in-devdays-2018"
typepad_status: "Publish"
---

<p>去る 2月20日 大阪で、2月22日 東京で開催した <strong><a href="http://adndevblog.typepad.com/technology_perspective/2018/02/developer-days-2018.html" rel="noopener noreferrer" target="_blank">Developer Days</a></strong> の&#0160; <strong>ビジネスモデルの遷移にみるデスクトップ製品と</strong><strong>&#0160;Forge</strong>&#0160;セッションで、2 点ほど、Forge を使って作成されたサンプルをご紹介しました。</p>
<p>実際のプロジェクトで使用されたものが含まれるので Github 等でソースコードは公開されていないのですが、イベント終了後にご質問、お問合せをうけましたので、Live Demo としてのみご紹介しておきます。なお、あくまでデモ サイトであるため、予告なく削除される可能性があります。あらかじめ、ご了承いただければと思います。</p>
<p><strong>Forge Dashboard デモ</strong></p>
<p style="padding-left: 30px;"><a href="https://forgedashboard.herokuapp.com/" rel="noopener noreferrer" target="_blank">https://forgedashboard.herokuapp.com/</a>&#0160;&#0160;からアクセス出来るデモです。Google Map API を利用して Forge Viewer を介した 3D モデルと他のデータソースを統合表示するダッシュボードのデモです。</p>
<p style="padding-left: 30px;">最初に表示される地図内のオフィス名 North Office、Main Factory のいずれかをクリックすることで、対応する 3D モデルが中央の Forge Viewer 上に表示されます。画面右手には、他のデータソースから取得したメタデータが表示され、Risk Index 内の円形ゲージや Building Projects 内にプロジェクト名をクリックすることで、関連付けられた要素を Viewer 内に表示します。</p>
<p style="padding-left: 30px;">ここで表示されるのは、3D モデルも含め、あくまでダミーデータです。さまざまなデータを Web ページに一元表示することで、プロジェクト運用やメインテナンス等で分かりやく情報を把握出来る例となります。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2de35bc970c-pi" style="display: inline;"><img alt="Forge_dashboard" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2de35bc970c image-full img-responsive" src="/assets/image_165251.jpg" title="Forge_dashboard" /></a></p>
<p><strong>4D/Phasing デモ</strong></p>
<p style="padding-left: 30px;"><a href="https://forge-bim-phasing.azurewebsites.net/" rel="noopener noreferrer" target="_blank">https://forge-bim-phasing.azurewebsites.net/</a> からアクセス可能なデモです。Navisiworks の Timeliner のように、時間軸に沿って施工フェーズ を視覚化しています。</p>
<p style="padding-left: 30px;">各フェーズの切り替えは、Forge Viewer 内に表示されるツールバーから、右手のカスタムツールバー上の左側のボタンをクリックし、表示される [PHASES] パネルで実行します。この操作で Forge Viewer 内に対応するフェーズの施工要素が表示されてくるので、適宜、Viewer 機能を利用して視覚的に邪魔になるモデルを非表示にしたりすることが可能です。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09f73386970d-pi" style="display: inline;"><img alt="Forge_bim_phasing" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb09f73386970d image-full img-responsive" src="/assets/image_697520.jpg" title="Forge_bim_phasing" /></a></p>
<p>Forge で何が出来るのか、という視点でご確認いただけるといいかと思います。なお。デモで使用されているグラフやゲージ、背後で利用されているデータベース機能などは、一般的な Web 開発でよく利用されるもので、オートデスク、ないし、Forge で提供しているものではありません。</p>
<p>By Toshiaki Isezaki</p>
