using api.Dtos;
using api.Interfaces;
using api.Models;
using api.Settings;
using MongoDB.Driver;

namespace api.Repositories;

public class AppUserAccountRepository : IAppUserAccountRepository
{
    #region dependenci injection
    //anjam dependency injection
    private const string _collectionName = "users";
    private readonly IMongoCollection<AppUser> _collection;
    // // private readonly IAppUserRepository _appUserRepository;
    // Dependency Injection
    public AppUserAccountRepository(IMongoClient client, IMongoDbSettings dbSettings)
    {
        var dbName = client.GetDatabase(dbSettings.DatabaseName);
        _collection = dbName.GetCollection<AppUser>(_collectionName);
        // _appUserRepository = appUserRepository;
    }
    #endregion

    public async Task<UserDto?> Create(RegisterDto userInput)
    {
        //Check email already exist or not
        bool emailExist = await _collection.Find<AppUser>(user => user.Email == userInput.Email.ToLower().Trim()).AnyAsync();

        if(emailExist == true)
        {
            return null;
        }
        //create obj
        AppUser appUser = new(
            Id: null,
            Email: userInput.Email.ToLower().Trim(),
            Password: userInput.Password,
            ConfirmPassword: userInput.ConfirmPassword
        );

        await _collection.InsertOneAsync(appUser);

        //create new obj for new type
        if (appUser.Id is not null)
        {
            UserDto userDto = new UserDto(
                Id: appUser.Id!,
                Email: userInput.Email
            );
            return userDto;
        }
        return null;
    }

    public Task<UserDto?> Create(UserDto userInput)
    {
        throw new NotImplementedException();
    }

    public async Task<UserDto?> Login(LoginDto userInput)
    {
        AppUser appUser = await _collection.Find<AppUser>(user =>
            user.Email == userInput.Email.ToLower().Trim()
            && user.Password == userInput.Password).FirstOrDefaultAsync();

        if (appUser is null)
            return null;

        if (appUser.Id is not null)
        {
            UserDto userDto = new UserDto(
                Id: appUser.Id,
                Email: appUser.Email
            );

            return userDto;
        }

        return null;
    }
}
