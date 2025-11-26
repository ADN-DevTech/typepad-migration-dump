---
layout: "post"
title: "Design Automation API v3 リリース"
date: "2019-10-16 00:04:03"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2019/10/design-automation-api-v3-release.html "
typepad_basename: "design-automation-api-v3-release"
typepad_status: "Publish"
---

<p>Design Automation API v3（バージョン 3）が 10 月 28 日（日本時間 29 日）に正式にリリースされることが<strong><a href="https://forge.autodesk.com/blog/design-automation-api-revit-inventor-3ds-max-release-october-28" rel="noopener" target="_blank">アナウンス</a></strong>されました。これにともない、<strong><a href="https://forge.autodesk.com" rel="noopener" target="_blank">Forge ポータル</a></strong> の<strong><a href="https://forge.autodesk.com/pricing" rel="noopener" target="_blank"> Pricing ページ（Forge 課金情報ページ）</a></strong>が更新されましたので、ご案内いたします。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4dfb9f2200b-pi" style="display: inline;"><img alt="New_forge_pricing" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4dfb9f2200b image-full img-responsive" src="/assets/image_945053.jpg" title="New_forge_pricing" /></a></p>
<p>着目いただきたいのは、Design Automation API v3（バージョン 3）の消費クラウド クレジットが初めて公開された点です。今まで、Design Automation API <strong>v2</strong> （バージョン 2）では、サポートされるコアエンジンが<span style="text-decoration: underline;"> AutoCAD のみ</span>で、1 CPU 時間当たりの消費クラウド クレジットが 4 クラウド クレジットに設定されていました。</p>
<p>Design Automation API <strong>v3</strong> では、サポートされるコアエンジンに <span style="text-decoration: underline;">Revit、Inventor、3ds Max が加わり</span>、1 CPU 時間当たりの消費クラウド クレジットが<strong> 6 クラウド クレジット</strong>に設定されています。<span style="text-decoration: underline;">AutoCAD のみ</span>、従来通り、 CPU 時間たりの消費クラウド クレジットが <strong>4 クラウド クレジット</strong>になっている点にご注意ください。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4deac56200b-pi" style="display: inline;"><img alt="Consumed_cloudcredits" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4deac56200b image-full img-responsive" src="/assets/image_261879.jpg" title="Consumed_cloudcredits" /></a></p>
<p>Design Automation API for AutoCAD v3 と Design Automation API for Revit /Inventor / 3ds Max v3 で消費クラウド クレジット数に差があるのは、大きく次の要因によるものです。</p>
<ul>
<li>Design Automation API for AutoCAD は、しばらく、v2 と v3 を平行してお使いいただくことが出来るため、v3 への移行期のビジネス上の混乱を避けるため。</li>
<li>Design Automation API for Revit /Inventor / 3ds で使われている仮想環境は、コアエンジンの仕様から、Design Automation API for AutoCAD の仮想環境に比べて多くのリソース（メモリなど）を使用しているため。（オートデスクも AWS から従量課金されている）</li>
</ul>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4dead1b200b-pi" style="display: inline;"><img alt="Da_v3" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4dead1b200b image-full img-responsive" src="/assets/image_194843.jpg" title="Da_v3" /></a></p>
<p>なお、オートデスクは、Design Automation API for AutoCAD v2 をお使いの方に、v3 への移行を<strong>強く</strong>お勧めしています。Design Automation API for AutoCAD v2&#0160; の endpoint と、Design Automation API for AutoCAD v3 の endpoint は異なるので、移植作業が必須となりますが、v3 間では endpoint が共通であるため、他のコアエンジン利用時などみ経験を活かすことが出来ます。&#0160;</p>
<p>By Toshiaki Isezaki</p>
