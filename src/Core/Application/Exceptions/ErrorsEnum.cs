using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Exceptions
{
    public enum ErrorCode
    {
        None,
        BadLoginOrPass,
        UserRegistered,
        UserNotExist,
        Bug,
        ProductNotFound,
    }
}
