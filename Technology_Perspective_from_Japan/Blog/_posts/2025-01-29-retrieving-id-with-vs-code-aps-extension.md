---
layout: "post"
title: "VS Code APS エクステンションでの ID 取得"
date: "2025-01-29 00:17:41"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2025/01/retrieving-id-with-vs-code-aps-extension.html "
typepad_basename: "retrieving-id-with-vs-code-aps-extension"
typepad_status: "Publish"
---

<p><a href="https://marketplace.visualstudio.com/items?itemName=petrbroz.vscode-forge-tools" rel="noopener" target="_blank">APS VS Code エクステンション</a> に新しいバージョン 2.8.0 がリリースされて、便利な機能が加わりました。エクステンションで使用している Client ID が Autodesk Construction Cloud の「<a href="https://adndevblog.typepad.com/technology_perspective/2024/02/acc-new-custom-integration-ui.html" rel="noopener" target="_blank">カスタム統合</a>」されている場合、HUB &amp; DERIVATIVES 下に Autodesk Construction Cloud の Hub や Project 等のツリーが表示されます。</p>
<p>バージョン 2.8.0 では、ツリー上の Hub、Project、Folder、Item、Verision を選択して右クリックすることで、それぞれの ID（Hub ID、Project ID、Folder ID、Item ID、Version ID）をクリップボードにコピー出来るようになっています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860e06e7f200b-pi" style="display: inline;"><img alt="Copy_id_to_clipboard" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860e06e7f200b image-full img-responsive" src="/assets/image_703701.jpg" title="Copy_id_to_clipboard" /></a></p>
<p>シンプルな機能ですが、Autodesk Construction Cloud と統合するアプリを開発する際には、<a href="https://www.postman.com/" rel="noopener" target="_blank">Postman</a> や <a href="https://insomnia.rest/" rel="noopener" target="_blank">Insomnia</a> などのツールでエンドポイントを呼び出さなくても、簡単に各種 ID を得ることが出来て便利です。</p>
<p>すでに VS Code に APS エクステンションをインストールしている場合には、VS Code の起動時、「機能拡張」タブにエクステンションの再起動を求めるメッセージが表示されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3c9dc68200c-pi" style="display: inline;"><img alt="Update" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3c9dc68200c img-responsive" src="/assets/image_179763.jpg" title="Update" /></a></p>
<p>[機能拡張の再起動] をクリックすると、新しいバージョン 2.8.0 に更新されて、ID のクリップボードへのコピー機能を利用出来るようになります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860e06e84200b-pi" style="display: inline;"><img alt="Aps_extension_v2.8.0" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860e06e84200b image-full img-responsive" src="/assets/image_987404.jpg" title="Aps_extension_v2.8.0" /></a></p>
<p>By Toshiaki Isezaki</p>
