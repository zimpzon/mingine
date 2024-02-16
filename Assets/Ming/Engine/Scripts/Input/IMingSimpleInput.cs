namespace Ming
{
    public interface IMingSimpleInput<T>
    {
        bool IsActive(T inputType);
    }
}
