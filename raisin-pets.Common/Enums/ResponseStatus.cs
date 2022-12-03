namespace raisin_pets.Common.Enums;

[Flags]
public enum ResponseStatus : byte
{
    Success = 1,
    Failed = Success << 2,
    InvalidUserId = Success << 3
}