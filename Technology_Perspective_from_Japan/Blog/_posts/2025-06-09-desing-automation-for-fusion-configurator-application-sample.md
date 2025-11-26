---
layout: "post"
title: "Desing Automation for Fusion コンフィギュレーター サンプルアプリケーション"
date: "2025-06-09 01:03:23"
author: "Takehiro Kato"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2025/06/desing-automation-for-fusion-configurator-application-sample.html "
typepad_basename: "desing-automation-for-fusion-configurator-application-sample"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860ec2a9d200b-pi" style="display: inline;"><img alt="Titile" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860ec2a9d200b img-responsive" src="/assets/image_681610.jpg" title="Titile" /></a></p>
<p>先のブログ記事「<a href="https://adndevblog.typepad.com/technology_perspective/2025/06/design-automation-api-for-fusion-generally-available.html">Design Automation API for Fusion : 一般リリース</a>」でご案内しましたようにDesing Automation for Fusionが一般リリースとなっております。</p>
<p>Desing Automation for Fusionを活用したサンプルアプリケーションとして、コンフィギュレーター サンプルアプリケーション が公開されております。：<a href="https://fusion-config-demo.autodesk.io/">https://fusion-config-demo.autodesk.io/</a></p>
<p>&#0160;</p>
<p>非常に単純なサンプルアプリケーションですが、Design Automation for Fusion の機能を理解するのに十分な機能を有しております。</p>
<p>Design Automation for Fusionのデモアプリケーションでは、Design Automation for Fusionのコアエンジンが、クラウド上に保存されているFusionモデルにアクセスするために、自身のAutodesk Accountでログインの上、パーソナルアクセストークン（PAT、 Personal Access Toke)を、アプリケーションの右上のボックスに入力して指定する必要があります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e86103126d200d-pi" style="display: inline;"><img alt="PAT" class="asset  asset-image at-xid-6a0167607c2431970b02e86103126d200d img-responsive" src="/assets/image_233742.jpg" title="PAT" /></a></p>
<p>PATは、https://profile.autodesk.com/security にアクセスし、パーソナル アクセス トークン（Personal Access Token、PAT ）を生成して利用することが出来ます。このデモアプリケーションで利用する場合、パーソナル アクセス トークンの生成時のProduct Scopeに「Desing Automation for Fusion」を指定してPATを生成する必要があります。</p>
<p>なお、生成したパーソナル アクセス トークンは作成時に表示されますが、その後は参照をすることが出来ないことにご留意ください。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860ec2aaa200b-pi" style="display: inline;"><img alt="GeneratePAT" class="asset  asset-image at-xid-6a0167607c2431970b02e860ec2aaa200b img-responsive" src="/assets/image_818792.jpg" title="GeneratePAT" /></a></p>
<p>このサンプルは、ハブとプロジェクトおよび保存されているデザインへのアクセスを提供する <a href="https://get-started.aps.autodesk.com/tutorials/hubs-browser/">Hubs Browser チュートリアル</a>に基づいています。</p>
<p>Hubs Browserチュートリアルについては、先日行いましたオンライントレーニング<a href="https://adndevblog.typepad.com/technology_perspective/2025/05/aps-online-training-hub-browser-rmaterials.html">APS Online Training：Hub Browser 収録公開</a>にてプレゼンテーション資料および収録動画を参照することが出来ますので、是非ご参照ください。</p>
<p>&#0160;</p>
<p>このサンプルアプリケーションでは Design Automation for Fusion を使用して、ツリーで選択したFusion モデルからすべてのパラメータを抽出し、Viewerのパネルの表示します。</p>
<p>また、パネル上で抽出したパラメータの値を変更した後に「Update Desing」をクリックすると、Design Automation for Fusionでパラメータ変更処理を起動し、元のモデルと同じ名前に GUID が追加された新しいモデルが保存され、モデルの準備が整うとViewer に読み込まれます。</p>
<p>&#0160;</p>
<p>コンフィギュレーター サンプルアプリケーションのソースコードは、<a href="https://github.com/autodesk-platform-services/aps-configurator-fusion/tree/main?tab=readme-ov-file">GitHubリポジトリ</a>にて公開されております。Desing Automation for Fusionのより詳細な動作を把握したい場合は、ソースコードを取得してご参照ください。</p>
<p>&#0160;</p>
<p>※ 本記事は<a href="https://aps.autodesk.com/blog/fusion-configurator-sample"> Fusion configurator sample | Autodesk Platform Services</a> から転写・意訳、補足したものです。</p>
<p>By Takehiro Kato</p>
