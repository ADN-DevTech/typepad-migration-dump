---
layout: "post"
title: "Inventor2013 API機能紹介(Developer Days 抜粋)"
date: "2013-04-08 02:30:00"
author: "Shigelazu Saitou"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2013/04/inventor2013-api%E6%A9%9F%E8%83%BD%E7%B4%B9%E4%BB%8Bdeveloper-days-%E6%8A%9C%E7%B2%8B.html "
typepad_basename: "inventor2013-api機能紹介developer-days-抜粋"
typepad_status: "Publish"
---

<p>オートデスクでは毎年、各製品の新バージョンがリリースされる約半年前に、ADN（Autodesk Developer Network）メンバー様のアプリケーション開発に携わるエンジニア様を対象に、API中心の新機能や強化機能の情報を提供しています。<br />
　今回、一昨年(2011/11/17東京・2011/11/23大阪)で開催された Inventor2013製品のAPIの機能紹介（新機能・強化機能）の中より、特にアプリケーション開発に有益なAPI機能を中心に幾つか抜粋してご案内します。<br />
　記事の最後には、これら機能を抜粋して再レコードしたビデオをポストしています。<br />
また、デモ内で使用しているマクロやファイルなどは、<span class="asset  asset-generic at-xid-6a0167607c2431970b017d429e7034970c"><a href="http://adndevblog.typepad.com/files/adn2013demo.zip">ここ</a></span> よりダウンロードしてご利用する事ができます。</p>

<p>1. ユーザーインターフェース<br />
ユーザーインターフェースの強化として、ミニツールバー内のスライダーコントロールのサポート。<br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c386f564c970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b017c386f564c970b" alt="Fig1" title="Fig1" src="/assets/image_947917.jpg" border="0" /></a> <br />
Inventor2012製品より、ユーザーインターフェースにリボンが採用され、マウス位置に連動したラジアルマーキングメニュー・リニアメニュー・ミニツールバーメニュー（自動フェード機能付き）がAPIによりサポートされましたが、ミニツールバーメニュー内に新たにスライダーコントロールがAPIによりサポートされました。</p>

<p>2. マルチボディのパーツファイル内での ボディ移動<br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c386f5756970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b017c386f5756970b image-full" alt="Fig5" title="Fig5" src="/assets/image_370613.jpg" border="0" /></a><br />
「ボディを移動」機能の作成と編集をAPIでサポート。<br />
マルチボディパーツファイル内の任意のボディを「フリードラッグ」・「X/Y/Z方向への直線状に移動」・「回転」操作が可能となっています。</p>

<p>3. ジオメトリへのAPI強化<br />
・全てのTransient Geometry Object *1のCopyメソッドのサポート。<br />
・2D及び3Dの Transient Geometry Curveより、ストロークの獲得。<br />
・2D及び 3DのTransient Geometry Splineから部分的な曲線の抽出。<br />
・2D及び3D のTransient Geometry Splineの分割。<br />
・サーフェスより ISO 曲線の抽出.<br />
・2Dパラメータの空間曲線と等価な3D曲線を得る。<br />
・3Dポイントでサーフェスの法線を得る。<br />
　  ( *1 Transient Geometry Object: 一時的な計算に用いる幾何オブジェクト )</p>

<p>APIにより、モデルの曲面のサーフェスを使って、UあるいはV方向のいずれかのパラメータ値により、サーフェス上の曲面に沿ってISOパラメトリック曲線をTransient Geometry Objectとして取り出し、3Dスケッチを作成し、その中に3Dスケッチカーブを作成します。<br />
サーフェスのパラメータの空間に存在する2D曲線を得て、一時的な幾何曲線として等価な3D曲線を返したり、モデル空間の示される3Dポイントから、サーフェスの法線を返す事ができます。<br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017d429e6c57970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b017d429e6c57970c image-full" alt="Fig6" title="Fig6" src="/assets/image_254838.jpg" border="0" /></a><br />
（左図は、APIにより3Dスプライン上の近似値点を発生させ、クライアントグラフィックスで表現したものであり、右図は2Dスケッチ上の2Dスプラインのハンドリングしたものです。）</p>

