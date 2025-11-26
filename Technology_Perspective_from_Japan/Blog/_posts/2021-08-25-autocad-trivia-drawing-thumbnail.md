---
layout: "post"
title: "AutoCAD 雑学：図面のサムネイル画像"
date: "2021-08-25 00:21:00"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/08/autocad-trivia-drawing-thumbnail.html "
typepad_basename: "autocad-trivia-drawing-thumbnail"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b027880427cc5200d-pi" style="display: inline;"><img alt="Title" border="0" class="asset  asset-image at-xid-6a0167607c2431970b027880427cc5200d image-full img-responsive" src="/assets/image_88419.jpg" title="Title" /></a></p>
<p>AutoCAD で図面を作成して図面を保存すると、Windows Explorer や Mac の Finder で DWG ファイル アイコンの表示サイズを中アイコン以上に設定すると、図面のプレビューが表示されます。</p>
<p>これを可能にしているのは、AutoCAD が図面保存時にサムネイル画像（プレビュー画像）の生成してファイルに埋め込んでいるためです。埋め込まれるサムネイル画像の解像度は図面ファイル形式によって変化していますが、現在では、システム変数 <a href="https://help.autodesk.com/view/ACD/2022/JPN/?guid=GUID-E6BA0E3A-7F9A-4C94-BF85-284309263ED0" rel="noopener" target="_blank"><strong>THUMBSAVE</strong></a> で図面保存時のサムネイル画像の更新オン・オフのコントロールと、システム変数 <a href="https://help.autodesk.com/view/ACD/2022/JPN/?guid=GUID-E295F164-A540-4B4A-9844-6159CC91BF89" rel="noopener" target="_blank"><strong>THUMBSIZE</strong></a> で解像度を指定出来るようになっています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e11aed9a200b-pi" style="display: inline;"><img alt="Thumbnails" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e11aed9a200b image-full img-responsive" src="/assets/image_905012.jpg" title="Thumbnails" /></a></p>
<p>さて、このサムネイル画像ですが、埋め込みを正しくおこなうには、AutoCAD のユーザインタフェース上に図面を開き、保存する必要があります。</p>
<p>AutoCAD API には、ObjectARX の AcDbDatabase::readDwgFile()&#0160; と AcDbDatabase::saveAs() 関数の組み合わせで、また、 AutoCAD .NET API の Database.ReadDwgFile と Database.SaveAs メソッドの組み合わせで、AutoCAD のユーザインタフェース上に図面を開くことなく、メモリ上に図面を開いて、（編集、）保存することが出来ます。だたし、残念ながら、この方法ではサムネイル画像は埋め込まれません。</p>
<p>場合によっては、一括して特定フォルダ内のすべての図面に対して、サムネイル画像を更新したいケースがあるかもしれません。ただ、AutoCAD API を使ってユーザインタフェース上に図面を開き、保存する処理では、対象図面の数が多いと時間がかかってしまいます。</p>
<p>このような場合、AutoCAD に同梱されている<strong><a href="https://nam11.safelinks.protection.outlook.com/?url=https%3A%2F%2Fadndevblog.typepad.com%2Ftechnology_perspective%2F2013%2F06%2Fconsole-version-of-autocad.html&amp;data=04%7C01%7Ctoshiaki.isezaki%40autodesk.com%7C4583b921895c4cc56f6108d957c6e442%7C67bff79e7f914433a8e5c9252d2ddc1d%7C0%7C0%7C637637334022943884%7CUnknown%7CTWFpbGZsb3d8eyJWIjoiMC4wLjAwMDAiLCJQIjoiV2luMzIiLCJBTiI6Ik1haWwiLCJXVCI6Mn0%3D%7C1000&amp;sdata=icUo6MaIVpFb3A1PFexBiiYLPEq33iQcuXVuBh8Cw6s%3D&amp;reserved=0" rel="noopener" target="_blank">コンソール版の AutoCAD</a></strong> である <strong>AcCoreConsole.exe</strong> を使ってサムネイル画像の生成と保存する方法を代替することが出来ます。</p>
<p>AcCoreConsole.exe は AutoCAD のインストールフォルダに一緒にインストールされています。また、Forge の <strong><a href="https://nam11.safelinks.protection.outlook.com/?url=https%3A%2F%2Fadndevblog.typepad.com%2Ftechnology_perspective%2F2021%2F07%2Feffectiveness-on-design-automation-api-for-autocad.html&amp;data=04%7C01%7Ctoshiaki.isezaki%40autodesk.com%7C4583b921895c4cc56f6108d957c6e442%7C67bff79e7f914433a8e5c9252d2ddc1d%7C0%7C0%7C637637334022943884%7CUnknown%7CTWFpbGZsb3d8eyJWIjoiMC4wLjAwMDAiLCJQIjoiV2luMzIiLCJBTiI6Ik1haWwiLCJXVCI6Mn0%3D%7C1000&amp;sdata=p6MaGpiR3jofFSNx9ovJoJdvoe1CvAID8MgwnJCUf8I%3D&amp;reserved=0" rel="noopener" target="_blank">Design Automation API for AutoCAD</a></strong> のコアエンジンとして機能拡張がされている、ユーザインタフェースを持たない軽量な AutoCAD です。</p>
<p>サムネイル画像の更新には、Windows のバッチファイル（.bat）と AutoCAD スクリプト（.scr）を組み合わせることで、API を使用せずにサムネイル画像を生成出来ることが出来ます。</p>
<p>次の例は、バッチ ファイルで C:\temp フォルダ内の DWG ファイルを AcCoreConsole.exe で１つづつ開き、<em><strong>do_make_preview.scr</strong></em> スクリプトを実行するものです。（ZOOM コマンド E オプションで図形範囲にズームして上書き保存）</p>
<p><strong> <span class="asset  asset-generic at-xid-6a0167607c2431970b02788042abb6200d img-responsive"><a href="https://adndevblog.typepad.com/files/todo.bat"><em>todo.bat</em> ファイル</a></span></strong></p>
<blockquote>
<p><strong><em>@echo off<br /><br /><span style="color: #0000ff;">set accoreexe=&quot;C:\Program Files\Autodesk\AutoCAD 2022\accoreconsole.exe&quot;</span><br /><span style="color: #0000ff;">set script=&quot;C:\temp\do_make_preview.scr&quot; </span><br /><span style="color: #0000ff;">set source=&quot;C:\temp&quot;</span><br /><br />for %%f in (*.dwg) do (<br />&#0160; echo %%f </em></strong><strong><em>を処理中</em></strong><strong><em>...<br />&#0160; %accoreexe% /i &quot;%source%\%%f&quot; /s %script%<br />)</em></strong></p>
</blockquote>
<p>下記は、<em><strong>do_make_preview.scr</strong></em> スクリプトの内容です。</p>
<p><strong> <span class="asset  asset-generic at-xid-6a0167607c2431970b0282e11b20a5200b img-responsive"><a href="https://adndevblog.typepad.com/files/do_make_preview.scr"><em>do_make_preview.scr</em> ファイル</a></span></strong><em><strong>（改行行はリターンキー押下を意味します）</strong></em></p>
<blockquote>
<p><strong><em>zoom<br />e<br />save<br /><br />y<br />quit<br /><br /></em></strong></p>
</blockquote>
<p>バッチファイル内で<strong><span style="color: #0000ff;">環境変数に設定しているパス</span></strong>は、お使いの環境にあわせて置き換えてください。なお、日本語名のパスを認識しないことが報告されているようですので、一時的にフォルダ名を英語（ASCII 表現のアルファベット英数字）に置き換えて実行することをお勧めします。（また、操作対象のフォルダによっては、管理者権限が必要になる場合があります。ご注意ください。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e11aeeed200b-pi" style="display: inline;"><img alt="Todo" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e11aeeed200b image-full img-responsive" src="/assets/image_755662.jpg" title="Todo" /></a></p>
<p>By Toshiaki Isezaki</p>
