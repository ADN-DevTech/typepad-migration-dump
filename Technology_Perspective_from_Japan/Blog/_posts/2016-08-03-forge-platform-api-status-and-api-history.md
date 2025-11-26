---
layout: "post"
title: "Forge Platform API のステータスと変更履歴"
date: "2016-08-03 01:14:40"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2016/07/forge-platform-api-status-and-api-history.html "
typepad_basename: "forge-platform-api-status-and-api-history"
typepad_status: "Publish"
---

<p>Forge Platform API &#0160;の稼働状況を示す API ステータスについて、以前もブログ記事でご紹介していますが、独立した記事として再記載することにします。</p>
<p>Forge Platform API は安全なゲートウェイを介してオートデスクが公開している Web サービス API です。クラウド サービスと同様に、定期的なメンテナンス等で一時的にサービスの提供を停止する可能性もあります。そんな際には、デベロッパ ポータル（<a href="http://developer.autodesk.com/" target="_blank">http://developer.autodesk.com</a>) &#0160;ページ上部の API Status メニューから、API が正常に稼働しているかを表示するヘルス ステータスを確認することが出来ます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c86e8262970b-pi"><img alt="Api_status1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c86e8262970b image-full img-responsive" src="/assets/image_575062.jpg" title="Api_status1" /></a></p>
<p>残念ながら、オートデスク クラウド サービス（SaaS） の&#0160;<strong><a href="https://health.autodesk.com" target="_blank">ヘルス ダッシュボード</a></strong>&#0160;とは異なり、状態変化が発生した場合などにメール通知を配信する機能は、今のところありません。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c86e8267970b-pi"><img alt="Api_status2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c86e8267970b image-full img-responsive" src="/assets/image_624100.jpg" title="Api_status2" /></a></p>
<p>メインテナンス時以外にサービスが止まることは考えにくいのですが、正式にリリースされている Forge Platform API を利用した開発時に、突然、動作不良になってしまう、といった症状に見舞われた際には、念のため、<strong><a href="https://developer.autodesk.com/en/support/api-status" target="_blank">API Status</a></strong> ページをご確認ください。</p>
<p>Viewer など、旧 View and Data API &#0160;時代からバージョン アップを繰り返している API には、互換性を崩すような更新がある場合もあります。バージョンアップの内容は、過去、Forum 上で告知してきた経緯がありますが、現在では、デベロッパ ポータルの各 API ページの中に <strong>Recent Changes</strong> （最近の変更）などの名称で変更点が説明されされています。&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0925c55b970d-pi" style="display: inline;"><img alt="Api_changes_menu" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb0925c55b970d image-full img-responsive" src="/assets/image_955901.jpg" title="Api_changes_menu" /></a></p>
<p>もちろん、新機能の紹介や利用方法も説明されていますので、定期的にチェックしていただくことをお勧めします。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0925c573970d-pi" style="display: inline;"><img alt="Api_changes_description" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb0925c573970d image-full img-responsive" src="/assets/image_525820.jpg" title="Api_changes_description" /></a></p>
<p>&#0160;By Toshiaki Isezaki</p>
