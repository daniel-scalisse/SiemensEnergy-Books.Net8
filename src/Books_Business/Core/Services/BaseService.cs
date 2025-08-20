using Books_Business.Core.Models;
using Books_Business.Core.Notifications;
using FluentValidation;
using FluentValidation.Results;

namespace Books_Business.Core.Services
{
    public abstract class BaseService
    {
        private readonly INotifier _notifier;

        public BaseService(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected void Notify(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notify(error.ErrorMessage);
            }
        }

        protected void Notify(string msg)
        {
            _notifier.Handle(new Notification(msg));
        }

        protected bool Validate<TV, TE>(TV validation, TE entidade) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = validation.Validate(entidade);

            if (validator.IsValid) return true;

            Notify(validator);

            return false;
        }
    }
}