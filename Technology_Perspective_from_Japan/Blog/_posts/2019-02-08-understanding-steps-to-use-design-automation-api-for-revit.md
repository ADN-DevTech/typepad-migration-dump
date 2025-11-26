---
layout: "post"
title: "Design Automation API for Revit 開発手順の理解"
date: "2019-02-08 17:54:38"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2019/02/understanding-steps-to-use-design-automation-api-for-revit.html "
typepad_basename: "understanding-steps-to-use-design-automation-api-for-revit"
typepad_status: "Publish"
---

<p><a href="https://adndevblog.typepad.com/technology_perspective/2019/02/design-automation-api-for-revit-public-beta.html">前回のブログ記事</a>で、Design Automation API for Revit パブリックベータ版公開のアナウンスを致しました。<br />今回は、<a href="https://forge.autodesk.com/en/docs/design-automation/v3/tutorials/revit/">Developer&#39;s Guide の Step-by-Step チュートリアル</a>をベースに開発手順の流れを解説いたします。</p>
<p><strong>ステップ 1 – Revit アドインの変換（もしくは新規作成）とバンドルパッケージの準備</strong></p>
<p>まず、Design Automation for Revit で実行する Revit アドインを用意します。<br />既に Revit アドインのソースコードをお持ちの場合は、ソースコードの一部を修正して Design Automation 用に変換することができます。<br />パブリックベータ版では、Revit 2018 と Revit 2019 のアドインをサポートしております。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3e09edc200b-pi" style="display: inline;"><img alt="DesignAutomationRevit7" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3e09edc200b image-full img-responsive" src="/assets/image_537966.jpg" title="DesignAutomationRevit7" /></a></p>
<p>Revit アドインの開発方法については、下記のページをご参照ください。</p>
<ul>
<li><a href="https://www.autodesk.co.jp/developer-network/platform-technologies/revit">Revit デベロッパー センター</a><br /><br /></li>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2018/07/revit-api-developer-contents.html">Revit API 開発者向けコンテンツのご案内</a></li>
</ul>
<p>従来の Revit アドイン開発と同様に、Microsoft Visual Studio を利用し、Revit のバージョンに対応する .NET Framework のバージョンを設定します。</p>
<ul>
<li>Revit 2018 -&gt; .NET Framework 4.6</li>
<li>Revit 2019 -&gt; .NET Framework 4.7</li>
</ul>
<p>次に参照ライブラリ（ <a href="https://forge.autodesk.com/en/docs/design-automation/v3/tutorials/revit/step1-convert-addin/">DesignAutomationBridge.dll</a> ）をダウンロードして、プロジェクトの参照設定に追加します。同時に、RevitAPIUI.dll を参照設定している場合は、その参照を削除します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad39ae5d0200c-pi" style="display: inline;"><img alt="DesignAutomationRevit8" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad39ae5d0200c img-responsive" src="/assets/image_883084.jpg" title="DesignAutomationRevit8" /></a></p>
<p>Revit アドインは、通常は外部アプリケーションや外部コマンドのためのインターフェースを実装しますが、Design Automation の場合は、外部DBアプリケーションインターフェースを実装します。</p>
<p>DesignAutomationBridge.DesignAutomationReadyEvent イベントのイベントハンドラに、実際のカスタム処理を追加します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad39ae696200c-pi" style="display: inline;"><img alt="DesignAutomationRevit9" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad39ae696200c image-full img-responsive" src="/assets/image_766118.jpg" title="DesignAutomationRevit9" /></a></p>
<p>ローカルの Revit 環境でテストして、バグがないことを確認します。<br />DesignAutomationBridge.DesignAutomationReadyEvent イベントを一時的に ApplicationInitialized イベントに置き換えることで、Revit アプリケーションの初期化時にカスタム処理を実行することができます。</p>
<p>デバッグして問題がなければ、アセンブリファイルを<a href="https://forge.autodesk.com/en/docs/design-automation/v3/tutorials/revit/step4-publish-appbundle/">指定のディレクトリ形式</a>で ZIP 圧縮して、バンドルパッケージを作成します。<br /><br />これで、Revit アドインの準備は完了です。</p>
<p>&#0160;</p>
<p><strong>ステップ 2 – Forge アプリの作成</strong></p>
<p>Design Automation API を利用するためには、Forge アプリを作成する必要があります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3c0f28e200d-pi" style="display: inline;"><img alt="DesignAutomationRevit10" class="asset  asset-image at-xid-6a0167607c2431970b022ad3c0f28e200d img-responsive" src="/assets/image_519740.jpg" title="DesignAutomationRevit10" /></a></p>
<p>まずは作成するアプリケーションの情報を登録して、Client ID と Client Secret を取得します。</p>
<ul>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2016/07/app-registration-and-getting-keys-for-forge-api.html">Forge API を利用するアプリの登録とキーの取得</a></li>
</ul>
<p>Forge アプリは、Web 開発が主体となるため、開発環境は自由です。<br />サーバーには Node.js や ASP.NET などをお勧めしております。</p>
<ul>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2017/01/development-environment-for-forge.html">Forge の開発環境</a></li>
</ul>
<p>Design Automation API を利用するために、Authentication API の 2-legged 認証によって、Access Token（アクセス トークン） を取得します。<br />2-legged 認証については、下記のブログ記事で解説しております。</p>
<ul>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2016/08/oauth-authentication-scenario-on-forge.html">Forge での OAuth 認証シナリオ</a></li>
</ul>
<p>Forge アプリでアクセストークンを取得できれば、以降は Design Automation API の呼び出しになります。</p>
<p>&#0160;</p>
<p><strong>ステップ 3 – Forge アプリのニックネームの作成</strong></p>
<p>Design Automation は、Forge アプリを識別するために Client ID を使用しますが、これは人間には覚えづらい文字列（例えば、YnhayiOjhgnsd&amp;jsafh890ryehQW）となっています。<br /><br />Forge アプリから Design Automation に処理を依頼するときにも、この Client ID が必要となりますが、より覚えやすいニックネームで名前を付けてマッピングを行うことができます。</p>
<p>&#0160;</p>
<p><strong>ステップ 4 – Appbundle のパブリッシュ</strong></p>
<p>ステップ 1 で作成した Revit アドインを Design Automation で実行するために、バンドルパッケージを Appbundle として Design Automation に登録します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3e0a165200b-pi" style="display: inline;"><img alt="DesignAutomationRevit11" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3e0a165200b image-full img-responsive" src="/assets/image_752344.jpg" title="DesignAutomationRevit11" /></a>Appbundle を作成する際、任意の名前で ID を指定し、Revit アドインを実行する Revit のバージョン（2018 もしくは 2019）を指定します。<br /><br />Appbundle の作成が成功すると、レスポンスの JSON データに、バンドルパッケージをアップロードするエンドポイント URL が返されます。このエンドポイント URL に対して、バンドルパッケージをアップロードします。</p>
<p>実はこれだけでは Appbundle の作成は完了しておりません。<br /><br />作成した Appbundle は、開発中に何度もバンドルパッケージを再アップロードしたり、更新が発生します。<br /><br />そのような状況に対応するために、Appbundle には、バージョンとエイリアスを指定する仕組みが備わっております。エイリアスは、Appbundle のバージョンに対して開発者が任意の名称でつけるラベルです。</p>
<p>たとえば、はじめて Appbundle を作成した場合、バージョンは自動的に 1 に指定されます。<br />このバージョンをテスト版として利用する場合は、エイリアスに test や development といった名前をつけます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3e0a20d200b-pi" style="display: inline;"><img alt="DesignAutomationRevit12" class="asset  asset-image at-xid-6a0167607c2431970b022ad3e0a20d200b img-responsive" src="/assets/image_765255.jpg" title="DesignAutomationRevit12" /></a></p>
<p>このように Appbundle のバージョン 1 に対して、test とエイリアス名をつけた場合、ステップ 3 で作成したニックネームと合わせて、Appbundle は下記のように識別することができます。</p>
<ul>
<li>形式: YourNickname.SomeAppBundleId+SomeAliasName</li>
<li>例: MyFirstForgeApp.DeleteWallApp.test</li>
</ul>
<p>&#0160;</p>
<p><strong>ステップ 5 – Activity のパブリッシュ</strong></p>
<p>ステップ 4 で登録した Appbundle では、実際にどういったデータをインプットして、どういったデータをアウトプットするのか定義しませんでした。<br />そのため、次は Activity を登録して、Design Automation で実行されるアクションを定義します。</p>
<p>たとえば、Revit モデルとテキストファイルをインプットとして Revit アドインに渡して、アドインの処理の結果として、Revit モデルを返却するといったように、入出力のパラメータを定義します。</p>
<p>そして、このアクション定義と Appbundle を紐づけるために、先ほどの Appbundle の識別コードを指定します。<br />Activity でも、バージョンとエイリアスの仕組みがありますので、任意の名前をつけます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad39ae8e1200c-pi" style="display: inline;"><img alt="DesignAutomationRevit13" class="asset  asset-image at-xid-6a0167607c2431970b022ad39ae8e1200c img-responsive" src="/assets/image_350498.jpg" title="DesignAutomationRevit13" /></a></p>
<p>Activity も同様に下記のような形式で識別することができるようになります。</p>
<ul>
<li>形式: YourNickname.SomeActivityId+SomeAliasName</li>
<li>例: MyFirstForgeApp.DeleteWallActivity.test</li>
</ul>
<p>バージョンとエイリアスは、Design Automation API V3 で新規に追加された仕組みです。開発時に、リリース用、テスト用、ステージング用など、それぞれラベリングしてバージョン指定することで、開発効率が向上します。</p>
<p>&#0160;</p>
<p><strong>ステップ 6 – Workitem のポスト</strong></p>
<p>Appbundle と Activity の登録ができれば、最後は Design Automation にジョブを依頼します。<br />このジョブを Workitem と呼びます。Workitem が Activity を呼び出し、Activity は Appbundle を呼び出します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3e0a32a200b-pi" style="display: inline;"> </a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad39ae9ff200c-pi" style="display: inline;"><img alt="DesignAutomationRevit14" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad39ae9ff200c image-full img-responsive" src="/assets/image_681765.jpg" title="DesignAutomationRevit14" /></a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3e0a32a200b-pi" style="display: inline;"><br /></a></p>
<p>Workitem では、Revit アドインに渡す Revit モデルやテキストファイルの実際の URL、出力結果のアップロード先のクラウドストレージの URL と、実行する Activity を指定します。<br /><br />レスポンスには Workitem の ID が返却されますので、この ID を利用して、処理の進捗状況を確認することができます。</p>
<p>このように、WorkItem と Activity、Appbundle は相互に関連付けられて、最終的に Revit アドインが実行される仕組みになっております。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3e0a3e4200b-pi" style="display: inline;"><img alt="DesignAutomationRevit15" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3e0a3e4200b image-full img-responsive" src="/assets/image_732052.jpg" title="DesignAutomationRevit15" /></a></p>
<p>今回は、Design Automation API を利用して Forge アプリを開発する手順をご紹介しました。より詳細な解説は、先日の <a href="https://adndevblog.typepad.com/technology_perspective/2019/01/bim-forge-seminar-summary.html">BIM &amp; Forge セミナーのレコーディングとスライド</a>でご覧いただけます。</p>
<p>次回は、Postman を使って実際に API を利用する方法をご紹介いたします。</p>
<p>By Ryuji Ogasawara</p>
