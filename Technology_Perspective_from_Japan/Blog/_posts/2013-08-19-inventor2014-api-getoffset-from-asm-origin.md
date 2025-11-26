---
layout: "post"
title: "Inventor2014 APIで「親アセンブリ原点からの現在のオフセット」の情報を取り出す方法"
date: "2013-08-19 12:32:47"
author: "Shigelazu Saitou"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2013/08/inventor2014-api-getoffset-from-asm-origin.html "
typepad_basename: "inventor2014-api-getoffset-from-asm-origin"
typepad_status: "Publish"
---

<p><a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019104d6b8c1970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b019104d6b8c1970c" alt="SubAsm" title="SubAsm" src="/assets/image_291090.jpg" /></a><br /><br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0192aca0208a970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b0192aca0208a970d" alt="SubPart" title="SubPart" src="/assets/image_282687.jpg" /></a><br /></p>

<p>アセンブリファイルに配置されたオカレンスの位置情報は、<a href="http://tech.autodesk.jp/faq/faq/adsk_result_dd.asp?QA_ID=8359">QA-8359 「アセンブリファイルに配置されたオカレンスの位置情報を取得する方法」</a>で紹介されている通りで、本来ComponentOccurrenceのTransformationであるマトリクスオブジェクトに格納されているTranslationプロパティ情報は、トップアセンブリから見ての相対座標が格納されています。</p>

<p>しかし、サブアセンブリなどにポジションリプリゼンテーション”が設定されている場合は、サブアセンブリのオカレンス情報「oSubAsmOccur.Definition.Occurrence.Item()」でオカレンスの位置を取得した場合、現在の位置ではなく、サブアセンブリドキュメント内の位置（ポジションリプリゼンテーションのマスター位置）を返し、同時にリプレゼンテーションでポジションが移動している場合のサブアセンブリパーツ位置（相対座標）を取得できず、この場合は、絶対座標になってしまいます。</p>

<p>ポジションリプリゼンテーションが設定されている場合では、直接オカレンス側ではなくサブオカレンス側のマトリクスを使い、更に移動後に反転などの計算に用いなければ求められません。<br />
（計算後のマトリクスを与える必要があります。）</p>

<p>一方、”iPropart”の”オカレンス”タブ内に表示される”親アセンブリ原点からの現在のオフセット”内の「角度」情報は、Inventor製品側のオカレンスタブ内の「コアプログラム内」でこのComponentOccurrenceのTransformationであるマトリクスオブジェクト内のベクトルオブジェクト（Vector）を用いて「内積」による角度計算をして表示しています。</p>

<p>APIでは以下のサンプルコードのようにMatrixから角度計算にて求められます。</p>

<p>注意しなければならないのは、角度の計算に伴う符号検査の中で、値の符号を取り出すための Sgn 関数の取り扱いに注意が必要です。</p>

<p>number > 0 　　　1 <br />
number = 0 　　　0 <br />
number < 0 　　　-1 </p>

<p>つまり、検査したMatrixの値が"0"の場合、戻りが0となり、直後に乗算に用いていますので、２π（=180度）の値も、結果として "0"ラジアンにして　デグリ変換しているので　0 度として現れますので、判定が必要となります。</p>

<p><br />
<strong>Public Function Acos(value) As Double</strong><br />
  Acos = Math.Atn(-value / Math.Sqr(-value * value + 1)) + 2 * Math.Atn(1)<br />
<strong>End Function</strong></p>

