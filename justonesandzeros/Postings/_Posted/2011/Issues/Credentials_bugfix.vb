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
	''' Credentials for making server calls with userId and ticket information from a previous sign in.
	''' </summary>
	Public Class UserIdTicketCredentials_bugfix
		Implements IWebServiceCredentials
		''' <summary>
		''' The constructor.
		''' </summary>
		''' <param name="serverName">The name of the Vault server.</param>
		''' <param name="vaultName">The name of the Vault.</param>
		''' <param name="userId">The User ID</param>
		''' <param name="ticket">The security ticket.</param>
		Public Sub New(serverName__1 As String, vaultName__2 As String, userId As Long, ticket As String)
			m_ServerName = serverName__1
			m_VaultName = vaultName__2

			m_SecurityHeaderInfo = New SecurityHeaderInfo(userId, ticket)
		End Sub

		#Region "IWebServiceCredentials Members"

		''' <summary>
		''' Gets the server name.
		''' </summary>
		Public ReadOnly  Property ServerName() As String Implements IWebServiceCredentials.ServerName
			Get
				Return m_ServerName
			End Get
		End Property
		Private m_ServerName As String

		''' <summary>
		''' Gets the vault name.
		''' </summary>
		Public ReadOnly Property VaultName() As String Implements IWebServiceCredentials.VaultName 
			Get
				Return m_VaultName
			End Get
		End Property
		Private m_VaultName As String

		''' <summary>
		''' Gets the value telling if a sign in is required before a service can be used.
		''' </summary>
		Public ReadOnly Property RequiresSignIn() As Boolean Implements IWebServiceCredentials.RequiresSignIn 
			Get
				Return False
			End Get
		End Property

		''' <summary>
		''' Gets the value telling if a sign out should be called when the service goes out of scope.
		''' </summary>
		Public ReadOnly Property RequiresSignOut() As Boolean Implements IWebServiceCredentials.RequiresSignOut 
			Get
				Return False
			End Get
		End Property

		''' <summary>
		''' Gets the value telling if the credentials can sign in.
		''' </summary>
		Public ReadOnly Property SupportsSignIn() As Boolean Implements IWebServiceCredentials.SupportsSignIn
			Get
				Return False
			End Get
		End Property

		''' <summary>
		''' Gets the value telling if the credentials can sign out.
		''' </summary>
		Public ReadOnly Property SupportsSignOut() As Boolean Implements IWebServiceCredentials.SupportsSignOut 
			Get
				Return False
			End Get
		End Property

		''' <summary>
		''' Gets the security header.
		''' </summary>
		Public ReadOnly Property SecurityHeaderInfo() As SecurityHeaderInfo Implements IWebServiceCredentials.SecurityHeaderInfo
			Get
				Return m_SecurityHeaderInfo
			End Get
		End Property
		Private m_SecurityHeaderInfo As SecurityHeaderInfo

		<System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)> _
		Public Function SignIn(secSvc As SecurityService, winAuthSvc As WinAuthService) As SecurityHeaderInfo Implements IWebServiceCredentials.SignIn 
			Throw New NotImplementedException()
		End Function

		<System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)> _
		Public Sub SignOut(secSvc As SecurityService, winAuthSvc As WinAuthService)implements IWebServiceCredentials.SignOut 
			Throw New NotImplementedException()
		End Sub

		#End Region
	End Class


	''' <summary>
	''' Credentials for making server calls based on information from an existing web service.
	''' </summary>
	Public Class WebServiceCredentials_bugfix
		Implements IWebServiceCredentials
		''' <summary>
		''' The constructor.
		''' </summary>
		''' <param name="svc">A web service object. The service must already have security information
		''' from a prior sign-in.</param>
		Public Sub New(svc As IWebService)
			' convert the web service URL to the servername string
			Dim serviceUrl As New Uri(svc.Url)
			m_ServerName = serviceUrl.Host
			If Not serviceUrl.IsDefaultPort Then
				m_ServerName += ":" & serviceUrl.Port
			End If
			If serviceUrl.Scheme.Equals("https", StringComparison.InvariantCultureIgnoreCase) Then
				m_ServerName = "https://" & ServerName
			End If

			m_VaultName = [String].Empty

			m_SecurityHeaderInfo = New SecurityHeaderInfo(svc.SecurityHeader.UserId, svc.SecurityHeader.Ticket)
		End Sub

		#Region "IWebServiceCredentials Members"

		''' <summary>
		''' Gets the server name.
		''' </summary>
		Public ReadOnly Property ServerName() As String Implements IWebServiceCredentials.ServerName 
			Get
				Return m_ServerName
			End Get
		End Property
		Private m_ServerName As String

		''' <summary>
		''' Gets an empty string. The vault name information is not known in this case.
		''' </summary>
		Public ReadOnly Property VaultName() As String Implements IWebServiceCredentials.VaultName 
			Get
				Return m_VaultName
			End Get
		End Property
		Private m_VaultName As String

		''' <summary>
		''' Gets the value telling if a sign in is required before a service can be used.
		''' </summary>
		Public ReadOnly Property RequiresSignIn() As Boolean Implements IWebServiceCredentials.RequiresSignIn 
			Get
				Return False
			End Get
		End Property

		''' <summary>
		''' Gets the value telling if a sign out should be called when the service goes out of scope.
		''' </summary>
		Public ReadOnly Property RequiresSignOut() As Boolean Implements IWebServiceCredentials.RequiresSignOut 
			Get
				Return False
			End Get
		End Property

		''' <summary>
		''' Gets the value telling if the credentials can sign in.
		''' </summary>
		Public ReadOnly Property SupportsSignIn() As Boolean Implements IWebServiceCredentials.SupportsSignIn 
			Get
				Return False
			End Get
		End Property

		''' <summary>
		''' Gets the value telling if the credentials can sign out.
		''' </summary>
		Public ReadOnly Property SupportsSignOut() As Boolean Implements IWebServiceCredentials.SupportsSignOut 
			Get
				Return False
			End Get
		End Property

		''' <summary>
		''' Gets the security header.
		''' </summary>
		Public ReadOnly Property SecurityHeaderInfo() As SecurityHeaderInfo Implements IWebServiceCredentials.SecurityHeaderInfo 
			Get
				Return m_SecurityHeaderInfo
			End Get
		End Property
		Private m_SecurityHeaderInfo As SecurityHeaderInfo

		<System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)> _
		Public Function SignIn(secSvc As SecurityService, winAuthSvc As WinAuthService) As SecurityHeaderInfo Implements IWebServiceCredentials.SignIn 
			Throw New NotImplementedException()
		End Function

		<System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)> _
		Public Sub SignOut(secSvc As SecurityService, winAuthSvc As WinAuthService) Implements IWebServiceCredentials.SignOut 
			Throw New NotImplementedException()
		End Sub

		#End Region
	End Class


