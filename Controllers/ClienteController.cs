
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using System.Threading.Tasks;
    using Vendas.Models; // Substitua pelo namespace do seu projeto
    using Vendas.Data;   // Substitua pelo namespace do seu contexto de dados
    using Microsoft.EntityFrameworkCore;
    using Vendas.Data;

    namespace Vendas.Controllers
{
        public class ClienteController : Controller
        {
            private readonly ApplicationDbContext _context;

            public ClienteController(ApplicationDbContext context)
            {
                _context = context;
            }

            // GET: Cliente (Exibir lista de clientes)
            public async Task<IActionResult> Index()
            {
                var clientes = await _context.Clientes.ToListAsync();
                return View(clientes);
            }

            // GET: Cliente/Details/5 (Exibir detalhes de um cliente)
            public async Task<IActionResult> Details(int? id)
            {
                if (id == null)
                    return NotFound();

                var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Id == id);
                if (cliente == null)
                    return NotFound();

                return View(cliente);
            }

            // GET: Cliente/Create (Formulário para criar cliente)
            public IActionResult Create()
            {
                return View();
            }

            // POST: Cliente/Create (Criar cliente)
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create([Bind("Id,Nome,CPF_CNPJ,Endereco,Telefone,Email,Ativo")] Cliente cliente)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(cliente);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(cliente);
            }

            // GET: Cliente/Edit/5 (Formulário para editar cliente)
            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null)
                    return NotFound();

                var cliente = await _context.Clientes.FindAsync(id);
                if (cliente == null)
                    return NotFound();

                return View(cliente);
            }

            // POST: Cliente/Edit/5 (Editar cliente)
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,CPF_CNPJ,Endereco,Telefone,Email,Ativo")] Cliente cliente)
            {
                if (id != cliente.Id)
                    return NotFound();

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(cliente);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ClienteExists(cliente.Id))
                            return NotFound();
                        else
                            throw;
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(cliente);
            }

            // GET: Cliente/Delete/5 (Confirmar exclusão de cliente)
            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null)
                    return NotFound();

                var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Id == id);
                if (cliente == null)
                    return NotFound();

                return View(cliente);
            }

            // POST: Cliente/Delete/5 (Excluir cliente)
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var cliente = await _context.Clientes.FindAsync(id);
                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            private bool ClienteExists(int id)
            {
                return _context.Clientes.Any(e => e.Id == id);
            }
        }
    }
