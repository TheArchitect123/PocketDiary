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
	[Register ("PassCodeContinousController")]
	partial class PassCodeContinousController
	{
		[Outlet]
		UIKit.UIView mainView { get; set; }

		[Outlet]
		UIKit.UITextField passCode { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (passCode != null) {
				passCode.Dispose ();
				passCode = null;
			}

			if (mainView != null) {
				mainView.Dispose ();
				mainView = null;
			}
		}
	}
}
