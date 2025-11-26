---
layout: "post"
title: "Design Automation API for Revit - Learn Autodesk Forge チュートリアル .NET Core Sample で動作確認"
date: "2019-03-22 07:44:39"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2019/03/design-automation-api-for-revit-learn-autodesk-forge-tutorial-sample-part1.html "
typepad_basename: "design-automation-api-for-revit-learn-autodesk-forge-tutorial-sample-part1"
typepad_status: "Publish"
---

<p>Forge Platform API を包括的に習得いただけるチュートリアル <strong><a href="http://learnforge.autodesk.io/#/">Learn Autodesk Forge（英語）</a></strong>の Design Automation API のセクション、<strong><a href="http://learnforge.autodesk.io/#/tutorials/modifymodels">Modify your models</a> </strong>について、ご紹介しています。</p>
<p><a href="https://adndevblog.typepad.com/technology_perspective/2019/03/setup-design-automation-api-for-revit-learn-autodesk-forge-tutorial-sample.html"><strong>前回のセットアップ手順</strong></a>にて、動作確認のための環境が準備できていることを前提とします。<br />今回は、<strong><a href="https://github.com/Autodesk-Forge/learn.forge.designautomation">learn.forge.designautomation サンプルのソースコード</a></strong>を入手して、ローカル環境で実行する手順をご紹介します。</p>
<p><strong>1. ソースコードの入手</strong><br /><strong>コマンド プロンプト</strong>を起動し、CD コマンドでカレント フォルダを任意のディレクトリ（ドキュメント ディレクトリの場合は C:\Users\&lt;Windowsログインユーザ名&gt;\Documents など）に移動してから、git clone コマンドで GitHub のソースコードの URL をパラメータに指定して、リポジトリ の内容をクライアント コンピュータにコピーします。</p>
<p><strong>git clone https://github.com/Autodesk-Forge/learn.forge.designautomation.git</strong> と入力してください。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4974646200b-pi" style="display: inline;"><img alt="LearnForgeTutorialPart1_1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4974646200b image-full img-responsive" src="/assets/image_959620.jpg" title="LearnForgeTutorialPart1_1" /></a></p>
<p>コピーされたリポジトリが、指定したディレクトリ（ドキュメントの場合は、C:\Users\&lt;Windows ユーザ名&gt;\Documents ディレクトリ）直下の<strong> learn.forge.designautomation フォルダ配下</strong>にコピーされていることを確認してください。</p>
<p>&#0160;</p>
<p><strong>2. Visual Studio でプロジェクトを開く</strong></p>
<p><strong>Visual Studio 2017</strong> を起動し、[ファイル]-&gt;[開く]-&gt;[プロジェクト/ソリューション]から、learn.forge.designautomation フォルダ配下にある <strong>designautomation.sln</strong> を選択してください。</p>
<p>ソリューション &quot;designautomation&quot; が開きます。<br />[表示]-&gt;[ソリューションエクスプローラー]ウィンドウを開くと、その配下に以下の<strong>5つのプロジェクト</strong>が一覧で表示されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a472ad8a200d-pi" style="display: inline;"><img alt="LearnForgeTutorialPart1_2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a472ad8a200d img-responsive" src="/assets/image_682965.jpg" title="LearnForgeTutorialPart1_2" /></a></p>
<p>[表示]-&gt;[エラー一覧]ウィンドウを開き、エラーが何もないことを確認してください。</p>
<p>ここで、何らかのエラーがある場合は、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2019/03/setup-design-automation-api-for-revit-learn-autodesk-forge-tutorial-sample.html">前回のセットアップ手順</a></strong>にて、下記のいずれかの要件が満たされていない可能性があります。<br />再度、下記のプログラムがインストールされているか、ご確認ください。</p>
<ul>
<li>.NET Core 2.1 開発ツール</li>
<li>.NET Framework 4.7 SDK/Targeting Pack もしくは 4.7.1 SDK/Targeting Pack</li>
<li>RevitAPI.dll</li>
</ul>
<p>&#0160;</p>
<p><strong>3. Revit アドインのビルド</strong></p>
<p>Revit アドインのバンドルパッケージを作成するためには、まずは Revit アドインをビルドする必要があります。<br />[ソリューションエクスプローラー]ウィンドウから、<strong>Revit アドインのプロジェクト [UpdateRVTParam]</strong> をクリックして展開し、Command.cs を開きます。</p>
<p>すると、[エラー一覧]ウィンドウに、下記の 2つの名前空間が見つからないという警告とそれに関連するエラーが表示される場合があります。</p>
<ul>
<li><em>警告 参照コンポーネント &#39;Newtonsoft.Json&#39; が見つかりませんでした。 UpdateRVTParam</em></li>
<li><em>警告 参照コンポーネント &#39;DesignAutomationBridge&#39; が見つかりませんでした。 UpdateRVTParam</em></li>
</ul>
<p>これは、参照設定で指定されている<strong>「DesignAutomationBridge.dll」</strong>と<strong>「Newtonsoft.Json.dll」</strong>がまだ取得されていないことが原因です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a472add6200d-pi" style="display: inline;"><img alt="LearnForgeTutorialPart1_3" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a472add6200d img-responsive" src="/assets/image_67091.jpg" title="LearnForgeTutorialPart1_3" /></a></p>
<p>このサンプルでは、開発者が作成したコードを共有するための<a href="https://docs.microsoft.com/ja-jp/nuget/what-is-nuget"><strong>パッケージマネージャー NuGet</strong> </a>を通じて、上記のパッケージを取得するように設定されております。<br />そのため、インターネットに接続されていれば、自動的にこれらのパッケージはダウンロードされるはずです。</p>
<p>パッケージが適切にインストールされているか確認するためには、[ソリューションエクスプローラー]ウィンドウから <strong>UpdateRVTParam プロジェクト</strong>配下の[参照]を右クリックし、<strong>[NuGet パッケージの管理...]</strong>を選択してください。</p>
<p>NuGet パッケージマネージャが開き、[インストール済み]タブに 3つのパッケージがインストールされていることがわかります。</p>
<ul>
<li>1つ目のパッケージは、Revit アドインを Design Automation API 用に変換するために利用するパッケージです。</li>
<li>2つ目のパッケージは、Revit アドインの実装に使用する C# 言語のコンパイルをサポートするためのパッケージです。</li>
<li>3つ目のパッケージは、.NET 環境で JSON フォーマットのデータを扱う際に利用するパッケージです。</li>
</ul>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4497dda200c-pi" style="display: inline;"><img alt="LearnForgeTutorialPart1_4" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4497dda200c image-full img-responsive" src="/assets/image_836076.jpg" title="LearnForgeTutorialPart1_4" /></a></p>
<p>また下記のような警告も表示される場合があります。これは、UpdateRVTParam プロジェクトのビルド設定で、<strong>ビルドターゲットプラットフォーム</strong>が適切に選択されていないことが原因です。</p>
<p style="padding-left: 40px;"><em>警告 構築されているプロジェクトのプロセッサ アーキテクチャ &quot;MSIL&quot; と、参照 &quot;RevitAPI&quot; のプロセッサ アーキテクチャ &quot;AMD64&quot; の間には不一致がありました。この不一致は、ランタイム エラーを発生させる可能性があります。プロジェクトと参照の間でプロセッサ アーキテクチャが一致するように、構成マネージャーを使用してターゲットとするプロジェクトのプロセッサ アーキテクチャを変更するか、ターゲットとするプロジェクトのプロセッサ アーキテクチャに一致するプロセッサ アーキテクチャとの依存関係を参照で設定することを検討してください。</em></p>
<p>この警告を解決するためには、[ソリューションエクスプローラー]ウィンドウから <strong>UpdateRVTParam プロジェクト</strong>を右クリックし、プロパティを表示します。</p>
<p>[ビルド]タブにある[全般]セクションの[プラットフォームターゲット]の設定が<strong>「Any CPU」</strong>になっているので、<strong>「x64」</strong>に切り替えます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a472ae3e200d-pi" style="display: inline;"><img alt="LearnForgeTutorialPart1_5" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a472ae3e200d image-full img-responsive" src="/assets/image_575951.jpg" title="LearnForgeTutorialPart1_5" /></a></p>
<p>次に、もう一度、<strong>UpdateRVTParam プロジェクト</strong>のプロパティを開き、[アプリケーション]タブを開いて、[既定の名前空間]の設定を 「<strong>Autodesk.Forge.Sample.DesignAutomation.Revit</strong>」に変更し、[ターゲットフレームワーク]の設定が <strong>.NET Framework 4.7 または 4.7.1</strong> になっていることを確認してください。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a472ae5f200d-pi" style="display: inline;"><img alt="LearnForgeTutorialPart1_6" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a472ae5f200d image-full img-responsive" src="/assets/image_877021.jpg" title="LearnForgeTutorialPart1_6" /></a></p>
<p>[エラー一覧]ウィンドウから警告がなくなったことを確認し、<strong>UpdateRVTParam プロジェクトのビルドを実行してください。</strong><br />[出力]ウィンドウにビルド結果が表示されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a472ae97200d-pi" style="display: inline;"><img alt="LearnForgeTutorialPart1_7" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a472ae97200d image-full img-responsive" src="/assets/image_196640.jpg" title="LearnForgeTutorialPart1_7" /></a></p>
<p>ビルドが正常終了していれば、下記のようなメッセージが出力されているはずです。</p>
<ul>
<li><em>Creating archive: C:\Users\ogasawr\GitHub Repo\learn.forge.designautomation\updateRVTParam\../forgesample/wwwroot/bundles/UpdateRVTParam.zip</em></li>
</ul>
<p>これは、ビルド実行後のイベントで、<strong>AppBundle 用のディレクトリ構成でバンドルパッケージを作成し、ZIP 圧縮して、./forgesample/wwwroot/bundles/UpdateRVTParam.zip として保存する処理</strong>が実行されていることを意味します。</p>
<p>UpdateRVTParam プロジェクトのプロパティにある[ビルド イベント]タブから、<strong>[ビルド後イベントのコマンドライン]</strong>を表示すると、実際の処理を確認することができます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4497ec0200c-pi" style="display: inline;"><img alt="LearnForgeTutorialPart1_8" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4497ec0200c image-full img-responsive" src="/assets/image_493155.jpg" title="LearnForgeTutorialPart1_8" /></a></p>
<p>[ソリューションエクスプローラー]ウィンドウから、forgesample プロジェクト配下の[bundles]フォルダを右クリックし、「エクスプローラーでフォルダーを開く」を選択し、<strong>UpdateRVTParam.zip ファイル</strong>が保存されていれば、バンドルパッケージの準備は完了です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a472aec7200d-pi" style="display: inline;"><img alt="LearnForgeTutorialPart1_9" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a472aec7200d img-responsive" src="/assets/image_435767.jpg" title="LearnForgeTutorialPart1_9" /></a></p>
<p>Visual Studio 2017 は起動した状態のまま、次のステップに移行します。</p>
<p>&#0160;</p>
<p><strong>4. ngrok の起動</strong></p>
<p>次に、<strong>ngrok ツール</strong>を使用して、<strong>Design Automation API からのコールバックによる通知処理を受信</strong>できるように、ローカル環境で起動するforgesample プロジェクトを<strong>インターネット上に一時的に公開</strong>します。</p>
<p><strong>コマンド プロンプト</strong>を起動し、CD コマンドで <strong>ngrok.exe</strong> が保存されているディレクトリに移動します。<br />そして、下記のコマンドを実行してください。</p>
<ul>
<li><strong>ngrok http 3000 -host-header=&quot;localhost:3000&quot;</strong></li>
</ul>
<p>すると、下記のように ngrok ツールが起動し、<strong>&quot;localhost:3000&quot;</strong> というローカルアドレスが、<strong>&quot;http://********.ngrok.io&quot;</strong>という、ランダムな文字列のホスト名が割り当てられたアドレスにフォワードされます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a472af0c200d-pi" style="display: inline;"><img alt="LearnForgeTutorialPart1_10" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a472af0c200d image-full img-responsive" src="/assets/image_767936.jpg" title="LearnForgeTutorialPart1_10" /></a></p>
<p>このアドレスは、ngrok ツールが起動している間だけ有効です。<br />そのため、サンプルを実行する間は、このツールを起動したままの状態にします。</p>
<p>&#0160;</p>
<p><strong>5. 環境変数の設定</strong></p>
<p>forgesample プロジェクトは、Design Automation API を利用する Forge アプリです。このサンプルを実行するためには、環境変数といくつかの設定を行う必要があります。</p>
<p>Visual Studio 2017 に戻り、[ソリューションエクスプローラー]ウィンドウから、<strong>forgesample プロジェクト</strong>を右クリックし、プロパティを選択してください。</p>
<p>[デバッグ]タブを開き、それぞれ以下の設定を行ってください。</p>
<ul>
<li>[プロファイル]設定: プルダウンメニューから、<strong>「forgesample」</strong>を選択</li>
<li>[ブラウザの起動]設定: チェックボックスを<strong>有効化</strong></li>
<li>環境変数:<br />
<ul>
<li>ASPNETCORE_ENVIRONMENT: <strong>Development</strong></li>
<li>FORGE_CLIENT_ID: <strong>Forge アプリの Client ID</strong></li>
<li>FORGE_CLIENT_SECRET: <strong>Forge アプリの Client Secret</strong></li>
<li>ASPNETCORE_URLS: <strong>http://localhost:3000</strong></li>
</ul>
</li>
<li>FORGE_WEBHOOK_URL: <strong>ngrok ツールで割り当てられたアドレス</strong></li>
<li>[Web サーバーの設定]-&gt;[アプリ URL]: <strong>「http://localhost:3000/」</strong>に変更</li>
</ul>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4498104200c-pi" style="display: inline;"><img alt="LearnForgeTutorialPart1_11" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4498104200c image-full img-responsive" src="/assets/image_153657.jpg" title="LearnForgeTutorialPart1_11" /></a></p>
<p>これで Forge アプリの準備は完了です。</p>
<p>&#0160;</p>
<p><strong>6. デバッグ実行</strong></p>
<p>[ソリューションエクスプローラー]ウィンドウで forgesample プロジェクトを選択している状態で、下図のように実行するプロジェクトを forgesample に変更し、RUN ボタンをクリックします。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a472b1e5200d-pi" style="display: inline;"><img alt="LearnForgeTutorialPart1_12" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a472b1e5200d image-full img-responsive" src="/assets/image_945994.jpg" title="LearnForgeTutorialPart1_12" /></a></p>
<p>すると、Web ブラウザが起動し、このサンプルの画面が表示されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4498150200c-pi" style="display: inline;"><img alt="LearnForgeTutorialPart1_13" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4498150200c image-full img-responsive" src="/assets/image_95709.jpg" title="LearnForgeTutorialPart1_13" /></a></p>
<p>&#0160;</p>
<p><strong>7. AppBundle の登録</strong></p>
<p>画面右上の [Configure]ボタンをクリックすると、ダイアログが表示されます。<br />「Select engine」の設定で、<strong>「Autodesk.Revit+2019」</strong>を選択してください。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4974a2c200b-pi" style="display: inline;"><img alt="LearnForgeTutorialPart1_14" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4974a2c200b image-full img-responsive" src="/assets/image_711438.jpg" title="LearnForgeTutorialPart1_14" /></a></p>
<p>[Create/Update]ボタンをクリックすると、AppBundle、Activity の登録処理が始まり、処理結果が出力コンポーネントに表示され、左サイドメニューの[Existing activities]に<strong>「UpdateRVTParamActivity+dev」</strong>という Activity ID が表示されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a472b27e200d-pi" style="display: inline;"><img alt="LearnForgeTutorialPart1_15" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a472b27e200d image-full img-responsive" src="/assets/image_361531.jpg" title="LearnForgeTutorialPart1_15" /></a></p>
<p>&#0160;</p>
<p><strong>8. Revit サンプルプロジェクトをダウンロードと確認</strong></p>
<p><a href="https://github.com/Developer-Autodesk/learn.forge.designautomation/tree/master/sample%20files"><strong>こちらのページ</strong></a>から、<strong>revit sample file.rvt</strong> をダウンロードしてください。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4974ac1200b-pi" style="display: inline;"><img alt="LearnForgeTutorialPart1_16" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4974ac1200b image-full img-responsive" src="/assets/image_852590.jpg" title="LearnForgeTutorialPart1_16" /></a></p>
<p>Revit 2019 でプロジェクトを開くと、シンプルな壁と窓が配置されていることがわかります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a472b319200d-pi" style="display: inline;"><img alt="LearnForgeTutorialPart1_17" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a472b319200d image-full img-responsive" src="/assets/image_366093.jpg" title="LearnForgeTutorialPart1_17" /></a></p>
<p><strong> <a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4974b1b200b-pi" style="display: inline;"><img alt="LearnForgeTutorialPart1_18" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4974b1b200b image-full img-responsive" src="/assets/image_792800.jpg" title="LearnForgeTutorialPart1_18" /></a><br /></strong></p>
<p>&#0160;</p>
<p><strong>9. WorkItem の作成</strong></p>
<p><strong>[Width]</strong> と <strong>[Height]</strong> に値を入力し、<strong>[Input file]</strong>のファイル選択で、ダウンロードした <strong>revit sample file.rvt ファイル</strong>を選択します。<br /><strong>[Start workitem]ボタン</strong>をクリックすると、<strong>WorkItem</strong> が作成され、処理が開始します。</p>
<p>※このプロジェクト単位は、フィートに設定されているため、<strong>フィート単位</strong>で値を指定してください。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4498487200c-pi" style="display: inline;"><img alt="LearnForgeTutorialPart1_19" class="asset  asset-image at-xid-6a0167607c2431970b0240a4498487200c img-responsive" src="/assets/image_930614.jpg" title="LearnForgeTutorialPart1_19" /></a></p>
<p>&#0160;</p>
<p><strong>10. Revit プロジェクトのダウンロードと確認</strong></p>
<p>WorkItem の処理が完了すると、出力コンポーネントにログが表示されます。<br />そして、処理が成功した場合は、最終行に<strong>「Download result file here」リンク</strong>が表示されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a472b47a200d-pi" style="display: inline;"><br /><img alt="LearnForgeTutorialPart1_20" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a472b47a200d image-full img-responsive" src="/assets/image_786503.jpg" title="LearnForgeTutorialPart1_20" /></a></p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a472b4c3200d-pi" style="display: inline;"><img alt="LearnForgeTutorialPart1_21" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a472b4c3200d image-full img-responsive" src="/assets/image_807277.jpg" title="LearnForgeTutorialPart1_21" /></a></p>
<p>Revit プロジェクトをダウンロードして、Revit 2019 で開いて確認してみましょう。<br />窓ファミリタイプの幅と高さが設定した値に変更されていることがわかります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4974cb0200b-pi" style="display: inline;"><img alt="LearnForgeTutorialPart1_22" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4974cb0200b image-full img-responsive" src="/assets/image_797227.jpg" title="LearnForgeTutorialPart1_22" /></a></p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4498432200c-pi" style="display: inline;"><img alt="LearnForgeTutorialPart1_23" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4498432200c image-full img-responsive" src="/assets/image_899281.jpg" title="LearnForgeTutorialPart1_23" /></a></p>
<p>これで、Design Automation API のサンプルの動作を確認することができました。<br />次回は、処理結果の Revit プロジェクトを Model Derivative API で変換して、Forge Viewer に表示する処理を追加する手順をご紹介します。</p>
<p>By Ryuji Ogasawara</p>
