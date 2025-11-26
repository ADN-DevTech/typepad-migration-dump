---
layout: "post"
title: "Web アプリのデプロイ：Visual Studio から Azure への直接デプロイ"
date: "2019-05-22 00:06:06"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2019/05/deploy-web-app-from-visual-studio.html "
typepad_basename: "deploy-web-app-from-visual-studio"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a48147c3200d-pi" style="display: inline;"></a>過<a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4abfd54200b-pi" style="float: right;"><img alt="Vs_azure" class="asset  asset-image at-xid-6a0167607c2431970b0240a4abfd54200b img-responsive" src="/assets/image_155442.jpg" style="width: 210px; margin: 0px 0px 5px 5px;" title="Vs_azure" /></a>去 3 回に渡って、GitHub 上のリポジトリ（リモート リポジトリ）から Heroku と Azure にデプロイする手順をご案内してきました。この場合、Web アプリのプログラムに改編（ソースコードの変更）があった場合には、ローカル リポジトリへのコミット、リモート リポジトリへのプッシュを経て、再度デプロイする手順が必要になってきます。</p>
<p>もし、GitHub リモート リポジトリでの共有が不要で、かつ、Visual Studio をお使いなら、Azure へのデプロイを Visual Studio 側から直接おこなうことも可能です。今回は、その手順を紹介しておきます。&#0160;</p>
<p><span style="background-color: #ffff00;">※ ご紹介する画面遷移は2019年4月時点のものです。今後当該サービスプロバイダーによって変更される可能性があります。</span></p>
<hr />
<p><strong>ローカルでの Web アプリの準備</strong></p>
<p style="padding-left: 40px;">ここでは、Web アプリとして Visual Studio 2017&#0160; Professional エディションで作成した Node.js テンプレートを利用したスケルトン プロジェクトを利用します。</p>
<p style="padding-left: 40px;"><strong><a href="https://adndevblog.typepad.com/technology_perspective/2019/05/push-web-app-to-github-using-github-desktop.html" rel="noopener" target="_blank">GitHub Desktop を使った Web アプリの GitHub へのプッシュ</a> </strong>記事でも Node.js テンプレートを利用したプロジェクトを作成しましたが、Visual Studio からの直接デプロイで参照される <strong>Web. config</strong> ファイルを生成しないため、新たに別のテンプレートを利用してスケルトン プロジェクトを作成することにします。</p>
<p style="padding-left: 40px;">Visual Studio 2017&#0160; Professionalで Node.js プロジェクトを作成するには、インストールで Node.js 開発 を選択しておく必要があります。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a453fe2e200c-pi" style="display: inline;"><img alt="Nodejs_install" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a453fe2e200c image-full img-responsive" src="/assets/image_210174.jpg" title="Nodejs_install" /></a></p>
<ol>
<li>Visual Studio 2017 Professional エディションを起動して、Node.js プロジェクトを作成します。[ファイル(F)] メニューから [新規作成(N)] &gt;&gt; [プロジェクト(P)] を選択します。</li>
<li>[新しいプロジェクト] ダイアログが表示されたら、「<strong>空の Azure Node.js Web アプリケーション</strong>」テンプレートを選択して [OK] ボタンをクリックします。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4581547200c-pi" style="display: inline;"><img alt="New_project_dialog" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4581547200c image-full img-responsive" src="/assets/image_592195.jpg" title="New_project_dialog" /></a></li>
<li>作成されたスケルトン プロジェクト（<strong>NodejsWebApp2 プロジェクト</strong>）の実行をテストしてみます。[デバッグ(D)] メニューから [▶ デバッグッグの開始(S)] を選択して、Web ブラウザの起動後に server.js の実装である <strong>Hello World</strong>&#0160;が表示されることを確認してください。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4a1d991200b-pi" style="display: inline;"><img alt="Debug_app" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4a1d991200b img-responsive" src="/assets/image_327345.jpg" title="Debug_app" /></a></li>
</ol>
<hr />
<p><strong>Visual Studio から Azure への直接デプロイ</strong></p>
<p style="padding-left: 40px;">先に作成したスケルトン プロジェクトから作成される Web アプリ <strong>NodejsWebApp2 </strong> を、Visual Studio 内から Azure に直接デプロイしていきます。</p>
<ol>
<li>NodejsWebApp2 プロジェクトを開いている状態で、[ビルド(B)] メニューから [NodejsWebApp2 の発行(H)] を選択します。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a458178b200c-pi" style="display: inline;"><img alt="App_issue" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a458178b200c image-full img-responsive" src="/assets/image_299468.jpg" title="App_issue" /></a></li>
<li>[発行] ダイアログが表示されたら、[Microsoft Azure App Services(<span style="text-decoration: underline;">A</span>)] をクリックしてください。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4a5ef05200b-pi" style="display: inline;"><img alt="App_publish1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4a5ef05200b image-full img-responsive" src="/assets/image_567263.jpg" title="App_publish1" /></a></li>
<li>[App Service] ダイアログが表示されるので、[新規作成(N)...] ボタンをクリックしてください。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4814751200d-pi" style="display: inline;"><img alt="App_service" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4814751200d image-full img-responsive" src="/assets/image_427019.jpg" title="App_service" /></a></li>
<li>アプリ名と Visual Studio サブスクリプションが設定された [App Service の作成] ダイアログが表示されるので、必要に応じて新しい<strong><a href="https://docs.microsoft.com/ja-jp/azure/azure-resource-manager/manage-resource-groups-portal#what-is-a-resource-group" rel="noopener" target="_blank">リソース グループ</a></strong>やホスティング プランを選択（変更、あるいは、新規作成）してください。ここでは、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2019/05/deploy-web-app-to-azure.html" rel="noopener" target="_blank">Web アプリのデプロイ：Azure へのデプロイ</a></strong> の過程で作成された <strong>NodejsWebApp1-app</strong> リソース グループを使用することにします。アプリ名はユニークなものが自動的に設定されてきますので、これも必要に応じて変更してください。必要な入力/設定が完了したら、[作成(R)] ボタンをクリックしてください。<a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a481476a200d-pi" style="display: inline;"><img alt="App_service2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a481476a200d image-full img-responsive" src="/assets/image_82260.jpg" title="App_service2" /></a></li>
<li>しばらく時間がかかりますが、再び [発行] ダイアログが表示されるはずです。[接続] タブが表示され、すべての項目が設定されていることを確認したら、[発行(P)] ボタンをクリックして Web アプリのデプロイを開始します。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a48147c3200d-pi" style="display: inline;"><img alt="App_publish2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a48147c3200d image-full img-responsive" src="/assets/image_561093.jpg" title="App_publish2" /></a></li>
<li>しばらくの後、デプロイが正常に完了すると、デプロイされた Web アプリが表示されます（この例では <a href="https://nodejswebapp220190428054945.azurewebsites.net" rel="noopener" target="_blank">https://nodejswebapp220190428054945.azurewebsites.net</a>）。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a48147e0200d-pi" style="display: inline;"><img alt="Deployed_app" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a48147e0200d image-full img-responsive" src="/assets/image_283699.jpg" title="Deployed_app" /></a><br />初回のデプロイ後にコードに変更が生じた際には、2.以降の手順で設定した内容をそのまま流用して、再度、発行処理をすることで、新しい内容を反映することが出来ます。</li>
</ol>
<hr />
<p>このように、Visual Studio からの Azure へのデプロイは簡単におこなうことが出来ます。また、ここで使用した Node.js のほかに、ASP.NET など、Visual Studio と Azure App Services がサポートする Web サーバー テクノロジを利用する Web アプリのデプロイも可能です。</p>
<p>By Toshiaki Isezaki</p>
<hr />
