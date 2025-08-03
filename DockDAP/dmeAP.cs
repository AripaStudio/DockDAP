using Microsoft.VisualStudio.Shell;
using System;
using System.Runtime.InteropServices;

namespace DockDAP
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
    [Guid("26a463a5-a9d5-479e-a107-e6a22c75d47a")]
    public class dmeAP : ToolWindowPane
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="dmeAP"/> class.
        /// </summary>
        public dmeAP() : base(null)
        {
            this.Caption = "dmeAP";

            // This is the user control hosted by the tool window; Note that, even if this class implements IDisposable,
            // we are not calling Dispose on this object. This is because ToolWindowPane calls Dispose on
            // the object returned by the Content property.
            this.Content = new dmeAPControl();
        }
    }
}