<p><br />
<strong>Sub CalculateRotationAngles(ByVal oMatrix As Inventor.Matrix, ByRef aRotAngles() As Double)</strong><br />
 <br />
  Const PI = 3.14159265358979<br />
  Const TODEGREES As Double = 180 / PI<br />
  <br />
  Dim oApp As Inventor.Application<br />
  Set oApp = ThisApplication<br />
  <br />
  Dim dB As Double<br />
  Dim dC As Double<br />
  Dim dNumer As Double<br />
  Dim dDenom As Double<br />
  Dim dAcosValue As Double<br />
      <br />
  Dim oRotate As Inventor.Matrix<br />
  Dim oAxis As Inventor.Vector<br />
  Dim oCenter As Inventor.Point<br />
  <br />
  Set oRotate = oApp.TransientGeometry.CreateMatrix<br />
  Set oAxis = oApp.TransientGeometry.CreateVector<br />
  Set oCenter = oApp.TransientGeometry.CreatePoint<br />
  <br />
  oCenter.X = 0<br />
  oCenter.Y = 0<br />
  oCenter.Z = 0<br />
  <br />
  ' Xに関するaRotAngles[0]の選択、X-Z平面上に軸を変換する<br />
  dB = oMatrix.Cell(2, 3)<br />
  dC = oMatrix.Cell(3, 3)</p>

<p>  dNumer = dC<br />
  dDenom = Sqr(dB * dB + dC * dC)</p>

