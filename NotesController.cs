// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using AudioToolbox;
using AVFoundation;
using System.Data; 
using CoreGraphics;
using Foundation;
using CoreFoundation; 
using UIKit;

namespace DataVault
{
	public partial class NotesController : UITableViewController
	{
		public NotesController (IntPtr handle) : base (handle)
		{
		}

		public NotesController()
		{
		}

		List<string> data = new List<string>() { };
		List<int> chosenIndices = new List<int>() { };


		UISearchController searchController = new UISearchController(); 
		UISearchBar search = new UISearchBar();
		public AppDelegate appDelegate
		{
			get
			{
				return (AppDelegate)UIApplication.SharedApplication.Delegate;
			}
		}

		public override UITableViewCellEditingStyle EditingStyleForRow(UITableView tableView, NSIndexPath indexPath)
		{
			if (tableView.Editing == true)
			{
				return UITableViewCellEditingStyle.Delete;
			}
			else {
				return UITableViewCellEditingStyle.None;
			}
		}

		public void filteredContent(string searchedText)
		{
			this.appDelegate.newResults = this.appDelegate.dataList.Where((arg) => arg.ToLower().Contains(searchedText.ToLower()) || arg.ToUpper().Contains(searchedText.ToUpper())).ToList();

			this.TableView.ReloadData();

			this.appDelegate.tableView.ReloadData();
		}



		private void errorDelete(string title, string message) {
			UIAlertController errorControlller = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);

			UIAlertAction confirmed = UIAlertAction.Create("Ok", UIAlertActionStyle.Default, (UIAlertAction obj) => {
				errorControlller.Dispose();
			});

			errorControlller.AddAction(confirmed);

			if(this.PresentedViewController == null) {
				this.PresentViewController(errorControlller, true, null);
			}
			else {
				this.PresentedViewController.DismissViewController(true, () =>
				{
					this.PresentedViewController.Dispose();
					this.PresentViewController(errorControlller, true, null);
				});
			}
		}

		public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
		{
			if(tableView.Editing == true) {
				NSError error = new NSError();
				this.appDelegate.dataList.RemoveAt(indexPath.Row);

				var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

				this.chosenIndices.Add(indexPath.Row);

				try {
					if(this.appDelegate.textFieldNotes[indexPath.Row] == null) {
						throw new ArgumentOutOfRangeException();
					}
					else {
						this.appDelegate.textFieldNotes.RemoveAt(indexPath.Row);
					}
				}
				catch(ArgumentOutOfRangeException) {
					Console.WriteLine("Hello exception!");
				}


				var FilePath_2 = Path.Combine(documents, "Notes" + indexPath.Row + ".txt");

				Console.WriteLine("File Path: " + FilePath_2);

				NSUrl url = new NSUrl(FilePath_2);

				this.appDelegate.indexChosen = indexPath.Row;
				Console.WriteLine("Number of files left after deletion: " + NSFileManager.DefaultManager.GetDirectoryContent(documents, out error).Length);

				for (int i = 0; i <= NSFileManager.DefaultManager.GetDirectoryContent(documents, out error).Length - 1;  i++) {
					var FilePathOld = Path.Combine(documents, "Notes" + (indexPath.Row + i - 1) + ".txt");
					var FilePathNew = Path.Combine(documents, "Notes" + (indexPath.Row + i) + ".txt");
					NSFileManager.DefaultManager.Replace(new NSUrl(FilePathOld), new NSUrl(FilePathNew), "Sample Note", NSFileManagerItemReplacementOptions.WithoutDeletingBackupItem, out url, out error);
				}
				//the problem here is that the index the loop gets cut

				//File.Delete(FilePath_2);

				ViewDidAppear(true);

				if (this.appDelegate.dataList.Count == 0)
				{
					this.NavigationItem.SetRightBarButtonItem(this.appDelegate.editingBegan, true);
					this.TableView.SetEditing(false, true);
				}

				this.TableView.ReloadData();
			}
		}

