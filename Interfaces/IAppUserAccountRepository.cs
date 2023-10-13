using api.Dtos;
using api.Models;

namespace api.Interfaces;

public interface IAppUserAccountRepository
{
    public Task<UserDto?> Create(UserDto userInput);
    public Task<UserDto?> Login(LoginDto userInput);
}
