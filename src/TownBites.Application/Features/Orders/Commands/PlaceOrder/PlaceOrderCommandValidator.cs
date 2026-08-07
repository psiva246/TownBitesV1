using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TownBites.Application.Features.Orders.Commands.PlaceOrder
{
    public class PlaceOrderCommandValidator
    {

    }
    //public class PlaceOrderCommandValidator : AbstractValidator<PlaceOrderCommand>
    //{
    //    public PlaceOrderCommandValidator()
    //    {
    //        RuleFor(x => x.RestaurantId)
    //            .GreaterThan(0);

    //        RuleFor(x => x.CustomerId)
    //            .GreaterThan(0);

    //        RuleFor(x => x.Items)
    //            .NotEmpty();

    //        RuleForEach(x => x.Items)
    //            .ChildRules(item =>
    //            {
    //                item.RuleFor(i => i.MenuItemId).GreaterThan(0);
    //                item.RuleFor(i => i.Quantity).GreaterThan(0);
    //            });
    //    }
    //}
}
