namespace Olympics.Api.Infrastructure;

public record Query(int Page = 1, int PageSize = 50)
{
    public int Skip => Math.Max(0, (Page - 1) * PageSize);
    public int Take => Math.Clamp(PageSize, 1, 200);
}
