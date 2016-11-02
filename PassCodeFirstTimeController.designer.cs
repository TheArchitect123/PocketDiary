// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace DataVault
{
	[Register ("PassCodeFirstTimeController")]
	partial class PassCodeFirstTimeController
	{
		[Outlet]
		UIKit.UIView mainView { get; set; }

		[Outlet]
		UIKit.UITextField passCodeContinous { get; set; }

		[Outlet]
		UIKit.UITextField passCodeFirst { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (mainView != null) {
				mainView.Dispose ();
				mainView = null;
			}

			if (passCodeContinous != null) {
				passCodeContinous.Dispose ();
				passCodeContinous = null;
			}

			if (passCodeFirst != null) {
				passCodeFirst.Dispose ();
				passCodeFirst = null;
			}
		}
	}
}
