namespace Ayx.AvalonSword.Serialization
{
    public interface IJson
    {
        T Parse<T>(string json) where T : class;
        dynamic ParseDynamic(string json);
        string ToJson<T>(T item);
    }
}
