namespace RDS.Interface
{
    interface IRDSValue<T> : IRDSObject
    {
        T Value { get; set; }
    }
}
