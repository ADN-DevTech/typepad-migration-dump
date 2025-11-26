---
layout: "post"
title: "Design Automation：WorkItem クエリーの追加"
date: "2024-01-24 00:01:06"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/01/desigm-automation-new-endpoints%E3%83%B3%E3%83%88.html "
typepad_basename: "desigm-automation-new-endpointsント"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a4b69f200c-pi" style="display: inline;"> </a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a98edf200d-pi" style="display: inline;"><img alt="Office-SF-300_Mission-6861" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a98edf200d image-full img-responsive" src="/assets/image_843916.jpg" title="Office-SF-300_Mission-6861" /></a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a4b69f200c-pi" style="display: inline;"><br /></a></p>
<p><a href="https://aps.autodesk.com/en/docs/design-automation/v3/reference/http/" rel="noopener" target="_blank">ドキュメント</a>への記載が遅れていますが、Design Automation API の利便性向上を目的に、WorkItem 関連の新しい機能が追加されました。<a  _istranslated="1" href="https://aps.autodesk.com/en/docs/design-automation/v3/reference/http/workitems-id-GET/" rel="noopener" target="_blank">GET workitems/:id</a> エンドポイントへの新しいクエリーパラメータと WorkItem ステータスを得る新しいエンドポイントの追加です。</p>
<hr />
<p data-sourcepos="5:1-5:2"><strong>WorkItem IDの取得</strong></p>
<p data-sourcepos="5:1-5:2" style="padding-left: 40px;">既存の <a  _istranslated="1" href="https://aps.autodesk.com/en/docs/design-automation/v3/reference/http/workitems-id-GET/" rel="noopener" target="_blank">GET workitems/:id</a> エンドポイントに新しいクエリーパラメータ startAfterTime が追加されました。startAfterTime パラメータを利用すると、指定した <a href="https://ja.wikipedia.org/wiki/UNIX%E6%99%82%E9%96%93" rel="noopener" target="_blank">UNIX 時間</a>&#0160;以降に起動した WorkItem の ID 一覧を JSON 内の配列で返します。</p>
<p data-sourcepos="5:1-5:2" style="padding-left: 80px;">メソッド：<strong>GET</strong><br />エンドポイント：<strong>https://developer.api.autodesk.com/da/us-east/v3/workitems</strong><br />クエリーパラメータ：<strong>startAfterTime=</strong>&lt;&lt;UNIX 時間&gt;&gt;<br />リクエスト ヘッダー：<strong>Authorization Bearer&#0160;</strong> &lt;&lt;アクセス トークン&gt;&gt;</p>
<ul>
<li>UNIX 時間は、<a href="https://www.epochconverter.com/" rel="noopener" target="_blank">Epoch Converter - Unix Timestamp Converter</a> のようなツールで得ることが出来ます。</li>
<li data-sourcepos="5:1-5:2">WorkItem のステータスは、WirkItem 完了後、3 日間のみ保持されます。</li>
</ul>
<p style="padding-left: 40px;">例：</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a8c401200b-pi" style="display: inline;"><img alt="Get" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a8c401200b image-full img-responsive" src="/assets/image_29780.jpg" title="Get" /></a></p>
<hr />
<p data-sourcepos="10:1-10:33"><strong>WorkItem ステータスの取得</strong></p>
<p data-sourcepos="10:1-10:33" style="padding-left: 40px;">複数の WorkItem ID からステータスを得るための新しいエンドポイントです。リクエスボディに WorkItem ID を配列指定すると、レポート URL を含むステータス等の情報が同じく配列で返されます。</p>
<p data-sourcepos="10:1-10:33" style="padding-left: 80px;">メソッド：<strong>POST</strong><br />エンドポイント：<strong>https://developer.api.autodesk.com/da/us-east/v3/workitems/status</strong><br />リクエスト ヘッダー：<strong>Authorization Bearer&#0160;</strong> &lt;&lt;アクセス トークン&gt;&gt;<br />リクエスト ヘッダー：<strong>Content-Type&#0160; application/json</strong><br />リクエスト ボディ： [<strong>&quot;workitem id1&quot;,&quot;workitem id2&quot;,&quot;workitem id3&quot;,</strong> ...]</p>
<ul>
<li>WorkItem のステータスは、WirkItem 完了後、3 日間のみ保持されます。</li>
</ul>
<p style="padding-left: 40px;">例：</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a4e199200c-pi" style="display: inline;"><img alt="Post" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a4e199200c image-full img-responsive" src="/assets/image_607716.jpg" title="Post" /></a></p>
<hr />
<p>By Toshiaki Isezaki</p>
