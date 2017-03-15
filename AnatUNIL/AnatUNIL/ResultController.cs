﻿using System;
using System.Collections.Generic;
using Foundation;
using Logic;
using UIKit;

namespace AnatUNIL
{
	public partial class ResultController : UIViewController
	{
		public Partie _partie;
		public Settings _settings;

		public ResultController() : base("ResultController", null)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			MyTableView.Source = new TableViewSource(_partie, this);
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		public ResultController(IntPtr handle) : base (handle)
        {
		}
	}

	public class TableViewSource : UITableViewSource
	{
		string CellIdentifier = "TableCell";
		Partie partie;
		ResultController mybase;

		public TableViewSource(Partie _partie, ResultController _base)
		{
			partie = _partie;
			mybase = _base;
		}

		void SendMail(UIAlertAction obj)
		{

		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			var okAlertController = UIAlertController.Create("Un correctif?", partie.getListQuestions[indexPath.Row], UIAlertControllerStyle.Alert);
			okAlertController.AddAction(UIAlertAction.Create("Oui", UIAlertActionStyle.Default, SendMail));
			okAlertController.AddAction(UIAlertAction.Create("Non", UIAlertActionStyle.Cancel,null));
			mybase.PresentViewController(okAlertController, true, null);

    		tableView.DeselectRow(indexPath, true);
		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return partie.getListAnswer.Count;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			UITableViewCell cell = tableView.DequeueReusableCell(CellIdentifier);

			//---- if there are no cells to reuse, create a new one
			if (cell == null)
			{ cell = new UITableViewCell(UITableViewCellStyle.Subtitle, CellIdentifier) { BackgroundColor = new UIColor(0, 0) }; }

			string item = partie.getListQuestions[indexPath.Row];
			item = item.Split(new string[] { "muscles", "muscle", "?" }, StringSplitOptions.RemoveEmptyEntries)[1];
			cell.TextLabel.Text = item;
			cell.TextLabel.TextColor = UIColor.White;

			item = partie.getListQuestions[indexPath.Row];
			if (item.Contains("origine")) item = "origine";
			if (item.Contains("terminaison")) item = "terminaison";
			if (item.Contains("innervation")) item = "innervation";
			if (item.Contains("mouvement")) item = "mouvement";

			cell.DetailTextLabel.Text = item;
			if (partie.getListAnswer[indexPath.Row] == partie.getListHisAnswer[indexPath.Row])
				cell.DetailTextLabel.TextColor = UIColor.Green;
			else
				cell.DetailTextLabel.TextColor = UIColor.Red;

			cell.Accessory = UITableViewCellAccessory.DetailButton;

			return cell;
		}

		public override void AccessoryButtonTapped(UITableView tableView, NSIndexPath indexPath)
		{
			UIAlertController okAlertController = UIAlertController.Create("Détail sur : " + partie.getListQuestions[indexPath.Row], "La réponse était : " + partie.getListAnswer[indexPath.Row] + " et vous avez répondu : " + partie.getListHisAnswer[indexPath.Row], UIAlertControllerStyle.Alert);
			okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
			mybase.PresentViewController(okAlertController, true, null);

			tableView.DeselectRow(indexPath, true);
		}
	}
}