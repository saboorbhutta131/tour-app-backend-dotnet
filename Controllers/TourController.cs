using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.ControllerActions._Tour;

namespace PlaceApp.Controllers;

[ApiController]
[Route("[controller]")]
public class TourController : ControllerBase
{
    private ApplicationDbContext _db;
    public TourController(ApplicationDbContext _dbContext)
    {
        this._db = _dbContext;
    }
    [HttpGet("{Id}")]
    public IActionResult Get(Guid Id )
    {
        Tour _tour = _db.Tour.Include(t => t.Place).FirstOrDefault(t => t.Id == Id);
        if (_tour != null)
        {
            return Ok(_tour);
        }
        return NotFound();
    }
    [HttpGet("")]
    public IActionResult GetAll(Guid? PlaceId)
    {

        IQueryable<Tour> _tours = _db.Tour.Include(t => t.Place);
        if (PlaceId !=null){
            _tours = _tours.Where(t => t.PlaceId == PlaceId);
        }
        return Ok(_tours.ToList());
    }
    [HttpPost("create")]
    public IActionResult Create(CreateTour _Tour)
    {
        _db.Tour.Add(new Tour{
            Id = new Guid(),
            PlaceId = _Tour.PlaceId,
            Days = _Tour.Days,
            Price = _Tour.Price
        });
        _db.SaveChanges();
        return Ok("Tour Created Successfully");
    }
    [HttpPut("edit")]
    public IActionResult Edit(EditTour _tour)
    {
        var _existingTour = _db.Tour.FirstOrDefault(p => p.Id == _tour.Id);
        if (_existingTour != null)
        {
            _existingTour.Days = _tour.Days;
            _existingTour.PlaceId = _tour.PlaceId!= null ? _tour.PlaceId : _existingTour.PlaceId;
            _existingTour.Price = _tour.Price;
            _existingTour.TotalSeats = _tour.TotalSeats;
            _db.SaveChanges();
            return Ok("Tour Updated Successfully");
        }
        return NotFound();
    }
    [HttpDelete("{Id}")]
    public IActionResult Delete(Guid Id)
    {
        Tour tourToDelete = _db.Tour.FirstOrDefault(pl => pl.Id == Id);
        if (tourToDelete != null)
        {
            _db.Tour.Remove(tourToDelete);
            _db.SaveChanges();
        }
        return NotFound();
    }
}
