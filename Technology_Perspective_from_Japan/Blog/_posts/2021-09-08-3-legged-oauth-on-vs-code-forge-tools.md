---
layout: "post"
title: "VS Code Forge Tools での 3-legged OAuth"
date: "2021-09-08 00:05:49"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/09/3-legged-oauth-on-vs-code-forge-tools.html "
typepad_basename: "3-legged-oauth-on-vs-code-forge-tools"
typepad_status: "Publish"
---

<p>以前、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2020/06/forge-development-using-vs-code.html" rel="noopener" target="_blank">Visual Studio Code での Forge 開発</a></strong> でご紹介した <a href="https://marketplace.visualstudio.com/items?itemName=petrbroz.vscode-forge-tools" rel="noopener" target="_blank"><strong>Autodesk Forge Tools</strong></a> での 3-legged OAuth 対応についてご案内したいと思います。</p>
<p>通常、VS Code 上で Autodesk Forge Tools を利用すると、2-legged OAuth で取得した Access Token が使用されます。この時、VS Code の画面左下のステータスバーには、それを示す文字が表示されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdeeda069200c-pi" style="display: inline;"><img alt="2-legged" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdeeda069200c image-full img-responsive" src="/assets/image_693987.jpg" title="2-legged" /></a></p>
<p><a href="https://forge.autodesk.com/" rel="noopener" target="_blank"><strong>Forge ポータル（https://forge.autodesk.com/）</strong></a>で作成するアプリの「<strong><label class="f2c-label fw-medium type-family-body" for="inputRedirectUri">Callback URL</label></strong>」に <strong>http://localhost:8123/vscode-forge-tools/auth/callback</strong> を設定しておくと、3-legged OAuth を使った認証と認可のプロセスを VS Code でおこなうことが出来るようになります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdeef2176200c-pi" style="display: inline;"><img alt="My_app" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdeef2176200c image-full img-responsive" src="/assets/image_380041.jpg" title="My_app" /></a></p>
<p>（VS Code の settings.json で使用する Client Id と Client Secret を持つアプリについて、）あらかじめ、前述の Callback URL が設定されていれば、ステータスバーの「<strong>Forge Auth: 2-legged</strong>」をマウス左ボタンでクリックすると、既定のブラウザが起動して 3-legged OAuth のプロセスがはじまります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02788047008b200d-pi" style="display: inline;"><img alt="Login" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02788047008b200d image-full img-responsive" src="/assets/image_511206.jpg" title="Login" /></a></p>
<p>[Login] ボタンをクリックしてアクセス対象となる A360、BIM 360 Docs、Fusion Team などのストレージ データ所有者の Autodesk ID でサインインすると、次の認可画面が表示されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdeef2198200c-pi" style="display: inline;"><img alt="Authorization" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdeef2198200c img-responsive" src="/assets/image_40728.jpg" title="Authorization" /></a></p>
<p>&#0160;[許可] ボタンで Autodesk Forge Tools のユーザ データアクセスを認可すると、次の画面に遷移して 3-legged OAuth プロセスが完了したことが通知されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e11dfa88200b-pi" style="display: inline;"><img alt="Success" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e11dfa88200b image-full img-responsive" src="/assets/image_237622.jpg" title="Success" /></a></p>
<p>&#0160;Web ブラウザの画面を閉じて&#0160; VS Code に戻り、「<strong>Hubs &amp; Derivatives View</strong>」にアクセスすると、認可したユーザの権限に応じて、Hub &gt;&gt; Project と内容を表示していくことが出来るようになるはずです。</p>
<ul>
<li><span style="background-color: #ffff00;">BIM 360 Docs のストレージ内容を表示させるには、<a href="https://adndevblog.typepad.com/technology_perspective/2017/06/bim-360-docs-and-data-management-api-access.html" rel="noopener" style="background-color: #ffff00;" target="_blank"><strong>BIM 360 Docs と Data Management API アクセス</strong></a> 中程からご紹介している手順で、事前に BIM 360 Docs のアカウント管理者によって使用する Client Id を登録しておく必要があります。</span></li>
</ul>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdeed9ef5200c-pi" style="display: inline;"><img alt="Vs_code_a360_hub" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdeed9ef5200c image-full img-responsive" src="/assets/image_876141.jpg" title="Vs_code_a360_hub" /></a></p>
<p>なお、Autodesk Forge Tools が 3-legged OAuth で使用するポート番号は 8123 ですが、この値は Autodesk Forge Tools エクステンションの設定で変更することも可能です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdeeda5c5200c-pi" style="display: inline;"><img alt="Port_no" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdeeda5c5200c image-full img-responsive" src="/assets/image_885784.jpg" title="Port_no" /></a></p>
<p>By Toshiaki Isezaki</p>
