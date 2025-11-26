---
layout: "post"
title: "Secure Service Accounts（SSA）ベータ"
date: "2025-04-23 01:43:55"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2025/04/introducing-secure-service-accounts-ssa-now-in-public-beta.html "
typepad_basename: "introducing-secure-service-accounts-ssa-now-in-public-beta"
typepad_status: "Publish"
---

<section class="content__primary lg:dhig-col-span-2">
<div class="field field--name-field-primary-image field--type-image field--label-hidden field__item"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860e748c7200b-pi" style="display: inline;"><img alt="Toy-robots-jpg" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860e748c7200b img-responsive" src="/assets/image_582378.jpg" title="Toy-robots-jpg" /></a></div>
<div class="clearfix text-formatted field field--name-field-body field--type-text-with-summary field--label-hidden field__item">
<p data-pm-slice="1 1 []">Autodesk Platform Services（APS） とのサーバー間統合を認証するための、より安全で柔軟性のある最新の方法である新しい<strong> Secure Service Accounts（SSA）API</strong> が<strong>パブリック ベータ</strong>としてリリースされました。</p>
<h4 data-pm-slice="1 1 []">サービス アカウント保護の必要性</h4>
<p><a href="https://adndevblog.typepad.com/technology_perspective/2024/12/aps-access-2-legged-and-3-legged.html">APS アクセス：2-legged と 3-legged</a> の記事でご案内のとおり、従来、Autodesk Construction Cloud とのバックエンド統合を構築する開発者は、用途に応じてユーザー代理（<strong><code>x-user-id</code></strong>）の 2-legged 認証フローを使用しています。</p>
<p>しかし、<a href="https://aps.autodesk.com/en/docs/acc/v1/overview/field-guide/issues/" rel="noopener" target="_blank">ACC Issues API</a> など、主要な ACC/BIM360 API のエンドポイントは 3-legged 認証フローのみに限定しているため、2-legged 認証フローでの完全な自動化が実現出来ない状態です。3-legged 認証フローでサインイン プロンプトやリフレッシュ トークンを含む仕組みでは、長期的なセキュリティ、スケーラビリティ、またはガバナンスにとって理想的ではありません。</p>
<h4>課題を解決するセキュア サービス アカウント（Secure Service Account、SSA）の機能</h4>
<ul data-spread="false">
<li>
<p>安全な秘密鍵を使用して認証</p>
</li>
<li>
<p>特定のプロジェクトやハブに、ユーザーのような正確な権限を適用</p>
</li>
<li>
<p>自動化のためにパスワードを保存、またはの、パスワード共有の必要性を排除</p>
</li>
</ul>
<p>SSA は、<strong>ゼロトラストの原則</strong>と最新のエンタープライズ セキュリティ標準に準拠して、アプリの統合に<strong>十分なアクセス</strong>を提供します。</p>
<h4>主な機能</h4>
<p>🔐 <strong>サインイン UI 操作、ReCaptcha、OTP </strong>が不要</p>
<p style="padding-left: 40px;"><strong>秘密鍵</strong>を使用して JSON Web Tokens（JWT）経由で認証するため、ログインボックスのプロンプトやリフレッシュトークンを管理する必要がなくなります。</p>
<p>⚙️ 自動化<strong>のための設計</strong></p>
<p style="padding-left: 40px;">CI/CD パイプライン、データ統合、バックエンド タスクに最適化されているため、ユーザーによるの操作が不要です。</p>
<p>📋&#0160;<strong>Audit-Ready</strong></p>
<p style="padding-left: 40px;">SSA トークンの使用状況は、ACC アクティビティ ログ システムの一般的なユーザーと同様に追跡可能であり、エンタープライズ コンプライアンス モデルに適合します。</p>
<h4>ベータの内容</h4>
<p>このパブリックベータでは、以下の機能を利用できます。</p>
<ul data-spread="false">
<li>
<p><a  _istranslated="1" href="https://aps.autodesk.com/en/docs/ssa/v1/developers_guide/overview/" rel="noopener" target="_blank" title="SSA Documentation">詳細な SSA API ドキュメント（英語）</a></p>
</li>
<li>
<p>多くの&#0160; 3-legged 認証フローのみの ACC API （Admin、Docs、Cost、Build、Issues、Forms など) のサポート</p>
</li>
<li>
<p>開発者向けのサンプル&#0160;<a  _istranslated="1" disabled="disabled">SSA 管理ツール</a></p>
</li>
</ul>
<h4>今すぐ始める</h4>
<p>API ドキュメントにある 3 つのステップ&#0160;<a  _istranslated="1" href="https://aps.autodesk.com/en/docs/ssa/v1/tutorials/getting-started-with-ssa/about-this-walkthrough/" rel="noopener" target="_blank" title="3-Step How-To Guide">SSA - How to Guide</a>&#0160;に従ってください。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3d0960b200c-pi" style="display: inline;"><img alt="3steps" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3d0960b200c img-responsive" src="/assets/image_947250.jpg" title="3steps" /></a></p>
<h4>フィードバックのお願い</h4>
<p>これはほんの始まりに過ぎません。残りの 3LO API でのサポートの拡大、2LO によるデータ管理 API の制限機能の追加、ACC と管理ポータルの UI の改善に取り組んでいます。</p>
<ul>
<li>ベータで SSA を使用している場合は、<a  _istranslated="1" href="mailto:ssa-api-beta-feedback@autodesk.com" title="ssa-api-beta-feedback@autodesk.com">ssa-api-beta-feedback@autodesk.com</a>&#0160;からフィードバックをお寄せください。</li>
<li>サポートについては、APS ポータルの&#0160;<strong>Support - &gt; Get Help&#0160;</strong>メカニズムを通じてお問い合わせください。</li>
</ul>
</div>
<div class="node__tags">
<div class="field field--name-field-categories field--type-entity-reference field--label-hidden field__items">
<div class="field__item">
<p>※ 本記事は <a href="https://aps.autodesk.com/blog/introducing-secure-service-accounts-ssa-now-public-beta" rel="noopener" target="_blank">Introducing Secure Service Accounts (SSA) – Now in Public Beta! | Autodesk Platform Services</a>&#0160;から転写・意訳・補足したものです。</p>
<p>By Toshiaki Isezaki</p>
</div>
</div>
</div>
</section>
