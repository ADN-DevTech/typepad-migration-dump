---
layout: "post"
title: "進化する Design Automation API"
date: "2021-11-08 00:16:00"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/11/evolving-design-automation-api.html "
typepad_basename: "evolving-design-automation-api"
typepad_status: "Publish"
---

<p>Forge API の 1 つである Design Automation API は、他の API 同様、継続した改善、改良をおこなっています。</p>
<p>ここ最近の改良では、Design Automation API for Revit のパフォーマンスが、次の 2 点で改善されています。</p>
<ul>
<li>Design Automation API では、WorkItem のリクエスト時、すべてのリクエストはキューに蓄積されて、実行環境が仮想マシン（AMI）により動的に作成されて処理されます。<br /><br />この際、短い時間に多くのリクエストが集中してしまうと、仮想マシンの確立（スピンアップ）に時間がかかってしまい、pending レスポンスが返るような場面が見られことがあります。Design Automation API for Revit では、仮想マシンのスピンアップの待ち時間が短縮するよう改善を加えて、WorkItem の処理時間全体を短縮する努力を続けています。<br /><br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdef79521200c-pi" style="display: inline;"><img alt="Elastic2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdef79521200c image-full img-responsive" src="/assets/image_956686.jpg" title="Elastic2" /></a><br /><br /></li>
<li>Design Automation API for Revit が WorkItem 毎に使用する仮想マシンの仕様が強化され、最大メモリ量の合計が、従来の 16GB から 32GB に増加しています。これにより、大規模な Revit プロジェクトを使用した AppBundle（アドイン）の実行パフォーマンスが従来より改善しているはずです。</li>
</ul>
<p>この他、Design Automation API がサポートするすべてのコアエンジン（UI のない AutoCAD、Revit、Inventor、3ds Max）で、WebSocket を使った WorkItem リクエストと通知メカニズムがサポートされるようになりました。&#0160;</p>
<ul>
<li>従来、WorkItem リクエストは、RESTful API として用意されている <strong><a href="https://forge.autodesk.com/en/docs/design-automation/v3/reference/http/workitems-POST/" rel="noopener" target="_blank">POST workItems</a></strong> endpoint 呼び出しでおこなっていました、また、WorkItem 処理の進捗状況と完了には OnProgress、OnComplete コールバックによって通知を得ることが出来ました。<br /><br />ただ、コールバックは URL 指定が必要なため、Web サーバー実装を介した実装が必須になってしまいます。このため、クライアントが WorkItem の進捗と完了を知るには、通常、<a href="https://forge.autodesk.com/en/docs/design-automation/v3/reference/http/workitems-id-GET/" rel="noopener" target="_blank"><strong>GET workitems/:id</strong></a> endpoint を使ったポーリング処理を、Web サーバー上にルーティングした endpoint を介して検出しているかと思います。</li>
</ul>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278804f796f200d-pi" style="display: inline;"><img alt="Rest_comminication" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0278804f796f200d image-full img-responsive" src="/assets/image_241428.jpg" title="Rest_comminication" /></a></p>
<p style="padding-left: 40px;">今回、この処理に <a href="https://ja.wikipedia.org/wiki/WebSocket" rel="noopener" target="_blank"><strong>WebSocket</strong></a> を用いた API が追加されています。具体的には、クライアントとして使用するデバイスと Forge サーバーとの間にリンクを確立して、直接、WorkItem のリクエストや進捗、完了通知を得ようとするものです。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e127e7d8200b-pi" style="display: inline;"><img alt="Socket_comminication" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e127e7d8200b image-full img-responsive" src="/assets/image_298320.jpg" title="Socket_comminication" /></a></p>
<p style="padding-left: 40px;">これによって、冗長なコミュニケーションの低減出来るようになります。ただ、リンクの確立はデバイス毎になってしまうため、RESTful 呼び出しと相対比較して十分な効果が得られるか、テストと評価をお勧めしています。</p>
<ul>
<li>WebSocket API の使用が常にベストなわけではありません。また、従来の RESTful API&#0160; を使った運用を置き換えていくものでもありません。既に、Web サーバー実装で OnComplete コールバックを運用している場合には、特に明示的な理由がない限り、WebSocket API に移行する必要性はありません。</li>
<li>WebSocket API を利用した場合でも、pending ステータスの状況がなくなる訳ではありません。</li>
</ul>
<p style="padding-left: 40px;">Design Automation API・WebSocket APIの詳細は、Forge ポータルのドキュメントをご確認ください。</p>
<p style="padding-left: 40px;"><strong>Developer&#39;s Guide：</strong><br /><strong><a href="https://forge.autodesk.com/en/docs/design-automation/v3/developers_guide/websocket-api/" rel="noopener" target="_blank">https://forge.autodesk.com/en/docs/design-automation/v3/developers_guide/websocket-api/</a></strong></p>
<p style="padding-left: 40px;"><strong>Reference：</strong><br /><strong><a href="https://forge.autodesk.com/en/docs/design-automation/v3/reference/websocket/" rel="noopener" target="_blank">https://forge.autodesk.com/en/docs/design-automation/v3/reference/websocket/</a></strong></p>
<p style="padding-left: 40px;"><a href="https://web.autocad.com/" rel="noopener" target="_blank"><strong>AutoCAD Web アプリ</strong></a>の PDF 出力機能では、ここでご紹介した Design Automation API の WebSocket API が内部的に使用されています。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdef79d7c200c-pi" style="display: inline;"><img alt="Autocad_web_pdf_plot" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdef79d7c200c image-full img-responsive" src="/assets/image_939740.jpg" title="Autocad_web_pdf_plot" /></a></p>
<p>By Toshiaki Isezaki</p>
