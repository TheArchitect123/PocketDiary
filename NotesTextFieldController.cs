using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UIKit;
using CoreGraphics;
using Foundation;
using CoreFoundation;
using AudioToolbox; 

namespace DataVault
{
	public partial class NotesTextFieldController : UIViewController
	{
		public NotesTextFieldController() : base("NotesTextFieldController", null)
		{
		}

		List<string> textFieldValues = new List<string>() { };

		public UITextField text = new UITextField();
		UINavigationBar navigation = new UINavigationBar();

		public UITextField titleText = new UITextField();

		public AppDelegate applicationDelegate {
			get {
				return (AppDelegate)UIApplication.SharedApplication.Delegate; 
			}
		}

		public override void ViewDidLayoutSubviews()
		{
			if(UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone) {
				if (UIDevice.CurrentDevice.Orientation == UIDeviceOrientation.LandscapeLeft || UIDevice.CurrentDevice.Orientation == UIDeviceOrientation.LandscapeRight)
				{
					navigation.RemoveFromSuperview();
					text.Frame = new CGRect(0, 60, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height);
					navigation.Frame = new CGRect(0, 0, UIScreen.MainScreen.Bounds.Width, 60);
					titleText.Frame = new CGRect(0, 25, 200, 25);
					this.View.AddSubview(navigation);

				}
				else if (UIDevice.CurrentDevice.Orientation == UIDeviceOrientation.Portrait || UIDevice.CurrentDevice.Orientation == UIDeviceOrientation.PortraitUpsideDown)
				{
					navigation.RemoveFromSuperview();
					text.Frame = new CGRect(0, 60, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height);
					navigation.Frame = new CGRect(0, 0, UIScreen.MainScreen.Bounds.Width, 60);
					titleText.Frame = new CGRect(0, 25, 200, 25);
					this.View.AddSubview(navigation);
				}	
			}
			else if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad)
			{
				if (UIDevice.CurrentDevice.Orientation == UIDeviceOrientation.LandscapeLeft || UIDevice.CurrentDevice.Orientation == UIDeviceOrientation.LandscapeRight)
				{
					navigation.RemoveFromSuperview();
					text.Frame = new CGRect(0, 60, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height);
					navigation.Frame = new CGRect(0, 0, UIScreen.MainScreen.Bounds.Width, 60);
					titleText.Frame = new CGRect(0, 25, 200, 25);
					this.View.AddSubview(navigation);
				}
				else if (UIDevice.CurrentDevice.Orientation == UIDeviceOrientation.Portrait || UIDevice.CurrentDevice.Orientation == UIDeviceOrientation.PortraitUpsideDown)
				{
					navigation.RemoveFromSuperview();
					text.Frame = new CGRect(0, 60, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height);
					navigation.Frame = new CGRect(0, 0, UIScreen.MainScreen.Bounds.Width, 60);
					titleText.Frame = new CGRect(0, 25, 200, 25);
					this.View.AddSubview(navigation);
				}
			}
		}

		public override void ViewDidAppear(bool animated)
		{
			this.text.BecomeFirstResponder();
			try
			{
				if (this.applicationDelegate.textFieldNotes[this.applicationDelegate.tableSelected] == null)
				{
					throw new ArgumentOutOfRangeException();
				}
				else {
					if (this.applicationDelegate.isTableSelected == 0)
					{
						text.Text = "";
					}
					else {
						text.Text = this.applicationDelegate.textFieldNotes[this.applicationDelegate.tableSelected];
						titleText.Text = this.applicationDelegate.dataList[this.applicationDelegate.tableSelected];
					}
				}
			}
			catch (ArgumentOutOfRangeException)
			{
				Console.WriteLine("No value exists to render");
			}
		}

