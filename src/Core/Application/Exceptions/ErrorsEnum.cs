namespace Application.Exceptions
{
    public enum ErrorCode : byte
    {
        None,
        BadLoginOrPass,
        UserRegistered,
        UserNotExist,
        Bug,
        ProductNotFound,
        NotFound,
        InvalidToken
    }
}
