using ClientManager.DAL.Repository.Interfaces;
using ClientManager.Domain;
using ClientManager.Domain.Enum;
using ClientManager.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClientManager.Web.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientRepository _clientRepository;

        public ClientController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<IActionResult> Index()
        {
            var clients = await _clientRepository.GetAll();
            var clientViewModels = clients.Select(c => new ClientViewModel
            {
                Id = c.Id,
                INN = c.INN,
                Name = c.Name,
                Type = c.Type,
                CreatedAt = c.CreatedAt,
            }).ToList();

            return View(clientViewModels);
        }

        public async Task<IActionResult> Details(int id)
        {
            var client = await _clientRepository.GetById(id);
            if (client == null)
            {
                return NotFound();
            }

            var clientViewModel = new ClientViewModel
            {
                Id= client.Id,
                INN = client.INN,
                Name = client.Name,
                Type = client.Type,
                CreatedAt = client.CreatedAt,
                UpdatedAt = client.UpdatedAt,
                Shareholders = client.Shareholders,
            };

            return View(clientViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ClientViewModel clientViewModel)
        {
            if (ModelState.IsValid)
            {
                var client = new Client
                {
                    Id = clientViewModel.Id,
                    INN = clientViewModel.INN,
                    Name = clientViewModel.Name,
                    Type = clientViewModel.Type,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };

                _clientRepository.Add(client);

                return RedirectToAction(nameof(Index));
            }

            return View(clientViewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var client = await _clientRepository.GetById(id);
            if (client == null)
            {
                return NotFound();
            }

            var clientViewModel = new ClientViewModel
            {
                Id = client.Id,
                INN = client.INN,
                Name = client.Name,
                Type = client.Type,
            };

            return View(clientViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ClientViewModel clientViewModel)
        {
            if (id != clientViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingClient = await _clientRepository.GetById(id);
                if (existingClient == null)
                {
                    return NotFound();
                }
                if (clientViewModel.Type == ClientType.IndividualEntrepreneur && existingClient.Shareholders.Count > 1)
                {
                    ModelState.AddModelError(nameof(clientViewModel.Type), "ИП не может иметь 2 и более учредителей.");
                    return View(clientViewModel);
                }


                existingClient.INN = clientViewModel.INN;
                existingClient.Name = clientViewModel.Name;
                existingClient.Type = clientViewModel.Type;
                existingClient.UpdatedAt = DateTime.Now;

                await _clientRepository.Update(existingClient);

                return RedirectToAction(nameof(Index));
            }

            return View(clientViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var client = await _clientRepository.GetById(id);
            if (client == null)
            {
                return NotFound();
            }

            await _clientRepository.Delete(client);

            return RedirectToAction(nameof(Index));
        }
    }
}
