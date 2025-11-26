---
layout: "post"
title: "AutoCAD ノーコード ソリューション：フィールド"
date: "2025-06-18 00:56:01"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2025/06/autocad-lno-code-solution-field.html "
typepad_basename: "autocad-lno-code-solution-field"
typepad_status: "Publish"
---

<p>&#0160;</p>
<p><br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860fa9e77200d-pi" style="display: inline;"><img alt="Field" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860fa9e77200d img-responsive" src="/assets/image_505799.jpg" title="Field" /></a></p>
<p>AutoCAD 2005 で導入されたフィールドは、特定の情報を文字（MTEXT）として作図する機能を提供すると同時に、元の情報ソースを監視して、常に最新の情報を文字に反映する機能も持ち合わせたインテリジェントな能力を持ち合わせています。</p>
<p>例えば、表題欄に現在の時刻を示す文字オブジェクトを挿入したい場合、<a href="https://help.autodesk.com/view/ACD/2026/JPN/?guid=GUID-742C92C3-1284-4722-B650-C46F9191C701" rel="noopener" target="_blank">FIELD[フィールド] コマンド</a>で <a href="https://help.autodesk.com/view/ACD/2026/JPN/?guid=GUID-1B6DD22B-10D1-44ED-BAA2-E6D79FE52327" rel="noopener" target="_blank">[フィールド] ダイアログ</a>を表示させて、日付フィールドを挿入することで、日付が自動更新して表示されるので、以前のように手作業で修正する必要がなく、手間が省け直し忘れもなくなります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3ccf88c200c-pi" style="display: inline;"><img alt="Date_field" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3ccf88c200c image-full img-responsive" src="/assets/image_639014.jpg" title="Date_field" /></a></p>
<p>フィールドの特筆 すべき点が、オブジェクト プロパティを文字化して、常に最新の情報を維持出来る点です。</p>
<p>例えば、[フィールド] ダイアログの「フィールド名」で「オブジェクト」を選択して作図済の円オブジェクトを選択すると、ダイアログ中央に円オブジェクトが持つプロパティが表示されます。「面積」を選択後、[その他の形式(O)...] ボタンで接尾語の &quot;㎡&quot; を指定してフィールドを作図すると、円の面積に &quot;㎡&quot; が付加された MTEXT が作図されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860faa045200d-pi" style="display: inline;"><img alt="Circle_area" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860faa045200d image-full img-responsive" src="/assets/image_909017.jpg" title="Circle_area" /></a></p>
<p>作図された面積は、選択した円を監視して、常に最新の面積を反映します。反映のタイミングは REGEN[再作図] コマンドの実行、あるいは、再作図が自動的におこなわれる図面の印刷時や図面のオープン時になります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860faa149200d-pi" style="display: inline;"><img alt="Regen" class="asset  asset-image at-xid-6a0167607c2431970b02e860faa149200d img-responsive" src="/assets/image_846834.jpg" style="width: 640px; display: block; margin-left: auto; margin-right: auto;" title="Regen" /><br /></a>指定したオブジェクトのプロパティを自動的に反映するオブジェクト フィールドは、図面を開き直しても維持されます。このような機能を AutoCAD API でゼロから実装するには、C++ 言語を用いる ObjectARX の不変リアクタの利用が必要になるため、比較的、難易度の高い API カスタマイズが必要です。</p>
<p>そういった意味も含め、フィールドは AutoCAD の高機能な<strong>ノーコード ソリューション</strong>ということが出来ます。</p>
<p>なお、AutoCAD 画面上のフィールド文字の背景は、通常の MTEXT オブジェクトと区別するためにグレーで表示されますが、印刷時には背景は印刷されずに文字のみが表現されます。</p>
<p>そんな高機能なフィールドは、AutoCAD の様々な機能で活用されています。AutoCAD 2022 で導入された <a href="https://help.autodesk.com/view/ACD/2026/JPN/?guid=GUID-3A0C3460-6ABC-4D13-BF1F-D2BFCD399851" rel="noopener" target="_blank">COUNT[カウント] コマンド</a>では、ブロック集計で作図出来る表オブジェクトの値にフィールドが使われています。これによって、図面内で集計済のブロック数が編集で増減しても集計表を維持することが出来るようになっています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860faa174200d-pi" style="display: inline;"><img alt="Count" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860faa174200d image-full img-responsive" src="/assets/image_190614.jpg" title="Count" /></a></p>
<p>フィールドの機能は、AutoCAD API からも利用することが出来ます。もちろん、その機能を流用するかたちになるので、不変リアクタを意識する必要はなく、ObjectARX を使用する必要もありません。</p>
<p>オブジェクト フィールドは、特定のオブジェクトを監視するために、内部的に対象オブジェクトの ObjectId 識別子を</p>
<p>フィールドの機能は、AutoCAD API からも利用することが出来ます。もちろん、その機能を流用するかたちになるので、不変リアクタを利用する必要はなく、ObjectARX を使用する必要もありません。</p>
<p>オブジェクト フィールドは特定のオブジェクトを監視するために、内部的に対象オブジェクトの<a href="https://adndevblog.typepad.com/technology_perspective/2013/10/object-identifer-of-dautocad-api.html" rel="noopener" target="_blank">オブジェクト ID 識別子</a>を使用します。あとは、オブジェクト フィールドを作成する際に [フィールド] ダイアログ下部に表示される「フィールド式」を MText オブジェクトのコンテンツに設定するだけです。</p>
<ul>
<li><a href="https://forums.autodesk.com/t5/autodesk-community-tips-adnopun/autocad-net-api-obujekutowo-can-zhaosurufirudono-zuo-cheng/ta-p/12129442" rel="noopener" target="_blank">AutoCAD .NET API ：オブジェクトを参照するフィールドの作成</a></li>
<li><a href="https://forums.autodesk.com/t5/autodesk-community-tips-adnopun/autocad-vba-firudowo-shitta-biaono-zuo-cheng/ta-p/13099139" rel="noopener" target="_blank">AutoCAD VBA：フィールドを使った表の作成</a></li>
</ul>
<p>By Toshiaki Isezaki</p>
