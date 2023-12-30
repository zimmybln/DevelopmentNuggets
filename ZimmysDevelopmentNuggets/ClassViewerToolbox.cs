using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Runtime.InteropServices;

namespace ZimmysDevelopmentNuggets
{
    /// <summary>
    /// This class implements the tool window exposed by this package and hosts a user control.
    /// </summary>
    /// <remarks>
    /// In Visual Studio tool windows are composed of a frame (implemented by the shell) and a pane,
    /// usually implemented by the package implementer.
    /// <para>
    /// This class derives from the ToolWindowPane class provided from the MPF in order to use its
    /// implementation of the IVsUIElementPane interface.
    /// </para>
    /// </remarks>
    [Guid("420ab79b-cc44-4cc5-8f1b-04536950da43")]
    public class ClassViewerToolbox : ToolWindowPane
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClassViewerToolbox"/> class.
        /// </summary>
        public ClassViewerToolbox(ClassViewerState state) : base(null)
        {
            this.Caption = "ClassViewerToolbox";

            if (this.Package is AsyncPackage asyncPackage)
            {

            }

            // This is the user control hosted by the tool window; Note that, even if this class implements IDisposable,
            // we are not calling Dispose on this object. This is because ToolWindowPane calls Dispose on
            // the object returned by the Content property.
            this.Content = new ClassViewerToolboxControl(state);
        }

        

        //protected override void OnCreate()
        //{
        //    var package = this.Package as AsyncPackage;

        //    if (package != null)
        //    {
        //        var service = package.GetService<DTE, SDTE>() as DTE;

        //        if (service != null)
        //        {
        //            service.Events.DocumentEvents.DocumentOpened += delegate (Document document)
        //            {

        //                VsShellUtilities.ShowMessageBox(
        //                    package,
        //                    document.Name + "wurde geöffnet",
        //                    $"Dokument {document.Name} wurde geöffnet",
        //                    OLEMSGICON.OLEMSGICON_INFO,
        //                    OLEMSGBUTTON.OLEMSGBUTTON_OK,
        //                    OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);

        //            };

        //        }
        //    }

        //    base.OnCreate();
        //}

    }
}
