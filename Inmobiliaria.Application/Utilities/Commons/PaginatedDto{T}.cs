using Inmobiliaria.Application.Abstractions.Commons;

namespace Inmobiliaria.Application.Utilities.Commons;

public class PaginatedDto<T> : IPaginated
{
    public IReadOnlyCollection<T> Data { get; set; } = Array.Empty<T>();
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}
