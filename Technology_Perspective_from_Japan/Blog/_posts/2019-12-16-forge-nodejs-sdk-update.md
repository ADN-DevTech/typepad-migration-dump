---
layout: "post"
title: "Forge Node.js SDK アップデート"
date: "2019-12-16 00:01:35"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2019/12/forge-nodejs-sdk-update.html "
typepad_basename: "forge-nodejs-sdk-update"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4d2e16a200d-pi" style="display: inline;"> </a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4d2e172200d-pi" style="float: right;"><img alt="Nodejs-forge-sdk" class="asset  asset-image at-xid-6a0167607c2431970b0240a4d2e172200d img-responsive" src="/assets/image_761836.jpg" style="margin: 0px 0px 5px 5px;" title="Nodejs-forge-sdk" /></a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4d2e16a200d-pi" style="display: inline;"></a>Node.js を利用した Web サーバー実装でお使いいただける <strong><a href="https://adndevblog.typepad.com/technology_perspective/2017/01/forge-sdk.html" rel="noopener" target="_blank">Forge SDK</a></strong> が更新されています。</p>
<p>今回の更新では、Data Management API のフィルター/ページネーション、<a href="https://forge.autodesk.com/en/docs/data/v2/reference/http/CheckPermission/" rel="noopener" target="_blank">コマンド API</a>、また、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2018/03/bim-360-docs-and-forge-oauth.html" rel="noopener" target="_blank">BIM 360 Docs と Forge OAuth </a></strong>のような用途でお使いいただける 2-legged OAuth を利用したアクセスをサポートしています。</p>
<p>新しいバージョン（0.5.0）は、0.4.8コードベースのマイナーリライトです。 <span style="font-family: tahoma, arial, helvetica, sans-serif;">コードはこのバージョンでもそのまま実行することが出来ます。新しい</span> API 機能を使用する場合には、新しいメソッドを使用する必要があります。</p>
<p>Forge Node.js SDK をお使いの場合は、GitHub リポジトリをご確認ください。</p>
<p style="text-align: center;"><a href="https://github.com/Autodesk-Forge/forge-api-nodejs-client" rel="noopener" target="_blank"><span style="font-size: 14pt; font-family: tahoma, arial, helvetica, sans-serif;"><strong>https://github.com/Autodesk-Forge/forge-api-nodejs-client</strong></span></a></p>
<p>なお、この Forge Node.js SDK に含まれる Design Automation API v2 実装の利用は、<a href="https://adndevblog.typepad.com/technology_perspective/2019/10/design-automation-api-v3-release.html" rel="noopener" target="_blank">D<strong>esign Automation API v3 の正式サポート開始</strong></a>に沿って、もはや推奨されません。事実、新しく Forge App を作成する際には、Design Automation API v3 への移行を促進する目的で、使用する API に Design Automation API v2 を選択することが出来なくなっています。なお、これらの処置は、直ちに Design Automation API v2 を利用した Forge App 運用停止を意味するわけではありませんのでご注意ください。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4d0ed6e200d-pi" style="display: inline;"><img alt="Dav2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4d0ed6e200d image-full img-responsive" src="/assets/image_270650.jpg" title="Dav2" /></a></p>
<p>Node.js で Design Automation API v3 をお使いいただく場合には、次の URL で NPM パッケージを入手出来ます。</p>
<p style="text-align: center;"><a href="https://www.npmjs.com/package/autodesk.forge.designautomation" rel="noopener" target="_blank"><strong><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 14pt;">https://www.npmjs.com/package/autodesk.forge.designautomation</span></strong></a></p>
<p>By Toshiaki Isezaki</p>
