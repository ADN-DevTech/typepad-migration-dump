---
layout: "post"
title: "Visual Studio Code での Forge 開発"
date: "2020-06-12 00:07:00"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2020/06/forge-development-using-vs-code.html "
typepad_basename: "forge-development-using-vs-code"
typepad_status: "Publish"
---

<p>AutoCAD 2021 での <a href="https://adndevblog.typepad.com/technology_perspective/2020/04/autolisp-development-using-vs-code.html" rel="noopener" target="_blank">AutoLISP 開発</a>で、Fusion 360 の <a href="https://adndevblog.typepad.com/technology_perspective/2020/05/python-development-for-fusion-360-using-vs-code.html" rel="noopener" target="_blank">Python スクリプト・アドイン開発</a>で、それぞれ利用されている Visual Studio Code、通称、VS Code ですが、多様な利用を可能にしているのが、だれでも自由に開発することが出来る <strong><a href="https://code.visualstudio.com/api/get-started/your-first-extension" rel="noopener" target="_blank">エクステンション（Extension）</a></strong>と呼ばれる VS Code&#0160; の優れた機能拡張です。</p>
<p>VS Code のエクステンションには、Autodesk Forge の開発を支援する <em><strong><a href="https://marketplace.visualstudio.com/items?itemName=petrbroz.vscode-forge-tools" rel="noopener" target="_blank">Autodesk Forge Tools</a> </strong>エクステンションも</em>用意されています。VS Code で Autodesk Forge Tools エクステンションを利用出来るようにする手順は、次のとおりです。</p>
<hr />
<ol>
<li><a href="https://code.visualstudio.com/Download" rel="noopener" target="_blank">https://code.visualstudio.com/Download</a> から VS Code をダウンロード、インストールします。</li>
<li>VS Codeを起動して、VS Code の画面左手のアクティビティバーから「拡張機能」アイコン（①）をクリックして、検索ボックスに「Autodesk Forge Tools」（②）と入力後、リターンキーを押下します。画面右手に <strong><a href="https://marketplace.visualstudio.com/items?itemName=petrbroz.vscode-forge-tools" rel="noopener" target="_blank">Autodesk Forge Tools</a></strong> の公開ページが表示されるので、[インストール]（③）をクリックして、Autodesk Forge Tools をインストールしてください。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263ec19e32e200c-pi" style="display: inline;"><img alt="Autodesk_forge_tools" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263ec19e32e200c image-full img-responsive" src="/assets/image_158773.jpg" title="Autodesk_forge_tools" /></a></li>
<li>Autodesk Forge Tools エクステンションがインストールされると、左手のアクティビティバーに Forge アイコンが表示されます。Forge アイコンをクリックすると、アクティビティバー右手に Forge サイドバーが表示されるはずです。この時、画面右下に Autodesk Forge Tools エクステンションから credentials（Client ID、Client Secret） が未設定であるとの通知が表示されますが、ここでは無視してください。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0264e2da9839200d-pi" style="display: inline;"><img alt="Right_after_install" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0264e2da9839200d image-full img-responsive" src="/assets/image_574349.jpg" title="Right_after_install" /></a></li>
<li>次に、Autodesk Forge Tools エクステンションを利用するための設定をしていきます。アクティビティバーの「拡張機能アイコン」（④）をクリックして、サイドバーパネルに表示される「有効」な Autodesk Forge Tools エクステンション タイルから、管理（歯車）アイコン（⑤）をクリックしてください。画面に Autodesk Forge Tools エクステンションの設定項目が表示されたら、「<strong>settings.json で編集</strong>」リンク（⑥）をクリックします。<a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e94884c7200b-pi" style="display: inline;"></a><br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e94885d9200b-pi" style="display: inline;"><img alt="Before_settings" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e94885d9200b image-full img-responsive" src="/assets/image_540769.jpg" title="Before_settings" /></a></li>
<li>settings.json が表示されたら、&quot;<strong>autodesk.forge.environments</strong>&quot; セクションに、”<strong>title</strong>&quot;（設定名）、&quot;<strong>clientId</strong>&quot;（VS Code 環境で使用する Client ID）、&quot;<strong>clientSecret</strong>&quot;（VS Code 環境で使用する Client Secret）、&quot;<strong>region</strong>&quot;（VS Code 環境からデータ アクセスで使用する Data Management API のストレージ地域、日本からは、通常、<strong>US</strong>）を次のように追加して、settings.json を上書き保存してください。<br />なお、&quot;autodesk.forge.environments&quot; セクションには、{ ～ } ブロックで複数の credential を設定することが出来ます。<br />
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-4">{
    &quot;extensions.confirmedUriHandlerExtensionIds&quot;: [
        &quot;Autodesk.autolispext&quot;
    ],
    &quot;editor.minimap.enabled&quot;: true,
    &quot;files.associations&quot;: {
        &quot;*.cps&quot;: &quot;javascript&quot;
    },
    &quot;window.zoomLevel&quot;: 0,
    &quot;workbench.activityBar.visible&quot;: true,
