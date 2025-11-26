---
layout: "post"
title: "OSS のアップロード/ダウンロード時の Content-Type ヘッダに関する変更点【セキュリティ向上】"
date: "2022-01-20 00:13:38"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/01/changes-oss-upload-and-download-content-type-header-security-improvement-2022-1.html "
typepad_basename: "changes-oss-upload-and-download-content-type-header-security-improvement-2022-1"
typepad_status: "Publish"
---

<div class="node__image"><strong>2月7日</strong>より、OSS アップロードの PUT endpoint は、Content-Disposition : inline と Content-Type が以下のいずれかである場合、HTTP 400 ステータス&#0160; コードを返すようになります。</div>
<div class="node__body">
<div class="field-body">
<ul>
<li><strong>text/xml, application/xml</strong></li>
</ul>
<p>既にアップロード済みのファイル ダウンロード時にも、HTTP 400 のステータスコードが返されるように変更されました。これは、<a href="https://forge.autodesk.com/blog/changes-oss-upload-and-download-content-type-header-security-improvement" rel="noopener" target="_blank">2019年10月</a> および <a href="https://adndevblog.typepad.com/technology_perspective/2019/05/oss-upload-and-download-for-scriptable-plain-text-files.html" rel="noopener" target="_blank">2019年5月</a> にアナウンスされた、以下の Content-Type に追加されるものです。</p>
<ul>
<li><strong> <a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e13efccb200b-pi" style="float: right;"><img alt="Security" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e13efccb200b img-responsive" src="/assets/image_425256.jpg" style="margin: 0px 0px 5px 5px;" title="Security" /></a>text/html, text/javascript, text/x-javascript,</strong></li>
<li><strong>application/javascript, application/x-javascript</strong></li>
<li><strong>application/xhtml+xml</strong></li>
<li><strong>image/svg+xml</strong></li>
</ul>
<div class="result-shield-container tlid-copy-target" tabindex="0"><span class="tlid-translation translation" lang="ja"><span class="" title="">この変更は他のコンテンツ処理フォーマットには影響しません。ご質問等ございましたら、<a href="https://forge.autodesk.com/en/support/get-help" rel="noopener" target="_blank">https://forge.autodesk.com/en/support/get-help</a></span></span></div>
<div class="result-shield-container tlid-copy-target" tabindex="0">をご利用ください。</div>
<div class="result-shield-container tlid-copy-target" tabindex="0">&#0160;</div>
<div class="result-shield-container tlid-copy-target" tabindex="0">By Toshiaki Isezaki</div>
</div>
</div>
