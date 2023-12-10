namespace Entities.Exceptions
{
    public class TooManyImagesCountException : Exception
    {
        public TooManyImagesCountException(int maxImagesCount) :
            base($"You cannot add more than {maxImagesCount} images to one product.")
        {

        }
    }
}
