namespace IfCommerce.Catalog.Application.Parameters
{
    public abstract class QueryParameters
    {
        public int Page { get; set; }
        public int Size { get; set; }
        public string Order { get; set; }
    }
}
