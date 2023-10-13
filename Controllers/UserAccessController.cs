using api.Models;
using api.Settings;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserAccessController : ControllerBase
{
    #region dependenci injection
    //anjam dependency injection
    private const string _collectionName = "users";
    private readonly IMongoCollection<AppUser> _collection;
    // // private readonly IAppUserRepository _appUserRepository;
    // Dependency Injection
    public UserAccessController(IMongoClient client, IMongoDbSettings dbSettings)
    {
        var dbName = client.GetDatabase(dbSettings.DatabaseName);
        _collection = dbName.GetCollection<AppUser>(_collectionName);
        // _appUserRepository = appUserRepository;
    }
    #endregion
    
    #region update
    [HttpPut("update-by-email/{emailInput}")]
    public ActionResult<UpdateResult> UpdateByEmail(string emailInput, AppUser appUserInput)
    {
        //check if email exist
        // bool emailExist = _collection.Find<AppUser>(user => user.Email == emailInput.ToLower().Trim()).Any();

        // if (emailExist == false)
        // {
        //     return NotFound();
        // }
        var userUpdate = Builders<AppUser>.Update
        .Set(user => user.Email, appUserInput.Email.ToLower().Trim())
        .Set(user => user.Password, appUserInput.Password)
        .Set(user => user.ConfirmPassword, appUserInput.ConfirmPassword);

        UpdateResult updateResult = _collection.UpdateOne(user => user.Email == emailInput.ToLower().Trim(), userUpdate);

        return updateResult;
    }
    #endregion

    #region get-list
    [HttpGet("get-all")]
    public ActionResult<IEnumerable<AppUser>> GetAll()
    {
        List<AppUser> users = _collection.Find<AppUser>(new BsonDocument()).ToList();
        //tolist => ezafe kon on chizio ke peyda kardi be list
        return users;
    }
    #endregion
}
