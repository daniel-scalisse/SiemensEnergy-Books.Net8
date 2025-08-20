using AutoMapper;
using Books_Api.Controllers;
using Books_Api.Services;
using Books_Api.ViewModels;
using Books_Business.Core.Notifications;
using Books_Business.Modules.Authors;
using Books_Business.Modules.Books;
using Books_Business.Modules.Users;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Books_Api.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/authors")]
    public class AuthorsController : MainController
    {
        private readonly IAuthorRepository _repository;
        private readonly IAuthorService _service;

        private readonly IBookRepository _bookRepository;

        private readonly IMapper _mapper;

        public AuthorsController(
            IAuthorRepository repository,
            IAuthorService service,
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
        public async Task<PagedDTO<AuthorInput>> Get(int ps = 5, int p = 1, string q = null)
        {
            var result = await _repository.GetPaged(ps, p, q);

            return new PagedDTO<AuthorInput>
            {
                List = _mapper.Map<IEnumerable<AuthorInput>>(result.List),
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

            return Ok(new AuthorDetails
            {
                Author = vm,
                Books = new BookMap().FromBookToBookView(await _bookRepository.Search(b => b.AuthorId == id))
            });
        }

        [HttpGet("api/authors/getSummary")]
        public async Task<IEnumerable<KeyValuePair<int, string>>> GetSummary()
        {
            return await _repository.GetSummary();
        }

        [HttpPost]
        public async Task<ActionResult> Add(AuthorInput vm)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _service.Add(_mapper.Map<Author>(vm));

            //return CreatedAtRoute("DefaultApi", new { id = vm.Id }, vm);
            return CustomResponse(vm);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, AuthorInput vm)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (id != vm.Id)
            {
                NotifyError("The ID provided is not the same as the one registered.");
                return CustomResponse(vm);
            }

            await _service.Update(_mapper.Map<Author>(vm));

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

        private async Task<AuthorInput> GetById(int id)
        {
            return _mapper.Map<AuthorInput>(await _repository.GetById(id));
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