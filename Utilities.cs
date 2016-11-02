using System;
using UIKit;
using AudioToolbox; 

namespace DataVault
{
	public class Utilities
	{
		public Utilities()
		{
		}

		AppDelegate appDelegate {
			get {
				return (AppDelegate)UIApplication.SharedApplication.Delegate;
			}
		}

		private void batteryAlert(string title, string message) {
			UIAlertController batteryController = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);

			UIAlertAction confirmed = UIAlertAction.Create("Ok", UIAlertActionStyle.Default,(UIAlertAction obj) => {
				batteryController.Dispose();
			});

			batteryController.AddAction(confirmed); 

			if(this.appDelegate.navController.PresentedViewController == null) {
				this.appDelegate.navController.PresentViewController(batteryController, true, () =>
				{
					SystemSound batteryError = new SystemSound(4095);
					batteryError.PlaySystemSound();
				});
			}
			else {
				this.appDelegate.navController.PresentedViewController.DismissViewController(true, () => {
					this.appDelegate.navController.PresentedViewController.Dispose();
					this.appDelegate.navController.PresentViewController(batteryController, true, () =>
					{
						SystemSound batteryError = new SystemSound(4095);
						batteryError.PlaySystemSound();
					});
				});
			}
		}
		public void BatteryLevel() {
			if (this.appDelegate.batteryEnabled == true)
			{
				UIDevice.CurrentDevice.BatteryMonitoringEnabled = true;
				if (UIDevice.CurrentDevice.BatteryLevel >= 0.3f && UIDevice.CurrentDevice.BatteryLevel <= 0.5f)
				{
					batteryAlert("Low battery", "Your battery is getting low, currently measured at" + (UIDevice.CurrentDevice.BatteryLevel * 100) + "%. You should charge it soon");
				}
			}
			else {
				Console.WriteLine("Battery monitor disabled");
			}
		}
	}
}
