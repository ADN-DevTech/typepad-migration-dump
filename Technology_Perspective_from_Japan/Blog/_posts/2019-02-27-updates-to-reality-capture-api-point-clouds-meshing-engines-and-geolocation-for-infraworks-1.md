---
layout: "post"
title: "Reality Capture API アップデート"
date: "2019-02-27 00:07:17"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2019/02/updates-to-reality-capture-api-point-clouds-meshing-engines-and-geolocation-for-infraworks-1.html "
typepad_basename: "updates-to-reality-capture-api-point-clouds-meshing-engines-and-geolocation-for-infraworks-1"
typepad_status: "Publish"
---

<p>ご案内が遅くなってしまいましたが、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2018/11/devcon-2018-forge-roadmap.html" rel="noopener" target="_blank">Forge DevCon Las Vegas 2018 アップデート: Forge ロードマップ</a></strong> でご案内したとおり、DevCon Las Vegas 2018 でアナウンスのあった Reality Capture API のアップデートが完了していますので、改めてご案内しておきたいと思います。</p>
<p>Reality Solutions チームは、建設プロジェクトを念頭に設計し直した次世代の Forge Reality Capture API の提供を開始しました。新機能の概要は次のとおりです。</p>
<ol>
<li><strong>点群（Point cloud）</strong>: <br />ゼロから作られた真新しいマルチビュー ステレオ エンジンを搭載しています。 この新しいエンジンは、従来のエンジンより、より詳細で精度が向上した高密度の高い点群を再構築することができます。:
<ol style="list-style-type: lower-alpha;">
<li>建物の屋根や側面のエッジが、復元中、維持されます。</li>
<li>構造梁やパイプのような小さな部品は点群に保存されます。 これにより、石油精製工場のパイプ、構造用鋼製の柱および梁、フェンシングなど、多くの細いパーツを持つ点群の品質が大幅に向上します。&#0160;</li>
<li>点群の範囲と精度が向上し、より正確になっています。 下記のベンチマークを参照してください。</li>
</ol>
</li>
<li><strong>メッシュ エンジン:</strong> <br />新エンジンは、より高い品質とより高速で並列メッシュ生成を可能にしています。将来的には、より大規模なデータセット処理出来るよう準備されています。<br />
<ol style="list-style-type: lower-alpha;">
<li>より完全なメッシュモデルを提供します。再構築されたシーンは、通常、オクルージョン/テクスチャのないサーフェス/特定のビューからの欠落した写真が原因で欠けた領域が生成されてきました。新しいメッシュ エンジンは、新しいマルチビュー ステレオ エンジンからの正確な位置と法線情報から欠落領域を補完します。</li>
</ol>
</li>
<li><strong>オルソ画像品質の改善:</strong><br />
<ol style="list-style-type: lower-alpha;">
<li>屋根や建物のエッジが、より明確で正確になっています。</li>
<li>より正確な 3D モデルの利用で再投影エラーが少なくなるため、薄い部分はオルソ画像でより正確になっています。</li>
</ol>
</li>
<li><strong>Autodesk InfraWorks® 用地理的情報のサポート:</strong><br />
<ol style="list-style-type: lower-alpha;">
<li>メッシュモデル（OBJとFBX）は、InfraWorks<sup>®</sup> にインポートされると自動的にジオロケーションされるようになっています。</li>
</ol>
</li>
</ol>
<table width="853">
<tbody>
<tr>
<td width="427"><strong>旧エンジン</strong></td>
<td width="427"><strong>新エンジン</strong></td>
</tr>
<tr>
<td colspan="2" width="853"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad39f0975200c-pi" style="display: inline;"><img alt="Comparison" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad39f0975200c image-full img-responsive" src="/assets/image_930861.jpg" style="float: left;" title="Comparison" /></a></td>
</tr>
</tbody>
</table>
<p>旧エンジンと新エンジンを比較した初期のベンチマーク結果は次のとおりです。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad39f0ad7200c-pi" style="display: inline;"><img alt="Comparison_table" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad39f0ad7200c image-full img-responsive" src="/assets/image_860539.jpg" title="Comparison_table" /></a></p>
<p>新しいエンジンを利用するには、POST / photoscene endpoint でフォトシーンを作成するときに追加の version=2.0フォームパラメータを指定します。 詳細は <strong><a href="https://forge.autodesk.com/en/docs/reality-capture/v1/reference/http/photoscene-POST/#form-parameters" rel="noopener" target="_blank">API リファレンス</a></strong>を参照してください。</p>
<p>評価結果のフィードバックをお持ちであれば <strong><a href="https://stackoverflow.com/questions/tagged/autodesk-realitycapture">こちら</a></strong> まで、ぜひお送りください。&#0160;</p>
<p>By Toshiaki Isezaki</p>
