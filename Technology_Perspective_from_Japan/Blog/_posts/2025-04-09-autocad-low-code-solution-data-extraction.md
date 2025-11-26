---
layout: "post"
title: "AutoCAD ノーコード ソリューション：データ書き出し"
date: "2025-04-09 00:11:49"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2025/04/autocad-low-code-solution-data-extraction.html "
typepad_basename: "autocad-low-code-solution-data-extraction"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3ccf59f200c-pi" style="display: inline;"><img alt="Data_extraction" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3ccf59f200c img-responsive" src="/assets/image_562670.jpg" title="Data_extraction" /></a></p>
<p>AutoCAD での図面編集中、よくあるタスクに作図空間に挿入されているブロック数の拾い上げ（集計）があります。一般的に、機器や部品、建具など、規格化された形状をブロック化することが多いため、図面内のブロックを数えることで、部品表や建具表などの集計表作成に役立てることが出来ます。</p>
<p>古くから AutoCAD API で作成したカスタム コマンドでブロックの集計と表作図の自動化を実現している場合には、AutoCAD 2008 で導入されている<a href="https://help.autodesk.com/view/ACD/2026/JPN/?guid=GUID-B0D32260-45E3-4643-B574-7F6C31579B68" rel="noopener" target="_blank">データ書き出し</a>（<a href="https://help.autodesk.com/view/ACD/2026/JPN/?guid=GUID-5A39FFE8-10AC-4AE5-8EF4-D097C8261D1A" rel="noopener" target="_blank">DATAEXTRACTION[データ書き出し] コマンド</a>）を使ってカスタム コマンドの機能を代替出来る可能性があります。</p>
<p>データ書き出しは、ウィザード形式でオプション選択を進めるだけで、対象のオブジェクト情報を表オブジェクトとして作図したり、外部の EXCEL ファイルに出力したりすることが出来る機能を提供します。</p>
<p>DATAEXTRACTION コマンド&#0160;を実行すると、<a href="https://help.autodesk.com/view/ACD/2026/JPN/?guid=GUID-8D59F5C3-6571-4D34-A35B-3C1BC71F7FF3" rel="noopener" target="_blank">データ書き出し ウィザード</a> が表示されます。データ書き出しの対象は図面内に作図されているすべてのオブジェクトですが、3/8 の画面で「ブロックのみ」を選択して集計を進めることが出来ます。また、ブロックに属性が設定されている場合には、「属性を持つブロックのみを表示」を選択することも出来ます。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860e37953200b-pi" style="display: inline;"><img alt="Extract_wizard2" class="asset  asset-image at-xid-6a0167607c2431970b02e860e37953200b img-responsive" src="/assets/image_968879.jpg" style="width: 550px;" title="Extract_wizard2" /></a></p>
<p>4/8 の画面では、集計から特定のブロックを除外したり、分類フィルタで不要な情報を除外したりする指定が可能です。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860e37963200b-pi" style="display: inline;"><img alt="Extract_wizard4" class="asset  asset-image at-xid-6a0167607c2431970b02e860e37963200b img-responsive" src="/assets/image_456289.jpg" style="width: 550px;" title="Extract_wizard4" /></a></p>
<p>5/8 画面では、集計された情報をプレビューして列の並びなどを変更することが出来ます。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860e37965200b-pi" style="display: inline;"><img alt="Extract_wizard5" class="asset  asset-image at-xid-6a0167607c2431970b02e860e37965200b img-responsive" src="/assets/image_416848.jpg" style="width: 550px;" title="Extract_wizard5" /></a></p>
<p>6/8 画面では、集計内容を表オブジェクト（TABLE オブジェクト）として図面に作図するか、EXCEL ファイルに書き出すかを指定することが可能になっています。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860e3796b200b-pi" style="display: inline;"><img alt="Extract_wizard6" class="asset  asset-image at-xid-6a0167607c2431970b02e860e3796b200b img-responsive" src="/assets/image_446452.jpg" style="width: 550px;" title="Extract_wizard6" /></a></p>
<p>表オブジェクトの作図を選択した場合、表スタイルや表タイトルを指定して、表を指定位置に作図することが出来ます。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860e3796d200b-pi" style="display: inline;"><img alt="Extract_wizard7" class="asset  asset-image at-xid-6a0167607c2431970b02e860e3796d200b img-responsive" src="/assets/image_878108.jpg" style="width: 550px;" title="Extract_wizard7" /></a></p>
<p>つまり、このデータ書き出し機能も、古くは API カスタマイズが必要だった集計表の作成を、標準コマンドで実現出来る<strong>ノーコード ソリューション</strong>と捉えることが出来ます。古いカスタム コマンドでブロックの集計表を作成していたり、EXCEL への書き出しが必要な場合には、一度、お試しください。</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/LoSWPRTdqaM" width="480"></iframe></p>
<p>By Toshiaki Isezaki</p>