<p>3D曲線を使い、カーブ高さの許容値を入力し、曲線状の近似点を計算させたり、2Dスケッチ上のスプライン曲線を、Transient 2D スプライン曲線に２分割したり、2Dスケッチ上のスプライン曲線からジオメトリを読み込んで、Transient Curveとした後に３分割するハンドリングが可能です。<br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017d429e6ceb970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b017d429e6ceb970c image-full" alt="Fig7" title="Fig7" src="/assets/image_519293.jpg" border="0" /></a><br />
（左図は、サーフェスの曲面上に沿ったISOパラメトリック曲線をTransient Geometry Objectとして取り出し3Dスケッチ上に作成したものであり、右図は、サーフェスの面に沿って、クライアントグラフィックスにて、円と５つの線分を作図したものです。）</p>

<p>4．Transient B-rep<br />
・ワイアーボディの作成。（ワイアーフレームジオメトリをB-Repとして定義する事が可能）<br />
・平面ワイアーボディのオフセット<br />
・SATもしくはDWG出力用としても利用可能。</p>

<p>ワイアーボディはワイアーフレームジオメトリをB-Repとして定義する方法です。<br />
エッジと頂点で接続されたワイアーフレームジオメトリから成り立っています。<br />
入力としてワイアーボディを使用する事で、ルールドサーフェースが作成できます。<br />
平面ワイアーボディはオフセットする事ができます。<br />
Transient B-RepはSATまたはDWGとしても書き出す事ができます。</p>

<p><a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017d429e6da9970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b017d429e6da9970c image-full" alt="Fig8" title="Fig8" src="/assets/image_670334.jpg" border="0" /></a><br />
（図は、一辺が円弧であり、対する辺が２直線で構成されるワイアーボディを使って「ルールドサーフェース」をAPIで作成したものです。）<br />
 <br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c386f5a35970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b017c386f5a35970b image-full" alt="Fig9" title="Fig9" src="/assets/image_250315.jpg" border="0" /></a><br />
（図は、フェースのエッジを見つけワイアーボディを作成し、移動コピーや、コピーしたワイアーをオフセットしてできたワイアーを使ってテーパー上のサーフェスを作成したものです）</p>

<p>5．クライアントグラフィックス<br />
・ダイレクトにサポートされた「クライアントフィーチャーオブジェクト」<br />
　（これにより、他のパーツファイルなどのフィーチャーやオブジェクトを配置や読み込まずに、構成されるサーフェス形状を「クライアントグラフィックス」のデータとして表現可能となる）<br />
・クライアントグラフィックスのプリミティブが選択可能。<br />
（これにより、表現されているクライアントグラフィックスを、一般のフェースやノードとして選択する事も可能となる）<br />
B-RepコンポーネントはSurfaceGraphicsインスタンスを構築。<br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017eea12aa0b970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b017eea12aa0b970d image-full" alt="Fig10" title="Fig10" src="/assets/image_211458.jpg" border="0" /></a> </p>

<p>デモ ( HD画質 でご覧いただけます)<br />
<iframe width="500" height="281" src="http://www.youtube.com/embed/anUmaqpPgQ0?feature=oembed" frameborder="0" allowfullscreen></iframe>&nbsp;<br />
デモ内で使用しているマクロやファイルなどは、<span class="asset  asset-generic at-xid-6a0167607c2431970b017d429e7034970c"><a href="http://adndevblog.typepad.com/files/adn2013demo.zip">ここ</a></span> よりダウンロードしてご利用する事ができます。</p>

<p>次回より、Inventor2014製品のU.Sバージョン発売日を過ぎる予定ですので、「Inventor2014の新機能やAPIの情報」を中心にお届けする予定です。<br />
</p>
