using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Runtime.InteropServices;
using ZimmysDevelopmentNuggets.Toolboxes.DocumentManagement;

namespace ZimmysDevelopmentNuggets.Toolboxes.DocumentManagement
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
    public class DocumentManagementToolbox : ToolWindowPane
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClassViewerToolbox"/> class.
        /// </summary>
        public DocumentManagementToolbox(Components.ClassViewerState state) : base(null)
        {
            this.Caption = "DocumentManagementToolbox";

            if (this.Package is AsyncPackage asyncPackage)
            {

            }

            // This is the user control hosted by the tool window; Note that, even if this class implements IDisposable,
            // we are not calling Dispose on this object. This is because ToolWindowPane calls Dispose on
            // the object returned by the Content property.
            this.Content = new DocumentManagementToolboxControl(state);
        }

    }
}
