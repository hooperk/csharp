using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_Fiddling
{
    class BookingException : Exception
    {
        public byte ErrorCode { get; private set; }

        public BookingException(byte error)
        {
            ErrorCode = error;
        }

        public BookingException(byte error, string message)
            : base(message)
        {
            ErrorCode = error;
        }

        public BookingException(byte error, string message, Exception inner)
            : base(message, inner)
        {
            ErrorCode = error;
        }

        public String ErrorMessage()
        {
            string result = "Unknown Error";
            switch (ErrorCode)
            {
                case 1:
                    result = "File incorrect format or missing or dialog cancelled";
                    break;
                case 2:
                    result = "File open error or dialog cancelled";
                    break;
                case 3:
                    result = "File save error";
                    break;
                case 4:
                    result = "Invalid Date";
                    break;
            }
            return result;
        }
    }
}
