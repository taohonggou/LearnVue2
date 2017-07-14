using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearnVue2.Web.Controllers
{
    public class Vue2Controller : Controller
    {
        // GET: Vue2
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Class 与 Style 绑定
        /// </summary>
        /// <returns></returns>
        public ActionResult ClassAndStyleBind()
        {
            return View();
        }

        /// <summary>
        /// 条件渲染
        /// </summary>
        /// <returns></returns>
        public ActionResult Condition()
        {
            return View();
        }

        public ActionResult Component()
        {
            return View();
        }
    }
}