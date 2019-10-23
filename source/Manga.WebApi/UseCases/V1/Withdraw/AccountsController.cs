namespace Manga.WebApi.UseCases.V1.Withdraw
{
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;
    using FluentMediator;
    using Manga.Application.Boundaries.Withdraw;
    using Manga.Domain.ValueObjects;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [ApiVersion("1.0")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public sealed class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly WithdrawPresenter _presenter;

        public AccountsController(
            IMediator mediator,
            WithdrawPresenter presenter)
        {
            _mediator = mediator;
            _presenter = presenter;
        }

        /// <summary>
        /// Withdraw on an account
        /// </summary>
        /// <response code="200">The updated balance.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="500">Error.</response>
        /// <param name="request">The request to Withdraw.</param>
        /// <returns>The updated balance.</returns>
        [HttpPatch("Withdraw")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WithdrawResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Withdraw([FromBody][Required] WithdrawRequest request)
        {
            var message = new WithdrawInput(
                request.AccountId,
                new PositiveMoney(request.Amount)
            );
            await _mediator.PublishAsync(message);
            return _presenter.ViewModel;
        }
    }
}