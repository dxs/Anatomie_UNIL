using Foundation;
using System;
using UIKit;
using Logic;
using System.Collections.Generic;

namespace AnatUNIL
{
    public partial class QuestionController : UIViewController
    {

		public string ViewTitle = "Default";
		private Partie partie;
		public Settings settings;
		private Question question;
		private NSTimer waiter;
		private string s_member;
		private int i_member;
		private string[] listQuestion = null;
		private string realAnswer = null;
		private Random random;
		private int QuestionNb = 0;

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.
			Title.Text = ViewTitle;

			partie = new Partie(ConvertTitleToMembre());
			question = new Question(ConvertMemberToInt());
			question.settings = settings;
			random = new Random();
			realAnswer = string.Empty;
			ProgressBar.Progress = 0;
			SetQuestionNumber();
			SetQuestion();
		}

		void Ticker()
		{
			waiter.Invalidate();
			waiter.Dispose();
			waiter = null;
			Answer1.SetTitleColor(UIColor.White, UIControlState.Normal);
			Answer2.SetTitleColor(UIColor.White, UIControlState.Normal);
			Answer3.SetTitleColor(UIColor.White, UIControlState.Normal);
			Answer4.SetTitleColor(UIColor.White, UIControlState.Normal);
			if (QuestionNb >= settings.nbQuestionToDo)
				GoToResults();
			else
			{
				SetQuestion();
				SetQuestionNumber();
			}
		}

		void GoToResults()
		{
			try
			{
				PerformSegue("ResultSegue", this);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		void SetQuestionNumber()
		{
			if (settings.nbQuestionToDo != 0)
			{
				QuestionToDoField.Text = ++QuestionNb + " of " + settings.nbQuestionToDo;
				ProgressBar.SetProgress((float)QuestionNb/(float)settings.nbQuestionToDo, true);
			}
			else
			{
				QuestionToDoField.Text = string.Empty;
				ProgressBar.SetProgress(0, true);
			}
		}

		void SetQuestion()
		{
			listQuestion = question.getQuestion(partie.getListQuestions);
			if (listQuestion == null)
				return;
			realAnswer = listQuestion[1];
			List<string> q = new List<string>();
			for (int i = 0; i < 4; i++)
				q.Add(listQuestion[i + 1]);
			q = MixArray(q);
			if (q == null)
				return;
			QuestionField.Text = listQuestion[0];
			Answer1.SetTitle(q[0], UIControlState.Normal);
			Answer2.SetTitle(q[1], UIControlState.Normal);
			Answer3.SetTitle(q[2], UIControlState.Normal);
			Answer4.SetTitle(q[3], UIControlState.Normal);
		}

		private List<string> MixArray(List<string> input)
		{
			List<string> randomList = new List<string>();
			int randomIndex = 0;

			while (input.Count > 0)
			{
				randomIndex = random.Next(0, input.Count);
				randomList.Add(input[randomIndex]);
				input.RemoveAt(randomIndex);
			}
			return randomList;
		}

		private string ConvertTitleToMembre()
		{
			switch (ViewTitle)
			{
				case "Membre supérieur": s_member = "supérieur"; break;
				case "Membre inférieur": s_member = "inférieur"; break;
				case "Tronc": s_member = "tronc"; break;
				case "Tout": s_member = "tout"; break;
				default: s_member = "supérieur"; break;
			}
			return s_member;
		}

		private int ConvertMemberToInt()
		{
			switch (ViewTitle)
			{
				case "Membre supérieur": i_member = 1; break;
				case "Membre inférieur": i_member = 2; break;
				case "Tronc": i_member = 3; break;
				case "Tout": i_member = 4; break;
				default: i_member = 1; break;
			}
			return i_member;
		}

		partial void Answer1Click(UIButton sender)
		{
			string answer = sender.Title(UIControlState.Normal);
			if (answer == realAnswer)
			{
				sender.SetTitleColor(UIColor.Green, UIControlState.Normal);
				waiter = NSTimer.CreateScheduledTimer(new TimeSpan(0, 0, 1), delegate { Ticker(); });
			}
			else
				sender.SetTitleColor(UIColor.Red, UIControlState.Normal);
		}

		partial void Answer2Click(UIButton sender)
		{
			string answer = sender.Title(UIControlState.Normal);
			if (answer == realAnswer)
			{
				sender.SetTitleColor(UIColor.Green, UIControlState.Normal);
				waiter = NSTimer.CreateScheduledTimer(new TimeSpan(0, 0, 1), delegate { Ticker(); });
			}
			else
				sender.SetTitleColor(UIColor.Red, UIControlState.Normal);
		}

		partial void Answer3Click(UIButton sender)
		{
			string answer = sender.Title(UIControlState.Normal);
			if (answer == realAnswer)
			{
				sender.SetTitleColor(UIColor.Green, UIControlState.Normal);
				waiter = NSTimer.CreateScheduledTimer(new TimeSpan(0, 0, 1), delegate { Ticker(); });
			}
			else
				sender.SetTitleColor(UIColor.Red, UIControlState.Normal);
		}

		partial void Answer4Click(UIButton sender)
		{
			string answer = sender.Title(UIControlState.Normal);
			if (answer == realAnswer)
			{
				sender.SetTitleColor(UIColor.Green, UIControlState.Normal);
				waiter = NSTimer.CreateScheduledTimer(new TimeSpan(0, 0, 1), delegate { Ticker(); });
			}
			else
				sender.SetTitleColor(UIColor.Red, UIControlState.Normal);
		}

		public QuestionController (IntPtr handle) : base (handle)
        {
        }
	}
}