		public override void ViewDidLoad()
		{
			this.applicationDelegate.notesController = this;
			//main writing text
			text.Placeholder = "Write something here...";
			text.Frame = new CGRect(0, 60, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height);

			var documentsTextField = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			var fileUsedText = Path.Combine(documentsTextField, "Notes" + this.applicationDelegate.tableSelected + ".txt");

			if (this.applicationDelegate.isTableSelected == 1)
			{
				try
				{
					if (File.ReadAllText(fileUsedText) == null)
					{
						throw new FileNotFoundException();
					}
					else {
						text.Text = File.ReadAllText(fileUsedText);
					}
				}
				catch (FileNotFoundException)
				{
					Console.WriteLine("Cannot find file");
				}
			}

			else if(this.applicationDelegate.isTableSelected == 0) {
				try {
					if(File.ReadAllText(fileUsedText) == null) {
						throw new FileNotFoundException();
					}
					else {
						text.Text = "";		
					}
				}
				catch(FileNotFoundException) {
					Console.WriteLine("Cannot find file");
				}
			}

			this.applicationDelegate.titleText = this.titleText;
			//title text
			titleText.Placeholder = "Title...";
			titleText.Frame = new CGRect(0, 25, 200, 25);
			titleText.BorderStyle = UITextBorderStyle.RoundedRect;

			//titleText.Text = this.applicationDelegate.titlesOfNotes[this.applicationDelegate.tableSelected];

			//this.applicationDelegate.textFieldNotes = this.applicationDelegate.dataList;

			NSError error = new NSError();

			if (NSFileManager.DefaultManager.GetDirectoryContent(documentsTextField, out error).Length != 0)
			{
				for (int i = 0; i <= NSFileManager.DefaultManager.GetDirectoryContent(documentsTextField, out error).Length - 1; i++)
				{
					Console.WriteLine("Iteration is executed here");
					try {
						if(this.applicationDelegate.dataList[i] == null) {
							throw new ArgumentOutOfRangeException();
						}
						else {
							if (this.applicationDelegate.textFieldNotes.Contains(this.applicationDelegate.dataList[i]) == true)
							{
								Console.WriteLine("Value already exists inside this list");
							}
							else {
								this.applicationDelegate.textFieldNotes.Add(this.applicationDelegate.dataList[i]);
							}
						}
					}
					catch(ArgumentOutOfRangeException) {
						Console.WriteLine("No files exist inside the notes controller");
					}
				}
			}
			else {
				Console.WriteLine("da fuck? No values found");
			}

			UIBarButtonItem confirmed = new UIBarButtonItem("Accept", UIBarButtonItemStyle.Plain, (sender, e) =>
			{
				if (this.applicationDelegate.dataList.Count >= 1)
				{
					Console.WriteLine("Null notes executed");
					if (this.View.Subviews.Contains(this.applicationDelegate.nullViewDelegate))
					{
						this.applicationDelegate.nullViewDelegate.RemoveFromSuperview();
					}
					else {
						Console.WriteLine("No null notes listed as subview");
					}
				}

				this.applicationDelegate.cancelInt = 0;

				this.applicationDelegate.searchBar.ResignFirstResponder();
				this.applicationDelegate.searchController.Active = false;

				//user has selected a cell
				if (this.applicationDelegate.isTableSelected == 1)
				{
					Console.WriteLine("Table cell is selected for Accept");
					try
					{
						if (this.applicationDelegate.textFieldNotes[this.applicationDelegate.tableSelected] == null || this.applicationDelegate.textFieldNotes[this.applicationDelegate.tableSelected] == null
						  || this.applicationDelegate.dataList[this.applicationDelegate.tableSelected] == null)
						{
							throw new ArgumentOutOfRangeException();
						}
						else {
							this.applicationDelegate.textFieldNotes.RemoveAt(this.applicationDelegate.tableSelected);
							this.applicationDelegate.textFieldNotes.Insert(this.applicationDelegate.tableSelected, this.text.Text);

							this.applicationDelegate.dataList.RemoveAt(this.applicationDelegate.tableSelected);
							this.applicationDelegate.dataList.Insert(this.applicationDelegate.tableSelected, this.text.Text);
						}
					}
					catch (ArgumentOutOfRangeException)
					{
						Console.WriteLine("Argument out of range exception caught on table view");
					}

					this.applicationDelegate.tableReload.ReloadData();
					this.applicationDelegate.tableView.ReloadData();
				}

				else if (this.applicationDelegate.isTableSelected == 2)
				{
					Console.WriteLine("Search table selected");
					try
					{
						if (this.applicationDelegate.textFieldNotes[this.applicationDelegate.tableSelected] == null || this.applicationDelegate.textFieldNotes[this.applicationDelegate.tableSelected] == null
						  || this.applicationDelegate.dataList[this.applicationDelegate.tableSelected] == null)
						{
							throw new ArgumentOutOfRangeException();
						}
						else {
							this.applicationDelegate.textFieldNotes.RemoveAt(this.applicationDelegate.tableSelected);
							this.applicationDelegate.textFieldNotes.Insert(this.applicationDelegate.tableSelected, this.text.Text);

							this.applicationDelegate.dataList.RemoveAt(this.applicationDelegate.tableSelected);
							this.applicationDelegate.dataList.Insert(this.applicationDelegate.tableSelected, this.text.Text);
						}
					}
					catch (ArgumentOutOfRangeException)
					{
						Console.WriteLine("Argument out of range exception caught on table view");
					}

					this.applicationDelegate.tableReload.ReloadData();
					this.applicationDelegate.tableView.ReloadData();
				}

				//user has just added a new cell
				else if(this.applicationDelegate.isTableSelected == 0) {
					this.applicationDelegate.textFieldNotes.Add(this.text.Text);

					int index = this.applicationDelegate.dataList.Count - 1;
					this.applicationDelegate.dataList.RemoveAt(index);
					this.applicationDelegate.dataList.Insert(index, this.text.Text);

					this.applicationDelegate.tableReload.ReloadData();
				}

				this.DismissViewController(true, () =>
				{
					//retains the file associated with this controller and writes the text in the textfield into the file
					Console.WriteLine("Final index: " + this.applicationDelegate.finalIndex);
					Console.WriteLine("Text Field Notes count: " + this.applicationDelegate.textFieldNotes.Count);
				});
			});
			UIBarButtonItem denied = new UIBarButtonItem("Cancel", UIBarButtonItemStyle.Done, (object sender, EventArgs e) => {
				this.applicationDelegate.cancelInt = 1; 
				this.DismissViewController(true, () =>
				{
					if(this.applicationDelegate.isTableSelected == 1) {
						Console.WriteLine("Cell is selected");
						this.applicationDelegate.tableReload.ReloadData();
					}
					else if(this.applicationDelegate.isTableSelected == 0) {
						this.applicationDelegate.dataList.RemoveAt(this.applicationDelegate.dataList.Count - 1);

						var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
						var FilePath_2 = Path.Combine(documents, "Notes" + (this.applicationDelegate.dataList.Count) + ".txt");

						File.Delete(FilePath_2);
						Console.WriteLine("File Path Cancel: " + FilePath_2);

						this.applicationDelegate.tableReload.ReloadData();
					}
				});	
			});

			confirmed.TintColor = UIColor.Blue;
			denied.TintColor = UIColor.Red;

			text.VerticalAlignment = UIControlContentVerticalAlignment.Top;
			text.HorizontalAlignment = UIControlContentHorizontalAlignment.Left; 

			navigation.Frame = new CGRect(0, 0, UIScreen.MainScreen.Bounds.Width, 60);

			UIApplication.SharedApplication.StatusBarHidden = true; 

			UINavigationItem navItem = new UINavigationItem();
			navItem.LeftBarButtonItem = denied;
			navItem.RightBarButtonItem = confirmed;

			navigation.Items = new UINavigationItem[] {navItem};

			navigation.BarStyle = UIBarStyle.Default;
			navigation.Layer.BorderWidth = 0.3f;
			navigation.Layer.BorderColor = UIColor.Black.CGColor; 

			text.TextColor = UIColor.Black;

			text.EditingChanged += (object sender, EventArgs e) => {
				SystemSound keyboardSound = new SystemSound(1105);
				keyboardSound.PlaySystemSound();
			};

			text.ReturnKeyType = UIReturnKeyType.Done;

			text.ShouldReturn += (UITextField textField) => {
				text.ResignFirstResponder();
				return false;
			};

			titleText.EditingChanged += (object sender, EventArgs e) =>
			{
				SystemSound keyboardSound = new SystemSound(1105);
				keyboardSound.PlaySystemSound();
			};

			titleText.ReturnKeyType = UIReturnKeyType.Done;
			titleText.ShouldReturn += (UITextField textField) =>
			{
				titleText.ResignFirstResponder();
				return false;
			};

			this.View.AddSubview(navigation);
			this.View.AddSubview(text); 
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

