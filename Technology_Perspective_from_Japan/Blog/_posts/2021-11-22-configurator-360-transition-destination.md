---
layout: "post"
title: "Configurator 360 の移行先"
date: "2021-11-22 00:00:55"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/11/configurator-360-transition-destination.html "
typepad_basename: "configurator-360-transition-destination"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02788058d5f1200d-pi" style="display: inline;"><img alt="Ransition" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02788058d5f1200d image-full img-responsive" src="/assets/image_125888.jpg" title="Ransition" /></a></p>
<p class="shortdesc">少し前になりますが、2020 年 5 月、オートデスクは Autodesk Configurator 360 の<strong><a href="https://www.autodesk.com/products/configurator-360/overview" rel="noopener" target="_blank">新規販売を終了</a></strong>しています。<strong><a href="https://knowledge.autodesk.com/ja/support/inventor-products/learn-explore/caas/CloudHelp/cloudhelp/2016/JPN/Inventor-Help/files/GUID-1AB75A16-1E6A-4CB6-8586-820F7982C2EC-htm.html" rel="noopener" target="_blank">Configurator 360</a></strong> は、Autodesk Inventor で作成した製品モデルを使った Web ベースのコンフィギュレーター アプリを構築し、Web やモバイル端末から製品の仕様設定をおこなうためのクラウド サービスです。</p>
<p class="shortdesc">現在、Autodesk Configurator 360 サイト（<a href="https://configurator360.autodesk.com/" rel="noopener" target="_blank">https://configurator360.autodesk.com/</a>）にサインインすると、次のように表示されるはずです。</p>
<p class="shortdesc"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e13172ae200b-pi" style="display: inline;"><img alt="Cannot_createnew_account" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e13172ae200b image-full img-responsive" src="/assets/image_359370.jpg" title="Cannot_createnew_account" /></a></p>
<p>つまり、この Configurator 360 の後継となる製品はなく、Forge の <strong><a href="https://forge.autodesk.com/api/design-automation-cover-page/" rel="noopener" target="_blank">Design Automation API for Inventor</a></strong> を使って代替アプリを開発していただく、という方向です。Web インタフェースの作成やコンフィギュレーターの内部実装も、既成製品よりも自由度を高めることが出来る、というのが理由です。</p>
<p>といっても、すぐにプログラムを開発してコンフィギュレーター アプリを構築するのはハードルが高いのも事実です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e1317255200b-pi" style="display: inline;"><img alt="High_hardle" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e1317255200b image-full img-responsive" src="/assets/image_972238.jpg" title="High_hardle" /></a></p>
<p>そんな方のために、ひな形となりうる Github リポジトリが用意されています。前述の Autodesk Configurator 360 サイトを一度サインアウトして、右上の「ニュースグループ」をクリック後、一覧から「<strong>Example for migrating to Forge Design Automation available</strong>」のタイトルのフォーラム トピックをクリックしてみてください。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e13172a1200b-pi" style="display: inline;"><img alt="Sign_out" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e13172a1200b image-full img-responsive" src="/assets/image_465967.jpg" title="Sign_out" /></a></p>
<p>GitHub リポジトリの<a href="https://forums.autodesk.com/t5/configurator-360-forum/example-for-migrating-to-forge-design-automation-available/td-p/9881973" rel="noopener" target="_blank">リンク</a>が記載されているはずです。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e1317308200b-pi" style="display: inline;"><img alt="Link_to_github" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e1317308200b image-full img-responsive" src="/assets/image_660848.jpg" title="Link_to_github" /></a></p>
<p>この GitHub リポジトリの内容は、以前、<a href="https://adndevblog.typepad.com/technology_perspective/2021/03/efficient-demo-sites-to-know-forge.html" rel="noopener" target="_blank"><strong>Forge を知る効果的なデモ サイト</strong></a> のブログ記事でご紹介した&#0160;<a href="https://inventor-config-demo.autodesk.io/" rel="noopener" target="_blank"><strong>https://inventor-config-demo.autodesk.io/</strong></a> での内容であることがわかります。</p>
<p><a class="asset-img-link" href="https://inventor-config-demo.autodesk.io/" rel="noopener" target="_blank"><img alt="Da4i" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdec4ff6b200c image-full img-responsive" src="/assets/image_737655.jpg" title="Da4i" /></a></p>
<p>同リポジトリは <strong><a href="https://ja.wikipedia.org/wiki/MIT_License" rel="noopener" target="_blank">MIT ライセンス</a></strong>を採用しているので、そのままの状態でも、また、ローカル環境で修正を加えてからでも、独自のドメインでデプロイして評価することが出来ます。&#0160;</p>
<p>By Toshiaki Isezaki</p>
