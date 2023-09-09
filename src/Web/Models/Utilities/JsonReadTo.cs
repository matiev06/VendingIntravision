using Newtonsoft.Json;
using VendingIntravision.Web.Exceptions;

namespace VendingIntravision.Web.Models.Utilities;

public static class JsonReadTo
{
    public static string ResponseMessageReadAsString(HttpResponseMessage AnswerM)
    {
        string HTTPresult;
        try
        {
            HTTPresult = AnswerM.Content.ReadAsStringAsync().Result;
        }
        catch
        {
            throw new Exception("Ошибка при чтении ответа");
        }
        if (string.IsNullOrEmpty(HTTPresult))
            throw new EmptyDataEx("Пустой ответ от сервера. ");
        return HTTPresult;
    }

    public static T Deserialize<T>(string json, T anonymousType)
    {
        try
        {
            return JsonConvert.DeserializeAnonymousType(json, anonymousType);
        }
        catch (Exception ex)
        {
            throw new ExData("Ошибка при обработке ответа. ", ex);
        }
    }

    public static T Deserialize<T>(string json)
    {
        try
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
        catch (Exception ex)
        {
            throw new ExData("Ошибка при обработке ответа. ", ex);
        }
    }
}
