---
layout: "post"
title: "Design Automation API for AutoCAD とオブジェクト イネーブラ"
date: "2022-09-14 00:19:45"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/09/design-automation-api-and-object-enabler.html "
typepad_basename: "design-automation-api-and-object-enabler"
typepad_status: "Publish"
---

<p><a href="https://adndevblog.typepad.com/technology_perspective/2022/09/desktop-vanilla-autocad-and-object-enabler.html" rel="noopener" target="_blank">デスクトップ AutoCAD とオブジェクト イネーブラ</a> 記事に続き、AutoCAD コアエンジンを使用する Design Automation API for AutoCAD 環境とオートデスク製オブジェクト イネーブラについてご案内します。</p>
<hr />
<p><strong>Design Automation API for AutoCAD 環境の業種別製品オブジェクト イネーブラと効果</strong></p>
<p style="padding-left: 40px;">Design Automation API for AutoCAD では、オートデスク製のオブジェクト イネーブラはどう提供されているでしょうか？</p>
<p style="padding-left: 40px;">答えは簡単です。<a href="https://adndevblog.typepad.com/technology_perspective/2022/09/desktop-vanilla-autocad-and-object-enabler.html" rel="noopener" target="_blank">デスクトップ AutoCAD とオブジェクト イネーブラ</a>&#0160;記事で触れたすべてのオブジェクト イネーブラが含まれています。デスクトップの AutoCAD 単体製品と Design Automation API for AutoCAD 環境でのオートデスク製オブジェクト イネーブラの有無は次のようになります。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a2eed37b96200d-pi" style="display: inline;"><img alt="Oe_table" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a2eed37b96200d image-full img-responsive" src="/assets/image_208774.jpg" title="Oe_table" /></a></p>
<p style="padding-left: 40px;">Design Automation API for AutoCAD 環境にオブジェクト イネーブラロードされることで、 AutoCAD 業種別製品のカスタム オブジェクトのプロキシ オブジェクト化を抑止して、AppBundle で提供する AutoCAD アドインの処理で特定のカスタム オブジェクトを識別したり、数量拾いを実施したりすることが出来るようになります。</p>
<p style="padding-left: 40px;">試しに、<a href="https://adndevblog.typepad.com/technology_perspective/2022/09/desktop-vanilla-autocad-and-object-enabler.html" rel="noopener" target="_blank">デスクトップ AutoCAD とオブジェクト イネーブラ</a>&#0160; 記事でもご紹介したコードを Design Automation API 用に少し修正して 、AppBundle として登録、WorkItem を実行したとします。</p>
<div>
<blockquote>
<div>[CommandMethod(&quot;MyGroup&quot;, &quot;MyCommand&quot;, &quot;MyCommand&quot;, CommandFlags.Modal)]</div>
<div>public void MyCommand()</div>
<div>{</div>
<div>&#0160; &#0160; log(&quot;\nAddin process start ...&quot;);</div>
<div>&#0160; &#0160; Database db = Application.DocumentManager.MdiActiveDocument.Database;</div>
<div>&#0160; &#0160; try</div>
<div>&#0160; &#0160; {</div>
<div>&#0160; &#0160; &#0160; &#0160; using (Transaction tr = db.TransactionManager.StartTransaction())</div>
<div>&#0160; &#0160; &#0160; &#0160; {</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; BlockTable bt = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForRead);</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; BlockTableRecord btr = (BlockTableRecord)tr.GetObject(bt[&quot;*MODEL_SPACE&quot;], OpenMode.ForRead);</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; Entity ent = null;</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; foreach (ObjectId objId in btr)</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ent = (Entity)tr.GetObject(objId, OpenMode.ForRead);</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; log(&quot;\n&quot; + ent.GetRXClass().DxfName + &quot; (&quot; + ent.GetRXClass().Name + &quot; クラス)&quot;);</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; tr.Commit();</div>
<div>&#0160; &#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; }</div>
<div>&#0160; &#0160; catch (Autodesk.AutoCAD.Runtime.Exception ex)</div>
<div>&#0160; &#0160; {</div>
<div>&#0160; &#0160; &#0160; &#0160; log(&quot;\n!!! Exception happened on Addin process ...&quot;);</div>
<div>&#0160; &#0160; &#0160; &#0160; log(ex.Message);</div>
<div>&#0160; &#0160; }</div>
<div>&#0160; &#0160; finally</div>
<div>&#0160; &#0160; {</div>
<div>&#0160; &#0160; }</div>
<div>&#0160; &#0160; log(&quot;\nAddin process end ...&quot;);</div>
<div>}</div>
<div>&#0160;</div>
<div>private static void log(string format, params object[] args) { Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage(format, args); }</div>
</blockquote>
</div>
<p style="padding-left: 40px;">ここでも、<a href="https://adndevblog.typepad.com/technology_perspective/2022/09/desktop-vanilla-autocad-and-object-enabler.html" rel="noopener" target="_blank">デスクトップ AutoCAD とオブジェクト イネーブラ</a>&#0160;記事と同じ対象図面を処理することにします。AutoCAD Mechanical のサンプル図面 gear_pump_subassy.dwg から六角ボルトとバルーンを 1 つづつ残してすべて削除し、比較用に線分を 2 つ追加した図面です。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a30d4b84c5200b-pi" style="display: inline;"><img alt="Acm_drawing" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a30d4b84c5200b image-full img-responsive" src="/assets/image_89411.jpg" title="Acm_drawing" /></a></p>
<p style="padding-left: 40px;">Design Automation API にはコマンド プロンプトを含むユーザ インタフェースがないので、WorkItem 実行時に生成されるレポート ログのメッセージをを見て判断することにします。</p>
<p style="padding-left: 40px;">結果、次のように WorkItem として実行されるアドイン処理でも、オブジェクト イネーブラによって、AutoCAD Mechanical のカスタム オブジェクトが正しく認識されることがわかります。</p>
<blockquote>
<p>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;：<br />[09/04/2022 13:40:50] Version Number: S.51.Z.100 (UNICODE)<br />[09/04/2022 13:40:50] LogFilePath has been set to the working folder.<br /><strong>[09/04/2022 13:40:50] Loading Mechanical modules.....</strong><br />[09/04/2022 13:40:51] Loader application completed<br />[09/04/2022 13:40:51] Regenerating model.<br />[09/04/2022 13:40:51] AutoCAD menu utilities loaded.<br />[09/04/2022 13:40:51] Command:<br />[09/04/2022 13:40:51] Command:<br />[09/04/2022 13:40:51] Command:<br />[09/04/2022 13:40:51] Command: MyCommand<br />[09/04/2022 13:40:52] Addin process start ...<br /><strong>[09/04/2022 13:40:53] STDPART2D (AmgStdPart クラス)</strong><br /><strong>[09/04/2022 13:40:53] ACMBALLOON (AcmBalloon クラス)</strong><br />[09/04/2022 13:40:53] LINE (AcDbLine クラス)<br />[09/04/2022 13:40:53] LINE (AcDbLine クラス)_tilemode<br />&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;：</p>
</blockquote>
<hr />
<p><strong>AutoCAD Mechanical Desktop カスタム オブジェクトの影響</strong></p>
<p style="padding-left: 40px;">DWG ファイルのライフサイクルは、日頃考えるよりも長いものです。もし、Design Automation API for AutoCAD で利用時に、レポートログに「<strong>ERROR: Something went wrong. ErrorStatus=53.</strong>」と記録されて処理が失敗する場合には、AutoCAD Mechanical Desktop カスタム オブジェクトの影響を疑ってみてください。</p>
<blockquote>
<p>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;：<br />[09/04/2022 15:25:41] LogFilePath has been set to the working folder.<br /><strong>[09/04/2022 15:25:41] Loading Mechanical modules.....</strong><br />[09/04/2022 15:25:42] Loader application completed<br />[09/04/2022 15:25:42] Regenerating model.<br /><strong>[09/04/2022 15:25:43] ERROR: Something went wrong. ErrorStatus=53.</strong><br />[09/042022 15:25:44] End AutoCAD Core Engine standard output dump.<br /><strong>[09/04/2022 15:25:44] Error: Application accoreconsole.exe exits with code 53 which indicates an error.<br /></strong>[09/04/2022 15:25:44] End script phase.<br />[09/04/2022 15:25:44] Error: An unexpected error happened during phase CoreEngineExecution of job.[09/04/2022 15:25:44] Job finished with result FailedExecution<br />&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;：</p>
</blockquote>
<p style="padding-left: 40px;">オートデスクは、以前、AutoCAD 上でフィーチャ ベースの 3D パラメトリック設計を実現する製品を開発・販売していました。<strong>AutoCAD Mechanical Desktop</strong> と名付けられた製品は、同等の機能を持つ Inventor に役割を譲り、AutoCAD 2009&#0160; Mechanical Desktop を最後に販売を停止した経緯があります。</p>
<p style="padding-left: 40px;">当時、同じ製造業向けアプリケーションとして、3D の AutoCAD Mechanical Desktop と 2D の AutoCAD Mechanical の連携利用を想定していたため、AutoCAD Mechanical しか利用していなくても、あらかじめ AutoCAD Mechanical Desktop のカスタム オブジェクトが埋め込まれていました（図形としてではなく。ディクショナリ領域に）。</p>
<p style="padding-left: 40px;">Design Automation API for AutoCAD は AutoCAD Mechanical のカスタム オブジェクトを正しく認識出来ますが、Mechanical カスタム オブジェクトが旧 AutoCAD Mechanical Desktop のカスタム オブジェクトを内部的に「参照」していると、参照先のオブジェクトが解決出来ず、図面破損と判定されてしまいます。ちょうど、<a href="https://adndevblog.typepad.com/technology_perspective/2014/07/drawing-structure-and-corruption-from-autocad-api-perspective.html">AutoCAD API から見た図面構造と破損</a> でご紹介したような状態です。AutoCAD Mechanical Desktop カスタム オブジェクトのオブジェクト イネーブラがすでに提供されていないのが原因です。</p>
<p style="padding-left: 40px;">このような DWG ファイルを処理するには、Activity 登録時に &quot;commandLine&quot; で指定するコアエンジンの起動スイッチに <strong>/recover</strong> を追加してください。WorkItem 実行時に解決できなかった AutoCAD Mechanical Desktop カスタム オブジェクトを削除します。</p>
<blockquote>
<p>&quot;commandLine&quot;: [&#39;$(engine.path)\\accoreconsole.exe /i &quot;$(args[DWGInput].path)&quot; /al &quot;$(appbundles[TestHarness].path)&quot; /s $(settings[script].path) <strong>/recover</strong>&#39;],</p>
</blockquote>
<p style="padding-left: 40px;"><strong>/recover</strong> &#0160;オプションを使用して図面開き、上書き保存しても、AutoCAD Mechanical を含めた図面の運用には影響はありません。</p>
<p style="padding-left: 40px;">もし、ローカル環境で「<strong>ERROR: Something went wrong. ErrorStatus=53.</strong>」が発生する DWG ファイルを事前に修復したい場合には、システム変数 <a href="https://knowledge.autodesk.com/ja/support/autocad/learn-explore/caas/CloudHelp/cloudhelp/2023/JPN/AutoCAD-Core/files/GUID-18E43007-8DE9-4C6D-AE55-13FF0AF492C3-htm.html" rel="noopener" target="_blank">RECOVERAUTO</a> を 3 に設定した<strong><span style="text-decoration: underline;"> AutoCAD Mechanical（エラーを検出してしまう AutoCAD Mechanical オブジェクト イネーブラがロードされる）で当該図面を開いて、上書き保存するだけ</span></strong>で、AutoCAD Mechanical Desktop のカスタム オブジェクトを削除することが出来ます。別途、RECOVER コマンドを実行する必要はありません。</p>
<hr />
<p><strong>AutoCAD Mechanical バージョンの影響</strong></p>
<p style="padding-left: 40px;">Design Automation API for AutoCAD で AutoCAD Mechanical のカスタム オブジェクトを含む DWG ファイルを処理する際には、 AutoCAD Mechanical の保存バージョンとコアエンジンの差異にも注意すべきです。</p>
<p style="padding-left: 40px;">ご存知のように、AutoCAD Mechanical はオブジェクトのバージョン管理を非常に細かくおこなっています。AutoCAD Mechanical の [図面に名前を付けて保存] ダイアログ（SAVEAS コマンド）を見てみてください。2018 DWG形式 1 つに、4 つの AutoCAD Mechanical バージョンが存在していることがわかります。</p>
<p style="padding-left: 40px;">具体的には、AutoCAD Mechanical 2018 図面、AutoCAD Mechanical 2021 図面、AutoCAD Mechanical 2022 図面、AutoCAD Mechanical 2023 図面を指しますが、これは、すなわち 4 つのバージョン毎のオブジェクト イネーブラが必要になることを意味します。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a30d4b835a200b-pi" style="display: inline;"><img alt="Acm_saveas" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a30d4b835a200b image-full img-responsive" src="/assets/image_557539.jpg" style="width: 757.37px;" title="Acm_saveas" /></a></p>
<p style="padding-left: 40px;">AutoCAD 2022 に相当する Autodesk.AutoCAD+24_1 コアエンジンを使用して、「AutoCAD Mechanical 2022 図面」として保存された 2018 DWG 形式の図面を処理すると、期待通りに動作します。WokItem は AutoCAD Mechanical カスタム オブジェクトを適切に認識してリストします。</p>
<p style="padding-left: 40px;">しかし、同じ図面を AutoCAD 2020 相当の Autodesk.AutoCAD+23_1 コアエンジンで処理すると、コアエンジンが図面を開くことが出来るものの、アドイン処理には失敗してしまいます。お気づきのとおり、AutoCAD Mechanical カスタム オブジェクトのバージョンと、Autodesk.AutoCAD+23_1 コアエンジン環境のオブジェクト イネーブラが一致しないのが原因です。</p>
<p style="padding-left: 40px;">あいにく、一般的な AutoCAD アドインと同様、オブジェクト イネーブラにも下位バージョン互換はありません。このため、Autodesk.AutoCAD+24_1 コアエンジンの「AutoCAD Mechanical 2022 図面」用オブジェクト イネーブラを、AutoCAD 2020 相当の Autodesk.AutoCAD+23_1 コアエンジンで使用することは出来ません。</p>
<p style="padding-left: 40px;">オブジェクト イネーブラと AutoCAD Mechanical カスタム オブジェクトのバージョン ミスマッチが発生しても、レポートログにはその詳細について記載はされず、「timeout（タイムアウト）」とだけレポートされるのみです。</p>
<blockquote>
<p>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;：<br /><strong>[09/04/2022 19:41:57] Loading Mechanical modules.....</strong><br />[09/04/2022 19:41:57] Loader application completed<br />[09/04/2022 19:41:57] Regenerating model.<br /><strong>[09/04/2022 19:42:58] Error: AutoCAD Core Console is shut down due to timeout.</strong><br />[09/04/2022 19:42:58] End script phase.<br />[09/04/2022 19:42:58] Error: An unexpected error happened during phase CoreEngineExecution of job.<br />[09/04/2022 19:42:58] Job finished with result FailedExecution<br />&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;：</p>
</blockquote>
<p style="padding-left: 40px;">この部分は少し分かりにくいので、今後の改善検討課題とされています。Design Automation API for AutoCAD で、最新のコアエンジンの使用が推奨される由縁です。</p>
<hr />
<p>なお、独自に ObjectARX で実装したカスタム オブジェクト用のオブジェクト イネーブラを AppBundle&#0160; 化して、バンドルパッケージ内の <a href="https://help.autodesk.com/view/ACD/2023/JPN/?guid=GUID-BC76355D-682B-46ED-B9B7-66C95EEF2BD0" rel="noopener" target="_blank">PackageContents.xml</a> で Loadreasons 値に <a href="https://help.autodesk.com/view/ACD/2023/JPN/?guid=GUID-3C25E517-8660-4BB7-9447-2310462EF06F" rel="noopener" target="_blank">LoadOnProxy</a> を指定しておくことで、処理対象の図面にカスタム オブジェクトが含まれた場合に自動的にロードさせて、プロキシ オブジェクトになることを抑止することが出来ます。</p>
<p>By Toshiaki Isezaki</p>
