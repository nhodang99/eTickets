using eTickets.Data.Base;
using eTickets.Models;

namespace eTickets.Data.Services;

public class CinemasService(AppDbContext context) : EntityBaseRepository<Cinema>(context), ICinemasService
{
}