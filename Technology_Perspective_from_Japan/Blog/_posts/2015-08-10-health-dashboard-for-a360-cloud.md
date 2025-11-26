---
layout: "post"
title: "A360 クラウドのヘルス ダッシュボード"
date: "2015-08-10 01:14:32"
author: "Toshiaki Isezaki"
categories:
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/08/health-dashboard-for-a360-cloud.html "
typepad_basename: "health-dashboard-for-a360-cloud"
typepad_status: "Publish"
---

<p>オートデスクのクラウド サービスは、<a href="https://aws.amazon.com/jp/" target="_blank">AWS</a> 上に構築されているサービス群です。今日現在、米国西海岸と東海岸、欧州 2 か所、シンガポール 2 か所のデータセンターを利用しています。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d14508a3970c-pi" style="display: inline;"><img alt="Cloud_services" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d14508a3970c image-full img-responsive" src="/assets/image_711652.jpg" title="Cloud_services" /></a></p>
<p>外部にあるインフラを利用することから、稼働状況が気になる方もいらっしゃるかと思います。また、今後、災害等で、一時的にクラウド サービスに接続できない場合が発生する可能性もあるかもしれません。</p>
<p>そのような場合には、オートデスク側の問題なのか、クラウド サービスに至るネットワーク環境の問題なのかを切り分けるために、クラウド サービスの稼働状況を表示する <a href="https://health.autodesk.com" target="_blank"><strong>Health Dashboard</strong> サイト</a>&#0160;で、状況を確認する方法が用意されています。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb085f59ed970d-pi" style="display: inline;"><img alt="Health_dashboard" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb085f59ed970d image-full img-responsive" src="/assets/image_927082.jpg" title="Health_dashboard" /></a></p>
<p>もし、Autodesk ID をお持ちであれば、ページ右上にある [Sign In] リンクからサインインすることで、リストされているクラウド サービスの稼働状況が変化した際の通知をメールで受け取れるように設定することが出来ます。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb085f5a69970d-pi" style="display: inline;"><img alt="Subscribe_status_changes" class="asset  asset-image at-xid-6a0167607c2431970b01bb085f5a69970d img-responsive" src="/assets/image_97548.jpg" style="width: 370px;" title="Subscribe_status_changes" /></a></p>
<p>残念ながら、通知されるメールは英語になってしまいますが、クラウドへのアクセス前にサービス状態を把握することが出来ます。なお、この通知メールで表示されている時間は、米国太平洋標準時（PDT）です。現在の日本との時差はマイナス 16 時間（夏季）なので、米国太平洋時間に 16 時間を加算すると、日本標準時（JST）になります。つまり、 2:00 PM は日本時間では翌日の 6：00 AM、同じく 5:00 PM は日本時間の 9：00 AM になります。なお、米国には<a href="https://ja.wikipedia.org/wiki/%E5%A4%AA%E5%B9%B3%E6%B4%8B%E5%A4%8F%E6%99%82%E9%96%93" target="_blank">夏時間（サマータイム）</a>があるので、夏季と冬季で 1 時間差が出てしまいます（冬季の時差はマイナス 17 時間）。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7bc7951970b-pi" style="display: inline;"><img alt="Email_notification" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7bc7951970b image-full img-responsive" src="/assets/image_46553.jpg" title="Email_notification" /></a></p>
<p>気になる方は、ぜひご確認ください。</p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
