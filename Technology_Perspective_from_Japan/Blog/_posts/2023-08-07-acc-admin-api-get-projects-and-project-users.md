---
layout: "post"
title: "ACC Admin API: GET Projects と Project Users"
date: "2023-08-07 00:17:15"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2023/08/acc-admin-api-get-projects-and-project-users.html "
typepad_basename: "acc-admin-api-get-projects-and-project-users"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c1a6cff5b2200b-pi" style="display: inline;"><img alt="Aps" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c1a6cff5b2200b image-full img-responsive" src="/assets/image_507756.jpg" title="Aps" /></a></p>
<p>日本語でのご案内が遅くなってしまいましたが、Autodesk Construction Cloud (ACC) の Admin API である GET projects エンドポイント、および GET project users エンドポイントのパブリックベータの提供が開始されています。ACC 用のプロジェクト関連の Admin API は、非常にご要望の高い機能です。特にプロジェクトの作成やプロジェクトへのユーザーの追加など、まだ埋めるべきギャップがありますが、はじめてとなる ACC Admin API のリリースでプロジェクトとプロジェクトユーザーのリストアップが可能になります。</p>
<p>現時点で次の 4 つの GET エンドポイント（読み取り AP）Iが公開されています：</p>
<ul>
<li><strong>GET projects</strong><br />UI と同様、ACC と BIM 360 の両方のプロジェクトを含む、指定されたアカウントのプロジェクト一覧を返す</li>
<li><strong>GET project/:projectId<br /></strong>指定されたプロジェクトの情報を取得する</li>
<li><strong>GET project/:projectId/users<br /></strong>プロジェクトのメンバーをリストアップする</li>
<li><strong>GET project/:projectId/users/:userId<br /></strong>&#0160;指定されたユーザーに関する情報を取得する</li>
</ul>
<p>注意点：&#0160;</p>
<ul>
<li>上記の 4 つのエンドポイントには下位互換性があるので、ACC 統合（unified）アカウントと BIM 360 の両方で機能します。</li>
<li>既定では、GET projects エンドポイントは、ACC ユニファイドアカウントで使用された場合、すべての &quot;通常 &quot;プロジェクトを一覧を返します。また、&quot;filter[classification]=template&amp; filter[type]=Template &quot;と指定することで、&quot;テンプレート &quot;プロジェクトを取得することもできます（注：両方のフィルターが必要です）。</li>
<li>BIM 360 Admin（管理者）ユーザーインターフェースでは、テンプレート/ライブラリ/サンプルといった異なるプロジェクト間の区別が存在しないため、これらはすべてプロジェクト配下に表示されます。</li>
<li>例えば、ACC の build と BIM 360 の fieldManagement、ACC の docs と BIM 360 の documentManagement などです。一覧はリファレンスガイドをご参照ください。</li>
</ul>
<p>ドキュメント:</p>
<ul>
<li>リファレンス ガイド:
<ul>
<li><a href="https://aps.autodesk.com/en/docs/acc/v1/reference/http/admin-accounts-accountidprojects-GET/" rel="noopener" target="_blank">GET projects</a></li>
<li><a href="https://aps.autodesk.com/en/docs/acc/v1/reference/http/admin-projectsprojectId-GET/" rel="noopener" target="_blank">GET projects/:projectId</a></li>
<li><a href="https://aps.autodesk.com/en/docs/acc/v1/reference/http/admin-projectsprojectId-users-GET/" rel="noopener" target="_blank">GET projects/:projectId/users</a></li>
<li><a href="https://aps.autodesk.com/en/docs/acc/v1/reference/http/admin-projectsprojectId-users-userId-GET/" rel="noopener" target="_blank">GET projects/:projectId/users/userId</a></li>
</ul>
</li>
<li><a href="https://aps.autodesk.com/en/docs/acc/v1/change_history/admin_v1_changelog/" rel="noopener" target="_blank">変更履歴（Change Log）</a></li>
</ul>
<p>後日、GitHub の公開リポジトリに Postman コレクションを掲載する予定です。</p>
<p>ご質問をお持ちの場合には、<a href="mailto://aps.help@autodesk.com"><u>aps.help@autodesk.com</u></a>&#0160;にお問い合わせください。</p>
<p>※ 本記事は <a href="https://aps.autodesk.com/blog/acc-admin-api-get-projects-and-project-users" rel="noopener" target="_blank">ACC Admin API: GET Projects and Project Users | Autodesk Platform Services</a>&#0160;から転写・翻訳して一部加筆、修正したものです。</p>
<p>By Toshiaki Isezaki</p>
