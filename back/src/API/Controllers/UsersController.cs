using Application.DTOs;
using Application.UseCases.Users;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UsersController : BaseController
{
    private readonly IGetUserUseCase _getUserUseCase;
    private readonly ICreateUserUseCase _createUserUseCase;
    private readonly IUpdateUserUseCase _updateUserUseCase;
    private readonly IUpdateUserPhotoUseCase _updateUserPhotoUseCase;
    private readonly IDeleteUserUseCase _deleteUserUseCase;

    public UsersController(
        IGetUserUseCase getUserUseCase,
        ICreateUserUseCase createUserUseCase,
        IUpdateUserUseCase updateUserUseCase,
        IUpdateUserPhotoUseCase updateUserPhotoUseCase,
        IDeleteUserUseCase deleteUserUseCase)
    {
        _getUserUseCase = getUserUseCase;
        _createUserUseCase = createUserUseCase;
        _updateUserUseCase = updateUserUseCase;
        _updateUserPhotoUseCase = updateUserPhotoUseCase;
        _deleteUserUseCase = deleteUserUseCase;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
    {
        var users = await _getUserUseCase.GetAllAsync();
        return Ok(users);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserDto>> GetById(Guid id)
    {
        var user = await _getUserUseCase.GetByIdAsync(id);
        return HandleResult(user);
    }

    [HttpGet("email/{email}")]
    public async Task<ActionResult<UserDto>> GetByEmail(string email)
    {
        var user = await _getUserUseCase.GetByEmailAsync(email);
        return HandleResult(user);
    }

    [HttpGet("city/{city}")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetByCity(string city)
    {
        var users = await _getUserUseCase.GetByCityAsync(city);
        return Ok(users);
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> Create(CreateUserDto createUserDto)
    {
        try
        {
            var user = await _createUserUseCase.ExecuteAsync(createUserDto);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<UserDto>> Update(Guid id, UpdateUserDto updateUserDto)
    {
        try
        {
            var user = await _updateUserUseCase.ExecuteAsync(id, updateUserDto);
            return HandleResult(user);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:guid}/photo")]
    public async Task<ActionResult<UserDto>> UpdatePhoto(Guid id, UpdateUserPhotoDto updateUserPhotoDto)
    {
        try
        {
            var user = await _updateUserPhotoUseCase.ExecuteAsync(id, updateUserPhotoDto);
            return HandleResult(user);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var deleted = await _deleteUserUseCase.ExecuteAsync(id);
        return HandleResult(deleted);
    }
} 