<p>  '符号検査用の値<br />
  Dim oFigou As Integer<br />
  <br />
  If (Abs(dDenom) <= 0.000001) Then<br />
    aRotAngles(0) = 0#<br />
  Else<br />
    If (dNumer / dDenom >= 1#) Then<br />
      dAcosValue = 0#<br />
    Else<br />
      If (dNumer / dDenom <= -1#) Then<br />
        dAcosValue = PI<br />
      Else<br />
        dAcosValue = Acos(dNumer / dDenom)<br />
      End If<br />
    End If<br />
    <br />
    ' 符号の検査<br />
    oFigou = 1<br />
    If (Sgn(dB) < 0) Then<br />
      oFigou = -1<br />
    End If</p>

<p>    aRotAngles(0) = oFigou * dAcosValue<br />
    oAxis.X = 1<br />
    oAxis.Y = 0<br />
    oAxis.Z = 0</p>

<p>    Call oRotate.SetToRotation(aRotAngles(0), oAxis, oCenter)<br />
    Call oMatrix.PreMultiplyBy(oRotate)<br />
  End If</p>

<p>  If (oMatrix.Cell(3, 3) >= 1#) Then<br />
      dAcosValue = 0#<br />
  Else<br />
    If (oMatrix.Cell(3, 3) <= -1#) Then<br />
      dAcosValue = PI<br />
    Else<br />
      dAcosValue = Acos(oMatrix.Cell(3, 3))<br />
    End If<br />
  End If<br />
    <br />
  ' 符号の検査<br />
  oFigou = 1<br />
  If ((-oMatrix.Cell(1, 3)) < 0) Then<br />
    oFigou = -1<br />
  End If<br />
  <br />
  aRotAngles(1) = oFigou * dAcosValue<br />
  oAxis.X = 0<br />
  oAxis.Y = 1<br />
  oAxis.Z = 0<br />
  Call oRotate.SetToRotation(aRotAngles(1), oAxis, oCenter)<br />
  Call oMatrix.PreMultiplyBy(oRotate)<br />
  <br />
  If (oMatrix.Cell(1, 1) >= 1#) Then<br />
    dAcosValue = 0#<br />
  Else<br />
    If (oMatrix.Cell(1, 1) <= -1#) Then<br />
      dAcosValue = PI<br />
    Else<br />
      dAcosValue = Acos(oMatrix.Cell(1, 1))<br />
    End If<br />
  End If<br />
  <br />
  ' 符号の検査<br />
  oFigou = 1<br />
  If ((-oMatrix.Cell(2, 1)) < 0) Then<br />
    oFigou = -1<br />
  End If<br />
  <br />
  aRotAngles(2) = oFigou * dAcosValue<br />
  <br />
  aRotAngles(0) = aRotAngles(0) * TODEGREES<br />
  aRotAngles(1) = aRotAngles(1) * TODEGREES<br />
  aRotAngles(2) = aRotAngles(2) * TODEGREES<br />
  <br />
  Debug.Print "角度(deg) : " & aRotAngles(0) & " ," _<br />
    & aRotAngles(1) & ", " & aRotAngles(2) & vbCrLf<br />
    <br />
<strong>End Sub</strong></p>

<p><br />
<strong>Sub GetAngles()</strong><br />
    Dim oDoc As AssemblyDocument<br />
    Set oDoc = ThisApplication.ActiveDocument<br />
    Dim oOcc As ComponentOccurrence<br />
    Set oOcc = oDoc.ComponentDefinition.Occurrences(1)<br />
    Dim oMat As Matrix<br />
    Set oMat = oOcc.Transformation</p>

<p>    Dim aRotAngles(2) As Double<br />
    <br />
    Call CalculateRotationAngles(oMat, aRotAngles)<br />
<strong>End Sub</strong></p>

<p><strong>'==============================<br />
' ”親アセンブリ原点からの現在のオフセット”の<br />
' 情報を求める場合のマクロ<br />
'==============================<br />
Public Sub GetOffset()</strong><br />
  Dim oAsmDoc As AssemblyDocument<br />
  Set oAsmDoc = ThisApplication.ActiveDocument<br />
  <br />
  ' サブアセンブリオカレンス<br />
  Dim oSubAsmOccur As ComponentOccurrence<br />
  Set oSubAsmOccur = _<br />
    oAsmDoc.ComponentDefinition.Occurrences.Item(1)<br />
  <br />
  'サブアセンブリ内のパーツオカレンス<br />
  Dim oOccur1 As ComponentOccurrence<br />
  Set oOccur1 = oSubAsmOccur.SubOccurrences.Item(2)<br />
      <br />
  Dim oTransSub As Matrix<br />
  Set oTransSub = oSubAsmOccur.Transformation<br />
  <br />
  ' トップアセンブリのTransformation マトリクス<br />
  Dim oTransPart As Matrix<br />
  Set oTransPart = oOccur1.Transformation<br />
  <br />
  Dim oTransSubInv As Matrix<br />
  Set oTransSubInv = _<br />
           ThisApplication.TransientGeometry.CreateMatrix<br />
  oTransSubInv.TransformBy oTransSub<br />
  oTransSubInv.Invert<br />
  <br />
  Dim oTransSubToPart As Matrix<br />
  Set oTransSubToPart = _<br />
            ThisApplication.TransientGeometry.CreateMatrix<br />
  oTransSubToPart.TransformBy oTransPart<br />
  oTransSubToPart.PreMultiplyBy oTransSubInv<br />
  <br />
  Dim aRotAngles(2) As Double<br />
  <br />
  Debug.Print oSubAsmOccur.Name & " [" & _<br />
        oSubAsmOccur.ActivePositionalRepresentation & "]" _<br />
         & " = " & vbCr & "オフセット(mm) " & _<br />
         oTransSub.Translation.X * 10# & " , " & _<br />
         oTransSub.Translation.Y * 10# & " , " & _<br />
         oTransSub.Translation.Z * 10#<br />
         <br />
  Call CalculateRotationAngles(oTransSubInv, aRotAngles)<br />
  <br />
  Debug.Print oOccur1.Name & " = " & vbCr & "オフセット(mm) " & _<br />
         oTransSubToPart.Translation.X * 10# & " , " & _<br />
         oTransSubToPart.Translation.Y * 10# & " , " & _<br />
         oTransSubToPart.Translation.Z * 10#<br />
         <br />
  Call CalculateRotationAngles(oTransSubToPart, aRotAngles)<br />
         <br />
<strong>End Sub</strong></p>

<p>実行結果です。</p>

<p>SubAssembly:1 [New Test] = <br />
オフセット(mm) -54.2881604011507 , -11.8148364991365 , 44.6815345195355<br />
角度(deg) : 13.5838722978378 ,-37.4036005446772, -159.890668277901</p>

<p>Bolt:1 = <br />
オフセット(mm) -0.764232373660239 , 12.2810417835483 , 81.106793287568<br />
角度(deg) : -13.6836076758785 ,31.6637479393269, -129.098901591566</p>

<p>尚、サンプルファイルとコードは <span class="asset  asset-generic at-xid-6a0167607c2431970b01901ee0dea9970b"><a href="http://adndevblog.typepad.com/files/offsetsample.zip">ここ</a></span> よりダウンロードできます。</p>

<p>By Shigekazu Saito</p>
