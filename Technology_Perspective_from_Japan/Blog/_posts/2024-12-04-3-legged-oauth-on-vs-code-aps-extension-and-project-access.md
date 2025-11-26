---
layout: "post"
title: "VS Code APS エクステンションでの 3-legged OAuth と Project アクセス"
date: "2024-12-04 00:01:58"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/12/3-legged-oauth-on-vs-code-aps-extension-and-project-access.html "
typepad_basename: "3-legged-oauth-on-vs-code-aps-extension-and-project-access"
typepad_status: "Publish"
---

<p>過去に <a href="https://adndevblog.typepad.com/technology_perspective/2021/09/3-legged-oauth-on-vs-code-forge-tools.html" rel="noopener" target="_blank">VS Code Forge Tools での 3-legged OAuth</a> の記事で VS Code にインストールした <a href="https://marketplace.visualstudio.com/items?itemName=petrbroz.vscode-forge-tools" rel="noopener" target="_blank">Autodesk Platform Services (VSCode Extension)</a> から 3-legged OAuth で Fusion Team や Autodesk Docs（Autodesk Construction Cloud）にアクセスする方法をご紹介しました。その後、Forge から Autodesk Platform Services（APS）への改名にともない、3-legged 認証フローで利用するコールバック URL も変更されているので、改めて、VS Code APS エクステンションでの 3-legged OAuth 運用する手順を記しておきたいと思います。</p>
<p>通常、VS Code 上で Autodesk Platform Services (VSCode Extension) を利用すると、2-legged OAuth で取得したアクセストークンが使用されます。この時、VS Code の画面左下のステータスバーには、それを示す文字が表示されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860da3d26200b-pi" style="display: inline;"><img alt="2-legged" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860da3d26200b img-responsive" src="/assets/image_478232.jpg" title="2-legged" /></a></p>
<p><a href="https://adndevblog.typepad.com/technology_perspective/2016/07/app-registration-and-getting-keys-for-forge-api.html" rel="noopener" target="_blank">APS API を利用するアプリの登録とキーの取得</a> でアプリを作成する際に&#0160;<strong><label class="f2c-label fw-medium type-family-body" for="inputRedirectUri">Callback URL&#0160;</label></strong>に <strong>http://localhost:8123/auth/callback</strong>&#0160;値を設定しておくと、3-legged OAuth を使った認証と認可のプロセスを VS Code でおこなうことが出来るようになります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860da3d38200b-pi" style="display: inline;"><img alt="My_app" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860da3d38200b img-responsive" src="/assets/image_856349.jpg" title="My_app" /></a></p>
<p>（VS Code の settings.json で使用する Client Id と Client Secret を持つアプリについて、あらかじめ、前述の Callback URL が設定されていれば、ステータスバーの <strong>APS Auth: 2-legged</strong>&#0160;をマウス左ボタンでクリックすると、既定のブラウザが起動して 3-legged OAuth のプロセスがはじまります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860da3d6c200b-pi" style="display: inline;"><img alt="Login" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860da3d6c200b image-full img-responsive" src="/assets/image_991348.jpg" title="Login" /></a></p>
<p>[Login] ボタンをクリックしてアクセス対象となる&#0160; Fusion Team や Autodesk Docs（Autodesk Construction Cloud）などのストレージ データにアクセス出来る Autodesk ID でサインインすると、次の認可画面が表示されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860da3edc200b-pi" style="display: inline;"><img alt="Authorization" class="asset  asset-image at-xid-6a0167607c2431970b02e860da3edc200b img-responsive" src="/assets/image_314484.jpg" title="Authorization" /></a></p>
<p>&#0160;[許可] ボタンで Autodesk Platform Services (VSCode Extension) のデータアクセスを認可（許可）すると、次の画面に遷移して 3-legged OAuth プロセスが完了したことが表示されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860da3ef0200b-pi" style="display: inline;"><img alt="Success" class="asset  asset-image at-xid-6a0167607c2431970b02e860da3ef0200b img-responsive" src="/assets/image_110534.jpg" style="width: 520px;" title="Success" /></a></p>
<p>&#0160;Web ブラウザの画面を閉じて&#0160; VS Code に戻り、<strong>Hubs &amp; Derivatives </strong>にアクセスすると、認可したユーザの権限に応じて、Hub &gt;&gt; Project と内容を表示していくことが出来るようになるはずです。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3c3ba75200c-pi" style="display: inline;"><img alt="Vs_code_acc" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3c3ba75200c img-responsive" src="/assets/image_5976.jpg" title="Vs_code_acc" /></a></p>
<ul>
<li>&#0160;Autodesk Docs（Autodesk Construction Cloud）のストレージ内容を表示させるには、<a href="https://adndevblog.typepad.com/technology_perspective/2024/02/acc-new-custom-integration-ui.html">ACC：新しいカスタム統合 UI</a> でご案内している手順で、VS Code で使用している Client ID を「カスタム統合」しておく必要があります。</li>
</ul>
<p>この際、ステータスバーには <strong>APS Auth: 3-legged</strong>&#0160;と表示されているはずです。同時に、<strong>Hubs &amp; Derivatives </strong>下に表示されるプロジェクトは、<strong>APS Auth: 2-legged</strong> 時に表示されていた内容と異なっているはずです。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3c3bbbd200c-pi" style="display: inline;"><img alt="3-legged" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3c3bbbd200c img-responsive" src="/assets/image_511301.jpg" title="3-legged" /></a></p>
<p>ステータスバーの <strong>APS Auth: 3-legged</strong> をマウス左ボタンでクリックすると、VS Code 上部に「Would you like to log out?（ログアウトしますか？）」と表示されるので、「Yes」を選択すると <strong>APS Auth: 2-legged</strong> の状態に戻ります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860f163e7200d-pi" style="display: inline;"><img alt="Logout" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860f163e7200d img-responsive" src="/assets/image_851613.jpg" title="Logout" /></a></p>
<p>なお、Autodesk Platform Services (VSCode Extension) が 3-legged OAuth で使用するポート番号の既定値は 8123 ですが、この値は Extension の設定から、必要に応じて変更することが可能です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860f162ce200d-pi" style="display: inline;"><img alt="Port_no" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860f162ce200d img-responsive" src="/assets/image_273836.jpg" title="Port_no" /></a></p>
<p>By Toshiaki Isezaki</p>
