using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using System.Collections.ObjectModel;

namespace frajerborec
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewReminder:ContentPage
	{
		public Reminder Reminder { get; set; }
		public Goal Goal { get; set; }
		public ObservableCollection<Reminder> reminders { get; set; }
		public bool New { get; set; }

		public NewReminder(Goal goal, ObservableCollection<Reminder> reminders, Reminder reminder)
		{
			InitializeComponent();

			this.reminders = reminders;
			Reminder = reminder;
			Goal = goal;
			New = false;

			BindingContext = this;

		}

		public NewReminder(Goal goal, ObservableCollection<Reminder> reminders)
		{
			InitializeComponent();

			this.reminders = reminders;
			Goal = goal;
			Reminder = new Reminder() { GoalID = Goal.ID, Time = new TimeSpan(0, 0, 0)};
			New = true;

			BindingContext = this;
		}

		private void save_reminder(object sender, EventArgs e)
		{
			var db = Database.Db;


			if (New) {
				reminders.Add(Reminder);
				db.Insert(Reminder);
			}
			else {
				reminders[reminders.IndexOf(Reminder)] = Reminder;
				db.Update(Reminder);
			}

			var zajimavequery = db.Query<Reminder>("select * from Reminder where GoalID = " + Goal.ID);
			Navigation.PopAsync();
		}
	}
}