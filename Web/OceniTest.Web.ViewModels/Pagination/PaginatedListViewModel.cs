namespace OceniTest.Web.ViewModels.Pagination
{
    using System;
    using System.Collections.Generic;

    public class PaginatedListViewModel<T> : List<T>
    {
        public PaginatedListViewModel(IEnumerable<T> items, int count, int pageIndex, int pageSize)
        {
            this.PageIndex = pageIndex;
            this.TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            this.AddRange(items);
        }

        public int PageIndex { get; private set; }

        public int TotalPages { get; private set; }

        public bool HasPreviousPage => this.PageIndex > 1;

        public bool HasNextPage => this.PageIndex < this.TotalPages;
    }
}
