using Microsoft.AspNetCore.Mvc;

namespace FileManager.Tests.FileManagerWebTests
{
    public static class ActionResultExtensions
    {
        public static T GetValue<T>(this ActionResult<T> actionResult)
        {
            var objResult = actionResult.Result as ObjectResult;
            return (T)objResult.Value;
        }
    }
}
