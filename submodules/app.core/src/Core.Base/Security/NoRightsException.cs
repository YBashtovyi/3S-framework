using System;
using System.Collections.Generic;
using System.Text;
using Core.Base.Exceptions;

namespace Core.Security
{
    public class NoRightsException: AppException
    {
        public NoRightsException()
        {
        }

        public NoRightsException(string message) : base(message)
        {
        }

        public NoRightsException(string message, Exception inner) : base(message, inner)
        {
        }

        public NoRightsException(string message, string title) : base(message)
        {
            Title = title;
        }

        public NoRightsException(string message, string title, Exception inner) : base(message, inner)
        {
            Title = title;
        }
    }
}
