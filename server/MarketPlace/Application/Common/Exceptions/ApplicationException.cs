using System.Globalization;

namespace Application.Common.Exceptions;

public class ApplicationException : Exception
{
    public ApplicationException()
    {
    }

    public ApplicationException(string message) : base(message)
    {
    }

    public ApplicationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public ApplicationException(string message, params object[] args)
        : base(string.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
}