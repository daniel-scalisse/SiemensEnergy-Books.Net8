using Books_Business.Core.Notifications;
using Books_Business.Core.Services;
using Books_Business.Modules.Books;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books_Business.Modules.Authors
{
    public class AuthorService : BaseService, IAuthorService
    {
        private readonly IAuthorRepository _repository;
        private readonly IBookRepository _bookRepository;

        public AuthorService(IAuthorRepository repository, IBookRepository bookRepository, INotifier notifier) : base(notifier)
        {
            _repository = repository;
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<Author>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<bool> Add(Author entity)
        {
            if (!entity.IsValid()) return false;

            if (await Existente(entity)) return false;

            await _repository.Add(entity);

            return true;
        }

        public async Task<bool> Update(Author entity)
        {
            if (!Validate(new AuthorValidation(), entity)) return false;

            if (await Existente(entity)) return false;

            await _repository.Update(entity);

            return true;
        }

        public async Task<bool> Remove(int id)
        {
            var qt = await _bookRepository.Count(l => l.AuthorId == id);
            if (qt > 0)
            {
                Notify($"This Author has {qt} Book{(qt > 1 ? "s" : "")}!");
                return false;
            }

            await _repository.Remove(id);

            return true;
        }

        private async Task<bool> Existente(Author entity)
        {
            var current = await _repository.Search(a => a.Name == entity.Name && a.Id != entity.Id);

            if (current.Count() == 0) return false;

            Notify("There is already an Author with that name!");

            return true;
        }

        public void Dispose()
        {
            _repository?.Dispose();
            _bookRepository?.Dispose();
        }
    }
}