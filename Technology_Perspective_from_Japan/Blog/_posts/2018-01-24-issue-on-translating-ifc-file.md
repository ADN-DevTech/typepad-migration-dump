---
layout: "post"
title: "IFC ファイルの変換の問題について"
date: "2018-01-24 00:04:40"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2018/01/issue-on-translating-ifc-file.html "
typepad_basename: "issue-on-translating-ifc-file"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09ebdf09970d-pi" style="float: right;"><img alt="Icon - model derivative api" class="asset  asset-image at-xid-6a0167607c2431970b01bb09ebdf09970d img-responsive" src="/assets/image_702442.jpg" style="margin: 0px 0px 5px 5px;" title="Icon - model derivative api" /></a>Forge をお使いの一部の開発者の方から、Model Derivative API を使った IFC ファイル から SFVファイル（Viewer）への変換処理において、予期しない変更が報告されています。</p>
<p>報告されている変更は次のとおりです。</p>
<ol>
<li>マテリアルとテクスチャは抽出されない。</li>
<li>作成された SVF のモデルの向きが、一部のソースから IFC ファイルに対して変更されている。</li>
<li>モデルツリー構造が変更されている。</li>
</ol>
<p>現在、これらの問題は最優先事項として開発チームが調査委しています。 解決策を見つかった時点で、このブログに更新情報を掲載します。 これらの変更により生じた問題についてお詫び申し上げます。&#0160;</p>
<p>By Toshiaki Isezaki</p>
