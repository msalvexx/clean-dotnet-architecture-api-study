namespace Utils.Protocols
{
    public interface IValidator
    {
        void Validate<T>(T input);
    }
}
