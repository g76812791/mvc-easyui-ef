﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KCollector.Controllers
{
    public class AddController : Controller
    {
        // GET: Add
        public ActionResult Index(string w)
        {
            ViewBag.message = Common.Common.AddQueue(w).ToString();
            return View();
        }

        // GET: Add/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Add/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Add/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Add/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Add/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Add/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Add/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
