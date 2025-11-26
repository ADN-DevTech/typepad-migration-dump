---
layout: "post"
title: "APS Developer ハブ レベルの使用状況レポート"
date: "2025-08-12 00:12:00"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2025/08/introducing-hub-level-usage-reporting-aps-developer-hubs.html "
typepad_basename: "introducing-hub-level-usage-reporting-aps-developer-hubs"
typepad_status: "Publish"
---

<div class="field field--name-field-primary-image field--type-image field--label-hidden field__item"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e86107a3d5200d-pi" style="display: inline;"><img alt="Placeholder - Blog images_Lifestyle 16x9 1920x1080" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e86107a3d5200d image-full img-responsive" src="/assets/image_603490.jpg" title="Placeholder - Blog images_Lifestyle 16x9 1920x1080" /></a></div>
<div class="clearfix text-formatted field field--name-field-primary-image-caption field--type-text-long field--label-hidden field__item">
<p>Developer ハブに&#0160;<strong>ハブ</strong>&#0160;<strong>レベルの使用状況レポート&#0160;</strong>の機能が追加されました。Developer ハブの管理者は、ハブ内のすべてのアプリのトークン、および API の使用状況を表示出来るようになります。この機能強化は、APS コストの可視性と制御を管理者に提供する継続的な取り組みの一環です。</p>
<p>今回のリリースでは、ハブ管理者は、APS Developer ポータルから集計された使用状況に直接アクセス出来るようになりました。新しい UI では、次の 2 つの専用タブが導入されています。</p>
<ul>
<li><strong>Token usage：</strong>ハブ内のすべてのアプリについて、合計のトークン消費量を表示します。</li>
<li><strong>API usage：</strong>Automation API（旧名 Design Automation API）、Model Derivative API、Reality Capture API、フFlow Graph Engine API、Data Management API、ACC Issues API、BIM 360 Isuues API など、API ごとに消費されたユニット数を監視します。</li>
</ul>
<p>この UI は、<a href="https://adndevblog.typepad.com/technology_perspective/2024/07/token-usage-report-per-application.html" rel="noopener" target="_blank">アプリ レベルのレポート</a>の使い慣れたユーザー体験を反映しており、API とサブユニット（エンジンやジョブの種類など） でフィルタリングするための直感的なドロップダウンを備えています。カスタム期間を設定することも可能で、すべてのフィルターがダウンロード可能な CSV レポートに反映されます。</p>
<h3>ハブ レベルの使用状況レポートが重要なのはなぜですか?</h3>
<p>以前は、管理者はアプリごとに使用状況を確認する必要があり、APS 消費の全体像を把握することが困難でした。今回の拡張で実現した機能は次のとおりです。</p>
<ul>
<li>ハブ全体の合計使用量を即座に確認できます。</li>
<li>アプリケーション、API 参照名、サブユニット、日付範囲でフィルタリングして、詳細なインサイトを得ることができます。</li>
<li>内部分析やコスト最適化のための包括的なレポートをダウンロードしてください。</li>
<li>この機能は、組織が成長し、より複雑な APS 統合を採用するにつれて特に価値があり、管理者が使用量をプロアクティブに管理し、コストを管理できるようになります。</li>
</ul>
<h3>ハブ レベルの使用状況レポートにアクセスする方法</h3>
<ol>
<li>APS Developer ポータルにログインします。</li>
<li>Developer ハブに移動します。（下図 ① をクリック）</li>
<li>左手に表示される <strong>Hub analytics</strong> セクションをクリックします。（下図 ② をクリック）</li>
<li><strong>Token usage</strong> または<strong>API usage</strong> タブを選択します。</li>
<li>必要に応じてフィルターを適用し、レポートをダウンロードしてさらに分析を進めることが出来ます。&#0160;</li>
</ol>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860f0ca59200b-pi" style="display: inline;"><img alt="Hub_analytics" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860f0ca59200b image-full img-responsive" src="/assets/image_291011.jpg" title="Hub_analytics" /></a></p>
<h3>よくある質問</h3>
<p><strong>Q: この機能にアクセス出来るのは誰ですか?</strong></p>
<p>この機能は、Developer ハブの一部であるユーザーのみが利用できます。今年初めに Developer ハブをリリースし、リクエストに応じて機能を有効にしています。ハブの詳細については、<a href="https://adndevblog.typepad.com/technology_perspective/2025/02/aps-developer-hubs.html" rel="noopener" target="_blank">APS Developer ハブについて</a>&#0160;の記事をご確認ください。</p>
<p style="text-align: center;"><span style="background-color: #111111; color: #ffffff;"><strong>&#0160;<a  _msthash="108"  _msttexthash="51330500" class="button button--default button--medium" data-wat-cta="Submit your interest for Developer Hubs" href="https://autodeskfeedback.az1.qualtrics.com/jfe/form/SV_9sfUcRVwKCOnJ6C" rel="noopener" role="button" style="background-color: #111111; color: #ffffff;" target="_blank">Developer Hubsに関心を送信する</a>&#0160;</strong></span></p>
<p><strong>Q: タブにはどのような使用状況が表示されますか?</strong></p>
<p>ハブ レベルの使用状況は、ハブに存在するアプリのプレミアム API（課金対象 API）の使用状況のみを示します。ハブに移動していないアプリは、このレポートには表示されません。&#0160;</p>
<p>※ 本記事は <a href="https://aps.autodesk.com/blog/introducing-hub-level-usage-reporting-aps-developer-hubs" rel="noopener" target="_blank">Introducing Hub Level Usage Reporting for APS Developer Hubs | Autodesk Platform Services</a>&#0160;から転写・意訳・補足したものです。</p>
<p>By Toshiaki Isezaki</p>
</div>
