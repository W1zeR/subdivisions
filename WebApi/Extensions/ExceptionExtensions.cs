using FluentValidation;
using WebApi.Exceptions;
using WebApi.Responses;

namespace WebApi.Extensions
{
    public static class ExceptionExtensions
    {
        public static ErrorResponse ToErrorResponse(this ValidationException data)
        {
            var res = new ErrorResponse()
            {
                Code = 400,
                Message = "Bad Request",
                Errors = data.Errors.Select(x =>
                {
                    return new Responses.Error()
                    {
                        Field = x.PropertyName,
                        Message = x.ErrorMessage
                    };
                })
            };

            return res;
        }

        public static ErrorResponse ToErrorResponse(this SubdivisionException data)
        {
            var res = new ErrorResponse()
            {
                Code = 500,
                Message = data.Message
            };

            return res;
        }

        public static ErrorResponse ToErrorResponse(this Exception data)
        {
            var res = new ErrorResponse()
            {
                Code = 500,
                Message = data.Message
            };

            return res;
        }
    }
}
