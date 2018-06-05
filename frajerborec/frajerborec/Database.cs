using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace frajerborec
{


    public class Database
    {

		private static SQLiteConnection db;

		public static SQLiteConnection Db { get {
				if (db == null) {
					db = new SQLiteConnection(DependencyService.Get<IFileHelper>().GetLocalFilePath("TodoSQLite.db3"));
				}

				return db;
			}
		}



	}
}
