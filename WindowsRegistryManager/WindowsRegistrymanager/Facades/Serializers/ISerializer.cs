namespace WindowsRegistryManager.Facades.Serializers
{
    internal interface ISerializer<T>
    {
        T Serialize<U>(U objectToSerialize);
        U Deserialize<U>(T objectToDeserialize);
    }
}