		private void SpeechText(string testedSpeech) {
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

		public override void MotionBegan(UIEventSubtype motion, UIEvent evt)
		{
			if(evt.Type == UIEventType.Motion) {
				this.search.ResignFirstResponder();
			}
		}


		public override void ViewDidAppear(bool animated)
		{
			this.appDelegate.navSharedBar = this.NavigationController.NavigationBar;

			this.TableView.ReloadData();
			Console.WriteLine("Notes list count: " + this.appDelegate.textFieldNotes.Count);
			var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			NSError error_2 = new NSError();

			NSError error = new NSError();
			Console.WriteLine("Total files: " + NSFileManager.DefaultManager.GetDirectoryContent(documents, out error).Length);

			var enumeration = NSFileManager.DefaultManager.GetUrl(NSSearchPathDirectory.DocumentDirectory, NSSearchPathDomain.All, null, false, out error);

			//this.appDelegate.dataList.Add(File.ReadAllText(directory));
			Console.WriteLine("Directory is: " + enumeration);
			//SpeechText("Total encryption keys found on the operating system: " + 0);

			if(this.appDelegate.dataList.Count == 0 && NSFileManager.DefaultManager.GetDirectoryContent(documents, out error).Length >= 1) {
				//SpeechText("No hash values found. Updating data structure");
				this.appDelegate.dataList.Clear();
				for (int i = 0; i <= NSFileManager.DefaultManager.GetDirectoryContent(documents, out error).Length - 1; i++) {
					Console.WriteLine("Statement executed");
					try {
						Console.WriteLine("Update data structure here");
						var fileUsed = Path.Combine(documents, "Notes" + i + ".txt");
						if (File.ReadAllText(fileUsed) == null) {
							throw new FileNotFoundException();
						}
						else {
							this.appDelegate.dataList.Add(File.ReadAllText(fileUsed));
							this.TableView.ReloadData();
						}
					}
					catch(FileNotFoundException) {
						Console.WriteLine("Cannot find the index");
					}
				}
				this.TableView.ReloadData();
			}

			if (this.appDelegate.isTableSelected == 1)
			{
					var file = Path.Combine(documents, "Notes" + this.appDelegate.tableSelected + ".txt");
					File.Delete(file);
					try
					{
					if (this.appDelegate.textFieldNotes[this.appDelegate.tableSelected] == null)
						{
							throw new ArgumentOutOfRangeException();
						}
						else {
						File.WriteAllText(file, this.appDelegate.textFieldNotes[this.appDelegate.tableSelected]);
						}
					}
					catch (ArgumentOutOfRangeException)
					{
						Console.WriteLine("No key found");
					}
			}

			else if (this.appDelegate.isTableSelected == 2) {
				var file = Path.Combine(documents, "Notes" + this.appDelegate.tableSelected + ".txt");
				File.Delete(file);
				try
				{
					if (this.appDelegate.textFieldNotes[this.appDelegate.tableSelected] == null)
					{
						throw new ArgumentOutOfRangeException();
					}
					else {
						File.WriteAllText(file, this.appDelegate.textFieldNotes[this.appDelegate.tableSelected]);
					}
				}
				catch (ArgumentOutOfRangeException)
				{
					Console.WriteLine("Search text value cannot be found");
				}
			}

			else if (this.appDelegate.isTableSelected == 0)
			{
				if (this.appDelegate.notesController.IsBeingDismissed == true)
				{
					if (this.appDelegate.cancelInt == 0)
					{
						for (int i = 0; i <= this.appDelegate.dataList.Count - 1; i++)
						{
							Console.WriteLine("File path execution here");
							var file = Path.Combine(documents, "Notes" + i + ".txt");
							try {
								if(this.appDelegate.textFieldNotes[i] == null) {
									throw new ArgumentOutOfRangeException();
								}
								else {
									File.WriteAllText(file, this.appDelegate.textFieldNotes[i]);
								}
							}
							catch(ArgumentOutOfRangeException) {
								Console.WriteLine("No key found");
							}
						}
					}
					else {
						Console.WriteLine("Cancel button is clicked");
					}
				}
				else {
						Console.WriteLine("Notes controller is not being dismissed");
				}
			}
			//SpeechText("The amount of files in the current directory are: " + NSFileManager.DefaultManager.GetDirectoryContent(documents, out error).Length);
		}

		public override void ViewDidLoad()
		{

			searchController = new UISearchController(new resultsController());
			searchController.SearchResultsUpdater = new searchUpdator(this);
			searchController.DimsBackgroundDuringPresentation = true;
			searchController.HidesNavigationBarDuringPresentation = true;

			this.EdgesForExtendedLayout = UIRectEdge.None;

			this.search = searchController.SearchBar;

			this.appDelegate.searchBar = this.search;
			this.appDelegate.searchController = searchController;

			//sample file 
			NSError error = new NSError();

			var documentsInitial = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
		
			/*if (NSFileManager.DefaultManager.GetDirectoryContent(documentsInitial, out error).Length == 0) {
				var FilePathInitial = Path.Combine(documentsInitial, "Notes0.txt");
				this.appDelegate.textFieldNotes.Add("Sample Notes");
				File.WriteAllText(FilePathInitial, "Sample Notes");
			}*/
			this.TableView.ReloadData();

			this.search.Placeholder = "Search";
			this.search.SearchBarStyle = UISearchBarStyle.Prominent;
			this.search.Frame = new CoreGraphics.CGRect(10, 20, UIScreen.MainScreen.Bounds.Width - 5, 40);

			this.search.SearchButtonClicked += (object sender, EventArgs e) => {
				this.search.ResignFirstResponder();
			};

			this.search.TextChanged += (object sender, UISearchBarTextChangedEventArgs e) =>
			{
				SystemSound keyboardSound = new SystemSound(1105);
				keyboardSound.PlaySystemSound();
			};

			Console.WriteLine("Text Field notes Count: " + this.appDelegate.textFieldNotes.Count);


			this.TableView.TableHeaderView = this.search;

			//toolbar items
			UIBarButtonItem createNewNote = new UIBarButtonItem("+", UIBarButtonItemStyle.Bordered, (object sender, EventArgs e) =>
			{
				this.appDelegate.isTableSelected = 0; 
				//add a new value to the list

				this.appDelegate.dataList.Add("Notes");

				this.TableView.ReloadData();

				NotesTextFieldController notes = new NotesTextFieldController(); 

				if (this.PresentedViewController == null)
				{
					this.PresentViewController(notes, true, null);
				}
				else {
					this.PresentedViewController.DismissViewController(true, () =>
					{
						this.PresentedViewController.Dispose();
						this.PresentViewController(notes, true, null);
					});
				}
			});

			createNewNote.TintColor = UIColor.Blue;

			UITextAttributes createNoteSize = new UITextAttributes();
			createNoteSize.Font = UIFont.SystemFontOfSize(25);

			createNewNote.SetTitleTextAttributes(createNoteSize, UIControlState.Normal);
			createNewNote.SetTitleTextAttributes(createNoteSize, UIControlState.Highlighted);

			this.appDelegate.navItem.SetRightBarButtonItem(createNewNote, false); 

			//this.ToolbarItems = new UIBarButtonItem[] { createNewNote };

			//writing
			UIBarButtonItem bar = new UIBarButtonItem("Edit", UIBarButtonItemStyle.Plain, (object sender, EventArgs e) =>
			{
				if (this.appDelegate.dataList.Count == 0)
				{
					errorDelete("Nothing to delete", "There are no notes here to delete. You must write something in order to delete it");
					if(this.TableView.Editing == true) {
						this.TableView.SetEditing(false, true);
						this.appDelegate.navItem.SetLeftBarButtonItem(this.appDelegate.editingBegan, true);	
					}
				}
				else {
					this.TableView.SetEditing(true, true);
					this.appDelegate.navItem.SetLeftBarButtonItem(appDelegate.barEditing, true);
				}

			});

			UIBarButtonItem editingDone = new UIBarButtonItem("Done", UIBarButtonItemStyle.Done, (object sender, EventArgs e) =>
			{
				this.TableView.SetEditing(false, true);
				this.appDelegate.navItem.SetLeftBarButtonItem(bar, true);
			});

			this.appDelegate.editingBegan = bar;
			this.appDelegate.addButton = editingDone;
			this.appDelegate.addButton = createNewNote; 

			bar.TintColor = UIColor.Red;
			editingDone.TintColor = UIColor.Red;

			appDelegate.barEditing = editingDone;

		
			//reading
			try {
				var documents_2 = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				var filePath = Path.Combine(documents_2, "Notes0.txt");
				var filePath_2 = Path.Combine(documents_2, "Notes0.txt");

				if (filePath_2 == null || filePath == null)
				{
					throw new System.IO.FileNotFoundException();
				}
				else {
					for (int i = 0; i <= NSFileManager.DefaultManager.GetDirectoryContent(documents_2, out error).Length - 1; i++)
					{
						if(NSFileManager.DefaultManager.GetDirectoryContent(documents_2, out error).Contains(Path.Combine(documents_2, "Notes" + i + ".txt")) == true) {
							Console.WriteLine("File alrady exists");
						}
						else {
							//this.appDelegate.dataList.Add(File.ReadAllText(Path.Combine(documents_2, "Notes" + i + ".txt")));	
						}
					}
				}
			}
			catch (FileNotFoundException)
			{
				var documents_2 = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				Console.WriteLine(NSFileManager.DefaultManager.GetDirectoryContent(documents_2, out error).Length);

				for (int i = this.appDelegate.indexChosen + 1; i <= NSFileManager.DefaultManager.GetDirectoryContent(documents_2, out error).Length - 1; i++)
				{
					if (this.chosenIndices.Contains(i) == false)
						{
						try {
							if(File.ReadAllText(Path.Combine(documents_2, "Notes" + i + ".txt")) == null) {
								throw new FileNotFoundException();
							}
							else {
								//this.appDelegate.dataList.Add(File.ReadAllText(Path.Combine(documents_2, "Notes" + i + ".txt")));	
							}
						}
						catch(FileNotFoundException) {
							Console.WriteLine("File not found exception");
						}
					}
					else {
						Console.WriteLine("Indice cannot be found");
					}
				}
				try
				{
					if (NSFileManager.DefaultManager.GetDirectoryContent(documents_2, out error).Last() == null)
					{
						throw new InvalidOperationException();
					}
					else {
						var filePath_2 = Path.Combine(documents_2, "Notes0.txt");
						var documents_3 = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
						Console.WriteLine("Final File amounts: " + NSFileManager.DefaultManager.GetDirectoryContent(documents_3, out error).Length);

						//var file = Path.GetFullPath(NSFileManager.DefaultManager.GetDirectoryContent(documents_3, out error).Last()).TrimEnd();
						//string lastMessage = File.ReadAllText(file);
						//this.data.Add(lastMessage);
					}
				}
				catch(InvalidOperationException) {
				}

				Console.WriteLine("File nt found");
			}

			this.appDelegate.titlesOfNotes = this.appDelegate.dataList; 

			this.EdgesForExtendedLayout = UIRectEdge.None;

			this.appDelegate.finalIndex = this.appDelegate.dataList.Count - 1; 

			this.appDelegate.navItem.SetLeftBarButtonItem(bar, true);
			this.appDelegate.navItem.Title = "My Diary";

			this.appDelegate.tableReload = this.TableView;
			Console.WriteLine("App Delegate Table count: " + this.appDelegate.dataList.Count);

			UIView nullNotes = new UIView();
			this.appDelegate.nullViewDelegate = nullNotes;

			if (this.appDelegate.dataList.Count == 0)
			{
				nullNotes.Frame = new CGRect(0, 50, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height);
				nullNotes.BackgroundColor = UIColor.White;

				UIImageView imageNull = new UIImageView();
				imageNull.Frame = new CGRect(30, 80, UIScreen.MainScreen.Bounds.Width - 10.0f, UIScreen.MainScreen.Bounds.Height / 3.0f);
				imageNull.Image = new UIImage("Number_2.png");
				imageNull.ContentMode = UIViewContentMode.ScaleAspectFit;

				UILabel description = new UILabel();
				description.Frame = new CGRect(5, this.View.Bounds.Bottom - 50.0f, UIScreen.MainScreen.Bounds.Width - 10.0f, 50.0f);
				description.TextColor = UIColor.GroupTableViewBackgroundColor;
				description.Text = "No notes found. To write a new note please click on +";
				description.AdjustsFontSizeToFitWidth = true;
				nullNotes.AddSubview(imageNull);
				nullNotes.AddSubview(description);

				//this.TableView.SectionIndexColor = UIColor.White;
				//this.TableView.TintColor = UIColor.White; 

				//this.View.AddSubview(nullNotes);
			}
		}
		public override nint RowsInSection(UITableView tableView, nint section)
		{
			return this.appDelegate.dataList.Count;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			UITableViewCell notesCell = tableView.DequeueReusableCell("Cell");

			//appDelegate.index = indexPath.Row;
			if (notesCell == null)
			{
				notesCell = new UITableViewCell(UITableViewCellStyle.Value1, "Cell");
			}

			this.appDelegate.tableCell = notesCell;
			notesCell.TextLabel.Text = this.appDelegate.dataList[indexPath.Row];
			notesCell.TextLabel.TextColor = UIColor.Black;

			notesCell.DetailTextLabel.Text = ">";
			notesCell.DetailTextLabel.TextColor = UIColor.LightGray;

			return notesCell;
			
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			NotesTextFieldController notes = new NotesTextFieldController();

			this.appDelegate.tableSelected = indexPath.Row; 
			if(this.PresentedViewController == null) {
				this.PresentViewController(notes, true, null);
			}
			else {
				this.PresentedViewController.DismissViewController(true, () => {
					this.PresentedViewController.Dispose();
					this.PresentViewController(notes, true, null);
				});
			}

			this.appDelegate.isTableSelected = 1; 
			tableView.DeselectRow(indexPath, true);
		}
	}

