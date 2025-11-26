---
layout: "post"
title: "Inventor 2016 の新機能 その１"
date: "2015-04-20 02:30:00"
author: "Shigelazu Saitou"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/04/inventor-2016-new-product-no1.html "
typepad_basename: "inventor-2016-new-product-no1"
typepad_status: "Publish"
---

<p>Inventor 2016 製品が 2013/04/08プレス発表され、今回より昨年(2014/11/19東京・2014/11/28大阪)にて Developer Daysで紹介いたしました Inventor2016製品の新機能・強化機能 を中心に複数回に分けて、ご紹介させていただきます。<br />
<strong><br />
Ⅰ. マルチCAD環境による効率的な作業</strong></p>

<p><strong>(1)「モデルを参照」インポート機能による同期的な更新</strong></p>

<p>　一般的なCAD製品は、他のCADファイルを製品内にインポートする場合は、中間ファイルなどで「モデルを変換」して自社のCAD製品内に取り込み作業をするのが通例です。<br />
Inventor 2016 製品では、従来までのバージョンに備わっておりました 他の CAD ファイルを直接インポートして「モデルを変換」し Inventor 製品内で取り組む機能の他に、インポート機能が充実し「モデルを参照」機能が備わりました。<br />
「モデルを参照」機能の最大の特長は、他のCADのファイル( CATIA , Solidworks , NX , Pro-E / Creo , Alias など ) を関連付けした状態で Inventor 製品内に取り込む事ができる為に、Inventor製品側にCADファイルをインポート操作した後に、他のCAD上で対象のCADファイルが更新された場合、既にInventor製品内に取り込んでいたモデルも連動して自動的にInventor 製品内で更新されるといった振る舞いを持ち、一方の製品で設計データの変更がなされた場合、同期的に他が更新されます。<br />
（ 「モデルを参照」としてインポートされている場合は、Inventor 製品内ではモデルのジオメトリの変更はできません ）</p>

<p><a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08130e0a970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb08130e0a970d img-responsive" alt="Imports" title="Imports" src="/assets/image_472279.jpg" /></a><br /><br />
<strong>(2) AutoCAD DWG との同期的な連動</strong></p>

<p>　Inventor内にDWGファイルをインポートし、参照ブロックとInventor内のスケッチにスケッチと連動した他のジオメトリを関連付けした場合、DWGファイルが変更されるとスケッチが更新されます。</p>

<p><a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0f8b6cf970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b8d0f8b6cf970c img-responsive" alt="ACADRef" title="ACADRef" src="/assets/image_332547.jpg" /></a><br /></p>

<p><strong>Ⅱ.  T-Spline モデルの強化</strong><br />
 <br />
  T-Spline モデリングが大幅に強化され、以下に挙げる機能の他にも多くの機能拡張が存在します。</p>

<p><a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0f8b6ec970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b8d0f8b6ec970c img-responsive" alt="T-Spline" title="T-Spline" src="/assets/image_454930.jpg" /></a><br /><br />
開いたサーフェス のサポート<br />
フリーフォームの フェース の削除<br />
ブリッジコマンドによる２つのソリッドの結合<br />
未溶接エッジでフリーフォームモデルを分割<br />
開放サーフェスに結合するエッジをマージ<br />
既存モデルをフリーフォームジオメトリに変換<br />
厚みコマンドによるソリッド・オフセット・シェルの作成</p>

<p><strong>Ⅲ.  モデリングの強化</strong></p>

<p><strong>(1)	鋳造設計の機能強化</strong></p>

<p><strong>(ア)	新しい ルールド サーフェス フィーチャ</strong></p>

<p>ルールドサーフェスコマンドに 垂直・接線・スイープサーフェス による作成が追加。</p>

<p><strong>(イ)	サーフェスはパーティングライン分割作成、開いたサーフェスとソリッドサーフェス両方の面の置き換え、面の勾配の強化、シルエット曲線の強化に使用可能。</strong></p>

<p>ルールドサーフェスはポケットの作成・シーリング後のコンプレックスボディの分割・モールド設計の分割面の作成に使われます。<br />
	   サーフェスはパーティングライン分割の生成に使用可能です。<br />
置き換えフェースとして、フェース・ソリッドの両方の、フェースの置き換えをサポート。<br />
面の勾配では、保存されるモデルの厚さの一部を制御することができます。<br />
新しい除外オプションでは、シルエット曲線コマンドに曲線生成のより大きな制御の提供が<br />
追加されています。</p>

<p><strong>(2)	シートメタルでのマルチボディ</strong></p>

<p>特定のシートメタルフィーチャーを作成する新しいボディを作成する事ができます。<br />
（フェース ・ コンターフランジ ・ コンターロール ・ ロフトフランジ ・ 分割）<br />
フランジ ・ コーナーヘム ・ 曲げ ・ カット ・ 折り曲げ ・ パンチ  のマルチボディのサポート</p>

<p><strong>(3)	コマンド内での 曲げ半径 ゼロ のサポート</strong></p>

<p>多くのコマンド（フェース ・ 曲げ ・ フランジ ・ コンターロール ・ ロフトフランジ ・ ヘム ・ 折り曲げフィーチャー）で、半径ゼロによる曲げをサポート。</p>

<p>By Shigekazu Saito<br />
</p>
