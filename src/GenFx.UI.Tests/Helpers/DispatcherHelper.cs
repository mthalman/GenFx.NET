using System.Security.Permissions;
using System.Windows.Threading;

namespace GenFx.UI.Tests.Helpers
{
    /// <summary>
    /// Contains helper methods related to the <see cref="Dispatcher"/> class.
    /// </summary>
    public static class DispatcherHelper
    {
        /// <summary>
        /// Invokes all remaining events queued on the <see cref="Dispatcher"/>.
        /// </summary>
        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        public static void DoEvents()
        {
            DispatcherFrame frame = new DispatcherFrame();
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background,
                new DispatcherOperationCallback(ExitFrame), frame);
            Dispatcher.PushFrame(frame);
        }

        private static object ExitFrame(object frame)
        {
            ((DispatcherFrame)frame).Continue = false;
            return null;
        }
    }
}
