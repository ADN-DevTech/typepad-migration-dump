---
layout: "post"
title: "Design Automation API for AutoCAD：カスタム フォントの扱いと解決"
date: "2021-10-20 00:13:16"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/10/handling-and-resolution-on-custom-font.html "
typepad_basename: "handling-and-resolution-on-custom-font"
typepad_status: "Publish"
---

<p>Design Automation API は、WorkItem 実行時に実行環境となる仮想マシン（AMI）を確立して、コアエンジンを実行し、WorkItem 終了時にシャットダウンするクラウド コンピューティング環境です。この際、仮想マシンに使用するのは英語版の Windows OS で、Supplyental Fonts をインストールしているものの、<a href="https://adndevblog.typepad.com/technology_perspective/2020/12/notes-on-using-da4a.html" rel="noopener" target="_blank"><strong>Design Automation API for AutoCAD 利用の注意点</strong></a> でご案内しているとおり、既定で使用可能な日本語 TrueType フォントは、MS ゴシック、MS P ゴシック、MS UI ゴシック MS 明朝、MS P 明朝、游 明朝、メイリオ、メイリオ UI のみです。</p>
<p>Design Automation API for AutoCAD の WorkItem 処理で、素材として扱う図面に上記以外の TrueType フォントが使用されていたら、何が起こるか見てみましょう。</p>
<p>例えば、図面ファイルのレイアウトに見積書にカスタムな TrueType フォント（.ttf）を利用したとします。この場合、その TrueType フォントを AppBundle に同梱してコアエンジン環境に渡すと、WorkItem 実行時に AppBundle 内の TrueType フォント ファイルを識別して、実行環境に展開するようになっています。</p>
<p>AppBundle 同梱の TrueType フォントの展開は、WorkItem 実行時に生成されるログファイル上にも表れます。</p>
<blockquote>
<p>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; :<br />[10/12/2021 07:24:36] Install user TrueType font<br />&quot;T:\Aces\Applications\73111aa8b77b232596bcdcdbbf316774.AjFukUWeRk05eA9XpH8Nnh6<br />2BzPD60mg.TableFanConfigurator[1].package\CreateQuotation.bundle\Contents\851MkPOP_100.ttf&quot;.<br />[10/12/2021 07:24:36] Version Number: S.51.Z.45 (UNICODE)<br />[10/12/2021 07:24:36] LogFilePath has been set to the working folder.<br />[10/12/2021 07:24:36] Loading Modeler DLLs.<br />&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; :</p>
</blockquote>
<p>にもかかわらず、コアエンジンにロードされたアドインで生成された PDF ファイルを見ると、素材の DWG 図面にあるフォントが置き換わってしまう現象が起こっていることがわかります。</p>
<ul>
<li>参考フォント出典：<a href="https://fontfree.me/">https://fontfree.me/</a> 851マカポップ Ver 0.01 フォントを利用</li>
</ul>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdef8e6b7200c-pi" style="display: inline;"><img alt="Incorrect" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdef8e6b7200c image-full img-responsive" src="/assets/image_769700.jpg" title="Incorrect" /></a></p>
<p>この状態は、コアエンジンが TrueType フォント（.ttf）を正しく認識できず、システム変数 <a href="https://help.autodesk.com/view/ACD/2022/JPN/?guid=GUID-858A8D84-5104-4A35-9C82-2094DF6F411D" rel="noopener" target="_blank"><strong>FONTALT</strong></a> に設定されている代替フォントが使用されていることを意味します。<span style="background-color: #ffffff;">この場合、フォント名（851マカポップ）とフォントファイル名（851MkPOP_100.ttf）が一致していないため、コアエンジンが正しく TrueType フォントを認識することが出来ていません。</span></p>
<p>これを解決するのは、<a href="https://help.autodesk.com/view/ACD/2022/JPN/?guid=GUID-BE7A48CE-E927-438E-B235-19220018A4DE" rel="noopener" target="_blank"><strong>フォント マッピング ファイル</strong></a>（.fmp）を利用して解決することが出来ます。</p>
<ul>
<li><span style="background-color: #ffffff;">代替フォントについての <a href="https://help.autodesk.com/view/ACD/2022/JPN/?guid=GUID-928DF015-1E04-4CC2-AF1B-0037548DFBAE" rel="noopener" target="_blank"><strong>オンライン ヘルプ</strong></a> には、フォント マッピング ファイルの記述方法について次のように説明されています。<br /><br />「フォント マッピング ファイルの各行には、フォント ファイルの名前(ファイル拡張子やパスを含まない)と代替フォント ファイル名をセミコロン(;)で区切って指定します。代替フォント ファイル名には、<em class="ph i" id="GUID-928DF015-1E04-4CC2-AF1B-0037548DFBAE__GUID-036C6244-3069-42DD-A482-7D826CFA8503">.ttf&#0160;</em>のようなファイル拡張子を付けます。」<br /><br />この説明に沿って、今回のフォントのマッピングを設定しようとすると、次のようになります。</span></li>
</ul>
<blockquote>
<p>851マカポップ;851MkPOP_100.ttf</p>
</blockquote>
<p style="padding-left: 40px;">ただし、この<span style="background-color: #ffffff;">フォント マッピング ファイルでは、AutoCAD のフォント処理コードがファイル名 851MkPOP_100.ttf の検証に失敗してしまうため、このフォントファイル名を simplex.shx にマッピングされてしまい、期待した結果が得られません。<br /></span></p>
<p style="padding-left: 40px;">Design Automation API プロセスの間にある AppBundle 内の TrueType フォントを一時的にインストールします。通常、<span style="background-color: #ffffff;">フォント マッピング ファイルの処理では、システム&#0160;</span>レジストリを調べますが、<span style="background-color: #ffffff;">システム&#0160;</span>レジストリには、一時的に登録されたフォントのエントリが含まれていません。</p>
<p style="padding-left: 40px;">今回のように、WorkItem 実行時に一時的に登録されるフォントを解決させるには、フォント名とフォントファイル名（除く拡張子 .ttf）を使用する必要があります。この問題は、Design Automation API for AutoCAD（AcCoreConsole.exe）上の一時的なフォント登録のサポートに特有のものです。</p>
<p style="padding-left: 40px;">下記は、851マカポップ をローカル コンピュータの Windows にインストールした際に得られるフォント ファイル名です（フォント設定で当該フォントの情報を表示させた画面）</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdef95a45200c-pi" style="display: inline;"><img alt="Font_settings" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdef95a45200c image-full img-responsive" src="/assets/image_985522.jpg" title="Font_settings" /></a></p>
<p style="padding-left: 40px;">ここで有効な<span style="background-color: #ffffff;">フォント マッピング ファイルの記述は、次のとおりです。</span></p>
<blockquote>
<p>851マカポップ;851MkPOP</p>
</blockquote>
<p style="padding-left: 40px;"><span style="background-color: #ffffff;">なお、フォント マッピング ファイル自体は、</span><span style="background-color: #ffffff;">UTF-8 エンコードで保存する必要があります。</span></p>
<p>フォント マッピング ファイルの利用を有効にするには、Activity で指定するコアエンジンへの CommandLine に <strong>/dwgfontmap</strong> を挿入する必要があります。</p>
<blockquote>
<p>&quot;commandLine&quot;: [&#39;$(engine.path)\\accoreconsole.exe /i &quot;$(args[DWGInput].path)&quot; /dwgfontmap /al &quot;$(appbundles[TableFanConfigurator].path)&quot; /s $(settings[script].path)&#39;],</p>
</blockquote>
<p>次に、Activity と WorkItem 登録時に、それぞれフォント マッピング ファイルが作業領域に展開されるように指定すると、正しく TrueType フォントが認識されるようになります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a30d3f67df200b-pi" style="display: inline;"><img alt="Font_map" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a30d3f67df200b image-full img-responsive" src="/assets/image_761017.jpg" title="Font_map" /></a></p>
<p>フォント マッピング ファイルの展開は、WorkItem のログ出力でも確認することが出来るはずです。</p>
<blockquote>
<p>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; :<br />[10/12/2021 08:40:22] Install user TrueType font &quot;T:\Aces\Applications\73111aa8b77b232596bcdcdbbf316774.AjFukUWeRk05eA9XpH8Nnh6<br />2BzPD60mg.TableFanConfigurator[1].package\CreateQuotation.bundle\Contents\851MkPOP_100.ttf&quot;.<br />[10/12/2021 08:40:22] Version Number: S.51.Z.45 (UNICODE)<br />[10/12/2021 08:40:22] LogFilePath has been set to the working folder.<br />[10/12/2021 08:40:22] Loaded drawing font-mapping file: T:\Aces\Jobs\f61c5aa49b7b477896b2b2b039911a44\dwg.fmp<br />[10/12/2021 08:40:22] Loading Modeler DLLs.<br />&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; :</p>
</blockquote>
<p>この環境で処理された WorkItem 処理では、TrueType フォントが正しく認識されて、成果ファイルである PDF ファイルに反映されるようになります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02788050c371200d-pi" style="display: inline;"><img alt="Correct" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02788050c371200d image-full img-responsive" src="/assets/image_463014.jpg" title="Correct" /></a></p>
<ul>
<li>カスタム フォント ファイルの利用に際しては、念のため、フォント ファイルのライセンス（使用許諾）をご確認いただくことをお薦めします。</li>
</ul>
<p>By Toshiaki Isezaki</p>
