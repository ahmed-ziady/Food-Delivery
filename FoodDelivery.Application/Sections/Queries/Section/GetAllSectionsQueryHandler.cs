using FoodDelivery.Application.Common.Exceptions;
using FoodDelivery.Application.Common.Interfaces.Persistence;
using FoodDelivery.Application.Menus.Queries.Section;
using FoodDelivery.Application.Sections.Common;
using MediatR;

namespace FoodDelivery.Application.Sections.Queries.Section
{
    public sealed class GetAllSectionsQueryHandler(IMenuRepository menuRepository) : IRequestHandler<GetAllSectionsQuery, IReadOnlyList<SectionResult>>
    {
        public async Task<IReadOnlyList<SectionResult>> Handle(GetAllSectionsQuery request, CancellationToken cancellationToken)
        {
            var restuarant = await menuRepository.GetByRestaurantIdAsync(request.RestaurantId, cancellationToken)
                 ?? throw new NotFoundException("Resuarant.NotFound", "Restuarant is not founded");
            var sections = restuarant.Sections.Select(i => new SectionResult(i.Id, i.Name)).OrderBy(i => i.Name).ToList();
            return sections;
        }
    }
}
