using Books_Business.Core.Notifications;
using Books_Business.Core.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books_Business.Modules.Books
{
    public class BookService : BaseService, IBookService
    {
        private readonly IBookRepository _repository;

        public BookService(IBookRepository repository, INotifier notifier) : base(notifier)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<bool> Add(Book entity)
        {
            if (!entity.IsValid()) return false;

            if (await Existente(entity)) return false;

            await _repository.Add(entity);

            return true;
        }

        public async Task<bool> Update(Book entity)
        {
            if (!Validate(new BookValidation(), entity)) return false;

            if (await Existente(entity)) return false;

            await _repository.Update(entity);

            return true;
        }

        public async Task<bool> Remove(int id)
        {
            await _repository.Remove(id);

            return true;
        }

        private async Task<bool> Existente(Book entity)
        {
            var current = await _repository.Search(l =>
                (l.Title == entity.Title || l.ISBN == entity.ISBN || l.Barcode == entity.Barcode)
                &&
                l.Id != entity.Id);

            if (current.Count() == 0) return false;

            var book = (from b in current select b).FirstOrDefault();

            //Mais campos exigiria uma lógica melhor.
            List<string> fields = new List<string>();

            if (book.Title.Trim().ToUpper() == entity.Title.Trim().ToUpper())
                fields.Add("Title");

            if (book.ISBN.Trim().ToUpper() == entity.ISBN.Trim().ToUpper())
                fields.Add("ISBN");

            if (book.Barcode.Trim().ToUpper() == entity.Barcode.Trim().ToUpper())
                fields.Add("Barcode");

            switch (fields.Count)
            {
                case 1:
                    Notify($"There is already a Book with that {fields[0]}!");
                    break;
                case 2:
                    Notify($"There is already a Book with that {fields[0]} and {fields[1]}!");
                    break;
                case 3:
                    Notify($"There is already a Book with that {fields[0]}, {fields[1]} and {fields[2]}!");
                    break;
            }

            return true;
        }

        public void Dispose()
        {
            _repository?.Dispose();
        }
    }
}