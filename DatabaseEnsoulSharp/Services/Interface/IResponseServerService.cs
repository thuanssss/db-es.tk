namespace DatabaseEnsoulSharp.Services.Interface
{
    public interface IResponseServerService
    {
        ResponseServerService ResponseError(string message = null);

        ResponseServerService ResponseSuccess(string message = null, dynamic data = null);
    }
}