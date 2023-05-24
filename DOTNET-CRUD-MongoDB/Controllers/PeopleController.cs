using DOTNET_CRUD_MongoDB.Collection;
using DOTNET_CRUD_MongoDB.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DOTNET_CRUD_MongoDB.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class PeopleController : ControllerBase
{
    private readonly IPeopleRepository _ipeopleRepository;
    public PeopleController(IPeopleRepository ipeopleRepository)
    {
        _ipeopleRepository = ipeopleRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetPeoples()
    {
        var people = await _ipeopleRepository.GetAllAsync();
        return Ok(people);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetPeople(string id)
    {
        var people = await _ipeopleRepository.GetByIdAsync(id);
        if (people == null)
        {
            return NotFound();
        }

        return Ok(people);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePeople(People newPeople)
    {
        await _ipeopleRepository.CreateNewPeopleAsync(newPeople);
        return CreatedAtAction(nameof(GetPeople), new { id = newPeople.Id }, newPeople);
    }

    [HttpPut]
    public async Task<IActionResult> UpdatePeople(People updatePeople)
    {
        var people = await _ipeopleRepository.GetByIdAsync(updatePeople.Id);
        if (people == null)
        {
            return NotFound();
        }

        await _ipeopleRepository.UpdatePeopleAsync(updatePeople);
        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> DeletePeople(string id)
    {
        var people = await _ipeopleRepository.GetByIdAsync(id);
        if (people == null)
        {
            return NotFound();
        }

        await _ipeopleRepository.DeletePeopleAsync(id);
        return NoContent();
    }
}
