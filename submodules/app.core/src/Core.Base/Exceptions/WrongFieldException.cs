using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Base.Exceptions
{
    public class WrongFieldException: AppException
    {
        public WrongFieldException()
        {
        }

        public WrongFieldException(string message) : base(message)
        {
        }

        public WrongFieldException(string message, Exception inner) : base(message, inner)
        {
        }

        public WrongFieldException(string message, string title) : base(message, title)
        {
        }

        public WrongFieldException(string message, string title, Exception inner) : base(message, title, inner)
        {
        }
    }
}
