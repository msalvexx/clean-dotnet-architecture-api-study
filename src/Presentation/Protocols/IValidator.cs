namespace Presentation.Protocols
{
    public interface IValidator
    {
        void Validate<T>(T input);
    }
}
