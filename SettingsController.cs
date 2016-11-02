
// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Collections;
using System.Collections.Generic;

using CoreGraphics;
using CoreFoundation;
using Foundation;
using MessageUI;
using UIKit;

namespace DataVault
{
	public partial class SettingsController : UITableViewController
	{
		public SettingsController (IntPtr handle) : base (handle)
		{
		}

		public SettingsController(){}

		UISwitch facialDetection = new UISwitch();
		UISwitch batteryMonitor = new UISwitch();
		UISwitch autoCrashReports = new UISwitch();

		List<int> switchIndices = new List<int>() { };

		public AppDelegate appDelegate { 
			get {
				return (AppDelegate)UIApplication.SharedApplication.Delegate; 
			}
		}

		//about the app
		private void aboutController(string title, string message) {
			UIAlertController _aboutController = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);

			UIAlertAction confirmed = UIAlertAction.Create("Ok", UIAlertActionStyle.Default, (UIAlertAction obj) =>
			{
				_aboutController.Dispose();
			});

			_aboutController.AddAction(confirmed);

			if (this.PresentedViewController == null)
			{
				this.PresentViewController(_aboutController, true, null);
			}
			else {
				this.PresentedViewController.DismissViewController(true, () =>
				{
					this.PresentedViewController.Dispose();
					this.PresentViewController(_aboutController, true, null);
				});
			}
		}


		//error connecting to servers
		private void errorController(string title, string message)
		{
			UIAlertController errorControl = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);

			UIAlertAction confirmed = UIAlertAction.Create("Ok", UIAlertActionStyle.Default, (UIAlertAction obj) =>
			{
				errorControl.Dispose();
			});

			errorControl.AddAction(confirmed);

