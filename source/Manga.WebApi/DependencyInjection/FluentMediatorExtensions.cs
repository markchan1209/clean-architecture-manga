namespace Manga.WebApi.DependencyInjection
{
    using Microsoft.Extensions.DependencyInjection;
    using FluentMediator;
    using Manga.Application.Boundaries.Register;
    using Manga.Application.Boundaries.Transfer;
    using Manga.Application.Boundaries.CloseAccount;
    using Manga.Application.Boundaries.Deposit;
    using Manga.Application.Boundaries.GetAccountDetails;
    using Manga.Application.Boundaries.GetCustomerDetails;
    using Manga.Application.Boundaries.Withdraw;

    public static class FluentMediatorExtensions
    {
        public static IServiceCollection AddMediator(this IServiceCollection services)
        {
            services.AddFluentMediator(
                builder =>
                {
                    builder.On<CloseAccountInput>().PipelineAsync()
                        .Call<Application.Boundaries.CloseAccount.IUseCase>((handler, request) => handler.Execute(request));

                     builder.On<DepositInput>().PipelineAsync()
                        .Call<Application.Boundaries.Deposit.IUseCase>((handler, request) => handler.Execute(request));

                    builder.On<GetAccountDetailsInput>().PipelineAsync()
                        .Call<Application.Boundaries.GetAccountDetails.IUseCase>((handler, request) => handler.Execute(request));

                    builder.On<GetCustomerDetailsInput>().PipelineAsync()
                        .Call<Application.Boundaries.GetCustomerDetails.IUseCase>((handler, request) => handler.Execute(request));

                    builder.On<RegisterInput>().PipelineAsync()
                        .Call<Application.Boundaries.Register.IUseCase>((handler, request) => handler.Execute(request));

                    builder.On<TransferInput>().PipelineAsync()
                        .Call<Application.Boundaries.Transfer.IUseCase>((handler, request) => handler.Execute(request));

                    builder.On<WithdrawInput>().PipelineAsync()
                        .Call<Application.Boundaries.Withdraw.IUseCase>((handler, request) => handler.Execute(request));
                });

            return services;
        }
    }
}