using System.Diagnostics;
using InsightLogs.Domain.Entities;
using InsightLogs.Infrastructure.Persistence;
using InsightLogs.Application.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsightLogs.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ErrorLogsController : ControllerBase
{
    private readonly InsightLogsDbContext _context;

    public ErrorLogsController(InsightLogsDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Create(ErrorLog log)
    {
        _context.ErrorLogs.Add(log);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = log.Id }, log);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var log = await _context.ErrorLogs.FindAsync(id);
        if (log == null) return NotFound();

        return Ok(log);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] string? service,
        [FromQuery] string? severity,
        [FromQuery] DateTime? from,
        [FromQuery] DateTime? to,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        try
        {
            var query = _context.ErrorLogs.AsQueryable();

            if (!string.IsNullOrEmpty(service))
                query = query.Where(log => log.Service.Contains(service));

            if (!string.IsNullOrEmpty(severity))
                query = query.Where(log => log.Severity.ToLower() == severity.ToLower());

            if (from.HasValue)
                query = query.Where(log => log.Timestamp >= from.Value);

            if (to.HasValue)
                query = query.Where(log => log.Timestamp <= to.Value);

            var total = await query.CountAsync();

            var logs = await query
                .OrderByDescending(log => log.Timestamp)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(new
            {
                total,
                page,
                pageSize,
                data = logs
            });
        }
        catch (Exception error)
        {
            var info = ExceptionInfoExtractor.Extract(error);

            var fullMessage = $"[{info.Assembly}.{info.Namespace}.{info.Class}.{info.Method} @ line {info.Line}]\n" +
                  $"Archivo: {info.File}\n" +
                  $"Mensaje: {error.Message}";

            Console.WriteLine(fullMessage);

            var errorObj = new ErrorLog()
            {
                Id = Guid.NewGuid(),
                Message = error.Message,
                StackTrace = error.StackTrace,
                Timestamp = DateTime.Now,
                Service = error.TargetSite.Name,
                ReportedBy = "System",
                ErrorMetadataJson = fullMessage
            };

            await Create(errorObj);
            return StatusCode(500, "Ocurrió un error inesperado en el servidor.");
        }
    }


    [HttpPut("{id}/resolve")]
    public async Task<IActionResult> MarkAsResolved(Guid id)
    {
        var log = await _context.ErrorLogs.FindAsync(id);
        if (log == null) return NotFound();

        log.Resolved = true;
        await _context.SaveChangesAsync();

        return NoContent();
    }


}