namespace IfCommerce.Core.Query
{
    public abstract class QueryParameters
    {
        private int _page = 1;
        public int Page
        {
            get
            {
                return _page;
            }
            set
            {
                _page = value < 1 ? _page : value;
            }
        }

        private int _size = 50;
        public int Size
        {
            get
            {
                return _size;
            }
            set
            {
                _size = value < 1 || value > _size ? _size : value;
            }
        }

        public string Order { get; set; }
    }
}
