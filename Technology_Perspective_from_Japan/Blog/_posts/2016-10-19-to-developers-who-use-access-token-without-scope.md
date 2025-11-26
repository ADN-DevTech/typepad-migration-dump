---
layout: "post"
title: "Scope 指定なしで Access Token を利用している方へ"
date: "2016-10-19 01:09:11"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2016/10/to-developers-who-use-access-token-without-scope.html "
typepad_basename: "to-developers-who-use-access-token-without-scope"
typepad_status: "Publish"
---

<p>本日未明に Forge チームから次のような英文のメールが届いている方がいらっしゃると思います。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb094770a1970d-pi" style="display: inline;"><img alt="Email" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb094770a1970d image-full img-responsive" src="/assets/image_772732.jpg" title="Email" /></a></p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb094772b3970d-pi" style="float: left;"><img alt="Caution" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb094772b3970d img-responsive" src="/assets/image_951027.jpg" style="margin: 0px 5px 5px 0px;" title="Caution" /></a>もし、2016年6月15日以前に&#0160;<strong><a href="https://developer.autodesk.com/" target="_blank">デベロッパ ポータル</a></strong>&#0160;から取得した Consumer Key と Consumer Secret で 2-legged 認証を実装して、View and Data API や AutoCAD I/O を使ったアプリやクラウド サービスをお持ちの方は注意が必要です。</p>
<p>6月に Forge Developer Conference で正式に Forge のリリースがアナウンスされた際、OAuth API（Authentication） で Access Token（アクセス トークン）を取得する際に、Scope の指定が追加されています。逆に言えば、6月以前の Authentication では、Scope を指定しなくても Access Token を取得して、Bucket に自由にアクセスすることが出来ていました。Scope については、過去のブログ記事 <strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/07/summary-about-bucket.html" target="_blank">Bucket に関してのサマリー</a></strong>&#0160;でも少し触れています。</p>
<p>今日現在でも、2016年6月15日以前に取得した Key を使って、Scope の指定をせずに Access Token を取得するとは出来ます。ただし、これは、2017年1月2日までの猶予期間内の暫定処置です。この期日以降は、Scope なしでAccess Token を得ることが出来なくなります。</p>
<p>もし、該当される方がいらっしゃいましたら、いまのうちに <a href="https://developer.autodesk.com/en/docs/oauth/v2/reference/http/authenticate-POST/" target="_blank"><strong>Authentication</strong> </a>時に <strong><a href="https://developer.autodesk.com/en/docs/oauth/v2/overview/scopes/" target="_blank">Scope</a>&#0160;</strong>のを明示的に指定するようにしてください。</p>
<ul>
<li>過去に開催した View and Data API 1 Day Workshop で利用したチュートリアル<strong> <a href="http://adndevblog.typepad.com/technology_perspective/2016/03/view-and-data-api-tutorial-part1-nodejs-basic.html" target="_blank">View and Data API チュートリアル ～ その1 ～ Node.js Basic</a></strong>&#0160;にも、Scope を追記していますのご確認ください。</li>
</ul>
<p>By Toshiaki Isezaki</p>
