using Books_Business.Core.Notifications;
using Books_Business.Core.Services;
using Books_Business.Modules.Books;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books_Business.Modules.Genders
{
    public class GenderService : BaseService, IGenderService
    {
        private readonly IGenderRepository _repository;
        private readonly IBookRepository _bookRepository;

        public GenderService(IGenderRepository repository, IBookRepository bookRepository, INotifier notifier) : base(notifier)
        {
            _repository = repository;
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<Gender>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<bool> Add(Gender entity)
        {
            if (!entity.IsValid()) return false;

            if (await Exists(entity)) return false;

            await _repository.Add(entity);

            return true;
        }

        public async Task<bool> Update(Gender entity)
        {
            if (!Validate(new GenderValidation(), entity)) return false;

            if (await Exists(entity)) return false;

            await _repository.Update(entity);

            return true;
        }

        public async Task<bool> Remove(int id)
        {
            var qt = await _bookRepository.Count(l => l.GenderId == id);
            if (qt > 0)
            {
                Notify($"This Gender has {qt} Book{(qt > 1 ? "s" : "")}!");
                return false;
            }

            await _repository.Remove(id);

            return true;
        }

        private async Task<bool> Exists(Gender entity)
        {
            var current = await _repository.Search(a => a.Name == entity.Name && a.Id != entity.Id);

            if (current.Count() == 0) return false;

            Notify("There is already a Gender with that Name!");

            return true;
        }

        public void Dispose()
        {
            _repository?.Dispose();
            _bookRepository?.Dispose();
        }
    }
}