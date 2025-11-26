---
layout: "post"
title: "ヘルス ダッシュボード メンテナンス"
date: "2022-02-16 00:01:23"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/02/health-dashboard-maintenance.html "
typepad_basename: "health-dashboard-maintenance"
typepad_status: "Publish"
---

<p>ご存じと思いますが、オートデスクのクラウド サービスや Forge の稼働状況を表示する <strong><a href="https://health.autodesk.com/" rel="noopener" target="_blank">ヘルス ダッシュボード （https://health.autodesk.com/）</a></strong>があります。</p>
<p>利用出来る機能は、過去のブログ記事 <a href="https://adndevblog.typepad.com/technology_perspective/2017/06/updated-health-dashboard.html" rel="noopener" target="_blank"><strong>ヘルス ダッシュボードの更新</strong></a> でご紹介した内容から大きく変わっていませんが、今回、内部実装に改良を加えるため、メンテナンスが実施されることになりました。</p>
<p>現在、<strong><a href="https://health.autodesk.com/" rel="noopener" target="_blank">https://health.autodesk.com</a>&#0160;</strong>にアクセスすると、ページ上部にメンテナンスを告知するメッセージが表示されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278806b1da5200d-pi" style="display: inline;"><img alt="Notification" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0278806b1da5200d image-full img-responsive" src="/assets/image_629821.jpg" title="Notification" /></a></p>
<p>このメッセージを直訳すると、次のようになります。</p>
<blockquote>
<p>2022年2月17日～2022年2月22日の間、ヘルス ダッシュボードのメンテナンスを開始いたします。<span style="text-decoration: underline;">この間もヘルス ダッシュボードはご利用いただけます</span>が、現在（メール通知機能を）Subscribe されている方はヘルス ダッシュボードへのメール設定を変更することが出来ず、また、通知メールに対する新規 Subscribe の手続きも出来ません。2022年2月22日、ヘルス ダッシュボードのメンテナンス終了後、前述の機能は再び使用可能になります。</p>
</blockquote>
<p>メインテナンス終了後には新しいオートデスク ロゴやカラースキームに対応した見た目になるはずですが、提供する機能に大きな変更はありません。ただ、使用するバックエンド システムの更新により、情報更新のリアルタイム性が向上する予定です。変更点については、後日ご案内する予定です。</p>
<p>By Toshiaki Isezaki</p>
