using AutoMapper;
using Books_Api.Controllers;
using Books_Api.ViewModels;
using Books_Business.Core.Notifications;
using Books_Business.Modules.Authors;
using Books_Business.Modules.Books;
using Books_Business.Modules.Genders;
using Books_Business.Modules.Users;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Books_Api.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/books")]
    public class BooksController : MainController
    {
        private readonly IBookRepository _repository;
        private readonly IBookService _service;

        private readonly IGenderRepository _genderRepository;
        private readonly IAuthorRepository _authorRepository;

        private readonly IMapper _mapper;

        public BooksController(
            IBookRepository repository,
            IBookService service,
            IGenderRepository genderRepository,
            IAuthorRepository authorRepository,
            IMapper mapper,
            INotifier notifier,
            IUser appUser) : base(notifier, appUser)
        {
            _repository = repository;
            _service = service;

            _genderRepository = genderRepository;
            _authorRepository = authorRepository;

            _mapper = mapper;
        }

        //Listagem paginada.
        [HttpGet]
        public async Task<PagedDTO<BookView>> Get(int ps = 5, int p = 1, string q = null)
        {
            var result = await _repository.GetPaged(ps, p, q);

            List<BookView> books = new List<BookView>();
            foreach (var b in result.List)
            {
                books.Add(new BookView
                {
                    Id = b.Id,
                    GenderName = b.Gender.Name,
                    AuthorName = b.Author.Name,
                    Title = b.Title,
                    Subtitle = b.SubTitle,
                    Year = b.Year,
                    Edition = b.Edition,
                    PageQuantity = b.PageQuantity,
                    ISBN = b.ISBN,
                    Barcode = b.Barcode,
                    Dedication = b.Dedication,
                    Observation = b.Observation,
                    Value = b.Value,
                    PurchaseDate = b.PurchaseDate,

                    ImageUpload =
                        b.BookImage != null && b.BookImage.Image != null ?
                        String.Format("data:image/gif;base64,{0}", Convert.ToBase64String(b.BookImage.Image))
                        : null,

                    InclusionDate = b.InclusionDate
                });
            }

            return new PagedDTO<BookView>
            {
                List = books,
                Total = result.Total,
                Pages = result.Pages
            };
        }

        //Telas de detalhes e delete.
        [HttpGet("{id:int}")]
        public async Task<ActionResult> Get(int id)
        {
            var vm = await _repository.GetDetailsById(id);
            if (vm == null)
                return NotFound();

            return Ok(new BookView
            {
                Id = vm.Id,
                GenderName = vm.Gender.Name,
                AuthorName = vm.Author.Name,
                Title = vm.Title,
                Subtitle = vm.SubTitle,
                Year = vm.Year,
                Edition = vm.Edition,
                PageQuantity = vm.PageQuantity,
                ISBN = vm.ISBN,
                Barcode = vm.Barcode,
                Dedication = vm.Dedication,
                Observation = vm.Observation,
                Value = vm.Value,
                PurchaseDate = vm.PurchaseDate,

                ImageUpload =
                    vm.BookImage != null && vm.BookImage.Image != null ?
                        String.Format("data:image/gif;base64,{0}", Convert.ToBase64String(vm.BookImage.Image))
                        : null,

                InclusionDate = vm.InclusionDate
            });
        }

        //Tela de alteração.
        [HttpGet("getWithLists")]
        public async Task<ActionResult> GetWithLists(int id)
        {
            var vm = await GetById(id);
            if (vm == null)
                return NotFound();

            return Ok(new BookEdit
            {
                Book = vm,
                BookLists = await GetBookLists()
            });
        }

        //Tela de inclusão.
        [HttpGet("getLists")]
        public async Task<ActionResult> GetLists()
        {
            return Ok(await GetBookLists());
        }

        [HttpPost]
        public async Task<ActionResult> Add(BookInput vm)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (string.IsNullOrEmpty(vm.ImageUpload))
            {
                NotifyError("Provide an image for this book!");
                return CustomResponse(vm);
            }

            var book = Map(vm);
            book.BookImage = new BookImage { Image = Convert.FromBase64String(vm.ImageUpload) };

            await _service.Add(book);

            //return CreatedAtRoute("DefaultApi", new { id = vm.Id }, vm);
            return CustomResponse(vm);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, BookInput vm)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (id != vm.Id)
            {
                NotifyError("O ID fornecido não é o mesmo que o registrado!");
                return CustomResponse(vm);
            }

            var book = Map(vm);

            if (!string.IsNullOrEmpty(vm.ImageUpload))
            {
                book.BookImage = new BookImage
                {
                    Id = id,
                    Image = Convert.FromBase64String(vm.ImageUpload)
                };
            }

            await _service.Update(book);

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

        private async Task<BookInput> GetById(int id)
        {
            return _mapper.Map<BookInput>(await _repository.GetById(id));
        }

        private Book Map(BookInput vm)
        {
            var m = _mapper.Map<Book>(vm);
            m.Gender = null;
            m.Author = null;
            return m;
        }

        private async Task<BookLists> GetBookLists()
        {
            return new BookLists
            {
                Genders = await _genderRepository.GetSummary(),
                Authors = await _authorRepository.GetSummary()
            };
        }

        protected void Dispose(bool disposing)//override
        {
            if (disposing)
            {
                _repository?.Dispose();
                _service?.Dispose();

                _genderRepository?.Dispose();
                _authorRepository?.Dispose();
            }

            //base.Dispose(disposing);
        }
    }
}