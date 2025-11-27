using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Autodesk.Connectivity.WebServices;
using Autodesk.Connectivity.WebServicesTools;
using Autodesk.Connectivity.Extensibility.Framework;

[assembly: ApiVersion("4.0")]
[assembly: ExtensionId("CAEA1F8E-C1F2-4C63-84D0-7E62CDCE2408")]

namespace FourEyes
{
    public class FourEyesChecker : IWebServiceExtension
    {

        // implement IWebServiceExtension
        public void OnLoad()
        {
            DocumentServiceExtensions.UpdateFileLifecycleStateEvents.GetRestrictions += 
                new EventHandler<UpdateFileLifeCycleStateCommandEventArgs>(
                    UpdateFileLifecycleStateEvents_GetRestrictions);
        }

        // event handler
        void UpdateFileLifecycleStateEvents_GetRestrictions(
            object sender, UpdateFileLifeCycleStateCommandEventArgs e)
        {
            try
            {
                IWebService service = sender as IWebService;
                if (service == null)
                    return;

                WebServiceCredentials_bugfix cred = new WebServiceCredentials_bugfix(service);
                using (WebServiceManager mgr = new WebServiceManager(cred))
                {
                    long currentUserId = mgr.SecurityService.SecurityHeader.UserId;

                    LfCycDef [] defs = 
                        mgr.DocumentServiceExtensions.GetAllLifeCycleDefinitions();
                    LfCycDef releaseProcess = defs.FirstOrDefault(
                        n => n.SysName == "Flexible Release Process");

                    LfCycState reviewState = 
                        releaseProcess.StateArray.FirstOrDefault(
                        n => n.DispName == "For Review");

                    LfCycState releaseState = 
                        releaseProcess.StateArray.FirstOrDefault(
                        n => n.DispName == "Released");

                    FileArray [] fileCollection = 
                        mgr.DocumentService.GetFilesByMasterIds(e.FileMasterIds);

                    for (int i = 0; i < fileCollection.Length; i++)
                    {
                        CheckFile(fileCollection[i].Files, e.ToStateIds[i], 
                            currentUserId, reviewState, releaseState, e);
                    }
                }
            }
            catch { }
        }

        // checks for a four eyes violation for a given file history
        private void CheckFile(File[] files, long toStateId, 
            long currentUserId, LfCycState reviewState, 
            LfCycState releaseState, WebServiceCommandEventArgs eventArgs)
        {
            // if we are not moving to released, don't event bother with the check
            if (toStateId != releaseState.Id)
                return;

            File maxFile = files.First(n => n.MaxCkInVerNum == n.VerNum);
            if (maxFile.FileRev == null)
                return;

            // gather all the files in the revision and arrange them by version
            IEnumerable<File> filesInRev = 
                from n in files
                where n.FileRev.RevId == maxFile.FileRev.RevId
                orderby n.VerNum
                select n;

            File [] filesArray = filesInRev.ToArray();

            long reviewUserId = -1;
            for (int i = 1; i < filesArray.Length; i++)
            {
                File f1 = filesArray[i-1];
                File f2 = filesArray[i];

                // compare two concecutive file versions to determine 
                // where a state changed happened
                if (f1.FileLfCyc != null && f2.FileLfCyc != null &&
                    f1.FileLfCyc.LfCycStateName != f2.FileLfCyc.LfCycStateName &&
                    f2.VerNum - f1.VerNum == 1)
                {
                    // f2 is a version where the state changed
                    if (f2.FileLfCyc.LfCycStateName == reviewState.DispName)
                        reviewUserId = f2.CreateUserId;
                }
            }

            if (reviewUserId > 0 && currentUserId == reviewUserId)
            {
                // the same person reviewed the file in an earlier version
                eventArgs.AddRestriction(
                    new ExtensionRestriction(maxFile.Name, 
                    "File cannot be reviewed and released by the same person"));
            }
        }

    }
}
