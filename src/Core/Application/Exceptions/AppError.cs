using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Exceptions
{
    public class AppError : Exception
    {
        public ErrorCode Code { get; set; }

        public AppError(ErrorCode code, string message) : base(message)
        {
            Code = code;
        }

        public static AppError BadAuth => new AppError(ErrorCode.BadLoginOrPass, "Wrong login or password");
        public static AppError UserExist => new AppError(ErrorCode.UserRegistered, "User already registered");
        public static AppError UserNotFound => new AppError(ErrorCode.UserNotExist, "User not found");
        public static AppError Bug(string message = "BUG occured!") => new AppError(ErrorCode.Bug, message);
        public static AppError ProductNotFound => new AppError(ErrorCode.ProductNotFound, "Product not found");
    }
}
