using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Base.Exceptions
{
    public class CannotDeleteEntityException: AppException
    {
        public CannotDeleteEntityException()
        {
        }

        public CannotDeleteEntityException(string message) : base(message)
        {
        }

        public CannotDeleteEntityException(string message, Exception inner) : base(message, inner)
        {
        }

        public CannotDeleteEntityException(string message, string title) : base(message, title)
        {
        }

        public CannotDeleteEntityException(string message, string title, Exception inner) : base(message, title, inner)
        {
        }
    }
}
