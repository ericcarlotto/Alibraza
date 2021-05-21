﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Entities.Models;
using PL;
using BLL;
using Microsoft.AspNetCore.Authorization;

namespace SecondHandWeb.Controllers
{
    [Authorize]
    public class ProdutoController : Controller
    {
        private readonly SecondHandContext _context;
        BusinesFacade _bll = new BusinesFacade();

        /*-------------------- TESTE PARA PEGAR USUÁRIO LOGADO -------------------------
        private readonly UserManager<Usuario> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        ------------------------------------------------------------------------------*/

        public ProdutoController()
        {
            _context = new SecondHandContext();
        }

        /*-------------------- TESTE PARA PEGAR USUÁRIO LOGADO (CONSTRUTOR) ------------
        public ProdutoController(UserManager<Usuario> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;

            _context = new SecondHandContext();
        }
        ------------------------------------------------------------------------------*/

        [AllowAnonymous]
        // GET: Produto
        //public async Task<IActionResult> Index()
        public IActionResult Index()
        {

            List<Produto> produtos = _bll.listaDeProdutos();
            //List<Produto> produtos = _bll.ItensPorVendedor();
            return View(produtos);
        }

        /*-------------------- TESTE PARA PEGAR USUÁRIO LOGADO -------------------------
        public async Task<IActionResult> IndexAsync()
        {
            Usuario user = await GetUser();

            List<Produto> produtos = _bll.ItensPorVendedor(user.UsuarioId);
            return View(produtos);
        }
        public async Task<Usuario> GetUser()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            return user;
        }
        ------------------------------------------------------------------------------*/

        [AllowAnonymous]
        // GET: Produto/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos
                .FirstOrDefaultAsync(m => m.ProdutoId == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // GET: Produto/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Produto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProdutoId,Name,Descricao,Estado,Valor,DataEntrada,DataVenda,UsuarioId,Categoria")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(produto);
        }

        // GET: Produto/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            return View(produto);
        }

        // POST: Produto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("ProdutoId,Name,Descricao,Estado,Valor,DataEntrada,DataVenda,UsuarioId,Categoria")] Produto produto)
        {
            if (id != produto.ProdutoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(produto.ProdutoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(produto);
        }

        // GET: Produto/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos
                .FirstOrDefaultAsync(m => m.ProdutoId == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // POST: Produto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoExists(long id)
        {
            return _context.Produtos.Any(e => e.ProdutoId == id);
        }
    }
}