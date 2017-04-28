namespace RealRestApi.Models
{
    public class Collection<T> : Resource
    {
        public T[] Items { get; set; }
    }
}
