---
layout: "post"
title: "Viewer 表示用の Scope の追加"
date: "2017-06-05 02:22:08"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2017/06/added-new-scope-for-viewing.html "
typepad_basename: "added-new-scope-for-viewing"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09a215c9970d-pi" style="float: right;"><img alt="Forge Access Token" class="asset  asset-image at-xid-6a0167607c2431970b01bb09a215c9970d img-responsive" src="/assets/image_533425.jpg" style="margin: 0px 0px 5px 5px;" title="Forge Access Token" /></a>以前、<strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/10/to-developers-who-use-access-token-without-scope.html" rel="noopener noreferrer" target="_blank">Scope 指定なしで Access Token を利用している方へ</a></strong> のブログ記事でご案内したとおり、現在、Authentication API(OAuthe API）で Access Token を取得する際には、必ず時の <strong><a href="https://forgecms-alpha.forge.autodesk.com/en/docs/oauth/v2/overview/scopes/" rel="noopener noreferrer" target="_blank">Scope</a></strong> を指定する必要があります。</p>
<p>この Scope に、今回、新しく&#0160;<strong>viewables:read</strong> が追加されています。Access Tokenを取得する目的が、Model Derivative API で既に変換済の SVF ファイルを表示するだけなら、Data Management からのデータ読み込みで必要となる data:read を指定する必要はもうありません。</p>
<p>この追加は、Viewer 表示に必要十分な値 viewables:read を利用することで必要以上な権限を付与してしまうことを避け、キュリティ向上を目指すためのものです。もし、前述の状況で Access Token を取得される場合には、data:read に替わって&#0160;viewables:read を指定するようコードの変更をお勧めします。</p>
<p>By Toshiaki Isezaki</p>
