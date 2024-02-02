using Microsoft.AspNetCore.Mvc;
using Backend165.Services; // Importieren Sie den OrderService
using Backend165.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System;
using Backend165.DTOs;

namespace Backend165.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService; // Verwenden Sie den OrderService anstelle von MongoDbContext

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: api/Order
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                var orders = await _orderService.GetAllAsync(); // Verwenden Sie den OrderService
                return Ok(orders);
            }
            catch (Exception ex)
            {
                // Fehlerbehandlung: Loggen Sie den Fehler und geben Sie eine Fehlermeldung zurück
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/Order
        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody] OrderPostDto orderDto)
        {
            try
            {
                var order = new SkiService
                {
                    // Konvertieren Sie das DTO in Ihr Datenmodell
                    kundenname = orderDto.kundenname,
                    Email = orderDto.Email,
                    Telefon = orderDto.Telefon,
                    Priorität = orderDto.Priorität,
                    Dienstleistung = orderDto.Dienstleistung,
                    Status = orderDto.Status,
                };

                await _orderService.CreateAsync(order);
                return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/Order/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(string id)
        {
            try
            {
                var order = await _orderService.GetByIdAsync(id); // Verwenden Sie den OrderService
                if (order == null)
                {
                    return NotFound();
                }
                return Ok(order);
            }
            catch (Exception ex)
            {
                // Fehlerbehandlung: Loggen Sie den Fehler und geben Sie eine Fehlermeldung zurück
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/Order/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(string id, [FromBody] SkiService orderUpdate)
        {
            try
            {
                var order = await _orderService.GetByIdAsync(id); // Verwenden Sie den OrderService
                if (order == null)
                {
                    return NotFound();
                }

                // Hier werden nur die Felder aktualisiert, die im orderUpdate-Objekt übergeben wurden
                order.kundenname = orderUpdate.kundenname;
                order.Email = orderUpdate.Email;
                order.Telefon = orderUpdate.Telefon;
                order.Priorität = orderUpdate.Priorität;
                order.Dienstleistung = orderUpdate.Dienstleistung;
                order.Status = orderUpdate.Status;

                await _orderService.UpdateAsync(id, order); // Verwenden Sie den OrderService
                return NoContent();
            }
            catch (Exception ex)
            {
                // Fehlerbehandlung: Loggen Sie den Fehler und geben Sie eine Fehlermeldung zurück
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/Order/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(string id)
        {
            try
            {
                await _orderService.DeleteAsync(id); // Verwenden Sie den OrderService

                // Überprüfen Sie, ob das Löschen erfolgreich war
                // Hier gehe ich davon aus, dass Ihre DeleteAsync-Methode eine Ausnahme wirft, wenn das Löschen fehlschlägt.

                return NoContent();
            }
            catch (Exception ex)
            {
                // Fehlerbehandlung: Loggen Sie den Fehler und geben Sie eine Fehlermeldung zurück
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
