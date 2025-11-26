---
layout: "post"
title: "AutoCAD 雑学：バックグラウンド ジョブとフォアグラウンド ジョブ"
date: "2022-06-08 01:19:03"
author: "Toshiaki Isezaki"
categories:
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/06/autocad-trivia-background-foreground-jobs.html "
typepad_basename: "autocad-trivia-background-foreground-jobs"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e1583414200b-pi" style="display: inline;"><img alt="Banner2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e1583414200b image-full img-responsive" src="/assets/image_842778.jpg" title="Banner2" /></a></p>
<p>AutoCAD 上の図面に他形式のファイルを読み込む <a href="https://help.autodesk.com/view/ACD/2023/JPN/?guid=GUID-81A5EE76-39A7-40A4-A5C5-E4921C03B33A" rel="noopener" target="_blank">IMPORT[読み込み]</a> コマンドや、図面内のオブジェクトを他のファイル形式で保存する <a href="https://help.autodesk.com/view/ACD/2023/JPN/?guid=GUID-A72DB257-3410-4792-B548-6B9FC1DED72B" rel="noopener" target="_blank">EXPORT[書き出し]</a> コマンドを起動すると、処理の開始時にバックグラウンド ジョブとして実行されることを知らせるメッセージが表示されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278807f9865200d-pi" style="display: inline;"><img alt="Background_process" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0278807f9865200d image-full img-responsive" src="/assets/image_191244.jpg" title="Background_process" /></a></p>
<p>「ジョブ」は一連の処理の単位を指しますが、ここでは「コマンド」と考えて差支えありません。「バックグラウンド」は、コマンドを起動した AutoCAD ではなく背後で起動した別の処理で実行されることを意味します。逆に、コマンドを起動した AutoCAD は、前面でユーザ操作を受け付ける対話処理も受け付ける「フォアグラウンド」な「ジョブ」と言えます。</p>
<p>バックグラウンド ジョブの利点は、フォアグラウンド ジョブの動作を妨げない点にあります。扱うファイルのサイズや内容にもよりますが、IMPORT / EXPORT コマンドの処理には時間がかかる場合があります。そのような場面でも、バックグラウンド ジョブ実行中に AutoCAD の操作を継続できるのは、この恩恵によるものです。</p>
<p>ただ、あまりに多くのバックグラウンド ジョブを起動してしまうと、メモリや CPU リソースを多く消費してしまい、結果としてフォアグラウンド ジョブの操作への反応が遅くなってしまうと本末転倒です。このため、処理中には添付のようなダイアログを表示して、過剰な負荷がかからないようになっています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278807faf1b200d-pi" style="display: inline;"><img alt="Background_job" class="asset  asset-image at-xid-6a0167607c2431970b0278807faf1b200d img-responsive" src="/assets/image_706264.jpg" style="width: 400px;" title="Background_job" /></a></p>
<p>さて、Windows で AutoCAD を利用して IMPORT コマンド、あるいは、EXPORT コマンドの実行したときに、[Ctrl] + [Alt] + [Del] キーを同時に押して<a href="https://ja.wikipedia.org/wiki/Windows_%E3%82%BF%E3%82%B9%E3%82%AF_%E3%83%9E%E3%83%8D%E3%83%BC%E3%82%B8%E3%83%A3%E3%83%BC" rel="noopener" target="_blank">タスクマネージャー</a>を起動してみると、[プロセス] タブで、フォアグラウンドで動作している AutoCAD に関連付いたバックグラウンド ジョブ「AutoCAD Translators」を確認することが出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e1581ab7200b-pi" style="display: inline;"><img alt="Task_manager" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e1581ab7200b image-full img-responsive" src="/assets/image_659227.jpg" style="width: 614.067px;" title="Task_manager" /></a></p>
<p>この「AutoCAD Translators」の実態は、AutoCAD のインストール フォルダ直下にある AcTranslators.exe です。AcTranslators.exe は Windows のコマンド プロンプトで実行出来るコンソール アプリケーションで、その使用方法やカスタマイズはオンライン ヘルプ等でも触れられていない<span style="background-color: #ffff00;">未サポート</span>なものです。</p>
<p>ただ、少しだけ触れておくと、-i オプションで変換対象の変換前のソース ファイルを、-o オプションで出力先の変換後ファイル名を指定するようになっています。</p>
<p>例）</p>
<blockquote>
<p><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">AcTranslators.exe -i C:\temp\Glass-Roof.dwg -o C:\temp\Glass-Roof.igs</span></p>
</blockquote>
<p>もう１つ、むしろ、こちらのほうが有名かもしれませんが、バックグラウンド ジョブで利用されるのが PLOT コマンドや PUBLISH コマンドを使った印刷処理です。既定値では、連続印刷処理を意図した PUBLISH コマンドが、バックグラウンド ジョブに設定されています。</p>
<p>PUBLISH コマンドのバックグラウンド処理で起動されるのは、IMPORT / EXPORT コマンドが利用する AcTranslators.exe よりも少し複雑になるので割愛しますが、やはり、AutoCAD のインストール フォルダ配下にある .exe が起動されることになります。</p>
<p>IMPORT / EXPORT コマンドとは異なり、PLOT / PUBLISH コマンドによる印刷処理は、システム変数 <a href="https://help.autodesk.com/view/ACD/2023/JPN/?guid=GUID-713029B7-B5AC-4860-BE2E-74878D418EA4" rel="noopener" target="_blank">BACKGROUNDPLOT</a> でコントロールすることが出来ます。</p>
<p>バックグラウンド ジョブの実行には、AcTranslators.exe をはじめとした .exe だけでなく、AutoCAD の他の<a href="https://adndevblog.typepad.com/technology_perspective/2022/03/autocad-trivia-components.html" rel="noopener" target="_blank">コンポーネント</a>も必要です。もちろん、AutoCAD ライセンスの一部として提供されるものなので、.exe や関連コンポーネントのみを抜き出して他の環境で利用することは、コンプライアンス上、<a href="https://www.autodesk.com/company/terms-of-use/jp/general-terms" rel="noopener" target="_blank">提供物の利用の許容範囲</a> 外になります。ご注意ください。</p>
<p>By Toshiaki Isezaki</p>
