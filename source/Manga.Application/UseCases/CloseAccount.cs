namespace Manga.Application.UseCases
{
    using System.Threading.Tasks;
    using Manga.Application.Boundaries.CloseAccount;
    using Manga.Application.Repositories;
    using Manga.Domain.Accounts;

    public sealed class CloseAccount : IUseCase
    {
        private readonly IOutputPort _outputHandler;
        private readonly IAccountRepository _accountRepository;

        public CloseAccount(
            IOutputPort outputHandler,
            IAccountRepository accountRepository)
        {
            _outputHandler = outputHandler;
            _accountRepository = accountRepository;
        }

        public async Task Execute(CloseAccountInput closeAccountInput)
        {
            var account = await _accountRepository.Get(closeAccountInput.AccountId);

            if (account.IsClosingAllowed())
            {
                await _accountRepository.Delete(account);
            }

            BuildOutput(account);
        }

        private void BuildOutput(IAccount account)
        {
            var closeAccountOutput = new CloseAccountOutput(account);
            _outputHandler.Standard(closeAccountOutput);
        }
    }
}
