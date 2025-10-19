namespace Olympics.Api.Dtos;

public record Paged<T>(int Page, int PageSize, int Total, IEnumerable<T> Items);
