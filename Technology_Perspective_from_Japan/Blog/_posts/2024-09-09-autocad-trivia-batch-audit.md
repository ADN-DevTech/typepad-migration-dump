---
layout: "post"
title: "AutoCAD 雑学：バッチ図面監査"
date: "2024-09-09 00:00:53"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/09/autocad-trivia-batch-audit.html "
typepad_basename: "autocad-trivia-batch-audit"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02dad0cf7e26200d-pi" style="display: inline;"><img alt="Batch_audit" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02dad0cf7e26200d image-full img-responsive" src="/assets/image_439562.jpg" title="Batch_audit" /></a></p>
<p>複数の図面に対して<a href="https://adndevblog.typepad.com/technology_perspective/2014/07/drawing-structure-and-corruption-from-autocad-api-perspective.html" rel="noopener" target="_blank">図面破損</a>の有無を確認したいことがあるかもしれません。</p>
<p>AutoCAD 上で図面破損を検出するためには、<a href="https://help.autodesk.com/view/ACD/2025/JPN/?guid=GUID-62DDB935-61B1-49DA-8238-3EF1CC45259B" rel="noopener" target="_blank">AUDIT[監査]</a> コマンドを使用がありますが、AUDIT コマンドは現在開いている図面のみを監査対象にするため、複数の図面が対象なると、図面を 1 つずつ開く処理も必要になってしまいます。</p>
<p>残念ながら、AutoCAD は複数の図面に対して図面監査をバッチ処理する標準機能がありません。</p>
<div>このような場面では、<a href="https://adndevblog.typepad.com/technology_perspective/2021/08/autocad-trivia-drawing-thumbnail.html" rel="noopener" target="_blank">AutoCAD 雑学：図面のサムネイル画像</a> でご紹介した <a href="https://adndevblog.typepad.com/technology_perspective/2013/06/console-version-of-autocad.html" rel="noopener" target="_blank">コンソール バージョンの AutoCAD</a> を Windows のバッチファイル（.bat）ファイルで呼び出す方法を利用することが出来ます。その際、メモリ上に開いた図面ファイルに AUDIT コマンドを実行させるために、AutoCAD スクリプト ファイル（.scr）を使用することになります。</div>
<div>
<div>
<p>次の例は、temp フォルダ内の図面をコンソール バージョンの AutoCAD（AcCoreConsole.exe）で開き、図面監査のみをおこなうものです。</p>
</div>
<div><strong>todo.bat</strong></div>
<div>
<blockquote>
<div>set accoreexe=&quot;C:\Program Files\Autodesk\AutoCAD 2025\accoreconsole.exe&quot;</div>
<div>set script=&quot;C:\temp\do_make_audit.scr&quot;</div>
<div>set source=&quot;C:\temp&quot;</div>
<div>cd source</div>
<br />
<div>for %%f in (*.dwg) do (</div>
<div>&#0160; echo %%f を処理中...</div>
<div>&#0160; %accoreexe% /i &quot;%source%\%%f&quot; /s %script%</div>
<div>)</div>
</blockquote>
<div><strong>do_make_audit.scr</strong></div>
<blockquote>
<div>auditctl<br />1</div>
<div>audit</div>
<div>n</div>
<div>quit</div>
</blockquote>
</div>
</div>
<p>バッチ処理による AUDIT コマンド実行前には、システム変数 <a href="https://help.autodesk.com/view/ACD/2025/JPN/?guid=GUID-05A7363C-4A43-4984-8262-DA7BEE7B860C" rel="noopener" target="_blank">AUDITCTL</a> を 1（オン）に設定しておくと、AUDIT コマンドがコマンド プロンプト ウィンドウに表示する監査内容を、図面ファイルと同じフォルダに図面監査ファイル（.adt の拡張子を持つテキスト ファイル）に書き出すことが可能です。この図面監査ファイルを後から調べれば、個々の図面の監査内容を把握することが出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3bbaf25200c-pi" style="display: inline;"><img alt="Bat_process" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3bbaf25200c image-full img-responsive" src="/assets/image_249659.jpg" title="Bat_process" /></a></p>
<p>また、<a href="https://www.autodesk.co.jp/support/technical/article/caas/tsarticles/ts/HMSn7mT0kA2fdCLqAjmpz.html" rel="noopener" target="_blank">AutoCAD .NET API：図⾯監査の⾃動化は可能か？</a> のように、ObjectARX と .NET API であれば、カスタム コマンドで AUDIT コマンドと同等の処理を実行させることも出来ます。例えば、次の .NET API コードを利用すると、temp フォルダ内の図面を<a href="https://help.autodesk.com/view/OARX/2025/JPN/?guid=OARX-ManagedRefGuide-Autodesk_AutoCAD_DatabaseServices_Database_ReadDwgFile_string_FileOpenMode__MarshalAsUnmanagedType_U1__bool_string" rel="noopener" target="_blank"> Database.ReadDwgFile</a> メソッドでメモリ上に開き（AutoCAD ユーザーインターフェース上には開かずに）、図面監査と修復を同時におこなうものです。</p>
<blockquote>
<div>[CommandMethod(&quot;MyCommand&quot;, CommandFlags.Modal)]</div>
<div>public void MyCommand()</div>
<div>{</div>
<div>&#0160; &#0160; Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;</div>
<div>&#0160; &#0160; Application.SetSystemVariable(&quot;AUDITCTL&quot;, 1);</div>
<div>&#0160; &#0160; IEnumerable&lt;string&gt; files = System.IO.Directory.EnumerateFiles(@&quot;C:\temp&quot;, &quot;*.dwg&quot;, System.IO.SearchOption.AllDirectories);</div>
<div>&#0160; &#0160; foreach (string dwg in files)</div>
<div>&#0160; &#0160; {</div>
<div>&#0160; &#0160; &#0160; &#0160; ed.WriteMessage(&quot;\n****** {0} 処理中&quot;, dwg);</div>
<div>&#0160; &#0160; &#0160; &#0160; using (Database db = new Database(false, true))</div>
<div>&#0160; &#0160; &#0160; &#0160; {</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; db.ReadDwgFile(dwg, FileOpenMode.OpenForReadAndAllShare, false, null);</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; db.CloseInput(true);</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; db.Audit(true, true);</div>
<div>&#0160; &#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; }</div>
<div>}</div>
</blockquote>
<p>この場合、各図面の監査状況は図面毎のウィンドウではなく、MyCommand を実行したウィンドウのコマンド プロンプトに一括表示しまます。ただし、.adt ファイルは図面毎に保存されるのは、バッチファイルでの処理と変わりません。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3bbb020200c-pi" style="display: inline;"><img alt="Api_readdwgfile" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3bbb020200c image-full img-responsive" src="/assets/image_963393.jpg" title="Api_readdwgfile" /></a></p>
<p>By Toshiaki Isezaki</p>
