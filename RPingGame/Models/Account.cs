using RPingGame.Utility;

namespace RPingGame.Models
{
	public class Account
	{
		public int pk = 0; // int(11) 帳號PK
		public string id { get; set; } // varchar(50) 登入ID
		public string pwd { get; set; } // varchar(50) 登入密碼
		public string type = "2"; // char(2) 帳號類別：1.版主 2.會員
		public string role = "2"; // char(2) 帳號身份：1.管理者 2.一般使用者
		public string name { get; set; } // varchar(50) 名稱
		public string phone { get; set; } // varchar(20) 電話
		public string date = ""; // datetime 建立日期
		public string status = "V"; // char(2) V.啟用

		public static int Insert(IDb db, Account account)
		{
			string sql = @"INSERT INTO account
(id,pwd,type,role,name,phone,date,status)
VALUES
(@id,@pwd,@type,@role,@name,@phone,@date,@status);
SELECT scope_identity();
";
			return (db.ExecuteScalar(sql, new Dictionary<string, object>
			{
				{"@id", account.id},
				{"@pwd", account.pwd},
				{"@type",account.type},
				{"@role",account.role},
				{"@name",account.name},
				{"@phone",account.phone},
				{"@date",account.date},
				{"@status",account.status},
			})).Int();
		}

	}
}
