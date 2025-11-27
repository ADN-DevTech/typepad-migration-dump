/*=====================================================================
  
  This file is part of the Autodesk Vault API Code Samples.

  Copyright (C) Autodesk Inc.  All rights reserved.

THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
PARTICULAR PURPOSE.
=====================================================================*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Autodesk.Connectivity.WebServices;
using Autodesk.Connectivity.WebServicesTools;

/// <summary>
/// A set of functions to add, check-in, check-out and download files using only web service calls.
/// I attempted to keep the function calls similar to the Vault 2013 versions.  For example, byte[] 
/// parameters are used, even though a Stream is more optimal.
/// </summary>
public static class LegacyFileTransfer
{
    private static int MAX_FILE_PART_SIZE = 49 * 1024 * 1024;   // 49 MB

    /// <summary>
    /// Adds a file using only web service API calls.
    /// </summary>
    /// <param name="fileContents">The entire contents of the file.
    /// If you work with large files, you may want to modify this to use a Stream instead.</param>
    public static File AddFile(WebServiceManager mgr, string vaultName,
        long folderId, string fileName, string comment,
        DateTime lastWrite, FileAssocParam[] associations, BOM bom, FileClassification fileClassification,
        bool hidden, byte[] fileContents)
    {

        ByteArray uploadTicket = UploadFile(mgr, vaultName, fileName, fileContents);

        File addedFile = mgr.DocumentService.AddUploadedFile(folderId, fileName, comment,
            lastWrite, associations, bom, fileClassification, hidden, uploadTicket);

        return addedFile;
    }

    /// <summary>
    /// Checks-in a file using only web service API calls.
    /// </summary>
    /// <param name="fileContents">The entire contents of the file.
    /// If you work with large files, you may want to modify this to use a Stream instead.</param>
    public static File CheckinFile(WebServiceManager mgr, string vaultName, 
        long fileMasterId, string comment, bool keepCheckedOut,
        DateTime lastWrite, FileAssocParam[] associations, BOM bom, bool copyBom,
        string newFileName, FileClassification fileClassification, bool hidden, byte[] fileContents)
    {
        ByteArray uploadTicket = UploadFile(mgr, vaultName, newFileName, fileContents);

        File checkedInFile = mgr.DocumentService.CheckinUploadedFile(fileMasterId, comment, keepCheckedOut, lastWrite,
            associations, bom, copyBom, newFileName, fileClassification, hidden, uploadTicket);

        return checkedInFile;
    }



    /// <summary>
    /// Checks out and optionally downloads a file using only web service API calls.
    /// </summary>
    /// <param name="fileContents">If downloadFile is true, this parameter will return 
    /// the entire contents of the file.
    /// If you work with large files, you may want to modify this to use a Stream instead.</param>
    /// <returns></returns>
    public static File CheckoutFile(WebServiceManager mgr, long folderId, long fileId, CheckoutFileOptions option, 
        string machine, string localPath, string comment, bool downloadFile, bool allowSync, 
        out byte[] fileContents)
    {
        ByteArray downloadTicket;
        fileContents = null;
        
        File checkedOutFile = mgr.DocumentService.CheckoutFile(fileId, option, machine, localPath, comment, out downloadTicket);

        if (downloadFile)
            DownloadFile(mgr, checkedOutFile.Id, allowSync, out fileContents);
        
        return checkedOutFile;
    }

    /// <summary>
    /// Downloads a file using only web service API calls.
    /// </summary>
    /// <param name="fileContents">The entire contents of the file.
    /// If you work with large files, you may want to modify this to use a Stream instead.</param>
    public static void DownloadFile(WebServiceManager mgr, long fileId, bool allowSync, out byte[] fileContents)
    {
        ByteArray [] tickets = mgr.DocumentService.GetDownloadTicketsByFileIds(new long[] { fileId });
        DownloadFile(mgr, out fileContents, tickets[0], true);
    }


    private static ByteArray UploadFile(WebServiceManager mgr, string vaultName, string fileName, byte[] fileContents)
    {
        FileTransferHeader fileTransferHeaderValue = new FileTransferHeader();
        mgr.FilestoreService.FileTransferHeaderValue = fileTransferHeaderValue;
        fileTransferHeaderValue.Identity = Guid.NewGuid();
        fileTransferHeaderValue.Extension = System.IO.Path.GetExtension(fileName);
        fileTransferHeaderValue.Vault = vaultName;

        // parse the file contents into indiviual parts and process each one individually
        ByteArray uploadTicket = new ByteArray();
        long bytesSent = 0;
        int bufferSize = MAX_FILE_PART_SIZE;
        while (bytesSent < fileContents.Length)
        {
            if ((fileContents.Length - bytesSent) < MAX_FILE_PART_SIZE)
                bufferSize = fileContents.Length - (int)bytesSent;
            else
                bufferSize = MAX_FILE_PART_SIZE;

            fileTransferHeaderValue.Compression = Compression.None;
            fileTransferHeaderValue.UncompressedSize = bufferSize;
            fileTransferHeaderValue.IsComplete = ((bufferSize + bytesSent) >= fileContents.Length);

            byte[] buffer = null;
            if (bufferSize == fileContents.Length)
                buffer = fileContents;
            else
            {
                buffer = new byte[bufferSize];
                Array.Copy(fileContents, bytesSent, buffer, 0, bufferSize);
            }

            uploadTicket.Bytes = mgr.FilestoreService.UploadFilePart(buffer);
            bytesSent += bufferSize;
        }

        return uploadTicket;
    }

    private static void DownloadFile(WebServiceManager mgr, out byte [] fileContents, ByteArray downloadTicket, bool allowSync)
    {
        mgr.FilestoreService.CompressionHeaderValue = new CompressionHeader();
        mgr.FilestoreService.CompressionHeaderValue.Supported = Compression.None;
        mgr.FilestoreService.FileTransferHeaderValue = new FileTransferHeader();
        mgr.FilestoreService.FileTransferHeaderValue = null;

        System.IO.MemoryStream stream = new System.IO.MemoryStream();

        long bytesRead = 0;
        while (mgr.FilestoreService.FileTransferHeaderValue == null || !mgr.FilestoreService.FileTransferHeaderValue.IsComplete)
        {
            byte[] tempBytes = mgr.FilestoreService.DownloadFilePart(downloadTicket.Bytes, bytesRead, bytesRead + MAX_FILE_PART_SIZE - 1, allowSync);
            int chunkSize = mgr.FilestoreService.FileTransferHeaderValue.UncompressedSize;
            stream.Write(tempBytes, 0, chunkSize);
            bytesRead += chunkSize;
        }

        fileContents = new byte[stream.Length];
        stream.Seek(0, System.IO.SeekOrigin.Begin);
        stream.Read(fileContents, 0, (int)stream.Length);
        stream.Close();
    }

}

