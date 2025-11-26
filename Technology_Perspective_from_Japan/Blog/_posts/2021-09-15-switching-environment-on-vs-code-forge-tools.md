---
layout: "post"
title: "VS Code Forge Tools での環境変更"
date: "2021-09-15 01:18:44"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/09/switching-environment-on-vs-code-forge-tools.html "
typepad_basename: "switching-environment-on-vs-code-forge-tools"
typepad_status: "Publish"
---

<p><a href="https://adndevblog.typepad.com/technology_perspective/2020/06/forge-development-using-vs-code.html" rel="noopener" target="_blank"><strong>Visual Studio Code での Forge 開発</strong></a> でご案内した Autodesk Forge Tools を利用する場合、事前に settings.json ファイルに使用する Client ID と Client Secret を記入しておく必要があります。</p>
<p>この設定は、&quot;autodesk.forge.environments&quot; セクション内に配列要素として複数登録しておくことが出来ます。（&quot;autodesk.forge.environments&quot;の [] 内に {} をカンマ区切りで記入する。）</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdef0e4ea200c-pi" style="display: inline;"><img alt="Vs_environment" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdef0e4ea200c image-full img-responsive" src="/assets/image_231199.jpg" title="Vs_environment" /></a></p>
<p>上記のように settings.json ファイルを記入、保存しておくと、VS Code 上での作業中に、動的に使用する Client ID と Client Secret を切り替えることが出来るようになります。</p>
<p>[表示] メニューの [コマンド パレット...] をクリックすると、コマンドを入力するユーザ インタフェースが表示されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdef0e548200c-pi" style="display: inline;"><img alt="Command_palette" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdef0e548200c image-full img-responsive" src="/assets/image_31873.jpg" title="Command_palette" /></a></p>
<p>表示されたコマンド パレットに <strong>switch environment</strong> と入力すると、settings.json ファイルに記入した&#0160; Client ID と Client Secret のペアにつけた環境の名前（{～} 内の title 値）が表示されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdef0e586200c-pi" style="display: inline;"><img alt="Switch_environment" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdef0e586200c image-full img-responsive" src="/assets/image_271198.jpg" title="Switch_environment" /></a></p>
<p>あとは、使用したい環境名を選択するだけで、Autodesk Forge Tools が利用するアクティブな Client ID と Client Secret のペアが切り替わります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02788048bdb7200d-pi" style="display: inline;"><img alt="Change_environment" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02788048bdb7200d image-full img-responsive" src="/assets/image_960490.jpg" title="Change_environment" /></a></p>
<p>毎回、settings.json ファイルを書き換える必要がないので、とても便利です。試してみてください。</p>
<p>By Toshiaki Isezaki</p>
