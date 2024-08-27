using System;

namespace Application.Common;

public class ListResponseDto<T>
{
    public int Count { get; set; }
    public int Take { get; set; }
    public int Skip { get; set; }
    public T Data { get; set; }
}

public class SortingRequestFilter
{
    public string? Key { get; set; }
    public string? SortBy { get; set; }
}