	public class searchUpdator : UISearchResultsUpdating
	{
		NotesController search = new NotesController();

		public searchUpdator(NotesController searchValue)
		{
			search = searchValue;
		}

		public override void UpdateSearchResultsForSearchController(UISearchController searchController)
		{
			search.filteredContent(searchController.SearchBar.Text);
		}
	}

	public class resultsController : UITableViewController
	{

		public AppDelegate appDelegate
		{
			get
			{
				return (AppDelegate)UIApplication.SharedApplication.Delegate;
			}
		}

		//controller used to display results
		UISearchController search = new UISearchController();
		List<string> filteredResults = new List<string>() { "" };

		public resultsController() { }

		public resultsController(UISearchController searchController)
		{
			search = searchController;
		}

		public resultsController(UISearchController searchController, List<string> filteredResultsRef)
		{
			search = searchController;
			filteredResults = filteredResultsRef;
		}

		public override nint RowsInSection(UITableView tableView, nint section)
		{
			return this.appDelegate.newResults.Count;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			NotesTextFieldController notes = new NotesTextFieldController();

			int indexUsed = this.appDelegate.textFieldNotes.IndexOf(tableView.CellAt(indexPath).TextLabel.Text);

			if(indexUsed == -1) {
				this.appDelegate.tableSelected = indexUsed + 1;
			}
			else {
				this.appDelegate.tableSelected = indexUsed;
			}

			Console.WriteLine("Searched text " + tableView.CellAt(indexPath).TextLabel.Text);
			Console.WriteLine("Index Search table is " + indexUsed);

			if (this.PresentedViewController == null)
			{
				this.PresentViewController(notes, true, null);
			}
			else {
				this.PresentedViewController.DismissViewController(true, () =>
				{
					this.PresentedViewController.Dispose();
					this.PresentViewController(notes, true, null);
				});
			}

			this.appDelegate.isTableSelected = 2;
			tableView.DeselectRow(indexPath, true);
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			UITableViewCell cellSearch = new UITableViewCell(UITableViewCellStyle.Value1, "SearchCell");

			if (cellSearch == null)
			{
				cellSearch = new UITableViewCell(UITableViewCellStyle.Value1, "SearchCell");
			}

			cellSearch.TextLabel.Text = this.appDelegate.newResults[indexPath.Row];
			cellSearch.TextLabel.TextColor = UIColor.Black;

			cellSearch.DetailTextLabel.Text = ">";
			cellSearch.DetailTextLabel.TextColor = UIColor.LightGray;

			return cellSearch;
		}

		public override void ViewDidAppear(bool animated)
		{
			this.TableView.Frame = new CGRect(0, -40, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height);

			this.EdgesForExtendedLayout = UIRectEdge.None;

			this.TableView.ContentMode = UIViewContentMode.Top;

			this.appDelegate.tableView = this.TableView;

			this.TableView.ReloadData();

		}

		public override void ViewDidLoad()
		{
			this.EdgesForExtendedLayout = UIRectEdge.Top;
			this.TableView.ContentMode = UIViewContentMode.Top;
		}

		public override void TouchesEnded(NSSet touches, UIEvent evt)
		{
			if (evt.Type == UIEventType.Touches)
			{
				Console.WriteLine("Table view count: " + this.appDelegate.newResults.Count);
			}
		}
	}
}
