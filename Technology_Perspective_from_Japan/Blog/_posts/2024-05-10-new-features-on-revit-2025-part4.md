---
layout: "post"
title: "Revit 2025 新機能 ～ その４"
date: "2024-05-10 01:04:31"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/05/new-features-on-revit-2025-part4.html "
typepad_basename: "new-features-on-revit-2025-part4"
typepad_status: "Publish"
---

<p>今回は、Autodesk Revit 2025 の構造設計分野に関連する新機能をご紹介いたします。</p>
<p><strong>一般的な鉄筋にパラメトリックの継手を作成する</strong></p>
<p>位置を選択して、または長さを指定して、1 つまたは複数の鉄筋セットに継手を作成し、必要に応じて作成された継手を修正します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02dad0c2b367200d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Revit2025_05_01" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02dad0c2b367200d image-full img-responsive" src="/assets/image_178896.jpg" title="Revit2025_05_01" /></a></p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3aed068200c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Revit2025-05-02" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3aed068200c image-full img-responsive" src="/assets/image_316954.jpg" title="Revit2025-05-02" /></a></p>
<ul>
<li>線分を選択して複数の鉄筋セットに継手を作成する
<ul>
<li>継手の位置を選択し、継手タイプを選択して、ラップ長さ、千鳥オフセット、鉄筋の移動を定義します。線分をクリックして、交差するすべてのセットに継手を作成します。</li>
</ul>
</li>
<li>長さに応じた鉄筋継手を作成する
<ul>
<li>すべての継手の作成結果に適用される継手タイプを選択し、ランアウトの位置、最小長さ、最大長さを入力します。クリックして鉄筋に継手を作成します。</li>
</ul>
</li>
<li>鉄筋継手を修正する
<ul>
<li>選択した継手のタイプを変更する、継手位置を切り替える、ビュー内のすべての継手線を表示する、1 つまたは複数の継手を移動、回転、削除する、などの操作ができます。</li>
</ul>
</li>
</ul>
<hr />
<p><strong>鉄筋モデルへの予期しない変更を防ぐ</strong></p>
<p>個々の鉄筋ハンドルの鉄筋の拘束を無効にして、コンクリートのジオメトリが変更される際に、鉄筋の寸法が不必要に変更されないようにブロックします。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3b2893c200b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Revit2025_05_03" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3b2893c200b image-full img-responsive" src="/assets/image_825282.jpg" title="Revit2025_05_03" /></a></p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02dad0c2b37d200d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Revit2025_05_04" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02dad0c2b37d200d image-full img-responsive" src="/assets/image_778296.jpg" title="Revit2025_05_04" /></a></p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3aed09b200c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Revit2025-05-06" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3aed09b200c image-full img-responsive" src="/assets/image_527002.jpg" title="Revit2025-05-06" /></a></p>
<ul>
<li>すべてのハンドルを無効にしたり、現在のハンドルまたはすべてのハンドルをリセットすることができます。選択した鉄筋ハンドルの拘束が無効になると、それぞれのハンドルはコンクリート面の変更に反応しなくなります。</li>
<li>拘束が無効になっている状態で、手動で鉄筋ハンドルをドラッグすることができます。</li>
<li>複数の鉄筋の拘束を編集し、すべての拘束を無効にすることができます。</li>
<li>集計表またはフィルタを使用して、鉄筋の拘束ステータスを確認することができます。</li>
</ul>
<hr />
<p><strong>鉄筋タグ付けのための曲げ形状概略</strong></p>
<p>組み込みタグで曲げ形状概略を使用して、鉄筋の曲げの手順を表示します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02dad0c2b398200d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Revit2025_05_05" class="asset  asset-image at-xid-6a0167607c2431970b02dad0c2b398200d img-responsive" src="/assets/image_919018.jpg" title="Revit2025_05_05" /></a></p>
<ul>
<li>1 つまたは複数の鉄筋を参照する曲げ形状概略を追加します。</li>
<li>曲げ形状概略に対する複数の引出線の表示設定と外観を編集します。</li>
<li>タイプ プロパティで、曲げ形状概略の幅と高さを指定します。</li>
<li>曲げ形状概略は、集計表の曲げ形状と似ており、ジオメトリはセグメント間の角度が 90 度の鉄筋形状では不均等にスケールされ、スターラップや他の角度の形状では均等にスケールされます。</li>
</ul>
<hr />
<p><strong>解析用パネルおよび部材のローカル座標系の方向</strong></p>
<p>解析用の部材およびパネルのローカル座標系ツールを使用して、軸の方向や向きを合わせます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02dad0c2b3a4200d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Revit2025_05_07" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02dad0c2b3a4200d image-full img-responsive" src="/assets/image_49571.jpg" title="Revit2025_05_07" /></a></p>
<ul>
<li>複数の解析用部材の X 軸方向を同時に変更または位置合わせすることができます。</li>
<li>参照線を使用して X 軸方向を位置合わせしたり、複数の解析用パネルの Z 軸方向を同時に位置合わせすることができます。</li>
<li>荷重、境界条件、結果などの方向のコントロールをサポートします。</li>
<li>相互運用性を実現するために、構造解析ソフトウェアとの一貫性をサポートします。</li>
<li>解析要素の方向を明確に理解し、構造解析結果を正確に解釈できるようにします。</li>
</ul>
<hr />
<p><strong>鉄骨接合のあるフレームおよび柱の分割機能</strong></p>
<p>[分割]ツールと[ギャップを使用して分割]ツールが、鉄骨接合のあるフレームおよび柱に使用できるようになりました。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02dad0c2b3b3200d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Revit2025_05_08" class="asset  asset-image at-xid-6a0167607c2431970b02dad0c2b3b3200d img-responsive" src="/assets/image_720438.jpg" title="Revit2025_05_08" /></a></p>
<ul>
<li>[ギャップを使用して分割]は、梁システム、トラス、またはグループの一部である梁には適用されません。</li>
<li>[ギャップを使用して分割]コマンドで使用できるギャップは、1.6 mm ～ 304.8 mm です。</li>
</ul>
<hr />
<p>By Ryuji Ogasawara</p>
