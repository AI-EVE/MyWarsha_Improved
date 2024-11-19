namespace Utils.PageUtils
{
    public class PaginationPropreties
    {
        private int _pageNumber;
        private int _pageSize;

        public int PageNumber
        {
            get => _pageNumber;
            set => _pageNumber = value >= 0 ? value : 0;
        }

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value >= 0 ? value : 0;
        }

        public int Skip()
        {
            return (PageNumber - 1) * PageSize;
        }

        public IQueryable<T> ApplyPagination<T>(IQueryable<T> query)
        {
            if (PageSize > 0  && PageNumber > 0)
            {
                return query.Skip(Skip()).Take(PageSize);
            }
            return query;
        }
    }
}