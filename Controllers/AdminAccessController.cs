using api.Models;
using api.Settings;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdminAccessController : ControllerBase
{
    #region dependenci injection
    //anjam dependency injection
    private const string _collectionName = "users";
    private readonly IMongoCollection<AppUser> _collection;
    // // private readonly IAppUserRepository _appUserRepository;
    // Dependency Injection
    public AdminAccessController(IMongoClient client, IMongoDbSettings dbSettings)
    {
        var dbName = client.GetDatabase(dbSettings.DatabaseName);
        _collection = dbName.GetCollection<AppUser>(_collectionName);
        // _appUserRepository = appUserRepository;
    }
    #endregion
    
    #region delete
    [HttpDelete("delete-by-id/{idInput}")]
    public ActionResult<DeleteResult> DeleteById(string idInput)
    {
        DeleteResult deleteResult = _collection.DeleteOne<AppUser>(user => user.Id == idInput);
        
        return deleteResult;
    }
    #endregion
}
