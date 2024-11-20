using Microsoft.AspNetCore.Mvc;
using System.Linq;

[ApiController]
[Route("api/[controller]")]
public class BloodBankController : ControllerBase
{
    private static List<BloodBankEntry> _bloodBankEntries = new();
    private static int _nextId = 1;

    // POST: Create new entry
    [HttpPost]
    public IActionResult CreateEntry([FromBody] BloodBankEntry entry)
    {
        entry.Id = _nextId++;
        _bloodBankEntries.Add(entry);
        return CreatedAtAction(nameof(GetEntryById), new { id = entry.Id }, entry);
    }

    // GET: Get all entries with pagination, filtering, and sorting
    [HttpGet]
    public IActionResult GetAllEntries(
        [FromQuery] int? page,
        [FromQuery] int? size,
        [FromQuery] string? bloodType,
        [FromQuery] string? status,
        [FromQuery] string? donorName,
        [FromQuery] string? sortBy,
        [FromQuery] bool descending = false)
    {
        var entries = _bloodBankEntries.AsQueryable();

        // Filtering
        if (!string.IsNullOrEmpty(bloodType))
            entries = entries.Where(e => e.BloodType.Equals(bloodType, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrEmpty(status))
            entries = entries.Where(e => e.Status.Equals(status, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrEmpty(donorName))
            entries = entries.Where(e => e.DonorName.Contains(donorName, StringComparison.OrdinalIgnoreCase));

        // Sorting
        entries = sortBy switch
        {
            "BloodType" => descending ? entries.OrderByDescending(e => e.BloodType) : entries.OrderBy(e => e.BloodType),
            "CollectionDate" => descending ? entries.OrderByDescending(e => e.CollectionDate) : entries.OrderBy(e => e.CollectionDate),
            "DonorName" => descending ? entries.OrderByDescending(e => e.DonorName) : entries.OrderBy(e => e.DonorName),
            _ => entries 
        };

        // Pagination
        if (page.HasValue && size.HasValue)
        {
            entries = entries.Skip((page.Value - 1) * size.Value).Take(size.Value);
        }

        return Ok(entries.ToList());
    }

    // GET: Get entry by Id
    [HttpGet("{id}")]
    public IActionResult GetEntryById(int id)
    {
        var entry = _bloodBankEntries.FirstOrDefault(e => e.Id == id);
        if (entry == null) return NotFound("Entry not found");
        return Ok(entry);
    }

    // PUT: Update entry
    [HttpPut("{id}")]
    public IActionResult UpdateEntry(int id, [FromBody] BloodBankEntry updatedEntry)
    {
        var entry = _bloodBankEntries.FirstOrDefault(e => e.Id == id);
        if (entry == null) return NotFound("Entry not found");

        entry.DonorName = updatedEntry.DonorName;
        entry.Age = updatedEntry.Age;
        entry.BloodType = updatedEntry.BloodType;
        entry.ContactInfo = updatedEntry.ContactInfo;
        entry.Quantity = updatedEntry.Quantity;
        entry.CollectionDate = updatedEntry.CollectionDate;
        entry.ExpirationDate = updatedEntry.ExpirationDate;
        entry.Status = updatedEntry.Status;

        return Ok(entry);
    }

    // DELETE: Delete entry
    [HttpDelete("{id}")]
    public IActionResult DeleteEntry(int id)
    {
        var entry = _bloodBankEntries.FirstOrDefault(e => e.Id == id);
        if (entry == null) return NotFound("Entry not found");

        _bloodBankEntries.Remove(entry);
        return Ok("Deleted donor data.");
    }

    // GET: Search by multiple criteria
    [HttpGet("search")]
    public IActionResult Search([FromQuery] string? bloodType, [FromQuery] string? status, [FromQuery] string? donorName)
    {
        var results = _bloodBankEntries.AsQueryable();

        if (!string.IsNullOrEmpty(bloodType))
            results = results.Where(e => e.BloodType.Equals(bloodType, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrEmpty(status))
            results = results.Where(e => e.Status.Equals(status, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrEmpty(donorName))
            results = results.Where(e => e.DonorName.Contains(donorName, StringComparison.OrdinalIgnoreCase));

        return Ok(results.ToList());
    }
}
