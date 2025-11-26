---
layout: "post"
title: "ACC Project Admin API: プロジェクト作成とユーザー管理"
date: "2023-11-29 00:03:29"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2023/11/acc-project-admin-api-project-creation-and-user-management.html "
typepad_basename: "acc-project-admin-api-project-creation-and-user-management"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a3cd2a200d-pi" style="display: inline;"><img alt="Placeholder - Blog images_Lifestyle 16x9 1920x1080" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a3cd2a200d image-full img-responsive" src="/assets/image_105040.jpg" title="Placeholder - Blog images_Lifestyle 16x9 1920x1080" /></a></p>
<p>Autodesk Construction Cloud（ACC）Project Admin API の一般提供が開始されました。新しいエンドポイントを使用して、プロジェクトのゼロからの作成やテンプレートを利用した作成、プロジェクト内のユーザーを追加/更新/削除が出来るようになりました。ユーザーを追加する際には、適切なアクセスレベルを製品に割り当てることもできます。</p>
<p>以前、<a href="https://adndevblog.typepad.com/technology_perspective/2023/08/acc-admin-api-get-projects-and-project-users.html" rel="noopener" target="_blank">ACC Admin API: GET Projects と Project Users</a>&#0160;の記事で、GET Projects と GET Project Users が Public Beta として利用可能になったことをお知らせしましたが、これらのエンドポイントも現在 Beta から卒業しています。</p>
<p>今回のリリースでは、次の 5 つのエンドポイントが追加されています。</p>
<ul>
<li><strong>POST projects</strong> - ACC プロジェクトのゼロからの作成、あるいはテンプレートからの作成</li>
<li><strong>POST&#0160;projects/:projectId/users</strong> - プロジェクトに 1 人のユーザーを割り当て</li>
<li><strong>POST&#0160;projects/:projectId/users:import</strong> - 複数のユーザーをプロジェクトにインポート</li>
<li><strong>PATCH&#0160;projects/:projectId/users/:userId</strong> - プロジェクト内の特定ユーザー情報の更新</li>
<li><strong>DELETE&#0160;projects/:projectId/users/:userId</strong> - プロジェクトからの特定ユーザーの削除</li>
</ul>
<p>これらエンドポイントを使用すると、API を使用する一般的なワークフローの 1 つであるプロジェクトのセットアップなど、プロセスの一部を自動化できるようになります。多くの煩雑な作業を排除し、生産性を向上させるのに役立ちます。</p>
<p>潜在的な混乱を減らすために、注意すべき点がいくつかあります。</p>
<ul>
<li>下位互換性があり、ACC プロジェクトと BIM 360 プロジェクトの両方で動作する&#0160;<a  _istranslated="1" href="https://aps.autodesk.com/blog/acc-admin-api-get-projects-and-project-users" rel="noopener" target="_blank">GET API</a>&#0160;とは異なり、上記の 5 つの新しいエンドポイントは ACC プロジェクトでのみ機能します。書き込みエンドポイントは BIM 360 との互換性がありません。</li>
<li><strong>3-legged</strong> <strong>token&#0160;</strong>と <strong>2-legged token&#0160; (ユーザー指定 - user impersonation あり/なし) </strong>で動作する&#0160;<a  _istranslated="1" href="https://aps.autodesk.com/blog/acc-admin-api-get-projects-and-project-users" rel="noopener" target="_blank">GET API</a> とは異なり、上記の 5 つのエンドポイントはすべて、3-legged tokenと&#0160;<strong>2-legged token ユーザー指定あり</strong>のみで動作します。ユーザー指定なしの純粋な 2-legged token はサポートされていません。2-legged token ユーザー指定あり とは、ヘッダーで <a href="https://aps.autodesk.com/en/docs/acc/v1/reference/http/admin-projects-project-Id-users-POST/#headers" rel="noopener" target="_blank">User-Id</a> 指定することを意味します。</li>
</ul>
<p>プロジェクト作成</p>
<ul>
<li><span style="text-decoration: line-through;">テンプレートからプロジェクトを作成する場合、プロジェクト管理者が最初に割り当てられない限り、テンプレートプロジェクトメンバーは複製されたプロジェクトに自動的に割り当てられません。この動作は UI とは異なります。</span><a href="https://aps.autodesk.com/en/docs/acc/v1/tutorials/admin/admin-create-configure-projects/#step-3-assign-members-to-your-project" rel="noopener" target="_blank">Step 3 Option 3A: Import template project members（該当する場合）</a>を参照してください。</li>
<li>プロジェクトが作成されると、jobId で応答します。ただし、現在、ジョブ ID の状態を取得する API はありません。プロジェクトを「ポーリング」し、ステータスを確認する必要があります。<a href="https://aps.autodesk.com/en/docs/acc/v1/tutorials/admin/admin-create-configure-projects/#step-2-check-project-activation-status-by-polling" rel="noopener" target="_blank">Step 2: Check Project Activation Status by Polling</a>&#0160;を参照してください。</li>
<li>現在、プロジェクトを更新したり、プロジェクトを削除したりすることはできません。</li>
<li>プロジェクト作成でユーザー コンテキスト -&#0160; <strong>3-legged</strong>&#0160;<strong>token </strong>または&#0160;<strong>2-legged token&#0160; (ユーザー指定 - user impersonation あり）</strong>- でエンドポイントを呼び出す際、同コンテキストのユーザーは Account Admin である必要があります。</li>
</ul>
<p>プロジェクトユーザー&#0160;</p>
<ul>
<li>ユーザーをプロジェクトに割り当てるには、ユーザーのロール (役割) を指定します（オプション）。ただし、現在、ACC プロジェクト ロールを取得するエンドポイントがありません。回避策として、<a  _istranslated="1" href="https://aps.autodesk.com/en/docs/acc/v1/reference/http/data-connector-requests-POST/" rel="noopener" target="_blank">Data Connector API</a> を使用してプロジェクト ロールの情報を取得することが出来ます。</li>
<li>複数のユーザーをプロジェクトにインポートすると、jobId のみで応答します。ただし、ジョブの状態を取得する API はありません。GET project users を呼び出して、ユーザーがプロジェクトに正常に割り当てられているかどうかを確認する必要があります。&#0160;<a href="https://aps.autodesk.com/en/docs/acc/v1/tutorials/admin/admin-create-configure-projects/#step-4-retrieve-information-about-the-project-members" rel="noopener" target="_blank">Step 4: Retrieve Information About the Project Members</a>&#0160;を参照してください。</li>
</ul>
<p><strong>ドキュメント：</strong></p>
<ul>
<li>リファレンスガイド:
<ul>
<li><a href="https://aps.autodesk.com/en/docs/acc/v1/reference/http/admin-accounts-accountidprojects-POST/" rel="noopener" target="_blank">POST projects</a></li>
<li><a href="https://aps.autodesk.com/en/docs/acc/v1/reference/http/admin-projects-project-Id-users-POST/" rel="noopener" target="_blank">POST&#0160;projects/:projectId/users</a></li>
<li><a href="https://aps.autodesk.com/en/docs/acc/v1/reference/http/admin-v2-projects-project-Id-users-import-POST/" rel="noopener" target="_blank">POST&#0160;projects/:projectId/users:import&#0160;</a></li>
<li><a href="https://aps.autodesk.com/en/docs/acc/v1/reference/http/admin-projects-project-Id-users-userId-PATCH/" rel="noopener" target="_blank">PATCH&#0160;projects/:projectId/users/:userId</a></li>
<li><a href="https://aps.autodesk.com/en/docs/acc/v1/reference/http/admin-projects-project-Id-users-userId-DELETE/" rel="noopener" target="_blank">DELETE&#0160;projects/:projectId/users/:userId</a></li>
</ul>
</li>
<li><a href="https://aps.autodesk.com/en/docs/acc/v1/overview/field-guide/admin/" rel="noopener" target="_blank">Field Guide（フィールドガイド）</a></li>
<li>Step-by-Step Tutorials:&#0160;<a href="https://aps.autodesk.com/en/docs/acc/v1/tutorials/admin/admin-create-configure-projects/" rel="noopener" target="_blank">Create and Configure Projects（プロジェクトの作成と構成）</a></li>
<li><a href="https://aps.autodesk.com/en/docs/acc/v1/change_history/admin_v1_changelog/" rel="noopener" target="_blank">Change History（変更履歴）</a></li>
</ul>
<p>2023 年 12 月 13 日に、新しい ACC Project Admin API に焦点を当てたウェビナーを開催する予定です。新しい API を紹介し、追加情報を共有し、質問にお答えします。ここに登録へのリンクを追加する予定です。一週間ほど経ってからまたチェックしてください。</p>
<p>それまでの間、ご不明な点がございましたら、<u>aps.help@autodesk.com</u>&#0160;からお問い合わせください。</p>
<p>※ 本記事は <a href="https://aps.autodesk.com/blog/acc-project-admin-api-project-creation-and-user-management" rel="noopener" target="_blank">ACC Project Admin API: Project Creation and User Management | Autodesk Platform Services</a>&#0160;から転写・翻訳したものです。</p>
<p>By Toshiaki Isezaki</p>
