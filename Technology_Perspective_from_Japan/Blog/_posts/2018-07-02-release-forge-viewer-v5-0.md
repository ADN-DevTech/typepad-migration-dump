---
layout: "post"
title: "Forge Viewer バージョン 5.0 リリース"
date: "2018-07-02 00:03:47"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2018/07/release-forge-viewer-v5_0.html "
typepad_basename: "release-forge-viewer-v5_0"
typepad_status: "Publish"
---

<p>概説になってしまいますが。新しくリリースされている Forge Viewer バージョン&#0160; 5.0 の新機能をご紹介しておきます。&#0160;</p>
<p><strong>変更点</strong></p>
<ul>
<li><em>three.min.js</em>&#0160;と&#0160;<em>three.js</em>&#0160;ライブラリが、それぞれ&#0160;<em>viewer3D.min.js</em>&#0160;と&#0160;<em>viewer3D.js</em>&#0160;にバンドルされるようになったため、両者を個別参照する必要がなくなりました。</li>
<li>設定パネルの表示とサイジングが改善されています。</li>
<li>AutoCAD や Inventor などで利用されている Protein マテリアル（Fusion 360 は Prism マテリアル）のいくつかで、フォンシェーダの斜面角で反射率を低下するようにしています。</li>
<li>PNG / JPG画像ファイルで100％以上ズーム出来るようになりました。</li>
<li><em>Autodesk.Viewing.Viewer3D.load()</em> の <em>loadOptions</em> で <em>applyRefPoint</em> オプションの既定値が false になりました。</li>
<li><em>Autodesk.Viewing.theHotkeyManager</em> が <a href="https://developer.autodesk.com/en/docs/viewer/v2/reference/javascript/hotkeymanager/" rel="noopener noreferrer" target="_blank"><em>viewer.getHotkeyManager</em></a> に置き換えられました。&#0160;</li>
<li><em>HotkeyManager.KEYCODES</em> が <em>Autodesk.Viewing.KeyCode</em> に置き換えられました。</li>
<li>IFC ファイルの変換は AEC モデルとして扱われるようになりました。&#0160;</li>
</ul>
<p><strong>新機能</strong></p>
<ul>
<li>2D モデルにモノクロモードが追加されました。：<em>viewer.setGrayscale(true)</em></li>
<li>&#0160;AEC Extension が追加されました。
<ul>
<li>この実装は初めての試みであり、Extension を完全に機能させるためには、SectionTool と BlendShaderに適応する必要があります。</li>
<li>この実装では、また JavaScript を使用した Extension の構築と WebPackでのパッケージ方法も示しています。</li>
</ul>
</li>
</ul>
<ul>
<li>設定パネルの右下にビューア バージョンの表示が追加されまし。<br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b022ad37ca3f8200d-pi" style="display: inline;"><img alt="Version_display" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad37ca3f8200d image-full img-responsive" src="/assets/image_467302.jpg" title="Version_display" /></a></li>
<li>ビュー遷移によって使用される様々なクロス フェード ロジックが追加されました。</li>
<li>ノード ボックス キャッシングを無効にするオプションが追加されました。</li>
<li>ナビゲーション メソッドと共に優先度値 <em>wheelSetsPivot</em> が追加されました。</li>
<li>ゴースト シェイプにエッジ カラー/不透明度が追加されました。</li>
<li><em>RenderContext</em> と <em>blend_frag.glsl</em>&#0160;<em>&#0160;</em>に空間フィルタ サポートが追加されました。</li>
<li>環境名が日本語化されました。<br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b022ad39c8946200b-pi" style="display: inline;"><img alt="Localized_ibl_text" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad39c8946200b image-full img-responsive" src="/assets/image_242653.jpg" title="Localized_ibl_text" /></a></li>
<li><em>config3d.modelBrowserExcludeRoot</em> を公開してモデル ブラウザにモデルのルートノードを表示するかどうかを設定することが出来ます（既定では表示されません）。</li>
<li>モデル ブラウザの最上位のノードが折りたたまれているかどうかを設定するため、<em>config3d.modelBrowserStartCollapsed</em> が公開されました（既定では no、別目：expanded）</li>
</ul>
<p><strong>削除された点</strong>&#0160;</p>
<ul>
<li>コメントとビルボード Extension の削除</li>
<li>デバッグにのみ使用されるコードの削除</li>
<li>In-viewer search（ビューア内）&#0160; Extension の削除</li>
<li>初期の一人称（First Person ツール）の切り替えを削除</li>
</ul>
<p>By Toshiaki Isezaki</p>
