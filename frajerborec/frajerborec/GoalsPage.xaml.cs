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
	public partial class GoalsPage:ContentPage
	{

		public SQLiteConnection db { get; set; }
		public ObservableCollection<Goal> goals { get; set; }

		public GoalsPage()
		{

			BindingContext = this;
			InitializeComponent();

			
			db = Database.Db;

			db.CreateTable<Goal>();


			db.Query<Goal>("DELETE FROM Goal where Name = ''");
			var q = db.Query<Goal>("SELECT * FROM Goal");

			if (q.Count < 1) {
				db.Insert(new Goal() { Name = "Frajer", Days = new Days(), Remind = false });
			}

			goals = new ObservableCollection<Goal>(db.Query<Goal>("SELECT * FROM Goal"));

			gols.ItemsSource = goals;



		}

		private void edit(object sender, EventArgs e)
		{
			Button button = (Button) sender;
			var listViewItem = button.CommandParameter as Goal;
			Navigation.PushAsync(new NewGoal(listViewItem, goals));

		}

		private async void delete(object sender, EventArgs e)
		{
			Button button = (Button) sender;
			var listViewItem = button.CommandParameter as Goal;
			var answer = await DisplayAlert("Delete", "rly nigga", "No", "Yes");

			if (!answer) {
				db.Delete(listViewItem);
				goals.Remove(listViewItem);
			}

		}

		private void new_goal(object sender, EventArgs e)
		{
			Navigation.PushAsync(new NewGoal(goals));
		}
	}
}