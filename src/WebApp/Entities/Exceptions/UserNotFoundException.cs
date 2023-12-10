namespace Entities.Exceptions
{
    public sealed class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException(int userId)
            : base($"The user with id = {userId} doesn't exists in the databse")
        { }
    }
}