<strong>    &quot;autodesk.forge.environments&quot;: [                
        {
            &quot;title&quot;: &quot;my environment&quot;,
            &quot;clientId&quot;: &quot;&lt;&lt;YOUR CLIENT ID&gt;&gt;&quot;,
            &quot;clientSecret&quot;: &quot;&lt;&lt;YOUR CLIENT SECRET&gt;&gt;&quot;,
            &quot;region&quot;: &quot;US&quot;
        }       
    ],
</strong>}
</code></pre>
<a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263ec1a2937200c-pi" style="display: inline;"><img alt="Add_credentials" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263ec1a2937200c image-full img-responsive" src="/assets/image_276846.jpg" title="Add_credentials" /></a></li>
</ol>
<hr />
<p>上記のセットアップ終了後、再度、アクティビティバーの Forge アイコンをクリックすると、設定した Client ID/Client Secret でアクセスすることが可能な情報が、Forge サイドバーに表示されます。</p>
<p>Forge サイドバーの各パネル上では、右クリック メニューからさまざまなアクションを実行することが出来ます。例えば、BIM 360 Docs 上に保存された Revit プロジェクト ファイル（.rvt ファイル）に、既に変換済の viewables が存在する場合、VS Code 上で Forge Viewer を使ってプレビュー表示することも出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e9488b89200b-pi" style="display: inline;"><img alt="Success" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e9488b89200b image-full img-responsive" src="/assets/image_18116.jpg" title="Success" /></a></p>
<p>Forge サイドバーに表示される各パネルの役割は、おおまかに次のとおりです。</p>
<p><strong>BUCKET &amp; DERIVATIVES</strong></p>
<p style="padding-left: 40px;">2-legged OAuth で取得した Access Token&#0160; を用いて、OSS Bucket、Bucket 内の Object（ファイル）、それらの派生物の情報を表示や操作が出来ます。また、Model Derivative API を用いた変換と、SVF 変換時の 2D 図面、3D&#0160; モデルの Forge Viewer での表示もサポートしています。ほとんどの操作は、表示される Bucket 内のノードを右クリックするか、「コマンドパレット」（Ctrl + Shift + P または Cmd + Shift + P）からアクセスすることが出来ます。</p>
<p><strong>HUB &amp; DERIVATIVES</strong></p>
<p style="padding-left: 40px;">初期状態では、2-legged OAuth で取得した Access Token&#0160; を用いて、Hub 配下の プロジェクト、プロジェクト内の Item や Version、また、変換済 SVF の表示をサポートしています。ほとんどの操作は、表示されるパネル内のノードを右クリックするか、「コマンドパレット」（Ctrl + Shift + P または Cmd + Shift + P）からアクセスすることが出来ます。</p>
<p><strong>WEBHOOKS</strong></p>
<p style="padding-left: 40px;">Data Management API、Model Derivative API、Fusion Lifevycle、Revit Cloud Worksharing の<a href="https://forge.autodesk.com/en/docs/webhooks/v1/reference/events/" rel="noopener" target="_blank">各種イベント</a>に対する Webhooks API の登録と削除、情報取得をサポートします。</p>
<p><strong>DESIGN AUTOMATION</strong></p>
<p style="padding-left: 40px;">独自に定義、作成した Design Automation API&#0160; v3 の Activity と App Bundle に加え、共有 Activity と共有 App Bundle の情報にアクセスすることが出来ます。関連するすべての操作は、個々のツリーノードを右クリックするか、コマンドパレットにコマンド名を入力することで利用できます。 コマンドパレットは「ファジー検索」をサポートしていることに注意してください。つまり、</p>
<p>コマンドパレットで利用することが出来るコマンドは、コマンドパレットに「Forge」と入力すると、ファジー検索によって一覧表示することが出来ます。同様に、「upd bun」と入力すると、利用可能なコマンドから「Forge Design Automation：Update App Bundle」と「Forge Design Automation：Update App」に絞り込むことも可能です。&#0160;</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0264e2daa945200d-pi" style="display: inline;"><img alt="Command_list" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0264e2daa945200d image-full img-responsive" src="/assets/image_575625.jpg" title="Command_list" /></a></p>
<hr />
<p>Autodesk Forge Tools エクステンションは、個々の endpoint をラップしたテスト ハーネスとして利用していただくことが出来ます。endpoint 呼び出し時のリクエスト ヘッダーやリクエスト ボディ、また、一連の処理手順を把握するような場面では、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2016/09/restful-api-and-testing-tools.html" rel="noopener" target="_blank">Postman</a></strong> などのツールの利用をお勧めします。</p>
<p>By Toshiaki Isezaki</p>
