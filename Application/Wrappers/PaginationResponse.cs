using System;

namespace Application.Wrappers
{
    public class PaginationResponse<T> : Response<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public PaginationResponse(T data, int pageNumber, int pageSize, int totalCount)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Data = data;
            Message = null;
            Succeeded = true;
            Errors = null;
            TotalCount = totalCount;
            TotalPages = (int)Math.Ceiling((TotalCount / (double)pageNumber));
        }
    }
}
