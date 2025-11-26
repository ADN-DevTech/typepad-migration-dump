---
layout: "post"
title: "APS Online Training：Hub Browser 収録公開"
date: "2025-05-28 01:00:00"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2025/05/aps-online-training-hub-browser-rmaterials.html "
typepad_basename: "aps-online-training-hub-browser-rmaterials"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860ff86b3200d-pi" style="display: inline;"><img alt="Aps_online_training" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860ff86b3200d image-full img-responsive" src="/assets/image_267501.jpg" title="Aps_online_training" /></a></p>
<p>2025年5月21日に、<span data-offset-key="c2bst-1-0"><a href="https://aps.autodesk.com/getting-started" rel="noopener" target="_blank">Learn APS Tutorial</a>&#0160;の&#0160;<strong><a href="https://get-started.aps.autodesk.com/tutorials/hubs-browser/" rel="noopener" target="_blank">Hub Browser</a></strong>&#0160;を題材に、オンラインで</span>トレーニングを開催しました。当日は、コードの説明を中心に、Autodesk のクラウドベースのアプリケーション（Autodesk Docs, BIM 360 Docs、Fusion Team) のストレージに保存されたユーザーのコンテンツにアクセスし、APS Viewer で表示するワークフローを把握していただきました。Web サーバー実装には、Node.js と APS SDK、クライアント実装に APS Viewer を使ってアプリを構築する過程をご案内しています。</p>
<hr />
<p><strong>前提</strong></p>
<ul>
<li>APS には、90 日間の無償トライアルが用意されています。APS を初めて使用される場合には、最初に次の記事をご確認ください。<br />&gt;&gt;&#0160;<a href="https://adndevblog.typepad.com/technology_perspective/2023/03/how-to-get-started-aps.html" rel="noopener" target="_blank"><strong>Autodesk Platform Services の始め方</strong></a></li>
<li>説明では Node.js と VS Code を利用します。次の記事を事前にご確認をお願いします。<br />&gt;&gt;&#0160;<strong><a href="https://adndevblog.typepad.com/technology_perspective/2017/01/development-environment-for-forge.html" rel="noopener" target="_blank">APS の開発環境</a></strong></li>
<li>デベロッパキー（Client Id と Client Secret）の取得が必要です。また、キーの取得には、Autodesk ID が必要となります。Autodesk ID、アクセスキーをお持ちでない場合には、次のブログ記事に沿って、それらを事前に取得しておくようお願いします。<br />&gt;&gt;&#0160;<a href="https://adndevblog.typepad.com/technology_perspective/2016/07/app-registration-and-getting-keys-for-forge-api.html" rel="noopener" target="_blank"><strong>APS API を利用するアプリの登録とキーの取得</strong></a></li>
<li>HTML、CSS、JavaScript、RESTful API の概要を把握されていると理解が深まります。もし、Web 開発が初めての場合には、<a href="https://developer.mozilla.org/ja/docs/Learn/Getting_started_with_the_web" rel="noopener" target="_blank"><strong>ウェブ入門 - ウェブ開発を学ぶ</strong>&#0160;| MDN (mozilla.org)</a>、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2022/02/need-to-know-before-forge-development.html" rel="noopener" target="_blank">APS（旧 Forge）開発に際して...</a></strong>&#0160;をご一読いただくことをお勧めします。</li>
</ul>
<hr />
<p><strong>プレゼンテーション資料</strong></p>
<p>当日使用したプレゼンテーション資料（PDF ファイル）は、次のリンクからダウンロードすることが出来ます。</p>
<p style="padding-left: 40px;"><span class="asset  asset-generic at-xid-6a0167607c2431970b02c8d3d41382200c img-responsive"><a href="https://adndevblog.typepad.com/files/aps-training---hub-browser.pdf" rel="noopener" target="_blank"><strong>APS Training - Hub Browser</strong> をダウンロード</a></span></p>
<hr />
<p><strong>収録動画</strong></p>
<ul>
<li>はじめに（約27分）</li>
</ul>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/Y29eNpYq-YA" width="480"></iframe></p>
<ul>
<li>デベロッパキーの取得（約5分）</li>
</ul>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/flvQ1qo2krQ" width="480"></iframe></p>
<ul>
<li>ACC カスタム統合の設定（約2分）</li>
</ul>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/XCYSyNfMjag" width="480"></iframe></p>
<ul>
<li>セットアップ（約9分）</li>
</ul>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/ebXeASP06l0" width="480"></iframe></p>
<ul>
<li>認証と認可（約19分）</li>
</ul>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/Wcz2FY3uyUk" width="480"></iframe></p>
<ul>
<li>データのブラウジング（約17分）</li>
</ul>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/2LfZFhDyL8o" width="480"></iframe></p>
<ul>
<li>Viewer と UI（約26分）</li>
</ul>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/6E3O2e_4DWU" width="480"></iframe></p>
<p>By Toshiaki Isezaki</p>