			if (this.PresentedViewController == null)
			{
				this.PresentViewController(errorControl, true, null);
			}
			else {
				this.PresentedViewController.DismissViewController(true, () =>
				{
					this.PresentedViewController.Dispose();
					this.PresentViewController(errorControl, true, null);
				});
			}
		}


		//share the app
		private void shareController(string title, string message)
		{
			UIAlertController alertShare = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);

			UIAlertAction facebookButton = UIAlertAction.Create("Facebook", UIAlertActionStyle.Default, (Action) =>
			{
				NSUrl facebookURL = NSUrl.FromString("https://www.facebook.com/");

				if (UIApplication.SharedApplication.CanOpenUrl(facebookURL))
				{
					UIApplication.SharedApplication.OpenUrl(facebookURL);
				}
			});

			UIAlertAction twitterButton = UIAlertAction.Create("Twitter", UIAlertActionStyle.Default, (Action) =>
			{
				NSUrl twitterURL = NSUrl.FromString("https://twitter.com/");

				if (UIApplication.SharedApplication.CanOpenUrl(twitterURL))
				{
					UIApplication.SharedApplication.OpenUrl(twitterURL);
				}
			});

			UIAlertAction emailFriend = UIAlertAction.Create("\ud83d\udc8c Email a friend", UIAlertActionStyle.Default, (Action) =>
			{
				MFMailComposeViewController sendEmail = new MFMailComposeViewController();
				if (MFMailComposeViewController.CanSendMail == true)
				{
					this.PresentViewController(sendEmail, true, null);
				}
				else {
					Console.WriteLine("Cannot open MFMailComposeViewController");
				}

				sendEmail.Finished += (object sender, MFComposeResultEventArgs e) =>
				{
					sendEmail.DismissViewController(true, null);
				};
			});

			UIAlertAction textFriend = UIAlertAction.Create("\ud83d\udcf1 Text a Friend", UIAlertActionStyle.Default, (Action) =>
			{
				NSUrl text = NSUrl.FromString("sms:");

				if (UIApplication.SharedApplication.CanOpenUrl(text) == true)
				{
					UIApplication.SharedApplication.OpenUrl(text);
				}
				else if (UIApplication.SharedApplication.CanOpenUrl(text) == false)
				{
					Console.WriteLine("Cannot open message box");
				}
			});

			UIAlertAction close = UIAlertAction.Create("Maybe Later", UIAlertActionStyle.Destructive, (Action) =>
			{
				alertShare.DismissViewController(true, null);
			});

			alertShare.AddAction(facebookButton);
			alertShare.AddAction(twitterButton);
			alertShare.AddAction(emailFriend);
			alertShare.AddAction(textFriend);
			alertShare.AddAction(close);

			if (this.PresentedViewController == null)
			{
				this.PresentViewController(alertShare, true, null);
			}
			else if (this.PresentedViewController != null)
			{
				this.PresentedViewController.DismissViewController(true, () =>
				{
					this.PresentedViewController.Dispose();
					this.PresentViewController(alertShare, true, null);
				});
			}
		}


		//send feedback 
		private void feedbackReports(string title, string message)
		{
			UIAlertController feedback = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);

			UIAlertAction confirmed = UIAlertAction.Create("\ud83d\udc8c Send Feedback Email", UIAlertActionStyle.Default, (Action) =>
			{
				MFMailComposeViewController feedbackMail = new MFMailComposeViewController();
				if (MFMailComposeViewController.CanSendMail == true)
				{
					this.PresentViewController(feedbackMail, true, null);
					feedbackMail.SetToRecipients(new string[] { "dan.developer789@gmail.com" });
				}
				else {
					Console.WriteLine("Cannot open MFMailComposeViewController");
				}

				feedbackMail.Finished += (object sender, MFComposeResultEventArgs e) =>
				{
					feedbackMail.DismissViewController(true, null);
				};
			});

			UIAlertAction denied = UIAlertAction.Create("Never Mind", UIAlertActionStyle.Cancel, (Action) =>
			{
				feedback.DismissViewController(true, null);
			});

			feedback.AddAction(confirmed);
			feedback.AddAction(denied);

			if (this.PresentedViewController == null)
			{
				this.PresentViewController(feedback, true, null);
			}
			else if (this.PresentedViewController != null)
			{
				this.PresentedViewController.DismissViewController(true, () =>
				{
					this.PresentedViewController.Dispose();
					this.PresentViewController(feedback, true, null);
				});
			}
		}

		//report issue: 
		private void reportIssueController(string title, string message)
		{
			UIAlertController feedback = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);

			UIAlertAction confirmed = UIAlertAction.Create("Send Report", UIAlertActionStyle.Default, (Action) =>
			{
				MFMailComposeViewController feedbackMail = new MFMailComposeViewController();
				if (MFMailComposeViewController.CanSendMail == true)
				{
					this.PresentViewController(feedbackMail, true, null);
					feedbackMail.SetToRecipients(new string[] { "dan.developer789@gmail.com" });
				}
				else {
					Console.WriteLine("Cannot open MFMailComposeViewController");
				}

				feedbackMail.Finished += (object sender, MFComposeResultEventArgs e) =>
				{
					feedbackMail.DismissViewController(true, null);
				};
			});

			UIAlertAction denied = UIAlertAction.Create("Never Mind", UIAlertActionStyle.Cancel, (Action) =>
			{
				feedback.DismissViewController(true, null);
			});

			feedback.AddAction(confirmed);
			feedback.AddAction(denied);

			if (this.PresentedViewController == null)
			{
				this.PresentViewController(feedback, true, null);
			}
			else if (this.PresentedViewController != null)
			{
				this.PresentedViewController.DismissViewController(true, () =>
				{
					this.PresentedViewController.Dispose();
					this.PresentViewController(feedback, true, null);
				});
			}
		}

		//Battery monitor
		private void batteryMonitorController(string title, string message)
		{
			UIAlertController batteryController = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);

			UIAlertAction confirmed = UIAlertAction.Create("Yes", UIAlertActionStyle.Default, (UIAlertAction obj) =>
			{
				this.appDelegate.batteryEnabled = true;
				this.batteryMonitor.SetState(true, true);
				if (this.switchIndices.Contains(4) == true)
				{
					Console.WriteLine("Already contains list");
				}
				else {
					this.switchIndices.Add(4);
				}
				this.TableView.ReloadData();
			});

			UIAlertAction denied = UIAlertAction.Create("No", UIAlertActionStyle.Cancel, (UIAlertAction obj) =>
			{
				this.appDelegate.batteryEnabled = false;
				this.batteryMonitor.SetState(false, true);
				try
				{
					int index = this.switchIndices.IndexOf(4);
					if (Double.IsNaN(index) == true)
					{
						throw new ArgumentOutOfRangeException();
					}
					else {
						Console.WriteLine("Battery monitor feature is activated");
						this.switchIndices.RemoveAt(index);
					}
				}
				catch (ArgumentOutOfRangeException)
				{
					Console.WriteLine("No index found battery monitor");
				}
				this.TableView.ReloadData();
			});

			batteryController.AddAction(denied);
			batteryController.AddAction(confirmed);

			if (this.PresentedViewController == null)
			{
				this.PresentViewController(batteryController, true, null);
			}
			else {
				this.PresentedViewController.DismissViewController(true, () =>
				{
					this.PresentedViewController.Dispose();
					this.PresentViewController(batteryController, true, null);
				});
			}
		}

		//Battery monitor
		private void autoCrashReportController(string title, string message)
		{
			UIAlertController autoCrashController = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);

			UIAlertAction confirmed = UIAlertAction.Create("Yes", UIAlertActionStyle.Default, (UIAlertAction obj) =>
			{
				this.autoCrashReports.SetState(true, true);
				if (this.switchIndices.Contains(5) == true)
				{
					Console.WriteLine("Already contains list");
				}
				else {
					this.switchIndices.Add(5);
				}
				this.TableView.ReloadData();
			});

			UIAlertAction denied = UIAlertAction.Create("No", UIAlertActionStyle.Cancel, (UIAlertAction obj) =>
			{
				this.autoCrashReports.SetState(false, true);
				try
				{
					int index = this.switchIndices.IndexOf(5);
					if (Double.IsNaN(index) == true)
					{
						throw new ArgumentOutOfRangeException();
					}
					else {
						Console.WriteLine("Auto crash reports is activated");
						this.switchIndices.RemoveAt(index);
					}
				}
				catch (ArgumentOutOfRangeException)
				{
					Console.WriteLine("No index found battery monitor");
				}
				this.TableView.ReloadData();
			});

			autoCrashController.AddAction(denied);
			autoCrashController.AddAction(confirmed);

			if (this.PresentedViewController == null)
			{
				this.PresentViewController(autoCrashController, true, null);
			}
			else {
				this.PresentedViewController.DismissViewController(true, () =>
				{
					this.PresentedViewController.Dispose();
					this.PresentViewController(autoCrashController, true, null);
				});
			}
		}

		public override void ViewDidLoad()
		{
			if(UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad) {
				this.TableView.RowHeight = 120.0f;
			}
			else {
				this.TableView.RowHeight = 70.0f;
			}
			this.EdgesForExtendedLayout = UIRectEdge.None;
		}

		//create a list of my other apps on the tab bar controller
		List<string> settingsOptions = new List<string>() { 
			{"\ud83d\udcd3 About"},{"\ud83d\udde3 Share"},{"\ud83e\udd14 Send feedback"},{"\ud83d\udd2c Report an issue"},{"\ud83d\udd0b Battery monitor"},{"☠ Automatic crash reports"}
		};

	
		public void switchFaceDetectionChanged() {
			if(this.facialDetection.On) {
				Console.WriteLine("the fuck?");
			}
			else {
				Console.WriteLine("Switch is off!"); 
			}
			
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			UITableViewCell settingsCell = tableView.DequeueReusableCell("SettingsCell");

			if (settingsCell == null)
			{
				settingsCell = new UITableViewCell(UITableViewCellStyle.Value1, "SettingsCell");
			}

			this.batteryMonitor = new UISwitch();
			batteryMonitor.Frame = new CGRect(0, 20, 40, 40);

			this.autoCrashReports = new UISwitch();
			autoCrashReports.Frame = new CGRect(0, 20, 40, 40);

			//battery monitor
			batteryMonitor.AddTarget((object sender, EventArgs e) =>
			{
			if (batteryMonitor.On == false)
			{
					if (this.switchIndices.Contains(4) == true)
					{
						int index = this.switchIndices.IndexOf(4);
						this.switchIndices.RemoveAt(index);
						Console.WriteLine("List already contains 5");
						batteryMonitor.SetState(false, true);
					}
					else {
						batteryMonitor.SetState(true, true);
						batteryMonitorController("Battery Monitor", "Would you like to activate the battery monitor to be notified of when your battery is running low and to allow device performance optimization?");
					}
				}
			}, UIControlEvent.ValueChanged);

			//auto crash reports
			autoCrashReports.AddTarget((object sender, EventArgs e) =>
			{
			if (autoCrashReports.On == false)
			{
					if (this.switchIndices.Contains(5) == true)
					{
						int index = this.switchIndices.IndexOf(5);
						this.switchIndices.RemoveAt(index);
						Console.WriteLine("List already contains 6");
						autoCrashReports.SetState(false, true);
					}
					else {
						autoCrashReports.SetState(true, true);
						autoCrashReportController("Auto Crash Reports", "Would you like to allow the device to automatically send me crash reports in the event that the app crashes? This will help me to address any issues within the app and provide you a better user experience");
					}

			}
				//autoCrashReports.SetState(false, true);
		
		}, UIControlEvent.ValueChanged);

			/*Console.WriteLine("is Facial detection on?:" + facialDetection.On);
			Console.WriteLine("is battery monitor on?:" + batteryMonitor.On);
			Console.WriteLine("is auto crash reports on?:" + autoCrashReports.On);
			*/

			settingsCell.TextLabel.Text = settingsOptions[indexPath.Row];
			settingsCell.TextLabel.TextColor = UIColor.Black;

			if (indexPath.Row >= 4)
			{
				settingsCell.DetailTextLabel.Text = "";
		
				if (indexPath.Row == 4)
				{
					settingsCell.AccessoryView = batteryMonitor;

					if (this.switchIndices.Contains(4) == true)
					{
						batteryMonitor.SetState(true, false);
						Console.WriteLine("4 is contained");
					}
					else {
						batteryMonitor.SetState(false, false);
						Console.WriteLine("4 is removed");
					}
				}
				else if (indexPath.Row == 5)
				{
					settingsCell.AccessoryView = autoCrashReports;

					if (this.switchIndices.Contains(5) == true)
					{
						autoCrashReports.SetState(true, false);
						Console.WriteLine("5 is contained");
					}
					else {
						autoCrashReports.SetState(false, false);
						Console.WriteLine("5 is removed");
					}
				}
			}
			else {
				settingsCell.AccessoryView = null;
				settingsCell.DetailTextLabel.Text = ">";
				settingsCell.DetailTextLabel.TextColor = UIColor.LightGray;
			}
			Console.WriteLine("Switch list count: " + this.switchIndices.Count);
			return settingsCell; 
		}	
	
		public override nint RowsInSection(UITableView tableView, nint section)
		{
			return this.settingsOptions.Count;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			switch(indexPath.Row) {
				case 0: //about the app
					aboutController("Welcome to your diary","This app will help protect all of your diary entry notes with a personal passcode of your choosing"); 
					break;

				case 1: // share the app
					shareController("", "Share this app with your friends!");
					break;

				case 2: //send feedback
					feedbackReports("Send feedback?", "If you have any suggestions for some improvements to this app, then send me an email");
					break;
				case 3:
					reportIssueController("Any problems?", "Send me an email and I will address your issues ASAP");
					break;
				default:
					Console.WriteLine("Invalid selection");
					break;
			}
			tableView.DeselectRow(indexPath, true);
		}
	}
}
