using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Base.Exceptions
{
    public class AppException: ApplicationException
    {
        public string Title { get; protected set; }

        public AppException()
        {
        }

        public AppException(string message) : base(message)
        {
        }

        public AppException(string message, Exception inner) : base(message, inner)
        {
        }

        public AppException(string message, string title) : base(message)
        {
            Title = title;
        }

        public AppException(string message, string title, Exception inner) : base(message, inner)
        {
            Title = title;
        }
    }
}
