'=====================================================================
'  
'  This file is part of the Autodesk Vault API Code Samples.
'
'  Copyright (C) Autodesk Inc.  All rights reserved.
'
'THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
'KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
'IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
'PARTICULAR PURPOSE.
'=====================================================================


Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Imports Autodesk.Connectivity.WebServices
Imports Autodesk.Connectivity.WebServicesTools

''' <summary>
''' A set of functions to add, check-in, check-out and download files using only web service calls.
''' I attempted to keep the function calls similar to the Vault 2013 versions.  For example, byte[] 
''' parameters are used, even though a Stream is more optimal.
''' </summary>
Public NotInheritable Class LegacyFileTransfer
	Private Sub New()
	End Sub
	Private Shared MAX_FILE_PART_SIZE As Integer = 49 * 1024 * 1024
	' 49 MB
	''' <summary>
	''' Adds a file using only web service API calls.
	''' </summary>
	''' <param name="fileContents">The entire contents of the file.
	''' If you work with large files, you may want to modify this to use a Stream instead.</param>
	Public Shared Function AddFile(mgr As WebServiceManager, vaultName As String, folderId As Long, fileName As String, comment As String, lastWrite As DateTime, _
		associations As FileAssocParam(), bom As BOM, fileClassification As FileClassification, hidden As Boolean, fileContents As Byte()) As File

		Dim uploadTicket As ByteArray = UploadFile(mgr, vaultName, fileName, fileContents)

		Dim addedFile As File = mgr.DocumentService.AddUploadedFile(folderId, fileName, comment, lastWrite, associations, bom, _
			fileClassification, hidden, uploadTicket)

		Return addedFile
	End Function

	''' <summary>
	''' Checks-in a file using only web service API calls.
	''' </summary>
	''' <param name="fileContents">The entire contents of the file.
	''' If you work with large files, you may want to modify this to use a Stream instead.</param>
	Public Shared Function CheckinFile(mgr As WebServiceManager, vaultName As String, fileMasterId As Long, comment As String, keepCheckedOut As Boolean, lastWrite As DateTime, _
		associations As FileAssocParam(), bom As BOM, copyBom As Boolean, newFileName As String, fileClassification As FileClassification, hidden As Boolean, _
		fileContents As Byte()) As File
		Dim uploadTicket As ByteArray = UploadFile(mgr, vaultName, newFileName, fileContents)

		Dim checkedInFile As File = mgr.DocumentService.CheckinUploadedFile(fileMasterId, comment, keepCheckedOut, lastWrite, associations, bom, _
			copyBom, newFileName, fileClassification, hidden, uploadTicket)

		Return checkedInFile
	End Function



	''' <summary>
	''' Checks out and optionally downloads a file using only web service API calls.
	''' </summary>
	''' <param name="fileContents">If downloadFile is true, this parameter will return 
	''' the entire contents of the file.
	''' If you work with large files, you may want to modify this to use a Stream instead.</param>
	''' <returns></returns>
	Public Shared Function CheckoutFile(mgr As WebServiceManager, folderId As Long, fileId As Long, [option] As CheckoutFileOptions, machine As String, localPath As String, _
		comment As String, downloadFile__1 As Boolean, allowSync As Boolean, ByRef fileContents As Byte()) As File
		Dim downloadTicket As ByteArray
		fileContents = Nothing

		Dim checkedOutFile As File = mgr.DocumentService.CheckoutFile(fileId, [option], machine, localPath, comment, downloadTicket)

		If downloadFile__1 Then
			DownloadFile(mgr, checkedOutFile.Id, allowSync, fileContents)
		End If

		Return checkedOutFile
	End Function

	''' <summary>
	''' Downloads a file using only web service API calls.
	''' </summary>
	''' <param name="fileContents">The entire contents of the file.
	''' If you work with large files, you may want to modify this to use a Stream instead.</param>
	Public Shared Sub DownloadFile(mgr As WebServiceManager, fileId As Long, allowSync As Boolean, ByRef fileContents As Byte())
		Dim tickets As ByteArray() = mgr.DocumentService.GetDownloadTicketsByFileIds(New Long() {fileId})
		DownloadFile(mgr, fileContents, tickets(0), True)
	End Sub


	Private Shared Function UploadFile(mgr As WebServiceManager, vaultName As String, fileName As String, fileContents As Byte()) As ByteArray
		Dim fileTransferHeaderValue As New FileTransferHeader()
		mgr.FilestoreService.FileTransferHeaderValue = fileTransferHeaderValue
		fileTransferHeaderValue.Identity = Guid.NewGuid()
		fileTransferHeaderValue.Extension = System.IO.Path.GetExtension(fileName)
		fileTransferHeaderValue.Vault = vaultName

		' parse the file contents into indiviual parts and process each one individually
		Dim uploadTicket As New ByteArray()
		Dim bytesSent As Long = 0
		Dim bufferSize As Integer = MAX_FILE_PART_SIZE
		While bytesSent < fileContents.Length
			If (fileContents.Length - bytesSent) < MAX_FILE_PART_SIZE Then
				bufferSize = fileContents.Length - CInt(bytesSent)
			Else
				bufferSize = MAX_FILE_PART_SIZE
			End If

			fileTransferHeaderValue.Compression = Compression.None
			fileTransferHeaderValue.UncompressedSize = bufferSize
			fileTransferHeaderValue.IsComplete = ((bufferSize + bytesSent) >= fileContents.Length)

			Dim buffer As Byte() = Nothing
			If bufferSize = fileContents.Length Then
				buffer = fileContents
			Else
				buffer = New Byte(bufferSize - 1) {}
				Array.Copy(fileContents, bytesSent, buffer, 0, bufferSize)
			End If

			uploadTicket.Bytes = mgr.FilestoreService.UploadFilePart(buffer)
			bytesSent += bufferSize
		End While

		Return uploadTicket
	End Function

	Private Shared Sub DownloadFile(mgr As WebServiceManager, ByRef fileContents As Byte(), downloadTicket As ByteArray, allowSync As Boolean)
		mgr.FilestoreService.CompressionHeaderValue = New CompressionHeader()
		mgr.FilestoreService.CompressionHeaderValue.Supported = Compression.None
		mgr.FilestoreService.FileTransferHeaderValue = New FileTransferHeader()
		mgr.FilestoreService.FileTransferHeaderValue = Nothing

		Dim stream As New System.IO.MemoryStream()

		Dim bytesRead As Long = 0
		While mgr.FilestoreService.FileTransferHeaderValue Is Nothing OrElse Not mgr.FilestoreService.FileTransferHeaderValue.IsComplete
			Dim tempBytes As Byte() = mgr.FilestoreService.DownloadFilePart(downloadTicket.Bytes, bytesRead, bytesRead + MAX_FILE_PART_SIZE - 1, allowSync)
			Dim chunkSize As Integer = mgr.FilestoreService.FileTransferHeaderValue.UncompressedSize
			stream.Write(tempBytes, 0, chunkSize)
			bytesRead += chunkSize
		End While

		fileContents = New Byte(stream.Length - 1) {}
		stream.Seek(0, System.IO.SeekOrigin.Begin)
		stream.Read(fileContents, 0, CInt(stream.Length))
		stream.Close()
	End Sub

End Class