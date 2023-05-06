using DesafioIlia.Application.Exceptions;

namespace DesafioIlia.Application.Validations
{
    public class ApplicationExceptionValidation : Exception
    {
        public enum ErrorType
        {
            Forbidden,
            BadRequest,
            Conflict
        }

        public ApplicationExceptionValidation(string error, ErrorType type) : base(error)
        {
        }

        public static void When(bool hasError, string error, ErrorType type)
        {
            if (hasError)
                switch (type)
                {
                    case ErrorType.Forbidden:
                        throw new ForbiddenException(error);
                    case ErrorType.BadRequest:
                        throw new BadRequestException(error);
                    case ErrorType.Conflict:
                        throw new ConflictException(error);
                }
        }
    }
}
