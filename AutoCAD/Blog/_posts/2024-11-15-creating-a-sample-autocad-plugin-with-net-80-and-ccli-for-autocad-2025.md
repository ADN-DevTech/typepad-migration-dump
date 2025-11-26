---
layout: "post"
title: "Creating a Sample AutoCAD Plugin with .NET 8.0 and C++/CLI for AutoCAD 2025"
date: "2024-11-15 02:09:00"
author: "Madhukar Moogala"
categories:
  - ".NET"
  - "AutoCAD"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2024/11/creating-a-sample-autocad-plugin-with-net-80-and-ccli-for-autocad-2025.html "
typepad_basename: "creating-a-sample-autocad-plugin-with-net-80-and-ccli-for-autocad-2025"
typepad_status: "Publish"
---

<p>     <script src="https://cdn.jsdelivr.net/gh/google/code-prettify@master/loader/run_prettify.js?skin=sunburst"></script>   </p>   <p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p>    <p>In this article, will walk through the development of a simple AutoCAD plugin using C++/CLI. The plugin will     add a feature to draw circles dynamically, where users can specify the radius and the plugin will generate a random     color for each circle.</p>   <p>We will break the plugin down into three major parts:</p>   <ol>     <li><strong>AutoCAD Database Helper Class</strong></li>     <li><strong>UI Form Implementation</strong></li>     <li><strong>ObjectARX Entry Point</strong></li>   </ol>   <h3>Part 1: AutoCAD Database Helper Class</h3>   <p>In AutoCAD, entities such as circles, lines, and other objects are stored in the AutoCAD database. The     <code>AcDbHelper</code> class in our plugin facilitates the creation and manipulation of AutoCAD entities.   </p>   <h4><strong>Smart Pointer for AutoCAD Database Objects</strong></h4>   <p>We start by creating a <code>unique_db_ptr</code> template class to manage AutoCAD objects with a smart pointer.     This ensures that AutoCAD objects are correctly cleaned up after use.</p>   <pre class="prettyprint lang-cpp">
      <code>
        template <class T> struct unique_db_ptr : public std::unique_ptr<T, void(*)(AcDbObject*)>
          {
              unique_db_ptr<T>(T* t) : std::unique_ptr<T, void(*)(AcDbObject*)>(t, closeOrDeleteDbObj) { }
          
              static unique_db_ptr<T> create()
              {
                  T* newObj = new T();
                  return unique_db_ptr<T>(newObj);
              }
          
              // Helper function for smart pointer cleanup
              static void closeOrDeleteDbObj(AcDbObject* pObj)
              {
                  if (pObj->objectId().isNull())
                      delete pObj;
                  else
                      pObj->close();
              }
          };
          
      </code>
    </pre>
  <h4><strong>Adding Entities to the Database</strong></h4>
  <p>We also define a method to add entities like circles to AutoCAD's model space, ensuring they are correctly appended
    to the database.</p>
  <pre class="prettyprint lang-cpp">
    <code>
    static bool addToDb(AcDbEntity* pEnt, AcDbDatabase* pDb = nullptr)
    {
    if (!pDb)
        pDb = acdbHostApplicationServices()->workingDatabase();

    unique_db_ptr<AcDbEntity> ent(pEnt);
    AcDbBlockTable* pBt;
    if (Acad::eOk != pDb->getBlockTable(pBt, AcDb::kForRead))
        return false;

    unique_db_ptr<AcDbBlockTable> bt(pBt);
    AcDbBlockTableRecord* pMs;
    if (Acad::eOk != pBt->getAt(ACDB_MODEL_SPACE, pMs, AcDb::kForWrite))
        return false;

    return Acad::eOk == unique_db_ptr<AcDbBlockTableRecord>(pMs)->appendAcDbEntity(ent.get());
    }
    </code>
  </pre>
  <h4><strong>Creating and Adding Circles</strong></h4>
  <p>The <code>createCircle</code> method creates a circle with a specified radius and color index, then adds it to the
    database:</p>
  <pre class="prettyprint lang-cpp">
  <code>
    static bool createCircle(const AcGePoint3d& center, double radius, int colorIndex = 1)
      {
          auto circlePtr = unique_db_ptr<AcDbCircle>::create();
          if (!circlePtr)
              return false;

          AcDbCircle* circle = circlePtr.get();
          circle->setDatabaseDefaults();
          circle->setRadius(radius);
          circle->setColorIndex(colorIndex);
          circle->setCenter(center);
          return addToDb(circle);
      }
  </code>
  </pre>
  <h3>Part 2: UI Form Implementation</h3>
  <p>Now, let&rsquo;s move on to creating the user interface (UI) that interacts with the AutoCAD database. We&rsquo;ll
    use Windows Forms in C++/CLI for this purpose.</p>
  <h4><strong>MainForm Class</strong></h4>
  <p>The <code>MainForm</code> class represents the UI, containing a button to draw a circle and a numeric input for the
    circle radius. It uses a task-based asynchronous method to perform the drawing operation on the main AutoCAD thread.
  </p>

  <pre class="prettyprint lang-cpp">
    <code>
      public ref class MainForm : public Form
      {
      private:
          Button^ drawButton;
          NumericUpDown^ radiusInput;
          Label^ radiusLabel;

          void InitializeComponent()
          {
              //adding controls to the form and initialising properties
          }

          Task^ DrawCircleAsync(System::Object^ data)
          {
              //draw the circle
          }

          void DrawButton_Click(System::Object^ sender, System::EventArgs^ e)
          {
              // Handle click event
              auto dm = Autodesk::AutoCAD::ApplicationServices::Core::Application::DocumentManager;

              // Create the delegate with the correct syntax
              auto callback = gcnew Func<Object^, Task^>(this, &MainForm::DrawCircleAsync);
             
              // Execute the callback in the command context
              auto task = dm->ExecuteInCommandContextAsync(callback, nullptr);
               task->GetResult();
              // Enable the button
               drawButton->Enabled = true;
              // Set focus to the drawing area and zoom extents
               AcadUtils::Utils::SetFocusToDwgView();
               AcadUtils::Utils::CancelAndRunCmds("_.zoom\n_extents\n");
          }

      public:
          MainForm()
          {
              InitializeComponent();
          }
      };
    </code>
  </pre>
  <h4><strong>ExecuteInCommandContextAsync Method</strong></h4>
  <p>In this code, the <code>ExecuteInCommandContextAsync</code> method is used to execute a callback (in this case, the
    <code>DrawCircleAsync</code> method) within AutoCAD's command context.
    The primary reason for using this API is to
    ensure that any interactions with AutoCAD, especially those that modify the drawing or perform operations on the
    AutoCAD database, are executed on the correct thread, which is the AutoCAD main thread.
  </p>
  <h3>Why Use <code>ExecuteInCommandContextAsync</code>?</h3>
  <p>AutoCAD, being a single-threaded application, has strict rules about how commands and modifications to the AutoCAD
    database should be performed. Interacting with AutoCAD from another thread, such as a UI thread, can cause problems
    because it bypasses AutoCAD's synchronization mechanisms, leading to potential crashes, invalid operations, or
    unexpected behavior.</p>
  <h4><strong>SynchronizationContext Issue</strong></h4>
  <p>When you display a WinForm dialog (like <code>MainForm</code> in your code), it restores the 'previous'
    <code>SynchronizationContext</code>, which in this case is the default context. This default context attempts to
    execute continuations using the thread pool, not the main AutoCAD thread. Since AutoCAD requires that commands (such
    as modifying the database or interacting with the drawing) be run on its main thread, executing on a background
    thread (via the thread pool) can cause synchronization issues.
  </p>
  <p>AutoCAD doesn't like this because it expects all UI operations and database modifications to occur on the main
    AutoCAD thread. If you try to execute these operations on a different thread (e.g., using the thread pool),
    AutoCAD's internal threading model will not handle it correctly.</p>
  <h4><strong>How <code>ExecuteInCommandContextAsync</code> Solves This</strong></h4>
  <p>By using <code>ExecuteInCommandContextAsync</code>, the callback (i.e., the <code>DrawCircleAsync</code> method) is
    explicitly executed within AutoCAD's command context. This method ensures that the operation is correctly scheduled
    on the AutoCAD thread, which means:</p>
  <ol>
    <li><strong>Correct Threading:</strong> It guarantees that the AutoCAD API calls are made on the main AutoCAD
      thread, which is crucial for thread safety.</li>
    <li><strong>Synchronization Context:</strong> It sets up the correct synchronization context for the operation, so
      any continuation (like UI updates or database operations) that happens after this method call will respect
      AutoCAD's threading model and ensure that UI updates (like enabling buttons) are done on the UI thread without
      causing conflicts.</li>
    <li><strong>Asynchronous Execution:</strong> It allows the asynchronous execution of AutoCAD commands without
      blocking the UI thread or causing AutoCAD to freeze while waiting for the operation to complete. The
      <code>GetResult()</code> method ensures that the UI thread waits for the task to finish before proceeding.
    </li>
  </ol>
  <h3>Part 3: ObjectARX Entry Point</h3>
  <p>The <code>CArxNetCoreApp</code> class represents the entry point of our AutoCAD plugin. It registers the
    application and launches the UI dialog.</p>
  <pre class="prettyprint lang-cpp">
    <code>
      class CArxNetCoreApp : public AcRxArxApp
      {
      public:
          virtual AcRx::AppRetCode On_kInitAppMsg(void* pkt) { 
            return AcRxArxApp::On_kInitAppMsg(pkt); }
          virtual AcRx::AppRetCode On_kUnloadAppMsg(void* pkt) { 
            return AcRxArxApp::On_kUnloadAppMsg(pkt); }
          virtual void RegisterServerComponents() {}

          static void MADGUIToolLaunch()
          {
              try
              {
                  auto form = gcnew UIForms::MainForm();
                  Autodesk::AutoCAD::ApplicationServices::Application::ShowModelessDialog(form);
              }
              catch (System::Exception^ ex)
              {
                  acutPrintf(L"\nException occurred: %s", ex->Message);
              }
          }
      };
    </code>
  </pre>
  <h3>Conclusion</h3>
  <p>This plugin demonstrates how to integrate AutoCAD with C++/CLI, offering a simple yet powerful tool to interact
    with AutoCAD's database, create entities, and provide a user-friendly interface for drawing circles. The use of
    smart pointers, task-based asynchronous methods, and Windows Forms for UI design highlights the flexibility and
    power of combining C++/CLI with AutoCAD's ObjectARX SDK.</p>
  <p>By following the steps outlined in this tutorial, you can build and expand upon this basic plugin to add more
    features and functionality to AutoCAD.</p>

  <a href="https://github.com/MadhukarMoogala/ArxNetCore" target="_blank" style="text-decoration: none;">
    <button style="
          background-color: #24292e; 
          color: white; 
          border: none; 
          border-radius: 5px; 
          padding: 10px 20px; 
          font-size: 14px; 
          font-weight: bold; 
          cursor: pointer;
          display: inline-flex; 
          align-items: center;">
      <img src="/assets/GitHub-Mark.png" alt="GitHub Logo" style="width: 20px; height: 20px; margin-right: 10px;">
      View on GitHub
    </button>
  </a>
  </br>
  </br>
  <script src="https://gist.github.com/MadhukarMoogala/2055d7204dfa1b9359cf216b7a771651.js"></script>
