namespace HotelBackend.Exceptions
{
    public enum ErrorCode
    {
        NotFound = 404,
        BadRequest = 400,
        InternalServerError = 500,
        Unauthorized = 401,
        Conflict = 409,
        DatabaseError = 500
    }
}
