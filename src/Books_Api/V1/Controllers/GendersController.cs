using AutoMapper;
using Books_Api.Controllers;
using Books_Api.Services;
using Books_Api.ViewModels;
using Books_Business.Core.Notifications;
using Books_Business.Modules.Books;
using Books_Business.Modules.Genders;
using Books_Business.Modules.Users;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Books_Api.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/genders")]
    public class GendersController : MainController
    {
        private readonly IGenderRepository _repository;
        private readonly IGenderService _service;

        private readonly IBookRepository _bookRepository;

        private readonly IMapper _mapper;

        public GendersController(
            IGenderRepository repository,
            IGenderService service,
            IBookRepository bookRepository,
            IMapper mapper,
            INotifier notifier,
            IUser appUser) : base(notifier, appUser)
        {
            _repository = repository;
            _service = service;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<PagedDTO<GenderInput>> Get(int ps = 5, int p = 1, string q = null)
        {
            var result = await _repository.GetPaged(ps, p, q);

            return new PagedDTO<GenderInput>
            {
                List = _mapper.Map<IEnumerable<GenderInput>>(result.List),
                Total = result.Total,
                Pages = result.Pages
            };
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> Get(int id)
        {
            var vm = await GetById(id);
            if (vm == null)
                return NotFound();

            return Ok(new GenderDetails
            {
                Gender = vm,
                Books = new BookMap().FromBookToBookView(await _bookRepository.Search(b => b.GenderId == id))
            });
        }

        [HttpGet("api/genders/getSummary")]
        public async Task<IEnumerable<KeyValuePair<int, string>>> GetSummary()
        {
            return await _repository.GetSummary();
        }

        [HttpPost]
        public async Task<ActionResult> Add(GenderInput vm)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _service.Add(_mapper.Map<Gender>(vm));

            //return CreatedAtRoute("DefaultApi", new { id = vm.Id }, vm);
            return CustomResponse(vm);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, GenderInput vm)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (id != vm.Id)
            {
                NotifyError("The ID provided is not the same as the one registered.");
                return CustomResponse(vm);
            }

            await _service.Update(_mapper.Map<Gender>(vm));

            return CustomResponse();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Remove(int id)
        {
            var vm = await GetById(id);
            if (vm == null)
                return NotFound();

            await _service.Remove(id);

            return CustomResponse();
        }

        private async Task<GenderInput> GetById(int id)
        {
            return _mapper.Map<GenderInput>(await _repository.GetById(id));
        }

        protected void Dispose(bool disposing)//override
        {
            if (disposing)
            {
                _repository?.Dispose();
                _service?.Dispose();

                _bookRepository?.Dispose();
            }

            //base.Dispose(disposing);
        }
    }
}