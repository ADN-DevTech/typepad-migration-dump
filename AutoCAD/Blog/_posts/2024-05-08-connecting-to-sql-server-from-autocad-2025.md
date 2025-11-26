---
layout: "post"
title: "Connecting to SQL Server from AutoCAD 2025"
date: "2024-05-08 17:24:00"
author: "Madhukar Moogala"
categories:
  - ".NET"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2024/05/connecting-to-sql-server-from-autocad-2025.html "
typepad_basename: "connecting-to-sql-server-from-autocad-2025"
typepad_status: "Publish"
---

<p>
<script src="https://cdn.jsdelivr.net/gh/google/code-prettify@master/loader/run_prettify.js?skin=sunburst"></script>
</p>
<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p>
 <p>With the advent of the modern .NET platform, we've received queries from customers about loading SQL data client
        libraries onto the AutoCAD runtime.</p>
    <p>This post aims to address those queries and guide you on how to connect to SQL Server from an AutoCAD
        application.</p>
    <h2>Microsoft SQL Data Client Library</h2>
    <p>The Microsoft SQL Data Client Library is a brand new library for accessing SQL Server databases from modern .NET
        client applications. It also provides a common core set of features accessible on both .NET Framework and .NET
        Core.</p>
    <p>As evidenced by this <a href="https://devblogs.microsoft.com/dotnet/introducing-the-new-microsoftdatasqlclient/">blog post</a>,
        Microsoft recommends this library over System.Data.SqlClient.</p>
    <blockquote>
        <p>We recommend using the latest LTS version of Microsoft.Data.SqlClient, which is currently <a href="https://github.com/dotnet/SqlClient/releases/tag/v5.1.0">5.2.0</a> as it's the latest stable
            version that will be actively maintained for another 3 years. You can find it on the <a href="https://learn.microsoft.com/en-us/sql/connect/ado-net/sqlclient-driver-support-lifecycle?view=sql-server-ver16">Life
                Cycle page</a>. If you were previously using System.Data.SqlClient, we also have a <a href="https://github.com/dotnet/SqlClient/blob/main/porting-cheat-sheet.md">guide</a> to ease that
            process. You can also refer to the <a href="https://github.com/dotnet/SqlClient/wiki/Frequently-Asked-Questions">FAQ</a>. Hope that helps with
            your decision on which library to use.</p>
    </blockquote>
    <p>Let's use this <a href="https://www.nuget.org/packages/Microsoft.Data.SqlClient/#readme-body-tab">NuGet
            package</a> to write an AutoCAD client application to access SQL Server.</p>
    <p>When built along with an AutoCAD plugin, this package produces various artifacts for different runtimes. We only
        need the Windows 64-bit runtime module, which is supported on AutoCAD.</p>
    <p>To ensure this, we need to explicitly tell the build system by setting the following property group in the
        project file:</p>
    <pre class="prettyprint">    <code class="language-xml">
    &lt;PropertyGroup&gt;
    &lt;RuntimeIdentifier&gt;win-x64&lt;/RuntimeIdentifier&gt;
    &lt;/PropertyGroup&gt;
    </code>
    </pre>
    <p>Alternatively, you can use the dotnet cli:</p>
    <pre class="prettyprint">    <code class="language-bash">
      dotnet publish -r win-x64 –c Debug
    </code>
    </pre>
    <p>Once the build is successful, you can see the artifacts in the `x64\Debug\net8.0-windows\win-x64` folder.</p>
    <p>Now let's write a simple AutoCAD plugin to connect to SQL Server and query data.</p>
    <h2>Setup Database</h2>
    <p>Let's write a Database Manager class to access the `connectionstring`, test the connection, and query data.</p>
    <p>We will inject `IConfiguration` to load the `connectionString` from `secret.json`, which is a secure way of
        accessing database connection strings.</p>
    <pre class="prettyprint">    <code class="language-csharp">
    public DatabaseManager()
    {
      // Target this class for user secrets
      var builder = new ConfigurationBuilder()
                     .AddUserSecrets<databasemanager>();
    
      IConfiguration configuration = builder.Build();
      _connectionString = configuration.GetConnectionString("mssqlserver");
    }
    </databasemanager></code>
    </pre>
    <p>Now let's write a method to test the connection:</p>
    <pre class="prettyprint">    <code class="language-csharp">
    public bool TestSqlServerConnection()
    {
      try
      {
        using (var connection = new SqlConnection(_connectionString))
        {
          connection.Open();
          return true;
        }
      }
      catch (System.Exception ex)
      {
        Console.WriteLine($"Cannot connect to Database server: {ex.Message});
        return false;
      }
    }  
  </code>
    </pre>
    <p>Now let's write a method to query the data:</p>
    <pre class="prettyprint">  <code class="language-csharp">
  using (var cmd = new SqlCommand(queryString, connection))
  {
    using (var reader = cmd.ExecuteReader())
    {
      if (reader.HasRows)
      {
        while (reader.Read())
        {
          string city = reader.GetString(0); // assuming city is the first column (index 0)
          int customerCount = reader.GetInt32(1); // assuming number of customers is the second column (index 1)
          ed.WriteMessage($"\nCity: {city}, Customer Count: {customerCount}");
        }
      }
      else
      {
        ed.WriteMessage("\nNo results found.");
      }
    }
  }
  </code>
  </pre>
    <p>Now let's write a command to test the connection and query the data:</p>
    <pre class="prettyprint">  <code class="language-csharp">
  using (var data = new DatabaseManager())
  {
    try
    {
      data.TestSqlServerConnection();
      ed.WriteMessage("\nConnected to SQL Server database successfully!");
      data.RunQueryAndWriteToEditor(ed);
    }
    catch (System.Exception ex)
    {
      ed.WriteMessage($"\nConnecting to SQL Server database failed!\n{ex.Message}");
    }
  }
  </code>
  </pre>
    <p>Build the code and load the compiled module in AutoCAD 2025.</p>
    <p>Run the command "ConnectDB".</p>
    <p>It should connect to SQL Server and query the data.</p><p><a href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3aea69c200c-pi"><img width="244" height="120" title="ConnectDB" style="display: inline; background-image: none;" alt="ConnectDB" src="/assets/image_325806.jpg" border="0"></a></p>
    <p>Full source code is provided on Github: <a href="https://github.com/MadhukarMoogala/ACAD-SQLPlugin"> Click Here
        </a> </p>
    <p>Hope this helps you connect to SQL Server from AutoCAD.</p>
