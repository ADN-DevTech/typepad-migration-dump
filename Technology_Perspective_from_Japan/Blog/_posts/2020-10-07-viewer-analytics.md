---
layout: "post"
title: "Forge Viewer の利用調査について"
date: "2020-10-07 00:01:13"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2020/10/viewer-analytics.html "
typepad_basename: "viewer-analytics"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026be414ce91200d-pi" style="display: inline;"><img alt="Viewer-analytics-shutterstock" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026be414ce91200d image-full img-responsive" src="/assets/image_995976.jpg" title="Viewer-analytics-shutterstock" /></a></p>
<p>Forge Viewer は、今後のサポートや機能改善をする目的で、まもなくリリース予定のバージョン&#0160; 7.29 で使用状況分析の収集を開始します。 この分析で収集する情報には、ユーザ情報は含まれません。</p>
<p>参加を希望されない場合には、後述する方法で、この機能をオプトアウトすることができます。</p>
<p><strong>どのような使用状況分析が収集されますか？</strong><br />Forge Viewer は、今後のをサポート、また、改善する目的で、バージョン 7.29で特定のユーザーに関連付けられていない使用状況分析の収集を開始します。これらの分析には、表示されているファイルタイプ、ドメイン情報、および画面サイズ、ブラウザー、OSバージョンなどのブラウザー情報が含まれます。</p>
<p><strong>オートデスクがこの情報を必要とする理由とその使用方法を教えてください。</strong><br />分析は、オートデスクが Forge Viewer をサポートや改善するのに役立ちます。私たちは常に機能を追加および改善しています。 Forge Viewer 開発チームは、Forge Viewer が実行される環境、Web ブラウザ、およびホスト OS を理解する必要があります。この情報は、パフォーマンスの向上、機能、および全体的なエクスペリエンスの向上に役立ちます。</p>
<p><strong>分析機能はパフォーマンスに影響しますか？</strong><br />いいえ。分析は並行して収集され、Forge Viewer は この機能に依存しません。何らかの理由で分析機能がブロックされた場合でも、Forge Viewer とモデルが読み込まれ、その機能は引き続き機能します。</p>
<p><strong>Forge Viewer の古いバージョンはどうですか？</strong><br />バージョン 7.16 から 7.28 の場合、Viewer JavaScript メソッド <strong>Autodesk.Viewing.Private.analytics.optIn()</strong> を使用して使用状況分析収集をオンにするアクションを明示的に実行しない限り、Forge Viewer はこれらの使用状況分析を収集しません。</p>
<p>なお、バージョン 7.15 以前では、これらの使用状況分析は収集されません。</p>
<p>Forge Viewer によって収集されたすべての情報は、Forge 開発者の利用規約およびオートデスクのプライバシーに関する声明の対象となります。オートデスクは、使用状況分析の洞察の作成を専門とする認定サードパーティ分析プロバイダーである Mixpanel を使用しています。</p>
<p><strong>オプトアウトするにはどうすればよいですか？</strong><br />アプリケーションの使用状況分析をオートデスクに提供したくない場合は、Viewer JavaScript メソッド<strong>Autodesk.Viewing.Private.analytics.optOut()</strong> を使用してオプトアウトできます。あなたの参加は、私たちが高度な機能を追加し、あなたのためにビューアを改善するのに役立ちます。</p>
<p>ご不明な点がございましたら、<a href="https://forge.autodesk.com/en/support/get-help" rel="noopener" target="_blank">サポートチーム</a>までお問い合わせください。&#0160;</p>
<p>By Toshiaki Isezaki</p>
