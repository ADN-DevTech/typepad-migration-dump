---
layout: "post"
title: "Internet Explorer 11 サポート終了と Forge"
date: "2021-11-24 00:02:38"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/11/internet-explorer-11-support-end-and-forge.html "
typepad_basename: "internet-explorer-11-support-end-and-forge"
typepad_status: "Publish"
---

<p>来年 6 月 15 日で Internet Explorer 11 のサポートを終了することが Microsoft 社によって<strong><a href="https://blogs.windows.com/japan/2021/05/19/the-future-of-internet-explorer-on-windows-10-is-in-microsoft-edge/" rel="noopener" target="_blank">アナウンス</a></strong>されています。Internet Explorer 11 （以下 IE11）は、各種システムのフロントエンドとして、多くの企業で業務アプリに利用されてきました。</p>
<p>ただ、Microsoft 社は主要 Web ブラウザに <strong><a data-linktype="external" href="https://www.microsoft.com/edge/business?form=MO12H3&amp;OCID=MO12H3" rel="noopener" target="_blank">Microsoft Edge</a></strong> を登場させて、その利用を推奨しています。以前、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2019/03/capabilities-evaluation-for-web-browsers.html" rel="noopener" target="_blank">Web ブラウザの能力評価</a></strong> のブログ記事でもご紹介したように、IE11 は&#0160;<strong><a href="https://html5test.com/" rel="noopener" target="_blank">HTML5 TEST（https://html5test.com/）</a></strong>サイトでのスコア評価も低くなる傾向があります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e131b468200b-pi" style="display: inline;"><img alt="Html5_1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e131b468200b image-full img-responsive" src="/assets/image_686034.jpg" title="Html5_1" /></a></p>
<p>Forge アプリのフロントエンドとして IE11 の利用を考えた場合、現時点でも Internet Explorer 11 のサポートが<a href="https://forge.autodesk.com/en/docs/viewer/v7/developers_guide/overview/" rel="noopener" target="_blank"><strong>明記</strong></a>しています。しかしながら、IE11 が採用する JavaScript の仕様がいくぶん古いせいもあり、Forge Viewer 用にカスタム処理を施した JavaScript コードが正しく動かない場合があります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e131b470200b-pi" style="display: inline;"><img alt="Html5_2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e131b470200b image-full img-responsive" src="/assets/image_89593.jpg" title="Html5_2" /></a></p>
<p>セキュリティの懸念もあるので、Forge アプリ新規開発で IE11 をターゲットにすることはないかと思いますが、現在、IE11 をお使いの場合には、なるべく早く他のブラウザへ移行することをお勧めします。ご参考まで、Forge Viewer を内部利用している Autodesk Drive では、すでにサポート ブラウザから<a href="https://help.autodesk.com/view/DRIVE/JPN/?guid=GUID-C00E7ABC-1C67-412D-8271-965B710B8F48" rel="noopener" target="_blank"><strong> IE11 が外れています</strong></a>。</p>
<p>By Toshiaki Isezaki</p>
