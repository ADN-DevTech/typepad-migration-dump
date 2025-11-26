---
layout: "post"
title: "Integrating an ARX application with SQLite"
date: "2023-11-21 01:20:42"
author: "Sreeparna Mandal"
categories:
  - "Sreeparna Mandal"
original_url: "https://adndevblog.typepad.com/autocad/2023/11/integrating-an-arx-application-with-sqlite.html "
typepad_basename: "integrating-an-arx-application-with-sqlite"
typepad_status: "Publish"
---

<p>
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?skin=sunburst"></script>
</p>
<p>By <a href="https://adndevblog.typepad.com/autocad/sreeparna-mandal.html" target="_self">Sreeparna Mandal</a></p>
<h2>A brief introduction to SQLite:</h2>
<p>SQLite is a C-language library that implements a small, fast, self-contained, high-reliability, full-featured SQL database engine. For programs that have a lot of data that must be sifted and sorted in diverse ways, it is often easier and quicker to load the data into an in-memory SQLite database and use queries with joins and ORDER BY clauses to extract the data in the form and order needed.</p>
<h2>What can an ARX Application integrated with SQLite do?</h2>
<p>The ARX application can create a new database and add tables to it by defining the structure of each table and adding data to it through INSERT statements. If a database containing tables already exists, the ARX can fetch the necessary data from it using SELECT statements combined with other clauses, store the records in a data structure and use it to create a drawing in AutoCAD.</p>
<h2>How to create an ARX Application integrated with SQLite?</h2>
<h3>Step 1:</h3>
<p>Download the <a href="https://www.sqlite.org/download.html">C source code</a> of SQLite as an amalgamation from its website.</p>
<h3>Step 2:</h3>
<p>Create a new project with &quot;ARX/DBX Project for AutoCAD&quot; template. Add shell.c and sqlite3.c to Source Files in the project.</p>
<h3>Step 3:</h3>
<p>Set Project-&gt;Properties-&gt;C/C++-&gt;Code Generation-&gt;Smaller Type Check to No</p>
<h3>Step 4:</h3>
<p>Set Properties-&gt;C/C++-&gt;Pre Compiled Header-&gt;Pre Compiled Header to NOT USING</p>
<h3>Step 5:</h3>
<h4>To create a table by adding it to a database:</h4>
<ol>
<li>Define the table structure(s) as members of the class.
<pre class="prettyprint"><span style="font-size: small;">
struct LineCoord 
{
	int x_coord;
	int y_coord;
};
</span></pre>
</li>
<li>Open the particular database using sqlite3_open() function.
<pre class="prettyprint"><span style="font-size: small;">
sqlite3* db;
if (sqlite3_open(&quot;LineCoordDB.db&quot;, &amp;db) != SQLITE_OK)
{
	sqlite3_close(db);
	return;
}
</span></pre>
</li>
<li>Store the CREATE TABLE query as a string. Prepare the sql query from this string using sqlite3_prepare() and execute this query using sqlite3_step().
<pre class="prettyprint"><span style="font-size: small;">
std::string createTableQuery = &quot;CREATE TABLE LINECOORD (&quot;
			&quot;ID INT PRIMARY KEY	NOT NULL,&quot;
			&quot;XCOORD INT NOT NULL,&quot;
			&quot;YCOORD INT NOT NULL);&quot;;

//prepare sql statement from the string containing the query
sqlite3_stmt* createTableStmt;
sqlite3_prepare(db, createTableQuery.c_str(), static_cast(createTableQuery.size()), &amp;createTableStmt, nullptr);
		
//execute sql query to create table
if (sqlite3_step(createTableStmt) != SQLITE_DONE)
{
	sqlite3_close(db);
}
</span></pre>
</li>
</ol>
<h3>Step 6:</h3>
<h4>Inserting data into table:</h4>
<ol>
<li>Store the INSERT query into a string or in case of multiple INSERT statements, use a vector of string.
<pre class="prettyprint"><span style="font-size: small;">
std::vector insertStmtList;
insertStmtList.emplace_back(&quot;INSERT INTO LINECOORD (&#39;ID&#39;, &#39;XCOORD&#39;, &#39;YCOORD&#39;) VALUES (&#39;101&#39;, &#39;1&#39;, &#39;1&#39;);&quot;);
</span></pre>
</li>
<li>Prepare the sql query from the string using sqlite3_prepare() and execute this query using sqlite3_step().
<pre class="prettyprint"><span style="font-size: small;">
for (const auto&amp; insertQueryStr : insertStmtList)
{
	sqlite3_stmt* insertStmt;
	sqlite3_prepare(db, insertQueryStr.c_str(), static_cast(insertQueryStr.size()), &amp;insertStmt, nullptr);

	//execute each sql stmt
	if (sqlite3_step(insertStmt) != SQLITE_DONE)
	{
		sqlite3_close(db);
		return;
	}
}
</span></pre>
</li>
<li>Close the database using sqlite3_close().</li>
</ol>
<p>The created table and its data can be checked by installing &#39;DB Browser (SQLite)&#39; from official website of SQLite.</p>
<h3>Step 7:</h3>
<h4>To fetch data from an existing table</h4>
<ol>
<li>Open the particular database using sqlite3_open() function.</li>
<li>Define the following where each variable of Record type will hold data of a tuple(row) and a variable of Records type will hold the data of the entire table:
<pre class="prettyprint"><span style="font-size: small;">
using Record = std::vector;
using Records = std::vector;
</span></pre>
</li>
<li>We call a user defined function with string parameter containing the sql SELECT query to fetch all the data and return it to a variable of Records type. Inside the user defined function, we use a callback. SQLite will call this callback function for each record processed in each SELECT statement executed within the SQL argument.</li>
</ol>
<p>For a very basic understanding, you may refer to the <a href="https://www.sqlite.org/quickstart.html">example shown in SQLite website</a>. Kindly refer to the methods ADSKMyGroupGetLineCoord(), select_callback() and exec_select_stmt() from the <a href="https://github.com/Sreeparna-11/Arx_SQLite_2DLineCoordinates">complete source code</a> to see the full implementation.</p>
<h3>Step 8:</h3>
<h4>To create a drawing using the data stored in the table</h4>
<p>In this case, extract data of each record(an X-coordinate and a Y-coordinate) from the data structure used to store the table values and use them to draw a line.</p>
<p>Complete sample with source code is available at Github : <a href="https://github.com/Sreeparna-11/Arx_SQLite_2DLineCoordinates">Arx_SQLite_2DLineCoordinates</a></p>
