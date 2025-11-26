---
layout: "post"
title: "Data Visualization：リファレンス アプリで独自モデルを表示"
date: "2021-05-05 00:19:42"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/05/data-viz-ref-app-show-own-model.html "
typepad_basename: "data-viz-ref-app-show-own-model"
typepad_status: "Publish"
---

<p>デジタルツインを実現する Data Visualization エクステンションが 2021年5月3日（米国太平洋標準時）に正式にリリースされました。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e9a2eedb200b-pi" style="display: inline;"><img alt="Preview" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e9a2eedb200b image-full img-responsive" src="/assets/image_180794.jpg" title="Preview" /></a></p>
<p><strong><a href="https://adndevblog.typepad.com/technology_perspective/2021/04/data-viz-ref-app-local-run.html" rel="noopener" target="_blank">Data Visualization :リファレンス アプリのローカル実行</a></strong> の記事では、Data Visualization エクステンションを使ったリファレンス アプリの GitHub リポジトリの内容を、Windows のローカル環境で実行する手順をご紹介しました。</p>
<p>リファレンス アプリが表示する 3D モデルには、あらかじめ用意・変換された rme_advanced_sample_project.rvt が使用されています。ただし、独自モデルも表示させることが出来ます。その手順 は <a class="menu-leaf" href="https://forge.autodesk.com/en/docs/dataviz/v1/developers_guide/quickstart/replace_model/" id="ad70c654-21e4-033a-2e30-3f2e6f0d9be6" rel="noopener" target="_blank">Replacing the Default Model</a> に記載されていますが、やはり、ドキュメントが Mac 環境の「ターミナル」での操作を前提に記述されているので、今回も <strong>git for Windows</strong>&#0160;に含まれる Git Bash を使って、Windows のローカル環境での手順をご紹介しておきたいと思います。</p>
<hr />
<p><strong>準備</strong></p>
<p style="padding-left: 40px;"><strong><a href="https://adndevblog.typepad.com/technology_perspective/2021/04/data-viz-ref-app-local-run.html" rel="noopener" target="_blank">Data Visualization :リファレンス アプリのローカル実行</a></strong> でご紹介した内容が実行出来るようになっていることを前提とします。まだ環境を用意されていない場合には、まず、同記事の内容をご用意ください。</p>
<p style="padding-left: 40px;"><span style="background-color: #ffff00;"><strong>ご注意：</strong></span><span style="text-decoration: underline;">リファレンス アプリでは</span>、英語版 Revit で保存した英語の要素カテゴリ名を持つ Revit プロジェクト ファイルの使用を前提にしています。日本版 Revit で保存した Revit プロジェクト ファイルでは、「レベル」や「部屋」を正しく認識しないため、手早く Data Visualization エクステンションの機能をお試しいただく目的で、Autodesk Knowledge Network 記事 <a href="https://knowledge.autodesk.com/ja/support/revit-products/troubleshooting/caas/CloudHelp/cloudhelp/2018/JPN/Revit-Installation/files/GUID-BD09C1B4-5520-475D-BE7E-773642EEBD6C-htm.html" rel="noopener" target="_blank"><strong>Revit を他の言語で使用する</strong></a> をもとに、ユーザ インタフェースを英語に切り替えた Revit で、 日本版 Revit で作成した Revit プロジェクトを開き、上書き保存（または、別名で保存）することをお勧めします。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b027880274429200d-pi" style="display: inline;"><img alt="Revit_launch_propaties" border="0" class="asset  asset-image at-xid-6a0167607c2431970b027880274429200d img-responsive" src="/assets/image_800943.jpg" title="Revit_launch_propaties" /></a></p>
<hr />
<p><strong>手順</strong></p>
<ol>
<li>Git Bash を起動して cd コマンドで、現在のフォルダを forge-dataviz-iot-reference-app フォルダに移動します。C:\Users\<em>&lt;user name&gt;</em>\Documents\GitHub\forge-dataviz-iot-reference-app フォルダの場合、<strong>cd C:/Users/&lt;user name&gt;/Documents/GitHub/forge-dataviz-iot-reference-app</strong> と入力します。</li>
<li><strong>cp server/env_template server/.env</strong> と入力して、リファレンス アプリのローカル環境用テンプレートから&#0160; .env ファイルを作成します。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e9a20a59200b-pi" style="display: inline;"><img alt="Create_.env" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e9a20a59200b image-full img-responsive" src="/assets/image_982244.jpg" title="Create_.env" /></a></li>
<li>server フォルダ（C:\Users\<em>&lt;user name&gt;</em>\Documents\GitHub\forge-dataviz-iot-reference-app\server）の .envファイルをテキストエディタで開いて、次の箇所を修正して上書き保存します。各値に文字列を表すダブルクォーテーションは不要です。シングルクォーテーションも同様です。<br />・FORGE_CLIENT_ID の値に使用する Client Id を記入<br />・FORGE_CLIENT_SECRET の値に使用する Client_Secretを記入<br />・FORGE_BUCKET の値に使用する Bucket 名を記入<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdecf5733200c-pi" style="display: inline;"><img alt=".env" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdecf5733200c image-full img-responsive" src="/assets/image_956795.jpg" title=".env" /></a></li>
<li>Git Bash 上の現在のフォルダが forge-dataviz-iot-reference-app フォルダであることを確認して、<strong>ENV=local npm run dev</strong> と入力して Node.js を起動、待機状態にします。</li>
<li>Web ブラウザを起動して、URL に&#0160;<strong>localhost:9000/upload</strong>&#0160;と入力します。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e9a20b96200b-pi" style="display: inline;"><img alt="Launch_nodejs" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e9a20b96200b image-full img-responsive" src="/assets/image_309312.jpg" title="Launch_nodejs" /></a></li>
<li>[ファイルを選択] ボタンをクリックして、英語版 Revit で保存した Revit プロジェクト ファイルを選択します。アップロードが終了すると自動的に変換処理に移行します。この時、「Enable SVF2 Upload」チェックボックスにチェックすることで、SVF2 での変換を実行出来ますが、ここではチェックはせずに SVF 変換で処理を進めることにします。 <a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b027880274699200d-pi" style="display: inline;"><img alt="Uploading" border="0" class="asset  asset-image at-xid-6a0167607c2431970b027880274699200d image-full img-responsive" src="/assets/image_630384.jpg" title="Uploading" /></a></li>
<li>変換処理が終了すると、Web ブラウザの URL が&#0160;&#0160;<strong>localhost:9000</strong> にリダイレクトされて、アップロードした 3D モデルが表示されます。左上に表示されるボタンからレベル表示を切り替えることが出来ます。&#0160;<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b027880274745200d-pi" style="display: inline;"><img alt="Own_model" border="0" class="asset  asset-image at-xid-6a0167607c2431970b027880274745200d image-full img-responsive" src="/assets/image_764925.jpg" title="Own_model" /></a></li>
</ol>
<hr />
<p><strong>ダミーセンサーの追加</strong></p>
<p style="padding-left: 40px;">リファレンス アプリには、合成データと呼んでいるランダムに生成された実データのようなダミーデータが用意されています。この合成データを独自のデータに置き換えるためには、独自のデータ・アダプタを作成する必要があります。</p>
<p style="padding-left: 40px;">具体的な実装方法は、<a class="reference external" href="https://forge.autodesk.com/en/docs/dataviz/v1/developers_guide/advanced_topics/custom_data_adapter" rel="noopener" target="_blank">Creating a Data-Adapter</a> に譲りますが、取り急ぎ、リファレンス アプリ上の独自モデルでセンサー情報の扱いを確認するには、サーバー側のデータベースから &quot;GET DEVICES &quot;インターフェースを介して保存される JSON ファイル（forge-dataviz-iot-reference-app\server\gateways\synthetic-data\devices.json）のセンサー リストに、独自モデル上のセンサーを追加することで実現することが出来ます。</p>
<p style="padding-left: 40px;">例）</p>
<blockquote>
<div>
<div><span style="font-size: 8pt;">[</span></div>
<div><span style="font-size: 8pt;">&#0160;&#0160;&#0160;&#0160;{</span></div>
<div><span style="font-size: 8pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;deviceModelId&quot;:&#0160;&quot;d370a293-4bd5-4bdb-a3df-376dc131d44c&quot;,</span></div>
<div><span style="font-size: 8pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;deviceInfo&quot;:&#0160;[</span></div>
<div><span style="font-size: 8pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;{</span></div>
<div><span style="font-size: 8pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;id&quot;: &quot;Custom-11&quot;,</span></div>
<div><span style="font-size: 8pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;name&quot;:&#0160;&quot;1階エントランス&quot;,</span></div>
<div><span style="font-size: 8pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;position&quot;:&#0160;{</span></div>
<div><span style="font-size: 8pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;x&quot;:&#0160;&quot;&#0160;-15&quot;,</span></div>
<div><span style="font-size: 8pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;y&quot;:&#0160;&quot;&#0160;10&quot;,</span></div>
<div><span style="font-size: 8pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;z&quot;:&#0160;&quot;&#0160;-50&quot;</span></div>
<div><span style="font-size: 8pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;},</span></div>
<div><span style="font-size: 8pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;lastActivityTime&quot;:&#0160;&quot;2020-10-15T02:43:14.8786418Z&quot;</span></div>
<div><span style="font-size: 8pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;},</span></div>
<div><span style="font-size: 8pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;{</span></div>
<div><span style="font-size: 8pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;id&quot;:&#0160;&quot;Custom-21&quot;,</span></div>
<div><span style="font-size: 8pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;name&quot;:&#0160;&quot;2階南&quot;,</span></div>
<div><span style="font-size: 8pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;position&quot;:&#0160;{</span></div>
<div><span style="font-size: 8pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;x&quot;:&#0160;&quot;&#0160;20&quot;,</span></div>
<div><span style="font-size: 8pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;y&quot;:&#0160;&quot;&#0160;-5&quot;,</span></div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; :</div>
</div>
</blockquote>
<p style="padding-left: 40px;">元になった Revit プロジェクト ファイルのに「部屋」が作成されていて、センサー位置（カンバス座標）が部屋の内側に位置していれば、レベル別にセンサーリストにセンサー名が表示されてヒートマップを確認することが可能なはずです。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e9a23d98200b-pi" style="display: inline;"><img alt="Rug_sample" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e9a23d98200b image-full img-responsive" src="/assets/image_663681.jpg" title="Rug_sample" /></a></p>
<hr />
<p>ご参考まで、リファレンス アプリが使用する「部屋」の Revit での作成は Autodesk Knowledge Network 記事 <a href="https://knowledge.autodesk.com/ja/support/revit-products/learn-explore/caas/CloudHelp/cloudhelp/2018/JPN/Revit-Model/files/GUID-B3420A54-A9BC-4AEE-A07C-CD7A9DC782FB-htm.html" rel="noopener" target="_blank"><strong>部屋を作成する</strong></a> を、「部屋」を Forge Viewer で表現するための変換方法はブログ記事、<a href="https://adndevblog.typepad.com/technology_perspective/2019/11/rvt-translation-enhancement-on-model-derivative-api.html" rel="noopener" target="_blank"><strong>Model Derivative API での RVT ファイル変換について</strong></a> を、レベルの切り替え表示を実現する &#0160;Autodesk.AEC.LevelsExtension&#0160; エクステンションについてはブログ記事 <a href="https://adndevblog.typepad.com/technology_perspective/2020/09/displaying-per-level-on-forge-viewer.html" rel="noopener" target="_blank"><strong>Forge Viewer：レベル別の表示</strong></a> をそれぞれ確認してみてください。</p>
<p>Data Visualization エクステンションでは、上記に加えて、部屋や部屋の平面を使ったヒートマップ表現も実現している点を確認することが出来るはずです。</p>
<p>By Toshiaki Isezaki</p>
