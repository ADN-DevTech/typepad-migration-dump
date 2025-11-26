---
layout: "post"
title: "Revit 2022 の新機能 その1"
date: "2021-04-09 01:00:57"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/04/new-features-on-revit-2022-part1.html "
typepad_basename: "new-features-on-revit-2022-part1"
typepad_status: "Publish"
---

<p>今年も Revit の新バージョンとなる Revit 2022 がリリースされました。</p>
<p>今回から複数回にわたって、Revit 2022 の新機能と改良された機能をご紹介します。<br />今回と次回は、プラットフォームに共通の新機能と機能向上の内容となります。その後、建築、MEP、構造の各専門分野の機能を取り上げる予定です。</p>
<p>また、Revit 2021 の更新においてオートデスクのサブスクリプションメンバー向けに用意されたほとんどの新機能と強化された機能が、Revit 2022 のユーザにもご利用いただけるようになりました。</p>
<hr />
<p><strong>PDF 書き出し</strong></p>
<p>Revit から PDF ファイルに直接 2D ビューとシートを書き出すことができるようになりました。<br />単一ページの PDF ファイルを書き出したり、選択した複数のビューやシートを PDF ファイルに結合します。<br /><br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdec9f045200c-pi" style="display: inline;"><img alt="Revit2022-01-01" class="asset  asset-image at-xid-6a0167607c2431970b026bdec9f045200c img-responsive" src="/assets/image_997.jpg" title="Revit2022-01-01" /></a><br />複数の PDF ファイルを書き出す場合は、書き出される PDF ファイルの命名規則を作成します。<br />ビューとシートからパラメータを選択して命名規則を設定します。<br /><br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02788021e0ed200d-pi" style="display: inline;"><img alt="Revit2022-01-02" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02788021e0ed200d image-full img-responsive" src="/assets/image_545883.jpg" title="Revit2022-01-02" /></a></p>
<hr />
<p><strong>2D ビューを共有する</strong></p>
<p>[共有ビュー]ツールを使用して、モデルの 2D ビューをクラウド内のプロジェクト関係者と共有でき、Revit を持っていないチーム メンバーとのコラボレーションが可能になります。<br />プロジェクト関係者は、共有ビューにコメントや返信を直接追加することができます。<br /><br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdec9f0e1200c-pi" style="display: inline;"><img alt="Revit2022-01-03" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdec9f0e1200c image-full img-responsive" src="/assets/image_359533.jpg" title="Revit2022-01-03" /></a><br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02788021e1a6200d-pi" style="display: inline;"><img alt="Revit2022-01-04" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02788021e1a6200d img-responsive" src="/assets/image_772014.jpg" title="Revit2022-01-04" /></a><br /><br />例えば、ある顧客向けに共有ビューを作成し、顧客に承認を求めたり、現場のチーム メンバーが簡単に見られるようにすることができます。用意した共有ビューのリンクを知らせるだけで、Revit をインストールしていない環境でもその共有ビューを表示したりコメントすることができます。誰かが共有ビューにコメントすると、電子メールが送信されてきます。コメントの表示や返信、共有ビューの管理は Revit から直接行うことができます。<br /><br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdec9f119200c-pi" style="display: inline;"><img alt="Revit2022-01-06" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdec9f119200c image-full img-responsive" src="/assets/image_995255.jpg" title="Revit2022-01-06" /></a></p>
<hr />
<p><strong>Rhinocerous® (3DM)ファイルをリンクする</strong></p>
<p>Rhino で設計を開始して、3DM ファイルを Revit モデルにリンクし、これをベースに Revit で設計することができるようになりました。<br />3DM ファイルの変更が予測される場合は、3DM ファイルを Revit モデルにリンクし、3DM ファイルが更新されると、変更を加えたファイルをモデルに再ロードすることで、常に最新の設計を確認することができます。</p>
<hr />
<p><strong>タグの自由回転</strong></p>
<p>プロパティの[角度]パラメータを使用して、タグを回転できるようになりました。<br />回転が必要なタグを選択し、[プロパティ]パレットで[角度]パラメータの値を設定します。<br />必要に応じて、複数のタグを同じ値で回転することもできます。<br /><br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02788021e245200d-pi" style="display: inline;"><img alt="Revit2022-01-07" class="asset  asset-image at-xid-6a0167607c2431970b02788021e245200d img-responsive" src="/assets/image_714806.jpg" title="Revit2022-01-07" /></a></p>
<hr />
<p><strong>マルチ引出線タグ</strong></p>
<p>新しいマルチ引出線タグを使用して、同じカテゴリの複数の要素を参照するタグを 1 つのタグにまとめて配置することができるようになりました。<br />このオプションを有効にすると、タグ付けするための複数の参照を選択できます。<br /><br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02788021e32f200d-pi" style="display: inline;"><img alt="Revit2022-01-08" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02788021e32f200d image-full img-responsive" src="/assets/image_642130.jpg" title="Revit2022-01-08" /></a><br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e99caa9a200b-pi" style="display: inline;"><img alt="Revit2022-01-09" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e99caa9a200b image-full img-responsive" src="/assets/image_209115.jpg" title="Revit2022-01-09" /></a><br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02788021e342200d-pi" style="display: inline;"><img alt="Revit2022-01-10" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02788021e342200d image-full img-responsive" src="/assets/image_574030.jpg" title="Revit2022-01-10" /></a></p>
<hr />
<p><strong>リンクへのタグの再ホスト</strong></p>
<p>ホスト モデルのタグは、リンク モデルのホストを記憶し、自動的に再度ホストします。<br />リンク モデル内の要素にアタッチされているホスト モデル内のタグは、リンク内の要素 ID を記憶するようになりました。リンクがロード解除されているか古い場合、タグは孤立します。最新のリンクを再ロードすると、タグが使用可能な場合は自動的に要素に再ホストされます。</p>
<hr />
<p><strong>マルチカテゴリ タグの拡張機能</strong></p>
<p>マルチカテゴリ タグを使用して、タグ付け可能なすべての要素にタグを付けることができるようになりました。<br />共通パラメータまたは共有パラメータをタグのラベルに表示できます。</p>
<hr />
<p><strong>3D ビューの通芯</strong></p>
<p>3D ビューで作業するときに、モデルの通芯を表示および修正できるようになりました。<br />通芯を表示するには、3D ビュー プロパティ パレットの[グラフィックス]で、[グリッドを表示]の横にある[編集]ボタンをクリックします。<br />[グリッドを表示]ダイアログで、通芯線を表示するレベルのチェックボックスをオンにします。<br /><br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e99cab36200b-pi" style="display: inline;"><img alt="Revit2022-01-11" class="asset  asset-image at-xid-6a0167607c2431970b0263e99cab36200b img-responsive" src="/assets/image_242333.jpg" title="Revit2022-01-11" /></a><br />通芯線は、レベル面とグリッド サーフェスの交点を表します。通芯線は、選択したレベルと交差するグリッド サーフェスに対してのみ表示されます。通芯またはレベルの位置が変更された場合、それらは自動的に更新されます。チェックされた各通芯とレベルの交点に通芯線が表示されます。<br /><br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdec9f3c0200c-pi" style="display: inline;"><img alt="Revit2022-01-12" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdec9f3c0200c image-full img-responsive" src="/assets/image_989784.jpg" title="Revit2022-01-12" /></a></p>
<hr />
<p><strong>3D 形状の参照面</strong></p>
<p>読み込んだ 3D 形状で参照面を使用して、寸法記入、スナップ、位置合わせを行うことができるようになりました。<br />これは、読み込んだ 3D 形状を Revit モデルに配置する際に役立ちます。<br />また、3D 形状として読み込んだ 3DM ファイルと SAT ファイルに、元のジオメトリの一部である参照面が含まれるようになりました。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e99cabb0200b-pi" style="display: inline;"><img alt="Revit2022-01-13" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e99cabb0200b img-responsive" src="/assets/image_570496.jpg" title="Revit2022-01-13" /></a></p>
<hr />
<p><strong>複数の値の表示</strong></p>
<p>複数の要素を選択、集計、タグ付けした場合に、異なる値を持つプロパティをどのように表示するかをコントロールできます。<br />このリリースでは、複数の要素を選択してパラメータ値が異なる場合、&lt;各種&gt;または指定したカスタム テキスト文字列として表示されるようになりました。<br />この動作は、プロパティ パレット、集計表、タグで共通しています。<br /><br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e99cabdb200b-pi" style="display: inline;"><img alt="Revit2022-01-15" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e99cabdb200b image-full img-responsive" src="/assets/image_280652.jpg" title="Revit2022-01-15" /></a></p>
<hr />
<p><strong>寸法タイプの接頭辞/接尾辞</strong></p>
<p>寸法タイプに接頭辞、接尾辞、またはその両方を追加して、ドキュメントのニーズに合わせて寸法をカスタマイズできるようになりました。<br /><br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e99cabf8200b-pi" style="display: inline;"><img alt="Revit2022-01-16" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e99cabf8200b img-responsive" src="/assets/image_587264.jpg" title="Revit2022-01-16" /></a><br />接頭辞または接尾辞を含むカスタム寸法が必要な場合、寸法ファミリ タイプで接頭辞と接尾辞の定義を含めることができるようになりました。<br />これまでは、寸法の個々のインスタンスに接頭辞や接尾辞の値を追加することしかできませんでした。タイプに追加することで、時間を節約できます。タイプで定義された接頭辞と接尾辞は、寸法を配置したときに自動的に追加されます。</p>
<hr />
<p>次回は、引き続きプラットフォームに共通の内容となります。集計表に関連する様々な機能とその他（カラースキームや改訂など）の機能をご紹介致します。</p>
<p>By Ryuji Ogasawara</p>
