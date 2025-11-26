---
layout: "post"
title: "複数 Callback URL の設定"
date: "2025-01-06 00:01:00"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2025/01/multiple-callback-urls.html "
typepad_basename: "multiple-callback-urls"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860dae6fd200b-pi" style="display: inline;"><img alt="Aps" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860dae6fd200b image-full img-responsive" src="/assets/image_894172.jpg" title="Aps" /></a></p>
<p>少し前にってしまいますが、<a href="https://adndevblog.typepad.com/technology_perspective/2016/07/app-registration-and-getting-keys-for-forge-api.html" rel="noopener" target="_blank">APS API を利用するアプリの登録とキーの取得</a> の手順で、開発に必要なデベロッパーキーを取得（アプリの作成）をおこなう際、複数のコールバック URL が登録出来るようになっています。コールバック URL とは、3-legged 認証フローでにエンドユーザーをリダイレクトするために使用する URL です。</p>
<p>以前、作成したアプリに対して 1 つのコールバック URL しか設定出来ませんでしたが、1 つのアプリに複数のコールバック URL が設定出来るようになったことで、ローカル環境の開発時や、デプロイ後の本番運用時毎にアプリを切り替えて（Client ID と Client Secret を置き換えて）テスト・運用する必要がなくなります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3c46104200c-pi" style="display: inline;"><img alt="Multiple_callbacks" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3c46104200c img-responsive" src="/assets/image_399796.jpg" title="Multiple_callbacks" /></a></p>
<p>2 つめ以降のコールバック URL を追加するには、<a href="https://aps.autodesk.com/myapps/" rel="noopener" target="_blank">https://aps.autodesk.com/myapps/</a> からアプリを選択、または、作成して、<strong>General Settings</strong> の Callback URL&#0160; 欄下の <span style="color: #80c0ff; font-family: tahoma, arial, helvetica, sans-serif;"><strong>＋ Add URL</strong></span> をクリックするだけです。URL の追加記入後にページ下部の&#0160;<span style="font-family: tahoma, arial, helvetica, sans-serif;"><strong><span style="background-color: #a040ff; color: #ffffff;"> &#0160;Save changes&#0160;</span></strong></span> をクリックしてアプリ設定を保存するのを忘れないでください。</p>
<p>By Toshiaki Isezaki</p>
