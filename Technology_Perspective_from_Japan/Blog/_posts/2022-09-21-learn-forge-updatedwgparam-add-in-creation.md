---
layout: "post"
title: "Learn Forge UpdateDWGParam アドインの作成"
date: "2022-09-21 00:11:31"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/09/learn-forge-updatedwgparam-add-in-creation.html "
typepad_basename: "learn-forge-updatedwgparam-add-in-creation"
typepad_status: "Publish"
---

<p>Design Automation API for AutoCAD の AppBundle 用に AutoCAD アドイン アプリを作成する場合、お薦めなのが AutoCAD .NET API です。Web ページなどで指定されたクライアントからの JSON パラメータを容易に扱うことが出来るためです。</p>
<p>具体的には、.NET ベースの Newtonsot.json の<a href="https://www.newtonsoft.com/json" rel="noopener" target="_blank"> Json.NET</a> パッケージを Visual Studio プロジェクトに導入するだけで、WorkItem 実行時にパラメータを格納する JSON ファイルを読み込んでパースしたり、処理した情報を <a href="https://adndevblog.typepad.com/technology_perspective/2021/10/da-retrieving-json-fromappbundle.html" rel="noopener" target="_blank">JSON ファイルに書き出し</a>て Web ページに反映したりすることが出来るようになります。</p>
<p>ここでは、Learn Forge の「<a href="https://learnforge.autodesk.io/#/ja-JP/tutorials/modifymodels" rel="noopener" target="_blank">モデルを修正する</a>」や <a href="I&#39;m%20curious how to be used Forma, Flow, Fusion from bit different point of view." rel="noopener" target="_blank">Design Automation API for AutoCAD サンプル</a>で使われている UpdateDWGParam アドインを作成して、Json.NET パッケージの組み込みを主眼に導入手順をご紹介してみたいと思います。</p>
<p>アドイン作成には、AutoCAD 用の開発環境を利用することにします。少しイレギュラーですが、</p>
<p>Learn Forge に合わせて ObjectARX SDK for AutoCAD 20<strong>20</strong> がインストールされた環境で、Visual Studio 2019 Professional エディションと <a href="https://github.com/ADN-DevTech/AutoCAD-Net-Wizards/blob/ForAutoCAD2023/AutoCADNetWizardsInstaller/AutoCAD_2023_dotnet_wizards.zip" rel="noopener" target="_blank">AutoCAD 2023 DotNet Wizard</a>（AutoCAD .NET Wizard）を利用します。AutoCAD 2023 用の開発環境については、<a href="https://adndevblog.typepad.com/technology_perspective/2022/04/autocad-2023-interoperability-for-customization.html" rel="noopener" target="_blank">AutoCAD 2023 のカスタマイズ互換性</a>&#0160;のブログ記事を確認してみてください。</p>
<hr />
<ol>
<li>AutoCAD 2023 DotNet Wizard をインストールした Visual Studio 2019 Professional を起動して、「新しいプロジェクトの作成(N)」をクリックします。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a2eed458e6200d-pi" style="display: inline;"><img alt="Launch_vs" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a2eed458e6200d image-full img-responsive" src="/assets/image_748542.jpg" title="Launch_vs" /></a></li>
<li>画面上部の検索ボックスに &quot;AutoCAD&quot; と入力すると、環境にインストールされている AutoCAD 関連のウィザードがリストされます。C# を使用するので「AutoCAD 2023 CSharp plug-in」を選択して [次へ(<span style="text-decoration: underline;">N</span>)] をクリックします。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a2eed45904200d-pi" style="display: inline;"><img alt="New_project" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a2eed45904200d image-full img-responsive" src="/assets/image_799593.jpg" title="New_project" /></a></li>
<li>プロジェクト名に <strong>UpdateDWGParam</strong> と入力、フレームワークに Learn Forge に合わせて <strong>.NET Framework 4.7</strong> を選択して [作成(C)] をクリックします。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a2eed4591e200d-pi" style="display: inline;"><img alt="Project_settings" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a2eed4591e200d image-full img-responsive" src="/assets/image_699678.jpg" title="Project_settings" /></a></li>
<li>インストールされている ObjectARX SDK からプロジェクトが参照する AutoCAD アセンブリの参照先（パス）を指定します。ここでは、Learn Forge と同じ Autodesk.AutoCAD+23_1 コアエンジンでの利用を想定して、最上部の「Specify the location of the folder inside the ObectARX SDK that contains AcMgd.dll」パスに相当する AutoCAD 2020 用の ObjectARX SDK for AutoCAD 2020 の inc フォルダを指定して、[OK] ボタンをクリックします。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a30d4baea8200b-pi" style="display: inline;"><img alt="Wizard_config" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a30d4baea8200b img-responsive" src="/assets/image_847393.jpg" title="Wizard_config" /></a></li>
<li>Wizard によってスケルトン プロジェクトが作成されます。[ソリューション エクスプローラー] には、次のような構成が表示されるはずです。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a308de98b7200c-pi" style="display: inline;"><img alt="Sckelton_project" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a308de98b7200c img-responsive" src="/assets/image_667804.jpg" title="Sckelton_project" /></a></li>
<li>[ソリューション エクスプローラー] の「参照」をクリックして、Design Automation API for AutoCAD でのカスタム コマンド実行に不要な AutoCAD アセンブリの参照を除去します。AcMgd を見つけて右クリックし、「削除」を選択します。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a2eed4a077200d-pi" style="display: inline;"><img alt="Remove_reference" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a2eed4a077200d img-responsive" src="/assets/image_575479.jpg" title="Remove_reference" /></a></li>
<li>続いて、プロジェクトに Json.NET パッケージを導入します。[ツール(T)] メニューから [NuGet パッケージ マネージャー(N)] &gt;&gt; [ソリューションの NuGet パッケージの管理(N)...] を選択します。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a30d4bf601200b-pi" style="display: inline;"><img alt="Nuget_package_manager" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a30d4bf601200b image-full img-responsive" src="/assets/image_837447.jpg" title="Nuget_package_manager" /></a></li>
<li>「Nuget ソリューション」タブが表示されたら [参照] タブをアクティブして（①）、検索ボックスに「<strong>Newtonsot.json</strong>」と入力します（②）。検索ボックス下に Newtonsot.json 関連のパッケージがリストされたら、「Newtonsot.json」をクリックすると、画面右の領域にパッケージ内容が表示されます（③）。「プロジェクト」にチェックして（④）、右下の [インストール] ボタンをクリックしてパッケージをインストールします（⑤）。パッケージの導入には、使用するコンピュータがインターネット接続されている必要があります。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a308de9b37200c-pi" style="display: inline;"><img alt="Install_nuget_package" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a308de9b37200c image-full img-responsive" src="/assets/image_316665.jpg" title="Install_nuget_package" /></a></li>
<li>ここまでの操作で、[ソリューション エクスプローラー] のアセンブリ参照の状態が次のように変わっているはずです。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a30d4bf7c4200b-pi" style="display: inline;"><img alt="Changed_references" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a30d4bf7c4200b img-responsive" src="/assets/image_264200.jpg" title="Changed_references" /></a></li>
<li>[ソリューション エクスプローラー] から <strong>MyCommand.cs</strong> を見つけてダブルクリックし、画面に MyCommand.cs のソースコード表示させます。</li>
<li>Learn Forge 「<a href="https://learnforge.autodesk.io/#/ja-JP/designautomation/appbundle/engines/autocad" rel="noopener" target="_blank">AutoCAD バンドルを準備する</a>」の Commands.cs の内容を現在のプロジェクトに反映していきます。ソースコード冒頭の<a href="https://docs.microsoft.com/ja-jp/dotnet/csharp/language-reference/keywords/using-directive" rel="noopener" target="_blank"> Using ディレクティブ</a>で解決する名前空間の行を、次の内容で全行<span style="text-decoration: underline;">置き換え</span>ます。<span style="color: #0000ff;"><strong>青字</strong></span>はここでは無視してください。最後に説明しています。<br />
<blockquote>
<p>using Autodesk.AutoCAD.ApplicationServices.Core;<br />using Autodesk.AutoCAD.DatabaseServices;<br />using Autodesk.AutoCAD.Runtime;<br /><span style="color: #0000ff;"><strong>using Newtonsoft.Json;</strong></span><br />using System.IO;</p>
</blockquote>
</li>
<li>同様に、ソースコード内の MyCommands クラス スコープ（public class MyCommands { ～ }）を、次の内容で全行<span style="text-decoration: underline;">置き換え</span>ます。<br />
<blockquote>
<div>
<div>&#0160; [CommandMethod(&quot;UpdateParam&quot;, CommandFlags.Modal)]</div>
<div>&#0160; public static void UpdateParam()</div>
<div>&#0160; {</div>
<div>&#0160; &#0160;//Get active document of drawing with Dynamic block</div>
<div>&#0160; &#0160;var doc = Application.DocumentManager.MdiActiveDocument;</div>
<div>&#0160; &#0160;var db = doc.Database;</div>
<br />
<div>&#0160; &#0160;// read input parameters from JSON file</div>
<div><span style="color: #0000ff;"><strong>&#0160; &#0160;InputParams inputParams = JsonConvert.DeserializeObject&lt;InputParams&gt;(File.ReadAllText(&quot;params.json&quot;));</strong></span></div>
<br />
<div>&#0160; &#0160;using (Transaction t = db.TransactionManager.StartTransaction())</div>
<div>&#0160; &#0160;{</div>
<div>&#0160; &#0160; var bt = t.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;</div>
<br />
<div>&#0160; &#0160; foreach (ObjectId btrId in bt)</div>
<div>&#0160; &#0160; {</div>
<div>&#0160; &#0160; &#0160;//get the blockDef and check if is anonymous</div>
<div>&#0160; &#0160; &#0160;BlockTableRecord btr = (BlockTableRecord)t.GetObject(btrId, OpenMode.ForRead);</div>
<div>&#0160; &#0160; &#0160;if (btr.IsDynamicBlock)</div>
<div>&#0160; &#0160; &#0160;{</div>
<div>&#0160; &#0160; &#0160; //get all anonymous blocks from this dynamic block</div>
<div>&#0160; &#0160; &#0160; ObjectIdCollection anonymousIds = btr.GetAnonymousBlockIds();</div>
<div>&#0160; &#0160; &#0160; ObjectIdCollection dynBlockRefs = new ObjectIdCollection();</div>
<div>&#0160; &#0160; &#0160; foreach (ObjectId anonymousBtrId in anonymousIds)</div>
<div>&#0160; &#0160; &#0160; {</div>
<div>&#0160; &#0160; &#0160; &#0160;//get the anonymous block</div>
<div>&#0160; &#0160; &#0160; &#0160;BlockTableRecord anonymousBtr = (BlockTableRecord)t.GetObject(anonymousBtrId, OpenMode.ForRead);</div>
<div>&#0160; &#0160; &#0160; &#0160;//and all references to this block</div>
<div>&#0160; &#0160; &#0160; &#0160;ObjectIdCollection blockRefIds = anonymousBtr.GetBlockReferenceIds(true, true);</div>
<div>&#0160; &#0160; &#0160; &#0160;foreach (ObjectId id in blockRefIds)</div>
<div>&#0160; &#0160; &#0160; &#0160;{</div>
<div>&#0160; &#0160; &#0160; &#0160; dynBlockRefs.Add(id);</div>
<div>&#0160; &#0160; &#0160; &#0160;}</div>
<div>&#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; &#0160; if (dynBlockRefs.Count &gt; 0)</div>
<div>&#0160; &#0160; &#0160; {</div>
<div>&#0160; &#0160; &#0160; &#0160;//Get the first dynamic block reference, we have only one Dyanmic Block reference in Drawing</div>
<div>&#0160; &#0160; &#0160; &#0160;var dBref = t.GetObject(dynBlockRefs[0], OpenMode.ForWrite) as BlockReference;</div>
<div>&#0160; &#0160; &#0160; &#0160;UpdateDynamicProperties(dBref, inputParams);</div>
<div>&#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; &#0160;}</div>
<div>&#0160; &#0160; }</div>
<div>&#0160; &#0160; t.Commit();</div>
<div>&#0160; &#0160;}</div>
<div>&#0160; &#0160;LogTrace(&quot;Saving file...&quot;);</div>
<div>&#0160; &#0160;db.SaveAs(&quot;outputFile.dwg&quot;, DwgVersion.Current);</div>
<div>&#0160; }</div>
<br />
<div>&#0160; /// &lt;summary&gt;</div>
<div>&#0160; /// This updates the Dyanmic Blockreference with given Width and Height</div>
<div>&#0160; /// The initial parameters of Dynamic Blockrefence, Width =20.00 and Height =40.00</div>
<div>&#0160; /// &lt;/summary&gt;</div>
<div>&#0160; /// &lt;param Editor=&quot;ed&quot;&gt;&lt;/param&gt;</div>
<div>&#0160; /// &lt;param BlockReference=&quot;br&quot;&gt;&lt;/param&gt;</div>
<div>&#0160; /// &lt;param String=&quot;name&quot;&gt;&lt;/param&gt;</div>
<div>&#0160; private static void UpdateDynamicProperties(BlockReference br, InputParams inputParams)</div>
<div>&#0160; {</div>
<div>&#0160; &#0160;// Only continue is we have a valid dynamic block</div>
<div>&#0160; &#0160;if (br != null &amp;&amp; br.IsDynamicBlock)</div>
<div>&#0160; &#0160;{</div>
<div>&#0160; &#0160; // Get the dynamic block&#39;s property collection</div>
<div>&#0160; &#0160; DynamicBlockReferencePropertyCollection pc = br.DynamicBlockReferencePropertyCollection;</div>
<div>&#0160; &#0160; foreach (DynamicBlockReferenceProperty prop in pc)</div>
<div>&#0160; &#0160; {</div>
<div>&#0160; &#0160; &#0160;switch (prop.PropertyName)</div>
<div>&#0160; &#0160; &#0160;{</div>
<div>&#0160; &#0160; &#0160; case &quot;Width&quot;:</div>
<div><span style="color: #0000ff;"><strong>&#0160; &#0160; &#0160; &#0160;prop.Value = inputParams.Width;</strong></span></div>
<div>&#0160; &#0160; &#0160; &#0160;break;</div>
<div>&#0160; &#0160; &#0160; case &quot;Height&quot;:</div>
<div><span style="color: #0000ff;"><strong>&#0160; &#0160; &#0160; &#0160;prop.Value = inputParams.Height;</strong></span></div>
<div>&#0160; &#0160; &#0160; &#0160;break;</div>
<div>&#0160; &#0160; &#0160; default:</div>
<div>&#0160; &#0160; &#0160; &#0160;break;</div>
<div>&#0160; &#0160; &#0160;}</div>
<div>&#0160; &#0160; }</div>
<div>&#0160; &#0160;}</div>
<div>&#0160; }</div>
<br />
<div>&#0160; /// &lt;summary&gt;</div>
<div>&#0160; /// This will appear on the Design Automation output</div>
<div>&#0160; /// &lt;/summary&gt;</div>
<div>&#0160; private static void LogTrace(string format, params object[] args) { Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage(format, args); }</div>
</div>
</blockquote>
</li>
<li>MyCommands クラス スコープ（public class MyCommands { ～ }）の後に、次のクラスを追記します。<br />
<blockquote><span style="color: #0000ff;"><strong>public class InputParams</strong></span><br /><span style="color: #0000ff;"><strong>{</strong></span><br /><span style="color: #0000ff;"><strong>&#0160; public double Width { get; set; }&#0160;</strong></span><br /><span style="color: #0000ff;"><strong>&#0160; public double Height { get; set; }</strong></span><br /><span style="color: #0000ff;"><strong>}</strong></span></blockquote>
</li>
<li>ここまでの操作で、ビルドに必要な準備が整いました。[ビルド(B)] メニューから [ソリューションのリビルド(R)] を選択して、アドイン（UpdateDWGParam.dll アセンブリ）をビルド出来ます。</li>
</ol>
<hr />
<p><strong><span style="color: #0000ff;">青字</span></strong>にした箇所が、Newtonsot.json の<a href="https://www.newtonsoft.com/json" rel="noopener" target="_blank"> Json.NET</a> パッケージを使ったパラメータ値を格納した JSON ファイル（params.json）の読み取りに関係するコードです。</p>
<p>あとは、UpdateDWGParam.dll の生成フォルダにある Newtonsoft.Json.dll と<span style="text-decoration: underline;">共に</span>、Learn Forge 「<a href="https://learnforge.autodesk.io/#/ja-JP/designautomation/appbundle/engines/autocad" rel="noopener" target="_blank">AutoCAD バンドルを準備する</a>」の PackageContents.xml セクションにある構造でバッケージ バンドルを UpdateDWGParam.zip 名で ZIP 圧縮して、forgesample\bundles フォルダに保存すれば、Learn Forge の内容に沿って AppBundle を登録、WorkItem でアドインを使用することが出来ます。</p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
