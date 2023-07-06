using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrgovinskaRadnja.Data.Model;
using TrgovinskaRadnja.Domain.Dtos;
using TrgovinskaRadnja.Services.Services;
using TrgovinskaRadnja.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Stripe;
using TrgovinskaRadnja.Domain.Enums;

namespace TrgovinskaRadnja.API.Controllers
{
    [Route("api/payment")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly PaymentService _paymentService;
        private readonly TrgovinskaRadnjaDataBaseContext _context;
        private readonly IConfiguration _config;

        public PaymentsController(PaymentService paymentService, TrgovinskaRadnjaDataBaseContext context, IConfiguration config)
        {
            _paymentService = paymentService;
            _context = context;
            _config = config;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<CartDto>> CreateOrUpdatePaymentIntent()
        {
            var basket = await _context.Carts
                .RetrieveCartWithItems(this.User.FindFirst("Id").Value)
                .FirstOrDefaultAsync();

            if (basket == null) return NotFound();

            var intent = await _paymentService.CreateOrUpdatePaymentIntent(basket);

            if (intent == null) return BadRequest(new ProblemDetails { Title = "Problem creating payment intent" });

            basket.PaymentIntentId = basket.PaymentIntentId ?? intent.Id;
            basket.ClientSecret = basket.ClientSecret ?? intent.ClientSecret;

            _context.Update(basket);

            var result = await _context.SaveChangesAsync() > 0;
            if (!result) return BadRequest(new ProblemDetails { Title = "Problem updating basket with intent" });

            return basket.MapCartToDto();
        }

        [HttpPost("webhook")]
        public async Task<ActionResult> StripeWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"],
                _config["StripeSettings:WhSecret"]);

            var charge = (Charge)stripeEvent.Data.Object;
            
            var order = await _context.ShopOrders.FirstOrDefaultAsync(x =>
                x.PaymentIntentId == charge.PaymentIntentId);

            if (charge.Status == "succeeded") order.OrderStatus = OrderStatus.PaymentReceived;

            await _context.SaveChangesAsync();

            return new EmptyResult();
        }
    }
}
