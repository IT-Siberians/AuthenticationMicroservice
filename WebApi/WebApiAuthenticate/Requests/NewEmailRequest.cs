namespace WebApiAuthenticate.Requests;

/// <summary>
/// Запрос смены почты
/// </summary>
/// <param name="EmailValue">Email на который создается запрос на смену</param>
public record NewEmailRequest(string EmailValue);