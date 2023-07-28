using Microsoft.AspNetCore.Mvc;
using RPingGame.Models;
using RPingGame.Utility;

namespace RPingGame.Controllers
{
	public class AccountController : Controller
	{
		private IDb db;
		public AccountController(IDb db)
		{
			this.db = db;
		}
		public IActionResult account()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Insert([Bind("id,pwd,name,phone")] Account obj)
		{
			string error = "";
			obj.date = DateTime.Now.Date("yyyy-MM-dd HH:mm:ss");

			try
			{
				Account.Insert(db, obj);
			}
			catch (Exception ex)
			{
				error = ex.Message;
			}
			return Content("ID" + obj.id + "錯誤" + error);
		}
	}
}
