﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using web.task.bueno.Common.Filters;
using web.task.bueno.Common.Session;
using web.task.bueno.domain;
using web.task.bueno.presentation.Repositories;
using web.task.bueno.presentation.Repositories.Interfaces;
using web.task.bueno.Tools;

namespace web.task.bueno.Controllers
{
    [LoginFilterAtribute]
    public class TareaController : Controller
    {

        private ITareaRepository tareaRepository;
        private WebCurrentSession webCurrentSession;

        public TareaController() {
            tareaRepository = new TareaRepository(Util.getCadenaConexion());
        }

        // GET: Tarea
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            webCurrentSession = new WebCurrentSession(this.HttpContext);

            var listaTareas = await tareaRepository.ListarTareaPorUsuario(1);
            ViewBag.listatareas = listaTareas;
            ViewBag.perfil = webCurrentSession.EsLogeado;

            return View();
        }

        // GET: Tarea/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(int? id)
        {
            if(id is null) return RedirectToAction("Index");

            var tarea = await tareaRepository.obtenerTarea((int)id);

            ViewBag.tarea = tarea;

            return View();
        }

        // GET: Tarea/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tarea/Create
        [HttpPost]
        public async Task<ActionResult> Create(FormCollection collection)
        {
            try
            {
                webCurrentSession = new WebCurrentSession(this.HttpContext);

                string titulo = Convert.ToString(collection["titulo"]);
                string descripcion = Convert.ToString(collection["descripcion"]);
                int idUsuario = Convert.ToInt32(webCurrentSession.EsLogeado.id);

                var tarea = new Tarea();
                tarea.Titulo = titulo;
                tarea.Descripcion = descripcion;
                tarea.IdUsuario = idUsuario;

                var agregar = await tareaRepository.agregarTarea(tarea);

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return View();
            }
        }

        // POST: Tarea/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, FormCollection collection)
        {
            try
            {
                string titulo = Convert.ToString(collection["titulo"]);
                string descripcion = Convert.ToString(collection["descripcion"]);

                var editar = await tareaRepository.editarTarea(id, titulo, descripcion);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return View();
            }
        }


        // POST: Tarea/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            bool eliminar = false;

            try
            {
                string idEnviado = Convert.ToString(id);
                int idTarea = Convert.ToInt32(idEnviado.Replace("eliminar.", ""));
          
                eliminar = await tareaRepository.eliminarTarea(idTarea);

                return Json(eliminar);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return Json(eliminar);
            }
        }
    }
}
