using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.ControllerActions._Booking;
using Models.ControllerActions._Place;

namespace PlaceApp.Controllers;

[ApiController]
[Route("[controller]")]
public class BookingController : ControllerBase
{
    private ApplicationDbContext _db;
    public BookingController(ApplicationDbContext _dbContext)
    {
        this._db = _dbContext;
    }
    [HttpGet("")]
    public IActionResult GetBookings(Guid? _tourId , Guid _userId)
    {
        IQueryable<Booking> _bookings = _db.Booking.Include(b => b.Tour).Include(b => b.User);
        if (_tourId != null){
            _bookings = _bookings.Where(b => b.TourId == _tourId);
        }
        if (_userId != null){
            _bookings = _bookings.Where(b => b.UserId == _userId);
    }
        
        return Ok(_bookings.ToList());
    }
    [HttpPost("create")]
    [Authorize(Roles = "Admin")]
    public IActionResult Create(CreateBooking _Booking)
    {
        Tour  _tour  = _db.Tour.Find(_Booking.TourId);
        if (_tour  == null){
            return BadRequest("Invalid Data For Booking");
        }
        int _PrevBookings = _db.Booking.Where(b => b.TourId  == _Booking.TourId).Count();
        if (_Booking.Seats > (_tour.TotalSeats - _PrevBookings)){
            return BadRequest("Seats Not Available");
        }
        _db.Booking.Add(new Booking{
            TourId = _Booking.TourId,
            UserId = _Booking.UserId,
            PaymentId = 0,
            Seats = _Booking.Seats
        });
        _db.SaveChanges();
        return Ok("Booking Created Successfully");
    }
}
