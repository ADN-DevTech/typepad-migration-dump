---
layout: "post"
title: "VS Code APS エクステンションの Client ID 切り替え"
date: "2024-12-16 00:02:19"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/12/switch-client-id-on-vs-code-aps-extension.html "
typepad_basename: "switch-client-id-on-vs-code-aps-extension"
typepad_status: "Publish"
---

<p>VS Code APS エクステンション － <a href="https://marketplace.visualstudio.com/items?itemName=petrbroz.vscode-forge-tools" rel="noopener" target="_blank">Autodesk Platform Services (VSCode Extension)</a>&#0160; の利用時、エクステンション自体が Client ID と Client Secret を使って内部的にアクセス トークンを取得して APS エンドポイントにアクセス、エクステンション上の機能を開発者に提供します。内部の認証フローで使用する Client ID と Client Secret は、 settings.json ファイルに設定する必要があります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860f3db03200d-pi" style="display: inline;"><img alt="Settings_json" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860f3db03200d img-responsive" src="/assets/image_521227.jpg" title="Settings_json" /></a></p>
<p>使用する Client ID と Client Secret は、settings.json ファイルの &quot;autodesk.forge.environments&quot; セクションで指定しますが、この値は配列になっているので、[] 内に複数登録しておくことが出来ます。（&quot;autodesk.forge.environments&quot;の [] 内に {} をカンマ区切りで記入する。）</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860dcbf24200b-pi" style="display: inline;"><img alt="Clientid_array" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860dcbf24200b img-responsive" src="/assets/image_205549.jpg" title="Clientid_array" /></a></p>
<p>上記のように settings.json ファイルを記入、保存しておくと、VS Code 上での作業中に、動的に使用する Client ID と Client Secret を切り替えることが出来るようになります。</p>
<p>[表示] メニューの [コマンド パレット...] をクリックすると、コマンドを入力するユーザ インタフェースが表示されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860dd1a3c200b-pi" style="display: inline;"></a> <a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860f436f3200d-pi" style="display: inline;"><img alt="Command_palette" class="asset  asset-image at-xid-6a0167607c2431970b02e860f436f3200d img-responsive" src="/assets/image_895425.jpg" style="width: 700px;" title="Command_palette" /></a></p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860dd1a27200b-pi" style="display: inline;"></a>表示されたコマンド パレットに&#0160;<strong>switch environment</strong>&#0160;と入力すると、settings.json ファイルに記入した&#0160; Client ID と Client Secret のペアにつけた環境の名前（{～} 内の title 値）が表示されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860dd1a1d200b-pi" style="display: inline;"><img alt="Switch_environment_command" class="asset  asset-image at-xid-6a0167607c2431970b02e860dd1a1d200b img-responsive" src="/assets/image_834263.jpg" style="width: 700px;" title="Switch_environment_command" /></a></p>
<p>あとは、使用したい環境名を選択するだけで、APS エクステンションが利用するアクティブな Client ID と Client Secret のペアが切り替わります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3c62f9b200c-pi" style="display: inline;"><img alt="Switch_environment" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3c62f9b200c img-responsive" src="/assets/image_495688.jpg" title="Switch_environment" /></a></p>
<p>環境（アクティブな Client ID と Client Secret）が切り替わると、VS Code 左下のステータスバーに、APS Env 値として環境名が表示されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3c6ad80200c-pi" style="display: inline;"><img alt="Key1" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3c6ad80200c img-responsive" src="/assets/image_501497.jpg" title="Key1" /></a></p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860f457de200d-pi" style="display: inline;"><img alt="Key2" class="asset  asset-image at-xid-6a0167607c2431970b02e860f457de200d img-responsive" src="/assets/image_954450.jpg" title="Key2" /></a></p>
<p>settings.json ファイルの内容を毎回書き換える必要がないので便利です。</p>
<p>By Toshiaki Isezaki</p>
