﻿namespace Application.Request
{
    public class PaginationRequest
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public PaginationRequest()
        {
            this.PageNumber = 0;
            this.PageSize = 10;
        }
        public PaginationRequest(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber < 0 ? 0 : pageNumber;
            this.PageSize = pageSize > 10 ? 10 : pageSize;
        }
    }
}
