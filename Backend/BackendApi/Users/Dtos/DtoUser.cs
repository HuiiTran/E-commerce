﻿namespace UsersApi.Dtos
{
    public record UserDto(
        Guid Id,
        string? UserName,
        string? Password,
        string? Email,
        bool IsEmailConfirmed,
        int? ConfirmedCode,
        string? FullName,
        List<string>? PhoneNumber,
        List<string>? Address,
        List<Guid>? BoughtProducts,
        bool isDeleted,
        DateTimeOffset CreatedDate,
        DateTimeOffset LatestUpdatedDate,
        string Role
        );
    public record CreateUserDto(
        string? UserName,
        string? Password,
        string? Email,
        string? FullName,
        List<string>? PhoneNumber,
        List<string>? Address
        );
    public record UpdateUserDto(
        string? UserName,
        string? Password,
        string? Email,
        bool IsEmailConfirmed,
        string? FullName,
        List<string>? PhoneNumber,
        List<string>? Address,
        List<Guid>? BoughtProducts,
        bool isDeleted
        );
    public record ChangePasswordDto(
        string OldPassword,
        string newPassword
        );
}
