using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace frajerborec
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RemindersPage:ContentPage
	{

		public SQLiteConnection db { get;set; }
		public ObservableCollection<Reminder> reminders { get; set; }
		public Goal Goal { get; set; }

		public RemindersPage(Goal goal)
		{

			BindingContext = this;
			InitializeComponent();

			Goal = goal;

			db = Database.Db;
			db.CreateTable<Reminder>();


			reminders = new ObservableCollection<Reminder>(db.Query<Reminder>("SELECT * FROM Reminder WHERE GoalID = " + goal.ID));
			rems.ItemsSource = reminders;



		}

		private void edit(object sender, EventArgs e)
		{
			Button button = (Button) sender;
			var listViewItem = button.CommandParameter as Reminder;
			Navigation.PushAsync(new NewReminder(Goal, reminders, listViewItem));

		}

		private async void delete(object sender, EventArgs e)
		{
			Button button = (Button) sender;
			var listViewItem = button.CommandParameter as Reminder;
			var answer = await DisplayAlert("Delete", "rly nigga", "No", "Yes");

			if (!answer) {
				db.Delete(listViewItem);
				reminders.Remove(listViewItem);
			}

		}

		private void new_reminder(object sender, EventArgs e)
		{
			Navigation.PushAsync(new NewReminder(Goal, reminders));
		}


	}
}
