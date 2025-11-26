---
layout: "post"
title: "Design Automation API for AutoCAD：.pc3 ファイルの上書き"
date: "2023-05-10 00:57:07"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2023/05/design-automation-api-for-autocad-override-pc3-file.html "
typepad_basename: "design-automation-api-for-autocad-override-pc3-file"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b7519c9af1200c-pi" style="display: inline;"><img alt="Default_pc3" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b7519c9af1200c image-full img-responsive" src="/assets/image_957230.jpg" title="Default_pc3" /></a></p>
<p>Design Automation API for AutoCAD で PDF ファイルや DWF ファイルを生成する際、ローカル コンピュータの環境で日頃利用しているプロッタ環境設定ファイル（.pc3 ファイル）をそのまま使用したい場合があります。</p>
<p><a href="https://help.autodesk.com/view/ACD/2023/JPN/?guid=GUID-C7895A4B-F22E-40D7-B529-917ACEED6518" rel="noopener" target="_blank">プロッタを追加ウィザード</a> を利用して作成した独自の pc3 ファイルも、AutoCAD の既定の pc3 ファイルを編集した場合も、Design Automation API for AutoCAD で活用することが出来ます。いずれの場合も、pc3 ファイルを AppBundle に同梱しておき、WorkItem の実行時に、コアエンジンが参照出来るようにする必要があります。</p>
<p>AppBundle から展開されるパスは、アドイン ファイル内の処理で取得することが出来ます。また、同じパスに pc3 ファイルが配置されていると考えることが可能です。アドイン ファイルの配置パスは、過去にご紹介した <a href="https://adndevblog.typepad.com/technology_perspective/2021/10/use-of-contents-in-appbundle.html" rel="noopener" target="_blank">Design Automation API for AutoCAD：AppBundle 内のコンテンツ利用</a> の記事でご紹介しています。</p>
<p>印刷処理の場合、Design Automation API for AutoCAD のコアエンジンも、ローカル コンピュータのデスクトップ版 AutoCAD 同様、WorkItem 実行時に用意された仮想マシンの作業フォルダに、既定の pc3 ファイルを展開して印刷処理に備えます。</p>
<p><a href="https://adndevblog.typepad.com/technology_perspective/2023/03/design-automation-apiadskdebug-option.html" rel="noopener" target="_blank">Design Automation API：adskDebug オプション</a> の方法で、作業環境上の相対パスを把握することが可能です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751784bcf200b-pi" style="display: inline;"><img alt="Plotter_path_on_vm" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b751784bcf200b img-responsive" src="/assets/image_4274.jpg" title="Plotter_path_on_vm" /></a></p>
<p>AppBundle に同梱した pc3 ファイルは、次のようにパスを解決、コア エンジンが参照するパスにコピーすることで、印刷処理時に流用出来るようになります。</p>
<div>
<blockquote>
<div><span style="font-size: 11pt; font-family: arial, helvetica, sans-serif;">...</span></div>
<div><span style="font-size: 11pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; string strCfgPath = System.Reflection.Assembly.GetExecutingAssembly().Location;</span></div>
<div><span style="font-size: 11pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; strCfgPath = strCfgPath.Substring(0, strCfgPath.IndexOf(System.IO.Path.GetFileName(strCfgPath)));</span></div>
<div><span style="font-size: 11pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; string strDestPath = strDwgPath;</span></div>
<div><span style="font-size: 11pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; strDestPath += &quot;userdata\\Roaming\\Plotters\\&quot; + &quot;DWG To PDF.pc3&quot;;</span></div>
<div><span style="font-size: 11pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; strCfgPath += &quot;DWG To PDF.pc3&quot;;</span></div>
<div><span style="font-size: 11pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; log(&quot;\n&gt;&gt;&gt;&gt;&gt; DWG To PDF.pc3 path = {0}&quot;, strCfgPath);</span></div>
<div><span style="font-size: 11pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; log(&quot;\n&gt;&gt;&gt;&gt;&gt;&gt; Destination path = {0}&quot;, strDestPath);</span></div>
<div><span style="font-size: 11pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; if (System.IO.File.Exists(strCfgPath))</span></div>
<div><span style="font-size: 11pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; {</span></div>
<div><span style="font-size: 11pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &#0160;log(&quot;\n&gt;&gt;&gt;&gt;&gt;&gt; DWG To PDF.pc3 Found&quot;);</span></div>
<div><span style="font-size: 11pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &#0160;try</span></div>
<div><span style="font-size: 11pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &#0160;{</span></div>
<div><span style="font-size: 11pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; System.IO.File.Copy(strCfgPath, strDestPath, true);</span></div>
<div><span style="font-size: 11pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; log(&quot;\n&gt;&gt;&gt;&gt;&gt;&gt; DWG To PDF.pc3 was copied to destination path = {0}&quot;, strDestPath);</span></div>
<div><span style="font-size: 11pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &#0160;}</span></div>
<div><span style="font-size: 11pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &#0160;catch (System.Exception ex)</span></div>
<div><span style="font-size: 11pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &#0160;{</span></div>
<div><span style="font-size: 11pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; log(&quot;\n&gt;&gt;&gt;&gt;&gt;&gt; System.IO.File.Copy failed : {0}&quot;, ex.Message);</span></div>
<div><span style="font-size: 11pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &#0160;}</span></div>
<div><span style="font-size: 11pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; }</span></div>
<div><span style="font-size: 11pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; else</span></div>
<div><span style="font-size: 11pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; {</span></div>
<div><span style="font-size: 11pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &#0160;log(&quot;\n&gt;&gt;&gt;&gt;&gt;&gt; DWG To PDF.pc3 is *NOT* Found&quot;);</span></div>
<div><span style="font-size: 11pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; }</span></div>
<div><span style="font-size: 11pt; font-family: arial, helvetica, sans-serif;">...</span></div>
<div>&#0160;</div>
<div><span style="font-size: 11pt; font-family: arial, helvetica, sans-serif;">&#0160; private static void log(string format, params object[] args) { Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage(format, args); }</span></div>
<div>&#0160;</div>
<div><span style="font-size: 11pt; font-family: arial, helvetica, sans-serif;">...</span></div>
</blockquote>
By Toshiaki Isezaki</div>
