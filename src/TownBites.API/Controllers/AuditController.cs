using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TownBites.Infrastructure.Data;

namespace TownBites.API.Controllers;

[ApiController]
[Authorize(Roles = "Admin")]
[Route("api/audit")]
public class AuditController : ControllerBase
{
    private readonly ApplicationDbContext _db;

    public AuditController(ApplicationDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var logs = await _db.AuditLogs
            .OrderByDescending(x => x.CreatedOn)
            .Take(200)
            .ToListAsync();

        return Ok(logs);
    }
}