using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.ControllerActions._Review;

namespace PlaceApp.Controllers;

[ApiController]
[Route("[controller]")]
public class ReviewController : ControllerBase
{
    private ApplicationDbContext _db;
    public ReviewController(ApplicationDbContext _dbContext)
    {
        this._db = _dbContext;
    }
    [HttpGet("")]
    public IActionResult GetReviews(Guid? _tourId , Guid _userId)
    {
        IQueryable<Review> _reviews = _db.Review.Include(b => b.Tour).Include(b => b.User);
        if (_tourId != null){
            _reviews = _reviews.Where(r => r.TourId == _tourId);
        }
        if (_userId != null){
            _reviews = _reviews.Where(r => r.UserId == _userId);
    }
        
        return Ok(_reviews.ToList());
    }
    [HttpPost("create")]
    public IActionResult Create(CreateReview _Review)
    {
        
        _db.Review.Add(new Review{
            TourId = _Review.TourId,
            UserId = _Review.UserId,
            Rating = _Review.Rating,
            Comment = _Review.Comment
        });
        _db.SaveChanges();
        return Ok("Review Created Successfully");
    }
}
