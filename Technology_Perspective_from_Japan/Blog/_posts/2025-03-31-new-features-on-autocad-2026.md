---
layout: "post"
title: "AutoCAD 2026 新機能"
date: "2025-03-31 00:05:18"
author: "Toshiaki Isezaki"
categories:
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2025/03/new-features-on-autocad-2026.html "
typepad_basename: "new-features-on-autocad-2026"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860fd6bca200d-pi" style="display: inline;"><img alt="Acad2026" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860fd6bca200d img-responsive" src="/assets/image_748629.jpg" title="Acad2026" /></a></p>
<p>AutoCAD 2026 で、今日の利用環境にあわせて継続した機能の改善・改良に取り組んでいます。特に AI を使用した機能について、機械学習により精度が向上したり、より利便性の高い機能が追加されてきています。</p>
<p>簡単ですが、順にご紹介していきます。</p>
<p><strong>接続されたサポート ファイル</strong></p>
<p>AutoCAD 2026 は、Autodesk Docs プロジェクトから直接図面を開いて編集することが出来るようになっています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860e5a129200b-pi" style="display: inline;"><img alt="Docs" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860e5a129200b img-responsive" src="/assets/image_862888.jpg" title="Docs" /></a></p>
<p>[オプション] ダイアログで Autodesk Docs プロジェクト毎にサポートファイルの検索パスをセットアップすると、プロジェクト配下に<strong> .autodesk.support</strong> フォルダが作成されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860e66fb9200b-pi" style="display: inline;"><img alt="Options" class="asset  asset-image at-xid-6a0167607c2431970b02e860e66fb9200b img-responsive" src="/assets/image_964075.jpg" style="width: 640px;" title="Options" /></a></p>
<p>この<strong> .autodesk.support</strong> フォルダに図面が参照するファイルを保存しておくと、Autodesk Docs プロジェクトから図面を開く際に、見つからない参照ファイルを検索、パスを解決して図面を開くことが出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3cedd3e200c-pi" style="display: inline;"><img alt="接続されたサポート ファイル" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3cedd3e200c image-full img-responsive" src="/assets/image_247299.jpg" title="接続されたサポート ファイル" /></a></p>
<p>ローカル コンピュータから図面を開く際には、従来通り、ローカル コンピュータ内のパスを使用して参照ファイルのパスを解決します。</p>
<p><strong>アクティビティ インサイト</strong></p>
<p>[アクティビティ インサイト] パレットでアクティビティをクリックすると、[アクティビティ プロパティ] パネルに [変更点]&#0160; インサイトが表示されるようになりました。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3cfbec5200c-pi" style="display: inline;"><img alt="Activities_insight" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3cfbec5200c img-responsive" src="/assets/image_660442.jpg" title="Activities_insight" /></a></p>
<p>[変更点] のインサイトは、ローカル、LAN、および Autodesk Docs に保存されている AutoCAD 図面ファイルでのみ使用できます。現在、これらのインサイトは、AutoCAD 内で行われた保存に対してのみ生成されます。つまり、共有図面が別の製品（AutoCAD Web など）で最後に保存された場合、変更点インサイトは表示されません。また、再販不可品（NFR）、体験版、教育機関限定ライセンスではアクティビティ インサイトは利用出来ません。</p>
<p><strong>中心線 と 中心マークに 画層 オプションを追加</strong></p>
<p>システム変数 CENTERLAYER が追加されて、新しい中心線と中心マークの作成時に既定の画層を適用することが出来るようになりました。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860fca0e5200d-pi" style="display: inline;"><img alt="Center_lines-marks" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860fca0e5200d image-full img-responsive" src="/assets/image_6501.jpg" title="Center_lines-marks" /></a></p>
<p><strong>マークアップ読み込みとマークアップ アシスト</strong></p>
<p>Autodesk Docs の PDF マークアップで作成されパブリッシュされた指摘事項を、AutoCAD で表示出来るようになっています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860fca18d200d-pi" style="display: inline;"><img alt="マークアップ読み込みとマークアップ アシスト" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860fca18d200d image-full img-responsive" src="/assets/image_999540.jpg" title="マークアップ読み込みとマークアップ アシスト" /></a></p>
<p><strong>スマート ブロック</strong><strong>: </strong><strong>検索と変換</strong></p>
<p>図面内にある類似形状のジオメトリを検索してブロックに変換する作業を支援する BSEARCH[ブロック検索] コマンド（旧&#0160; BCONVERT[ブロック変換] コマンド）が強化されて、文字を認識してブロック属性に変換出来るようになっています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3cedce1200c-pi" style="display: inline;"><img alt="ブロック検出" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3cedce1200c image-full img-responsive" src="/assets/image_135599.jpg" title="ブロック検出" /></a></p>
<p><strong>スマート ブロック</strong><strong>: </strong><strong>検出と変換</strong></p>
<p>図面内の類似形状を自動検出してブロック化する BDETECT[ブロック検出] コマンド（旧 DETECT[検出] コマンド）が強化されて、より精度の高い検出が可能になっています、引き続き、テクニカル プレビュー機能としての提供ですが、機械学習を使用をしているため、多くの利用で更なる精度向上を見込むことが出来ます。ぜひ、お試しいただき、精度向上にご協力をお願いします。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860fc8d10200d-pi" style="display: inline;"><img alt="ブロック検索" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860fc8d10200d image-full img-responsive" src="/assets/image_908358.jpg" title="ブロック検索" /></a></p>
<p>By Toshiaki Isezaki</p>
