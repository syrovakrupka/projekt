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
	public partial class NewGoal : ContentPage
	{

		public Goal Goal { get; set; }
		public ObservableCollection<Goal> goals { get; set; }


		public NewGoal ()
		{
			InitializeComponent();

			var db = Database.Db;

			Goal = new Goal() { Name = "", Days = new Days(), Remind = false, };
			db.Insert(Goal);

			BindingContext = this;
		}

		public NewGoal(Goal goal, ObservableCollection<Goal> goals)
		{

			InitializeComponent();
			Goal = goal;
			this.goals = goals;
			BindingContext = this;
		}

		public NewGoal(ObservableCollection<Goal> goals) {

			InitializeComponent();
			Goal = new Goal() { Name = "", Days = new Days(), Remind = false, };
			this.goals = goals;
			BindingContext = this;
		}

		private void set_clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new RemindersPage(Goal));
		}

		private void save(object sender, EventArgs e)
		{
			var db = Database.Db;
			db.InsertOrReplace(Goal);

			if (goals != default(ObservableCollection<Goal>)) {
				if (goals.Contains(Goal)) goals[goals.IndexOf(Goal)] = Goal;
				else goals.Add(Goal);
			}
		

			var query = db.Query<Goal>("select * from Goal");
			Navigation.PopAsync();
		}
	}
}