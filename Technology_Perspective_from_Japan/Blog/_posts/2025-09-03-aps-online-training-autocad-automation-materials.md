---
layout: "post"
title: "APS Online Training：AutoCAD Automation 収録公開"
date: "2025-09-03 00:10:00"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2025/09/aps-online-training-autocad-automation-materials.html "
typepad_basename: "aps-online-training-autocad-automation-materials"
typepad_status: "Future"
---

<p><a href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e86100a4bc200d-pi"> </a><a href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3d31867200c-pi"><img alt="Aps_online_training" border="0" src="/assets/image_710832.jpg" title="Aps_online_training" /></a></p>
<p><span data-offset-key="c2bst-1-0">2025年8月27日に、</span><a href="https://aps.autodesk.com/getting-started" rel="noopener" target="_blank"></a><span data-offset-key="c2bst-1-0"><a href="https://aps.autodesk.com/getting-started" rel="noopener" target="_blank">Learn APS Tutorial</a>&#0160;に記載されている&#0160;<strong><a href="https://get-started.aps.autodesk.com/tutorials/design-automation/" rel="noopener" target="_blank">Design Automation</a></strong>&#0160;の学習コンテンツを用いて、AutoCAD エンジンを使用したオンラインで</span>トレーニングを開催し、コードの説明を中心に、クラウド上の AutoCAD Automation API（旧名 Design Automation for AutoCAD）環境で AutoCAD アドインを利用する自動化プロセスのワークフローをご案内しました。Web サーバー実装には Node.js を VS Code を使って構築する過程をご案内しています。</p>
<hr />
<p><strong>前提</strong></p>
<ul>
<li>APS には、90 日間の無償トライアルが用意されています。APS を初めて使用される場合には、最初に次の記事をご確認ください。<br />&gt;&gt;&#0160;<a href="https://adndevblog.typepad.com/technology_perspective/2023/03/how-to-get-started-aps.html" rel="noopener" target="_blank"><strong>Autodesk Platform Services の始め方</strong></a></li>
<li>説明では Node.js と VS Code を利用します。次の記事を事前にご確認をお願いします。<br />&gt;&gt;&#0160;<strong><a href="https://adndevblog.typepad.com/technology_perspective/2017/01/development-environment-for-forge.html" rel="noopener" target="_blank">APS の開発環境</a></strong></li>
<li>デベロッパキー（Client Id と Client Secret）の取得が必要です。また、キーの取得には、Autodesk ID が必要となります。Autodesk ID、アクセスキーをお持ちでない場合には、次のブログ記事に沿 って、それらを事前に取得しておくようお願いします。<br />&gt;&gt;&#0160;<a href="https://adndevblog.typepad.com/technology_perspective/2016/07/app-registration-and-getting-keys-for-forge-api.html" rel="noopener" target="_blank"><strong>APS API を利用するアプリの登録とキーの取得</strong></a></li>
<li>HTML、CSS、JavaScript、RESTful API の概要を把握されていると理解が深まります。もし、Web 開発が初めての場合には、<a href="https://developer.mozilla.org/ja/docs/Learn/Getting_started_with_the_web" rel="noopener" target="_blank"><strong>ウェブ入門 - ウェブ開発を学ぶ</strong>&#0160;| MDN (mozilla.org)</a>、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2022/02/need-to-know-before-forge-development.html" rel="noopener" target="_blank">APS（旧 Forge）開発に際して...</a></strong>&#0160;をご一読いただくことをお勧めします。</li>
</ul>
<hr />
<p><strong>プレゼンテーション資料</strong></p>
<p>当日使用したプレゼンテーション資料（PDF ファイル）は、次のリンクからダウンロードすることが出来ます。</p>
<p style="padding-left: 40px;"><span class="asset  asset-generic at-xid-6a0167607c2431970b02e860f225fb200b img-responsive"><a href="https://adndevblog.typepad.com/files/aps-training---autocad-automation.pdf" rel="noopener" target="_blank"><strong>APS Training - AutoCAD Automation</strong> をダウンロード</a></span></p>
<hr />
<p><strong>収録動画</strong></p>
<ul>
<li>はじめに（約20分）</li>
</ul>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/v83_H69yF44" width="480"></iframe></p>
<ul>
<li>理由と仕組み（約16分）</li>
</ul>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/4wyobhRkb4Y" width="480"></iframe></p>
<ul>
<li>AutoCAD Automation の理解（約16分）</li>
</ul>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/4X2o0b6_2_I" width="480"></iframe></p>
<ul>
<li>デベロッパキーの取得（約8分）</li>
</ul>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/Be1GOB6OXJ0" width="480"></iframe></p>
<ul>
<li>サーバーの作成（約17分）</li>
</ul>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/IRAo14QgEpQ" width="480"></iframe></p>
<ul>
<li>基本 UI（約5分）</li>
</ul>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/vXbxewXmN3g" width="480"></iframe></p>
<ul>
<li>プラグイン（アドイン）の作成（約20分）</li>
</ul>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/rOoyXwT-pxQ" width="480"></iframe></p>
<ul>
<li>Activity（アクティビティ）の定義（約9分）</li>
</ul>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/dOCjbP9vh_c" width="480"></iframe></p>
<ul>
<li>WorkItem（ワークアイテム）の実行（約12分）</li>
</ul>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/1fNAy47Be3E" width="480"></iframe></p>
<ul>
<li>Automation API サポート ポリシー（約5分）</li>
</ul>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/9Afr8FHyg6k" width="480"></iframe></p>
<p>By Toshiaki Isezaki</p>
