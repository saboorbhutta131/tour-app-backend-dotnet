using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.ControllerActions._Place;

namespace PlaceApp.Controllers;

[ApiController]
[Route("[controller]")]
public class PlaceController : ControllerBase
{
    private ApplicationDbContext _db;
    public PlaceController(ApplicationDbContext _dbContext)
    {
        this._db = _dbContext;
    }
    [HttpGet("{Id}")]
    public IActionResult Get(Guid Id)
    {
        Place _place = _db.Place.Find(Id);
        if (_place != null)
        {
            return Ok(new ViewPlace{
                Id = _place.Id,
                Title = _place.Title,
                Image = _place.Image,
                Description = _place.Description,
            });
        }
        return NotFound();
    }
    [HttpGet("")]
    public IActionResult GetAll()
    {
        List<ViewPlace> _places = _db.Place.Select(pl => 
            new ViewPlace{
                Id = pl.Id,
                Title = pl.Title,
                Image = pl.Image,
                Description = pl.Description,
            }
        ).ToList();
        return Ok(_places);
    }
    [HttpPost("create")]
    [Authorize(Roles = "Admin")]
    public IActionResult Create(CreatePlace _Place)
    {
        _db.Place.Add(new Place{
            Id = new Guid(),
            Title = _Place.Title,
            Description = _Place.Description,
            Image = _Place.Image
        });
        _db.SaveChanges();
        return Ok("Place Created Successfully");
    }
    [HttpPut("edit")]
    [Authorize(Roles = "Admin")]
    public IActionResult Edit(EditPlace _place)
    {
        var _existingPlace = _db.Place.FirstOrDefault(p => p.Id == _place.Id);
        if (_existingPlace != null)
        {
            _existingPlace.Title = _place.Title!=null ? _place.Title :  _existingPlace.Title;
            _existingPlace.Description = _place.Description!=null ? _place.Description :  _existingPlace.Description;
            _existingPlace.Image = _place.Image!=null ? _place.Image :  _existingPlace.Image;
            _db.SaveChanges();
            return Ok("Place updated successfully");
        }
        return NotFound();
    }
    [HttpDelete("{Id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult Delete(Guid Id)
    {
        Place placeToDelete = _db.Place.FirstOrDefault(pl => pl.Id == Id);
        if (placeToDelete != null)
        {
            _db.Place.Remove(placeToDelete);
            _db.SaveChanges();
            return Ok("Place deleted successfully");
        }
        return NotFound();
    }
}
