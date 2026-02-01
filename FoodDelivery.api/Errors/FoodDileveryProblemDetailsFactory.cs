
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.ModelBinding;
//using Microsoft.Extensions.Options;

//namespace FoodDelivery.api.Errors
//{
//    public class FoodDeliveryProblemDetailsFactory(IOptions<ApiBehaviorOptions> _options) : Microsoft.AspNetCore.Mvc.Infrastructure.ProblemDetailsFactory
//    {


//        public override ProblemDetails CreateProblemDetails(
//            HttpContext httpContext,
//            int? statusCode = null,
//            string? title = null,
//            string? type = null,
//            string? detail = null,
//            string? instance = null)
//        {
//            statusCode ??= StatusCodes.Status500InternalServerError;

//            var problemDetails = new ProblemDetails
//            {
//                Status = statusCode,
//                Title = title,
//                Type = type,
//                Detail = detail,
//                Instance = instance
//            };

//            ApplyDefaults(httpContext, problemDetails, statusCode.Value);

//            return problemDetails;
//        }

//        public override ValidationProblemDetails CreateValidationProblemDetails(
//            HttpContext httpContext,
//            ModelStateDictionary modelStateDictionary,
//            int? statusCode = null,
//            string? title = null,
//            string? type = null,
//            string? detail = null,
//            string? instance = null)
//        {
//            statusCode ??= StatusCodes.Status400BadRequest;

//            var problemDetails = new ValidationProblemDetails(modelStateDictionary)
//            {
//                Status = statusCode,
//                Title = title,
//                Type = type,
//                Detail = detail,
//                Instance = instance
//            };

//            ApplyDefaults(httpContext, problemDetails, statusCode.Value);

//            return problemDetails;
//        }

//        private void ApplyDefaults(
//            HttpContext httpContext,
//            ProblemDetails problemDetails,
//            int statusCode)
//        {
//            problemDetails.Status ??= statusCode;

//            if (_options.Value.ClientErrorMapping
//    .TryGetValue(statusCode, out var clientError))
//            {
//                problemDetails.Title ??= clientError.Title;
//                problemDetails.Type ??= clientError.Link;
//            }

//            problemDetails.Instance ??= httpContext.Request.Path;
//            problemDetails.Extensions.Add("customInfo", "FoodDelivery API Error");
//        }
//    }
//}
      
    