---
layout: "post"
title: "モデルのフェースを使いサーフェスを作成する方法"
date: "2013-06-03 04:39:52"
author: "Shigelazu Saitou"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2013/06/inventor2014_createsurfacefromface.html "
typepad_basename: "inventor2014_createsurfacefromface"
typepad_status: "Publish"
---

<p>以前、<a href="http://adndevblog.typepad.com/technology_perspective/2013/04/inventor2013-api%E6%A9%9F%E8%83%BD%E7%B4%B9%E4%BB%8Bdeveloper-days-%E6%8A%9C%E7%B2%8B.html">Inventor2013 API機能トピックス(Developer Days 抜粋)</a>で、クライアントグラフィックス機能（「クライアントフィーチャーオブジェクト」）を利用する事で、他のパーツファイルなどのフィーチャーやオブジェクトを実体化せずに、構成されるサーフェス形状を「クライアントグラフィックス」のデータとして表現し、マウスやAPIにより一般のフェースやノードと同じようにAPIによるフェースオブジェクトとしてのハンドリングが可能になりました事を、ご案内して参りました。</p>

<p>これは「クライアントグラフィックス」であるがゆえに、最大の特徴として実際には実行中のInventorプロセスや、保存時のファイルサイズの大きさに変化をもたらすことなく、実体のあるフェースオブジェクト同様にAPIによる目的のフェース情報のハンドリングが可能となっています。<br />
（カスタマイズ時のハンドリングロジックを始め、ファイルのロード時間やPCのメモリ節約にも良い影響を与える機能としてご利用いただけます）</p>

<p>しかし、これと同じように、単純にモデル内フィーチャーのフェースを選択し、選択されたフェース情報でサーフェスオブジェクトそのものを、モデルと同じ空間に実体化したい場合の要求も多く存在します。</p>

<p>今回は、「イベントを使いモデル内の「面」を選択させた後に、面情報を使いサーフェスを実体化させ、モデルと同じ空間に存在させる方法」についてサンプルコードと共に、ご紹介させていただきます。<br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019102e435b8970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b019102e435b8970c" alt="Fig1" title="Fig1" src="/assets/image_702972.jpg" /></a><br /> </p>

<p><a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019102e4363a970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b019102e4363a970c" alt="Fig3" title="Fig3" src="/assets/image_831568.jpg" /></a><br /> </p>

<p>Option Explicit<br />
<strong>Public Sub CreateSurface()</strong><br />
  ' 選択用クラスを初期化<br />
  Dim oSelect As New clsSelect<br />
  Dim oDoc As PartDocument<br />
  Set oDoc = ThisApplication.ActiveDocument<br />
  If oDoc Is Nothing Then<br />
      MsgBox "パーツドキュメントを開いてください"<br />
      Exit Sub<br />
  End If<br />
  <br />
  '　イベントを使ってパーツファイル内のフェースオブジェクトを選択し獲得<br />
  '  選択用フィルタは、パーツフェースフィルタ（kPartFaceFilter）<br />
  Dim oFace As Face<br />
  Set oFace = oSelect.PickObj(kPartFaceFilter)<br />
  <br />
  ' パーツコンポーネント定義変数<br />
  Dim oDef As PartComponentDefinition<br />
  Set oDef = oDoc.ComponentDefinition<br />
  <br />
  ' サーフェスオブジェクト定義を用意<br />
  Dim oFeatureDef As NonParametricBaseFeatureDefinition<br />
  Set oFeatureDef = oDef.Features.NonParametricBaseFeatures.CreateDefinition<br />
  <br />
  ' 選択されたフェースをオブジェクトコレクションにセット<br />
  Dim oCollection As ObjectCollection<br />
  Set oCollection = ThisApplication.TransientObjects.CreateObjectCollection<br />
  oCollection.Add oFace</p>

<p>  ' 作成したフェースのコレクション情報を使ってサーフェスオブジェクト定義を作成<br />
  oFeatureDef.BRepEntities = oCollection<br />
  oFeatureDef.OutputType = kSurfaceOutputType</p>

<p>  ' サーフェスオブジェクト定義をNonParametricBaseFeaturesコレクションにフィーチャーとして追加作成<br />
  Dim oBaseFeature As NonParametricBaseFeature<br />
  Set oBaseFeature = oDef.Features.NonParametricBaseFeatures.AddByDefinition(oFeatureDef)<br />
  <br />
<strong>End Sub</strong></p>

<p><strong><<<<<<  clsSelect クラスモジュール Begin >>>>>></strong></p>

<p>Private WithEvents oInteraction As InteractionEvents<br />
Private WithEvents oSelect As SelectEvents<br />
' 選択終了のときの決定に使うフラグの宣言<br />
Private bStillSelecting As Boolean<br />
Private oObj As Object</p>

<p>Public Function PickObj(ByRef oFillter As SelectionFilterEnum) As Object<br />
    bStillSelecting = True  ' フラグの初期化<br />
    Set oInteraction = ThisApplication.CommandManager.CreateInteractionEvents<br />
    <br />
    '選択用のフィルターをセット<br />
    Set oSelect = oInteraction.SelectEvents<br />
    oSelect.AddSelectionFilter oFillter</p>

<p>     oSelect.SingleSelectEnabled = True      '一度に１つのエンティティの選択を許す.<br />
    <br />
    oInteraction.Start       'interaction イベントの開始.<br />
    Do While bStillSelecting<br />
      ThisApplication.UserInterfaceManager.DoEvents<br />
    Loop<br />
    oInteraction.Stop        'イベントの終了<br />
    <br />
    ' イベントのクリーンアップ.<br />
    Set oSelectEvents = Nothing<br />
    Set oInteractEvents = Nothing<br />
    ' 選択されたオブジェクトを関数の返り値にセット<br />
    Set PickObj = oObj<br />
End Function</p>

<p>Private Sub oSelect_OnSelect(ByVal JustSelectedEntities As ObjectsEnumerator, _<br />
          ByVal SelectionDevice As SelectionDeviceEnum, ByVal ModelPosition As Point, _<br />
          ByVal ViewPosition As Point2d, ByVal View As View)<br />
    <br />
    ' 選択されたオブジェクト<br />
    Set oObj = JustSelectedEntities.Item(1)<br />
    <br />
    bStillSelecting = False    '終了フラグセット<br />
End Sub</p>

<p><strong><<<<<<  clsSelect クラスモジュール End >>>>>></strong></p>

<p>サンプルマクロは <span class="asset  asset-generic at-xid-6a0167607c2431970b0192aaac7c63970d"><a href="http://adndevblog.typepad.com/files/createsurface.ivb">ここ</a></span> よりダウンロードできます。</p>

<p>By Shigekazu Saito.<br />
</p>
