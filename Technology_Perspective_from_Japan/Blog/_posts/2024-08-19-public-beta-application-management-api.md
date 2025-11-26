---
layout: "post"
title: "Public Beta：Application Management API について"
date: "2024-08-19 02:19:19"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/08/public-beta-application-management-api.html "
typepad_basename: "public-beta-application-management-api"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3b696ac200c-pi" style="display: inline;"> </a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3b6ed6c200c-pi" style="display: inline;"><img alt="Aps2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3b6ed6c200c image-full img-responsive" src="/assets/image_525657.jpg" title="Aps2" /></a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3b696ac200c-pi" style="display: inline;"><br /></a></p>
<p>まだ、Public Beta の扱いになりますが、Autodesk Platform Services（APS）に新しく Application Management API が加わりました。Application Management API は、APS アプリを管理する方法を提供します。この API を利用すると、APS ポータルでアプリ管理を自動化することが出来るようになります。</p>
<h2>Application Management API の機能とは？</h2>
<ul role="list">
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="1" data-font="Symbol" data-leveltext="" data-list-defn-props="{&quot;335552541&quot;:1,&quot;335559685&quot;:720,&quot;335559991&quot;:360,&quot;469769226&quot;:&quot;Symbol&quot;,&quot;469769242&quot;:[8226],&quot;469777803&quot;:&quot;left&quot;,&quot;469777804&quot;:&quot;&quot;,&quot;469777815&quot;:&quot;multilevel&quot;}" data-listid="1">
<p><strong>APS アプリをリストする：<br /></strong>自分が所有者または閲覧または編集権限を持つコラボレーターであるアプリの一覧を取得出来ます。</p>
</li>
</ul>
<ul role="list">
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="2" data-font="Symbol" data-leveltext="" data-list-defn-props="{&quot;335552541&quot;:1,&quot;335559685&quot;:720,&quot;335559991&quot;:360,&quot;469769226&quot;:&quot;Symbol&quot;,&quot;469769242&quot;:[8226],&quot;469777803&quot;:&quot;left&quot;,&quot;469777804&quot;:&quot;&quot;,&quot;469777815&quot;:&quot;multilevel&quot;}" data-listid="1">
<p><strong>アプリケーションの共同作業者を一覧表示する：<br /></strong>アプリへのアクセス権を持つすべてのコラボレーターを表示し、各コラボレーターのメールアドレス、アクセス レベル、アプリへの招待を受け入れた日時など、詳細を表示します。</p>
</li>
</ul>
<ul role="list">
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="3" data-font="Symbol" data-leveltext="" data-list-defn-props="{&quot;335552541&quot;:1,&quot;335559685&quot;:720,&quot;335559991&quot;:360,&quot;469769226&quot;:&quot;Symbol&quot;,&quot;469769242&quot;:[8226],&quot;469777803&quot;:&quot;left&quot;,&quot;469777804&quot;:&quot;&quot;,&quot;469777815&quot;:&quot;multilevel&quot;}" data-listid="1">
<p><strong>Client Secret をローテーションする：<br /></strong>Client Secret のローテーションを自動化して、アプリのダウンタイムや中断が発生しないようにします。</p>
</li>
</ul>
<ul role="list">
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="4" data-font="Symbol" data-leveltext="" data-list-defn-props="{&quot;335552541&quot;:1,&quot;335559685&quot;:720,&quot;335559991&quot;:360,&quot;469769226&quot;:&quot;Symbol&quot;,&quot;469769242&quot;:[8226],&quot;469777803&quot;:&quot;left&quot;,&quot;469777804&quot;:&quot;&quot;,&quot;469777815&quot;:&quot;multilevel&quot;}" data-listid="1">
<p><strong>日次使用量の合計を取得する：<br /></strong>&#0160;所有しているアプリやコラボレーターであるアプリが日常的に消費しているトークン数の分析情報を取得することが出来ます。同機能のエンドポイントを使用して、独自のレポートを生成し、アプリが利用している日付範囲やサービスでフィルタリングできます。</p>
</li>
</ul>
<h2>Public Beta への参加方法</h2>
<p>Application Management API の Public Beta フェーズは、皆様からのフィードバックで API を改良、そして完成させるために非常に重要です。ベータテスト コミュニティに参加して、次のドキュメント リンクにアクセス、評価した結果や経験を共有し、問題を報告していただくことで、API を調整して多様なニーズを満たせるようにしたい意向があります。</p>
<p>APS ポータルの <a href="https://aps.autodesk.com/myapps/" rel="noopener" target="_blank">Applications</a> ページでも引き続きアプリを管理できますが、同ページでのタスクの一部を API を使用してプログラムで実行することで、プロセスがより合理化し、効率化することが可能です。</p>
<h4>参照：</h4>
<ul>
<li><a  _msthash="106"  _msttexthash="21073442" href="https://feedback.autodesk.com/key/AppManagementAPIPublicBeta" rel="noopener" target="_blank">Beta プログラムに参加する</a></li>
<li><a  _msthash="107"  _msttexthash="12211420" href="https://aps.autodesk.com/en/docs/applications/v1/developers_guide/overview/" rel="noopener" target="_blank">API ドキュメント</a></li>
<li><a  _msthash="108"  _msttexthash="11379628" href="https://github.com/autodesk-platform-services/application-management-api-sample" rel="noopener" target="_blank">コードサンプル</a></li>
</ul>
<p>※ 本記事は <a href="https://aps.autodesk.com/blog/introducing-application-management-api" rel="noopener" target="_blank">Introducing the Application Management API! | Autodesk Platform Services</a>&#0160;から転写・意訳、補足を加えたものです。</p>
<p>By Toshiaki Isezaki</p>
