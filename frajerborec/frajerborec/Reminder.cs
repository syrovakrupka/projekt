using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace frajerborec
{
    public class Reminder
    {
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public int GoalID { get; set; }
		public TimeSpan Time { get; set; }
		public string TimeString {
			get {
				return Time.ToString(@"hh\:mm");
			}
		}

    }
}
