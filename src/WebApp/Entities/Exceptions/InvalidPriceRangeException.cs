namespace Entities.Exceptions
{
    public class InvalidPriceRangeException : Exception
    {
        public InvalidPriceRangeException() : base("Максимальная цена не может быть меньше минимальной.")
        {

        }
    }
}
