using ClientManager.DAL.Repository;
using ClientManager.Web.Models;
using ClientManager.DAL.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ClientManager.Domain;

namespace ClientManager.Web.Controllers
{
    public class ShareholderController : Controller
    {
        private readonly IShareholderRepository _shareholderRepository;

        public ShareholderController(IShareholderRepository shareholderRepository)
        {
            _shareholderRepository = shareholderRepository;
        }

        public async Task<IActionResult> Index()
        {
            var shareholders = await _shareholderRepository.GetAll();
            var shareholdersViewModels = shareholders.Select(c => new ShareholderViewModel
            {
                Id = c.Id,
                INN = c.INN,
                FullName = c.FullName,
                CreatedAt = c.CreatedAt,
                Client = c.Client,
            }).ToList();

            return View(shareholdersViewModels);
        }

        public IActionResult Create(int shareholderId)
        {
            var shareholderViewModel = new ShareholderViewModel
            {
                ClientId = shareholderId
            };

            return View(shareholderViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ShareholderViewModel shareholderViewModel)
        {
            if (ModelState.IsValid)
            {
                var shareholder = new Shareholder
                {
                    ClientId = shareholderViewModel.ClientId,
                    INN = shareholderViewModel.INN,
                    FullName = shareholderViewModel.FullName,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };

                await _shareholderRepository.Add(shareholder);

                return RedirectToAction("Details", "Client", new { id = shareholderViewModel.ClientId });
            }

            return View(shareholderViewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var shareholder = await _shareholderRepository.GetById(id);
            if (shareholder == null)
            {
                return NotFound();
            }

            var shareholderViewModel = new ShareholderViewModel
            {
                Id = shareholder.Id,
                INN = shareholder.INN,
                FullName = shareholder.FullName,
            };

            return View(shareholderViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ShareholderViewModel shareholderViewModel)
        {
            if (id != shareholderViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingShareholder = await _shareholderRepository.GetById(id);
                if (existingShareholder == null)
                {
                    return NotFound();
                }

                existingShareholder.INN = shareholderViewModel.INN;
                existingShareholder.FullName = shareholderViewModel.FullName;
                existingShareholder.UpdatedAt = DateTime.Now;

                await _shareholderRepository.Update(existingShareholder);

                return RedirectToAction(nameof(Index));
            }

            return View(shareholderViewModel);
        }

        public async Task<IActionResult> Details(int shareholderId)
        {
            var shareholder = await _shareholderRepository.GetById(shareholderId);
            if (shareholder == null)
            {
                return NotFound();
            }

            var shareholderViewModel = new ShareholderViewModel
            {
                Id = shareholder.Id,
                ClientId = shareholder.ClientId,
                INN = shareholder.INN,
                FullName = shareholder.FullName,
                CreatedAt = shareholder.CreatedAt,
                UpdatedAt = shareholder.UpdatedAt,
                Client = shareholder.Client,
            };

            return View(shareholderViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var shareholder = await _shareholderRepository.GetById(id);
            if (shareholder == null)
            {
                return NotFound();
            }

            await _shareholderRepository.Delete(shareholder);

            return RedirectToAction(nameof(Index));
        }
    }

}
