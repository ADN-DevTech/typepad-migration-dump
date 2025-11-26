---
layout: "post"
title: "Design Automation API for AutoCAD：AppBundle 内のコンテンツ利用"
date: "2021-10-25 00:19:23"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/10/use-of-contents-in-appbundle.html "
typepad_basename: "use-of-contents-in-appbundle"
typepad_status: "Publish"
---

<p>Design Automation API for AutoCAD を利用する場合、パラメータによって、AppBundle に同梱したファイルを変化させて異なる内容の成果ファイルを作成したいことがあります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b027880525555200d-pi" style="display: inline;"><img alt="Configurator" border="0" class="asset  asset-image at-xid-6a0167607c2431970b027880525555200d image-full img-responsive" src="/assets/image_484023.jpg" title="Configurator" /></a></p>
<p>例えば、パターン別に複数のレンダリング画像を Rendering フォルダに用意、AppBundle に同梱して、パラメータによってレンダリング画像を使い分けるような場面を想定してみます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b027880513676200d-pi" style="display: inline;"><img alt="Explorer" border="0" class="asset  asset-image at-xid-6a0167607c2431970b027880513676200d image-full img-responsive" src="/assets/image_192943.jpg" title="Explorer" /></a></p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdef95cda200c-pi" style="display: inline;"><img alt="Appbundle" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdef95cda200c image-full img-responsive" src="/assets/image_435204.jpg" title="Appbundle" /></a></p>
<p>WorkItem 実行時、Design Automation API は AppBundle 内のファイルをフラットに実行環境のルートフォルダに転換するわけではありません。サブフォルダで同梱したファイルは、展開されたアドインのパスを使って絶対パスとして解決するのが現実的です。</p>
<p>Rendering フォルダに同梱したレンダリング画像をアドインからパス解決して図面に貼り付けるには、次のようなアドインコードが必要です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e129a688200b-pi" style="display: inline;"><img alt="Addin_code" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e129a688200b image-full img-responsive" src="/assets/image_501824.jpg" title="Addin_code" /></a></p>
<p>これにより、適切にレンダリング画像を使い分けることが出来ます。AppBundle に同梱したブロック図面を挿入するような場面でも、この方法を利用することが出来ます。</p>
<p>By Toshiaki Isezaki</p>
