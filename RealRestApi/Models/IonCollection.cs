namespace RealRestApi.Models
{
    public class IonCollection<T> : IonResource
    {
        public T[] Items { get; set; }
    }
}
