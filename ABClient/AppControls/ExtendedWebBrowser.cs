namespace ABClient.AppControls
{
    using System;
    using System.Drawing;
    using System.Security.Permissions;
    using System.Windows.Forms;

    [ToolboxBitmap(typeof(ExtendedWebBrowser))]
    public class ExtendedWebBrowser : WebBrowser
    {
        private AxHost.ConnectionPointCookie _cookie;
        private WebBrowserExtendedEvents _events;

        internal event EventHandler<WebBrowserExtendedNavigatingEventArgs> BeforeNavigate;
        internal event EventHandler<WebBrowserExtendedNavigatingEventArgs> BeforeNewWindow;
        [System.Runtime.InteropServices.ComImport, System.Runtime.InteropServices.Guid("34A715A0-6587-11D0-924A-0020AFC7AC4D"),
         System.Runtime.InteropServices.InterfaceTypeAttribute(System.Runtime.InteropServices.ComInterfaceType.InterfaceIsIDispatch),
         System.Runtime.InteropServices.TypeLibType(System.Runtime.InteropServices.TypeLibTypeFlags.FHidden)]
        private interface IDWebBrowserEvents2
        {
            [System.Runtime.InteropServices.DispId(250)]
            void BeforeNavigate2(
                [System.Runtime.InteropServices.In, System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.IDispatch)] object pointerDisp,
                [System.Runtime.InteropServices.In] ref object url,
                [System.Runtime.InteropServices.In] ref object flags,
                [System.Runtime.InteropServices.In] ref object targetFrameName, 
                [System.Runtime.InteropServices.In] ref object postData,
                [System.Runtime.InteropServices.In] ref object headers,
                [System.Runtime.InteropServices.In, System.Runtime.InteropServices.Out] ref bool cancel);
            [System.Runtime.InteropServices.DispId(273)]
            void NewWindow3(
                [System.Runtime.InteropServices.In,
                 System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.IDispatch)] object pointerDisp,
                [System.Runtime.InteropServices.In, System.Runtime.InteropServices.Out] ref bool cancel,
                [System.Runtime.InteropServices.In] ref object flags,
                [System.Runtime.InteropServices.In] ref object urlcontext,
                [System.Runtime.InteropServices.In] ref object url);
        }

        [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
        protected override void CreateSink()
        {
            base.CreateSink();
            _events = new WebBrowserExtendedEvents(this);
            _cookie = new AxHost.ConnectionPointCookie(ActiveXInstance, _events, typeof(IDWebBrowserEvents2));
        }

        [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
        protected override void DetachSink()
        {
            if (null != _cookie)
            {
                _cookie.Disconnect();
                _cookie = null;
            }

            base.DetachSink();
        }

#pragma warning disable 628
        protected void OnBeforeNewWindow(string address, out bool cancel)
#pragma warning restore 628
        {
            var h = BeforeNewWindow;
            var args = new WebBrowserExtendedNavigatingEventArgs(address, null);
            if (null != h)
            {
                h(this, args);
            }

            cancel = args.Cancel;
        }

// ReSharper disable MemberCanBePrivate.Local
        protected void OnBeforeNavigate(string address, string frame, out bool cancel)
// ReSharper restore MemberCanBePrivate.Local
        {
            var h = BeforeNavigate;
            var args = new WebBrowserExtendedNavigatingEventArgs(address, frame);
            if (null != h)
            {
                h(this, args);
            }

            cancel = args.Cancel;
        }

// ReSharper disable MemberCanBePrivate.Global
        public sealed class WebBrowserExtendedEvents : System.Runtime.InteropServices.StandardOleMarshalObject, IDWebBrowserEvents2
// ReSharper restore MemberCanBePrivate.Global
        {
            private readonly ExtendedWebBrowser _browser;

            public WebBrowserExtendedEvents(ExtendedWebBrowser extbrowser)
            {
                _browser = extbrowser;
            }

            public void BeforeNavigate2(object pointerDisp, ref object url, ref object flags, ref object targetFrameName, ref object postData, ref object headers, ref bool cancel)
            {
                _browser.OnBeforeNavigate((string)url, (string)targetFrameName, out cancel);
            }

            public void NewWindow3(object pointerDisp, ref bool cancel, ref object flags, ref object urlcontext, ref object url)
            {
                _browser.OnBeforeNewWindow((string)url, out cancel);
            }
        }
    }
}