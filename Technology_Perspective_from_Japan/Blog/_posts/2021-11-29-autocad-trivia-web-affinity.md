---
layout: "post"
title: "AutoCAD 雑学：Web 親和性"
date: "2021-11-29 00:04:43"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/11/autocad-trivia-web-affinity.html "
typepad_basename: "autocad-trivia-web-affinity"
typepad_status: "Publish"
---

<p>AutoCAD は Web に親和性を持っています。製品内部に <strong><a href="https://ja.wikipedia.org/wiki/WebKit" rel="noopener" target="_blank">WebKit</a>&#0160;</strong>を採用しているので、 [スタート] タブが表示されている状態でキーボードの [F12] キーを押すと、Web ブラウザのときと同じように、<a href="https://adndevblog.typepad.com/technology_perspective/2018/10/about-developer-tool-on-web-browser.html" rel="noopener" target="_blank"><strong>デベロッパーツール </strong></a>が表示されるはずです。</p>
<p>[スタート] タブ内のコンテンツはカスタマイズ対象にはなっていませんが、HTML や CSS が使用されていることがわかります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b027880593c4a200d-pi" style="display: inline;"><img alt="Webkit" border="0" class="asset  asset-image at-xid-6a0167607c2431970b027880593c4a200d image-full img-responsive" src="/assets/image_432242.jpg" title="Webkit" /></a></p>
<p>AutoCAD .NET API でパレット インタフェースを使用する場面では、パレット内に URL 指定した Web ページを表示させることも容易です。次のコードは、以前、<a href="https://adndevblog.typepad.com/technology_perspective/2020/11/timeliner-information.html" rel="noopener" target="_blank"><strong>Model Derivative API：Timeliner 情報の出力</strong></a> でご紹介した Web ページを表示させるものです。</p>
<div>
<blockquote>
<div><span style="font-size: 10pt; font-family: tahoma, arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; // Decraration variables for PalleteSet</span></div>
<div><span style="font-size: 10pt; font-family: tahoma, arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; static PaletteSet ps = null;</span></div>
<div>&#0160;</div>
<div><span style="font-size: 10pt; font-family: tahoma, arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; // Modal Command with localized name</span></div>
<div><span style="font-size: 10pt; font-family: tahoma, arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; [CommandMethod(&quot;MyPalette&quot;)]</span></div>
<div><span style="font-size: 10pt; font-family: tahoma, arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; public void MyPalette() // This method can have any name</span></div>
<div><span style="font-size: 10pt; font-family: tahoma, arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; {</span></div>
<div><span style="font-size: 10pt; font-family: tahoma, arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; if (ps == null)</span></div>
<div><span style="font-size: 10pt; font-family: tahoma, arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></div>
<div><span style="font-size: 10pt; font-family: tahoma, arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ps = new Autodesk.AutoCAD.Windows.PaletteSet(&quot;Find Room&quot;);</span></div>
<div><span style="font-size: 10pt; font-family: tahoma, arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ps.Style = PaletteSetStyles.ShowPropertiesMenu |</span></div>
<div><span style="font-size: 10pt; font-family: tahoma, arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; PaletteSetStyles.ShowAutoHideButton |</span></div>
<div><span style="font-size: 10pt; font-family: tahoma, arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; PaletteSetStyles.ShowCloseButton |</span></div>
<div><span style="font-size: 10pt; font-family: tahoma, arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; PaletteSetStyles.Snappable;</span></div>
<div><span style="font-size: 10pt; font-family: tahoma, arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ps.Visible = true;</span></div>
<div><span style="font-size: 10pt; font-family: tahoma, arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></div>
<div><span style="font-size: 10pt; font-family: tahoma, arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; else</span></div>
<div><span style="font-size: 10pt; font-family: tahoma, arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></div>
<div><span style="font-size: 10pt; font-family: tahoma, arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ps.Visible = true;</span></div>
<div><span style="font-size: 10pt; font-family: tahoma, arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></div>
<div>&#0160;</div>
<div><span style="font-size: 10pt; font-family: tahoma, arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; // Create Pallete</span></div>
<div><span style="font-size: 10pt; font-family: tahoma, arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; if (ps.Count == 0)</span></div>
<div><span style="font-size: 10pt; font-family: tahoma, arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></div>
<div><span style="font-size: 10pt; font-family: tahoma, arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; Uri url = new Uri(&quot;https://forge-viewer-md-options.herokuapp.com/&quot;);</span></div>
<div><span style="font-size: 10pt; font-family: tahoma, arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; Autodesk.AutoCAD.Windows.Palette p = ps.Add(&quot;Find Room&quot;, url);</span></div>
<div><span style="font-size: 10pt; font-family: tahoma, arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ps.Dock = DockSides.None;</span></div>
<div><span style="font-size: 10pt; font-family: tahoma, arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></div>
<div><span style="font-size: 10pt; font-family: tahoma, arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; }</span></div>
<div><span style="font-size: 10pt; font-family: tahoma, arial, helvetica, sans-serif;">&#0160; &#0160; }</span></div>
</blockquote>
</div>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e134b8ca200b-pi" style="display: inline;"><img alt="Palette2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e134b8ca200b image-full img-responsive" src="/assets/image_784339.jpg" title="Palette2" /></a></p>
<p>上の例では、Forge Viewer を持つ Web ページ表示させていますが、あくまで例としての紹介なので、この内容に特に意味はありません。ただ、アイデア次第では、コラボレーションや作図支援などの外部とのインタフェースとして、その可能性を見出していただけるはずです。</p>
<p>By Toshiaki Isezaki</p>
