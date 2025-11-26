---
layout: "post"
title: "Flow Graph Engine API 一般提供開始"
date: "2024-11-27 00:01:17"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/11/general-availability-of-flow-graph-engine-api-.html "
typepad_basename: "general-availability-of-flow-graph-engine-api-"
typepad_status: "Publish"
---

<p>ご案内が遅くなってしまいましたが、<a href="https://adndevblog.typepad.com/technology_perspective/2024/05/autodesk-flow-graph-rngine-service.html" rel="noopener" target="_blank">Autodesk Flow のグラフ エンジン サービス</a>&#0160;でご紹介した Flow Graph Engine API が Beta を卒業して一般提供が開始されました。</p>
<p>Flow Graph Engine API は、従来、ローカル環境でしか実現出来なかった Autodesk Maya の<a href="https://www.autodesk.com/jp/products/maya/bifrost" rel="noopener" target="_blank"> Bifrost グラフ</a> ジョブの実行を、クラウドで実行させる演算処理能力を提供します。その意味で、Design Automation API の演算処理に似ていると考えることが出来ます。</p>
<p>今回の一般提供が開始を受けて、稼働状況を示すヘルス ダッシュボード（<a href="https://health.autodesk.com/" rel="noopener" target="_blank">https://health.autodesk.com/</a>、および、<a href="https://health.autodesk.com/?search=APS" rel="noopener" target="_blank">https://health.autodesk.com/?search=APS</a>）に、Flow Graph Engine API が追加されています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3c3310e200c-pi" style="display: inline;"><img alt="Hds" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3c3310e200c img-responsive" src="/assets/image_507952.jpg" title="Hds" /></a></p>
<p>クラウド リソースを利用する演算処理サービスであるため、利用時には Autodesk Flex を使った従量課金の対象となります。消費トークンは、1 時間あたりの処理時間に対して 1.0 トークンとなります。 これには、素材ファイルのダウンロード時間と成果ファイルのアップロード時間も含まれます。この点も <a href="https://adndevblog.typepad.com/technology_perspective/2020/02/estimate-design-automation-costs.html" rel="noopener" target="_blank">Design Automation API</a> と同様です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860d9b1bb200b-pi" style="display: inline;"><img alt="Pricing" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860d9b1bb200b image-full img-responsive" src="/assets/image_916231.jpg" title="Pricing" /></a></p>
<p>By Toshiaki Isezaki</p>
