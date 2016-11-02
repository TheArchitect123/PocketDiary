using Foundation;
using UIKit;
using AudioToolbox;

using System; 
using System.Collections;
using System.Collections.Generic;
using System.Linq; 
using System.IO;

using AVFoundation; 

namespace DataVault
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
	[Register("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations

		public override UIWindow Window
		{
			get;
			set;
		}

		public UINavigationItem navItem = new UINavigationItem();

		public bool batteryEnabled; 
		//index used to track the final value inside the table view 
		public int finalIndex;

		//navigation bar to share the navigation of the notes controller and the results search controller
		public UINavigationBar navSharedBar = new UINavigationBar();
		public int cancelInt; 
		public List<string> dataList = new List<string>() { };

		public VaultController vaultController = new VaultController(); 
		public UITableView tableView = new UITableView();

		//search results string carrying values that are to be rendered by the table view
		public List<string> newResults = new List<string>() { };

		public List<string> textFieldNotes = new List<string>() { };

		public UIBarButtonItem editingBegan = new UIBarButtonItem();
		public UIBarButtonItem editingEnded = new UIBarButtonItem();
		public UIBarButtonItem addButton = new UIBarButtonItem();

		public UITextField titleText = new UITextField();

		public NotesTextFieldController notesController = new NotesTextFieldController();
		public int tableSelected;

		public UIBarButtonItem barEditing = new UIBarButtonItem();
		public string filePath;

		public string titleName;

		public int isTableSelected; 
		public UITableViewCell tableCell = new UITableViewCell();

		public UIView nullViewDelegate = new UIView();

		public UITextField textFieldData = new UITextField();
		public List<string> filePathsList = new List<string>() { };
		public List<string> titlesOfNotes = new List<string>() { };

		public UITableView tableReload = new UITableView();

		public UITabBar tabBar = new UITabBar();
		public UISearchBar searchBar = new UISearchBar();
		public UISearchController searchController = new UISearchController();

		public UINavigationController navController = new UINavigationController(); 

		public int index; 
		public int indexChosen;

		public string password; 

		private void SpeechText(string testedSpeech)
		{
			AVSpeechSynthesizer speech = new AVSpeechSynthesizer();

			AVSpeechUtterance speechUtterance = new AVSpeechUtterance(testedSpeech)
			{
				Rate = AVSpeechUtterance.MaximumSpeechRate / 2.0f,
				Voice = AVSpeechSynthesisVoice.FromLanguage("en-US"),
				Volume = 1.0f,
				PitchMultiplier = 1.0f
			};

			speech.SpeakUtterance(speechUtterance);
		}


		public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
		{
			//vault alert needs to play on full sound whether sound is on or off 
			var audio = AVAudioSession.SharedInstance();

			var memoryCache = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			var filePathPassword = Path.Combine(memoryCache, "password.txt");

			Utilities utility = new Utilities();

			//local notifications 
			var settings = UIUserNotificationSettings.GetSettingsForTypes(UIUserNotificationType.Alert | UIUserNotificationType.Sound, null);
			UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);

			var batteryNotification = new UILocalNotification();
			NSDate.FromObject(UIDevice.BatteryLevelDidChangeNotification);
			UIApplication.SharedApplication.ScheduleLocalNotification(batteryNotification);

			NSError error = new NSError();
			//checks for security key 

			try
			{
				if (File.ReadAllText(filePathPassword) == null)
				{
					throw new FileNotFoundException();
				}
				else {
					//password is already set 
					Console.WriteLine("Password exist");
					PassCodeContinousController passcodeContinous = new PassCodeContinousController();
					if (this.navController.VisibleViewController != passcodeContinous)
					{
						Console.WriteLine("Current controller presented: " + this.navController.VisibleViewController);
						if (this.navController.ViewControllers.Contains(passcodeContinous) == true)
						{
							this.navController.PopToViewController(passcodeContinous, true);
						}
						else {
							this.navController.SetViewControllers(new UIViewController[] { passcodeContinous }, true);
						}
					}
					else if (this.navController.VisibleViewController == passcodeContinous)
					{
						Console.WriteLine("Security lock is already presented");
					}
				}
			}
			catch(FileNotFoundException) {
				NSUserDefaults.StandardUserDefaults.SetBool(true, "passcode");

				Console.WriteLine("Password is null");
				PassCodeFirstTimeController passCode = new PassCodeFirstTimeController();

				if (this.navController.VisibleViewController != passCode)
				{
					Console.WriteLine("Current controller presented: " + this.navController.VisibleViewController);
					if (this.navController.ViewControllers.Contains(passCode) == true)
					{
						this.navController.PopToViewController(passCode, true);
					}
					else {
						this.navController.SetViewControllers(new UIViewController[] { passCode }, true);
					}
				}
				else if(this.navController.VisibleViewController == passCode) {
					Console.WriteLine("Security lock is already presented");
				}
				NSUserDefaults.StandardUserDefaults.Synchronize();
			}
			//this.Window.RootViewController = passCode;
			return true;
		}

		//when device is locked
		public override void ProtectedDataWillBecomeUnavailable(UIApplication application)
		{
		//	base.ProtectedDataWillBecomeUnavailable(application);
			SystemSound soundLock = new SystemSound(1100);
			soundLock.PlaySystemSound();
		}

		//when device is unlocked
		public override void ProtectedDataDidBecomeAvailable(UIApplication application)
		{
			//base.ProtectedDataDidBecomeAvailable(application);
		}

		public override void ReceivedLocalNotification(UIApplication application, UILocalNotification notification)
		{
			Utilities utility = new Utilities();
			utility.BatteryLevel();
		}

		public override void OnResignActivation(UIApplication application)
		{
			// Invoked when the application is about to move from active to inactive state.
			// This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
			// or when the user quits the application and it begins the transition to the background state.
			// Games should use this method to pause the game.
			var memoryCache = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			var filePathPassword = Path.Combine(memoryCache, "password.txt");
			NSError error = new NSError();

			try
			{
				if (File.ReadAllText(filePathPassword) == null)
				{
					throw new FileNotFoundException();
				}
				else {
					//password is already set 
					Console.WriteLine("Password exist");
					PassCodeContinousController passcodeContinous = new PassCodeContinousController();
					if (this.navController.VisibleViewController != passcodeContinous)
					{
						Console.WriteLine("Current controller presented: " + this.navController.VisibleViewController);
						if (this.navController.ViewControllers.Contains(passcodeContinous) == true)
						{
							this.navController.PopToViewController(passcodeContinous, true);
						}
						else {
							this.navController.SetViewControllers(new UIViewController[] { passcodeContinous }, true);
						}
					}
					else if (this.navController.VisibleViewController == passcodeContinous)
					{
						Console.WriteLine("Security lock is already presented");
					}
				}
			}
			catch (FileNotFoundException)
			{
				Console.WriteLine("Password is null");
				PassCodeFirstTimeController passCode = new PassCodeFirstTimeController();
				if (this.navController.VisibleViewController != passCode)
				{
					Console.WriteLine("Current controller presented: " + this.navController.VisibleViewController);
					if (this.navController.ViewControllers.Contains(passCode) == true)
					{
						this.navController.PopToViewController(passCode, true);
					}
					else {
						this.navController.SetViewControllers(new UIViewController[] { passCode }, true);
					}
				}
				else if (this.navController.VisibleViewController == passCode)
				{
					Console.WriteLine("Security lock is already presented");
				}
			}
		}

		public override void DidEnterBackground(UIApplication application)
		{
			// Use this method to release shared resources, save user data, invalidate timers and store the application state.
			// If your application supports background exection this method is called instead of WillTerminate when the user quits.

			SystemSound soundLock = new SystemSound(1100);
			soundLock.PlaySystemSound();

			NSError error = new NSError();
			var memoryCache = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			var filePathPassword = Path.Combine(memoryCache, "password.txt");


			try
			{
				if (File.ReadAllText(filePathPassword) == null)
				{
					throw new FileNotFoundException();
				}
				else {
					//password is already set 
					Console.WriteLine("Password exist");
					PassCodeContinousController passcodeContinous = new PassCodeContinousController();
					if (this.navController.VisibleViewController != passcodeContinous)
					{
						Console.WriteLine("Current controller presented: " + this.navController.VisibleViewController);
						if (this.navController.ViewControllers.Contains(passcodeContinous) == true)
						{
							this.navController.PopToViewController(passcodeContinous, true);
						}
						else {
							this.navController.SetViewControllers(new UIViewController[] { passcodeContinous }, true);
						}
					}
					else if (this.navController.VisibleViewController == passcodeContinous)
					{
						Console.WriteLine("Security lock is already presented");
					}
				}
			}
			catch (FileNotFoundException)
			{
				Console.WriteLine("Password is null");
				PassCodeFirstTimeController passCode = new PassCodeFirstTimeController();
				if (this.navController.VisibleViewController != passCode)
				{
					Console.WriteLine("Current controller presented: " + this.navController.VisibleViewController);
					if (this.navController.ViewControllers.Contains(passCode) == true)
					{
						this.navController.PopToViewController(passCode, true);
					}
					else {
						this.navController.SetViewControllers(new UIViewController[] { passCode }, true);
					}
				}
				else if (this.navController.VisibleViewController == passCode)
				{
					Console.WriteLine("Security lock is already presented");
				}
			}
		}

		public override void WillEnterForeground(UIApplication application)
		{
			// Called as part of the transiton from background to active state.
			// Here you can undo many of the changes made on entering the background.
			//checks for security key 

			NSError error = new NSError();
			var memoryCache = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			var filePathPassword = Path.Combine(memoryCache, "password.txt");
			                             
		
			try
			{
				if (File.ReadAllText(filePathPassword) == null)
				{
					throw new FileNotFoundException();
				}
				else {
					//password is already set 
					Console.WriteLine("Password exist");
					PassCodeContinousController passcodeContinous = new PassCodeContinousController();
					//this.navController.SetViewControllers(new UIViewController[] { passcodeContinous }, true);
				}
			}
			catch (FileNotFoundException)
			{
				Console.WriteLine("Password is null");
				PassCodeFirstTimeController passCode = new PassCodeFirstTimeController();
				//this.navController.PopToViewController(passCode, true);
			}
		}


		public override void OnActivated(UIApplication application)
		{
			// Restart any tasks that were paused (or not yet started) while the application was inactive. 
			// If the application was previously in the background, optionally refresh the user interface.

			NSError error = new NSError();

			var memoryCache = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			var filePathPassword = Path.Combine(memoryCache, "password.txt");
			//checks for security key 


			try
			{
				if (File.ReadAllText(filePathPassword) == null)
				{
					throw new FileNotFoundException();
				}
				else {
					//password is already set 
					Console.WriteLine("Password exist");
					PassCodeContinousController passcodeContinous = new PassCodeContinousController();
					if (this.navController.VisibleViewController != passcodeContinous)
					{
						if (this.navController.ViewControllers.Contains(passcodeContinous) == true)
						{
							this.navController.PopToViewController(passcodeContinous, true);
						}
						else {
							this.navController.SetViewControllers(new UIViewController[] { passcodeContinous }, true);
						}
					}
					else if (this.navController.VisibleViewController == passcodeContinous)
					{
						Console.WriteLine("Security lock is already presented");
					}
				}
			}
			catch (FileNotFoundException)
			{
				Console.WriteLine("Password is null");
				PassCodeFirstTimeController passCode = new PassCodeFirstTimeController();
				if (this.navController.VisibleViewController != passCode)
				{
					if (this.navController.ViewControllers.Contains(passCode) == true)
					{
						this.navController.PopToViewController(passCode, true);
					}
					else {
						this.navController.SetViewControllers(new UIViewController[] { passCode }, true);
					}
				}
				else if (this.navController.VisibleViewController == passCode)
				{
					Console.WriteLine("Security lock is already presented");
				}
			}
		}

		public override void WillTerminate(UIApplication application)
		{
			// Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
		}
	}
}

