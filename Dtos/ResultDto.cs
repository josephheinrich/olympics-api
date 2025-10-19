namespace Olympics.Api.Dtos;

public record ResultDto(
    int AthleteId,
    string AthleteName,
    string Noc,
    string Team,
    string Sport,
    string Event,
    int Year,
    string Season,
    string City,
    string? Medal
);
