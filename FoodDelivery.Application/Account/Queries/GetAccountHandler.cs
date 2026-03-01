using FoodDelivery.Application.Account.Common;
using FoodDelivery.Application.Common.Interfaces.Persistence;
using FoodDelivery.Application.Common.Mapping;
using MediatR;

namespace FoodDelivery.Application.Account.Queries;

public sealed class GetAccountHandler(IUserRepository userRepository) : IRequestHandler<GetAccountQuery, AccountResult>
{
    public async Task<AccountResult> Handle(GetAccountQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken)?? throw new UnauthorizedAccessException("Access Denied");

        return user.ToAccountResult();
    }
}
