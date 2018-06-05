using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace frajerborec
{
	public class Goal
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string Name { get; set; }

		[TextBlob(nameof(Days))]
		public Days Days { get; set; }
		//public Days DaysBlobbed { get; set; }
		public bool Remind { get; set; }

		public override string ToString() {
			return Name;		
		}
	}
}
